using ExperimentalGoal.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using RazorPagesDemo.Models;

namespace ExperimentalGoal.Pages
{
    public class BeforeSuccessModel : PageModel
    {
        private readonly ILogger<GameSelectionModel> _logger;
        DatabaseContext _Context;

        [BindProperty]
        public Game Game { get; set; }

        public BeforeSuccessModel(ILogger<GameSelectionModel> logger, DatabaseContext databasecontext)
        {
            _Context = databasecontext;
            _logger = logger;
        }


        public void OnGet()
        {
            TempData.Keep();
        }

        public ActionResult OnPost() {

            string? nogame = Game.NoGame;

            int gameid = Game.GameID;

            var data = (from games in _Context.Game
                        where games.GameID == gameid
                        select games).ToList().FirstOrDefault();

            if (!string.IsNullOrEmpty(nogame))
            {
                
                try
                {
                    if (data != null)
                    {
                        data.NoGame = nogame;
                        _Context.Game.Update(data);
                        _Context.SaveChanges(); // Saving Data in database
                    }
                }
                catch (Exception)
                {

                }
            }

            TempData["Game"] = JsonConvert.SerializeObject(data);
            return RedirectToPage("Success");
        }
    }
}
