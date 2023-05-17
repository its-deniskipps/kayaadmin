using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace kayaadmin.Pages.Payments
{
    public class IndexModel : PageModel
    {
        public List<Paymentsinfo> listPayments = new List<Paymentsinfo>();
        public void OnGet()
        {
            try
            {
                String connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=KAYADB;Integrated Security=True";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String query = "select customers.custcode, paymentAmt,token,CreatedTime,lastname from payments,customers where payments.custcode=customers.custcode order by CreatedTime desc";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Paymentsinfo PamtDet = new Paymentsinfo
                                {
                                    custcode = reader.GetString(0),
                                    paymentAmt = reader.GetString(1),
                                    token = reader.GetString(2),
                                    CreatedTime = reader.GetString(3),
                                    lastname = reader.GetString(4)
                                };

                                listPayments.Add(PamtDet);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.ToString());
            }
        }
        public class Paymentsinfo
        {
            public string custcode;
            public string paymentAmt;
            public string token;
            public string CreatedTime;
            public string lastname;
        }
    }
}
