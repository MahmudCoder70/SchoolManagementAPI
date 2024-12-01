namespace SchoolManagementSystem.Models
{
    public class SchoolType
    {
        public int SchoolTypeId { get; set; }
        public string SchoolTypeName { get; set; } = default!;
        public virtual ICollection<School> Schools { get; set; } = new List<School>();
       
    }
}
