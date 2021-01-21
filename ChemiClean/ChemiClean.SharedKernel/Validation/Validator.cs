using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChemiClean.SharedKernel.Validation
{
    public abstract class Validator<TEntity>
    {
        public class ValidatorResult
        {
            public bool ConditionSatatus { get; set; }
            public string[] Errors { get; set; }
        }


        public abstract ValidatorResult IsValid(TEntity data);

        public ValidatorResult GetValidation(TEntity model, List<(Func<TEntity, bool> QualifingConditions, Func<TEntity, string> GetError)> rules)
        {
            var result = new ValidatorResult();

            var errors = rules.Where(a => !a.QualifingConditions(model))
                .Select(b => b.GetError(model))
                .ToArray();

            result.ConditionSatatus = !errors.Any();
            result.Errors = errors;
            return result;
        }
    }
}
