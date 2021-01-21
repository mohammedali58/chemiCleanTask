using ChemiClean.Core.DTOS;
using ChemiClean.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChemiClean.Core.Interfaces.UseCases
{
    public interface IProductAddUseCase : IUseCaseRequestResponseHandler<ProductRequestDto, bool>
    {

    }
}
