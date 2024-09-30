namespace Garage.Contracts;

internal interface IFlowControlManagerFactory
{
    /// <summary>
    /// Creates and returns a specific instance of FlowControlManager.
    /// </summary>
    /// <returns>The instance of FlowControlManager</returns>
    public abstract static IFlowControlManager Create();
}
