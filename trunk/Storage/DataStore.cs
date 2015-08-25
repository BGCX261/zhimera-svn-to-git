using System;
using System.Collections.Generic;
using System.Text;

namespace Zhimera.Storage
{
    class DataStore
    {
        List<String> data;
        public void storeData(string data)
        {
            this.data.Add(data);
        }

        public void storeData(byte[] data)
        {
            this.data.Add(Encoding.ASCII.GetString(data));
        }

    }
}
