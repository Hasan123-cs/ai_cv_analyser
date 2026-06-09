using Ai_Cv_Analyser.Model;
using Ai_Cv_Analyser.Model.DTO;

namespace Ai_Cv_Analyser.Services.Interfaces
{
    public interface IAiService
    {
        Task<AnalysisResultDto> AnalyzeCVAsync(string cvText, CV cv);
        Task<JobMatchResultDto> MatchCVWithJobAsync(
    string cvText,
    string jobDescription,CV cv);
    }
}
