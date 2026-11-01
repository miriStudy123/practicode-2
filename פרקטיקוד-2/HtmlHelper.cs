using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.IO;
namespace פרקטיקוד_2
{
    public class HtmlHelper
    {
        private static readonly HtmlHelper instance=new HtmlHelper();
        public static HtmlHelper Instance => instance;
        public string[] Htmltags { get; set; }
        public string[] HtmlVoidtasg {  get; set; }

        public HtmlHelper() {
            var context = File.ReadAllText("HtmlTags.json");
            var context2 = File.ReadAllText("HtmlVoidTags.json");
            Htmltags = JsonSerializer.Deserialize<string[]>(context);
            HtmlVoidtasg = JsonSerializer.Deserialize<string[]>(context2);
        }
        
    }
}
