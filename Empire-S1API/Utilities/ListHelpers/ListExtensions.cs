using System;
using System.Collections.Generic;
using System.Text;

namespace Empire.Utilities.ListHelpers
{
	public static class ListExtensions
	{
		public static T RandomElement<T>(this IList<T> list)
		{
			if (list == null || list.Count == 0)
				throw new InvalidOperationException("Cannot select a random element from an empty list.");

			return list[UnityEngine.Random.Range(0, list.Count)];
		}

		public static T RandomOrDefault<T>(this IList<T> list)
		{
			if (list == null || list.Count == 0)
				return default;

			return list[UnityEngine.Random.Range(0, list.Count)];
		}
	}
}
