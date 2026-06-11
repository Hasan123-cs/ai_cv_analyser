using Ai_Cv_Analyser.Model.DTO;
using Ai_Cv_Analyser.Services.Interfaces;
using AICVAnalyzer.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Ai_Cv_Analyser.Pages.JobMatch
{
    [Authorize]
    public class IndexModel : PageModel
    {

        public IAiService _aiService;
        public ApplicationDbContext _context;
        public IndexModel(IAiService s,ApplicationDbContext d)
        {
            this._aiService = s;
            this._context = d;
        }
        [BindProperty]
        public int CVId { get; set; }

        [BindProperty]
        public string JobTitle { get; set; }

        [BindProperty]
        public string JobDescription { get; set; }
        public JobMatchResultDto Result { get; set; }

        public void OnGet(int id)
        {
            CVId = id;
        }
        public async Task<IActionResult> OnPostAsync()
        {
            var cv = await _context.CVs.FindAsync(CVId);

            var result =
                await _aiService.MatchCVWithJobAsync(
                    cv.ExtractedText,
                    JobDescription,cv);
            Console.WriteLine(result);
            Console.WriteLine("hi");
            Result = result;
            return Page();
        }
    }
}
