using ColoradoLuxury.Attributes;
using ColoradoLuxury.Models.BLL;
using ColoradoLuxury.Models.DAL;
using Microsoft.AspNetCore.Mvc;

namespace ColoradoLuxury.Areas.WebCms.Controllers
{
    [Area("WebCms")]
    [Route("WebCms/[controller]/[action]")]
    public class ResultMessageController : AuthController
    {
        private readonly ColoradoContext _context;
        public ResultMessageController(ColoradoContext context)
        {
            _context= context;
        }
        public IActionResult Index()
        {
            var resultMessage = _context.ResultMessages.FirstOrDefault();
            return View(resultMessage);
        }

        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddOrUpdate(ResultMessage resultMessage)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction(nameof(Index));
            }

            if (!_context.ResultMessages.Any())
            {
                ResultMessage messages = new ResultMessage();
                messages.SuccessMessage = resultMessage.SuccessMessage;
                messages.FailMessage = resultMessage.FailMessage;

                _context.ResultMessages.Add(messages);
                _context.SaveChanges();
            }
            else
            {
                var previusMessage = _context.ResultMessages.FirstOrDefault();
                previusMessage.SuccessMessage = resultMessage.SuccessMessage;
                previusMessage.FailMessage = resultMessage.FailMessage;

                _context.ResultMessages.Update(previusMessage);
                _context.SaveChanges();
            }
            return View(nameof(Index));
        }
    }
}
