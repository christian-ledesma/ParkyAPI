using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ParkyWeb.Models;
using ParkyWeb.Repository.Interfaces;
using System.IO;
using System.Threading.Tasks;

namespace ParkyWeb.Controllers
{
    [Authorize]
    public class NationalParksController : Controller
    {
        private readonly INationalParkRepository _parkRepository;
        public NationalParksController(INationalParkRepository parkRepository)
        {
            _parkRepository = parkRepository;
        }
        public IActionResult Index()
        {
            return View(new NationalPark());
        }
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Upsert(int? id)
        {
            NationalPark nationalPark = new NationalPark();
            if (id == null)
                return View(nationalPark);
            nationalPark = await _parkRepository.GetById(StaticDetails.NationalParkApiPath, id.GetValueOrDefault(), HttpContext.Session.GetString("JWToken"));
            if (nationalPark == null)
                return NotFound();
            return View(nationalPark);
        }

        public async Task<IActionResult> GetAllNationalParks()
        {
            return Json(new
            {
                data = await _parkRepository.GetAll(StaticDetails.NationalParkApiPath, HttpContext.Session.GetString("JWToken"))
            });
        }

        [HttpDelete]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var status = await _parkRepository.Delete(StaticDetails.NationalParkApiPath, id, HttpContext.Session.GetString("JWToken"));
            if (status)
            {
                return Json(new
                {
                    success = true,
                    message = "Delete succesful"
                });
            }
            return Json(new
            {
                success = true,
                message = "Delete not succesful"
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(NationalPark nationalPark)
        {
            if (ModelState.IsValid)
            {
                var files = HttpContext.Request.Form.Files;
                if (files.Count > 0)
                {
                    byte[] p1 = null;
                    using (var fs1 = files[0].OpenReadStream())
                    {
                        using (var ms1 = new MemoryStream())
                        {
                            fs1.CopyTo(ms1);
                            p1 = ms1.ToArray();
                        }
                    }
                    nationalPark.Picture = p1;
                }
                else
                {
                    var nationalParkFromDb = await _parkRepository.GetById(StaticDetails.NationalParkApiPath, nationalPark.Id, HttpContext.Session.GetString("JWToken"));
                    nationalPark.Picture = nationalParkFromDb.Picture;
                }
                if(nationalPark.Id == 0)
                {
                    await _parkRepository.Create(StaticDetails.NationalParkApiPath, nationalPark, HttpContext.Session.GetString("JWToken"));
                }
                else
                {
                    await _parkRepository.Update(StaticDetails.NationalParkApiPath + nationalPark.Id, nationalPark, HttpContext.Session.GetString("JWToken"));
                }
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View(nationalPark);
            }
        }
    }
}
