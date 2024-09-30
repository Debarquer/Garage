using Garage.Contracts;

namespace Garage.UserInput;

delegate void MyAction(string[] parameters);

internal class Command : ICommand
{
    private string name { get; set; }
    private int minimumNumberOfParameters { get; }
    private int maximumNumberOfParameters { get; }
    private string parameters { get; set; }
    private string description { get; set; }
    private MyAction myAction { get; }

    public Command(string name, string parameters, string description, MyAction myAction)
    {
        this.name = name;
        minimumNumberOfParameters = parameters == "" ? 0 : parameters.Split(' ').Length - parameters.Split(' ').Where(x => x.StartsWith("?")).Count();
        maximumNumberOfParameters = parameters == "" ? 0 : parameters.Split(' ').Length;
        this.parameters = parameters;
        this.description = description;
        this.myAction = myAction;
    }

    public void PrintHelp(IUI ui)
    {
        ui.PrintMessage($"{name} help:");
        string[] parametersArray = parameters.Split(" ");

        string parametersString = "";
        if (minimumNumberOfParameters == maximumNumberOfParameters)
        {
            parametersString += $"{minimumNumberOfParameters} Parameters: ";
        }
        else
        {
            parametersString += $"[{minimumNumberOfParameters}-{maximumNumberOfParameters}] Parameters: ";
        }

        foreach (string parameter in parametersArray)
        {
            if (parameter.StartsWith("?"))
            {
                parametersString += $"{parameter.Split("?")[1]}(Optional) ";
            }
            else
            {
                parametersString += parameter + " ";
            }
        }

        ui.PrintMessage(parametersString);

        ui.PrintMessage("");
        ui.PrintMessage($"Description: {description}");
    }

    public void Invoke(string[] parameters)
    {
        myAction(parameters);
    }

    public bool ValidateCommand(string[]? parameters)
    {
        if (parameters == null && minimumNumberOfParameters > 0 ||
            parameters.Length < minimumNumberOfParameters)
        {
            Console.WriteLine($"Too few parameters. {name} requires at least {minimumNumberOfParameters} parameters.");
            return false;
        }
        else if (parameters.Length > maximumNumberOfParameters)
        {
            Console.WriteLine($"Too many parameters. {name} requires no more than {maximumNumberOfParameters} parameters.");
            return false;
        }

        return true;
    }

    public string GetName()
    {
        return name;
    }
}
