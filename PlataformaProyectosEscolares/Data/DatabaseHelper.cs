using MySql.Data.MySqlClient;
using System;
using System.Configuration;
using System.Data;
using System.Windows.Forms;

namespace PlataformaProyectosEscolares.Data
{
    public class DatabaseHelper
    {
        private readonly string _connectionString;

        public DatabaseHelper()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        }

        // Método para probar la conexión
        public bool TestConnection()
        {
            try
            {
                using (var connection = new MySqlConnection(_connectionString))
                {
                    connection.Open();
                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error de conexión: {ex.Message}");
                return false;
            }
        }

        // MÉTODOS PARA ESTUDIANTES
        public DataTable GetEstudiantes()
        {
            DataTable dataTable = new DataTable();

            try
            {
                using (var connection = new MySqlConnection(_connectionString))
                {
                    connection.Open();

                    string query = "SELECT * FROM Estudiantes";
                    using (var command = new MySqlCommand(query, connection))
                    {
                        using (var adapter = new MySqlDataAdapter(command))
                        {
                            adapter.Fill(dataTable);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al obtener estudiantes: {ex.Message}");
            }

            return dataTable;
        }

        public bool InsertarEstudiante(string nombre, string email, string grado)
        {
            try
            {
                using (var connection = new MySqlConnection(_connectionString))
                {
                    connection.Open();

                    string query = "INSERT INTO Estudiantes (Nombre, Email, Grado) VALUES (@nombre, @email, @grado)";

                    using (var command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@nombre", nombre);
                        command.Parameters.AddWithValue("@email", email);
                        command.Parameters.AddWithValue("@grado", grado);

                        return command.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al insertar estudiante: {ex.Message}");
                return false;
            }
        }

        // MÉTODOS PARA PROFESORES
        public DataTable GetProfesores()
        {
            DataTable dataTable = new DataTable();

            try
            {
                using (var connection = new MySqlConnection(_connectionString))
                {
                    connection.Open();

                    string query = "SELECT * FROM Profesores";
                    using (var command = new MySqlCommand(query, connection))
                    {
                        using (var adapter = new MySqlDataAdapter(command))
                        {
                            adapter.Fill(dataTable);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al obtener profesores: {ex.Message}");
            }

            return dataTable;
        }

        public bool InsertarProfesor(string nombre, string especialidad, string email)
        {
            try
            {
                using (var connection = new MySqlConnection(_connectionString))
                {
                    connection.Open();

                    string query = "INSERT INTO Profesores (Nombre, Especialidad, Email) VALUES (@nombre, @especialidad, @email)";

                    using (var command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@nombre", nombre);
                        command.Parameters.AddWithValue("@especialidad", especialidad);
                        command.Parameters.AddWithValue("@email", email);

                        return command.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al insertar profesor: {ex.Message}");
                return false;
            }
        }

        // MÉTODOS PARA PROYECTOS
        public DataTable GetProyectos()
        {
            DataTable dataTable = new DataTable();

            try
            {
                using (var connection = new MySqlConnection(_connectionString))
                {
                    connection.Open();

                    string query = @"SELECT p.*, e.Nombre as EstudianteNombre, pr.Nombre as ProfesorNombre 
                                   FROM Proyectos p
                                   LEFT JOIN Estudiantes e ON p.EstudianteId = e.Id
                                   LEFT JOIN Profesores pr ON p.ProfesorId = pr.Id";

                    using (var command = new MySqlCommand(query, connection))
                    {
                        using (var adapter = new MySqlDataAdapter(command))
                        {
                            adapter.Fill(dataTable);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al obtener proyectos: {ex.Message}");
            }

            return dataTable;
        }

        // MÉTODO PARA OBTENER PROYECTOS CON CALIFICACIONES (PARA PROFESOR)
        public DataTable GetProyectosParaProfesor()
        {
            DataTable dataTable = new DataTable();

            try
            {
                using (var connection = new MySqlConnection(_connectionString))
                {
                    connection.Open();

                    string query = @"SELECT p.Id, p.Titulo, p.Descripcion, p.Fecha, 
                                   p.Calificacion, p.ComentarioProfesor, p.FechaCalificacion,
                                   e.Nombre as EstudianteNombre, 
                                   pr.Nombre as ProfesorNombre
                                   FROM Proyectos p
                                   LEFT JOIN Estudiantes e ON p.EstudianteId = e.Id
                                   LEFT JOIN Profesores pr ON p.ProfesorId = pr.Id
                                   ORDER BY p.Fecha DESC";

                    using (var command = new MySqlCommand(query, connection))
                    {
                        using (var adapter = new MySqlDataAdapter(command))
                        {
                            adapter.Fill(dataTable);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al obtener proyectos para profesor: {ex.Message}");
            }

            return dataTable;
        }

        // MÉTODO PARA CALIFICAR PROYECTO
        public bool CalificarProyecto(int proyectoId, decimal calificacion, string comentario)
        {
            try
            {
                using (var connection = new MySqlConnection(_connectionString))
                {
                    connection.Open();

                    string query = @"UPDATE Proyectos 
                                   SET Calificacion = @calificacion, 
                                       ComentarioProfesor = @comentario,
                                       FechaCalificacion = @fecha
                                   WHERE Id = @id";

                    using (var command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@calificacion", calificacion);
                        command.Parameters.AddWithValue("@comentario", comentario);
                        command.Parameters.AddWithValue("@fecha", DateTime.Now.Date);
                        command.Parameters.AddWithValue("@id", proyectoId);

                        return command.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al calificar proyecto: {ex.Message}");
                return false;
            }
        }

        public bool InsertarProyecto(string titulo, string descripcion, int estudianteId, int profesorId)
        {
            try
            {
                using (var connection = new MySqlConnection(_connectionString))
                {
                    connection.Open();

                    string query = "INSERT INTO Proyectos (Titulo, Descripcion, EstudianteId, ProfesorId, Fecha) VALUES (@titulo, @descripcion, @estudianteId, @profesorId, @fecha)";

                    using (var command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@titulo", titulo);
                        command.Parameters.AddWithValue("@descripcion", descripcion);
                        command.Parameters.AddWithValue("@estudianteId", estudianteId);
                        command.Parameters.AddWithValue("@profesorId", profesorId);
                        command.Parameters.AddWithValue("@fecha", DateTime.Now);

                        return command.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al insertar proyecto: {ex.Message}");
                return false;
            }
        }

        // MÉTODOS PARA ACTUALIZAR
        public bool ActualizarProyecto(int id, string titulo, string descripcion, int estudianteId, int profesorId)
        {
            try
            {
                using (var connection = new MySqlConnection(_connectionString))
                {
                    connection.Open();

                    string query = "UPDATE Proyectos SET Titulo = @titulo, Descripcion = @descripcion, EstudianteId = @estudianteId, ProfesorId = @profesorId WHERE Id = @id";

                    using (var command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@titulo", titulo);
                        command.Parameters.AddWithValue("@descripcion", descripcion);
                        command.Parameters.AddWithValue("@estudianteId", estudianteId);
                        command.Parameters.AddWithValue("@profesorId", profesorId);
                        command.Parameters.AddWithValue("@id", id);

                        return command.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al actualizar proyecto: {ex.Message}");
                return false;
            }
        }

        // MÉTODOS PARA ELIMINAR
        public bool EliminarProyecto(int id)
        {
            try
            {
                using (var connection = new MySqlConnection(_connectionString))
                {
                    connection.Open();

                    string query = "DELETE FROM Proyectos WHERE Id = @id";

                    using (var command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@id", id);

                        return command.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al eliminar proyecto: {ex.Message}");
                return false;
            }
        }

        // MÉTODO PARA OBTENER PROYECTOS POR ESTUDIANTE
        public DataTable GetProyectosPorEstudiante(int estudianteId)
        {
            DataTable dataTable = new DataTable();

            try
            {
                using (var connection = new MySqlConnection(_connectionString))
                {
                    connection.Open();

                    string query = @"SELECT p.*, pr.Nombre as ProfesorNombre 
                                   FROM Proyectos p
                                   LEFT JOIN Profesores pr ON p.ProfesorId = pr.Id
                                   WHERE p.EstudianteId = @estudianteId";

                    using (var command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@estudianteId", estudianteId);
                        using (var adapter = new MySqlDataAdapter(command))
                        {
                            adapter.Fill(dataTable);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al obtener proyectos del estudiante: {ex.Message}");
            }

            return dataTable;
        }

        // MÉTODO PARA OBTENER PROYECTOS POR PROFESOR
        public DataTable GetProyectosPorProfesor(int profesorId)
        {
            DataTable dataTable = new DataTable();

            try
            {
                using (var connection = new MySqlConnection(_connectionString))
                {
                    connection.Open();

                    string query = @"SELECT p.*, e.Nombre as EstudianteNombre 
                                   FROM Proyectos p
                                   LEFT JOIN Estudiantes e ON p.EstudianteId = e.Id
                                   WHERE p.ProfesorId = @profesorId";

                    using (var command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@profesorId", profesorId);
                        using (var adapter = new MySqlDataAdapter(command))
                        {
                            adapter.Fill(dataTable);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al obtener proyectos del profesor: {ex.Message}");
            }

            return dataTable;
        }
    }
}
