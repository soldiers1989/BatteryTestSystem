using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BatteryTestSystem_Model
{
    public class BatteryTestSystem_Permissions
    {
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 管理员
        /// </summary>
        public bool Admini { get; set; }
        /// <summary>
        /// 删除数据
        /// </summary>
        public bool DataDelete { get; set; }
        /// <summary>
        /// 信息化登录权限
        /// </summary>
        public bool InformationSystem { get; set; }
        /// <summary>
        /// 编辑设备与设备参数权限
        /// </summary>
        public bool ChangingEquipment { get;set;}
        /// <summary>
        /// 更改服务器连接权限
        /// </summary>
        public bool ServerConnection { get; set; }

        /// <summary>
        /// 添加用户
        /// </summary>
        public bool AddUser { get; set; }
        /// <summary>
        /// 修改权限
        /// </summary>
        public bool ModifyPermissions { get; set; }
    }
}
