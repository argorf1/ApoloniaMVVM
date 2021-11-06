using System;
using System.Collections.Generic;
using System.Text;
using ApoloniaApp.Models;
using ApoloniaApp.ViewModels;
using ApoloniaApp;
using NUnit.Framework;
using Oracle.ManagedDataAccess.Client;
using System.Linq;

namespace ApoloniaApp.Test
{
    [TestFixture]
    public class RegionModelTest
    {
        [Test]
        public void ReadAllTest()
            
        {
            List<RegionModel> datosBD = new RegionModel.ReadAll();
            List<RegionModel> listaNegocio = new List<RegionModel>();


            RegionModel e = new RegionModel();
            e.Detalle = "Habia una vez";
            e.Id = 2;

            listaNegocio.Add(e);

            Assert.AreEqual(datosBD.Count(), listaNegocio.Count());

        }

    }
}
