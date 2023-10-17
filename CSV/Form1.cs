using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;
using System.IO;


namespace CSV
{
    public partial class Form1 : Form
    {
        private string csvFilePath; // Ruta del archivo CSV

        public Form1()
        {
            InitializeComponent();
            dataGridView1.AutoGenerateColumns = true;
        }

        private void btnOpenFile_Click(object sender, EventArgs e)
        {
            // Mostrar el cuadro de diálogo de apertura de archivo
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Archivos CSV|*.csv";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                csvFilePath = openFileDialog.FileName;
                LoadAndDisplayCsvData();
                MessageBox.Show("Archivo CSV cargado exitosamente.");
            }
        }

        private void LoadAndDisplayCsvData()
        {
            try
            {
                // Leer todos los datos del archivo CSV y cargarlos en el DataGridView
                using (StreamReader reader = new StreamReader(csvFilePath))
                {
                    string[] columnHeaders = reader.ReadLine()?.Split(',');
                    if (columnHeaders != null)
                    {
                        foreach (var columnHeader in columnHeaders)
                        {
                            dataGridView1.Columns.Add(columnHeader, columnHeader);
                        }
                    }

                    dataGridView1.Rows.Clear();

                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        string[] values = line.Split(',');
                        dataGridView1.Rows.Add(values);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar y mostrar desde CSV: " + ex.Message);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                // Guardar los datos del DataGridView de vuelta al archivo CSV
                using (StreamWriter writer = new StreamWriter(csvFilePath))
                {
                    // Escribir encabezados de columna
                    for (int i = 0; i < dataGridView1.Columns.Count; i++)
                    {
                        writer.Write(dataGridView1.Columns[i].HeaderText);
                        if (i < dataGridView1.Columns.Count - 1)
                            writer.Write(",");
                    }
                    writer.WriteLine();

                    // Escribir datos de las filas
                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        for (int i = 0; i < dataGridView1.Columns.Count; i++)
                        {
                            writer.Write(row.Cells[i].Value);
                            if (i < dataGridView1.Columns.Count - 1)
                                writer.Write(",");
                        }
                        writer.WriteLine();
                    }
                }

                MessageBox.Show("Guardado en CSV");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar en CSV: " + ex.Message);
            }
        }
        // Evento para el botón "Editar"
        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                // Obtener la fila seleccionada
                DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];

                // Mostrar una ventana de edición o un cuadro de diálogo para que el usuario edite los datos
                // Puedes abrir un nuevo formulario para la edición o usar cuadros de texto en el formulario actual
                // Por ejemplo, puedes usar un cuadro de diálogo de entrada para que el usuario ingrese los nuevos valores

                string editedValue = Microsoft.VisualBasic.Interaction.InputBox("Editar valor:", "Editar", selectedRow.Cells[0].Value.ToString());

                // Actualizar el valor editado en la fila seleccionada
                selectedRow.Cells[0].Value = editedValue;
            }
            else
            {
                MessageBox.Show("Seleccione una fila para editar.");
            }
        }

        // Evento para el botón "Borrar"
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                // Obtener la fila seleccionada
                DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];

                // Eliminar la fila seleccionada
                dataGridView1.Rows.Remove(selectedRow);
            }
            else
            {
                MessageBox.Show("Seleccione una fila para borrar.");
            }
        }

    }
}

