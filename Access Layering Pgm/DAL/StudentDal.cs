using Access_Layering_Pgm.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Access_Layering_Pgm.DAL
{
    public class StudentDal
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader dr;

        public StudentDal()
        {
            string constr = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            con = new SqlConnection(constr);
        }

        public DataTable GetAllStudes()
        {
            DataTable table = new DataTable();
            string qry = "select * from Student";
            cmd = new SqlCommand(qry, con);
            con.Open();
            dr = cmd.ExecuteReader();
            table.Load(dr);
            con.Close();
            return table;
        }
        public Student GetstudsById(int id)
        {
            Student stud = new Student();
            string qry = "select * from Student where Id=@id";
            cmd = new SqlCommand(qry, con);
            cmd.Parameters.AddWithValue("@id", id);
            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    stud.Id = Convert.ToInt32(dr["Id"]);
                    stud.Name = dr["Name"].ToString();
                    stud.Branch = dr["Branch"].ToString();
                    stud.Percentage = Convert.ToInt32(dr["Percentage"]);
                }
            }
            con.Close();
            return stud;
        }
        public int SaveStudent(Student stud)
        {

            string qry = "insert into Student values(@name,@branch,@percentage)";
            cmd = new SqlCommand(qry, con);
            //cmd.Parameters.AddWithValue("@id", stud.Id);
            cmd.Parameters.AddWithValue("@name", stud.Name);
            cmd.Parameters.AddWithValue("@branch",stud.Branch);
            cmd.Parameters.AddWithValue("@percentage", stud.Percentage);
            con.Open();
            int res = cmd.ExecuteNonQuery();
            con.Close();
            return res;
        }
        public int UpdateStudent(Student stud)
        {
            string qry = "update Student set Name=@name,Branch=@branch,Percentage=@percentage where Id=@id";
            cmd = new SqlCommand(qry, con);
            cmd.Parameters.AddWithValue("@id", stud.Id);
            cmd.Parameters.AddWithValue("@name",stud.Name);
            cmd.Parameters.AddWithValue("@branch", stud.Branch);
            cmd.Parameters.AddWithValue("@percentage", stud.Percentage);
            con.Open();
            int res = cmd.ExecuteNonQuery();
            con.Close();
            return res;
        }
        public int Delete(int id)
        {
            string qry = "delete from Student where Id=@id";
            cmd = new SqlCommand(qry, con);
            cmd.Parameters.AddWithValue("@id", id);
            con.Open();
            int res = cmd.ExecuteNonQuery();
            con.Close();
            return res;
        }

    }
}
