using BaseProject.Models.Utils;

namespace BaseProject.Models;

public abstract class TimeStampedModel
{
    public long UpdatedAt { get; set; }
    public long CreatedAt { get; set; }
    public long? DeletedAt { get; set; }

    public void Update()
    {
        UpdatedAt = TimeUtil.GetTimeLong();
    }

    public void Delete(bool delete = true)
    {
        if (delete)
        {
            DeletedAt = TimeUtil.GetTimeLong();
        }
        else
        {
            DeletedAt = null;
        }
    }
}
