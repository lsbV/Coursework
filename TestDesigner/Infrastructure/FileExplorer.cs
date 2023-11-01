using DALTestsDB;
using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestLib.Abstractions;

namespace TestDesigner.Infrastructure
{
    public class FileExplorer : IFileExplorerProvider
    {
        public Test OpenFile(string path)
        {
            if(path == null)
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
                    var task = JsonConvert.DeserializeObject<Test>(json, jsonSettings);
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

        public string? OpenFileDialog()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "JSON files (*.json)|*.json|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == true)
            {
                return openFileDialog.FileName;
            }
            else
            {
                return null;
            }
        }

        public bool SaveFile(Test test, string path = default)
        {            
            var jsonSettings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All, Formatting = Formatting.Indented };
            var json = JsonConvert.SerializeObject(test, jsonSettings);
            if (path == default)
            {
                return CreateNewFile(json);
            }
            else
            {
                return RewriteFile(path, json);
            }
        }

        private static bool RewriteFile(string path, string json)
        {
            if (File.Exists(path) && Path.GetExtension(path) == ".json")
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
            saveFileDialog.Filter = "JSON files (*.json)|*.json|All files (*.*)|*.*";
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
    }
}
