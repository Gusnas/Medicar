using Application.Contracts;
using Application.Interfaces;
using Application.Queries.Medicos;
using Domain.Entities;
using MediatR;

namespace Application.Commands.Medicos
{
    public class CreateMedicoCommand : IRequest<IGenericResponse>
    {
        public string Nome { get; set; }
        public string CRM { get; set; }
        public string Email { get; set; }
    }
    public class CreateMedicoCommandHandler : IRequestHandler<CreateMedicoCommand, IGenericResponse>
    {
        private readonly IMediator _mediator;
        private readonly IRepository<Medico> _medicoRepository;

        public CreateMedicoCommandHandler(IMediator mediator, IRepository<Medico> medicoRepository)
        {
            _mediator = mediator;
            _medicoRepository = medicoRepository;
        }
        public async Task<IGenericResponse> Handle(CreateMedicoCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var queryMedicoByCRM = await _mediator.Send(new GetMedicoByCRMQuery { CRM = request.CRM });
                if (queryMedicoByCRM.IsSuccessful)
                    return new FailureResponse("Já existe um médico cadastrado com esse CRM");

                Medico medicoDTO = new()
                {
                    CRM = request.CRM,
                    Email = request.Email,
                    Nome = request.Nome
                };

                _medicoRepository.Insert(medicoDTO);
                _medicoRepository.Save();
                return await Task.FromResult(new SuccessResponse());
            }
            catch (Exception ex)
            {
                return await Task.FromResult(new FailureResponse(ex.Message));
            }
        }
    }
}
