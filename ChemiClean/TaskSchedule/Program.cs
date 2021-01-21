using ChemiClean.Core;
using ChemiClean.Core.DTOS;
using ChemiClean.Core.Interfaces.UseCases;
using ChemiClean.Core.UseCases;
using ChemiClean.SharedKernel;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace TaskSchedule
{
    class Program
    {
        public IProductGetDDLUseCase GetDDLUseCase { get; set; }
        public OutputPort<ListResultDto<ProductResponseDto>> GetDDLPresenter { get; set; }
        public Program()
        {
                
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");


            Task.Run(async () =>
            {

                await RunSchedule();
            }).Wait();
        }
        static HttpClient client = new HttpClient();
        static async Task<Product> RunSchedule()
        {
            Product product = null;
            //await GetDDLUseCase.HandleUseCase(GetDDLPresenter);

            client.BaseAddress = new Uri("https://localhost:44381/api/Product/GetAllWithUpdate");
            HttpResponseMessage response = await client.GetAsync(client.BaseAddress);
            if (response.IsSuccessStatusCode)
            {
                product = await response.Content.ReadAsAsync<Product>();
            }
            return product;
        }
    }
}
