using Application.Commands.Consultas;
using Application.Queries.Consultas;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace MedicarAPI.Controllers
{
    [ApiController]
    [Route("Controller/Consulta")]
    public partial class ConsultaController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ConsultaController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet(Name = "GetAllConsultas")]
        public async Task<IActionResult> GetAsync()
        {
            var response = await _mediator.Send(new GetAllConsultasQuery() { });
            return Ok(response);
        }

        [HttpPost(Name = "MarcarConsulta")]
        public async Task<IActionResult> PostAsync([FromBody] ConsultaRequest request)
        {
            var response = await _mediator.Send(new CreateConsultaCommand() { AgendaId = request.AgendaId, Horario = TimeOnly.Parse(request.Horario)});
            return Ok(response);
        }

        [HttpDelete(Name = "DesmarcarConsulta")]
        public async Task<IActionResult> DeleteAsync(int consultaId)
        {
            var response = await _mediator.Send(new DeleteConsultaCommand() { ConsultaId = consultaId });
            return Ok(response);
        }
    }
}
