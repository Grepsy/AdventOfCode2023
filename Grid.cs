using System.Collections;
using System.Text;

public class Grid<T> : IEnumerable<Grid<T>> {
    public readonly T[][] Data;
    public readonly Vec Pos;
    public int X => Pos.X;
    public int Y => Pos.Y;
    public int LengthY => Data.Length;
    public int LengthX => Data[0].Length;
    public bool InRange => Pos.X >= 0 && Pos.X < LengthX && Pos.Y >= 0 && Pos.Y < LengthY;
    public T? Value {
        get => InRange ? Data[Pos.Y][Pos.X] : default;
        set { if (InRange && value != null) Data[Pos.Y][Pos.X] = value; }
    }

    public Grid(IEnumerable<IEnumerable<T>> grid) : this(grid.Select(x => x.ToArray()).ToArray(), (0, 0)) { }

    private Grid(T[][] data, Vec offset) {
        Data = data;
        Pos = offset;
    }

    public Grid<T> this[int x, int y] => new(Data, Pos + (x, y));
    public Grid<T> this[Vec offset] => new(Data, Pos + offset);
    public static implicit operator T?(Grid<T> grid) => grid.Value;

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    public IEnumerator<Grid<T>> GetEnumerator() {
        var grid = this;
        var list = from y in Enumerable.Range(0, grid.LengthY)
                   from x in Enumerable.Range(0, grid.LengthX)
                   select grid[x, y];
        return list.GetEnumerator();
    }

    public IEnumerable<Grid<T>> CardinalNeighbors() => new[] {
        this[0, -1],
        this[1, 0],
        this[0, 1],
        this[-1, 0],
    }.Where(x => x.InRange);

    public IEnumerable<Grid<T>> AdjacentNeighbors() => new[] {
        this[-1, -1], // Top-left
        this[-1, 0],  // Top
        this[-1, 1],  // Top-right
        this[0, -1],  // Left
        this[0, 1],   // Right
        this[1, -1],  // Bottom-left
        this[1, 0],   // Bottom
        this[1, 1]    // Bottom-right
    }.Where(x => x.InRange);

    public string ToStringValue() => Value?.ToString() ?? "";

    public override string ToString() {
        var sb = new StringBuilder();
        for (var y = 0; y < Data.Length; y++) {
            for (var x = 0; x < Data[0].Length; x++) {
                sb.Append((Y == y && X == x) ? '@' : Data[y][x]);
            }
            sb.AppendLine();
        }

        return sb.ToString();
    }
}