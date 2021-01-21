using ChemiClean.Core.DTOS;
using ChemiClean.Core.Helper;
using ChemiClean.Core.Interfaces;
using ChemiClean.Core.Interfaces.UseCases;
using ChemiClean.SharedKernel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ChemiClean.Core.UseCases
{
    public class ProductGetAllToShowUseCase : BaseUseCase, IProductGetAllToShowUseCase
    {
        public IProductRepository Repository { get; set; }
        public async Task<bool> HandleUseCase(IOutputPort<ListResultDto<ProductResponseDto>> _response)
        {
            List<Product> dataList = await GetProducts();
            var _map = Mapper.Map<List<ProductResponseDto>>(dataList);
            _response.HandlePresenter(response: new ListResultDto<ProductResponseDto>(_map, dataList.Count));
            return true;
        }
        public async Task<List<Product>> GetProducts()
        {
            List<Product> dataList = await Repository.GetWhereAsync();
            
            return dataList;
        }

    }
}