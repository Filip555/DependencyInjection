using Castle.MicroKernel.Registration;
using Castle.Windsor;
using System;

namespace DI
{
    class Program
    {
        public interface IEmployee
        {
            void WorkOn(string message);
        }
        public interface IEmployer
        {
            void CommandForTheEmployee(string message);
        }
        public class JuniorProgrammer : IEmployee
        {
            public void WorkOn(string message)
            {
                string WhoIAm = "Jestem młodszym programistą i wykonam czynność o która szef prosi: ";
                Console.WriteLine(WhoIAm + message);
            }
        }
        public class SeniorProgrammer : IEmployee
        {
            public void WorkOn(string message)
            {
                string WhoIAm = "Jestem starszym programistą i wykonam czynność o która szef prosi: ";
                Console.WriteLine(WhoIAm + message);
            }
        }
        public class Boss : IEmployer
        {
            IEmployee employee = null;
            public Boss(IEmployee _employee)
            {
                employee = _employee;
            }
            public void CommandForTheEmployee(string message)
            {
                employee.WorkOn(message);
            }
        }
        public static class SetContainer
        {
            public static void SetIt()
            {
                var container = new WindsorContainer();
                container.Register
                    (
                        // gdy dam Ci to
                        Component.For<IEmployee>()
                        // daj im to
                        .ImplementedBy<JuniorProgrammer>(),

                        Component.For<IEmployer>().ImplementedBy<Boss>()
                    );

                Container = container;
            }

            public static T Resolve<T>()
            {
                return Container.Resolve<T>();
            }

            public static IWindsorContainer Container;
        }


        static void Main(string[] args)
        {
            //JuniorProgrammer junior= new JuniorProgrammer();
            //SeniorProgrammer senior = new SeniorProgrammer();
            //Boss boss = new Boss(junior);
            //boss.CommandForTheEmployee("Napisz aplikacje w MVC");
            //boss = new Boss(senior);
            //boss.CommandForTheEmployee("Napisz aplikacje w WPF");

            SetContainer.SetIt();
            var a = SetContainer.Container.Resolve<IEmployer>();
            a.CommandForTheEmployee("Napisz aplikacje w MVC");
            Console.ReadKey();
        }
    }
}