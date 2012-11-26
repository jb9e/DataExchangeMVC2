using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataExchangeMVC;
using DataExchangeMVC.Controllers;
using DataExchangeMVC.Models;

namespace DataExchangeMVC.Tests.Controllers
{
    /// <summary>
    /// Summary description for PersonControllerTest
    /// </summary>
    [TestClass]
    public class PersonsControllerTest
    {
        public PersonsControllerTest()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void TestPersonCreate()
        {
            PersonsController controller = new PersonsController();
            var result = controller.Create() as ViewResult;
            //Assert.AreEqual("PersonEntry", result.ViewName);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void TestPersonIndex()
        {
            PersonsController controller = new PersonsController();
            var result = controller.Index() as ViewResult;
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void TestPersonQuery()
        {
            PersonsController controller = new PersonsController();
            var result = controller.PersonQuery() as ViewResult;
            Assert.IsNotNull(result);
        }

        //[TestMethod]
        //public void TestCreateRedirect()
        //{
        //    PersonsController controller = new PersonsController();
        //    Person person = new Person();
        //    person.FirstName = "Bob";
        //    person.LastName = "Roberts";
        //    person.DOB = new DateTime(1900, 4, 1);
        //    RedirectToRouteResult result = controller.Create(person) as RedirectToRouteResult;
        //    Assert.AreEqual("Index", result.RouteName);
        //}
    }
}
