using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Xml;

namespace MotorRegSln
{
    /// <summary>
    /// Summary description for InsuranceServices
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class InsuranceServices : System.Web.Services.WebService
    {

        private string connString = System.Configuration.ConfigurationManager
                                     .ConnectionStrings["MotorRegDB"]
                                     .ConnectionString;

        /// <summary>
        /// Checks insurance by plate OR chassis.
        /// Returns:
        ///   "UpToDate|2025-12-31"
        ///   "Expired|2023-01-10"
        ///   "NotFound|Vehicle not found"
        /// </summary>
        [WebMethod]
        public string CheckInsurance(string plateOrChassis)
        {
            try
            {
                int vehicleId = GetVehicleId(plateOrChassis);

                if (vehicleId == 0)
                    return "NotFound|Vehicle not found";

                DateTime? validUntil = GetInsuranceExpiry(vehicleId);

                if (validUntil == null)
                    return "NotFound|No insurance record found";

                if (validUntil.Value >= DateTime.Today)
                    return "UpToDate|" + validUntil.Value.ToString("yyyy-MM-dd");
                else
                    return "Expired|" + validUntil.Value.ToString("yyyy-MM-dd");
            }
            catch (Exception ex)
            {
                return "Error|" + ex.Message;
            }
        }

        // ------------------------------
        // Helper methods
        // ------------------------------

        private int GetVehicleId(string param)
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(
                    @"SELECT VehicleId
                      FROM Vehicles 
                      WHERE PlateNumber=@p OR ChassisNumber=@p", conn);

                cmd.Parameters.AddWithValue("@p", param);

                object result = cmd.ExecuteScalar();
                return result == null ? 0 : Convert.ToInt32(result);
            }
        }

        private DateTime? GetInsuranceExpiry(int vehicleId)
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(
                    "SELECT ValidUntil FROM InsuranceRecords WHERE VehicleId=@v", conn);

                cmd.Parameters.AddWithValue("@v", vehicleId);

                object result = cmd.ExecuteScalar();
                if (result == null || result == DBNull.Value)
                    return null;

                return Convert.ToDateTime(result);
            }
        }
    }
}
