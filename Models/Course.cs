using System;
using System.ComponentModel.DataAnnotations;

namespace atc_backend.Models
{
    public class Course
    {
        public int CourseId { get; set; }

        [Required]
        [StringLength(100)]
        public string CourseTitle { get; set; }

        public string Description { get; set; }

        public int Price { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; }
    }
}
