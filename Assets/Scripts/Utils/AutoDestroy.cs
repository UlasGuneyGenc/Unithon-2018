using System.Collections;
using UnityEngine;

public class AutoDestroy : MonoBehaviour 
{
	public void Start ()
	{
		StartCoroutine(WaitAndDestroy());
	}

	private IEnumerator WaitAndDestroy()
	{
		yield return new WaitForSeconds(2F);
		Destroy(gameObject);
	}	
}
