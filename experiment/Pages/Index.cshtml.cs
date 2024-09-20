using ExperimentalGoal.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPagesDemo.Models;

namespace ExperimentalGoal.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        DatabaseContext _Context;

        [BindProperty]
        public Game Game { get; set; }


        public IndexModel(ILogger<IndexModel> logger, DatabaseContext databasecontext)
        {
            _Context = databasecontext;
            _logger = logger;
        }

        public void OnGet()
        {
            var data = (from playerList in _Context.Player
                        select playerList).ToList();

            ViewData["message"] = string.Empty;
        }

        public ActionResult OnPost()
        {
            return RedirectToPage("GameSelection");
        }

    }
}