using Garage.Contracts;
using System.Reflection;

namespace Garage;

public class PatternMatcher<T> where T : IPatternMatchable
{
    public T[] GetObjectsMatchingPatternUsingLinq(T[] allObjects, string[] parameterNames, Dictionary<string, string> parameterValues, Func<int, int, bool>[] ops)
    {
        return allObjects.Where(@object => MatchesPattern(@object, parameterNames.ToArray(), parameterValues, ops.ToArray())).ToArray();
    }

    public bool MatchesPattern(T @object, string[] parameterNames, Dictionary<string, string> parameterValues, Func<int, int, bool>[] ops)
    {
        for (int i = 0; i < parameterNames.Length; i++)
        {
            string? name = parameterNames[i];
            string? value = parameterValues[name];
            Func<int, int, bool>? op = ops[i];

            foreach (var property in @object.GetType().GetNestedTypes().FirstOrDefault().GetProperties())
            {
                if (name.ToLower() == property.Name.ToLower())
                {
                    if (!ComparePropertyValue(value, property, op, @object))
                    {
                        return false;
                    }
                }
            }
        }

        return true;
    }

    private bool ComparePropertyValue(string value, PropertyInfo property, Func<int, int, bool> op, T @object)
    {
        if (property.PropertyType.Name == "Int32")
        {
            return CompareInteger(@object, op, value, property);
        }
        else
        {
            return CompareString(@object, value, property);
        }
    }

    private bool CompareInteger(T vehicle, Func<int, int, bool> op, string parameterValue, PropertyInfo prop)
    {
        int a = int.Parse(parameterValue);
        int b = (int)prop.GetValue(vehicle.MatchableData);
        return op(b, a);
    }

    private static bool CompareString(T vehicle, string parameterValue, PropertyInfo prop)
    {
        return prop.GetValue(vehicle.MatchableData).ToString() == parameterValue;
    }
}
