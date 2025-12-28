namespace Empire.Utilities.QualityHelpers
{
    public static class QualityExtensions
	{
		public static QualityInfo? GetQuality(this string name)
			=> QualityRegistry.ByName.TryGetValue(name, out var q) ? q : null;
	}
}