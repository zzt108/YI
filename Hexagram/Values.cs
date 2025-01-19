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
    {
        values = new bool[rowCount, colCount];
        Changed = false;
    }

    // TODO contsructor that takes an 2 dimensional array and a bool function to initialize the array

    // Example usage:
    public bool GetValue(int row, int col) { return values[row, col]; }


    /// <summary>
    /// Sets a value in the values array. 
    /// </summary>
    /// <param name="index"></param>
    /// <param name="col"></param>
    /// <param name="value"></param>
    public void SetValue(int index, int col, bool value)
    {
        values[index, col] = value;
        Changed = true;
    }

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

/// <summary>
/// Sets a values row based on the line value.
/// </summary>
/// <param name="index"></param>
/// <param name="value"></param>
    public void SetIndexRow(int index, int value)
    {
        // see [three-coin method](https://en.wikipedia.org/wiki/I_Ching_divination)
        switch (value)
        {
            // Changing line
            case 6: //yin changing into yang
                SetValue(index, 0, false);
                SetValue(index, 1, false);
                SetValue(index, 2, false);
                break;
            case 7: //yang unchanging 
                SetValue(index, 0, false);
                SetValue(index, 1, false);
                SetValue(index, 2, true);
                break;
            case 8: //yin unchanging 
                SetValue(index, 0, false);
                SetValue(index, 1, true);
                SetValue(index, 2, true);
                break;
            // Changing line
            case 9: //yang changing into yin
                SetValue(index, 0, true);
                SetValue(index, 1, true);
                SetValue(index, 2, true);
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

    public void UpdateValues<T>(T[,] array, Func<int, int, bool, T> func)
    {
        int rows = array.GetLength(0);
        int cols = array.GetLength(1);

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                array[i, j] = func(i, j, values[i, j]);
            }
        }
    }

    internal void SetIndexRow(int index, bool isYang, bool isChanging)
    {
        // use SetHexagramRow method
        if (isChanging)
        {
            SetIndexRow(index, isYang ? 9 : 6);
        }
        else
        {
            SetIndexRow(index, isYang ? 7 : 8);
        }
    }
}
