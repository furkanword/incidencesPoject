namespace Api.Dtos;

    public class PersonaxIncidenciaDto
    {
        public string ? Nombre {get; set; }
        public string ? Apellido {get; set; }
        public int SuIdDeDocumento {get; set; }
        public List <IncidenciaDto> ? Incidencias {get; set;}
    }
