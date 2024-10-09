using System.Text;

namespace WPF_Dusza.Utils
{
    public static class Extension
    {
        public static string DisplayList(this List<string> list)
        {
            StringBuilder stringBuilder = new();
            foreach (string item in list) 
            {
                stringBuilder.AppendLine(item);
            }
            return stringBuilder.ToString();
        }
    }
}
