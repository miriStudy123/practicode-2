using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace פרקטיקוד_2
{
    public class HtmlElement
    {
        public string id { get; set; }
        public string name { get; set; }
        public List<string> Attributes { get; set; }
        public List<string> Classes { get; set; }
        public string InnerHtml { get; set; }
        public HtmlElement Parent { get; set; }
        public List<HtmlElement> children { get; set; }

        public HtmlElement()
        {
            children = new List<HtmlElement>();
            Attributes = new List<string>();
            Classes = new List<string>();
        }


        public IEnumerable<HtmlElement> Descendants()
        {
            Queue<HtmlElement> q = new Queue<HtmlElement>();
            foreach (HtmlElement child in children)
            {
                q.Enqueue(child);
            }
            while (q.Count != 0)
            {
                HtmlElement th = q.Dequeue();
                yield return th;
                for (int i = 0; i < th.children.Count; i++)
                {
                    q.Enqueue(th.children[i]);
                }
            }
        }
        public static bool Matches(HtmlElement element, Selector selector)
        {

            if (selector.TagName != null &&
                element.name != selector.TagName)
                return false;


            if (selector.Id != null &&
                element.id != selector.Id)
                return false;


            foreach (string className in selector.Classes)
            {
                if (!element.Classes.Contains(className))
                    return false;
            }

            return true;
        }
        public IEnumerable<HtmlElement> Ancestors()
        {
            HtmlElement current = Parent;

            while (current != null)
            {
                yield return current;
                current = current.Parent;
            }
        }
        
       
    }
}
