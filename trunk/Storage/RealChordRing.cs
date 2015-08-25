/*
 * Created by SharpDevelop.
 * User: ratul
 * Date: 7/6/2009
 * Time: 10:54 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */

using System;
using System.Collections.Generic;
using Tashjik;
using Tashjik.Tier2;
using System.Net;
	
namespace Zhimera.Storage
{
	/// <summary>
	/// Description of RealChordRing.
	/// </summary>
	public class RealChordRing : IChordRing
	{
		ChordServer chordServer;
        IncentiveStorageManager incentiveStorageManager;

		public RealChordRing()
		{
            chordServer = (ChordServer)(TashjikServer.createNew("Chord"));
            Guid chordInstanceGuid = chordServer.getGuid();

            incentiveStorageManager = new IncentiveStorageManager();				
		}
        public RealChordRing(IPAddress bootStrapIP, Guid bootStrapChordInstanceGuid)
        {
            chordServer = (ChordServer)(TashjikServer.joinExisting(bootStrapIP, "Chord", bootStrapChordInstanceGuid));
            Guid chordInstanceGuid = chordServer.getGuid();

            incentiveStorageManager = new IncentiveStorageManager();	
        }
		/*public void Store(List<byte[]> chunkList)
		{
			
		}
		*/
		
		public void beginStore(byte[] byteKey, Object data, string applicationGUID, AsyncCallback storeCallBack, Object appState)
		{
			//chordServer.beginPutData(byteKey.ToString(), (Tashjik.Common.Data)(data), storeCallBack, appState);
		}
		
		
	}
}
