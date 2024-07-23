using Application.Contracts;
using Application.Interfaces;
using Application.ViewModel;
using Domain.Entities;
using MediatR;

namespace Application.Queries.Agendas
{
    public class GetAgendaQuery : IRequest<IGenericResponse<IEnumerable<AgendaView>>>
    {
        public List<int>? MedicoIds { get; set; }
        public List<string>? CRMs { get; set; }
        public DateOnly? InitialDate { get; set; }
        public DateOnly? FinalDate { get; set; }
    }
    public class GetAgendaQueryHandler : IRequestHandler<GetAgendaQuery, IGenericResponse<IEnumerable<AgendaView>>>
    {
        private readonly IRepository<Medico> _medicoRepository;
        private readonly IRepository<Agenda> _agendaRepository;
        private readonly IRepository<AgendaHorario> _agendaHorarioRepository;
        private readonly IMediator _mediator;

        public GetAgendaQueryHandler(IRepository<Medico> medicoRepository, IRepository<Agenda> agendaRepository, IRepository<AgendaHorario> agendaHorarioRepository, IMediator mediator)
        {
            _medicoRepository = medicoRepository;
            _agendaRepository = agendaRepository;
            _agendaHorarioRepository = agendaHorarioRepository;
            _mediator = mediator;
        }

        public async Task<IGenericResponse<IEnumerable<AgendaView>>> Handle(GetAgendaQuery request, CancellationToken cancellationToken)
        {
            try
            {
                DateTime now = DateTime.UtcNow;
                DateOnly today = DateOnly.FromDateTime(now);
                TimeOnly currentTime = TimeOnly.FromDateTime(now);

                if (request.MedicoIds == null)
                    request.MedicoIds = new List<int>();
                if (request.CRMs == null)
                    request.CRMs = new List<string>();

                var agendas = (from agenda in _agendaRepository.Get()
                              join medico in _medicoRepository.Get() on agenda.MedicoId equals medico.Id
                              join horario in _agendaHorarioRepository.Get() on agenda.Id equals horario.AgendaId
                              where agenda.Dia >= today
                              && horario.Disponivel
                              && horario.Horario >= currentTime
                              && (!request.MedicoIds.Any() || request.MedicoIds.Contains(agenda.MedicoId))
                              && (!request.CRMs.Any() || request.CRMs.Contains(medico.CRM))
                              && (!request.InitialDate.HasValue || agenda.Dia >= request.InitialDate.Value)
                              && (!request.FinalDate.HasValue || agenda.Dia <= request.FinalDate.Value)
                               select new {agenda, medico}).Distinct().ToList();

                List<AgendaView> agendasView = [];

                foreach (var agenda in agendas)
                {
                    AgendaView agendaView = new()
                    {
                        AgendaId = agenda.agenda.Id,
                        Dia = agenda.agenda.Dia,
                        Medico = new()
                        {
                            Id = agenda.medico.Id,
                            CRM = agenda.medico.CRM,
                            Email = agenda.medico.Email,
                            Nome = agenda.medico.Nome
                        },
                        Horarios = _agendaHorarioRepository.Get(x => x.AgendaId == agenda.agenda.Id && x.Disponivel == true).Select(h => h.Horario).ToList()
                    };

                    agendasView.Add(agendaView);
                }

                agendasView = agendasView.OrderBy(x => x.Dia).ToList();

                return await Task.FromResult(new SuccessResponse<IEnumerable<AgendaView>>(agendasView));
            }
            catch (Exception ex)
            {
                return await Task.FromResult(new FailureResponse<IEnumerable<AgendaView>>(ex.Message));
            }
        }
    }
}