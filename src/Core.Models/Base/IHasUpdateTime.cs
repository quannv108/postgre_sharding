namespace Core.Models.Base;

public interface IHasUpdateTime
{
    DateTime UpdatedAt { get; set; }
}