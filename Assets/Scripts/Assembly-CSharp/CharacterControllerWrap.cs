using System;
using LuaInterface;
using UnityEngine;

public class CharacterControllerWrap
{
	private static Type classType = typeof(CharacterController);

	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[5]
		{
			new LuaMethod("SimpleMove", SimpleMove),
			new LuaMethod("Move", Move),
			new LuaMethod("New", _CreateCharacterController),
			new LuaMethod("GetClassType", GetClassType),
			new LuaMethod("__eq", Lua_Eq)
		};
		LuaField[] fields = new LuaField[10]
		{
			new LuaField("isGrounded", get_isGrounded, null),
			new LuaField("velocity", get_velocity, null),
			new LuaField("collisionFlags", get_collisionFlags, null),
			new LuaField("radius", get_radius, set_radius),
			new LuaField("height", get_height, set_height),
			new LuaField("center", get_center, set_center),
			new LuaField("slopeLimit", get_slopeLimit, set_slopeLimit),
			new LuaField("stepOffset", get_stepOffset, set_stepOffset),
			new LuaField("skinWidth", get_skinWidth, set_skinWidth),
			new LuaField("detectCollisions", get_detectCollisions, set_detectCollisions)
		};
		LuaScriptMgr.RegisterLib(L, "UnityEngine.CharacterController", typeof(CharacterController), regs, fields, typeof(Collider));
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int _CreateCharacterController(IntPtr L)
	{
		if (LuaDLL.lua_gettop(L) == 0)
		{
			CharacterController obj = new CharacterController();
			LuaScriptMgr.Push(L, obj);
			return 1;
		}
		LuaDLL.luaL_error(L, "invalid arguments to method: CharacterController.New");
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, classType);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_isGrounded(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		CharacterController characterController = (CharacterController)luaObject;
		if (characterController == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name isGrounded");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index isGrounded on a nil value");
			}
		}
		LuaScriptMgr.Push(L, characterController.isGrounded);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_velocity(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		CharacterController characterController = (CharacterController)luaObject;
		if (characterController == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name velocity");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index velocity on a nil value");
			}
		}
		LuaScriptMgr.Push(L, characterController.velocity);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_collisionFlags(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		CharacterController characterController = (CharacterController)luaObject;
		if (characterController == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name collisionFlags");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index collisionFlags on a nil value");
			}
		}
		LuaScriptMgr.Push(L, characterController.collisionFlags);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_radius(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		CharacterController characterController = (CharacterController)luaObject;
		if (characterController == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name radius");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index radius on a nil value");
			}
		}
		LuaScriptMgr.Push(L, characterController.radius);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_height(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		CharacterController characterController = (CharacterController)luaObject;
		if (characterController == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name height");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index height on a nil value");
			}
		}
		LuaScriptMgr.Push(L, characterController.height);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_center(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		CharacterController characterController = (CharacterController)luaObject;
		if (characterController == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name center");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index center on a nil value");
			}
		}
		LuaScriptMgr.Push(L, characterController.center);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_slopeLimit(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		CharacterController characterController = (CharacterController)luaObject;
		if (characterController == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name slopeLimit");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index slopeLimit on a nil value");
			}
		}
		LuaScriptMgr.Push(L, characterController.slopeLimit);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_stepOffset(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		CharacterController characterController = (CharacterController)luaObject;
		if (characterController == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name stepOffset");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index stepOffset on a nil value");
			}
		}
		LuaScriptMgr.Push(L, characterController.stepOffset);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_skinWidth(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		CharacterController characterController = (CharacterController)luaObject;
		if (characterController == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name skinWidth");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index skinWidth on a nil value");
			}
		}
		LuaScriptMgr.Push(L, characterController.skinWidth);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_detectCollisions(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		CharacterController characterController = (CharacterController)luaObject;
		if (characterController == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name detectCollisions");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index detectCollisions on a nil value");
			}
		}
		LuaScriptMgr.Push(L, characterController.detectCollisions);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_radius(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		CharacterController characterController = (CharacterController)luaObject;
		if (characterController == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name radius");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index radius on a nil value");
			}
		}
		characterController.radius = (float)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_height(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		CharacterController characterController = (CharacterController)luaObject;
		if (characterController == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name height");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index height on a nil value");
			}
		}
		characterController.height = (float)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_center(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		CharacterController characterController = (CharacterController)luaObject;
		if (characterController == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name center");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index center on a nil value");
			}
		}
		characterController.center = LuaScriptMgr.GetVector3(L, 3);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_slopeLimit(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		CharacterController characterController = (CharacterController)luaObject;
		if (characterController == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name slopeLimit");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index slopeLimit on a nil value");
			}
		}
		characterController.slopeLimit = (float)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_stepOffset(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		CharacterController characterController = (CharacterController)luaObject;
		if (characterController == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name stepOffset");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index stepOffset on a nil value");
			}
		}
		characterController.stepOffset = (float)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_skinWidth(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		CharacterController characterController = (CharacterController)luaObject;
		if (characterController == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name skinWidth");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index skinWidth on a nil value");
			}
		}
		characterController.skinWidth = (float)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_detectCollisions(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		CharacterController characterController = (CharacterController)luaObject;
		if (characterController == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name detectCollisions");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index detectCollisions on a nil value");
			}
		}
		characterController.detectCollisions = LuaScriptMgr.GetBoolean(L, 3);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int SimpleMove(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		CharacterController characterController = (CharacterController)LuaScriptMgr.GetUnityObjectSelf(L, 1, "CharacterController");
		Vector3 vector = LuaScriptMgr.GetVector3(L, 2);
		bool b = characterController.SimpleMove(vector);
		LuaScriptMgr.Push(L, b);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int Move(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		CharacterController characterController = (CharacterController)LuaScriptMgr.GetUnityObjectSelf(L, 1, "CharacterController");
		Vector3 vector = LuaScriptMgr.GetVector3(L, 2);
		CollisionFlags collisionFlags = characterController.Move(vector);
		LuaScriptMgr.Push(L, collisionFlags);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int Lua_Eq(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		UnityEngine.Object obj = LuaScriptMgr.GetLuaObject(L, 1) as UnityEngine.Object;
		UnityEngine.Object obj2 = LuaScriptMgr.GetLuaObject(L, 2) as UnityEngine.Object;
		bool b = obj == obj2;
		LuaScriptMgr.Push(L, b);
		return 1;
	}
}
