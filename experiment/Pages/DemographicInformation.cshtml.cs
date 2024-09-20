using ExperimentalGoal.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using RazorPagesDemo.Models;

namespace ExperimentalGoal.Pages
{
    public class DemographicInformationModel : PageModel
    {
       private readonly ILogger<DemographicInformationModel> _logger;
        DatabaseContext _Context;

        [BindProperty]
        public Game Game { get; set; }


        public DemographicInformationModel(ILogger<DemographicInformationModel> logger, DatabaseContext databasecontext)
        {
            _Context = databasecontext;
            _logger = logger;
        }

        public void OnGet()
        {
            

            //var generator = new ImageGenerator();
            //var age = 25;
            //var gender = Gender.Male;

            //var generatedImage = generator.GenerateImage(age, gender);
            //testImage.GenerateImage(10, Gender.Male.ToString());
            // Set user properties (age, race, gender)
            //var age = "35";
            //var race = "Asian";
            //var gender = "Female";
            //testImage.generateImage(age, race, gender);

            ViewData["message"] = string.Empty;
        }

        public ActionResult OnPost()
        {
            try
            {
                var game = Game;
                if (!ModelState.IsValid)
                {
                    return Page(); // return page
                }
                _logger.LogError("Game in saving data:", JsonConvert.SerializeObject(Game));
                //var data = (from games in _Context.Game
                //            where games.Age == game.Age
                //            && games.Race == game.Race
                //            && games.Gender == game.Gender
                //            select Game).ToList();

                //if (data != null && data.Count > 0)
                //{
                //    ViewData["message"] = "Already game existed with this criteria.";
                //    return Page(); // return page
                //}

                var result = _Context.Add(Game);
                _Context.SaveChanges(); // Saving Data in database
                TempData["Game"] = JsonConvert.SerializeObject(Game);
                return RedirectToPage("Ratings");
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in saving data:", ex.Message);
                _logger.LogError("Error in post:" + ex.StackTrace);
            }
            return RedirectToPage("Ratings");
        }
    }
}
