using System;

namespace LuaInterface
{
	public abstract class LuaBase : IDisposable
	{
		private bool _Disposed;

		protected int _Reference;

		protected LuaState _Interpreter;

		protected ObjectTranslator translator;

		public string name;

		private int count;

		public LuaBase()
		{
			count = 1;
		}

		~LuaBase()
		{
			Dispose(false);
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		public void AddRef()
		{
			count++;
		}

		public void Release()
		{
			if (_Disposed || name == null)
			{
				Dispose();
				return;
			}
			count--;
			if (count > 0)
			{
				return;
			}
			if (name != null)
			{
				LuaScriptMgr mgrFromLuaState = LuaScriptMgr.GetMgrFromLuaState(_Interpreter.L);
				if (mgrFromLuaState != null)
				{
					mgrFromLuaState.RemoveLuaRes(name);
				}
			}
			Dispose();
		}

		public virtual void Dispose(bool disposeManagedResources)
		{
			if (_Disposed)
			{
				return;
			}
			if (_Reference != 0 && _Interpreter != null)
			{
				if (disposeManagedResources)
				{
					_Interpreter.dispose(_Reference);
					_Reference = 0;
				}
				else if (_Interpreter.L != IntPtr.Zero)
				{
					LuaScriptMgr.refGCList.Enqueue(new LuaRef(_Interpreter.L, _Reference));
					_Reference = 0;
				}
			}
			_Interpreter = null;
			_Disposed = true;
		}

		protected void PushArgs(IntPtr L, object o)
		{
			LuaScriptMgr.PushVarObject(L, o);
		}

		public override bool Equals(object o)
		{
			if (o is LuaBase)
			{
				LuaBase luaBase = (LuaBase)o;
				return _Interpreter.compareRef(luaBase._Reference, _Reference);
			}
			return false;
		}

		public override int GetHashCode()
		{
			return _Reference;
		}
	}
}
