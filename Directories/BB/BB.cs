using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FollettSFTP.Interfaces;
using Renci.SshNet;

namespace FollettSFTP.Directories.BB
{
    public class BB : IBB
    {
        private readonly IProcessASN _processASN;
        public BB(IProcessASN processASN)
        {
            _processASN = processASN;
        }

        public async Task CheckBBDirectory()
        {

            using (SftpClient client = new SftpClient(new PasswordConnectionInfo(Environment.GetEnvironmentVariable("SFTPIP"), Environment.GetEnvironmentVariable("SFTPUSER"), Environment.GetEnvironmentVariable("SFTPPW"))))
            {
                client.Connect();
                client.ChangeDirectory("/Buyback");
                var filePaths = client.ListDirectory(client.WorkingDirectory);
                foreach (var file in filePaths)
                {
                    using(MemoryStream ms = new MemoryStream())
                    {
                        client.DownloadFile(file.Name, ms);
                        await _processASN.ProcessTheASN(file.Name, 0, 4, ms);
                    }
                    
                   
                }
                client.Disconnect();
            }

        }
    }
}
