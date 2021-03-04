using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EntityFrameworkDBF.FOREIGN_VISITOR
{
    public partial class FOR_HOME : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void lnkDashboard_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Operator_Home.aspx");
        }

        protected void lnkFor_Visitor_Click(object sender, EventArgs e)
        {
            Response.Redirect("~\\FOREIGN VISITOR\\ForeignVisitor.aspx");
        }

        protected void btnissuehome_Click(object sender, EventArgs e)
        {
            Response.Redirect("~\\DVSC_HOME.aspx");
        }

        protected void lnk_Report_Click(object sender, EventArgs e)
        {
            Response.Redirect("~\\DVSC_HOME.aspx");
        }
    }
}