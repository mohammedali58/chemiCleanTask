using System.Threading.Tasks;

namespace ChemiClean.SharedKernel

{
    public interface IUseCaseResponseHandler</*out*/ TUseCaseResponse>
    {
        Task<bool> HandleUseCase(IOutputPort<ResultDto<TUseCaseResponse>> _response);
    }
}