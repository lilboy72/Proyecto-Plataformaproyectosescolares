namespace PlataformaProyectosEscolares.Models
{
    public class Profesor
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Email { get; set; }
        public string Departamento { get; set; }
        public string Password { get; set; }

        public string NombreCompleto => $"{Nombre} {Apellido}";
    }
}
