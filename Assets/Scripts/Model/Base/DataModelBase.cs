using System;

namespace Model.Base
{
    /// <summary>
    /// 数据模型基类。
    /// 所有手动重写的 Model 类都应继承此类。
    /// 子类需 override Id 指向自己的主键字段。
    /// </summary>
    [Serializable]
    public abstract class DataModelBase : IDataModel
    {
        /// <summary>
        /// 数据条目的唯一标识，对应 JSON 中的 "$id" 字段。
        /// 子类 override 返回实际的主键属性值。
        /// </summary>
        public abstract string Id { get; }

        /// <summary>
        /// 编辑器中的显示名称，默认返回 Id。
        /// </summary>
        public virtual string DisplayName => Id;

        public override string ToString() => DisplayName;
    }
}
