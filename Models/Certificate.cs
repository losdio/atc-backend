using System.ComponentModel.DataAnnotations;

namespace atc_backend.Models
{
    public class Certificate
    {
        [Key]
        public int Id { get; set; }
        public string TraineeName { get; set; }
        public string CourseName { get; set; }
        public DateTime CompletionDate { get; set; }
        public string CertificatePath { get; set; }
    }
}
