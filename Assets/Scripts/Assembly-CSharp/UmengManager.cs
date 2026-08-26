using UnityEngine;

public class UmengManager : MonoBehaviour
{
	private void Start()
	{
		Object.DontDestroyOnLoad(base.transform.gameObject);
	}
}
