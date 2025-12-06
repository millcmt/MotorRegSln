using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MotorRegSln
{
    public partial class MyVehicles : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                LoadVehicles();
        }

        private void LoadVehicles()
        {
            if (Session["UserId"] == null)
            {
                Response.Redirect("~/Login.aspx");
                return;
            }

            string query = @"
                SELECT v.VehicleId, v.PlateNumber, v.ChassisNumber, v.Make, v.Model, v.Year,
                       (SELECT TOP 1 ExpiryDate FROM Registrations r 
                        WHERE r.VehicleId = v.VehicleId 
                        ORDER BY ExpiryDate DESC) AS LastExpiry
                FROM Vehicles v
                WHERE v.OwnerUserId = @uid";

            using (SqlConnection conn = new SqlConnection(
                System.Configuration.ConfigurationManager.ConnectionStrings["MotorRegDB"].ConnectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@uid", Session["UserId"]);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                rptVehicles.DataSource = dt;
                rptVehicles.DataBind();
            }
        }

        protected void rptVehicles_ItemCommand(object source, System.Web.UI.WebControls.RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Renew")
            {
                string vehicleId = e.CommandArgument.ToString();
                Response.Redirect($"RenewRegistration.aspx?vid={vehicleId}");
            }
        }

        protected void btnLinkVehicle_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/LinkVehicle.aspx");
        }
    }
}