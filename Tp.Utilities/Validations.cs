using System;
using System.Text.RegularExpressions;
using Tp.Models;

namespace TP.Utilities
{
    public class Validations
    {
        public static ValidationResponse ValidateName(string name)
        {
            var response = new ValidationResponse();

            if (string.IsNullOrEmpty(name))
                return response.AddError("El nombre es nulo o vacio.");

            if (name.Length > 20)
                return response.AddError("El nombre debe tener una longitud maxima de 20 caracteres.");

            if (!Regex.IsMatch(name, "^[a-zA-Z0-9äöüÄÖÜ]*$"))
                return response.AddError("Los caracteres ingresados no son validos.");

            return response;
        }
    }
}
