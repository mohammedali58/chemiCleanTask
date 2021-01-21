using ChemiClean.Core.DTOS;
using ChemiClean.SharedKernel;

namespace ChemiClean.Core.Interfaces.UseCases
{
    public interface IProductGetAllUseCase : IUseCaseRequestResponseListHandler<ProductRequestDto, ProductResponseDto>
    {

    }
}
