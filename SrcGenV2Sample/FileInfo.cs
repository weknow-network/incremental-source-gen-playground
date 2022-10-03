
namespace Bnaya.Samples
{
    public class FileInfo
    {
        public string Name { get; private set; }
        public string Content { get; private set; }

        public FileInfo(string name, string content)
        {
            Name = name;
            Content = content;
        }

    }
}
