using Accudata.Data.General.Command;
using Accudata.Uda.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Script.Serialization;

namespace IGA06
{
    /// <summary>
    /// JobHandler 的摘要描述
    /// </summary>
    public class JobHandler : IHttpHandler
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

                var searchNo = dictData.Values.ToArray()[0].ToString();
                var userId = dictData.Values.ToArray()[1].ToString();
                var empNo = dictData.Values.ToArray()[2].ToString();

                List<UDA_D_JOBCONDITION> udjcList = new List<UDA_D_JOBCONDITION>();
                List<UDA_D_JOB_OUTCONDITION> udjocList = new List<UDA_D_JOB_OUTCONDITION>();

                foreach (Dictionary<string, object> item in objData)
                {
                    if (item["Type"].ToString() == "UDA_D_CONDITIN")
                    {
                        UDA_D_JOBCONDITION udjc = new UDA_D_JOBCONDITION();

                        foreach (string key in item.Keys)
                        {
                            PropertyInfo pi = udjc.GetType().GetProperties()
                                .Where(prop => prop.GetCustomAttributes(typeof(NameMappingAttribute), false).Cast<NameMappingAttribute>().Where(attr => attr.JsonName == key).Count() > 0).DefaultIfEmpty(null).First();

                            if (pi != null)
                            {
                                pi.SetValue(udjc, ConvertType(pi, item[key]), null);
                            }
                        }

                        udjcList.Add(udjc);
                    }
                    else
                    {
                        UDA_D_JOB_OUTCONDITION udjoc = new UDA_D_JOB_OUTCONDITION();

                        foreach (string key in item.Keys)
                        {
                            PropertyInfo pi = udjoc.GetType().GetProperties()
                                .Where(prop => prop.GetCustomAttributes(typeof(NameMappingAttribute), false).Cast<NameMappingAttribute>().Where(attr => attr.JsonName == key).Count() > 0).DefaultIfEmpty(null).First();

                            if (pi != null)
                            {
                                pi.SetValue(udjoc, ConvertType(pi, item[key]), null);
                            }
                        }

                        udjocList.Add(udjoc);
                    }
                }
                
                UpdateParameter[] ups = new UpdateParameter[4];
                ups[0] = new UpdateParameter("i_SEARCH_NO", "OleDbType.Integer", 10, searchNo, "ParameterDirection.Input");
                ups[1] = new UpdateParameter("i_EMP_NO", "OleDbType.VarChar", 30, empNo, "ParameterDirection.Input");
                ups[2] = new UpdateParameter("i_USER_ID", "OleDbType.VarChar", 30, userId, "ParameterDirection.Input");
                ups[3] = new UpdateParameter("o_JOB_NO", "OleDbType.Integer", 24, "", "ParameterDirection.Output");

                //呼叫SP
                string[] getData = m_da.Update("sp_create_new_job", ups, m_connectionStringKey);

                //資料更新
                List<string> uCmd = new List<string>();
                foreach (var item in udjcList)
                {
                    uCmd.Add(string.Format(@"UPDATE UDA_D_JOBCONDITION SET COND_VALUE = '{0}' WHERE JOB_NO = '{1}' AND STEP_ID = '{2}' AND COLUMN_SEQ = '{3}' AND CONDTITION_ID = '{4}'", item.COND_VALUE, getData[0], item.STEP_ID, item.COLUMN_SEQ, item.CONDTITION_ID));
                }

                m_da.Update(uCmd.ToArray(), m_connectionStringKey);

                Agent ag = new Agent("172.16.5.89", 9980);
                ag.ExecuteJob(getData[0].ToString());
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
        /// 工作來源篩選條件檔
        /// </summary>
        class UDA_D_JOBCONDITION
        {
            public int JOB_NO { get; set; }
            [NameMapping("Step")]
            public int STEP_ID { get; set; }
            [NameMapping("ColumnSeq")]
            public int COLUMN_SEQ { get; set; }
            [NameMapping("ConditionId")]
            public int CONDTITION_ID { get; set; }
            [NameMapping("ConditionValue")]
            public string COND_VALUE { get; set; }
            //[NameMapping("")]
            //public string COND_CODE { get; set; }
            //[NameMapping("")]
            //public string COND_TYPE { get; set; }
            //public string USER_ID { get; set; }
            //public DateTime UPDATE_TIME { get; set; }
        }

        /// <summary>
        /// 工作輸出篩選條件檔
        /// </summary>
        class UDA_D_JOB_OUTCONDITION
        {
            public int JOB_NO { get; set; }
            [NameMapping("Step")]
            public int STEP_ID { get; set; }
            [NameMapping("ColumnSeq")]
            public int COLUMN_SEQ { get; set; }
            [NameMapping("ConditionId")]
            public int CONDTITION_ID { get; set; }
            [NameMapping("ConditionValue")]
            public string COND_VALUE { get; set; }
            //[NameMapping("")]
            //public string COND_CODE { get; set; }
            //[NameMapping("")]
            //public string COND_TYPE { get; set; }
            //public string USER_ID { get; set; }
            //public DateTime UPDATE_TIME { get; set; }
        }
    }
}