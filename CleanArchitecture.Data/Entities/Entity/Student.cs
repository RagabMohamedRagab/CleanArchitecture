using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Data.Entities.Entity
{
    public class Student
    {
        public Student()
        {
            StudentSubjects=new HashSet<StudentSubject>();
        }
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public  string Address {  get; set; }

        public string Phone {  get; set; }
        public int? DepartMentId { get; set; }
        public virtual DepartMent  DepartMent { get; set; }
        public virtual ICollection<StudentSubject> StudentSubjects { get; set; }
    }
}
