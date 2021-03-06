﻿namespace ZoomLa.BLL
{
    using System;
    using System.Data;
    using ZoomLa.Model;
    using ZoomLa.Common;
    using ZoomLa.SQLDAL;
    using System.Data.SqlClient;
    using SQLDAL.SQL;
    /// <summary>
    /// 招生人员记录表
    /// </summary>
    public class B_EnrollList
    {
        public B_EnrollList()
        {
            strTableName = initmod.TbName;
            PK = initmod.PK;
        }
        public string PK, strTableName;
        private M_EnrollList initmod = new M_EnrollList();
        /// <summary>
        ///添加记录
        /// </summary>
        /// <param name="EnrollList"></param>
        /// <returns></returns>
        public int GetInsert(M_EnrollList model)
        {
         return DBCenter.Insert(model);
        }

        /// <summary>
        ///更新记录
        /// </summary>
        /// <param name="EnrollList"></param>
        /// <returns></returns>
        public bool GetUpdate(M_EnrollList model)
        {
         return DBCenter.UpdateByID(model,model.id);
        }

        /// <summary>
        ///删除记录
        /// </summary>
        /// <param name="EnrollList"></param>
        /// <returns></returns>
        public bool DeleteByGroupID(int EnrollListID)
        {
            return Sql.Del(strTableName, EnrollListID);
        }

        /// <summary>
        ///查找一条记录
        /// </summary>
        /// <param name="EnrollList"></param>
        /// <returns></returns>
        public M_EnrollList GetSelect(int EnrollListID)
        {
            string sqlStr = "SELECT [id],[UesrID],[CreateTime],[infos] FROM [dbo].[ZL_EnrollList] WHERE [id] = @id";
            SqlParameter[] cmdParams = new SqlParameter[1];
            cmdParams[0] = new SqlParameter("@id", SqlDbType.Int, 4);
            cmdParams[0].Value = EnrollListID;
            using (SqlDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, sqlStr, cmdParams))
            {
                if (reader.Read())
                {
                    return GetInfoFromReader(reader);
                }
                else
                {
                    return new M_EnrollList();
                }
            }
        }
        private static M_EnrollList GetInfoFromReader(SqlDataReader rdr)
        {
            M_EnrollList info = new M_EnrollList();
            info.id = DataConverter.CLng(rdr["id"].ToString());
            info.UesrID = DataConverter.CLng(rdr["UesrID"].ToString());
            info.CreateTime = DataConverter.CDate(rdr["CreateTime"].ToString());
            info.infos = rdr["infos"].ToString();
            rdr.Close();
            rdr.Dispose();
            return info;
        }

        /// <summary>
        ///返回所有记录
        /// </summary>
        /// <returns></returns>
        public DataTable Select_All()
        {
            return Sql.Sel(strTableName);
        }
        public PageSetting SelPage(int cpage, int psize)
        {
            PageSetting setting = PageSetting.Single(cpage, psize, strTableName, PK, "");
            DBCenter.SelPage(setting);
            return setting;
        }
    }
}
