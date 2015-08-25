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

namespace Zhimera.Storage
{
	/// <summary>
	/// Description of IChordRing.
	/// </summary>
	public interface IChordRing
	{
		void beginStore(byte[] byteKey, Object data, string applicationGUID, AsyncCallback storeCallBack, Object appState);
	}
}
