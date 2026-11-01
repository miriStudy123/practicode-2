using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace פרקטיקוד_2
{
    public static class HtmlElementExtensions
    {

        public static HashSet<HtmlElement>FindBySelector(this HtmlElement element, Selector selector)
        {
            HashSet<HtmlElement> results = new HashSet<HtmlElement>();
            FindRecursive(element, selector, results);
            return results;
        }
        private static void FindRecursive(HtmlElement element, Selector selector, HashSet<HtmlElement> results)
        {
            List<HtmlElement> list = element.Descendants().ToList();
            foreach (HtmlElement item in list)
            {

                if (HtmlElement.Matches(item, selector))
                {
                    if (selector.Child == null)
                    {
                        results.Add(item);
                    }
                    else
                    {
                        FindRecursive(item, selector.Child, results);
                    }
                }

            }
        }
    }
}
