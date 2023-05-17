using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using System.Reflection.Metadata;

namespace kayaadmin.Pages.Customers
{
    public class EditModel : PageModel
    {
        public Customersinfo customersinfo = new Customersinfo();
        public string errorMessage = "";
        public string successMessage = "";
        public void OnGet()
        {
            String custcode = Request.Query["custcode"];
            try
            {
                String connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=KAYADB;Integrated Security=True";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String query = "select * from customers where custcode = @custcode";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@custcode" , custcode);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                Customersinfo customersinfo = new Customersinfo();
                                customersinfo.firstname = reader.GetString(0);
                                customersinfo.lastname = reader.GetString(1);
                                customersinfo.custcode = reader.GetString(2);
                                customersinfo.email = reader.GetString(3);
                                customersinfo.phoneno = reader.GetString(4);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
              errorMessage = ex.Message;  
            }
        }
        public void OnPost() 
        {
            customersinfo.firstname = Request.Form["firstname"];
            customersinfo.lastname = Request.Form["lastname"];
            customersinfo.custcode = Request.Form["custcode"];
            customersinfo.email = Request.Form["email"];
            customersinfo.phoneno = Request.Form["phoneno"];

            if (customersinfo.firstname.Length == 0 || customersinfo.lastname.Length == 0 || customersinfo.custcode.Length == 0 || customersinfo.email.Length == 0 || customersinfo.phoneno.Length == 0)
            {
                errorMessage = "All the fields are required";
                return;
            }
            try
            {
                String connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=KAYADB;Integrated Security=True";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    String query = "update customers set firstname=@firstname,lastname=@lastname,custcode=@custcode,email=@email,phoneno=phoneno where custcode = @custcode;";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@firstname", customersinfo.firstname);
                        command.Parameters.AddWithValue("@lastname", customersinfo.lastname);
                        command.Parameters.AddWithValue("@custcode", customersinfo.custcode);
                        command.Parameters.AddWithValue("@email", customersinfo.email);
                        command.Parameters.AddWithValue("@phoneno", customersinfo.phoneno);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }
            Response.Redirect("Index");
        }
    }
}
