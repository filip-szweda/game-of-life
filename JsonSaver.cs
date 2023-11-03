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

            for (int i = 0; i < rows; i++)
            {
                jaggedArray[i] = new bool[cols];
                for (int j = 0; j < cols; j++)
                {
                    jaggedArray[i][j] = twoDimensionalArray[i, j];
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

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    twoDimensionalArray[i, j] = jaggedArray[i][j];
                }
            }
            return twoDimensionalArray;
        }
    }
}
