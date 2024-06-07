namespace Core.Models.Base;

public interface IEntity
{
    
}

public interface IEntity<T> : IEntity
{
    T Id { get; set; }
}

public interface IShardEntity<T> where T : struct
{
    T ShardKey { get; }
}