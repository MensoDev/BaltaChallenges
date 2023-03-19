namespace BaltaChallenges;

public static class NumbersExtensions
{
    public static string ToCardinal(this long unit) => CardinalConverter.ToCardinalName(unit);
    public static string ToCardinal(this int unit) => CardinalConverter.ToCardinalName(unit);
    public static string ToCardinal(this uint unit) => CardinalConverter.ToCardinalName(unit);
}