<%@ Page Language="VB" AutoEventWireup="false" MasterPageFile="~/MasterPage/MasterPage.master" CodeFile="MaintainPWD.aspx.vb" Inherits="Account_MaintainPWD" Title="Maintain Password" %>

<%--Project Name: IDMS Phase 2
    Module Name: Accounts Management
    Page Name: Maintain Password
    Summary: Changes Password and Deletes the User Accounts
    Created on: 10/05/08
    Created By: Yogesh Kumar Verma

--%>
<asp:Content ID="MantainPassword" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<script language="javascript" type="text/javascript">
function confirm_delete()
{
  if (confirm("Are you sure you want to delete this item?")==true)
    return true;
  else
    return false;
}	
		</script>


         <div>
    
			<asp:textbox id="txtsort"  runat="server" Visible="False"></asp:textbox>
			<asp:textbox id="txtsortorder" runat="server" Visible="False"></asp:textbox>
			
			<table cellspacing="0" cellpadding="0" width="80%" class="table" summary="purpose of this page is, Maintaining password">
				<caption style ="background-color:#0591D3">Password Maintenance</caption>
				<%--<tr>
					<th class="tableHeader"colspan="4">
						Password Maintenance
					</th>
				</tr>--%>
				<tr>
					<td style="WIDTH: 177px; HEIGHT: 15px" scope="col"></td>
					<td style="WIDTH: 170px; HEIGHT: 15px" scope="col"></td>
					<td style="WIDTH: 141px; HEIGHT: 15px" scope="col"></td>
					<td style="HEIGHT: 15px" scope="col"></td>
				</tr>
				<tr>
					<td class="label" scope="col" style ="color:black" >User Type
					</td>
					<td scope="col"><asp:dropdownlist id="cboUserType" runat="server"  Width="75" AutoPostBack="True" CssClass="dropdownlist" ToolTip="Select User Type" TabIndex="1"></asp:dropdownlist></td>
					<td class="label" scope="col" style ="color:black" >User Id
					</td>
					<td ><asp:dropdownlist id="cboUserId" runat="server" AutoPostBack="True" CssClass="dropdownlist" ToolTip="Select UserId" TabIndex="2"></asp:dropdownlist></td>
				</tr>
				<tr>
					<td style="HEIGHT: 30px"colspan="4" scope="colgroup">
						<hr style="color:lightsteelblue" />
					</td>
				</tr>
				<tr id="trstatus" runat="server">
					<td colspan="4" scope="colgroup">
						<table cellspacing="0" cellpadding="0" width="100%">
							<tr >
								<td colspan="2" scope="colgroup" class="label" style="height: 20px; color : Black ">Account&nbsp;Status<img id="imglock" src="../Images/lockedfile.gif"  runat="server" alt="locked file image"/></td>
							</tr>
							<tr>
								<td scope="col" style ="color:black" ><asp:radiobuttonlist id="rdoLock" runat="server" RepeatDirection="Horizontal" ToolTip="Make  Lock/Unlock" TabIndex="3">
										<asp:ListItem Value="Locked">Locked</asp:ListItem>
										<asp:ListItem Value="Unlocked">Unlocked</asp:ListItem>
									</asp:radiobuttonlist></td>
								<td scope="col"><asp:button id="cmdSave" runat="server" CssClass="button" Text="Save" ToolTip="Save" TabIndex="5"></asp:button></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td colspan="4" scope="colgroup">&nbsp;</td>
				</tr>
				<tr id="trgrid" runat="server">
					<td colspan="4" scope="colgroup">
					<asp:datagrid id="grdUsers" runat="server" ForeColor="Black" Width="90%" DataKeyField="autoid"
							HeaderStyle-ForeColor="white" ItemStyle-Wrap="False" PagerStyle-Wrap="False" AlternatingItemStyle-Wrap="False"
							ShowFooter="True" PageSize="15" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" CssClass="datagrid">
							<SelectedItemStyle Wrap="False" ForeColor="Maroon"></SelectedItemStyle>
							<EditItemStyle Wrap="False"></EditItemStyle>
							<AlternatingItemStyle ></AlternatingItemStyle>
							<ItemStyle ></ItemStyle>
							<HeaderStyle CssClass="datagridHeader" ></HeaderStyle>
							<FooterStyle CssClass="datagridHeader"></FooterStyle>
							<Columns>
								<asp:BoundColumn DataField="Userid" SortExpression="Userid"  HeaderStyle-Font-Underline="true"  HeaderText="User Id">
									<HeaderStyle CssClass="Head"></HeaderStyle>
								</asp:BoundColumn>
								<asp:TemplateColumn>
									<HeaderTemplate>
										<strong>Click here to Change Password</strong>
									</HeaderTemplate>
									<ItemTemplate>
										<asp:LinkButton ID="lblusrId" Runat="server" text='Change Password'  CommandName="Edit" Visible="true"></asp:LinkButton>
										<asp:LinkButton id="LnkPwd" Runat="server" Font-Name="Password" ></asp:LinkButton>
										<asp:TextBox ID="txtPWD" TextMode="Password" Runat="server" Visible="False" MaxLength="15"></asp:TextBox>
										<asp:ImageButton Runat="server" ID="cmdimgEdit" BorderWidth="0" ImageUrl="../Images/edit_16.gif"
											CommandName="EditSave" Visible="False" AlternateText="Save Password"></asp:ImageButton>
										<asp:ImageButton Runat="server" ID="cancel" BorderWidth="0" ImageUrl="../Images/undo.gif" CommandName="Cancel"
											Visible="False" AlternateText="Back"></asp:ImageButton>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:BoundColumn DataField="Updatedate" SortExpression="Updatedate" HeaderText="Updated On">
									<HeaderStyle CssClass="Head"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="updatedby" SortExpression="updatedby" HeaderText="Updated By">
									<HeaderStyle CssClass="Head"></HeaderStyle>
								</asp:BoundColumn>
								<asp:TemplateColumn>
									<HeaderStyle Wrap="False" Width="0px" CssClass="Head"></HeaderStyle>
									<ItemStyle Wrap="False"></ItemStyle>
									<HeaderTemplate>
										<center>
											<asp:LinkButton id="lkbtnSelectAll" Runat="server" CommandName="Select All" >Select All/</asp:LinkButton>
											<asp:LinkButton id="lkbtnDeSelectAll" Runat="server" CommandName="DeSelect All" >DeSelect All</asp:LinkButton>
										</center>
									</HeaderTemplate>
									<ItemTemplate>
										<center>
											<asp:CheckBox ID="chkSelect" Runat="server" BorderWidth="0"></asp:CheckBox>
										</center>
									</ItemTemplate>
									<FooterTemplate>
										<center>
											<asp:Button ID="BtnDelete" Runat="server" CommandName="Delete" Text="Delete" ToolTip="Delete Record" CssClass="button"></asp:Button>
										</center>
									</FooterTemplate>
								</asp:TemplateColumn>
							</Columns>
							<PagerStyle Wrap="False" Mode="NumericPages"></PagerStyle>
						</asp:datagrid>
						<hr color="lightsteelblue"/>
					</td>
				</tr>
				<tr>
					<td colspan="4" scope="colgroup"></td>
				</tr>
				<tr>
					<td colspan="4" scope="colgroup">
						<table cellspacing="0" cellpadding="0" width="100%" >
							<tr>
								<td class="label" scope="col" style ="color:black" >Set Password Duration(Days)
								</td>
								<td valign="bottom" scope="col"><asp:textbox id="txtDuration" runat="server" MaxLength="5" CssClass="textBox" ToolTip="Set Password Duration" TabIndex="5"></asp:textbox><asp:imagebutton id="imgSaveDuration" runat="server" AlternateText="Save Password" ImageAlign="AbsBottom" BorderWidth="0px" ImageUrl="../Images/Save.jpg" ToolTip="Save Duration" TabIndex="6"></asp:imagebutton>&nbsp;
									<asp:label id="lblLastUpdate" runat="server" ForeColor="Navy" CssClass ="label">Label</asp:label></td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
		
    </div>

</asp:Content>


<%--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    
    <script language="javascript" type="text/javascript">
function confirm_delete()
{
  if (confirm("Are you sure you want to delete this item?")==true)
    return true;
  else
    return false;
}	
		</script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
			<asp:textbox id="txtsort" style="Z-INDEX: 100; LEFT: -40px; POSITION: absolute; TOP: 56px" runat="server"
				Visible="False"></asp:textbox><asp:textbox id="txtsortorder" style="Z-INDEX: 101; LEFT: -40px; POSITION: absolute; TOP: 80px"
				runat="server" Visible="False"></asp:textbox>
			<%--<uc1:Menu id="Menu1" runat="server"></uc1:Menu>
			<table cellspacing="0" cellpadding="0" width="80%" class="tableposition">
				<tr>
					<th class="tablehead"colspan="4">
						Password Maintenance
					</th>
				</tr>
				<tr>
					<td style="WIDTH: 177px; HEIGHT: 15px"></td>
					<td style="WIDTH: 170px; HEIGHT: 15px"></td>
					<td style="WIDTH: 141px; HEIGHT: 15px"></td>
					<td style="HEIGHT: 15px"></td>
				</tr>
				<tr>
					<td style="WIDTH: 177px; HEIGHT: 15px">User Type
					</td>
					<td style="WIDTH: 170px; HEIGHT: 15px"><asp:dropdownlist id="cboUserType" runat="server" AutoPostBack="True"></asp:dropdownlist></td>
					<td style="WIDTH: 141px; HEIGHT: 15px">User Id
					</td>
					<td style="HEIGHT: 15px"><asp:dropdownlist id="cboUserId" runat="server" AutoPostBack="True"></asp:dropdownlist></td>
				</tr>
				<tr>
					<td style="HEIGHT: 30px"colspan="4">
						<hr color="lightsteelblue" />
					</td>
				</tr>
				<tr id="trstatus" runat="server">
					<td colspan="4">
						<table cellspacing="0" cellpadding="0" width="100%">
							<tr bgColor="whitesmoke">
								<td colspan="2"><strong>Account&nbsp;Status</strong><img id="imglock" src="../Images/lockedfile.gif" align="absMiddle" runat="server" alt="locked file image"></td>
							</tr>
							<tr>
								<td style="WIDTH: 157px"><asp:radiobuttonlist id="rdoLock" runat="server" RepeatDirection="Horizontal">
										<asp:ListItem Value="Locked">Locked</asp:ListItem>
										<asp:ListItem Value="Unlocked">Unlocked</asp:ListItem>
									</asp:radiobuttonlist></td>
								<td><asp:button id="cmdSave" runat="server" CssClass="butn" Text="Save"></asp:button></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td colspan="4">&nbsp;</td>
				</tr>
				<tr id="trgrid" runat="server">
					<td colspan="4"><asp:datagrid id="grdUsers" runat="server" ForeColor="Black" Width="100%" DataKeyField="autoid"
							HeaderStyle-ForeColor="white" ItemStyle-Wrap="False" PagerStyle-Wrap="False" AlternatingItemStyle-Wrap="False"
							ShowFooter="True" PageSize="15" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False">
							<SelectedItemStyle Wrap="False" ForeColor="Maroon"></SelectedItemStyle>
							<EditItemStyle Wrap="False"></EditItemStyle>
							<AlternatingItemStyle Wrap="False" ForeColor="Black" BackColor="WhiteSmoke"></AlternatingItemStyle>
							<ItemStyle Font-Names="Verdana" Wrap="False" BackColor="GhostWhite"></ItemStyle>
							<HeaderStyle Font-Size="Medium" Font-Names="Verdana" Font-Bold="True" Wrap="False" ForeColor="Black"
								CssClass="Head" BackColor="LightGray"></HeaderStyle>
							<FooterStyle ForeColor="Black" BackColor="White"></FooterStyle>
							<Columns>
								<asp:BoundColumn DataField="Userid" SortExpression="Userid" HeaderText="User Id">
									<HeaderStyle CssClass="Head"></HeaderStyle>
								</asp:BoundColumn>
								<asp:TemplateColumn>
									<HeaderTemplate>
										<strong>Click here to Change Password</strong>
									</HeaderTemplate>
									<ItemTemplate>
										<asp:LinkButton ID="lblusrId" Runat="server" text='Change Password' CommandName="Edit" Visible="true"></asp:LinkButton>
										<asp:LinkButton id="LnkPwd" Runat="server" Font-Name="Password"></asp:LinkButton>
										<asp:TextBox ID="txtPWD" TextMode="Password" Runat="server" Visible="False" MaxLength="15"></asp:TextBox>
										<asp:ImageButton Runat="server" ID="cmdimgEdit" BorderWidth="0" ImageUrl="/IDMS/Images/edit_16.gif"
											CommandName="EditSave" Visible="False" AlternateText="Save Password"></asp:ImageButton>
										<asp:ImageButton Runat="server" ID="cancel" BorderWidth="0" ImageUrl="/IDMS/Images/undo.gif" CommandName="Cancel"
											Visible="False" AlternateText="Back"></asp:ImageButton>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:BoundColumn DataField="Updatedate" SortExpression="Updatedate" HeaderText="Updated On">
									<HeaderStyle CssClass="Head"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="updatedby" SortExpression="updatedby" HeaderText="Updated By">
									<HeaderStyle CssClass="Head"></HeaderStyle>
								</asp:BoundColumn>
								<asp:TemplateColumn>
									<HeaderStyle Wrap="False" Width="0px" CssClass="Head"></HeaderStyle>
									<ItemStyle Wrap="False"></ItemStyle>
									<HeaderTemplate>
										<center>
											<asp:LinkButton id="lkbtnSelectAll" Runat="server" CommandName="Select All">Select All/</asp:LinkButton>
											<asp:LinkButton id="lkbtnDeSelectAll" Runat="server" CommandName="DeSelect All">DeSelect All</asp:LinkButton>
										</center>
									</HeaderTemplate>
									<ItemTemplate>
										<center>
											<asp:CheckBox ID="chkSelect" Runat="server" BorderWidth="0"></asp:CheckBox>
										</center>
									</ItemTemplate>
									<FooterTemplate>
										<center>
											<asp:Button ID="BtnDelete" Runat="server" CommandName="Delete" Text="Delete" CssClass="butn"></asp:Button>
										</center>
									</FooterTemplate>
								</asp:TemplateColumn>
							</Columns>
							<PagerStyle Wrap="False" Mode="NumericPages"></PagerStyle>
						</asp:datagrid>
						<hr color="lightsteelblue"/>
					</td>
				</tr>
				<tr>
					<td colspan="4"></td>
				</tr>
				<tr>
					<td colspan="4">
						<table cellspacing="0" cellpadding="0" width="100%">
							<tr>
								<td style="WIDTH: 182px">Set Password Duration(Days)
								</td>
								<td valign="bottom" ><asp:textbox id="txtDuration" runat="server" MaxLength="5"></asp:textbox><asp:imagebutton id="imgSaveDuration" runat="server" ImageAlign="AbsBottom" BorderWidth="0px" ImageUrl="/IDMS/Images/Save.jpg"></asp:imagebutton>&nbsp;
									<asp:label id="lblLastUpdate" runat="server" ForeColor="Navy">Label</asp:label></td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
		
    </div>
    </form>
</body>
</html>
--%>