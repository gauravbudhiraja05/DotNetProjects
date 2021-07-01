<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Registration.aspx.vb" Inherits="Content_Registration" %>
<%@ Register src="~/Content/WebControl/Navigation.ascx" tagname="Navigation" tagprefix="uc1" %>
<%@ Register Src="~/Content/WebControl/TopBanner.ascx" TagName="Top" TagPrefix="uc2" %>
<%@ Register Src="~/Content/WebControl/LeftPanel.ascx" TagName="Left" TagPrefix="uc3" %>
<%@ Register Src="~/Content/WebControl/FooterBanner.ascx" TagName="Footer" TagPrefix="uc4" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1" />
<title>QlickReport Software combines your Financial and Operational data into a single, seamless source of information for better business decisions</title>
<link href="../css/base.css" rel="stylesheet" type="text/css" />
</head>
<body style="background-image:url('images/fadestrip2.png'); background-position:top; background-repeat:repeat-x; background-attachment: fixed;">
<!--navigation -->
 <form id="Form2" runat="server">
<uc1:Navigation ID="Navigation" runat="server" />

<!--navigation -->
<!--header -->

<uc2:Top ID="Top" runat="server" />

<!--header -->
<!--body -->

<div class="container_16">
    <div class="grid_16" id="body">
        <div class="grid_6  alpha">
<!--left panel -->
            <%--<uc3:Left ID="Left" runat="server" />--%>
             <div id="lftPan" style="background-color:White; margin-left:10px; margin-right:10px; border-bottom-color:Black;">
                <%--<form id="form1" runat="server">--%>
                <asp:Label runat="server" ID="Label1" Visible="false" ForeColor="Red"></asp:Label><asp:Label runat="server" ID="lbldays" Visible="false" ForeColor="Red"></asp:Label>
                    <p class="member">Member login</p>
                    <label style="width:80px;">Username :</label>
                    <input id="txtuserid" name="txtuserid" style="width:183px;" type="text" class="txtfield" runat="server"/>
                    <label style="width:80px;">Password :</label>
                    <input id="txtpassword" name="txtpassword" style="width:183px;" type="password" class="txtfield" runat="server"/>
                    <p class="forgot"><a href="Misc/ForgotPassword.aspx">Forgot Password?</a></p>
                  <%--  <input name="" type="submit" class="login" value="" title="login"/>--%>
                    <asp:Button ID="Button1" Text="Login" CssClass="login" runat="server" />
                    <%--<input id="Login" name="" type="button" class="login" value="" title="login" onclick="return Login_onclick()" />--%>
                    <div class="clear"></div>
                   <%-- </form>--%>
            </div>
            <div id="Div1">    
                <h2 class="service">Salient Features </h2>
                <p class="genareted">To build any type of report, Query, Analysis & Graphs</p>
                    <div class="clear"></div>
                    <p class="pic">&nbsp;</p>
                        <ul class="numberLink">
	                        <li><span>01</span>To enable dynamic, interactive analysis like calculations, grouping and filtering for end users</li>
	                        <li><span>02</span>To control who can view reports as well as the data within those reports. </li>
	                        <li><span>03</span>For end users to select, view, compare and visualize the data most meaningful to them.</li>
	                        <li><span>04</span>Make effective use of the graphical presentation of your data with powerful mapping capabilities </li>
	                        <li><span>05</span>able to provide various analysis base data tables and views. </li>
	                    </ul>
            </div>
<!--left panel -->
        </div>
        <div class="grid_10 omega" style="width:550px;text-align:justify;">
            <!--Detail Content panel START-->
            <h2>Registration</h2>
               
            <h2 style="height:10px;"></h2>You Selected : <asp:Label ID="selectitem" runat="server" Text="Selected Item" ></asp:Label>&nbsp;&nbsp;<asp:Label ID="lblmsg" runat="server" Visible="false" ForeColor="Green" Text=""></asp:Label>  
             <h2 style="height:10px;"></h2><asp:Label ID="finalmsg" runat="server"  Font-Bold="true" ForeColor="Blue"  Visible="false" ></asp:Label>&nbsp;&nbsp;<asp:Label 
                    ID="mailmsg" runat="server" 
                    Text="Your Userid and Password has send to your given E-mail Id" 
                    Font-Bold="True" ForeColor="Blue"  Visible="False" ></asp:Label>
             <table cellpadding="0" cellspacing="0" border="1" style="width:550px;">
                <tr>
                    <td style="font-family:Verdana; font-size:11px; color:Black; width:50px;"><b>Name</b></td>
                    <td style="width:20px;"></td>
                    <td><asp:TextBox ID="txtname" runat="server" BorderStyle="Solid" Width="150px" BorderColor="#D7D6D6" BorderWidth="1px" Font-Names="verdana"></asp:TextBox></td>
                    <td style="width:10px;"></td>
                    <td style="font-family:Verdana; font-size:11px; color:Red; width:320px;"><asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtname" ErrorMessage="Plz enter characters only" ValidationExpression="([a-zA-Z][a-zA-Z.][a-zA-Z.])[a-z A-Z.]*" ValidationGroup="check"></asp:RegularExpressionValidator></td>
                </tr>
                <tr><td style="height:15px;"></td></tr>
                <tr>
                    <td style="font-family:Verdana; font-size:11px; color:Black; width:50px;"><b>Company</b></td>
                    <td style="width:20px;"></td>
                    <td><asp:TextBox ID="txtCmy" runat="server" BorderStyle="Solid" Width="150px" BorderColor="#D7D6D6" BorderWidth="1px" Font-Names="verdana"></asp:TextBox></td>
                    <td style="width:10px;"></td>
                    <td style="font-family:Verdana; font-size:11px; color:Red; width:320px;"></td>
                </tr>
                <tr><td style="height:15px;"></td></tr>
                <tr>
                    <td style="font-family:Verdana; font-size:11px; color:Black; width:50px;"><b>Email</b></td>
                    <td style="width:20px;"></td>
                    <td><asp:TextBox ID="txtemail" runat="server" BorderStyle="Solid" Width="150px" BorderColor="#D7D6D6" BorderWidth="1px" Font-Names="verdana"></asp:TextBox></td>
                    <td style="width:10px;"></td>
                    <td style="font-family:Verdana; font-size:11px; color:Red; width:320px;"></td>
                </tr>
                <tr><td style="height:15px;"></td></tr>
                <tr>
                    <td style="font-family:Verdana; font-size:11px; color:Black; width:50px;"><b>Mobile</b></td>
                    <td style="width:20px;"></td>
                    <td><asp:TextBox ID="txtmobile" runat="server" BorderStyle="Solid" Width="150px" BorderColor="#D7D6D6" BorderWidth="1px" Font-Names="verdana"></asp:TextBox></td>
                    <td style="width:10px;"></td>
                    <td style="font-family:Verdana; font-size:11px; color:Red; width:320px;"><asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
                            ControlToValidate="txtmobile" ErrorMessage="Plz enter numeric and only 11 digit number !!" 
                            ValidationExpression="(^[0-9]{1,11})"></asp:RegularExpressionValidator></td>
                </tr>
                <tr><td style="height:15px;"></td></tr>
                <tr>
                        
                    <td colspan="3" align="center"  >
                         <asp:Button id="submit"  ForeColor="Black"   Font-Bold="true"  runat="server" Text="Submit" />
                    </td>
                </tr>
             </table>
           </form>
            <!--Detail Content panel END-->
        </div>
    </div>
</div>

<!--body -->
<!--footer -->
<uc4:Footer ID="Footer" runat="server" />
<!--footer -->
</body>
</html>
