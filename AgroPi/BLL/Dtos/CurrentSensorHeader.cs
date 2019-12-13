using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Dtos
{
    public class CurrentSensorHeader
    {
        /// <summary>
        /// Date and time of measur
        /// </summary>
        public DateTimeOffset DateTime { get; set; }

        /// <summary>
        /// Read the measured shunt voltage.
        /// </summary>
        public float ShuntVoltage { get; set; }

        /// <summary>
        /// Read the measured Bus voltage.
        /// </summary>
        public float BusVoltage { get; set; }

        /// <summary>
        /// Read the calculated current through the INA219.
        /// </summary>
        public float Current { get; set; }

        /// <summary>
        /// Get the calculated power in the circuit being monitored by the INA219.
        /// </summary>
        public float Power { get; set; }
    }
}
