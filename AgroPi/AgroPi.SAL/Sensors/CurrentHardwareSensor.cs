using AgroPi.SAL.Sensors.IN219;
using System;
using System.Collections.Generic;
using System.Device.I2c;
using System.Text;

namespace AgroPi.SAL.Sensors
{
    public class CurrentHardwareSensor
    {
        private const byte Adafruit_Ina219_I2cAddress = 0x40;
        private const byte Adafruit_Ina219_I2cBus = 0x1;

        private I2cConnectionSettings settings;
        private INA219Sensor device;

        public CurrentHardwareSensor()
        {
            settings = new I2cConnectionSettings(Adafruit_Ina219_I2cBus, Adafruit_Ina219_I2cAddress);
            device = new INA219Sensor(settings);
        }

        /// <summary>
        /// Get Bus Voltage in V
        /// </summary>
        /// <returns></returns>
        public float GetBusVoltage()
        {
            using (INA219Sensor sensor = device)
            {
                // reset the device 
                device.Reset();

                // set up the bus and shunt voltage ranges and the calibration. Other values left at default.
                device.BusVoltageRange = Ina219BusVoltageRange.Range16v;
                device.PgaSensitivity = Ina219PgaSensitivity.PlusOrMinus40mv;
                device.SetCalibration(33574, (float)12.2e-6);

                return sensor.ReadBusVoltage();
            }
        }

        /// <summary>
        /// Get Shung Voltage in mV
        /// </summary>
        /// <returns></returns>
        public float GetShuntVoltage()
        {
            using (INA219Sensor sensor = device)
            {
                // reset the device 
                device.Reset();

                // set up the bus and shunt voltage ranges and the calibration. Other values left at default.
                device.BusVoltageRange = Ina219BusVoltageRange.Range32v;
                device.PgaSensitivity = Ina219PgaSensitivity.PlusOrMinus320mv;
                device.SetCalibration(33574, (float)12.2e-6);

                return sensor.ReadShuntVoltage() * 1000;
            }
        }

        /// <summary>
        /// Get Current in mA
        /// </summary>
        /// <returns></returns>
        public float GetCurrent()
        {
            using (INA219Sensor sensor = device)
            {
                // reset the device 
                device.Reset();

                // set up the bus and shunt voltage ranges and the calibration. Other values left at default.
                device.BusVoltageRange = Ina219BusVoltageRange.Range32v;
                device.PgaSensitivity = Ina219PgaSensitivity.PlusOrMinus320mv;
                device.SetCalibration(33574, (float)12.2e-6);

                return sensor.ReadCurrent() * 1000;
            }
        }

        /// <summary>
        /// Get Power Consumption in mW
        /// </summary>
        /// <returns></returns>
        public float GetPower()
        {
            using (INA219Sensor sensor = device)
            {
                // reset the device 
                device.Reset();

                // set up the bus and shunt voltage ranges and the calibration. Other values left at default.
                device.BusVoltageRange = Ina219BusVoltageRange.Range32v;
                device.PgaSensitivity = Ina219PgaSensitivity.PlusOrMinus320mv;
                device.SetCalibration(33574, (float)12.2e-6);

                return sensor.ReadPower() * 1000;
            }
        }
    }
}
