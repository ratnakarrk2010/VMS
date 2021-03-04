<%@ Page Title="" Language="C#" MasterPageFile="~/VMSMaster.master" AutoEventWireup="true"
    CodeBehind="visitorUpdate.aspx.cs" Inherits="EntityFrameworkDBF.FOREIGN_VISITOR.visitorUpdate"
    Theme="Admin_Basic" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="Scripts/jquery-ui.css" rel="stylesheet" type="text/css" />
    <script src="Scripts/jquery-ui.min.js" type="text/javascript"></script>
    <script src="Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="Scripts/jquery-1.4.1-vsdoc.js" type="text/javascript"></script>
    <script src="Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="Scripts/jquery-1.4.1.js" type="text/javascript"></script>
    <script src="Scripts/jquery-1.7.1.min.js" type="text/javascript"></script>
    <style type="text/css">
        .style2
        {
            width: 138px;
        }
    </style>
    <script src="Scripts/jquery-1.7.2.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(function () {
            $(".editable").keyup(function () {
                var label = $(this);

                label.after("<input type='text' style='display:none;cursor:pointer;background-color:yellow;' />");
                var textbox = $(this).next();

                var id = this.id.split('_')[this.id.split('_').length - 1];
                textbox[0].name = id.replace("lbl", "txt");
                textbox.val(label.html());
                label.click(function () {
                    $(this).hide();
                    $(this).next().show();
                });

                textbox.focusout(function () {
                    $(this).hide();
                    $(this).prev().html($(this).val());
                    $(this).prev().show();
                });
            });
        });
    </script>
    <style type="text/css">
        .editable
        {
            cursor: pointer;
        }
        
        .editable:hover
        {
            background-color: #a0dbd5;
        }
        .style3
        {
            width: 301px;
        }
        .style4
        {
            width: 108px;
        }
        .button
        {
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="main" runat="Server">
    &nbsp;&nbsp;
    <td id="td_message" runat="server" colspan="3" align="left">
        <span id="lblErrorMessage" runat="server"></span>
    </td>
    <center>
        <%-- <div class="header_02">
            DVSC : PASS MANAGEMENT SYSTEM - <span id="spnDate" runat="server"></span>
            <br />
            <br />
            ISSUES
        </div>--%>
        <div style="padding-left: 900px">
            <asp:Button ID="btnissuehome" runat="server" Text="BACK TO HOME" CssClass="btnView"
                Style="color: White" OnClick="btnissuehome_Click" />
        </div>
    </center>
    <table width="100%">
        <tr>
            <td valign="top" nowrap="nowrap">
                Search By:
            </td>
            <td valign="top" class="style2">
                <asp:DropDownList runat="server" ID="ddlSearchBy" Style="height: 29px" OnTextChanged="OnSelectedIndexChanged_ddlSearchBy"
                    AutoPostBack="true">
                    <asp:ListItem>VISITOR ID</asp:ListItem>
                    <asp:ListItem>MOBILE NUMBER</asp:ListItem>
                    <asp:ListItem>PASSPORT</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td valign="top" class="style2">
                <asp:TextBox ID="txtSearchVisitor" runat="server" CssClass="bck" Width="120px" Style="margin-left: 0px;
                    margin-right: 1px;" Height="25px" placeholder="SEARCH" onkeydown="return CheckFirstChar(event.keyCode, this);"
                    onkeyup="CheckFirstChar(event.keyCode, this);" BackColor="#FFFFE8" OnTextChanged="txtSearchVisitor_TextChanged"></asp:TextBox>
            </td>
            <td valign="top" colspan="2">
                <asp:Button ID="btnVisitorSerach" runat="server" CssClass="button" Text="GO" Width="87px"
                    Font-Bold="True" Font-Names="Arial" Font-Size="Small" OnClick="btnVisitorSerach_Click" />
            </td>
            <td valign="top">
                <asp:Label ID="lblcount" runat="server" Text="count" Width="200" Style="font-weight: 700;
                    font-size: medium; color: #3838A9; font-family: Arial;" Visible="false"></asp:Label>
            </td>
            <td>
                <asp:CheckBox runat="server" ID="chk_visitor_type" AutoPostBack="true" Text="Executive"
                    Visible="false" />
                <%-- OnCheckedChanged="chk_visitor_type_checkchanged"--%>
            </td>
            <td>
                <asp:Image runat="server" Width="30" Height="30" ID="img_visitor_type_indi" ImageUrl="~/Images/red-triangle.png"
                    Visible="false" />
            </td>
            <td>
                <asp:Label runat="server" ID="lbl_visitor_color_code" ForeColor="Red" Text="RED"
                    Visible="false"></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="9">
                <hr style="width: 100%; color: #FFFFCC; margin-bottom: 4px; height: -12px; margin-left: 0px;
                    background-color: #666666;">
            </td>
        </tr>
        <tr>
            <td colspan="4" nowrap="nowrap">
                <asp:Label ID="lblvisitorid" runat="server" Text="DOCKYARD ID NO:"></asp:Label>
                <asp:Label ID="txtvisitorid" runat="server" Font-Size="Medium" Style="font-family: Arial;
                    font-weight: 700;"></asp:Label>
            </td>
            <td class="style123" rowspan="7" colspan="5" valign="top">
                <table style="width: 100%">
                    <tr>
                        <td class="style3">
                            <asp:Panel ID="pnl" runat="server">
                                <object style="height: 197px; width: 295px">
                                    <param name="movie" value="WebcamResources/save_picture.swf" />
                                    <embed height="200" src="WebcamResources/save_picture.swf" width="405"></embed>
                                </object>
                            </asp:Panel>
                        </td>
                        <td valign="top">
                            <asp:Panel ID="pnlAlreadyImage" runat="server">
                                <asp:Image ID="imgPicture" runat="server" Height="197px" Width="235px" />
                            </asp:Panel>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td nowrap="nowrap">
                VISITOR NAME:
            </td>
            <td class="style2">
                <asp:TextBox ID="txtVisitorName" runat="server" Height="25px" Width="120px" placeholder="VISITOR NAME"
                    CssClass="bck editable"></asp:TextBox>
            </td>
            <td nowrap="nowrap">
                <asp:Label ID="lblage" runat="server" Text="AGE :" CssClass="style187"></asp:Label>
            </td>
            <td class="style4">
                <asp:TextBox ID="txtage" runat="server" CssClass="bck" Height="25px" MaxLength="12"
                    placeholder="AGE" Width="90px" Enabled="true"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td nowrap="nowrap">
                <asp:Label ID="lblsex" runat="server" Text="GENDER :" CssClass="style187"></asp:Label>
            </td>
            <td class="style2">
                <asp:DropDownList ID="ddlsex" runat="server" Height="25px" Width="120px" Font-Bold="True"
                    Style="font-family: Arial; font-size: small; color: #808080">
                    <asp:ListItem Text="..SELECT.." Value="-1"></asp:ListItem>
                    <asp:ListItem Text="MALE" Value="MALE"></asp:ListItem>
                    <asp:ListItem Text="FEMALE" Value="FEMALE"></asp:ListItem>
                </asp:DropDownList>
            </td>
            <td nowrap="nowrap">
                NATIONALITY :
            </td>
            <td class="style4">
                <asp:DropDownList runat="server" ID="drpnationality" Width="90px">
                    <asp:ListItem Value="-1">..SELECT..</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td nowrap="nowrap">
                FIRM NAME :
            </td>
            <td class="style2">
                <asp:TextBox ID="txtOrganization" runat="server" Height="25px" Width="120px" placeholder="FIRM NAME"
                    CssClass="bck"></asp:TextBox>
            </td>
            <td nowrap="nowrap">
                FIRM TIN :
            </td>
            <td class="style4">
                <asp:TextBox ID="TXTFRIMTIN" runat="server" Height="25px" Width="90px" placeholder="FIRM TIN"
                    CssClass="bck" MaxLength="13"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td nowrap="nowrap">
                MOBILE :
            </td>
            <td>
                <asp:TextBox ID="txtMobileNumber" runat="server" Height="25px" Width="120px" placeholder="MOBILE NO"
                    MaxLength="10" CssClass="bck"></asp:TextBox>
            </td>
            <td nowrap="nowrap">
                VISITOR TYPE :
            </td>
            <td colspan="3">
                <asp:TextBox ID="TXTVISITORTYPE" runat="server" Height="25px" Width="90px" placeholder="VISITOR TYPE"
                    CssClass="bck"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td nowrap="nowrap" style="visibility: hidden;">
                VISIT PURPOSE :
            </td>
            <td class="style2">
                <asp:TextBox ID="TXTPURPOSE" runat="server" Height="25px" Width="120px" placeholder="VISIT PURPOSE"
                    Visible="false" CssClass="bck"></asp:TextBox>
            </td>
            <td nowrap="nowrap" style="visibility: hidden;">
                DURATION :
            </td>
            <td class="style4">
                <asp:TextBox ID="TXTDUR" runat="server" Height="25px" Width="90px" placeholder="VISIT DURATION"
                    Visible="false" CssClass="bck"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td colspan="4">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td nowrap="nowrap">
                VISA :
            </td>
            <td class="style2">
                <asp:TextBox ID="txtVisa" runat="server" CssClass="bck" Height="25px" placeholder="VISA"
                    Width="90px" Enabled="true"></asp:TextBox>
            </td>
            <td nowrap="nowrap">
                SECURITY CL
            </td>
            <td class="style4">
                <asp:TextBox ID="txtSecCL" runat="server" CssClass="bck" Height="25px" MaxLength="12"
                    placeholder="SECURITY CL" Width="90px" Enabled="true"></asp:TextBox>
            </td>
            <td nowrap="nowrap">
                SECURITY CL LETTER NO
            </td>
            <td class="style4">
                <asp:TextBox ID="txtLetterNo" runat="server" CssClass="bck" Height="25px" MaxLength="12"
                    placeholder="SECURITY CL LETTER NO" Width="90px" Enabled="true"></asp:TextBox>
            </td>
            <td nowrap="nowrap">
                SECURITY CL VALIDITY
            </td>
            <td class="style4">
                <asp:TextBox ID="txtCLVALIDITY" runat="server" CssClass="bck" Height="25px" MaxLength="12"
                    placeholder="SECURITY CL VALIDITY" Width="90px" Enabled="true"></asp:TextBox>
                <asp:CalendarExtender ID="FromDateCalendar" runat="server" Enabled="True" Format="dd/MM/yyyy"
                    TargetControlID="txtCLVALIDITY">
                </asp:CalendarExtender>
            </td>
        </tr>
        <tr>
            <td nowrap="nowrap">
                ID TYPE :
            </td>
            <td class="style2">
                <asp:DropDownList ID="ddlidentity" runat="server" Height="25px" Width="120px" Font-Bold="True"
                    Style="font-family: Arial; font-size: small; color: #808080" OnSelectedIndexChanged="ddlidentity_SelectedIndexChanged"
                    AutoPostBack="true">
                    <asp:ListItem Text="..SELECT.." Value="-1"></asp:ListItem>
                    <asp:ListItem Text="PASSPORT" Value="PASSPORT"></asp:ListItem>
                    <%--  <asp:ListItem Text="AADHAR CARD" Value="AADHAR CARD"></asp:ListItem>
                    <asp:ListItem Text="PAN CARD" Value="PAN CARD"></asp:ListItem>
                    <asp:ListItem Text="DRIVING LICENCE" Value="DRIVING LICENCE"></asp:ListItem>--%>
                </asp:DropDownList>
            </td>
            <td nowrap="nowrap">
                ID NO :
            </td>
            <td class="style4">
                <asp:TextBox ID="txtidno" runat="server" CssClass="bck" Height="25px" MaxLength="12"
                    placeholder="ID NUMBER" Width="90px" Enabled="true"></asp:TextBox>
            </td>
            <td colspan="3">
                <asp:FileUpload runat="server" ID="flileupload" />
                <asp:Button runat="server" Text="ADD" ID="btn_add_file" OnClick="btn_add_file_click" />
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button ID="Button1" runat="server" CssClass="button" Text="UPDATE PHOTO" Width="120px"
                    Font-Bold="True" Font-Names="Arial" Font-Size="Small" OnClick="Button1_Click" />
            </td>
        </tr>
        <tr>
            <td colspan="9">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td colspan="9">
                <table width="100%">
                    <tr>
                        <td colspan="2">
                            <center>
                                <asp:Label runat="server" ID="lbl_aadhar" Text="AADHAR CARD"></asp:Label><br />
                                <asp:Image runat="server" ID="img_aadhar" Width="200" Height="200" /><br />
                                <asp:Label runat="server" ID="lbl_aadhar_no"></asp:Label>
                            </center>
                        </td>
                        <td colspan="2">
                            <center>
                                <asp:Label runat="server" ID="Label1" Text="PANCARD"></asp:Label><br />
                                <asp:Image runat="server" ID="img_pancard" Width="200" Height="200" /><br />
                                <asp:Label runat="server" ID="lbl_pancard_no"></asp:Label>
                            </center>
                        </td>
                        <td colspan="2">
                            <center>
                                <asp:Label runat="server" ID="Label3" Text="PASSPORT"></asp:Label><br />
                                <asp:Image runat="server" ID="img_passport" Width="200" Height="200" /><br />
                                <asp:Label runat="server" ID="lbl_passport_no"></asp:Label>
                            </center>
                        </td>
                        <td colspan="3">
                            <center>
                                <asp:Label runat="server" ID="Label5" Text="DRIVING LIC."></asp:Label><br />
                                <asp:Image runat="server" ID="img_driving_lic" Width="200" Height="200" /><br />
                                <asp:Label runat="server" ID="lbl_drivinglic_no"></asp:Label>
                            </center>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <br />
    <center>
        <table>
            <tr>
                <td class="style195">
                    <asp:Button ID="BTNUPDATE" runat="server" CssClass="button" Text="UPDATE" Width="80px"
                        Font-Bold="True" Font-Names="Arial" Font-Size="Small" OnClick="BTNUPDATE_Click" />
                </td>
                <td>
                </td>
                <td>
                    <asp:Button ID="btnCancel" runat="server" CssClass="button" Text="CANCEL" Width="80px"
                        Font-Bold="True" Font-Names="Arial" Font-Size="Small" OnClick="btnCancel_Click" />
                </td>
                <td>
                    &nbsp;
                    <asp:Button ID="btnPrint" runat="server" CssClass="button" Text="PRINT" Width="80px"
                        Font-Bold="True" Font-Names="Arial" Font-Size="Small" OnClick="btnPrint_Click"
                        Visible="false" />
                </td>
            </tr>
        </table>
    </center>
</asp:Content>
