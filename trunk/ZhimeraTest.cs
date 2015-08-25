/************************************************************
* File Name: ZhimeraTest.cs 
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
using log4net;
using log4net.Config;

namespace ZhimeraTest
{
	/// <summary>
	/// Description of ZhimeraTest.
	/// </summary>
	public class ZhimeraTest
	{
		private static readonly ILog log = LogManager.GetLogger(typeof(ZhimeraTest));
		
		public ZhimeraTest()
		{
		}
		
		public static void Main(string[] args)
		{
			log4net.Config.BasicConfigurator.Configure(); 
			log.Info("" + "ENTER application ZhimerTest");
			log.Info("BEGIN - Checking log4net");
			if (log.IsErrorEnabled)
			{
 			   log.Error("log4net Error channel enabled" );
			}
			if (log.IsDebugEnabled)
			{
    			log.Debug("log4net Debug channel enabled");
			}
			log.Info("END - Checking log4net");
			
			//log.Info("Configuring ZhimeraTest");
			//configure(new System.IO.FileInfo(args[0]));
			
			//generating new guids
			Guid g;
			// Create and display the value of two GUIDs.
    		g = Guid.NewGuid();
		    Console.WriteLine(g);	

			
			log.Info("EXIT application ZhimerTest");

		}
	}
}
