using Empire.NPC.S1API_NPCs;
using NUnit.Framework;
using System;
using System.IO;
using System.Reflection;
using UnityEngine;

namespace EmpireTest
{
	[TestFixture]
	public class EmpireTests
	{
		[Test]
		[TestCase("combo_costco.png")]
		[TestCase("tuco.png")]
		public void Icon_ShouldLoad(string fileName)
		{
			string resourcePath = $"Empire.NPC.S1API_NPCs.Icons.{fileName}";

			var asm = typeof(EmpireNPC).Assembly;
			using Stream? stream = asm.GetManifestResourceStream(resourcePath);
			Assert.That(stream, Is.Not.Null);
		}
	}
}
