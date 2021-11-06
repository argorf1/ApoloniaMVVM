﻿using System;
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
    public class UnidadModelTests
    {
        [Test]
        public void CreateTest()
        {

            var UnidadModel = new UnidadModel();
            UnidadModel u = new UnidadModel()
            {
                Rut = "96.575.280-3",
                RazonSocial = "Natura cosméticos ",
                RubroId = 11118,
                Calle = "Av. Padre Hurtado Sur",
                Numero = "875",
                Complemento = "",
                ComunaId = 13114,
                PersonaContacto = "Pia Fernandez",
                TelefonoContacto = 22343454,
                EmailContacto = "pfernandez@natura.cl",
                ResponsableRun = "100787989"
            };

            bool result = u.ReadByRutTest();
            Assert.That(result, Is.True);

        }

        [Test]
        public void ReadAllTest()
        {
            List<UnidadModel> datosBD = new UnidadModel.ReadAll();
            List<UnidadModel> listaNegocio = new List<UnidadModel>();


            UnidadModel e = new UnidadModel();
            e.Detalle = "Habia una vez";
            e.Id = 2;

            listaNegocio.Add(e);

            //Assert.AreEqual(listaNegocio[0].Detalle, datosBD[0].Detalle);
            Assert.AreEqual(datosBD.Count(), listaNegocio.Count());
        }


        [Test]
        public bool ReadByRutTest()
        {
            UnidadModel u = new UnidadModel()
            {
                Rut = ""

            };
            bool result = u.ReadByRutTest();
            Assert.That(result, Is.True);

        }

        [Test]
        public bool UpdateTest()
        {
            UnidadModel u = new UnidadModel()
            {
                Rut = "",
                RazonSocial = "",
                RubroId = 0,
                DireccionId = 0,
                Calle = "",
                Numero = "",
                Complemento = "",
                ComunaId = 0,
                PersonaContacto = "",
                TelefonoContacto = 0,
                EmailContacto = "",
                ResponsableRun = "167635059",
                EstadoId = 0
            };
            bool result = u.UpdateTest();
            Assert.That(result, Is.True);

        }

    }
}
