

namespace A2_project_work.Domain.Entities
{
    public class User : Entity<Guid>
    {
        public string? name { get; set; }
        public string? surname { get; set; }
        public string? username { get; set; }
        public string? password { get; set; }

        public string? email { get; set; }
      
    }
    public class NoGuidUser
    {
        public string? name { get; set; }
        public string? surname { get; set; }
        public string? username { get; set; }
        public string? password { get; set; }
        public string? email { get; set; }
    }
    public class UsernameAndEmailUser
    {
        public string? username { get; set; }
        public string? email { get; set; }
    }
    public class AdminUser : User
    {
        public bool admin { get; set; }
    }
}
