namespace HexagramNS;

public class Values
{
    private bool[,] values;
    /// <summary>
    /// Gets a value indicating whether the hexagram values have changed.
    /// This property is set to true when SetValue, SetValues, SetHexagramRow or InitValues methods are called.
    /// </summary>
    public bool Changed { get; private set; }

    public int RowCount { get => values.GetLength(0); }

    public Values(int rowCount = Hexagram.RowCount, int colCount = Hexagram.ColCount)
    { values = new bool[rowCount, colCount];
    Changed = false;}

    // TODO contsructor that takes an 2 dimensional array and a bool function to initialize the array

    // Example usage:
    public bool GetValue(int row, int col) { return values[row, col]; }



/// <summary>
/// A hexagram line is changing when a row has 3 columns with the same values 
/// </summary>
/// <param name="row"></param>
/// <param name="col"></param>
/// <param name="value"></param>
    public void SetValue(int row, int col, bool value) { values[row, col] = value; 
    Changed = true;}

    public void SetValues<T>(T[] array, Func<T, int, int, bool> func)
    {
        for (int i = 0; i < array.Length; i++)
        {
            int row = i / values.GetLength(1);
            int col = i % values.GetLength(1);
            values[row, col] = func(array[i], row, col);
        }
        Changed = true;
    }

    public void SetHexagramRow(int hexagramRow, int value)
    {
        switch (value)
        {
            // Changing line
            case 6:
                SetValue(hexagramRow, 0, false);
                SetValue(hexagramRow, 1, false);
                SetValue(hexagramRow, 2, false);
                break;
            case 7:
                SetValue(hexagramRow, 0, true);
                SetValue(hexagramRow, 1, true);
                SetValue(hexagramRow, 2, false);
                break;
            case 8:
                SetValue(hexagramRow, 0, false);
                SetValue(hexagramRow, 1, false);
                SetValue(hexagramRow, 2, true);
                break;
            // Changing line
            case 9:
                SetValue(hexagramRow, 0, true);
                SetValue(hexagramRow, 1, true);
                SetValue(hexagramRow, 2, true);
                break;
        }
    }
    
    /// <summary>
    /// Initializes the values in a two-dimensional array based on a given function.
    /// </summary>
    /// <typeparam name="T">The type of elements in the array.</typeparam>
    /// <param name="array">The two-dimensional array to initialize.</param>
    /// <param name="func">The function used to determine the value for each element in the array.</param>
    /// <exception cref="ArgumentNullException">Thrown when the array or func parameter is null.</exception>
    /// <returns>The updated Values object.</returns>
    public Values InitValues<T>(T[,] array, Func<T, int, int, bool> func)
    {
        if (array == null)
        {
            throw new ArgumentNullException(nameof(array));
        }

        if (func == null)
        {
            throw new ArgumentNullException(nameof(func));
        }

        int rows = array.GetLength(0);
        int cols = array.GetLength(1);

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                values[i, j] = func(array[i, j], i, j);
            }
        }
        Changed = true;
        return this;
    }

    public void UpdateValues<T>(T[,] array, Func< int, int, bool, T> func)
    {
        int rows = array.GetLength(0);
        int cols = array.GetLength(1);

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                array[i, j] = func(i, j , values[i, j]);
            }
        }
    }
}
