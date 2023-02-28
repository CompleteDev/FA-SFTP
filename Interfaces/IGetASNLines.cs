using FollettSFTP.Models;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace FollettSFTP.Interfaces
{
    public interface IGetASNLines
    {
        Task<List<ASNDetails>> ASNLines(string FileName, Stream memoryStream, int IsChargeBack);
    }
}