
namespace A2_project_work.Domain.Entities
{
    public class Log : Entity<long>
    {
        public int authcode { get; set; }

        public int unlockcode { get; set; }

        public bool opened { get; set; }

        public Guid user_id { get; set; }

        public Guid pic_id { get; set;}
    }
}
