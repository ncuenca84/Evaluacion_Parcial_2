using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Evaluacion_Parcial_2.Models
{
    public class Participante
    {
        public int ParticipanteId { get; set; }

        [Required]
        public string Nombre { get; set; } = string.Empty;

        [Required]
        public string Apellido { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        public string? Telefono { get; set; }

        public ICollection<Inscripcion> Inscripciones { get; set; } = new List<Inscripcion>();
    }
}