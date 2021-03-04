using System;
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
using System.IO;

namespace EntityFrameworkDBF.ACTIVATION_MODULE
{
    public partial class ACTIAVTE_HOME : System.Web.UI.Page
    {
        VMSEntities DVSC = new VMSEntities();
        SmartAccessEntities smartAccess = new SmartAccessEntities();

        ServiceReference1.SmartWebServiceClient ObjServiceClient = new ServiceReference1.SmartWebServiceClient("BasicHttpBinding_ISmartWebService");

        ServiceReference1.Enroll_Error_Code errcode = new ServiceReference1.Enroll_Error_Code();
        //ServiceReference1.SmartWebServiceClient ObjServiceClient = new ServiceReference1.SmartWebServiceClient();
        ServiceReference1.SmartWebServiceClient objclient = new ServiceReference1.SmartWebServiceClient();

        ServiceReference1.CardDetails objCardDetails = new ServiceReference1.CardDetails();
        ServiceReference1.CardStatus objCardStatus = new ServiceReference1.CardStatus();
        // ServiceReference1.TemplateStatus ObjTemplateStatus = null;

        // CardDetails objCardDetails = new CardDetails();
        //CardStatus[] objCardStatus = null;
        TemplateStatus[] ObjTemplateStatus = null;
        DataTable dt = new DataTable();
        DataSet ds = new DataSet();
        DataSet dsDownloadStatus = new DataSet();
        DataTable DT1, DT3;
        string Message = "";
        HiddenField hdn;
        Label Name, Mindate, Dockyardid, RfidCardNo, RfidNo;
        string Controllerlist, CardCommand, TableName, controllerofflinelist;
        int ReaderCount = 0;
        int DeviceTemplateID, ActivateCardCommandID = 0;
        int Cno;
        string UpdateCardno;
        int downloadcount = 0;
        bool Attempt = false;
        int DownloadControllerNo = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["DownloadControllerNo"]);
        Int32 SleepTime = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["SleepTime"]);

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
            // ADDED BY MSJ ON 14 JUNE 2019 START
            UpdateCardno = ConverttoTenDig(txtSearch.Text.Trim());
            //UpdateCardno = ConverttoSixteenDig(txtSearch.Text.Trim());
            // ADDED BY MSJ ON 14 JUNE 2019 END
            
            #region
            if (ddlsearch.SelectedItem.Text != "--SELECT--" && ddlsearch.SelectedValue != "-1")
            {
                int Dockyardid = 0;
                    if (ddlsearch.SelectedItem.Text == "DOCKYARD ID NO")
                    {
                        Dockyardid = Convert.ToInt32(txtSearch.Text.Trim());
                    }

                 //where   (x.Cont_CancelFLag!="CANCEL") && ((x.Cont_Aadhaar==txtSearch.Text.Trim())
                 //             || (x.Cont_CardNo ==txtSearch.Text.Trim())
                 //             || (x.Cont_Mobile == (txtSearch.Text.Trim())) ||
                 //             (x.Cont_RFIDNo == (txtSearch.Text.Trim())) ||
                 //             (x.Cont_Name == (txtSearch.Text.Trim()))) 
                //       select x).First(); //  || (c.Cont_DocID == Dockyardid)
                var table1 = (from c in DVSC.CONTRACTOR_DETAIL
                              join v in DVSC.VisitorTransactionDetails on c.Cont_DocID equals v.VisitorTranID
                              join d in DVSC.DESIGNATION_MASTER on c.Cont_DesignationID equals d.DESIGNATION_ID
                              join s in DVSC.STATE_MASTER on c.Cont_StateID equals s.STATE_ID
                              join f in DVSC.FIRMMASTERs on c.Cont_FirmID equals f.FIRM_ID
                              join r in DVSC.RELIGION_MASTER on c.Cont_ReligionID equals r.R_ID
                              join n in DVSC.COUNTRY_MASTER on c.Cont_NationalityID equals n.COUNTRY_ID
                              join cr in DVSC.CANCEL_REASON_MASTER on c.Cont_CancelReason equals cr.CR_ID
                              join u in DVSC.PSU_MASTER on c.Cont_PSUunitID equals u.PSU_ID
                              join sh in DVSC.SHOP_MASTER on c.Cont_ShopId equals sh.SHOP_ID
                              where (c.Cont_CancelFLag != "CANCEL") && ((c.Cont_Aadhaar == txtSearch.Text.Trim())
                                || (c.Cont_CardNo == txtSearch.Text.Trim())
                                || (c.Cont_RFIDNo == UpdateCardno))
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
                        string CardNo = table1[i].Cont_RFIDNo.ToString();
                        Session["RFIDNo"] = CardNo;
                        Session["MinDate"] = table1[i].Cont_MinDate;
                        Session["Name"] = table1[i].Cont_Name;
                        Session["Cont_CardNo"] = table1[i].Cont_CardNo;

                        Grd_ActivateViewData.DataSource = table1;
                        Grd_ActivateViewData.DataBind();
                        //Session["table1"] = table1;
                        //Message = GetExpiryDate(Convert.ToDateTime(table1[i].Cont_MinDate), CardNo);
                        downloadcount = GetDownloadStatus(CardNo);
                        if (downloadcount > 0)
                            LblDownload.Visible = false;
                        else
                            LblDownload.Visible = true;

                        BindActiveController(CardNo);

                    }

                    GvContoller.Visible = true;
                    lblactivate.Visible = true;
                    viewActive.Visible = true;
                    viewDeActive.Visible = true;
                    viewRefresh.Visible = true;
                }
                else
                {
                    GvContoller.Visible = false;
                    lblactivate.Visible = false;
                    viewActive.Visible = false;
                    viewDeActive.Visible = false;
                    viewRefresh.Visible = false;
                    Grd_ActivateViewData.DataSource = null;
                    Grd_ActivateViewData.DataBind();
                    Response.Write("<script> alert('No data found')</script>");
                }
            }
            else
            {
                Response.Write("<script> alert('Please select any search criteria')</script>");
            }
            #endregion
        }

        //public DataTable BindActiveController(string Cardno)
        public void BindActiveController(string Cardno)
        {
            try
            {
                if (smartAccess.Controllers.Count() > 0)
                {
                    //GvContoller.DataSource = (from em in smartAccess.Controllers
                    //                          select new { em.ControllerID, em.ControllerName, em.IPAddress, em.ControllerNo }).ToList();

                    DataSet dsDetails = new DataSet();
                    DataTable dd1 = new DataTable();
                    dd1 = null;
                    try
                    {
                        SqlConnection SqlCon = new SqlConnection(ConfigurationManager.ConnectionStrings["SqlConnection"].ConnectionString);
                        SqlCommand SqlCmd = new SqlCommand();
                        SqlCmd.Connection = SqlCon;
                        using (SqlCon)
                        {
                            using (SqlDataAdapter da = new SqlDataAdapter())
                            {
                                SqlCmd.Parameters.AddWithValue("@CardNo", Cardno);
                                SqlCmd.CommandText = "dbo.GetControllerCardStatus";
                                da.SelectCommand = SqlCmd;
                                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                                da.Fill(dsDetails);
                               // dd1 = dsDetails.Tables[0];
                               // dd1 = dd1.AsEnumerable()
                               //     //.Where(r => r.Field<Int32>("controllerNo") != DownloadControllerNo)
                               //.CopyToDataTable();
                                GvContoller.DataSource = dsDetails.Tables[1];
                                GvContoller.DataBind();

                                DDeaciiveFail.Visible = false;
                                DDeaciiveSuccess.Visible = false;

                                downloadcount = GetDownloadStatus(Cardno);
                                if (downloadcount > 0)
                                    LblDownload.Visible = false;
                                else
                                    LblDownload.Visible = true;

                            }
                        }
                    }
                    catch (Exception ex)
                    {

                    }

                }

            }
            catch (Exception EX)
            {

            }
        }
        //public void BindActiveController(string Cardno)
        //{
        //    try
        //    {
        //        if (smartAccess.Controllers.Count() > 0)
        //        {
        //            //GvContoller.DataSource = (from em in smartAccess.Controllers
        //            //                          select new { em.ControllerID, em.ControllerName, em.IPAddress, em.ControllerNo }).ToList();

        //            DataSet dsDetails = new DataSet();
        //            DataTable dd1 = new DataTable();
        //            dd1 = null;
        //            try
        //            {
        //                SqlConnection SqlCon = new SqlConnection(ConfigurationManager.ConnectionStrings["SqlConnection"].ConnectionString);
        //                SqlCommand SqlCmd = new SqlCommand();
        //                SqlCmd.Connection = SqlCon;
        //                using (SqlCon)
        //                {
        //                    using (SqlDataAdapter da = new SqlDataAdapter())
        //                    {
        //                        SqlCmd.Parameters.AddWithValue("@CardNo", Cardno);
        //                        SqlCmd.CommandText = "dbo.GetControllerCardStatus";
        //                        da.SelectCommand = SqlCmd;
        //                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
        //                        da.Fill(dsDetails);
        //                        dd1 = dsDetails.Tables[0];
        //                        dd1 = dd1.AsEnumerable()
        //                            //.Where(r => r.Field<Int32>("controllerNo") != DownloadControllerNo)
        //                       .CopyToDataTable();
        //                        GvContoller.DataSource = dd1;
        //                        GvContoller.DataBind();

        //                        DDeaciiveFail.Visible = false;
        //                        DDeaciiveSuccess.Visible = false;

        //                        downloadcount = GetDownloadStatus(Cardno);
        //                        if (downloadcount > 0)
        //                            LblDownload.Visible = false;
        //                        else
        //                            LblDownload.Visible = true;

        //                    }
        //                }
        //            }
        //            catch (Exception ex)
        //            {

        //            }

        //        }

        //    }
        //    catch (Exception EX)
        //    {

        //    }
        //}

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
            foreach (GridViewRow row in Grd_ActivateViewData.Rows)
            {
                //Attempt = false;
                RfidNo = (Label)row.FindControl("lblRFIDNO");
                //SetActivate();
                //Thread.Sleep(SleepTime);
                Attempt = true;
                SetActivate();
                Thread.Sleep(SleepTime);
                BindActiveController(RfidNo.Text);
            }
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
                                objCardDetails.NoOfReader = 5;//GvContoller.Rows.Count;
                                //Cno = Convert.ToInt32(Controllerlist);

                                //var Ipaddress = (from em in smartAccess.Controllers
                                //                 where em.ControllerNo == Cno
                                //                 select new { em.IPAddress, em.ControllerName }).ToList();


                                //Ping myPing = new Ping();


                                //PingReply reply = myPing.Send(Ipaddress[0].IPAddress, Convert.ToInt16(ConfigurationManager.AppSettings["PingTime"].ToString()));

                                //if (reply.Status == IPStatus.Success)
                                //{
                                //---ACTIVATION SECTION----
                                objCardStatus = ObjServiceClient.ActivateCard(objCardDetails);
                                try
                                {
                                    if (Attempt == true)
                                    {
                                        TableName = "DeviceActivateCard";
                                        CardCommand = "Activate";

                                        ActivateCardCommandID = getcardActiveLastRecord(objCardDetails);
                                        insertintoDeviceRequestStatus(objCardDetails, row, ActivateCardCommandID, TableName, CardCommand);
                                    }

                                }
                                catch (Exception ex)
                                {
                                    ErrorLogFile(ex.ToString());
                                }

                                //---UPLOAD SECTION----
                                ObjTemplateStatus = ObjServiceClient.UploadTemplate(objCardDetails.CardNo, objCardDetails.ControllerNo);

                                try
                                {
                                    if (Attempt == true)
                                    {
                                        TableName = "DeviceTemplates";
                                        CardCommand = "Uplaod";

                                        DeviceTemplateID = getcardUploadLastRecord(objCardDetails);
                                        insertintoDeviceRequestStatus(objCardDetails, row, DeviceTemplateID, TableName, CardCommand);
                                    }
                                }
                                catch (Exception ex)
                                {
                                    ErrorLogFile(ex.ToString());
                                }

                                // dSucess.Visible = true;
                                dfail.Visible = false;

                                DDeaciiveSuccess.Visible = false;
                                DDeaciiveFail.Visible = false;

                                //lblTemplateMessage.Text = "ACTIVATED TEMPLATE SUCESSFULLY";
                                //lblTemplateMessage.ForeColor = System.Drawing.Color.Green;

                                Controllerlist = string.Empty;
                                //}
                                //else
                                //{
                                //    controllerofflinelist += Ipaddress[0].ControllerName + ",";
                                //}
                            }
                            Controllerlist = string.Empty;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                dSucess.Visible = false;
                dfail.Visible = true;
                DDeaciiveSuccess.Visible = false;
                DDeaciiveFail.Visible = false;
                ErrorLogFile(ex.ToString());
                //lblTemplateMessage.Text = "ACTIVATED TEMPLATE FAIL";
                //lblTemplateMessage.ForeColor = System.Drawing.Color.Red;
            }

            //if (controllerofflinelist.Trim() != string.Empty)
            //{
            //    controllerofflinelist = controllerofflinelist.Trim().Remove(controllerofflinelist.Length - 1);
            //    Response.Write("<script> alert('CONTROLLERS " + controllerofflinelist + " IS OFFLINE')</script>");
            //}
        }

        private void BtnDeavtive()
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
                    Mindate =(Label)row.FindControl("lblMinDate");

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
                                objCardDetails.Expiry =Convert.ToDateTime(Mindate.Text);//Convert.ToDateTime("2018/10/01");//Convert.ToDateTime(txtrfidValidity.Text.Trim());
                                //objCardDetails.Expiry = Convert.ToDateTime("2018/10/01");
                                objCardDetails.NoOfReader = 1;// ReaderCount;//GvContoller.Rows.Count;

                                //Cno = Convert.ToInt32(Controllerlist);

                                //var Ipaddress = (from em in smartAccess.Controllers
                                //                 where em.ControllerNo == Cno
                                //                 select new { em.IPAddress, em.ControllerName }).ToList();


                                //Ping myPing = new Ping();


                                //PingReply reply = myPing.Send(Ipaddress[0].IPAddress, Convert.ToInt16(ConfigurationManager.AppSettings["PingTime"].ToString()));

                                //if (reply.Status == IPStatus.Success)
                                //{

                                objCardStatus = ObjServiceClient.DeactivateCard(objCardDetails);
                                try
                                {
                                    if (Attempt == true)
                                    {
                                        TableName = "DeviceActivateCard";
                                        CardCommand = "DeActivate";

                                        ActivateCardCommandID = getcardActiveLastRecord(objCardDetails);
                                        insertintoDeviceRequestStatus(objCardDetails, row, ActivateCardCommandID, TableName, CardCommand);
                                    }
                                }
                                catch (Exception ex)
                                {
                                    ErrorLogFile(ex.ToString());
                                }

                                // DDeaciiveSuccess.Visible = true;
                                DDeaciiveFail.Visible = false;
                                dSucess.Visible = false;
                                dfail.Visible = false;
                                Controllerlist = string.Empty;
                                //}
                                //else
                                //{
                                //    controllerofflinelist += Ipaddress[0].ControllerName + ",";
                                //}
                            }
                            Controllerlist = string.Empty;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLogFile(ex.ToString());
                DDeaciiveSuccess.Visible = false;
                DDeaciiveFail.Visible = true;
                dSucess.Visible = false;
                dfail.Visible = false;
            }


            //if (controllerofflinelist.Trim() != string.Empty)
            //{
            //    controllerofflinelist = controllerofflinelist.Trim().Remove(controllerofflinelist.Length - 1);
            //    Response.Write("<script> alert('CONTROLLERS " + controllerofflinelist + " IS OFFLINE')</script>");
            //}
        }

        protected void BtnDeavtive_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow row in Grd_ActivateViewData.Rows)
            {
                RfidNo = (Label)row.FindControl("lblRFIDNO");
                Attempt = true;
                BtnDeavtive();
                Thread.Sleep(SleepTime);
                BindActiveController(RfidNo.Text);
            }
        }

        protected void BtnRefresh_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow row in Grd_ActivateViewData.Rows)
            {
                RfidNo = (Label)row.FindControl("lblRFIDNO");
                BindActiveController(RfidNo.Text);
            }

            DDeaciiveFail.Visible = false;
            DDeaciiveSuccess.Visible = false;
            LblDownload.Visible = false;
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

        protected void ddlDownload_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlDownload.SelectedIndex > 0)
            {
                AddFig.Enabled = true;
                Btndownlaod.Enabled = true;
            }
            else
            {
                AddFig.Enabled = false;
                Btndownlaod.Enabled = false;
            }
        }

        protected void Btndownlaod_Click(object sender, EventArgs e)
        {
            string Rfidno = Session["RFIDNo"].ToString();
            DateTime MinDate = Convert.ToDateTime(Session["MinDate"]);
            string name = Session["Name"].ToString();
            try
            {
                objCardDetails.CardNo = Rfidno;
                //objCardDetails.CardNo = txtrfidno.Text.Trim();
                objCardDetails.AuthCode = "6";
                objCardDetails.ControllerNo = ddlDownload.SelectedItem.Value.ToString();
                objCardDetails.Expiry = MinDate;//Convert.ToDateTime(txtrfidValidity.Text.Trim());
                //objCardDetails.Expiry =Convert.ToDateTime(txtrfidValidity.Text.Trim());
                objCardDetails.Name = name;
                objCardDetails.NoOfReader = 4;
                //objCardStatus = ObjServiceClient.ActivateCard(objCardDetails);
                objCardStatus = ObjServiceClient.ActivateCard(objCardDetails);


                Cno = Convert.ToInt32(ddlDownload.SelectedItem.Value);
                var Ipaddress = (from em in smartAccess.Controllers
                                 where em.ControllerNo == Cno
                                 select new { em.IPAddress, em.ControllerName }).ToList();


                Ping myPing = new Ping();


                PingReply reply = myPing.Send(Ipaddress[0].IPAddress, Convert.ToInt16(ConfigurationManager.AppSettings["PingTime"].ToString()));

                if (reply.Status == IPStatus.Success)
                {
                    ObjTemplateStatus = ObjServiceClient.DownLoadTemplate(objCardDetails.CardNo, objCardDetails.ControllerNo);
                    //ObjServiceClient.CheckDownloadTemplateStatus(objCardDetails.CardNo, Convert.ToInt32(objCardDetails.ControllerNo));
                    ObjServiceClient.CheckDownloadTemplateStatus(objCardDetails.CardNo, objCardDetails.ControllerNo);
                    DDeaciiveSuccess.Visible = true;
                    DDeaciiveFail.Visible = false;
                }
                else
                {
                    Response.Write("<script> alert('CONTROLLER " + Ipaddress[0].ControllerName + " OFFLINE')</script>");
                }
            }
            catch
            {
                DDeaciiveSuccess.Visible = false;
                DDeaciiveFail.Visible = true;
                Response.Write("<script> alert('BY CONTROLLER IS OFFLINE')</script>");
            }
        }

        //// ADDED BY MSJ ON 14 JUNE 2019 START
        //public string ConverttoSixteenDig(string CrdNo)
        //{
        //    UpdateCardno = CrdNo.PadLeft(10, '0');

        //    return UpdateCardno;
        //}
        //// ADDED BY MSJ ON 14 JUNE 2019 END

        public string ConverttoTenDig(string CrdNo)
        {
            if (CrdNo.Trim().Length == 1)
                UpdateCardno = "000000000" + CrdNo.Trim();
            if (CrdNo.Trim().Length == 2)
                UpdateCardno = "00000000" + CrdNo.Trim();
            if (CrdNo.Trim().Length == 3)
                UpdateCardno = "0000000" + CrdNo.Trim();
            if (CrdNo.Trim().Length == 4)
                UpdateCardno = "000000" + CrdNo.Trim();
            if (CrdNo.Trim().Length == 5)
                UpdateCardno = "00000" + CrdNo.Trim();
            if (CrdNo.Trim().Length == 6)
                UpdateCardno = "0000" + CrdNo.Trim();
            if (CrdNo.Trim().Length == 7)
                UpdateCardno = "000" + CrdNo.Trim();
            if (CrdNo.Trim().Length == 8)
                UpdateCardno = "00" + CrdNo.Trim();
            if (CrdNo.Trim().Length == 9)
                UpdateCardno = "0" + CrdNo.Trim();
            if (CrdNo.Trim().Length == 10)
                UpdateCardno = CrdNo.Trim();

            return UpdateCardno;
        }

        protected void AddFig_Click(object sender, EventArgs e)
        {
            if (ddlDownload.SelectedIndex > 0)
            {
                string Msg = "";
                string Rfidno = Session["RFIDNo"].ToString();
                DateTime MinDate = Convert.ToDateTime(Session["MinDate"]);
                string name = Session["Name"].ToString();

                Cno = Convert.ToInt32(ddlDownload.SelectedItem.Value);
                errcode = objclient.EnrollFinger(Rfidno.Trim(), Cno.ToString());
                if (errcode.ToString() == "Finger_Time_Out_108")
                    Msg = "FINGER TIMED OUT";
                else if (errcode.ToString() == "Socket_Timed_out")
                    Msg = "SOCKET TIMED OUT OR DEVICE IS OFLINE";
                else if (errcode.ToString() == "User_Enrolled_Successfully_Code_97")
                {
                    Msg = "USER ENROLLED SUCCESSFULLY";
                    Btndownlaod.Enabled = true;
                }
                else
                    Msg = errcode.ToString();
                Response.Write("<script> alert(' " + Msg + "')</script>");
            }
            else
            {
                Response.Write("<script> alert('Controller is Not Selected Or RFIDNO Is not Mention')</script>");
            }
        }

        public int GetDownloadStatus(string Cardno)
        {
            try
            {
                if (smartAccess.Controllers.Count() > 0)
                {
                    try
                    {
                        SqlConnection SqlCon = new SqlConnection(ConfigurationManager.ConnectionStrings["SqlConnection"].ConnectionString);
                        SqlCommand SqlCmd = new SqlCommand();
                        SqlCmd.Connection = SqlCon;
                        using (SqlCon)
                        {
                            using (SqlDataAdapter da = new SqlDataAdapter())
                            {
                                SqlCmd.Parameters.AddWithValue("@CardNo", Cardno);
                                SqlCmd.CommandText = "dbo.GetDownloadStatus";
                                da.SelectCommand = SqlCmd;
                                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                                da.Fill(dsDownloadStatus);

                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        return 0;
                    }

                }
                return Convert.ToInt32(dsDownloadStatus.Tables[0].Rows[0][0]);
            }
            catch (Exception EX)
            {
                return 0;
            }
        }

        public void ErrorLogFile(string strError)
        {
            string path = "";
            path = System.Configuration.ConfigurationManager.AppSettings["MessageLog"].ToString();
            try
            {

                // check if directory exists
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                path = path + DateTime.Today.ToString("dd-MMM-yy") + ".log";
                // check if file exist
                if (!File.Exists(path))
                {
                    File.Create(path).Dispose();
                }
                // log the error now
                using (StreamWriter writer = File.AppendText(path))
                {

                    string error = "\r\nLog written at : " + DateTime.Now.ToString() +
                         "\r\nMessage : " + strError;


                    writer.WriteLine(error);
                    writer.WriteLine("===========================================================================================================================================================");
                    writer.Flush();
                    writer.Close();

                }

            }
            catch
            {
                // throw;
                path = path + DateTime.Today.ToString("ddMMyyhhmmss") + ".log";
                // check if file exist
                if (!File.Exists(path))
                {
                    File.Create(path).Dispose();
                }
                // log the error now
                using (StreamWriter writer = File.AppendText(path))
                {

                    string error = "\r\nLog written at : " + DateTime.Now.ToString() +
                      "\r\nMessage : " + strError;

                    writer.WriteLine(error);
                    writer.WriteLine("==========================================");
                    writer.Flush();
                    writer.Close();

                }

            }
            // return strError;
        }

        public string GetExpiryDate(DateTime ExpiryDate1, string CardNo1)
        {
            DateTime ExpiryDate = ExpiryDate1; //Convert.ToDateTime(Session["MinDate"]);
            string CardNo = CardNo1;//Session["Cont_CardNo"].ToString();

            var table1 = (from c in DVSC.CONTRACTOR_DETAIL where (c.Cont_CardNo == CardNo) select c).ToList();
            if (table1[0].Cont_PVCValidity == ExpiryDate)
            {
                Message = "PVC VALIDITY";
            }
            if (table1[0].Cont_WOValidity == ExpiryDate)
            {
                Message = "WORK ORDER VALIDITY";
            }
            if (table1[0].Cont_RFIDValidity == ExpiryDate)
            {
                Message = "RFID VALIDITY";
            }
            return Message;
        }

        protected void Grd_ActivateViewData_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            DateTime ExpiryDate = Convert.ToDateTime(Session["MinDate"]);
            string CardNo = Session["Cont_CardNo"].ToString();
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //Label lblExpireCheck = (e.Row.FindControl("lblExpireCheck") as Label);
                //Label lblName = (e.Row.FindControl("lblName") as Label);
                //Label lblDockyardid = (e.Row.FindControl("lblDockyardid") as Label);
                //Label lblFIRM = (e.Row.FindControl("lblFIRM") as Label);
                Label lblVALIDITY = (e.Row.FindControl("lblVALIDITY") as Label);
                ExpiryDate = Convert.ToDateTime(lblVALIDITY.Text);
                //Label lblPASSNO = (e.Row.FindControl("lblPASSNO") as Label);
                //Label lblAADHAARNO = (e.Row.FindControl("lblAADHAARNO") as Label);
                //Label lblRFIDNO = (e.Row.FindControl("lblRFIDNO") as Label);
                //Label lblMinDate = (e.Row.FindControl("lblMinDate") as Label);
                //if (!string.IsNullOrEmpty(lblName.Text) && !string.IsNullOrEmpty(lblDockyardid.Text)
                //    && !string.IsNullOrEmpty(lblFIRM.Text) && !string.IsNullOrEmpty(lblVALIDITY.Text)
                //    && !string.IsNullOrEmpty(lblPASSNO.Text) && !string.IsNullOrEmpty(lblAADHAARNO.Text)
                //    && !string.IsNullOrEmpty(lblRFIDNO.Text) && !string.IsNullOrEmpty(lblMinDate.Text)
                //   )
                //{
                //    
                //}
                Message = GetExpiryDate(ExpiryDate, CardNo);
                lblVALIDITY.Text = ExpiryDate.ToString("dd/MM/yyyy") + " - " + Message;
                if (ExpiryDate <= System.DateTime.Now)
                {
                    Grd_ActivateViewData.Columns[4].ControlStyle.BorderColor = System.Drawing.Color.Red;
                    Grd_ActivateViewData.Columns[4].ControlStyle.BorderStyle = BorderStyle.Solid;
                }
            }
        }
    }
}