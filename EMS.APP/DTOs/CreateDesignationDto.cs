namespace EMS.APP.DTOs
{
    public class CreateDesignationDto
    {
        public string DesignationName { get; set; } = string.Empty;

        public int DepartmentId { get; set; }
    }
}
