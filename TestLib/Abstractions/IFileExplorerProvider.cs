using TestLib.Classes.Test;

namespace TestLib.Abstractions
{
    public interface IFileExplorerProvider
    {
        public TEntity OpenFile<TEntity>(string path);
        public bool SaveFile<TEntity>(TEntity entity, string path);
        public string? OpenFileDialog();
    }
}