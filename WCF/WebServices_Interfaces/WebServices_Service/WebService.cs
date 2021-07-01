using System.Collections.Generic;
using WebServices_DataSources;
using WebServices_Interfaces;

namespace WebServices_Service
{
    public class WebService : ISaveData
    {
        // Adds the Body to the methods  
        public void AddData(Data d)
        {
            DataSource ds = new DataSource();
            ds.Save(d.DataLabel);
        }

        public Data[] GetCurrentData()
        {
            DataSource sd = new DataSource();
            List<string> dataList = sd.GetDataList();
            Data[] dataArray = new Data[dataList.Count];
            for (int i = 0; i < dataList.Count; i++)
            {
                Data s = new Data();
                s.DataLabel = dataList[i];
                dataArray[i] = s;
            }
            return dataArray;
        }
    }
}
