using BaseProject.Models.Utils;

namespace BaseProject.Models;

public abstract class TimeStampedModel
{
    public long UpdateAt { get; set; }
    public long CreateAt { get; set; }
    public long? DeleteAt { get; set; }

    public void Update()
    {
        UpdateAt = TimeUtil.GetTimeLong();
    }

    public void Delete(bool delete = true)
    {
        if (delete)
        {
            DeleteAt = TimeUtil.GetTimeLong();
        }
        else
        {
            DeleteAt = null;
        }
    }
}
