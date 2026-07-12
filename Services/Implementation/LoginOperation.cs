using Ai_Cv_Analyser.Model;
using Ai_Cv_Analyser.Model.ViewModel;
using AICVAnalyzer.Data;
using Microsoft.AspNetCore.Identity;

namespace Ai_Cv_Analyser.Services.Implementation
{
    public class LoginOperation
    {
        private readonly SignInManager<ApplicationUser>
    _signInManager;
        private readonly ApplicationDbContext _db;
        public LoginOperation(SignInManager<ApplicationUser> signInManager, ApplicationDbContext db)
        {
            _signInManager = signInManager;
            _db = db;
        }
        public async Task<int> CheckUser(LoginViewModel Input)
        {
            var result =
       await _signInManager.PasswordSignInAsync(
           Input.Email,
           Input.Password,
           false,
           false);
    // first false for remember me and the second for the lockout
            if (result.Succeeded)
            {
                return 1;
            }
            return 0;
        }
    }
}
