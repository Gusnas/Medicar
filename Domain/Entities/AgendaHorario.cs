using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    [Table("AgendaHorario")]
    public class AgendaHorario
    {
        [Key]
        public int Id { get; set; }
        public int AgendaId { get; set; }
        public TimeOnly Horario { get; set; }
        public bool Disponivel { get; set; }
    }
}
