using Microsoft.AspNetCore.Identity;

namespace Ai_Cv_Analyser.Model
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; }
    }
}
