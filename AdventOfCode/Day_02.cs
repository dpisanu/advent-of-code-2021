namespace AdventOfCode;

public class Day_02 : BaseDay
{
    private readonly IEnumerable<(Direction, long)> _input;

    enum Direction
    {
        forward,
        down,
        up
    }

    private abstract class BasePosition
    {
        public BasePosition(long startValue)
        {
            X = startValue;
            Y = startValue;
        }

        public long X { internal set; get; }
        public long Y { internal set; get; }

        public abstract void Forward(long points);

        public abstract void Down(long points);

        public abstract void Up(long points);
    }

    private class Position : BasePosition
    {
        public Position(long startValue) : base(startValue)
        { }

        public override void Forward (long points)
        {
            X += points;
        }

        public override void Down(long points)
        {
            Y += points;
        }

        public override void Up(long points)
        {
            Y -= points;
        }
    }

    private class PositionWithAim : BasePosition
    {
        public PositionWithAim(long startValue) : base (startValue) 
        {
            Aim = startValue;
        }

        public long Aim { internal set; get; }

        public override void Forward(long points)
        {
            X += points;
            Y += Aim * points;
        }

        public override void Down(long points)
        {
            Aim += points;
        }

        public override void Up(long points)
        {
            Aim -= points;
        }
    }

    public Day_02()
    {
        _input = ParseInput(InputFilePath);
    }

    /// <summary>
    ///     Calculate the horizontal position and depth you would have after following the planned course. 
    ///     What do you get if you multiply your final horizontal position by your final depth?
    /// </summary>
    public override ValueTask<string> Solve_1() => Solve1();

    /// <summary>
    ///     In addition to horizontal position and depth, you'll also need to track a third value, aim, which also starts at 0.
    ///     - down X increases your aim by X units.
    ///     - up X decreases your aim by X units.
    ///     - forward X does two things:
    ///     -- It increases your horizontal position by X units.
    ///     -- It increases your depth by your aim multiplied by X.
    /// </summary>
    public override ValueTask<string> Solve_2() => Solve2();

    private ValueTask<string> Solve1 ()
    {
        var depth = CalculatePosition(new Position(0L));
        return new($"Solution to {ClassPrefix} {CalculateIndex()}, part 1 is {depth}");
    }

    private ValueTask<string> Solve2()
    {
        var depth = CalculatePosition(new PositionWithAim(0L));
        return new($"Solution to {ClassPrefix} {CalculateIndex()}, part 2 is {depth}");
    }

    private long CalculatePosition(BasePosition position)
    {
        foreach (var direction in _input)
        {
            switch (direction.Item1)
            {
                case Direction.forward:
                    position.Forward(direction.Item2);
                    break;
                case Direction.up:
                    position.Up(direction.Item2);
                    break;

                case Direction.down:
                    position.Down(direction.Item2);
                    break;
            }
        }

        return position.X * position.Y;
    }

    private static IEnumerable<(Direction, long)> ParseInput(string path)
    {
        var directionMap = new List<(Direction, long)>();
        var fileConent = File.ReadAllText(path).Split('\n', StringSplitOptions.RemoveEmptyEntries);
        foreach (var line in fileConent.Select(x => x.Split(' ')))
        {
            var direction = line.First();
            var steps = Convert.ToInt64(line.Last());

            switch (direction)
            {
                case "forward":
                    directionMap.Add(new (Direction.forward, steps));
                    break;
                case "down":
                    directionMap.Add(new(Direction.down, steps));
                    break;

                case "up":
                    directionMap.Add(new(Direction.up, steps));
                    break;

                default:
                    directionMap.Add(default);
                    break;
            }            
        }

        return directionMap;
    }
}