using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Text;

namespace EntityFrameworkDBF
{
    public partial class PrintPass_ForeignVisitor : System.Web.UI.Page
    {
        DataSet ds = new DataSet();
        public static int TranID = 0, off_tran_id = 0;
        protected ParameterDetails stParameterDetails;
        byte[] data;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    DataTable dt = Session["LoadDetail"] as DataTable;
                    data = Encoding.ASCII.GetBytes(dt.Rows[0]["VisitiorPhoto"].ToString());
                    Session["ImageData"] = data;
                    TranID = Convert.ToInt32(Request.QueryString["TranID"].ToString());
                    imgVisitor.ImageUrl = "~/RetrivedImageHandler.ashx?TranID=" + TranID + " &PassType=VPASS";
                    txtVID.Text = dt.Rows[0]["VisitorTranID"].ToString();
                    txtVName.Text = dt.Rows[0]["VisitorName"].ToString();
                    txtMobileNUmber.Text = dt.Rows[0]["MobileNumber"].ToString();
                    ddlorg.Text = dt.Rows[0]["Oragantization"].ToString();
                    txtnation.Text = dt.Rows[0]["Nationality"].ToString();
                    ddlidentity.Text = dt.Rows[0]["ID_Type"].ToString();
                    txtidno.Text = dt.Rows[0]["ID_No"].ToString();
                    txtage.Text = dt.Rows[0]["Age"].ToString();
                    txtGender.Text = dt.Rows[0]["Sex"].ToString();
                    txtFromDate1.Text = dt.Rows[0]["FromDate"].ToString();
                    txtTodate.Text = dt.Rows[0]["ToDate"].ToString();
                    ddlidentity.Text = dt.Rows[0]["ID_Type"].ToString();
                    txtidno.Text = dt.Rows[0]["ID_No"].ToString();
                    txtInTime.Text = DateTime.Now.Hour + ":" + DateTime.Now.Minute + ":" + DateTime.Now.Second; //dt.Rows[0]["TimeIn"].ToString();
                    txtTPNumber.Text = dt.Rows[0]["EmployeeCode"].ToString();
                    txtEmployeeName.Text = dt.Rows[0]["EmployeeName"].ToString();
                    txtDepartmentName.Text = dt.Rows[0]["DepartmentID"].ToString();
                    txtHostName.Text = dt.Rows[0]["HostId"].ToString();
                    txtTPNumber.Text = dt.Rows[0]["EmployeeCode"].ToString();
                    txtEmployeeName.Text = dt.Rows[0]["EmployeeName"].ToString();
                    img_visitor_indicator.ImageUrl = dt.Rows[0]["Status"].ToString();
                    txtPassNo.Text = dt.Rows[0]["PassNumber"].ToString();
                    txtCardNo.Text = dt.Rows[0]["CardNumber"].ToString();
                    txtValidity.Text = dt.Rows[0]["ToDate"].ToString();

                    print_barcode(TranID.ToString());
                    Get_Visitor_type(txtVID.Text, dt.Rows[0]["Status"].ToString());
                    try
                    {
                        get_vehical_detail(txtVID.Text);
                    }
                    catch (Exception ee)
                    {
                    }

                    get_Gadget_detail(txtVID.Text);
                }
                catch (Exception ex)
                {
                }
                finally
                {
                    ds.Reset();
                }
                make_printed();
            }
            GetOtehrDetail();
            updateID_OfcrTran();
            //txtVID.Text = dt.Rows[0]["VisitorTranID"].ToString();
        }

        public void updateID_OfcrTran()
        {
            //txtVID.Text = dt.Rows[0]["VisitorTranID"].ToString();
            string query = "Update Officer_Transaction set V_PassNo='" + txtVID.Text + "' where Ofcr_Tran_Id='" + Request.QueryString["Off_tran_ID"] + "'";
            update_db(query);
        }

        public void make_printed()
        {
            off_tran_id = Convert.ToInt32(Request.QueryString["Off_tran_ID"].ToString());
            string query = "update Officer_Transaction set Print_status='Y' , Print_Date=GetDate() where Ofcr_Tran_Id='" + off_tran_id + "'";
            update_db(query);

        }

        public void update_db(string query)
        {
            try
            {
                off_tran_id = Convert.ToInt32(Request.QueryString["Off_tran_ID"].ToString());
                //query = "update Officer_Transaction set Print_status='Y' where Ofcr_Tran_Id='" + off_tran_id + "'";
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["VMSConnection"].ToString());
                con.Open();
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ee)
            {
            }
        }

        public void print_barcode(string barCode)
        {
            string LogID = GetSerialNo_for_barcode(barCode);
            string ENCODEVALUE = LogID;
            System.Web.UI.WebControls.Image imgBarCode = new System.Web.UI.WebControls.Image();
            imgBarCode.Width = 250;
            imgBarCode.Height = 50;
            using (Bitmap bitMap = new Bitmap(ENCODEVALUE.Length * 40, 80))
            {
                using (Graphics graphics = Graphics.FromImage(bitMap))
                {
                    Font oFont = new Font("IDAutomationHC39M", 16);
                    PointF point = new PointF(2f, 2f);
                    SolidBrush blackBrush = new SolidBrush(Color.Black);
                    SolidBrush whiteBrush = new SolidBrush(Color.White);
                    graphics.FillRectangle(whiteBrush, 0, 0, bitMap.Width, bitMap.Height);
                    graphics.DrawString("*" + ENCODEVALUE + "*", oFont, blackBrush, point);
                }
                using (MemoryStream ms = new MemoryStream())
                {
                    bitMap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                    byte[] byteImage = ms.ToArray();

                    Convert.ToBase64String(byteImage);
                    imgBarCode.ImageUrl = "data:image/png;base64," + Convert.ToBase64String(byteImage);
                }
                plBarCode.Controls.Add(imgBarCode);
            }
        }

        public string GetSerialNo_for_barcode(string TranID)
        {
            string LogID = "";

            // int index = (int)(Convert.ToChar(txtVName.Text.Substring(0, 1))) - 64;
            //  LogID = Utility.base64Encode(txtPassNo.Text + index + DateTime.Now.Second);

            LogID = Helper.encreptSubstution(txtPassNo.Text);
            try
            {

                SqlConnection scon = new SqlConnection(ConfigurationManager.ConnectionStrings["VMSConnection"].ToString());
                scon.Open();
                DataSet sds = new DataSet();
                SqlCommand scmd = new SqlCommand();

                string query = "update VisitorLogDetail set barcodeID='" + LogID + "' where serialNo='" + txtPassNo.Text + "'";
                scmd = new SqlCommand(query, scon);
                scmd.ExecuteNonQuery();
            }
            catch (Exception ee)
            {

            }
            return LogID;
        }

        public void Substitute()
        {

        }

        public void get_vehical_detail(string VisitorID)
        {
            ParameterList.AddParameter.Clear();
            stParameterDetails.Value = VisitorID;
            stParameterDetails.DataType = SqlDbType.VarChar;
            ParameterList.AddParameter.Add("VisitorID", stParameterDetails);
            using (Project.Db.DbConnection db = new Project.Db.DbConnection())
            {
                ds = db.CommonCollection.GetAsDataSet("dbo.Vehicle_Select", ParameterList.AddParameter);
                //lblvehname.Text = ds.Tables[0].Rows[0][1].ToString();
                //lblVehno.Text = ds.Tables[0].Rows[0][2].ToString();
            }
        }

        public void get_Gadget_detail(string VisitorID)
        {
            ParameterList.AddParameter.Clear();
            stParameterDetails.Value = VisitorID;
            stParameterDetails.DataType = SqlDbType.VarChar;
            ParameterList.AddParameter.Add("VisitorID", stParameterDetails);
            using (Project.Db.DbConnection db = new Project.Db.DbConnection())
            {
                ds = db.CommonCollection.GetAsDataSet("dbo.Gadget_Select", ParameterList.AddParameter);

            }
        }

        public void Get_Visitor_type(string VisitorID, string defined_color)
        {
            if (defined_color.Contains("green"))
            {
                lbl_visitor_indicator.Text = "Green";
                lbl_visitor_indicator.ForeColor = System.Drawing.Color.Green;
                img_visitor_indicator.ImageUrl = defined_color;
            }
            else
            {
                ParameterList.AddParameter.Clear();
                stParameterDetails.Value = "Visitor ID";
                stParameterDetails.DataType = SqlDbType.VarChar;
                ParameterList.AddParameter.Add("SearchCriteria", stParameterDetails);

                stParameterDetails.Value = VisitorID;
                stParameterDetails.DataType = SqlDbType.VarChar;
                ParameterList.AddParameter.Add("SearchParameter", stParameterDetails);


                using (Project.Db.DbConnection db = new Project.Db.DbConnection())
                {
                    ds = db.CommonCollection.GetAsDataSet("dbo.VisitorLogDetail_LoadDetails", ParameterList.AddParameter);
                    if (ds.Tables[0].Rows.Count > 1)
                    {
                        lbl_visitor_indicator.Text = "YELLOW";
                        lbl_visitor_indicator.ForeColor = System.Drawing.Color.Black;
                        img_visitor_indicator.ImageUrl = "~/Images/orange-circle.png";
                    }
                    else
                    {
                        lbl_visitor_indicator.Text = "RED";
                        lbl_visitor_indicator.ForeColor = System.Drawing.Color.Red;
                        img_visitor_indicator.ImageUrl = "~/Images/red-triangle.png";
                    }
                    // lbldevicename.Text = ds.Tables[0].Rows[0][3].ToString();
                    //  lbldeviceNo.Text = ds.Tables[0].Rows[0][4].ToString();
                }
            }
        }

        protected void btnprint_Click(object sender, EventArgs e)
        {

        }

        public void GetOtehrDetail()
        {
            TranID = Convert.ToInt32(Request.QueryString["TranID"].ToString());
            off_tran_id = Convert.ToInt32(Request.QueryString["Off_tran_ID"].ToString());
            string query = "select * from Gadget_Details where Ofcr_Tran_Id='" + off_tran_id + "'";
            DataTable dt = get_data_from_DB(query).Tables[0];
            GVGadget.DataSource = dt;
            GVGadget.DataBind();
            if (dt.Rows.Count == 0)
            {
                Gstatus.Text = "NA";
            }
            else
            {
                Gstatus.Text = "";
            }

            //lblvehname.Text = dt.Rows[0]["VehicleName"].ToString();
            //lblVehno.Text = dt.Rows[0]["VehicleNo"].ToString();
            string query1 = "select * from VehicleDetails where Ofcr_Tran_Id='" + off_tran_id + "'";
            DataTable dt1 = get_data_from_DB(query1).Tables[0];
            GVVehicle.DataSource = dt1;
            GVVehicle.DataBind();
            if (dt1.Rows.Count == 0)
            {
                Vstatus.Text = "NA";
            }
            else
            {
                Vstatus.Text = "";
            }
            //lbldevicename.Text = dt.Rows[0]["GadgetName"].ToString();
            //lbldeviceNo.Text = dt.Rows[0]["Gadget_SerialNo"].ToString();
        }

        public DataSet get_data_from_DB(string query)
        {
            SqlConnection scon = new SqlConnection(ConfigurationManager.ConnectionStrings["VMSConnection"].ToString());
            SqlCommand scmd;
            SqlDataAdapter sadp;
            DataSet sds = new System.Data.DataSet();

            using (scmd = new SqlCommand(query, scon))
            {
                sadp = new SqlDataAdapter(scmd);
                sadp.Fill(sds);
            }
            return sds;
        }
    }
}