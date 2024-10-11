namespace Garage;

internal class ReadableName : Attribute
{
    public string Name { get; }

    public ReadableName(string name)
    {
        Name = name;
    }
}
