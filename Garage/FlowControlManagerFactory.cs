using Garage.Contracts;

namespace Garage;

internal class FlowControlManagerFactory : IFlowControlManagerFactory
{
    public static IFlowControlManager Create()
    {
        CommandManager[] managers =
        {
        };

        Action initialization = () =>
        {
        };

        IUI ui = new ConsoleUI();

        IFlowControlManager flowControlManager = new FlowControlManagerNoMenu(managers, ui, initialization);
        return flowControlManager;
    }
}
