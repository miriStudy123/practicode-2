using System.Text.RegularExpressions;
using פרקטיקוד_2;

static async Task<string> Load(string url)
{
    HttpClient client = new HttpClient();
    var response = await client.GetAsync(url);
    var html = await response.Content.ReadAsStringAsync();
    return html;
}
var Html = await Load("http://localhost:5173/");
Console.WriteLine(Html.Substring(0, Math.Min(500, Html.Length)));
//var withoutSpace = new Regex("\\s").Replace(Html, "");
var htmlLines = new Regex("<(.*?)>").Split(Html).Where(s => s.Length != 0);
var instance = HtmlHelper.Instance;
HtmlElement root = new HtmlElement();
root.name = "Html";
HtmlElement current = root;
bool flag = false;
foreach (var line in htmlLines)
{
    if (line.Length == 0)
        continue;
    int spaceIndex = line.IndexOf(' ');
    string src,newline="";
    if (spaceIndex != -1)
    {
        src = line.Substring(0, spaceIndex);
        newline = line.Substring(spaceIndex + 1);        
    }
    else
    {
        src = line;

    }
    if (src[0]=='/')
    {
        src = src.Substring(1,src.Length-1);
        flag = true;
    }
    if (instance.Htmltags.Contains(src))
    {
        
        HtmlElement c = new HtmlElement();
       var Items= new Regex("(\\w+)\\s*=\\s*\"([^\"]*)\"").Matches(newline);
        foreach (Match item in Items) 
        {
            string key = item.Groups[1].Value;
            string value = item.Groups[2].Value;
            if (key == "class")
            {
                c.Classes.AddRange(value.Split(' '));
            }
            else if (key == "id")
            {
                c.id= value;
            }
            else
            {
                c.Attributes.Add(key + "=" + value);
            }
        }
        if(src.Equals("html", StringComparison.OrdinalIgnoreCase)&&flag)
        { break; }
        
            if (!flag)
            {
                c.name = src;
                current.children.Add(c);
                c.Parent = current;

                if (!instance.HtmlVoidtasg.Contains(src) && !line.TrimEnd().EndsWith("/"))
                {
                    current = c;
                }

            }
            else
            {
                
                    if (current.Parent != null)
                        current = current.Parent;
               
            }
            flag = false;
       
        
      

    }
    else
    {
        current.InnerHtml += line;
    }
}
Console.WriteLine("כל האלמנטים בעץ:");

foreach (var item in root.Descendants())
{
    Console.WriteLine(item.name);
}
foreach (var item in root.Descendants())
{
    Console.WriteLine(
        $"{item.name} | אבא: {item.Parent?.name ?? "אין"}");
}
Selector selector = new Selector
{
    Classes = new List<string> { "input" }
};

var results = root.FindBySelector(selector);

Console.WriteLine("נמצאו: " + results.Count);

foreach (var item in results)
{
    Console.WriteLine(item.name);
}