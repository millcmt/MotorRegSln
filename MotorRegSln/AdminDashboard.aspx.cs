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
    public partial class AdminDashboard : System.Web.UI.Page
    {
        private string connString = System.Configuration.ConfigurationManager
                                    .ConnectionStrings["MotorRegDB"].ConnectionString;

        private int SelectedVehicleId
        {
            get { return ViewState["SelectedVehicleId"] == null ? 0 : (int)ViewState["SelectedVehicleId"]; }
            set { ViewState["SelectedVehicleId"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                LoadVehicles();
        }

        // ------------------------------------
        // LOAD VEHICLES LIST
        // ------------------------------------
        private void LoadVehicles()
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(
                    @"SELECT VehicleId, PlateNumber, ChassisNumber, Make, Model, Year 
                      FROM Vehicles ORDER BY PlateNumber", conn);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                gvVehicles.DataSource = dt;
                gvVehicles.DataBind();
            }
        }

        // ------------------------------------
        // GRIDVIEW EDIT
        // ------------------------------------
        protected void gvVehicles_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvVehicles.EditIndex = e.NewEditIndex;
            LoadVehicles();
        }

        protected void gvVehicles_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvVehicles.EditIndex = -1;
            LoadVehicles();
        }

        protected void gvVehicles_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int id = Convert.ToInt32(gvVehicles.DataKeys[e.RowIndex].Value);

            string plate = ((TextBox)gvVehicles.Rows[e.RowIndex].Cells[0].Controls[0]).Text;
            string chassis = ((TextBox)gvVehicles.Rows[e.RowIndex].Cells[1].Controls[0]).Text;
            string make = ((TextBox)gvVehicles.Rows[e.RowIndex].Cells[2].Controls[0]).Text;
            string model = ((TextBox)gvVehicles.Rows[e.RowIndex].Cells[3].Controls[0]).Text;
            string year = ((TextBox)gvVehicles.Rows[e.RowIndex].Cells[4].Controls[0]).Text;

            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(
                    @"UPDATE Vehicles
                      SET PlateNumber=@p, ChassisNumber=@c, Make=@m, Model=@mo, Year=@y
                      WHERE VehicleId=@id", conn);

                cmd.Parameters.AddWithValue("@p", plate);
                cmd.Parameters.AddWithValue("@c", chassis);
                cmd.Parameters.AddWithValue("@m", make);
                cmd.Parameters.AddWithValue("@mo", model);
                cmd.Parameters.AddWithValue("@y", year);
                cmd.Parameters.AddWithValue("@id", id);

                cmd.ExecuteNonQuery();
            }

            gvVehicles.EditIndex = -1;
            LoadVehicles();
            lblMessage.Text = "Vehicle updated.";
            lblMessage.CssClass = "text-success";
        }

        // ------------------------------------
        // SELECT VEHICLE
        // ------------------------------------
        protected void gvVehicles_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "SelectVehicle")
            {
                SelectedVehicleId = Convert.ToInt32(e.CommandArgument);

                LoadVehicleDetails();
                pnlDetails.Visible = true;
            }
        }

        // ------------------------------------
        // LOAD VEHICLE DETAILS
        // ------------------------------------
        private void LoadVehicleDetails()
        {
            lblVehicleHeader.Text = "Vehicle ID: " + SelectedVehicleId;

            // Insurance
            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();

                SqlCommand cmdI = new SqlCommand(
                    "SELECT ValidUntil FROM InsuranceRecords WHERE VehicleId=@id", conn);
                cmdI.Parameters.AddWithValue("@id", SelectedVehicleId);

                object resultI = cmdI.ExecuteScalar();

                if (resultI == null)
                {
                    lblInsStatus.Text = "No Record";
                    lblInsDate.Text = "-";
                }
                else
                {
                    DateTime d = Convert.ToDateTime(resultI);
                    lblInsDate.Text = d.ToString("yyyy-MM-dd");
                    lblInsStatus.Text = d >= DateTime.Today ? "Up to Date" : "Expired";
                }

                // Fitness
                SqlCommand cmdF = new SqlCommand(
                    "SELECT ValidUntil FROM FitnessRecords WHERE VehicleId=@id", conn);
                cmdF.Parameters.AddWithValue("@id", SelectedVehicleId);

                object resultF = cmdF.ExecuteScalar();

                if (resultF == null)
                {
                    lblFitStatus.Text = "No Record";
                    lblFitDate.Text = "-";
                }
                else
                {
                    DateTime d = Convert.ToDateTime(resultF);
                    lblFitDate.Text = d.ToString("yyyy-MM-dd");
                    lblFitStatus.Text = d >= DateTime.Today ? "Up to Date" : "Expired";
                }

                // History
                SqlCommand cmdH = new SqlCommand(
                    @"SELECT StartDate, ExpiryDate, DurationMonths
                      FROM Registrations
                      WHERE VehicleId=@id
                      ORDER BY ExpiryDate DESC", conn);

                cmdH.Parameters.AddWithValue("@id", SelectedVehicleId);

                SqlDataAdapter da = new SqlDataAdapter(cmdH);
                DataTable dt = new DataTable();
                da.Fill(dt);

                gvHistory.DataSource = dt;
                gvHistory.DataBind();
            }
        }

        // ------------------------------------
        // CALENDAR SYNC
        // ------------------------------------
        protected void calInsurance_SelectionChanged(object sender, EventArgs e)
        {
            txtNewInsDate.Text = calInsurance.SelectedDate.ToString("yyyy-MM-dd");
        }

        protected void calFitness_SelectionChanged(object sender, EventArgs e)
        {
            txtNewFitDate.Text = calFitness.SelectedDate.ToString("yyyy-MM-dd");
        }

        // ------------------------------------
        // UPDATE INSURANCE
        // ------------------------------------
        protected void btnUpdateInsurance_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(@"
                    IF EXISTS (SELECT 1 FROM InsuranceRecords WHERE VehicleId=@id)
                        UPDATE InsuranceRecords SET ValidUntil=@d WHERE VehicleId=@id
                    ELSE
                        INSERT INTO InsuranceRecords (VehicleId, ValidUntil) VALUES (@id, @d)
                ", conn);

                cmd.Parameters.AddWithValue("@id", SelectedVehicleId);
                cmd.Parameters.AddWithValue("@d", txtNewInsDate.Text);

                cmd.ExecuteNonQuery();
            }

            lblMessage.Text = "Insurance updated.";
            lblMessage.CssClass = "text-success";

            LoadVehicleDetails();
        }

        // ------------------------------------
        // UPDATE FITNESS
        // ------------------------------------
        protected void btnUpdateFitness_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(@"
                    IF EXISTS (SELECT 1 FROM FitnessRecords WHERE VehicleId=@id)
                        UPDATE FitnessRecords SET ValidUntil=@d WHERE VehicleId=@id
                    ELSE
                        INSERT INTO FitnessRecords (VehicleId, ValidUntil) VALUES (@id, @d)
                ", conn);

                cmd.Parameters.AddWithValue("@id", SelectedVehicleId);
                cmd.Parameters.AddWithValue("@d", txtNewFitDate.Text);

                cmd.ExecuteNonQuery();
            }

            lblMessage.Text = "Fitness updated.";
            lblMessage.CssClass = "text-success";

            LoadVehicleDetails();
        }

        // ------------------------------------
        // ADD REGISTRATION (ADMIN OVERRIDE)
        // ------------------------------------
        protected void btnAddReg_Click(object sender, EventArgs e)
        {
            int months = Convert.ToInt32(ddlMonths.SelectedValue);

            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(
                    @"INSERT INTO Registrations (VehicleId, StartDate, ExpiryDate, DurationMonths, CreatedByUserId)
                      VALUES (@v, @s, @e, @m, 1)", conn);

                DateTime start = DateTime.Today;
                DateTime expiry = start.AddMonths(months);

                cmd.Parameters.AddWithValue("@v", SelectedVehicleId);
                cmd.Parameters.AddWithValue("@s", start);
                cmd.Parameters.AddWithValue("@e", expiry);
                cmd.Parameters.AddWithValue("@m", months);

                cmd.ExecuteNonQuery();
            }

            lblMessage.Text = "Registration added.";
            lblMessage.CssClass = "text-success";

            LoadVehicleDetails();
        }
        protected void btnAddVehicle_Click(object sender, EventArgs e)
        {
            pnlAddVehicle.Visible = true;
        }

        // ------------------------------------
        // SHOW ADD VEHICLE PANEL
        // ------------------------------------
        protected void btnShowAdd_Click(object sender, EventArgs e)
        {
            pnlAddVehicle.Visible = !pnlAddVehicle.Visible;
        }

        // ------------------------------------
        // SAVE NEW VEHICLE
        // ------------------------------------
        protected void btnSaveVehicle_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(
                    @"INSERT INTO Vehicles (PlateNumber, ChassisNumber, Make, Model, Year)
                      VALUES (@p, @c, @m, @mo, @y)", conn);

                cmd.Parameters.AddWithValue("@p", txtPlate.Text);
                cmd.Parameters.AddWithValue("@c", txtChassis.Text);
                cmd.Parameters.AddWithValue("@m", txtMake.Text);
                cmd.Parameters.AddWithValue("@mo", txtModel.Text);
                cmd.Parameters.AddWithValue("@y", txtYear.Text);

                cmd.ExecuteNonQuery();
            }

            lblMessage.Text = "Vehicle added.";
            lblMessage.CssClass = "text-success";

            pnlAddVehicle.Visible = false;
            LoadVehicles();
        }
    }
}