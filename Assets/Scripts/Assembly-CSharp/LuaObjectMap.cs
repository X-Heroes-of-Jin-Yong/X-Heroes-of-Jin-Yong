using System.Collections.Generic;

public class LuaObjectMap
{
	private List<object> list;

	private Queue<int> pool;

	public object this[int i]
	{
		get
		{
			return list[i];
		}
	}

	public LuaObjectMap()
	{
		list = new List<object>(1024);
		pool = new Queue<int>(1024);
	}

	public int Add(object obj)
	{
		int num = -1;
		if (pool.Count > 0)
		{
			num = pool.Dequeue();
			list[num] = obj;
		}
		else
		{
			list.Add(obj);
			num = list.Count - 1;
		}
		return num;
	}

	public bool TryGetValue(int index, out object obj)
	{
		if (index >= 0 && index < list.Count)
		{
			obj = list[index];
			return obj != null;
		}
		obj = null;
		return false;
	}

	public object Remove(int index)
	{
		if (index >= 0 && index < list.Count)
		{
			object obj = list[index];
			if (obj != null)
			{
				pool.Enqueue(index);
			}
			list[index] = null;
			return obj;
		}
		return null;
	}
}
