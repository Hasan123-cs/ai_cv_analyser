using Ai_Cv_Analyser.Model;
using Ai_Cv_Analyser.Services.Implementation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Ai_Cv_Analyser.Pages.Upload
{
    [Authorize]

    public class IndexModel : PageModel
    {
        [BindProperty]
        public IFormFile CvFile { get; set; }
        public UserManager<ApplicationUser> _user;
        public DashboardOperation op;
        public IndexModel(UserManager<ApplicationUser> s , DashboardOperation sz)
        {
            _user = s;
            op = sz;
        }
        public void OnGet()
        {

        }
        public async Task<IActionResult> OnPostAsync()
        {
            
            var user = await _user.GetUserAsync(User);
            if (CvFile == null ||
            Path.GetExtension(CvFile.FileName).ToLower() != ".pdf")
            {
                ModelState.AddModelError("", "Must be a PDF file");
            }
            if (!ModelState.IsValid)
            {

                return Page();
            }
            await op.UploadCvFile(CvFile, user);
            return RedirectToPage("/Dashboard/Index");
        }
    }
}
