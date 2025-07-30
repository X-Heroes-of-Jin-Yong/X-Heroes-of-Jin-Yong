using System.Collections.Generic;
using System.Xml.Serialization;

namespace JyGame
{
	[XmlType("event")]
	public class MapEvent
	{
		[XmlAttribute]
		public string type;

		[XmlAttribute]
		public string value;

		[XmlAttribute]
		public string image;

		[XmlAttribute("probability")]
		public int? probabilityValue;

		[XmlAttribute]
		public int lv = -1;

		[XmlAttribute]
		public string description;

		[XmlAttribute("repeat")]
		public string repeatValue;

		[XmlElement("condition")]
		public List<Condition> Conditions;

		[XmlIgnore]
		public int probability
		{
			get
			{
				int? num = probabilityValue;
				int result;
				if (!num.HasValue)
				{
					result = 100;
				}
				else
				{
					int? num2 = probabilityValue;
					result = num2.Value;
				}
				return result;
			}
		}

		public bool IsRepeatOnce
		{
			get
			{
				return repeatValue == "once";
			}
		}

		[XmlIgnore]
		public bool IsActive
		{
			get
			{
				if (IsRepeatOnce && RuntimeData.Instance.IsStoryFinished(value))
				{
					return false;
				}
				if (!Tools.ProbabilityTest((double)probability / 100.0))
				{
					return false;
				}
				foreach (Condition condition in Conditions)
				{
					if (!condition.IsTrue)
					{
						return false;
					}
				}
				return true;
			}
		}
	}
}
