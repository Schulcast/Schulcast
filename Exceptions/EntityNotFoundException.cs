using System;

namespace Schulcast.Server.Exceptions
{
	[Serializable()]
	public class EntityNotFoundException : Exception
	{
		public EntityNotFoundException(string message) : base(message) { }
	}
}