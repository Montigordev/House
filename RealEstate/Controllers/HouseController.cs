using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using RealEstate.Core.Dtos;
using RealEstate.Core.ServiceInterface;
using RealEstate.Data;
using RealEstate.Models.Files;
using RealEstate.Models.House;

namespace RealEstate.Controllers
{
    public class HouseController : Controller
    {
        private readonly RealEstateDbContext _context;
        private readonly IHouseService _houseService;

        public HouseController
            (
                RealEstateDbContext context,
                IHouseService houseService
            )
        {
            _context = context;
            _houseService = houseService;
        }


        public IActionResult Index()
        {
            var result = _context.House
                .Select(x => new HouseListViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Price = x.Price,
                    SquareMeters = x.SquareMeters,
                    Rooms = x.Rooms,
                    Type = x.Type
                });

            return View(result);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            var house = await _houseService.Delete(id);

            if (house == null)
            {
                RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Add()
        {
            HouseViewModel model = new HouseViewModel();

            return View("Edit", model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(HouseViewModel model)
        {
            var dto = new HouseDto()
            {
                Id = model.Id,
                Type = model.Type,
                Name = model.Name,
                SquareMeters = model.SquareMeters,
                Rooms = model.Rooms,
                Price = model.Price,
                Files = model.Files,
                ExistingFilePaths = model.ExistingFilePaths
                    .Select(x => new ExistingFilePathDto
                    {
                        PhotoId = x.PhotoId,
                        FilePath = x.FilePath,
                        HouseId = x.HouseId
                    }).ToArray()
            };

            var result = await _houseService.Add(dto);
            if (result == null)
            {
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var house = await _houseService.Edit(id);
            if (house == null)
            {
                return NotFound();
            }

            var photos = await _context.ExistingFilePath
                .Where(x => x.HouseId == id)
                .Select(y => new ExistingFilePathViewModel
                {
                    FilePath = y.FilePath,
                    PhotoId = y.Id
                })
                .ToArrayAsync();


            var model = new HouseViewModel();

            model.Id = house.Id;
            model.Type = house.Type;
            model.Name = house.Name;
            model.SquareMeters = house.SquareMeters;
            model.Rooms = house.Rooms;
            model.Price = house.Price;
            model.ExistingFilePaths.AddRange(photos);

            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> Update(HouseViewModel model)
        {
            var dto = new HouseDto()
            {
                Id = model.Id,
                Type = model.Type,
                Name = model.Name,
                SquareMeters = model.SquareMeters,
                Rooms = model.Rooms,
                Price = model.Price,
                Files = model.Files,
                ExistingFilePaths = model.ExistingFilePaths
                    .Select(x => new ExistingFilePathDto
                    {
                        PhotoId = x.PhotoId,
                        FilePath = x.FilePath,
                        HouseId = x.HouseId
                    }).ToArray()
            };

            var result = await _houseService.Update(dto);

            if (result == null)
            {
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index), model);
        }
    }
}
