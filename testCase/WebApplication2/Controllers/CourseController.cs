using Microsoft.AspNetCore.Mvc;
using System;
using WebApplication2.IServices;
using WebApplication2.Services;

namespace WebApplication2.Controllers
{

    /// <summary>
    /// Отримання поточного курсу BTC до UAH
    /// </summary>
    [ApiController]
    public class rateController : ControllerBase
    {
        private ICourseService courseService { get; set; }

        public rateController()
        {
            courseService = new CourseService();
        }
        /// <summary>
        /// Отримати поточний курс BTC до UAH
        /// </summary>
        /// <remarks>Запит має повертати поточний курс BTC до UAH використовуючи будь-який third party сервіс з публічним АРІ</remarks>>
        /// <response code="200">Повертається актуальний курс BTC до UAH</response>
        /// <response code="400">Invalid status value</response>
        [Route("/rate")]
        [HttpGet]
        [Produces("application/json", Type = typeof(int))]
        public ActionResult Get()
        {
            try
            {
                return Ok(courseService.GetBitcoinToUAH());
            }
            catch (Exception)
            {
                return BadRequest("Invalid status value");
            }
        }

    }
}
