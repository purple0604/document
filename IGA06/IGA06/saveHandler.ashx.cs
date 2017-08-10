using Accudata.Data.General.Command;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Script.Serialization;

namespace IGA06
{
    /// <summary>
    /// SaveHandler 的摘要描述
    /// </summary>
    public class SaveHandler : IHttpHandler
    {
        private const string m_connectionStringKey = "UDA";
        private Accudata.Data.Agent.DataAccessAgent m_da = null;

        private object ConvertType(PropertyInfo pi, object value)
        {
            if (value == null)
            {
                return null;
            }
            if (pi.PropertyType == typeof(int))
            {
                return int.Parse(value.ToString());
            }
            return value.ToString();
        }

        public void ProcessRequest(HttpContext context)
        {
            try
            {
                m_da = new Accudata.Data.Agent.DataAccessAgent();

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                var data = serializer.DeserializeObject(context.Request.Form[0]);
                Dictionary<string, object> dictData = (Dictionary<string, object>)data;
                object[] objData = dictData.Values.ToArray()[3] as object[];

                var cond_ID = 0;//條件序號流水號
                var joinStep_ID = 0;//關聯組合序號流水號
                //var joinColSeq = 0;//關聯輸出欄位序號流水號
                var aggreStep_ID = 0;//聚合組合序號流水號
                //var aggreColSeq = 0;//聚合輸出欄位序號流水號
                var outputSeq_ID = 0;//輸出檔.欄位序號流水號

                #region Json To Object

                UDA_M_SEARCH ums = new UDA_M_SEARCH();
                ums.SEARCH_NO = dictData.Values.ToArray()[0].ToString() == "" ? int.Parse(m_da.GetValue("select get_search_no() from dual", m_connectionStringKey).ToString()) : int.Parse(dictData.Values.ToArray()[0].ToString());
                ums.SEARCH_NAME = dictData.Values.ToArray()[1].ToString();
                ums.STATUS = dictData.Values.ToArray()[2].ToString();
                ums.CREATE_TIME = DateTime.Now;
                ums.USER_ID = "System";//待變更
                ums.UPDATE_TIME = DateTime.Now;

                List<UDA_D_SOURCE> udsList = new List<UDA_D_SOURCE>();
                List<UDA_D_COLUMN> udcList = new List<UDA_D_COLUMN>();
                List<UDA_D_CONDITION> udcnList = new List<UDA_D_CONDITION>();
                List<UDA_D_JOIN> udjList = new List<UDA_D_JOIN>();
                List<UDA_D_JOINCOLUMN> udjcList = new List<UDA_D_JOINCOLUMN>();
                //List<UDA_D_JOINOUTPUT> udjoList = new List<UDA_D_JOINOUTPUT>();
                List<UDA_D_AGGRATION> udaList = new List<UDA_D_AGGRATION>();
                List<UDA_D_AGGRECOLUMN> udacList = new List<UDA_D_AGGRECOLUMN>();
                //List<UDA_D_AGGRATIONOUTPUT> udaoList = new List<UDA_D_AGGRATIONOUTPUT>();
                List<UDA_D_OUTPUT> udoList = new List<UDA_D_OUTPUT>();
                List<UDA_D_OUTCOLUMN> udocList = new List<UDA_D_OUTCOLUMN>();

                foreach (Dictionary<string, object> item in objData)
                {
                    switch (item["StepFeatures"].ToString())
                    {
                        #region DataSRC
                        case "DataSRC":

                            UDA_D_SOURCE uds = new UDA_D_SOURCE();

                            foreach (string key in item.Keys)
                            {
                                PropertyInfo pi = uds.GetType().GetProperties()
                                    .Where(prop => prop.GetCustomAttributes(typeof(NameMappingAttribute), false).Cast<NameMappingAttribute>().Where(attr => attr.JsonName == key).Count() > 0).DefaultIfEmpty(null).First();

                                if (pi != null)
                                {
                                    pi.SetValue(uds, ConvertType(pi, item[key]), null);
                                }
                            }
                            uds.SEARCH_NO = ums.SEARCH_NO;
                            uds.USER_ID = ums.USER_ID;
                            uds.UPDATE_TIME = ums.UPDATE_TIME;

                            udsList.Add(uds);

                            foreach (Dictionary<string, object> column in (object[])item["Columns"])
                            {
                                UDA_D_COLUMN udc = new UDA_D_COLUMN();
                                UDA_D_CONDITION udcn = new UDA_D_CONDITION();

                                foreach (string col in column.Keys)
                                {
                                    PropertyInfo cpi = udc.GetType().GetProperties()
                                    .Where(prop => prop.GetCustomAttributes(typeof(NameMappingAttribute), false).Cast<NameMappingAttribute>().Where(attr => attr.JsonName == col).Count() > 0).DefaultIfEmpty(null).First();

                                    if (cpi != null)
                                    {
                                        cpi.SetValue(udc, ConvertType(cpi, column[col]), null);
                                    }

                                    PropertyInfo cnpi = udcn.GetType().GetProperties()
                                    .Where(prop => prop.GetCustomAttributes(typeof(NameMappingAttribute), false).Cast<NameMappingAttribute>().Where(attr => attr.JsonName == col).Count() > 0).DefaultIfEmpty(null).First();

                                    if (cnpi != null)
                                    {
                                        cnpi.SetValue(udcn, ConvertType(cnpi, column[col]), null);
                                    }
                                }
                                udc.SEARCH_NO = ums.SEARCH_NO;
                                udc.STEP_ID = uds.STEP_ID;
                                udc.VER_NO = uds.VER_NO;
                                udc.DATABASE_NAME = uds.DATABASE_NAME;
                                udc.TABLE_NAME = uds.TABLE_NAME;
                                udc.USER_ID = ums.USER_ID;
                                udc.UPDATE_TIME = ums.UPDATE_TIME;

                                udcn.SEARCH_NO = ums.SEARCH_NO;
                                udcn.STEP_ID = uds.STEP_ID;
                                udcn.COLUMN_SEQ = udc.COLUMN_SEQ;
                                udcn.CONDTITION_ID = ++cond_ID;
                                udcn.USER_ID = ums.USER_ID;
                                udcn.UPDATE_TIME = ums.UPDATE_TIME;

                                udcList.Add(udc);
                                udcnList.Add(udcn);
                            }
                            break;
                        #endregion DataSRC

                        #region DataJoin
                        case "DataJoin":

                            UDA_D_JOIN udj = new UDA_D_JOIN();

                            foreach (string key in item.Keys)
                            {
                                PropertyInfo pi = udj.GetType().GetProperties()
                                    .Where(prop => prop.GetCustomAttributes(typeof(NameMappingAttribute), false).Cast<NameMappingAttribute>().Where(attr => attr.JsonName == key).Count() > 0).DefaultIfEmpty(null).First();

                                if (pi != null)
                                {
                                    pi.SetValue(udj, ConvertType(pi, item[key]), null);
                                }
                            }
                            udj.SEARCH_NO = ums.SEARCH_NO;
                            udj.USER_ID = ums.USER_ID;
                            udj.UPDATE_TIME = ums.UPDATE_TIME;

                            udjList.Add(udj);

                            foreach (Dictionary<string, object> jColumn in (object[])item["JoinColumns"])
                            {
                                UDA_D_JOINCOLUMN udjc = new UDA_D_JOINCOLUMN();

                                udjc.SEARCH_NO = ums.SEARCH_NO;
                                udjc.STEP_ID = udj.STEP_ID;
                                udjc.JOIN_SET_ID = ++joinStep_ID;
                                udjc.JOIN_SOURCE_FROM = (int)((Dictionary<string, object>)jColumn["f1"])["CStep"];
                                udjc.JOIN_COLUMN_FROM = (int)((Dictionary<string, object>)jColumn["f1"])["ColumnSeq"];
                                udjc.JOIN_SOURCE_TO = (int)((Dictionary<string, object>)jColumn["f2"])["CStep"];
                                udjc.JOIN_COLUMN_TO = (int)((Dictionary<string, object>)jColumn["f2"])["ColumnSeq"];
                                udjc.USER_ID = ums.USER_ID;
                                udjc.UPDATE_TIME = ums.UPDATE_TIME;

                                udjcList.Add(udjc);
                            }

                            //foreach (Dictionary<string, object> sColumn in (object[])item["ShowColumns"])
                            //{
                            //    UDA_D_JOINOUTPUT udjo = new UDA_D_JOINOUTPUT();

                            //    foreach (string  col in sColumn.Keys)
                            //    {
                            //        PropertyInfo cpi = udjo.GetType().GetProperties()
                            //        .Where(prop => prop.GetCustomAttributes(typeof(NameMappingAttribute), false).Cast<NameMappingAttribute>().Where(attr => attr.JsonName == col).Count() > 0).DefaultIfEmpty(null).First();

                            //        if (cpi != null)
                            //        {
                            //            cpi.SetValue(udjo, ConvertType(cpi, sColumn[col]), null);
                            //        }
                            //    }

                            //    udjo.SEARCH_NO = ums.SEARCH_NO;
                            //    udjo.STEP_ID = udj.STEP_ID;
                            //    udjo.COLUMN_SEQ = ++joinColSeq;
                            //    udjo.COLUMN_ALIAS_NAME = string.Format("{0}_{1}", udjo.STEP_ID, udjo.COLUMN_ALIAS_NAME);
                            //    udjo.USER_ID = ums.USER_ID;
                            //    udjo.UPDATE_TIME = ums.UPDATE_TIME;

                            //    udjoList.Add(udjo);
                            //}

                            foreach (Dictionary<string, object> sColumn in (object[])item["ShowColumns"])
                            {
                                //取得於定義來源欄位檔內，相同定義編號且同名稱的欄位序號，並更新自訂欄位名稱及是否顯示為結果等欄位
                                foreach (var uItem in udcList)
                                {
                                    if ((uItem.SEARCH_NO == ums.SEARCH_NO) && (uItem.STEP_ID.ToString() == sColumn["SRCStep"].ToString()) && (uItem.COLUMN_SEQ == (int)sColumn["ColumnSeq"]))
                                    {
                                        uItem.COLUMN_SHOW_NAME = sColumn["CName"].ToString();
                                        uItem.IS_SHOW_FG = sColumn["IsShow"].ToString();
                                        break;
                                    }
                                }
                            }

                            break;
                        #endregion DataJoin

                        #region 自訂聚合、一般聚合
                        case "Customize":
                        case "CustSum":
                        case "CustAvg":
                        case "CustMax":
                        case "CustMin":
                        case "CustCount":
                            //var aggCode = string.Empty;

                            UDA_D_AGGRATION uda = new UDA_D_AGGRATION();

                            foreach (string key in item.Keys)
                            {
                                PropertyInfo pi = uda.GetType().GetProperties()
                                    .Where(prop => prop.GetCustomAttributes(typeof(NameMappingAttribute), false).Cast<NameMappingAttribute>().Where(attr => attr.JsonName == key).Count() > 0).DefaultIfEmpty(null).First();

                                if (pi != null)
                                {
                                    pi.SetValue(uda, ConvertType(pi, item[key]), null);
                                }
                            }
                            uda.SEARCH_NO = ums.SEARCH_NO;

                            udaList.Add(uda);

                            foreach (Dictionary<string, object> cColumn in (object[])item["CustColumns"])
                            {
                                UDA_D_AGGRECOLUMN udac = new UDA_D_AGGRECOLUMN();

                                foreach (string col in cColumn.Keys)
                                {
                                    PropertyInfo cpi = udac.GetType().GetProperties()
                                    .Where(prop => prop.GetCustomAttributes(typeof(NameMappingAttribute), false).Cast<NameMappingAttribute>().Where(attr => attr.JsonName == col).Count() > 0).DefaultIfEmpty(null).First();

                                    if (cpi != null)
                                    {
                                        cpi.SetValue(udac, ConvertType(cpi, cColumn[col]), null);
                                    }
                                }
                                udac.SEARCH_NO = ums.SEARCH_NO;
                                udac.STEP_ID = uda.STEP_ID;
                                udac.AGGRE_SET_ID = ++aggreStep_ID;
                                udac.USER_ID = ums.USER_ID;
                                udac.UPDATE_TIME = ums.UPDATE_TIME;

                                //if (item["StepFeatures"].ToString() != "Customize")
                                //{
                                //    aggCode = udac.AGGREGATION_CODE;
                                //}
                                udacList.Add(udac);
                            }

                            //foreach (Dictionary<string, object> aColumn in (object[])item["AllColumns"])
                            //{
                            //    UDA_D_AGGRATIONOUTPUT udao = new UDA_D_AGGRATIONOUTPUT();

                            //    foreach (string col in aColumn.Keys)
                            //    {
                            //        PropertyInfo cpi = udao.GetType().GetProperties()
                            //        .Where(prop => prop.GetCustomAttributes(typeof(NameMappingAttribute), false).Cast<NameMappingAttribute>().Where(attr => attr.JsonName == col).Count() > 0).DefaultIfEmpty(null).First();

                            //        if (cpi != null)
                            //        {
                            //            cpi.SetValue(udao, ConvertType(cpi, aColumn[col]), null);
                            //        }
                            //    }
                            //    udao.SEARCH_NO = ums.SEARCH_NO;
                            //    udao.STEP_ID = uda.STEP_ID;
                            //    udao.COLUMN_SEQ = ++aggreColSeq;
                            //    udao.AGGREGATION_CODE = aggCode;
                            //    udao.COLUMN_ALIAS_NAME = string.Format("{0}_{1}", udao.STEP_ID, udao.COLUMN_ALIAS_NAME);
                            //    udao.USER_ID = ums.USER_ID;
                            //    udao.UPDATE_TIME = ums.UPDATE_TIME;

                            //    udaoList.Add(udao);
                            //}
                            break;
                        #endregion 自訂聚合、一般聚合

                        #region 資料輸出
                        case "Output":

                            UDA_D_OUTPUT udo = new UDA_D_OUTPUT();

                            foreach (string key in item.Keys)
                            {
                                PropertyInfo pi = udo.GetType().GetProperties()
                                    .Where(prop => prop.GetCustomAttributes(typeof(NameMappingAttribute), false).Cast<NameMappingAttribute>().Where(attr => attr.JsonName == key).Count() > 0).DefaultIfEmpty(null).First();

                                if (pi != null)
                                {
                                    pi.SetValue(udo, ConvertType(pi, item[key]), null);
                                }
                            }
                            udo.SEARCH_NO = ums.SEARCH_NO;
                            udo.USER_ID = ums.USER_ID;
                            udo.UPDATE_TIME = ums.UPDATE_TIME;

                            udoList.Add(udo);

                            foreach (Dictionary<string, object> column in (object[])item["Columns"])
                            {
                                UDA_D_OUTCOLUMN udoc = new UDA_D_OUTCOLUMN();

                                foreach (string col in column.Keys)
                                {
                                    PropertyInfo cpi = udoc.GetType().GetProperties()
                                    .Where(prop => prop.GetCustomAttributes(typeof(NameMappingAttribute), false).Cast<NameMappingAttribute>().Where(attr => attr.JsonName == col).Count() > 0).DefaultIfEmpty(null).First();

                                    if (cpi != null)
                                    {
                                        cpi.SetValue(udoc, ConvertType(cpi, column[col]), null);
                                    }
                                }
                                udoc.SEARCH_NO = ums.SEARCH_NO;
                                udoc.STEP_ID = udo.STEP_ID;
                                udoc.COLUMN_SEQ = ++outputSeq_ID;
                                udoc.COLUMN_ALIAS_NAME = string.Format("{0}_{1}", udoc.STEP_ID, udoc.COLUMN_ALIAS_NAME);
                                udoc.USER_ID = ums.USER_ID;
                                udoc.UPDATE_TIME = ums.UPDATE_TIME;

                                udocList.Add(udoc);
                            }
                            break;
                            #endregion 資料輸出
                    }
                }

                #endregion Json To Object

                #region Object To DataTable

                SelectCommand[] cmds = new[]
                {
                    new SelectCommand(string.Format("SELECT * FROM UDA_M_SEARCH WHERE SEARCH_NO = {0}", ums.SEARCH_NO), "UDA_M_SEARCH", m_connectionStringKey),
                    new SelectCommand(string.Format("SELECT * FROM UDA_D_SOURCE WHERE SEARCH_NO = {0}", ums.SEARCH_NO), "UDA_D_SOURCE", m_connectionStringKey),
                    new SelectCommand(string.Format("SELECT * FROM UDA_D_COLUMN WHERE SEARCH_NO = {0}", ums.SEARCH_NO), "UDA_D_COLUMN", m_connectionStringKey),
                    new SelectCommand(string.Format("SELECT * FROM UDA_D_CONDITION WHERE SEARCH_NO = {0}", ums.SEARCH_NO), "UDA_D_CONDITION", m_connectionStringKey),
                    new SelectCommand(string.Format("SELECT * FROM UDA_D_JOIN WHERE SEARCH_NO = {0}", ums.SEARCH_NO), "UDA_D_JOIN", m_connectionStringKey),
                    new SelectCommand(string.Format("SELECT * FROM UDA_D_JOINCOLUMN WHERE SEARCH_NO = {0}", ums.SEARCH_NO), "UDA_D_JOINCOLUMN", m_connectionStringKey),
                    //new SelectCommand(string.Format("SELECT * FROM UDA_D_JOINOUTPUT WHERE SEARCH_NO = {0}", ums.SEARCH_NO), "UDA_D_JOINOUTPUT", m_connectionStringKey),
                    new SelectCommand(string.Format("SELECT * FROM UDA_D_AGGRATION WHERE SEARCH_NO = {0}", ums.SEARCH_NO), "UDA_D_AGGRATION", m_connectionStringKey),
                    new SelectCommand(string.Format("SELECT * FROM UDA_D_AGGRECOLUMN WHERE SEARCH_NO = {0}", ums.SEARCH_NO), "UDA_D_AGGRECOLUMN", m_connectionStringKey),
                    //new SelectCommand(string.Format("SELECT * FROM UDA_D_AGGRATIONOUTPUT WHERE SEARCH_NO = {0}", ums.SEARCH_NO), "UDA_D_AGGRATIONOUTPUT", m_connectionStringKey),
                    new SelectCommand(string.Format("SELECT * FROM UDA_D_OUTPUT WHERE SEARCH_NO = {0}", ums.SEARCH_NO), "UDA_D_OUTPUT", m_connectionStringKey),
                    new SelectCommand(string.Format("SELECT * FROM UDA_D_OUTCOLUMN WHERE SEARCH_NO = {0}", ums.SEARCH_NO), "UDA_D_OUTCOLUMN", m_connectionStringKey)
                };

                DataSet ds = m_da.GetDataSet(cmds);

                //刪除DB資料
                foreach (DataTable table in ds.Tables)
                {
                    foreach (DataRow dr in table.Rows)
                    {
                        dr.Delete();
                    }
                }

                //使用者查詢定義檔
                DataRow umsDr = ds.Tables["UDA_M_SEARCH"].NewRow();
                umsDr.BeginEdit();
                umsDr["SEARCH_NO"] = ums.SEARCH_NO;
                umsDr["SEARCH_NAME"] = ums.SEARCH_NAME;
                umsDr["NOTE"] = ums.NOTE;
                umsDr["EMP_NO"] = ums.EMP_NO;
                umsDr["STATUS"] = ums.STATUS;
                umsDr["CREATE_TIME"] = ums.CREATE_TIME;
                umsDr["USER_ID"] = ums.USER_ID;
                umsDr["UPDATE_TIME"] = ums.UPDATE_TIME;
                umsDr.EndEdit();
                ds.Tables["UDA_M_SEARCH"].Rows.Add(umsDr);

                //定義來源檔
                foreach (var item in udsList)
                {
                    DataRow udsDr = ds.Tables["UDA_D_SOURCE"].NewRow();
                    udsDr.BeginEdit();
                    udsDr["SEARCH_NO"] = item.SEARCH_NO;
                    udsDr["STEP_ID"] = item.STEP_ID;
                    udsDr["VER_NO"] = item.VER_NO;
                    udsDr["DATABASE_NAME"] = item.DATABASE_NAME;
                    udsDr["TABLE_NAME"] = item.TABLE_NAME;
                    udsDr["SOURCE_NAME"] = item.SOURCE_NAME;
                    udsDr["USER_ID"] = item.USER_ID;
                    udsDr["UPDATE_TIME"] = item.UPDATE_TIME;
                    udsDr.EndEdit();
                    ds.Tables["UDA_D_SOURCE"].Rows.Add(udsDr);
                }

                //定義來源欄位檔
                foreach (var item in udcList)
                {
                    DataRow udcDr = ds.Tables["UDA_D_COLUMN"].NewRow();
                    udcDr.BeginEdit();
                    udcDr["SEARCH_NO"] = item.SEARCH_NO;
                    udcDr["STEP_ID"] = item.STEP_ID;
                    udcDr["COLUMN_SEQ"] = item.COLUMN_SEQ;
                    udcDr["VER_NO"] = item.VER_NO;
                    udcDr["DATABASE_NAME"] = item.DATABASE_NAME;
                    udcDr["TABLE_NAME"] = item.TABLE_NAME;
                    udcDr["COLUMN_NAME"] = item.COLUMN_NAME;
                    udcDr["COLUMN_SHOW_NAME"] = item.COLUMN_SHOW_NAME;
                    udcDr["COLUMNTYPE_CODE"] = item.COLUMNTYPE_CODE;
                    udcDr["IS_SELECT_FG"] = item.IS_SELECT_FG;
                    udcDr["IS_SHOW_FG"] = item.IS_SHOW_FG;
                    udcDr["USER_ID"] = item.USER_ID;
                    udcDr["UPDATE_TIME"] = item.UPDATE_TIME;
                    udcDr.EndEdit();
                    ds.Tables["UDA_D_COLUMN"].Rows.Add(udcDr);
                }

                //定義來源篩選條件檔
                foreach (var item in udcnList)
                {
                    if (!string.IsNullOrEmpty(item.COND_CODE) && !string.IsNullOrEmpty(item.COND_TYPE))
                    {
                        DataRow udcnDr = ds.Tables["UDA_D_CONDITION"].NewRow();
                        udcnDr.BeginEdit();
                        udcnDr["SEARCH_NO"] = item.SEARCH_NO;
                        udcnDr["STEP_ID"] = item.STEP_ID;
                        udcnDr["COLUMN_SEQ"] = item.COLUMN_SEQ;
                        udcnDr["CONDTITION_ID"] = item.CONDTITION_ID;
                        udcnDr["COND_CODE"] = item.COND_CODE;
                        udcnDr["COND_TYPE"] = item.COND_TYPE;
                        udcnDr["COND_VALUE"] = item.COND_VALUE;
                        udcnDr["USER_ID"] = item.USER_ID;
                        udcnDr["UPDATE_TIME"] = item.UPDATE_TIME;
                        udcnDr.EndEdit();
                        ds.Tables["UDA_D_CONDITION"].Rows.Add(udcnDr);
                    }
                }

                //定義來源關聯檔
                foreach (var item in udjList)
                {
                    DataRow udjDr = ds.Tables["UDA_D_JOIN"].NewRow();
                    udjDr.BeginEdit();
                    udjDr["SEARCH_NO"] = item.SEARCH_NO;
                    udjDr["STEP_ID"] = item.STEP_ID;
                    udjDr["JOIN_TYPE"] = item.JOIN_TYPE;
                    udjDr["USER_ID"] = item.USER_ID;
                    udjDr["UPDATE_TIME"] = item.UPDATE_TIME;
                    udjDr.EndEdit();
                    ds.Tables["UDA_D_JOIN"].Rows.Add(udjDr);
                }

                //定義來源欄位關聯檔
                foreach (var item in udjcList)
                {
                    DataRow udjcDr = ds.Tables["UDA_D_JOINCOLUMN"].NewRow();
                    udjcDr.BeginEdit();
                    udjcDr["SEARCH_NO"] = item.SEARCH_NO;
                    udjcDr["STEP_ID"] = item.STEP_ID;
                    udjcDr["JOIN_SET_ID"] = item.JOIN_SET_ID;
                    udjcDr["JOIN_SOURCE_FROM"] = item.JOIN_SOURCE_FROM;
                    udjcDr["JOIN_COLUMN_FROM"] = item.JOIN_COLUMN_FROM;
                    udjcDr["JOIN_SOURCE_TO"] = item.JOIN_SOURCE_TO;
                    udjcDr["JOIN_COLUMN_TO"] = item.JOIN_COLUMN_TO;
                    udjcDr["USER_ID"] = item.USER_ID;
                    udjcDr["UPDATE_TIME"] = item.UPDATE_TIME;
                    udjcDr.EndEdit();
                    ds.Tables["UDA_D_JOINCOLUMN"].Rows.Add(udjcDr);
                }

                //定義關聯輸出欄位檔
                //foreach (var item in udjoList)
                //{
                //    DataRow udjoDr = ds.Tables["UDA_D_JOINOUTPUT"].NewRow();
                //    udjoDr.BeginEdit();
                //    udjoDr["SEARCH_NO"] = item.SEARCH_NO;
                //    udjoDr["STEP_ID"] = item.STEP_ID;
                //    udjoDr["COLUMN_SEQ"] = item.COLUMN_SEQ;
                //    udjoDr["SRC_STEP_ID"] = item.SRC_STEP_ID;
                //    udjoDr["SRC_COLUMN_SEQ"] = item.SRC_COLUMN_SEQ;
                //    udjoDr["COLUMN_SHOW_NAME"] = item.COLUMN_SHOW_NAME;
                //    udjoDr["COLUMN_ALIAS_NAME"] = item.COLUMN_ALIAS_NAME;
                //    udjoDr["COLUMNTYPE_CODE"] = item.COLUMNTYPE_CODE;
                //    udjoDr["IS_SELECT_FG"] = item.IS_SELECT_FG;
                //    udjoDr["USER_ID"] = item.USER_ID;
                //    udjoDr["UPDATE_TIME"] = item.UPDATE_TIME;
                //    udjoDr.EndEdit();
                //    ds.Tables["UDA_D_JOINOUTPUT"].Rows.Add(udjoDr);
                //}

                //定義聚合檔
                foreach (var item in udaList)
                {
                    DataRow udaDr = ds.Tables["UDA_D_AGGRATION"].NewRow();
                    udaDr.BeginEdit();
                    udaDr["SEARCH_NO"] = item.SEARCH_NO;
                    udaDr["STEP_ID"] = item.STEP_ID;
                    udaDr.EndEdit();
                    ds.Tables["UDA_D_AGGRATION"].Rows.Add(udaDr);
                }

                //定義聚合欄位檔
                foreach (var item in udacList)
                {
                    DataRow udacDr = ds.Tables["UDA_D_AGGRECOLUMN"].NewRow();
                    udacDr.BeginEdit();
                    udacDr["SEARCH_NO"] = item.SEARCH_NO;
                    udacDr["STEP_ID"] = item.STEP_ID;
                    udacDr["AGGRE_SET_ID"] = item.AGGRE_SET_ID;
                    udacDr["AGGREGATION_CODE"] = item.AGGREGATION_CODE;
                    udacDr["AGGREGATION_DESC"] = item.AGGREGATION_DESC;
                    udacDr["SRC_STEP_ID"] = item.SRC_STEP_ID;
                    udacDr["SRC_COLUMN_ID"] = item.SRC_COLUMN_ID;
                    udacDr["USER_ID"] = item.USER_ID;
                    udacDr["UPDATE_TIME"] = item.UPDATE_TIME;
                    udacDr["COLUMNTYPE_CODE"] = item.COLUMNTYPE_CODE;
                    udacDr.EndEdit();
                    ds.Tables["UDA_D_AGGRECOLUMN"].Rows.Add(udacDr);
                }

                //定義聚合輸出欄位檔
                //foreach (var item in udaoList)
                //{
                //    DataRow udaoDr = ds.Tables["UDA_D_AGGRATIONOUTPUT"].NewRow();
                //    udaoDr.BeginEdit();
                //    udaoDr["SEARCH_NO"] = item.SEARCH_NO;
                //    udaoDr["STEP_ID"] = item.STEP_ID;
                //    udaoDr["COLUMN_SEQ"] = item.COLUMN_SEQ;
                //    udaoDr["SRC_STEP_ID"] = item.SRC_STEP_ID;
                //    udaoDr["SRC_COLUMN_SEQ"] = item.SRC_COLUMN_SEQ;
                //    udaoDr["AGGREGATION_CODE"] = item.AGGREGATION_CODE;
                //    udaoDr["COLUMN_SHOW_NAME"] = item.COLUMN_SHOW_NAME;
                //    udaoDr["COLUMN_ALIAS_NAME"] = item.COLUMN_ALIAS_NAME;
                //    udaoDr["COLUMNTYPE_CODE"] = item.COLUMNTYPE_CODE;
                //    udaoDr["IS_SELECT_FG"] = item.IS_SELECT_FG;
                //    udaoDr["USER_ID"] = item.USER_ID;
                //    udaoDr["UPDATE_TIME"] = item.UPDATE_TIME;
                //    udaoDr.EndEdit();
                //    ds.Tables["UDA_D_AGGRATIONOUTPUT"].Rows.Add(udaoDr);
                //}

                //定義輸出檔
                foreach (var item in udoList)
                {
                    DataRow udoDr = ds.Tables["UDA_D_OUTPUT"].NewRow();
                    udoDr.BeginEdit();
                    udoDr["SEARCH_NO"] = item.SEARCH_NO;
                    udoDr["STEP_ID"] = item.STEP_ID;
                    udoDr["OUTPUT_NAME"] = item.OUTPUT_NAME;
                    udoDr["USER_ID"] = item.USER_ID;
                    udoDr["UPDATE_TIME"] = item.UPDATE_TIME;
                    udoDr.EndEdit();
                    ds.Tables["UDA_D_OUTPUT"].Rows.Add(udoDr);
                }

                //定義輸出欄位檔
                foreach (var item in udocList)
                {
                    DataRow udocDr = ds.Tables["UDA_D_OUTCOLUMN"].NewRow();
                    udocDr.BeginEdit();
                    udocDr["SEARCH_NO"] = item.SEARCH_NO;
                    udocDr["STEP_ID"] = item.STEP_ID;
                    udocDr["COLUMN_SEQ"] = item.COLUMN_SEQ;
                    udocDr["SRC_STEP_ID"] = item.SRC_STEP_ID;
                    udocDr["SRC_COLUMN_SEQ"] = item.SRC_COLUMN_SEQ;
                    udocDr["COLUMN_SHOW_NAME"] = item.COLUMN_SHOW_NAME;
                    udocDr["COLUMNTYPE_CODE"] = item.COLUMNTYPE_CODE;
                    udocDr["IS_SELECT_FG"] = item.IS_SELECT_FG;
                    udocDr["IS_SHOW_FG"] = item.IS_SHOW_FG;
                    udocDr["USER_ID"] = item.USER_ID;
                    udocDr["UPDATE_TIME"] = item.UPDATE_TIME;
                    udocDr.EndEdit();
                    ds.Tables["UDA_D_OUTCOLUMN"].Rows.Add(udocDr);
                }

                TableParam[] param;
                param = new TableParam[]
                {
                    new TableParam("UDA_M_SEARCH", "UDA_M_SEARCH", DataConcurrencyMode.None, null, null, new string[]{"SEARCH_NO"}),
                    new TableParam("UDA_D_SOURCE", "UDA_D_SOURCE", DataConcurrencyMode.None, null, null, new string[]{"SEARCH_NO", "STEP_ID"}),
                    new TableParam("UDA_D_COLUMN", "UDA_D_COLUMN", DataConcurrencyMode.None, null, null, new string[]{"SEARCH_NO", "STEP_ID", "COLUMN_SEQ"}),
                    new TableParam("UDA_D_CONDITION", "UDA_D_CONDITION", DataConcurrencyMode.None, null, null, new string[]{"SEARCH_NO", "STEP_ID", "COLUMN_SEQ", "CONDTITION_ID"}),
                    new TableParam("UDA_D_JOIN", "UDA_D_JOIN", DataConcurrencyMode.None, null, null, new string[]{"SEARCH_NO", "STEP_ID"}),
                    new TableParam("UDA_D_JOINCOLUMN", "UDA_D_JOINCOLUMN", DataConcurrencyMode.None, null, null, new string[]{"SEARCH_NO", "STEP_ID", "JOIN_SET_ID"}),
                    //new TableParam("UDA_D_JOINOUTPUT", "UDA_D_JOINOUTPUT", DataConcurrencyMode.None, null, null, new string[]{"SEARCH_NO", "STEP_ID", "COLUMN_SEQ"}),
                    new TableParam("UDA_D_AGGRATION", "UDA_D_AGGRATION", DataConcurrencyMode.None, null, null, new string[]{"SEARCH_NO", "STEP_ID"}),
                    new TableParam("UDA_D_AGGRECOLUMN", "UDA_D_AGGRECOLUMN", DataConcurrencyMode.None, null, null, new string[]{"SEARCH_NO", "STEP_ID", "AGGRE_SET_ID"}),
                    //new TableParam("UDA_D_AGGRATIONOUTPUT", "UDA_D_AGGRATIONOUTPUT", DataConcurrencyMode.None, null, null, new string[]{"SEARCH_NO", "STEP_ID", "COLUMN_SEQ"}),
                    new TableParam("UDA_D_OUTPUT", "UDA_D_OUTPUT", DataConcurrencyMode.None, null, null, new string[]{"SEARCH_NO", "STEP_ID"}),
                    new TableParam("UDA_D_OUTCOLUMN", "UDA_D_OUTCOLUMN", DataConcurrencyMode.None, null, null, new string[]{"SEARCH_NO", "STEP_ID", "COLUMN_SEQ"})
                };

                m_da.Update(ds, param, m_connectionStringKey);

                #endregion
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        class NameMappingAttribute : Attribute
        {
            private string jsonName = string.Empty;
            public NameMappingAttribute(string jsonName)
            {
                this.jsonName = jsonName;
            }

            public string JsonName
            {
                get { return jsonName; }
                set { jsonName = value; }
            }
        }

        /// <summary>
        /// 使用者查詢定義檔
        /// </summary>
        class UDA_M_SEARCH
        {
            public int SEARCH_NO { get; set; }
            public string SEARCH_NAME { get; set; }
            public string NOTE { get; set; }
            public string EMP_NO { get; set; }
            public string STATUS { get; set; }
            public DateTime CREATE_TIME { get; set; }
            public string USER_ID { get; set; }
            public DateTime UPDATE_TIME { get; set; }
        }

        /// <summary>
        /// 定義來源檔
        /// </summary>
        class UDA_D_SOURCE
        {
            public int SEARCH_NO { get; set; }
            [NameMapping("Step")]
            public int STEP_ID { get; set; }
            [NameMapping("VerNo")]
            public string VER_NO { get; set; }
            [NameMapping("Name")]
            public string DATABASE_NAME { get; set; }
            [NameMapping("TableName")]
            public string TABLE_NAME { get; set; }
            [NameMapping("TableCName")]
            public string SOURCE_NAME { get; set; }
            public string USER_ID { get; set; }
            public DateTime UPDATE_TIME { get; set; }
        }

        /// <summary>
        /// 定義來源欄位檔
        /// </summary>
        class UDA_D_COLUMN
        {
            public int SEARCH_NO { get; set; }
            public int STEP_ID { get; set; }
            [NameMapping("ColumnSeq")]
            public int COLUMN_SEQ { get; set; }
            public string VER_NO { get; set; }
            public string DATABASE_NAME { get; set; }
            public string TABLE_NAME { get; set; }
            [NameMapping("Name")]
            public string COLUMN_NAME { get; set; }
            [NameMapping("CName")]
            public string COLUMN_SHOW_NAME { get; set; }
            [NameMapping("CType")]
            public string COLUMNTYPE_CODE { get; set; }
            [NameMapping("IsSelected")]
            public string IS_SELECT_FG { get; set; }
            [NameMapping("IsShow")]
            public string IS_SHOW_FG { get; set; }
            public string USER_ID { get; set; }
            public DateTime UPDATE_TIME { get; set; }
        }

        /// <summary>
        /// 定義來源篩選條件檔
        /// </summary>
        class UDA_D_CONDITION
        {
            public int SEARCH_NO { get; set; }
            public int STEP_ID { get; set; }
            [NameMapping("ColumnSeq")]
            public int COLUMN_SEQ { get; set; }
            public int CONDTITION_ID { get; set; }
            [NameMapping("ConditionDefine")]
            public string COND_CODE { get; set; }
            [NameMapping("ConditionType")]
            public string COND_TYPE { get; set; }
            [NameMapping("ConditionValue")]
            public string COND_VALUE { get; set; }
            public string USER_ID { get; set; }
            public DateTime UPDATE_TIME { get; set; }
        }

        /// <summary>
        /// 定義來源關聯檔
        /// </summary>
        class UDA_D_JOIN
        {
            public int SEARCH_NO { get; set; }
            [NameMapping("Step")]
            public int STEP_ID { get; set; }
            [NameMapping("JoinType")]
            public string JOIN_TYPE { get; set; }
            public string USER_ID { get; set; }
            public DateTime UPDATE_TIME { get; set; }
        }

        /// <summary>
        /// 定義來源欄位關聯檔
        /// </summary>
        class UDA_D_JOINCOLUMN
        {
            public int SEARCH_NO { get; set; }
            public int STEP_ID { get; set; }
            public int JOIN_SET_ID { get; set; }
            public int JOIN_SOURCE_FROM { get; set; }
            public int JOIN_COLUMN_FROM { get; set; }
            public int JOIN_SOURCE_TO { get; set; }
            public int JOIN_COLUMN_TO { get; set; }
            public string USER_ID { get; set; }
            public DateTime UPDATE_TIME { get; set; }
        }

        /// <summary>
        /// 定義關聯輸出欄位檔
        /// </summary>
        //class UDA_D_JOINOUTPUT
        //{
        //    public int SEARCH_NO { get; set; }
        //    public int STEP_ID { get; set; }
        //    public int COLUMN_SEQ { get; set; }
        //    [NameMapping("SRCStep")]
        //    public int SRC_STEP_ID { get; set; }
        //    [NameMapping("ColumnSeq")]
        //    public int SRC_COLUMN_SEQ { get; set; }
        //    [NameMapping("CName")]
        //    public string COLUMN_SHOW_NAME { get; set; }
        //    [NameMapping("Name")]
        //    public string COLUMN_ALIAS_NAME { get; set; }
        //    [NameMapping("CType")]
        //    public string COLUMNTYPE_CODE { get; set; }
        //    [NameMapping("IsShow")]
        //    public string IS_SELECT_FG { get; set; }
        //    public string USER_ID { get; set; }
        //    public DateTime UPDATE_TIME { get; set; }
        //}

        /// <summary>
        /// 定義聚合檔
        /// </summary>
        class UDA_D_AGGRATION
        {
            public int SEARCH_NO { get; set; }
            [NameMapping("Step")]
            public int STEP_ID { get; set; }
        }

        /// <summary>
        /// 定義聚合欄位檔
        /// </summary>
        class UDA_D_AGGRECOLUMN
        {
            public int SEARCH_NO { get; set; }
            public int STEP_ID { get; set; }
            public int AGGRE_SET_ID { get; set; }
            [NameMapping("CustType")]
            public string AGGREGATION_CODE { get; set; }
            [NameMapping("UPDName")]
            public string AGGREGATION_DESC { get; set; }
            [NameMapping("CStep")]
            public int SRC_STEP_ID { get; set; }
            [NameMapping("ColumnSeq")]
            public int SRC_COLUMN_ID { get; set; }
            public string USER_ID { get; set; }
            public DateTime UPDATE_TIME { get; set; }
            [NameMapping("CType")]
            public string COLUMNTYPE_CODE { get; set; }
        }

        /// <summary>
        /// 定義聚合輸出欄位檔
        /// </summary>
        //class UDA_D_AGGRATIONOUTPUT
        //{
        //    public int SEARCH_NO { get; set; }
        //    public int STEP_ID { get; set; }
        //    public int COLUMN_SEQ { get; set; }
        //    [NameMapping("CStep")]
        //    public int SRC_STEP_ID { get; set; }
        //    [NameMapping("ColumnSeq")]
        //    public int SRC_COLUMN_SEQ { get; set; }
        //    public string AGGREGATION_CODE { get; set; }
        //    [NameMapping("CName")]
        //    public string COLUMN_SHOW_NAME { get; set; }
        //    [NameMapping("Name")]
        //    public string COLUMN_ALIAS_NAME { get; set; }
        //    [NameMapping("CType")]
        //    public string COLUMNTYPE_CODE { get; set; }
        //    [NameMapping("IsSelected")]
        //    public string IS_SELECT_FG { get; set; }
        //    public string USER_ID { get; set; }
        //    public DateTime UPDATE_TIME { get; set; }
        //}

        /// <summary>
        /// 定義輸出檔
        /// </summary>
        class UDA_D_OUTPUT
        {
            public int SEARCH_NO { get; set; }
            [NameMapping("Step")]
            public int STEP_ID { get; set; }
            [NameMapping("TableCName")]
            public string OUTPUT_NAME { get; set; }
            public string NOTE { get; set; }
            public string USER_ID { get; set; }
            public DateTime UPDATE_TIME { get; set; }
        }

        /// <summary>
        /// 定義輸出欄位檔
        /// </summary>
        class UDA_D_OUTCOLUMN
        {
            public int SEARCH_NO { get; set; }
            public int STEP_ID { get; set; }
            public int COLUMN_SEQ { get; set; }
            [NameMapping("SRCStep")]
            public int SRC_STEP_ID { get; set; }
            [NameMapping("ColumnSeq")]
            public int SRC_COLUMN_SEQ { get; set; }
            [NameMapping("CName")]
            public string COLUMN_SHOW_NAME { get; set; }
            [NameMapping("Name")]
            public string COLUMN_ALIAS_NAME { get; set; }
            public string NOTE { get; set; }
            [NameMapping("CType")]
            public string COLUMNTYPE_CODE { get; set; }
            [NameMapping("IsSelected")]
            public string IS_SELECT_FG { get; set; }
            [NameMapping("IsShow")]
            public string IS_SHOW_FG { get; set; }
            public string USER_ID { get; set; }
            public DateTime UPDATE_TIME { get; set; }
        }
    }
}