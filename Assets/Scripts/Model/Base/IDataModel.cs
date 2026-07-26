namespace Model.Base
{
    /// <summary>
    /// 所有数据模型的统一接口。
    /// 每个数据条目必须有一个唯一标识 Id。
    /// </summary>
    public interface IDataModel
    {
        string Id { get; }
    }
}
