using System;
using System.Collections.Generic;
using System.Text;

namespace AgroPi.BLL.Dtos
{
    public class SensorDataHeader
    {
        public int Id { get; set; }

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

        /// <summary>
        /// Measured Lux value
        /// </summary>
        public float Lux { get; set; }

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
