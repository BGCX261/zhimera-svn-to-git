/************************************************************
* File Name: ChordCylinderManager.cs 
*
* Author: Ratul Mukhopadhyay
* ratuldotmukhATgmaildotcom
*
* This software is licensed under the terms and conditions of
* the MIT license, as given below.
*
* Copyright (c) <2008> <Ratul Mukhopadhyay>
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
using System.Collections.Generic;
//using Tashjik.Tier0;

namespace Zhimera.Storage
{
	/// <summary>
	/// Description of ChordCylinderManager.
	/// </summary>
	public class ChordCylinderManager
	{
/*		private static readonly  ChordCylinderConfigurator chordCylinderConfigurator = ChordCylinderConfigurator.getChordCylinderConfigurator();
		private  ChordCylinder chordCylinder;
		
		private ChordCylinderManager()
		{
			ChordCylinder.BeginChordCylinderStoreDelegate chordCylinderStoreDelegate = chordCylinderConfigurator.getChordCylinderStore();
			ProxyChordRing.ChordRingFindNodeDelegate chordRingFindNodeDelegate = chordCylinderConfigurator.getChordRingFindNode();
			ProxyChordRing.ChordRingStoreDelegate chordRingStoreDelegate = chordCylinderConfigurator.getChordRingStore();
			
			chordCylinder = ChordCylinder.getChordCylinder(chordCylinderStoreDelegate, chordRingFindNodeDelegate, chordRingStoreDelegate);
		}
		
		//singleton
		private static ChordCylinderManager chordCylinderManager = null;
		
		//need to take care of threading issues
		public static ChordCylinderManager getChordCylinderManager()
		{
			if(chordCylinderManager!=null)
				return chordCylinderManager;
			else
			{
				chordCylinderManager = new ChordCylinderManager();
				return chordCylinderManager;
			}
		}
		public void beginStore(byte[] byteKey, List<Object> dataList, AsyncCallback storeCallBack, Object appState)
		{
			chordCylinder.beginStore(byteKey, dataList, storeCallBack, appState);
		}

		class ChordCylinderConfigurator
		{

			//singleton
			private static ChordCylinderConfigurator chordCylinderConfigurator = null;
		
			//need to take care of threading issues
			public static ChordCylinderConfigurator getChordCylinderConfigurator()
			{
				if(chordCylinderConfigurator!=null)
					return chordCylinderConfigurator;
				else
				{	
					chordCylinderConfigurator = new ChordCylinderConfigurator();
					return chordCylinderConfigurator;
				}
			}
			
			
			public ChordCylinder.BeginChordCylinderStoreDelegate getChordCylinderStore()
			{
				return Type1_BeginChordCylinderStore;
			}
			
			public ProxyChordRing.ChordRingFindNodeDelegate getChordRingFindNode()
			{
				return Type1_ChordRingFindNode;
			}
			
			public ProxyChordRing.ChordRingStoreDelegate getChordRingStore()
			{
				return Type1_ChordRingStore;
			}
			
			private void Type1_BeginChordCylinderStore(byte[] byteKey, List<Object> dataList, string applicationGUID, RangeDictionary<double, IChordRing> chordRingRegistry, AsyncCallback storeCallBack, Object appState)
			{
				int randomRank = new Random().Next(0, 10); 
				try
				{
					IChordRing chordRing = chordRingRegistry.Retrieve(randomRank);
					foreach(Object obj in dataList)
						chordRing.beginStore(byteKey, obj, applicationGUID, storeCallBack, appState);
					
				}
				catch(KeyNotFoundException e)
				{
					Type1_BeginChordCylinderStore(byteKey, dataList, applicationGUID, chordRingRegistry, storeCallBack, appState);
				}
			}
			
			private ZhimeraProxyNode Type1_ChordRingFindNode(List<ZhimeraProxyNode> proxyZhimeraNodeList, RealChordRing realChordRing)
			{
				return null;
			}
			
			private void Type1_ChordRingStore(List<byte[]> chunkList, List<ZhimeraProxyNode> proxyZhimeraNodeList, RealChordRing realChordRing)
			{
				
			}

		}
		
		*/
		
	}
}
