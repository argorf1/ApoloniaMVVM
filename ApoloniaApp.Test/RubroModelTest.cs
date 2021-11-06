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
    public class RubroModelTest
    {
        [Test]
        public void ReadAll()
        {

            //List<RubroModel> datosBD = new RubroModel.ReadAll();
            //List<RubroModel> listaNegocio = new List<RubroModel>();
            //RubroModel e = new RubroModel();
            //{
            //    e.Detalle = "Construcción";
            //    e.Id = 11113;
            //};

            //listaNegocio.Add(e);

            //Assert.AreEqual(datosBD.Count(), listaNegocio.Count());
        }
    }
}
