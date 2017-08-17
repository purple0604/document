using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IGA06
{
    public partial class Main : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["SearchNo"] != null)
            {
                SearchNo = Request.QueryString["SearchNo"];
            }
            else
            {
                SearchNo = "";
            }
            this.UserId = Request.QueryString["User_Id"];
            this.EmpNo = Request.QueryString["Emp_No"];

            DataBind();
        }

        public string UserId { get; set; }
        public string EmpNo { get; set; }

        public string SearchNo
        {
            get; set;
        }
    }
}