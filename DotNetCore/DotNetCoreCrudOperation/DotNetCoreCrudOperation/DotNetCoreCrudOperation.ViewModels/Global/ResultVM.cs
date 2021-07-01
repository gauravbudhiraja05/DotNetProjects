using System;
using System.Collections.Generic;
using System.Text;

namespace PickfordsIntranet.ViewModels.Global
{
    /// <summary>
    /// ResultVM is a generic data structure that represents each return type of operation
    /// </summary>
    public class ResultVM<T>
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
    }
}
