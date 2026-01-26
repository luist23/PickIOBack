namespace BaseProject.Models.Interfaces;

public interface IHasTimestamps
{
    long UpdateAt { get; set; }
    long CreateAt { get; set; }
}
