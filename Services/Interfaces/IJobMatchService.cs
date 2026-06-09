using Ai_Cv_Analyser.Model.DTO;
using OpenAI.Assistants;

namespace Ai_Cv_Analyser.Services.Interfaces
{
    //AI Career Assistant Platform
    public interface IJobMatchService
    {
        Task<JobMatchResultDto> CalculateMatchAsync(
    string cvText,
    string jobDescription);
    }
}
