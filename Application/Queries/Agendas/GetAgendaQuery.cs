using Application.Contracts;
using Application.Interfaces;
using Application.ViewModel;
using Domain.Entities;
using MediatR;

namespace Application.Queries.Agendas
{
    public class GetAgendaQuery : IRequest<IGenericResponse<IEnumerable<AgendaView>>>
    {
        public List<int> MedicoIds { get; set; }
        public List<string> CRMs { get; set; }
        public DateOnly InitialDate { get; set; }
        public DateOnly FinalDate { get; set; }
    }
    public class GetAgendaQueryHandler : IRequestHandler<GetAgendaQuery, IGenericResponse<IEnumerable<AgendaView>>>
    {
        private readonly IRepository<Medico> _medicoRepository;
        private readonly IRepository<Agenda> _agendaRepository;
        private readonly IMediator _mediator;

        public GetAgendaQueryHandler(IRepository<Medico> medicoRepository, IRepository<Agenda> agendaRepository, IMediator mediator)
        {
            _medicoRepository = medicoRepository;
            _agendaRepository = agendaRepository;
            _mediator = mediator;
        }

        public async Task<IGenericResponse<IEnumerable<AgendaView>>> Handle(GetAgendaQuery request, CancellationToken cancellationToken)
        {
            try
            {
                //if(request.InitialDate != null)
                //{
                //    var queryFilter = 
                //}
                var agendas = _agendaRepository.Get();
                List<AgendaView> agendasView = new();
                foreach (var item in agendas)
                {
                    AgendaView consulta = new()
                    {

                    };
                }

                return await Task.FromResult(new SuccessResponse<IEnumerable<AgendaView>>(agendasView));
            }
            catch (Exception ex)
            {
                return await Task.FromResult(new FailureResponse<IEnumerable<AgendaView>>(ex.Message));
            }
        }
    }
}