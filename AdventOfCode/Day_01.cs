﻿namespace AdventOfCode;

public class Day_01 : BaseDay
{
    private readonly int[] _input;

    public Day_01()
    {
        _input = ParseInput(InputFilePath);
    }

    /// <summary>
    ///     How many measurements are larger than the previous measurement?
    ///     (There is no measurement before the first measurement.)
    /// </summary>
    public override ValueTask<string> Solve_1() => Solve1();

    /// <summary>
    ///     Your goal now is to count the number of times the sum of measurements 
    ///     in this sliding window increases from the previous sum.
    ///     Compare A with B, then compare B with C, then C with D, and so on. 
    ///     Stop when there aren't enough measurements left to create a new three-measurement sum.
    /// </summary>
    public override ValueTask<string> Solve_2() => Solve2();

    private ValueTask<string> Solve1 ()
    {
        int increases = IncreaseCounts(_input);
        return new($"Solution to {ClassPrefix} {CalculateIndex()}, part 1 -> {increases}");
    }

    private ValueTask<string> Solve2()
    {
        var range = 3;
        var intermediateList = new List<int>();

        for (int position = 0; position < _input.Length - 2; position++)
        {
            var threeRange = new Range(position, position + range);

            var sumOfThree = _input.Take(threeRange).Sum();
            intermediateList.Add(sumOfThree);
        }
        int increases = IncreaseCounts(intermediateList.ToArray());
        return new($"Solution to {ClassPrefix} {CalculateIndex()}, part 2 -> {increases}");
    }

    private static int[] ParseInput(string path) => 
        File.ReadAllText(path).Split('\n', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();

    private static int IncreaseCounts(int[] input)
    {
        int increases = 0;
        for (int position = 1; position < input.Length; position++)
        {
            if (input[position] > input[position - 1])
            {
                increases++;
            }
        }
        return increases;
    }
}