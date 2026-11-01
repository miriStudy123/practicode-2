using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace פרקטיקוד_2
{
    public class Selector
    {
        public string TagName { get; set; }
        public string Id { get; set; }
        public List<string> Classes { get; set; }
        public Selector Parent { get; set; }
        public Selector Child { get; set; }
        public Selector()
        {
            Classes = new List<string>();
        }

        public static Selector convertSelector(string s)
        {
            string[] parts = s.Split(' ');
            Selector root = new Selector();

            var instance = HtmlHelper.Instance;

            var match1 = new Regex("([.#]?)([^.#]+)").Matches(parts[0]);
            foreach (Match match2 in match1)
            {
                if (match2.Groups[1].Value == "#")
                {
                    root.Id = match2.Groups[2].Value;
                }
                else if (match2.Groups[1].Value == ".")
                {
                    root.Classes.Add(match2.Groups[2].Value);
                }
                else
                {
                    if (instance.Htmltags.Contains(match2.Groups[2].Value))
                    {
                        root.TagName = match2.Groups[2].Value;
                    }


                }


            }

            Selector current = root;


            for (int i = 1; i < parts.Length; i++)
            {
                Selector s1 = new Selector();
                var match3 = new Regex("([.#]?)([^.#]+)").Matches(parts[i]);
                foreach (Match match2 in match3)
                {
                    if (match2.Groups[1].Value == "#")
                    {
                        s1.Id = match2.Groups[2].Value;
                    }
                    else if (match2.Groups[1].Value == ".")
                    {
                        s1.Classes.Add(match2.Groups[2].Value);
                    }
                    else
                    {
                        if (instance.Htmltags.Contains(match2.Groups[2].Value))
                        {
                            s1.TagName = match2.Groups[2].Value;
                        }

                    }


                }
                current.Child = s1;
                s1.Parent = current;
                current = s1;
            }
            return root;
        }



    }
}

