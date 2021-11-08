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
        public void ReadAllTest()
        {
            
            {
                List<EstadoModel> datosBD = new EstadoModel().ReadAll();
                
                Assert.AreEqual(datosBD.Any(), true);

            };
            

        }


    }
}
