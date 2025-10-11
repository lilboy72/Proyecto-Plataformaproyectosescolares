using System;

namespace PlataformaProyectosEscolares.Models
{
    public class Proyecto
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public int EstudianteId { get; set; }
        public int ProfesorId { get; set; }
        public DateTime FechaEntrega { get; set; }
        public decimal? Calificacion { get; set; }
        public string ArchivoPath { get; set; }

        // Propiedades de navegación
        public string NombreEstudiante { get; set; }
        public string NombreProfesor { get; set; }
    }
}
