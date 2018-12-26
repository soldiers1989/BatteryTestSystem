using BatteryTestSystem_Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BatteryTestSystem_Comm
{
    public static class UsersHelp
    {
        public static bool Login = false;

        /// <summary>
        /// 用户名
        /// </summary>
        public static string UserName = "";
        /// <summary>
        /// 密码
        /// </summary>
        public static string Password = "";

        /// <summary>
        /// 权限
        /// </summary>
        /// <summary>
        /// <summary>
        /// 管理员
        /// </summary>
        public static bool Admini = false;
        /// <summary>
        /// 删除数据
        /// </summary>
        public static bool DataDelete = false;
        /// <summary>
        /// 信息化登录权限
        /// </summary>
        public static bool InformationSystem = false;
        /// <summary>
        /// 编辑设备与设备参数权限
        /// </summary>
        public static bool ChangingEquipment = false;
        /// <summary>
        /// 更改服务器连接权限
        /// </summary>
        public static bool ServerConnection = false;
        /// <summary>
        /// 添加用户
        /// </summary>
        public static bool AddUser = false;
        /// <summary>
        /// 修改权限
        /// </summary>
        public static bool ModifyPermissions = false;

        public static void Logining(BatteryTestSystem_User Users, BatteryTestSystem_Permissions Permissions)
        {
            Login = true;

            UserName = Users.UserName;

            Password = Users.Password;

            Admini = Permissions.Admini;

            ChangingEquipment = Permissions.ChangingEquipment;

            DataDelete = Permissions.DataDelete;

            InformationSystem = Permissions.InformationSystem;

            ServerConnection = Permissions.ServerConnection;

            AddUser = Permissions.AddUser;

            ModifyPermissions = Permissions.ModifyPermissions;
        }
        /// <summary>
        /// 注销登录
        /// </summary>
        public static void Cancellation()
        {
            Login = false;

            UserName = "";

            Password = "";

            Admini = false;

            ChangingEquipment = false;

            DataDelete = false;

            InformationSystem = false;

            ServerConnection = false;

            AddUser = false;

            ModifyPermissions = false;
        }
    }
}
