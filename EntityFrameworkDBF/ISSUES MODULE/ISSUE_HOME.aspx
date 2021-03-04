<%@ Page Title="" Language="C#" MasterPageFile="~/VMSMaster.master" AutoEventWireup="true"
    CodeBehind="ISSUE_HOME.aspx.cs" Inherits="EntityFrameworkDBF.ISSUES.ISSUE_HOME" %>

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
                        <asp:LinkButton ID="lnkContractor" runat="server" Text="CONTRACTOR / " Style="color: White;
                            text-decoration: none; padding-top: 20px;" onclick="lnkContractor_Click"> </asp:LinkButton>
                        <br />
                        <center>
                            <strong><span class="sub-box" style="color: White; font-size: 25px;">SUPERVISOR</span></strong></center>
                    </div>
                    <strong>
                        <%--<span>PENDING WITH UNIT</span>--%></strong>
                </div>
            </td>
            <td>
                <div class="service_box1 box-4">
                    <div class="sub-box">
                        <asp:LinkButton ID="lnkEscrted" runat="server" Text="ESCORTED " Style="color: White;
                            text-decoration: none; padding-top: 20px;" onclick="lnkEscrted_Click"> </asp:LinkButton>
                        <br />
                        <center>
                            <strong><span class="sub-box" style="color: White; font-size: 25px;">WORKERS</span></strong></center>
                        <%--    OnClick="lnkPenApproval_Click">--%>
                    </div>
                    <strong>
                        <%--<span>PENDING WITH CHSFO</span>--%></strong>
                </div>
            </td>
        </tr>
        <tr>
            <td>
                <div class="service_box1 box-4">
                    <div class="sub-box">
                        <asp:LinkButton ID="lnkBank" runat="server" Text="BANK / PSU / BEL" Style="color: White;
                            text-decoration: none; padding-top: 20px;" onclick="lnkBank_Click"> </asp:LinkButton>
                        <%--   OnClick="lnkApproved_Click"--%>
                        <br />
                        <strong>
                            <%--<span class="sub-box" style="color: White; font-size: 25px;">MASTER</span>--%></strong>
                    </div>
                </div>
            </td>
            <td>
                <div class="service_box1 box-4">
                    <div class="sub-box">
                        <asp:LinkButton ID="lnkCanteen" runat="server" Text="CANTEEN / SHIPS " Style="color: White;
                            text-decoration: none; padding-top: 20px;" onclick="lnkCanteen_Click"> </asp:LinkButton>
                        <br />
                        <center>
                            <strong><span class="sub-box" style="color: White; font-size: 25px;">HIRED STAFFS</span></strong></center>
                    </div>
                    <%--<strong><span>FIRM MASTER</span></strong>--%>
                </div>
            </td>
        </tr>
    </table>
</asp:Content>
