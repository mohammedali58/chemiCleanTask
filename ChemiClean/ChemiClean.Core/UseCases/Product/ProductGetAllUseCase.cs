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
    public class ProductGetAllUseCase : BaseUseCase, IProductGetDDLUseCase
    {
        public IProductRepository Repository { get; set; }
        public async Task<bool> HandleUseCase(IOutputPort<ListResultDto<ProductResponseDto>> _response)
        {
            List<Product> dataList = await UpdateFileContent();
            var _map = Mapper.Map<List<ProductResponseDto>>(dataList);
            _response.HandlePresenter(response: new ListResultDto<ProductResponseDto>(_map, dataList.Count));
            return true;
        }
        public async Task<List<Product>> UpdateFileContent()
        {
            List<Product> dataList = await Repository.GetWhereAsync();
            dataList.ToList().ForEach(async item =>
            {
                var result = Path.GetExtension(item.Url).Contains("pdf");
                if (result)
                {
                    var data = FileDownloader.DownloadFile(item.Url, Configuration, 2000);
                    if (item.FileContent == null || (item.FileContent != null && !item.FileContent.SequenceEqual(data)))
                    {
                        item.LastModified = DateTime.Now;
                        item.FileContent = data;
                        Repository.Update(item);
                        await UnitOfWork.Commit();
                    }
                }
            });
            return dataList;
        }

    }
}