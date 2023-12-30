using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Capstone.Models;
using Capstone.DAL.Interfaces;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Capstone.DAL
{
    public class UserDAL : IUserDAL
    {
        private readonly string connectionString;

        private const string SQL_GetTenants = "SELECT* FROM site_user WHERE role = 'tenant';";

        public UserDAL(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public User GetUser(string emailAddress)
        {
            User user = null;
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("SELECT * FROM site_user WHERE email_address = @emailAddress;", conn);
                    cmd.Parameters.AddWithValue("@emailAddress", emailAddress);

                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        user = MapRowToUser(reader);
                    }
                }
            }
            catch (SqlException)
            {
                user = null;
            }

            return user;
        }

        public bool CreateUser(User user)
        {
            bool result = false;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("INSERT INTO site_user VALUES (@first_name, @last_name, @phone_number, @email_address, @role, @password, @salt);", conn);
                    cmd.Parameters.AddWithValue("@first_name", user.FirstName);
                    cmd.Parameters.AddWithValue("@last_name", user.LastName);
                    cmd.Parameters.AddWithValue("@phone_number", user.PhoneNumber);
                    cmd.Parameters.AddWithValue("@email_address", user.EmailAddress);
                    cmd.Parameters.AddWithValue("@role", user.Role);
                    cmd.Parameters.AddWithValue("@password", user.Password);
                    cmd.Parameters.AddWithValue("@salt", user.Salt);

                    result = (cmd.ExecuteNonQuery() > 0) ? true : false;
                }
            }
            catch (SqlException)
            {
                result = false;
            }

            return result;
        }

        public bool UpdateUser(User user)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("UPDATE site_user SET password = @password, salt = @salt, role = @role WHERE id = @id;", conn);
                    cmd.Parameters.AddWithValue("@password", user.Password);
                    cmd.Parameters.AddWithValue("@salt", user.Salt);
                    cmd.Parameters.AddWithValue("@role", user.Role);
                    cmd.Parameters.AddWithValue("@id", user.UserID);

                    return cmd.ExecuteNonQuery() >= 1 ? true : false;
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }

        public bool DeleteUser(User user)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("DELETE FROM site_user WHERE id = @id;", conn);
                    cmd.Parameters.AddWithValue("@id", user.UserID);

                    return cmd.ExecuteNonQuery() >= 1 ? true : false;
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }

        public List<User> GetTenantUsers()
        {
            List<User> output = new List<User>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(SQL_GetTenants, conn);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        User user = new User();

                        user.UserID = Convert.ToInt32(reader["user_id"]);
                        user.EmailAddress = Convert.ToString(reader["email_address"]);
                        user.Password = Convert.ToString(reader["password"]);
                        user.Salt = Convert.ToString(reader["salt"]);
                        user.Role = Convert.ToString(reader["role"]);
                        user.FirstName = Convert.ToString(reader["first_name"]);
                        user.LastName = Convert.ToString(reader["last_name"]);
                        user.PhoneNumber = Convert.ToString(reader["phone_number"]);

                        output.Add(user);
                    }
                }
            }
            catch (SqlException)
            {
                output = new List<User>();
            }

            return output;
        }

        public List<SelectListItem> GetUserEmailSelectList()
        {
            List<SelectListItem> output = new List<SelectListItem>();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandText = SQL_GetTenants;
                    cmd.Connection = connection;

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        SelectListItem item = new SelectListItem();

                        item.Text = Convert.ToString(reader["email_address"]);
                        item.Value = Convert.ToString(reader["email_address"]);

                        output.Add(item);
                    }
                }
            }
            catch (SqlException ex)
            {
                output = new List<SelectListItem>();
            }

            return output;
        }

        private User MapRowToUser(SqlDataReader reader)
        {
            return new User()
            {
                UserID = Convert.ToInt32(reader["user_id"]),
                EmailAddress = Convert.ToString(reader["email_address"]),
                Password = Convert.ToString(reader["password"]),
                Salt = Convert.ToString(reader["salt"]),
                Role = Convert.ToString(reader["role"]),
                FirstName = Convert.ToString(reader["first_name"]),
                LastName = Convert.ToString(reader["last_name"]),
                PhoneNumber = Convert.ToString(reader["phone_number"])
            };
        }
    }
}
