using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Access_Layering_Pgm.DAL;
using Access_Layering_Pgm.Model;

namespace Access_Layering_Pgm
{
    public partial class ProductD : Form
    {
        ProductDal productdal=new ProductDal();
        public ProductD()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Product prod=new Product();
            prod.Name = txtName.Text;
            prod.Price = Convert.ToInt32(txtPrice.Text);
            int res = productdal.SaveProduct(prod);
            if (res == 1)
                MessageBox.Show("Inserted the record");
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            Product prod = new Product();
            prod.Id = Convert.ToInt32(txtId.Text);
            prod.Name = txtName.Text;
            prod.Price = Convert.ToInt32(txtPrice.Text);
            int res = productdal.UpdateProduct(prod);
            if (res == 1)
                MessageBox.Show("updated the record");

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            int res = productdal.Delete(Convert.ToInt32(txtId.Text));
            if (res == 1)
                MessageBox.Show("deleted the record");
        }

        private void btnSerach_Click(object sender, EventArgs e)
        {
            Product prod= productdal.GetProdById(Convert.ToInt32(txtId.Text));
            if (prod.Id > 0)
            {
                txtName.Text = prod.Name;
                txtPrice.Text = prod.Price.ToString();
            }
            else
            {
                MessageBox.Show("Record not found");
            }
        }

        private void btnShowAll_Click(object sender, EventArgs e)
        {
            DataTable table = productdal.GetAllProds();
            dataGridView1.DataSource = table;
        }
    }
}
