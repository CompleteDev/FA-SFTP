using System.Threading.Tasks;

namespace FollettSFTP.Interfaces
{
    public interface ISendPayload
    {
        Task SendASNPayload(string jsonData);
    }
}