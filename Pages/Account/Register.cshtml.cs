using Ai_Cv_Analyser.Model.ViewModel;
using Ai_Cv_Analyser.Services.Implementation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Ai_Cv_Analyser.Pages.Account
{
    public class RegisterModel : PageModel
    {
        public UserOperation user { get; set; }
        [BindProperty]
        
        public RegisterViewModel Input { get; set; }
        public RegisterModel(UserOperation user)
        {
            this.user= user;
        }
        public void OnGet()
        {
        }
        public async Task<IActionResult> OnPost()
        {
            
            List<string> x = await user.CreateUser(Input);
            if (!x.Any())
            {

            return RedirectToPage("/Account/Login");
            }
            foreach(var error in x)
            {
                ModelState.AddModelError("", error);
            }    
            return Page();
        }
    }
}
