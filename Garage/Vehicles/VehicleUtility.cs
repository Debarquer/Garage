using Garage.Contracts;
using System.Globalization;
using System.Reflection;

namespace Garage.Vehicles;

internal static class VehicleUtility
{
    public static string GetAvailableVehicleStrings()
    {
        string availableTypes = "";

        IEnumerable<Type> types = Assembly.Load("Garage").GetTypes().Where(t => t.Namespace == "Garage.Vehicles.Vehicles");
        foreach (var availableType in types)
        {
            if (!availableType.Name.ToLower().Contains("<>c") && !availableType.Name.ToLower().Contains("data"))
                availableTypes += availableType.Name.ToLower() + ", ";
        }
        return availableTypes;
    }

    public static bool ValidateType(string typeName)
    {
        IEnumerable<Type> types = Assembly.Load("Garage").GetTypes().Where(t => t.Namespace == "Garage.Vehicles.Vehicles");

        foreach (Type? type in types)
        {
            var typeNameLower = type.Name.ToLower();
            if (typeName.ToLower() == typeNameLower)
            {
                return true;
            }
        }
        return false;
    }

    public static Type GetType(string typeName)
    {
        typeName = FormatInput(typeName);
        return Assembly.Load("Garage").GetTypes().First(t => t.Name == typeName);
    }

    private static string FormatInput(string typeName)
    {
        TextInfo myTI = new CultureInfo("en-US", false).TextInfo;
        typeName = myTI.ToTitleCase(typeName);
        return typeName;
    }

    public static bool IsIVehicle(Type type) => type.IsAssignableTo(typeof(IVehicle));
}
