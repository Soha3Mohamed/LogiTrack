using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogiTrack.Entities
{
    internal class Project : BaseEntity
    {

        public Department Department { get; set; }


        public ICollection<EmployeeProject> EmployeeProjects { get; set; } = new HashSet<EmployeeProject>();
    }
}
