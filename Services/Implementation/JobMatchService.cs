using Ai_Cv_Analyser.Model.DTO;
using Ai_Cv_Analyser.Services.Interfaces;

namespace Ai_Cv_Analyser.Services.Implementation
{
    public class JobMatchService : IJobMatchService
    {
        public Task<JobMatchResultDto> CalculateMatchAsync(string cvText, string jobDescription)
        {
            throw new NotImplementedException();
        }
    }
}
