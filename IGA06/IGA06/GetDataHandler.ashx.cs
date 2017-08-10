using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.Script.Serialization;

namespace IGA06
{
    /// <summary>
    /// GetDataHandler 的摘要描述
    /// </summary>
    public class GetDataHandler : IHttpHandler
    {
        private const string m_connectionStringKey = "UDA";
        private Accudata.Data.Agent.DataAccessAgent m_da = null;

        public void ProcessRequest(HttpContext context)
        {
            m_da = new Accudata.Data.Agent.DataAccessAgent();

            JavaScriptSerializer serializer = new JavaScriptSerializer();
            context.Response.ContentType = "application/json";

            switch (context.Request.QueryString["Get"])
            {
                case "IGAList":
                    DataTable dtIGA = m_da.GetDataTable(@"SELECT ROWNUM, S.*
                                                        FROM(SELECT M.SEARCH_NO,
                                                                NVL(M.SEARCH_NAME, ' ') SEARCH_NAME,
                                                                C.CODE_NAME STATUS,
                                                                TO_CHAR(M.CREATE_TIME, 'yyyy/mm/dd') CREATE_TIME
                                                            FROM UDA_M_SEARCH M
                                                            LEFT JOIN UDA_CODE C
                                                            ON M.STATUS = C.CODE_ID
                                                            AND CODE_TYPE = 'SEARCH_STATUS'
                                                        ORDER BY C.CODE_NAME, M.CREATE_TIME DESC) S", m_connectionStringKey);
                    List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
                    Dictionary<string, object> row;
                    foreach (DataRow dr in dtIGA.Rows)
                    {
                        row = new Dictionary<string, object>();
                        foreach (DataColumn col in dtIGA.Columns)
                        {
                            row.Add(col.ColumnName, dr[col]);
                        }
                        rows.Add(row);
                    }
                    context.Response.Write(serializer.Serialize(rows));
                    break;
                case "selDataSRC"://來源別
                    DataTable dt = m_da.GetDataTable("SELECT DATABASE_NAME NAME, DATA_INDEX S_ID, VER_NO FROM UDA_V_SYSDB", m_connectionStringKey);

                    List<Dictionary<string, object>> rows1 = new List<Dictionary<string, object>>();
                    Dictionary<string, object> row1;
                    foreach (DataRow dr in dt.Rows)
                    {
                        row1 = new Dictionary<string, object>();
                        foreach (DataColumn col in dt.Columns)
                        {
                            row1.Add(col.ColumnName, dr[col]);
                        }
                        rows1.Add(row1);
                    }
                    context.Response.Write(serializer.Serialize(rows1));
                    break;
                case "tbDataTable"://資料庫資料表
                    string selSourceVal = context.Request.QueryString["selSourceVal"].ToString();
                    DataTable dtTable = m_da.GetDataTable(string.Format(@"SELECT TABLE_NAME T_NAME, NVL(TABLENAME_TITLE, TABLE_NAME) C_NAME, DATABASE_NAME, VER_NO FROM UDA_V_SYSTABLE WHERE DATABASE_NAME = '{0}'", selSourceVal), m_connectionStringKey);

                    List<Dictionary<string, object>> rows2 = new List<Dictionary<string, object>>();
                    Dictionary<string, object> row2;
                    foreach (DataRow dr in dtTable.Rows)
                    {
                        row2 = new Dictionary<string, object>();
                        foreach (DataColumn col in dtTable.Columns)
                        {
                            row2.Add(col.ColumnName, dr[col]);
                        }
                        rows2.Add(row2);
                    }
                    context.Response.Write(serializer.Serialize(rows2));
                    break;
                case "tbDataColumns"://資料庫欄位
                    string tableNameVal = context.Request.QueryString["tableName"].ToString();
                    DataTable dtColumn = m_da.GetDataTable(string.Format(@"SELECT TABLE_NAME T_NAME, DATABASE_INDEX S_ID, COLUMN_NAME NAME, NVL(COLUMN_TITLE, COLUMN_NAME) C_NAME, COLUMNTYPE_CODE C_TYPE, VER_NO, COLUMN_SEQ SEQ FROM UDA_V_SYSCOLUMN S LEFT JOIN UDA_CODE C ON S.COLUMNTYPE_CODE = C.CODE_ID AND CODE_TYPE = 'COLUMN_TYPE_CODE' WHERE TABLE_NAME = '{0}' ORDER BY COLUMN_SEQ", tableNameVal), m_connectionStringKey);

                    List<Dictionary<string, object>> rows3 = new List<Dictionary<string, object>>();
                    Dictionary<string, object> row3;
                    foreach (DataRow dr in dtColumn.Rows)
                    {
                        row3 = new Dictionary<string, object>();
                        foreach (DataColumn col in dtColumn.Columns)
                        {
                            row3.Add(col.ColumnName, dr[col]);
                        }
                        rows3.Add(row3);
                    }
                    context.Response.Write(serializer.Serialize(rows3));
                    break;
                case "udaCode"://代碼檔
                    DataTable dtCode = m_da.GetDataTable(@"SELECT CODE_TYPE, CODE_ID, CODE_NAME FROM UDA_CODE", m_connectionStringKey);

                    List<Dictionary<string, object>> rows4 = new List<Dictionary<string, object>>();
                    Dictionary<string, object> row4;
                    foreach (DataRow dr in dtCode.Rows)
                    {
                        row4 = new Dictionary<string, object>();
                        foreach (DataColumn col in dtCode.Columns)
                        {
                            row4.Add(col.ColumnName, dr[col]);
                        }
                        rows4.Add(row4);
                    }
                    context.Response.Write(serializer.Serialize(rows4));
                    break;
                case "floatList"://浮動變數
                    string searchNoVal = context.Request.QueryString["searchNo"].ToString();

                    #region SQL
                    string floatSql = @"
                                    SELECT DCD.*, DC.COLUMN_SHOW_NAME, DC.COLUMNTYPE_CODE, 'UDA_D_CONDITIN' type
                                      FROM UDA_D_CONDITION DCD
                                     INNER JOIN UDA_D_COLUMN DC
                                        ON DCD.STEP_ID = DC.STEP_ID
                                       AND DCD.COLUMN_SEQ = DC.COLUMN_SEQ
                                       AND DCD.SEARCH_NO = DC.SEARCH_NO
                                     WHERE DCD.SEARCH_NO = '{0}'
                                       AND DCD.COND_TYPE = '1'
                                    UNION ALL
                                    SELECT DOCD.*, DOC.COLUMN_SHOW_NAME, DOC.COLUMNTYPE_CODE, 'UDA_D_OUTCONDITIN' type
                                      FROM UDA_D_OUTCONDITION DOCD
                                     INNER JOIN UDA_D_OUTCOLUMN DOC
                                        ON DOCD.STEP_ID = DOC.STEP_ID
                                       AND DOCD.COLUMN_SEQ = DOC.COLUMN_SEQ
                                       AND DOCD.SEARCH_NO = DOC.SEARCH_NO
                                     WHERE DOCD.SEARCH_NO = '{0}'
                                       AND DOCD.COND_TYPE = '1'
                                    ";
                    #endregion

                    DataTable dtFloat = m_da.GetDataTable(string.Format(floatSql, searchNoVal), m_connectionStringKey);

                    List<Dictionary<string, object>> rows5 = new List<Dictionary<string, object>>();
                    Dictionary<string, object> row5;
                    foreach (DataRow dr in dtFloat.Rows)
                    {
                        row5 = new Dictionary<string, object>();
                        foreach (DataColumn col in dtFloat.Columns)
                        {
                            row5.Add(col.ColumnName, dr[col]);
                        }
                        rows5.Add(row5);
                    }
                    context.Response.Write(serializer.Serialize(rows5));
                    break;
                    //case "preview":
                    //    #region 預覽用
                    //    DataTable table = new DataTable();
                    //    table.Columns.Add("Value1");
                    //    table.Columns.Add("Value2");
                    //    table.Columns.Add("Value3");
                    //    table.Columns.Add("Value4");


                    //    for (int i = 0; i < 10; i++)
                    //    {
                    //        DataRow dr = table.NewRow();
                    //        dr.BeginEdit();
                    //        dr["Value1"] = "a" + i.ToString();
                    //        dr["Value2"] = "a" + i.ToString();
                    //        dr["Value3"] = "a" + i.ToString();
                    //        dr["Value4"] = "a" + i.ToString();
                    //        dr.EndEdit();
                    //        table.Rows.Add(dr);
                    //    }

                    //    List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
                    //    Dictionary<string, object> row;
                    //    foreach (DataRow dr in table.Rows)
                    //    {
                    //        row = new Dictionary<string, object>();
                    //        foreach (DataColumn col in table.Columns)
                    //        {
                    //            row.Add(col.ColumnName, dr[col]);
                    //        }
                    //        rows.Add(row);
                    //    }
                    //    context.Response.Write(serializer.Serialize(rows));

                    //    #endregion 預覽用
                    //    break;
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}