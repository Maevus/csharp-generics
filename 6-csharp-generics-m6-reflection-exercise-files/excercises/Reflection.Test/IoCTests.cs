using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Reflection.Test
{
    [TestClass]
    public class IoCTests
    {
        [TestMethod]
        public void CanResolveTypes()
        {
            var ioc = new Container();
            ioc.For<ILogger>().Use<SqlServerLogger>();
            // register an abstract type with associated concrete type. 
            // returns correct contrete type when asked to resolve said abstract type.
            // ie "injects" correct concrete version of abstraction.

            var logger = ioc.Resolve<ILogger>();

            Assert.AreEqual(typeof(SqlServerLogger), logger.GetType());
        }

        [TestMethod]
        public void CanResolveTypesWithoutDefaultConstructor()
        {
            var ioc = new Container();
            ioc.For<ILogger>().Use<SqlServerLogger>();
            ioc.For<IRepository<Employee>>().Use<SqlRepository<Employee>>();

            var repository = ioc.Resolve<IRepository<Employee>>();

            Assert.AreEqual(typeof(SqlRepository<Employee>), repository.GetType());
        }

        [TestMethod]
        public void CanResolveConcreteTypes()
        {
            var ioc = new Container();
            ioc.For<ILogger>().Use<SqlServerLogger>();
            ioc.For(typeof(IRepository<>)).Use(typeof(SqlRepository<>));

            var service = ioc.Resolve<InvoiceService>(); // most containers allow you to instantiate concrete type directly, without requiring any config.

            Assert.IsNotNull(service);
        }
    }
}
