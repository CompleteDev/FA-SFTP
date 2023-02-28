using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FollettSFTP.Models
{
    public class ASNDetails
    {
        public string AccountNumber { get; set; }
        public string ShipmentNumber { get; set; }
        public string ListOfTrackingNumbers { get; set; }
        public string ShipmentType { get; set; }
        public string SentDate { get; set; }
        public string Quantity { get; set; }
        public string Price { get; set; }
        public string SKU { get; set; }
        public string Pallets { get; set; }
        public string Cartons { get; set; }
        public List<InboundProducts> inboundProducts { get; set; }
    }

}
