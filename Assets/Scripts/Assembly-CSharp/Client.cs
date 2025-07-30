using UnityEngine;

public class Client : MonoBehaviour
{
	private LuaScriptMgr luaMgr;

	private void Start()
	{
		luaMgr = new LuaScriptMgr();
		luaMgr.Start();
		luaMgr.DoFile("System.Test");
	}

	private void Update()
	{
		if (luaMgr != null)
		{
			luaMgr.Update();
		}
	}

	private void LateUpdate()
	{
		if (luaMgr != null)
		{
			luaMgr.LateUpate();
		}
	}

	private void FixedUpdate()
	{
		if (luaMgr != null)
		{
			luaMgr.FixedUpdate();
		}
	}

	private void OnGUI()
	{
		if (GUI.Button(new Rect(10f, 10f, 120f, 50f), "Test"))
		{
			float realtimeSinceStartup = Time.realtimeSinceStartup;
			Vector3 one = Vector3.one;
			for (int i = 0; i < 200000; i++)
			{
				one = base.transform.position;
				base.transform.position = Vector3.one;
			}
			Debug.Log("c# cost time: " + (Time.realtimeSinceStartup - realtimeSinceStartup));
			base.transform.position = Vector3.zero;
			luaMgr.CallLuaFunction("Test");
		}
		if (GUI.Button(new Rect(10f, 70f, 120f, 50f), "Test2"))
		{
			float realtimeSinceStartup2 = Time.realtimeSinceStartup;
			for (int j = 0; j < 200000; j++)
			{
				base.transform.Rotate(Vector3.up, 1f);
			}
			Debug.Log("c# cost time: " + (Time.realtimeSinceStartup - realtimeSinceStartup2));
			luaMgr.CallLuaFunction("Test2", base.transform);
		}
		if (GUI.Button(new Rect(10f, 130f, 120f, 50f), "Test3"))
		{
			float realtimeSinceStartup3 = Time.realtimeSinceStartup;
			Vector3 one2 = Vector3.one;
			for (int k = 0; k < 200000; k++)
			{
				one2 = new Vector3(k, k, k);
			}
			Debug.Log("c# cost time: " + (Time.realtimeSinceStartup - realtimeSinceStartup3));
			luaMgr.CallLuaFunction("Test3", base.transform);
		}
		if (GUI.Button(new Rect(10f, 190f, 120f, 50f), "Test4"))
		{
			float realtimeSinceStartup4 = Time.realtimeSinceStartup;
			for (int l = 0; l < 200000; l++)
			{
				GameObject gameObject = new GameObject();
			}
			Debug.Log("c# cost time: " + (Time.realtimeSinceStartup - realtimeSinceStartup4));
			luaMgr.CallLuaFunction("Test4", base.transform);
		}
		if (GUI.Button(new Rect(10f, 250f, 120f, 50f), "Test5"))
		{
			float realtimeSinceStartup5 = Time.realtimeSinceStartup;
			for (int m = 0; m < 20000; m++)
			{
				GameObject gameObject2 = new GameObject();
				gameObject2.AddComponent<SkinnedMeshRenderer>();
				SkinnedMeshRenderer component = gameObject2.GetComponent<SkinnedMeshRenderer>();
				component.castShadows = false;
				component.receiveShadows = false;
			}
			Debug.Log("c# cost time: " + (Time.realtimeSinceStartup - realtimeSinceStartup5));
			luaMgr.CallLuaFunction("Test5", base.transform);
		}
	}
}
