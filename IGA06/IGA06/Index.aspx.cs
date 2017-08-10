using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IGA06
{
    public partial class Index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.UserId = Request.QueryString["user_id"];
            this.EmpNo = Request.QueryString["Emp_No"];
        }

        public string UserId { get; set; }
        public string EmpNo { get; set; }
    }
}