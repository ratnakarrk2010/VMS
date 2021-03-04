<%@ Page Title="" Language="C#" MasterPageFile="~/VMSMaster.master" AutoEventWireup="true"
    CodeBehind="ShowApplication.aspx.cs" Inherits="EntityFrameworkDBF.ISSUES_MODULE.ShowApplication"
    Theme="Admin_Basic" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function Close() {
            window.close();
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="main" runat="server">
    <center>
        <div class="header_02">
            DVSC : PASS MANAGEMENT SYSTEM - <span id="spnDate" runat="server"></span>
            <br />
            <br />
            CONTRACTOR / SUPERVISOR PASS
        </div>
        <div style="padding-left: 600px">
            <asp:Button ID="btnhome" runat="server" Text="CLOSE" CssClass="button" OnClientClick="Close()" />
        </div>
        <div class="header_03">
            CONTRACTOR APPLICATION :
        </div>
        <table>
            <tr>
                <td>
                    <asp:Label ID="lblAppNo" runat="server" Text="APPLICATION NUMBER :"></asp:Label>
                </td>
                <td>
                </td>
                <td>
                    <asp:Label ID="lblGetAppNo" runat="server"></asp:Label>
                </td>
            </tr>
        </table>
        <div>
            <asp:Panel ID="pnlAlreadyImage" runat="server" Height="300px" Width="300px">
                <asp:Image ID="imgApplication" runat="server" Height="300px" Width="300px" />
            </asp:Panel>
        </div>
        <div>
          <asp:Literal ID="ltEmbed" runat="server" />
        </div>
    </center>
</asp:Content>
