namespace DomainEntities.NotificationAggregate
{
    public enum CategoryEnum:short
    {
        PostedTask = 1, //وظیفه ارسالی
        ChangeTaskState = 2, //تغییر وضعیت وظیفه
        DateOfDuty = 3, //موعد انجام وظیفه
        DateOfTest = 4 //موعد انجام تست
    }
}
