using ChemiClean.Core.Interface;
using ChemiClean.SharedKernel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ChemiClean.Core.Helper
{
    public static class ExtensionMethods
    {
        public static AppSettings Objsetting;
        public static T Clone<T>(this T source) where T : class
        {
            string serialized = JsonConvert.SerializeObject(source, Formatting.Indented, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
            return JsonConvert.DeserializeObject<T>(serialized);
        }

        public static void ForEach<T>(this ICollection<T> source, Action<T> action)
        {
            foreach (T element in source)
                action(element);
        }
        public static async Task<int> GetRecordsCount<T>(this List<T> DataList, PagingModel PageingModel, IRepository<T> repo, Expression<Func<T, bool>> filter) where T :class
        {
            return DataList.Any() && PageingModel.PageNumber <= 1 /*First page*/ && DataList.Count < PageingModel.PageSize ? DataList.Count : await repo.GetCountAsync(filter);
        }
        public static async Task<int> GetAuditEntityRecordsCount<E>(this List<E> DataList, PagingModel PageingModel, IRepository<E> repo, Expression<Func<E, bool>> filter) where E :class
        {
            return DataList.Any() && PageingModel.PageNumber <= 1 /*First page*/ && DataList.Count < PageingModel.PageSize ? DataList.Count : await repo.GetCountAsync(filter);
        }
 

 


    }
}
