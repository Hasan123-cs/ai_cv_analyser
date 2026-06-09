namespace Ai_Cv_Analyser.Model.DTO
{
    public class JobMatchResultDto
    {
        public double MatchPercentage { get; set; }



        public string Recommendation { get; set; }

        public List<string> MatchingSkills { get; set; }
        public List<string> MissingSkills { get; set; }
        public List<string> Roadmap { get; set; }
    }
}
