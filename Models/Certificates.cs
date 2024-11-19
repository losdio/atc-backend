using System.ComponentModel.DataAnnotations;

namespace atc_backend.Models
{
    public class Certificates
    {
        [Key]
        public int CertificateId { get; set; }
        public int UserId { get; set; }
        public int CourseId { get; set; }
        public DateTime IssuedDate { get; set; }
        public int IsActive { get; set; } = 1;
    }
}
