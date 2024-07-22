using Application.Contracts;
using Application.Interfaces;
using Application.ViewModel;
using Domain.Entities;
using MediatR;

namespace Application.Queries.Consultas
{
    public class GetAllConsultasQuery : IRequest<IGenericResponse<IEnumerable<ConsultaView>>>
    {
    }
    public class GetAllConsultasQueryHandler : IRequestHandler<GetAllConsultasQuery, IGenericResponse<IEnumerable<ConsultaView>>>
    {
        private readonly IRepository<Medico> _medicoRepository;
        private readonly IRepository<Consulta> _consultaRepository;
        private readonly IMediator _mediator;

        public GetAllConsultasQueryHandler(IRepository<Medico> medicoRepository, IRepository<Consulta> consultaRepository, IMediator mediator)
        {
            _medicoRepository = medicoRepository;
            _consultaRepository = consultaRepository;
            _mediator = mediator;
        }

        public async Task<IGenericResponse<IEnumerable<ConsultaView>>> Handle(GetAllConsultasQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var consultas = _consultaRepository.Get();
                List<ConsultaView> consultasView = new();
                foreach (var item in consultas)
                {
                    ConsultaView consulta = new()
                    {
                        ConsultaId = item.Id,
                        DataAgendamento = item.DataAgendamento,
                        Dia = item.Dia,
                        Horario = item.Horario,
                        Medico = _medicoRepository.GetByID(item.MedicoId)
                    };
                }

                return await Task.FromResult(new SuccessResponse<IEnumerable<ConsultaView>>(consultasView));
            }
            catch (Exception ex)
            {
                return await Task.FromResult(new FailureResponse<IEnumerable<ConsultaView>>(ex.Message));
            }
        }
    }
}

