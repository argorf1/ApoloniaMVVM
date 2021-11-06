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
    class ComunaModelTest

    {
        [Test]
        public void ReadAll()
        {
            List<ComunaModel> datosBD = new ComunaModel.ReadAll();
            List<ComunaModel> listaNegocio = new List<ComunaModel>();


            ComunaModel e = new ComunaModel();
            e.Detalle = "";
                e.Id = 0;
                e.IdProvincia = 0;
            
            listaNegocio.Add(e);

            //Assert.AreEqual(listaNegocio[0].Detalle, datosBD[0].Detalle);
            Assert.AreEqual(datosBD.Count(), listaNegocio.Count());
        }
    }
}
