using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataExchangeMVC.Controllers;
using System.Web.Mvc;

namespace DataExchangeMVC.Tests.Controllers
{
    [TestClass]
    public class VehicleControllerTest
    {
        [TestMethod]
        public void TestVehicleIndex()
        {
            VehiclesController controller = new VehiclesController();
            var result = controller.Index() as ViewResult;
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void TestVehicleCreate()
        {
            VehiclesController controller = new VehiclesController();
            var result = controller.Create() as ViewResult;
            Assert.IsNotNull(result);
        }
    }
}
