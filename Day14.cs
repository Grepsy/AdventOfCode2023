public static class Day14 {
    public static long Part1() {
        var grid = File.ReadAllLines("day14.txt").Select(y => y.ToArray()).ToArray();
        grid = Tilt(grid.Transpose().Select(x => x.ToArray()).ToArray());

        return new Grid<char>(grid).Sum(x => x == 'O' ? grid.Length - x.Pos.X : 0);
    }

    public static long Part2() {
        var grid = File.ReadAllLines("day14.txt").Select(y => y.ToArray()).ToArray();
        grid = grid.Transpose().Select(x => x.ToArray()).ToArray();

        var history = new List<char[][]>();
        long i = 1;
        for (; i < 10000; i++) {
            for (var j = 0; j < 4; j++) {
                grid = Tilt(grid);
                history.Add(grid);
                grid = RotateCCW(grid);
            }

            if (i > 0 && history.Index().FirstOrDefault(x => x.Value.IsSeqEqual(grid)) is { Key: int loop, Value: char[][] }) {
                Console.WriteLine($"Loop at {i} - {loop}");

                for (var k = 0; k < 5; k++) {
                    $"Tilt {k}".Log();
                    new Grid<char>(grid).Sum(x => x == 'O' ? grid.Length - x.Pos.X : 0).Log();
                    grid = Tilt(grid);
                    grid = RotateCCW(grid);
                }

                return new Grid<char>(grid).Sum(x => x == 'O' ? grid.Length - x.Pos.X : 0);
            }
            //new Grid<char>(grid.Transpose().Select(x => x.ToArray()).ToArray()).Log();
        }

        return 0;
    }

    public static char[][] Tilt(char[][] grid) {
        for (var y = 0; y < grid.Length; y++) {
            var row = grid[y];
            for (var x = 0; x < row.Length - 1; x++) {
                if (row[x] == '.' && row[x + 1] == 'O') {
                    row[x] = 'O';
                    row[x + 1] = '.';
                    x = -1;
                }
            }
        }
        return grid;
    }

    public static T[][] RotateCW<T>(T[][] originalArray) {
        var size = originalArray.Length;
        var rotatedArray = new T[size][];

        for (var i = 0; i < size; i++) {
            rotatedArray[i] = new T[size];
            for (var j = 0; j < size; j++) {
                rotatedArray[i][j] = originalArray[size - 1 - j][i];
            }
        }

        return rotatedArray;
    }


    public static T[][] RotateCCW<T>(T[][] originalArray) {
        var size = originalArray.Length;
        var rotatedArray = new T[size][];

        for (var i = 0; i < size; i++) {
            rotatedArray[i] = new T[size];
            for (var j = 0; j < size; j++) {
                rotatedArray[i][j] = originalArray[j][size - 1 - i];
            }
        }

        return rotatedArray;
    }
}