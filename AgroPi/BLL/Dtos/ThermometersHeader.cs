using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Dtos
{
    public class ThermometersHeader
    {
        /// <summary>
        /// Date and time of measur
        /// </summary>
        public DateTimeOffset DateTime { get; set; }

        /// <summary>
        /// Humidity in %rH
        /// </summary>
        public double Humidity { get; set; }

        /// <summary>
        /// Pressure in hPa
        /// </summary>
        public double Pressure { get; set; }

        /// <summary>
        /// Temperature in Celsius
        /// </summary>
        public double Temperature { get; set; }
    }
}
