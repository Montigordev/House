using System;
using System.Threading.Tasks;
using RealEstate.Core.Domain;
using RealEstate.Core.Dtos;

namespace RealEstate.Core.ServiceInterface
{
    public interface IHouseService : IApplicationService
    {
        Task<House> Delete(Guid id);

        Task<House> Add(HouseDto dto);

        Task<House> Edit(Guid id);

        Task<House> Update(HouseDto dto);
        Task GetAsync(Guid insertGuid);
    }
}
