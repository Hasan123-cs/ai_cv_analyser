using Ai_Cv_Analyser.Model;
using Ai_Cv_Analyser.Services.Implementation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Ai_Cv_Analyser.Pages.History
{
    [Authorize]
    public class IndexModel : PageModel
    {
        public HistoryOperation h;
        public UserManager<ApplicationUser> _userManager;
        public string Filter { get; set; } = "All";

        public List<Ai_Cv_Analyser.Model.ViewModel.HistoryViewModel> HistoryItems { get; set; } = new();
        public IndexModel(HistoryOperation s, UserManager<ApplicationUser> userManager)
        {
            h = s;
            _userManager = userManager;
        }
        public async Task OnGet(string filter = "All")
        {
            Filter = filter;
            var user = await _userManager.GetUserAsync(User);

            var analysis = await h.LoadAllAnalyseCv(user.Id);    
            var jobMatch = await h.LoadAllJobMatchCv(user.Id);
           
            var history = analysis.Concat(jobMatch);
            if (filter == "Analyze")
            {
                history = history.Where(x => x.Type == "Analyze");
            }
            else if (filter == "JobMatch")
            {
                history = history.Where(x => x.Type == "JobMatch");
            }
            if(filter=="All")
            {
                HistoryItems = analysis
                  .Concat(jobMatch)
                  .OrderByDescending(x => x.Date)
                  .ToList();
               
            }
            else
            {

            HistoryItems = history
                .OrderByDescending(x => x.Date)
                .ToList();
            }

        }
    }
}
