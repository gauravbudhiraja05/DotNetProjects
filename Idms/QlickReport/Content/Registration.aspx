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
            <uc3:Left ID="Left" runat="server" />
<!--left panel -->
        </div>
        <div class="grid_10 omega" style="width:550px;text-align:justify;">
            <!--Detail Content panel START-->
            <h2>Registration</h2>
                <form runat="server">
            <h2 style="height:10px;"></h2>You Selected : <asp:Label ID="selectitem" runat="server" Text="Selected Item" ></asp:Label> 
             <h2 style="height:10px;"></h2><asp:Label ID="finalmsg" runat="server"  Font-Bold="true" ForeColor="Blue"  Visible="false" ></asp:Label>&nbsp;&nbsp;<asp:Label ID="mailmsg" runat="server" Text="Your Userid and Password are send to your given E-mail Id" Font-Bold="true" ForeColor="Blue"  Visible="false" ></asp:Label>
             <table>
                   <tr>
                       <td style="width:120px; height:40px;" valign="top"><asp:Label ID="lblname" Text="Name" ForeColor="Black" Font-Bold="true" runat="server"></asp:Label> </td>
                       <td valign="top"><asp:TextBox ID="txtname" CssClass="txtfield" runat="server" ></asp:TextBox>
                       <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                           ControlToValidate="txtname" ErrorMessage="Plz enter your name first !!"></asp:RequiredFieldValidator>

                       <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" 
                           ControlToValidate="txtname" ErrorMessage="Plz enter alphanumeric only" 
                           ValidationExpression="([a-zA-Z][a-zA-Z.][a-zA-Z.])[a-z A-Z.]*" 
                               ValidationGroup="check"></asp:RegularExpressionValidator>
                       </td>
                    </tr>
                    <tr>
                        <td style="width:120px; height:30px;" valign="top"><asp:Label ID="lblcmy" Text="Company" ForeColor="Black" Font-Bold="true" runat="server"></asp:Label></td><td><asp:TextBox ID="txtCmy"  CssClass="txtfield" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                            ControlToValidate="txtCmy" ErrorMessage="Plz enter your company name !!"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td style="width:120px; height:30px;"><asp:Label ID="lblemail" Text="E-Mail :" ForeColor="Black" Font-Bold="true" runat="server"></asp:Label></td><td><asp:TextBox ID="txtemail" CssClass="txtfield" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                            ControlToValidate="txtemail" ErrorMessage="Plz enter your e-mail Id !!"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" 
                            ControlToValidate="txtemail" ErrorMessage="Plz enter a valid e-mail id !!" 
                            ValidationExpression="^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$"></asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    <tr>
                        <td style="width:120px; height:30px;"><asp:Label ID="lblmobile" Text="Mobile" ForeColor="Black" Font-Bold="true" runat="server"></asp:Label></td><td><asp:TextBox ID="txtmobile" ValidationGroup="check"  CssClass="txtfield" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
                            ControlToValidate="txtmobile" ErrorMessage="Plz enter your mobile number !!"></asp:RequiredFieldValidator>
&nbsp;<asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
                            ControlToValidate="txtmobile" ErrorMessage="Plz enter numeric and only 11 digit number !!" 
                            ValidationExpression="(^[0-9]{1,11})"></asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    <tr><td style="height:30px;"></td></tr>
                    <tr>
                        <td style="width:120px; height:10px;">
                        <td><asp:Button id="submit"  ForeColor="Black"   Font-Bold="true"  runat="server" Text="Submit" /> </td>
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
