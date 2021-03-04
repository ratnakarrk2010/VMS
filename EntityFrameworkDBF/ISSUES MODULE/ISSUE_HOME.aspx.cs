using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EntityFrameworkDBF.ISSUES
{
    public partial class ISSUE_HOME : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                spnDate.InnerText = DateTime.Now.Date.ToString("dd-MMM-yyyy").Replace("-", " ").ToUpper();
            }
        }
        /// <summary>
        /// Note In Session["PASSTYPE"] 1 for Contractor ,2 For Escorted ,3 for Bank ,4 for Canteen ,5 For Labour
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnissuehome_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("~\\DVSC_HOME.aspx");
            }
            catch (Exception)
            {
                Response.Write("<script> alert('Not able to reedirect due to some issue')</script>");
            }
        }

        protected void lnkContractor_Click(object sender, EventArgs e)
        {
            try
            {
                Session["PASSTYPE"] = 1;
                Response.Redirect("~\\ISSUES MODULE\\CONTRACTOR.aspx");
            }
            catch (Exception)
            {
                Response.Write("<script> alert('Not able to reedirect due to some issue')</script>");
            }
        }

        protected void lnkEscrted_Click(object sender, EventArgs e)
        {
            try
            {
                Session["PASSTYPE"] = 2;
                Response.Redirect("~\\ISSUES MODULE\\CONTRACTOR.aspx");
            }
            catch (Exception)
            {
                Response.Write("<script> alert('Not able to reedirect due to some issue')</script>");
            }
        }

        protected void lnkBank_Click(object sender, EventArgs e)
        {
            try
            {
                Session["PASSTYPE"] = 3;
                Response.Redirect("~\\ISSUES MODULE\\CONTRACTOR.aspx");
            }
            catch (Exception)
            {
                Response.Write("<script> alert('Not able to reedirect due to some issue')</script>");
            }
        }

        protected void lnkCanteen_Click(object sender, EventArgs e)
        {
            try
            {
                Session["PASSTYPE"] = 4;
                Response.Redirect("~\\ISSUES MODULE\\CONTRACTOR.aspx");
            }
            catch (Exception)
            {
                Response.Write("<script> alert('Not able to reedirect due to some issue')</script>");
            }
        }
    }
}