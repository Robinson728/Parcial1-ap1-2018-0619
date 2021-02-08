using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PrimerParcial.Entidades
{
    public class Ciudades
    {
        [Key]

        public int CiudadId { get; set; }

        public string Nombre { get; set; }
    }
}
