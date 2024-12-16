using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using AdventOfCode2024.Extensions;

namespace AdventOfCode2024;

[Challenge(Name = "Day06")]
public class Day06Challenge : Challenge
{
    private const char obstruction = '#';

    public override long Solution1(string inputPath)
    {
        var (position, map) = ParseInput(inputPath);
        var positions = new HashSet<Position>(new Part1EqualityComparer()) {position};
        position.MoveForward();
        while (true)
        {
            if (!position.IsValid(map))
            {
                break;
            }

            if (map[position.Y][position.X] != obstruction)
            {
                positions.Add(position);
            }
            else
            {
                position.MoveBack();
                position.MoveRight();
            }

            position.MoveForward();
        }

        return positions.Count();
    }


    public override long Solution2(string inputPath)
    {
        var (position, map) = ParseInput(inputPath);
        var initialPosition = new Position(position.X, position.Y);
        var positions = new HashSet<Position>(new Part2EqualityComparer());
        var obstructionPositions = 0;
        Position obstructionPosition = new Position(0, 0);

        while (true)
        {
            positions.Add(position);
            position.MoveForward();

            while (position.IsValid(map) &&
                   (map[position.Y][position.X] == obstruction ||
                    (position.X == obstructionPosition.X && position.Y == obstructionPosition.Y)))
            {
                position.MoveBack();
                position.MoveRight();
                position.MoveForward();
            }

            if (!position.IsValid(map))
            {
                if (!AdvanceObstructionPosition(ref obstructionPosition, map))
                    break;

                position = initialPosition;
                positions.Clear();
                continue;
            }

            if (!positions.Add(position))
            {
                obstructionPositions++;
                if (!AdvanceObstructionPosition(ref obstructionPosition, map))
                    break;

                position = initialPosition;
                positions.Clear();
            }
        }

        return obstructionPositions;
    }

    private static bool AdvanceObstructionPosition(ref Position obstructionPosition, string[] map)
    {
        if (obstructionPosition.X == map[0].Length - 1)
            obstructionPosition = new Position(0, obstructionPosition.Y + 1);
        else
            obstructionPosition = new Position(obstructionPosition.X + 1, obstructionPosition.Y);

        return obstructionPosition.IsValid(map);
    }


    private (Position, string[]) ParseInput(string inputPath)
    {
        if (!File.Exists(inputPath))
        {
            throw new FileNotFoundException();
        }

        var map = File.ReadAllLines(inputPath);
        int x = -1;
        int y = -1;

        foreach (var (index, line) in map.Index())
        {
            x = line.IndexOf('^');
            if (x > -1)
            {
                y = index;
                break;
            }
        }

        return (new Position(x, y), map);
    }
}

[DebuggerDisplay("X: {X}, Y: {Y}")]
public struct Position
{
    private Direction direction = Direction.Up;

    public Direction Direction => direction;

    public Position(int x, int y)
    {
        this.X = x;
        this.Y = y;
    }

    public int X { get; private set; }
    public int Y { get; private set; }

    public void MoveForward()
    {
        var (x, y) = direction switch
        {
            Direction.Up => (0, -1),
            Direction.Right => (1, 0),
            Direction.Left => (-1, 0),
            Direction.Down => (0, 1),
            _ => throw new InvalidOperationException()
        };

        X += x;
        Y += y;
    }

    public void MoveBack()
    {
        var (x, y) = direction switch
        {
            Direction.Up => (0, 1),
            Direction.Right => (-1, 0),
            Direction.Left => (1, 0),
            Direction.Down => (0, -1),
            _ => throw new InvalidOperationException()
        };

        X += x;
        Y += y;
    }

    public void MoveRight()
    {
        direction = (Direction) (direction + 1);
        if ((int) direction > 3)
        {
            direction = Direction.Up;
        }
    }

    public bool IsValid(string[] map)
    {
        return X >= 0 && X < map[0].Length
                      && Y >= 0 && Y < map.Length;
    }

    public override string ToString()
    {
        return $"X: {X}, Y: {Y}";
    }
}

public class Part1EqualityComparer : IEqualityComparer<Position>
{
    public bool Equals(Position x, Position y)
    {
        return x.X == y.X && x.Y == y.Y;
    }

    public int GetHashCode([DisallowNull] Position obj)
    {
        return HashCode.Combine(obj.X, obj.Y);
    }
}

public class Part2EqualityComparer : IEqualityComparer<Position>
{
    public bool Equals(Position x, Position y)
    {
        return x.X == y.X && x.Y == y.Y && x.Direction == y.Direction;
    }

    public int GetHashCode(Position obj)
    {
        return HashCode.Combine(obj.X, obj.Y, obj.Direction);
    }
}

public enum Direction
{
    Up = 0,
    Right,
    Down,
    Left
}