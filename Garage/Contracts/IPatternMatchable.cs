namespace Garage.Contracts;

public interface IPatternMatchable
{
    public IMatchableData MatchableData { get; }
    public Type MatchableObjectType { get; }
}
