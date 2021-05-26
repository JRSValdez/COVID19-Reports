
namespace COVID19_API.DTOs.COVID19_API.TopTen { 

    public class TopTenDto
    {
        //public string date { get; set; }
        public int confirmed { get; set; }
        public int deaths { get; set; }
        public RegionDto region { get; set; }
    }

}