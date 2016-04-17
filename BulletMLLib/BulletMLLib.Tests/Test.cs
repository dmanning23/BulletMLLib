using NUnit.Framework;
using System;
using BulletMLLib;
using System.IO;
using FilenameBuddy;

namespace BulletMLTests
{
	[TestFixture()]
	public class Test
	{
		[SetUp()]
		public void setupHarness()
		{
			Filename.SetCurrentDirectory(@"C:\Projects\BulletMLLib\BulletMLLib\BulletMLLib.Tests\bin\Debug");
		}

		[Test()]
		public void ValidateTestData()
		{
			//Get all the xml files in the Content\\Samples directory
			var filename = new Filename("");
			foreach (var source in Directory.GetFiles(filename.GetPath(), "*.xml"))
			{
				//load & validate the pattern
				BulletPattern pattern = new BulletPattern();
				pattern.ParseXML(source);
			}
		}
	}
}

