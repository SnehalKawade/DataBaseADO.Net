using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DataBaseADO.Net.DisconnectedPgm
{
    public partial class StudentDisconnect : Form
    {
        SqlConnection con;
        SqlDataAdapter da;
        SqlCommandBuilder scb;
        DataSet ds;
        public StudentDisconnect()
        {
            InitializeComponent();
            string constr = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            con = new SqlConnection(constr);
        }

        public DataSet GetStuds()
        {
            da = new SqlDataAdapter("select * from Student", con);
            // apply PK contrainst to the col which is in Dataset table.
            // Id -> Pk in the DB same apply PK to Id col which is in the DataSet
            da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
            // commandbuilder track dataset table & generate sql query that will be pass to the 
            // dataadapter object
            scb = new SqlCommandBuilder(da);
            ds = new DataSet();
            da.Fill(ds, "stud");// emp is a name given to DataTable which is in DataSet
            return ds;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            ds = GetStuds();
            //created a new row to add record. row have same structure as table
            DataRow row = ds.Tables["stud"].NewRow();
            // added data to the row
            row["Name"] = txtName.Text;
            row["Branch"] = txtBranch.Text;
            row["Percentage"] = txtPercentage.Text.ToString();
            // attach row to the emp table
            ds.Tables["stud"].Rows.Add(row);
            // reflect the changes from DataSet to Database
            int res = da.Update(ds.Tables["stud"]);
            if (res == 1)
                MessageBox.Show("Record saved");
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            ds = GetStuds();
            // Find() method only work with PK col in the dataset
            DataRow row = ds.Tables["stud"].Rows.Find(Convert.ToInt32(txtId.Text));
            if (row != null)
            {
                txtName.Text = row["Name"].ToString();
                txtBranch.Text = row["Branch"].ToString();
                txtPercentage.Text = row["Percentage"].ToString();
            }
            else
            {
                MessageBox.Show("Record not found");
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            int per = Convert.ToInt32(txtPercentage.Text);
            if (string.IsNullOrEmpty(txtName.Text) && per > 0)
            {
                MessageBox.Show("Enter name or percentage should be greater than 0");
            }
            else
            {
                ds = GetStuds();
                // Find() method only work with PK col in the dataset
                DataRow row = ds.Tables["stud"].Rows.Find(Convert.ToInt32(txtId.Text));
                if (row != null)
                {
                    row["Name"] = txtName.Text;
                    row["Branch"] = txtBranch.Text;
                    row["Percentage"] = txtPercentage.Text;
                    int res = da.Update(ds.Tables["stud"]);
                    if (res == 1)
                        MessageBox.Show("record updated");
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            ds = GetStuds();
            // Find() method only work with PK col in the dataset
            DataRow row = ds.Tables["stud"].Rows.Find(Convert.ToInt32(txtId.Text));
            if (row != null)
            {
                row.Delete();

                int res = da.Update(ds.Tables["stud"]);
                if (res == 1)
                    MessageBox.Show("record deleted");
            }
            else
            {
                MessageBox.Show("Record not found");
            }
        }

        private void btnShow_Click(object sender, EventArgs e)
        {

        }
    }
}
