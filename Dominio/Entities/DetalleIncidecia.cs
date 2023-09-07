using Dominio;

namespace Entities;

public class DetalleIncidencia : BaseEntity
{
        public int Id_Incidence { get; set; }
        public Incidencia ? Incidencia { get; set; }

        public int Id_Peripheral { get; set; }
        public ICollection<Perisferico> ?Perisfericos { get; set; }

        public int Id_TypeIncidence { get; set; }
        public TipoIncidencia ? TipoIncidencia { get; set; }

        public int Id_LevelIncidence { get; set; }
        public NivelIncidencia ? NivelIncidencia { get; set; }

        public int Id_State { get; set; }
        public Estado ? Estado { get; set; }

        public string ?Description_DetailIncidence { get; set; }
}