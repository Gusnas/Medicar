using Application.Contracts;
using Application.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Commands.Consultas
{
    public class DeleteConsultaCommand : IRequest<IGenericResponse>
    {
        public int ConsultaId { get; set; }
    }
    public class DeleteConsultaCommandHandler : IRequestHandler<DeleteConsultaCommand, IGenericResponse>
    {
        private readonly IMediator _mediator;
        private readonly IRepository<Consulta> _consultaRepository;
        private readonly IRepository<AgendaHorario> _agendaHorarioRepository;

        public DeleteConsultaCommandHandler(IMediator mediator, IRepository<Consulta> consultaRepository, IRepository<AgendaHorario> agendaHorarioRepository)
        {
            _mediator = mediator;
            _consultaRepository = consultaRepository;
            _agendaHorarioRepository = agendaHorarioRepository;
        }
        public async Task<IGenericResponse> Handle(DeleteConsultaCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var consulta = _consultaRepository.GetByID(request.ConsultaId);
                if (consulta == null)
                    return await Task.FromResult(new FailureResponse("Consulta inexistente!"));

                if (consulta.Dia <= DateOnly.FromDateTime(DateTime.UtcNow) && consulta.Horario < TimeOnly.FromDateTime(DateTime.UtcNow))
                    return await Task.FromResult(new FailureResponse("Consulta já aconteceu!"));

                _consultaRepository.Delete(request.ConsultaId);
                _consultaRepository.Save();

                var agendaHorario = _agendaHorarioRepository.GetByID(consulta.AgendaHorarioId);
                agendaHorario.Disponivel = true;
                _agendaHorarioRepository.Update(agendaHorario);
                _agendaHorarioRepository.Save();
                
                return await Task.FromResult(new SuccessResponse());
            }
            catch (Exception ex)
            {
                return await Task.FromResult(new FailureResponse(ex.Message));
            }
        }
    }
}
