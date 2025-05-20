using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogiTrack.Entities
{
    internal class Project : BaseEntity
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public int DepartmentId { get; set; }
        public Department Department { get; set; }

        [ForeignKey("Manager")]
        public int? ManagerId { get; set; }
        public Employee? Manager { get; set; } 
        public ICollection<EmployeeProject> EmployeeProjects { get; set; } = new HashSet<EmployeeProject>();
    }
}
