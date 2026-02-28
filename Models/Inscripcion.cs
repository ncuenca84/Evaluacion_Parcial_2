using System;
using System.ComponentModel.DataAnnotations;

namespace Evaluacion_Parcial_2.Models
{
    public class Inscripcion
    {
        public int InscripcionId { get; set; }

        public int TallerId { get; set; }
        public int ParticipanteId { get; set; }

        public DateTime FechaInscripcion { get; set; } = DateTime.Now;

        public Taller? Taller { get; set; }
        public Participante? Participante { get; set; }
    }
}