namespace Api.Dtos
{
    public class DireccionDto
    {
        public string ? Neigborhood { get; set; }
        public string ? TypeWay  {get; set;}
        public string ? QuadrantPrefix {get; set;}
        public string ?  NumberWay {get; set;}
        public string? NumberVenereableWay {get; set;}
        public string? NumberPlate {get; set;}
        public int Id_Pa {get; set;}
        public int Id_CityA {get; set;}
    }
}