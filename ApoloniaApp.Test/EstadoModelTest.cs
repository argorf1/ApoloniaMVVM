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
    public class EstadoModelTest
    {
        [Test]
        public void ReadAll()
        {
            
            {
                List<EstadoModel> datosBD = new EstadoModel.ReadAll();
                List<EstadoModel> listaNegocio = new List<EstadoModel>();

                EstadoModel e = new EstadoModel();

                e.Detalle = "Activo";
                e.Id = 1;
                
                listaNegocio.Add(e);
                Assert.AreEqual(datosBD.Count(), listaNegocio.Count());

            };
            

        }


    }
}
