using System.Threading.Tasks;

namespace Server.Pages
{
    public interface IUpdateable
    {
        Task UpdateAsynk();
    }
}