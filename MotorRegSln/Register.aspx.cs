using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MotorRegSln
{
    public partial class Register : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void btnRegister_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MotorRegDB"].ConnectionString))
            {
                conn.Open();

                string query = "INSERT INTO Users (FullName, Username, Password, Role) VALUES (@name, @user, @pass, 'User')";
                SqlCommand cmd = new SqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@name", txtFullName.Text.Trim());
                cmd.Parameters.AddWithValue("@user", txtUsername.Text.Trim());
                cmd.Parameters.AddWithValue("@pass", txtPassword.Text.Trim());

                try
                {
                    cmd.ExecuteNonQuery();

                    // auto login
                    Session["UserId"] = GetUserId(conn, txtUsername.Text.Trim());
                    Session["Role"] = "User";

                    Response.Redirect("~/MyVehicles.aspx");
                }
                catch
                {
                    lblMessage.Text = "Username already exists.";
                }
            }
        }

        private int GetUserId(SqlConnection conn, string username)
        {
            SqlCommand cmd = new SqlCommand("SELECT UserId FROM Users WHERE Username=@u", conn);
            cmd.Parameters.AddWithValue("@u", username);
            return Convert.ToInt32(cmd.ExecuteScalar());
        }
    }
}