using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Linq.SqlClient;
using EntityFrameworkDBF.ServiceReference1;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Net.NetworkInformation;

namespace EntityFrameworkDBF.CANCELLATION_MODULE
{
    public partial class CANCEL_HOME : System.Web.UI.Page
    {
        VMSEntities DVSC = new VMSEntities();
        CONTRACTOR_DETAIL Cont = new CONTRACTOR_DETAIL();
        CANCEL_LOG cl = new CANCEL_LOG();
        DropDownFunction ddl = new DropDownFunction();

        SmartAccessEntities smartAccess = new SmartAccessEntities();
        int DockyardidNo = 0;
        ServiceReference1.SmartWebServiceClient ObjServiceClient = new ServiceReference1.SmartWebServiceClient();

        CardDetails objCardDetails = new CardDetails();
        CardStatus objCardStatus = null;
        //CardStatus[] objCardStatus = null;
        TemplateStatus[] ObjTemplateStatus = null;


        Label Name, Mindate, Dockyardid, RfidCardNo, RfidNo;
        string Controllerlist, CardCommand, TableName, controllerofflinelist;
        int ReaderCount = 0;
        int DeviceTemplateID, ActivateCardCommandID = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                spnDate.InnerText = DateTime.Now.Date.ToString("dd-MMM-yyyy").Replace("-", " ").ToUpper();
                ddl.BindCancelReasonddl(ref ddlLossReason);
                VisiBilityOff();
                lblLossReason.Visible = false;
                ddlLossReason.Visible = false;
                btnCancelPass.Enabled = false;
                //ddlLossReason.Enabled = false;
            }
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

        public void BindGrid()
        {
            if (ddlsearch.SelectedItem.Text == "DOCKYARD ID NO")
            {
                DockyardidNo = Convert.ToInt32(txtSearch.Text.Trim());
            }
            try
            {
                if (ddlsearch.SelectedItem.Text != "--SELECT--" && !string.IsNullOrEmpty(txtSearch.Text))
                {
                    var query = (from x in DVSC.CONTRACTOR_DETAIL
                                 join f in DVSC.FIRMMASTERs on x.Cont_FirmID equals f.FIRM_ID
                                 where (x.Cont_CardNo == (txtSearch.Text.Trim()) || (x.Cont_Aadhaar == (txtSearch.Text.Trim()))
                                 || (x.Cont_Name.Contains(txtSearch.Text.Trim())) || (x.Cont_RFIDNo == (txtSearch.Text.Trim()))
                                 || (x.Cont_Mobile == (txtSearch.Text.Trim())) || (x.Cont_DocID == DockyardidNo) && x.Cont_CancelFLag == "N")
                                 select new
                                 {
                                     x.Cont_DocID,   // Additional by Pranam
                                     x.Cont_Id,
                                     x.Cont_Name,
                                     f.FIRM_NAME,
                                     x.Cont_RFIDValidity,
                                     x.Cont_CardNo,
                                     x.Cont_Aadhaar,
                                     x.Cont_RFIDNo,
                                     x.Cont_Delete_Flag,
                                     x.Cont_CancelFLag
                                 }).ToList();
                    Gv_CancelViewData.DataSource = query;
                    Gv_CancelViewData.DataBind();
                    lblLossReason.Visible = true;
                    ddlLossReason.Visible = true;
                    ddl.BindCancelReasonddl(ref ddlLossReason);
                    btnCancelPass.Enabled = true;
                    LossCount();
                }
            }
            catch (Exception)
            {
                Response.Write("<script> alert('Error while loading records Please try after some time.')</script>");
            }
        }

        public void BindGridAfterCancel()
        {
            if (ddlsearch.SelectedItem.Text == "DOCKYARD ID NO")
            {
                DockyardidNo = Convert.ToInt32(txtSearch.Text.Trim());
            }
            try
            {
                if (ddlsearch.SelectedItem.Text != "--SELECT--" && !string.IsNullOrEmpty(txtSearch.Text))
                {
                    var query = (from x in DVSC.CONTRACTOR_DETAIL
                                 join f in DVSC.FIRMMASTERs on x.Cont_FirmID equals f.FIRM_ID
                                 where (x.Cont_CardNo == (txtSearch.Text.Trim()) || (x.Cont_Aadhaar == (txtSearch.Text.Trim()))
                                 || (x.Cont_Name.Contains(txtSearch.Text.Trim())) || (x.Cont_RFIDNo == (txtSearch.Text.Trim()))
                                 || (x.Cont_Mobile == (txtSearch.Text.Trim())) || (x.Cont_DocID == DockyardidNo) && ((x.Cont_CancelFLag == "LOSS") || (x.Cont_CancelFLag == "CANCEL")))
                                 select new
                                 {
                                     x.Cont_DocID,   // Additional by Pranam
                                     x.Cont_Id,
                                     x.Cont_Name,
                                     f.FIRM_NAME,
                                     x.Cont_RFIDValidity,
                                     x.Cont_CardNo,
                                     x.Cont_Aadhaar,
                                     x.Cont_RFIDNo,
                                     x.Cont_Delete_Flag,
                                     x.Cont_CancelFLag
                                 }).ToList();
                    Gv_CancelViewData.DataSource = query;
                    Gv_CancelViewData.DataBind();
                    lblLossReason.Visible = true;
                    ddlLossReason.Visible = true;
                    ddl.BindCancelReasonddl(ref ddlLossReason);
                    btnCancelPass.Enabled = true;
                }
            }
            catch (Exception)
            {
                Response.Write("<script> alert('Error while loading records Please try after some time.')</script>");
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                BindGrid();
                VisiBilityOff();
            }
            catch (Exception)
            {
                Response.Write("<script> alert('Error while loading records Please try after some time.')</script>");
            }
        }

        protected void ddlLossReason_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlLossReason.SelectedItem.Text == "ON LOSS")
                {
                    VisiBilityOn();
                }
                else
                {
                    VisiBilityOff();
                }
            }
            catch (Exception)
            {
                Response.Write("<script> alert('Error while loading data Please try after sometime.')</script>");
            }
        }

        public void VisiBilityOn()
        {
            txtDateOfLoss.Visible = true;
            txtPlaceOfLoss.Visible = true;
            txtFine.Visible = true;
            txtFir.Visible = true;
            lblDateOFLoss.Visible = true;
            lblPlaceOfLoss.Visible = true;
            lblFine.Visible = true;
            lblFir.Visible = true;

        }

        public void VisiBilityOff()
        {
            txtDateOfLoss.Visible = false;
            txtPlaceOfLoss.Visible = false;
            txtFine.Visible = false;
            txtFir.Visible = false;
            lblDateOFLoss.Visible = false;
            lblPlaceOfLoss.Visible = false;
            lblFine.Visible = false;
            lblFir.Visible = false;

        }

        protected void btnCancelPass_Click(object sender, EventArgs e)
        {
            controllerofflinelist = string.Empty;
            if (ddlsearch.SelectedItem.Text == "DOCKYARD ID NO")
            {
                DockyardidNo = Convert.ToInt32(txtSearch.Text.Trim());
            }
            try
            {
                foreach (GridViewRow row in Gv_CancelViewData.Rows)
                {
                    CheckBox chk = (CheckBox)row.FindControl("chkSelect");
                    if (chk.Checked)
                    {
                        int ConID = Convert.ToInt32(Gv_CancelViewData.DataKeys[row.RowIndex].Values[0].ToString());
                        var ContDetail = (from x in DVSC.CONTRACTOR_DETAIL where x.Cont_Id == ConID select x).First();
                        Cont = DVSC.CONTRACTOR_DETAIL.First(x => x.Cont_Id == ConID);


                        if ((ddlLossReason.SelectedIndex) > 0)
                        {
                            Cont.Cont_Delete_Flag = "Y";
                            Cont.Cont_CancelReason = Convert.ToInt32(ddlLossReason.SelectedItem.Value);

                            if (ddlLossReason.SelectedItem.Text.ToUpper()=="ON LOSS") //if (ddlLossReason.SelectedItem.Value == "2")
                            {
                                Cont.Cont_CancelFLag = "LOSS";
                            }
                            else
                            {
                                Cont.Cont_CancelFLag = "CANCEL";
                            }

                            if (!string.IsNullOrEmpty(txtDateOfLoss.Text))
                                Cont.Cont_DateOFLoss = Convert.ToDateTime(txtDateOfLoss.Text.Trim());
                            else
                                Cont.Cont_DateOFLoss = System.DateTime.Now;
                            Cont.Cont_CancelDate = System.DateTime.Now;
                            if (!string.IsNullOrEmpty(txtFine.Text.Trim()))
                                Cont.Cont_Fine = txtFine.Text.Trim();
                            else
                                Cont.Cont_Fine = "NA";
                            if (!string.IsNullOrEmpty(txtFir.Text.Trim()))
                                Cont.Cont_Fir = txtFir.Text.Trim();
                            else
                                Cont.Cont_Fir = "NA";
                            if (!string.IsNullOrEmpty(txtPlaceOfLoss.Text.Trim()))
                                Cont.Cont_PlaceOfLoss = txtPlaceOfLoss.Text.Trim();
                            else
                                Cont.Cont_PlaceOfLoss = "NA";

                            DVSC.SaveChanges();
                            CancelLog();

                            Response.Write("<script> alert('Pass has been cancelled for the person " + ContDetail.Cont_Name + ".')</script>");
                            //BindGridAfterCancel();
                            //  LossCount();

                            // DeActivation
                            ReaderCount = 0;
                            RfidNo = (Label)row.FindControl("lblCRfidNo");
                            Name = (Label)row.FindControl("lblCName");
                            Dockyardid = (Label)row.FindControl("lblDockyardid");

                            objCardDetails.CardNo = RfidNo.Text;
                            objCardDetails.Name = Name.Text;
                            objCardDetails.AuthCode = "6";
                           
                           if (smartAccess.Controllers.Count() > 0)
                            {

                                var ControllerNolst = (from em in smartAccess.Controllers
                                                       select new { em.ControllerNo, em.ControllerName, em.IPAddress }).ToList();


                                foreach (var Cntrl in ControllerNolst)
                                {

                                    objCardDetails.ControllerNo = Cntrl.ControllerNo.ToString();

                                    //objCardDetails.Expiry = Convert.ToDateTime(Mindate.Text);//Convert.ToDateTime("2018/10/01");//Convert.ToDateTime(txtrfidValidity.Text.Trim());
                                    Ping myPing = new Ping();
                                    
                                    PingReply reply = myPing.Send(Cntrl.IPAddress, Convert.ToInt16(ConfigurationManager.AppSettings["PingTime"].ToString()));

                                    if (reply.Status == IPStatus.Success)
                                    {
                                        objCardDetails.NoOfReader = smartAccess.Controllers.Count();
                                        objCardStatus = ObjServiceClient.DeactivateCard(objCardDetails);

                                        try
                                        {
                                            TableName = "DeviceActivateCard";
                                            CardCommand = "DeActivate";

                                            ActivateCardCommandID = getcardActiveLastRecord(objCardDetails);
                                            insertintoDeviceRequestStatus(objCardDetails, row, ActivateCardCommandID, TableName, CardCommand);

                                            DDeaciiveSuccess.Visible = true;
                                            DDeaciiveFail.Visible = false;

                                        }
                                        catch (Exception ex)
                                        {
                                            DDeaciiveSuccess.Visible = false;
                                            DDeaciiveFail.Visible = true;
                                            
                                        }
                                    }
                                    else
                                    {
                                        controllerofflinelist += Cntrl.ControllerName + ",";
                                    }
                                }
                                Controllerlist = string.Empty;
                            }
                            
                            BindGridAfterCancel();
                        }
                        else
                        {
                            Response.Write("<script> alert('Please Select CancelReason for" + ContDetail.Cont_Name + ".')</script>");
                        }

                    }
                    else
                    {
                        Response.Write("<script> alert('Please select atleast one record.')</script>");
                    }
                }
            }
            catch (Exception)
            {
                Response.Write("<script> alert('Error while loading records Please try after some time.')</script>");
            }


            if (controllerofflinelist.Trim() != string.Empty)
            {
                controllerofflinelist = controllerofflinelist.Trim().Remove(controllerofflinelist.Length - 1);
                Response.Write("<script> alert('CONTROLLERS " + controllerofflinelist + " IS OFFLINE')</script>");
            }
        }

        protected void chkSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox ChkBoxHeader = (CheckBox)Gv_CancelViewData.HeaderRow.FindControl("chkSelectAll");
            foreach (GridViewRow row in Gv_CancelViewData.Rows)
            {
                CheckBox CheckboxRows = (CheckBox)row.FindControl("chkSelect");
                if (ChkBoxHeader.Checked == true)
                {
                    CheckboxRows.Checked = true;
                }
                else
                {
                    CheckboxRows.Checked = false;
                }
            }
        }

        protected void chkSelect_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox ChkBoxHeader = (CheckBox)Gv_CancelViewData.HeaderRow.FindControl("chkSelectAll");
            foreach (GridViewRow row in Gv_CancelViewData.Rows)
            {
                CheckBox CheckboxRows = (CheckBox)row.FindControl("chkSelect");
                Label lblCPassno = (Label)row.FindControl("lblCPassno");
                var Log_info = (from x in DVSC.CANCEL_LOG.Where(x => x.Cancel_PassNo == lblCPassno.Text.Trim() && x.Cancel_Cr == "ON LOSS") select new { x.Cancel_PassNo }).ToList();
                if (Log_info.Count > 0)
                {
                    if (CheckboxRows.Checked == true)
                    {
                        if (Log_info.Count > 0)
                        {
                            Response.Write("<script> alert('This Card id " + lblCPassno.Text.Trim() + " already misplaced once.')</script>");
                        }
                    }
                }
            }
        }

        protected void Gv_CancelViewData_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label txtCancelFlag = (e.Row.FindControl("lblCancelFlag") as Label);
                CheckBox Chkcheck = (e.Row.FindControl("chkSelect") as CheckBox);
                foreach (TableCell cell in e.Row.Cells)
                {
                    if ((txtCancelFlag.Text.Trim() == "CANCEL"))
                    {
                        cell.BackColor = System.Drawing.Color.OrangeRed;
                        Chkcheck.Visible = false;
                        e.Row.ForeColor = System.Drawing.Color.White;
                    }
                }
            }
        }

        public void CancelLog()
        {
            if (ddlLossReason.SelectedItem.Text.Trim() != "ON LOSS")
            {
                cl.Cancel_Cr = ddlLossReason.SelectedItem.Text.Trim();
                cl.Cancel_Date = Cont.Cont_CancelDate;
                cl.Cancel_Fine = "NA";
                cl.Cancel_Firm = Cont.Cont_FirmID;
                cl.Cancel_Name = Cont.Cont_Name;
                cl.Cancel_PassNo = Cont.Cont_CardNo;
                cl.Cancel_PlaceLoss = "NA";
                cl.Dockyard_id = Cont.Cont_DocID;
                cl.Loss_Date = System.DateTime.Now;
                cl.Loss_Fir = "NA";
            }
            else
            {
                cl.Cancel_Cr = ddlLossReason.SelectedItem.Text.Trim();
                cl.Cancel_Date = Cont.Cont_CancelDate;
                cl.Cancel_Fine = txtFine.Text.Trim();
                cl.Cancel_Firm = Cont.Cont_FirmID;
                cl.Cancel_Name = Cont.Cont_Name;
                cl.Cancel_PassNo = Cont.Cont_CardNo;
                cl.Cancel_PlaceLoss = txtPlaceOfLoss.Text.Trim();
                cl.Dockyard_id = Cont.Cont_DocID;
                cl.Loss_Date = Convert.ToDateTime(txtDateOfLoss.Text.Trim());
                cl.Loss_Fir = txtFir.Text.Trim();
            }
            DVSC.CANCEL_LOG.AddObject(cl);
            DVSC.SaveChanges();
        }

        public void LossCount()
        {
            try
            {
                foreach (GridViewRow row in Gv_CancelViewData.Rows)
                {
                    Label lblCPassno = (Label)row.FindControl("lblCPassno");
                    var query = (from x in DVSC.CANCEL_LOG
                                 join f in DVSC.FIRMMASTERs on x.Cancel_Firm equals f.FIRM_ID
                                 where (x.Cancel_PassNo.Contains(lblCPassno.Text.Trim())) && (x.Cancel_Cr == "ON LOSS")
                                 select new
                                 {
                                     x.Cancel_Name,
                                     x.Cancel_PassNo,
                                     f.FIRM_NAME,
                                     x.Cancel_Cr,
                                     x.Cancel_PlaceLoss,
                                     x.Loss_Date,
                                     x.Loss_Fir,
                                     x.Cancel_Id,
                                     x.Cancel_Fine
                                 }).ToList();
                    if (query.Count > 0)
                    {
                        Gv_LossDetail.DataSource = query;
                        Gv_LossDetail.DataBind();
                    }
                    else
                    {
                        Gv_LossDetail.DataSource = null;
                        Gv_LossDetail.DataBind();
                    }
                }
            }
            catch (Exception)
            {
                Response.Write("<script> alert('Error while loading records Please try after some time.')</script>");
            }
        }

        public int getcardActiveLastRecord(CardDetails crdDetails)
        {
            DataSet dsDetails = new DataSet();
            int ActivateCardCommandID = 0;
            try
            {
                SqlConnection SqlCon = new SqlConnection(ConfigurationManager.ConnectionStrings["SqlConnection"].ConnectionString);
                SqlCommand SqlCmd = new SqlCommand();
                SqlCmd.Connection = SqlCon;

                using (SqlCon)
                {
                    using (SqlDataAdapter da = new SqlDataAdapter())
                    {

                        SqlCmd.Parameters.AddWithValue("@CardNo", crdDetails.CardNo);
                        SqlCmd.Parameters.AddWithValue("@ControllerNo", crdDetails.ControllerNo);
                        SqlCmd.CommandText = "dbo.getActivecardLastRecord";
                        da.SelectCommand = SqlCmd;
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.Fill(dsDetails);
                    }

                }
                if (dsDetails.Tables.Count > 0)
                {
                    return ActivateCardCommandID = Convert.ToInt32(dsDetails.Tables[0].Rows[0][0]);
                }
                else
                {
                    return ActivateCardCommandID = 0;
                }

            }
            catch (Exception ex)
            {
                return ActivateCardCommandID = 0;
            }

        }

        public void insertintoDeviceRequestStatus(CardDetails crdDetails, GridViewRow Contractordetails, int Ids, string tblname, string CardCommand)
        {
            try
            {
                SqlConnection SqlCon = new SqlConnection(ConfigurationManager.ConnectionStrings["SqlConnection"].ConnectionString);
                SqlDataReader rdr = null;
                //SqlCommand SqlCmd = new SqlCommand();
                //SqlCmd.Connection = SqlCon;
                SqlCommand SqlCmd = new SqlCommand("dbo.InsertDeviceRequestStatus", SqlCon);
                SqlCmd.CommandType = CommandType.StoredProcedure;
                //rdr = SqlCmd.ExecuteReader();

                Dockyardid = (Label)Contractordetails.FindControl("lblDockyardid");
                SqlCmd.Parameters.AddWithValue("@DockyardId", Dockyardid.Text.ToString());
                SqlCmd.Parameters.AddWithValue("@ControllerId", crdDetails.ControllerNo);
                SqlCmd.Parameters.AddWithValue("@CardNo", crdDetails.CardNo);
                SqlCmd.Parameters.AddWithValue("@DeviceCommand", CardCommand);
                SqlCmd.Parameters.AddWithValue("@RequestStatus", "");
                SqlCmd.Parameters.AddWithValue("@Trandate", System.DateTime.Now.ToString());
                SqlCmd.Parameters.AddWithValue("@RequestID", Ids);
                SqlCmd.Parameters.AddWithValue("@RequestTable", tblname);
                SqlCmd.Parameters.AddWithValue("@CreatedBy", "1");

                SqlCon.Open();
                SqlCmd.ExecuteNonQuery();
                SqlCon.Close();

            }
            catch (Exception ex)
            {

            }
        }
    }
}