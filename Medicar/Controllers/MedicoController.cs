using Application.Commands.Medicos;
using Application.Queries.Medicos;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace MedicarAPI.Controllers
{
    [ApiController]
    [Route("Controller/Medico")]
    public partial class MedicoController : ControllerBase
    {
        private readonly IMediator _mediator;

        public MedicoController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet(Name = "GetAllMedicos")]
        public async Task<IActionResult> GetAsync()
        {
            var response = await _mediator.Send(new GetAllMedicosQuery() { });
            return Ok(response);
        }

        [HttpPost(Name = "CreateMedico")]
        public async Task<IActionResult> PostAsync([FromBody] CreateMedicoRequest request)
        {
            var response = await _mediator.Send(new CreateMedicoCommand() { CRM = request.CRM, Email = request.Email, Nome = request.Nome });
            return Ok(response);
        }
    }
}
