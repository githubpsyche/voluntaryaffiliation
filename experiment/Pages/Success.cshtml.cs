using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using RazorPagesDemo.Models;

namespace ExperimentalGoal.Pages
{
    public class SuccessModel : PageModel
    {
        private readonly AppSettings _mySettings;

        public SuccessModel(IOptions<AppSettings> settings)
        {
            //This is always null
            _mySettings = settings.Value;
        }

        public void OnGet()
        {
            ViewData["website"] = _mySettings.Website;
        }
    }
}
