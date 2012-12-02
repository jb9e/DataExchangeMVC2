using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataExchangeMVC.Controllers;
using System.Web.Mvc;
using DataExchangeMVC.Models;

namespace DataExchangeMVC.Tests.Controllers
{
    [TestClass]
    public class VehiclesControllerTest
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

        //[TestMethod]
        //public void TestVehicleQuery()
        //{
        //    VehiclesController controller = new VehiclesController();
        //    Vehicle vehicle = new Vehicle();
        //    vehicle.Make = "Chevrolet";
        //    vehicle.Model = "Corvette";
        //    var result = controller.VehicleQuery(vehicle) as ViewResult;
        //    Assert.IsNotNull(result);
        //}
    }
}
