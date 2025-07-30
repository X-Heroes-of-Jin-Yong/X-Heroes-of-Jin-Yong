using UnityEngine;

public class TestOverride
{
	public enum Space
	{
		World = 1
	}

	public int Test(object o, string str)
	{
		Debug.Log("call Test(object o, string str)");
		return 1;
	}

	public int Test(char c)
	{
		Debug.Log("call Test(char c)");
		return 2;
	}

	public int Test(int i)
	{
		Debug.Log("call Test(int i)");
		return 3;
	}

	public int Test(double d)
	{
		Debug.Log("call Test(double d)");
		return 4;
	}

	public int Test(int i, int j)
	{
		Debug.Log("call Test(int i, int j)");
		return 5;
	}

	public int Test(string str)
	{
		Debug.Log("call Test(string str)");
		return 6;
	}

	public static int Test(string str1, string str2)
	{
		Debug.Log("call static Test(string str1, string str2)");
		return 7;
	}

	public int Test(object o)
	{
		Debug.Log("call Test(object o)");
		return 8;
	}

	public int Test(params object[] objs)
	{
		Debug.Log("call Test(params object[] objs)");
		return 9;
	}

	public int Test(Space e)
	{
		Debug.Log("call Test(TestEnum e)");
		return 10;
	}
}
