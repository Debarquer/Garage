using Garage.Contracts;
using System.Reflection;

namespace Garage;

public class PatternMatcher<T> where T : IPatternMatchable
{
    Dictionary<string, Func<int, int, bool>> opToFunc = new()
    {
        {"=", (int a, int b) => {
            return a == b;
        } },
        {">", (int a, int b) => {
            return a > b;
        } },
        {"<", (int a, int b) => {
            return a < b;
        } }
    };

    public T[] GetObjectsMatchingPattern(T[] allObjects, string[] parameters)
    {
        List<T> matchingObjects = new List<T>();

        foreach (T @object in allObjects)
        {
            if (CheckIfObjectIsMatchingPattern(parameters, @object))
            {
                matchingObjects.Add(@object);
            }
        }

        return matchingObjects.ToArray();
    }

    public bool CheckIfObjectIsMatchingPattern(string[] parameters, T vehicle)
    {
        foreach (string parameter in parameters)
        {
            // setup
            string @operator = "";
            foreach (string ops in opToFunc.Keys)
            {
                if (parameter.Contains(ops))
                {
                    @operator = ops;
                    break;
                }
            }

            string[] parameterSplit = parameter.Split(@operator);
            if (parameterSplit.Length != 2)
            {
                return false;
            }

            string parameterName = parameterSplit[0];
            string parameterValue = parameterSplit[1];

            // check
            if (parameterName == "type")
            {
                if (!CheckObjectType(vehicle, parameterValue))
                    return false;
            }
            else
            {
                if (!CheckProperties(vehicle, @operator, parameterName, parameterValue))
                    return false;
            }
        }

        return true;
    }

    private static bool CheckObjectType(T @object, string parameterValue)
    {
        return parameterValue.ToLower() == @object.GetType().Name.ToLower();
    }

    private bool CheckProperties(T vehicle, string @operator, string parameterName, string parameterValue)
    {
        PropertyInfo[] props = vehicle.MatchableObjectType.GetProperties();

        bool foundProp = false;
        foreach (PropertyInfo prop in props)
        {
            if (prop.Name.ToLower() == parameterName.ToLower())
            {
                foundProp = true;

                if (!CheckProperty(vehicle, @operator, parameterValue, prop))
                    return false;
            }
        }

        if (!foundProp)
        {
            // no matching prop found means that the type was invalid
            // for example, trying to match the car fueltype to an airplane
            return false;
        }

        return true;
    }

    private bool CheckProperty(T vehicle, string op, string parameterValue, PropertyInfo prop)
    {
        if (prop.PropertyType.Name == "Int32")
        {
            int a = int.Parse(parameterValue);
            if (!opToFunc[op]((int)prop.GetValue(vehicle.MatchableData), a))
            {
                return false;
            }
        }
        else
        {
            if (prop.GetValue(vehicle.MatchableData).ToString() != parameterValue)
            {
                return false;
            }
        }

        return true;
    }
}
