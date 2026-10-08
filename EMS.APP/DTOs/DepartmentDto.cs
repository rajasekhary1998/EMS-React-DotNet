
namespace EMS.APP.DTOs
{
    public class DepartmentDto
    {
        public int DepartmentId { get; set; }

        public string DepartmentName { get; set; }
            = string.Empty;

        public DateTime CreatedAt { get; set; }
    }
}