using Capstone.DAL.Interfaces;
using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.DAL
{
    public class ServiceRequestDAL : IServiceRequestDAL
    {
        private const string SQL_AddServiceRequest = "INSERT INTO service_request (tenant_id, description, is_emergency, category, is_completed) VALUES (@tenantID, @description, @isEmergency, @category, @isCompleted);";

        private string connectionString;

        public ServiceRequestDAL(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public bool AddServiceRequest(ServiceRequest serviceRequest)
        {
            bool result;

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    SqlCommand cmd = new SqlCommand(SQL_AddServiceRequest, connection);
                    cmd.Parameters.AddWithValue("@tenantID", serviceRequest.TenantID);
                    cmd.Parameters.AddWithValue("@description", serviceRequest.Description);
                    cmd.Parameters.AddWithValue("@isEmergency", serviceRequest.IsEmergency);
                    cmd.Parameters.AddWithValue("@category", serviceRequest.Category);
                    cmd.Parameters.AddWithValue("@isCompleted", serviceRequest.IsCompleted);

                    cmd.ExecuteNonQuery();
                }

                result = true;
            }
            catch (SqlException ex)
            {
                result = false;
                throw ex;
            }

            return result;
        }

        //TODO: Add GetAllServiceRequests()

        //public List<ServiceRequest> GetAllServiceRequests()
        //{

        //}
    }
}
