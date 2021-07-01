using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Routing;
using PickfordsIntranet.Core.Helper;
using System;
using System.IO;
using System.Threading.Tasks;

namespace PickfordsIntranet.WebAdmin.Utility
{
    /// <summary>
    /// ViewParser implemented IViewParser
    /// </summary>
    public class ViewParser : IViewParser
    {

        #region Properties
        private readonly IRazorViewEngine _razorViewEngine;
        private readonly ITempDataProvider _tempDataProvider;
        private readonly IServiceProvider _serviceProvider;
        #endregion

        #region Constructor
        /// <summary>
        /// This is constructor i.e. used for injected the razor interface
        /// </summary>
        /// <param name="razorViewEngine"></param>
        /// <param name="tempDataProvider"></param>
        /// <param name="serviceProvider"></param>
        public ViewParser(IRazorViewEngine razorViewEngine,
            ITempDataProvider tempDataProvider,
            IServiceProvider serviceProvider)
        {
            _razorViewEngine = razorViewEngine;
            _tempDataProvider = tempDataProvider;
            _serviceProvider = serviceProvider;
        }
        #endregion

        /// <summary>
        /// Convert target View to Strings
        /// </summary>
        /// <param name="viewName">Name of Razor View</param>
        /// <param name="model">Model object that is required to View</param>
        /// <returns>strings</returns>
        public string RenderToStringAsync(string viewName, object model)
        {
            var httpContext = new DefaultHttpContext { RequestServices = _serviceProvider };
            var actionContext = new ActionContext(httpContext, new RouteData(), new ActionDescriptor());

            using (var sw = new StringWriter())
            {
                //var viewResult = _razorViewEngine.FindView(actionContext, viewName, false);
                var viewResult = _razorViewEngine.FindView(actionContext, viewName, false);

                if (viewResult.View == null)
                {
                    throw new ArgumentNullException($"{viewName} does not match any available view");
                }

                var viewDictionary = new ViewDataDictionary(new EmptyModelMetadataProvider(), new ModelStateDictionary())
                {
                    Model = model
                };

                var viewContext = new ViewContext(
                    actionContext,
                    viewResult.View,
                    viewDictionary,
                    new TempDataDictionary(actionContext.HttpContext, _tempDataProvider),
                    sw,
                    new HtmlHelperOptions()
                );

                viewResult.View.RenderAsync(viewContext);
                return sw.ToString();
            }
        }
    }
}
