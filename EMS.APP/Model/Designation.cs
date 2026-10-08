
namespace EMS.APP.Model
{
    public class Designation
    {
        public int DesignationId { get; set; }

        public string DesignationName { get; set; } = string.Empty;

        public int DepartmentId { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
