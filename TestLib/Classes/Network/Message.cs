using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestLib.Classes.Network
{
    public class Message
    {
        //public Dictionary<string, string> Headers { get; set; } = new Dictionary<string, string>();
        public string Header { get; set; } = null!;
        public string Body { get; set; } = null!;
        //public object? Body { get; set; }

        //public bool IsEmpty => Headers.Count == 0 && Body == null ;
        //public bool IsHeaderOnly => Headers.Count > 0 && Body == null;
        //public bool IsBodyOnly => Headers.Count == 0 && Body != null;
        //public bool IsFull => Headers.Count > 0 && Body != null;
        //public bool ContainsHeader(string header)
        //{
        //    return Headers.ContainsKey(header);
        //}
        //public bool ContainsHeader(string header, out string? value)
        //{
        //    return Headers.TryGetValue(header, out value);
        //}
        //public void AddHeader(string header, string value)
        //{
        //    Headers.Add(header, value);
        //}
        //public void RemoveHeader(string header)
        //{
        //    Headers.Remove(header);
        //}
        //public void ClearHeaders()
        //{
        //    Headers.Clear();
        //}
        //public void ClearBody()
        //{
        //    Body = null;
        //}
        //public void Clear()
        //{
        //    ClearHeaders();
        //    ClearBody();
        //}
    }
}
