

namespace A2_project_work.Domain.Entities
{
    public class Raspberry : Entity<Guid>
    {
        public int buildingnumber { get; set; }
    }
    public class NoGuidRasp
    {
        public int buildingnumber { get; set; }
    }
}
