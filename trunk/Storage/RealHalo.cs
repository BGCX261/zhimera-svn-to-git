using System;
using System.Collections.Generic;
using System.Text;
using Tashjik;
using Tashjik.Tier2;

namespace Zhimera.Storage
{
    class RealHalo : IHalo
    {
        ChordServer chordServer;

        public RealHalo()
		{
            chordServer = (ChordServer)(TashjikServer.createNew("Chord"));
            Guid chordInstanceGuid = chordServer.getGuid();

            
		}
    }
}
