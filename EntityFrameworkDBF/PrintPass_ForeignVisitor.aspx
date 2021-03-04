<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PrintPass_ForeignVisitor.aspx.cs"
    Inherits="EntityFrameworkDBF.PrintPass_ForeignVisitor" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <script type="text/javascript">
        function PrintPanel() {
            var panel = document.getElementById("<%=Panel1.ClientID %>");
            var printWindow = window.open('', '_blank', 'height=600,width=800,location=no,toolbar=no,menubar=no,status=no,scrollbars=yes,resizable=yes,left=0,top=0');
            printWindow.document.write('<html><head>');
            printWindow.document.write('</head><body onload="window.print(); window.close();">');
            printWindow.document.write(panel.innerHTML);
            printWindow.document.write('</body></html>');
            document.getElementById('printbtn').visible = false;
            printWindow.document.close();
            printWindow.focus();
            /* setTimeout(function () {
            printWindow.print();
            }, 500);
            return false;*/
        }

        function PrintSection() {
            document.getElementById('printbtn').visible = false;
            window.print();
        }
    </script>
    <script language="javascript" type="text/javascript">
        function prinpage() {
            document.getElementById('printbtn').visible = false;
            window.print();
        }
    
    </script>
    <script language="javascript" type="text/javascript">
        function prinpage() {
            document.getElementById('printbtn').visible = false;
            window.print();
        }
    </script>
    <script type="text/javascript">
        document.body.onload = function () { window.print1(); }
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
</head>
<%--onload="window.print(); window.close();"--%>
<body>
    <form id="form1" runat="server">
    <asp:Panel ID="Panel1" runat="server" Width="100%">
        <div style="border-style: solid; border-color: Black; height: 575px; width: 100%;
            font-family: Calibri;">
            <div style="width: 100%; height: 100%; position: absolute; background: url(Images/logo.jpg) no-repeat scroll center center ! important;
                opacity: 0.5; z-index: -1;">
            </div>
            <div>
                <table style="margin: 0; border: 1px;" border="0px;" width="100%">
                    <tr>
                        <td colspan="3">
                            <table width="100%">
                                <tr>
                                    <td valign="top" class="style1">
                                        <asp:Panel ID="Panel6" runat="server" GroupingText="Visitor's ID" Height="1px" Width="90px"
                                            Font-Bold="True" Font-Names="Calibri" Font-Size="Small">
                                            &nbsp; &nbsp;&nbsp;
                                            <asp:Label ID="txtVID" runat="server" Font-Names="Times New Roman" Font-Size="Small"
                                                Style="font-weight: 700; font-family: Calibri;"></asp:Label>
                                        </asp:Panel>
                                        <br />
                                        <br />
                                        <asp:Panel ID="Panel7" runat="server" Height="1px" Width="90px" GroupingText="Pass No."
                                            Font-Bold="True" Font-Names="Calibri" Font-Size="Small">
                                            &nbsp;&nbsp;&nbsp;&nbsp;
                                            <asp:Label ID="txtPassNo" runat="server" Font-Names="Times New Roman" Font-Size="Small"
                                                Style="font-weight: 700; font-family: Calibri;"></asp:Label>
                                        </asp:Panel>
                                        <br />
                                        <br />
                                        <asp:Panel ID="Panel8" runat="server" Height="1px" Width="90px" GroupingText="Card No."
                                            Font-Bold="True" Font-Size="Small" Font-Names="Calibri">
                                            &nbsp;&nbsp;&nbsp;&nbsp;
                                            <asp:Label ID="txtCardNo" runat="server" BorderStyle="None" Font-Names="Times New Roman"
                                                Font-Size="Small" OnTextChanged="TextBox8_TextChanged" Style="margin-left: 0px;
                                                font-weight: 700; font-family: Calibri;" Width="33px"></asp:Label>
                                        </asp:Panel>
                                    </td>
                                    <td colspan="2" valign="top">
                                        <center>
                                            <asp:Panel ID="Panel3" runat="server">
                                                <span style="color: #000000; font-size: 13pt; font-weight: 800; font-style: normal;
                                                    text-decoration: none;">NAVAL DOCKYARD MUMBAI<br />
                                                </span>(To be returned to the security office)<br />
                                                <b>FOREIGN VISITOR DAILY PASS - NO EXTENSION</b>
                                                <br />
                                                <asp:PlaceHolder ID="plBarCode" runat="server" />
                                            </asp:Panel>
                                        </center>
                                    </td>
                                    <td style="width: 110px; height: 110px; margin-right: 10px;">
                                        <asp:Image ID="imgVisitor" runat="server" Height="100%" Width="100%" /><br />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" height="1px">
                            <hr />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <table style="width: 100%">
                                <tr>
                                    <td style="height: 0px;">
                                        Visitors Name
                                    </td>
                                    <td style="height: 0px;">
                                        :
                                        <asp:Label ID="txtVName" runat="server"></asp:Label>
                                    </td>
                                    <td style="height: 0px;">
                                        Mobile Number
                                    </td>
                                    <td style="height: 0px;">
                                        :
                                        <asp:Label ID="txtMobileNUmber" runat="server"></asp:Label>
                                    </td>
                                    <td rowspan="9">
                                        <table style="margin-right: 0px; width: 100%;">
                                            <tr>
                                                <td>
                                                    <%--  <asp:PlaceHolder ID="plBarCode" runat="server" />--%>
                                                    <asp:Image runat="server" ID="img_visitor_indicator" />
                                                    <asp:Label runat="server" ID="lbl_visitor_indicator"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Panel ID="Panel9" runat="server" Font-Size="Small" GroupingText="Date Of Validity"
                                                        Height="63px" Width="170px" Font-Bold="True" Font-Names="Calibri" Visible="false">
                                                        &nbsp;<asp:Label ID="From" runat="server" Font-Size="Small" Font-Names="Calibri"
                                                            CssClass="label"></asp:Label>
                                                        &nbsp;&nbsp;
                                                        <asp:Label ID="txtFromDate1" runat="server" BorderStyle="None" Font-Names="Calibri"
                                                            Font-Size="Small" Height="16px" OnTextChanged="TextBox4_TextChanged" Width="46%"></asp:Label>
                                                        &nbsp;&nbsp;&nbsp;&nbsp;
                                                        <br />
                                                        <asp:Label ID="lbltodate" runat="server" CssClass="label" Font-Names="Calibri" Font-Size="Small"
                                                            Height="16px" Text="&nbsp;To" Width="16%" Visible="false"></asp:Label>
                                                        &nbsp;&nbsp;&nbsp;&nbsp;
                                                        <asp:Label ID="txtTodate" runat="server" BorderStyle="None" Font-Bold="False" Font-Names="Calibri"
                                                            Font-Size="Small" Height="16px" OnTextChanged="TextBox4_TextChanged" Style="margin-bottom: 0px;
                                                            font-weight: 700;" Width="49%" Visible="false"></asp:Label>
                                                        <br />
                                                    </asp:Panel>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Panel ID="Panel11" runat="server" GroupingText="Time " Font-Bold="True" Font-Names="Calibri"
                                                        Font-Size="Small" Width="170px">
                                                        &nbsp; IN&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; :
                                                        <asp:Label ID="txtInTime" runat="server"></asp:Label>
                                                    </asp:Panel>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Panel ID="Panel4" runat="server" Font-Size="Small" GroupingText="Rec. Officer Sign &amp; Seal "
                                                        Font-Bold="True" Font-Names="Calibri" Width="170px">
                                                        <br />
                                                    </asp:Panel>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Panel ID="Panel5" runat="server" Font-Bold="True" Font-Names="Calibri" Font-Size="Small"
                                                        GroupingText="Security Office Seal" Width="170px" Height="32px">
                                                        <br />
                                                        <br />
                                                    </asp:Panel>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="height: 0px;">
                                        Organization
                                    </td>
                                    <td style="height: 0px;">
                                        :
                                        <asp:Label ID="ddlorg" runat="server"></asp:Label>
                                    </td>
                                    <td style="height: 0px;">
                                        Nationality
                                    </td>
                                    <td style="height: 0px;">
                                        :
                                        <asp:Label ID="txtnation" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="height: 0px;">
                                        Govt ID Type
                                    </td>
                                    <td style="height: 0px;">
                                        :
                                        <asp:Label ID="ddlidentity" runat="server"></asp:Label>
                                    </td>
                                    <td style="height: 0px;">
                                        Govt ID Number
                                    </td>
                                    <td style="height: 0px;">
                                        :
                                        <asp:Label ID="txtidno" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="height: 0px;">
                                        Age
                                    </td>
                                    <td style="height: 0px;">
                                        :
                                        <asp:Label ID="txtage" runat="server"></asp:Label>
                                    </td>
                                    <td style="height: 0px;">
                                        Gender
                                    </td>
                                    <td style="height: 0px;">
                                        :
                                        <asp:Label ID="txtGender" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="height: 0px;">
                                        Valid Till
                                    </td>
                                    <td style="height: 0px;">
                                        :
                                        <asp:Label ID="txtValidity" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="height: 0px;" colspan="2">
                                        <strong><u>Electronic Storage Device Details</u> </strong>
                                    </td>
                                    <td style="height: 0px;" colspan="2">
                                        <strong><u>Vehicle Details</u> </strong>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" style="height: 0px;">
                                        <asp:GridView ID="GVGadget" runat="server" AutoGenerateColumns="false" Font-Size="Smaller"
                                            SkinID="grid">
                                            <Columns>
                                                <asp:BoundField DataField="GadgetName" HeaderText="Device Name" ReadOnly="True">
                                                    <ItemStyle Width="" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Gadget_SerialNo" HeaderText="Device Number" ReadOnly="True">
                                                    <ItemStyle Width="" />
                                                </asp:BoundField>
                                            </Columns>
                                        </asp:GridView>
                                        <asp:Label ID="Gstatus" runat="server"></asp:Label>
                                    </td>
                                    <td colspan="2" style="height: 0px;">
                                        <asp:GridView ID="GVVehicle" runat="server" AutoGenerateColumns="false" Font-Size="Smaller"
                                            SkinID="grid">
                                            <Columns>
                                                <asp:BoundField DataField="VehicleName" HeaderText="Vehicle Name" ReadOnly="True">
                                                    <ItemStyle Width="" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="VehicleNo" HeaderText="Vehicle Number" ReadOnly="True">
                                                    <ItemStyle Width="" />
                                                </asp:BoundField>
                                            </Columns>
                                        </asp:GridView>
                                        <asp:Label ID="Vstatus" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4" style="height: 0px;">
                                        <b><u>Recommending Officer</u></b>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" style="height: 0px;">
                                        Rank &amp; Name:
                                        <asp:Label ID="txtHostName" runat="server"></asp:Label>
                                    </td>
                                    <td colspan="2" style="height: 0px;">
                                        Department/Ship :
                                        <asp:Label ID="txtDepartmentName" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4" style="height: 0px;">
                                        <b><u>Escorted By</u></b>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" style="height: 0px;">
                                        Name &amp; Rank :
                                        <asp:Label ID="txtEmployeeName" runat="server"></asp:Label>
                                    </td>
                                    <td colspan="2" style="height: 0px;">
                                        T/P No :
                                        <asp:Label ID="txtTPNumber" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="height: 0px;" colspan="5">
                                        <span style="color: #000000; font-style: normal; text-decoration: underline;"><strong>
                                            Notes/Instructions</strong></span>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="5">
                                        <span style="color: #444444; font-style: normal; text-decoration: none; font-size: x-small;">
                                            1. This pass is valid only for the day till17:30 hrs.</span>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="5">
                                        <span style="color: #444444; font-style: normal; text-decoration: none; font-size: x-small;">
                                            2. Electronic Storage Devices like laptop,pen drive,mobile phone,camera,CD/DVD are
                                            not permitted unless authorised on separate performa.</span>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="5">
                                        <span style="color: #444444; font-style: normal; text-decoration: none; font-size: x-small;">
                                            3. The visitor will be escorted to and from the place of visit by nominated representative
                                            of the department being visited.</span>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="5">
                                        <span style="color: #444444; font-style: normal; text-decoration: none; font-size: x-small;">
                                            4. The visitor will only visit the officer or department mentioned in this pass.</span>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="5">
                                        <span style="color: #444444; font-style: normal; text-decoration: none; font-size: x-small;">
                                            5. This pass is to be signed by the officer being visited and returned to CV pass
                                            section before the visitor leaves the yard.</span>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="5">
                                        <span style="color: #444444; font-style: normal; text-decoration: none; font-size: xx-small;">
                                            6. While returning the passes ensure that it is signed by the recommending officer
                                            and has the department Stamp.</span>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </asp:Panel>
    <br />
    <input id="printbtn" type="button" value="Print this page" onclick="PrintSection();" />
    </form>
</body>
</html>
