/*
 * Created by SharpDevelop.
 * User: ratul
 * Date: 7/13/2009
 * Time: 12:47 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */

using System;
using System.Collections;
using System.Collections.Generic;
namespace Zhimera
{
	/// <summary>
	/// Description of ZhimeraNode.
	/// </summary>
    internal interface IZhimeraNode 
	{
		void beginGetHaloBootstrap(AsyncCallback getHaloBootstrapCallBack, Object appState);
        void beginStoreContent(byte[] data, AsyncCallback storeContentCallBack, Object appState);
        void beginFindSuccessor(byte[] hashedKey, AsyncCallback findSuccessorCallBack, Object appState, Guid relayTicket);
        void beginGetRank(AsyncCallback getRankCallBack, Object appState);
        List<ZhimeraProxyNode> getHaloBootstrap();
        void storeContent(byte[] data);
        ZhimeraProxyNode findSuccessor(byte[] hashedKey, AsyncCallback findSuccessorCallBack, Object appState, Guid relayTicket);
        double[] getRank();
        
    }
}
