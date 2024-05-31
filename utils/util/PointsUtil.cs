
public static class PointsUtil
{
    internal static int GetScore(Dictionary<int, int> points)
    {
        int sum = 0;
        List<int> values = new List<int>(points.Values);
        foreach (int value in values)
        {
            sum += value;
        }
        return sum / values.Count;
    }
}