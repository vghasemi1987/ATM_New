using System.Collections.Generic;

namespace Infrastructure.Data.ApplicationUserAggregate
{
    public class UserPanelMenu
    {
        public string Text { get; set; }
        public string Link { get; set; }
        public string Icon { get; set; }
        public List<UserPanelMenu> Items { get; set; }
    }
}
