using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.IO;
using Newtonsoft.Json;
using System.Windows;

namespace game_of_life
{
    public class State
    {
        public bool[,] Generation { get; set; }
        public int CellsDied { get; set; }
        public int CellsBorn { get; set; }
        public int Generations { get; set; }
    }

    public class StateHandler
    {
        public static void SaveStateToJson(string filename, bool[,] generation, int cellsDied, int cellsBorn, int generations)
        {
            var data = new State
            {
                Generation = generation,
                CellsDied = cellsDied,
                CellsBorn = cellsBorn,
                Generations = generations
            };

            string json = JsonConvert.SerializeObject(data, Formatting.Indented);
            File.WriteAllText(filename, json);
        }

        public static State? LoadStateFromJson(string filename)
        {
            try
            {
                string json = File.ReadAllText(filename);
                var data = JsonConvert.DeserializeObject<State>(json);
                return data;
            }
            catch (FileNotFoundException)
            {
                MessageBox.Show($"[ERROR] The file was not found: {filename}");
                return null;
            }
            catch (JsonException ex)
            {
                MessageBox.Show($"[ERROR] Error parsing JSON: {ex.Message}");
                return null;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"[ERROR] An unexpected error occurred: {ex.Message}");
                return null;
            }
        }
    }
}
