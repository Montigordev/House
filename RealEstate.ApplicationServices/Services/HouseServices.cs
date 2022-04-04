using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using RealEstate.Core.Domain;
using RealEstate.Core.Dtos;
using RealEstate.Core.ServiceInterface;
using RealEstate.Data;
using System.Linq;

namespace RealEstate.ApplicationServices.Services
{
    public class HouseServices : IHouseService
    {
        private readonly RealEstateDbContext _context;

        public HouseServices
            (
                RealEstateDbContext context
            )
        {
            _context = context;
        }

        public async Task<House> Delete(Guid id)
        {
            var photos = await _context.ExistingFilePath
                .Where(x => x.HouseId == id)
                .Select(y => new ExistingFilePathDto
                {
                    HouseId = y.HouseId,
                    FilePath = y.FilePath,
                    PhotoId = y.Id
                })
                .ToArrayAsync();


            var autoId = await _context.House
                .Include(x => x.ExistingFilePaths)
                .FirstOrDefaultAsync(x => x.Id == id);

            _context.House.Remove(autoId);
            await _context.SaveChangesAsync();

            return autoId;
        }

        public async Task<House> Add(HouseDto dto)
        {
            House house = new House();

            house.Id = Guid.NewGuid();
            house.Type = dto.Type;
            house.Name = dto.Name;
            house.SquareMeters = dto.SquareMeters;
            house.Rooms = dto.Rooms;
            house.Price = dto.Price;

            await _context.House.AddAsync(house);
            await _context.SaveChangesAsync();

            return house;
        }


        public async Task<House> Edit(Guid id)
        {
            var result = await _context.House
                .FirstOrDefaultAsync(x => x.Id == id);

            return result;
        }

        public async Task<House> Update(HouseDto dto)
        {
            House house = new House();

            house.Id = dto.Id;
            house.Type = dto.Type;
            house.Name = dto.Name;
            house.SquareMeters = dto.SquareMeters;
            house.Rooms = dto.Rooms;
            house.Price = dto.Price;

            _context.House.Update(house);
            await _context.SaveChangesAsync();

            return house;
        }

        public Task GetAsync(Guid insertGuid)
        {
            throw new NotImplementedException();
        }
    }
}