using System;
using System.Collections;
using UnityEngine;

namespace Lvl3Mage.CoroutineToolkit
{
	public class CoroutineUtility
	{
		public static IEnumerator WaitWithInterrupt(IEnumerator enumerator, IEnumerator interrupter,
			IEnumerator onInterrupt = null)
		{
			bool interrupted = false;
			Coroutine interruptCoroutine = CoroutineRunner.StartCoroutine(EnumeratorCallback(interrupter, () => interrupted = true));
			yield return WaitForAll(new[]{
				enumerator,
				new WaitUntil(() => interrupted)
			});
			if (interrupted && onInterrupt != null)
			{
				yield return onInterrupt;
			}
			if(interruptCoroutine != null){
				CoroutineRunner.StopCoroutine(interruptCoroutine);
			}
		}
		public static IEnumerator WaitWithInterrupt(IEnumerator enumerator, IEnumerator interrupter,
			Action onInterrupt = null)
		{
			bool interrupted = false;
			Coroutine interruptCoroutine = CoroutineRunner.StartCoroutine(EnumeratorCallback(interrupter, () => interrupted = true));
			yield return WaitForAll(new[]{
				enumerator,
				new WaitUntil(() => interrupted)
			});
			if (interrupted){
				onInterrupt?.Invoke();
			}
			if(interruptCoroutine != null){
				CoroutineRunner.StopCoroutine(interruptCoroutine);
			}
		}
		public static IEnumerator WaitForOne(IEnumerator[] enumerators)
		{
			Coroutine[] coroutines = new Coroutine[enumerators.Length];
			bool done = false;
			for (int i = 0; i < enumerators.Length; i++)
			{
				coroutines[i] = CoroutineRunner.StartCoroutine(EnumeratorCallback(enumerators[i], () => done = true));
			}
			yield return new WaitUntil(() => done);
			foreach (Coroutine c in coroutines){
				CoroutineRunner.StopCoroutine(c);
			}
		}
		public static IEnumerator WaitForAll(Coroutine[] coroutines){
			int i = 0;
			foreach(Coroutine coroutine in coroutines){
				CoroutineRunner.StartCoroutine(CoroutineCallback(coroutine, () => i++));
			}
			yield return new WaitUntil(() => i == coroutines.Length);
		}

		public static IEnumerator WaitForAll(IEnumerator[] enumerators){
			int i = 0;
			foreach(IEnumerator enumerator in enumerators){
				CoroutineRunner.StartCoroutine(EnumeratorCallback(enumerator, () => i++));
			}
			yield return new WaitUntil(() => i == enumerators.Length);
		}

		public static IEnumerator CoroutineCallback(Coroutine coroutine, Action callback){
			yield return coroutine;
			callback();

		}

		public static IEnumerator EnumeratorCallback(IEnumerator enumerator, Action callback){
			yield return enumerator;
			callback();

		}
	}
}