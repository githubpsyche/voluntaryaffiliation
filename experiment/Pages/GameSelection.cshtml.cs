using ExperimentalGoal.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using RazorPagesDemo.Models;

namespace ExperimentalGoal.Pages
{
    public class GameSelectionModel : PageModel
    {
        private readonly ILogger<GameSelectionModel> _logger;
        DatabaseContext _Context;
        
        [BindProperty]
        public Game Game { get; set; }

        public GameSelectionModel(ILogger<GameSelectionModel> logger, DatabaseContext databasecontext)
        {
            _Context = databasecontext;
            _logger = logger;
        }

        public void OnGet()
        {
            try
            {
                TempData.Keep();
                Random ran = new Random();
                int getRanNum1 = ran.Next(10);
                ViewData["script"] = getRanNum1 % 2;

                Game game = JsonConvert.DeserializeObject<Game>(TempData["Game"].ToString());
                ViewData["gameid"] = game.GameID;

                // Fetch random players from the database
                var data = GetRandomPlayers();
                ViewData["players"] = data;
                TempData.Keep();
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in getting data:", ex.StackTrace);

                // Handle the error appropriately, or rethrow the exception
                throw;  // Or handle it in a way that's appropriate for your application
            }
        }

        public ActionResult OnPost()
        {
            int gameid = Game.GameID;
            var data = (from games in _Context.Game
                        where games.GameID == gameid
                        select games).ToList().FirstOrDefault();
            try
            {
                if (data != null)
                {
                    data.PlayersOrder = Game.PlayersOrder;
                    data.Team1 = Game.Team1;
                    data.Team2 = Game.Team2;
                    data.GameSelectionText = Game.GameSelectionText;
                    _Context.Game.Update(data);
                    _Context.SaveChanges(); // Saving Data in database
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in saving data:", ex.StackTrace);
                // Handle the exception as necessary
            }
            return RedirectToPage("BeforeSuccess");
        }

        public List<Players> GetPlayers()
        {
            // Fetch all players from the database
            return _Context.Player.ToList();
        }

        public List<Players> GetRandomPlayers()
        {
            List<Players> players = GetPlayers();
            Random random = new Random();
            List<Players> randomPlayers = new List<Players>();

            // Select 16 random players from the list
            while (randomPlayers.Count < 16 && players.Count > 0)
            {
                int randomIndex = random.Next(0, players.Count);
                randomPlayers.Add(players[randomIndex]);
                players.RemoveAt(randomIndex);
            }

            return randomPlayers;
        }
    }
}