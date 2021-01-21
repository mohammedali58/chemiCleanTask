﻿using System.Threading.Tasks;

namespace ChemiClean.SharedKernel

{
    public interface IUseCaseRequestResponseListHandler<in TUseCaseRequest, /*out*/ TUseCaseResponse>
    //where TUseCaseRequest : IUseCaseRequest<TUseCaseResponse> that where commented because we used generics and the request type can be used with many response types (Ex: IdDto)
    {
        Task<bool> HandleUseCase(TUseCaseRequest request, IOutputPort<ListResultDto<TUseCaseResponse>> outputPort);
    }
}