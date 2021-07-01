﻿using Microsoft.AspNetCore.Mvc.ApplicationModels;
using System.Linq;

namespace CoreWebAPiWIthSwagger
{
    public class ApiExplorerVersionConvention : IControllerModelConvention
    {
        public void Apply(ControllerModel controller)
        {
            var controllerNamespace = controller.ControllerType.Namespace; // e.g. "Controllers.v1"
            var apiVersion = controllerNamespace.Split('.').Last().ToLower();

            controller.ApiExplorer.GroupName = apiVersion;
        }
    }
}
