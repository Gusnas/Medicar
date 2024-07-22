using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    [Table("Agenda")]
    public class Agenda
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("Medico")]
        public int MedicoId { get; set; }
        public DateOnly Dia { get; set; }
    }
}
