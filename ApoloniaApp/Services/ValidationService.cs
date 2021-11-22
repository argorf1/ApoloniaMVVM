using ApoloniaApp.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace ApoloniaApp.Services
{
    public class ValidationService
    {
        #region Validaciones

        public static bool Text(string text) => !string.IsNullOrEmpty(text);
        public static bool PhoneNumber(string text) => text.Length >= 8; 
        public static bool Email(string email) => Text(email) && Regex.IsMatch(email, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase);
        public static bool Run(string run) => Text(run) && (ValidateRunService.ValidaRut(run)|| run == "2");
        public static bool Password(string password)
        {
            Regex hasNumber = new Regex(@"[0-9]+");
            Regex hasUpperChar = new Regex(@"[A-Z]+");
            Regex hasLowerChar = new Regex(@"[a-z]+");
            Regex hasMinimum8Chars = new Regex(@".{8,}");

            return Text(password)
                && hasNumber.IsMatch(password)
                && hasUpperChar.IsMatch(password)
                && hasLowerChar.IsMatch(password)
                && hasMinimum8Chars.IsMatch(password);
        }
        public static bool Match(string password, string passwordConfirm) => Text(password) && Text(passwordConfirm) && password == passwordConfirm;
        public static bool ComboBoxId(int id)=> id != 0;

        public static bool All(List<Func<bool>> validations)
        {

            foreach (Func<bool> f in validations)
            {
                if (!f())
                {
                    return false;
                }
            }
            return true;
        }
        #endregion    

    }
}
