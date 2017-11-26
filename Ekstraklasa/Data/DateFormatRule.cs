using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Ekstraklasa
{
    public class DateFormatRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            DateTime parsed;
            if (String.IsNullOrEmpty(value as string) || DateTime.TryParseExact((value as string), "dd.MM.yyyy", cultureInfo, DateTimeStyles.None, out parsed))
            {
                return new ValidationResult(true, null);
            }
            else
            {
                return new ValidationResult(false, "Zły format daty");
            }

        }
    }
}

