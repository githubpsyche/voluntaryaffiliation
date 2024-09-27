TASKS
1. update demographic form - DONE
2. update demographics info for current team members
3. add additional team members for the new demos and current demos to broaden database
4. enforce a stable distribution of possible teammates across trials


Current demographic characteristic options:
    - Asian-Pacific Islander
    - African American
    - Latinx
    - Indigenous
    - Other

```javascript
using System;
using System.IO;
using System.Collections.Generic;

public class ImageRenamer
{
    public void RenameImages(string directoryPath)
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

        // Get all image files (jpg and png)
        var imageFiles = Directory.GetFiles(directoryPath, "*.jpg")
                        .Concat(Directory.GetFiles(directoryPath, "*.png"));

        foreach (var filePath in imageFiles)
        {
            // Extract the filename without the extension
            var fileName = Path.GetFileNameWithoutExtension(filePath);
            var extension = Path.GetExtension(filePath);

            // Split the filename by underscore
            var parts = fileName.Split('_');

            if (parts.Length != 3)
            {
                Console.WriteLine($"Skipping invalid file name: {fileName}");
                continue; // Skip any files that don't match the expected pattern
            }

            // Apply race mapping
            string race = parts[0];
            if (raceMapping.ContainsKey(race))
            {
                race = raceMapping[race];
            }

            // Apply gender mapping
            string gender = parts[1];
            if (genderMapping.ContainsKey(gender))
            {
                gender = genderMapping[gender];
            }

            // Age remains unchanged
            string age = parts[2];

            // Construct the new filename
            var newFileName = $"{race}_{gender}_{age}{extension}";

            // Construct the full path of the new file
            var newFilePath = Path.Combine(directoryPath, newFileName);

            // Rename the file
            File.Move(filePath, newFilePath);
            Console.WriteLine($"Renamed {filePath} to {newFilePath}");
        }
    }
}
```

Census designations: 
    Black or African American.  A person having origins in any of the Black racial groups of Africa. It includes people who indicate their race as "Black or African American," or report responses such as African American, Jamaican, Haitian, Nigerian, Ethiopian, or Somali. The category also includes groups such as Ghanaian, South African, Barbadian, Kenyan, Liberian, Bahamian, etc.

    American Indian and Alaska Native  A person having origins in any of the original peoples of North and South America (including Central America) and who maintains tribal affiliation or community attachment. This category includes people who indicate their race as "American Indian or Alaska Native" or report entries such as Navajo Nation, Blackfeet Tribe, Mayan, Aztec, Native Village of Barrow Inupiat Traditional Government, or Nome Eskimo Community.

    Asian. A person having origins in any of the original peoples of the Far East, Southeast Asia, or the Indian subcontinent including, for example, India, China, the Philippine Islands, Japan, Korea, or Vietnam. It includes people who indicate their race as “Asian Indian,” “Chinese,” “Filipino,” “Korean,” “Japanese,” “Vietnamese,” and “Other Asian” or provide other detailed Asian responses such as Pakistani, Cambodian, Hmong, Thai, Bengali, Mien, etc.

    Native Hawaiian and Other Pacific Islander. A person having origins in any of the original peoples of Hawaii, Guam, Samoa, or other Pacific Islands. It includes people who indicate their race as “Native Hawaiian,” “Chamorro,” “Samoan,” and “Other Pacific Islander” or provide other detailed Pacific Islander responses such as Palauan, Tahitian, Chuukese, Pohnpeian, Saipanese, Yapese, etc.

    Two or more races. People may choose to provide two or more races either by checking two or more race response check boxes, by providing multiple responses, or by some combination of check boxes and other responses. The race response categories shown on the questionnaire are collapsed into the five minimum race groups identified by OMB, and the Census Bureau’s “Some Other Race” category. For data product purposes, “Two or More Races” refers to combinations of two or more of the following race categories: White, Black or African American, American Indian or Alaska Native, Asian, Native Hawaiian or Other Pacific Islander, or Some Other Race.


APA Inclusive Language Guidelines (https://www.apa.org/about/apa/equity-diversity-inclusion/language-guidelines)

For Asian Pacific Islander, guidlines suggest that Asians are people with ancestry in Asia and Pacific Islanders are an Indigenous population -- need to split these two up. Also, we should consider separating Desi people from East Asians, as physical differences between these groups are important in self-identification

For African-American, APA Guidelines suggest that we change to Black to designate a racial category

For Latinx, APA guidelines suggest that the -x suffix is atypical in Hispanic langauges and Latine may be preferred. Consider using Hispanic/Latine/Latinx

For Indigenous, we should consider that the category is incredibly broad and encompasses individuals with a wide degree of physical dissimilarity. 