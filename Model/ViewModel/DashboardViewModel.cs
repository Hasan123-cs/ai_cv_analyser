namespace Ai_Cv_Analyser.Model.ViewModel
{
    public class DashboardViewModel
    {
        public int TotalCVs { get; set; }

        public int TotalAnalyses { get; set; }

        public int TotalJobMatches { get; set; }

        public int BestScore { get; set; }

        public List<RecentAnalysisVM> RecentAnalyses { get; set; }
    }
}
