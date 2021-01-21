using ChemiClean.Core.DTOS;
using ChemiClean.Core;
using ChemiClean.Core.Interfaces;
using ChemiClean.Core.Interfaces.UseCases;
using ChemiClean.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ChemiClean.Core.UseCases
{
    public class ProductUpdateUseCase : BaseUseCase, IProductUpdateUseCase
    {
        public IProductRepository Repository { get; set; }

        public async Task<bool> HandleUseCase(ProductUpdateDto _request, IOutputPort<ResultDto<bool>> _response)
        {

            //HelperSharedMethods.ValidateToSave(_request);

            var Entity = await Repository.GetFirstOrDefaultAsync(x => x.Id == _request.Id) ?? null;
            var path = Path.GetExtension(Entity.Url).Contains('.') ? Entity.Url : $"{Entity.Url}.txt";
            FileDownloader.DownloadFile(path, Configuration, 2000);
            var BinaryContent = FileDownloader.GetBinaryFile(Entity.Url);
            var IsEqual = Entity.FileContent.Equals(BinaryContent);
            if (!IsEqual)
            {
                Entity.LastModified = DateTime.Now;
                Entity.FileContent = BinaryContent;
            }
            Repository.Update( Mapper.Map<Product>(_request) );
            await UnitOfWork.Commit();
            _response.HandlePresenter(new ResultDto<bool>(true));

            return true;
        }
    }
}


