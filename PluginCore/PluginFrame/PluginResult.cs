using Shared.Messaging;

namespace PluginCore.PluginFrame
{
	public abstract class PluginResult
    {
        public bool IsSuccess { get; set; }
        public bool IsError { get; set; }
        public object? ExecutionResult { get; set; }
        public abstract Task<TransferMessage> BuildErrorMessage();
        public abstract Task<TransferMessage> BuildSuccessMessage();
    }
}