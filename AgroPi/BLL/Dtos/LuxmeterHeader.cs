using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Dtos
{
    public class LuxmeterHeader
    {
        /// <summary>
        /// Date and time of measur
        /// </summary>
        public DateTimeOffset DateTime { get; set; }

        /// <summary>
        /// Measured Lux value
        /// </summary>
        public float Lux { get; set; }
    }
}
