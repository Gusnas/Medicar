using Application.Contracts;
using Application.Interfaces;
using Application.ViewModel;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.Consultas
{
    public class CreateConsultaCommand : IRequest<IGenericResponse<ConsultaView>>
    {
        public int AgendaId { get; set; }
        public TimeOnly Horario { get; set; }
    }
    public class CreateConsultaCommandHandler : IRequestHandler<CreateConsultaCommand, IGenericResponse<ConsultaView>>
    {
        private readonly IRepository<Consulta> _consultaRepository;
        private readonly IRepository<AgendaHorario> _agendaHorarioRepository;
        private readonly IRepository<Agenda> _agendaRepository;
        private readonly IRepository<Medico> _medicoRepository;
        private readonly IMediator _mediator;

        public CreateConsultaCommandHandler(IRepository<Consulta> consultaRepository, IRepository<AgendaHorario> agendaHorarioRepository, IRepository<Agenda> agendaRepository, IRepository<Medico> medicoRepository, IMediator mediator)
        {
            _consultaRepository = consultaRepository;
            _agendaHorarioRepository = agendaHorarioRepository;
            _agendaRepository = agendaRepository;
            _medicoRepository = medicoRepository;
            _mediator = mediator;
        }

        public async Task<IGenericResponse<ConsultaView>> Handle(CreateConsultaCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var agendaHorario = _agendaHorarioRepository.Get(x => x.AgendaId == request.AgendaId && x.Horario == request.Horario && x.Disponivel == true);
                if (agendaHorario == null)
                    return new FailureResponse<ConsultaView>("Horário indisponível!");
                var agenda = _agendaRepository.GetByID(request.AgendaId);

                Consulta consulta = new()
                {
                    Horario = request.Horario,
                    DataAgendamento = DateTime.Now,
                    MedicoId = agenda.MedicoId,
                    Dia = agenda.Dia
                };

                _consultaRepository.Insert(consulta);
                _consultaRepository.Save();

                ConsultaView consultaView = new()
                {
                    ConsultaId = consulta.Id,
                    Dia = consulta.Dia,
                    Horario = consulta.Horario,
                    DataAgendamento = consulta.DataAgendamento,
                    Medico = _medicoRepository.GetByID(agenda.MedicoId)
                };
                return await Task.FromResult(new SuccessResponse<ConsultaView>(consultaView));
            }
            catch (Exception ex)
            {
                return await Task.FromResult(new FailureResponse<ConsultaView>(ex.Message));
            }
        }
    }
}