using System;
using System.Collections.Generic;
using System.Text;
using ApoloniaApp.Models;
using ApoloniaApp.ViewModels;
using ApoloniaApp;
using NUnit.Framework;
using Oracle.ManagedDataAccess.Client;

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
                Nombre = "",
                Descripcion = "",
                SubUnidadPadreId = 0,
                RutUnidad = ""
            };
            bool result = u.Create();
            Assert.AreEqual(false, result);
        }

        [Test]
        public void ReadAllTest()
        {

            //SubUnidadModel u = new SubUnidadModel()
            //{
            //    Id = 0,
            //    Nombre = "",
            //    Descripcion = "",
            //    SubUnidadPadre = "",
            //    SubUnidadPadreId = 0,
            //    NumFuncionarios = 12,
            //    NumProcesos = 0
            //};

            //List<SubUnidadModel> response = u.ReadAll();
            //CollectionAssert.AreEqual(u, response);
        }

        [Test]
        public void ReadByName()
        {
            SubUnidadModel u = new SubUnidadModel()
            {
                Nombre = "NOMBREPRUEBA"
            };
            //List<SubUnidadModel> response = u.ReadByName(u.Nombre.ToString());
            string  response = u.ReadByName_RRA(u.Nombre.ToString());
            CollectionAssert.AreEqual(u.Nombre, response);

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
            Assert.AreEqual(false, result);
        }

    }
}
