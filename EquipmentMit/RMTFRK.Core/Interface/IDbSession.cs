using System.Data.Common;

namespace RMTFRK.Core.Interface
{
    //添加接口，起约束作用
    public partial interface IDbSession
    {
        #region ----次代码由t4模版自动生成----

        //IDAL.IRoleRepository RoleRepository { get; }

        //IDAL.IUserInfoRepository UserInfoRepository { get; } 

        #endregion

        int SaveChanges();

        int ExcuteSql(string strSql, DbParameter[] parameters);


    }
}
