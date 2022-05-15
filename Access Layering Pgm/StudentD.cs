using Access_Layering_Pgm.DAL;
using Access_Layering_Pgm.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Access_Layering_Pgm
{
    public partial class StudentD : Form
    {
        StudentDal studentdal=new StudentDal();
        public StudentD()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Student stud=new Student();
            stud.Name = txtName.Text;
            stud.Branch=txtBranch.Text;
            stud.Percentage = Convert.ToInt32(txtPercentage.Text);
            int res = studentdal.SaveStudent(stud);
            if (res == 1)
                MessageBox.Show("Inserted the record");

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            Student stud= studentdal.GetstudsById(Convert.ToInt32(txtId.Text));
            if (stud.Id > 0)
            {
                txtName.Text = stud.Name;
                txtBranch.Text = stud.Branch;
                txtPercentage.Text = stud.Percentage.ToString();
            }
            else
            {
                MessageBox.Show("Record not found");
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            Student stud = new Student();
            stud.Id = Convert.ToInt32(txtId.Text);
            stud.Name = txtName.Text;
            stud.Branch=txtBranch.Text;
            stud.Percentage = Convert.ToInt32(txtPercentage.Text);
            int res = studentdal.UpdateStudent(stud);
            if (res == 1)
                MessageBox.Show("updated the record");
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            int res =studentdal.Delete(Convert.ToInt32(txtId.Text));
            if (res == 1)
                MessageBox.Show("deleted the record");
        }

        private void btnShow_Click(object sender, EventArgs e)
        {
            DataTable table = studentdal.GetAllStudes();
            dataGridView1.DataSource = table;
        }
    }
}
