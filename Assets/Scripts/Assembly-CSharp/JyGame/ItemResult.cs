using System.Collections.Generic;

namespace JyGame
{
	public class ItemResult
	{
		public int Hp;

		public int Mp;

		public int DescPoisonLevel;

		public int DescPoisonDuration;

		public int Balls;

		public int MaxHp;

		public int MaxMp;

		public string UpgradeSkill = string.Empty;

		public string UpgradeInternalSkill = string.Empty;

		public IEnumerable<Buff> Buffs;

		/// <summary>物品扩展数据（替代原 LuaTable，存储 Lua 自定义的键值对）</summary>
		public Dictionary<string, object> data = new Dictionary<string, object>();
	}
}
