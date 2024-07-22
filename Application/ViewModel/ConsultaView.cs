using Domain.Entities;

namespace Application.ViewModel
{
    public class ConsultaView
    {
        public int ConsultaId { get; set; }
        public DateOnly Dia { get; set; }
        public TimeOnly Horario { get; set; }
        public DateTime DataAgendamento { get; set; }
        public Medico Medico { get; set; }
    }
}
