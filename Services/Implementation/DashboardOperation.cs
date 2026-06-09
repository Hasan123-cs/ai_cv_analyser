using Ai_Cv_Analyser.Model;
using Ai_Cv_Analyser.Model.ViewModel;
using AICVAnalyzer.Data;
using Microsoft.EntityFrameworkCore;
using UglyToad.PdfPig;
using System.Text;
namespace Ai_Cv_Analyser.Services.Implementation
{
    public class DashboardOperation
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _environment;
        public DashboardOperation(ApplicationDbContext context,IWebHostEnvironment s)
        {
            _environment = s;
            _context = context;
        }
        public async Task<DashboardViewModel> LoadDashboardUserView(string id)
        {
            DashboardViewModel Dashboard = new DashboardViewModel();
            
            Dashboard.TotalCVs = await _context.CVs
                .CountAsync(x => x.UserId == id);

            var userCVIds = await _context.CVs
                .Where(x => x.UserId == id)
                .Select(x => x.Id)
                .ToListAsync();

            Dashboard.TotalAnalyses = await _context.Analyses
                .CountAsync(x => userCVIds.Contains(x.CVId));

            Dashboard.TotalJobMatches = await _context.JobMatches
                .CountAsync(x => userCVIds.Contains(x.CVId));

            Dashboard.BestScore = await _context.Analyses
                .Where(x => userCVIds.Contains(x.CVId))
                .Select(x => (int?)x.Score)
                .MaxAsync() ?? 0;

            Dashboard.RecentAnalyses = await(
                from a in _context.Analyses
                join cv in _context.CVs
                    on a.CVId equals cv.Id
                where cv.UserId == id
                orderby a.CreatedAt descending
                select new RecentAnalysisVM
                {
                    CVName = cv.FileName,
                    Score = a.Score,
                    Date = a.CreatedAt
                })
                .Take(5)
                .ToListAsync();
            return Dashboard;
        }
        // upload file methode
        public async Task<int> UploadCvFile(IFormFile file,ApplicationUser user)
        {
            // Create folder if not exists
            string folderPath = Path.Combine(
                _environment.WebRootPath,
                "cvs");

            Directory.CreateDirectory(folderPath);

            // Generate unique file name Guided .. 
            
            string uniqueFileName =
                Guid.NewGuid().ToString() +
                Path.GetExtension(file.FileName);

            string physicalPath =
                Path.Combine(folderPath, uniqueFileName);

            // Save file
            using (var stream = new FileStream(
                physicalPath,
                FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            // Read PDF text
            StringBuilder textBuilder = new();

            using (var pdf = PdfDocument.Open(physicalPath))
            {
                foreach (var page in pdf.GetPages())
                {
                    textBuilder.AppendLine(page.Text);
                }
            }

            string extractedText = textBuilder.ToString();

            // Save to database
            
            CV cv = new CV
            {
                FileName = file.FileName,
                FilePath = "/cvs/" + uniqueFileName,
                ExtractedText = extractedText,
                UploadedAt = DateTime.UtcNow,
                UserId = user.Id,
                User=user,
            };

            _context.CVs.Add(cv);

            await _context.SaveChangesAsync();

            return cv.Id;
        }
        public async Task<CV> LoadCvById(int id)
        {
            return await _context.CVs.FindAsync(id);
        }
       public async Task<List<Ai_Cv_Analyser.Model.CV>> LoadAllCvUser(string id)
        {
            return await _context.CVs.Where(x => x.UserId == id).ToListAsync();
        }
    }
}
