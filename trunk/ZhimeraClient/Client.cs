/************************************************************
* File Name: Client.cs
*
* Author: Ratul Mukhopadhyay
* ratuldotmukhATgmaildotcom
*
* This software is licensed under the terms and conditions of
* the MIT license, as given below.
*
* Copyright (c) <2008-2010> <Ratul Mukhopadhyay>
*
* Permission is hereby granted, free of charge, to any person
* obtaining a copy of this software and associated documentation
* files (the "Software"), to deal in the Software without
* restriction, including without limitation the rights to use,
* copy, modify, merge, publish, distribute, sublicense, and/or sell
* copies of the Software, and to permit persons to whom the
* Software is furnished to do so, subject to the following
* conditions:
*
* The above copyright notice and this permission notice shall be
* included in all copies or substantial portions of the Software.
*
* THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
* EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES
* OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
* NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT
* HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY,
* WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
* FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
* OTHER DEALINGS IN THE SOFTWARE.
*
*
* Description:
* 
* 
*
* Supporting Documentation:
*
* Portability: .NET VES
* Status: Experimental
* Reuse Reviews:
* Date Name Comment
*
* Modification History:
*
************************************************************/
#define SIM

using System;
using System.Collections;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;

using Tashjik;
using Tashjik.Common;

using Tashjik.Tier2;
using Tashjik.Tier0;

using Zhimera;






//using log4net;
//using log4net.Appender;
//using log4net.Config;

namespace TashjikClient
{
	public class Client : TransportLayerCommunicator.ISink
	{
		//private static readonly ILog log = LogManager.GetLogger(typeof(Client));
		
		public Client()
		{
			
		}
		
		static TransportLayerCommunicator transportLayerCommunicator; // = TransportLayerCommunicator.getRefTransportLayerCommunicator();

#if SIM		
		public static void Main(string[] args) 
		{
			Console.WriteLine("hello");
			//Console.WriteLine(args.Length);
		
			if(args.Length >= 1)
			{
				//byte[] byteIP = System.Text.Encoding.ASCII.GetBytes(args[0]);
				String[] IPsplit = args[0].Split(new char[] {'.'});
				Console.WriteLine("Hi there");

				IPAddress ipAddress = UtilityMethod.convertStrToIP(args[0]);
				
				UtilityMethod.SetLocalHostIP(ipAddress);
				Console.Write("received port=");
				Console.WriteLine(args[1]);
				UtilityMethod.SetPort(args[1]);
				
				transportLayerCommunicator = TransportLayerCommunicator.getRefTransportLayerCommunicator();
				
			}

#else
		public static void Main()
		{
#endif
		
			Guid g = Guid.NewGuid();
			Console.Write(g.ToString());

			Client client = new Client();
	//		client.chkTransPortLayerComm();
			client.chkChord();
			
		}
		private void requestBootStrapNode()
		{
			Console.WriteLine("Entering requestBootStrapNode");
#if SIM
			
			transportLayerCommunicator.register(ClientGuid, this);
			byte[] byteIP = {127, 0, 0, 1};
			IPAddress ipAddress = new IPAddress(byteIP);

			String strMsg = "bootStrap request";
			byte[] msg = System.Text.Encoding.ASCII.GetBytes(strMsg);
			transportLayerCommunicator.BeginTransportLayerSendOneWay(ipAddress, msg, 0, strMsg.Length, ClientGuid, new AsyncCallback(sendDataCallBack), ipAddress);
#else
			chordInstanceGuid = new Guid("");
			return null;
#endif			
		}
		
		private void receiveBootStrapNode(IPAddress fromIP, byte[] buffer, int offset, int size)
		{
			Console.WriteLine("Client::receiveBootStrapNode ENTER");
			//Console.WriteLine(Encoding.ASCII.GetString(buffer));
			//ChordServer chord;
            Zhimera.ZhimeraServer zhimera;

			if(String.Compare(Encoding.ASCII.GetString(buffer), "no bootstrapnode") == 0)
			{
				Console.WriteLine("Client::receiveBootStrapNode NO bootstrapnode received");

                zhimera = new Zhimera.ZhimeraServer();

			}
			else
			{
				Console.WriteLine("Client::receiveBootStrapNode bootstrapnode RECEIVED");
				Console.Write("RECEIVED bootstrapnode=");
				String strBootStrapData = Encoding.ASCII.GetString(buffer);
				Console.WriteLine(strBootStrapData);
				String[] split = strBootStrapData.Split(new char[] {'\t'});
				String strBootStrapIP = split[0];
				String strBootStrapChordInstanceGuid = split[1];
				Console.WriteLine(strBootStrapIP);
				Console.WriteLine(strBootStrapChordInstanceGuid);
                IPAddress bootStrapIP = UtilityMethod.convertStrToIP(strBootStrapIP);

                zhimera = new Zhimera.ZhimeraServer(bootStrapIP, new Guid(strBootStrapChordInstanceGuid));

								
			}
		}
		
		private ChordServer joinExistingChord(String strBootStrapIP, String strBootStrapChordInstanceGuid)
		{
            Console.WriteLine("Client::joinExistingChord ENTER");
			IPAddress bootStrapIP = UtilityMethod.convertStrToIP(strBootStrapIP);
				
			ChordServer chord = (ChordServer)(TashjikServer.joinExisting(bootStrapIP, "Chord", new Guid(strBootStrapChordInstanceGuid)));
            Console.WriteLine("Client::joinExistingChord CHORD RING JOINED");
		
			return chord;
		}
		
		private void exereciseChord(ChordServer chord)
		{
            for (int i = 0; i < 16; i++)
                Thread.Sleep(10000);

            String sg = "PUT DATA TESTING";
            byte[] byteSg = System.Text.Encoding.ASCII.GetBytes(sg);
            byte[] byteSgKey = Tashjik.Common.UtilityMethod.sha.ComputeHash(byteSg);
            chord.beginPutData(byteSgKey, byteSg, 0, byteSg.Length, null, null);

            for (int i = 0; i < 6; i++)
                Thread.Sleep(10000);

            chord.beginGetData(byteSgKey, new AsyncCallback(processGetDataForExereciseChord), null);
		}

        void processGetDataForExereciseChord(IAsyncResult result)
		{
            Console.WriteLine("TashjikClient::Client::processGetPredecessorForStabilize ENTER");

 /*           ChordCommon.IChordNode_Object iNode_Object = (ChordCommon.IChordNode_Object)(result.AsyncState);
            Object appState = iNode_Object.obj;
            
            AsyncCallback callBack = ((Tashjik.Common.AsyncCallback_Object)appState).callBack;
            Object appState1 = ((Tashjik.Common.AsyncCallback_Object)appState).obj;

            IChordNode x = iNode_Object.node;		
*/		}
		
		private void chkChord()
		{
			Console.WriteLine("Entering chkChord");
	
			//transportLayerCommunicator.register(ClientGuid, this);
		
			//Console.WriteLine("Creating new Chord overlay");
			Guid chordInstanceGuid;
			//IPAddress bootStrapIP =
			requestBootStrapNode();
	/*		ChordServer chord;
			
			if(bootStrapIP == null)
			{
				chord = (ChordServer)(TashjikServer.createNew("Chord")); //new Guid("0c400880-0722-420e-a792-0a764d6539ee")));
				chordInstanceGuid = chord.getGuid();
			}
			else
				chord = (ChordServer)(TashjikServer.joinExisting(bootStrapIP, new Guid("0c400880-0722-420e-a792-0a764d6539ee"), chordInstanceGuid));
	*/		

/*			String strKey = "key";
			String strData = "data";
			Console.WriteLine(strKey);
			Console.WriteLine(strData);
			
			byte[] key = System.Text.Encoding.ASCII.GetBytes(strKey);
			byte[] data = System.Text.Encoding.ASCII.GetBytes(strData);
			Console.WriteLine(key);
			Console.WriteLine(data);
			
			Console.WriteLine(Encoding.ASCII.GetString(key));
			Console.WriteLine(Encoding.ASCII.GetString(data));
			
			Console.WriteLine(key.ToString());
			Console.WriteLine(data.ToString());
			
			Console.WriteLine("Putting data to new Chord ");
			chord.beginPutData(key, data, 0, strData.Length, new AsyncCallback(processPutDataCallBack), chord);
			Console.WriteLine("After Putting data to new Chord ");
*/		
			
			
			
			
			
			
			
			
			
			
			
			/*			//ChordServer chord = (ChordServer)(TashjikServer.createNew(String.Chord));
			ArrayList arr = TashjikServer.getList(String.Chord);
			ChordServer chord = (ChordServer)(arr[0]);
			
			String key = "key";
			Tashjik.Common.Data data = new Tashjik.Common.Data();
			AsyncCallback putDataCallBack = new AsyncCallback(processPutDataCallBack);
			//chord.beginGetData(key, data, putDataCallBack, null);
*/
//Tashjik.Server.Node node = new Tashjik.Server.Node();
			//Console.WriteLine("Please enter IP address of node to send msg to");
			//String IP = Console.ReadLine();
		}
		
		public void notifyOneWayMsg(IPAddress fromIP, byte[] buffer, int offset, int size)
		{
		
			Console.WriteLine("Msg received");
			Console.WriteLine();
			Console.WriteLine();
			Console.WriteLine();
			Console.WriteLine();
			Console.WriteLine();
			
			receiveBootStrapNode(fromIP, buffer, offset, size);
		}
		
		public Tashjik.Tier0.TransportLayerCommunicator.Data notifyTwoWayMsg(IPAddress fromIP, byte[] buffer, int offset, int size)
		{
			Console.WriteLine("Client::notifyTwoWayMsg ENTER");
			return null;
		}
		
		public Tashjik.Tier0.TransportLayerCommunicator.Data notifyTwoWayRelayMsg(IPAddress fromIP, IPAddress originalFromIP, byte[] buffer, int offset, int size, Guid relayTicket)
		{
			Console.WriteLine("Client::notifyTwoWayRelayMsg ENTER");
			return null;
		}
		
		public void notifyTwoWayReplyReceived(IPAddress fromIP, byte[] buffer, int offset, int size, AsyncCallback originalRequestCallBack, Object originalAppState)
		{
			
		}
		

		Guid ClientGuid = new Guid("2527df07-e8c5-4f0d-a46e-effa26cfcb0d");
		
		public void chkTransPortLayerComm()
		{
			transportLayerCommunicator.register(ClientGuid, this);
			Console.WriteLine("Please enter IP address of node to send msg to");
			String IP = Console.ReadLine();
			IPAddress ipAddress = UtilityMethod.convertStrToIP(IP);

			String strMsg = "Client sending msg ";
			byte[] msg = System.Text.Encoding.ASCII.GetBytes(strMsg);

			transportLayerCommunicator.BeginTransportLayerSendOneWay(ipAddress, msg, 0, strMsg.Length, ClientGuid, new AsyncCallback(sendDataCallBack), ipAddress);
		}
		
		static void sendDataCallBack(IAsyncResult result)
		{
			IPAddress IP = (IPAddress)(result.AsyncState);
			try
			{
				transportLayerCommunicator.EndTransportLayerSend(IP);
			}
			catch(SocketException)
			{
				Console.WriteLine("Client caught SocketException");
			}
			
		}
		
		static void processPutDataCallBack(IAsyncResult result)
		{
			ChordServer chord = (ChordServer)(result.AsyncState);
			String strKey = "key";
			byte[] key = System.Text.Encoding.ASCII.GetBytes(strKey);
			chord.beginGetData(key, new AsyncCallback(processGetDataCallBack), null);
		}
		
		static void processGetDataCallBack(IAsyncResult result)
		{
			Tashjik.Common.Data_Object data_Object = (Tashjik.Common.Data_Object)(result.AsyncState);
			byte[] data = data_Object.data;
			Console.Write("DATA FOUND: IT IS ");
			Console.WriteLine(Encoding.ASCII.GetString(data));
			//#if DINKUM
		//	Console.WriteLine("tada");
		//	#endif			                  
		//	log.Info(data);
							
		}
		
		
	}
}
