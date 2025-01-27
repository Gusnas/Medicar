﻿using Application.Contracts;
using Application.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Commands.Agendas
{
    public class CreateAgendaCommand : IRequest<IGenericResponse>
    {
        public int MedicoId { get; set; }
        public DateTime Dia { get; set; }
        public List<DateTime> Horarios { get; set; }
    }
    public class CreateAgendaCommandHandler : IRequestHandler<CreateAgendaCommand, IGenericResponse>
    {
        private readonly IMediator _mediator;
        private readonly IRepository<Agenda> _agendaRepository;
        private readonly IRepository<AgendaHorario> _agendaHorarioRepository;

        public CreateAgendaCommandHandler(IMediator mediator, IRepository<Agenda> agendaRepository, IRepository<AgendaHorario> agendaHorarioRepository)
        {
            _mediator = mediator;
            _agendaRepository = agendaRepository;
            _agendaHorarioRepository = agendaHorarioRepository;
        }
        public async Task<IGenericResponse> Handle(CreateAgendaCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var agendaExistent = _agendaRepository.Get(x => x.MedicoId == request.MedicoId && x.Dia == DateOnly.FromDateTime(request.Dia)).FirstOrDefault();
                if (agendaExistent != null)
                    return await Task.FromResult(new FailureResponse("Já existe uma agenda para este médico neste dia!"));

                if (request.Dia.Date < DateTime.UtcNow.Date)
                    return await Task.FromResult(new FailureResponse("Não é possível criar agenda para um dia passado!"));

                Agenda agenda = new()
                {
                    Dia = DateOnly.FromDateTime(request.Dia),
                    MedicoId = request.MedicoId
                };
                _agendaRepository.Insert(agenda);
                _agendaRepository.Save();

                foreach (var horario in request.Horarios)
                {
                    AgendaHorario agendaHorario = new()
                    {
                        AgendaId = agenda.Id,
                        Disponivel = true,
                        Horario = TimeOnly.FromDateTime(horario)
                    };
                    _agendaHorarioRepository.Insert(agendaHorario);
                }

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
