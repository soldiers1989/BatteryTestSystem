using BatteryTestSystem_Dal;
using BatteryTestSystem_Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BatteryTestSystem_Bll
{
    public class BatteryTestSystem_UserBll
    {
        BatteryTestSystem_UserDal UsersDal = new BatteryTestSystem_UserDal();

        public bool selectCountBll(string SQLCommand, string UseName, string password)
        {
            try
            {
                if (UsersDal.selectCountDal(SQLCommand, UseName,password) > 0)
                    return true;
                else
                    return false;
            }
            catch
            {
                throw;
            }
        }


        public bool selectCountUserNameBll(string SQLCommand, string UseName)
        {
            try
            {
                if (UsersDal.selectCountUserNameDal(SQLCommand, UseName) > 0)
                    return true;
                else
                    return false;
            }
            catch
            {
                throw;
            }
        }

        public bool AddUserBll(string SQLCommand, string UseName, string password)
        {
            try
            {
                if (UsersDal.AddUserDal(SQLCommand, UseName, password) > 0)
                    return true;
                else
                    return false;
            }
            catch
            {
                throw;
            }

        }
        /// <summary>
        /// 查询账号密码表
        /// </summary>
        /// <param name="UserName"></param>
        /// <param name="password"></param>
        /// <param name="SQLCommand"></param>
        /// <returns></returns>
        public BatteryTestSystem_User selectBatteryTestSystem_UserUserNameBll(string UserName, string password, string SQLCommand)
        {
            try
            {
                return UsersDal.selectBatteryTestSystem_UserUserNameDal(UserName, password, SQLCommand);
            }
            catch
            {
                throw;
            }

        }
        /// <summary>
        /// 查询权限表
        /// </summary>
        /// <param name="UserName"></param>
        /// <param name="SQLCommand"></param>
        /// <returns></returns>
        public BatteryTestSystem_Permissions selectBatteryTestSystem_PermissionsUserNameDal(string UserName, string SQLCommand)
        {
            try
            {
                return UsersDal.selectBatteryTestSystem_PermissionsUserNameDal(UserName, SQLCommand);
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="UserName"></param>
        /// <param name="password"></param>
        /// <param name="SQLCommand"></param>
        /// <returns></returns>
        public bool updateBatteryTestSystem_UserPasswordBll(string UserName,string password, string SQLCommand)
        {
            try
            {
                if (UsersDal.updateBatteryTestSystem_UserPasswordDal(UserName, password, SQLCommand) > 0)
                    return true;
                else
                    return false;
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// 查询除自身和管理员以外的用户
        /// </summary>
        /// <param name="UserName"></param>
        /// <param name="SQLCommand"></param>
        /// <returns></returns>
        public List<BatteryTestSystem_Permissions> selectBatteryTestSystem_PermissionsBll(string UserName,string SQLCommand)
        {
            try
            {
                return UsersDal.selectBatteryTestSystem_PermissionsDal(UserName, SQLCommand);
            }
            catch
            {
                throw;
            }
        }

        public bool updateBatteryTestSystem_PermissionsBll(BatteryTestSystem_Permissions data,string SQLCommand)
        {
            try
            {
                if (UsersDal.updateBatteryTestSystem_PermissionsDal(data, SQLCommand) > 0)
                    return true;
                else
                    return false;
            }
            catch
            {
                throw;
            }
        }

        public int deleteUsersBll(string UseName, string SQLCommand)
        {
            try
            {
                return UsersDal.deleteUsersDal(UseName, SQLCommand);
            }
            catch
            {
                throw;
            }
        }
    }
}
