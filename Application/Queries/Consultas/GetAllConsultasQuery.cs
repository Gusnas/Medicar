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
                DateTime now = DateTime.UtcNow;
                DateOnly today = DateOnly.FromDateTime(now);
                TimeOnly currentTime = TimeOnly.FromDateTime(now);

                var consultas = _consultaRepository.Get(x => x.Dia > today || (x.Dia == today && x.Horario > currentTime));
                List<ConsultaView> consultasView = new();
                foreach (var item in consultas)
                {
                    ConsultaView consulta = new()
                    {
                        Id = item.Id,
                        DataAgendamento = item.DataAgendamento,
                        Dia = item.Dia,
                        Horario = item.Horario,
                        Medico = _medicoRepository.GetByID(item.MedicoId)
                    };
                    consultasView.Add(consulta);
                }

                consultasView = consultasView.OrderBy(x => x.Dia).ThenBy(x => x.Horario).ToList();

                return await Task.FromResult(new SuccessResponse<IEnumerable<ConsultaView>>(consultasView));
            }
            catch (Exception ex)
            {
                return await Task.FromResult(new FailureResponse<IEnumerable<ConsultaView>>(ex.Message));
            }
        }
    }
}

