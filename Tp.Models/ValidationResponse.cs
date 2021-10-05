using System;

namespace Tp.Models
{
    public class ValidationResponse
    {
        public bool Success { get; set; } = true;
        public string Error { get; set; }

        public ValidationResponse AddError(string error)
        {
            Success = false;
            Error = error;
            return this;
        }
    }
}
