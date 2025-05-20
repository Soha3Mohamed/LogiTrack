using LogiTrack.Contexts;
using LogiTrack.Utility;
using LogiTrack.Entities;
using Microsoft.Identity.Client;
using LogiTrack.Services;

namespace LogiTrack
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using (CompanyDbContext dbContext = new CompanyDbContext())
            {

                DbSeeder.Seed(dbContext);

                var service = new CompanyService(dbContext);
                while (true)
                {
                    Console.WriteLine("\n=== Company Service Menu ===");
                    Console.WriteLine("1. List All Employees");
                    Console.WriteLine("2. List All Managers");
                    Console.WriteLine("3. Add a Project");
                    Console.WriteLine("4. List All Projects");
                    Console.WriteLine("5. Assign Employees to a Project");
                    Console.WriteLine("6. Show Employees in a Project");
                    Console.WriteLine("7. Exit");
                    Console.Write("Choose an option: ");
                    var choice = Console.ReadLine();

                    switch (choice)
                    {
                        case "1":
                            var employees = service.GetAllEmployees();
                            foreach (var emp in employees)
                                Console.WriteLine($"{emp.Id}: {emp.Name} - {emp.Role}");
                            break;

                        case "2":
                            var managers = service.GetAllManagers();
                            foreach (var mgr in managers)
                                Console.WriteLine($"{mgr.Id}: {mgr.Name}");
                            break;

                        case "3":
                            Console.Write("Enter project name: ");
                            var name = Console.ReadLine();
                            Console.Write("Enter description: ");
                            var desc = Console.ReadLine();
                            Console.Write("Enter department ID: ");
                            var deptId = int.Parse(Console.ReadLine() ?? "0");
                            Console.Write("Enter manager ID: ");
                            var mgrId = int.Parse(Console.ReadLine() ?? "0");

                            if (service.AddProject(name, desc, deptId, mgrId, out var error))
                                Console.WriteLine("Project added.");
                            else
                                Console.WriteLine($"Error: {error}");
                            break;

                        case "4":
                            var projects = service.GetAllProjects();
                            foreach (var p in projects)
                                Console.WriteLine($"{p.Id}: {p.Name} - {p.Description}");
                            break;

                        case "5":
                            Console.Write("Enter project name: ");
                            var projName = Console.ReadLine();

                            bool success = service.AssignEmployeesToProject(
                                projName,
                                e => e.Age >=28,
                                out var assignError
                            );

                            Console.WriteLine(success ? "Employees assigned." : $"Error: {assignError}");
                            break;

                        case "6":
                            Console.Write("Enter project name: ");
                            var projForList = Console.ReadLine();
                            var projectEmployees = service.GetAllEmployeesInProject(projForList);
                            foreach (var emp in projectEmployees)
                                Console.WriteLine($"{emp.Id}: {emp.Name}");
                            break;

                        case "7":
                            return;

                        default:
                            Console.WriteLine("Invalid option.");
                            break;
                    }


                }
            }
        }
    }
}
