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

//using Tashjik.Tier0;

namespace Zhimera.Storage
{
	/// <summary>
	/// Description of ProxyChordRing.
	/// </summary>
	public class ProxyChordRing : IChordRing
	{
		private readonly List<ZhimeraProxyNode> proxyZhimeraNodeList = new List<ZhimeraProxyNode>();
		//private static TransportLayerCommunicator transportLayerCommunicator = null;
		private RealChordRing realChordRing;
		
		//public delegate ZhimeraProxyNode ChordRingFindNodeDelegate(List<ZhimeraProxyNode> proxyZhimeraNodeList,  RealChordRing realChordRing);
		//public delegate void ChordRingStoreDelegate(List<byte[]> chunkList, List<ZhimeraProxyNode> proxyZhimeraNodeList, RealChordRing realChordRing);
		
		//private readonly ChordRingFindNodeDelegate chordRingFindNodeDelegate;
		//private readonly ChordRingStoreDelegate chordRingStoreDelegate;

        BootstrapManager bootstrapManager;
        IHalo halo;

		public ProxyChordRing(IHalo halo, int noOfChordRings) //RealChordRing realChordRing, ChordRingFindNodeDelegate chordRingFindNodeDelegate, ChordRingStoreDelegate chordRingStoreDelegate)
		{
            this.halo = halo;
            bootstrapManager = new BootstrapManager(halo, noOfChordRings);

			//this.chordRingFindNodeDelegate = chordRingFindNodeDelegate;
		//	this.chordRingStoreDelegate    = chordRingStoreDelegate;
			//this.realChordRing = realChordRing;
			//transportLayerCommunicator = TransportLayerCommunicator.getRefTransportLayerCommunicator();
		}

        public ProxyChordRing()
        {
        }
		public void beginStore(byte[] byteKey, Object data, string applicationGUID, AsyncCallback storeCallBack, Object appState)
		{
			//need to do this ... feeling lazy
/*			int randomPick = new Random().Next(0, proxyZhimeraNodeList.Count - 1);
			try
			{
				
				Socket sock = Tashjik.Common.UtilityMethod.CreateSocketConnection(selfNodeBasic.getIP());
				sock.Close();
				proxyZhimeraNodeList[randomPick].beginStore(byteKey, data, applicationGUID, storeCallBack, appState);
			}
			//catch (IP down exception)
			{
				
			}
*/
		}
		
		/*
		public void store(List<byte[]> chunkList)
		{
			
		}
		
		private ZhimeraProxyNode findNode()
		{
			return new ZhimeraProxyNode();
		}
		*/
	}
}
