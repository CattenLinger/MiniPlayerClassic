using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MiniPlayerClassic
{
    struct PlayListItem
    {
        public string FileAddress = "";
        public string Infomations = "";

        public string Singer = "";
        public string Title = "";
        public string Album = "";

        public void refresh_info()
        {
            string temp = Infomations;

        }

        public PlayListItem()
        {
            if (FileAddress == "" || Infomations == "") { return; }
            refresh_info();
        }
    }

    public class PlayList
    {

    }
}
