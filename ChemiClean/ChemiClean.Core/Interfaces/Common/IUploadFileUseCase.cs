using ChemiClean.Core.DTOs;
using ChemiClean.SharedKernel;


namespace ChemiClean.Core.Interfaces
{
    public interface IUploadFileUseCase : IUseCaseRequestResponseListHandler<UploadFileDTO, string>
    {
    }
}