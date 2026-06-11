using Ai_Cv_Analyser.Model.DTO;
using Ai_Cv_Analyser.Services.Implementation;
using Ai_Cv_Analyser.Services.Interfaces;
using AICVAnalyzer.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Ai_Cv_Analyser.Pages.Analysis
{

    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly DashboardOperation d;
        private readonly IAiService _aiService;

        public AnalysisResultDto Result { get; set; }

        public IndexModel(
            DashboardOperation x,
            IAiService aiService)
        {
            d = x;
            _aiService = aiService;
        }

        public async Task<IActionResult> OnGetAsync(int cvId)
        {
            var cv = await d.LoadCvById(cvId);

            if (cv == null)
            {
                return NotFound();
            }

            Result = await _aiService
                .AnalyzeCVAsync(cv.ExtractedText,cv);

            return Page();
        }
    }
}
