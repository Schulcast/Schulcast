namespace Schulcast.Application.Abstractions;

public interface ICurrentUserService
{
	int? Id { get; }
	bool IsAuthenticated { get; }
}