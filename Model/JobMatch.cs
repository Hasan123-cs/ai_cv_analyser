namespace Ai_Cv_Analyser.Model
{
    public class JobMatch
    {
        public int Id { get; set; }

        public int CVId { get; set; }
        public CV cv { get; set; }

        public string JobDescription { get; set; }

        public int MatchPercentage { get; set; }

        public string MissingSkills { get; set; }

        public string MatchingSkills { get; set; }

        public DateTime CreatedAt { get; set; }
    
}
}
