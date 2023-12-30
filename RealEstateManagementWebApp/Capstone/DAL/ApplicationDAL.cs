using Capstone.DAL.Interfaces;
using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.DAL
{
    public class ApplicationDAL : IApplicationDAL
    {
        private const string SQL_AddProperty = "INSERT INTO tenant_application (unit_id, first_name, last_name, social_security_number, phone_number, email_address, last_residence_owner, last_residence_contact_phone_number, last_residence_tenancy_start_date, last_residence_tenancy_end_date, employment_status, employer_name, employer_contact_phone_number, annual_income, number_of_residents, number_of_cats, number_of_dogs) VALUES (@unitID, @firstName, @lastName, @socialSecurityNumber, @phoneNumber, @emailAddress, @lastResidenceOwner, @lastResidencePhoneNumber, @lastResidenceTenancyStartDate, @lastResidenceTenancyEndDate, @employmentStatus, @employerName, @employerContactPhoneNumber, @annualIncome, @numOfResidents, @numOfCats, @numOfDogs);";
        private const string SQL_GetAllUnreviewedApplications = "SELECT * FROM tenant_application WHERE application_approval_status IS NULL;";
        private const string SQL_ApproveApplication = "UPDATE tenant_application SET application_approval_status = 1 WHERE application_id = @applicationID;";
        private const string SQL_DenyApplication = "UPDATE tenant_application SET application_approval_status = 0 WHERE application_id = @applicationID;";
        private const string SQL_GetAllApplications = "SELECT * FROM tenant_application;";

        //private const string SQL_GetAllApprovedApplications = "SELECT * FROM tenant_application WHERE application_approval_status IS 1;";
        //private const string SQL_GetAllDeniedApplications = "SELECT * FROM tenant_application WHERE application_approval_status IS 0;";

        private string connectionString;

        public ApplicationDAL(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public bool AddApplication(Application application)
        {
            bool result;

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    SqlCommand cmd = new SqlCommand(SQL_AddProperty, connection);

                    cmd.Parameters.AddWithValue("@unitID", application.UnitID);
                    cmd.Parameters.AddWithValue("@firstName", application.FirstName);
                    cmd.Parameters.AddWithValue("@lastName", application.LastName);
                    cmd.Parameters.AddWithValue("@socialSecurityNumber", application.SocialSecurityNumber);
                    cmd.Parameters.AddWithValue("@phoneNumber", application.PhoneNumber);
                    cmd.Parameters.AddWithValue("@emailAddress", application.EmailAddress);
                    cmd.Parameters.AddWithValue("@lastResidenceOwner", application.LastResidenceOwner);
                    cmd.Parameters.AddWithValue("@lastResidencePhoneNumber", application.LastResidenceContactPhoneNumber);
                    cmd.Parameters.AddWithValue("@lastResidenceTenancyStartDate", application.LastResidenceTenancyStartDate);
                    cmd.Parameters.AddWithValue("@lastResidenceTenancyEndDate", application.LastResidenceTenancyEndDate);
                    cmd.Parameters.AddWithValue("@employmentStatus", application.EmploymentStatus);
                    cmd.Parameters.AddWithValue("@employerName", application.EmployerName);
                    cmd.Parameters.AddWithValue("@employerContactPhoneNumber", application.EmployerContactPhoneNumber);
                    cmd.Parameters.AddWithValue("@annualIncome", application.AnnualIncome);
                    cmd.Parameters.AddWithValue("@numOfResidents", application.NumberOfResidents);
                    cmd.Parameters.AddWithValue("@numOfCats", application.NumberOfCats);
                    cmd.Parameters.AddWithValue("@numOfDogs", application.NumberOfDogs);

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

        public List<Application> GetAllUnreviewedApplications()
        {
            List<Application> output = new List<Application>();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    SqlCommand cmd = new SqlCommand(SQL_GetAllUnreviewedApplications, connection);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Application application = new Application();

                        //All following if statements prevent an exception by checking if the database value is null before setting the property
                        application.ApplicationID = Convert.ToInt32(reader["application_id"]);
                        if (!Convert.IsDBNull(reader["unit_id"]))
                        {
                            application.UnitID = Convert.ToInt32(reader["unit_id"]);
                        }
                        application.FirstName = Convert.ToString(reader["first_name"]);
                        application.LastName = Convert.ToString(reader["last_name"]);
                        if (!Convert.IsDBNull(reader["social_security_number"]))
                        {
                            application.SocialSecurityNumber = Convert.ToInt32(reader["social_security_number"]);
                        }
                        application.PhoneNumber = Convert.ToString(reader["phone_number"]);
                        application.EmailAddress = Convert.ToString(reader["email_address"]);
                        if (!Convert.IsDBNull(reader["last_residence_owner"]))
                        {
                            application.LastResidenceOwner = Convert.ToString(reader["last_residence_owner"]);
                        }
                        if (!Convert.IsDBNull(reader["last_residence_contact_phone_number"]))
                        {
                            application.LastResidenceContactPhoneNumber = Convert.ToString(reader["last_residence_contact_phone_number"]);
                        }
                        if (!Convert.IsDBNull(reader["last_residence_tenancy_start_date"]))
                        {
                            application.LastResidenceTenancyStartDate = Convert.ToString(reader["last_residence_tenancy_start_date"]);
                        }
                        if (!Convert.IsDBNull(reader["last_residence_tenancy_end_date"]))
                        {
                            application.LastResidenceTenancyEndDate = Convert.ToString(reader["last_residence_tenancy_end_date"]);
                        }
                        if (!Convert.IsDBNull(reader["employment_status"]))
                        {
                            application.EmploymentStatus = Convert.ToBoolean(reader["employment_status"]);
                        }
                        if (!Convert.IsDBNull(reader["employer_name"]))
                        {
                            application.EmployerName = Convert.ToString(reader["employer_name"]);
                        }
                        if (!Convert.IsDBNull(reader["employer_contact_phone_number"]))
                        {
                            application.EmployerContactPhoneNumber = Convert.ToString(reader["employer_contact_phone_number"]);
                        }
                        if (!Convert.IsDBNull(reader["annual_income"]))
                        {
                            application.AnnualIncome = Convert.ToString(reader["annual_income"]);
                        }
                        application.NumberOfResidents = Convert.ToInt32(reader["number_of_residents"]);
                        if (!Convert.IsDBNull(reader["number_of_cats"]))
                        {
                            application.NumberOfCats = Convert.ToInt32(reader["number_of_cats"]);
                        }
                        if (!Convert.IsDBNull(reader["number_of_dogs"]))
                        {
                            application.NumberOfDogs = Convert.ToInt32(reader["number_of_dogs"]);
                        }
                        if (!Convert.IsDBNull(reader["application_approval_status"]))
                        {
                            application.ApplicationApprovalStatus = Convert.ToBoolean(reader["application_approval_status"]);
                        }

                        output.Add(application);
                    }
                }
            }
            catch (SqlException ex)
            {
                output = new List<Application>();
                throw ex;
            }

            return output;
        }

        public bool ApproveApplication(int applicationID)
        {
            bool result;

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    SqlCommand cmd = new SqlCommand(SQL_ApproveApplication, connection);

                    cmd.Parameters.AddWithValue("@applicationID", applicationID);

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

        public bool DenyApplication(int applicationID)
        {
            bool result;

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    SqlCommand cmd = new SqlCommand(SQL_DenyApplication, connection);

                    cmd.Parameters.AddWithValue("@applicationID", applicationID);

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

        public List<Application> GetAllApplications()
        {
            List<Application> output = new List<Application>();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    SqlCommand cmd = new SqlCommand(SQL_GetAllApplications, connection);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Application application = new Application();

                        //All following if statements prevent an exception by checking if the database value is null before setting the property
                        application.ApplicationID = Convert.ToInt32(reader["application_id"]);
                        if (!Convert.IsDBNull(reader["unit_id"]))
                        {
                            application.UnitID = Convert.ToInt32(reader["unit_id"]);
                        }
                        application.FirstName = Convert.ToString(reader["first_name"]);
                        application.LastName = Convert.ToString(reader["last_name"]);
                        if (!Convert.IsDBNull(reader["social_security_number"]))
                        {
                            application.SocialSecurityNumber = Convert.ToInt32(reader["social_security_number"]);
                        }
                        application.PhoneNumber = Convert.ToString(reader["phone_number"]);
                        application.EmailAddress = Convert.ToString(reader["email_address"]);
                        if (!Convert.IsDBNull(reader["last_residence_owner"]))
                        {
                            application.LastResidenceOwner = Convert.ToString(reader["last_residence_owner"]);
                        }
                        if (!Convert.IsDBNull(reader["last_residence_contact_phone_number"]))
                        {
                            application.LastResidenceContactPhoneNumber = Convert.ToString(reader["last_residence_contact_phone_number"]);
                        }
                        if (!Convert.IsDBNull(reader["last_residence_tenancy_start_date"]))
                        {
                            application.LastResidenceTenancyStartDate = Convert.ToString(reader["last_residence_tenancy_start_date"]);
                        }
                        if (!Convert.IsDBNull(reader["last_residence_tenancy_end_date"]))
                        {
                            application.LastResidenceTenancyEndDate = Convert.ToString(reader["last_residence_tenancy_end_date"]);
                        }
                        if (!Convert.IsDBNull(reader["employment_status"]))
                        {
                            application.EmploymentStatus = Convert.ToBoolean(reader["employment_status"]);
                        }
                        if (!Convert.IsDBNull(reader["employer_name"]))
                        {
                            application.EmployerName = Convert.ToString(reader["employer_name"]);
                        }
                        if (!Convert.IsDBNull(reader["employer_contact_phone_number"]))
                        {
                            application.EmployerContactPhoneNumber = Convert.ToString(reader["employer_contact_phone_number"]);
                        }
                        if (!Convert.IsDBNull(reader["annual_income"]))
                        {
                            application.AnnualIncome = Convert.ToString(reader["annual_income"]);
                        }
                        application.NumberOfResidents = Convert.ToInt32(reader["number_of_residents"]);
                        if (!Convert.IsDBNull(reader["number_of_cats"]))
                        {
                            application.NumberOfCats = Convert.ToInt32(reader["number_of_cats"]);
                        }
                        if (!Convert.IsDBNull(reader["number_of_dogs"]))
                        {
                            application.NumberOfDogs = Convert.ToInt32(reader["number_of_dogs"]);
                        }
                        if (!Convert.IsDBNull(reader["application_approval_status"]))
                        {
                            application.ApplicationApprovalStatus = Convert.ToBoolean(reader["application_approval_status"]);
                        }

                        output.Add(application);
                    }
                }
            }
            catch (SqlException ex)
            {
                output = new List<Application>();
                throw ex;
            }

            return output;
        }

        //public List<Application> GetAllApprovedApplications()
        //{
        //    List<Application> output = new List<Application>();

        //    try
        //    {
        //        using (SqlConnection connection = new SqlConnection(connectionString))
        //        {
        //            connection.Open();

        //            SqlCommand cmd = new SqlCommand(SQL_GetAllApprovedApplications, connection);
        //            SqlDataReader reader = cmd.ExecuteReader();

        //            while (reader.Read())
        //            {
        //                Application application = new Application();

        //                //All following if statements prevent an exception by checking if the database value is null before setting the property
        //                application.ApplicationID = Convert.ToInt32(reader["application_id"]);
        //                if (!Convert.IsDBNull(reader["unit_id"]))
        //                {
        //                    application.UnitID = Convert.ToInt32(reader["unit_id"]);
        //                }
        //                application.FirstName = Convert.ToString(reader["first_name"]);
        //                application.LastName = Convert.ToString(reader["last_name"]);
        //                if (!Convert.IsDBNull(reader["social_security_number"]))
        //                {
        //                    application.SocialSecurityNumber = Convert.ToInt32(reader["social_security_number"]);
        //                }
        //                application.PhoneNumber = Convert.ToString(reader["phone_number"]);
        //                application.EmailAddress = Convert.ToString(reader["email_address"]);
        //                if (!Convert.IsDBNull(reader["last_residence_owner"]))
        //                {
        //                    application.LastResidenceOwner = Convert.ToString(reader["last_residence_owner"]);
        //                }
        //                if (!Convert.IsDBNull(reader["last_residence_contact_phone_number"]))
        //                {
        //                    application.LastResidenceContactPhoneNumber = Convert.ToString(reader["last_residence_contact_phone_number"]);
        //                }
        //                if (!Convert.IsDBNull(reader["last_residence_tenancy_start_date"]))
        //                {
        //                    application.LastResidenceTenancyStartDate = Convert.ToString(reader["last_residence_tenancy_start_date"]);
        //                }
        //                if (!Convert.IsDBNull(reader["last_residence_tenancy_end_date"]))
        //                {
        //                    application.LastResidenceTenancyEndDate = Convert.ToString(reader["last_residence_tenancy_end_date"]);
        //                }
        //                if (!Convert.IsDBNull(reader["employment_status"]))
        //                {
        //                    application.EmploymentStatus = Convert.ToBoolean(reader["employment_status"]);
        //                }
        //                if (!Convert.IsDBNull(reader["employer_name"]))
        //                {
        //                    application.EmployerName = Convert.ToString(reader["employer_name"]);
        //                }
        //                if (!Convert.IsDBNull(reader["employer_contact_phone_number"]))
        //                {
        //                    application.EmployerContactPhoneNumber = Convert.ToString(reader["employer_contact_phone_number"]);
        //                }
        //                if (!Convert.IsDBNull(reader["annual_income"]))
        //                {
        //                    application.AnnualIncome = Convert.ToString(reader["annual_income"]);
        //                }
        //                application.NumberOfResidents = Convert.ToInt32(reader["number_of_residents"]);
        //                if (!Convert.IsDBNull(reader["number_of_cats"]))
        //                {
        //                    application.NumberOfCats = Convert.ToInt32(reader["number_of_cats"]);
        //                }
        //                if (!Convert.IsDBNull(reader["number_of_dogs"]))
        //                {
        //                    application.NumberOfDogs = Convert.ToInt32(reader["number_of_dogs"]);
        //                }
        //                if (!Convert.IsDBNull(reader["application_approval_status"]))
        //                {
        //                    application.ApplicationApprovalStatus = Convert.ToBoolean(reader["application_approval_status"]);
        //                }

        //                output.Add(application);
        //            }
        //        }
        //    }
        //    catch (SqlException ex)
        //    {
        //        output = new List<Application>();
        //        throw ex;
        //    }

        //    return output;
        //}

        //public List<Application> GetAllDeniedApplications()
        //{
        //    List<Application> output = new List<Application>();

        //    try
        //    {
        //        using (SqlConnection connection = new SqlConnection(connectionString))
        //        {
        //            connection.Open();

        //            SqlCommand cmd = new SqlCommand(SQL_GetAllDeniedApplications, connection);
        //            SqlDataReader reader = cmd.ExecuteReader();

        //            while (reader.Read())
        //            {
        //                Application application = new Application();

        //                //All following if statements prevent an exception by checking if the database value is null before setting the property
        //                application.ApplicationID = Convert.ToInt32(reader["application_id"]);
        //                if (!Convert.IsDBNull(reader["unit_id"]))
        //                {
        //                    application.UnitID = Convert.ToInt32(reader["unit_id"]);
        //                }
        //                application.FirstName = Convert.ToString(reader["first_name"]);
        //                application.LastName = Convert.ToString(reader["last_name"]);
        //                if (!Convert.IsDBNull(reader["social_security_number"]))
        //                {
        //                    application.SocialSecurityNumber = Convert.ToInt32(reader["social_security_number"]);
        //                }
        //                application.PhoneNumber = Convert.ToString(reader["phone_number"]);
        //                application.EmailAddress = Convert.ToString(reader["email_address"]);
        //                if (!Convert.IsDBNull(reader["last_residence_owner"]))
        //                {
        //                    application.LastResidenceOwner = Convert.ToString(reader["last_residence_owner"]);
        //                }
        //                if (!Convert.IsDBNull(reader["last_residence_contact_phone_number"]))
        //                {
        //                    application.LastResidenceContactPhoneNumber = Convert.ToString(reader["last_residence_contact_phone_number"]);
        //                }
        //                if (!Convert.IsDBNull(reader["last_residence_tenancy_start_date"]))
        //                {
        //                    application.LastResidenceTenancyStartDate = Convert.ToString(reader["last_residence_tenancy_start_date"]);
        //                }
        //                if (!Convert.IsDBNull(reader["last_residence_tenancy_end_date"]))
        //                {
        //                    application.LastResidenceTenancyEndDate = Convert.ToString(reader["last_residence_tenancy_end_date"]);
        //                }
        //                if (!Convert.IsDBNull(reader["employment_status"]))
        //                {
        //                    application.EmploymentStatus = Convert.ToBoolean(reader["employment_status"]);
        //                }
        //                if (!Convert.IsDBNull(reader["employer_name"]))
        //                {
        //                    application.EmployerName = Convert.ToString(reader["employer_name"]);
        //                }
        //                if (!Convert.IsDBNull(reader["employer_contact_phone_number"]))
        //                {
        //                    application.EmployerContactPhoneNumber = Convert.ToString(reader["employer_contact_phone_number"]);
        //                }
        //                if (!Convert.IsDBNull(reader["annual_income"]))
        //                {
        //                    application.AnnualIncome = Convert.ToString(reader["annual_income"]);
        //                }
        //                application.NumberOfResidents = Convert.ToInt32(reader["number_of_residents"]);
        //                if (!Convert.IsDBNull(reader["number_of_cats"]))
        //                {
        //                    application.NumberOfCats = Convert.ToInt32(reader["number_of_cats"]);
        //                }
        //                if (!Convert.IsDBNull(reader["number_of_dogs"]))
        //                {
        //                    application.NumberOfDogs = Convert.ToInt32(reader["number_of_dogs"]);
        //                }
        //                if (!Convert.IsDBNull(reader["application_approval_status"]))
        //                {
        //                    application.ApplicationApprovalStatus = Convert.ToBoolean(reader["application_approval_status"]);
        //                }

        //                output.Add(application);
        //            }
        //        }
        //    }
        //    catch (SqlException ex)
        //    {
        //        output = new List<Application>();
        //        throw ex;
        //    }

        //    return output;
        //}
    }
}
