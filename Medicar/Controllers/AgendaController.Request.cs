namespace MedicarAPI.Controllers
{
    public partial class AgendaController
    {
        public class CreateAgendaRequest
        {
            public int MedicoId { get; set; }
            public DateTime Dia { get; set; }
            public List<DateTime> Horarios { get; set; }
        }
        public class GetAgendaRequest
        {
            public List<int> MedicoIds { get; set; }
            public List<string> CRMs { get; set; }
            public DateOnly InitialDate { get; set; }
            public DateOnly FinalDate { get; set; }
        }
    }
}
