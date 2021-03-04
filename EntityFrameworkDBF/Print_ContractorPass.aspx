<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Print_ContractorPass.aspx.cs"
    Inherits="EntityFrameworkDBF.Print_ContractorPass" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <script type="text/javascript">
        function PrintPanel() {
            var panel = document.getElementById("<%=Panel1.ClientID %>");
            var printWindow = window.open('', '', 'height=310,width=200');
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
            font-size: small;
            font-family: Arial, Helvetica, sans-serif;
            color: #FF0000;
            margin-top: 0px;
            margin-left: 4px;
        }
        .style209
        {
            text-decoration: underline;
            font-family: Calibri;
            font-size: medium;
            width: 100%; /*height: 10px;*/
        }
        .style211
        {
            /*height: 6px;*/
        }
        .style212
        {
            /*height: 22px;*/
        }
        .style219
        {
            width: 100%; /*height: 6px;*/
        }
        .style220
        {
            width: 100%; /*height: 6px;*/
        }
        .style221
        {
            width: 100%; /*height: 2px;*/
        }
        .style224
        {
            font-size: x-small;
        }
        .style226
        {
            font-size: x-small;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server" style="margin-left: 20px;">
    <div>
        <asp:Panel ID="Panel1" runat="server" Width="200px" Height="295px" BorderColor="Black"
            BorderStyle="Solid" BorderWidth="1px">
            <%--<hr style="background-color: #FF0000" id="Head"/> --%>
            <table id="Table1" style="width: 213px;" cellpadding="0" runat="server" cellspacing="0">
                <tr>
                    <td align="center" id="Head" height="12px" width="12">
                        <img src="Images/Red.png" style="width: 190px; height: 12px; padding-right: 10px;" />
                    </td>
                </tr>
                <tr>
                    <td align="center" class="style211">
                        <asp:Label ID="lblheading" runat="server" CssClass="label" Font-Bold="True" Font-Names="arial"
                            Font-Size="13px" Font-Underline="True" Text="NDMB PASS" Width="81%" Height="8px"></asp:Label>
                        <hr size="1" style="height: 1; width: 205px; margin-left: 0px;" />
                    </td>
                </tr>
                <tr>
                    <td align="center" class="style212">
                        <table>
                            <tr>
                                <td>
                                    <asp:Label ID="txtContractorCardNo" Font-Bold="true" ForeColor="Red" runat="server"></asp:Label>
                                    <br />
                                    <br />
                                    <strong><span style="color: Red; font-size: 12px;">DOI</span></strong>
                                    <br />
                                    <asp:Label ID="txtDateOfIssue" runat="server" BorderStyle="None" CssClass="style226"
                                        Font-Names="arial" Font-Size="12px" Font-Bold="true" OnTextChanged="TextBox3_TextChanged"
                                        Style="font-family: Calibri;" Width="45%"></asp:Label>
                                </td>
                                <td>
                                    <asp:Image ID="imgContractor" runat="server" BorderColor="#1F76AE" BorderStyle="Solid"
                                        BorderWidth="1px" Height="90px" Width="100px" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td class="style219" height="4px">
                        <asp:Label ID="lblName" runat="server" CssClass="label" Font-Bold="True" Font-Names="Calibri"
                            Font-Size="Small" Text="Name:" Width="21%"></asp:Label>
                        <asp:Label ID="txtContractorName" runat="server" BorderStyle="None" CssClass="style226"
                            Font-Names="Calibri" Font-Size="12px" Height="14px" OnTextChanged="TextBox2_TextChanged"
                            Style="font-family: Calibri; margin-top: 0px;" Width="73%"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="style220">
                        <asp:Label ID="lbldesi" runat="server" CssClass="label" Font-Bold="True" Font-Names="Calibri"
                            Font-Size="Small" Text="Desig:" Width="15%" Height="16px"></asp:Label>&nbsp;&nbsp;&nbsp;
                        <asp:Label ID="txtDesignation" runat="server" BorderStyle="None" CssClass="style226"
                            Font-Names="Calibri" Font-Size="12px" OnTextChanged="TextBox3_TextChanged" Style="font-family: Calibri;"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="style221">
                        <asp:Label ID="lblfirm" runat="server" CssClass="label" Font-Bold="True" Font-Names="Calibri"
                            Font-Size="Small" Text="Firm:" Width="19%"></asp:Label>
                        <asp:Label ID="txtContractorFirm" runat="server" BorderStyle="None" CssClass="style224"
                            Font-Names="Calibri" Font-Size="12px" Height="16px" OnTextChanged="TextBox3_TextChanged"
                            Width="76%"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="style221">
                        <asp:Label ID="lblpvc" runat="server" CssClass="label" Font-Bold="True" Font-Names="Calibri"
                            Font-Size="Small" Text="UID NO :" Width="15%"></asp:Label>&nbsp&nbsp&nbsp
                        <asp:Label ID="txtPVCDate" runat="server" BorderStyle="None" CssClass="style226"
                            Font-Names="Calibri" Font-Size="12px" Height="16px" OnTextChanged="TextBox3_TextChanged"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="style209">
                        <asp:Label ID="txtEscortType" runat="server" BorderStyle="None" Font-Names="Calibri"
                            Font-Size="Small" Font-Underline="False" OnTextChanged="TextBox3_TextChanged"
                            Style="font-size: 12px; color: #FF0000; font-weight: 700; margin-left: 5px;"
                            Visible="false"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="style220">
                        <asp:Label ID="lblcard" runat="server" CssClass="label" Font-Bold="True" Font-Names="Calibri"
                            Font-Size="Small" Text="Card No:" Visible="false" Width="25%"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="center">
                        <table>
                            <caption>
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
                            </caption>
                        </table>
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <br />
        <br />
        <input id="printbtn" type="button" value="Print this page" onclick="window.print();" /><%-- <input id="printbtn" type="button" value="Print this page" onclick="window.print();" />--%>
    </div>
    </form>
</body>
</html>
