using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

namespace BUS
{
    public class UpdateDataBUS
    {
        private string _url = "http://www.cophieu68.com/export/excel.php?id=";
        private string _uri;
 
        public void DownloadStockData(string stockId, string savePath)
        {
            string _uri = _url + stockId;
            try
            {
                WebClient webClient = new WebClient();
                webClient.DownloadFile(_uri, savePath);
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.ToString());
            }
        }

        public void doUpdate(List<string> listStockIdUpdate, string updateFolder)
        {            
            for (int i = 0; i < listStockIdUpdate.Count; i++)
            {
                string stockId = listStockIdUpdate[i].ToUpper();
                string fileName = updateFolder+stockId.ToLower()+".csv";                
                DownloadStockData(stockId, fileName);
            }
        }
    }
}
