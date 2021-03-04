using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EntityFrameworkDBF
{
    public partial class Print_EscortedPass : System.Web.UI.Page
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
                                     where (x.Cont_CardNo == CardNo)
                                     select new {x.Cont_Aadhaar, x.Cont_CardNo, x.Cont_Name, f.FIRM_NAME, d.DESIGNATION_NAME, x.Cont_IssueDate, x.Cont_PVCValidity, x.Cont_Photo, x.Cont_Id }).ToList();
                    //txtContractorCardNo.Text = PrintData[0].Cont_CardNo;
                    txtContractorFirm.Text = PrintData[0].FIRM_NAME;
                    txtContractorName.Text = PrintData[0].Cont_Name;
                    //txtDateOfIssue.Text = Convert.ToDateTime(PrintData[0].Cont_IssueDate).ToString("dd/MM/yyyy").Replace("/", " ");
                    txtDesignation.Text = PrintData[0].DESIGNATION_NAME;
                    txtEscortType.Text = "ESCORTED";
                    txtPVCDate.Text = PrintData[0].Cont_Aadhaar; //Convert.ToDateTime(PrintData[0].Cont_PVCValidity).ToString("dd/MM/yyyy").Replace("/", " ");
                    imgContractor.ImageUrl = "~/GetImages.ashx?id=" + PrintData[0].Cont_Id;
                }
                catch (Exception ee)
                {
                    Response.Write("<script> alert('Not able to get data due to some issue')</script>");
                }
            }
        }
    }
}