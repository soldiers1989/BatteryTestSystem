using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace 电池测试系统
{
    public static class MessageToolscs
    {
        public static void Tools(string Str)
        {
            MessageTools mt = new MessageTools(Str);
            mt.Show();
        }
    }
}
