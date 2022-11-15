using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace StudentsDiary
{
    public partial class Main : Form
    {

        private FileHelper<List<Student>> _fileHelper =
            new FileHelper<List<Student>>(Program.FilePath);

        public Main()
        {
            InitializeComponent();
            SetComboBoxSearchIdGroup();
            RefreshDiary();
            SetColumnsHeader();
        }

        private void RefreshDiary()
        {
            var students = _fileHelper.DeserializeFromFile();

            if(cbxSearchIdGroup.SelectedIndex == 0)
                dgvDiary.DataSource =
                students.OrderBy(x => x.Id).ToList();
            else
                dgvDiary.DataSource =
               students.Where(x => x.IdGroup == cbxSearchIdGroup.Text).OrderBy(x => x.Id).ToList();


        }

        private void SetComboBoxSearchIdGroup()
        {
            cbxSearchIdGroup.Items.Add("Wszystkie");

            foreach (var item in Program.ComboBoxItems)
            {
                cbxSearchIdGroup.Items.Add(item.ToString());
            }
            cbxSearchIdGroup.SelectedIndex = 0;
        }

        private void SetColumnsHeader()
        {
            dgvDiary.Columns[0].HeaderText = "Numer";
            dgvDiary.Columns[1].HeaderText = "Imię";
            dgvDiary.Columns[2].HeaderText = "Nazwisko";
            dgvDiary.Columns[3].HeaderText = "Matematyka";
            dgvDiary.Columns[4].HeaderText = "Technologia";
            dgvDiary.Columns[5].HeaderText = "Fizyka";
            dgvDiary.Columns[6].HeaderText = "Polski";
            dgvDiary.Columns[7].HeaderText = "Angielski";
            dgvDiary.Columns[8].HeaderText = "Uwagi";
            dgvDiary.Columns[9].HeaderText = "Zajęcia dodatkowe";
            dgvDiary.Columns[10].HeaderText = "Grupa";
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            var addEditStudent = new AddEditStudent();
            addEditStudent.FormClosing += AddEditStudent_FormClosing;
            addEditStudent.ShowDialog();

        }

        private void AddEditStudent_FormClosing(object sender, FormClosingEventArgs e)
        {
            RefreshDiary();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgvDiary.SelectedRows.Count == 0)
            {
                MessageBox.Show("Proszę zaznaczyć studenta do edycji!");
                return;
            }

            var addEditStudent = new AddEditStudent(
                Convert.ToInt32(dgvDiary.SelectedRows[0].Cells[0].Value));
            addEditStudent.FormClosing += AddEditStudent_FormClosing;
            addEditStudent.ShowDialog();

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            //sprawdzenie czy został zaznaczony uczeń do usuniecia
            if (dgvDiary.SelectedRows.Count == 0)
            {
                MessageBox.Show("Proszę, zaznaczyć studenta do usunięcia!");
                return;
            }

            var selectedStudent = dgvDiary.SelectedRows[0];

            //potwierdzenie usunięcia
            var confirmDelete = MessageBox.Show(
                $"Czy napewno chcesz usunąć {(selectedStudent.Cells[1].Value + " " + selectedStudent.Cells[2].Value).Trim()}", "Usuwanie studenta", MessageBoxButtons.YesNo);

            if (confirmDelete == DialogResult.Yes)
            {
                var students = _fileHelper.DeserializeFromFile();
                students.RemoveAll(x => x.Id == Convert.ToInt32(selectedStudent.Cells[0].Value));
                _fileHelper.SerializeToFile(students);
                dgvDiary.DataSource = students;
            }

        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            RefreshDiary();
        }
    }
}
