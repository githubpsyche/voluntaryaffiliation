using System;
using System.IO;
using System.Linq;
using RazorPagesDemo.Models;
using ExperimentalGoal.Models;
using System.Globalization;

namespace ExperimentalGoal
{
    public class DatabasePopulator
    {
        private readonly DatabaseContext _context;

        public DatabasePopulator(DatabaseContext context)
        {
            _context = context;
        }

        public void PopulatePlayersFromImages(string directoryPath)
        {
            // Define the mappings for demographic and gender replacements
            var raceMapping = new Dictionary<string, string>
            {
                { "asian", "East-Southeast Asian" },  // Modify as per your exact categories
                { "black", "Black" },
                { "latino", "Hispanic-Latino-a" }
            };

            var genderMapping = new Dictionary<string, string>
            {
                { "female", "woman" },
                { "male", "man" }
            };

            // Get all image files in the directory (jpg and png)
            var imageFiles = Directory.GetFiles(directoryPath, "*.jpg")
                            .Concat(Directory.GetFiles(directoryPath, "*.png"));

            foreach (var filePath in imageFiles)
            {
                // Extract the filename without the extension
                var fileName = Path.GetFileNameWithoutExtension(filePath);
                var extension = Path.GetExtension(filePath);

                // Split the filename into parts (race, gender, age)
                var parts = fileName.Split('_');
                if (parts.Length != 3)
                {
                    Console.WriteLine($"Skipping invalid file name: {fileName}");
                    continue;  // Skip any files that don't follow the expected pattern
                }

                // Map the race, gender, and age based on the mappings
                string race = parts[0];
                string gender = parts[1];
                string age = parts[2];

                // Apply race mapping
                if (raceMapping.ContainsKey(race))
                {
                    race = raceMapping[race];
                }

                // Apply gender mapping
                if (genderMapping.ContainsKey(gender))
                {
                    gender = genderMapping[gender];
                }

                // Construct the new filename
                var newFileName = $"{race}_{gender}_{age}{extension}";
                var newFilePath = Path.Combine(directoryPath, newFileName);

                // Rename the file
                File.Move(filePath, newFilePath);
                Console.WriteLine($"Renamed {filePath} to {newFilePath}");

                // Check if the player already exists in the database
                bool playerExists = _context.Player.Any(p =>
                    p.Race == race &&
                    p.Gender == gender &&
                    p.Age == age &&
                    p.Picture == newFileName
                );

                if (!playerExists)
                {
                    // Create a new Player object
                    var player = new Players
                    {
                        Race = race,
                        Gender = gender,
                        Age = age,
                        Picture = newFileName  // Save just the filename, not the full path
                    };

                    // Add the player to the database context
                    _context.Player.Add(player);
                }
            }

            // Save all changes to the database
            _context.SaveChanges();
        }

        // Helper method to capitalize the first letter of each word
        private string CapitalizeWords(string input)
        {
            if (string.IsNullOrEmpty(input))
                return input;

            // Split the string into words, capitalize each word, and join them back
            return string.Join('-', input.Split('-').Select(word =>
                CultureInfo.CurrentCulture.TextInfo.ToTitleCase(word.ToLower())));
        }
    }
}
