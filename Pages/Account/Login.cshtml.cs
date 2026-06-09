using Ai_Cv_Analyser.Model.ViewModel;
using Ai_Cv_Analyser.Services.Implementation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Ai_Cv_Analyser.Pages.Account
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        public LoginViewModel Input { get; set; }
        public LoginOperation l { get; set; }
        public LoginModel(LoginOperation s)
        {
            l = s;
        }
        public void OnGet()
        {
        }
        public async Task<IActionResult> OnPost()
        {
            int x = await l.CheckUser(Input);
            if(x!=0)
            {
                return RedirectToPage("/Dashboard/Index");
            }
            ModelState.AddModelError(
                    "Input.Password",
                "Invalid Login - Email Or Password Inncorrect");

            return Page();
        }
    }
}
