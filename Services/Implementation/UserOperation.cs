using Ai_Cv_Analyser.Model;
using Ai_Cv_Analyser.Model.ViewModel;
using AICVAnalyzer.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Ai_Cv_Analyser.Services.Implementation
{
    public class UserOperation
    {
        private readonly UserManager<ApplicationUser> _userManager;
        public readonly ApplicationDbContext _db;
        public UserOperation(UserManager<ApplicationUser> userManager, ApplicationDbContext db)
        {
            _userManager = userManager;
            _db = db;
        }

        public async Task<List<string>> CreateUser(RegisterViewModel Input)
        {
            var user = new ApplicationUser
            {
                FullName = Input.FullName,
                UserName = Input.Email,
                Email = Input.Email
            };

            var result =
                await _userManager.CreateAsync(
                    user,
                    Input.Password);
            if (result.Succeeded)
            {
                return [];
            }
            return  result.Errors.Where(r=>r.Code!= "DuplicateUserName").Select(x=>x.Description).ToList();

        }
    }
}
