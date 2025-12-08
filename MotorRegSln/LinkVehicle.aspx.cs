using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MotorRegSln
{
    public partial class LinkVehicle : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string id = txtIdentifier.Text.Trim();

            string findQuery = @"
                SELECT VehicleId FROM Vehicles 
                WHERE PlateNumber=@id OR ChassisNumber=@id";

            using (SqlConnection conn = new SqlConnection(
                System.Configuration.ConfigurationManager.ConnectionStrings["MotorRegDB"].ConnectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(findQuery, conn);
                cmd.Parameters.AddWithValue("@id", id);

                object result = cmd.ExecuteScalar();

                if (result == null)
                {
                    lblMsg.Text = "Vehicle not found.";
                    return;
                }

                int vehicleId = Convert.ToInt32(result);

                string updateQuery = "UPDATE Vehicles SET OwnerUserId=@uid WHERE VehicleId=@vid";

                SqlCommand cmd2 = new SqlCommand(updateQuery, conn);
                cmd2.Parameters.AddWithValue("@uid", Session["UserId"]);
                cmd2.Parameters.AddWithValue("@vid", vehicleId);

                cmd2.ExecuteNonQuery();

                Response.Redirect("~/MyVehicles.aspx");
            }
        }
    }
}