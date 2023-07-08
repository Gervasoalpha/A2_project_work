namespace A2_project_work.Domain.Entities
{
    public abstract class Entity<TPrimaryKey>
    {
        public TPrimaryKey? id { get; set; }
    }
    
}
