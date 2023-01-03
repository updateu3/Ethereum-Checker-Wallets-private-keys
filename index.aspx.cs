using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EtherscanApi.Net.Interfaces;
using Nethereum.Web3;
using Nethereum.RPC.Eth.DTOs;
using Nethereum.Hex.HexConvertors;
using Nethereum.Hex.HexTypes;

namespace EthereumChecker
{
    public partial class index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            //Collect address and starting block
            string address = tbxAdress.Text;
            int startingblock=0;
            int.TryParse(tbxStartingBlock.Text, out startingblock);
            List<Transaction> listTransactions = new List<Transaction>();
            try
            {
                if (address != string.Empty)
                {
                    //Collect transaction with given address and starting block
                    listTransactions = this.GetTransaction(address, startingblock);
                }
                if (listTransactions != null)
                {
                    //Insert transactions in gridview
                    GridView1.DataSource = listTransactions;
                    GridView1.DataBind();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
      
        }
        //Button for showing balance at given time
        protected async void Button2_Click(object sender, EventArgs e)
        {
            System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;

            //Using infura with web3
            Web3 web = new Web3("https://mainnet.infura.io/v3/b1ff5ed326e24235a69695a19553cf14");

            if (tbxAdress.Text != string.Empty && tbxTime.Text != string.Empty)
            {
                //collecting address and time for balance
                string address = tbxAdress.Text;
                DateTime givenTime = DateTime.Parse(tbxTime.Text);
                if (givenTime <= DateTime.Now)
                {
                    //Calling function where balance get calculated
                    var balance = await GetBalanceAsync(address, givenTime, web);
                    lblBalance.Text = "Balance: " + balance.ToString();
                }
                else
                {
                    lblBalance.Text = "Error:Date should be lower then " + DateTime.Now.ToShortDateString();
                }
            }
            else
            {
                lblBalance.Text = "Error:Wrong input";
            }
        }
        public async System.Threading.Tasks.Task<decimal> GetBalanceAsync(string address,DateTime givenTime,Web3 web)
        {
            decimal balance = 0;

            System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
             List<Transaction> listTransactions = new List<Transaction>();
            try
            {
                //get all transactions connected with address
                listTransactions = GetTransaction(address, 0);
                if (listTransactions != null)
                {
                    foreach (var item in listTransactions)
                    {
                        //Geting balance after nearest transaction to given time
                        if (givenTime >= item.TimeSpan)
                        {
                            BlockParameter blockParameter = new BlockParameter(item.HexNumber);

                            var wei = await web.Eth.GetBalance.SendRequestAsync(address, blockParameter.BlockNumber); //.SendRequestAsync(address, blockParameter.BlockNumber);
                            balance = Web3.Convert.FromWei(wei.Value);

                            return balance;

                        }
                    }
                }

                return balance;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
               
            }



            //Finding balance with blocks timestamp(very slow method)
            

            // var blocknumber = await web.Eth.Blocks.GetBlockNumber.SendRequestAsync();
            //BlockParameter block1 = new BlockParameter(blocknumber);
            //decimal balance1;
            //while (true)
            //{
            //    var block = await web.Eth.Blocks.GetBlockWithTransactionsByNumber.SendRequestAsync(blocknumber);
            //    var timestamp = block.Timestamp;
            //    System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            //    dtDateTime = dtDateTime.AddSeconds((double)timestamp.Value).ToLocalTime();
            //   BlockParameter bloc1 = new BlockParameter(blocknumber);
            //    if (givenTime>=dtDateTime)
            //    {
                   
            //       var dec = await web.Eth.GetBalance.SendRequestAsync(address, bloc1.BlockNumber);
            //        balance1= Convert.ToDecimal(dec);
            //        break;
            //    }
            //    blocknumber.Value = blocknumber.Value - 5000;
            //}
            //return balance1;
             
              
            
        }
        public List<Transaction> GetTransaction(string address,int startingblock)
        {

            List<Transaction> listTransactions = new List<Transaction>();
           
            string APIKEY = "6H3S7JSIPGMQIEE71BCD8AXXK5YCASZE1F";
            try
            {

                //Using EtherScan api for geting transactions
                var etherscan = new EtherScanClient(APIKEY);
                //get all transactions connected with address
                var allTransactions = etherscan.GetTransactions(address);
                if (allTransactions.Result != null)
                {
                    foreach (var item in allTransactions.Result)
                    {
                        //Store only transactions that are happend after starting block
                        if (Convert.ToInt32(item.BlockNumber) >= startingblock)
                        {
                            
                            Transaction _transaction = new Transaction()
                            {
                                TransactionID = item.TxId,
                                AdressFrom = item.FromId,
                                AdressTo = item.ToId,
                                HexNumber=new HexBigInteger(item.BlockNumber),
                                BlockNumber = item.BlockNumber.ToString(),
                                Value = item.Value,
                                TimeSpan = item.TimeStamp.Date
                            };

                            listTransactions.Add(_transaction);
                        }
                    }
                 return listTransactions;  
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
                
            }
        }

      
    }
}