using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace kayaadmin.Pages.Customers
{
    public class IndexModel : PageModel
    {
        public List<Customersinfo> listCustomers = new List<Customersinfo>();
        public void OnGet()
        {
            try
            {
                String connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=KAYADB;Integrated Security=True";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String query = "select * from customers";

                    using (SqlCommand command = new SqlCommand(query, connection)) 
                    {
                        using (SqlDataReader reader = command.ExecuteReader()) 
                        { 
                            while (reader.Read()) 
                            {
                                Customersinfo CustDet = new Customersinfo();
                                CustDet.firstname = reader.GetString(0);
                                CustDet.lastname = reader.GetString(1);
                                CustDet.custcode = reader.GetString(2);
                                CustDet.email = reader.GetString(3);
                                CustDet.phoneno = reader.GetString(4);

                                listCustomers.Add(CustDet);
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
    }

    public class Customersinfo
    {
        public string firstname;
        public string lastname;
        public string custcode;
        public string email;
        public string phoneno;
    }
}
