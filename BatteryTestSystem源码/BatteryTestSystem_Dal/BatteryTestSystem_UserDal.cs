using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using BatteryTestSystem_Model;

namespace BatteryTestSystem_Dal
{
    public class BatteryTestSystem_UserDal
    {
        public int selectCountDal(string SQLCommand,string UseName,string password)
        {
            try
            {
                string sql = SQLhelp.GetSQLCommand(SQLCommand);

                SqlParameter[] pms = new SqlParameter[]
            {
                new SqlParameter("@UserName",SqlDbType.VarChar,50){Value=UseName},
                new SqlParameter("@Password",SqlDbType.VarChar,80){Value=password}
            };

                return Convert.ToInt32( SQLhelp.ExecuteScalar(sql, CommandType.Text, pms));
            }
            catch
            {
                throw;
            }
        }

        public int selectCountUserNameDal(string SQLCommand, string UseName)
        {
            try
            {
                string sql = SQLhelp.GetSQLCommand(SQLCommand);

                SqlParameter[] pms = new SqlParameter[]
            {
                new SqlParameter("@UserName",SqlDbType.VarChar,50){Value=UseName}
            };

                return Convert.ToInt32( SQLhelp.ExecuteScalar(sql, CommandType.Text, pms));
            }
            catch
            {
                throw;
            }
        }

        public int AddUserDal(string SQLCommand, string UseName, string password)
        {
            try
            {
                string sql = SQLhelp.GetSQLCommand(SQLCommand);

                SqlParameter[] pms = new SqlParameter[]
            {
                new SqlParameter("@UserName",SqlDbType.VarChar,50){Value=UseName},
                new SqlParameter("@Password",SqlDbType.VarChar,80){Value=password}
            };

                return SQLhelp.ExecuteNonQuery(sql,CommandType.Text,pms);
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
        /// <param name="SQLCommand"></param>
        /// <returns></returns>
        public BatteryTestSystem_User selectBatteryTestSystem_UserUserNameDal(string UserName, string password, string SQLCommand)
        {
            try
            {
                string sql = SQLhelp.GetSQLCommand(SQLCommand);
                BatteryTestSystem_User data = new BatteryTestSystem_User();
                SqlParameter[] pms = new SqlParameter[]
            {
                new SqlParameter("@UserName",SqlDbType.VarChar,50){Value=UserName},
                new SqlParameter("@Password",SqlDbType.VarChar,80){Value=password}
            };
                using(SqlDataReader reader=SQLhelp.ExecuteReader(sql,CommandType.Text,pms))
                {
                    if(reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            data.UserName = reader.IsDBNull(0) ? "" : reader.GetString(0);
                            data.Password = reader.IsDBNull(1) ? "" : reader.GetString(1);
                        }
                    }
                    return data;
                }

            }
            catch
            {
                throw;
            }

        }

        public BatteryTestSystem_Permissions selectBatteryTestSystem_PermissionsUserNameDal(string UserName, string SQLCommand)
        {
            try
            {
                string sql = SQLhelp.GetSQLCommand(SQLCommand);
                BatteryTestSystem_Permissions data = new BatteryTestSystem_Permissions();
                SqlParameter[] pms = new SqlParameter[]
                {
                    new SqlParameter("@UserName",SqlDbType.VarChar,50){Value=UserName}
                };
                using(SqlDataReader reader=SQLhelp.ExecuteReader(sql,CommandType.Text,pms))
                {
                    if(reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            data.UserName = reader.IsDBNull(0) ? "" : reader.GetString(0);
                            data.Admini = reader.IsDBNull(1) ? false : reader.GetBoolean(1);
                            data.DataDelete = reader.IsDBNull(2) ? false : reader.GetBoolean(2);
                            data.InformationSystem = reader.IsDBNull(3) ? false : reader.GetBoolean(3);
                            data.ChangingEquipment = reader.IsDBNull(4) ? false : reader.GetBoolean(4);
                            data.ServerConnection = reader.IsDBNull(5) ? false : reader.GetBoolean(5);
                            data.AddUser = reader.IsDBNull(6) ? false : reader.GetBoolean(6);
                            data.ModifyPermissions = reader.IsDBNull(7) ? false : reader.GetBoolean(7);
                        }
                    }
                    return data;
                }

            }
            catch
            {
                throw;
            }
        }

        public int updateBatteryTestSystem_UserPasswordDal(string UserName,string password, string SQLCommand)
        {
            try
            {
                string sql = SQLhelp.GetSQLCommand(SQLCommand);
                SqlParameter[] pms = new SqlParameter[]
                {
                    new SqlParameter("@UserName",SqlDbType.VarChar,50){Value=UserName},
                    new SqlParameter("@Password",SqlDbType.VarChar,80){Value=password}
                };
                return SQLhelp.ExecuteNonQuery(sql, CommandType.Text, pms);
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
        public List<BatteryTestSystem_Permissions> selectBatteryTestSystem_PermissionsDal(string UserName,string SQLCommand)
        {
            try
            {
                string sql = SQLhelp.GetSQLCommand(SQLCommand);
                List<BatteryTestSystem_Permissions> list = new List<BatteryTestSystem_Permissions>();
                SqlParameter[] pms = new SqlParameter[]
                {
                    new SqlParameter("@UserName",SqlDbType.VarChar,50){Value=UserName}
                };

                using(SqlDataReader reader=SQLhelp.ExecuteReader(sql,CommandType.Text,pms))
                {
                    if(reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            list.Add(new BatteryTestSystem_Permissions()
                            {
                                UserName=reader.IsDBNull(0)?"":reader.GetString(0),
                                Admini=reader.IsDBNull(1)?false:reader.GetBoolean(1),
                                DataDelete = reader.IsDBNull(2) ? false : reader.GetBoolean(2),
                                InformationSystem = reader.IsDBNull(3) ? false : reader.GetBoolean(3),
                                ChangingEquipment = reader.IsDBNull(4) ? false : reader.GetBoolean(4),
                                ServerConnection = reader.IsDBNull(5) ? false : reader.GetBoolean(5),
                                AddUser = reader.IsDBNull(6) ? false : reader.GetBoolean(6),
                                ModifyPermissions = reader.IsDBNull(7) ? false : reader.GetBoolean(7)
                            });
                        }
                    }
                    return list;
                }
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// 修改权限
        /// </summary>
        /// <param name="data"></param>
        /// <param name="SQLCommand"></param>
        /// <returns></returns>
        public int updateBatteryTestSystem_PermissionsDal(BatteryTestSystem_Permissions data,string SQLCommand)
        {
            try
            {
                string sql = SQLhelp.GetSQLCommand(SQLCommand);

                SqlParameter[] pms = new SqlParameter[]
            {
                new SqlParameter("@UserName",SqlDbType.VarChar,50){Value=data.UserName},
                new SqlParameter("@Admini",SqlDbType.Bit){Value=data.Admini},
                new SqlParameter("@AddUser",SqlDbType.Bit){Value=data.AddUser},
                new SqlParameter("@ChangingEquipment",SqlDbType.Bit){Value=data.ChangingEquipment},
                new SqlParameter("@DataDelete",SqlDbType.Bit){Value=data.DataDelete},
                new SqlParameter("@InformationSystem",SqlDbType.Bit){Value=data.InformationSystem},
                new SqlParameter("@ModifyPermissions",SqlDbType.Bit){Value=data.ModifyPermissions},
                new SqlParameter("@ServerConnection",SqlDbType.Bit){Value=data.ServerConnection},
            };

                return SQLhelp.ExecuteNonQuery(sql, CommandType.Text, pms);
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="UseName"></param>
        /// <param name="SQLCommand"></param>
        /// <returns></returns>
        public int deleteUsersDal(string UseName, string SQLCommand)
        {
            try
            {
                string sql = SQLhelp.GetSQLCommand(SQLCommand);

                SqlParameter[] pms = new SqlParameter[]
            {
                new SqlParameter("@UserName",SqlDbType.VarChar,50){Value=UseName}
            };

                return SQLhelp.ExecuteNonQuery(sql, CommandType.Text, pms);
            }
            catch
            {
                throw;
            }
        }
    }
}
