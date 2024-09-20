using System;
using System.Security.Cryptography;
using System.Text;
using System.Net;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using System.IO;

namespace ExperimentalGoal.AI_Images
{
    public static class TestImage
    {
        public static string GenerateImage(string age, string race, string gender)
        {
            string filename = "avatar" + DateTime.Now.ToString("MMddyyyHHmmssfff") + ".png";
            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, filename);

            // Generate a unique hash based on user properties
            var hash = CalculateHash(age, race, gender);

            // Generate Gravatar URL based on the hash
            var gravatarUrl = $"https://www.gravatar.com/avatar/{hash}?d=identicon&s=128";

            // Download the Gravatar image
            using (var webClient = new WebClient())
            {
                var imageData = webClient.DownloadData(gravatarUrl);
                using (var stream = new MemoryStream(imageData))
                {
                    // Load the image from the downloaded data using ImageSharp
                    using (var image = Image.Load<Rgba32>(stream))
                    {
                        // Save the image using ImageSharp
                        image.Save(filePath); // Automatically selects the format based on the file extension
                    }
                }
            }

            return filePath;
        }

        public static string CalculateHash(string age, string race, string gender)
        {
            // Concatenate user properties into a single string
            var userProperties = $"{age}{race}{gender}";

            // Compute the MD5 hash of the concatenated string
            using (var md5 = MD5.Create())
            {
                var inputBytes = Encoding.ASCII.GetBytes(userProperties);
                var hashBytes = md5.ComputeHash(inputBytes);

                // Convert the byte array to a hexadecimal string
                var sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("X2"));
                }
                return sb.ToString().ToLower();
            }
        }
    }
}