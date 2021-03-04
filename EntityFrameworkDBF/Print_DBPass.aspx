<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Print_DBPass.aspx.cs" Inherits="EntityFrameworkDBF.Print_DBPass" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <script type="text/javascript">
        function PrintPanel() {
            var panel = document.getElementById("<%=Panel1.ClientID %>");
            var printWindow = window.open('', '', 'height=329,width=213');
            printWindow.document.write('<html><head>');
            printWindow.document.write('</head><body >');
            printWindow.document.write(panel.innerHTML);
            printWindow.document.write('</body></html>');
            printWindow.document.close();
            setTimeout(function () {
                printWindow.print();
            }, 500);
            return false;
        }
    </script>
    <script language="javascript" type="text/javascript">
        function prinpage() {
            window.print();
        }
    
    </script>
    <script language="javascript" type="text/javascript">
        function prinpage() {
            window.print();
        }
    </script>
    <script type="text/javascript">
        document.body.onload = function () { window.print(); }
    </script>
    <style type="text/css">
        @media print
        {
            #printbtn
            {
                display: none;
            }
        }
    </style>
    <style type="text/css">
        .label
        {
            font-size: medium;
            font-family: Calibri;
            color: #FF0000;
            margin-top: 0px;
            margin-left: 4px;
        }
        .style186
        {
            color: #FF0000;
        }
        .style211
        {
            /*height: 12px;*/
        }
        .style212
        {
            /*height: 29px;*/
        }
        .style219
        {
            width: 100%; /*height: 6px;*/
        }
        .style220
        {
            width: 100%; /*height: 6px;*/
        }
        .style222
        {
            /*height: 30px;*/
        }
        .style225
        {
            /*height: 3px;*/
        }
        .style226
        {
            font-size: x-small;
        }
        .style227
        {
            font-size: medium;
            font-family: Calibri;
            color: #006600;
            margin-top: 0px;
            margin-left: 4px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server" style="margin-left: 20px;">
    <div>
        <asp:Panel ID="Panel1" runat="server" Width="200px" Height="282px" BorderColor="Black"
            BorderStyle="Solid" BorderWidth="2px">
            <table style="width: 213px;" cellpadding="0" cellspacing="0">
                <tr>
                    <%--<td align="center" class="style186" height="20px" bgcolor="Green">
                </td>--%>
                    <td align="center" id="Head" height="12px" width="12">
                        <img src="Images/Green.png" style="width: 190px; height: 12px;" />
                    </td>
                </tr>
                <tr>
                    <td align="center" class="style211">
                        <asp:Label ID="lblheading" runat="server" CssClass="style227" Font-Bold="True" Font-Names="Calibri"
                            Font-Size="13px" Font-Underline="True" Text="NDMB PASS" Width="81%" Height="15px"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="center" class="style212">
                        <table>
                            <tr>
                                <td>
                                    <asp:Label ID="txtTemporaryCardNo" Font-Bold="true" ForeColor="Green" runat="server"></asp:Label>
                                    <br />
                                    <br />
                                    <strong><span style="color: Green; font-size: 12px;">DOI</span></strong>
                                    <br />
                                    <asp:Label ID="txtDateOfIssue" runat="server" BorderStyle="None" Font-Names="Calibri"
                                        Font-Size="12px" OnTextChanged="TextBox3_TextChanged" Style="font-family: Arial, Helvetica, sans-serif;"></asp:Label>
                                </td>
                                <td>
                                    <asp:Image ID="imgTemporaryor" runat="server" BorderColor="#1F76AE" BorderStyle="Solid"
                                        BorderWidth="1px" Height="90px" Width="100px" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td class="style219">
                        <asp:Label ID="lblName" runat="server" CssClass="style227" Font-Bold="True" Font-Names="Calibri"
                            Font-Size="Small" Text="Name:" Width="21%"></asp:Label>&nbsp&nbsp&nbsp&nbsp
                        <asp:Label ID="txtTemporaryName" runat="server" BorderStyle="None" Font-Names="Calibri"
                            Font-Size="12px" Height="16px" OnTextChanged="TextBox2_TextChanged" Style="font-family: Calibri"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="style220">
                        <asp:Label ID="lbldesi" runat="server" CssClass="style227" Font-Bold="True" Font-Names="Calibri"
                            Font-Size="Small" Text="Desig :"></asp:Label>&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp
                        <asp:Label ID="txtDesignation" runat="server" BorderStyle="None" Font-Names="Calibri"
                            Font-Size="12px" OnTextChanged="TextBox3_TextChanged" Style="font-family: Calibri;"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="style220">
                        <asp:Label ID="lblFirm" runat="server" CssClass="style227" Font-Bold="True" Font-Names="Calibri"
                            Font-Size="Small" Text="FIrm :"></asp:Label>&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp
                        <asp:Label ID="txtFirm" runat="server" BorderStyle="None" Font-Names="Calibri" Font-Size="12px"
                            OnTextChanged="TextBox3_TextChanged" Style="font-family: Calibri;"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="style220">
                        <asp:Label ID="lblPvc" runat="server" CssClass="style227" Font-Bold="True" Font-Names="Calibri"
                            Font-Size="Small" Text="UID NO :"></asp:Label>&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp
                        <asp:Label ID="txtpvc" runat="server" BorderStyle="None" Font-Names="Calibri" Font-Size="12px"
                            OnTextChanged="TextBox3_TextChanged" Style="font-family: Calibri;"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="center">
                        <table>
                            <tr>
                                <td>
                                </td>
                                <td>
                                    <hr size="1" style="height: 1; width: 205px; margin-left: 0px;" />
                                    <center>
                                        <asp:Image ID="Image2" runat="server" Height="40px" ImageUrl="~/Images/SING_CSO.png"
                                            Width="120px" />
                                        <br />
                                        <asp:Label ID="Label50" runat="server" CssClass="label" Font-Bold="True" Font-Names="Calibri"
                                            Font-Size="Small" ForeColor="Black" Height="12px" Text="Chief Security Officer"
                                            Width="99%"></asp:Label>
                                    </center>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <input id="printbtn" type="button" value="Print this page" onclick="window.print();" />
    </div>
    </form>
</body>
</html>
