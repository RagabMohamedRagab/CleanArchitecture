using System.ComponentModel.DataAnnotations;

namespace CleanArchitecture.Data.Entities.Entity
{
    public class StudentSubject
    {
        [Key]
        public int Id { get; set; }

        public int? StudentId { get; set; }

        public int? SubjectId { get; set; }


        public virtual Student Student { get; set; }

        public virtual Subject Subject { get; set; }
    }
}
