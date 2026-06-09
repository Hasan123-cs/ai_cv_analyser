using Ai_Cv_Analyser.Model;
using Ai_Cv_Analyser.Model.ViewModel;
using AICVAnalyzer.Data;
using Microsoft.EntityFrameworkCore;

namespace Ai_Cv_Analyser.Services.Implementation
{
    public class HistoryOperation
    {
        public ApplicationDbContext _db;
        public HistoryOperation(ApplicationDbContext d)
        {
            _db = d;
        }
        public async Task<List<HistoryViewModel>> LoadAllJobMatchCv(string id)
        {
            return await _db.JobMatches.Include(r => r.cv).Where(x => x.cv.UserId == id)
                 .Select(a => new HistoryViewModel
                 {
                     Id = a.Id,
                     CVName = a.cv.FileName,
                     Type = "JobMatch",
                     Score = a.MatchPercentage,
                     Date = a.CreatedAt
                 })
                .ToListAsync(); 
        }

        public async Task<List<HistoryViewModel>> LoadAllAnalyseCv(string id)
        {
            var  z=  await _db.Analyses.Include(r => r.cv).Where(x => x.cv.UserId == id)
                .Select(a => new HistoryViewModel
                {
                    Id = a.Id,
                    CVName = a.cv.FileName,
                    Type = "Analyze",
                    Score = a.Score,
                    Date = a.CreatedAt
                })
                .ToListAsync();
            Console.WriteLine("USER ID = " + id);
            Console.WriteLine(z.Count);
            return z;
        }
    }
}
