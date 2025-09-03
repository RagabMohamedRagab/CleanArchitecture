namespace CleanArchitecture.Data.Entities.Entity
{
    public class DepartMent
    {
        public DepartMent()
        {
            DepartMentSubjects = new HashSet<DepartMentSubject>();
            Students = new HashSet<Student>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<DepartMentSubject>  DepartMentSubjects { get; set; }
       public virtual ICollection<Student> Students { get; set; }
    }
}
