using System;

namespace PlataformaProyectosEscolares.Models
{
    public class Calificacion
    {
        public int Id { get; set; }
        public int ProyectoId { get; set; }
        public double Puntaje { get; set; }
        public string Comentarios { get; set; }
        public DateTime FechaCalificacion { get; set; } = DateTime.Now;
        public string Profesor { get; set; }

        // Método para validar la calificación
        public bool EsCalificacionValida()
        {
            return Puntaje >= 0 && Puntaje <= 5.0 && !string.IsNullOrEmpty(Profesor);
        }

        // Método para obtener estado de aprobación
        public string ObtenerEstado()
        {
            return Puntaje >= 3.0 ? "Aprobado" : "Reprobado";
        }

        public override string ToString()
        {
            return $"Calificación: {Puntaje}/5.0 - {ObtenerEstado()} - {FechaCalificacion:dd/MM/yyyy}";
        }
    }
}
