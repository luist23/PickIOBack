namespace BaseProject.Models.Enums;

public enum OrderLog
{
    Login,
    PickingStart,
    PickingEnd,
    Justification,
    Manager,
    SyncBackend,
    SyncHH,
    Logout,
    All
}

public enum LogTypeLogin
{
    Desktop,
    App
}

public enum LogTypeManager
{
    User,
    Customer,
    BarCode,
    Justification
}