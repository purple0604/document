using Accudata.Data.General.Command;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace IGA06
{
    /// <summary>
    /// RevertHandler 的摘要描述
    /// </summary>
    public class RevertHandler : IHttpHandler
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
                case "dataRevert":
                    #region SQL
                    var uda_d_column_sql = @"select DC.*,
                                                    NVL(SC.COLUMN_TITLE, SC.COLUMN_NAME) COLUMN_CNAME,
                                                    NVL(MST.TABLENAME_TITLE, MST.TABLE_NAME) AS TABLENAME_TITLE
                                            from 
                                            (
                                            select ver_no,
                                                    search_no,
                                                    database_name,
                                                    table_name,
                                                    column_name,
                                                    column_show_name,
                                                    columntype_code,
                                                    step_id,
                                                    column_seq,
                                                    is_select_fg,
                                                    is_show_fg
                                                from uda_d_column dc
                                                where search_no = '{0}'
                                                and (step_id, column_seq) not in
                                                    (select src_step_id, src_column_id
                                                        from UDA_D_AGGRECOLUMN
                                                        where search_no = '{0}')
                                            union all
                                            select dc.ver_no,
                                                    da.search_no,
                                                    dc.database_name,
                                                    dc.table_name,
                                                    dc.column_name,
                                                    da.aggregation_desc column_show_name,
                                                    da.columntype_code,
                                                    da.src_step_id step_id,
                                                    da.src_column_id column_seq,
                                                    'Y' is_select_fg,
                                                    'Y' is_show_fg
                                                from UDA_D_AGGRECOLUMN DA inner join uda_d_column dc on da.search_no = dc.search_no and da.src_step_id = dc.step_id and da.src_column_id = dc.column_seq
                                                where da.search_no = '{0}'
                                                order by step_id,column_seq
                                            ) DC
                                            LEFT JOIN UDA_D_SYSCOLUMN SC
                                                ON DC.VER_NO = SC.VER_NO
                                                AND DC.DATABASE_NAME = SC.DATABASE_NAME
                                                AND DC.TABLE_NAME = SC.TABLE_NAME
                                                AND DC.COLUMN_NAME = SC.COLUMN_NAME
                                                LEFT JOIN UDA_M_SYSTABLE MST
                                                ON SC.VER_NO = MST.VER_NO
                                                AND SC.DATABASE_NAME = MST.DATABASE_NAME
                                                AND SC.TABLE_NAME = MST.TABLE_NAME
                                        ";

                    var uda_d_joincolumn_sql = @"SELECT DJC.*,
                                                       NVL(DCF.COLUMN_NAME, DOC.COLUMN_SHOW_NAME) F_COLUMN_NAME,
                                                       NVL(NVL(DCF.COLUMN_SHOW_NAME, DCF.COLUMN_NAME), DOC.COLUMN_SHOW_NAME) F_COLUMN_TITLE,
                                                       DCF.DATABASE_NAME F_DATABASE_NAME,
                                                       DCF.TABLE_NAME F_TABLE_NAME,
                                                       DCT.COLUMN_NAME T_COLUMN_NAME,
                                                       NVL(DCT.COLUMN_SHOW_NAME, DCT.COLUMN_NAME) T_COLUMN_TITLE,
                                                       DCT.DATABASE_NAME T_DATABASE_NAME,
                                                       DCT.TABLE_NAME T_TABLE_NAME
                                                  FROM UDA_D_JOINCOLUMN DJC
                                                  LEFT JOIN UDA_D_COLUMN DCF
                                                    ON DJC.SEARCH_NO = DCF.SEARCH_NO
                                                   AND DJC.JOIN_SOURCE_FROM = DCF.STEP_ID
                                                   AND DJC.JOIN_COLUMN_FROM = DCF.COLUMN_SEQ
                                                  LEFT JOIN UDA_D_COLUMN DCT
                                                    ON DJC.SEARCH_NO = DCT.SEARCH_NO
                                                   AND DJC.JOIN_SOURCE_TO = DCT.STEP_ID
                                                   AND DJC.JOIN_COLUMN_TO = DCT.COLUMN_SEQ
                                                  LEFT JOIN UDA_D_OUTCOLUMN DOC
                                                    ON DJC.SEARCH_NO = DOC.SEARCH_NO
                                                   AND DJC.JOIN_SOURCE_FROM = DOC.STEP_ID
                                                   AND DJC.JOIN_COLUMN_FROM = DOC.COLUMN_SEQ
                                                  LEFT JOIN UDA_D_OUTPUT DO
                                                    ON DOC.SEARCH_NO = DO.SEARCH_NO
                                                   AND DOC.STEP_ID = DO.STEP_ID
                                                 WHERE DJC.SEARCH_NO = '{0}'
                                                ";
                    #endregion SQL

                    string search_no = context.Request.QueryString["searchNo"].ToString();
                    DataSet dset = m_da.GetDataSet(new SelectCommand[] {
                        new SelectCommand (string.Format(@"SELECT * FROM (SELECT 3 TYPE,STEP_ID,'UDA_D_AGGRATION' T_NAME FROM UDA_D_AGGRATION WHERE SEARCH_NO ='{0}' UNION ALL SELECT 1 TYPE,STEP_ID,'UDA_D_SOURCE' T_NAME FROM UDA_D_SOURCE WHERE SEARCH_NO ='{0}' UNION ALL SELECT 2 TYPE,STEP_ID,'UDA_D_JOIN' T_NAME FROM UDA_D_JOIN WHERE SEARCH_NO ='{0}' UNION ALL SELECT 4 TYPE,STEP_ID,'UDA_D_OUTPUT' T_NAME FROM UDA_D_OUTPUT WHERE SEARCH_NO ='{0}') ORDER BY STEP_ID", search_no), "SEARCH_STEP", m_connectionStringKey),//依定義編號取得流程順序
                        new SelectCommand (string.Format(@"SELECT * FROM UDA_M_SEARCH WHERE SEARCH_NO ='{0}'", search_no), "UDA_M_SEARCH", m_connectionStringKey),//使用者查詢定義檔
                        new SelectCommand (string.Format(@"SELECT * FROM UDA_D_SOURCE WHERE SEARCH_NO ='{0}'", search_no), "UDA_D_SOURCE", m_connectionStringKey),//定義來源檔
                        new SelectCommand (string.Format(uda_d_column_sql, search_no), "UDA_D_COLUMN", m_connectionStringKey),//定義來源欄位檔
                        new SelectCommand (string.Format(@"SELECT * FROM UDA_D_AGGRATION WHERE SEARCH_NO ='{0}'", search_no), "UDA_D_AGGRATION", m_connectionStringKey),//定義聚合檔
                        new SelectCommand (string.Format(@"SELECT DAC.*, DC.COLUMN_NAME, DC.COLUMN_SHOW_NAME COLUMN_TITLE, DC.DATABASE_NAME, DC.TABLE_NAME FROM UDA_D_AGGRECOLUMN DAC INNER JOIN UDA_D_COLUMN DC ON DAC.SEARCH_NO = DC.SEARCH_NO AND DAC.SRC_STEP_ID = DC.STEP_ID AND DAC.SRC_COLUMN_ID = DC.COLUMN_SEQ WHERE DAC.SEARCH_NO ='{0}' ORDER BY DAC.SEARCH_NO, DAC.STEP_ID, DAC.AGGRE_SET_ID", search_no), "UDA_D_AGGRECOLUMN", m_connectionStringKey),//定義聚合欄位檔
                        new SelectCommand (string.Format(@"SELECT * FROM UDA_D_JOIN WHERE SEARCH_NO ='{0}'", search_no), "UDA_D_JOIN", m_connectionStringKey),//定義來源關聯檔
                        new SelectCommand (string.Format(uda_d_joincolumn_sql, search_no), "UDA_D_JOINCOLUMN", m_connectionStringKey),//定義來源欄位關聯檔
                        new SelectCommand (string.Format(@"SELECT * FROM UDA_D_CONDITION WHERE SEARCH_NO ='{0}'", search_no), "UDA_D_CONDITION", m_connectionStringKey),//定義來源篩選條件檔
                        new SelectCommand (string.Format(@"SELECT * FROM UDA_D_OUTPUT WHERE SEARCH_NO ='{0}'", search_no), "UDA_D_OUTPUT", m_connectionStringKey),//定義輸出檔
                        new SelectCommand (string.Format(@"SELECT * FROM UDA_D_OUTCOLUMN WHERE SEARCH_NO ='{0}'", search_no), "UDA_D_OUTCOLUMN", m_connectionStringKey)//定義輸出欄位檔
                    });

                    DataTable dtSs = dset.Tables["SEARCH_STEP"];//定義步驟
                    DataTable dtMs = dset.Tables["UDA_M_SEARCH"];//使用者查詢定義檔

                    DataIGA dIGA = new DataIGA();
                    dIGA.SearchNo = dtMs.Rows[0]["SEARCH_NO"].ToString();
                    dIGA.SearchName = dtMs.Rows[0]["SEARCH_NAME"].ToString();
                    dIGA.Status = int.Parse(dtMs.Rows[0]["STATUS"].ToString());

                    foreach (DataRow dr in dtSs.Rows)
                    {
                        DataRow drDT = dset.Tables[dr["T_NAME"].ToString()].AsEnumerable().Where(w => w["STEP_ID"].ToString() == dr["STEP_ID"].ToString()).FirstOrDefault();

                        switch (dr["TYPE"].ToString())
                        {
                            case "1":
                                DataSource ds = new DataSource();
                                ds.Step = drDT["STEP_ID"].ToString();
                                ds.StepFeatures = "DataSRC";
                                ds.Name = drDT["DATABASE_NAME"].ToString();
                                ds.TableName = drDT["TABLE_NAME"].ToString();
                                ds.TableCName = drDT["SOURCE_NAME"].ToString();
                                ds.VerNo = drDT["VER_NO"].ToString();

                                foreach (DataRow drDc in dset.Tables["UDA_D_COLUMN"].AsEnumerable().Where(w => w["STEP_ID"].ToString() == dr["STEP_ID"].ToString()))
                                {
                                    Column col = new Column();
                                    col.CStep = int.Parse(drDc["STEP_ID"].ToString());
                                    col.Name = drDc["COLUMN_NAME"].ToString();
                                    col.CName = drDc["COLUMN_CNAME"].ToString();
                                    col.CType = drDc["COLUMNTYPE_CODE"].ToString();
                                    col.IsSelected = drDc["IS_SELECT_FG"].ToString();
                                    col.IsShow = drDc["IS_SHOW_FG"].ToString();
                                    //col.DataSourceName = "";
                                    col.ColumnSeq = int.Parse(drDc["COLUMN_SEQ"].ToString());
                                    col.SRCStep = drDc["STEP_ID"].ToString();

                                    foreach (DataRow drDcd in dset.Tables["UDA_D_CONDITION"].AsEnumerable().Where(w => w["STEP_ID"].ToString() == dr["STEP_ID"].ToString() && w["COLUMN_SEQ"].ToString() == drDc["COLUMN_SEQ"].ToString()))
                                    {
                                        col.ConditionDefine = drDcd["COND_CODE"].ToString();
                                        col.ConditionType = drDcd["COND_TYPE"].ToString();
                                        col.ConditionValue = drDcd["COND_VALUE"].ToString();
                                    }

                                    ds.Columns.Add(col);
                                }

                                dIGA.Step.Add(ds);
                                break;
                            case "2":
                                DataJoin dj = new DataJoin();
                                dj.Step = drDT["STEP_ID"].ToString();
                                dj.StepFeatures = "DataJoin";
                                dj.JoinType = drDT["JOIN_TYPE"].ToString();

                                string step1 = Convert.ToString(int.Parse(drDT["STEP_ID"].ToString()) - 1);
                                string step2 = Convert.ToString(int.Parse(drDT["STEP_ID"].ToString()) - 2);

                                foreach (DataRow drShow in dset.Tables["UDA_D_COLUMN"].AsEnumerable())
                                {
                                    Column colS = new Column();
                                    colS.CStep = int.Parse(drShow["STEP_ID"].ToString());
                                    colS.Name = drShow["COLUMN_NAME"].ToString();
                                    colS.CName = drShow["COLUMN_SHOW_NAME"].ToString();
                                    colS.CType = drShow["COLUMNTYPE_CODE"].ToString();
                                    //colS.ConditionDefine = "";
                                    //colS.ConditionType = "";
                                    //colS.ConditionValue = "";
                                    colS.IsSelected = drShow["IS_SELECT_FG"].ToString();
                                    colS.IsShow = drShow["IS_SHOW_FG"].ToString();
                                    colS.DataSourceName = drShow["DATABASE_NAME"].ToString() + "." + drShow["TABLE_NAME"].ToString();
                                    colS.ColumnSeq = int.Parse(drShow["COLUMN_SEQ"].ToString());
                                    //colS.SRCStep = "";
                                    colS.DataSourceCName = drShow["TABLENAME_TITLE"].ToString();

                                    dj.ShowColumns.Add(colS);
                                }

                                foreach (DataRow drJoin in dset.Tables["UDA_D_JOINCOLUMN"].AsEnumerable().Where(w => w["STEP_ID"].ToString() == dr["STEP_ID"].ToString()))
                                {
                                    Column c1 = new Column();
                                    Column c2 = new Column();
                                    c1.CStep = int.Parse(drJoin["JOIN_SOURCE_FROM"].ToString());
                                    c1.Name = drJoin["F_COLUMN_NAME"].ToString();
                                    c1.CName = drJoin["F_COLUMN_TITLE"].ToString();
                                    //c1.CType = "";
                                    //c1.ConditionDefine = "";
                                    //c1.ConditionType = "";
                                    //c1.ConditionValue = "";
                                    c1.IsSelected = "Y";
                                    //c1.IsShow = "";
                                    c1.DataSourceName = drJoin["F_DATABASE_NAME"].ToString() + "." + drJoin["F_TABLE_NAME"].ToString();
                                    c1.ColumnSeq = int.Parse(drJoin["JOIN_COLUMN_FROM"].ToString());
                                    //c1.SRCStep = "";

                                    c2.CStep = int.Parse(drJoin["JOIN_SOURCE_TO"].ToString());
                                    c2.Name = drJoin["T_COLUMN_NAME"].ToString();
                                    c2.CName = drJoin["T_COLUMN_TITLE"].ToString();
                                    //c2.CType = "";
                                    //c2.ConditionDefine = "";
                                    //c2.ConditionType = "";
                                    //c2.ConditionValue = "";
                                    c2.IsSelected = "Y";
                                    //c2.IsShow = "";
                                    c2.DataSourceName = drJoin["T_DATABASE_NAME"].ToString() + "." + drJoin["T_TABLE_NAME"].ToString();
                                    c2.ColumnSeq = int.Parse(drJoin["JOIN_COLUMN_TO"].ToString());
                                    //c2.SRCStep = "";

                                    dj.JoinColumns.Add(new { f1 = c1, f2 = c2 });
                                }

                                dIGA.Step.Add(dj);
                                break;
                            case "3":
                                Customize cust = new Customize();
                                cust.Step = drDT["STEP_ID"].ToString();
                                cust.StepFeatures = "Customize";

                                foreach (DataRow drDac in dset.Tables["UDA_D_AGGRECOLUMN"].AsEnumerable().Where(w => w["STEP_ID"].ToString() == dr["STEP_ID"].ToString()))
                                {
                                    CustColumn cc = new CustColumn();
                                    cc.CStep = int.Parse(drDac["SRC_STEP_ID"].ToString());
                                    cc.Name = drDac["COLUMN_NAME"].ToString();
                                    cc.CName = drDac["COLUMN_TITLE"].ToString();
                                    cc.CType = drDac["COLUMNTYPE_CODE"].ToString();
                                    cc.TableName = drDac["DATABASE_NAME"].ToString() + "." + drDac["TABLE_NAME"].ToString();
                                    cc.CustType = drDac["AGGREGATION_CODE"].ToString();
                                    cc.UPDName = drDac["AGGREGATION_DESC"].ToString();
                                    cc.ColumnSeq = int.Parse(drDac["SRC_COLUMN_ID"].ToString());

                                    cust.CustColumns.Add(cc);
                                }

                                dIGA.Step.Add(cust);
                                break;
                            case "4":
                                DataSource dsOP = new DataSource();
                                dsOP.Step = drDT["STEP_ID"].ToString();
                                dsOP.StepFeatures = "Output";
                                dsOP.TableCName = drDT["OUTPUT_NAME"].ToString();

                                foreach (DataRow drOp in dset.Tables["UDA_D_OUTCOLUMN"].AsEnumerable().Where(w => w["STEP_ID"].ToString() == dr["STEP_ID"].ToString()))
                                {
                                    Column colOp = new Column();
                                    colOp.CStep = int.Parse(drOp["STEP_ID"].ToString());
                                    colOp.Name = drOp["COLUMN_SHOW_NAME"].ToString();
                                    colOp.CName = drOp["COLUMN_SHOW_NAME"].ToString();
                                    colOp.CType = drOp["COLUMNTYPE_CODE"].ToString();
                                    //colOp.ConditionDefine = "";
                                    //colOp.ConditionType = "";
                                    //colOp.ConditionValue = "";
                                    colOp.IsSelected = drOp["IS_SELECT_FG"].ToString();
                                    //colOp.IsShow = "";
                                    //colOp.DataSourceName = "";
                                    colOp.ColumnSeq = int.Parse(drOp["SRC_COLUMN_SEQ"].ToString());
                                    colOp.SRCStep = drOp["SRC_STEP_ID"].ToString();

                                    dsOP.Columns.Add(colOp);
                                }

                                dIGA.Step.Add(dsOP);
                                break;
                        }
                    }
                    context.Response.Write(serializer.Serialize(dIGA));

                    break;
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        public class DataIGA
        {
            public DataIGA()
            {
                Step = new List<object>();
            }
            public string SearchNo { get; set; }
            public string SearchName { get; set; }
            public int Status { get; set; }
            public List<object> Step { get; set; }
        }

        public class DataSource
        {
            public DataSource()
            {
                Columns = new List<Column>();
            }
            public string Mode { get { return "U"; } }
            public string Step { get; set; }
            public string StepDesc { get; set; }
            public string StepFeatures { get; set; }
            public string Name { get; set; }
            public string TableName { get; set; }
            public string TableCName { get; set; }
            public string VerNo { get; set; }
            public List<Column> Columns { get; set; }
        }

        public class Column
        {
            public int CStep { get; set; }
            public string Name { get; set; }
            public string CName { get; set; }
            public string CType { get; set; }
            public string ConditionDefine { get; set; }
            public string ConditionType { get; set; }
            public string ConditionValue { get; set; }
            public string IsSelected { get; set; }
            public string IsShow { get; set; }
            public string DataSourceName { get; set; }
            public string DataSourceCName { get; set; }
            public int ColumnSeq { get; set; }
            public string SRCStep { get; set; }
        }

        public class DataJoin
        {
            public DataJoin()
            {
                ShowColumns = new List<Column>();
                JoinColumns = new List<object>();
            }
            public string Mode { get { return "U"; } }
            public string Step { get; set; }
            public string StepDesc { get; set; }
            public string StepFeatures { get; set; }
            public string JoinType { get; set; }
            public List<Column> ShowColumns { get; set; }
            public List<object> JoinColumns { get; set; }
        }

        public class Customize
        {
            public Customize()
            {
                CustColumns = new List<CustColumn>();
                AllColumns = new List<Column>();
            }
            public string Mode { get { return "U"; } }
            public string Step { get; set; }
            public string StepDesc { get; set; }
            public string StepFeatures { get; set; }
            public List<CustColumn> CustColumns { get; set; }
            public List<Column> AllColumns { get; set; }
        }

        public class CustColumn
        {
            public int CStep { get; set; }
            public string Name { get; set; }
            public string CName { get; set; }
            public string CType { get; set; }
            public string TableName { get; set; }
            public string CustType { get; set; }
            public string UPDName { get; set; }
            public int ColumnSeq { get; set; }
        }
    }
}