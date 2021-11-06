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
    public class PerfilModelTest
    {
        [Test]
        public void ReadAll()
        {
            List<PerfilModel> datosBD = new PerfilModel.ReadAll();
            List<PerfilModel> listaNegocio = new List<PerfilModel>();


            PerfilModel e = new PerfilModel();
            e.Id = 2;

            listaNegocio.Add(e);

            Assert.AreEqual(listaNegocio[0].Detalle, datosBD[0].Detalle);
            Assert.AreEqual(datosBD.Count(), listaNegocio.Count());
        }

        [Test]
        public void ReadByIdTest()
        {
            PerfilModel u = new PerfilModel
            {
                Id = 0
            };
            bool result = u.ReadByIdTest();
            Assert.That(result, Is.True);
        }
            
    }
}
