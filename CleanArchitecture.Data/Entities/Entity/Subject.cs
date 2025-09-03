using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Data.Entities.Entity
{
    public class Subject
    {
        public Subject()
        {
            StudentSubjects=new HashSet<StudentSubject>();
            DepartMentSubjects=new HashSet<DepartMentSubject>();
        }
        public int Id { get; set; }

        public string Name { get; set; }

       public DateTime Peroid { get; set; }
      
        public virtual ICollection<StudentSubject> StudentSubjects { get; set; }

        public virtual ICollection<DepartMentSubject> DepartMentSubjects { get;set; }
    }
}
