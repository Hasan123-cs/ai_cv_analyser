namespace Ai_Cv_Analyser.Model.ViewModel
{
    public class HistoryViewModel
    {
        public string CVName { get; set; }
        public string Type { get; set; } // "Analyze" ||  "JobMatch"
        public int Score { get; set; }
        public int Id { get; set; }
        public DateTime Date { get; set; }
    }
}
