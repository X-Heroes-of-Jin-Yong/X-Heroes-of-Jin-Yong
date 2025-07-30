using UnityEngine;

public class LuaClass : MonoBehaviour
{
	private const string source = "\n        Account = { balance = 0 };\n        \n        function Account:new(o)    \n            o = o or {};\n            setmetatable(o, { __index = self });     \n            return o;  \n        end  \n  \n        function Account.deposit(self, v)  \n            self.balance = self.balance + v;  \n        end  \n  \n        function Account:withdraw(v)  \n            if (v) > self.balance then error 'insufficient funds'; end  \n            self.balance = self.balance - v;  \n        end \n\n        SpecialAccount = Account:new();\n\n        function SpecialAccount:withdraw(v)  \n            if v - self.balance >= self:getLimit() then  \n                error 'insufficient funds';  \n            end  \n            self.balance = self.balance - v;  \n        end  \n  \n        function SpecialAccount.getLimit(self)  \n            return self.limit or 0;  \n        end  \n\n        s = SpecialAccount:new{ limit = 1000 };\n        print(s.balance);  \n        s:deposit(100.00);\n\n        print (s.limit);  \n        print (s.getLimit(s))  \n        print (s.balance)  \n    ";

	private void Start()
	{
		LuaScriptMgr luaScriptMgr = new LuaScriptMgr();
		luaScriptMgr.Start();
		luaScriptMgr.lua.DoString("\n        Account = { balance = 0 };\n        \n        function Account:new(o)    \n            o = o or {};\n            setmetatable(o, { __index = self });     \n            return o;  \n        end  \n  \n        function Account.deposit(self, v)  \n            self.balance = self.balance + v;  \n        end  \n  \n        function Account:withdraw(v)  \n            if (v) > self.balance then error 'insufficient funds'; end  \n            self.balance = self.balance - v;  \n        end \n\n        SpecialAccount = Account:new();\n\n        function SpecialAccount:withdraw(v)  \n            if v - self.balance >= self:getLimit() then  \n                error 'insufficient funds';  \n            end  \n            self.balance = self.balance - v;  \n        end  \n  \n        function SpecialAccount.getLimit(self)  \n            return self.limit or 0;  \n        end  \n\n        s = SpecialAccount:new{ limit = 1000 };\n        print(s.balance);  \n        s:deposit(100.00);\n\n        print (s.limit);  \n        print (s.getLimit(s))  \n        print (s.balance)  \n    ");
	}

	private void Update()
	{
	}
}
