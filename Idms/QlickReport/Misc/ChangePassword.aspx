<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="false" CodeFile="ChangePassword.aspx.vb" Inherits="Misc_ChangePassword" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div id="passworddiv" runat="server" style="padding:20px">
<%--<asp:textbox id="txtUserName" runat="server" Visible="False"></asp:textbox>--%>
<TABLE id="tabAddMembers" cellpadding="0" cellspacing="0" align="center" runat="server" width="650" border=1 style="background:#cccccc; border-color:Black; border-bottom-width:thin;">
    <tr><td colspan="3"></td></tr>
	<TR style="height:20px">
		<th colspan="3" align="center" style="background:#67A897;font-family:Verdana;color:#ffffff;font-size:12px;font-weight:bold;letter-spacing:0.01em;">
			 <b>CHANGE PASSWORD</b></th>
	</TR>
    <tr>
        <td>
              <TABLE id="TABLE1" cellpadding="0" cellspacing="0" align="center" runat="server" width="550" border=0>
				
				<tr>
					<td colSpan="3" style="height:15px">
                    <asp:Label runat="server" ID="lblmsg" Visible="false" ForeColor="Red"></asp:Label>
                    </td>
				</tr>
                <tr>
                <td colspan="3"><br /></td>
                </tr>
				<TR>
					<TD style="width: 217px">Enter Your Old Password</TD>
					<TD colspan="2"><asp:textbox  id="txtoldPassword" tabIndex="1" MaxLength="15" TextMode="Password"  ToolTip="Enter Old Password" Runat="server" BorderColor="black" BorderWidth="1" ></asp:textbox>
                    </TD>
				</TR>
				<tr>
					<td colSpan="3" style="height:13px"></td>
				</tr>
				<TR>
					<TD style="width: 217px; height: 22px;">Enter Your New Password</TD>
					<TD style="height: 22px"><asp:textbox id="txtnewPassword" tabIndex="2" MaxLength="15" TextMode="Password" ToolTip="Enter New Password" Runat="server"  BorderColor="black" BorderWidth="1" CausesValidation="True"></asp:textbox>
                    </td>
				</tr>
				<tr>
				    <TD style="width: 217px"></TD>
					<td valign=top><asp:RegularExpressionValidator ID="RegularExpressionValidator1" 
                            ControlToValidate="txtnewPassword" 
                            ValidationExpression="^.*(?=.{8,})(?=.*\d)(?=.*[a-zA-Z])(?=.*[0-9])(?=.*[@#$%^&+=]).*$" 
                            runat="server" 
                            ErrorMessage="Enter 8-15 Characters in Length (Mix Of Alphabetic, Non Alphabetic & Special Characters)" 
                            Font-Size="Small"></asp:RegularExpressionValidator>
                    </TD>
				</TR>
				<TR>
					<TD style="width: 217px">Re-Enter Your New Password</TD>
					<TD colspan="2"><asp:textbox id="txtreEnterPassword" tabIndex="3" MaxLength="15" TextMode="Password" ToolTip="Re-Enter New Password" Runat="server"  BorderColor="black" BorderWidth="1"></asp:textbox></TD>
				</TR>
				<tr>
					<TD style="width: 217px"></TD>
                    <td valign=top>
                        &nbsp;</td>
				</tr>
				<TR>
					<TD align="center" colSpan="3" style="height: 29px">
                        <br>
						<asp:button id="cmdsave" tabIndex="4" Height="22px" Runat="server" CssClass="button" ToolTip="Click to Change password" Text="Change Password"></asp:button></TD>
				</TR>
			</TABLE>
		</td>
	</tr>
</table>
</div>
</asp:Content>

