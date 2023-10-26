using TestLib.Interfaces;

namespace TestDesigner.Infrastructure
{
    public interface IFileExplorerProvider
    {
        public ITest OpenFile(string path);
        public bool SaveFile(ITest test, string path);
        public string OpenFileDialog();
    }
}