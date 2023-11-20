using TestLib.Classes.Test;

namespace TestLib.Abstractions
{
    public interface IFileExplorerProvider
    {
        TEntity OpenFile<TEntity>(string path);
        bool SaveFile<TEntity>(TEntity entity, string path);
        string? OpenFileDialog();
        string? SaveFileDialog();
    }
}