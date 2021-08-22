using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace CustomIdentityApp.Models
{
    public class CustomUserValidator : IUserValidator<User>
    {
        public Task<IdentityResult> ValidateAsync(UserManager<User> manager, User user)
        {
            List<IdentityError> errors = new List<IdentityError>();

            if (user.Email.ToLower().EndsWith("@spam.com"))
            {
                errors.Add(new IdentityError
                {
                    Description = "Данный домен находится в спам-базе. Выберите другой почтовый сервис"
                });
            }
            if (user.UserName.Contains("admin"))
            {
                errors.Add(new IdentityError
                {
                    Description = "Ник пользователя не должен содержать слово 'admin'"
                });
            }
            const string connectionString = "Server=(localdb)\\mssqllocaldb;Database=usersstoredb;Trusted_Connection=True;";
            SqlConnection connection = new SqlConnection(connectionString);
            SqlDataAdapter adapter = new SqlDataAdapter("SELECT UserName FROM AspNetUsers WHERE Year > 0", connection);
            DataTable table = new DataTable();
            adapter.Fill(table);
            List<string> list = new List<string>();
        foreach (DataRow row in table.Rows)
            {
                list.Add(row[0].ToString());
                //row[2].ToString(); //- к отдельной ячейке в указанной строке
            }
            foreach (var names in list)
            {
                if (user.UserName.Contains(names))
                {
                    errors.Add(new IdentityError
                    {
                        Description = "Этот Email уже используется! Введите какой-нибудь другой."
                    });
                }
            }

            return Task.FromResult(errors.Count == 0 ?
                IdentityResult.Success : IdentityResult.Failed(errors.ToArray()));
        }
    }
}
