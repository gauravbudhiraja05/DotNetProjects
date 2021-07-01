using System;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Server.Kestrel.Core.Internal.Http;
using PickfordsIntranet.ViewModels.Global;

namespace PickfordsIntranet.WebAdmin.Filters
{
    public class AutoPopulateLoggingDetails : ActionFilterAttribute
    {

        #region Properties
        private int _actionId;
        private string _logDescription;
        #endregion



        /// <summary>
        /// This action filter attribute is responsible for adding common application, system, and http context
        /// information to the view model so this information is available to the business
        /// repository for logging purpses. This filter facilitates the developer's work so that
        /// this does not have to be done in code for each and every action on a controller. This
        /// filter can be applied to the controller (assuming all actions within the controller have
        /// models that contain the 'LoggingDetails' object property. A 'loggingDetails' object will
        /// be added into the controller action's parameter collection which you MUST define as a new
        /// parameter in your action's parameters in order to consume in code.
        /// </summary>
        /// <param name="ActionId">Defines the integer ActionId that should be logged into the authorization
        /// log. This value defines the unique type of operation that is taking place in the application.</param>
        /// <param name="LogDescription">An informative description that will be added to the authorization log entry
        /// to describe the operation that has taken place.</param>
        public AutoPopulateLoggingDetails(int ActionId, string LogDescription)
        {
            _actionId = ActionId;
            _logDescription = LogDescription;
        }

        public AutoPopulateLoggingDetails()
        {
            _actionId = 0;
            _logDescription = string.Empty;
        }

        /// <summary>
        /// This method fires before the code within the controller action is executed. This allows the attribute
        /// to filter and process nearly all aspects of the input request before the controller action's code
        /// takes over.
        /// </summary>
        /// <param name="context"></param>
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var loggingDetails = new UserActionLoggingDetails();

            loggingDetails.ServerId = Environment.MachineName;
            loggingDetails.LocalIp = context.HttpContext.Connection.LocalIpAddress.ToString() + ":" + context.HttpContext.Connection.LocalPort.ToString();
            loggingDetails.AppId = "";
            loggingDetails.SubComponent = context.ActionDescriptor.DisplayName;
            loggingDetails.ActionId = _actionId;
            loggingDetails.Description = _logDescription;
            loggingDetails.ServerDateTime = DateTime.Now;
            loggingDetails.SourceIP = context.HttpContext.Connection.RemoteIpAddress.ToString() + ":" + context.HttpContext.Connection.RemotePort.ToString();
            loggingDetails.RemoteMachineInfo = ((FrameRequestHeaders)context.HttpContext.Request.Headers).HeaderUserAgent;
            //loggingDetails.UserId = context.HttpContext.User.Claims?.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value ?? null;
            loggingDetails.UserId = context.HttpContext.User.Claims?.FirstOrDefault(c => c.Type == "UserID")?.Value ?? null;
            //loggingDetails.Username = context.HttpContext.User.Identity.Name;
            loggingDetails.Username = context.HttpContext.User.Claims?.FirstOrDefault(c => c.Type == "Email")?.Value ?? null;
            loggingDetails.Url = context.HttpContext.Request.Path;
            loggingDetails.RoleName = context.HttpContext.User.Claims?.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value ?? null;//"SuperUser";//Get RoleName from Identity Context
            loggingDetails.FullName = context.HttpContext.User.Claims?.FirstOrDefault(c => c.Type == "FullName")?.Value ?? null;
            //loggingDetails.FunctionCode = string.Join(",", functionCodes);//context.HttpContext.User.Claims?.FirstOrDefault(c => c.Type == "AllActions")?.Value ?? null;//"SuperUser";

            if (context.ActionArguments.ContainsKey("loggingDetails"))
            {
                if (context.ActionArguments["loggingDetails"] != null)
                {
                    context.ActionArguments["loggingDetails"] = loggingDetails;
                }
            }
           
        }

        /// <summary>
        /// This method fires before the request result is returned to the browser or client agent. This allows the
        /// attribute to filer and process nearly all aspects of the result just before the controller's action
        /// returns the result or redirect.
        /// </summary>
        /// <param name="context"></param>
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            // Not needed at the moment - CHD
        }


    }
}
