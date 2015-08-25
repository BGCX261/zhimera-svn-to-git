using System;
using System.Collections.Generic;
using System.Text;

namespace Zhimera.Storage
{
    class BootstrapManager
    {
        IHalo halo;
        List<ZhimeraProxyNode>[] bootStrapCache;

        public BootstrapManager(IHalo halo, int noOfChordRings)
        {
            this.halo = halo;
            bootStrapCache = new List<ZhimeraProxyNode>[noOfChordRings];
        }
    }
}
