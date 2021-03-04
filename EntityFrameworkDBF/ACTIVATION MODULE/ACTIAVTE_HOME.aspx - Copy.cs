﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EntityFrameworkDBF.ServiceReference1;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Net.NetworkInformation;
using System.Threading;

namespace EntityFrameworkDBF.ACTIVATION_MODULE
{
    public partial class ACTIAVTE_HOME : System.Web.UI.Page
    {
        VMSEntities DVSC = new VMSEntities();
        SmartAccessEntities smartAccess = new SmartAccessEntities();

        ServiceReference1.SmartWebServiceClient ObjServiceClient = new ServiceReference1.SmartWebServiceClient("BasicHttpBinding_ISmartWebService");

        ServiceReference1.CardDetails objCardDetails = new ServiceReference1.CardDetails();
        ServiceReference1.CardStatus objCardStatus = new ServiceReference1.CardStatus();
       // ServiceReference1.TemplateStatus ObjTemplateStatus = null;

        // CardDetails objCardDetails = new CardDetails();
        //CardStatus[] objCardStatus = null;
        TemplateStatus[] ObjTemplateStatus = null;

        DataTable dt = new DataTable();
        DataSet ds = new DataSet();

        DataTable DT1, DT3;

        HiddenField hdn;
        Label Name, Mindate, Dockyardid, RfidCardNo, RfidNo;
        string Controllerlist, CardCommand, TableName, controllerofflinelist;
        int ReaderCount = 0;
        int DeviceTemplateID, ActivateCardCommandID = 0;
        int Cno;


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                spnDate.InnerText = DateTime.Now.Date.ToString("dd-MMM-yyyy").Replace("-", " ").ToUpper();
            }
        }

        protected void btnactivatehome_Click(object sender, EventArgs e)
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
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            #region
            int Dockyardid = 0;
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
                          where (c.Cont_Aadhaar == txtSearch.Text.Trim())
                || (c.Cont_CardNo == txtSearch.Text.Trim())
                || (c.Cont_Mobile == txtSearch.Text.Trim()) ||
                (c.Cont_RFIDNo == txtSearch.Text.Trim()) ||
                (c.Cont_Name == txtSearch.Text.Trim()) ||
                (f.FIRM_NAME == txtSearch.Text.Trim()) ||
                (c.Cont_DocID == Dockyardid)
                          select new
                          {
                              c.Cont_Id,
                              c.Cont_DocID,
                              c.Cont_Name,
                              f.FIRM_NAME,
                              c.Cont_RFIDValidity,
                              c.Cont_CardNo,
                              c.Cont_Aadhaar,
                              c.Cont_RFIDNo,
                              c.Cont_ShopId,
                              c.Cont_MinDate
                          }).ToList();
            if (table1.Count > 0)
            {
                for (int i = 0; i < table1.Count; i++)
                {
                    Grd_ActivateViewData.DataSource = table1;
                    Grd_ActivateViewData.DataBind();
                    //Session["table1"] = table1;
                    string CardNo = table1[i].Cont_RFIDNo.ToString();
                    // DataTable DT2;

                    //if (CardNo.Trim() != string.Empty)
                    //{

                    //    DT1 = BindActiveController(CardNo);
                    //    DT2 = DT1.Clone();
                    //    foreach (DataRow Drow in DT1.Rows)
                    //    {
                    //        DT2.Rows.Add(Drow.ItemArray);
                    //        //DT2.ImportRow(Drow.Table.Rows[0]);
                    //    }
                    //    DT3 = DT2;
                    //}
                    //else
                    //    lblTemplateMessage.Text = "Please Mention RFIDNO";

                }

                //GvContoller.DataSource = DT3;
                //GvContoller.DataBind();

                BindActiveController();


                GvContoller.Visible = true;
                lblactivate.Visible = true;
                viewActive.Visible = true;
                viewDeActive.Visible = true;
            }
            else
            {
                GvContoller.Visible = false;
                lblactivate.Visible = false;
                viewActive.Visible = false;
                viewDeActive.Visible = false;
                Grd_ActivateViewData.DataSource = null;
                Grd_ActivateViewData.DataBind();
                Response.Write("<script> alert('No data found')</script>");
            }
            #endregion

        }


        //public DataTable BindActiveController(string Cardno)
        public void BindActiveController()
        {
            try
            {
                if (smartAccess.Controllers.Count() > 0)
                {
                    GvContoller.DataSource = (from em in smartAccess.Controllers
                                              select new { em.ControllerID, em.ControllerName, em.IPAddress, em.ControllerNo }).ToList();
                    GvContoller.DataBind();
                }

            }
            catch (Exception EX)
            {

            }
            // return DT1;
        }

        public void BindDeActiveController()
        {
            try
            {
                if (smartAccess.Controllers.Count() > 0)
                {
                    GvContoller.DataSource = (from em in smartAccess.Controllers
                                              select new { em.ControllerID, em.ControllerName, em.IPAddress, em.ControllerNo }).ToList();
                    GvContoller.DataBind();

                }
            }
            catch (Exception EX)
            {

            }
        }

        protected void chkSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox ChkBoxHeader = (CheckBox)GvContoller.HeaderRow.FindControl("chkSelectAll");
            foreach (GridViewRow row in GvContoller.Rows)
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

        protected void BtnActive_Click(object sender, EventArgs e)
        {
            SetActivate();
            Thread.Sleep(10000);
            SetActivate();
        }
        private void SetActivate()
        {
            try
            {


                //lblTemplateMessage.Text = string.Empty;
                controllerofflinelist = string.Empty;

                foreach (GridViewRow row in Grd_ActivateViewData.Rows)
                {
                    ReaderCount = 0;
                    RfidNo = (Label)row.FindControl("lblRFIDNO");
                    Name = (Label)row.FindControl("lblName");
                    Mindate = (Label)row.FindControl("lblMinDate");
                    objCardDetails.CardNo = RfidNo.Text;
                    objCardDetails.Name = Name.Text;
                    objCardDetails.AuthCode = "6";

                    foreach (GridViewRow Controllerrow in GvContoller.Rows)
                    {
                        CheckBox chk = (CheckBox)Controllerrow.FindControl("chkSelect");
                        if (chk.Checked)
                        {
                            hdn = (HiddenField)Controllerrow.FindControl("hdConreollerNo");
                            Controllerlist = hdn.Value;

                            if (Controllerlist.Length > 0)
                            {
                                objCardDetails.ControllerNo = Controllerlist;

                                objCardDetails.Expiry = Convert.ToDateTime(Mindate.Text);//Convert.ToDateTime(txtrfidValidity.Text.Trim());
                                objCardDetails.NoOfReader = 1;//GvContoller.Rows.Count;

                                Cno = Convert.ToInt32(Controllerlist);

                                var Ipaddress = (from em in smartAccess.Controllers
                                                 where em.ControllerNo == Cno
                                                 select new { em.IPAddress, em.ControllerName }).ToList();


                                Ping myPing = new Ping();


                                PingReply reply = myPing.Send(Ipaddress[0].IPAddress, Convert.ToInt16(ConfigurationManager.AppSettings["PingTime"].ToString()));

                                if (reply.Status == IPStatus.Success)
                                {
                                    //---ACTIVATION SECTION----
                                    objCardStatus = ObjServiceClient.ActivateCard(objCardDetails);
                                    //ObjServiceClient2.ActivateCard(objCardDetails2);
                                    try
                                    {
                                        TableName = "DeviceActivateCard";
                                        CardCommand = "Activate";

                                        ActivateCardCommandID = getcardActiveLastRecord(objCardDetails);
                                        insertintoDeviceRequestStatus(objCardDetails, row, ActivateCardCommandID, TableName, CardCommand);

                                    }
                                    catch (Exception ex)
                                    {
                                        //
                                    }

                                    //---UPLOAD SECTION----
                                    ObjTemplateStatus = ObjServiceClient.UploadTemplate(objCardDetails.CardNo, objCardDetails.ControllerNo);
                                    // ObjTemplateStatus2 = ObjServiceClient2.UploadTemplate(objCardDetails2.CardNo, objCardDetails2.ControllerNo);

                                    try
                                    {
                                        TableName = "DeviceTemplates";
                                        CardCommand = "Uplaod";

                                        DeviceTemplateID = getcardUploadLastRecord(objCardDetails);
                                        insertintoDeviceRequestStatus(objCardDetails, row, DeviceTemplateID, TableName, CardCommand);

                                    }
                                    catch (Exception ex)
                                    {
                                        //
                                    }

                                    dSucess.Visible = true;
                                    dfail.Visible = false;

                                    DDeaciiveSuccess.Visible = false;
                                    DDeaciiveFail.Visible = false;
                                    //lblTemplateMessage.Text = "ACTIVATED TEMPLATE SUCESSFULLY";
                                    //lblTemplateMessage.ForeColor = System.Drawing.Color.Green;

                                    Controllerlist = string.Empty;
                                }
                                else
                                {
                                    controllerofflinelist += Ipaddress[0].ControllerName + ",";
                                }
                            }
                            Controllerlist = string.Empty;
                        }
                    }
                }
            }
            catch
            {
                dSucess.Visible = false;
                dfail.Visible = true;
                DDeaciiveSuccess.Visible = false;
                DDeaciiveFail.Visible = false;
                //lblTemplateMessage.Text = "ACTIVATED TEMPLATE FAIL";
                //lblTemplateMessage.ForeColor = System.Drawing.Color.Red;
            }

            if (controllerofflinelist.Trim() != string.Empty)
            {
                controllerofflinelist = controllerofflinelist.Trim().Remove(controllerofflinelist.Length - 1);
                Response.Write("<script> alert('CONTROLLERS " + controllerofflinelist + " IS OFFLINE')</script>");
            }
        }
        protected void BtnDeavtive_Click(object sender, EventArgs e)
        {
            controllerofflinelist = string.Empty;
            try
            {
                // lblTemplateMessage.Text = string.Empty;
                foreach (GridViewRow row in Grd_ActivateViewData.Rows)
                {
                    ReaderCount = 0;
                    RfidNo = (Label)row.FindControl("lblRFIDNO");
                    Name = (Label)row.FindControl("lblName");
                    Mindate = (Label)row.FindControl("lblMinDate");

                    objCardDetails.CardNo = RfidNo.Text;
                    objCardDetails.Name = Name.Text;
                    objCardDetails.AuthCode = "6";

                    foreach (GridViewRow Controllerrow in GvContoller.Rows)
                    {

                        CheckBox chk = (CheckBox)Controllerrow.FindControl("chkSelect");
                        if (chk.Checked)
                        {
                            hdn = (HiddenField)Controllerrow.FindControl("hdConreollerNo");
                            Controllerlist = hdn.Value;

                            if (Controllerlist.Length > 0)
                            {
                                objCardDetails.ControllerNo = Controllerlist;
                                objCardDetails.Expiry = Convert.ToDateTime(Mindate.Text);//Convert.ToDateTime("2018/10/01");//Convert.ToDateTime(txtrfidValidity.Text.Trim());
                                //objCardDetails.Expiry = Convert.ToDateTime("2018/10/01");
                                objCardDetails.NoOfReader = 1;// ReaderCount;//GvContoller.Rows.Count;

                                Cno = Convert.ToInt32(Controllerlist);

                                var Ipaddress = (from em in smartAccess.Controllers
                                                 where em.ControllerNo == Cno
                                                 select new { em.IPAddress, em.ControllerName }).ToList();


                                Ping myPing = new Ping();


                                PingReply reply = myPing.Send(Ipaddress[0].IPAddress, Convert.ToInt16(ConfigurationManager.AppSettings["PingTime"].ToString()));

                                if (reply.Status == IPStatus.Success)
                                {

                                    objCardStatus = ObjServiceClient.DeactivateCard(objCardDetails);
                                    // ObjServiceClient2.DeactivateCard(objCardDetails2);
                                    try
                                    {
                                        TableName = "DeviceActivateCard";
                                        CardCommand = "DeActivate";

                                        ActivateCardCommandID = getcardActiveLastRecord(objCardDetails);
                                        insertintoDeviceRequestStatus(objCardDetails, row, ActivateCardCommandID, TableName, CardCommand);

                                    }
                                    catch (Exception ex)
                                    {
                                        //
                                    }

                                    DDeaciiveSuccess.Visible = true;
                                    DDeaciiveFail.Visible = false;
                                    dSucess.Visible = false;
                                    dfail.Visible = false;
                                    Controllerlist = string.Empty;
                                }
                                else
                                {
                                    controllerofflinelist += Ipaddress[0].ControllerName + ",";
                                }
                            }
                            Controllerlist = string.Empty;
                        }
                    }
                }
            }
            catch
            {
                DDeaciiveSuccess.Visible = false;
                DDeaciiveFail.Visible = true;
                dSucess.Visible = false;
                dfail.Visible = false;
            }


            if (controllerofflinelist.Trim() != string.Empty)
            {
                controllerofflinelist = controllerofflinelist.Trim().Remove(controllerofflinelist.Length - 1);
                Response.Write("<script> alert('CONTROLLERS " + controllerofflinelist + " IS OFFLINE')</script>");
            }
        }


        public void insertintoDeviceRequestStatus(ServiceReference1.CardDetails crdDetails, GridViewRow Contractordetails, int Ids, string tblname, string CardCommand)
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

        public int getcardActiveLastRecord(ServiceReference1.CardDetails crdDetails)
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

        public int getcardUploadLastRecord(ServiceReference1.CardDetails crdDetails)
        {
            int ActivateCardCommandID = 0;

            DataSet dsDetails = new DataSet();
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
                        SqlCmd.CommandText = "dbo.getUploadcardLastRecord";
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
    }
}