namespace PluginCore.PluginFrame
{
	public class PluginResult
    {
        public bool IsSuccess { get; set; }
        public bool IsError { get; set; }
        public object? ExecutionResult { get; set; }

        public async Task<string> BuildErrorMessage() 
        {
            return null;
        }

        public async Task<string> BuildSuccessMessage()
        {
            return null;
        }
    }
}