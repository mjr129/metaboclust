using System;
using System.Collections.Generic;
using MetaboliteLevels.Utilities;
using Microsoft.VisualBasic.FileIO;

namespace MetaboliteLevels.DataLoader
{
    class Matrix<T>
    {
        public readonly string Title;
        public readonly string[] RowNames;
        public readonly string[] ColNames;
        public readonly T[,] Data;
        public readonly int NumRows;
        public readonly int NumCols;
        private DataTypes dataType;

        enum DataTypes
        {
            Double,
            Integer,
            String,
        }

        public Matrix(string title, int rows, int cols)
        {
            SetType();
            Title = title;
            RowNames = new string[rows];
            ColNames = new string[cols];
            Data = new T[rows, cols];
        }

        private void SetType()
        {
            if (typeof(T) == typeof(double))
            {
                dataType = DataTypes.Double;
            }
            else if (typeof(T) == typeof(int))
            {
                dataType = DataTypes.Integer;
            }
            else if (typeof(T) == typeof(string))
            {
                dataType = DataTypes.String;
            }
            else
            {
                throw new InvalidOperationException("Error");
            }
        }

        public T this[int row, int col]
        {
            get { return Data[row, col]; }
        }

        public Matrix(string fileName, bool hasRowNames, bool hasColNames, IProgressReporter progress)
        {
            UiControls.Assert(!string.IsNullOrWhiteSpace(fileName), "Filename is missing.");

            Title = fileName;

            SetType();

            // INITIAL READ TO GET SIZE OF DATA   
            using (TextFieldParser sr = new TextFieldParser(fileName))
            {
                sr.Delimiters = new[] { "," };
                sr.HasFieldsEnclosedInQuotes = true;

                if (hasColNames)
                {
                    sr.ReadLine();
                }

                string[] fields = sr.ReadFields();

                NumRows++;

                NumCols = hasRowNames ? fields.Length - 1 : fields.Length;

                while (!sr.EndOfData)
                {
                    string line = sr.ReadLine();

                    if (line.Length == 0)
                    {
                        break;
                    }

                    NumRows++;
                }
            }

            Data = new T[NumRows, NumCols];
            RowNames = new string[NumRows];

            using (TextFieldParser sr = new TextFieldParser(fileName))
            {
                sr.Delimiters = new[] { "," };
                sr.HasFieldsEnclosedInQuotes = true;

                // READ OR CREATE COLUMN NAMES
                if (hasColNames)
                {
                    // First row name is blank
                    string[] colNameData = sr.ReadFields();

                    if (hasRowNames)
                    {
                        ColNames = new string[colNameData.Length - 1];
                        Array.Copy(colNameData, 1, ColNames, 0, colNameData.Length - 1);
                    }
                    else
                    {
                        ColNames = colNameData;
                    }

                    NumCols = ColNames.Length;
                }
                else
                {
                    ColNames = new string[NumCols];

                    for (int col = 0; col < NumCols; col++)
                    {
                        ColNames[col] = "C" + col;
                    }
                }

                Assert(ColNames.Length == NumCols, "The number of column names (" + ColNames.Length + ") is different from number of columns (" + NumCols + "). Check the CSV file for errors.");

                // READ DATA ENTRIES
                int rowIndex = 0;

                while (!sr.EndOfData)
                {
                    string[] lineData = sr.ReadFields();

                    if (lineData.Length == 1 && lineData[0] == "")
                    {
                        break;
                    }

                    int dataCol;

                    if (hasRowNames)
                    {
                        dataCol = 1;
                        RowNames[rowIndex] = lineData[0];
                    }
                    else
                    {
                        dataCol = 0;
                        RowNames[rowIndex] = "R" + rowIndex;
                    }

                    int colIndex = 0;

                    for (int c = dataCol; c < lineData.Length; c++)
                    {
                        Data[rowIndex, colIndex] = ReadData(lineData[c]);
                        colIndex++;
                    }

                    Assert(colIndex == NumCols, "The number of columns is different for row " + rowIndex + " of the CSV file \"" + Title + "\". Check the CSV file for errors.");

                    if (progress != null)
                    {
                        progress.ReportProgress(rowIndex, NumRows);
                    }

                    rowIndex++;
                }

                Assert(rowIndex == NumRows, "Did not load all data for the CSV file \"" + Title + "\". Check the CSV file for errors.");
            }
        }

        private dynamic ReadData(string data)
        {
            switch (dataType)
            {
                case DataTypes.Double:
                    {
                        double r;

                        if (double.TryParse(data, out r))
                        {
                            return r;
                        }
                    }
                    break;

                case DataTypes.Integer:
                    {
                        int r;

                        if (int.TryParse(data, out r))
                        {
                            return r;
                        }
                    }
                    break;

                case DataTypes.String:
                    return data;

                default:
                    return default(T);
            }

            return default(T);
        }

        /// <summary>
        /// Gets the index of the column with any of the specified title(s).
        /// </summary>
        /// <param name="colTitles">Comma delimited titles</param>
        public int ColIndex(string colTitles)
        {
            int n = OptionalColIndex(colTitles);

            if (n == -1)
            {
                throw new KeyNotFoundException("Expected to find a COLUMN with any of the titles {" + colTitles + "} in the \"" + Title + "\" data but there are NO matching columns. The names are not case sensitive. Check the CSV file for errors and make sure the settings are correct.");
            }

            return n;
        }

        /// <summary>
        /// Gets the index of the column with any of the specified title(s).
        /// </summary>
        /// <param name="colTitles">Comma delimited titles</param>
        public int OptionalColIndex(string colTitles)
        {
            if (colTitles.Contains(","))
            {
                int result = -1;

                foreach (string colTitle in colTitles.Split(','))
                {
                    int n = OptionalColIndex(colTitle);

                    if (n != -1)
                    {
                        if (result != -1)
                        {
                            throw new KeyNotFoundException("Expected to find a COLUMN with any of the titles {" + colTitles + "} in the \"" + Title + "\" data but there are MULTIPLE matching columns. The names are not case sensitive. Check the CSV file for errors and make sure the settings are correct.");
                        }

                        result = n;
                    }
                }

                return result;
            }

            for (int n = 0; n < ColNames.Length; n++)
            {
                if (ColNames[n].ToUpper() == colTitles.ToUpper())
                {
                    return n;
                }
            }

            return -1;
        }

        public int RowIndex(string rowTitle)
        {
            for (int n = 0; n < RowNames.Length; n++)
            {
                if (RowNames[n] == rowTitle)
                {
                    return n;
                }
            }

            throw new KeyNotFoundException("Expected to find a ROW with the title \"" + rowTitle + "\" in the \"" + Title + "\" data. Check the CSV file for errors.");
        }

        private static void Assert(bool condition, string message)
        {
            if (!condition)
            {
                throw new FormatException(message);
            }
        }

        public T[] ExtractRow(int row)
        {
            T[] target = new T[NumCols];

            for (int col = 0; col < NumCols; col++)
            {
                target[col] = Data[row, col];
            }

            return target;
        }

        public T[] ExtractColumn(int col)
        {
            T[] target = new T[NumRows];

            for (int row = 0; row < NumRows; row++)
            {
                target[row] = Data[row, col];
            }

            return target;
        }

        internal string NextUid()
        {
            throw new NotImplementedException();
        }

        internal void WriteMeta(int row, MetaInfoCollection collection, MetaInfoHeader headers)
        {
            for (int col = 0; col < NumCols; col++)
            {
                collection.Write(headers, ColNames[col], Data[row, col].ToString());
            }
        }
    }
}
