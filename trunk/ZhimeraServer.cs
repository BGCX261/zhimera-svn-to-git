/************************************************************
* File Name: Zhimera.cs
*
* Author: Ratul Mukhopadhyay
* ratuldotmukhATgmaildotcom
*
* This software is licensed under the terms and conditions of
* the MIT license, as given below.
*
* Copyright (c) <2009> <Ratul Mukhopadhyay>
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



using System;
using System.Runtime.CompilerServices;
using Tashjik;
using System.Net;
using Zhimera.Storage;
	
[assembly:InternalsVisibleTo("ZhimeraTest")]

namespace Zhimera
{
	/// <summary>
	/// Description of Zhimer.
	/// </summary>
	public  class ZhimeraServer
	{
		public const string zhimeraGUID = "6da2a748-b42f-4e79-a5f1-60a11936237d";

		private ZhimeraRealNode zhimeraRealNode;
        private ProxyNodeController proxyNodeController;
		       
        public ZhimeraServer()
        {
            proxyNodeController = new ProxyNodeController(new ProxyNodeController.CreateProxyNodeDelegate(createZhimeraProxyNode), new Guid(zhimeraGUID));
            zhimeraRealNode = new ZhimeraRealNode(proxyNodeController);
            
        }
        

        public ZhimeraServer(IPAddress bootStrapIP, Guid bootStrapChordInstanceGuid)
        {
            proxyNodeController = new ProxyNodeController(new ProxyNodeController.CreateProxyNodeDelegate(createZhimeraProxyNode), new Guid(zhimeraGUID));
            zhimeraRealNode = new ZhimeraRealNode(proxyNodeController, bootStrapIP, bootStrapChordInstanceGuid);
        }

        public void storeVideo()
        {
        }

        

        private static ProxyNode createZhimeraProxyNode(IPAddress IP, ProxyNodeController proxyNodeController)
        {
            return new ZhimeraProxyNode(IP, /*base.getProxyNodeController*/proxyNodeController);
            //return new ChordProxyNode(IP, base.getProxyNodeController());
            //return null;
        }


        /*		private void configure(System.IO.FileInfo fileInfo)
                {
                    Tashjik.Tier0.TransportLayerCommunicator transportLayerCommunicator = Tashjik.Tier0.TransportLayerCommunicator.getRefTransportLayerCommunicator();
                    transportLayerCommunicator.register(new Guid(zhimeraGUID), proxyZhimeraNodeController);
			
                }
        */
		
	}
}
