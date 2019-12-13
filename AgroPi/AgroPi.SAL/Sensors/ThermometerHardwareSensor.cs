using Iot.Device.Bmxx80;
using Iot.Device.Bmxx80.PowerMode;
using System;
using System.Collections.Generic;
using System.Device.I2c;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AgroPi.SAL.Sensors
{
    public class ThermometerHardwareSensor
    {

        // The I2C bus ID on the Raspberry Pi 3.
        private const int busId = 1;
        private I2cConnectionSettings settings;
        private I2cDevice device;

        public ThermometerHardwareSensor()
        {
            settings = new I2cConnectionSettings(busId, Bme680.SecondaryI2cAddress);
            device = I2cDevice.Create(settings);
        }

        public async Task<double> GetTemperature()
        {
            using (var bme680 = new Bme680(device))
            {
                // Prevents reading old data from the sensor's registers.
                bme680.Reset();

                bme680.SetHumiditySampling(Sampling.UltraLowPower);
                bme680.SetTemperatureSampling(Sampling.LowPower);
                bme680.SetPressureSampling(Sampling.UltraHighResolution);

                while (true)
                {
                    // Once a reading has been taken, the sensor goes back to sleep mode.
                    if (bme680.ReadPowerMode() == Bme680PowerMode.Sleep)
                    {
                        // This instructs the sensor to take a measurement.
                        bme680.SetPowerMode(Bme680PowerMode.Forced);
                    }

                    // This prevent us from reading old data from the sensor.
                    if (bme680.ReadHasNewData())
                    {
                        return Math.Round((await bme680.ReadTemperatureAsync()).Celsius, 2);
                    }
                }
            }
        }
        public async Task<double> GetHumidity()
        {
            using (var bme680 = new Bme680(device))
            {
                // Prevents reading old data from the sensor's registers.
                bme680.Reset();

                bme680.SetHumiditySampling(Sampling.UltraLowPower);
                bme680.SetTemperatureSampling(Sampling.LowPower);
                bme680.SetPressureSampling(Sampling.UltraHighResolution);

                while (true)
                {
                    // Once a reading has been taken, the sensor goes back to sleep mode.
                    if (bme680.ReadPowerMode() == Bme680PowerMode.Sleep)
                    {
                        // This instructs the sensor to take a measurement.
                        bme680.SetPowerMode(Bme680PowerMode.Forced);
                    }

                    // This prevent us from reading old data from the sensor.
                    if (bme680.ReadHasNewData())
                    {
                        return Math.Round(await bme680.ReadHumidityAsync(), 2);
                    }
                }
            }
        }
        public async Task<double> GetPressure()
        {
            using (var bme680 = new Bme680(device))
            {
                // Prevents reading old data from the sensor's registers.
                bme680.Reset();

                bme680.SetHumiditySampling(Sampling.UltraLowPower);
                bme680.SetTemperatureSampling(Sampling.LowPower);
                bme680.SetPressureSampling(Sampling.UltraHighResolution);

                while (true)
                {
                    // Once a reading has been taken, the sensor goes back to sleep mode.
                    if (bme680.ReadPowerMode() == Bme680PowerMode.Sleep)
                    {
                        // This instructs the sensor to take a measurement.
                        bme680.SetPowerMode(Bme680PowerMode.Forced);
                    }

                    // This prevent us from reading old data from the sensor.
                    if (bme680.ReadHasNewData())
                    {
                        return Math.Round(await bme680.ReadPressureAsync() / 100, 2);
                    }
                }
            }
        }
    }
}
