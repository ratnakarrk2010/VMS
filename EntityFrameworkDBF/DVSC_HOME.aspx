<%@ Page Title="" Language="C#" MasterPageFile="~/VMSMaster.master" AutoEventWireup="true"
    CodeBehind="DVSC_HOME.aspx.cs" Inherits="EntityFrameworkDBF.DVSC_HOME" %>

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
            MODULES
        </div>
    </center>
    <table class="table-box" style="width: 900px; margin: auto;">
        <tr>
            <td>
                <div class="service_box1 box-3">
                    <div class="sub-box">
                        <asp:LinkButton ID="lnkadmpnl" runat="server" Text="ADMIN PANEL" Style="color: White;
                            text-decoration: none; padding-top: 20px;" OnClick="lnkadmpnl_Click"> </asp:LinkButton>
                        <%--     <div style="visibility:hidden" >
                                <span>Labour</span> <a href="">0</a>
                            </div>--%>
                    </div>
                    <strong>
                        <%--<span>PENDING WITH UNIT</span>--%></strong>
                </div>
            </td>
            <td>
                <div class="service_box1 box-3">
                    <div class="sub-box">
                        <asp:LinkButton ID="lnkissue" runat="server" Text="ISSUE" Style="color: White; text-decoration: none;
                            padding-top: 20px;" OnClick="lnkissue_Click"> </asp:LinkButton>
                        <%--    OnClick="lnkPenApproval_Click">--%>
                    </div>
                    <strong>
                        <%--<span>PENDING WITH CHSFO</span>--%></strong>
                </div>
            </td>
            <td>
                <div class="service_box1 box-3">
                    <div class="sub-box">
                        <asp:LinkButton ID="lnkrenew" runat="server" Text="RENEWAL / MODIFY" Style="color: White;
                            text-decoration: none; padding-top: 20px;" OnClick="lnkrenew_Click"> </asp:LinkButton>
                        <%--   OnClick="lnkApproved_Click"--%>
                        <br />
                        <strong>
                            <%--<span class="sub-box" style="color: White; font-size: 25px;">MASTER</span>--%></strong>
                    </div>
                </div>
            </td>
        </tr>
        <tr>
            <td>
                <div class="service_box1 box-3">
                    <div class="sub-box">
                        <asp:LinkButton ID="lnkactivate" runat="server" Text="ACTIVATION" Style="color: White;
                            text-decoration: none; padding-top: 20px;" OnClick="lnkactivate_Click"> </asp:LinkButton>
                        <br />
                        <center>
                            <strong><span class="sub-box" style="color: White; font-size: 25px;">DEACTIVATION</span></strong></center>
                    </div>
                    <%--<strong><span>FIRM MASTER</span></strong>--%>
                </div>
            </td>
            <td>
                <div class="service_box1 box-3">
                    <div class="sub-box">
                        <asp:LinkButton ID="lnkcancel" runat="server" Text="CANCELLATION" Style="color: White;
                            text-decoration: none; padding-top: 20px;" OnClick="lnkcancel_Click"> </asp:LinkButton>
                        <%--     <div style="visibility:hidden" >
                                <span>Labour</span> <a href="">0</a>
                            </div>--%>
                    </div>
                    <strong>
                        <%--<span>PENDING WITH UNIT</span>--%></strong>
                </div>
            </td>
            <td>
                <div class="service_box1 box-3">
                    <div class="sub-box">
                        <asp:LinkButton ID="lnklabour" runat="server" Text="LABOUR " Style="color: White;
                            text-decoration: none; padding-top: 20px;" OnClick="lnklabour_Click"> </asp:LinkButton>
                        <br />
                        <center>
                            <strong><span class="sub-box" style="color: White; font-size: 25px;">REGISTRATION</span></strong></center>
                        <%--    OnClick="lnkPenApproval_Click">--%>
                    </div>
                    <strong>
                        <%--<span>PENDING WITH CHSFO</span>--%></strong>
                </div>
            </td>
        </tr>
        <tr>
             <td>
                <div class="service_box1 box-3">
                    <div class="sub-box">
                        <asp:LinkButton ID="lnkreports" runat="server" Text="REPORTS" Style="color: White;
                            text-decoration: none; padding-top: 20px;" OnClick="lnkreports_Click"> </asp:LinkButton>
                        <%--     <div style="visibility:hidden" >
                                <span>Labour</span> <a href="">0</a>
                            </div>--%>
                    </div>
                    <strong>
                        <%--<span>PENDING WITH UNIT</span>--%></strong>
                </div>
            </td>
            <td>
              <!--  <div class="service_box1 box-3">
                    <div class="sub-box">
                        <asp:LinkButton ID="lnkrfid" Visible="false" runat="server" Text="PRINT RFID PASS" Style="color: White;
                            text-decoration: none; padding-top: 20px;" OnClick="lnkrfid_Click"> </asp:LinkButton>
                        <%--   OnClick="lnkApproved_Click"--%>
                        <br />
                        <strong>
                            <%--<span class="sub-box" style="color: White; font-size: 25px;">MASTER</span>--%></strong>
                    </div>
                </div>-->
            </td>
            <td>
                <!--<div class="service_box1 box-3">
                    <div class="sub-box">
                        <asp:LinkButton ID="lnkcvpass" runat="server"  Visible="false"  Text="FOREIGN VISITOR" Style="color: White;
                            text-decoration: none; padding-top: 20px;" OnClick="lnkcvpass_Click"> </asp:LinkButton>
                        <br />
                        <center>
                            <strong><span class="sub-box" style="color: White; font-size: 25px;"> </span></strong></center>
                    </div>
                </div>-->
            </td>
       
        </tr>
    </table>
</asp:Content>
