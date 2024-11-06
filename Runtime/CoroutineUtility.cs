using System;
using System.Collections;
using UnityEngine;

namespace Lvl3Mage.CoroutineToolkit
{
	public class CoroutineUtility
	{
		static IEnumerator WaitAll(Coroutine[] coroutines){
			int i = 0;
			foreach(Coroutine coroutine in coroutines){
				CoroutineRunner.StartCoroutine(CoroutineCallback(coroutine, () => i++));
			}
			yield return new WaitUntil(() => i == coroutines.Length);
		}

		static IEnumerator WaitAll(IEnumerator[] enumerators){
			int i = 0;
			foreach(IEnumerator enumerator in enumerators){
				CoroutineRunner.StartCoroutine(EnumeratorCallback(enumerator, () => i++));
			}
			yield return new WaitUntil(() => i == enumerators.Length);
		}

		static IEnumerator CoroutineCallback(Coroutine coroutine, Action callback){
			yield return coroutine;
			callback();

		}

		static IEnumerator EnumeratorCallback(IEnumerator enumerator, Action callback){
			yield return enumerator;
			callback();

		}
	}
}