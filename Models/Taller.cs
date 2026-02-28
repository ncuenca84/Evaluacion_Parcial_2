using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Evaluacion_Parcial_2.Models
{
    public class Taller
    {
        public int TallerId { get; set; }

        [Required]
        public string Nombre { get; set; } = string.Empty;

        public string? Descripcion { get; set; }

        public DateTime Fecha { get; set; }

        public string? Ubicacion { get; set; }

        public ICollection<Inscripcion> Inscripciones { get; set; } = new List<Inscripcion>();
    }
}