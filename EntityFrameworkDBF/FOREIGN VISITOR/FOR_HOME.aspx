<%@ Page Title="" Language="C#" MasterPageFile="~/VMSMaster.master" AutoEventWireup="true"
    CodeBehind="FOR_HOME.aspx.cs" Inherits="EntityFrameworkDBF.FOREIGN_VISITOR.FOR_HOME"
    Theme="Admin_Basic" %>

<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Styles/templatemo_style.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="main" runat="server">
    <center>
        <div class="header_02">
            DVSC : PASS MANAGEMENT SYSTEM - <span id="spnDate" runat="server"></span>
            <br />
            <br />
            ISSUES
        </div>
        <div style="padding-left: 900px">
            <asp:Button ID="btnissuehome" runat="server" Text="BACK TO HOME" CssClass="btnView"
                Style="color: White" OnClick="btnissuehome_Click" />
        </div>
    </center>
    <table class="table-box" style="width: 900px; margin: auto;">
        <tr>
            <td>
                <div class="service_box1 box-4">
                    <div class="sub-box">
                        <asp:LinkButton ID="lnkDashboard" runat="server" Text="DASHBOARD" Style="color: White;
                            text-decoration: none; padding-top: 20px;" OnClick="lnkDashboard_Click"> </asp:LinkButton>
                        <br />
                        <%--<center>
                            <strong><span class="sub-box" style="color: White; font-size: 25px;">SUPERVISOR</span></strong></center>--%>
                    </div>
                    <strong>
                        <%--<span>PENDING WITH UNIT</span>--%></strong>
                </div>
            </td>
            <td>
                <div class="service_box1 box-4">
                    <div class="sub-box">
                        <asp:LinkButton ID="lnkFor_Visitor" runat="server" Text="NEW FOREIGN" Style="color: White;
                            text-decoration: none; padding-top: 20px;" OnClick="lnkFor_Visitor_Click"> </asp:LinkButton>
                        <br />
                        <center>
                            <strong><span class="sub-box" style="color: White; font-size: 25px;">PASS</span></strong></center>
                        <%--    OnClick="lnkPenApproval_Click">--%>
                    </div>
                    <strong>
                        <%--<span>PENDING WITH CHSFO</span>--%></strong>
                </div>
            </td>
            <td>
                <div class="service_box1 box-4">
                    <div class="sub-box">
                        <asp:LinkButton ID="lnk_Report" runat="server" Text="FOREIGN PASS " Style="color: White;
                            text-decoration: none; padding-top: 20px;" onclick="lnk_Report_Click"> </asp:LinkButton>
                        <br />
                        <center>
                            <strong><span class="sub-box" style="color: White; font-size: 25px;">REPORT</span></strong></center>
                        <%--    OnClick="lnkPenApproval_Click">--%>
                    </div>
                    <strong>
                        <%--<span>PENDING WITH CHSFO</span>--%></strong>
                </div>
            </td>
        </tr>
    </table>
</asp:Content>
