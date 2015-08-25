/************************************************************
* File Name: RangeDictionary.cs
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
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

[assembly:InternalsVisibleTo("ZhimeraTest")]
namespace Zhimera.Storage
{
	/// <summary>
	/// Description of RangeDisctionary.
	/// </summary>
	public class RangeDictionary<TRangeKey, TValue> :
		ICollection<RangeValueTriplet<TRangeKey, TValue>>, 
		IEnumerable<RangeValueTriplet<TRangeKey, TValue>> where TRangeKey : IComparable<TRangeKey>
		//ICollection, IEnumerable
	{
			
		List<RangeValueTriplet<TRangeKey, TValue>> list = new List<RangeValueTriplet<TRangeKey, TValue>>();
			
		public RangeDictionary()
		{
		}
		
		//IEnumerable interface members
/*		public IEnumerator GetEnumerator()
		{
			return new RangeDictionaryEnumerator;
		}
*/
		
		//ICollection interface members
		public void CopyTo(Array array, int index) {}
		//public IEnumerator GetEnumerator() see IEnumerable members

		public int Count { get {return-1;} }
		public bool IsSynchronized { get {return false;} }
		public Object SyncRoot { get {return false;} }

		//IEnumerable<T> interface members
		
		//ICollection<T> interface members
		public IEnumerator<RangeValueTriplet<TRangeKey, TValue>> GetEnumerator ()
		{
			return null;
		}	

		IEnumerator IEnumerable.GetEnumerator ()
		{
			return null;
		}	

		
		public void Add(RangeValueTriplet<TRangeKey, TValue> item)
		{
			list.Add(item);
		}
		
		public void Clear()
		{
			list.Clear();
		}
		
		public bool Contains(RangeValueTriplet<TRangeKey, TValue> val)
		{
			return false;
		}
		
		public bool Contains(TRangeKey key)
		{
			foreach (RangeValueTriplet<TRangeKey, TValue> triplet in list)
				if(key.CompareTo(triplet.rangeLower)>0 && key.CompareTo(triplet.rangeUpper)<0)
					return true;
			return false;
		}
		
		public TValue Retrieve(TRangeKey key)
		{
			foreach (RangeValueTriplet<TRangeKey, TValue> triplet in list)
				if(key.CompareTo(triplet.rangeLower)>0 && key.CompareTo(triplet.rangeUpper)<0)
					return triplet.val;
			throw new KeyNotFoundException();
		}
		
		public void CopyTo(RangeValueTriplet<TRangeKey, TValue>[] array, int index)
		{
			
		}
		
		public bool Remove(RangeValueTriplet<TRangeKey, TValue> item)
		{
			return false;
		}
		
		public bool IsReadOnly 
		{
			get
			{
				return false;
			}
		}
	}
}
