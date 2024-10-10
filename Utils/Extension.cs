using System.Text;
using WPF_MVVM_Template.Models;

namespace WPF_Dusza.Utils
{
    public static class Extension
    {
        public static string DisplayList(this List<Participant> list)
        {
            StringBuilder stringBuilder = new();
            foreach (Participant item in list) 
            {
                stringBuilder.AppendLine(item.Name);
            }
            return stringBuilder.ToString();
        }
    }
}
