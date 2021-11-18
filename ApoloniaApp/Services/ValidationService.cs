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

        public static bool ValidateText(string text) => !string.IsNullOrEmpty(text);
        public static bool ValidateEmail(string email) => ValidateText(email) && Regex.IsMatch(email, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase);
        public static bool ValidateRun(string run) => ValidateText(run) && ValidateRunService.ValidaRut(run);
        public static bool ValidateComboBoxId<TModel>(TModel model)
            where TModel : ModelBase => model.Id != 0;
        public static void ValidateAll(List<Func<bool>> validations, bool canCrud)
        {

            foreach (Func<bool> f in validations)
            {
                if (!f())
                {
                    canCrud = false;
                    return;
                }
            }
            canCrud = true;
        }
        #endregion    

    }
}
