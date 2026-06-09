namespace Ai_Cv_Analyser.Model
{
    public class Analysis
    {
        public int Id { get; set; }

        public int CVId { get; set; }
        public CV cv { get; set; }
        public int Score { get; set; }

        public string Summary { get; set; }

        public string Strengths { get; set; }

        public string Weaknesses { get; set; }

        public string SuggestedProjects { get; set; }

        public string SuggestedSkills { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
