<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ForgotPassword.aspx.vb" Inherits="ForgotPassword" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" lang="en-us" xml:lang="en-us">
<head runat="server">
    <title>Forgot Password</title>
    <link href="../App_Themes/Themes/StyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <table id="tabFutureclosingPrice" cellSpacing="0" cellPadding="0" align="center" bgColor="white" runat="server" height="100" class="table">
			<caption>Forgot Password</caption>
			<%--<tr>
				<td colSpan="3" align="center" style="background:#59afbb;font-family:Verdana;color:#ffffff;font-size:12px;font-weight:bold;letter-spacing:0.01em;">
				<asp:label id="lblheading" Runat="server" Width="100%" >
				<b>Forgot Password</b></asp:label></td>
			</tr>--%>
			<tr><td height="10" style="width: 99px"></td></tr>
			<tr>
			    <td style="height: 29px" title="Enter Login ID" class="label">Enter Login ID&nbsp;&nbsp;&nbsp;</td>
				<td style="height: 29px"><asp:TextBox id="txtuserid" runat="server" Width="188px" ToolTip="Enter UserId to Retrieve Password" CssClass="textBox"></asp:TextBox></td>
			</tr>
			<tr>
			    <td></td>
			    <td><asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtuserid" ErrorMessage="UserId Cann't be Empty"></asp:RequiredFieldValidator></td>
			</tr>
			<tr>
			    <td></td>
			    <td><asp:Button ID="cmdSubmit" runat="server" Text="Retrieve Password" CssClass="button" ToolTip="Click To Retrieve Password" /></td>
			</tr>
	</table>
    </div>
    </form>
</body>
</html>
<%--
---------------- Change History -------------------------
none

--%>
