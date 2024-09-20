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
            // Get all image files in the directory
            var imageFiles = Directory.GetFiles(directoryPath, "*.jpg")
                            .Concat(Directory.GetFiles(directoryPath, "*.png"));

            foreach (var filePath in imageFiles)
            {
                // Extract the filename without extension
                var fileName = Path.GetFileNameWithoutExtension(filePath);

                // Split the filename into parts
                var parts = fileName.Split('_');
                if (parts.Length != 3)
                {
                    Console.WriteLine($"Skipping invalid file name: {fileName}");
                    continue;  // Skip any files that don't follow the expected pattern
                }

                var race = CapitalizeWords(parts[0]);
                var gender = CapitalizeWords(parts[1]);
                var age = parts[2];

                // Check if the player already exists in the database
                bool playerExists = _context.Player.Any(p => 
                    p.Race == race && 
                    p.Gender == gender && 
                    p.Age == age && 
                    p.Picture == Path.GetFileName(filePath)
                );

                if (!playerExists)
                {
                    // Create a new Player object
                    var player = new Players
                    {
                        Race = race,
                        Gender = gender,
                        Age = age,
                        Picture = Path.GetFileName(filePath)  // Save just the filename, not the full path
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