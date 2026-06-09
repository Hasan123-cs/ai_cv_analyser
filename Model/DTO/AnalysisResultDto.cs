namespace Ai_Cv_Analyser.Model.DTO
{
    public class AnalysisResultDto
    {
        public int Score { get; set; }

        public string Summary { get; set; }

        public List<string> Strengths { get; set; }

        public List<string> Weaknesses { get; set; }

        public List<string> SuggestedProjects { get; set; }

        public List<string> SuggestedSkills { get; set; }
        public string Recommendation { get; set; }
    }
}
