using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AgroPi.BLL.Services;
using Microsoft.AspNetCore.Mvc;

namespace AgroPi.Web.Pages.api
{
    [Route("api/[controller]")]
    [ApiController]
    [IgnoreAntiforgeryToken(Order = 1001)]
    public class SensorDataController : Controller
    {
        private SensorsService SensorsService { get; }
        public SensorDataController(SensorsService sensorsService)
        {
            SensorsService = sensorsService;
        }

        public JsonResult Get()
        {
            return new JsonResult(SensorsService.GetSensorDataHeader());
        }
    }
}