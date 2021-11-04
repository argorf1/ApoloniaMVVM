using System;
using System.Collections.Generic;
using System.Text;

namespace ApoloniaApp.Models
{
    public class RolModel : EntityModelBase
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public SubUnidadModel Subunidad { get; set; }
        public int RolSuperior { get; set; }

        public RolModel()
        {
            Subunidad = new SubUnidadModel();
        }
    }
}
