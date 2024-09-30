using Garage.Contracts;

namespace Garage;

internal class FlowControlManagerFactory : IFlowControlManagerFactory
{
    public static IFlowControlManager Create()
    {
        IUI ui = new ConsoleUI();

        CommandManager[] managers =
        {
            new GarageManager(ui)
        };

        Action initialization = () =>
        {
        };


        IFlowControlManager flowControlManager = new FlowControlManagerNoMenu(managers, ui, initialization);
        return flowControlManager;
    }
}
