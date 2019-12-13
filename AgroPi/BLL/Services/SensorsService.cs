using AgroPi.BLL.Dtos;
using AgroPi.Dal;
using AgroPi.Dal.Entities;
using AgroPi.SAL.Sensors;
using BLL.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace AgroPi.BLL.Services
{
    public class SensorsService
    {
        private AgroPiDbContext DbContext { get; }

        public SensorsService(AgroPiDbContext dbContext)
        {
            DbContext = dbContext;
        }

        /// <summary>
        /// SensorData -> SensorDataHeader
        /// </summary>
        public Expression<Func<SensorData, SensorDataHeader>> SensorDataHeaderSelector { get; } = h => new SensorDataHeader 
        {
            Id = h.Id,
            Temperature = h.Temperature ?? -9999d,
            ShuntVoltage = h.ShuntVoltage ?? -9999f,
            BusVoltage = h.BusVoltage ?? -9999f,
            Current = h.Current ?? -9999f,
            DateTime = h.DateTime,
            Humidity = h.Humidity ?? -9999d,
            Lux = h.Lux ?? -9999f,
            Power = h.Power ?? -9999f,
            Pressure = h.Pressure ?? -9999d
        };

        public RTCHeader GetRTCHeader()
        {
            RTCHardwareSensor rtc = new RTCHardwareSensor();

            return new RTCHeader
            {
                DateTime = rtc.GetDateTime()
            };
        }

        public LuxmeterHeader GetLuxmeterHeader()
        {
            LuxmeterHardwareSensor lhs = new LuxmeterHardwareSensor();

            return new LuxmeterHeader
            {
                DateTime = DateTime.Now,
                Lux = lhs.GetLux()
            };
        }
        public CurrentSensorHeader GetCurrentSensorHeader()
        {
            CurrentHardwareSensor chs = new CurrentHardwareSensor();

            return new CurrentSensorHeader()
            {
                DateTime = DateTime.Now,
                Current = chs.GetCurrent(),
                ShuntVoltage = chs.GetShuntVoltage(),
                BusVoltage = chs.GetBusVoltage(),
                Power = chs.GetPower()
            };
        }

        public ThermometersHeader GetThermometersHeader()
        {
            ThermometerHardwareSensor ths = new ThermometerHardwareSensor();
            return new ThermometersHeader
            {
                DateTime = DateTime.Now,
                Temperature = ths.GetTemperature().GetAwaiter().GetResult(),
                Humidity = ths.GetHumidity().GetAwaiter().GetResult(),
                Pressure = ths.GetPressure().GetAwaiter().GetResult()
            };
        }
        
        public bool SaveAllSensorData()
        {
            SensorData sd = new SensorData
            {
                BusVoltage = GetCurrentSensorHeader().BusVoltage,
                Current = GetCurrentSensorHeader().Current,
                DateTime = GetRTCHeader().DateTime,
                Humidity = GetThermometersHeader().Humidity,
                Lux = GetLuxmeterHeader().Lux,
                Power = GetCurrentSensorHeader().Power,
                Pressure = GetThermometersHeader().Pressure,
                ShuntVoltage = GetCurrentSensorHeader().ShuntVoltage,
                Temperature = GetThermometersHeader().Temperature
            };

            DbContext.Add(sd);
            
            try
            {
                var dbresult = DbContext.SaveChanges();

                if (dbresult != 0)
                {
                    return true;
                }

                return false;
               
            }
            catch (Exception)
            {

                return false;
            }
        }

        public SensorDataHeader GetSensorDataHeader() => DbContext.SensorDatas.OrderByDescending(s => s.DateTime).Select(SensorDataHeaderSelector).FirstOrDefault();

        public IEnumerable<SensorDataHeader> GetSensorAllDataHeader() => DbContext.SensorDatas.Select(SensorDataHeaderSelector);

    }
}
