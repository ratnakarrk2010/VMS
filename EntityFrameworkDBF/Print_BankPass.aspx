<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Print_BankPass.aspx.cs"
    Inherits="EntityFrameworkDBF.Print_BankPass" %>

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
            background-color: #FF0066;
            width: 205px;
        }
        .style211
        {
            /*height: 12px;*/
            width: 205px;
        }
        .style212
        {
            /*height: 14px;*/
            width: 205px;
        }
        .style219
        {
            width: 100%; /*height: 5px;*/
        }
        .style220
        {
            width: 205px; /*height: 6px;*/
        }
        .style222
        {
            /*height: 30px;*/
            width: 205px;
        }
        /* .style226
        {
            font-size: small;
            margin-top: 5px;
            margin-bottom: 0px;
        }*/
        .style227
        {
            font-size: medium;
            font-family: Calibri;
            color: #FF0066;
            margin-top: 0px;
            margin-left: 4px;
            margin-bottom: 0px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server" style="margin-left: 20px;">
    <div>
        <asp:Panel ID="Panel1" runat="server" Width="200px" Height="300px" BorderColor="Black"
            BorderStyle="Solid" BorderWidth="2px">
            <table style="width: 222px;" cellpadding="0" cellspacing="0">
                <tr>
                    <td align="center" id="Head" height="10px" width="12">
                        <img src="Images/Pink.png" style="width: 200px; height: 10px; padding-right: 25px;" />
                    </td>
                </tr>
                <tr>
                    <td align="center" class="style211">
                        <asp:Label ID="lblheading" runat="server" CssClass="style227" Font-Bold="True" Font-Names="Calibri"
                            Font-Size="13px" Font-Underline="True" Text="NDMB PASS" Width="81%" Height="34px"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="center" class="style212">
                        <table>
                            <tr>
                                <td>
                                    <asp:Label ID="txtCasualCardNo" Font-Bold="true" ForeColor="Pink" runat="server"></asp:Label>
                                    <br />
                                    <br />
                                    <strong><span style="color: #FF0066; font-size: 12px;">DOI</span></strong>
                                    <br />
                                    <asp:Label ID="txtDateOfIssue" runat="server" BorderStyle="None" CssClass="style226"
                                        Font-Names="Calibri" Font-Size="12px" OnTextChanged="TextBox3_TextChanged" Style="font-family: Arial, Helvetica, sans-serif;
                                        font-weight: 700;" Width="45%"></asp:Label>
                                </td>
                                <td>
                                    <asp:Image ID="imgCasual" runat="server" BorderColor="#1F76AE" BorderStyle="Solid"
                                        BorderWidth="1px" Height="90px" Width="100px" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td class="style219">
                        <asp:Label ID="lblName" runat="server" CssClass="style227" Font-Bold="True" Font-Names="Calibri"
                            Font-Size="12px" Text="NAME:" Width="20%"></asp:Label>
                        <asp:Label ID="txtCasualName" runat="server" BorderStyle="None" CssClass="style226"
                            Font-Names="Calibri" Font-Size="12px" Height="16px" OnTextChanged="TextBox2_TextChanged"
                            Style="font-family: Calibri"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="style219">
                        <asp:Label ID="lblDesi" runat="server" CssClass="style227" Font-Bold="True" Font-Names="Calibri"
                            Font-Size="12px" Text="Desig :" Width="18%"></asp:Label>
                        <asp:Label ID="txtDesignation" runat="server" BorderStyle="None" CssClass="style226"
                            Font-Names="Calibri" Font-Size="12px" Height="16px" OnTextChanged="TextBox3_TextChanged"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="style219">
                        <asp:Label ID="lblfirm" runat="server" CssClass="style227" Font-Bold="True" Font-Names="Calibri"
                            Font-Size="12px" Text="Firm :" Width="18%"></asp:Label>
                        <asp:Label ID="txtFirm" runat="server" BorderStyle="None" CssClass="style226" Font-Names="Calibri"
                            Font-Size="12px" Height="16px" OnTextChanged="TextBox3_TextChanged"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="style219">
                        <asp:Label ID="lblPvc" runat="server" CssClass="style227" Font-Bold="True" Font-Names="Calibri"
                            Font-Size="12px" Text="UID NO :" Width="18%"></asp:Label>
                        <asp:Label ID="txtPvc" runat="server" BorderStyle="None" CssClass="style226" Font-Names="Calibri"
                            Font-Size="12px" Height="16px" OnTextChanged="TextBox3_TextChanged"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="center" class="style222">
                        <hr size="1" style="height: -15px; width: 210px;" />
                        <table>
                            <tr>
                                <td>
                                </td>
                                <td>
                                    <asp:Image ID="Image2" runat="server" Height="40px" ImageUrl="~/Images/SING_CSO.png"
                                        Width="100px" />
                                    <br />
                                    <asp:Label ID="Label50" runat="server" CssClass="label" Font-Bold="True" Font-Names="Calibri"
                                        Font-Size="Small" ForeColor="Black" Height="12px" Text="Chief Security Officer"
                                        Width="99%"></asp:Label>
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
