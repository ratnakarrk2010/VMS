using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EntityFrameworkDBF
{
    public partial class DVSC_HOME : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                spnDate.InnerText = DateTime.Now.Date.ToString("dd-MMM-yyyy").Replace("-", " ").ToUpper();
            }
        }

        protected void lnkadmpnl_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("~\\ADMIN PANEL\\ADMIN_PANEL_HOME.aspx");
            }
            catch (Exception)
            {

                Response.Write("<script> alert('Not able to redirect due to some issue')</script>");
            }
        }

        protected void lnkissue_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("~\\ISSUES MODULE\\ISSUE_HOME.aspx");
            }
            catch (Exception)
            {

                Response.Write("<script> alert('Not able to redirect due to some issue')</script>");
            }
        }

        protected void lnkactivate_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("~\\ACTIVATION MODULE\\ACTIAVTE_HOME.aspx");
            }
            catch (Exception)
            {

                Response.Write("<script> alert('Not able to redirect due to some issue')</script>");
            }
        }

        protected void lnkcancel_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("~\\CANCELLATION MODULE\\CANCEL_HOME.aspx");
            }
            catch (Exception)
            {

                Response.Write("<script> alert('Not able to redirect due to some issue')</script>");
            }
        }

        protected void lnklabour_Click(object sender, EventArgs e)
        {
            try
            {
                Session["PASSTYPE"] = 5;
                Response.Redirect("~\\ISSUES MODULE\\CONTRACTOR.aspx");
            }
            catch (Exception)
            {

                Response.Write("<script> alert('Not able to redirect due to some issue')</script>");
            }
        }

        protected void lnkreports_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("~\\REPORTS\\REPORT_HOME.aspx");
                //Response.Redirect("~\\REPORT_HOME.aspx");
            }
            catch (Exception)
            {

                Response.Write("<script> alert('Not able to redirect due to some issue')</script>");
            }

        }

        protected void lnkrenew_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("~\\RENEWAL\\RENEWAL_HOME.aspx");
            }
            catch (Exception)
            {

                Response.Write("<script> alert('Not able to redirect due to some issue')</script>");
            }
        }

        protected void lnkrfid_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("~\\PRINT\\PRINT_HOME.aspx");
            }
            catch (Exception)
            {

                Response.Write("<script> alert('Not able to redirect due to some issue')</script>");
            }
        }

        protected void lnkcvpass_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("~\\Operator_Home.aspx");
            }
            catch (Exception)
            {

                Response.Write("<script> alert('Not able to redirect due to some issue')</script>");
            }
        }
    }
}