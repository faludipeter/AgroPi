using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AgroPi.BLL.Services;
using BLL.Dtos;
using Microsoft.AspNetCore.SignalR;

namespace AgroPi.Web.Hubs
{
    public class SensorsHub: Hub
    {
        public SensorsService SensorsService { get; set; }
        public RTCHeader RTCHeader { get; set; }
        public ThermometersHeader ThermometersHeader { get; set; }
        public LuxmeterHeader LuxmeterHeader { get; set; }
        public CurrentSensorHeader CurrentSensorHeader { get; set; }

        public SensorsHub(SensorsService sensorsService)
        {
            SensorsService = sensorsService;
        }

        public async Task GetRefresh()
        {
            RTCHeader = SensorsService.GetRTCHeader();
            ThermometersHeader = SensorsService.GetThermometersHeader();
            LuxmeterHeader = SensorsService.GetLuxmeterHeader();
            CurrentSensorHeader = SensorsService.GetCurrentSensorHeader();
            await Clients.All.SendAsync("ReceiveMessage", RTCHeader, ThermometersHeader, LuxmeterHeader, CurrentSensorHeader);
        }
    }
}
