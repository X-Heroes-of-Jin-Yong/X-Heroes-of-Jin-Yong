using System;

namespace LuaInterface
{
	internal class ClassGenerator
	{
		private ObjectTranslator translator;

		private Type klass;

		public ClassGenerator(ObjectTranslator translator, Type klass)
		{
			this.translator = translator;
			this.klass = klass;
		}

		public object extractGenerated(IntPtr luaState, int stackPos)
		{
			return CodeGeneration.Instance.GetClassInstance(klass, translator.getTable(luaState, stackPos));
		}
	}
}
