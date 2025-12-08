using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

namespace MotorRegSln
{
    public partial class RenewRegistration : System.Web.UI.Page
    {
        private string connString = System.Configuration.ConfigurationManager
                                    .ConnectionStrings["MotorRegDB"]
                                    .ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["vid"] == null)
                {
                    Response.Redirect("MyVehicles.aspx");
                    return;
                }

                LoadVehicleInfo();
                CheckInsuranceStatus();
                CheckFitnessStatus();
            }
        }

        private int VehicleId => Convert.ToInt32(Request.QueryString["vid"]);

        // -----------------------------------------
        // Load vehicle info
        // -----------------------------------------
        private void LoadVehicleInfo()
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(@"
                    SELECT PlateNumber, ChassisNumber, Make, Model, Year
                    FROM Vehicles
                    WHERE VehicleId=@id", conn);

                cmd.Parameters.AddWithValue("@id", VehicleId);

                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    lblPlate.Text = dr["PlateNumber"].ToString();
                    lblChassis.Text = dr["ChassisNumber"].ToString();
                    lblMakeModel.Text = dr["Make"] + " " + dr["Model"];
                    lblYear.Text = dr["Year"].ToString();
                }
            }
        }

        // -----------------------------------------
        // Check insurance via web service
        // -----------------------------------------
        private void CheckInsuranceStatus()
        {
            localhost.InsuranceServices insuranceServices = new localhost.InsuranceServices();
            string response = insuranceServices.CheckInsurance(lblPlate.Text);


            string[] parts = response.Split('|');

            lblInsuranceStatus.Text = parts[0];
            lblInsuranceDate.Text = parts.Length > 1 ? parts[1] : "";

            ViewState["InsuranceOK"] = parts[0] == "UpToDate";
        }

        // -----------------------------------------
        // Check fitness via web service
        // -----------------------------------------
        private void CheckFitnessStatus()
        {
            localhost2.FitnessServices fitnessServicesRef = new localhost2.FitnessServices();

            string response = fitnessServicesRef.CheckFitness(lblChassis.Text);
            string[] parts = response.Split('|');

            lblFitnessStatus.Text = parts[0];
            lblFitnessDate.Text = parts.Length > 1 ? parts[1] : "";

            ViewState["FitnessOK"] = parts[0] == "UpToDate";
        }

        // -----------------------------------------
        // Confirm renewal
        // -----------------------------------------
        protected void btnRenew_Click(object sender, EventArgs e)
        {
            bool insuranceOk = (bool)ViewState["InsuranceOK"];
            bool fitnessOk = (bool)ViewState["FitnessOK"];

            if (!insuranceOk || !fitnessOk)
            {
                lblMessage.Text = "Cannot renew: Insurance or Fitness is expired.";
                lblMessage.CssClass = "text-danger";
                return;
            }

            int months = Convert.ToInt32(rblDuration.SelectedValue);
            DateTime today = DateTime.Today;
            DateTime expiry = today.AddMonths(months);

            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(@"
                    INSERT INTO Registrations (VehicleId, StartDate, ExpiryDate, DurationMonths, CreatedByUserId)
                    VALUES (@v, @s, @e, @d, @u)", conn);

                cmd.Parameters.AddWithValue("@v", VehicleId);
                cmd.Parameters.AddWithValue("@s", today);
                cmd.Parameters.AddWithValue("@e", expiry);
                cmd.Parameters.AddWithValue("@d", months);
                cmd.Parameters.AddWithValue("@u", Session["UserId"]);

                cmd.ExecuteNonQuery();
            }

            lblMessage.Text = "Registration successfully renewed!";
            lblMessage.CssClass = "text-success";
        }
    }
}