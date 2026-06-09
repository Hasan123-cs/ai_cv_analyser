using Ai_Cv_Analyser.Model;
using Ai_Cv_Analyser.Model.ViewModel;
using Ai_Cv_Analyser.Services.Implementation;
using AICVAnalyzer.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Ai_Cv_Analyser.Pages.Dashboard
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly DashboardOperation _context;
        private readonly UserManager<ApplicationUser> _userManager;
        public string fullName{ get; set; }
        public List<CvViewModel> CVs { get; set; } = new List<CvViewModel>();
        public IndexModel(UserManager<ApplicationUser> userManager, DashboardOperation c)
        {
            _context = c;
            _userManager = userManager;
        }
        public DashboardViewModel Dashboard { get; set; }
        public async Task OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            fullName = user.FullName;
            Dashboard = new DashboardViewModel();
            Dashboard = await  _context.LoadDashboardUserView(user.Id);
            var res = await _context.LoadAllCvUser(user.Id);
            foreach (var cv in res)
            {
                CVs.Add(new CvViewModel()
                {
                    FileName = cv.FileName,
                    Id = cv.Id,
                    UploadedAt = cv.UploadedAt,
                    userId = cv.UserId
                });
            }
        }
    }
}
