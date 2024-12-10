namespace Practice.Models
{
    public class Employee_Department
    {
        public long? Id { get; set; }
        public string? Department_Name { get; set; }
        public string? Employee_Name { get; set; }
        public string? Employee_Salary { get; set; }
        public char? MODE { get; set; }

    }

    public class Employee_Department_View
    {
        public long? Emp_Id { get; set; }
        public long? Dept_Id { get; set; }
    }
}
