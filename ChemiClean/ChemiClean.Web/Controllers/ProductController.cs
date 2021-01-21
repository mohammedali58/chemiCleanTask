using ChemiClean.Core.DTOS;
using ChemiClean.Core.Interfaces.UseCases;
using ChemiClean.SharedKernel;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace ChemiClean.Web.Controllers
{
    [ApiController]
    [Route(ApiUri.TeamplateUri)]
    public class ProductController : UnAuthorizedBaseController
    {
        #region Props

        public IProductAddUseCase AddUseCase { get; set; }
        public IProductGetDDLUseCase GetDDLUseCase { get; set; }
        public IProductGetAllToShowUseCase ProductGetAllToShowUseCase { get; set; }
        public IProductGetAllUseCase GetAllUseCase { get; set; }
        public IProductGetByIdUseCase GetByUseCase { get; set; }
        public IProductDeleteUseCase DeleteUseCase { get; set; }
        public IProductUpdateUseCase UpdateUseCase { get; set; }
        public OutputPort<ResultDto<bool>> Presenter { get; set; }
        public OutputPort<ResultDto<ProductResponseDto>> GetByIdPresenter { get; set; }
        public OutputPort<ListResultDto<ProductResponseDto>> GetAllPresenter { get; set; }
        public OutputPort<ListResultDto<ProductResponseDto>> GetDDLPresenter { get; set; }

        #endregion Props

        #region APIS

        [HttpGet]
        public async Task<ActionResult<ListResultDto<ProductResponseDto>>> GetAll()
        {
            await ProductGetAllToShowUseCase.HandleUseCase(GetAllPresenter);
            return GetAllPresenter.Result;
        }

        [HttpGet]
        public async Task<ActionResult<ListResultDto<ProductResponseDto>>> GetAllWithUpdate()
        {
            await GetDDLUseCase.HandleUseCase(GetDDLPresenter);
            return GetDDLPresenter.Result;
        }

        [HttpGet("GetFile/{productId}")]
        public async Task<ActionResult> ShowPDF(int productId)
        {
            await ProductGetAllToShowUseCase.HandleUseCase(GetAllPresenter);
            var result = GetAllPresenter.Result;
            return File(result.Data.First(c => c.Id == productId).FileContent, "application/pdf");
        }

        #endregion APIS
    }
}