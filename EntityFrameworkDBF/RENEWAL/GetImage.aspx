<%@ Page Title="" Language="C#" MasterPageFile="~/VMSMaster.master" AutoEventWireup="true"
    Theme="Admin_Basic" CodeBehind="GetImage.aspx.cs" Inherits="EntityFrameworkDBF.RENEWAL.GetImage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../Crop/jquery-1.7.1.min.js"></script>
    <link href="../Crop/jquery.Jcrop.css" rel="stylesheet" />
    <script src="../Crop/jquery.Jcrop.js"></script>
    <script language="javascript">
        $(document).ready(function () {
            $('#<%=imgApplication.ClientID%>').Jcrop({
                onSelect: SelectCropArea
            });
        });
        function SelectCropArea(c) {
            $('#<%=X.ClientID%>').val(parseInt(c.x));
            $('#<%=Y.ClientID%>').val(parseInt(c.y));
            $('#<%=W.ClientID%>').val(parseInt(c.w));
            $('#<%=H.ClientID%>').val(parseInt(c.h));
        }
    </script>
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
            CONTRACTOR PHOTO :
        </div>
        <table>
            <tr>
                <td>
                    <asp:Label ID="lblAppNo" runat="server" Text="APPLICATION NUMBER :" Visible="false"></asp:Label>
                </td>
                <td>
                </td>
                <td>
                    <asp:Label ID="lblGetAppNo" runat="server" Visible="false"></asp:Label>
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
        <div class="btnView" style="background-image: url('../images2/templatemo_button_02.png');
            background-repeat: no-repeat; width: 135px">
            <asp:Button ID="btnCrop" Text="CROP & SAVE" runat="server" OnClick="btnCrop_Click" />
        </div>
        <div>
            <asp:HiddenField ID="X" runat="server" />
            <asp:HiddenField ID="Y" runat="server" />
            <asp:HiddenField ID="W" runat="server" />
            <asp:HiddenField ID="H" runat="server" />
        </div>
    </center>
</asp:Content>
