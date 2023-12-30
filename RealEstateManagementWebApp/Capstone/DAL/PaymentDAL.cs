using Capstone.DAL.Interfaces;
using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.DAL
{
    public class PaymentDAL : IPaymentDAL
    {
        private const string SQL_AddPayment = "INSERT INTO payment (unit_id, tenant_id, payment_amount, payment_date, payment_for_month) VALUES (@unitID, @tenant_id, @payment_amount, GETDATE(), @payment_for_month);";
        private const string SQL_GetYTDPaymentsForUnit = "  SELECT * FROM payment JOIN unit ON payment.unit_id = unit.unit_id WHERE payment_date BETWEEN DATEADD(yy, DATEDIFF(yy, 0, GETDATE()), 0) AND GETDATE() AND unit.unit_id = @unitID; ";

        private string connectionString;

        public PaymentDAL(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public bool SubmitPayment(Payment payment)
        {
            bool result;

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    SqlCommand cmd = new SqlCommand(SQL_AddPayment, connection);
                    cmd.Parameters.AddWithValue("@unitID", payment.UnitID);
                    cmd.Parameters.AddWithValue("@tenant_id", payment.TenantID);
                    cmd.Parameters.AddWithValue("@payment_amount", payment.PaymentAmount);
                    cmd.Parameters.AddWithValue("@payment_for_month", payment.PaymentForMonth);

                    cmd.ExecuteNonQuery();
                }
                result = true;
            }
            catch (Exception)
            {
                result = false;
                throw;
            }
            return result;
        }

        public decimal GetYTDPaymentsforUnit(int unitID)
        {
            decimal result = 0;

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    SqlCommand cmd = new SqlCommand(SQL_GetYTDPaymentsForUnit, connection);

                    cmd.Parameters.AddWithValue("@unitID", unitID);

                    SqlDataReader reader = cmd.ExecuteReader();
                    while(reader.Read())
                    {
                        result += Convert.ToDecimal(reader["payment_amount"]);

                    }

                }
            }
            catch (Exception)
            {

                throw;
            }

            return result;
        }
    }
}
