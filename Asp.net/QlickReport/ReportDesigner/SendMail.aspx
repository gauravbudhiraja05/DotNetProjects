<%--Project Name: IDMS Phase 2
    Module Name: Advance Report Designer
    Page Name: SendMail
    Summary: Used to email a report.
    Created on: 10/03/08
    Created By: Usha Sehokand

--%>
<%@ Page Language="VB" AutoEventWireup="false" CodeFile="SendMail.aspx.vb" Inherits="ReportDesigner_SendMail" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" lang="en">
<head runat="server">
    <title>Send Mail</title>
    <link href="../App_Themes/Themes/StyleSheet.css" rel="stylesheet" />
    <script language="javascript">
        function onload()
        {
           // document.getElementById("txtemaildata").value=window.opener.document.getElementById("hidData").value;
           
        }
    </script>
</head>
	<body>
		<form id="frmSendmail" runat="server">
			<table>
			    <caption style ="background-color:#0591D3">Email Report</caption>
				<tr>
					<td scope="col" title="EmailID" style="width: 124px; color : Black ">
					    <label class="label" title="EmailID" for="txtemailid">EmailID</label>
					</td>
					<td  scope="col" title="Enter EmailID" valign="top"><asp:TextBox id="txtemailid" CssClass="textBox" ToolTip="Enter EmailID" runat="server" Width="288px" tabIndex="1"></asp:TextBox><span
                            style="font-size: 10pt; color: #ff3333">*</span></td>
				</tr>
				<tr>
					<td scope="col" title="Subject" style="width: 124px; color : Black ">
					    <label class="label" title="Subject" for="txtsubject">Subject</label>
					</td>
					<td  scope="col" title="Enter Subject" valign="top"><asp:TextBox id="txtsubject" CssClass="textBox" ToolTip="Enter Subject" runat="server" Width="288px" tabIndex="2"></asp:TextBox><span
                            style="font-size: 10pt; color: #ff3333">*</span></td>
				</tr>
				<tr>
					<td valign="top" title="Your Comments" scope="col" style="width: 124px; color : Black ">
					    <label class="label" title="Comments" for="txtcomments">Your Comments</label>
					</td>
					<td  title="Your Comments" scope="col">
						<asp:TextBox id="txtcomments" ToolTip="Leave Comments" CssClass="textBox" runat="server" Width="384px" Height="88px" tabIndex="3" TextMode="MultiLine"></asp:TextBox>
						
					</td>
				</tr>
				<tr>
					<td colspan="2" align="center" scope="colgroup" title="Send Report"><asp:Button  CssClass="button" id="Button1" runat="server" ToolTip="Send Report" Text="Send Report" tabIndex="4"></asp:Button></td>
				</tr>
			</table>
			<input id="txtemaildata" type="text" runat="server" Visible="false"/><!-- stores mail data -->
		</form>
	</body>
</html>
 <%--
---------------- Change History -------------------------
   None
--%>