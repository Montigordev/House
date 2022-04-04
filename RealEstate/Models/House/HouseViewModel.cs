using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using RealEstate.Models.Files;

namespace RealEstate.Models.House
{
    public class HouseViewModel
    {
        public Guid? Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public double Price { get; set; }
        public float SquareMeters { get; set; }
        public int Rooms { get; set; }
        public List<IFormFile> Files { get; set; }

        public List<ExistingFilePathViewModel> ExistingFilePaths { get; set; } = new List<ExistingFilePathViewModel>();
    }
}
