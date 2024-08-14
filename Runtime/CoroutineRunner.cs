using System.Collections;
using UnityEngine;

namespace Lvl3Mage.CoroutineToolkit
{
	public static class CoroutineRunner
	{
		static CoroutineRunnerBehaviour instance;
		static CoroutineRunnerBehaviour AccessInstance()
		{
			if (instance != null) return instance;
			
			instance = new GameObject("CoroutineRunner").AddComponent<CoroutineRunnerBehaviour>();
			Object.DontDestroyOnLoad(instance.gameObject);
			return instance;
		}
		/// <summary>
		/// Allows a non-monobehaviour class to start a coroutine
		/// </summary>
		/// <param name="method">The coroutine to start, passed in as an IEnumerator</param>
		/// <returns>The started coroutine</returns>
		public static Coroutine StartCoroutine(IEnumerator method)
		{
			return AccessInstance().StartCoroutine(method);
		}
		/// <summary>
		/// Allows a non-monobehaviour class to stop a coroutine
		/// </summary>
		/// <param name="coroutine">The coroutine to stop</param>
		public static void StopCoroutine(Coroutine coroutine)
		{
			AccessInstance().StopCoroutine(coroutine);
		}
	}
	/// <summary>
	/// The MonoBehaviour that actually runs the coroutines
	/// </summary>
	/// <remarks>
	///	Don't use this class directly, use <see cref="CoroutineRunner"/> instead
	/// </remarks>
	public class CoroutineRunnerBehaviour : MonoBehaviour
	{
		void OnDestroy()
		{
			StopAllCoroutines();
		}
	}
}