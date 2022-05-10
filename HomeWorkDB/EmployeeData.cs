using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace HomeWorkDB
{
    public partial class EmployeeData : Form
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader dr;
        public EmployeeData()
        {
            InitializeComponent();

            con = new SqlConnection(@"Server=DESKTOP-D3A70GD\SQLEXPRESS;database=ThinkQ;Integrated Security=True");
        }
        public void ClearAll()
        {
           txtEmpID.Clear();
           txtEmpName.Clear();
           txtDesignation.Clear();
           txtSalary.Clear();      
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            try
            {
                
                string qry = "insert into Employee values(@eid,@ename,@designation,@salary)";
                cmd = new SqlCommand(qry, con);
                cmd.Parameters.AddWithValue("@eid", Convert.ToInt32(txtEmpID.Text));
                cmd.Parameters.AddWithValue("@ename", txtEmpName.Text);
                cmd.Parameters.AddWithValue("@designation", txtDesignation.Text);
                cmd.Parameters.AddWithValue("@salary", Convert.ToInt32(txtSalary.Text));

                con.Open();

                int res = cmd.ExecuteNonQuery();
                if (res == 1)
                {
                    MessageBox.Show("Record Inserted");
                    txtEmpID.Enabled = true;
                    ClearAll();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                string qry = "update Employee set EmpName=@ename, Designation=@designation, Salary=@salary where EmpId=@eid";
                cmd = new SqlCommand(qry, con);
                cmd.Parameters.AddWithValue("@eid", Convert.ToInt32(txtEmpID.Text));
                cmd.Parameters.AddWithValue("@ename", txtEmpName.Text);
                cmd.Parameters.AddWithValue("@designation", txtDesignation.Text);
                cmd.Parameters.AddWithValue("@salary", Convert.ToInt32(txtSalary.Text));

                con.Open();

                int res = cmd.ExecuteNonQuery();
                if (res == 1)
                {
                    MessageBox.Show("Record Updated");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
            }
        }

        private void btnRead_Click(object sender, EventArgs e)
        {
            try
            {
                string qry = "select * from Employee where EmpId=@eid";
                cmd = new SqlCommand(qry, con);
                cmd.Parameters.AddWithValue("@eid", Convert.ToInt32(txtEmpID.Text));
                con.Open();
                dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        txtEmpName.Text = dr["EmpName"].ToString();
                        txtDesignation.Text = dr["Designation"].ToString();
                        txtSalary.Text = dr["Salary"].ToString();
                    }
                }
                else
                {
                    MessageBox.Show("Record Not Found");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                string qry = "delete from Employee where EmpId=@eid";
                cmd = new SqlCommand(qry, con);
                cmd.Parameters.AddWithValue("@eid", Convert.ToInt32(txtEmpID.Text));

                con.Open();

                int res = cmd.ExecuteNonQuery();
                if (res == 1)
                {
                    MessageBox.Show("Record Deleted");
                    ClearAll();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
            }
        }

        private void btnShow_Click(object sender, EventArgs e)
        {
            try
            {
                string qry = "select * from Employee";
                cmd = new SqlCommand(qry, con);
                con.Open();
                dr = cmd.ExecuteReader();
                if (dr.HasRows)    
                {
                    DataTable table = new DataTable();
                    table.Load(dr);
                    dataGridView1.DataSource = table;
                }
                else
                {
                    MessageBox.Show("Record Not Found");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
            }
        }

        private void dataGridView1_Click(object sender, EventArgs e)
        {
            txtEmpID.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            txtEmpName.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            txtDesignation.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            txtSalary.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
        }

        private void EmployeeData_Load(object sender, EventArgs e)
        {

        }
    }
}
