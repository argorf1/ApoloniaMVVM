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
    public class UsuarioInternoModelTests
    {
        [Test]
        public void CreateTest()

        {
            var UsuarioInternoModel = new UsuarioInternoModel();
            UsuarioInternoModel u = new UsuarioInternoModel()
            {
                Run = "703673463",
                Nombres = "Veronica",
                ApellidoP = "Prohens",
                ApellidoM = "Soissa",
                Email = "veroka.ps@gmail.com",
                Password = "12345678",
                PerfilDet = "Diseñador",
                EstadoDet = "Activo",
                IdPerfil = 1,
                IdEstado = 1
            };

            bool result = u.Create();

            Assert.AreEqual(false, result);
        }

        [Test]
        public void ReadAllTest()
        {

            {
                List<UsuarioInternoModel> datosBD = new UsuarioInternoModel().ReadAll();
                List<UsuarioInternoModel> listaNegocio = new List<UsuarioInternoModel>();

                UsuarioInternoModel e = new UsuarioInternoModel();

                e.Run = "167635059";
                e.Nombres = "Veronica";
                e.ApellidoP = "Chelen";
                e.ApellidoM = "Prohens";
                e.Email = "vero.c87@gmail.com";
                e.EstadoDet = "Activo";
                e.PerfilDet = "Administrador";
                e.IdPerfil = 1;
                e.IdEstado = 1;

                listaNegocio.Add(e);

                Assert.AreEqual(datosBD.Any(), true);
            };
        }



        [Test]
        public void Read_LoginTest()
        {
            UsuarioInternoModel u = new UsuarioInternoModel()
            {
                Nombres = "Veronica",
                ApellidoP = "Chelen",
                ApellidoM = "Prohens",
                Email = "vchelen@process.cl",
                IdPerfil = 1,
                Password = "12345678"
            };

            bool response = u.Read_Login();
            Assert.AreEqual(false, response);
        }


        [Test]
        public void ReadByRunTest()
        {
            UsuarioInternoModel u = new UsuarioInternoModel()
            {
                Run = "167635059"
            };
            bool response = u.ReadByRun();
            Assert.AreEqual(false, response);
        }

        [Test]
        public void ReadDesignerTest()
        {
            List<UnidadModel> datosBD = new UnidadModel().ReadAll();

            Assert.AreEqual(datosBD.Any(), true);
        }




        [Test]
        public void Update()
        {
            UsuarioInternoModel u = new UsuarioInternoModel()
            {
                Run = "76366163",
                Nombres = "Veronica",
                ApellidoP = "Prohens",
                ApellidoM = "Soissa",
                Email = "veronicap@process.cl",
                Password = "1234",
                IdPerfil = 2,
                IdEstado = 2
            };
            bool result = u.Update();
            Assert.AreEqual(true, result);
        }

    }
}