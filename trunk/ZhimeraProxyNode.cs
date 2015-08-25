/*
 * Created by SharpDevelop.
 * User: ratul
 * Date: 7/13/2009
 * Time: 12:48 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */

using System;
using Tashjik;
using System.Net;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using Tashjik.Common;
using Zhimera.Storage;
namespace Zhimera
{
	/// <summary>
	/// Description of ZhimeraProxyNode.
	/// </summary>
    internal class ZhimeraProxyNode : ProxyNode , IZhimeraNode
	{
		private ZhimeraRealNode zhimeraRealNode;
        private ProxyNodeController proxyController;
        
	
		public ZhimeraProxyNode(IPAddress IP, ZhimeraRealNode zhimeraRealNode) : base(IP)
		{
			this.zhimeraRealNode = zhimeraRealNode;
		}

        public ZhimeraProxyNode(IPAddress ip, ProxyNodeController proxyController)
            : base(ip)
		{
		//	lowLevelComm = Base.LowLevelComm.getRefLowLevelComm();
//			selfNodeBasic = new Tashjik.Common.NodeBasic(ip);
			this.proxyController = proxyController;
		}

        public void beginGetHaloBootstrap(AsyncCallback getHaloBootstrapCallBack, Object appState)
        {
            Console.WriteLine("ZhimeraProxyNode::beginGetHaloBootstrap ENTER");

            byte[] compositeMsg = UtilityMethod.convertToTabSeparatedByteArray(true, "GET_HALOBOOTSTRAP");

            Console.WriteLine("ZhimeraProxyNode::getRank before sendMsg to proxyController");
            proxyController.sendMsgTwoWay(this, compositeMsg, 0, compositeMsg.Length, getHaloBootstrapCallBack, appState);

        }

        public void beginStoreContent(byte[] data, AsyncCallback storeContentCallBack, Object appState)
        {
            Console.WriteLine("ZhimeraProxyNode::beginNotify ENTER");

            byte[] compositeMsg = UtilityMethod.convertToTabSeparatedByteArray(true, "STORE_CONTENT", "CONTENT");

            Console.WriteLine("ZhimeraProxyNode::beginGetPredecessor before sendMsg to proxyController");
            proxyController.sendMsg(this, compositeMsg, 0, compositeMsg.Length, storeContentCallBack, appState);

        }

        public void beginFindSuccessor(byte[] hashedKey, AsyncCallback findSuccessorCallBack, Object appState, Guid relayTicket)  
        {
            //querying node is not necessary as of now, cause it doesn't matter as 
            //to who raised the query....the result is being sent back to the 
            //called via the callback and stateApp .. this petty knowledge is being
            //used in joinNode(queryingNode is passed as null) to overcome a deep design problem
            Console.WriteLine("ZhimeraProxyNode::beginFindSuccessor ENTER");

            byte[] compositeMsg = UtilityMethod.convertToTabSeparatedByteArray(true, "FIND_SUCCESSOR", Encoding.ASCII.GetString(hashedKey));
            Console.WriteLine("ZhimeraProxyNode::beginFindSuccessor before sendMsg to proxyController");
            proxyController.sendMsgTwoWayRelay(this, compositeMsg, 0, compositeMsg.Length, findSuccessorCallBack, appState, relayTicket);

        }

        public void beginGetRank(AsyncCallback getRankCallBack, Object appState)  
        {
            Console.WriteLine("ZhimeraProxyNode::getRank ENTER");

            byte[] compositeMsg = UtilityMethod.convertToTabSeparatedByteArray(true, "GET_RANK");

            Console.WriteLine("ZhimeraProxyNode::getRank before sendMsg to proxyController");
            proxyController.sendMsgTwoWay(this, compositeMsg, 0, compositeMsg.Length, getRankCallBack, appState);

        }

        public List<ZhimeraProxyNode> getHaloBootstrap()
        {
            return null;
        }
        public void storeContent(byte[] data)
        {

        }
        public ZhimeraProxyNode findSuccessor(byte[] hashedKey, AsyncCallback findSuccessorCallBack, Object appState, Guid relayTicket)
        {
            return null;
        }
        public double[] getRank()
        {
            return new double[3] { 0.0, 0.0, 0.0 };
        }

        private String[] splitMsgBuffer(byte[] buffer, int offset, int size)
        {
            String strReceivedData = Encoding.ASCII.GetString(buffer, offset, size);
            return strReceivedData.Split(new char[] { '\r' });
        }

        public override void notifyOneWayMsg(IPAddress fromIP, byte[] buffer, int offset, int size)
        {
            Console.WriteLine("ChordProxyNode::notifyOneWayMsg ENTER");
            String[] split = splitMsgBuffer(buffer, offset, size);
            if (String.Compare(split[0], "STORE_CONTENT") == 0)
                zhimeraRealNode.storeContent(Encoding.ASCII.GetBytes(split[1])); 
        }

        public override Tashjik.Tier0.TransportLayerCommunicator.Data notifyTwoWayMsg(IPAddress fromIP, byte[] buffer, int offset, int size)
        {
            Console.WriteLine("ChordProxyNode::notifyTwoWayMsg ENTER");
            String[] split = splitMsgBuffer(buffer, offset, size);
                    if (String.Compare(split[0], "GET_HALOBOOTSTRAP") == 0)
                    {
                        byte[] compositeMsg;
                        List<ZhimeraProxyNode> haloBootstrapList = zhimeraRealNode.getHaloBootstrap();

                        if (haloBootstrapList == null)
                        {
                            Console.WriteLine("ChordProxyNode::notifyTwoWayMsg  haloBootstrapList is empty");
                            compositeMsg = UtilityMethod.convertToTabSeparatedByteArray(true, "GET_HALOBOOTSTRAP_REPLY", "NO_HALOBOOTSTRAP");
                        }
                        else
                        {
                            Console.WriteLine("ChordProxyNode::notifyTwoWayMsg haloBootstrapList =well something");
                            String[] getHaloBootstrapReplyString = new string[haloBootstrapList.Count+1];
                            int i = 0;
                            getHaloBootstrapReplyString[0] = "GET_HALOBOOTSTRAP_REPLY";
                            foreach (ZhimeraProxyNode zhimeraProxyNode in haloBootstrapList)
                            {
                                i++;
                                getHaloBootstrapReplyString[i] = zhimeraProxyNode.getIP().ToString();
                            }
                               compositeMsg = UtilityMethod.convertToTabSeparatedByteArray(true, getHaloBootstrapReplyString);
                        }
                        return new Tashjik.Tier0.TransportLayerCommunicator.Data(compositeMsg, 0, compositeMsg.Length);
                    }
                    else if (String.Compare(split[0], "GET_RANK") == 0)
                    {
                        byte[] compositeMsg;
                        IChordNode[] fingerTable = thisNode.getFingerTable();
                        String[] str = new String[fingerTable.Length + 1];
                        str[0] = "GET_FINGERTABLE_REPLY";
                        int i = 1;
                        foreach (IChordNode chordNode in fingerTable)
                        {
                            Console.WriteLine("ChordProxyNode::notifyTwoWayMsg addong fingertable entry");
                            if (chordNode == null)
                                str[i++] = "NULL";
                            else
                                str[i++] = chordNode.getIP().ToString();
                        }
                        Console.WriteLine("ChordProxyNode::notifyTwoWayMsg FingerTable = .... well some other day");
                        compositeMsg = UtilityMethod.convertToTabSeparatedByteArray(true, str);
                        return new Tashjik.Tier0.TransportLayerCommunicator.Data(compositeMsg, 0, compositeMsg.Length);
                    }
                    return null;
        }

        internal class IP_ChordProxyNode
        {
            public IPAddress IP;
            public ZhimeraProxyNode zhimeraProxyNode;
            public Guid ticket;
            public IP_ChordProxyNode()
            {

            }
            public IP_ChordProxyNode(IPAddress IP, ZhimeraProxyNode zhimeraProxyNode, Guid ticket)
            {
                this.IP = IP;
                this.zhimeraProxyNode = zhimeraProxyNode;
                this.ticket = ticket;
            }

        }

        public override Tashjik.Tier0.TransportLayerCommunicator.Data notifyTwoWayRelayMsg(IPAddress fromIP, IPAddress originalFromIP, byte[] buffer, int offset, int size, Guid relayTicket)
        {
            Console.WriteLine("ChordProxyNode::notifyTwoWayRelayMsg ENTER");
            String[] split = splitMsgBuffer(buffer, offset, size);
   /*         if (String.Compare(split[0], "FIND_SUCCESSOR") == 0)
            {
                Console.WriteLine("ChordProxyNode::notifyTwoWayRelayMsg message = FIND_SUCCESSOR");
                IP_ChordProxyNode iIP_ChordProxyNode = new IP_ChordProxyNode(originalFromIP, this, relayTicket);
                thisNode.beginFindSuccessor(System.Text.Encoding.ASCII.GetBytes(split[1]), (IChordNode)(proxyController.getProxyNode(originalFromIP)), new AsyncCallback(processFindSuccessor_notifyTwoWayRelayMsg_callback), iIP_ChordProxyNode, relayTicket);
            }
     */       return null;
        }

        static void processFindSuccessor_notifyTwoWayRelayMsg_callback(IAsyncResult ayncResult)
        {
 /*           ChordCommon.IChordNode_Object iNode_Object = (ChordCommon.IChordNode_Object)(ayncResult.AsyncState);
            IChordNode successor = (iNode_Object.node);
            ChordProxyNode chordProxyNode = ((IP_ChordProxyNode)(iNode_Object.obj)).chordProxyNode;
            IPAddress originalFromIP = ((IP_ChordProxyNode)(iNode_Object.obj)).IP;
            Guid relayTicket = ((IP_ChordProxyNode)(iNode_Object.obj)).ticket;

            byte[] compositeMsg = UtilityMethod.convertToTabSeparatedByteArray(true, "FIND_SUCCESSOR_REPLY", successor.getIP().ToString());
            chordProxyNode.proxyController.sendMsgTwoWayRelay(chordProxyNode.proxyController.getProxyNode(originalFromIP), compositeMsg, 0, compositeMsg.Length, null, null, relayTicket);
   */     }

        public override void notifyTwoWayReplyReceived(IPAddress fromIP, byte[] buffer, int offset, int size, AsyncCallback originalRequestCallBack, Object originalAppState)
        {
            Console.WriteLine("ChordProxyNode::notifyTwoWayReplyReceived ENTER");
            String[] split = splitMsgBuffer(buffer, offset, size);
  /*          if (String.Compare(split[0], "FIND_SUCCESSOR_REPLY") == 0)
            {
                Console.WriteLine("ChordProxyNode::notifyTwoWayReplyReceived FIND_SUCCESSOR_RECEIVED");
                String strSuccessorIP = split[1];
                Console.Write("ChordProxyNode::notifyTwoWayReplyReceived SuccessorIP = ");
                Console.WriteLine(strSuccessorIP);

                IPAddress successorIP = UtilityMethod.convertStrToIP(strSuccessorIP);

                ChordCommon.IChordNode_Object iNode_Object = new ChordCommon.IChordNode_Object();
                iNode_Object.node = (IChordNode)(proxyController.getProxyNode(successorIP));
                iNode_Object.obj = originalAppState;

                Tashjik.Common.ObjectAsyncResult objectAsyncResult = new Tashjik.Common.ObjectAsyncResult(iNode_Object, false, false);
                if (originalRequestCallBack != null)
                    originalRequestCallBack(objectAsyncResult);
            }
            else if (String.Compare(split[0], "GET_PREDECESSOR_REPLY") == 0)
            {
                Console.WriteLine("ChordProxyNode::notifyTwoWayReplyReceived GET_PREDECESSOR_RECEIVED");
                String strPredecessorIP = split[1];
                ChordCommon.IChordNode_Object iNode_Object = new ChordCommon.IChordNode_Object();

                if (String.Compare(strPredecessorIP, "UNKNOWN_PREDECESSOR") == 0)
                {
                    Console.Write("ChordProxyNode::notifyTwoWayReplyReceived PREDECESSOR_UNKNOWN");
                    iNode_Object.node = null;

                }
                else
                {
                    Console.Write("ChordProxyNode::notifyTwoWayReplyReceived PredecessorIP = ");
                    Console.WriteLine(strPredecessorIP);

                    IPAddress predecessorIP = UtilityMethod.convertStrToIP(strPredecessorIP);

                    iNode_Object.node = (IChordNode)(proxyController.getProxyNode(predecessorIP));
                }
                iNode_Object.obj = originalAppState;

                Tashjik.Common.ObjectAsyncResult objectAsyncResult = new Tashjik.Common.ObjectAsyncResult(iNode_Object, false, false);
                if (originalRequestCallBack != null)
                    originalRequestCallBack(objectAsyncResult);
            }
            else if (String.Compare(split[0], "GET_FINGERTABLE_REPLY") == 0)
            {
                Console.WriteLine("ChordProxyNode::notifyTwoWayReplyReceived GET_FINGERTABLE_RECEIVED");

                String strPredecessorIP = split[1];
                IChordNode[] fingerTable = new IChordNode[split.Length - 1];
                for (int i = 1; i < 161; i++)
                {
                    if (split[i] == "NULL")
                        fingerTable[i - 1] = null;
                    else
                        fingerTable[i - 1] = (IChordNode)(proxyController.getProxyNode(UtilityMethod.convertStrToIP(split[i].ToString())));
                }
                ChordCommon.IChordNodeArray_Object iNodeArray_Object = new ChordCommon.IChordNodeArray_Object();
                iNodeArray_Object.nodeArray = fingerTable;
                iNodeArray_Object.obj = originalAppState;

                Tashjik.Common.ObjectAsyncResult objectAsyncResult = new Tashjik.Common.ObjectAsyncResult(iNodeArray_Object, false, false);
                if (originalRequestCallBack != null)
                    originalRequestCallBack(objectAsyncResult);
            }
    */    }

        //need to make this asynchronous
        public /*override*/ void beginNotifyMsgRec(IPAddress fromIP, Object data, AsyncCallback notifyMsgRecCallBack, Object appState)
        {

        }


  /*      public override void notifyOneWayMsg(IPAddress fromIP, byte[] buffer, int offset, int size)
        {
        }

        public override Tashjik.Tier0.TransportLayerCommunicator.Data notifyTwoWayMsg(IPAddress fromIP, byte[] buffer, int offset, int size)
        {
            return null;
        }

        public override Tashjik.Tier0.TransportLayerCommunicator.Data notifyTwoWayRelayMsg(IPAddress fromIP, IPAddress originalFromIP, byte[] buffer, int offset, int size, Guid relayTicket)
        {
            return null;
        }

        public override void notifyTwoWayReplyReceived(IPAddress fromIP, byte[] buffer, int offset, int size, AsyncCallback originalRequestCallBack, Object originalAppState)
        {
        }
        */


        /*	enum ZhimeraMsgType
            {	
                STORE
            }
		
            struct ZhimeraMsg
            {
                public ZhimeraMsgType zhimeraMsgType;
                public Object data;
            }
		
            struct ZhimeraStoreMsg
            {
                public byte[] byteKey;
                public Object data;
            }
        */	


/*		public override void beginNotifyMsgRec(IPAddress fromIP, Object data, AsyncCallback notifyMsgRecCallBack, Object appState)
		{
			ZhimeraMsg zhimeraMsg = (ZhimeraMsg)data;
			
			switch (zhimeraMsg.zhimeraMsgType)
			{
				case ZhimeraMsgType.STORE:
					ZhimeraStoreMsg zhimeraStoreMsg = (ZhimeraStoreMsg)(zhimeraMsg.data);
					zhimeraRealNode.BeginStore(zhimeraStoreMsg.byteKey, zhimeraStoreMsg.data, notifyMsgRecCallBack, appState);
				break;
			}
		}
		
		public void beginStore(byte[] byteKey, Object data, AsyncCallback storeCallBack, Object appState)
		{
			ZhimeraStoreMsg zhimeraStoreMsg = new ZhimeraStoreMsg();
			zhimeraStoreMsg.byteKey = byteKey;
			zhimeraStoreMsg.data = data;
			
			ZhimeraMsg zhimeraMsg = new ZhimeraMsg();
			zhimeraMsg.zhimeraMsgType = ZhimeraMsgType.STORE;
			zhimeraMsg.data = zhimeraStoreMsg;
			
			Tashjik.Tier0.TransportLayerCommunicator.Msg transportLayerCommunicatorMsg = new Tashjik.Tier0.TransportLayerCommunicator.Msg(new Guid(Zhimera.zhimeraGUID));
			transportLayerCommunicatorMsg.setData(zhimeraMsg);
			
			
			transportLayerCommunicator.forwardMsgToRemoteHost(getIP(), transportLayerCommunicatorMsg);
			//how do we get an exception if remote host is down, and how do we relay it upwards?
		}*/
	}
 
}
