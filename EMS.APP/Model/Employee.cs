using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EMS.APP.Model
{
    [Table("employees")]
    public class Employee
    {
        [Key]
        [Column("employee_id")]
        public int EmployeeId { get; set; }

        [Column("employee_code")]
        public string EmployeeCode { get; set; } = string.Empty;

        [Column("first_name")]
        public string FirstName { get; set; } = string.Empty;

        [Column("last_name")]
        public string LastName { get; set; } = string.Empty;

        [Column("email")]
        public string Email { get; set; } = string.Empty;

        [Column("phone")]
        public string Phone { get; set; } = string.Empty;

        [Column("status")]
        public string Status { get; set; } = string.Empty;

        [Column("department_id")]
        public int DepartmentId { get; set; }

        [Column("joining_date")]
        public DateOnly? JoiningDate { get; set; }


        [Column("created_at")]
        public DateTime? CreatedAt { get; set; }

        public Departments? Department { get; set; }
    }

}
