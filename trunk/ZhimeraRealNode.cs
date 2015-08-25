/*
 * Created by SharpDevelop.
 * User: ratul
 * Date: 7/13/2009
 * Time: 12:47 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */

using System;
using System.Net;
using System.Collections;
using System.Collections.Generic;
using Tashjik;
using Zhimera.Storage;

namespace Zhimera
{
	/// <summary>
	/// Description of ZhimeraRealNode.
	/// </summary>
	internal class ZhimeraRealNode : RealNode, IZhimeraNode
	{
	//private static readonly StorageManager storageManager = StorageManager.getStorageManager();
        private ErasureCoder erasureCoder;
        private SVCEncoder _SVCEncoder;
        private ChordCylinder chordCylinder;

        private IncentiveStorageManager incentiveStorageManager = new IncentiveStorageManager();
        private DataStore dataStore = new DataStore();
        private ProxyNodeController proxyNodeController;

        public ZhimeraRealNode(ProxyNodeController proxyNodeController)
        {
            this.proxyNodeController = proxyNodeController;
            
            erasureCoder = new ErasureCoder();
            _SVCEncoder = new SVCEncoder();
            chordCylinder = new ChordCylinder(proxyNodeController);

        }


        public ZhimeraRealNode(ProxyNodeController proxyNodeController, IPAddress bootStrapIP, Guid bootStrapChordInstanceGuid)
        {
            this.proxyNodeController = proxyNodeController; // new ProxyNodeController(new ProxyNodeController.CreateProxyNodeDelegate(createZhimeraProxyNode), new Guid(zhimeraGUID));

            erasureCoder = new ErasureCoder();
            _SVCEncoder = new SVCEncoder();
            chordCylinder = new ChordCylinder(bootStrapIP, bootStrapChordInstanceGuid, proxyNodeController);
        }


        public void beginGetHaloBootstrap(AsyncCallback getHaloBootstrapCallBack, Object appState)
        {
        }

        public List<ZhimeraProxyNode> getHaloBootstrap()
        {
            return chordCylinder.getHaloBootstrap();
        }

        public void beginStoreContent(byte[] data, AsyncCallback storeContentCallBack, Object appState)
        {

        }

        public void storeContent(byte[] data)
        {
            dataStore.storeData(data);
        }
        public ZhimeraProxyNode findSuccessor(byte[] hashedKey, AsyncCallback findSuccessorCallBack, Object appState, Guid relayTicket)
        {
            return null;
        }

        public void beginFindSuccessor(byte[] hashedKey, AsyncCallback findSuccessorCallBack, Object appState, Guid relayTicket)
        {
            
        }

        public void beginGetRank(AsyncCallback getRankCallBack, Object appState)
        {
        }

        public double[] getRank()
        {
            return incentiveStorageManager.getRank();
        }
	}

}
