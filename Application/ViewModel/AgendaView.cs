using Domain.Entities;

namespace Application.ViewModel
{
    public class AgendaView
    {
        public int AgendaId { get; set; }
        public Medico Medico { get; set; }
        public DateOnly Dia { get; set; }
        public List<TimeOnly> Horarios { get; set; }
    }
}
