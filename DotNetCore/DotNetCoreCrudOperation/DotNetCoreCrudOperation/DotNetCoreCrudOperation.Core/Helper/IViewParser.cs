namespace PickfordsIntranet.Core.Helper
{
    /// <summary>
    /// IViewParser descibes the implementation required for converting Razor View into String
    /// </summary>
    public interface IViewParser
    {
        /// <summary>
        /// This method is responsible to render the partial view into string
        /// </summary>
        /// <param name="viewName"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        string RenderToStringAsync(string viewName, object model);
    }
}
