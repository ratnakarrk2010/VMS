using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EntityFrameworkDBF.PRINT
{
    public partial class PRINT_HOME : System.Web.UI.Page
    {
        VMSEntities DVSC = new VMSEntities();
        DropDownFunction ddl = new DropDownFunction();
        protected void Page_Load(object sender, EventArgs e)
        {
            MaintainScrollPositionOnPostBack = true;
            if (!IsPostBack)
            {
                spnDate.InnerText = DateTime.Now.Date.ToString("dd-MMM-yyyy").Replace("-", " ").ToUpper();
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            int Dockyardid = 0;
            if (ddlsearch.SelectedIndex > 0)
            {
                if (ddlsearch.SelectedItem.Text == "DOCKYARD ID NO")
                {
                    Dockyardid = Convert.ToInt32(txtSearch.Text.Trim());
                }
                var table1 = (from c in DVSC.CONTRACTOR_DETAIL
                              join d in DVSC.DESIGNATION_MASTER on c.Cont_DesignationID equals d.DESIGNATION_ID
                              join s in DVSC.STATE_MASTER on c.Cont_StateID equals s.STATE_ID
                              join f in DVSC.FIRMMASTERs on c.Cont_FirmID equals f.FIRM_ID
                              join r in DVSC.RELIGION_MASTER on c.Cont_ReligionID equals r.R_ID
                              join n in DVSC.COUNTRY_MASTER on c.Cont_NationalityID equals n.COUNTRY_ID
                              join cr in DVSC.CANCEL_REASON_MASTER on c.Cont_CancelReason equals cr.CR_ID
                              join u in DVSC.PSU_MASTER on c.Cont_PSUunitID equals u.PSU_ID
                              where ((c.Cont_Aadhaar == (txtSearch.Text.Trim()))
                    || (c.Cont_CardNo == (txtSearch.Text.Trim()))
                    || (c.Cont_RFIDNo == (txtSearch.Text.Trim())) ||
                    (c.Cont_DocID == Dockyardid))
                              select new
                              {
                                  c.Cont_Name,
                                  c.Cont_Aadhaar,
                                  c.Cont_Mobile,
                                  c.Cont_FirmFileNo,
                                  c.Cont_FirmID,
                                  f.FIRM_NAME,
                                  c.Cont_FirmWO,
                                  c.Cont_WOValidity,
                                  c.Cont_Unit,
                                  c.Cont_StateID,
                                  s.STATE_NAME,
                                  c.Cont_PVCNO,
                                  c.Cont_PVCValidity,
                                  c.Cont_RFIDNo,
                                  c.Cont_RFIDValidity,
                                  c.Cont_CardNo,
                                  c.Cont_DesignationID,
                                  d.DESIGNATION_NAME,
                                  c.Cont_ReligionID,
                                  r.R_NAME,
                                  c.Cont_Gender,
                                  c.Cont_IssueDate,
                                  c.Cont_CancelFLag,
                                  c.Cont_CancelReason,
                                  cr.CR_NAME,
                                  c.Cont_PassType,
                                  c.Cont_DateOFLoss,
                                  c.Cont_PlaceOfLoss,
                                  c.Cont_Fine,
                                  c.Cont_Fir,
                                  c.Cont_CancelDate,
                                  c.Cont_BlackList,
                                  c.Cont_IcardrNo,
                                  c.Cont_PSUunitID,
                                  u.PSU_NAME,
                                  c.Cont_UnitEmp
                              }).ToList();
                if (table1.Count > 0)
                {
                    Session["Cont_CardNo"] = table1[0].Cont_CardNo;
                    Gv_Reports.DataSource = table1;
                    Gv_Reports.DataBind();
                    Session["table1"] = table1;
                    //PrintPass(table1[0].Cont_CardNo);
                }
                else
                {
                    Gv_Reports.DataSource = null;
                    Gv_Reports.DataBind();
                    Response.Write("<script> alert('No data found')</script>");
                }
            }
        }

        protected void Gv_Reports_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            Gv_Reports.PageIndex = e.NewPageIndex;
            Gv_Reports.DataSource = Session["table1"];
            Gv_Reports.DataBind();
        }

        protected void btnPrint_Click(object sender, EventArgs e)
        {
            string CardNo = Session["Cont_CardNo"].ToString();
            PrintPass(CardNo);
        }

        public void PrintPass(string cardNo)
        {
            string CardNo = cardNo;
            Session["Cont_CardNo"] = cardNo;
            try
            {
                if (Session["Cont_CardNo"] != null)
                {
                    var PrintData = (from x in DVSC.CONTRACTOR_DETAIL.Where(x => x.Cont_CardNo == CardNo) select x).First();
                    if (PrintData.Cont_PassType == "CONTRACTOR")
                    {
                        string url = "../Print_ContractorPass.aspx?TranID=" + PrintData.Cont_CardNo;
                        string fullURL = "window.open('" + url + "', '_blank', 'top=50,left=100,height=500,width=400,status=yes,toolbar=no,menubar=no,location=no,scrollbars=no,resizable=no,titlebar=no' );";
                        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", fullURL, true);
                        //ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ContPopup();", true);
                        //ClientScript.RegisterStartupScript(this.GetType(), "Popup", "Popup2();", true);

                        ddl.save_data_db("update PrintStatus set PrintValue = 1");
                    }
                    if (PrintData.Cont_PassType == "ESCORTED")
                    {
                        string url = "../Print_EscortedPass.aspx?TranID=" + PrintData.Cont_CardNo;
                        string fullURL = "window.open('" + url + "', '_blank', 'top=50,left=100,height=500,width=400,status=yes,toolbar=no,menubar=no,location=no,scrollbars=no,resizable=no,titlebar=no' );";
                        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", fullURL, true);
                        //ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ContPopupEs();", true);
                        //ClientScript.RegisterStartupScript(this.GetType(), "Popup", "Popup2();", true);

                        ddl.save_data_db("update PrintStatus set PrintValue = 1");
                    }
                    if (PrintData.Cont_PassType == "BANK")
                    {
                        string url = "../Print_BankPass.aspx?TranID=" + PrintData.Cont_CardNo;
                        string fullURL = "window.open('" + url + "', '_blank', 'top=50,left=100,height=500,width=400,status=yes,toolbar=no,menubar=no,location=no,scrollbars=no,resizable=no,titlebar=no' );";
                        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", fullURL, true);
                        //ClientScript.RegisterStartupScript(this.GetType(), "Popup", "BankPopup();", true);
                        //ClientScript.RegisterStartupScript(this.GetType(), "Popup", "Popup2();", true);

                        ddl.save_data_db("update PrintStatus set PrintValue = 1");
                    }
                    if (PrintData.Cont_PassType == "CB")
                    {
                        string url = "../Print_DBPass.aspx?TranID=" + PrintData.Cont_CardNo;
                        string fullURL = "window.open('" + url + "', '_blank', 'top=50,left=100,height=500,width=400,status=yes,toolbar=no,menubar=no,location=no,scrollbars=no,resizable=no,titlebar=no' );";
                        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", fullURL, true);
                        //ClientScript.RegisterStartupScript(this.GetType(), "Popup", "CBPopup();", true);
                        //ClientScript.RegisterStartupScript(this.GetType(), "Popup", "Popup2();", true);

                        ddl.save_data_db("update PrintStatus set PrintValue = 1");
                    }
                    if (PrintData.Cont_PassType == "LABOUR")
                    {
                        Response.Write("<script>alert('You cannot print pass for labours.')</script>");
                    }
                }
            }
            catch (Exception)
            {
                Response.Write("<script> alert('Error while Loading data.')</script>");
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

    }
}