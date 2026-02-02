using BaseProject.Models.Utils;

namespace BaseProject.Models.Data;

public abstract class TimeStampedModel
{
    public long UpdateAt { get; set; }
    public long CreateAt { get; set; }
    public long? DeleteAt { get; set; }

    public void Update()
    {
        UpdateAt = long.Parse(DateTime.UtcNow.ToString("yyyyMMddHHmmss"));
    }

    public void Delete(bool soft = true)
    {
        if (soft)
        {
            DeleteAt = long.Parse(DateTime.UtcNow.ToString("yyyyMMddHHmmss"));
        }
        else
        {
            // Hard delete logic usually handled by repository/context removal, 
            // but for the model state, we might just mark it. 
            // However, typically 'Delete(false)' might imply we want to clear it or it's just a flag.
            // Given the context, we'll just set the timestamp.
            DeleteAt = long.Parse(DateTime.UtcNow.ToString("yyyyMMddHHmmss"));
        }
    }
}
