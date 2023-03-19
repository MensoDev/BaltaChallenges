namespace BaltaChallenges;

public static class BinarySearch
{
    public static long FindIndexByBinarySearch(this long[] numbers, long element, long unit) => 
        numbers.BinarySearchInternal(0, numbers.Length - 1, element, unit);

    private static long BinarySearchInternal(this long[] numbers, long startPosition, long lastPosition, long element, long unit)
    {
        var middleIndex = (startPosition + lastPosition) / 2;
        if (middleIndex < startPosition || middleIndex > lastPosition) throw new InvalidOperationException();

        if (IsMatch(numbers[middleIndex], unit, element)) return middleIndex;

        return element - unit * numbers[middleIndex] > 0 ?
            BinarySearchInternal(numbers, middleIndex + 1, lastPosition, element, unit) :
            BinarySearchInternal(numbers, startPosition, middleIndex - 1, element, unit);
    }

    private static bool IsMatch(long number, long unit, long element)
    {
        var result = element - unit * number;
        return result >= 0 && result < unit;
    }
}