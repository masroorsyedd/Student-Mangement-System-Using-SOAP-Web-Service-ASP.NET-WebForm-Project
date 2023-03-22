using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data.SqlClient;

namespace StudentCRUD_WebFormUsingWebService
{
    /// <summary>
    /// Summary description for Student_WebService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class Student_WebService : System.Web.Services.WebService
    {
        public string cs = ConfigurationManager.ConnectionStrings["SqlConnection"].ConnectionString;

        [WebMethod]
        public Student StudentGetById(int id)
        {
            using(SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("Select Name,Email,Gender from students where studentId = @Id", con);
                cmd.Parameters.AddWithValue("@Id", id);

                Student student = new Student();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {                    
                    student.Name = reader["Name"].ToString();
                    student.Gender = reader["Gender"].ToString();
                    student.Email = reader["Email"].ToString();
                }
                con.Close();
                return student;

            }
        }

        [WebMethod]
        public List<Student> GetAllStudents()
        {
            using(SqlConnection conn = new SqlConnection(cs))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("Select Name,Email,Gender from Students", conn);

                List<Student> student = new List<Student>();
                SqlDataReader reader = cmd.ExecuteReader();
                while(reader.Read())
                {
                    student.Add(new Student
                    {
                        Name = reader["Name"].ToString(),
                        Gender = reader["Gender"].ToString(),
                        Email = reader["Email"].ToString(),
                    });
                    //student.Name =reader["Name"].ToString();
                    //student.Email = reader["Email"].ToString();
                    //student.Gender = reader["Gender"].ToString();
                }
                return student;

            }
        }

        [WebMethod]
        public string Insert(string name, string gender, string email)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("Insert into students (Name, Email, Gender) Values(@name,@email,@gender)", con);
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@email", email);
                cmd.Parameters.AddWithValue("@gender", gender);

                int id = cmd.ExecuteNonQuery();
                con.Close();
                if (id > 0)
                {
                    return "Record Inserted Successfully";
                }
                else
                {
                    return "Record Not Inserted";
                }
            }

        }

        [WebMethod]
        public string Delete(int id)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("Delete From students Where StudentId = @Id", con);
                cmd.Parameters.AddWithValue("@Id", id);

                int count = cmd.ExecuteNonQuery();
                con.Close();

                if (count > 0)
                {
                    return "Record Deleted Successfully";
                }
                else
                {
                    return "Record Not Deleted";
                }
            }
        }

        [WebMethod]
        public string Update(int id, string name, string email, string gender)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("Update students set Name = @name, Gender = @gender, Email = @email Where StudentId = @Id", con);
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@email", email);
                cmd.Parameters.AddWithValue("@gender", gender);

                Student std = new Student();
                std.Name = name;
                std.Gender = gender;
                std.Email = email;

                int count = cmd.ExecuteNonQuery();
                con.Close();

                if (count > 0)
                {
                    return "Record Updated Successfully";
                }
                else
                {
                    return "Record Not Updated";
                }
            }
        }
    }
}

