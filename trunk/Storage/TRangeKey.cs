/*
 * Created by SharpDevelop.
 * User: ratul
 * Date: 7/12/2009
 * Time: 12:23 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */

using System;

namespace Zhimera.Storage
{
	/// <summary>
	/// Description of TRangeKey.
	/// </summary>
	public class TRangeKey : IComparable<TRangeKey>
	{
		public TRangeKey()
		{
		}
		
		public static bool operator>=(TRangeKey key1, TRangeKey key2)
		{
			return true;
		}
		
		public static bool operator<=(TRangeKey key1, TRangeKey key2)
		{
			return true;
		}

        public  int CompareTo(TRangeKey _TRangeKey)
        {
            return 0;
        }

	}
}
