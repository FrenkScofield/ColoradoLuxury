using ColoradoLuxury.Areas.WebCms.VM;
using ColoradoLuxury.Attributes;
using ColoradoLuxury.Models.BLL;
using ColoradoLuxury.Models.DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ColoradoLuxury.Areas.WebCms.Controllers
{
    [Area("WebCms")]
    [Route("WebCms/[controller]/[action]")]
    public class CalculationMethotController :  AuthController
    {
        private readonly ColoradoContext _context;

        public CalculationMethotController(ColoradoContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
           
                ValueOfTipBttonsVM? model = null;
                var lastData = _context.ValueOfTipButtons.OrderBy(x => x.Id).LastOrDefault();
                if (lastData != null)
                {
                    model = new ValueOfTipBttonsVM();
                    model.Id = lastData.Id;
                    model.lowInterest = lastData.lowInterest;
                    model.MiddleInterest = lastData.MiddleInterest;

                    model.HighInterest = lastData.HighInterest;
                }
                return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> CreateValueOfTipButtons(ValueOfTipBttonsVM valueOfTip)
        {

            ValueOfTipButton valueOfTipButton = new ValueOfTipButton();
            valueOfTipButton.lowInterest = valueOfTip.lowInterest;
            valueOfTipButton.MiddleInterest = valueOfTip.MiddleInterest;
            valueOfTipButton.HighInterest = valueOfTip.HighInterest;

            _context.ValueOfTipButtons.Add(valueOfTipButton);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
