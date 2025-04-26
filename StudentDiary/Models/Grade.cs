using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentDiary.Models
{
    public class Grade
    {
        public int Id { get; set; }


        public int? StudentId { get; set; }

        [ForeignKey(nameof(StudentId))]
        public Student? Student { get; set; }



        [Required]
        public string Subject { get; set; }

        [Required]
        public string Value { get; set; }

        public DateTime Date { get; set; }
    }
}
