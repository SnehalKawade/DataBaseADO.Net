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

namespace DataBaseADO.Net
{
    public partial class ProductDB : Form
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader dr;
        public ProductDB()
        {
            InitializeComponent();
            con = new SqlConnection(@"Server=DESKTOP-D3A70GD\SQLEXPRESS;database=ThinkQ;Integrated Security=True");
        }
        public void ClearAll()
        {
            txtProductId.Clear();
            txtProductName.Clear(); 
            txtPrice.Clear();   
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                //while writing query follow the col sequence
                string qry = "insert into product values(@id,@name,@price)";
                //this configuration is to assign query & connection details to command
                //so tat qry will be executed on the given connection
                cmd=new SqlCommand(qry,con);
                //assign values to the parameter
                //no need to follow the sequence
                cmd.Parameters.AddWithValue("@id", Convert.ToInt32(txtProductId.Text));
                cmd.Parameters.AddWithValue("@name",txtProductName.Text);
                cmd.Parameters.AddWithValue("@price", Convert.ToInt32(txtPrice.Text));
                //open DB connection
                con.Open();
                //fire the query 
                int res=cmd.ExecuteNonQuery();  
                if(res == 1)
                {
                    MessageBox.Show("Record Inserted");
                    txtProductId.Enabled = true;
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

        private void btnSerach_Click(object sender, EventArgs e)
        {
            try
            {
                string qry = "select * from Product where Id=@id";
                cmd = new SqlCommand(qry, con);
                cmd.Parameters.AddWithValue("@id", Convert.ToInt32(txtProductId.Text));
                con.Open();
                dr=cmd.ExecuteReader();
                if(dr.HasRows)
                {
                    while(dr.Read())
                    {
                        txtProductName.Text = dr["Name"].ToString();
                        txtPrice.Text = dr["Price"].ToString();
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
                string qry = "update Product set Name=@name,Price=@price where Id=@id";
                cmd = new SqlCommand(qry, con);
                cmd.Parameters.AddWithValue("@id", Convert.ToInt32(txtProductId.Text));
                cmd.Parameters.AddWithValue("@name", txtProductName.Text);
                cmd.Parameters.AddWithValue("@price", Convert.ToInt32(txtPrice.Text));

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
                string qry = "delete from Product where Id=@id";
                cmd = new SqlCommand(qry, con);
                cmd.Parameters.AddWithValue("@id", Convert.ToInt32(txtProductId.Text));
      
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

        private void btnAddNew_Click(object sender, EventArgs e)
        {
            try
            {
                string qry = "select max(Id) from Product";
                cmd = new SqlCommand(qry, con);
                con.Open();

                object obj = cmd.ExecuteScalar();
                if(obj==DBNull.Value)
                {
                    txtProductId.Text = "1";
                }
                else
                {
                    int id = Convert.ToInt32(obj);
                    id++;
                    txtProductId.Text = id.ToString();
                }
                txtProductId.Enabled = false;
                txtProductName.Clear();
                txtPrice.Clear();
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

        private void btnShowAllProducts_Click(object sender, EventArgs e)
        {
            try
            {
                string qry = "select * from Product";
                cmd = new SqlCommand(qry, con);
                con.Open();
                dr = cmd.ExecuteReader();
                if (dr.HasRows)    //existance of record in dr object
                {
                    DataTable table = new DataTable();
                    table.Load(dr);
                    dataGridView1.DataSource= table;
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
           txtProductId.Text=dataGridView1.CurrentRow.Cells[0].Value.ToString();
           txtProductName.Text=dataGridView1.CurrentRow.Cells[1].Value.ToString();
           txtPrice.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();

        }
    }
}
