using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;

namespace crudd.Pages.Home
{
    public class EditModel : PageModel
    {
        [BindProperty]
        public int id { get; set; }

        [BindProperty, Required(ErrorMessage = "The firstname is required")]
        public string firstname { get; set; } = "";

        [BindProperty, Required(ErrorMessage = "The surname is required")]
        public string surname { get; set; } = "";

        [BindProperty, Required(ErrorMessage = "The Date is required")]
        [DataType(DataType.Date)]
        public DateTime register_date { get; set; }  // Alterado para DateTime

        [BindProperty, Phone]
        public string phone { get; set; } = "";
        
        [BindProperty, Phone]
        public string security_number { get; set; } = "";

        [BindProperty, Required]
        public string gender { get; set; } = "";

        [BindProperty, Required]
        public string nacionalidade { get; set; } = "";

        [BindProperty, Required(ErrorMessage = "The username is required")]
        public string username { get; set; } = "";

        [BindProperty, Required, EmailAddress(ErrorMessage = "The email is required")]
        public string email { get; set; } = "";

        [BindProperty, Required(ErrorMessage = "The password is required")]
        public string senha { get; set; } = "";

        public string ErrorMessage { get; set; } = "";

        public void OnGet(int id)
        {
            try
            {
                string connectionString = "Server=.;Database=Gabini;Trusted_Connection=True;TrustServerCertificate=True;";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string sql = "SELECT * FROM users WHERE id=@id";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@id", id);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                this.id = reader.GetInt32(0);
                                firstname = reader.GetString(1);
                                surname = reader.GetString(2);

                                if (!reader.IsDBNull(3))
                                {
                                    register_date = reader.GetDateTime(3);  // Pega a data diretamente como DateTime
                                }

                                phone = reader.GetString(4);
                                security_number = reader.GetString(5);
                                gender = reader.GetString(6);
                                nacionalidade = reader.GetString(7);
                                username = reader.GetString(8);
                                email = reader.GetString(9);
                                senha = reader.GetString(10);
                            }
                            else
                            {
                                Response.Redirect("/Home/Index");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (phone == null) phone = "";
            if (email == null) email = "";

            try
            {
                string connectionString = "Server=.;Database=Gabini;Trusted_Connection=True;TrustServerCertificate=True;";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string sql = "UPDATE users SET firstname=@firstname, surname=@surname, register_date=@register_date, phone=@phone," +
                        "security_number=@security_number, gender=@gender, nacionalidade=@nacionalidade, username=@username, email=@email, senha=@senha WHERE id=@id;";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@firstname", firstname);
                        command.Parameters.AddWithValue("@surname", surname);
                        command.Parameters.AddWithValue("@register_date", register_date); // Use a DateTime diretamente
                        command.Parameters.AddWithValue("@phone", phone);
                        command.Parameters.AddWithValue("@security_number", security_number);
                        command.Parameters.AddWithValue("@gender", gender);
                        command.Parameters.AddWithValue("@nacionalidade", nacionalidade);
                        command.Parameters.AddWithValue("@username", username);
                        command.Parameters.AddWithValue("@email", email);
                        command.Parameters.AddWithValue("@senha", senha);
                        command.Parameters.AddWithValue("@id", id);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return Page();
            }

            return RedirectToPage("/Home/Index"); // Redireciona após sucesso
        }
    }
}
