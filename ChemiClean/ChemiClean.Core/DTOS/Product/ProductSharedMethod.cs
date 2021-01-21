using ChemiClean.Core.Interface;
using ChemiClean.SharedKernel;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MSCC.Core.DTOS
{
    public static class ProductSharedMethod
    {

        public static async Task<bool> ValidateToUpdateAsync<T, TKeyType, TRequest>(TRequest request, int id, IRepository<T> _Repository) where T : class
        {
            #region User Input Validation

            if (request == null)
                throw new ArgumentNullException($"Invalid Request {nameof(T)}");

            var classType = request.GetType().GetProperties().Where(p => !p.PropertyType.IsGenericType).ToList();
            classType.AsParallel().ForAll(p =>
            {
                switch (p.PropertyType)
                {
                    case var stringType when stringType == typeof(string):
                        if (string.IsNullOrWhiteSpace((string)p.GetValue(request)))
                            throw new ValidationsException($"Invalid Name {p.Name}");
                        break;
                    case var numberType when numberType == typeof(int):
                        if (Convert.ToInt32(p.GetValue(request)) <= default(int))
                            throw new ValidationsException($"Invalid {p.Name}");
                        break;
                    case var decimalType when decimalType == typeof(decimal):
                        if (Convert.ToDecimal(p.GetValue(request)) <= default(decimal))
                            throw new ValidationsException($"Invalid {p.Name}");
                        break;
                    case var floatType when floatType == typeof(double):
                        if (Convert.ToDouble(p.GetValue(request)) <= default(double))
                            throw new ValidationsException($"Invalid {p.Name}");
                        break;

                }

            });
            #endregion User Input Validation



            return true;
        }

        public static bool ValidateToSave<T>(T request) where T : class
        {
            if (request is null)
                throw new ArgumentNullException($"Invalid Request {nameof(T)}");

            var classType = request.GetType().GetProperties().Where(p => !p.PropertyType.IsGenericType).ToList();
            classType.AsParallel().ForAll(p =>
            {
                switch (p.PropertyType)
                {
                    case var stringType when stringType == typeof(string):
                        if (string.IsNullOrWhiteSpace((string)p.GetValue(request)))
                            throw new ValidationsException($"Invalid Name {p.Name}");
                        break;
                    case var numberType when numberType == typeof(int):
                        if (Convert.ToInt32(p.GetValue(request)) <= default(int))
                            throw new ValidationsException($"Invalid {p.Name}");
                        break;
                    case var decimalType when decimalType == typeof(decimal):
                        if (Convert.ToDecimal(p.GetValue(request)) <= default(decimal))
                            throw new ValidationsException($"Invalid {p.Name}");
                        break;
                    case var floatType when floatType == typeof(double):
                        if (Convert.ToDouble(p.GetValue(request)) <= default(double))
                            throw new ValidationsException($"Invalid {p.Name}");
                        break;

                }
            });

            return true;
        }




    }
}
