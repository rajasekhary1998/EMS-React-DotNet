namespace EMS.APP.Model
{
    public class Departments
    {
        public int DepartmentId { get; set; }

        public string DepartmentName { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; }

        public ICollection<Employee> Employees
        { get; set; }
        = new List<Employee>();
    }
}
