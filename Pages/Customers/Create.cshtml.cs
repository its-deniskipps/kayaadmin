using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace kayaadmin.Pages.Customers
{
    public class CreateModel : PageModel
    {
        public Customersinfo customerinfo = new Customersinfo();
        public string errorMessage = "";
        public string successMessage = "Success";
        public void OnGet()
        {
        }
        public void OnPost() 
        {
            customerinfo.firstname= Request.Form["firstname"];
            customerinfo.lastname = Request.Form["lastname"];
            customerinfo.custcode = Request.Form["custcode"];
            customerinfo.email = Request.Form["email"];
            customerinfo.phoneno = Request.Form["phoneno"];

            if(customerinfo.firstname.Length ==0 || customerinfo.lastname.Length == 0 || customerinfo.custcode.Length == 0 || customerinfo.email.Length == 0 || customerinfo.phoneno.Length == 0)
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

                    String query = "Insert into customers values (@firstname,@lastname,@custcode,@email,@phoneno);";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@firstname", customerinfo.firstname);
                        command.Parameters.AddWithValue("@lastname", customerinfo.lastname);
                        command.Parameters.AddWithValue("@custcode", customerinfo.custcode);
                        command.Parameters.AddWithValue("@email", customerinfo.email);
                        command.Parameters.AddWithValue("@phoneno", customerinfo.phoneno);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
             errorMessage = ex.Message;
                return;
            }
            customerinfo.firstname = "";
            customerinfo.lastname = "";
            customerinfo.custcode = "";
            customerinfo.email = "";
            customerinfo.phoneno = "";
            successMessage = "Customer Added Successfully";

            Response.Redirect("Index");
        }
    }
}
