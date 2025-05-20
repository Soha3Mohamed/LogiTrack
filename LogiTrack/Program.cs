using LogiTrack.Contexts;
using LogiTrack.Utility;
using LogiTrack.Entities;
using Microsoft.Identity.Client;

namespace LogiTrack
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using (CompanyDbContext dbContext = new CompanyDbContext())
            {

                DbSeeder.Seed(dbContext);

                #region Get all Employees's names
                //var employee = (from emp in dbContext.Employees

                //                select emp);
                //foreach (var item in employee)
                //{
                //    Console.WriteLine(item.Name);
                //}

                #endregion

                #region Get all managers (with Role.Manager )
                var emps = from emp in dbContext.Employees
                           where emp.Role == Role.Manager
                           select emp;
                foreach (var emp in emps)
                {
                    Console.WriteLine(emp.Id);
                }
                #endregion


                #region Add a new project to a department
                //Project project = new Project()
                //{
                //    Name = "Project E",
                //    Description = "Test Project",
                //    DepartmentId = 2,
                //    ManagerId = 2,
                //};
                //dbContext.Projects.Add(project);
                //dbContext.SaveChanges();
                //Console.WriteLine("pppp");
                #endregion


                #region Get all Projects
                //var projects = (from P in dbContext.Projects

                //                select P);
                //foreach (var item in projects)
                //{
                //    Console.WriteLine(item.Name);
                //} 
                #endregion


                #region Remove project
                //var projects = from P in dbContext.Projects
                //               where P.Description == "Test Project"
                //               select P;

                //foreach (var project in projects)
                //{
                //    Console.WriteLine(project.Name);
                //    dbContext.Projects.Remove(project);
                //}
                //dbContext.SaveChanges();
                #endregion

                #region Assign employee(s) to a project
                var project= (from P in dbContext.Projects
                             where P.Name == "Project A"
                             select P).FirstOrDefault();

                if (project != null )
                {
                    var employees = (from emp in dbContext.Employees
                                     where emp.Age <= 28
                                     select emp).ToList() ;
                    foreach (var emp in employees)
                    {
                        bool IsAssigned = dbContext.EmployeeProjects.Any(EP=>EP.EmployeeId == emp.Id && EP.ProjectId == project.Id);

                        if(!IsAssigned)
                        {
                            var assignedProject = new EmployeeProject
                            {
                                EmployeeId = emp.Id,
                                ProjectId = project.Id,
                                AssignedAt = DateTime.Now,
                            };

                            dbContext.EmployeeProjects.Add(assignedProject);
                        }

                    }
                    dbContext.SaveChanges();
                }
                #endregion

                #region Change role of an employee in a project
                //var employees = (from emp in dbContext.Employees
                //                where emp.Name == "Manal Ali"
                //                select emp).FirstOrDefault();

                //employees.Role= Role.Manager;
                //dbContext.SaveChanges();
                #endregion

                #region List all employees in a department/project

                var hrDepartment = dbContext.Departments
                                            .FirstOrDefault(d => d.Name == "HR");

                if (hrDepartment != null)
                {
                    // Explicitly load the related Employees collection
                    dbContext.Entry(hrDepartment)
                             .Collection(d => d.Employees)
                             .Load();


                    foreach (var emp in hrDepartment.Employees)
                    {
                        Console.WriteLine(emp.Name);
                    }

                    #endregion
                }
            }
        }
    }
}
