using LogiTrack.Contexts;
using LogiTrack.Entities;

namespace LogiTrack.Utility
{
    internal class DbSeeder
    {
        public static void Seed(CompanyDbContext context)
        {
            // 1. Seed Departments first
            if (!context.Departments.Any())
            {
                var departments = new List<Department>
                {
                    new Department { Name = "Engineering" },
                    new Department { Name = "HR" },
                    new Department { Name = "Marketing" }
                };

                context.Departments.AddRange(departments);
                context.SaveChanges();
            }

            // 2. Seed Employees next
            if (!context.Employees.Any())
            {
                var employees = new List<Employee>
                {
                    new Employee { Name = "Omar Ibrahim", Salary = 20000, Age = 27, DepartmentId = context.Departments.First(d => d.Name == "Engineering").Id, Role = Role.None },
                    new Employee { Name = "Sara Salah", Salary = 35000, Age = 31, DepartmentId = context.Departments.First(d => d.Name == "HR").Id, Role = Role.Manager },
                    new Employee { Name = "Manal Ali", Salary = 10000, Age = 33, DepartmentId = context.Departments.First(d => d.Name == "Engineering").Id, Role = Role.None },
                    new Employee { Name = "Ahmed Saber", Salary = 25000, Age = 22, DepartmentId = context.Departments.First(d => d.Name == "Marketing").Id, Role = Role.Manager },
                    new Employee { Name = "Mostafa Mohamed", Salary = 30000, Age = 23, DepartmentId = context.Departments.First(d => d.Name == "HR").Id, Role = Role.None }
                };

                context.Employees.AddRange(employees);
                context.SaveChanges();
            }

            //// 3. Seed Projects WITHOUT ManagerId first
            if (!context.Projects.Any())
            {
                var engineeringId = context.Departments.First(d => d.Name == "Engineering").Id;
                var hrId = context.Departments.First(d => d.Name == "HR").Id;
                var marketingId = context.Departments.First(d => d.Name == "Marketing").Id;

                var projects = new List<Project>
                {
                    new Project { Name = "Project A", DepartmentId = engineeringId, Description = "project A" },
                    new Project { Name = "Project B", DepartmentId = hrId, Description = "project B" },
                    new Project { Name = "Project C", DepartmentId = engineeringId, Description = "project C" },
                    new Project { Name = "Project D", DepartmentId = marketingId, Description = "project D" }
                };

                context.Projects.AddRange(projects);
                context.SaveChanges();
            }

            //// 4. Now update Projects with ManagerId, after Employees are seeded
            //var saraId = context.Employees.First(e => e.Name == "Sara Salah").Id;
            //var ahmedId = context.Employees.First(e => e.Name == "Ahmed Saber").Id;

            //var projectB = context.Projects.First(p => p.Name == "Project B");
            //var projectD = context.Projects.First(p => p.Name == "Project D");

            //bool updated = false;

            //if (projectB.ManagerId != saraId)
            //{
            //    projectB.ManagerId = saraId;
            //    context.Entry(projectB).Property(p => p.ManagerId).IsModified = true;
            //    updated = true;
            //}
            //if (projectD.ManagerId != ahmedId)
            //{
            //    projectD.ManagerId = ahmedId;
            //    context.Entry(projectD).Property(p => p.ManagerId).IsModified = true;
            //    updated = true;
            //}

            //if (updated)
            //{
            //    context.SaveChanges();
            //}

            // 5. Seed many-to-many relations (EmployeeProjects)
            if (!context.EmployeeProjects.Any())
            {
                var employeeProjects = new List<EmployeeProject>
                {
                    new EmployeeProject { EmployeeId = context.Employees.First(e => e.Name == "Omar Ibrahim").Id, ProjectId = context.Projects.First(p => p.Name == "Project A").Id },
                    new EmployeeProject { EmployeeId = context.Employees.First(e => e.Name == "Sara Salah").Id, ProjectId = context.Projects.First(p => p.Name == "Project B").Id },
                    new EmployeeProject { EmployeeId = context.Employees.First(e => e.Name == "Manal Ali").Id, ProjectId = context.Projects.First(p => p.Name == "Project A").Id },
                    new EmployeeProject { EmployeeId = context.Employees.First(e => e.Name == "Sara Salah").Id, ProjectId = context.Projects.First(p => p.Name == "Project C").Id },
                    new EmployeeProject { EmployeeId = context.Employees.First(e => e.Name == "Ahmed Saber").Id, ProjectId = context.Projects.First(p => p.Name == "Project B").Id },
                    new EmployeeProject { EmployeeId = context.Employees.First(e => e.Name == "Mostafa Mohamed").Id, ProjectId = context.Projects.First(p => p.Name == "Project C").Id }
                };

                context.EmployeeProjects.AddRange(employeeProjects);
                context.SaveChanges();
            }
        }
    }
}
