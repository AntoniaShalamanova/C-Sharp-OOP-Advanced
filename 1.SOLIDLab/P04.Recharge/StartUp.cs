namespace P04.Recharge
{
    using System;

    class StartUp
    {
        static void Main()
        {
            Employee employee = new Employee("1");
            Robot robot = new Robot("2", 20);

            employee.Sleep();
            robot.Recharge();
        }
    }
}
