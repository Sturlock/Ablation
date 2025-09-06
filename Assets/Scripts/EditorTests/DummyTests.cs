using System.Collections;
using NUnit.Framework;
using UnityEngine.TestTools;

namespace Owl.EditorTests
{
	public class DummyTests
	{
		// A Test behaves as an ordinary method
		[Test]
		public void DummyTestsSimplePasses()
		{
			// Use the Assert class to test conditions
			Assert.Pass();
		}

		// A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
		// `yield return null;` to skip a frame.
		[UnityTest]
		public IEnumerator DummyTestsWithEnumeratorPasses()
		{
			// Use the Assert class to test conditions.
			// Use yield to skip a frame.
			Assert.Pass();
			yield return null;
		}
	}
}
