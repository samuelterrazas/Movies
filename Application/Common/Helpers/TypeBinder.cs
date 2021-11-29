using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Movies.Application.Common.Helpers
{
    public class TypeBinder<T> : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            var propertyName = bindingContext.ModelName;
            var valuesProvider = bindingContext.ValueProvider.GetValue(propertyName);

            if (valuesProvider == ValueProviderResult.None)
                return Task.CompletedTask;

            try
            {
                var deserializedValue = JsonConvert.DeserializeObject<T>(valuesProvider.FirstValue);
                bindingContext.Result = ModelBindingResult.Success(deserializedValue);
            }
            catch
            {
                bindingContext.ModelState.TryAddModelError(propertyName, "Invalid values.");
            }

            return Task.CompletedTask;
        }
    }
}
