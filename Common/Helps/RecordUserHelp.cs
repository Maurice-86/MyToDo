using MyToDo.Common.Models;
using Newtonsoft.Json;
using System.IO;

namespace MyToDo.Common.Helps
{
    public class RecordUserHelp
    {
        private static readonly string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "userInfo.json");

        public static void SaveUser(RecordUserModel user)
        {
            var json = JsonConvert.SerializeObject(user);
            File.WriteAllText(filePath, json);
        }

        public static RecordUserModel? LoadUser()
        {
            if (!File.Exists(filePath))
                return null;

            var json = File.ReadAllText(filePath);
            return JsonConvert.DeserializeObject<RecordUserModel>(json);
        }
    }
}
