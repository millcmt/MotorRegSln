using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace MotorRegSln
{
    /// <summary>
    /// Summary description for FitnessServices
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class FitnessServices : System.Web.Services.WebService
    {

        private string connString = System.Configuration.ConfigurationManager
                                    .ConnectionStrings["MotorRegDB"]
                                    .ConnectionString;

        /// <summary>
        /// Check if a vehicle’s fitness is up to date by chassis number.
        /// Returns:
        ///   "UpToDate|2025-10-01"
        ///   "Expired|2023-05-10"
        ///   "NotFound|No fitness record found"
        /// </summary>
        [WebMethod]
        public string CheckFitness(string chassisNumber)
        {
            try
            {
                int vehicleId = GetVehicleIdByChassis(chassisNumber);

                if (vehicleId == 0)
                    return "NotFound|Chassis number not found";

                DateTime? validUntil = GetFitnessExpiry(vehicleId);

                if (validUntil == null)
                    return "NotFound|No fitness record found";

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


        // -----------------------------
        //   HELPER METHODS
        // -----------------------------

        private int GetVehicleIdByChassis(string chassis)
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(
                    "SELECT VehicleId FROM Vehicles WHERE ChassisNumber=@c", conn);

                cmd.Parameters.AddWithValue("@c", chassis);

                object result = cmd.ExecuteScalar();
                return result == null ? 0 : Convert.ToInt32(result);
            }
        }


        private DateTime? GetFitnessExpiry(int vehicleId)
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(
                    @"SELECT ValidUntil 
                      FROM FitnessRecords 
                      WHERE VehicleId=@v", conn);

                cmd.Parameters.AddWithValue("@v", vehicleId);

                object result = cmd.ExecuteScalar();

                if (result == null || result == DBNull.Value)
                    return null;

                return Convert.ToDateTime(result);
            }
        }
    }
}
