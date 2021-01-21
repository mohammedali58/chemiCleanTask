using System.Threading.Tasks;

namespace ChemiClean.SharedKernel

{
    public interface IUseCaseResponseListHandler</*out*/ TUseCaseResponse>
    {
        Task<bool> HandleUseCase(IOutputPort<ListResultDto<TUseCaseResponse>> _response);
    }
}