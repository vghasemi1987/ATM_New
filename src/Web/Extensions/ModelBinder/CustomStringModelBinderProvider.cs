//using System;
//using Microsoft.AspNetCore.Mvc.ModelBinding;

//namespace Web.Extensions.ModelBinder
//{
//    public class CustomStringModelBinderProvider : IModelBinderProvider
//    {
//        public IModelBinder GetBinder(ModelBinderProviderContext context)
//        {
//            if (context == null) throw new ArgumentNullException(nameof(context));

//            if (!context.Metadata.IsComplexType && context.Metadata.ModelType == typeof(string))
//            {
//                return new CustomStringModelBinder(context.Metadata.ModelType);
//            }
//            return null;
//        }
//    }
//}
