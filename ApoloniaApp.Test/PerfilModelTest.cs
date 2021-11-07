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
            List<PerfilModel> datosBD = new PerfilModel().ReadAll();
            
            Assert.AreEqual(datosBD.Any(), true);
        }

        //[Test]
        //public void ReadByIdTest()
        //{
        //    PerfilModel u = new PerfilModel
        //    {
        //        Id = 1
        //    };
        //    bool result = u.ReadById();
        //    Assert.AreEqual(false, result);
        //}

    }
}
