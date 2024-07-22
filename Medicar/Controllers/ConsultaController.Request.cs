namespace MedicarAPI.Controllers
{
    public partial class ConsultaController
    {
        public class ConsultaRequest
        {
            public int AgendaId { get; set; }
            public TimeOnly Horario { get; set; }
        }
    }
}
