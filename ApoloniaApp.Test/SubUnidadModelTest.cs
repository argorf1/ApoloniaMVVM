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
    public class SubUnidadModelTests
    {
        [Test]
        public void CreateTest()

        {
            var SubUnidadModel = new SubUnidadModel();
            SubUnidadModel u = new SubUnidadModel()
            {
                Nombre = "Finanzas",
                Descripcion = "Area encargada de gastos",
                SubUnidadPadreId = 1,
                RutUnidad = "273229523"
            };
            bool result = u.Create();
            Assert.AreEqual(false, result);
        }

        [Test]
        public void ReadAllTest()
        {
            List<SubUnidadModel> datosBD = new SubUnidadModel().ReadAll();

            Assert.AreEqual(datosBD.Any(), true);
        }

        [Test]
        public void ReadByNameTest()
        {
            SubUnidadModel u = new SubUnidadModel()
            {
                Nombre = "Natura",
                RutUnidad = "27322952"

            };
            bool result = u.ReadByName();
            Assert.AreEqual(false, result);


        }


        [Test]
        public void Update()
        {
            SubUnidadModel u = new SubUnidadModel()
            {
                Id=0,
                Nombre="",
                Descripcion="",
                SubUnidadPadreId = 0,
                RutUnidad=""  
            };
            bool result = u.Update();
            Assert.AreEqual(true, result);
        }

    }
}
