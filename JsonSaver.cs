using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.IO;
using Newtonsoft.Json;

namespace game_of_life
{
    public class JsonSaver
    {
        private static bool[][] ConvertToJaggedArray(bool[,] twoDimensionalArray)
        {
            int rows = twoDimensionalArray.GetLength(0);
            int cols = twoDimensionalArray.GetLength(1);
            bool[][] jaggedArray = new bool[rows][];

            for (int x = 0; x < rows; x++)
            {
                jaggedArray[x] = new bool[cols];
                for (int y = 0; y < cols; y++)
                {
                    jaggedArray[x][y] = twoDimensionalArray[x, y];
                }
            }
            return jaggedArray;
        }

        public static void SaveBoolArrayToJson(bool[,] boolArray, string filePath)
        {
            bool[][] jaggedArray = ConvertToJaggedArray(boolArray);
            string jsonString = JsonConvert.SerializeObject(jaggedArray, Formatting.Indented);
            File.WriteAllText(filePath, jsonString);
        }

        public static bool[,] LoadBoolArrayFromJson(string filePath)
        {
            string jsonString = File.ReadAllText(filePath);
            bool[][] jaggedArray = JsonConvert.DeserializeObject<bool[][]>(jsonString);
            int rows = jaggedArray.Length;
            int cols = jaggedArray[0].Length;
            bool[,] twoDimensionalArray = new bool[rows, cols];

            for (int x = 0; x < rows; x++)
            {
                for (int y = 0; y < cols; y++)
                {
                    twoDimensionalArray[x, y] = jaggedArray[x][y];
                }
            }
            return twoDimensionalArray;
        }
    }
}
