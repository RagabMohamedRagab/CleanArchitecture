using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Data.Entities.Entity
{
    public class DepartMentSubject
    {
        public int Id { get; set; }

        public int? DepartMentId { get; set; }
        public int? SubjectId { get; set; }

        public virtual DepartMent  DepartMent { get; set; }

        public virtual Subject Subject { get; set; }
    }
}
