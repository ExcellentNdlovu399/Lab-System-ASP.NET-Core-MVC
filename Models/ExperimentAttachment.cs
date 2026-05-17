namespace Lab_System.Models
{
    public class ExperimentAttachment
    {
        public int Id { get; set; }

        public int ExperimentId { get; set; }

        public Experiment? Experiment { get; set; }

        public string FileName { get; set; }

        public string FilePath { get; set; }

        public string FileType { get; set; }

        public DateTime UploadedAt { get; set; } = DateTime.Now;
    }
}