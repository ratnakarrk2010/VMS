using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EntityFrameworkDBF
{
    public partial class Print_BankPass : System.Web.UI.Page
    {
        VMSEntities DVSC = new VMSEntities();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    string CardNo = Session["Cont_CardNo"].ToString();
                    var PrintData = (from x in DVSC.CONTRACTOR_DETAIL
                                     join f in DVSC.FIRMMASTERs on x.Cont_FirmID equals f.FIRM_ID
                                     join d in DVSC.DESIGNATION_MASTER on x.Cont_DesignationID equals d.DESIGNATION_ID
                                     join p in DVSC.PSU_MASTER on x.Cont_PSUunitID equals p.PSU_ID
                                     where (x.Cont_CardNo == CardNo)
                                     select new { x.Cont_Aadhaar, x.Cont_CardNo, x.Cont_Name, x.Cont_Unit, d.DESIGNATION_NAME, p.PSU_NAME, x.Cont_IssueDate, x.Cont_PVCValidity, x.Cont_Photo, x.Cont_Id, x.Cont_IcardrNo }).ToList();
                    txtCasualCardNo.Text = PrintData[0].Cont_CardNo;
                    txtDesignation.Text = PrintData[0].DESIGNATION_NAME;
                    txtCasualName.Text = PrintData[0].Cont_Name;
                    txtDateOfIssue.Text = Convert.ToDateTime(PrintData[0].Cont_IssueDate).ToString("dd/MM/yyyy").Replace("/", " ");
                    txtPvc.Text = PrintData[0].Cont_Aadhaar;//Convert.ToDateTime(PrintData[0].Cont_PVCValidity).ToString("dd/MM/yyyy").Replace("/", " ");
                    txtFirm.Text = PrintData[0].PSU_NAME;
                    imgCasual.ImageUrl = "~/GetImages.ashx?id=" + PrintData[0].Cont_Id;
                }
                catch (Exception ee)
                {
                    Response.Write("<script> alert('Not able to get data due to some issue')</script>");
                }
            }
        }
    }
}