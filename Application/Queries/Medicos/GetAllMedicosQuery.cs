using Application.Contracts;
using Application.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Queries.Medicos
{
    public class GetAllMedicosQuery : IRequest<IGenericResponse<IEnumerable<Medico>>>
    {
    }
    public class GetAllMedicosQueryHandler : IRequestHandler<GetAllMedicosQuery, IGenericResponse<IEnumerable<Medico>>>
    {
        private readonly IRepository<Medico> _medicoRepository;

        public GetAllMedicosQueryHandler(IRepository<Medico> medicoRepository)
        {
            _medicoRepository = medicoRepository;
        }

        public async Task<IGenericResponse<IEnumerable<Medico>>> Handle(GetAllMedicosQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var medico = _medicoRepository.Get();
                if (medico == null)
                    return await Task.FromResult(new FailureResponse<IEnumerable<Medico>>("Nenhum médico encontrado"));

                return await Task.FromResult(new SuccessResponse<IEnumerable<Medico>>(medico));
            }
            catch (Exception ex)
            {
                return await Task.FromResult(new FailureResponse<IEnumerable<Medico>>(ex.Message));
            }
        }
    }
}
