using Newtonsoft.Json;
using System.IO;

namespace SGSVostokLimited
{
    public class WorkingWithJSON
    {
        //путь к json
        private readonly string PATH;

        public WorkingWithJSON(string path)
        {
            PATH = path;
        }

        //сохраняем json
        public void SaveData<T>(T anyClass)
        {
            if (!File.Exists(PATH))
            {
                File.CreateText(PATH).Dispose();
            }

            using (StreamWriter writer = File.AppendText(PATH))
            {
                string serialize = JsonConvert.SerializeObject(anyClass);

                writer.WriteLine(serialize);
            }
        }
    }
}
