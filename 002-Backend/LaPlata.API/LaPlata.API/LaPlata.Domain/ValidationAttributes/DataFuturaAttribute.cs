using System.ComponentModel.DataAnnotations;

namespace LaPlata.Domain.ValidationAttributes
{
    public class DataFuturaAttribute : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            DateTime obj = Convert.ToDateTime(value);
            return obj >= DateTime.Now;
        }
    }
}
