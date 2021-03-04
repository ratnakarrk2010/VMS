<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LOGIN.aspx.cs" Inherits="EntityFrameworkDBF.LOGIN" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server" id="Head1">
    <title></title>
    <link href="../App_Themes/Admin_Basic/Themes/Style12.css" rel="stylesheet" type="text/css" />
    <link href="../App_Themes/Admin_Basic/Themes/commonCSS.css" rel="stylesheet" type="text/css" />
    <link href="../App_Themes/Admin_Basic/Themes/lightbox.css" rel="stylesheet" type="text/css" />
    <link href="../App_Themes/Admin_Basic/Themes/menu.css" rel="stylesheet" type="text/css" />
    <link href="../App_Themes/Admin_Basic/Themes/mouseovertabs.css" rel="stylesheet"
        type="text/css" />
    <link href="../App_Themes/Admin_Basic/Themes/sdmenu.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/templatemo_style.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        #slider1
        {
            width: 720px; /* important to be same as image width */
            height: 300px; /* important to be same as image height */
            position: relative; /* important */
            overflow: hidden;
            top: -13px;
            left: -3px;
        }
        
        #slider1Content
        {
            width: 720px; /* important to be same as image width or wider */
            position: absolute;
            top: 0;
            margin-left: 0;
            margin-right: auto;
        }
        .slider1Image
        {
            float: left;
            position: relative;
            display: none;
        }
        .slider1Image span
        {
            position: absolute;
            font: 10px/15px Arial, Helvetica, sans-serif;
            padding: 10px 13px;
            width: 694px;
            background-color: #000;
            filter: alpha(opacity=70);
            -moz-opacity: 0.7;
            -khtml-opacity: 0.7;
            opacity: 0.7;
            color: #fff;
            display: none;
        }
        .clear
        {
            clear: both;
        }
        .slider1Image span strong
        {
            font-size: 14px;
        }
        .left
        {
            top: 0;
            left: 0;
            width: 110px !important;
            height: 280px;
        }
        .right
        {
            right: 30px;
            bottom: 0;
            width: 110px !important;
            height: 280px;
        }
        ul
        {
            list-style-type: none;
        }
        
     
        .twoColLiqLtHdr #loginpage
        {
            padding: 0 10px;
            width: 97%;
            margin-left: auto;
            margin-right: auto;
            margin-top: 5px;
            margin-bottom: 0px;
            font-family: Verdana, Arial, Helvetica, sans-serif;
            color: #36749f;
            font-size: 70%;
            padding-top: 10px;
            padding-left: 5px;
            padding-right: 5px;
            padding-bottom: 5px;
            min-height: 400px;
        }
        
        .loginhead
        {
            font-size: 18px;
            font-family: Verdana, Arial, Helvetica, sans-serif;
            color: #145d7f;
            margin: 0px;
            padding-top: 5px;
        }
        
        .textbox
        {
            width: 90%;
            padding: 5px;
            background-color: #eef9ff;
            margin: 5px 0px;
            border: 1px solid #84bbe0;
            color: #3e3e3e;
            font-size: 12px;
        }
        
        .textbox:hover
        {
            background-color: #ffffff;
        }
        
        
        .style1
        {
            width: 309px;
            height: 308px;
        }
        
        #footer1
        {
            padding: 0 10px;
            -moz-border-radius: 10px 10px 10px 10px;
            border-radius: 10px 10px 10px 10px;
            -webkit-border-radius: 10px 10px 10px 10px;
            -khtml-border-radius: 10px 10px 10px 10px;
            border: 1px solid #7bb3d9;
            background-color: #b8e2ff;
            height: 30px;
            width: 97%;
            margin-left: auto;
            margin-right: auto;
            margin-top: 5px;
            margin-bottom: 5px;
            font-family: Verdana, Arial, Helvetica, sans-serif;
            color: #36749f;
            font-size: 70%;
            bottom: 0;
            position: absolute;
        }
    </style>
    <!-- JavaScripts-->
    <!-- <script>window.jQuery || document.write('<script src="local/jquery-X.X.X.min.js">\x3C/script>')</script>-->
    <script src="Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="Scripts/s3Slider.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#slider1').s3Slider({
                timeOut: 4000
            });
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div id="templatemo_header_wrapper">
        <div id="templatemo_header">
            <div id="templatemo_menu">
                 <div class="UserView">
                    <span>
                         <br /><br />

                          <b> DESIGNED BY:- LT CDR SANT SINGH, ACSO&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</b> 
                   
                       
                    </span>
                </div>
            </div>
            <div class="cleaner" >
             
            </div>
        </div>
    </div>
    <div id="templatemo_content_wrapper">
        <div id="templatemo_content">
            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                <tr>
                    <td width="64%" height="60px">
                        &nbsp;
                    </td>
                    <td width="36%">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td align="center" width="70%" style="padding-right: 20px;">
                        <div id="slider1">
                            <ul id="slider1Content">
                                <li class="slider1Image"><a href="">
                                    <img src="images/11.jpg" alt="1" /></a> <span class="left"><strong>VMS</strong><br />
                                        The secure and professional approach to identify and manage visitors within your
                                        organization.</span></li>
                                <li class="slider1Image"><a href="">
                                    <img src="images/22.jpg" alt="2" /></a> <span class="right"><strong>VMS</strong><br />
                                        Monitoring movement of visitors in the organization.</span></li>
                                <li class="slider1Image">
                                    <img src="images/33.jpg" alt="3" />
                                    <span class="right"><strong>VMS</strong><br />
                                        Restricted access to visitors to any particular department or area.</span></li>
                                <li class="slider1Image">
                                    <img src="images/44.jpg" alt="4" />
                                    <span class="left"><strong>VMS</strong><br />
                                        Monitoring the visitors & their activities in the company.</span></li>
                                <li class="slider1Image">
                                    <img src="images/55.jpg" alt="5" />
                                    <span class="right"><strong>VMS</strong><br />
                                        Secure Visitor Access.</span></li>
                                <div class="clear slider1Image">
                                </div>
                            </ul>
                        </div>
                    </td>
                    <td align="left" width="30%" style="border-left: 2px dotted #a2a2a2; padding-left: 15px;">
                        <table width="200px" cellpadding="0" cellspacing="0" class="border">
                            <tr>
                                <td style="border-bottom: 1px dotted #5e91c5; color: #000000; padding-left: 10px;"
                                    class="loginhead">
                                    Sign In
                                </td>
                            </tr>
                            <tr>
                                <td height="5px">
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:TextBox ID="UserName" runat="server" onblur="if(this.value=='') this.value='User Name'"
                                        onFocus="if(this.value =='User Name' ) this.value=''" Text="User Name" CssClass="textbox">
                                    </asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:TextBox ID="Password" runat="server" Text="Password" onblur="if(this.value=='') this.value='Password'"
                                        onFocus="if(this.value =='Password   ' ) this.value=''" TextMode="Password" CssClass="textbox">
                                    </asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" style="padding-top: 3px;">
                                    <asp:Button runat="server" ID="btnLogin" CssClass="button" Text="Login" OnClick="btnLogin_Click" />
                                    <%--<asp:Button runat="server" ID="resetBtn" CssClass="button" Text="Reset" OnClick="resetBtn_Click" />--%>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" style="padding-left: 4px; padding-top: 7px;">
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    &nbsp;<asp:Label ID="FailureText" runat="server" Text=" "></asp:Label>
                                    &nbsp;
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <div id="templatemo_footer_wrapper">
        <div id="templatemo_footer">
                <div style="vertical-align:middle; font-size:14px; text-align:center;">
                     
                </div>
        </div>
    </div>
    </form>
</body>
</html>
