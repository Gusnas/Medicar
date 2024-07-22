namespace MedicarAPI.Controllers
{
    public partial class MedicoController
    {
        public class CreateMedicoRequest()
        {
            public string Nome { get; set; }
            public string CRM { get; set; }
            public string Email { get; set; }
        }
    }
}
