using System;
using System.Collections.Generic;
using System.Text;

namespace ApoloniaApp.Models
{
    public class UsuarioModelBase : EntityModelBase
    {
        public string Run { get; set; }
        
        public string ApellidoP { get; set; }
        public string ApellidoM { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public EstadoModel Estado { get; set; }
        public string NombreCompleto => (Nombre + " " + ApellidoP + " " + ApellidoM);



        public UsuarioModelBase()
        {
            Run = "";
            Nombre = "";
            ApellidoP = "";
            ApellidoM = "";
            Email = "";
            Password = "";
            Estado = new EstadoModel();
        }
    }
}
