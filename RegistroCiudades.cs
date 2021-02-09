using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PrimerParcial.Entidades;
using PrimerParcial.BLL;

namespace PrimerParcial
{
    public partial class RegistroCiudades : Form
    {
        public RegistroCiudades()
        {
            InitializeComponent();
        }

        private void RegistroCiudades_Load(object sender, EventArgs e)
        {

        }

        private void Limpiar()
        {
            IdNumericUpDown1.Value = 0;
            NombreTextBox.Clear();
            ErrorProvider.Clear();
        }

        private void LlenaCampo(Ciudades ciudad)
        {
            IdNumericUpDown1.Value = ciudad.CiudadId;
            NombreTextBox.Text = ciudad.Nombre;
        }

        private Ciudades LlenaClase()
        {
            Ciudades ciudad = new Ciudades();
            ciudad.CiudadId = (int)IdNumericUpDown1.Value;
            ciudad.Nombre = NombreTextBox.Text;

            return ciudad;
        }

        private bool ExisteEnLaBaseDeDatos()
        {
            Ciudades ciudad = CiudadesBLL.Buscar((int)IdNumericUpDown1.Value);

            return (ciudad != null);
        }

        private bool Validar()
        {
            bool paso = true;
            ErrorProvider.Clear();

            if(IdNumericUpDown1.Value == 0)
            {
                ErrorProvider.SetError(IdNumericUpDown1, "El Id no puede ser igual a cero (0)");
                IdNumericUpDown1.Focus();
                paso = false;
            }
            else if(NombreTextBox.Text == string.Empty)
            {
                ErrorProvider.SetError(NombreTextBox, "El campo nombre no puede estar vacío");
                NombreTextBox.Focus();
                paso = false;
            }

            return paso;
        }

        private void BuscarButton_Click(object sender, EventArgs e)
        {
            int id;
            Ciudades ciudad = new Ciudades();
            int.TryParse(IdNumericUpDown1.Text, out id);

            ciudad = CiudadesBLL.Buscar(id);

            if (ciudad != null)
                LlenaCampo(ciudad);
            else
                MessageBox.Show("Persona no encontrada", "Id no existente", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void NuevoButton_Click(object sender, EventArgs e)
        {
            Limpiar();
        }

        private void GuardarButton_Click(object sender, EventArgs e)
        {
            Ciudades ciudad;
            bool paso = false;

            if (Validar())
                return;

            ciudad = LlenaClase();

            if (ExisteEnLaBaseDeDatos())
            {
                paso = CiudadesBLL.Guardar(ciudad);
                MessageBox.Show("Modificado Correctamente", "Correcto", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                paso = CiudadesBLL.Guardar(ciudad);

                if (paso)
                {
                    Limpiar();
                    MessageBox.Show("Guardado Correctamente", "Correcto", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                    MessageBox.Show("No se pudo guardar", "Incorrecto", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void EliminarButton_Click(object sender, EventArgs e)
        {
            ErrorProvider.Clear();
            int id;
            int.TryParse(IdNumericUpDown1.Text, out id);

            Limpiar();

            if (CiudadesBLL.Eliminar(id))
                MessageBox.Show("Eliminado Correctamente", "Correcto", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                ErrorProvider.SetError(IdNumericUpDown1, "Id no existe");
        }
    }
}
