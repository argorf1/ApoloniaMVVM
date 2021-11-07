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
    public class ProvinciaModelTest
    {
        [Test]
        public void ReadAll()
        {
            List<ProvinciaModel> datosBD = new ProvinciaModel().ReadAll();
            List<ProvinciaModel> listaNegocio = new List<ProvinciaModel>();


            ProvinciaModel e = new ProvinciaModel();
            e.Detalle = "Habia una vez";
            e.Id = 2;

            listaNegocio.Add(e);

            Assert.AreEqual(datosBD.Count(), listaNegocio.Count());

        }
    }
}
