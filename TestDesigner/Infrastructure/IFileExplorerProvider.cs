using DALTestsDB;
using TestLib.Abstractions;

namespace TestDesigner.Infrastructure
{
    public interface IFileExplorerProvider
    {
        public Test OpenFile(string path);
        public bool SaveFile(Test test, string path);
        public string OpenFileDialog();
    }
}