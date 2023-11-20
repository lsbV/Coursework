using System.Threading.Tasks;

namespace Client.Infrastructure
{
    public interface IUpdateable
    {
        Task UpdateAsynk();
    }
}