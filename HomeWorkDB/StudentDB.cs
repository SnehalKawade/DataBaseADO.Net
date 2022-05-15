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
    public partial class StudentDB : Form
    {

        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader dr;
        public StudentDB()
        {
            InitializeComponent();
            con = new SqlConnection(@"Server=DESKTOP-D3A70GD\SQLEXPRESS;database=ThinkQ;Integrated Security=True");
        }

        public void ClearAll()
        {
            txtSID.Clear();
            txtSName.Clear();
            txtBranch.Clear();
            txtPercentage.Clear();
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            try
            {

                string qry = "insert into Student values(@id,@sname,@branch,@percentage)";
                cmd = new SqlCommand(qry, con);
                cmd.Parameters.AddWithValue("@id", Convert.ToInt32(txtSID.Text));
                cmd.Parameters.AddWithValue("@sname", txtSName.Text);
                cmd.Parameters.AddWithValue("@branch", txtBranch.Text);
                cmd.Parameters.AddWithValue("@percentage", Convert.ToInt32(txtPercentage.Text));

                con.Open();

                int res = cmd.ExecuteNonQuery();
                if (res == 1)
                {
                    MessageBox.Show("Record Inserted");
                    txtSID.Enabled = true;
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

        private void btnRead_Click(object sender, EventArgs e)
        {
            try
            {
                string qry = "select * from Student where Id=@id";
                cmd = new SqlCommand(qry, con);
                cmd.Parameters.AddWithValue("@id", Convert.ToInt32(txtSID.Text));
                con.Open();
                dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        txtSName.Text = dr["Name"].ToString();
                        txtBranch.Text = dr["Branch"].ToString();
                        txtPercentage.Text = dr["Percentage"].ToString();
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

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                string qry = "update Student set Name=@sname, Branch=@branch, Percentage=@percentage where Id=@id";
                cmd = new SqlCommand(qry, con);
                cmd.Parameters.AddWithValue("@id", Convert.ToInt32(txtSID.Text));
                cmd.Parameters.AddWithValue("@sname", txtSName.Text);
                cmd.Parameters.AddWithValue("@branch", txtBranch.Text);
                cmd.Parameters.AddWithValue("@percentage", Convert.ToInt32(txtPercentage.Text));

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

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                string qry = "delete from Student where Id=@id";
                cmd = new SqlCommand(qry, con);
                cmd.Parameters.AddWithValue("@id", Convert.ToInt32(txtSID.Text));

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
                string qry = "select * from Student";
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
            txtSID.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            txtSName.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            txtBranch.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            txtPercentage.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
        }
       
    }
}
