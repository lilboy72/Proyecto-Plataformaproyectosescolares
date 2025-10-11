using PlataformaProyectosEscolares.Data;
using System.Data;
using System.Windows.Forms;

namespace PlataformaProyectosEscolares.Service
{
    public class ProyectoService
    {
        private DatabaseHelper _dbHelper;

        public ProyectoService()
        {
            _dbHelper = new DatabaseHelper();
        }

        // MÉTODOS PARA PROYECTOS
        public DataTable ObtenerTodosLosProyectos()
        {
            return _dbHelper.GetProyectos();
        }

        public DataTable ObtenerProyectosParaProfesor()
        {
            return _dbHelper.GetProyectosParaProfesor();
        }

        public bool InsertarProyecto(string titulo, string descripcion, int estudianteId, int profesorId)
        {
            return _dbHelper.InsertarProyecto(titulo, descripcion, estudianteId, profesorId);
        }

        public bool ActualizarProyecto(int id, string titulo, string descripcion, int estudianteId, int profesorId)
        {
            return _dbHelper.ActualizarProyecto(id, titulo, descripcion, estudianteId, profesorId);
        }

        public bool EliminarProyecto(int id)
        {
            return _dbHelper.EliminarProyecto(id);
        }

        public bool CalificarProyecto(int proyectoId, decimal calificacion, string comentario)
        {
            // Validar calificación (0-10)
            if (calificacion < 0 || calificacion > 10)
            {
                MessageBox.Show("La calificación debe estar entre 0 y 10", "Validación",
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return _dbHelper.CalificarProyecto(proyectoId, calificacion, comentario);
        }

        public DataTable ObtenerProyectosPorEstudiante(int estudianteId)
        {
            return _dbHelper.GetProyectosPorEstudiante(estudianteId);
        }

        public DataTable ObtenerProyectosPorProfesor(int profesorId)
        {
            return _dbHelper.GetProyectosPorProfesor(profesorId);
        }

        // MÉTODOS PARA ESTUDIANTES
        public DataTable ObtenerTodosLosEstudiantes()
        {
            return _dbHelper.GetEstudiantes();
        }

        public bool InsertarEstudiante(string nombre, string email, string grado)
        {
            return _dbHelper.InsertarEstudiante(nombre, email, grado);
        }

        // MÉTODOS PARA PROFESORES
        public DataTable ObtenerTodosLosProfesores()
        {
            return _dbHelper.GetProfesores();
        }

        public bool InsertarProfesor(string nombre, string especialidad, string email)
        {
            return _dbHelper.InsertarProfesor(nombre, especialidad, email);
        }

        // MÉTODOS DE UTILIDAD
        public bool ProbarConexion()
        {
            return _dbHelper.TestConnection();
        }

        // MÉTODO PARA CARGAR DATOS EN UN DATA GRID VIEW
        public void CargarProyectosEnDataGridView(DataGridView dataGridView)
        {
            try
            {
                DataTable proyectos = ObtenerTodosLosProyectos();
                dataGridView.DataSource = proyectos;

                dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
            catch (System.Exception ex)
            {
                MessageBox.Show($"Error al cargar proyectos: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // MÉTODO PARA CARGAR PROYECTOS EN DATA GRID VIEW (PARA PROFESOR)
        public void CargarProyectosEnDataGridViewProfesor(DataGridView dataGridView)
        {
            try
            {
                DataTable proyectos = ObtenerProyectosParaProfesor();
                dataGridView.DataSource = proyectos;

                dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
            catch (System.Exception ex)
            {
                MessageBox.Show($"Error al cargar proyectos: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // MÉTODO PARA CARGAR ESTUDIANTES EN UN COMBO BOX
        public void CargarEstudiantesEnComboBox(ComboBox comboBox)
        {
            try
            {
                DataTable estudiantes = ObtenerTodosLosEstudiantes();
                comboBox.DataSource = estudiantes;
                comboBox.DisplayMember = "Nombre";
                comboBox.ValueMember = "Id";
            }
            catch (System.Exception ex)
            {
                MessageBox.Show($"Error al cargar estudiantes: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // MÉTODO PARA CARGAR PROFESORES EN UN COMBO BOX
        public void CargarProfesoresEnComboBox(ComboBox comboBox)
        {
            try
            {
                DataTable profesores = ObtenerTodosLosProfesores();
                comboBox.DataSource = profesores;
                comboBox.DisplayMember = "Nombre";
                comboBox.ValueMember = "Id";
            }
            catch (System.Exception ex)
            {
                MessageBox.Show($"Error al cargar profesores: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // MÉTODO PARA VALIDAR DATOS DE PROYECTO
        public bool ValidarDatosProyecto(string titulo, string descripcion, int estudianteId, int profesorId)
        {
            if (string.IsNullOrWhiteSpace(titulo))
            {
                MessageBox.Show("El título no puede estar vacío", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (string.IsNullOrWhiteSpace(descripcion))
            {
                MessageBox.Show("La descripción no puede estar vacía", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (estudianteId <= 0)
            {
                MessageBox.Show("Debe seleccionar un estudiante", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (profesorId <= 0)
            {
                MessageBox.Show("Debe seleccionar un profesor", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }
    }
}
