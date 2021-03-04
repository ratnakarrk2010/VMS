using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Microsoft.Reporting.WebForms;
using System.Data.Entity.SqlServer;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;
using System.Drawing;

namespace EntityFrameworkDBF.REPORTS
{
    public partial class REPORT_HOME : System.Web.UI.Page
    {
        // protected ParameterDetails stParameterDetails;
        VMSEntities DVSC = new VMSEntities();
        SmartAccessEntities smartaccess = new SmartAccessEntities();
        DataTable dt = new DataTable();
        DataSet ds = new DataSet();
        DateTime fromdate;
        DateTime todate;
        DateTime CuurentDate;
        SqlConnection scon = new SqlConnection(ConfigurationManager.ConnectionStrings["VMSConnection"].ConnectionString);
        DropDownFunction ddl = new DropDownFunction();
        int Dockyardid = 0;
        int firmid = 0;
        bool FlgFromDate, FlgTodate;
        int Count = 0;
        string ReportName = "";
        int Religion = 0;
        bool ReligionReport = false;
        int State = 0;
        bool StateReport = false;
        string PassType = "";
        List<string> pass = null;
        
        //Label lblmsg;
        protected void Page_Load(object sender, EventArgs e)
        {
            MaintainScrollPositionOnPostBack = true;
            if (!IsPostBack)
            {
                //spnDate.InnerText = DateTime.Now.Date.ToString("dd-MMM-yyyy").Replace("-", " ").ToUpper();
                ddl.BindFirmddl1(ref ddlFirm);
                ddlRelegion.Visible = false;
                lblRelegion.Visible = false;
                ddl.BindReligionddl1(ref ddlRelegion);
                lblCount.InnerText = "";
                //spnDate.InnerText = "fgdffdfdsf";
                //lblmsg.Text = "page load function invoked";
            }
            //lblmsg.Text = "page load after postback function invoked";
        }

        protected void btncancelhome_Click(object sender, EventArgs e)
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

        public void GridVisibility()
        {
            if (chkName.Checked == true)
                Gv_Reports.Columns[1].Visible = true;
            else
                Gv_Reports.Columns[1].Visible = false;

            if (chkAadhaar.Checked == true)
                Gv_Reports.Columns[2].Visible = true;
            else
                Gv_Reports.Columns[2].Visible = false;

            if (chkMobile.Checked == true)
                Gv_Reports.Columns[3].Visible = true;
            else
                Gv_Reports.Columns[3].Visible = false;

            if (chkFirmFileNo.Checked == true)
                Gv_Reports.Columns[4].Visible = true;
            else
                Gv_Reports.Columns[4].Visible = false;

            if (ChkFirmName.Checked == true)
                Gv_Reports.Columns[5].Visible = true;
            else
                Gv_Reports.Columns[5].Visible = false;

            if (chkWONO.Checked == true)
                Gv_Reports.Columns[6].Visible = true;
            else
                Gv_Reports.Columns[6].Visible = false;

            if (chkWOValidity.Checked == true)
                Gv_Reports.Columns[7].Visible = true;
            else
                Gv_Reports.Columns[7].Visible = false;

            if (chkunit.Checked == true)
                Gv_Reports.Columns[8].Visible = true;
            else
                Gv_Reports.Columns[8].Visible = false;

            if (chkState.Checked == true)
                Gv_Reports.Columns[9].Visible = true;
            else
                Gv_Reports.Columns[9].Visible = false;

            if (chkPVC.Checked == true)
                Gv_Reports.Columns[10].Visible = true;
            else
                Gv_Reports.Columns[10].Visible = false;

            if (chkPVCvalidity.Checked == true)
                Gv_Reports.Columns[11].Visible = true;
            else
                Gv_Reports.Columns[11].Visible = false;

            if (chkRFIDNo.Checked == true)
                Gv_Reports.Columns[12].Visible = true;
            else
                Gv_Reports.Columns[12].Visible = false;

            if (chkRFIDvalidity.Checked == true)
                Gv_Reports.Columns[13].Visible = true;
            else
                Gv_Reports.Columns[13].Visible = false;

            if (chkCardNo.Checked == true)
                Gv_Reports.Columns[14].Visible = true;
            else
                Gv_Reports.Columns[14].Visible = false;

            if (chkDesignation.Checked == true)
                Gv_Reports.Columns[15].Visible = true;
            else
                Gv_Reports.Columns[15].Visible = false;

            if (chkReligion.Checked == true)
                Gv_Reports.Columns[16].Visible = true;
            else
                Gv_Reports.Columns[16].Visible = false;

            if (chkGender.Checked == true)
                Gv_Reports.Columns[17].Visible = true;
            else
                Gv_Reports.Columns[17].Visible = false;

            if (chkIssueDate.Checked == true)
                Gv_Reports.Columns[18].Visible = true;
            else
                Gv_Reports.Columns[18].Visible = false;

            if (chkCancel.Checked == true)
                Gv_Reports.Columns[19].Visible = true;
            else
                Gv_Reports.Columns[19].Visible = false;

            if (chkCancelReason.Checked == true)
                Gv_Reports.Columns[20].Visible = true;
            else
                Gv_Reports.Columns[20].Visible = false;

            if (chkPassType.Checked == true)
                Gv_Reports.Columns[21].Visible = true;
            else
                Gv_Reports.Columns[21].Visible = false;

            if (chkDateLoss.Checked == true)
                Gv_Reports.Columns[22].Visible = true;
            else
                Gv_Reports.Columns[22].Visible = false;

            if (chkPlaceLoss.Checked == true)
                Gv_Reports.Columns[23].Visible = true;
            else
                Gv_Reports.Columns[23].Visible = false;

            if (chkFine.Checked == true)
                Gv_Reports.Columns[24].Visible = true;
            else
                Gv_Reports.Columns[24].Visible = false;

            if (chkFIR.Checked == true)
                Gv_Reports.Columns[25].Visible = true;
            else
                Gv_Reports.Columns[25].Visible = false;

            if (chkCancelDate.Checked == true)
                Gv_Reports.Columns[26].Visible = true;
            else
                Gv_Reports.Columns[26].Visible = false;

            if (chkBlackList.Checked == true)
                Gv_Reports.Columns[27].Visible = true;
            else
                Gv_Reports.Columns[27].Visible = false;

            if (chkICardNo.Checked == true)
                Gv_Reports.Columns[28].Visible = true;
            else
                Gv_Reports.Columns[28].Visible = false;

            if (chkPsuUnit.Checked == true)
                Gv_Reports.Columns[29].Visible = true;
            else
                Gv_Reports.Columns[29].Visible = false;

            if (chkUnitEmploee.Checked == true)
                Gv_Reports.Columns[30].Visible = true;
            else
                Gv_Reports.Columns[30].Visible = false;

            if (ChkExpireDate.Checked == true)
                Gv_Reports.Columns[31].Visible = true;
            else
                Gv_Reports.Columns[31].Visible = false;
        }

        protected void chkAll_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAll.Checked == true)
            {
                chkName.Checked = true;

                chkAadhaar.Checked = true;

                chkMobile.Checked = true;

                chkFirmFileNo.Checked = true;

                ChkFirmName.Checked = true;

                chkWONO.Checked = true;

                chkWOValidity.Checked = true;

                chkunit.Checked = true;

                chkState.Checked = true;

                chkPVC.Checked = true;

                chkPVCvalidity.Checked = true;

                chkRFIDNo.Checked = true;

                chkRFIDvalidity.Checked = true;

                chkCardNo.Checked = true;

                chkDesignation.Checked = true;

                chkReligion.Checked = true;

                chkGender.Checked = true;

                chkIssueDate.Checked = true;

                chkCancel.Checked = true;

                chkCancelReason.Checked = true;

                chkPassType.Checked = true;

                chkDateLoss.Checked = true;

                chkPlaceLoss.Checked = true;

                chkFine.Checked = true;

                chkFIR.Checked = true;

                chkCancelDate.Checked = true;

                chkBlackList.Checked = true;

                chkICardNo.Checked = true;

                chkPsuUnit.Checked = true;

                chkUnitEmploee.Checked = true;
            }
            else
            {
                chkName.Checked = false;

                chkAadhaar.Checked = false;

                chkMobile.Checked = false;

                chkFirmFileNo.Checked = false;

                ChkFirmName.Checked = false;

                chkWONO.Checked = false;

                chkWOValidity.Checked = false;

                chkunit.Checked = false;

                chkState.Checked = false;

                chkPVC.Checked = false;

                chkPVCvalidity.Checked = false;

                chkRFIDNo.Checked = false;

                chkRFIDvalidity.Checked = false;

                chkCardNo.Checked = false;

                chkDesignation.Checked = false;

                chkReligion.Checked = false;

                chkGender.Checked = false;

                chkIssueDate.Checked = false;

                chkCancel.Checked = false;

                chkCancelReason.Checked = false;

                chkPassType.Checked = false;

                chkDateLoss.Checked = false;

                chkPlaceLoss.Checked = false;

                chkFine.Checked = false;

                chkFIR.Checked = false;

                chkCancelDate.Checked = false;

                chkBlackList.Checked = false;

                chkICardNo.Checked = false;

                chkPsuUnit.Checked = false;

                chkUnitEmploee.Checked = false;
            }
        }

        protected void Gv_Reports_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            Gv_Reports.PageIndex = e.NewPageIndex;
            Gv_Reports.DataSource = Session["table1"];
            Gv_Reports.DataBind();
        }

        protected void btnExportToExcel_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Clear();
                Response.Buffer = true;
                Response.AddHeader("content-disposition", "attachment;filename=GridViewExport.xls");
                Response.Charset = "";
                Response.ContentType = "application/vnd.ms-excel";
                using (StringWriter sw = new StringWriter())
                {
                    HtmlTextWriter hw = new HtmlTextWriter(sw);
                    //To Export all pages
                   if (ddlselectReport.SelectedIndex.ToString()=="7" || ddlselectReport.SelectedIndex.ToString()=="9"  || ddlselectReport.SelectedIndex.ToString()=="10"  ) // if (ddlselectReport.SelectedItem.Text == "Total In Out Report" || ddlselectReport.SelectedItem.Text == "In Out Details Report" || ddlselectReport.SelectedItem.Text == "Today's Pending Out Report")
                    {
                        Gv_Demo.AllowPaging = false;
                        Gv_Demo.DataSource = Session["table1"];
                        Gv_Demo.DataBind();

                        Gv_Demo.HeaderRow.BackColor = Color.White;
                        foreach (TableCell cell in Gv_Demo.HeaderRow.Cells)
                        {
                            cell.BackColor = Gv_Demo.HeaderStyle.BackColor;
                        }
                        foreach (GridViewRow row in Gv_Demo.Rows)
                        {
                            row.BackColor = Color.White;
                            foreach (TableCell cell in row.Cells)
                            {
                                if (row.RowIndex % 2 == 0)
                                {
                                    cell.BackColor = Gv_Demo.AlternatingRowStyle.BackColor;
                                }
                                else
                                {
                                    cell.BackColor = Gv_Demo.RowStyle.BackColor;
                                }
                                cell.CssClass = "textmode";
                            }
                        }

                        Gv_Demo.RenderControl(hw);
                        //style to format numbers to string
                        string style = @"<style> .textmode { } </style>";
                        Response.Write(style);
                        Response.Output.Write(sw.ToString());
                        Response.Flush();
                        Response.End();
                    }
                    else
                    {
                        Gv_Reports.AllowPaging = false;
                        Gv_Reports.DataSource = Session["table1"];
                        Gv_Reports.DataBind();

                        Gv_Reports.HeaderRow.BackColor = Color.White;
                        foreach (TableCell cell in Gv_Reports.HeaderRow.Cells)
                        {
                            cell.BackColor = Gv_Reports.HeaderStyle.BackColor;
                        }
                        foreach (GridViewRow row in Gv_Reports.Rows)
                        {
                            row.BackColor = Color.White;
                            foreach (TableCell cell in row.Cells)
                            {
                                if (row.RowIndex % 2 == 0)
                                {
                                    cell.BackColor = Gv_Reports.AlternatingRowStyle.BackColor;
                                }
                                else
                                {
                                    cell.BackColor = Gv_Reports.RowStyle.BackColor;
                                }
                                cell.CssClass = "textmode";
                            }
                        }

                        Gv_Reports.RenderControl(hw);
                        //style to format numbers to string
                        string style = @"<style> .textmode { } </style>";
                        Response.Write(style);
                        Response.Output.Write(sw.ToString());
                        Response.Flush();
                        Response.End();
                    }
                }
            }
            catch (Exception)
            {
                Response.Write("<script> alert('Please select any report.')</script>");
            }
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Confirms that an HtmlForm control is rendered for the specified ASP.NET
               server control at run time. */
        }

        protected void Gv_Reports_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    //if (e.Row.Cells[22].Text == "01/01/1900")
                    //{
                    //    e.Row.Cells[22].Text = "";
                    //    e.Row.Cells[23].Text = "";
                    //}
                    //if (e.Row.Cells[26].Text == "01/01/1900")
                    //    e.Row.Cells[26].Text = "";

                    GridVisibility();

                  
                    foreach (TableCell cell in e.Row.Cells)
                    {
                        Label lblcancelreason = (Label)e.Row.FindControl("lblcancelstatus");
                        Label lblValidity = (Label)e.Row.FindControl("lblValidity");
                        DateTime ValidityDate = Convert.ToDateTime(lblValidity.Text);

                        Label lbldateloss1 = (Label)e.Row.FindControl("lbldateloss");
                        Label lblplaceloss1 = (Label)e.Row.FindControl("lblplaceloss");
                        Label lblcanceldate1 = (Label)e.Row.FindControl("lblcanceldate");

                        if (lbldateloss1.Text == "01/01/1900 00:00:00" || lbldateloss1.Text == "01/01/1900")
                        {
                            lbldateloss1.Text = "";
                            lblplaceloss1.Text = "";
                        }
                        if (lblcanceldate1.Text == "01/01/1900 00:00:00" || lblcanceldate1.Text == "01/01/1900")
                        {
                            lblcanceldate1.Text = "";

                        }

                        if (ddlselectReport.SelectedItem.Text == "Expired But Not Renewed")
                        {
                            if (lblcancelreason.Text.Trim() == "LOSS")
                            {
                            }
                            else
                            {
                                lblcancelreason.Text = "RENEWAL PENDING / EXPIRED";
                            }
                        }
                        else
                        {

                            if (ValidityDate > System.DateTime.Now)
                            {
                                if (lblcancelreason.Text.Trim() == "LOSS")
                                {
                                    lblcancelreason.Text = "LOSS";
                                }
                                else if (lblcancelreason.Text.Trim() == "CANCEL")
                                {
                                    lblcancelreason.Text = "CANCEL";
                                }
                                else
                                {
                                    lblcancelreason.Text = "VALID CARDS";
                                }
                                //lblcancelreason.BackColor = System.Drawing.Color.Green;
                            }
                            else
                            {
                                if (lblcancelreason.Text.Trim() == "LOSS")
                                {
                                }
                                else
                                {
                                    lblcancelreason.Text = "RENEWAL PENDING / EXPIRED";
                                    //lblcancelreason.ForeColor = System.Drawing.Color.Red;
                                    //Gv_Reports.Columns[31].ControlStyle.ForeColor = System.Drawing.Color.Red;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
                // Response.Write("<script> alert('.')</script>");
            }
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            try
            {
                ddlFirm.ClearSelection();
                ddl.BindFirmddl1(ref ddlFirm);
                ddlsearch.ClearSelection();
                ddlselectReport.ClearSelection();
                txtfromdate.Text = System.DateTime.Now.ToString();
                txttoadte.Text = System.DateTime.Now.ToString();
                txtSearch.Text = "";
                //Gv_Demo.DataBind = null;
                //Gv_Demo
                Gv_Demo.DataSource = null;
                Gv_Demo.DataBind();

                Gv_Reports.DataSource = null;
                Gv_Reports.DataBind();
            }
            catch (Exception)
            {
                Response.Write("<script> alert('.')</script>");
            }
        }

        public void HideControls()
        {

            ChkExpireDate.Visible = false;

            chkName.Visible = false;

            chkAadhaar.Visible = false;

            chkMobile.Visible = false;

            chkFirmFileNo.Visible = false;

            ChkFirmName.Visible = false;

            chkWONO.Visible = false;

            chkWOValidity.Visible = false;

            chkunit.Visible = false;

            chkState.Visible = false;

            chkPVC.Visible = false;

            chkPVCvalidity.Visible = false;

            chkRFIDNo.Visible = false;

            chkRFIDvalidity.Visible = false;

            chkCardNo.Visible = false;

            chkDesignation.Visible = false;

            chkReligion.Visible = false;

            chkGender.Visible = false;

            chkIssueDate.Visible = false;

            chkCancel.Visible = false;

            chkCancelReason.Visible = false;

            chkPassType.Visible = false;

            chkDateLoss.Visible = false;

            chkPlaceLoss.Visible = false;

            chkFine.Visible = false;

            chkFIR.Visible = false;

            chkCancelDate.Visible = false;

            chkBlackList.Visible = false;

            chkICardNo.Visible = false;

            chkPsuUnit.Visible = false;

            chkUnitEmploee.Visible = false;

            chkAll.Visible = false;

            DivSelectColumn.Visible = false;
        }

        public void ShowControls()
        {
            ChkExpireDate.Visible = true;

            chkName.Visible = true;

            chkAadhaar.Visible = true;

            chkMobile.Visible = true;

            chkFirmFileNo.Visible = true;

            ChkFirmName.Visible = true;

            chkWONO.Visible = true;

            chkWOValidity.Visible = true;

            chkunit.Visible = true;

            chkState.Visible = true;

            chkPVC.Visible = true;

            chkPVCvalidity.Visible = true;

            chkRFIDNo.Visible = true;

            chkRFIDvalidity.Visible = true;

            chkCardNo.Visible = true;

            chkDesignation.Visible = true;

            chkReligion.Visible = true;

            chkGender.Visible = true;

            chkIssueDate.Visible = true;

            chkCancel.Visible = true;

            chkCancelReason.Visible = true;

            chkPassType.Visible = true;

            chkDateLoss.Visible = true;

            chkPlaceLoss.Visible = true;

            chkFine.Visible = true;

            chkFIR.Visible = true;

            chkCancelDate.Visible = true;

            chkBlackList.Visible = true;

            chkICardNo.Visible = true;

            chkPsuUnit.Visible = true;

            chkUnitEmploee.Visible = true;

            chkAll.Visible = true;

            DivSelectColumn.Visible = true;
        }

        protected void ddlselectReport_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                lblCount1.Visible = false;
                lblCount2.Visible = false;
                lblCount1.InnerText = string.Empty;
                lblCount2.InnerText = string.Empty;

                lblCount.InnerText = string.Empty;
                //if (ddlselectReport.SelectedItem.Value != "10")
                //{
                ddlRelegion.Visible = false;
                lblRelegion.Visible = false;
                lblRelegion.Text = "";
                    if (ddlselectReport.SelectedItem.Text.Trim() == "Religion Wise Report")
                    {
                        chkReligion.Checked = true;
                        ddl.BindReligionddl1(ref ddlRelegion);
                        ddlRelegion.Visible = true;
                        lblRelegion.Visible = true;
                        lblRelegion.Text = "SELECT RELIGION :";

                        Gv_Demo.Visible = false;
                        Gv_Demo.DataSource = null;
                    }
                    else if (ddlselectReport.SelectedValue.ToString() == "13")//State Wise Report
                    {
                        chkState.Checked = true;
                        ddl.BindStatedReportl(ref ddlRelegion);
                        ddlRelegion.Visible = true;
                        lblRelegion.Visible = true;
                        lblRelegion.Text = "SELECT STATE :";

                        Gv_Demo.Visible = false;
                        Gv_Demo.DataSource = null;
                    }
                   // if (ddlselectReport.SelectedItem.Text.Trim() == "Total In Out Summary Report" || ddlselectReport.SelectedItem.Text.Trim() == "In Out Details Report" || ddlselectReport.SelectedItem.Text.Trim() == "Today's Pending Out Report")
                    else  if (ddlselectReport.SelectedValue.ToString()=="7"||ddlselectReport.SelectedValue.ToString()=="10"||ddlselectReport.SelectedValue.ToString()=="11"||ddlselectReport.SelectedValue.ToString()=="12")
                    {
                        HideControls();
                        Gv_Demo.Visible = true;
                    }
                    else
                    {
                        ShowControls();
                        Gv_Demo.Visible = false;
                    }
                //}
                //else
                //{
                //    Response.Redirect("ReportTemplateCardStatus.aspx", false);
                //}

                Gv_Reports.Visible = false;
                Gv_Demo.Visible = false;
                Gv_Demo.DataSource = null;
                Gv_Demo.DataBind();
                Gv_Reports.DataSource = null;
                Gv_Reports.DataBind();
            }
            catch (Exception)
            {
                Response.Write("<script> alert('Select any Report.')</script>");
            }
        }

        public void GetReportDataForFirmWise(int FirmId)
        {
            var table1 = (from c in DVSC.CONTRACTOR_DETAIL
                          join d in DVSC.DESIGNATION_MASTER on c.Cont_DesignationID equals d.DESIGNATION_ID
                          join s in DVSC.STATE_MASTER on c.Cont_StateID equals s.STATE_ID
                          join f in DVSC.FIRMMASTERs on c.Cont_FirmID equals f.FIRM_ID
                          join r in DVSC.RELIGION_MASTER on c.Cont_ReligionID equals r.R_ID
                          join n in DVSC.COUNTRY_MASTER on c.Cont_NationalityID equals n.COUNTRY_ID
                          join cr in DVSC.CANCEL_REASON_MASTER on c.Cont_CancelReason equals cr.CR_ID
                          join u in DVSC.PSU_MASTER on c.Cont_PSUunitID equals u.PSU_ID
                          join sh in DVSC.SHOP_MASTER on c.Cont_ShopId equals sh.SHOP_ID
                          where (c.Cont_CreatedDate >= fromdate && (c.Cont_CreatedDate <= todate)) && ((c.Cont_FirmID == FirmId))
                          orderby c.Cont_ReligionID ascending
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
                Gv_Reports.DataSource = table1;
                Gv_Reports.DataBind();
                Session["table1"] = table1;
            }
            else
            {
                Gv_Reports.DataSource = null;
                Gv_Reports.DataBind();
                Response.Write("<script> alert('No data found')</script>");
            }
        }

        protected void btnSearch1_Click(object sender, EventArgs e)
        {
            //Response.Write("<script> alert('Search button clicked')</script>");
            //Response.Write("<script> alert('Start')</script>");
           
                ReportData();
        }
        //private bool IsValidReport()
        //{

        //}
        public void ReportData()
        {
            ReligionReport = false;
            StateReport = false;
            lblCount.InnerText = string.Empty;
            lblCount.InnerText = "";
            lblCount1.InnerText = "";
            lblCount2.InnerText = "";
            var From = (from x in DVSC.CONTRACTOR_DETAIL select x.Cont_CreatedDate).Min();//Convert.ToDateTime(txtfromdate.Text);
            var To = (from x in DVSC.CONTRACTOR_DETAIL select x.Cont_CreatedDate).Max();
            if (txtfromdate.Text.Trim() != "")
            {
                fromdate = Convert.ToDateTime(txtfromdate.Text);
                fromdate.ToString("dd/MM/yyyy");
                FlgFromDate = true;
            }
            else
            {
                fromdate = Convert.ToDateTime(From);
                fromdate.ToString("dd/MM/yyyy");
                FlgFromDate = true;
            }
            if (txttoadte.Text.Trim() != "")
            {
                todate = Convert.ToDateTime(txttoadte.Text);
                todate.ToString("dd/MM/yyyy");
                FlgTodate = true;
            }
            else
            {
                todate = Convert.ToDateTime(To);// Convert.ToDateTime(txttoadte.Text);
                todate.ToString("dd/MM/yyyy");
                FlgTodate = true;
            }
            if (ddlFirm.SelectedItem.Text == "--ALL--")
            {
                firmid = 0;
            }
            else
            {

                firmid = Convert.ToInt32(ddlFirm.SelectedValue);
            }

            if (ddlselectReport.SelectedValue == "7")
            {
                if (ddlRelegion.SelectedItem.Text == "--ALL--")
                {
                    Religion = 0;
                }
                else
                {
                    Religion = Convert.ToInt32(ddlRelegion.SelectedValue);
                }
                if ((firmid == 0) && (Religion == 0))
               
                {
                    ReligionReport = true;
                }
            }
            else  if (ddlselectReport.SelectedValue == "13")
                {
                    if (ddlRelegion.SelectedItem.Text == "--ALL--")
                    {
                        State = 0;
                    }
                    else
                    {
                        State = Convert.ToInt32(ddlRelegion.SelectedValue);
                    }
                    if ((firmid == 0) && (State == 0))
                    {
 
                        StateReport = true;
                    }
                }
            Session["ReportName"] = ddlselectReport.SelectedItem.Text.Trim();

            try
            {
                if (FlgFromDate == true && FlgTodate == true)
                {
                    if (ddlselectReport.SelectedIndex > 0)
                    {
                        if (ddlsearch.SelectedItem.Text == "DOCKYARD ID NO")
                        {
                            Dockyardid = Convert.ToInt32(txtSearch.Text.Trim());
                            //Response.Write("<script> alert('Dockyard ID " + Dockyardid + "')</script>");
                        }
                        int ReportId = Convert.ToInt32(ddlselectReport.SelectedValue);
                        switch (ReportId)
                        {
                            case 1:
                                // Individual report
                                Report1();
                                break;
                            case 2:
                                //Loss Report
                                Report2();
                                break;
                            case 3:
                                //Total Issue Report
                                Report3();
                                break;
                            case 4:
                                //Total Cancel Report
                                Report4();
                                break;
                            case 5:
                                //Total Valid Report
                                Report5();
                                break;
                            case 6:
                                //Expired But Not Renewed
                                Report6();
                                break;
                            case 7:
                                //Total In Out Report
                                Report7();
                                break;
                            case 8:
                                //Religion Wise Report
                                Report8();
                                break;
                            case 9:
                                //In Out Details Report
                                Report9();
                                break;
                            case 10:
                                //Today's Pending Out Report
                                Report10();
                                break;
                            case 11:
                                //Tiger Gate
                                Report11();
                                break;
                            case 12:
                                //Muster Gate
                                Report12();
                                break;
                            case 13:
                                //State Wise Report
                                Report13();
                                break;
                            default:
                                Response.Write("<script> alert('Select report')</script>");
                                break;
                        }
                        //Response.Write("<script> alert('3')</script>");
                        GridVisibility();
                        //Response.Write("<script> alert('2')</script>");
                    }
                }
            }
            catch (Exception)
            {
                Response.Write("<script> alert('Error while getting data')</script>");
            }
        }

        public void Report1()
        {
            PassType = GetPassType();
            pass = PassType.Split(',').ToList();
            // Individual report
            #region
            if (firmid == 0)
            {
                //Reeport  as per all firm
                if (ddlsearch.SelectedItem.Text == "--SELECT--")
                {
                    var table1 = (from c in DVSC.CONTRACTOR_DETAIL
                                  join d in DVSC.DESIGNATION_MASTER on c.Cont_DesignationID equals d.DESIGNATION_ID
                                  join s in DVSC.STATE_MASTER on c.Cont_StateID equals s.STATE_ID
                                  join f in DVSC.FIRMMASTERs on c.Cont_FirmID equals f.FIRM_ID
                                  join r in DVSC.RELIGION_MASTER on c.Cont_ReligionID equals r.R_ID
                                  join n in DVSC.COUNTRY_MASTER on c.Cont_NationalityID equals n.COUNTRY_ID
                                  join cr in DVSC.CANCEL_REASON_MASTER on c.Cont_CancelReason equals cr.CR_ID
                                  join u in DVSC.PSU_MASTER on c.Cont_PSUunitID equals u.PSU_ID
                                  join sh in DVSC.SHOP_MASTER on c.Cont_ShopId equals sh.SHOP_ID
                                  where (c.Cont_CreatedDate >= fromdate && (c.Cont_CreatedDate <= todate) && pass.Contains(c.Cont_PassType))
                                  orderby c.Cont_Id, c.Cont_CreatedDate ascending
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
                                      c.Cont_UnitEmp,
                                      c.Cont_MinDate,
                                      c.Cont_DocID
                                  }).ToList();
                    if (table1.Count > 0)
                    {
                        Gv_Reports.DataSource = table1;
                        Gv_Reports.DataBind();
                        Session["table1"] = table1;
                        Session["table1Count"] = table1.Count;
                    }
                    else
                    {
                        Gv_Reports.DataSource = null;
                        Gv_Reports.DataBind();
                        Response.Write("<script> alert('No data found')</script>");
                    }
                }
                else
                {
                    //report For all Firm + Other ID search Record
                    var table1 = (from c in DVSC.CONTRACTOR_DETAIL
                                  join d in DVSC.DESIGNATION_MASTER on c.Cont_DesignationID equals d.DESIGNATION_ID
                                  join s in DVSC.STATE_MASTER on c.Cont_StateID equals s.STATE_ID
                                  join f in DVSC.FIRMMASTERs on c.Cont_FirmID equals f.FIRM_ID
                                  join r in DVSC.RELIGION_MASTER on c.Cont_ReligionID equals r.R_ID
                                  join n in DVSC.COUNTRY_MASTER on c.Cont_NationalityID equals n.COUNTRY_ID
                                  join cr in DVSC.CANCEL_REASON_MASTER on c.Cont_CancelReason equals cr.CR_ID
                                  join u in DVSC.PSU_MASTER on c.Cont_PSUunitID equals u.PSU_ID
                                  join sh in DVSC.SHOP_MASTER on c.Cont_ShopId equals sh.SHOP_ID
                                  where ((c.Cont_Aadhaar == (txtSearch.Text.Trim()))
                            || (c.Cont_CardNo == (txtSearch.Text.Trim()))
                            || (c.Cont_Mobile == (txtSearch.Text.Trim())) ||
                            (c.Cont_RFIDNo == (txtSearch.Text.Trim())) ||
                            (c.Cont_Name.Contains(txtSearch.Text.Trim())) || (f.FIRM_NAME.Contains(txtSearch.Text.Trim())) ||
                            (c.Cont_DocID == Dockyardid)) && (c.Cont_CreatedDate >= fromdate && (c.Cont_CreatedDate <= todate)) && pass.Contains(c.Cont_PassType)
                                  orderby c.Cont_Id ascending
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
                                      c.Cont_UnitEmp,
                                      c.Cont_MinDate,
                                      c.Cont_DocID
                                  }).ToList();
                    if (table1.Count > 0)
                    {
                        Gv_Reports.DataSource = table1;
                        Gv_Reports.DataBind();
                        Session["table1"] = table1;
                        Session["table1Count"] = table1.Count;
                    }
                    else
                    {
                        Gv_Reports.DataSource = null;
                        Gv_Reports.DataBind();
                        Response.Write("<script> alert('No data found')</script>");
                    }
                }
            }
            else
            {
                // for firm master
                //Report for single Firm 
                if (ddlsearch.SelectedItem.Text == "--SELECT--")
                {
                    if (rdoShips.Checked == false)
                    {
                        var table1 = (from c in DVSC.CONTRACTOR_DETAIL
                                      join d in DVSC.DESIGNATION_MASTER on c.Cont_DesignationID equals d.DESIGNATION_ID
                                      join s in DVSC.STATE_MASTER on c.Cont_StateID equals s.STATE_ID
                                      join f in DVSC.FIRMMASTERs on c.Cont_FirmID equals f.FIRM_ID
                                      join r in DVSC.RELIGION_MASTER on c.Cont_ReligionID equals r.R_ID
                                      join n in DVSC.COUNTRY_MASTER on c.Cont_NationalityID equals n.COUNTRY_ID
                                      join cr in DVSC.CANCEL_REASON_MASTER on c.Cont_CancelReason equals cr.CR_ID
                                      join u in DVSC.PSU_MASTER on c.Cont_PSUunitID equals u.PSU_ID
                                      join sh in DVSC.SHOP_MASTER on c.Cont_ShopId equals sh.SHOP_ID
                                      where (c.Cont_CreatedDate >= fromdate && (c.Cont_CreatedDate <= todate))
                                      && (c.Cont_FirmID == firmid) && pass.Contains(c.Cont_PassType)
                                      orderby c.Cont_Id ascending
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
                                          c.Cont_UnitEmp,
                                          c.Cont_MinDate,
                                          c.Cont_DocID
                                      }).ToList();

                        if (table1.Count > 0)
                        {
                            Gv_Reports.DataSource = table1;
                            Gv_Reports.DataBind();
                            Session["table1"] = table1;
                            Session["table1Count"] = table1.Count;
                        }
                        else
                        {
                            Gv_Reports.DataSource = null;
                            Gv_Reports.DataBind();
                            Response.Write("<script> alert('No data found')</script>");
                        }
                    }
                    else if (rdoShips.Checked == true)
                    {                        
                        var table1 = (from c in DVSC.CONTRACTOR_DETAIL
                                      join d in DVSC.DESIGNATION_MASTER on c.Cont_DesignationID equals d.DESIGNATION_ID
                                      join s in DVSC.STATE_MASTER on c.Cont_StateID equals s.STATE_ID
                                      join f in DVSC.FIRMMASTERs on c.Cont_FirmID equals f.FIRM_ID
                                      join r in DVSC.RELIGION_MASTER on c.Cont_ReligionID equals r.R_ID
                                      join n in DVSC.COUNTRY_MASTER on c.Cont_NationalityID equals n.COUNTRY_ID
                                      join cr in DVSC.CANCEL_REASON_MASTER on c.Cont_CancelReason equals cr.CR_ID
                                      join u in DVSC.PSU_MASTER on c.Cont_PSUunitID equals u.PSU_ID
                                      join sh in DVSC.SHOP_MASTER on c.Cont_ShopId equals sh.SHOP_ID
                                      where (c.Cont_CreatedDate >= fromdate && (c.Cont_CreatedDate <= todate))
                                      && (c.Cont_ShopId == firmid) && pass.Contains(c.Cont_PassType)
                                      orderby c.Cont_Id ascending
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
                                          Cont_Unit= sh.SHOP_NAME,
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
                                          c.Cont_UnitEmp,
                                          c.Cont_MinDate,
                                          c.Cont_DocID
                                      }).ToList();

                        if (table1.Count > 0)
                        {
                            Gv_Reports.DataSource = table1;
                            Gv_Reports.DataBind();
                            Session["table1"] = table1;
                            Session["table1Count"] = table1.Count;
                        }
                        else
                        {
                            Gv_Reports.DataSource = null;
                            Gv_Reports.DataBind();
                            Response.Write("<script> alert('No data found')</script>");
                        }
                    }
                }
                else
                {
                    if (rdoShips.Checked == false)
                    {
                        var table1 = (from c in DVSC.CONTRACTOR_DETAIL
                                      join d in DVSC.DESIGNATION_MASTER on c.Cont_DesignationID equals d.DESIGNATION_ID
                                      join s in DVSC.STATE_MASTER on c.Cont_StateID equals s.STATE_ID
                                      join f in DVSC.FIRMMASTERs on c.Cont_FirmID equals f.FIRM_ID
                                      join r in DVSC.RELIGION_MASTER on c.Cont_ReligionID equals r.R_ID
                                      join n in DVSC.COUNTRY_MASTER on c.Cont_NationalityID equals n.COUNTRY_ID
                                      join cr in DVSC.CANCEL_REASON_MASTER on c.Cont_CancelReason equals cr.CR_ID
                                      join u in DVSC.PSU_MASTER on c.Cont_PSUunitID equals u.PSU_ID
                                      join sh in DVSC.SHOP_MASTER on c.Cont_ShopId equals sh.SHOP_ID
                                      where ((c.Cont_Aadhaar == (txtSearch.Text.Trim()))
                                    || (c.Cont_CardNo == (txtSearch.Text.Trim()))
                                    || (c.Cont_Mobile == (txtSearch.Text.Trim())) ||
                                    (c.Cont_RFIDNo == (txtSearch.Text.Trim())) ||
                                    (c.Cont_Name.Contains(txtSearch.Text.Trim())) || (f.FIRM_NAME.Contains(txtSearch.Text.Trim())) ||
                                    (c.Cont_DocID == Dockyardid)) && (c.Cont_CreatedDate >= fromdate
                                    && (c.Cont_CreatedDate <= todate)) && pass.Contains(c.Cont_PassType)
                                    && (c.Cont_FirmID == firmid)
                                      orderby c.Cont_Id ascending
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
                                          c.Cont_UnitEmp,
                                          c.Cont_MinDate,
                                          c.Cont_DocID
                                      }).ToList();
                        if (table1.Count > 0)
                        {
                            Gv_Reports.DataSource = table1;
                            Gv_Reports.DataBind();
                            Session["table1"] = table1;
                            Session["table1Count"] = table1.Count;
                        }
                        else
                        {
                            Gv_Reports.DataSource = null;
                            Gv_Reports.DataBind();
                            Response.Write("<script> alert('No data found')</script>");
                        }
                    }
                    else if (rdoShips.Checked == true)
                    {
                        var table1 = (from c in DVSC.CONTRACTOR_DETAIL
                                      join d in DVSC.DESIGNATION_MASTER on c.Cont_DesignationID equals d.DESIGNATION_ID
                                      join s in DVSC.STATE_MASTER on c.Cont_StateID equals s.STATE_ID
                                      join f in DVSC.FIRMMASTERs on c.Cont_FirmID equals f.FIRM_ID
                                      join r in DVSC.RELIGION_MASTER on c.Cont_ReligionID equals r.R_ID
                                      join n in DVSC.COUNTRY_MASTER on c.Cont_NationalityID equals n.COUNTRY_ID
                                      join cr in DVSC.CANCEL_REASON_MASTER on c.Cont_CancelReason equals cr.CR_ID
                                      join u in DVSC.PSU_MASTER on c.Cont_PSUunitID equals u.PSU_ID
                                      join sh in DVSC.SHOP_MASTER on c.Cont_ShopId equals sh.SHOP_ID
                                      where ((c.Cont_Aadhaar == (txtSearch.Text.Trim()))
                                        || (c.Cont_CardNo == (txtSearch.Text.Trim()))
                                        || (c.Cont_Mobile == (txtSearch.Text.Trim())) ||
                                        (c.Cont_RFIDNo == (txtSearch.Text.Trim())) ||
                                        (c.Cont_Name.Contains(txtSearch.Text.Trim())) 
                                        || (f.FIRM_NAME.Contains(txtSearch.Text.Trim()))
                                        || (c.Cont_DocID == Dockyardid)) 
                                        && (c.Cont_CreatedDate >= fromdate
                                        && (c.Cont_CreatedDate <= todate)) && pass.Contains(c.Cont_PassType)
                                        && (c.Cont_ShopId == firmid)
                                      orderby c.Cont_Id ascending
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
                                          Cont_Unit = sh.SHOP_NAME,
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
                                          c.Cont_UnitEmp,
                                          c.Cont_MinDate,
                                          c.Cont_DocID
                                      }).ToList();

                        if (table1.Count > 0)
                        {
                            Gv_Reports.DataSource = table1;
                            Gv_Reports.DataBind();
                            Session["table1"] = table1;
                            Session["table1Count"] = table1.Count;
                        }
                        else
                        {
                            Gv_Reports.DataSource = null;
                            Gv_Reports.DataBind();
                            Response.Write("<script> alert('No data found')</script>");
                        }
                    }
                      
                }
            }
            #endregion
            Gv_Reports.Visible = true;
            Gv_Demo.Visible = false;
            if (rdoAll.Checked == true)
            {
                lblCount.InnerText = "ALL PASS COUNT" + " :" + Session["table1Count"].ToString();
            }
            else
            {
                lblCount.InnerText = PassType + "  PASS COUNT :" + Session["table1Count"].ToString();
            }
        }

        public void Report2()
        {
            PassType = GetPassType();
            pass = PassType.Split(',').ToList();
            //Loss Report
            #region
            if (firmid == 0)
            {
                //Reeport  as per all firm
                if (ddlsearch.SelectedItem.Text == "--SELECT--")
                {
                    var table1 = (from c in DVSC.CONTRACTOR_DETAIL
                                  join d in DVSC.DESIGNATION_MASTER on c.Cont_DesignationID equals d.DESIGNATION_ID
                                  join s in DVSC.STATE_MASTER on c.Cont_StateID equals s.STATE_ID
                                  join f in DVSC.FIRMMASTERs on c.Cont_FirmID equals f.FIRM_ID
                                  join r in DVSC.RELIGION_MASTER on c.Cont_ReligionID equals r.R_ID
                                  join n in DVSC.COUNTRY_MASTER on c.Cont_NationalityID equals n.COUNTRY_ID
                                  join cr in DVSC.CANCEL_REASON_MASTER on c.Cont_CancelReason equals cr.CR_ID
                                  join u in DVSC.PSU_MASTER on c.Cont_PSUunitID equals u.PSU_ID
                                  join sh in DVSC.SHOP_MASTER on c.Cont_ShopId equals sh.SHOP_ID
                                  where (c.Cont_CreatedDate >= fromdate && (c.Cont_CreatedDate <= todate)) && (c.Cont_CancelFLag == "LOSS") && pass.Contains(c.Cont_PassType)
                                  orderby c.Cont_Id ascending
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
                                      c.Cont_UnitEmp,
                                      c.Cont_MinDate,
                                      c.Cont_DocID
                                  }).ToList();
                    if (table1.Count > 0)
                    {
                        Gv_Reports.DataSource = table1;
                        Gv_Reports.DataBind();
                        Session["table1"] = table1;
                        Session["table1Count"] = table1.Count;
                    }
                    else
                    {
                        Gv_Reports.DataSource = null;
                        Gv_Reports.DataBind();
                        Response.Write("<script> alert('No data found')</script>");
                    }
                }
                else
                {
                    //report For all Firm + Other ID search Record
                    var table1 = (from c in DVSC.CONTRACTOR_DETAIL
                                  join d in DVSC.DESIGNATION_MASTER on c.Cont_DesignationID equals d.DESIGNATION_ID
                                  join s in DVSC.STATE_MASTER on c.Cont_StateID equals s.STATE_ID
                                  join f in DVSC.FIRMMASTERs on c.Cont_FirmID equals f.FIRM_ID
                                  join r in DVSC.RELIGION_MASTER on c.Cont_ReligionID equals r.R_ID
                                  join n in DVSC.COUNTRY_MASTER on c.Cont_NationalityID equals n.COUNTRY_ID
                                  join cr in DVSC.CANCEL_REASON_MASTER on c.Cont_CancelReason equals cr.CR_ID
                                  join u in DVSC.PSU_MASTER on c.Cont_PSUunitID equals u.PSU_ID
                                  join sh in DVSC.SHOP_MASTER on c.Cont_ShopId equals sh.SHOP_ID
                                  where ((c.Cont_Aadhaar == (txtSearch.Text.Trim()))
                            || (c.Cont_CardNo == (txtSearch.Text.Trim()))
                            || (c.Cont_Mobile == (txtSearch.Text.Trim())) ||
                            (c.Cont_RFIDNo == (txtSearch.Text.Trim())) ||
                            (c.Cont_Name.Contains(txtSearch.Text.Trim())) || (f.FIRM_NAME.Contains(txtSearch.Text.Trim())) ||
                            (c.Cont_DocID == Dockyardid)) && (c.Cont_CreatedDate >= fromdate && (c.Cont_CreatedDate <= todate)) && (c.Cont_CancelFLag == "LOSS") && pass.Contains(c.Cont_PassType)
                                  orderby c.Cont_Id ascending
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
                                      c.Cont_UnitEmp,
                                      c.Cont_MinDate,
                                      c.Cont_DocID
                                  }).ToList();
                    if (table1.Count > 0)
                    {
                        Gv_Reports.DataSource = table1;
                        Gv_Reports.DataBind();
                        Session["table1"] = table1;
                        Session["table1Count"] = table1.Count;
                    }
                    else
                    {
                        Gv_Reports.DataSource = null;
                        Gv_Reports.DataBind();
                        Response.Write("<script> alert('No data found')</script>");
                    }
                }
            }
            else  //   if (firmid == 0)
            {
                //Report for single Firm 
                if (ddlsearch.SelectedItem.Text == "--SELECT--")
                {
                    if (rdoShips.Checked == false)
                    {
                        var table1 = (from c in DVSC.CONTRACTOR_DETAIL
                                      join d in DVSC.DESIGNATION_MASTER on c.Cont_DesignationID equals d.DESIGNATION_ID
                                      join s in DVSC.STATE_MASTER on c.Cont_StateID equals s.STATE_ID
                                      join f in DVSC.FIRMMASTERs on c.Cont_FirmID equals f.FIRM_ID
                                      join r in DVSC.RELIGION_MASTER on c.Cont_ReligionID equals r.R_ID
                                      join n in DVSC.COUNTRY_MASTER on c.Cont_NationalityID equals n.COUNTRY_ID
                                      join cr in DVSC.CANCEL_REASON_MASTER on c.Cont_CancelReason equals cr.CR_ID
                                      join u in DVSC.PSU_MASTER on c.Cont_PSUunitID equals u.PSU_ID
                                      join sh in DVSC.SHOP_MASTER on c.Cont_ShopId equals sh.SHOP_ID
                                      where (c.Cont_CreatedDate >= fromdate && (c.Cont_CreatedDate <= todate))
                                      && (c.Cont_FirmID == firmid) && (c.Cont_CancelFLag == "LOSS") && pass.Contains(c.Cont_PassType)
                                      orderby c.Cont_Id ascending
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
                                          c.Cont_UnitEmp,
                                          c.Cont_MinDate,
                                          c.Cont_DocID
                                      }).ToList();
                        if (table1.Count > 0)
                        {
                            Gv_Reports.DataSource = table1;
                            Gv_Reports.DataBind();
                            Session["table1"] = table1;
                            Session["table1Count"] = table1.Count;
                        }
                        else
                        {
                            Gv_Reports.DataSource = null;
                            Gv_Reports.DataBind();
                            Response.Write("<script> alert('No data found')</script>");

                        }
                    }
                    else if (rdoShips.Checked == true)
                    {

                        var table1 = (from c in DVSC.CONTRACTOR_DETAIL
                                      join d in DVSC.DESIGNATION_MASTER on c.Cont_DesignationID equals d.DESIGNATION_ID
                                      join s in DVSC.STATE_MASTER on c.Cont_StateID equals s.STATE_ID
                                      join f in DVSC.FIRMMASTERs on c.Cont_FirmID equals f.FIRM_ID
                                      join r in DVSC.RELIGION_MASTER on c.Cont_ReligionID equals r.R_ID
                                      join n in DVSC.COUNTRY_MASTER on c.Cont_NationalityID equals n.COUNTRY_ID
                                      join cr in DVSC.CANCEL_REASON_MASTER on c.Cont_CancelReason equals cr.CR_ID
                                      join u in DVSC.PSU_MASTER on c.Cont_PSUunitID equals u.PSU_ID
                                      join sh in DVSC.SHOP_MASTER on c.Cont_ShopId equals sh.SHOP_ID
                                      where (c.Cont_CreatedDate >= fromdate && (c.Cont_CreatedDate <= todate))
                                      && (c.Cont_ShopId == firmid) && (c.Cont_CancelFLag == "LOSS") && pass.Contains(c.Cont_PassType)
                                      orderby c.Cont_Id ascending
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
                                          Cont_Unit = sh.SHOP_NAME,
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
                                          c.Cont_UnitEmp,
                                          c.Cont_MinDate,
                                          c.Cont_DocID
                                      }).ToList();
                        if (table1.Count > 0)
                        {
                            Gv_Reports.DataSource = table1;
                            Gv_Reports.DataBind();
                            Session["table1"] = table1;
                            Session["table1Count"] = table1.Count;
                        }
                        else
                        {
                            Gv_Reports.DataSource = null;
                            Gv_Reports.DataBind();
                            Response.Write("<script> alert('No data found')</script>");

                        }
                    }
                }//
                else
                {

                   //  Cont_Unit = sh.SHOP_NAME,
                     if (rdoShips.Checked == false)
                     {
                         var table1 = (from c in DVSC.CONTRACTOR_DETAIL
                                       join d in DVSC.DESIGNATION_MASTER on c.Cont_DesignationID equals d.DESIGNATION_ID
                                       join s in DVSC.STATE_MASTER on c.Cont_StateID equals s.STATE_ID
                                       join f in DVSC.FIRMMASTERs on c.Cont_FirmID equals f.FIRM_ID
                                       join r in DVSC.RELIGION_MASTER on c.Cont_ReligionID equals r.R_ID
                                       join n in DVSC.COUNTRY_MASTER on c.Cont_NationalityID equals n.COUNTRY_ID
                                       join cr in DVSC.CANCEL_REASON_MASTER on c.Cont_CancelReason equals cr.CR_ID
                                       join u in DVSC.PSU_MASTER on c.Cont_PSUunitID equals u.PSU_ID
                                       join sh in DVSC.SHOP_MASTER on c.Cont_ShopId equals sh.SHOP_ID
                                       where ((c.Cont_Aadhaar == (txtSearch.Text.Trim()))
                                 || (c.Cont_CardNo == (txtSearch.Text.Trim()))
                                 || (c.Cont_Mobile == (txtSearch.Text.Trim())) ||
                                 (c.Cont_RFIDNo == (txtSearch.Text.Trim())) ||
                                 (c.Cont_Name.Contains(txtSearch.Text.Trim())) || (f.FIRM_NAME.Contains(txtSearch.Text.Trim())) ||
                                 (c.Cont_DocID == Dockyardid)) && (c.Cont_CreatedDate >= fromdate
                                 && (c.Cont_CreatedDate <= todate)) && (c.Cont_FirmID == firmid) && (c.Cont_CancelFLag == "LOSS")
                                 && pass.Contains(c.Cont_PassType)
                                       orderby c.Cont_Id ascending
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
                                           c.Cont_UnitEmp,
                                           c.Cont_MinDate,
                                           c.Cont_DocID
                                       }).ToList();
                         if (table1.Count > 0)
                         {
                             Gv_Reports.DataSource = table1;
                             Gv_Reports.DataBind();
                             Session["table1"] = table1;
                             Session["table1Count"] = table1.Count;
                         }
                         else
                         {
                             Gv_Reports.DataSource = null;
                             Gv_Reports.DataBind();
                             Response.Write("<script> alert('No data found')</script>");
                         }
                     }
                     else if (rdoShips.Checked == true)
                     {
                         var table1 = (from c in DVSC.CONTRACTOR_DETAIL
                                       join d in DVSC.DESIGNATION_MASTER on c.Cont_DesignationID equals d.DESIGNATION_ID
                                       join s in DVSC.STATE_MASTER on c.Cont_StateID equals s.STATE_ID
                                       join f in DVSC.FIRMMASTERs on c.Cont_FirmID equals f.FIRM_ID
                                       join r in DVSC.RELIGION_MASTER on c.Cont_ReligionID equals r.R_ID
                                       join n in DVSC.COUNTRY_MASTER on c.Cont_NationalityID equals n.COUNTRY_ID
                                       join cr in DVSC.CANCEL_REASON_MASTER on c.Cont_CancelReason equals cr.CR_ID
                                       join u in DVSC.PSU_MASTER on c.Cont_PSUunitID equals u.PSU_ID
                                       join sh in DVSC.SHOP_MASTER on c.Cont_ShopId equals sh.SHOP_ID
                                       where ((c.Cont_Aadhaar == (txtSearch.Text.Trim()))
                                 || (c.Cont_CardNo == (txtSearch.Text.Trim()))
                                 || (c.Cont_Mobile == (txtSearch.Text.Trim())) ||
                                 (c.Cont_RFIDNo == (txtSearch.Text.Trim())) ||
                                 (c.Cont_Name.Contains(txtSearch.Text.Trim())) || (sh.SHOP_NAME.Contains(txtSearch.Text.Trim())) ||
                                 (c.Cont_DocID == Dockyardid)) && (c.Cont_CreatedDate >= fromdate
                                 && (c.Cont_CreatedDate <= todate)) && (c.Cont_ShopId == firmid) && (c.Cont_CancelFLag == "LOSS")
                                 && pass.Contains(c.Cont_PassType)
                                       orderby c.Cont_Id ascending
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
                                           Cont_Unit = sh.SHOP_NAME,
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
                                           c.Cont_UnitEmp,
                                           c.Cont_MinDate,
                                           c.Cont_DocID
                                       }).ToList();
                         if (table1.Count > 0)
                         {
                             Gv_Reports.DataSource = table1;
                             Gv_Reports.DataBind();
                             Session["table1"] = table1;
                             Session["table1Count"] = table1.Count;
                         }
                         else
                         {
                             Gv_Reports.DataSource = null;
                             Gv_Reports.DataBind();
                             Response.Write("<script> alert('No data found')</script>");
                         }
                     }
                         
                }
            }
            #endregion
            Gv_Reports.Visible = true;
            Gv_Demo.Visible = false;
            if (rdoAll.Checked == true)
            {
                lblCount.InnerText = "ALL PASS COUNT" + " :" + Session["table1Count"].ToString();
            }
            else
            {
                lblCount.InnerText = PassType + "  PASS COUNT :" + Session["table1Count"].ToString();
            }
        }

        public void Report3() // Total Issue Report
        {
            PassType = GetPassType();
            pass = PassType.Split(',').ToList();
            //Total Issue Report
            #region
            if (firmid == 0)
            {
                //Reeport  as per all firm
                if (ddlsearch.SelectedItem.Text == "--SELECT--")
                {
                    var table1 = (from c in DVSC.CONTRACTOR_DETAIL
                                  join d in DVSC.DESIGNATION_MASTER on c.Cont_DesignationID equals d.DESIGNATION_ID
                                  join s in DVSC.STATE_MASTER on c.Cont_StateID equals s.STATE_ID
                                  join f in DVSC.FIRMMASTERs on c.Cont_FirmID equals f.FIRM_ID
                                  join r in DVSC.RELIGION_MASTER on c.Cont_ReligionID equals r.R_ID
                                  join n in DVSC.COUNTRY_MASTER on c.Cont_NationalityID equals n.COUNTRY_ID
                                  join cr in DVSC.CANCEL_REASON_MASTER on c.Cont_CancelReason equals cr.CR_ID
                                  join u in DVSC.PSU_MASTER on c.Cont_PSUunitID equals u.PSU_ID
                                  join sh in DVSC.SHOP_MASTER on c.Cont_ShopId equals sh.SHOP_ID
                                  where (c.Cont_CreatedDate >= fromdate && (c.Cont_CreatedDate <= todate)) && pass.Contains(c.Cont_PassType)
                                  orderby c.Cont_Id ascending
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
                                      c.Cont_UnitEmp,
                                      c.Cont_MinDate,
                                      c.Cont_DocID
                                  }).ToList();
                    if (table1.Count > 0)
                    {
                        Gv_Reports.DataSource = table1;
                        Gv_Reports.DataBind();
                        Session["table1"] = table1;
                        Session["table1Count"] = table1.Count;
                    }
                    else
                    {
                        Gv_Reports.DataSource = null;
                        Gv_Reports.DataBind();
                        Response.Write("<script> alert('No data found')</script>");
                    }
                }
                else
                {
                    //report For all Firm + Other ID search Record
                    var table1 = (from c in DVSC.CONTRACTOR_DETAIL
                                  join d in DVSC.DESIGNATION_MASTER on c.Cont_DesignationID equals d.DESIGNATION_ID
                                  join s in DVSC.STATE_MASTER on c.Cont_StateID equals s.STATE_ID
                                  join f in DVSC.FIRMMASTERs on c.Cont_FirmID equals f.FIRM_ID
                                  join r in DVSC.RELIGION_MASTER on c.Cont_ReligionID equals r.R_ID
                                  join n in DVSC.COUNTRY_MASTER on c.Cont_NationalityID equals n.COUNTRY_ID
                                  join cr in DVSC.CANCEL_REASON_MASTER on c.Cont_CancelReason equals cr.CR_ID
                                  join u in DVSC.PSU_MASTER on c.Cont_PSUunitID equals u.PSU_ID
                                  join sh in DVSC.SHOP_MASTER on c.Cont_ShopId equals sh.SHOP_ID
                                  where ((c.Cont_Aadhaar == (txtSearch.Text.Trim()))
                            || (c.Cont_CardNo == (txtSearch.Text.Trim()))
                            || (c.Cont_Mobile == (txtSearch.Text.Trim())) ||
                            (c.Cont_RFIDNo == (txtSearch.Text.Trim())) ||
                            (c.Cont_Name.Contains(txtSearch.Text.Trim())) || (f.FIRM_NAME.Contains(txtSearch.Text.Trim())) ||
                            (c.Cont_DocID == Dockyardid)) && (c.Cont_CreatedDate >= fromdate && (c.Cont_CreatedDate <= todate)) && pass.Contains(c.Cont_PassType)
                                  orderby c.Cont_Id ascending
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
                                      c.Cont_UnitEmp,
                                      c.Cont_MinDate,
                                      c.Cont_DocID
                                  }).ToList();
                    if (table1.Count > 0)
                    {
                        Gv_Reports.DataSource = table1;
                        Gv_Reports.DataBind();
                        Session["table1"] = table1;
                        Session["table1Count"] = table1.Count;
                    }
                    else
                    {
                        Gv_Reports.DataSource = null;
                        Gv_Reports.DataBind();
                        Response.Write("<script> alert('No data found')</script>");
                    }
                }
            }
            else // if (firmid == 0)
            {
                //Report for single Firm 
                if (ddlsearch.SelectedItem.Text == "--SELECT--")
                {
                    if (rdoShips.Checked == false)
                    {
                        var table1 = (from c in DVSC.CONTRACTOR_DETAIL
                                      join d in DVSC.DESIGNATION_MASTER on c.Cont_DesignationID equals d.DESIGNATION_ID
                                      join s in DVSC.STATE_MASTER on c.Cont_StateID equals s.STATE_ID
                                      join f in DVSC.FIRMMASTERs on c.Cont_FirmID equals f.FIRM_ID
                                      join r in DVSC.RELIGION_MASTER on c.Cont_ReligionID equals r.R_ID
                                      join n in DVSC.COUNTRY_MASTER on c.Cont_NationalityID equals n.COUNTRY_ID
                                      join cr in DVSC.CANCEL_REASON_MASTER on c.Cont_CancelReason equals cr.CR_ID
                                      join u in DVSC.PSU_MASTER on c.Cont_PSUunitID equals u.PSU_ID
                                      join sh in DVSC.SHOP_MASTER on c.Cont_ShopId equals sh.SHOP_ID
                                      where (c.Cont_CreatedDate >= fromdate && (c.Cont_CreatedDate <= todate))
                                      && (c.Cont_FirmID == firmid) && pass.Contains(c.Cont_PassType)
                                      orderby c.Cont_Id ascending
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
                                          c.Cont_UnitEmp,
                                          c.Cont_MinDate,
                                          c.Cont_DocID
                                      }).ToList();
                        if (table1.Count > 0)
                        {
                            Gv_Reports.DataSource = table1;
                            Gv_Reports.DataBind();
                            Session["table1"] = table1;
                            Session["table1Count"] = table1.Count;
                        }
                        else
                        {
                            Gv_Reports.DataSource = null;
                            Gv_Reports.DataBind();
                            Response.Write("<script> alert('No data found')</script>");
                        }
                    }
                    else if (rdoShips.Checked == true)
                    {
                        var table1 = (from c in DVSC.CONTRACTOR_DETAIL
                                      join d in DVSC.DESIGNATION_MASTER on c.Cont_DesignationID equals d.DESIGNATION_ID
                                      join s in DVSC.STATE_MASTER on c.Cont_StateID equals s.STATE_ID
                                      join f in DVSC.FIRMMASTERs on c.Cont_FirmID equals f.FIRM_ID
                                      join r in DVSC.RELIGION_MASTER on c.Cont_ReligionID equals r.R_ID
                                      join n in DVSC.COUNTRY_MASTER on c.Cont_NationalityID equals n.COUNTRY_ID
                                      join cr in DVSC.CANCEL_REASON_MASTER on c.Cont_CancelReason equals cr.CR_ID
                                      join u in DVSC.PSU_MASTER on c.Cont_PSUunitID equals u.PSU_ID
                                      join sh in DVSC.SHOP_MASTER on c.Cont_ShopId equals sh.SHOP_ID
                                      where (c.Cont_CreatedDate >= fromdate && (c.Cont_CreatedDate <= todate))
                                      && (c.Cont_ShopId == firmid) && pass.Contains(c.Cont_PassType)
                                      orderby c.Cont_Id ascending
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
                                          Cont_Unit = sh.SHOP_NAME,
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
                                          c.Cont_UnitEmp,
                                          c.Cont_MinDate,
                                          c.Cont_DocID
                                      }).ToList();
                        if (table1.Count > 0)
                        {
                            Gv_Reports.DataSource = table1;
                            Gv_Reports.DataBind();
                            Session["table1"] = table1;
                            Session["table1Count"] = table1.Count;
                        }
                        else
                        {
                            Gv_Reports.DataSource = null;
                            Gv_Reports.DataBind();
                            Response.Write("<script> alert('No data found')</script>");
                        }
                    }

                   
                }
                else
                {
                    if (rdoShips.Checked == false)
                    {
                        var table1 = (from c in DVSC.CONTRACTOR_DETAIL
                                      join d in DVSC.DESIGNATION_MASTER on c.Cont_DesignationID equals d.DESIGNATION_ID
                                      join s in DVSC.STATE_MASTER on c.Cont_StateID equals s.STATE_ID
                                      join f in DVSC.FIRMMASTERs on c.Cont_FirmID equals f.FIRM_ID
                                      join r in DVSC.RELIGION_MASTER on c.Cont_ReligionID equals r.R_ID
                                      join n in DVSC.COUNTRY_MASTER on c.Cont_NationalityID equals n.COUNTRY_ID
                                      join cr in DVSC.CANCEL_REASON_MASTER on c.Cont_CancelReason equals cr.CR_ID
                                      join u in DVSC.PSU_MASTER on c.Cont_PSUunitID equals u.PSU_ID
                                      join sh in DVSC.SHOP_MASTER on c.Cont_ShopId equals sh.SHOP_ID
                                      where ((c.Cont_Aadhaar == (txtSearch.Text.Trim()))
                                || (c.Cont_CardNo == (txtSearch.Text.Trim()))
                                || (c.Cont_Mobile == (txtSearch.Text.Trim())) ||
                                (c.Cont_RFIDNo == (txtSearch.Text.Trim())) ||
                                (c.Cont_Name.Contains(txtSearch.Text.Trim())) || (f.FIRM_NAME.Contains(txtSearch.Text.Trim())) ||
                                (c.Cont_DocID == Dockyardid)) && (c.Cont_CreatedDate >= fromdate && (c.Cont_CreatedDate <= todate)) && (c.Cont_FirmID == firmid) && pass.Contains(c.Cont_PassType)
                                      orderby c.Cont_Id ascending
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
                                          c.Cont_UnitEmp,
                                          c.Cont_MinDate,
                                          c.Cont_DocID
                                      }).ToList();
                        if (table1.Count > 0)
                        {
                            Gv_Reports.DataSource = table1;
                            Gv_Reports.DataBind();
                            Session["table1"] = table1;
                            Session["table1Count"] = table1.Count;
                        }
                        else
                        {
                            Gv_Reports.DataSource = null;
                            Gv_Reports.DataBind();
                            Response.Write("<script> alert('No data found')</script>");
                        }
                    }
                    else if (rdoShips.Checked == true)
                    {
                        var table1 = (from c in DVSC.CONTRACTOR_DETAIL
                                      join d in DVSC.DESIGNATION_MASTER on c.Cont_DesignationID equals d.DESIGNATION_ID
                                      join s in DVSC.STATE_MASTER on c.Cont_StateID equals s.STATE_ID
                                      join f in DVSC.FIRMMASTERs on c.Cont_FirmID equals f.FIRM_ID
                                      join r in DVSC.RELIGION_MASTER on c.Cont_ReligionID equals r.R_ID
                                      join n in DVSC.COUNTRY_MASTER on c.Cont_NationalityID equals n.COUNTRY_ID
                                      join cr in DVSC.CANCEL_REASON_MASTER on c.Cont_CancelReason equals cr.CR_ID
                                      join u in DVSC.PSU_MASTER on c.Cont_PSUunitID equals u.PSU_ID
                                      join sh in DVSC.SHOP_MASTER on c.Cont_ShopId equals sh.SHOP_ID
                                      where ((c.Cont_Aadhaar == (txtSearch.Text.Trim()))
                                || (c.Cont_CardNo == (txtSearch.Text.Trim()))
                                || (c.Cont_Mobile == (txtSearch.Text.Trim())) ||
                                (c.Cont_RFIDNo == (txtSearch.Text.Trim())) ||
                                (c.Cont_Name.Contains(txtSearch.Text.Trim())) || (sh.SHOP_NAME.Contains(txtSearch.Text.Trim())) ||
                                (c.Cont_DocID == Dockyardid)) && (c.Cont_CreatedDate >= fromdate 
                                && (c.Cont_CreatedDate <= todate)) && (c.Cont_ShopId == firmid) && pass.Contains(c.Cont_PassType)
                                      orderby c.Cont_Id ascending
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
                                          c.Cont_UnitEmp,
                                          c.Cont_MinDate,
                                          c.Cont_DocID
                                      }).ToList();
                        if (table1.Count > 0)
                        {
                            Gv_Reports.DataSource = table1;
                            Gv_Reports.DataBind();
                            Session["table1"] = table1;
                            Session["table1Count"] = table1.Count;
                        }
                        else
                        {
                            Gv_Reports.DataSource = null;
                            Gv_Reports.DataBind();
                            Response.Write("<script> alert('No data found')</script>");
                        }
                    }


                   
                }
            }
            #endregion
            Gv_Reports.Visible = true;
            Gv_Demo.Visible = false;
            if (rdoAll.Checked == true)
            {
                lblCount.InnerText = "ALL PASS COUNT" + " :" + Session["table1Count"].ToString();
            }
            else
            {
                lblCount.InnerText = PassType + "  PASS COUNT :" + Session["table1Count"].ToString();
            }
        }

        public void Report4()
        {
            PassType = GetPassType();
            pass = PassType.Split(',').ToList();
            //Total Cancel Report
            #region
            if (firmid == 0)
            {
                //Reeport  as per all firm
                if (ddlsearch.SelectedItem.Text == "--SELECT--")
                {
                    var table1 = (from c in DVSC.CONTRACTOR_DETAIL
                                  join d in DVSC.DESIGNATION_MASTER on c.Cont_DesignationID equals d.DESIGNATION_ID
                                  join s in DVSC.STATE_MASTER on c.Cont_StateID equals s.STATE_ID
                                  join f in DVSC.FIRMMASTERs on c.Cont_FirmID equals f.FIRM_ID
                                  join r in DVSC.RELIGION_MASTER on c.Cont_ReligionID equals r.R_ID
                                  join n in DVSC.COUNTRY_MASTER on c.Cont_NationalityID equals n.COUNTRY_ID
                                  join cr in DVSC.CANCEL_REASON_MASTER on c.Cont_CancelReason equals cr.CR_ID
                                  join u in DVSC.PSU_MASTER on c.Cont_PSUunitID equals u.PSU_ID
                                  join sh in DVSC.SHOP_MASTER on c.Cont_ShopId equals sh.SHOP_ID
                                  where (c.Cont_CreatedDate >= fromdate && (c.Cont_CreatedDate <= todate)) && (c.Cont_CancelFLag == "CANCEL") && pass.Contains(c.Cont_PassType)
                                  orderby c.Cont_Id ascending
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
                                      c.Cont_UnitEmp,
                                      c.Cont_MinDate,
                                      c.Cont_DocID
                                  }).ToList();
                    if (table1.Count > 0)
                    {
                        Gv_Reports.DataSource = table1;
                        Gv_Reports.DataBind();
                        Session["table1"] = table1;
                        Session["table1Count"] = table1.Count;
                    }
                    else
                    {
                        Gv_Reports.DataSource = null;
                        Gv_Reports.DataBind();
                        Response.Write("<script> alert('No data found')</script>");
                    }
                }
                else
                {
                    //report For all Firm + Other ID search Record
                    var table1 = (from c in DVSC.CONTRACTOR_DETAIL
                                  join d in DVSC.DESIGNATION_MASTER on c.Cont_DesignationID equals d.DESIGNATION_ID
                                  join s in DVSC.STATE_MASTER on c.Cont_StateID equals s.STATE_ID
                                  join f in DVSC.FIRMMASTERs on c.Cont_FirmID equals f.FIRM_ID
                                  join r in DVSC.RELIGION_MASTER on c.Cont_ReligionID equals r.R_ID
                                  join n in DVSC.COUNTRY_MASTER on c.Cont_NationalityID equals n.COUNTRY_ID
                                  join cr in DVSC.CANCEL_REASON_MASTER on c.Cont_CancelReason equals cr.CR_ID
                                  join u in DVSC.PSU_MASTER on c.Cont_PSUunitID equals u.PSU_ID
                                  join sh in DVSC.SHOP_MASTER on c.Cont_ShopId equals sh.SHOP_ID
                                  where ((c.Cont_Aadhaar == (txtSearch.Text.Trim()))
                            || (c.Cont_CardNo == (txtSearch.Text.Trim()))
                            || (c.Cont_Mobile == (txtSearch.Text.Trim())) ||
                            (c.Cont_RFIDNo == (txtSearch.Text.Trim())) ||
                            (c.Cont_Name.Contains(txtSearch.Text.Trim())) || (f.FIRM_NAME.Contains(txtSearch.Text.Trim())) ||
                            (c.Cont_DocID == Dockyardid)) && (c.Cont_CreatedDate >= fromdate && (c.Cont_CreatedDate <= todate)) && (c.Cont_CancelFLag == "CANCEL") && pass.Contains(c.Cont_PassType)
                                  orderby c.Cont_Id ascending
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
                                      c.Cont_UnitEmp,
                                      c.Cont_MinDate,
                                      c.Cont_DocID

                                  }).ToList();
                    if (table1.Count > 0)
                    {
                        Gv_Reports.DataSource = table1;
                        Gv_Reports.DataBind();
                        Session["table1"] = table1;
                        Session["table1Count"] = table1.Count;
                    }
                    else
                    {
                        Gv_Reports.DataSource = null;
                        Gv_Reports.DataBind();
                        Response.Write("<script> alert('No data found')</script>");
                    }
                }
            }
            else
            {
                //Report for single Firm 
                if (ddlsearch.SelectedItem.Text == "--SELECT--")
                {
                    var table1 = (from c in DVSC.CONTRACTOR_DETAIL
                                  join d in DVSC.DESIGNATION_MASTER on c.Cont_DesignationID equals d.DESIGNATION_ID
                                  join s in DVSC.STATE_MASTER on c.Cont_StateID equals s.STATE_ID
                                  join f in DVSC.FIRMMASTERs on c.Cont_FirmID equals f.FIRM_ID
                                  join r in DVSC.RELIGION_MASTER on c.Cont_ReligionID equals r.R_ID
                                  join n in DVSC.COUNTRY_MASTER on c.Cont_NationalityID equals n.COUNTRY_ID
                                  join cr in DVSC.CANCEL_REASON_MASTER on c.Cont_CancelReason equals cr.CR_ID
                                  join u in DVSC.PSU_MASTER on c.Cont_PSUunitID equals u.PSU_ID
                                  join sh in DVSC.SHOP_MASTER on c.Cont_ShopId equals sh.SHOP_ID
                                  where (c.Cont_CreatedDate >= fromdate && (c.Cont_CreatedDate <= todate)) && (c.Cont_FirmID == firmid) && (c.Cont_CancelFLag == "CANCEL") && pass.Contains(c.Cont_PassType)
                                  orderby c.Cont_Id ascending
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
                                      c.Cont_UnitEmp,
                                      c.Cont_MinDate,
                                      c.Cont_DocID
                                  }).ToList();
                    if (table1.Count > 0)
                    {
                        Gv_Reports.DataSource = table1;
                        Gv_Reports.DataBind();
                        Session["table1"] = table1;
                        Session["table1Count"] = table1.Count;
                    }
                    else
                    {
                        Gv_Reports.DataSource = null;
                        Gv_Reports.DataBind();
                        Response.Write("<script> alert('No data found')</script>");
                    }
                }
                else
                {
                    var table1 = (from c in DVSC.CONTRACTOR_DETAIL
                                  join d in DVSC.DESIGNATION_MASTER on c.Cont_DesignationID equals d.DESIGNATION_ID
                                  join s in DVSC.STATE_MASTER on c.Cont_StateID equals s.STATE_ID
                                  join f in DVSC.FIRMMASTERs on c.Cont_FirmID equals f.FIRM_ID
                                  join r in DVSC.RELIGION_MASTER on c.Cont_ReligionID equals r.R_ID
                                  join n in DVSC.COUNTRY_MASTER on c.Cont_NationalityID equals n.COUNTRY_ID
                                  join cr in DVSC.CANCEL_REASON_MASTER on c.Cont_CancelReason equals cr.CR_ID
                                  join u in DVSC.PSU_MASTER on c.Cont_PSUunitID equals u.PSU_ID
                                  join sh in DVSC.SHOP_MASTER on c.Cont_ShopId equals sh.SHOP_ID
                                  where ((c.Cont_Aadhaar == (txtSearch.Text.Trim()))
                            || (c.Cont_CardNo == (txtSearch.Text.Trim()))
                            || (c.Cont_Mobile == (txtSearch.Text.Trim())) ||
                            (c.Cont_RFIDNo == (txtSearch.Text.Trim())) ||
                            (c.Cont_Name.Contains(txtSearch.Text.Trim())) || (f.FIRM_NAME.Contains(txtSearch.Text.Trim())) ||
                            (c.Cont_DocID == Dockyardid)) && (c.Cont_CreatedDate >= fromdate && (c.Cont_CreatedDate <= todate)) && (c.Cont_FirmID == firmid) && (c.Cont_CancelFLag == "CANCEL") && pass.Contains(c.Cont_PassType)
                                  orderby c.Cont_Id ascending
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
                                      c.Cont_UnitEmp,
                                      c.Cont_MinDate,
                                      c.Cont_DocID
                                  }).ToList();
                    if (table1.Count > 0)
                    {
                        Gv_Reports.DataSource = table1;
                        Gv_Reports.DataBind();
                        Session["table1"] = table1;
                        Session["table1Count"] = table1.Count;
                    }
                    else
                    {
                        Gv_Reports.DataSource = null;
                        Gv_Reports.DataBind();
                        Response.Write("<script> alert('No data found')</script>");
                    }
                }
            }
            #endregion
            Gv_Reports.Visible = true;
            Gv_Demo.Visible = false;
            if (rdoAll.Checked == true)
            {
                lblCount.InnerText = "ALL PASS COUNT" + " :" + Session["table1Count"].ToString();
            }
            else
            {
                lblCount.InnerText = PassType + "  PASS COUNT :" + Session["table1Count"].ToString();
            }
        }

        public void Report5() // Total Valid Report
        {
            PassType = GetPassType();
            pass = PassType.Split(',').ToList();
            //Total Valid Report
            #region
            DateTime CuurentDate = Convert.ToDateTime(System.DateTime.Now.ToString("dd/MM/yyyy"));
            if (firmid == 0)
            {
                //Reeport  as per all firm
                if (ddlsearch.SelectedItem.Text == "--SELECT--")
                {
                    var table1 = (from c in DVSC.CONTRACTOR_DETAIL
                                  //join Min in DVSC.TempMinTimes on c.Cont_DocID equals Min.Cont_ID
                                  join d in DVSC.DESIGNATION_MASTER on c.Cont_DesignationID equals d.DESIGNATION_ID
                                  join s in DVSC.STATE_MASTER on c.Cont_StateID equals s.STATE_ID
                                  join f in DVSC.FIRMMASTERs on c.Cont_FirmID equals f.FIRM_ID
                                  join r in DVSC.RELIGION_MASTER on c.Cont_ReligionID equals r.R_ID
                                  join n in DVSC.COUNTRY_MASTER on c.Cont_NationalityID equals n.COUNTRY_ID
                                  join cr in DVSC.CANCEL_REASON_MASTER on c.Cont_CancelReason equals cr.CR_ID
                                  join u in DVSC.PSU_MASTER on c.Cont_PSUunitID equals u.PSU_ID
                                  join sh in DVSC.SHOP_MASTER on c.Cont_ShopId equals sh.SHOP_ID
                                  where (c.Cont_CreatedDate >= fromdate && (c.Cont_CreatedDate <= todate)) && pass.Contains(c.Cont_PassType)
                                  && ((c.Cont_MinDate >= CuurentDate))
                                  orderby c.Cont_Id ascending
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
                                      c.Cont_UnitEmp,
                                      c.Cont_MinDate,
                                      c.Cont_DocID
                                  }).ToList();

                    if (table1.Count > 0)
                    {
                        Gv_Reports.DataSource = table1;
                        Gv_Reports.DataBind();
                        Session["table1"] = table1;
                        Session["table1Count"] = table1.Count;
                    }
                    else
                    {
                        Gv_Reports.DataSource = null;
                        Gv_Reports.DataBind();
                        Response.Write("<script> alert('No data found')</script>");
                    }
                }
                else
                {
                    //report For all Firm + Other ID search Record
                    var table1 = (from c in DVSC.CONTRACTOR_DETAIL
                                  join d in DVSC.DESIGNATION_MASTER on c.Cont_DesignationID equals d.DESIGNATION_ID
                                  join s in DVSC.STATE_MASTER on c.Cont_StateID equals s.STATE_ID
                                  join f in DVSC.FIRMMASTERs on c.Cont_FirmID equals f.FIRM_ID
                                  join r in DVSC.RELIGION_MASTER on c.Cont_ReligionID equals r.R_ID
                                  join n in DVSC.COUNTRY_MASTER on c.Cont_NationalityID equals n.COUNTRY_ID
                                  join cr in DVSC.CANCEL_REASON_MASTER on c.Cont_CancelReason equals cr.CR_ID
                                  join u in DVSC.PSU_MASTER on c.Cont_PSUunitID equals u.PSU_ID
                                  join sh in DVSC.SHOP_MASTER on c.Cont_ShopId equals sh.SHOP_ID
                                  where ((c.Cont_Aadhaar == (txtSearch.Text.Trim()))
                            || (c.Cont_CardNo == (txtSearch.Text.Trim()))
                            || (c.Cont_Mobile == (txtSearch.Text.Trim())) ||
                            (c.Cont_RFIDNo == (txtSearch.Text.Trim())) ||
                            (c.Cont_Name.Contains(txtSearch.Text.Trim())) || (f.FIRM_NAME.Contains(txtSearch.Text.Trim())) ||
                            (c.Cont_DocID == Dockyardid)) && (c.Cont_CreatedDate >= fromdate && (c.Cont_CreatedDate <= todate)) && (c.Cont_MinDate >= CuurentDate) && pass.Contains(c.Cont_PassType)
                                  orderby c.Cont_Id ascending
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
                                      c.Cont_UnitEmp,
                                      c.Cont_MinDate,
                                      c.Cont_DocID
                                  }).ToList();
                    if (table1.Count > 0)
                    {
                        Gv_Reports.DataSource = table1;
                        Gv_Reports.DataBind();
                        Session["table1"] = table1;
                        Session["table1Count"] = table1.Count;
                    }
                    else
                    {
                        Gv_Reports.DataSource = null;
                        Gv_Reports.DataBind();
                        Response.Write("<script> alert('No data found')</script>");
                    }
                }
            }
            else // if (firmid == 0)
            {
                //Report for single Firm 
                if (ddlsearch.SelectedItem.Text == "--SELECT--")
                {
                    if (rdoShips.Checked == false)
                    {
                        var table1 = (from c in DVSC.CONTRACTOR_DETAIL
                                      join d in DVSC.DESIGNATION_MASTER on c.Cont_DesignationID equals d.DESIGNATION_ID
                                      join s in DVSC.STATE_MASTER on c.Cont_StateID equals s.STATE_ID
                                      join f in DVSC.FIRMMASTERs on c.Cont_FirmID equals f.FIRM_ID
                                      join r in DVSC.RELIGION_MASTER on c.Cont_ReligionID equals r.R_ID
                                      join n in DVSC.COUNTRY_MASTER on c.Cont_NationalityID equals n.COUNTRY_ID
                                      join cr in DVSC.CANCEL_REASON_MASTER on c.Cont_CancelReason equals cr.CR_ID
                                      join u in DVSC.PSU_MASTER on c.Cont_PSUunitID equals u.PSU_ID
                                      join sh in DVSC.SHOP_MASTER on c.Cont_ShopId equals sh.SHOP_ID
                                      where (c.Cont_CreatedDate >= fromdate && (c.Cont_CreatedDate <= todate)) && (c.Cont_FirmID == firmid) && (c.Cont_MinDate >= CuurentDate) && pass.Contains(c.Cont_PassType)
                                      orderby c.Cont_Id ascending
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
                                          c.Cont_UnitEmp,
                                          c.Cont_MinDate,
                                          c.Cont_DocID
                                      }).ToList();
                        if (table1.Count > 0)
                        {
                            Gv_Reports.DataSource = table1;
                            Gv_Reports.DataBind();
                            Session["table1"] = table1;
                            Session["table1Count"] = table1.Count;
                        }
                        else
                        {
                            Gv_Reports.DataSource = null;
                            Gv_Reports.DataBind();
                            Response.Write("<script> alert('No data found')</script>");
                        }
                    }
                    else if (rdoShips.Checked == true)
                    {
                        var table1 = (from c in DVSC.CONTRACTOR_DETAIL
                                      join d in DVSC.DESIGNATION_MASTER on c.Cont_DesignationID equals d.DESIGNATION_ID
                                      join s in DVSC.STATE_MASTER on c.Cont_StateID equals s.STATE_ID
                                      join f in DVSC.FIRMMASTERs on c.Cont_FirmID equals f.FIRM_ID
                                      join r in DVSC.RELIGION_MASTER on c.Cont_ReligionID equals r.R_ID
                                      join n in DVSC.COUNTRY_MASTER on c.Cont_NationalityID equals n.COUNTRY_ID
                                      join cr in DVSC.CANCEL_REASON_MASTER on c.Cont_CancelReason equals cr.CR_ID
                                      join u in DVSC.PSU_MASTER on c.Cont_PSUunitID equals u.PSU_ID
                                      join sh in DVSC.SHOP_MASTER on c.Cont_ShopId equals sh.SHOP_ID
                                      where (c.Cont_CreatedDate >= fromdate && (c.Cont_CreatedDate <= todate)) 
                                      && (c.Cont_ShopId == firmid) && (c.Cont_MinDate >= CuurentDate) && pass.Contains(c.Cont_PassType)
                                      orderby c.Cont_Id ascending
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
                                          Cont_Unit = sh.SHOP_NAME,
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
                                          c.Cont_UnitEmp,
                                          c.Cont_MinDate,
                                          c.Cont_DocID
                                      }).ToList();
                        if (table1.Count > 0)
                        {
                            Gv_Reports.DataSource = table1;
                            Gv_Reports.DataBind();
                            Session["table1"] = table1;
                            Session["table1Count"] = table1.Count;
                        }
                        else
                        {
                            Gv_Reports.DataSource = null;
                            Gv_Reports.DataBind();
                            Response.Write("<script> alert('No data found')</script>");
                        }
                    }

                    
                }
                else
                {
                    if (rdoShips.Checked == false)
                    {
                        var table1 = (from c in DVSC.CONTRACTOR_DETAIL
                                      join d in DVSC.DESIGNATION_MASTER on c.Cont_DesignationID equals d.DESIGNATION_ID
                                      join s in DVSC.STATE_MASTER on c.Cont_StateID equals s.STATE_ID
                                      join f in DVSC.FIRMMASTERs on c.Cont_FirmID equals f.FIRM_ID
                                      join r in DVSC.RELIGION_MASTER on c.Cont_ReligionID equals r.R_ID
                                      join n in DVSC.COUNTRY_MASTER on c.Cont_NationalityID equals n.COUNTRY_ID
                                      join cr in DVSC.CANCEL_REASON_MASTER on c.Cont_CancelReason equals cr.CR_ID
                                      join u in DVSC.PSU_MASTER on c.Cont_PSUunitID equals u.PSU_ID
                                      join sh in DVSC.SHOP_MASTER on c.Cont_ShopId equals sh.SHOP_ID
                                      where ((c.Cont_Aadhaar == (txtSearch.Text.Trim()))
                                || (c.Cont_CardNo == (txtSearch.Text.Trim()))
                                || (c.Cont_Mobile == (txtSearch.Text.Trim())) ||
                                (c.Cont_RFIDNo == (txtSearch.Text.Trim())) ||
                                (c.Cont_Name.Contains(txtSearch.Text.Trim())) || (f.FIRM_NAME.Contains(txtSearch.Text.Trim())) ||
                                (c.Cont_DocID == Dockyardid)) && (c.Cont_CreatedDate >= fromdate && (c.Cont_CreatedDate <= todate))
                                && (c.Cont_FirmID == firmid) && (c.Cont_MinDate >= CuurentDate) && pass.Contains(c.Cont_PassType)
                                      //&& ((c.Cont_WOValidity == "01-01-2017") || (c.Cont_PVCValidity == "01-01-2017") || (c.Cont_RFIDValidity == "01-01-2017"))
                                      orderby c.Cont_Id ascending
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
                                          c.Cont_UnitEmp,
                                          c.Cont_MinDate,
                                          c.Cont_DocID
                                      }).ToList();
                        if (table1.Count > 0)
                        {
                            Gv_Reports.DataSource = table1;
                            Gv_Reports.DataBind();
                            Session["table1"] = table1;
                            Session["table1Count"] = table1.Count;
                        }
                        else
                        {
                            Gv_Reports.DataSource = null;
                            Gv_Reports.DataBind();
                            Response.Write("<script> alert('No data found')</script>");
                        }

                    }
                    else if (rdoShips.Checked == true)
                    {
                        var table1 = (from c in DVSC.CONTRACTOR_DETAIL
                                      join d in DVSC.DESIGNATION_MASTER on c.Cont_DesignationID equals d.DESIGNATION_ID
                                      join s in DVSC.STATE_MASTER on c.Cont_StateID equals s.STATE_ID
                                      join f in DVSC.FIRMMASTERs on c.Cont_FirmID equals f.FIRM_ID
                                      join r in DVSC.RELIGION_MASTER on c.Cont_ReligionID equals r.R_ID
                                      join n in DVSC.COUNTRY_MASTER on c.Cont_NationalityID equals n.COUNTRY_ID
                                      join cr in DVSC.CANCEL_REASON_MASTER on c.Cont_CancelReason equals cr.CR_ID
                                      join u in DVSC.PSU_MASTER on c.Cont_PSUunitID equals u.PSU_ID
                                      join sh in DVSC.SHOP_MASTER on c.Cont_ShopId equals sh.SHOP_ID
                                      where ((c.Cont_Aadhaar == (txtSearch.Text.Trim()))
                                || (c.Cont_CardNo == (txtSearch.Text.Trim()))
                                || (c.Cont_Mobile == (txtSearch.Text.Trim())) ||
                                (c.Cont_RFIDNo == (txtSearch.Text.Trim())) ||
                                (c.Cont_Name.Contains(txtSearch.Text.Trim())) || (sh.SHOP_NAME.Contains(txtSearch.Text.Trim())) ||
                                (c.Cont_DocID == Dockyardid)) && (c.Cont_CreatedDate >= fromdate && (c.Cont_CreatedDate <= todate))
                                && (c.Cont_ShopId == firmid) && (c.Cont_MinDate >= CuurentDate) && pass.Contains(c.Cont_PassType)
                                      //&& ((c.Cont_WOValidity == "01-01-2017") || (c.Cont_PVCValidity == "01-01-2017") || (c.Cont_RFIDValidity == "01-01-2017"))
                                      orderby c.Cont_Id ascending
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
                                          Cont_Unit = sh.SHOP_NAME,
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
                                          c.Cont_UnitEmp,
                                          c.Cont_MinDate,
                                          c.Cont_DocID
                                      }).ToList();
                        if (table1.Count > 0)
                        {
                            Gv_Reports.DataSource = table1;
                            Gv_Reports.DataBind();
                            Session["table1"] = table1;
                            Session["table1Count"] = table1.Count;
                        }
                        else
                        {
                            Gv_Reports.DataSource = null;
                            Gv_Reports.DataBind();
                            Response.Write("<script> alert('No data found')</script>");
                        }

                    }

                  
                }
            }
            #endregion
            Gv_Reports.Visible = true;
            Gv_Demo.Visible = false;
            if (rdoAll.Checked == true)
            {
                lblCount.InnerText = "ALL PASS COUNT" + " :" + Session["table1Count"].ToString();
            }
            else
            {
                lblCount.InnerText = PassType + "  PASS COUNT :" + Session["table1Count"].ToString();
            }
        }

        public void Report6()
        {
            PassType = GetPassType();
            pass = PassType.Split(',').ToList();
            //Expired But Not Renewed
            #region
            CuurentDate = Convert.ToDateTime(System.DateTime.Now.ToString("dd/MM/yyyy"));
            if (firmid == 0)
            {
                //Reeport  as per all firm
                if (ddlsearch.SelectedItem.Text == "--SELECT--")
                {
                    var table1 = (from c in DVSC.CONTRACTOR_DETAIL
                                  //join Min in DVSC.TempMinTimes on c.Cont_DocID equals Min.Cont_ID
                                  join d in DVSC.DESIGNATION_MASTER on c.Cont_DesignationID equals d.DESIGNATION_ID
                                  join s in DVSC.STATE_MASTER on c.Cont_StateID equals s.STATE_ID
                                  join f in DVSC.FIRMMASTERs on c.Cont_FirmID equals f.FIRM_ID
                                  join r in DVSC.RELIGION_MASTER on c.Cont_ReligionID equals r.R_ID
                                  join n in DVSC.COUNTRY_MASTER on c.Cont_NationalityID equals n.COUNTRY_ID
                                  join cr in DVSC.CANCEL_REASON_MASTER on c.Cont_CancelReason equals cr.CR_ID
                                  join u in DVSC.PSU_MASTER on c.Cont_PSUunitID equals u.PSU_ID
                                  join sh in DVSC.SHOP_MASTER on c.Cont_ShopId equals sh.SHOP_ID
                                  where (c.Cont_CreatedDate >= fromdate && (c.Cont_CreatedDate <= todate)) && pass.Contains(c.Cont_PassType)
                                  && ((c.Cont_MinDate <= CuurentDate))
                                  orderby c.Cont_Id ascending
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
                                      c.Cont_UnitEmp,
                                      c.Cont_MinDate,
                                      c.Cont_DocID
                                  }).ToList();

                    if (table1.Count > 0)
                    {
                        Gv_Reports.DataSource = table1;
                        Gv_Reports.DataBind();
                        Session["table1"] = table1;
                        Session["table1Count"] = table1.Count;
                    }
                    else
                    {
                        Gv_Reports.DataSource = null;
                        Gv_Reports.DataBind();
                        Response.Write("<script> alert('No data found')</script>");
                    }
                }
                else
                {
                    //report For all Firm + Other ID search Record
                    var table1 = (from c in DVSC.CONTRACTOR_DETAIL
                                  join d in DVSC.DESIGNATION_MASTER on c.Cont_DesignationID equals d.DESIGNATION_ID
                                  join s in DVSC.STATE_MASTER on c.Cont_StateID equals s.STATE_ID
                                  join f in DVSC.FIRMMASTERs on c.Cont_FirmID equals f.FIRM_ID
                                  join r in DVSC.RELIGION_MASTER on c.Cont_ReligionID equals r.R_ID
                                  join n in DVSC.COUNTRY_MASTER on c.Cont_NationalityID equals n.COUNTRY_ID
                                  join cr in DVSC.CANCEL_REASON_MASTER on c.Cont_CancelReason equals cr.CR_ID
                                  join u in DVSC.PSU_MASTER on c.Cont_PSUunitID equals u.PSU_ID
                                  join sh in DVSC.SHOP_MASTER on c.Cont_ShopId equals sh.SHOP_ID
                                  where ((c.Cont_Aadhaar == (txtSearch.Text.Trim()))
                            || (c.Cont_CardNo == (txtSearch.Text.Trim()))
                            || (c.Cont_Mobile == (txtSearch.Text.Trim())) ||
                            (c.Cont_RFIDNo == (txtSearch.Text.Trim())) ||
                            (c.Cont_Name.Contains(txtSearch.Text.Trim())) || (f.FIRM_NAME.Contains(txtSearch.Text.Trim())) ||
                            (c.Cont_DocID == Dockyardid)) && (c.Cont_CreatedDate >= fromdate && (c.Cont_CreatedDate <= todate)) && (c.Cont_MinDate <= CuurentDate) && pass.Contains(c.Cont_PassType)
                                  orderby c.Cont_Id ascending
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
                                      c.Cont_UnitEmp,
                                      c.Cont_MinDate,
                                      c.Cont_DocID
                                  }).ToList();
                    if (table1.Count > 0)
                    {
                        Gv_Reports.DataSource = table1;
                        Gv_Reports.DataBind();
                        Session["table1"] = table1;
                        Session["table1Count"] = table1.Count;
                    }
                    else
                    {
                        Gv_Reports.DataSource = null;
                        Gv_Reports.DataBind();
                        Response.Write("<script> alert('No data found')</script>");
                    }
                }
            }
            else
            {
                //Report for single Firm 
                if (ddlsearch.SelectedItem.Text == "--SELECT--")
                {
                    var table1 = (from c in DVSC.CONTRACTOR_DETAIL
                                  join d in DVSC.DESIGNATION_MASTER on c.Cont_DesignationID equals d.DESIGNATION_ID
                                  join s in DVSC.STATE_MASTER on c.Cont_StateID equals s.STATE_ID
                                  join f in DVSC.FIRMMASTERs on c.Cont_FirmID equals f.FIRM_ID
                                  join r in DVSC.RELIGION_MASTER on c.Cont_ReligionID equals r.R_ID
                                  join n in DVSC.COUNTRY_MASTER on c.Cont_NationalityID equals n.COUNTRY_ID
                                  join cr in DVSC.CANCEL_REASON_MASTER on c.Cont_CancelReason equals cr.CR_ID
                                  join u in DVSC.PSU_MASTER on c.Cont_PSUunitID equals u.PSU_ID
                                  join sh in DVSC.SHOP_MASTER on c.Cont_ShopId equals sh.SHOP_ID
                                  where (c.Cont_CreatedDate >= fromdate && (c.Cont_CreatedDate <= todate)) && (c.Cont_FirmID == firmid) && (c.Cont_MinDate <= CuurentDate) && pass.Contains(c.Cont_PassType)
                                  orderby c.Cont_Id ascending
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
                                      c.Cont_UnitEmp,
                                      c.Cont_MinDate,
                                      c.Cont_DocID
                                  }).ToList();
                    if (table1.Count > 0)
                    {
                        Gv_Reports.DataSource = table1;
                        Gv_Reports.DataBind();
                        Session["table1"] = table1;
                        Session["table1Count"] = table1.Count;
                    }
                    else
                    {
                        Gv_Reports.DataSource = null;
                        Gv_Reports.DataBind();
                        Response.Write("<script> alert('No data found')</script>");
                    }
                }
                else
                {
                    var table1 = (from c in DVSC.CONTRACTOR_DETAIL
                                  join d in DVSC.DESIGNATION_MASTER on c.Cont_DesignationID equals d.DESIGNATION_ID
                                  join s in DVSC.STATE_MASTER on c.Cont_StateID equals s.STATE_ID
                                  join f in DVSC.FIRMMASTERs on c.Cont_FirmID equals f.FIRM_ID
                                  join r in DVSC.RELIGION_MASTER on c.Cont_ReligionID equals r.R_ID
                                  join n in DVSC.COUNTRY_MASTER on c.Cont_NationalityID equals n.COUNTRY_ID
                                  join cr in DVSC.CANCEL_REASON_MASTER on c.Cont_CancelReason equals cr.CR_ID
                                  join u in DVSC.PSU_MASTER on c.Cont_PSUunitID equals u.PSU_ID
                                  join sh in DVSC.SHOP_MASTER on c.Cont_ShopId equals sh.SHOP_ID
                                  where ((c.Cont_Aadhaar == (txtSearch.Text.Trim()))
                            || (c.Cont_CardNo == (txtSearch.Text.Trim()))
                            || (c.Cont_Mobile == (txtSearch.Text.Trim())) ||
                            (c.Cont_RFIDNo == (txtSearch.Text.Trim())) ||
                            (c.Cont_Name.Contains(txtSearch.Text.Trim())) || (f.FIRM_NAME.Contains(txtSearch.Text.Trim())) ||
                            (c.Cont_DocID == Dockyardid)) && (c.Cont_CreatedDate >= fromdate && (c.Cont_CreatedDate <= todate))
                            && (c.Cont_FirmID == firmid) && (c.Cont_MinDate <= CuurentDate) && pass.Contains(c.Cont_PassType)
                                  //&& ((c.Cont_WOValidity == "01-01-2017") || (c.Cont_PVCValidity == "01-01-2017") || (c.Cont_RFIDValidity == "01-01-2017"))
                                  orderby c.Cont_Id ascending
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
                                      c.Cont_UnitEmp,
                                      c.Cont_MinDate,
                                      c.Cont_DocID
                                  }).ToList();
                    if (table1.Count > 0)
                    {
                        Gv_Reports.DataSource = table1;
                        Gv_Reports.DataBind();
                        Session["table1"] = table1;
                        Session["table1Count"] = table1.Count;
                    }
                    else
                    {
                        Gv_Reports.DataSource = null;
                        Gv_Reports.DataBind();
                        Response.Write("<script> alert('No data found')</script>");
                    }

                }
            }
            #endregion
            Gv_Reports.Visible = true;
            Gv_Demo.Visible = false;
            if (rdoAll.Checked == true)
            {
                lblCount.InnerText = "ALL PASS COUNT" + " :" + Session["table1Count"].ToString();
            }
            else
            {
                lblCount.InnerText = PassType + "  PASS COUNT :" + Session["table1Count"].ToString();
            }
        }

        public void Report7()
        {
            lblCount1.Visible = false;
            lblCount2.Visible = false;
            PassType = GetPassType();

            if (rdoAll.Checked == true)
            {
                PassType = "All";
            }
            //Total In Out Report
            #region
            string PassNo = "";
            string Firm = "";
            if (txtSearch.Text == "")
            {
                txtSearch.Text = "0";
            }
            if (ddlsearch.SelectedItem.Text == "FIRM")
            {
                PassNo = "0";
                Firm = txtSearch.Text.Trim();
            }
            if (ddlsearch.SelectedItem.Text == "PASS NO")
            {
                Firm = "0";
                PassNo = txtSearch.Text.Trim();
            }
            if (ddlFirm.SelectedItem.Text != "--ALL--")
            {
                Firm = ddlFirm.SelectedItem.Text.Trim();
                PassNo = "0";
            }


            DataSet ds = new DataSet("TimeRanges");
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["VMSConnection"].ConnectionString))
            {
                SqlCommand sqlComm = new SqlCommand("Get_InOut_Report", conn);
                sqlComm.Parameters.AddWithValue("@fromdate", fromdate);
                sqlComm.Parameters.AddWithValue("@todate", todate);
                sqlComm.Parameters.AddWithValue("@Cardno", PassNo);
                sqlComm.Parameters.AddWithValue("@FirmName", Firm);
                sqlComm.Parameters.AddWithValue("@PassType", PassType);
                sqlComm.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = sqlComm;
                da.Fill(ds);
                Gv_Demo.DataSource = ds.Tables[0];
                Gv_Demo.DataBind();
                Session["table1"] = ds.Tables[0];
                //ds.Tables["0"].Select("Len(Intime)=0");
                 //DataRow[] drInTime =  ds.Tables["0"].Select("Intime<>''").;
                if (ds.Tables != null)
                {
                    if (ds.Tables.Count > 0)
                    {
                        lblCount1.Visible = true;
                        lblCount2.Visible = true;

                        lblCount1.InnerText = "Total In Count" + " :" + ds.Tables[1].Rows[0][0].ToString();


                        lblCount2.InnerText = "Total Out Count" + " :" + ds.Tables[2].Rows[0][0].ToString();
                    }
                }
            }
            #endregion
            Gv_Reports.Visible = false;
            Gv_Demo.Visible = true;


              //<asp:BoundField HeaderText="PASS NO" DataField="Cont_CardNo" />
              //      <asp:BoundField HeaderText="DOCKYARD ID NO" DataField="Cont_DocID" />
              //       <asp:BoundField HeaderText="TranDate" DataField="TranDate" />
              //      <asp:BoundField HeaderText="IN TIME" DataField="InTime" />
              //      <asp:BoundField HeaderText="OUT TIME" DataField="OutTime" />
              //      <asp:BoundField HeaderText="FULL NAME" DataField="Cont_Name" />
              //      <asp:BoundField HeaderText="MOBILE NO" DataField="Cont_Mobile" />
              //      <asp:BoundField HeaderText="FIRM NAME" DataField="FIRM_NAME" />
              //      <asp:BoundField HeaderText="PVC NO" DataField="Cont_PVCNO" />
              //      <asp:BoundField HeaderText="PVC VALIDITY" DataField="Cont_PVCValidity" />
              //      <asp:BoundField HeaderText="RFID NO" DataField="Cont_RFIDNo" />
              //      <asp:BoundField HeaderText="RFID VALIDITY" DataField="Cont_RFIDValidity" />

        }
        public void Report13()
        {
            PassType = GetPassType();
            pass = PassType.Split(',').ToList();
            //Religion Wise Report
            #region
            if (StateReport == true)
            {
                if (ddlsearch.SelectedItem.Text != "--SELECT--")
                {
                    //All Firm and All Religion with Search Condition
                    var table1 = (from c in DVSC.CONTRACTOR_DETAIL
                                  join d in DVSC.DESIGNATION_MASTER on c.Cont_DesignationID equals d.DESIGNATION_ID
                                  join s in DVSC.STATE_MASTER on c.Cont_StateID equals s.STATE_ID
                                  join f in DVSC.FIRMMASTERs on c.Cont_FirmID equals f.FIRM_ID
                                  join r in DVSC.RELIGION_MASTER on c.Cont_ReligionID equals r.R_ID
                                  join n in DVSC.COUNTRY_MASTER on c.Cont_NationalityID equals n.COUNTRY_ID
                                  join cr in DVSC.CANCEL_REASON_MASTER on c.Cont_CancelReason equals cr.CR_ID
                                  join u in DVSC.PSU_MASTER on c.Cont_PSUunitID equals u.PSU_ID
                                  join sh in DVSC.SHOP_MASTER on c.Cont_ShopId equals sh.SHOP_ID
                                  where ((c.Cont_Aadhaar == (txtSearch.Text.Trim()))
                            || (c.Cont_CardNo == (txtSearch.Text.Trim()))
                            || (c.Cont_Mobile == (txtSearch.Text.Trim())) ||
                            (c.Cont_RFIDNo == (txtSearch.Text.Trim())) ||
                            (c.Cont_Name.Contains(txtSearch.Text.Trim())) || (f.FIRM_NAME.Contains(txtSearch.Text.Trim())) ||
                            (c.Cont_DocID == Dockyardid)) && (c.Cont_CreatedDate >= fromdate && (c.Cont_CreatedDate <= todate)) && pass.Contains(c.Cont_PassType)
                                  orderby c.Cont_Id ascending
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
                                      c.Cont_UnitEmp,
                                      c.Cont_MinDate,
                                      c.Cont_DocID
                                  }).ToList();
                    if (table1.Count > 0)
                    {
                        Gv_Reports.DataSource = table1;
                        Gv_Reports.DataBind();
                        Session["table1"] = table1;
                        Session["table1Count"] = table1.Count;
                    }
                    else
                    {
                        Gv_Reports.DataSource = null;
                        Gv_Reports.DataBind();
                        Response.Write("<script> alert('No data found')</script>");
                    }
                }
                else
                {
                    //All Data With No Condition
                    var table1 = (from c in DVSC.CONTRACTOR_DETAIL
                                  join d in DVSC.DESIGNATION_MASTER on c.Cont_DesignationID equals d.DESIGNATION_ID
                                  join s in DVSC.STATE_MASTER on c.Cont_StateID equals s.STATE_ID
                                  join f in DVSC.FIRMMASTERs on c.Cont_FirmID equals f.FIRM_ID
                                  join r in DVSC.RELIGION_MASTER on c.Cont_ReligionID equals r.R_ID
                                  join n in DVSC.COUNTRY_MASTER on c.Cont_NationalityID equals n.COUNTRY_ID
                                  join cr in DVSC.CANCEL_REASON_MASTER on c.Cont_CancelReason equals cr.CR_ID
                                  join u in DVSC.PSU_MASTER on c.Cont_PSUunitID equals u.PSU_ID
                                  join sh in DVSC.SHOP_MASTER on c.Cont_ShopId equals sh.SHOP_ID
                                  where (c.Cont_CreatedDate >= fromdate && (c.Cont_CreatedDate <= todate)) && pass.Contains(c.Cont_PassType)
                                  orderby c.Cont_Id ascending
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
                                      c.Cont_UnitEmp,
                                      c.Cont_MinDate,
                                      c.Cont_DocID
                                  }).ToList();
                    if (table1.Count > 0)
                    {
                        Gv_Reports.DataSource = table1;
                        Gv_Reports.DataBind();
                        Session["table1"] = table1;
                        Session["table1Count"] = table1.Count;
                    }
                    else
                    {
                        Gv_Reports.DataSource = null;
                        Gv_Reports.DataBind();
                        Response.Write("<script> alert('No data found')</script>");
                    }
                }
            }
            else
            {
                if (firmid > 0)
                {
                    // Single Firm All State
                    if (State > 0)
                    {
                        var table1 = (from c in DVSC.CONTRACTOR_DETAIL
                                      join d in DVSC.DESIGNATION_MASTER on c.Cont_DesignationID equals d.DESIGNATION_ID
                                      join s in DVSC.STATE_MASTER on c.Cont_StateID equals s.STATE_ID
                                      join f in DVSC.FIRMMASTERs on c.Cont_FirmID equals f.FIRM_ID
                                      join r in DVSC.RELIGION_MASTER on c.Cont_ReligionID equals r.R_ID
                                      join n in DVSC.COUNTRY_MASTER on c.Cont_NationalityID equals n.COUNTRY_ID
                                      join cr in DVSC.CANCEL_REASON_MASTER on c.Cont_CancelReason equals cr.CR_ID
                                      join u in DVSC.PSU_MASTER on c.Cont_PSUunitID equals u.PSU_ID
                                      join sh in DVSC.SHOP_MASTER on c.Cont_ShopId equals sh.SHOP_ID
                                      where (c.Cont_CreatedDate >= fromdate && (c.Cont_CreatedDate <= todate)) 
                                      && ((c.Cont_FirmID == firmid) && (c.Cont_StateID == State)) && pass.Contains(c.Cont_PassType)
                                      orderby c.Cont_Id ascending
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
                                          c.Cont_UnitEmp,
                                          c.Cont_MinDate,
                                          c.Cont_DocID
                                      }).ToList();
                        if (table1.Count > 0)
                        {
                            Gv_Reports.DataSource = table1;
                            Gv_Reports.DataBind();
                            Session["table1"] = table1;
                            Session["table1Count"] = table1.Count;
                        }
                        else
                        {
                            Gv_Reports.DataSource = null;
                            Gv_Reports.DataBind();
                            Response.Write("<script> alert('No data found')</script>");
                        }
                    }
                    else
                    {
                        if (ddlsearch.SelectedItem.Text != "--SELECT--")
                        {
                            var table1 = (from c in DVSC.CONTRACTOR_DETAIL
                                          join d in DVSC.DESIGNATION_MASTER on c.Cont_DesignationID equals d.DESIGNATION_ID
                                          join s in DVSC.STATE_MASTER on c.Cont_StateID equals s.STATE_ID
                                          join f in DVSC.FIRMMASTERs on c.Cont_FirmID equals f.FIRM_ID
                                          join r in DVSC.RELIGION_MASTER on c.Cont_ReligionID equals r.R_ID
                                          join n in DVSC.COUNTRY_MASTER on c.Cont_NationalityID equals n.COUNTRY_ID
                                          join cr in DVSC.CANCEL_REASON_MASTER on c.Cont_CancelReason equals cr.CR_ID
                                          join u in DVSC.PSU_MASTER on c.Cont_PSUunitID equals u.PSU_ID
                                          join sh in DVSC.SHOP_MASTER on c.Cont_ShopId equals sh.SHOP_ID
                                          where ((c.Cont_Aadhaar == (txtSearch.Text.Trim()))
                            || (c.Cont_CardNo == (txtSearch.Text.Trim()))
                            || (c.Cont_Mobile == (txtSearch.Text.Trim())) ||
                            (c.Cont_RFIDNo == (txtSearch.Text.Trim())) ||
                            (c.Cont_Name.Contains(txtSearch.Text.Trim())) || (f.FIRM_NAME.Contains(txtSearch.Text.Trim())) ||
                            (c.Cont_DocID == Dockyardid)) && (c.Cont_CreatedDate >= fromdate && (c.Cont_CreatedDate <= todate)) && (c.Cont_FirmID == firmid) && pass.Contains(c.Cont_PassType)
                                          orderby c.Cont_Id ascending
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
                                              c.Cont_UnitEmp,
                                              c.Cont_MinDate,
                                              c.Cont_DocID
                                          }).ToList();
                            if (table1.Count > 0)
                            {
                                Gv_Reports.DataSource = table1;
                                Gv_Reports.DataBind();
                                Session["table1"] = table1;
                                Session["table1Count"] = table1.Count;
                            }
                            else
                            {
                                Gv_Reports.DataSource = null;
                                Gv_Reports.DataBind();
                                Response.Write("<script> alert('No data found')</script>");
                            }
                        }
                        else
                        {
                            var table1 = (from c in DVSC.CONTRACTOR_DETAIL
                                          join d in DVSC.DESIGNATION_MASTER on c.Cont_DesignationID equals d.DESIGNATION_ID
                                          join s in DVSC.STATE_MASTER on c.Cont_StateID equals s.STATE_ID
                                          join f in DVSC.FIRMMASTERs on c.Cont_FirmID equals f.FIRM_ID
                                          join r in DVSC.RELIGION_MASTER on c.Cont_ReligionID equals r.R_ID
                                          join n in DVSC.COUNTRY_MASTER on c.Cont_NationalityID equals n.COUNTRY_ID
                                          join cr in DVSC.CANCEL_REASON_MASTER on c.Cont_CancelReason equals cr.CR_ID
                                          join u in DVSC.PSU_MASTER on c.Cont_PSUunitID equals u.PSU_ID
                                          join sh in DVSC.SHOP_MASTER on c.Cont_ShopId equals sh.SHOP_ID
                                          where (c.Cont_CreatedDate >= fromdate && (c.Cont_CreatedDate <= todate)) && ((c.Cont_FirmID == firmid)) && pass.Contains(c.Cont_PassType)
                                          orderby c.Cont_Id ascending
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
                                              c.Cont_UnitEmp,
                                              c.Cont_MinDate,
                                              c.Cont_DocID
                                          }).ToList();
                            if (table1.Count > 0)
                            {
                                Gv_Reports.DataSource = table1;
                                Gv_Reports.DataBind();
                                Session["table1"] = table1;
                                Session["table1Count"] = table1.Count;
                            }
                            else
                            {
                                Gv_Reports.DataSource = null;
                                Gv_Reports.DataBind();
                                Response.Write("<script> alert('No data found')</script>");
                            }
                        }
                    }
                }
                else
                {
                    if (State > 0)
                    {
                        // Single State All firm c.Cont_ReligionID == Religion
                        var table1 = (from c in DVSC.CONTRACTOR_DETAIL
                                      join d in DVSC.DESIGNATION_MASTER on c.Cont_DesignationID equals d.DESIGNATION_ID
                                      join s in DVSC.STATE_MASTER on c.Cont_StateID equals s.STATE_ID
                                      join f in DVSC.FIRMMASTERs on c.Cont_FirmID equals f.FIRM_ID
                                      join r in DVSC.RELIGION_MASTER on c.Cont_ReligionID equals r.R_ID
                                      join n in DVSC.COUNTRY_MASTER on c.Cont_NationalityID equals n.COUNTRY_ID
                                      join cr in DVSC.CANCEL_REASON_MASTER on c.Cont_CancelReason equals cr.CR_ID
                                      join u in DVSC.PSU_MASTER on c.Cont_PSUunitID equals u.PSU_ID
                                      join sh in DVSC.SHOP_MASTER on c.Cont_ShopId equals sh.SHOP_ID
                                      where (c.Cont_CreatedDate >= fromdate && (c.Cont_CreatedDate <= todate)) 
                                      && ((c.Cont_StateID == State)) && pass.Contains(c.Cont_PassType)
                                      orderby c.Cont_Id ascending
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
                                          c.Cont_UnitEmp,
                                          c.Cont_MinDate,
                                          c.Cont_DocID
                                      }).ToList();

                        if (table1.Count > 0)
                        {
                            Gv_Reports.DataSource = table1;
                            Gv_Reports.DataBind();
                            Session["table1"] = table1;
                            Session["table1Count"] = table1.Count;
                        }
                        else
                        {
                            Gv_Reports.DataSource = null;
                            Gv_Reports.DataBind();
                            Session["table1Count"] = "0";
                            Response.Write("<script> alert('No data found')</script>");

                        }
                    }
                    else
                    {
                        // all religion all firm
                    }
                }
            }
            #endregion
            Gv_Reports.Visible = true;
            Gv_Demo.Visible = false;
            if (rdoAll.Checked == true)
            {
                lblCount.InnerText = "ALL PASS COUNT" + " :" + Session["table1Count"].ToString();
            }
            else
            {
                lblCount.InnerText = PassType + "  PASS COUNT :" + Session["table1Count"].ToString();
            }
        }

        public void Report8()
        {
            PassType = GetPassType();
            pass = PassType.Split(',').ToList();
            //Religion Wise Report
            #region
            if (ReligionReport == true)
            {
                if (ddlsearch.SelectedItem.Text != "--SELECT--")
                {
                    //All Firm and All Religion with Search Condition
                    var table1 = (from c in DVSC.CONTRACTOR_DETAIL
                                  join d in DVSC.DESIGNATION_MASTER on c.Cont_DesignationID equals d.DESIGNATION_ID
                                  join s in DVSC.STATE_MASTER on c.Cont_StateID equals s.STATE_ID
                                  join f in DVSC.FIRMMASTERs on c.Cont_FirmID equals f.FIRM_ID
                                  join r in DVSC.RELIGION_MASTER on c.Cont_ReligionID equals r.R_ID
                                  join n in DVSC.COUNTRY_MASTER on c.Cont_NationalityID equals n.COUNTRY_ID
                                  join cr in DVSC.CANCEL_REASON_MASTER on c.Cont_CancelReason equals cr.CR_ID
                                  join u in DVSC.PSU_MASTER on c.Cont_PSUunitID equals u.PSU_ID
                                  join sh in DVSC.SHOP_MASTER on c.Cont_ShopId equals sh.SHOP_ID
                                  where ((c.Cont_Aadhaar == (txtSearch.Text.Trim()))
                            || (c.Cont_CardNo == (txtSearch.Text.Trim()))
                            || (c.Cont_Mobile == (txtSearch.Text.Trim())) ||
                            (c.Cont_RFIDNo == (txtSearch.Text.Trim())) ||
                            (c.Cont_Name.Contains(txtSearch.Text.Trim())) || (f.FIRM_NAME.Contains(txtSearch.Text.Trim())) ||
                            (c.Cont_DocID == Dockyardid)) && (c.Cont_CreatedDate >= fromdate && (c.Cont_CreatedDate <= todate)) && pass.Contains(c.Cont_PassType)
                                  orderby c.Cont_Id ascending
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
                                      c.Cont_UnitEmp,
                                      c.Cont_MinDate,
                                      c.Cont_DocID
                                  }).ToList();
                    if (table1.Count > 0)
                    {
                        Gv_Reports.DataSource = table1;
                        Gv_Reports.DataBind();
                        Session["table1"] = table1;
                        Session["table1Count"] = table1.Count;
                    }
                    else
                    {
                        Gv_Reports.DataSource = null;
                        Gv_Reports.DataBind();
                        Response.Write("<script> alert('No data found')</script>");
                    }
                }
                else
                {
                    //All Data With No Condition
                    var table1 = (from c in DVSC.CONTRACTOR_DETAIL
                                  join d in DVSC.DESIGNATION_MASTER on c.Cont_DesignationID equals d.DESIGNATION_ID
                                  join s in DVSC.STATE_MASTER on c.Cont_StateID equals s.STATE_ID
                                  join f in DVSC.FIRMMASTERs on c.Cont_FirmID equals f.FIRM_ID
                                  join r in DVSC.RELIGION_MASTER on c.Cont_ReligionID equals r.R_ID
                                  join n in DVSC.COUNTRY_MASTER on c.Cont_NationalityID equals n.COUNTRY_ID
                                  join cr in DVSC.CANCEL_REASON_MASTER on c.Cont_CancelReason equals cr.CR_ID
                                  join u in DVSC.PSU_MASTER on c.Cont_PSUunitID equals u.PSU_ID
                                  join sh in DVSC.SHOP_MASTER on c.Cont_ShopId equals sh.SHOP_ID
                                  where (c.Cont_CreatedDate >= fromdate && (c.Cont_CreatedDate <= todate)) && pass.Contains(c.Cont_PassType)
                                  orderby c.Cont_Id ascending
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
                                      c.Cont_UnitEmp,
                                      c.Cont_MinDate,
                                      c.Cont_DocID
                                  }).ToList();
                    if (table1.Count > 0)
                    {
                        Gv_Reports.DataSource = table1;
                        Gv_Reports.DataBind();
                        Session["table1"] = table1;
                        Session["table1Count"] = table1.Count;
                    }
                    else
                    {
                        Gv_Reports.DataSource = null;
                        Gv_Reports.DataBind();
                        Response.Write("<script> alert('No data found')</script>");
                    }
                }
            }
            else
            {
                if (firmid > 0)
                {
                    // Single Firm All Religion
                    if (Religion > 0)
                    {
                        var table1 = (from c in DVSC.CONTRACTOR_DETAIL
                                      join d in DVSC.DESIGNATION_MASTER on c.Cont_DesignationID equals d.DESIGNATION_ID
                                      join s in DVSC.STATE_MASTER on c.Cont_StateID equals s.STATE_ID
                                      join f in DVSC.FIRMMASTERs on c.Cont_FirmID equals f.FIRM_ID
                                      join r in DVSC.RELIGION_MASTER on c.Cont_ReligionID equals r.R_ID
                                      join n in DVSC.COUNTRY_MASTER on c.Cont_NationalityID equals n.COUNTRY_ID
                                      join cr in DVSC.CANCEL_REASON_MASTER on c.Cont_CancelReason equals cr.CR_ID
                                      join u in DVSC.PSU_MASTER on c.Cont_PSUunitID equals u.PSU_ID
                                      join sh in DVSC.SHOP_MASTER on c.Cont_ShopId equals sh.SHOP_ID
                                      where (c.Cont_CreatedDate >= fromdate && (c.Cont_CreatedDate <= todate)) && ((c.Cont_FirmID == firmid) && (c.Cont_ReligionID == Religion)) && pass.Contains(c.Cont_PassType)
                                      orderby c.Cont_Id ascending
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
                                          c.Cont_UnitEmp,
                                          c.Cont_MinDate,
                                          c.Cont_DocID
                                      }).ToList();
                        if (table1.Count > 0)
                        {
                            Gv_Reports.DataSource = table1;
                            Gv_Reports.DataBind();
                            Session["table1"] = table1;
                            Session["table1Count"] = table1.Count;
                        }
                        else
                        {
                            Gv_Reports.DataSource = null;
                            Gv_Reports.DataBind();
                            Response.Write("<script> alert('No data found')</script>");
                        }
                    }
                    else
                    {
                        if (ddlsearch.SelectedItem.Text != "--SELECT--")
                        {
                            var table1 = (from c in DVSC.CONTRACTOR_DETAIL
                                          join d in DVSC.DESIGNATION_MASTER on c.Cont_DesignationID equals d.DESIGNATION_ID
                                          join s in DVSC.STATE_MASTER on c.Cont_StateID equals s.STATE_ID
                                          join f in DVSC.FIRMMASTERs on c.Cont_FirmID equals f.FIRM_ID
                                          join r in DVSC.RELIGION_MASTER on c.Cont_ReligionID equals r.R_ID
                                          join n in DVSC.COUNTRY_MASTER on c.Cont_NationalityID equals n.COUNTRY_ID
                                          join cr in DVSC.CANCEL_REASON_MASTER on c.Cont_CancelReason equals cr.CR_ID
                                          join u in DVSC.PSU_MASTER on c.Cont_PSUunitID equals u.PSU_ID
                                          join sh in DVSC.SHOP_MASTER on c.Cont_ShopId equals sh.SHOP_ID
                                          where ((c.Cont_Aadhaar == (txtSearch.Text.Trim()))
                            || (c.Cont_CardNo == (txtSearch.Text.Trim()))
                            || (c.Cont_Mobile == (txtSearch.Text.Trim())) ||
                            (c.Cont_RFIDNo == (txtSearch.Text.Trim())) ||
                            (c.Cont_Name.Contains(txtSearch.Text.Trim())) || (f.FIRM_NAME.Contains(txtSearch.Text.Trim())) ||
                            (c.Cont_DocID == Dockyardid)) && (c.Cont_CreatedDate >= fromdate && (c.Cont_CreatedDate <= todate)) && (c.Cont_FirmID == firmid) && pass.Contains(c.Cont_PassType)
                                          orderby c.Cont_Id ascending
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
                                              c.Cont_UnitEmp,
                                              c.Cont_MinDate,
                                              c.Cont_DocID
                                          }).ToList();
                            if (table1.Count > 0)
                            {
                                Gv_Reports.DataSource = table1;
                                Gv_Reports.DataBind();
                                Session["table1"] = table1;
                                Session["table1Count"] = table1.Count;
                            }
                            else
                            {
                                Gv_Reports.DataSource = null;
                                Gv_Reports.DataBind();
                                Response.Write("<script> alert('No data found')</script>");
                            }
                        }
                        else
                        {
                            var table1 = (from c in DVSC.CONTRACTOR_DETAIL
                                          join d in DVSC.DESIGNATION_MASTER on c.Cont_DesignationID equals d.DESIGNATION_ID
                                          join s in DVSC.STATE_MASTER on c.Cont_StateID equals s.STATE_ID
                                          join f in DVSC.FIRMMASTERs on c.Cont_FirmID equals f.FIRM_ID
                                          join r in DVSC.RELIGION_MASTER on c.Cont_ReligionID equals r.R_ID
                                          join n in DVSC.COUNTRY_MASTER on c.Cont_NationalityID equals n.COUNTRY_ID
                                          join cr in DVSC.CANCEL_REASON_MASTER on c.Cont_CancelReason equals cr.CR_ID
                                          join u in DVSC.PSU_MASTER on c.Cont_PSUunitID equals u.PSU_ID
                                          join sh in DVSC.SHOP_MASTER on c.Cont_ShopId equals sh.SHOP_ID
                                          where (c.Cont_CreatedDate >= fromdate && (c.Cont_CreatedDate <= todate)) && ((c.Cont_FirmID == firmid)) && pass.Contains(c.Cont_PassType)
                                          orderby c.Cont_Id ascending
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
                                              c.Cont_UnitEmp,
                                              c.Cont_MinDate,
                                              c.Cont_DocID
                                          }).ToList();
                            if (table1.Count > 0)
                            {
                                Gv_Reports.DataSource = table1;
                                Gv_Reports.DataBind();
                                Session["table1"] = table1;
                                Session["table1Count"] = table1.Count;
                            }
                            else
                            {
                                Gv_Reports.DataSource = null;
                                Gv_Reports.DataBind();
                                Response.Write("<script> alert('No data found')</script>");
                            }
                        }
                    }
                }
                else
                {
                    if (Religion > 0)
                    {
                        // Single Religion All firm
                        var table1 = (from c in DVSC.CONTRACTOR_DETAIL
                                      join d in DVSC.DESIGNATION_MASTER on c.Cont_DesignationID equals d.DESIGNATION_ID
                                      join s in DVSC.STATE_MASTER on c.Cont_StateID equals s.STATE_ID
                                      join f in DVSC.FIRMMASTERs on c.Cont_FirmID equals f.FIRM_ID
                                      join r in DVSC.RELIGION_MASTER on c.Cont_ReligionID equals r.R_ID
                                      join n in DVSC.COUNTRY_MASTER on c.Cont_NationalityID equals n.COUNTRY_ID
                                      join cr in DVSC.CANCEL_REASON_MASTER on c.Cont_CancelReason equals cr.CR_ID
                                      join u in DVSC.PSU_MASTER on c.Cont_PSUunitID equals u.PSU_ID
                                      join sh in DVSC.SHOP_MASTER on c.Cont_ShopId equals sh.SHOP_ID
                                      where (c.Cont_CreatedDate >= fromdate && (c.Cont_CreatedDate <= todate)) && ((c.Cont_ReligionID == Religion)) && pass.Contains(c.Cont_PassType)
                                      orderby c.Cont_Id ascending
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
                                          c.Cont_UnitEmp,
                                          c.Cont_MinDate,
                                          c.Cont_DocID
                                      }).ToList();
                        if (table1.Count > 0)
                        {
                            Gv_Reports.DataSource = table1;
                            Gv_Reports.DataBind();
                            Session["table1"] = table1;
                            Session["table1Count"] = table1.Count;
                        }
                        else
                        {
                            Gv_Reports.DataSource = null;
                            Gv_Reports.DataBind();
                            Response.Write("<script> alert('No data found')</script>");
                        }
                    }
                    else
                    {
                        // all religion all firm
                    }
                }
            }
            #endregion
            Gv_Reports.Visible = true;
            Gv_Demo.Visible = false;
            if (rdoAll.Checked == true)
            {
                lblCount.InnerText = "ALL PASS COUNT" + " :" + Session["table1Count"].ToString();
            }
            else
            {
                lblCount.InnerText = PassType + "  PASS COUNT :" + Session["table1Count"].ToString();
            }
        }

        public void Report9()
        {
            PassType = GetPassType();
            //Total In Out Report
            #region
            string PassNo = "";
            string Firm = "";
            if (txtSearch.Text == "")
            {
                txtSearch.Text = "0";
            }
            if (ddlsearch.SelectedItem.Text == "FIRM")
            {
                PassNo = "0";
                Firm = txtSearch.Text.Trim();
            }
            if (ddlsearch.SelectedItem.Text == "PASS NO")
            {
                Firm = "0";
                PassNo = txtSearch.Text.Trim();
            }
            if (ddlFirm.SelectedItem.Text != "--ALL--")
            {
                Firm = ddlFirm.SelectedItem.Text.Trim();
                PassNo = "0";
            }
            DataSet ds = new DataSet("TimeRanges");
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["VMSConnection"].ConnectionString))
            {
                SqlCommand sqlComm = new SqlCommand("Rpt_InOut_DetailsReport", conn);
                sqlComm.Parameters.AddWithValue("@fromdate", fromdate);
                sqlComm.Parameters.AddWithValue("@todate", todate);
                sqlComm.Parameters.AddWithValue("@Cardno", PassNo);
                sqlComm.Parameters.AddWithValue("@FirmName", Firm);
                //sqlComm.Parameters.AddWithValue("@PassType", PassType);
                sqlComm.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = sqlComm;
                da.Fill(ds);
                Gv_Demo.DataSource = ds.Tables[0];
                Gv_Demo.DataBind();
               // Session["table1"] = ds.Tables[0];
            }
            #endregion
            Gv_Reports.Visible = false;
            Gv_Demo.Visible = true;
            //<asp:BoundField HeaderText="PASS NO" DataField="Cont_CardNo" />
            //      <asp:BoundField HeaderText="DOCKYARD ID NO" DataField="Cont_DocID" />
            //       <asp:BoundField HeaderText="TranDate" DataField="TranDate" />
            //      <asp:BoundField HeaderText="IN TIME" DataField="InTime" />
            //      <asp:BoundField HeaderText="OUT TIME" DataField="OutTime" />
            //      <asp:BoundField HeaderText="FULL NAME" DataField="Cont_Name" />
            //      <asp:BoundField HeaderText="MOBILE NO" DataField="Cont_Mobile" />
            //      <asp:BoundField HeaderText="FIRM NAME" DataField="FIRM_NAME" />
            //      <asp:BoundField HeaderText="PVC NO" DataField="Cont_PVCNO" />
            //      <asp:BoundField HeaderText="PVC VALIDITY" DataField="Cont_PVCValidity" />
            //      <asp:BoundField HeaderText="RFID NO" DataField="Cont_RFIDNo" />
            //      <asp:BoundField HeaderText="RFID VALIDITY" DataField="Cont_RFIDValidity" />

        }
        public void Report10()
        {
            PassType = GetPassType();
            //Total In Out Report

            if (rdoAll.Checked == true)
            {
                PassType = "All";
            }
            #region
            string PassNo = "";
            string Firm = "";
            if (txtSearch.Text == "")
            {
                txtSearch.Text = "0";
            }
            if (ddlsearch.SelectedItem.Text == "FIRM")
            {
                PassNo = "0";
                Firm = txtSearch.Text.Trim();
            }
            if (ddlsearch.SelectedItem.Text == "PASS NO")
            {
                Firm = "0";
                PassNo = txtSearch.Text.Trim();
            }
            if (ddlFirm.SelectedItem.Text != "--ALL--")
            {
                Firm = ddlFirm.SelectedItem.Text.Trim();
                PassNo = "0";
            }

            DataSet ds = new DataSet("TimeRanges");

            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["VMSConnection"].ConnectionString))
            {
                SqlCommand sqlComm = new SqlCommand("Rpt_TodaysPendingOutList", conn);
                sqlComm.Parameters.AddWithValue("@fromdate", fromdate);
                sqlComm.Parameters.AddWithValue("@todate", todate);
                sqlComm.Parameters.AddWithValue("@Cardno", PassNo);
                sqlComm.Parameters.AddWithValue("@FirmName", Firm);
                sqlComm.Parameters.AddWithValue("@PassType", PassType); 
                sqlComm.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = sqlComm;
                da.Fill(ds);
                Gv_Demo.DataSource = ds.Tables[0];
                Gv_Demo.DataBind();
               // Session["table1"] = ds.Tables[0];
            }
            #endregion
            Gv_Reports.Visible = false;
            Gv_Demo.Visible = true;

            if (ds.Tables[0] !=null)
                    lblCount.InnerText = " Pending Out " + " :" + ds.Tables[0].Rows.Count.ToString();

        }

        public void Report11()
        {
            PassType = GetPassType();
            //Total In Out Report
            #region
            string PassNo = "";
            string Firm = "";
            if (txtSearch.Text == "")
            {
                txtSearch.Text = "0";
            }
            if (ddlsearch.SelectedItem.Text == "FIRM")
            {
                PassNo = "0";
                Firm = txtSearch.Text.Trim();
            }
            if (ddlsearch.SelectedItem.Text == "PASS NO")
            {
                Firm = "0";
                PassNo = txtSearch.Text.Trim();
            }
            if (ddlFirm.SelectedItem.Text != "--ALL--")
            {
                Firm = ddlFirm.SelectedItem.Text.Trim();
                PassNo = "0";
            }

            DataSet ds = new DataSet("TimeRanges");

            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["VMSConnection"].ConnectionString))
            {
                SqlCommand sqlComm = new SqlCommand("Rpt_Activated_TigerGate", conn);
               
                sqlComm.Parameters.AddWithValue("@Cardno", PassNo);
                sqlComm.Parameters.AddWithValue("@FirmName", Firm);
                //sqlComm.Parameters.AddWithValue("@PassType", PassType);
                sqlComm.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = sqlComm;
                da.Fill(ds);
                Gv_Demo.DataSource = ds.Tables[0];
                Gv_Demo.DataBind();
                // Session["table1"] = ds.Tables[0];
            }
            #endregion
            Gv_Reports.Visible = false;
            Gv_Demo.Visible = true;

            if (ds.Tables[0] != null)
                lblCount.InnerText = "Activated List " + " :" + ds.Tables[0].Rows.Count.ToString();

        }
        public void Report12()
        {
            PassType = GetPassType();
            //Total In Out Report
            #region
            string PassNo = "";
            string Firm = "";
            if (txtSearch.Text == "")
            {
                txtSearch.Text = "0";
            }
            if (ddlsearch.SelectedItem.Text == "FIRM")
            {
                PassNo = "0";
                Firm = txtSearch.Text.Trim();
            }
            if (ddlsearch.SelectedItem.Text == "PASS NO")
            {
                Firm = "0";
                PassNo = txtSearch.Text.Trim();
            }
            if (ddlFirm.SelectedItem.Text != "--ALL--")
            {
                Firm = ddlFirm.SelectedItem.Text.Trim();
                PassNo = "0";
            }

            DataSet ds = new DataSet("TimeRanges");

            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["VMSConnection"].ConnectionString))
            {
                SqlCommand sqlComm = new SqlCommand("Rpt_Activated_MusterGate", conn);
               
                sqlComm.Parameters.AddWithValue("@Cardno", PassNo);
                sqlComm.Parameters.AddWithValue("@FirmName", Firm);
                //sqlComm.Parameters.AddWithValue("@PassType", PassType);
                sqlComm.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = sqlComm;
                da.Fill(ds);
                Gv_Demo.DataSource = ds.Tables[0];
                Gv_Demo.DataBind();
                // Session["table1"] = ds.Tables[0];
            }
            #endregion
            Gv_Reports.Visible = false;
            Gv_Demo.Visible = true;

            if (ds.Tables[0] != null)
                lblCount.InnerText = "Activated List " + " :" + ds.Tables[0].Rows.Count.ToString();

        }
        public string GetPassType()
        {
            string PASSTYPE = "";
            if (rdoAll.Checked == true)
            {
                PASSTYPE = "CONTRACTOR,ESCORTED,BANK,CB,LABOUR";
            }
            if (rdoContractor.Checked == true)
            {
                PASSTYPE = "CONTRACTOR";
            }
            if (rdoEscorted.Checked == true)
            {
                PASSTYPE = "ESCORTED";
            }
            if (rdoBank.Checked == true)
            {
                PASSTYPE = "BANK";
            }
            if (rdoShips.Checked == true)
            {
                PASSTYPE = "CB";
            }
            if (rdoLabour.Checked == true)
            {
                PASSTYPE = "LABOUR";
            }
            return PASSTYPE;
        }

        private void SetMasterDDL()
        {
            if (rdoAll.Text == "ALL" || rdoAll.Text == "CONTRACTOR" || rdoAll.Text == "ESCORTED" || rdoAll.Text == "LABOUR")
            {
                ddl.BindFirmddl1(ref ddlFirm);
            }
            else if (rdoAll.Text == "BANK")//BANK
            {
                ddl.BindFirmddl1(ref ddlFirm);
            }
            else if (rdoAll.Text == "SHIPS")//SHIPS
            {
                ddl.BindShopddl(ref ddlFirm);//BindShopddl
            }
            //CONTRACTOR //ESCORTED//LABOUR
        }
        protected void rdoAll_CheckedChanged(object sender, EventArgs e)
        {
            //SetMasterDDL();
            if (rdoAll.Checked == true || rdoContractor.Checked == true || rdoEscorted.Checked == true || rdoLabour.Checked == true)
            {
                ddl.BindFirmddl1(ref ddlFirm);
            }
        }

        protected void rdoContractor_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoAll.Checked == true || rdoContractor.Checked == true || rdoEscorted.Checked == true || rdoLabour.Checked == true)
            {
                ddl.BindFirmddl1(ref ddlFirm);
            }
        }

        protected void rdoEscorted_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoAll.Checked == true || rdoContractor.Checked == true || rdoEscorted.Checked == true || rdoLabour.Checked == true)
            {
                ddl.BindFirmddl1(ref ddlFirm);
            }
        }

        protected void rdoBank_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoAll.Checked == true || rdoContractor.Checked == true || rdoEscorted.Checked == true || rdoLabour.Checked == true)
            {
                ddl.BindFirmddl1(ref ddlFirm);
            }
        }

        protected void rdoShips_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoShips.Checked == true)//SHIPS
            {
                ddl.BindShopddlReport(ref ddlFirm);//BindShopddl
                chkunit.Checked = true;
            }
        }

        protected void rdoLabour_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoAll.Checked == true || rdoContractor.Checked == true || rdoEscorted.Checked == true || rdoLabour.Checked == true)
            {
                ddl.BindFirmddl1(ref ddlFirm);
            }
        }

        
    }
}