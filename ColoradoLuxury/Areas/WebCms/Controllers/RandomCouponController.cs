using ColoradoLuxury.Models.BLL;
using ColoradoLuxury.Models.DAL;
using ColoradoLuxury.Models.VM;
using Microsoft.AspNetCore.Mvc;

namespace ColoradoLuxury.Areas.WebCms.Controllers
{
    [Area("WebCms")]
    [Route("WebCms/[controller]/[action]")]
    public class RandomCouponController : Controller
    {
        private readonly ColoradoContext _context;
        public RandomCouponController(ColoradoContext context)
        {
            _context= context;
        }
        public IActionResult Coupon()
        {
            var cupon = _context.Cupons.Select(c => new CuponVM
            {
                Id= c.Id,
                NewCupon = c.NewCupon,
                CuponCode = c.CuponCode,
                Percentage= c.Percentage,
                Amount= c.Amount,

                Status= c.Status,
                CouponDeatline = c.CouponDeatline

            }).ToList();
            return View(cupon);
        }

        [HttpPost]
        public IActionResult AddCoupon([FromBody]CuponVM model)
        {
            if (model == null)
            {
                return Json(new
                {
                    modelIsEmpty= true,

                });
            }

            if (model.Percentage <= 0 && model.Amount <= 0)
            {
                return Json(new
                {
                    discount = true,

                });
            }

            Cupon cupon = new Cupon()
            {
                Amount = model.Amount,
                Percentage = model.Percentage,
                NewCupon = model.NewCupon.ToUpper(),
                CuponCode = model.CuponCode.ToUpper(),

                Status = model.Status,
                CouponDeatline = model.CouponDeatline
            };

            _context.Cupons.Add(cupon);
            _context.SaveChanges();

            return Json(new
            {
                success = true,

            });
        }


        public async Task<IActionResult> DetailShow(int? Id)
        {
            if (Id == null)
            {
                return NotFound();
            }

            var cupon = await _context.Cupons.FindAsync(Id);

            if (cupon == null)
            {
                return NotFound();
            }
            return View(cupon);
        }
    }
}
