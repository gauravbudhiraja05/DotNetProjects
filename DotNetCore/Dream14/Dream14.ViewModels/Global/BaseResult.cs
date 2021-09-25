namespace Dream14.ViewModels.Global
{
    /// <summary>
    /// ResultVM is a generic data structure that represents each return type of operation
    /// </summary>
    public class BaseResult
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
    }
}
