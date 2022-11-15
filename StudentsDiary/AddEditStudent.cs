using StudentsDiary.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace StudentsDiary
{
    public partial class AddEditStudent : Form
    {
        //private string _filePath = Path.Combine(Environment.CurrentDirectory, "students.txt");


        FileHelper<List<Student>> _fileHelper =
            new FileHelper<List<Student>>(Program.FilePath);
        private int _studentId;

        public AddEditStudent(int id = 0)
        {
            InitializeComponent();
            cbxIdGroup.DataSource = Program.ComboBoxItems;

            if (id != 0)
            {
                Text = "Edycja studenta";

                var students = _fileHelper.DeserializeFromFile();
                _studentId = id;

                var student = students.FirstOrDefault(x => x.Id == id);

                if (student == null)
                    throw new Exception("Brak ucznia o podanym id!");

                tbId.Text = student.Id.ToString();
                tbFirstName.Text = student.FirstName;
                tbLastName.Text = student.LastName;
                tbMath.Text = student.Math;
                tbPhysics.Text = student.Physics;
                tbTechnology.Text = student.Technology;
                tbPolishLang.Text = student.PolishLang;
                tbForeignLang.Text = student.ForeignLang;
                rtbComments.Text = student.Comments;
                chbIsExtraLessons.Checked = student.IsExtraLessons;
                cbxIdGroup.SelectedIndex = cbxIdGroup.FindString(student.IdGroup);
            }

            //zaznaczenie kursorem danego TextBox
            tbFirstName.Select();

        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            var students = _fileHelper.DeserializeFromFile();

            if (_studentId != 0)
            {
                students.RemoveAll(x => x.Id == _studentId);
            }
            else
            {
                //szukamy największego id
                var studentWithHighestId = students.OrderByDescending(x => x.Id).FirstOrDefault();

                _studentId = studentWithHighestId == null ? 1 : studentWithHighestId.Id + 1;
            }

            var student = new Student();
            student.Id = _studentId;
            student.FirstName = tbFirstName.Text;
            student.LastName = tbLastName.Text;
            student.Math = tbMath.Text;
            student.Physics = tbPhysics.Text;
            student.PolishLang = tbPolishLang.Text;
            student.Technology = tbTechnology.Text;
            student.ForeignLang = tbForeignLang.Text;
            student.Comments = rtbComments.Text;
            student.IsExtraLessons = chbIsExtraLessons.Checked;
            student.IdGroup = (string)cbxIdGroup.SelectedItem;

            //dodanie studenta do listy
            students.Add(student);

            //dodanie listy do pliku
            _fileHelper.SerializeToFile(students);

            //await LongProcesAsync();

            //zamknięcie formatki na której dodawany był student
            Close();
        }

        private async Task LongProcesAsync()
        {
            await Task.Run(() =>
            {
                Thread.Sleep(3000);
            });
            //3000 ms - 3 s
            //Thread.Sleep(3000);
        }

        private void btmCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
