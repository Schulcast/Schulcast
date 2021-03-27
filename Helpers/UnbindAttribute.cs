using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Schulcast.Server.Helpers
{
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Parameter, AllowMultiple = false, Inherited = true)]
	public class UnbindAttribute : Attribute, IModelNameProvider, IPropertyFilterProvider
	{
		static readonly Func<ModelMetadata, bool> _default = m => true;

		Func<ModelMetadata, bool>? _propertyFilter;

		public string[] Exclude { get; }

		public UnbindAttribute(params string[] exclude)
		{
			var items = new List<string>();
			foreach (var item in exclude)
			{
				items.AddRange(SplitString(item));
			}

			Exclude = items.ToArray();
		}

		public Func<ModelMetadata, bool> PropertyFilter
		{
			get
			{
				if (Exclude != null && Exclude.Length > 0)
				{
					if (_propertyFilter == null)
					{
						_propertyFilter = m => !Exclude.Contains(m.PropertyName, StringComparer.Ordinal);
					}

					return _propertyFilter;
				}
				else
				{
					return _default;
				}
			}
		}

		public string Name => string.Join(',', Exclude);

		static IEnumerable<string> SplitString(string original)
		{
			return string.IsNullOrEmpty(original)
				? Array.Empty<string>()
				: original.Split(',').Select(piece => piece.Trim()).Where(piece => !string.IsNullOrEmpty(piece));
		}
	}
}