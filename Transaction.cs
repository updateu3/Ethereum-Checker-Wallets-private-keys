using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Nethereum.Web3;
using System.Threading.Tasks;
using Nethereum.Hex.HexTypes;
namespace EthereumChecker
{
    public class Transaction
    {
        public string TransactionID { get; set; }
        public string AdressFrom { get; set; }
        public string AdressTo { get; set; }
        public decimal Value { get; set; }
        public string BlockNumber { get; set; }
        public HexBigInteger HexNumber { get; set; }
        public DateTime TimeSpan { get; set; }
    }
}