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
    public partial class ProductDisconnect : Form
    {
        SqlConnection con;
        SqlDataAdapter da;
        SqlCommandBuilder scb;
        DataSet ds;
        public ProductDisconnect()
        {
            InitializeComponent();
            string constr = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            con = new SqlConnection(constr);
        }
        public DataSet GetProds()
        {
            da = new SqlDataAdapter("select * from Product", con);
            // apply PK contrainst to the col which is in Dataset table.
            // Id -> Pk in the DB same apply PK to Id col which is in the DataSet
            da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
            // commandbuilder track dataset table & generate sql query that will be pass to the 
            // dataadapter object
            scb = new SqlCommandBuilder(da);
            ds = new DataSet();
            da.Fill(ds, "prod");// emp is a name given to DataTable which is in DataSet
            return ds;
        }
        
        private void btnSave_Click(object sender, EventArgs e)
        {
            ds = GetProds();
            //created a new row to add record. row have same structure as table
            DataRow row = ds.Tables["prod"].NewRow();
            // added data to the row
            row["Name"] = txtName.Text;
            row["Price"] = txtPrice.Text.ToString();
            // attach row to the emp table
            ds.Tables["prod"].Rows.Add(row);
            // reflect the changes from DataSet to Database
            int res = da.Update(ds.Tables["prod"]);
            if (res == 1)
                MessageBox.Show("Record saved");
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            int price = Convert.ToInt32(txtPrice.Text);
            if (string.IsNullOrEmpty(txtName.Text) && price > 0)
            {
                MessageBox.Show("Enter name or price should be greater than 0");
            }
            else
            {
                ds = GetProds();
                // Find() method only work with PK col in the dataset
                DataRow row = ds.Tables["prod"].Rows.Find(Convert.ToInt32(txtId.Text));
                if (row != null)
                {
                    row["Name"] = txtName.Text;
                    row["Price"] = txtPrice.Text;
                    int res = da.Update(ds.Tables["prod"]);
                    if (res == 1)
                        MessageBox.Show("record updated");
                }
            }
        }

        private void btnSerach_Click(object sender, EventArgs e)
        {
            ds = GetProds();
            // Find() method only work with PK col in the dataset
            DataRow row = ds.Tables["prod"].Rows.Find(Convert.ToInt32(txtId.Text));
            if (row != null)
            {
                txtName.Text = row["Name"].ToString();
                txtPrice.Text = row["Price"].ToString();
            }
            else
            {
                MessageBox.Show("Record not found");
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            ds = GetProds();
            // Find() method only work with PK col in the dataset
            DataRow row = ds.Tables["prod"].Rows.Find(Convert.ToInt32(txtId.Text));
            if (row != null)
            {
                row.Delete();

                int res = da.Update(ds.Tables["prod"]);
                if (res == 1)
                    MessageBox.Show("record deleted");
            }
            else
            {
                MessageBox.Show("Record not found");
            }
        }

       
    }
}
