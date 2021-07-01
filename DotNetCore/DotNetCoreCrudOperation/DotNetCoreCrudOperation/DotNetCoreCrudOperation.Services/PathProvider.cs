using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using PickfordsIntranet.Core.DomainServices;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace PickfordsIntranet.Services
{
    public class PathProvider : IPathProvider
    {
        private IHostingEnvironment _hostingEnvironment;

        private IConfigurationRoot _config;

        public PathProvider(IHostingEnvironment environment, IConfigurationRoot config)
        {
            _hostingEnvironment = environment;
            _config = config;
        }

        public string MapPath(string path)
        {
            //string connString = config["ConnectionStrings:DBConnectionString"];
            //var filePath = Path.Combine(_hostingEnvironment.WebRootPath, path);
            //var filePath = @"D:\UploadTest";
            //var filePath = @"\\192.168.0.221\websites\gaurav";
            var filePath = _config["FileStoragePath"];
            return filePath;
        }
    }
}
