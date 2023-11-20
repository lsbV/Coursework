using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.IO;
using TestLib.Abstractions;
using TestLib.Classes.Test;

namespace TestDesigner.Infrastructure
{
    public class FileExplorer : IFileExplorerProvider
    {
        public string? OpenFileDialog()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                return openFileDialog.FileName;
            }
            else
            {
                return null;
            }
        }

        private static bool RewriteFile(string path, string json)
        {
            if (File.Exists(path))
            {
                try
                {
                    File.WriteAllText(path, json);
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
            return false;
        }

        private static bool CreateNewFile(string json)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            if (saveFileDialog.ShowDialog() == true)
            {
                try
                {
                    File.WriteAllText(saveFileDialog.FileName, json);
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public TEntity OpenFile<TEntity>(string path)
        {
            if (path == null)
            {
                throw new ArgumentNullException("Path is null");
            }
            if (File.Exists(path))
            {
                var ext = Path.GetExtension(path);
                if (ext == ".json")
                {
                    var json = File.ReadAllText(path);
                    var jsonSettings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All, Formatting = Formatting.Indented };
                    var task = JsonConvert.DeserializeObject<TEntity>(json, jsonSettings);
                    if (task == null) throw new ArgumentException("Invalid file");
                    return task;
                }
                else
                {
                    throw new ArgumentException("Invalid file extension");
                }
            }
            else
            {
                throw new ArgumentException("File not found");
            }
        }

        public bool SaveFile<TEntity>(TEntity entity, string path)
        {
            var jsonSettings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All, Formatting = Formatting.Indented };
            var json = JsonConvert.SerializeObject(entity, jsonSettings);
            if (path == default)
            {
                return CreateNewFile(json);
            }
            else
            {
                return RewriteFile(path, json);
            }
        }

        public string? SaveFileDialog()
        {
            SaveFileDialog saveFileDialog = new();
            if (saveFileDialog.ShowDialog() == true)
            {
                return saveFileDialog.FileName;
            }
            else
            {
                return null;
            }
        }
    }
}
