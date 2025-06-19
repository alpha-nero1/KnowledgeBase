using System;
using System.Collections.Generic;
using System.Linq;

namespace question_7._11
{
    // Base 'file' class.
    public abstract class FileSystemEntry<T>
    {
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
        public DateTime DisabledAt { get; set; }
        public string Name { get; set; }
        public T Data { get; set; }
        public Directory Parent { get; set; }

        public FileSystemEntry(string name, T data)
        {
            Name = name;
            Data = data;
        }
    }

    // File class.
    public class File<T> : FileSystemEntry<T>
    {
        public File(string name, T data) : base(name, data)
        { }
    }

    // Directory class.
    public class Directory : FileSystemEntry<string>
    {
        public Dictionary<string, FileSystemEntry<string>> Entries = new Dictionary<string, FileSystemEntry<string>>();
        public Directory(string name) : base(name, null)
        { }

        // List files in directory.
        public List<FileSystemEntry<string>> List()
        {
            return Entries.Values.ToList();
        }

        public void Print()
        {
            string prefix = Name;
            foreach (var f in List()) Console.WriteLine($"{prefix}/{f.Name}");
        }

        public void Delete(string name)
        {
            try
            {
                Entries.Remove(name);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Could not delete {name}");
            }
        }

        public void Add(FileSystemEntry<string> entry)
        {
            if (entry == null || Entries.ContainsKey(entry.Name)) return;
            entry.Parent = this;
            Entries.Add(entry.Name, entry);
        }
    }
}