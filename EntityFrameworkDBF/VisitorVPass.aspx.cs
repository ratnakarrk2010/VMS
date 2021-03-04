using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;
using System.Data;
using System.ServiceModel.Channels;
using System.Web.Security;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Globalization;
using System.Collections.Specialized;
using Microsoft.Reporting.WebForms;

namespace EntityFrameworkDBF
{
    public partial class VisitorVPass : System.Web.UI.Page
    {
        protected ParameterDetails stParameterDetails;
        DataSet ds = new DataSet();
        //DisplayLog objDisplayLog = new DisplayLog();
        public DataTable _griedBindData;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtfromdate.Text = DateTime.Now.Day + "/" + DateTime.Now.Month + "/" + DateTime.Now.Year;
                txttodate.Text = DateTime.Now.Day + "/" + DateTime.Now.Month + "/" + DateTime.Now.Year;
                btnCheckOut.Visible = false;
                btnExport.Visible = false;
                btnDelete.Visible = false;
                //GridViewPagingControl.Visible = false;
                CVIssued.Visible = false;
                CVReturned.Visible = false;
                CVBalanced.Visible = false;
                txti.Visible = false;
                txtr.Visible = false;
                txtb.Visible = false;
                Label18.Visible = false;
                Label19.Visible = false;
                Label20.Visible = false;
                ReportViewer2.Visible = false;
            }
            //GridViewPagingControl.pagingClickArgs += new EventHandler(Paging_Click);
        }

        public void BindSearchInfo(string sortExpression, string sortDirection)
        {
            try
            {
                if (rdoForeign.Checked == false && rdoVisitor.Checked == false)
                {
                    SetErrorMessage("Please Select Atleast One Report", "Info");
                }
                else
                {
                    ParameterList.AddParameter.Clear();
                    if ((ddlSearchBy.SelectedItem.Text == "Visitor ID") && (txtSearch.Text == "") && CheckForeignNull.Checked == true)
                    {
                        ReportViewer2.Visible = false;
                        ParameterList.AddParameter.Clear();

                        //stParameterDetails.Value = txtSearch.Text;
                        //stParameterDetails.DataType = SqlDbType.VarChar;
                        //ParameterList.AddParameter.Add("SearchParameter", stParameterDetails);

                        stParameterDetails.Value = txtfromdate.Text;
                        stParameterDetails.DataType = SqlDbType.DateTime;
                        ParameterList.AddParameter.Add("SearchFrom", stParameterDetails);

                        stParameterDetails.Value = txttodate.Text;
                        stParameterDetails.DataType = SqlDbType.DateTime;
                        ParameterList.AddParameter.Add("SearchTo", stParameterDetails);

                        stParameterDetails.Value = gvPass.PageIndex;
                        stParameterDetails.DataType = SqlDbType.Int;
                        ParameterList.AddParameter.Add("pageIndex", stParameterDetails);

                        stParameterDetails.Value = gvPass.PageSize;
                        stParameterDetails.DataType = SqlDbType.Int;
                        ParameterList.AddParameter.Add("pageSize", stParameterDetails);

                        using (Project.Db.DbConnection db = new Project.Db.DbConnection())
                        {
                            if (CheckNull.Checked == true && rdoVisitor.Checked == true)
                            {
                                ds = db.CommonCollection.GetAsDataSet("dbo.VisitorTransactionDetail_LoadDetails", ParameterList.AddParameter);
                            }
                            else if (CheckForeignNull.Checked == true && rdoForeign.Checked == true)
                            {
                                ds = db.CommonCollection.GetAsDataSet("dbo.foreignVisitorTransactionDetail_LoadDetails", ParameterList.AddParameter);
                            }
                            // DONE
                        }
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            //GridViewPagingControl.Visible = true;
                            gvPass.DataSource = ds.Tables[1];
                            gvPass.DataBind();

                            //GridViewPagingControl.Visible = true;

                            if (ds.Tables[0].Rows.Count > 0)
                            {
                                //((HiddenField)GridViewPagingControl.FindControl("TotalRows")).Value = ds.Tables[0].Rows[0]["recordCount"].ToString();
                            }

                            if (gvPass.PageIndex.ToString() == "0")
                            {
                                //((Label)GridViewPagingControl.FindControl("RecordDisplaySummary")).Text = "Records: 1 -" + (Convert.ToInt64(ds.Tables[0].Rows[0][0]) >= Convert.ToInt32(((DropDownList)GridViewPagingControl.FindControl("ddlPageSize")).SelectedValue) ? "10" : Convert.ToString(ds.Tables[0].Rows[0][0])) + " " + "of" + " " + Convert.ToString(ds.Tables[0].Rows[0][0]);
                                //((Label)GridViewPagingControl.FindControl("PageDisplaySummary")).Text = "Page 1 of " + Decimal.Ceiling(Convert.ToDecimal(ds.Tables[0].Rows[0][0]) / Convert.ToInt32(((DropDownList)GridViewPagingControl.FindControl("ddlPageSize")).SelectedValue));
                            }
                            btnCheckOut.Visible = true;
                            btnExport.Visible = true;
                            btnDelete.Visible = false;
                        }
                        else
                        {
                            gvPass.DataSource = null;
                            gvPass.DataBind();
                            //((HiddenField)GridViewPagingControl.FindControl("TotalRows")).Value = "0";
                            // GridViewPagingControl.Visible = false;
                        }
                        if (ds.Tables[0].Rows.Count == 0)
                        {
                            SetErrorMessage("No visitor found", "Info");
                        }
                        Session["dtPassDetails"] = ds.Tables[1];
                        ////  ViewState["dtPassDetails"] = ds.Tables[1];
                    }
                    else if (CheckNull.Checked == true)
                    {
                        ReportViewer2.Visible = false;
                        ParameterList.AddParameter.Clear();

                        stParameterDetails.Value = txtfromdate.Text;
                        stParameterDetails.DataType = SqlDbType.DateTime;
                        ParameterList.AddParameter.Add("SearchFrom", stParameterDetails);

                        stParameterDetails.Value = txttodate.Text;
                        stParameterDetails.DataType = SqlDbType.DateTime;
                        ParameterList.AddParameter.Add("SearchTo", stParameterDetails);

                        stParameterDetails.Value = gvPass.PageIndex;
                        stParameterDetails.DataType = SqlDbType.Int;
                        ParameterList.AddParameter.Add("pageIndex", stParameterDetails);

                        stParameterDetails.Value = gvPass.PageSize;
                        stParameterDetails.DataType = SqlDbType.Int;
                        ParameterList.AddParameter.Add("pageSize", stParameterDetails);
                        using (Project.Db.DbConnection db = new Project.Db.DbConnection())
                        {
                            if (CheckNull.Checked == true)
                            {
                                ds = db.CommonCollection.GetAsDataSet("dbo.VisitorLogDetail_SelectDatewise_WitoutTime", ParameterList.AddParameter);
                            }
                            else
                            {
                                ds = db.CommonCollection.GetAsDataSet("dbo.ForeignVisitorLogDetail_SelectDatewise_WitoutTime", ParameterList.AddParameter);
                            }
                        }
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            //GridViewPagingControl.Visible = true;
                            gvPass.DataSource = ds.Tables[1];
                            gvPass.DataBind();

                            //GridViewPagingControl.Visible = true;

                            if (ds.Tables[0].Rows.Count > 0)
                            {
                                //  ((HiddenField)GridViewPagingControl.FindControl("TotalRows")).Value = ds.Tables[0].Rows[0]["recordCount"].ToString();
                            }

                            if (gvPass.PageIndex.ToString() == "0")
                            {
                                //((Label)GridViewPagingControl.FindControl("RecordDisplaySummary")).Text = "Records: 1 -" + (Convert.ToInt64(ds.Tables[0].Rows[0][0]) >= Convert.ToInt32(((DropDownList)GridViewPagingControl.FindControl("ddlPageSize")).SelectedValue) ? "10" : Convert.ToString(ds.Tables[0].Rows[0][0])) + " " + "of" + " " + Convert.ToString(ds.Tables[0].Rows[0][0]);
                                //((Label)GridViewPagingControl.FindControl("PageDisplaySummary")).Text = "Page 1 of " + Decimal.Ceiling(Convert.ToDecimal(ds.Tables[0].Rows[0][0]) / Convert.ToInt32(((DropDownList)GridViewPagingControl.FindControl("ddlPageSize")).SelectedValue));
                            }
                            btnCheckOut.Visible = true;
                            btnExport.Visible = true;
                            btnDelete.Visible = false;
                        }
                        else
                        {
                            gvPass.DataSource = null;
                            gvPass.DataBind();
                            //((HiddenField)GridViewPagingControl.FindControl("TotalRows")).Value = "0";
                            //GridViewPagingControl.Visible = false;
                        }

                        if (ds.Tables[0].Rows.Count == 0)
                        {
                            SetErrorMessage("No visitor found", "Info");
                        }
                        Session["dtPassDetails"] = ds.Tables[1];
                    }
                    else
                    {
                        ReportViewer2.Visible = true;

                        ParameterList.AddParameter.Clear();
                        stParameterDetails.Value = txtfromdate.Text;
                        stParameterDetails.DataType = SqlDbType.DateTime;
                        ParameterList.AddParameter.Add("SearchFrom", stParameterDetails);

                        stParameterDetails.Value = txttodate.Text;
                        stParameterDetails.DataType = SqlDbType.DateTime;
                        ParameterList.AddParameter.Add("SearchTo", stParameterDetails);

                        stParameterDetails.Value = gvPass.PageIndex;
                        stParameterDetails.DataType = SqlDbType.BigInt;
                        ParameterList.AddParameter.Add("pageIndex", stParameterDetails);

                        stParameterDetails.Value = gvPass.PageSize;
                        stParameterDetails.DataType = SqlDbType.BigInt;
                        ParameterList.AddParameter.Add("pageSize", stParameterDetails);

                        using (Project.Db.DbConnection db = new Project.Db.DbConnection())
                        {
                            if (rdoVisitor.Checked == true)
                            {
                                ds = db.CommonCollection.GetAsDataSet("dbo.VisitorLogDetail_SelectDatewise_WitoutTimeOut", ParameterList.AddParameter);
                            }
                            else if (rdoForeign.Checked == true)
                            {
                                ds = db.CommonCollection.GetAsDataSet("dbo.foreignLogDetail_SelectDatewise_WitoutTimeOut", ParameterList.AddParameter);
                            }
                        }
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            //GridViewPagingControl.Visible = true;
                            gvPass.DataSource = ds.Tables[1];
                            gvPass.DataBind();

                            //GridViewPagingControl.Visible = true;

                            if (ds.Tables[0].Rows.Count > 0)
                            {
                                //((HiddenField)GridViewPagingControl.FindControl("TotalRows")).Value = ds.Tables[0].Rows[0]["recordCount"].ToString();
                            }

                            if (gvPass.PageIndex.ToString() == "0")
                            {
                                //((Label)GridViewPagingControl.FindControl("RecordDisplaySummary")).Text = "Records: 1 -" + (Convert.ToInt64(ds.Tables[0].Rows[0][0]) >= Convert.ToInt32(((DropDownList)GridViewPagingControl.FindControl("ddlPageSize")).SelectedValue) ? "10" : Convert.ToString(ds.Tables[0].Rows[0][0])) + " " + "of" + " " + Convert.ToString(ds.Tables[0].Rows[0][0]);
                                //((Label)GridViewPagingControl.FindControl("PageDisplaySummary")).Text = "Page 1 of " + Decimal.Ceiling(Convert.ToDecimal(ds.Tables[0].Rows[0][0]) / Convert.ToInt32(((DropDownList)GridViewPagingControl.FindControl("ddlPageSize")).SelectedValue));
                            }
                            btnCheckOut.Visible = true;
                            btnExport.Visible = true;
                            btnDelete.Visible = false;

                            using (Project.Db.DbConnection db = new Project.Db.DbConnection())
                            {
                                if (rdoVisitor.Checked == true)
                                {
                                    ds = db.CommonCollection.GetAsDataSet("dbo.VisitorLogDetail_SelectDatewise_WitoutTimeOutrdlc", ParameterList.AddParameter);
                                }
                                else if (rdoForeign.Checked == true)
                                {
                                    ds = db.CommonCollection.GetAsDataSet("dbo.foreignVisitorLogDetail_SelectDatewise_WitoutTimeOutrdlc", ParameterList.AddParameter);
                                }
                            }
                            ReportDataSource rds = new ReportDataSource();
                            rds.Name = "DataSet1";
                            rds.Value = ds.Tables[1];
                            ReportViewer2.LocalReport.DataSources.Clear();
                            ReportViewer2.LocalReport.ReportPath = Server.MapPath("~/VisitorVReport.rdlc");
                            ReportViewer2.LocalReport.DataSources.Add(rds);
                            ReportViewer2.LocalReport.Refresh();
                            btnExport.Visible = false;

                            if (ds.Tables[1].Rows.Count == 0)
                            {
                                //GridViewPagingControl.Visible = false;
                                ReportViewer2.Visible = false;
                                btnDelete.Visible = false;
                                btnCheckOut.Visible = false;
                                SetErrorMessage("No visitor found", "Info");
                            }
                            //  clearControl();
                        }
                        else
                        {
                            gvPass.DataSource = null;
                            gvPass.DataBind();
                            //((HiddenField)GridViewPagingControl.FindControl("TotalRows")).Value = "0";
                            //GridViewPagingControl.Visible = false;
                        }
                        if (ds.Tables[0].Rows.Count == 0)
                        {
                            SetErrorMessage("No visitor found", "Info");
                        }

                        //   ViewState["dtPassDetails"] = ds.Tables[1];
                    }
                }
                //ViewState["dtPassDetails"] = ds.Tables[1]; 
            }
            catch (Exception ex)
            {
                //objDisplayLog.CustomMessage("Error while Searching the visitor", this);
            }
        }

        #region comment block
        //public void BindSearchInfo(string sortExpression, string sortDirection)
        //{
        //    try
        //    {
        //        ParameterList.AddParameter.Clear();
        //        if ((ddlSearchBy.SelectedItem.Text == "Visitor ID") && (txtSearch.Text != ""))
        //        {
        //            ReportViewer2.Visible = false;
        //            ParameterList.AddParameter.Clear();

        //            stParameterDetails.Value = txtSearch.Text;
        //            stParameterDetails.DataType = SqlDbType.VarChar;
        //            ParameterList.AddParameter.Add("SearchParameter", stParameterDetails);

        //            stParameterDetails.Value = txtfromdate.Text;
        //            stParameterDetails.DataType = SqlDbType.DateTime;
        //            ParameterList.AddParameter.Add("SearchFrom", stParameterDetails);

        //            stParameterDetails.Value = txttodate.Text;
        //            stParameterDetails.DataType = SqlDbType.DateTime;
        //            ParameterList.AddParameter.Add("SearchTo", stParameterDetails);

        //            stParameterDetails.Value = gvPass.PageIndex;
        //            stParameterDetails.DataType = SqlDbType.Int;
        //            ParameterList.AddParameter.Add("pageIndex", stParameterDetails);

        //            stParameterDetails.Value = gvPass.PageSize;
        //            stParameterDetails.DataType = SqlDbType.Int;
        //            ParameterList.AddParameter.Add("pageSize", stParameterDetails);

        //            using (Project.Db.DbConnection db = new Project.Db.DbConnection())
        //            {
        //                if (CheckNull.Checked == true)
        //                {

        //                    ds = db.CommonCollection.GetAsDataSet("dbo.VisitorTransactionDetail_LoadDetails", ParameterList.AddParameter);
        //                }
        //                else
        //                {
        //                    ds = db.CommonCollection.GetAsDataSet("dbo.foreignVisitorTransactionDetail_LoadDetails", ParameterList.AddParameter);
        //                }
        //            }
        //            if (ds.Tables[0].Rows.Count > 0)
        //            {
        //                GridViewPagingControl.Visible = true;
        //                gvPass.DataSource = ds.Tables[1];
        //                gvPass.DataBind();

        //                GridViewPagingControl.Visible = true;

        //                if (ds.Tables[0].Rows.Count > 0)
        //                {
        //                    ((HiddenField)GridViewPagingControl.FindControl("TotalRows")).Value = ds.Tables[0].Rows[0]["recordCount"].ToString();
        //                }

        //                if (gvPass.PageIndex.ToString() == "0")
        //                {
        //                    ((Label)GridViewPagingControl.FindControl("RecordDisplaySummary")).Text = "Records: 1 -" + (Convert.ToInt64(ds.Tables[0].Rows[0][0]) >= Convert.ToInt32(((DropDownList)GridViewPagingControl.FindControl("ddlPageSize")).SelectedValue) ? "10" : Convert.ToString(ds.Tables[0].Rows[0][0])) + " " + "of" + " " + Convert.ToString(ds.Tables[0].Rows[0][0]);
        //                    ((Label)GridViewPagingControl.FindControl("PageDisplaySummary")).Text = "Page 1 of " + Decimal.Ceiling(Convert.ToDecimal(ds.Tables[0].Rows[0][0]) / Convert.ToInt32(((DropDownList)GridViewPagingControl.FindControl("ddlPageSize")).SelectedValue));
        //                }
        //                btnCheckOut.Visible = true;
        //                btnExport.Visible = true;
        //                btnDelete.Visible = false;
        //            }
        //            else
        //            {
        //                gvPass.DataSource = null;
        //                gvPass.DataBind();
        //                ((HiddenField)GridViewPagingControl.FindControl("TotalRows")).Value = "0";
        //                GridViewPagingControl.Visible = false;
        //            }

        //            if (ds.Tables[0].Rows.Count == 0)
        //            {
        //                SetErrorMessage("No visitor found", "Info");
        //            }
        //            Session["dtPassDetails"] = ds.Tables[1];
        //            ////  ViewState["dtPassDetails"] = ds.Tables[1];
        //        }

        //        else if (CheckNull.Checked == true)
        //        {
        //            ReportViewer2.Visible = false;
        //            ParameterList.AddParameter.Clear();

        //            stParameterDetails.Value = txtfromdate.Text;
        //            stParameterDetails.DataType = SqlDbType.DateTime;
        //            ParameterList.AddParameter.Add("SearchFrom", stParameterDetails);

        //            stParameterDetails.Value = txttodate.Text;
        //            stParameterDetails.DataType = SqlDbType.DateTime;
        //            ParameterList.AddParameter.Add("SearchTo", stParameterDetails);

        //            stParameterDetails.Value = gvPass.PageIndex;
        //            stParameterDetails.DataType = SqlDbType.Int;
        //            ParameterList.AddParameter.Add("pageIndex", stParameterDetails);

        //            stParameterDetails.Value = gvPass.PageSize;
        //            stParameterDetails.DataType = SqlDbType.Int;
        //            ParameterList.AddParameter.Add("pageSize", stParameterDetails);

        //            if (CheckNull.Checked == true)
        //            {
        //                using (Project.Db.DbConnection db = new Project.Db.DbConnection())
        //                {
        //                    ds = db.CommonCollection.GetAsDataSet("dbo.VisitorLogDetail_SelectDatewise_WitoutTime", ParameterList.AddParameter);
        //                }
        //            }
        //            else
        //            {
        //                using (Project.Db.DbConnection db = new Project.Db.DbConnection())
        //                {
        //                    ds = db.CommonCollection.GetAsDataSet("dbo.foreignLogDetail_SelectDatewise_WitoutTimeOut", ParameterList.AddParameter);

        //                }
        //            }


        //            if (ds.Tables[0].Rows.Count > 0)
        //            {
        //                GridViewPagingControl.Visible = true;
        //                gvPass.DataSource = ds.Tables[1];
        //                gvPass.DataBind();

        //                GridViewPagingControl.Visible = true;

        //                if (ds.Tables[0].Rows.Count > 0)
        //                {
        //                    ((HiddenField)GridViewPagingControl.FindControl("TotalRows")).Value = ds.Tables[0].Rows[0]["recordCount"].ToString();
        //                }

        //                if (gvPass.PageIndex.ToString() == "0")
        //                {
        //                    ((Label)GridViewPagingControl.FindControl("RecordDisplaySummary")).Text = "Records: 1 -" + (Convert.ToInt64(ds.Tables[0].Rows[0][0]) >= Convert.ToInt32(((DropDownList)GridViewPagingControl.FindControl("ddlPageSize")).SelectedValue) ? "10" : Convert.ToString(ds.Tables[0].Rows[0][0])) + " " + "of" + " " + Convert.ToString(ds.Tables[0].Rows[0][0]);
        //                    ((Label)GridViewPagingControl.FindControl("PageDisplaySummary")).Text = "Page 1 of " + Decimal.Ceiling(Convert.ToDecimal(ds.Tables[0].Rows[0][0]) / Convert.ToInt32(((DropDownList)GridViewPagingControl.FindControl("ddlPageSize")).SelectedValue));
        //                }
        //                btnCheckOut.Visible = true;
        //                btnExport.Visible = true;
        //                btnDelete.Visible = false;
        //            }
        //            else
        //            {
        //                gvPass.DataSource = null;
        //                gvPass.DataBind();
        //                ((HiddenField)GridViewPagingControl.FindControl("TotalRows")).Value = "0";
        //                GridViewPagingControl.Visible = false;
        //            }

        //            if (ds.Tables[0].Rows.Count == 0)
        //            {
        //                SetErrorMessage("No visitor found", "Info");
        //            }
        //            Session["dtPassDetails"] = ds.Tables[1];
        //        }
        //        else
        //        {
        //            ReportViewer2.Visible = true;

        //            ParameterList.AddParameter.Clear();
        //            stParameterDetails.Value = txtfromdate.Text;
        //            stParameterDetails.DataType = SqlDbType.DateTime;
        //            ParameterList.AddParameter.Add("SearchFrom", stParameterDetails);

        //            stParameterDetails.Value = txttodate.Text;
        //            stParameterDetails.DataType = SqlDbType.DateTime;
        //            ParameterList.AddParameter.Add("SearchTo", stParameterDetails);

        //            stParameterDetails.Value = gvPass.PageIndex;
        //            stParameterDetails.DataType = SqlDbType.BigInt;
        //            ParameterList.AddParameter.Add("pageIndex", stParameterDetails);

        //            stParameterDetails.Value = gvPass.PageSize;
        //            stParameterDetails.DataType = SqlDbType.BigInt;
        //            ParameterList.AddParameter.Add("pageSize", stParameterDetails);
        //            //
        //            using (Project.Db.DbConnection db = new Project.Db.DbConnection())
        //            {
        //                if (CheckNull.Checked == true)
        //                {
        //                    ds = db.CommonCollection.GetAsDataSet("dbo.VisitorLogDetail_SelectDatewise_WitoutTimeOut", ParameterList.AddParameter);
        //                }
        //                else
        //                {
        //                    ds = db.CommonCollection.GetAsDataSet("dbo.foreignLogDetail_SelectDatewise_WitoutTimeOut", ParameterList.AddParameter);
        //                }
        //            }
        //            if (ds.Tables[0].Rows.Count > 0)
        //            {
        //                GridViewPagingControl.Visible = true;
        //                gvPass.DataSource = ds.Tables[0];
        //                gvPass.DataBind();

        //                GridViewPagingControl.Visible = true;

        //                if (ds.Tables[0].Rows.Count > 0)
        //                {
        //                    ((HiddenField)GridViewPagingControl.FindControl("TotalRows")).Value = ds.Tables[0].Rows[0]["recordCount"].ToString();
        //                }

        //                if (gvPass.PageIndex.ToString() == "0")
        //                {
        //                    ((Label)GridViewPagingControl.FindControl("RecordDisplaySummary")).Text = "Records: 1 -" + (Convert.ToInt64(ds.Tables[0].Rows[0][0]) >= Convert.ToInt32(((DropDownList)GridViewPagingControl.FindControl("ddlPageSize")).SelectedValue) ? "10" : Convert.ToString(ds.Tables[0].Rows[0][0])) + " " + "of" + " " + Convert.ToString(ds.Tables[0].Rows[0][0]);
        //                    ((Label)GridViewPagingControl.FindControl("PageDisplaySummary")).Text = "Page 1 of " + Decimal.Ceiling(Convert.ToDecimal(ds.Tables[0].Rows[0][0]) / Convert.ToInt32(((DropDownList)GridViewPagingControl.FindControl("ddlPageSize")).SelectedValue));
        //                }
        //                btnCheckOut.Visible = true;
        //                btnExport.Visible = true;
        //                btnDelete.Visible = false;

        //                using (Project.Db.DbConnection db = new Project.Db.DbConnection())
        //                {
        //                    if (CheckNull.Checked == true)
        //                    {
        //                        ds = db.CommonCollection.GetAsDataSet("dbo.VisitorLogDetail_SelectDatewise_WitoutTimeOutrdlc", ParameterList.AddParameter);
        //                    }
        //                    else
        //                    {
        //                        ds = db.CommonCollection.GetAsDataSet("dbo.foreignVisitorLogDetail_SelectDatewise_WitoutTimeOutrdlc", ParameterList.AddParameter);
        //                    }
        //                }
        //                ReportDataSource rds = new ReportDataSource();
        //                rds.Name = "DataSet1";
        //                rds.Value = ds.Tables[1];
        //                ReportViewer2.LocalReport.DataSources.Clear();
        //                ReportViewer2.LocalReport.ReportPath = Server.MapPath("~/VisitorVReport.rdlc");
        //                ReportViewer2.LocalReport.DataSources.Add(rds);
        //                ReportViewer2.LocalReport.Refresh();
        //                btnExport.Visible = false;

        //                if (ds.Tables[1].Rows.Count == 0)
        //                {
        //                    GridViewPagingControl.Visible = false;
        //                    ReportViewer2.Visible = false;
        //                    btnDelete.Visible = false;
        //                    btnCheckOut.Visible = false;
        //                    SetErrorMessage("No visitor found", "Info");
        //                }
        //                //  clearControl();

        //            }
        //            else
        //            {
        //                gvPass.DataSource = null;
        //                gvPass.DataBind();
        //                ((HiddenField)GridViewPagingControl.FindControl("TotalRows")).Value = "0";
        //                GridViewPagingControl.Visible = false;
        //            }

        //            if (ds.Tables[0].Rows.Count == 0)
        //            {
        //                SetErrorMessage("No visitor found", "Info");
        //            }

        //            //   ViewState["dtPassDetails"] = ds.Tables[1];
        //        }
        //        //ViewState["dtPassDetails"] = ds.Tables[1]; 
        //    }
        //    catch (Exception ex)
        //    {
        //        objDisplayLog.CustomMessage("Error while Searching the visitor", this);
        //    }
        //}
        #endregion

        protected void btnShow_Click(object sender, EventArgs e)
        {
            CVIssued.Visible = false;
            CVReturned.Visible = false;
            CVBalanced.Visible = false;
            txti.Visible = false;
            txtr.Visible = false;
            txtb.Visible = false;
            Label18.Visible = false;
            Label19.Visible = false;
            Label20.Visible = false;
            SetErrorMessage("", "");
            Session["dtPassDetails"] = null;
            BindSearchInfo(GridViewsortExpression, GridViewSortDirection);
        }

        protected void btntotal_Click(object sender, EventArgs e)
        {
            ParameterList.AddParameter.Clear();

            stParameterDetails.Value = txtfromdate.Text;
            stParameterDetails.DataType = SqlDbType.DateTime;
            ParameterList.AddParameter.Add("SearchFrom", stParameterDetails);

            stParameterDetails.Value = txttodate.Text;
            stParameterDetails.DataType = SqlDbType.DateTime;
            ParameterList.AddParameter.Add("SearchTo", stParameterDetails);

            using (Project.Db.DbConnection db = new Project.Db.DbConnection())
            {
                if (rdoVisitor.Checked == true)
                {
                    ds = db.CommonCollection.GetAsDataSet("dbo.VisitorLogDetail_Calculations", ParameterList.AddParameter);
                }
                else if (rdoForeign.Checked == true)
                {
                    ds = db.CommonCollection.GetAsDataSet("dbo.foreignVisitorLogDetail_Calculations", ParameterList.AddParameter);
                }
            }
            txti.Text = ds.Tables[0].Rows[0]["TotalCount"].ToString();
            txtr.Text = ds.Tables[0].Rows[0]["OutCount"].ToString();
            txtb.Text = ds.Tables[0].Rows[0]["RemainingCount"].ToString();
            CVIssued.Visible = true;
            CVReturned.Visible = true;
            CVBalanced.Visible = true;
            txti.Visible = true;
            txtr.Visible = true;
            txtb.Visible = true;
            Label18.Visible = true;
            Label19.Visible = true;
            Label20.Visible = true;
            ds.Reset();
        }
        protected void btnhome_Click(object sender, EventArgs e)
        {

            Response.Redirect("~\\DVSC_HOME.aspx");

        }
        private void Paging_Click(object sender, EventArgs e)
        {
            //gvPass.PageSize = Convert.ToInt32(((DropDownList)GridViewPagingControl.FindControl("ddlPageSize")).SelectedValue);
            // gvPass.PageIndex = Convert.ToInt32(((TextBox)GridViewPagingControl.FindControl("SelectedPageNo")).Text) - 1;
            BindSearchInfo(GridViewsortExpression, GridViewSortDirection);
        }

        #region "Property"
        private Int32 PageNum
        {
            get
            {
                if (ViewState["PageNum"] != null)
                    return Convert.ToInt32(ViewState["PageNum"].ToString());
                else
                    return 1;
            }
            set
            {
                ViewState["PageNum"] = value;
            }
        }

        private DataTable dtSearchDetails
        {
            get
            {
                if (ViewState["dtSearchDetails"] != null)
                    return (DataTable)ViewState["dtSearchDetails"];
                else
                    return null;
            }
            set
            {
                ViewState["dtSearchDetails"] = value;
            }
        }
        protected string GridViewsortExpression
        {
            get
            {
                if (ViewState["sortExpression"] == null)
                {
                    ViewState["sortExpression"] = "";
                }
                return ViewState["sortExpression"].ToString();
            }
            set { ViewState["sortExpression"] = value; }
        }
        public string GridViewSortDirection
        {
            get
            {
                if (ViewState["sortDirection"] == null)
                    ViewState["sortDirection"] = "Asc";
                return ViewState["sortDirection"].ToString();
            }
            set { ViewState["sortDirection"] = value; }
        }

        #endregion
        private void SetErrorMessage(String text, String type)
        {
            lblErrorMessage.InnerHtml = Convert.ToString(ErrorMessage(text, type));
            td_message.Attributes.Add("class", Convert.ToString(ErrorClass(text, type)));
            lblErrorMessage.Visible = null != text && 0 < text.Length;
        }

        protected void cbAOutTime_CheckedChanged(object sender, EventArgs e)
        {

        }

        protected void btnCheckOut_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (GridViewRow gvrow in gvPass.Rows)
                {
                    CheckBox chbTemp = (CheckBox)gvrow.FindControl("cbAOutTime");
                    TextBox txtOut = (TextBox)gvrow.FindControl("txtOuttime");

                    if (chbTemp.Checked)
                    {
                        //if (txtOut.Text == "")
                        //{
                        //    objDisplayLog.CustomMessage("Please Enter Out time", this);
                        //    return;
                        //}

                        //TimeSpan tspanOut = TimeSpan.Parse(txtOut.Text);
                        //TimeSpan tspanTimeIn = TimeSpan.Parse(gvPass.DataKeys[gvrow.RowIndex].Values[1].ToString());

                        //if (tspanTimeIn.CompareTo(tspanOut) == 1)
                        //{
                        //    objDisplayLog.CustomMessage("Please Enter Valid Out time", this);
                        //    return;
                        //}

                        ParameterList.AddParameter.Clear();

                        stParameterDetails.Value = gvPass.DataKeys[gvrow.RowIndex].Values[0];
                        stParameterDetails.DataType = SqlDbType.Int;
                        ParameterList.AddParameter.Add("SerialNo", stParameterDetails);

                        stParameterDetails.Value = txtOut.Text;
                        stParameterDetails.DataType = SqlDbType.VarChar;
                        ParameterList.AddParameter.Add("TimeOut", stParameterDetails);

                        stParameterDetails.Value = txtfromdate.Text;
                        stParameterDetails.DataType = SqlDbType.DateTime;
                        ParameterList.AddParameter.Add("SearchFrom", stParameterDetails);

                        stParameterDetails.Value = txttodate.Text;
                        stParameterDetails.DataType = SqlDbType.DateTime;
                        ParameterList.AddParameter.Add("SearchTo", stParameterDetails);

                        using (Project.Db.DbConnection db = new Project.Db.DbConnection())
                        {
                            if (rdoVisitor.Checked == true)
                            {
                                ds = db.CommonCollection.GetAsDataSet("dbo.VisitorLogDetail_UpdateTimeout", ParameterList.AddParameter);
                            }
                            else if (rdoForeign.Checked == true)
                            {
                                ds = db.CommonCollection.GetAsDataSet("dbo.foreignVisitorLogDetail_UpdateTimeout", ParameterList.AddParameter);
                            }
                        }
                    }
                }
                BindSearchInfo(GridViewsortExpression, GridViewSortDirection);
                BindRdlc();
                txtSearch.Text = "";
                //objDisplayLog.CustomMessage("Visitor Out Succesfully", this);
            }
            catch (Exception ex)
            {
                SetErrorMessage("please select the Visitor", "Error");
            }
        }

        public void BindRdlc()
        {
            ParameterList.AddParameter.Clear();
            stParameterDetails.Value = txtfromdate.Text;
            stParameterDetails.DataType = SqlDbType.DateTime;
            ParameterList.AddParameter.Add("SearchFrom", stParameterDetails);

            stParameterDetails.Value = txttodate.Text;
            stParameterDetails.DataType = SqlDbType.DateTime;
            ParameterList.AddParameter.Add("SearchTo", stParameterDetails);

            stParameterDetails.Value = gvPass.PageIndex;
            stParameterDetails.DataType = SqlDbType.BigInt;
            ParameterList.AddParameter.Add("pageIndex", stParameterDetails);

            stParameterDetails.Value = gvPass.PageSize;
            stParameterDetails.DataType = SqlDbType.BigInt;
            ParameterList.AddParameter.Add("pageSize", stParameterDetails);
            using (Project.Db.DbConnection db = new Project.Db.DbConnection())
            {
                if (rdoVisitor.Checked == true)
                {
                    ds = db.CommonCollection.GetAsDataSet("dbo.VisitorLogDetail_SelectDatewise_WitoutTimeOutrdlc", ParameterList.AddParameter);
                }
                else if (rdoForeign.Checked == true)
                {
                    ds = db.CommonCollection.GetAsDataSet("dbo.foreignVisitorLogDetail_SelectDatewise_WitoutTimeOutrdlc", ParameterList.AddParameter);
                }
            }
            ReportDataSource rds = new ReportDataSource();
            rds.Name = "DataSet1";
            rds.Value = ds.Tables[1];
            ReportViewer2.LocalReport.DataSources.Clear();
            ReportViewer2.LocalReport.ReportPath = Server.MapPath("~/VisitorVReport.rdlc");
            ReportViewer2.LocalReport.DataSources.Add(rds);
            ReportViewer2.LocalReport.Refresh();
            SetErrorMessage("", "");
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (GridViewRow gvrow in gvPass.Rows)
                {
                    CheckBox chbTemp = (CheckBox)gvrow.FindControl("cbAOutTime");
                    TextBox txtOut = (TextBox)gvrow.FindControl("txtOuttime");

                    if (chbTemp.Checked)
                    {
                        ParameterList.AddParameter.Clear();

                        stParameterDetails.Value = gvPass.DataKeys[gvrow.RowIndex].Values[0];
                        stParameterDetails.DataType = SqlDbType.Int;
                        ParameterList.AddParameter.Add("SerialNo", stParameterDetails);

                        stParameterDetails.Value = txtOut.Text;
                        stParameterDetails.DataType = SqlDbType.VarChar;
                        ParameterList.AddParameter.Add("TimeOut", stParameterDetails);

                        stParameterDetails.Value = txtfromdate.Text;
                        stParameterDetails.DataType = SqlDbType.DateTime;
                        ParameterList.AddParameter.Add("SearchFrom", stParameterDetails);

                        stParameterDetails.Value = txttodate.Text;
                        stParameterDetails.DataType = SqlDbType.DateTime;
                        ParameterList.AddParameter.Add("SearchTo", stParameterDetails);

                        using (Project.Db.DbConnection db = new Project.Db.DbConnection())
                        {
                            if (rdoVisitor.Checked == true)
                            {
                                ds = db.CommonCollection.GetAsDataSet("dbo.VisitorLogDetail_Delete", ParameterList.AddParameter);
                            }
                            else if (rdoForeign.Checked == true)
                            {
                                ds = db.CommonCollection.GetAsDataSet("dbo.foreignVisitorLogDetail_Delete", ParameterList.AddParameter);
                            }
                        }
                    }
                }
                gvPass.DataSource = ds.Tables[0];
                gvPass.DataBind();
                txtSearch.Text = "";
                //objDisplayLog.CustomMessage("Visitor Deleted Succesfully", this);
            }
            catch (Exception ex)
            {
                SetErrorMessage("please select the Visitor", "Error");
            }
        }

        protected void btncancel_Click(object sender, EventArgs e)
        {
            txtSearch.Text = "";
        }

        //public override void VerifyRenderingInServerForm(Control control)
        //{
        //    /* Confirms that an HtmlForm control is rendered for the specified ASP.NET
        //       server control at run time. */
        //}

        #region "Export Excel"
        protected void btnExport_Click(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)Session["dtPassDetails"];//(DataTable)Session["table"];
            GridView gv = new GridView();
            gv.RowDataBound += new GridViewRowEventHandler(gv_RowDataBound);
            gv.DataSource = dt;
            gv.DataBind();
            this.EnableViewState = false;
            Response.Charset = String.Empty;
            Response.Clear();
            Response.AddHeader("content-disposition", "attachment;filename=VisitorPassDetails.xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.xls";
            System.IO.StringWriter stringWrite = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
            gv.RenderControl(htmlWrite);
            string headerTable = @"<Table><tr><TD></TD><TD></TD><TD></TD><TD></TD><TD></TD><TD></TD><td><B>NAVAL DOCKYARD</B></td></tr></Table>";
            Response.Write(headerTable);
            Response.Write(stringWrite.ToString());
            Response.End();
        }

        protected void btnExport_Click1(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)ViewState["dtPassDetails"];//(DataTable)Session["table"];
            GridView gv = new GridView();
            gv.RowDataBound += new GridViewRowEventHandler(gv_RowDataBound);
            gv.DataSource = dt;
            gv.DataBind();
            ReportDataSource rds = new ReportDataSource();
            rds.Name = "DataSet1";
            rds.Value = ds.Tables[0];
            ReportViewer2.LocalReport.DataSources.Clear();
            ReportViewer2.LocalReport.ReportPath = Server.MapPath("~/VisitorVReport.rdlc");
            ReportViewer2.LocalReport.DataSources.Add(rds);
            ReportViewer2.LocalReport.Refresh();
        }

        protected void gv_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow | e.Row.RowType == DataControlRowType.Header)
                {
                    e.Row.Cells[1].Visible = false;
                    e.Row.Cells[5].Visible = false;
                    e.Row.Cells[6].Visible = false;
                    e.Row.Cells[9].Visible = false;
                    e.Row.Cells[10].Visible = false;
                    e.Row.Cells[13].Visible = false;
                    e.Row.Cells[15].Visible = false;
                    if ((e.Row.Cells[20].Text != "TimeOut") && (CheckNull.Checked != true))
                    {
                        e.Row.Cells[20].Text = "";
                    }
                    e.Row.Cells[21].Visible = false;
                    e.Row.Cells[22].Visible = false;
                    e.Row.Cells[23].Visible = false;
                    e.Row.Cells[20].Text = "";
                }
            }
            catch (Exception ex)
            {
                //lblErrorMessage.InnerHtml = Logger.ErrorLogFile(ex, "Error occured");
            }
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            return;
        }

        private void ClearControls(Control control)
        {
            for (int i = control.Controls.Count - 1; i >= 0; i--)
            {
                ClearControls(control.Controls[i]);
            }

            if (!(control is TableCell))
            {
                if (control.GetType().GetProperty("SelectedItem") != null)
                {
                    LiteralControl literal = new LiteralControl();
                    control.Parent.Controls.Add(literal);
                    try
                    {
                        literal.Text = (string)control.GetType().GetProperty("SelectedItem").GetValue(control, null);
                    }
                    catch
                    {
                        // do nothing(); 
                    }
                    finally
                    {
                        control.Parent.Controls.Remove(control);
                    }
                }
                else
                {
                    if (control.GetType().GetProperty("Text") != null)
                    {
                        LiteralControl literal = new LiteralControl();
                        control.Parent.Controls.Add(literal);
                        literal.Text = (string)control.GetType().GetProperty("Text").GetValue(control, null);
                        control.Parent.Controls.Remove(control);
                    }
                }
            }
            return;
        }
        #endregion
        protected void ReportViewer_OnLoad(object sender, EventArgs e)
        {
            //string exportOption = "Excel";
            //string exportOption = "Word"
            string exportOption = "Excel";
            string exportOption1 = "WORD";
            RenderingExtension extension = ReportViewer2.LocalReport.ListRenderingExtensions().ToList().Find(x => x.Name.Equals(exportOption, StringComparison.CurrentCultureIgnoreCase));
            RenderingExtension extension1 = ReportViewer2.LocalReport.ListRenderingExtensions().ToList().Find(x => x.Name.Equals(exportOption1, StringComparison.CurrentCultureIgnoreCase));

            if (extension != null)
            {
                System.Reflection.FieldInfo fieldInfo = extension.GetType().GetField("m_isVisible", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
                fieldInfo.SetValue(extension, false);

                System.Reflection.FieldInfo fieldInfo1 = extension1.GetType().GetField("m_isVisible", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
                fieldInfo.SetValue(extension1, false);
            }
        }

        protected void gvPass_SelectedIndexChanged(object sender, EventArgs e)
        {

        }



        public static object ErrorMessage(string text, string type)
        {
            string strErrorMessage = "";
            switch (type)
            {
                case "Error":
                    strErrorMessage = "<font class='errormessagetype'>Error :</font><font class='errormessagetext'>" + text + "</font>";
                    break; // TODO: might not be correct. Was : Exit Select 

                    break;
                case "Warning":
                    strErrorMessage = "<font class='warningmessagetype'>Warning :</font><font class='warningmessagetext'>" + text + "</font>";
                    break; // TODO: might not be correct. Was : Exit Select 

                    break;
                case "Info":
                    strErrorMessage = "<font class='infomessagetype'>Info :</font><font class='infomessagetext'>" + text + "</font>";
                    break; // TODO: might not be correct. Was : Exit Select 

                    break;
                case "":
                    break; // TODO: might not be correct. Was : Exit Select 

                    break;
            }

            return strErrorMessage;
        }
        public static object ErrorClass(string text, string type)
        {
            string strErrorClass = "";
            switch (type)
            {
                case "Error":
                    strErrorClass = "errormessage";
                    break; // TODO: might not be correct. Was : Exit Select 

                    break;
                case "Warning":
                    strErrorClass = "warningmessage";
                    break; // TODO: might not be correct. Was : Exit Select 

                    break;
                case "Info":
                    strErrorClass = "infomessage";
                    break; // TODO: might not be correct. Was : Exit Select 

                    break;
                case "":
                    strErrorClass = "";
                    break; // TODO: might not be correct. Was : Exit Select 

                    break;
            }
            return strErrorClass;
        }
    }
}