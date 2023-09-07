namespace Api.Dtos
{
    public class IncidenciaxEstado
    {
        public int Id {get;set;}
        public string ?Description_State { get; set; }
        public List<EstadoDto> ? Estados {get;set;}
    }
}