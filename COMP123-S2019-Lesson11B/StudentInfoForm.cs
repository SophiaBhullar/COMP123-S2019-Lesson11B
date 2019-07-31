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
    public partial class StudentInfoForm : Form
    {
        public StudentInfoForm()
        {
            InitializeComponent();
        }

        private void StudentInfoForm_Activated(object sender, EventArgs e)
        { 
            try
            {
                //open the stream for reading
                using (StreamReader inputStream = new StreamReader(
                File.Open("Student.txt", FileMode.Open)))
                { 

                    //read stuff from the file into the student class
                    Program.student.Id = int.Parse(inputStream.ReadLine());
                    Program.student.StudentId = inputStream.ReadLine();
                    Program.student.FirstName = inputStream.ReadLine();
                    Program.student.LastName = inputStream.ReadLine();

                    //cleanup
                    inputStream.Close();
                    inputStream.Dispose();

                }
            }
             
            catch(IOException exception)
            {
                Debug.WriteLine("ERROR" + exception.Message);
                MessageBox.Show("Error:" + exception.Message, "File I/O Error",MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            //write data from student object to form labels
            IDLabel.Text = Program.student.Id.ToString();
            StudentIdLabel.Text = Program.student.StudentId;
            FirstNameLabel.Text = Program.student.FirstName;
            LastNameLabel.Text = Program.student.LastName;

        }

        private void BackButton_Click(object sender, EventArgs e)
        {
            Program.mainForm.Show();
            this.Hide();
        }

        private void StudentInfoForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

    }
}
