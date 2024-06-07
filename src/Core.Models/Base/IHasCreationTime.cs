namespace Core.Models.Base;

public interface IHasCreationTime
{
    DateTime CreatedAt { get; set; }
}