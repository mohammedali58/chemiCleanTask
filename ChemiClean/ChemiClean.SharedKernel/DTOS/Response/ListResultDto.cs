using System.Collections.Generic;

namespace ChemiClean.SharedKernel

{
    public class ListResultDto<T> : ResultBaseDto
    {
        //private List<T> lists;
        //private object p;

        public ListResultDto()
        {
        }

        public List<T> Data { get; set; }
        public int TotalCount { get; set; }

        public ListResultDto(List<T> data, int totalCount, bool isSuccess = true, string message = "") : base(isSuccess, message)
        {
            TotalCount = totalCount;
            Data = data;
        }

        public ListResultDto(List<T> data, int totalCount) => (Data, TotalCount) = (data, totalCount);
    }
}