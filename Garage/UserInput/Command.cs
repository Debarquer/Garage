using Garage.Contracts;

namespace Garage.UserInput;

delegate void MyAction(string[] parameters);

internal class Command : ICommand
{
    private string name { get; set; }
    private int minimumNumberOfParameters { get; }
    private int maximumNumberOfParameters { get; }
    private bool unlimitedParameters { get; }
    private string parameters { get; set; }
    private string description { get; set; }
    private MyAction myAction { get; }

    public Command(string name, string parameters, string description, MyAction myAction, bool unlimitedParameters = false)
    {
        this.name = name;
        minimumNumberOfParameters = parameters == "" ? 0 : parameters.Split(' ').Length - parameters.Split(' ').Where(x => x.StartsWith("?")).Count();
        maximumNumberOfParameters = parameters == "" ? 0 : parameters.Split(' ').Length;
        this.parameters = parameters;
        this.description = description;
        this.myAction = myAction;
        this.unlimitedParameters = unlimitedParameters;
    }

    public void PrintHelp(IUI ui)
    {
        ui.PrintMessage($"{name} help:");

        string parametersString = "";
        if (unlimitedParameters)
        {
            parametersString += $"Accepts any number of parameters. ";
        }
        else if (minimumNumberOfParameters == maximumNumberOfParameters)
        {
            parametersString += $"{minimumNumberOfParameters} Parameters: ";
        }
        else
        {
            parametersString += $"[{minimumNumberOfParameters}-{maximumNumberOfParameters}] Parameters: ";
        }

        parametersString += GetParametersString();

        ui.PrintMessage(parametersString);

        ui.PrintMessage("");
        ui.PrintMessage($"Description: {description}");
    }

    private string GetParametersString()
    {
        string parametersString = "";
        string[] parametersArray = parameters.Split(" ");
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

        return parametersString;
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
            throw new ArgumentOutOfRangeException($"Too few parameters. {name} requires at least {minimumNumberOfParameters} parameter(s). \nParameters: {GetParametersString()}");
        }
        else if (parameters.Length > maximumNumberOfParameters && !unlimitedParameters)
        {
            throw new ArgumentOutOfRangeException($"Too many parameters. {name} requires no more than {maximumNumberOfParameters} parameter(s). \nParameters: {GetParametersString()}");
        }

        return true;
    }

    public string GetName()
    {
        return name;
    }
}
