using RealEstate.ApplicationServices.Services;
using RealEstate.Core.Dtos;
using RealEstate.Core.ServiceInterface;
using System;
using System.Threading.Tasks;
using Xunit;

namespace House.HouseTest
{
    public class HouseTest : TestBase
    {
        [Fact]
        public async Task Should_AddNewHouse_WhenReturnResult()
        {
            string guid = "a1925975-d8fc-4f55-b614-d9b5aa7b4ebe";

            HouseDto house = new HouseDto();

            house.Id = Guid.Parse(guid);
            house.Name = "Harkuranna torn";
            house.Type = "Building";
            house.SquareMeters = 55.4f;
            house.Rooms = 3;
            house.Price = 350000;

            var result = await Svc<IHouseService>().Add(house);

            Assert.NotNull(result);
        }
        [Fact]
        public async Task GetByIdHouse_WhenReturnsResultAsync()
        {
            string guid = "e6771076-91cd-4169-bbdd-cfc5290a6b3f";
            string guid1 = "1ab8c12a-f8da-4e55-ab77-f45378d3adb5";

            var insertGuid = Guid.Parse(guid);
            var insertGuid1 = Guid.Parse(guid1);

            await Svc<IHouseService>().GetAsync(insertGuid);

            Assert.Equal(insertGuid1, insertGuid);
        }

        [Fact]
        public async Task DeleteByIdHouse_WhenDeleteSpaceship()
        {
            string guid = "e6771076-91cd-4169-bbdd-cfc5290a6b3f";

            var insertGuid = Guid.Parse(guid);

            var result = await Svc<IHouseService>().Delete(insertGuid);

            Assert.NotEmpty((System.Collections.IEnumerable)result);
            Assert.Single((System.Collections.IEnumerable)result);
        }

        public async Task UpdateByIdHouse_WhenUpdateSpaceship()
        {
            string guid = "1ab8c12a-f8da-4e55-ab77-f45378d3adb5";

            HouseDto house = new HouseDto();

            house.Id = Guid.Parse(guid);
            house.Name = "Pikaliiva kaarmaja";
            house.Type = "Townhouse";
            house.SquareMeters = 75.4f;
            house.Rooms = 4;
            house.Price = 250000;

            await Svc<IHouseService>().Update(house);

            Assert.NotEmpty((System.Collections.IEnumerable)house);
        }

    }
}
