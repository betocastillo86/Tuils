using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Services.Common
{
    public static class DateTimeExtensions
    {
        /// <summary>
        /// Agrega dias a una fecha pero tiene en cuenta que si son 30, 60 o 90 dias agrega en meses
        /// </summary>
        /// <param name="days"></param>
        /// <returns></returns>
        public static DateTime AddDaysToPlan(this DateTime date, int days)
        {
            //Multiplo de 30, agrega los meses
            if (days % 30 == 0)
                return date.AddMonths(days / 30).Date.AddHours(23).AddMinutes(59);
            else
                return date.AddDays(days).Date.AddHours(23).AddMinutes(59);
        }
    }
}
