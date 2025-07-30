#if USE_UNI_LUA
using LuaAPI = UniLua.Lua;
using RealStatePtr = UniLua.ILuaState;
using LuaCSFunction = UniLua.CSharpFunctionDelegate;
#else
using LuaAPI = XLua.LuaDLL.Lua;
using RealStatePtr = System.IntPtr;
using LuaCSFunction = XLua.LuaDLL.lua_CSFunction;
#endif

using XLua;
using System.Collections.Generic;


namespace XLua.CSObjectWrap
{
    using Utils = XLua.Utils;
    public class JyGameRollRoleUIWrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			System.Type type = typeof(JyGame.RollRoleUI);
			Utils.BeginObjectRegister(type, L, translator, 0, 7, 17, 12);
			
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "LoadSelection", _m_LoadSelection);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Show", _m_Show);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GenerateEmptyItems", _m_GenerateEmptyItems);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "MakeRandomCondition", _m_MakeRandomCondition);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "MakeZhoumuAndShilianBonus", _m_MakeZhoumuAndShilianBonus);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "okButton_Click", _m_okButton_Click);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "resetButton_Click", _m_resetButton_Click);
			
			
			Utils.RegisterFunc(L, Utils.GETTER_IDX, "nameInputPanel", _g_get_nameInputPanel);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "headSelectMenu", _g_get_headSelectMenu);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "rolePanel", _g_get_rolePanel);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "SelectTitle", _g_get_SelectTitle);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "selectMenu", _g_get_selectMenu);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "rolePanelObj", _g_get_rolePanelObj);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "headSelectPanelObj", _g_get_headSelectPanelObj);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "NameInputPanel", _g_get_NameInputPanel);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "selectPanelObj", _g_get_selectPanelObj);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "MultiSelectItemObj", _g_get_MultiSelectItemObj);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "RoleConfirmButtonObj", _g_get_RoleConfirmButtonObj);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "RoleResetButtonObj", _g_get_RoleResetButtonObj);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "results", _g_get_results);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "makeRole", _g_get_makeRole);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "makeMoney", _g_get_makeMoney);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "makeItems", _g_get_makeItems);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "selectHeadKey", _g_get_selectHeadKey);
            
			Utils.RegisterFunc(L, Utils.SETTER_IDX, "rolePanelObj", _s_set_rolePanelObj);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "headSelectPanelObj", _s_set_headSelectPanelObj);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "NameInputPanel", _s_set_NameInputPanel);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "selectPanelObj", _s_set_selectPanelObj);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "MultiSelectItemObj", _s_set_MultiSelectItemObj);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "RoleConfirmButtonObj", _s_set_RoleConfirmButtonObj);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "RoleResetButtonObj", _s_set_RoleResetButtonObj);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "results", _s_set_results);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "makeRole", _s_set_makeRole);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "makeMoney", _s_set_makeMoney);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "makeItems", _s_set_makeItems);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "selectHeadKey", _s_set_selectHeadKey);
            
			
			Utils.EndObjectRegister(type, L, translator, null, null,
			    null, null, null);

		    Utils.BeginClassRegister(type, L, __CreateInstance, 1, 0, 0);
			
			
            
			
			
			
			Utils.EndClassRegister(type, L, translator);
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CreateInstance(RealStatePtr L)
        {
            
			try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
				if(LuaAPI.lua_gettop(L) == 1)
				{
					
					var gen_ret = new JyGame.RollRoleUI();
					translator.Push(L, gen_ret);
                    
					return 1;
				}
				
			}
			catch(System.Exception gen_e) {
				return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
			}
            return LuaAPI.luaL_error(L, "invalid arguments to JyGame.RollRoleUI constructor!");
            
        }
        
		
        
		
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_LoadSelection(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                JyGame.RollRoleUI gen_to_be_invoked = (JyGame.RollRoleUI)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 3&& translator.Assignable<JyGame.StoryAction>(L, 2)&& translator.Assignable<JyGame.CommonSettings.IntCallBack>(L, 3)) 
                {
                    JyGame.StoryAction _selection = (JyGame.StoryAction)translator.GetObject(L, 2, typeof(JyGame.StoryAction));
                    JyGame.CommonSettings.IntCallBack _callback = translator.GetDelegate<JyGame.CommonSettings.IntCallBack>(L, 3);
                    
                    gen_to_be_invoked.LoadSelection( _selection, _callback );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 4&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& (LuaAPI.lua_isnil(L, 3) || LuaAPI.lua_type(L, 3) == LuaTypes.LUA_TTABLE)&& (LuaAPI.lua_isnil(L, 4) || LuaAPI.lua_type(L, 4) == LuaTypes.LUA_TFUNCTION)) 
                {
                    string _title = LuaAPI.lua_tostring(L, 2);
                    XLua.LuaTable _opts = (XLua.LuaTable)translator.GetObject(L, 3, typeof(XLua.LuaTable));
                    XLua.LuaFunction _callback = (XLua.LuaFunction)translator.GetObject(L, 4, typeof(XLua.LuaFunction));
                    
                    gen_to_be_invoked.LoadSelection( _title, _opts, _callback );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 4&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& translator.Assignable<System.Collections.Generic.List<string>>(L, 3)&& translator.Assignable<JyGame.CommonSettings.IntCallBack>(L, 4)) 
                {
                    string _title = LuaAPI.lua_tostring(L, 2);
                    System.Collections.Generic.List<string> _opts = (System.Collections.Generic.List<string>)translator.GetObject(L, 3, typeof(System.Collections.Generic.List<string>));
                    JyGame.CommonSettings.IntCallBack _callback = translator.GetDelegate<JyGame.CommonSettings.IntCallBack>(L, 4);
                    
                    gen_to_be_invoked.LoadSelection( _title, _opts, _callback );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to JyGame.RollRoleUI.LoadSelection!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Show(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                JyGame.RollRoleUI gen_to_be_invoked = (JyGame.RollRoleUI)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.Show(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GenerateEmptyItems(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                JyGame.RollRoleUI gen_to_be_invoked = (JyGame.RollRoleUI)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                        var gen_ret = gen_to_be_invoked.GenerateEmptyItems(  );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_MakeRandomCondition(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                JyGame.RollRoleUI gen_to_be_invoked = (JyGame.RollRoleUI)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.MakeRandomCondition(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_MakeZhoumuAndShilianBonus(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                JyGame.RollRoleUI gen_to_be_invoked = (JyGame.RollRoleUI)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.MakeZhoumuAndShilianBonus(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_okButton_Click(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                JyGame.RollRoleUI gen_to_be_invoked = (JyGame.RollRoleUI)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.okButton_Click(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_resetButton_Click(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                JyGame.RollRoleUI gen_to_be_invoked = (JyGame.RollRoleUI)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.resetButton_Click(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_nameInputPanel(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                JyGame.RollRoleUI gen_to_be_invoked = (JyGame.RollRoleUI)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.nameInputPanel);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_headSelectMenu(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                JyGame.RollRoleUI gen_to_be_invoked = (JyGame.RollRoleUI)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.headSelectMenu);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_rolePanel(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                JyGame.RollRoleUI gen_to_be_invoked = (JyGame.RollRoleUI)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.rolePanel);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_SelectTitle(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                JyGame.RollRoleUI gen_to_be_invoked = (JyGame.RollRoleUI)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.SelectTitle);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_selectMenu(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                JyGame.RollRoleUI gen_to_be_invoked = (JyGame.RollRoleUI)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.selectMenu);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_rolePanelObj(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                JyGame.RollRoleUI gen_to_be_invoked = (JyGame.RollRoleUI)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.rolePanelObj);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_headSelectPanelObj(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                JyGame.RollRoleUI gen_to_be_invoked = (JyGame.RollRoleUI)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.headSelectPanelObj);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_NameInputPanel(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                JyGame.RollRoleUI gen_to_be_invoked = (JyGame.RollRoleUI)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.NameInputPanel);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_selectPanelObj(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                JyGame.RollRoleUI gen_to_be_invoked = (JyGame.RollRoleUI)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.selectPanelObj);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_MultiSelectItemObj(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                JyGame.RollRoleUI gen_to_be_invoked = (JyGame.RollRoleUI)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.MultiSelectItemObj);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_RoleConfirmButtonObj(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                JyGame.RollRoleUI gen_to_be_invoked = (JyGame.RollRoleUI)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.RoleConfirmButtonObj);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_RoleResetButtonObj(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                JyGame.RollRoleUI gen_to_be_invoked = (JyGame.RollRoleUI)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.RoleResetButtonObj);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_results(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                JyGame.RollRoleUI gen_to_be_invoked = (JyGame.RollRoleUI)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.results);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_makeRole(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                JyGame.RollRoleUI gen_to_be_invoked = (JyGame.RollRoleUI)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.makeRole);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_makeMoney(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                JyGame.RollRoleUI gen_to_be_invoked = (JyGame.RollRoleUI)translator.FastGetCSObj(L, 1);
                LuaAPI.xlua_pushinteger(L, gen_to_be_invoked.makeMoney);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_makeItems(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                JyGame.RollRoleUI gen_to_be_invoked = (JyGame.RollRoleUI)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.makeItems);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_selectHeadKey(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                JyGame.RollRoleUI gen_to_be_invoked = (JyGame.RollRoleUI)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushstring(L, gen_to_be_invoked.selectHeadKey);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_rolePanelObj(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                JyGame.RollRoleUI gen_to_be_invoked = (JyGame.RollRoleUI)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.rolePanelObj = (UnityEngine.GameObject)translator.GetObject(L, 2, typeof(UnityEngine.GameObject));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_headSelectPanelObj(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                JyGame.RollRoleUI gen_to_be_invoked = (JyGame.RollRoleUI)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.headSelectPanelObj = (UnityEngine.GameObject)translator.GetObject(L, 2, typeof(UnityEngine.GameObject));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_NameInputPanel(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                JyGame.RollRoleUI gen_to_be_invoked = (JyGame.RollRoleUI)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.NameInputPanel = (UnityEngine.GameObject)translator.GetObject(L, 2, typeof(UnityEngine.GameObject));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_selectPanelObj(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                JyGame.RollRoleUI gen_to_be_invoked = (JyGame.RollRoleUI)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.selectPanelObj = (UnityEngine.GameObject)translator.GetObject(L, 2, typeof(UnityEngine.GameObject));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_MultiSelectItemObj(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                JyGame.RollRoleUI gen_to_be_invoked = (JyGame.RollRoleUI)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.MultiSelectItemObj = (UnityEngine.GameObject)translator.GetObject(L, 2, typeof(UnityEngine.GameObject));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_RoleConfirmButtonObj(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                JyGame.RollRoleUI gen_to_be_invoked = (JyGame.RollRoleUI)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.RoleConfirmButtonObj = (UnityEngine.GameObject)translator.GetObject(L, 2, typeof(UnityEngine.GameObject));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_RoleResetButtonObj(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                JyGame.RollRoleUI gen_to_be_invoked = (JyGame.RollRoleUI)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.RoleResetButtonObj = (UnityEngine.GameObject)translator.GetObject(L, 2, typeof(UnityEngine.GameObject));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_results(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                JyGame.RollRoleUI gen_to_be_invoked = (JyGame.RollRoleUI)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.results = (System.Collections.Generic.List<int>)translator.GetObject(L, 2, typeof(System.Collections.Generic.List<int>));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_makeRole(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                JyGame.RollRoleUI gen_to_be_invoked = (JyGame.RollRoleUI)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.makeRole = (JyGame.Role)translator.GetObject(L, 2, typeof(JyGame.Role));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_makeMoney(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                JyGame.RollRoleUI gen_to_be_invoked = (JyGame.RollRoleUI)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.makeMoney = LuaAPI.xlua_tointeger(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_makeItems(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                JyGame.RollRoleUI gen_to_be_invoked = (JyGame.RollRoleUI)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.makeItems = (System.Collections.Generic.List<JyGame.Item>)translator.GetObject(L, 2, typeof(System.Collections.Generic.List<JyGame.Item>));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_selectHeadKey(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                JyGame.RollRoleUI gen_to_be_invoked = (JyGame.RollRoleUI)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.selectHeadKey = LuaAPI.lua_tostring(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
		
		
		
		
    }
}
