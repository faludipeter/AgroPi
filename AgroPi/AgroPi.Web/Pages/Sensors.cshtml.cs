using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AgroPi.BLL.Services;
using BLL.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AgroPi.Web.Pages
{
    [Authorize(Roles = Dal.Users.Roles.Administrators)]
    public class SensorsModel : PageModel
    {

        public SensorsService SensorsService { get; set; }
        public RTCHeader RTCHeader { get; set; }
        public ThermometersHeader ThermometersHeader { get; set; }
        public LuxmeterHeader LuxmeterHeader { get; set; }

        public CurrentSensorHeader CurrentSensorHeader { get; set; }

        public SensorsModel(SensorsService sensorsService)
        {
            SensorsService = sensorsService;
        }

        public void OnGet()
        {
            RTCHeader = SensorsService.GetRTCHeader();
            ThermometersHeader = SensorsService.GetThermometersHeader();
            LuxmeterHeader = SensorsService.GetLuxmeterHeader();
            CurrentSensorHeader = SensorsService.GetCurrentSensorHeader();
        }
    }
}