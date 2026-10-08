namespace EMS.APP.DTOs
{
    public class EmployeeDto
    {
        public int EmployeeId { get; set; }

        public string EmployeeCode { get; set; } = string.Empty;

        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string Phone { get; set; } = string.Empty;

        public int DepartmentId { get; set; }

        public string Status { get; set; } = "ACTIVE";

        public DateOnly JoiningDate { get; set; }
    }
}
