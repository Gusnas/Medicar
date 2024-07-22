using Application.Contracts;
using Application.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Queries.Medicos
{
    public class GetMedicoByCRMQuery : IRequest<IGenericResponse<Medico>>
    {
        public string CRM { get; set; }
    }
    public class GetMedicoByCRMQueryHandler : IRequestHandler<GetMedicoByCRMQuery, IGenericResponse<Medico>>
    {
        private readonly IRepository<Medico> _medicoRepository;

        public GetMedicoByCRMQueryHandler(IRepository<Medico> medicoRepository)
        {
            _medicoRepository = medicoRepository;
        }
        public async Task<IGenericResponse<Medico>> Handle(GetMedicoByCRMQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var medico = _medicoRepository.Get(x => x.CRM == request.CRM).FirstOrDefault();
                if (medico == null)
                    return await Task.FromResult(new FailureResponse<Medico>("Médico não cadastrado"));

                return await Task.FromResult(new SuccessResponse<Medico>(medico));
            }
            catch (Exception ex)
            {
                return await Task.FromResult(new FailureResponse<Medico>(ex.Message));
            }
        }
    }
}
