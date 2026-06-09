namespace Ai_Cv_Analyser.Model
{
    public class CV
    {
        public int Id { get; set; }

        public string FileName { get; set; }

        public string FilePath { get; set; }

        public string ExtractedText { get; set; }

        public DateTime UploadedAt { get; set; }

        public string UserId { get; set; }

        public ApplicationUser User { get; set; }
    }
}
