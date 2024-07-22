using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    [Table("Consulta")]
    public class Consulta
    {
        [Key]
        public int Id { get; set; }
        public DateOnly Dia { get; set; }
        public TimeOnly Horario { get; set; }
        public DateTime DataAgendamento { get; set; }
        [ForeignKey("Medico")]
        public int MedicoId { get; set; }
    }
}
