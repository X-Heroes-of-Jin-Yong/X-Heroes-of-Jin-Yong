using System.Threading;

public class LockFreeQueue<T>
{
	public int head;

	public int tail;

	public T[] items;

	private int capacity;

	public int Count
	{
		get
		{
			return tail - head;
		}
	}

	public LockFreeQueue()
		: this(64)
	{
	}

	public LockFreeQueue(int count)
	{
		items = new T[count];
		tail = (head = 0);
		capacity = count;
	}

	public bool IsEmpty()
	{
		return head == tail;
	}

	public void Clear()
	{
		head = (tail = 0);
	}

	private bool IsFull()
	{
		return tail - head >= capacity;
	}

	public void Enqueue(T item)
	{
		while (IsFull())
		{
			Thread.Sleep(1);
		}
		int num = tail % capacity;
		items[num] = item;
		tail++;
	}

	public T Dequeue()
	{
		if (IsEmpty())
		{
			return default(T);
		}
		int num = head % capacity;
		T result = items[num];
		head++;
		return result;
	}
}
