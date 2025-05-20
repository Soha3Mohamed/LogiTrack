using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogiTrack.Entities
{

    public enum Role
    {
        None,      // Default: normal employee
        Staff,     // Specific functional role
        Manager    // Special authority
    }
    internal class Employee : BaseEntity
    {
        public string Name { get; set; }
        public decimal Salary { get; set; }
        public int Age { get; set; }

        public Role Role { get; set; }
        public int DepartmentId { get; set; }
        public Department Department { get; set; }

        public ICollection<EmployeeProject> EmployeeProjects { get; set; } = new HashSet<EmployeeProject>(); 
    }
}
