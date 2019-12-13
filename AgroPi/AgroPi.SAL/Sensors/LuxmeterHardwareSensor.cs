using System;
using System.Collections.Generic;
using System.Device.I2c;
using System.Text;
using AgroPi.SAL.Sensors.TSL2591;

namespace AgroPi.SAL.Sensors
{
    public class LuxmeterHardwareSensor
    {
        private I2cConnectionSettings settings;
        private I2cDevice device;

        public TSL2591Sensor TSL2591Sensor { get; set; }

        public LuxmeterHardwareSensor()
        {
            settings = new I2cConnectionSettings(1, TSL2591Sensor.DefaultI2cAddress);
            device = I2cDevice.Create(settings);
        }

        public float GetLux()
        {
            using (TSL2591Sensor = new TSL2591Sensor(device))
            {
                return TSL2591Sensor.GetLux();
            }
        }
    }
}
