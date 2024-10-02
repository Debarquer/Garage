using Garage.Contracts;

namespace Garage.UserInput;

internal class FlowControlManagerFactory : IFlowControlManagerFactory
{
    public static IFlowControlManager Create()
    {
        IUI ui = new ConsoleUI();

        CommandManager[] managers =
        {
            new GarageManager(ui, new GarageHandler<IVehicle>(ui)),
            new ConfigManager(ui)
        };

        Action initialization = () =>
        {
        };

        IFlowControlManager flowControlManager = new FlowControlManagerNoMenu(managers, ui, initialization);
        return flowControlManager;
    }
}
