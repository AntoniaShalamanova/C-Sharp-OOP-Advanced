using System;
using System.Collections.Generic;

namespace P03.DetailPrinter
{
    class StartUp
    {
        static void Main()
        {
            Employee employee = new Employee("Pesho");
            Manager manager = new Manager("Gosho",
                new List<string> { "1", "2", "3", "4" });

            DetailsPrinter dp = new DetailsPrinter
                (new List<Employee> { employee, manager });

            dp.PrintEmployees();
        }
    }
}
