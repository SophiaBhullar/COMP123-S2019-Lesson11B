using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace COMP123_S2019_Lesson11B
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }


        /// <summary>
        /// This is the Event Handler for the MainForm's FormClosing Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
        

        /// <summary>
        /// This is the Event Handler for the Exit Menu Item's Click Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void eXITToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void aBOUTToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Program.aboutBox.ShowDialog();
        }

        private void helpToolStripButton_Click(object sender, EventArgs e)
        {
            Program.aboutBox.ShowDialog();
        }

        private void ExitToolStripButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'sectionBDatabaseDataSet.StudentTable' table. You can move, or remove it, as needed.
            this.studentTableTableAdapter.Fill(this.sectionBDatabaseDataSet.StudentTable);

        }

        private void StudentDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void sAVEToolStripMenuItem_Click(object sender, EventArgs e)
        {

            //configure the file dialog
            StudentSaveFileDialog.FileName = "Student.txt";
            StudentSaveFileDialog.InitialDirectory = Directory.GetCurrentDirectory();
            StudentSaveFileDialog.Filter = "Text Files (*.txt)|(*.txt| All Files (*.*)|*.*";

            //open the file dialog
            var result = StudentSaveFileDialog.ShowDialog();
            if(result!= DialogResult.Cancel)
            {
                //open file to write
                using (StreamWriter outputStream = new StreamWriter(
                    File.Open(StudentSaveFileDialog.FileName, FileMode.Create)))
                {
                    //write stuff to the file
                    outputStream.WriteLine(Program.student.Id);
                    outputStream.WriteLine(Program.student.StudentId);
                    outputStream.WriteLine(Program.student.FirstName);
                    outputStream.WriteLine(Program.student.LastName);

                    //close the file
                    outputStream.Close();

                    //dispose of the memory
                    outputStream.Dispose();

                }

                //give feedback to the user that the file has been saved
                //this is a "modal" form
                MessageBox.Show("File Saved", "Saving...", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }

        }

        private void StudentDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            //local scope aliases
            var rowIndex = StudentDataGridView.CurrentCell.RowIndex;
            var rows = StudentDataGridView.Rows;
            var cells = rows[rowIndex].Cells;
            var columnCount = StudentDataGridView.ColumnCount;


            StudentDataGridView.Rows[rowIndex].Selected = true;

            string outputString = string.Empty;
            for (int index = 0; index < columnCount; index++)
            {
                outputString += cells[index].Value.ToString() + " ";
            }

            SelectionLabel.Text = outputString;

            Program.student.Id = int.Parse(cells[(int)StudentField.ID].Value.ToString());
            Program.student.StudentId = cells[(int)StudentField.STUDENT_ID].Value.ToString();
            Program.student.FirstName = cells[(int)StudentField.FIRST_NAME].Value.ToString();
            Program.student.LastName = cells[(int)StudentField.LAST_NAME].Value.ToString();

        }

        private void NextButton_Click(object sender, EventArgs e)
        {
            Program.studentInfoForm.Show();
            this.Hide();
        }

        private void ShowDataButton_Click(object sender, EventArgs e)
        {
            var StudentList = from student in this.sectionBDatabaseDataSet.StudentTable
                              select student;

            foreach (var student in StudentList.ToList())
            {
                Debug.WriteLine("Student ID:" + student.StudentID + "Last Name:" + student.LastName);
            }
        }
    }
}
