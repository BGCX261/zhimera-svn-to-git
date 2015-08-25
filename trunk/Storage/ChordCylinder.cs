/************************************************************
* File Name: ChordCylinder.cs
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
using System.Collections.Generic;
using Tashjik;
using Tashjik.Tier0;
using System.Net;


[assembly:InternalsVisibleTo("ZhimeraTest")]
namespace Zhimera.Storage
{
	/// <summary>
	/// Description of ChordCylinder.
	/// </summary>
	internal class ChordCylinder //: TransportLayerCommunicator.ISink
	{
        private ProxyNodeController proxyNodeController;
        private RangeIChordRing[] registry; // = new RangeDictionary<double, IChordRing>();
        private IHalo halo;
		//private const string chordCylinderGUID  = "96a7a3df-54c3-4f14-b8f2-b3248e8675a5";
		
		private const int noOfChordRings = 5;
        private const double totalRange = 10.0;

        public ChordCylinder(IPAddress bootStrapIP, Guid bootStrapChordInstanceGuid, ProxyNodeController proxyNodeController)
        {
            this.proxyNodeController = proxyNodeController;

            
            registry = new RangeIChordRing[noOfChordRings];
            halo = new ProxyHalo();


            ZhimeraProxyNode bootstrapProxyNode = (ZhimeraProxyNode)(proxyNodeController.getProxyNode(bootStrapIP));

            double perRange = totalRange / (double)(noOfChordRings);
            for (int i = 0; i < noOfChordRings; i++)
            {
                

                registry[i] = new RangeIChordRing();
                registry[i].min = i * perRange;
                registry[i].max = (i + 1) * perRange;
                if (i == noOfChordRings / 2)
                    registry[i].chordRing = new RealChordRing(bootStrapIP, bootStrapChordInstanceGuid);
                else
                    registry[i].chordRing = new ProxyChordRing(halo, noOfChordRings);
            }
        }

        public ChordCylinder(ProxyNodeController proxyNodeController)
        {
            this.proxyNodeController = proxyNodeController;

            registry = new RangeIChordRing[noOfChordRings];
            halo = new RealHalo();

            double perRange = totalRange / (double)(noOfChordRings);
            for (int i = 0; i < noOfChordRings; i++)
            {
                registry[i] = new RangeIChordRing();
                registry[i].min = i * perRange;
                registry[i].max = (i + 1) * perRange;
                if(i == noOfChordRings/2)
                    registry[i].chordRing = new RealChordRing();
                else
                    registry[i].chordRing = new ProxyChordRing(halo, noOfChordRings);

            }

            
			
		}

        public List<ZhimeraProxyNode> getHaloBootstrap()
        {
            return null;
        }

        public class RangeIChordRing
        {
            public double min;
            public double max;
            public IChordRing chordRing;
        }

        
        
        
        
        
        
        
        
        

	}
		
}
