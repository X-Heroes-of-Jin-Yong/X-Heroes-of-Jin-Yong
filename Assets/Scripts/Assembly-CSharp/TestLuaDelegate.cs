using UnityEngine;

public class TestLuaDelegate : MonoBehaviour
{
	public delegate void VoidDelegate(GameObject go);

	public VoidDelegate onClick;
}
