using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EntityFrameworkDBF.ADMIN_PANEL
{
    public partial class ADMIN_PANEL : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                spnDate.InnerText = DateTime.Now.Date.ToString("dd-MMM-yyyy").Replace("-", " ").ToUpper();
            }
        }

        protected void btnhome_Click(object sender, EventArgs e)
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

        protected void lnkcountry_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("~\\ADMIN PANEL\\COUNTRY.aspx");
            }
            catch (Exception)
            {
                Response.Write("<script> alert('Not able to reedirect due to some issue')</script>");
            }
        }

        protected void lnkstate_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("~\\ADMIN PANEL\\STATE.aspx");
            }
            catch (Exception)
            {
                Response.Write("<script> alert('Not able to reedirect due to some issue')</script>");
            }
        }

        protected void lnkdesignation_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("~\\ADMIN PANEL\\DESIGNATION.aspx");
            }
            catch (Exception)
            {
                Response.Write("<script> alert('Not able to reedirect due to some issue')</script>");
            }
        }

        protected void lnkfirm_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("~\\ADMIN PANEL\\FIRM.aspx");
            }
            catch (Exception)
            {
                Response.Write("<script> alert('Not able to reedirect due to some issue')</script>");
            }
        }

        protected void lnkdocument_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("~\\ADMIN PANEL\\DOCUMENT.aspx");
            }
            catch (Exception)
            {
                Response.Write("<script> alert('Not able to reedirect due to some issue')</script>");
            }
        }

        protected void lnkshop_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("~\\ADMIN PANEL\\SHOP.aspx");
            }
            catch (Exception)
            {
                Response.Write("<script> alert('Not able to reedirect due to some issue')</script>");
            }
        }

        protected void lnkcancel_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("~\\ADMIN PANEL\\CANCELREASON.aspx");
            }
            catch (Exception)
            {
                Response.Write("<script> alert('Not able to reedirect due to some issue')</script>");
            }
        }

        protected void lnkpasstype_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("~\\ADMIN PANEL\\PASSTYPE.aspx");
            }
            catch (Exception)
            {
                Response.Write("<script> alert('Not able to reedirect due to some issue')</script>");
            }
        }

        protected void lnkUserMaster_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("~\\ADMIN PANEL\\USERMASTER.aspx");
            }
            catch (Exception)
            {
                Response.Write("<script> alert('Not able to reedirect due to some issue')</script>");
            }
        }

        protected void lnkRoleMaster_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("~\\ADMIN PANEL\\ROLE.aspx");
            }
            catch(Exception)
            {
                Response.Write("<script> alert('Not able to reedirect due to some issue')</script>");
            }
        }

        protected void lnkReligion_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("~\\ADMIN PANEL\\RELIGION.aspx");
            }
            catch(Exception)
            {
                Response.Write("<script> alert('Not able to reedirect due to some issue')</script>");
            }
        }

        protected void lnkPSUunit_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("~\\ADMIN PANEL\\PSU.aspx");
            }
            catch (Exception)
            {
                Response.Write("<script> alert('Not able to reedirect due to some issue')</script>");
            }
        }
    }
}