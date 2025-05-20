using LogiTrack.Contexts;
using LogiTrack.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LogiTrack.Services
{
    internal class CompanyService
    {
        //this class is for managing operations on database to separate logic from presentation

        private CompanyDbContext dbContext;

        public CompanyService(CompanyDbContext _dbContext)
        {
                dbContext = _dbContext;
        }

        //is it better to return list of employees and there they get each employee name or i here return the names????? will ToString() work?
        public List<Employee> GetAllEmployees()
        {
            var employees = (from emp in dbContext.Employees
                             select emp).ToList();
            if(!employees.Any())
            {
                Console.WriteLine("No employees found");
                return new List<Employee>() ;
            }
            return employees;

        }

        public List<Employee> GetAllManagers()
        {
            var managerEmployees = (from E in dbContext.Employees
                                    where E.Role == Role.Manager
                                    select E).ToList();

            if(!managerEmployees.Any())
            {
                Console.WriteLine("No managers found");
                return new List<Employee>() ;
            }
            return managerEmployees;

        }

        //we need validation here 
        public bool AddProject( string name, string description, int departmentId, int managerId , out string errorMessage)
        {
            if (!dbContext.Departments.Any(D=>D.Id == departmentId))
            {
                errorMessage = "Invalid department ID.";
                return false;
            }
            if(!dbContext.Employees.Any(M=>M.Id == managerId && M.Role == Role.Manager))
            {
                errorMessage = "Invalid manager ID.";
                return false;
            }
            //var managerEmployees =  GetAllManagers();
            //bool isThere = managerEmployees.Any(E=>E.Id == managerId);
            //if (!isThere)
            //{
            //    errorMessage = "Invalid department ID.";
            //    return false;
            //}
            Project project = new Project()
            {
                Name = name,
                Description = description,
                DepartmentId = departmentId,
                ManagerId = managerId,
            };
            dbContext.Projects.Add(project);
            dbContext.SaveChanges();
            errorMessage = string.Empty;
            return true;
        }

        public List<Project> GetAllProjects()
        {
            var Projects = (from P in dbContext.Projects

                            select P).ToList();

            if(!Projects.Any())
            {
                Console.WriteLine("no projects found");
                return new List<Project>();
            }
            return Projects;
        }

        public void RemoveProjectByName(string name)
        {
            var project = (from P in dbContext.Projects
                           where P.Name == name
                           select P).FirstOrDefault();
            if(project == null)
            {
                Console.WriteLine("no project was found to be removed");
                return;
            }
            dbContext.Projects.Remove(project);
            dbContext.SaveChanges();
            Console.WriteLine(project.Name);
        }

        public bool AssignEmployeesToProject(string nameOfProject, Expression<Func<Employee, bool>> condition ,out string errorMessage )
        {

            var project = (from P in dbContext.Projects
                           where P.Name == nameOfProject
                           select P).FirstOrDefault();

            if(project == null)
            {
                errorMessage = "no project was found";
                return false;
            }

            var employees = dbContext.Employees.Where(condition).ToList();

            if (!employees.Any())
            {
                errorMessage = "no employees that match condition were found";
                return false;
            }
              foreach (var emp in employees)
                {
                  bool IsAssigned = dbContext.EmployeeProjects.Any(EP => EP.EmployeeId == emp.Id && EP.ProjectId == project.Id);

                    if (!IsAssigned)
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
            errorMessage = string.Empty;
            return true;
            }

        public bool ChangeRoleOfEmployeeToManager(string name, out string message)
        {
            var employee = dbContext.Employees
                                    .FirstOrDefault(emp => emp.Name == name);

            if (employee == null)
            {
                message = "No employee found";
                return false;
            }

            if (employee.Role == Role.Manager)
            {
                message = "Already a manager";
                return false;
            }

            employee.Role = Role.Manager;
            dbContext.SaveChanges();
            message = "Employee role updated to Manager.";
            return true;
        }

        public ICollection<Employee> GetAllEmployeesInDepartment(string nameOfDepartment)
        {
            var department = dbContext.Departments
                                      .Include(d => d.Employees)
                                      .FirstOrDefault(d => d.Name == nameOfDepartment);

            if (department == null)
            {
                Console.WriteLine("No department found");
                return new List<Employee>();
            }

            return department.Employees;
        }

        public ICollection<Employee> GetAllEmployeesInProject(string nameOfProject)
        {
            var project = dbContext.Projects
                                   .FirstOrDefault(p => p.Name == nameOfProject);

            if (project == null)
            {
                Console.WriteLine("No project found");
                return new List<Employee>();
            }

            var employees = dbContext.EmployeeProjects
                                      .Where(ep => ep.ProjectId == project.Id)
                                      .Include(ep => ep.Employee)
                                      .Select(ep => ep.Employee)
                                      .ToList();

            return employees;
        }

    }

}

