using ExperimentalGoal.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using RazorPagesDemo.Models;

namespace ExperimentalGoal.Pages
{
    public class RatingsModel : PageModel
    {
        private readonly ILogger<GameSelectionModel> _logger;
        DatabaseContext _Context;

        [BindProperty]
        public Game Game { get; set; }

        public RatingsModel(ILogger<GameSelectionModel> logger, DatabaseContext databasecontext)
        {
            _Context = databasecontext;
            _logger = logger;
        }

        public void OnGet()
        {
            try
            {
                TempData.Keep();
            }
            catch (Exception ex)
            { }
        }

        public ActionResult OnPost()
        {
            string? rating = Game.Rating;
            int gameid = Game.GameID;
            
            var data = (from games in _Context.Game
                        where games.GameID == gameid
                        select games).ToList().FirstOrDefault();
            try
            {
                if (data != null)
                {
                    data.Rating = Game.Rating;
                    _Context.Game.Update(data);
                    _Context.SaveChanges(); // Saving Data in database
                }
            }
            catch (Exception)
            {

            }
            TempData["Game"] = JsonConvert.SerializeObject(data);
            return RedirectToPage("GameSelection");
        }

    }
}
