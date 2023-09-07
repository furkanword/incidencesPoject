namespace Api.Dtos;

    public class IncidenciaDto
    {
        public int Id {get;set;}
        public int Codigo { get; set; }
        public int EstadoDeLaIncidencia { get; set;}
        public int Area_Incidencia { get; set; }
        public int Lugar_Incidencia { get; set;}
        public string ? Descripcion_Incidencia { get; set; }
        public string ? DetalleDeLaIncidencia { get; set; }
    }
