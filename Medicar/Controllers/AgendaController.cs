using Application.Commands.Agendas;
using Application.Queries.Agendas;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace MedicarAPI.Controllers
{
    [ApiController]
    [Route("Controller/Agenda")]
    public partial class AgendaController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AgendaController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost(Name = "CreateAgenda")]
        public async Task<IActionResult> PostAsync([FromBody] CreateAgendaRequest request)
        {
            // validar se já existe agenda para o medico naquele dia
            // validar se data é maior ou igual a hoje
            var response = await _mediator.Send(new CreateAgendaCommand { MedicoId = request.MedicoId, Dia = request.Dia, Horarios = request.Horarios });
            return Ok(response);
        }

        [HttpGet(Name = "GetAgenda")]
        public async Task<IActionResult> GetAsync(GetAgendaRequest request)
        {
            var response = await _mediator.Send(new GetAgendaQuery {MedicoIds = request.MedicoIds, CRMs = request.CRMs, InitialDate = request.InitialDate, FinalDate = request.FinalDate });
            return Ok(response);
        }
    }
}
