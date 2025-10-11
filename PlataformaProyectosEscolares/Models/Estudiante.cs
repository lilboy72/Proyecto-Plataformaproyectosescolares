using System;

namespace PlataformaProyectosEscolares.Models
{
    public class Estudiante
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Email { get; set; }
        public string Carrera { get; set; }
        public int Semestre { get; set; }
        public string Password { get; set; }

        public string NombreCompleto => $"{Nombre} {Apellido}";
    }
}
