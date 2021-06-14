using System.Threading.Tasks;

namespace M365UK.Functions.Interfaces
{
    public interface IAuthProvider
    {
        Task<string> GetAccessToken();
    }
}