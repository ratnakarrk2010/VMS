<%@ Page Title="" Language="C#" MasterPageFile="~/VMSMaster.master" AutoEventWireup="true"
    CodeBehind="ADMIN_PANEL_HOME.aspx.cs" Inherits="EntityFrameworkDBF.ADMIN_PANEL.ADMIN_PANEL" %>

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
            ADMIN PANEL
        </div>
        <div style="padding-left: 900px">
            <asp:Button ID="btnhome" runat="server" Text="BACK TO HOME" CssClass="btnView" OnClick="btnhome_Click" />
        </div>
    </center>
    <table class="table-box" style="width: 900px; margin: auto;">
        <tr>
            <td>
                <div class="service_box box-1">
                    <div class="sub-box">
                        <asp:LinkButton ID="lnkcountry" runat="server" Text="COUNTRY MASTER" Style="color: White;
                            text-decoration: none; padding-top: 20px;" OnClick="lnkcountry_Click"> </asp:LinkButton>
                        <%--     <div style="visibility:hidden" >
                                <span>Labour</span> <a href="">0</a>
                            </div>--%>
                    </div>
                    <strong>
                        <%--<span>PENDING WITH UNIT</span>--%></strong>
                </div>
            </td>
            <td>
                <div class="service_box box-1">
                    <div class="sub-box">
                        <asp:LinkButton ID="lnkstate" runat="server" Text="STATE MASTER" Style="color: White;
                            text-decoration: none; padding-top: 20px;" OnClick="lnkstate_Click"> </asp:LinkButton>
                        <%--    OnClick="lnkPenApproval_Click">--%>
                    </div>
                    <strong>
                        <%--<span>PENDING WITH CHSFO</span>--%></strong>
                </div>
            </td>
            <td>
                <div class="service_box box-1">
                    <div class="sub-box">
                        <asp:LinkButton ID="lnkdesignation" runat="server" Text="DESIGNATION " Style="color: White;
                            text-decoration: none; padding-top: 20px;" OnClick="lnkdesignation_Click"> </asp:LinkButton>
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
                <div class="service_box box-1">
                    <div class="sub-box">
                        <asp:LinkButton ID="lnkfirm" runat="server" Text="FIRM MASTER" Style="color: White;
                            text-decoration: none; padding-top: 20px;" OnClick="lnkfirm_Click"> </asp:LinkButton>
                    </div>
                    <%--<strong><span>FIRM MASTER</span></strong>--%>
                </div>
            </td>
            <td>
                <div class="service_box box-1">
                    <div class="sub-box">
                        <asp:LinkButton ID="lnkdocument" runat="server" Text="DOCUMENT MASTER" Style="color: White;
                            text-decoration: none; padding-top: 20px;" OnClick="lnkdocument_Click"> </asp:LinkButton>
                        <%--     <div style="visibility:hidden" >
                                <span>Labour</span> <a href="">0</a>
                            </div>--%>
                    </div>
                    <strong>
                        <%--<span>PENDING WITH UNIT</span>--%></strong>
                </div>
            </td>
            <td>
                <div class="service_box box-1">
                    <div class="sub-box">
                        <asp:LinkButton ID="lnkshop" runat="server" Text="SHIP MASTER" Style="color: White;
                            text-decoration: none; padding-top: 20px;" OnClick="lnkshop_Click"> </asp:LinkButton>
                        <%--    OnClick="lnkPenApproval_Click">--%>
                    </div>
                    <strong>
                        <%--<span>PENDING WITH CHSFO</span>--%></strong>
                </div>
            </td>
        </tr>
        <tr>
            <td>
                <div class="service_box box-1">
                    <div class="sub-box">
                        <asp:LinkButton ID="lnkcancel" runat="server" Text="CANCEL REASON" Style="color: White;
                            text-decoration: none; padding-top: 20px;" OnClick="lnkcancel_Click"> </asp:LinkButton>
                        <%--   OnClick="lnkApproved_Click"--%>
                        <br />
                        <strong>
                            <%--<span class="sub-box" style="color: White; font-size: 25px;">MASTER</span>--%></strong>
                    </div>
                </div>
            </td>
            <td>
                <div class="service_box box-1">
                    <div class="sub-box">
                        <asp:LinkButton ID="lnkpasstype" runat="server" Text="PASS TYPE" Style="color: White;
                            text-decoration: none; padding-top: 20px;" OnClick="lnkpasstype_Click"> </asp:LinkButton>
                    </div>
                    <%--<strong><span>FIRM MASTER</span></strong>--%>
                </div>
            </td>
            <td>
                <div class="service_box box-1">
                    <div class="sub-box">
                        <asp:LinkButton ID="lnkUserMaster" runat="server" Text="USER MASTER" Style="color: White;
                            text-decoration: none; padding-top: 20px;" OnClick="lnkUserMaster_Click" Enabled="false"> </asp:LinkButton>
                    </div>
                    <%--<strong><span>FIRM MASTER</span></strong>--%>
                </div>
            </td>
        </tr>
        <tr>
            <td>
                <div class="service_box box-1">
                    <div class="sub-box">
                        <asp:LinkButton ID="lnkRoleMaster" runat="server" Text="ROLE MASTER" Style="color: White;
                            text-decoration: none; padding-top: 20px;" OnClick="lnkRoleMaster_Click"> </asp:LinkButton>
                    </div>
                    <%--<strong><span>FIRM MASTER</span></strong>--%>
                </div>
            </td>
            <td>
                <div class="service_box box-1">
                    <div class="sub-box">
                        <asp:LinkButton ID="lnkReligion" runat="server" Text="RELIGION MASTER" Style="color: White;
                            text-decoration: none; padding-top: 20px;" OnClick="lnkReligion_Click"> </asp:LinkButton>
                    </div>
                    <%--<strong><span>FIRM MASTER</span></strong>--%>
                </div>
            </td>
            <td>
                <div class="service_box box-1">
                    <div class="sub-box">
                        <asp:LinkButton ID="lnkPSUunit" runat="server" Text="PSU MASTER" Style="color: White;
                            text-decoration: none; padding-top: 20px;" OnClick="lnkPSUunit_Click"> </asp:LinkButton>
                    </div>
                    <%--<strong><span>FIRM MASTER</span></strong>--%>
                </div>
            </td>
        </tr>
    </table>
</asp:Content>
