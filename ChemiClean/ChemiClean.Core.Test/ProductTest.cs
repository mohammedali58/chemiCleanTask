using AutoMapper;
using ChemiClean.Core;
using ChemiClean.Core.DTOS;
using ChemiClean.Core.Interfaces;
using ChemiClean.Core.Interfaces.UseCases;
using ChemiClean.Core.UseCases;
using ChemiClean.Infrastructure.Mapping;
using ChemiClean.SharedKernel;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Xunit;
using static ChemiClean.SharedKernel.SharedKernelEnums;

namespace Api_Tests
{
    public class ProductTest
    {

        [Fact]
        public async Task TestProduct_GetAll_ReturnsAListContainsNmeAsync()
        {
            //Arrange
            var mock = new Mock<IProductRepository>();
            mock.Setup(repo => repo.GetCountAsync(null)).ReturnsAsync(GetTestProduct().Count);

            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new ChemiCleanMapping());
            });

            var mapper = mockMapper.CreateMapper();
            var usecase = new ProductGetAllUseCase();
            OutputPort<ListResultDto<ProductResponseDto>> ProductGetAllPresenter = new OutputPort<ListResultDto<ProductResponseDto>>();

            // Act
            await usecase.HandleUseCase(ProductGetAllPresenter);

            // Assert
            var viewResult = Assert.IsType<Task<ListResultDto<ProductResponseDto>>>(ProductGetAllPresenter.Result);
            var model = Assert.IsAssignableFrom<IEnumerable<ListResultDto<ProductResponseDto>>>(viewResult.Result);
            Assert.Equal(GetTestProduct().Count, model.Count());
        }

        [Fact]
        public async Task TestProduct_GetAllToShowAsync()
        {
            //Arrange
            var mock = new Mock<IProductRepository>();
            mock.Setup(repo => repo.GetCountAsync(null)).ReturnsAsync(GetTestProduct().Count);

            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new ChemiCleanMapping());
            });

            var mapper = mockMapper.CreateMapper();
            var usecase = new ProductGetAllToShowUseCase();
            OutputPort<ListResultDto<ProductResponseDto>> GetDDLPresenter = new OutputPort<ListResultDto<ProductResponseDto>>();

            // Act
            await usecase.HandleUseCase(GetDDLPresenter);

            // Assert
            var viewResult = Assert.IsType<Task<ListResultDto<ProductResponseDto>>>(GetDDLPresenter.Result);
            var model = Assert.IsAssignableFrom<IEnumerable<ListResultDto<ProductResponseDto>>>(viewResult.Result);
            Assert.Equal(GetTestProduct().Count, model.Count());
        }

        private List<Product> GetTestProduct()
        {
            return new List<Product> 
            {
                new Product{ Id=1 , ProductName="Absol"}
            };
        }
    }
}
