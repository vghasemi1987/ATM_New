

namespace ApplicationCommon
{
    public static class Message
    {
        public static string Show(string message, MessageType type = MessageType.Info)
        {
            var title = string.Empty;
            switch (type)
            {
                case MessageType.Success:
                    title = "موفقیت";
                    break;
                case MessageType.Error:
                    title = "خطا";
                    break;
                case MessageType.Info:
                    title = "اطلاعات";
                    break;
                case MessageType.Warning:
                    title = "هشدار";
                    break;
            }
            return $"toastr[\"{type.ToString().ToLower()}\"](\"{message}\", \"{title}\");";
        }
    }

    public enum MessageType
    {
        Success,
        Error,
        Info,
        Warning
    }
}