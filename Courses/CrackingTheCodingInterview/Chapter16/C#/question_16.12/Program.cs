using System;
using System.Xml;

namespace question_16._12
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }

        static string Encode(XmlAttribute attribute, string currentString)
        {
            var str = Encode(attribute.Name, currentString);
            str = Encode(attribute.Value, currentString);
            return str;
        }

        static string Encode(String v, string str)
        {
            str += v;
            str += " ";
            return str;
        }

        static string Encode(XmlElement root, string currentString)
        {
            var str = Encode(root.Name, currentString);
            foreach (XmlAttribute a in root.Attributes)
            {
                str += Encode(a, str);
            }

            str += Encode("0", str);

            if (root.Value != null && root.Value != "")
            {
                str += Encode(root.Value, str);
            }
            else
            {
                foreach (XmlElement e in root.ChildNodes)
                {
                    str += Encode(e, str);
                }
            }
            return str;
        }
    }
}
