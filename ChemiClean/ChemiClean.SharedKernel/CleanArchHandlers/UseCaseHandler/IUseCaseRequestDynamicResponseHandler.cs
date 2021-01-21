using System.Threading.Tasks;

namespace ChemiClean.SharedKernel
{
    public interface IUseCaseRequestDynamicResponseHandler<in TUseCaseRequest, dynamic>
    {
        Task<bool> HandleUseCase(TUseCaseRequest _request, IOutputPort<ResultDto<dynamic>> _response);
    }
}