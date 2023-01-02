namespace Schulcast.Application.Abstractions;

[Serializable()]
public class EntityNotFoundException : Exception
{
	public EntityNotFoundException(string message) : base(message) { }
}