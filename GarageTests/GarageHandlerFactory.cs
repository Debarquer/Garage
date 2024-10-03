using Garage;
using Garage.Contracts;
namespace GarageTests;

internal static class GarageHandlerFactory
{
    public static GarageHandler<IVehicle> CreateGarageHandler()
    {
        IUI ui = new ConsoleUI();
        return new GarageHandler<IVehicle>(ui);
    }
}
