using System;
using System.Threading.Tasks;
using FollettSFTP.Interfaces;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace FollettSFTP
{
    public class CheckForASN
    {
        private readonly IBB _bb;
        public CheckForASN(IBB bb)
        {
            _bb = bb;
        }

        [FunctionName("CheckForASN")]
        public async Task Run([TimerTrigger("0 */5 * * * *",
#if DEBUG
    RunOnStartup= true
#endif
            )]TimerInfo myTimer, ILogger log)
        {
            log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");
            await _bb.CheckBBDirectory();
        }

    }
}
