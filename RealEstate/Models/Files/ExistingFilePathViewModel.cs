using System;


namespace RealEstate.Models.Files
{
    public class ExistingFilePathViewModel
    {
        public Guid PhotoId { get; set; }
        public string FilePath { get; set; }
        public Guid HouseId { get; set; }
    }
}
