using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Ekstraklasa
{
    class TimeFormatRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (DateTime.TryParseExact((value as string), "HH:mm", cultureInfo, DateTimeStyles.None, out DateTime parsed))
            {
                return new ValidationResult(true, null);
            }
            else
            {
                return new ValidationResult(false, "Zły format czasu");
            }
        }
    }
}
