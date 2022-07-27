using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using WebApplication2.Services;

namespace WebApplication2.Controllers
{

    /// <summary>
    /// Робота з підпискою
    /// </summary>
    [ApiController]
    public class subscriptionController : Controller
    {
        private SubscriptionService subscriptionService { get; set; }

        public subscriptionController()
        {
            subscriptionService = new SubscriptionService();
        }

        /// <summary>
        /// Підписати емейл на отримання поточного курсу
        /// </summary>
        /// <remarks>Запит має перевірити, чи немає данної електронної адреси в поточній базі даних (файловій) і, в разі її відсутності, записувати її. Пізніше, за допомогою іншого запиту ми будемо відправляти лист на ті електронні адреси, які будуть в цій базі.</remarks>>
        ///<param name = "email">Електронна адреса, яку потрібно підписати</param >
        /// <response code="200">E-mail додано</response>
        /// <response code="409">Повертати, якщо e-mail вже є в базі даних (файловій)</response>
        [Route("/subscribe")]
        [HttpPost]
        public ActionResult Subscibe([BindRequired] string email)
        {
            try
            {
                subscriptionService.Subscribe(email);
                return Ok("E - mail додано");
            }
            catch (Exception e)
            {
                if (e.Message == "Email already exists")
                    return Conflict(new { message = $"E-mail '{email}' вже є в базі даних (файловій)" });
                return BadRequest(e.Message);
            }
        }
        /// <summary>
        /// Відправити e-mail з поточним курсом на всі підписані електронні пошти.
        /// </summary>
        /// <remarks>Запит має отримувати актуальний курс BTC до UAH за допомогою third-party сервісу та відправляти його на всі електронні адреси, які були підписані раніше.</remarks>>
        /// <response code="200">E-mailʼи відправлено</response>
        [Route("/sendEmails")]
        [HttpPost]
        public ActionResult SendEmails()
        {
            try
            {
                subscriptionService.SendEmails();
                return Ok("E - mailʼи відправлено");
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

    }
}
