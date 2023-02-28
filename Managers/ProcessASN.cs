using FollettSFTP.Controller;
using FollettSFTP.Interfaces;
using FollettSFTP.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace FollettSFTP.Managers
{
    public class ProcessASN : IProcessASN
    {
        private readonly IGetASNLines _asnLines;
        private readonly ISendPayload _sendPayload;
        public ProcessASN(IGetASNLines asnLines, ISendPayload sendPayload)
        {
            _asnLines = asnLines;
            _sendPayload = sendPayload;
        }

        public async Task ProcessTheASN(string name, int IsChargeBack, int sentShipmentType, Stream myBlob)
        {
            try
            {
                var lines = await _asnLines.ASNLines(name, myBlob, IsChargeBack);
                var groupbyShipment =
                    from line in lines
                    group line by line.ShipmentNumber into NewGroup
                    select NewGroup;
                foreach (var line in groupbyShipment)
                {
                    var anumber = line.Select(x => x.AccountNumber).First();
                    string tnumbers = line.Select(x => x.ListOfTrackingNumbers.Replace(";", ",").TrimEnd().TrimEnd(',')).First();
                    var sentdate = line.Select(x => x.SentDate).First();
                    string ShipmentNumber = line.Key;
                    var CartQTY = line.Select(x => x.Cartons).First();
                    string[] TrackingNumbers = tnumbers.Split(new string[] { "," }, StringSplitOptions.None);
                    string SentDate = sentdate.ToString();

                    var inboundProducts = new List<InboundProducts>();
                    foreach (var detail in line)
                    {
                        var inbound = new InboundProducts();
                        inbound.SKU = detail.SKU;
                        inbound.Quantity = detail.Quantity;
                        inbound.Price = detail.Price;
                        inboundProducts.Add(inbound);
                    }

                    var newASN = new
                    {
                        ShipmentNumber,
                        AccountNumber = anumber,
                        ListOfTrackingNumbers = TrackingNumbers,
                        SentDate = sentdate,
                        ShipmentType = sentShipmentType,
                        Pallets = "0",
                        Cartons = CartQTY,
                        InboundProducts = inboundProducts,
                    };
                    string jsonData = JsonConvert.SerializeObject(newASN);
                    //Console.WriteLine(jsonData);
                    await _sendPayload.SendASNPayload(jsonData);
                }

                //MOVE FILE TO ARCHIVE
                string theDate = DateTime.Now.ToString();
                //_moveBlob.PrepBlobMove(theDate + "_" + name, Environment.GetEnvironmentVariable("ASNArchive"));


            }
            catch (Exception ex)
            {

                //await _err.InsertSystemError((int)EnumCS.ProcessType.FileRead, 0, "Created from FollettSFTP.cs -> ReadFollettFile function - " + ex.Message);
                //NEED TO KNOW WHERE TO PUT FILE AND HOW TO LOG IT
                //_moveBlob.PrepBlobMove(name, Environment.GetEnvironmentVariable("ASNArchive"));
            }
        }

    }
}
