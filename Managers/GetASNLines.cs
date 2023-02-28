using FollettSFTP.Interfaces;
using FollettSFTP.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FollettSFTP.Managers
{
    public class GetASNLines : IGetASNLines
    {

        public GetASNLines()
        {

        }

        public async Task<List<ASNDetails>> ASNLines(string FileName, Stream memoryStream, int IsChargeBack)
        {
            memoryStream.Position = 0;

            var rows = new List<string>();

            using (var reader = new StreamReader(memoryStream, Encoding.UTF8))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    rows.Add(line);
                }
            }

            if (IsChargeBack == 0)
            {
                return rows
                   .Skip(1)
                   .Select(line => line.Split('\t'))
                   .Select(column => new ASNDetails
                   {
                       AccountNumber = column[1],
                       ShipmentNumber = column[2],
                       Cartons = "0",
                       SentDate = column[4],
                       ListOfTrackingNumbers = column[5],
                       SKU = column[7],
                       Quantity = column[9],
                       Price = column[10],
                   }).ToList();
            }
            else
            {
                return rows
                .Skip(1)
                .Select(line => line.Split('\t'))
                .Select(column => new ASNDetails
                {
                    AccountNumber = column[1],
                    ShipmentNumber = column[2],
                    Cartons = column[3],
                    SentDate = column[5],
                    ListOfTrackingNumbers = column[6],
                    Quantity = column[10],
                    SKU = column[12],
                    Price = column[14],
                }).ToList();
            }
        }
    }
}
