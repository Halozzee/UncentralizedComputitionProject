namespace SharedServer.PluginCore
{
	public class PluginResult
    {
        public bool IsSuccess { get; set; }
        public bool IsError { get; set; }
        public object? ExecutionResult { get; set; }
    }
}