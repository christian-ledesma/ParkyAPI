using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ParkyWeb.Models;
using ParkyWeb.Models.ViewModel;
using ParkyWeb.Repository.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParkyWeb.Controllers
{
    [Authorize]
    public class TrailsController : Controller
    {
        private readonly INationalParkRepository _parkRepository;
        private readonly ITrailRepository _trailRepository;
        public TrailsController(INationalParkRepository parkRepository, ITrailRepository trailRepository)
        {
            _parkRepository = parkRepository;
            _trailRepository = trailRepository;
        }
        public IActionResult Index()
        {
            return View(new Trail());
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Upsert(int? id)
        {
            IEnumerable<NationalPark> nationalParks = await _parkRepository.GetAll(StaticDetails.NationalParkApiPath, HttpContext.Session.GetString("JWToken"));
            var trailsViewModel = new TrailsViewModel
            {
                NationalParkList = nationalParks.Select(n => new SelectListItem
                {
                    Text = n.Name,
                    Value = n.Id.ToString()
                }),
                Trail = new Trail()
            };

            if (id == null)
                return View(trailsViewModel);
            trailsViewModel.Trail = await _trailRepository.GetById(StaticDetails.TrailApiPath, id.GetValueOrDefault(), HttpContext.Session.GetString("JWToken"));
            if (trailsViewModel.Trail == null)
                return NotFound();
            return View(trailsViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(TrailsViewModel trailViewModel)
        {
            if (ModelState.IsValid)
            {
                if (trailViewModel.Trail.Id == 0)
                {
                    await _trailRepository.Create(StaticDetails.TrailApiPath, trailViewModel.Trail, HttpContext.Session.GetString("JWToken"));
                }
                else
                {
                    await _trailRepository.Update(StaticDetails.TrailApiPath + trailViewModel.Trail.Id, trailViewModel.Trail, HttpContext.Session.GetString("JWToken"));
                }
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View(trailViewModel);
            }
        }

        public async Task<IActionResult> GetTrails()
        {
            return Json(new
            {
                data = await _trailRepository.GetAll(StaticDetails.TrailApiPath, HttpContext.Session.GetString("JWToken"))
            });
        }

        [HttpDelete]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var status = await _trailRepository.Delete(StaticDetails.TrailApiPath, id, HttpContext.Session.GetString("JWToken"));
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
    }
}
