﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Chess.Lib.Tests
{
	[TestClass]
	public unsafe class TTableTest
	{
		[TestMethod]
		public void TesTTable1()
		{
			Assert.AreEqual(16777216, TTable.GetTableSize());
		}
	}
}
