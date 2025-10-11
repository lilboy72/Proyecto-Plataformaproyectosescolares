namespace PlataformaProyectosEscolares.Forms
{
    partial class CalificarProyectoForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.lblProyecto = new System.Windows.Forms.Label();
            this.lblTituloProyecto = new System.Windows.Forms.Label();
            this.lblCalificacion = new System.Windows.Forms.Label();
            this.numCalificacion = new System.Windows.Forms.NumericUpDown();
            this.lblComentarios = new System.Windows.Forms.Label();
            this.txtComentarios = new System.Windows.Forms.TextBox();
            this.btnGuardarCalificacion = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.lblMensaje = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numCalificacion)).BeginInit();
            this.SuspendLayout();
            // 
            // lblProyecto
            // 
            this.lblProyecto.AutoSize = true;
            this.lblProyecto.Location = new System.Drawing.Point(20, 20);
            this.lblProyecto.Name = "lblProyecto";
            this.lblProyecto.Size = new System.Drawing.Size(52, 13);
            this.lblProyecto.TabIndex = 0;
            this.lblProyecto.Text = "Proyecto:";
            // 
            // lblTituloProyecto
            // 
            this.lblTituloProyecto.AutoSize = true;
            this.lblTituloProyecto.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTituloProyecto.Location = new System.Drawing.Point(80, 20);
            this.lblTituloProyecto.Name = "lblTituloProyecto";
            this.lblTituloProyecto.Size = new System.Drawing.Size(100, 13);
            this.lblTituloProyecto.TabIndex = 1;
            this.lblTituloProyecto.Text = "Título del proyecto";
            // 
            // lblCalificacion
            // 
            this.lblCalificacion.AutoSize = true;
            this.lblCalificacion.Location = new System.Drawing.Point(20, 60);
            this.lblCalificacion.Name = "lblCalificacion";
            this.lblCalificacion.Size = new System.Drawing.Size(64, 13);
            this.lblCalificacion.TabIndex = 2;
            this.lblCalificacion.Text = "Calificación:";
            // 
            // numCalificacion
            // 
            this.numCalificacion.DecimalPlaces = 1;
            this.numCalificacion.Location = new System.Drawing.Point(90, 58);
            this.numCalificacion.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numCalificacion.Name = "numCalificacion";
            this.numCalificacion.Size = new System.Drawing.Size(60, 20);
            this.numCalificacion.TabIndex = 1;
            this.numCalificacion.ValueChanged += new System.EventHandler(this.numCalificacion_ValueChanged);
            // 
            // lblComentarios
            // 
            this.lblComentarios.AutoSize = true;
            this.lblComentarios.Location = new System.Drawing.Point(20, 100);
            this.lblComentarios.Name = "lblComentarios";
            this.lblComentarios.Size = new System.Drawing.Size(68, 13);
            this.lblComentarios.TabIndex = 4;
            this.lblComentarios.Text = "Comentarios:";
            // 
            // txtComentarios
            // 
            this.txtComentarios.Location = new System.Drawing.Point(20, 120);
            this.txtComentarios.Multiline = true;
            this.txtComentarios.Name = "txtComentarios";
            this.txtComentarios.Size = new System.Drawing.Size(400, 100);
            this.txtComentarios.TabIndex = 2;
            // 
            // btnGuardarCalificacion
            // 
            this.btnGuardarCalificacion.Location = new System.Drawing.Point(240, 240);
            this.btnGuardarCalificacion.Name = "btnGuardarCalificacion";
            this.btnGuardarCalificacion.Size = new System.Drawing.Size(85, 30);
            this.btnGuardarCalificacion.TabIndex = 3;
            this.btnGuardarCalificacion.Text = "Guardar";
            this.btnGuardarCalificacion.UseVisualStyleBackColor = true;
            this.btnGuardarCalificacion.Click += new System.EventHandler(this.btnGuardarCalificacion_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.Location = new System.Drawing.Point(335, 240);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(85, 30);
            this.btnCancelar.TabIndex = 4;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // lblMensaje
            // 
            this.lblMensaje.AutoSize = true;
            this.lblMensaje.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMensaje.ForeColor = System.Drawing.Color.Green;
            this.lblMensaje.Location = new System.Drawing.Point(160, 60);
            this.lblMensaje.Name = "lblMensaje";
            this.lblMensaje.Size = new System.Drawing.Size(0, 13);
            this.lblMensaje.TabIndex = 8;
            // 
            // CalificarProyectoForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(440, 290);
            this.Controls.Add(this.lblMensaje);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnGuardarCalificacion);
            this.Controls.Add(this.txtComentarios);
            this.Controls.Add(this.lblComentarios);
            this.Controls.Add(this.numCalificacion);
            this.Controls.Add(this.lblCalificacion);
            this.Controls.Add(this.lblTituloProyecto);
            this.Controls.Add(this.lblProyecto);
            this.Name = "CalificarProyectoForm";
            this.Text = "Calificar Proyecto";
            this.Load += new System.EventHandler(this.CalificarProyectoForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numCalificacion)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblProyecto;
        private System.Windows.Forms.Label lblTituloProyecto;
        private System.Windows.Forms.Label lblCalificacion;
        private System.Windows.Forms.NumericUpDown numCalificacion;
        private System.Windows.Forms.Label lblComentarios;
        private System.Windows.Forms.TextBox txtComentarios;
        private System.Windows.Forms.Button btnGuardarCalificacion;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Label lblMensaje;
    }
}
