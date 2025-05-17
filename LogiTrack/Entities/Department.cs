using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogiTrack.Entities
{
    internal class Department : BaseEntity
    {
        public string Name { get; set; }
        public ICollection<Employee> Employees { get; set; } = new HashSet<Employee>();

        public ICollection<Project> Projects { get; set; } = new HashSet<Project>();
    }
}
