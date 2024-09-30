using Garage.Contracts;

namespace Garage;

internal abstract class CommandManager
{
    public string Name { get; set; }
    public string[] Aliases { get; set; }

    protected ICommandList CommandList { get; set; }
    public virtual void HandleInput(string inputCommand, string[]? parameters, IUI ui) => CommandList.HandleInput(inputCommand, parameters, ui);
}
