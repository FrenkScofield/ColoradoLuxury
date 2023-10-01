using ColoradoLuxury.Extensions;
using ColoradoLuxury.Models.BLL;
using ColoradoLuxury.Models.DAL;
using Microsoft.AspNetCore.Mvc;

namespace ColoradoLuxury.Areas.WebCms.Controllers
{
    [Area("WebCms")]
    [Route("WebCms/[controller]/[action]")]
    public class ForApiKeyController : Controller
    {
        private readonly ColoradoContext _context;
        public ForApiKeyController(ColoradoContext context)
        {
            _context= context;
        }
        public IActionResult Index()
        {
            var apiSettings = _context.ApiSettingsDetails.FirstOrDefault();

            return View(apiSettings);
        }

        [HttpPost]
        public IActionResult AddApiSettingsDetails(ApiSettingsDetail model)
        {
            if (!ModelState.IsValid)
            {
                foreach (var item in ModelState.ToList())
                {
                    var t = item.Value.Errors;
                    foreach (var error in item.Value.Errors.ToList())
                    {
                        ModelState.AddModelError(item.Key, error.ErrorMessage);
                    }
                }

                return View(nameof(Index));
            }

            if (!_context.ApiSettingsDetails.Any())
            {
                ApiSettingsDetail apiSettingsDetail = new ApiSettingsDetail
                {
                    Secretkey = model.Secretkey,
                    Publickey= model.Publickey
                };

                _context.ApiSettingsDetails.Add(apiSettingsDetail);
                _context.SaveChanges();
            }
            else
            {
                var apiSettingsDetail = _context.ApiSettingsDetails.FirstOrDefault();

                apiSettingsDetail.Secretkey = model.Secretkey;
                apiSettingsDetail.Publickey = model.Publickey;
                apiSettingsDetail.EditDate = DateTime.Now;


                _context.ApiSettingsDetails.Update(apiSettingsDetail);
                _context.SaveChanges();
            }


            return View(nameof(Index));
        }
    }
}
