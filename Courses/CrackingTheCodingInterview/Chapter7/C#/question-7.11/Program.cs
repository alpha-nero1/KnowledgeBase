using System;

namespace question_7._11
{
    class Program
    {
        static void Main(string[] args)
        {
            var root = new Directory("root");

            root.Add(new File<string>("hello.txt", "content..."));
            var documents = new Directory("documents");
            root.Add(documents);

            documents.Add(new File<string>("first-doc.pdf", ""));

            root.Print();
        }
    }
}
