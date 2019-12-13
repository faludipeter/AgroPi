using Iot.Device.Rtc;
using System;
using System.Collections.Generic;
using System.Device.I2c;
using System.Text;

namespace AgroPi.SAL.Sensors
{
    public class RTCHardwareSensor
    {
        private I2cConnectionSettings settings;
        private I2cDevice device;
        public RTCHardwareSensor()
        {
            settings = new I2cConnectionSettings(1, Ds3231.DefaultI2cAddress);
            device = I2cDevice.Create(settings);

            using (Ds3231 rtc = new Ds3231(device))
            {
                rtc.DateTime = DateTime.Now;
            }
        }

        public DateTimeOffset GetDateTime()
        {
            using (Ds3231 rtc = new Ds3231(device))
            {
                return rtc.DateTime;
            }
        }
    }
}

