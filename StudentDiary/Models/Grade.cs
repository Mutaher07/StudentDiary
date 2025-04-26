using System;
using System.ComponentModel.DataAnnotations;

namespace StudentDiary.Models
{
    public class Grade
    {
        public int Id { get; set; }

        public int StudentId { get; set; }
        public Student Student { get; set; }

        [Required]
        public string Subject { get; set; }

        [Required]
        public string GradeValue { get; set; }

        public DateTime Date { get; set; }
    }
}
