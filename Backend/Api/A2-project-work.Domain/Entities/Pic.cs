

namespace A2_project_work.Domain.Entities
{
    public class Pic : Entity<Guid>
    {
        public int port_number { get; set; }
        public int buildingnumber { get; set; }
        public bool status { get; set; }
    }
    
    public class PicNoGuid
    {
        public int port_number { get; set; }
        public int buildingnumber { get; set; }
        public bool status { get; set; }

    }
}
