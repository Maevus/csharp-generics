using System;
using System.Collections.Generic;
using System.Data.Entity;

namespace QueryIt
{
    class Program
    {
        static void Main(string[] args)
        {
            Database.SetInitializer(new DropCreateDatabaseAlways<EmployeeDb>());
            Console.WriteLine("Generated database");

            using (IRepository<Employee> employeeRespository = new SqlRepository<Employee>(new EmployeeDb()))
            {
                AddEmployee(employeeRespository);
                CountEmployee(employeeRespository);
                QueryEmployee(employeeRespository);
                DumpPeople(employeeRespository);
                AddManagers(employeeRespository);

                IEnumerable<Person> temp = employeeRespository.FindAll();
            }
        }

        private static void AddManagers(IWriteOnlyRepository<Manager> employeeRespository)
        {
            throw new NotImplementedException();
        }

        private static void DumpPeople(IReadOnlyRepository<Employee> employeeRespository)
        {
            var employees = employeeRespository.FindAll();
            foreach(var employee in employees)
            {
                Console.WriteLine(employee.Name);
            }
        }

        private static void QueryEmployee(IRepository<Employee> employeeRespository)
        {
            var employee = employeeRespository.FindById(1);
            Console.WriteLine(employee.Name);
        }

        private static void AddEmployee(IRepository<Employee> employeeRespository)
        {
            Console.WriteLine("Adding employees");
            employeeRespository.Add(new Employee { Name = "Scott" });
            employeeRespository.Add(new Employee { Name = "Chris" });
            employeeRespository.Add(new Manager { Name = "Chris" });
        }

        private static void CountEmployee(IRepository<Employee> employeeRespository)
        {
            Console.WriteLine("Counting employee...");
            Console.WriteLine(employeeRespository.FindAll());
        }
    }
}
