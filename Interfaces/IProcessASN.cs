using System.IO;
using System.Threading.Tasks;

namespace FollettSFTP.Interfaces
{
    public interface IProcessASN
    {
        Task ProcessTheASN(string name, int IsChargeBack, int sentShipmentType, Stream myBlob);
    }
}