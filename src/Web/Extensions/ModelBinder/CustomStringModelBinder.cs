//using System;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Mvc.ModelBinding;
//using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;

//namespace Web.Extensions.ModelBinder
//{
//    public class CustomStringModelBinder : IModelBinder
//    {
//        private readonly IModelBinder _fallbackBinder;
//        public CustomStringModelBinder(Type modelType)
//        {
//            _fallbackBinder = new SimpleTypeModelBinder(modelType);
//        }

//        public Task BindModelAsync(ModelBindingContext bindingContext)
//        {
//            if (bindingContext == null) 
//            {
//                throw new ArgumentNullException(nameof(bindingContext));
//            }

//            var valueProviderResult = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
//            if (valueProviderResult != ValueProviderResult.None)
//            {
//                bindingContext.ModelState.SetModelValue(bindingContext.ModelName, valueProviderResult);

//                var valueAsString = valueProviderResult.FirstValue;
//                if (string.IsNullOrWhiteSpace(valueAsString))
//                {
//                    return _fallbackBinder.BindModelAsync(bindingContext);
//                }

//                var model = valueAsString.Replace((char)1610, (char)1740).Replace((char)1603, (char)1705);
//                bindingContext.Result = ModelBindingResult.Success(model);
//                return Task.CompletedTask;
//            }

//            return _fallbackBinder.BindModelAsync(bindingContext);
//        }
//    }
//}