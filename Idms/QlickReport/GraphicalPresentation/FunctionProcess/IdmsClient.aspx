<%@ Page Language="VB" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="false" CodeFile="IdmsClient.aspx.vb" Inherits="Function_And_Process_IdmsClient" %>

<asp:Content ID="LeftContent" ContentPlaceHolderID="LeftPlaceHolder" runat="server">


<table dir="ltr" width="206" summary ="table">
   <tr>
     <td style="width:206px;" scope="col" >
     </td>
   </tr>
</table>
</asp:Content>
<asp:Content ID="Rightcontent" ContentPlaceHolderID="MainPlaceHolder" runat="server">

    <div>
    <asp:textbox id="txtSort" style="Z-INDEX: 100; LEFT: 952px; POSITION: absolute; TOP: 45px" runat="server"
				MaxLength="50" Visible="False"></asp:textbox>
				
				<asp:panel id="pnlConfirmDel" style="Z-INDEX: 104; LEFT: 400px; POSITION: absolute; TOP: 425px"
				runat="server" BorderColor="Gray" BorderStyle="Groove" BorderWidth="2px"  Width="380px" BackColor="#eeeeee" Height="110px">
				<table id="Table1"   style="height: 100% "  cellspacing="0" cellpadding="0" width="100%" summary ="Confirm Deletion Panel">
					<tr>
						<td class="tableHeader" colspan="2" scope="colgroup"  align="center" style ="background-color:#0591D3"><strong>Confirm Deletion</strong></td>
					</tr>
					<tr>
					<td colspan="2" scope="colgroup" style="height: 12px"></td>
					</tr>
					<tr>
						<td colspan="2" scope="colgroup" style ="color:black">&nbsp;Are you sure you want to delete the selected records?</td>
					</tr>
					<tr>
						<td colspan="2" scope="colgroup">&nbsp;</td>
					</tr>
					<tr>
						<td align="right" style="width:50%" scope="col" >
							<asp:Button id="cmdYes" runat="server" Text="Yes" Width="40" CssClass="button"></asp:Button></td>
						<td align="left" style="width:50%" scope="col" >
							<asp:Button id="cmdNo" runat="server" Text="No" Width="40" CssClass="button"></asp:Button></td>
					</tr>
					<tr>
						<td align="center" colspan="2" scope="colgroup">&nbsp;</td>
					</tr>
				</table>
			</asp:panel><asp:textbox id="txtsortorder" style="Z-INDEX: 103; LEFT: 952px; POSITION: absolute; TOP: 17px"
				runat="server" MaxLength="50" Visible="False">ASC</asp:textbox>
			<table cellspacing="0" cellpadding="0" width="90%" class="table" summary="Purpose of this page is creating Client for any specific department">
				<caption style ="background-color:#0591D3">Add Client</caption>
				
			
				<tr>
					<td colspan="2" scope="colgroup">&nbsp;</td>
				</tr>
				<tr>
					<td  class="label" style="height: 22px; color :Black " scope="col"  ><label for="ctl00_MainPlaceHolder_department">Department Name</label></td>
					<td style="height: 22px" scope ="col" ><select id="department"  name="department" runat="server" class="dropdownlist" title="Select Department" tabindex="1"></select>
					</td>
				</tr>
				<tr>
					<td  class="label" scope ="col" style ="color:black"  ><label for="ctl00_MainPlaceHolder_txtclient">Client Name</label></td>
					<td scope ="col" ><input id="txtclient" type="text" maxlength="50" class="textBox"   name="txtdepart" runat="server" title="Enter Client Name" tabindex="2" />
					</td>
				</tr>
				<tr>
					<td  class="label" scope ="col" style ="color:black" ><label for="ctl00_MainPlaceHolder_txtmsg">Message</label></td>
					<td scope ="col" ><input id="txtmsg" type="text" maxlength="50"  class="textBox" name="txtmsg" runat="server" title="Enter Message" tabindex="3" />&nbsp;
						<asp:button id="cmdSave" runat="server" Width="56" CssClass="button" Text="Add" ToolTip="Create Client [ALT+A]" TabIndex="4" AccessKey="a"></asp:button>&nbsp;<asp:button id="cmdCancel" Width="56"  runat="server" CssClass="button" Text="Reset" ToolTip=" Reset Value [ALT+C]" TabIndex="5" AccessKey="c"></asp:button>
					</td>
				</tr>
				<tr>
					<td colspan="2" scope="colgroup">&nbsp;</td>
				</tr>
				<tr>
					<td valign="top" align="center" colspan="2" scope="colgroup">
						<div id="div2" style="WIDTH: 100%; color :Black " runat="server" >
							<asp:datagrid id="grdlob" runat="server" Width="100%" CssClass="datagrid" DataKeyField="autoid"
								HeaderStyle-ForeColor="white" ItemStyle-Wrap="False" PagerStyle-Wrap="False" AlternatingItemStyle-Wrap="False"
								ShowFooter="True" PageSize="15" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False">
								<SelectedItemStyle Wrap="False" ></SelectedItemStyle>
								<EditItemStyle Wrap="False"></EditItemStyle>
								<AlternatingItemStyle Wrap="False" ></AlternatingItemStyle>
								<ItemStyle Font-Names="Verdana" Wrap="False" HorizontalAlign="Left"></ItemStyle>
								<HeaderStyle  Wrap="False" CssClass="datagridHeader" ForeColor ="black" ></HeaderStyle>
								<FooterStyle  CssClass="datagridHeader"></FooterStyle>
								<Columns>
									<asp:BoundColumn DataField="departmentname" SortExpression="departmentname" HeaderStyle-Font-Bold="true"  ReadOnly="true" HeaderText="Department Name">
									</asp:BoundColumn>
									<asp:TemplateColumn>
										<HeaderTemplate>
											<%--<strong>Client Name</strong>--%>
											<asp:LinkButton id="Linkbutton1" Runat="server"    CommandName="ClientName"    >
											<strong >Client Name</strong>
										</asp:LinkButton>
										</HeaderTemplate>
										<ItemTemplate>
											<asp:Label ID="lblAutoId" Runat="server" text='<%#Container.DataItem("Autoid")%>' Visible="False">
											</asp:Label>
											<asp:Label id="LnkLOB" Runat="server" Text='<%#Container.DataItem("Clientname")%>' >
											</asp:Label>
											<asp:TextBox ID="txtLOB" Runat="server" Visible="False" MaxLength="50"></asp:TextBox>
											<asp:ImageButton Runat="server" ID="cmdimgEdit" BorderWidth="0"  ImageUrl="../Images/edit_16.gif"
												CommandName="EditSave" Visible="False"></asp:ImageButton>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn>
										<HeaderTemplate>
											<strong>Message</strong>
										</HeaderTemplate>
										<ItemTemplate>
											<asp:Label id="lnkmsg" Runat="server" Text='<%#Container.DataItem("Message")%>'>
											</asp:Label>
											<asp:TextBox ID="txtmessage" Runat="server" Visible="False" MaxLength="50"></asp:TextBox>
											<asp:ImageButton Runat="server" ID="cmdimgmsg" BorderWidth="0" ImageUrl="../Images/edit_16.gif"
												CommandName="EditSavemsg" Visible="False"></asp:ImageButton>
										</ItemTemplate>
										
									</asp:TemplateColumn>
									<%--<asp:BoundColumn DataField="SaveDate" SortExpression="SaveDate" ReadOnly="true" HeaderStyle-Font-Bold="true" HeaderText="Added On ">
										<HeaderStyle CssClass="datagridHeader"></HeaderStyle>
									</asp:BoundColumn>--%>
									<asp:TemplateColumn>
										<HeaderTemplate>
											<strong>Added On</strong>
										</HeaderTemplate>
										<ItemTemplate>
											<asp:Label id="lnkmsg1" Runat="server" Text='<%#Container.DataItem("SaveDate")%>'>
											</asp:Label>
											
										</ItemTemplate>
										
									</asp:TemplateColumn>
									
									<%--<asp:BoundColumn DataField="SavedBy" SortExpression="SavedBy" ReadOnly="true" HeaderStyle-Font-Bold="true" HeaderText="Created By">
										<HeaderStyle CssClass="datagridHeader"></HeaderStyle>
									</asp:BoundColumn>--%>
									<asp:TemplateColumn>
										<HeaderTemplate>
											<strong>Created By</strong>
										</HeaderTemplate>
										<ItemTemplate>
											<asp:Label id="lnkmsg2" Runat="server" Text='<%#Container.DataItem("SavedBy")%>'>
											</asp:Label>
											
										</ItemTemplate>
										
									</asp:TemplateColumn>
									<asp:TemplateColumn>
										<HeaderStyle Wrap="False" Width="110px" CssClass="Head" Font-Bold="False" Font-Italic="False"  Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="#0000FF"></HeaderStyle>
										<ItemStyle Wrap="False"></ItemStyle>
										<HeaderTemplate>
											<center>
												<asp:LinkButton id="lkbtnSelectAll" Runat="server" CommandName="Select All"  ToolTip="Select All CheckBox"  ><strong>Select All/</strong></asp:LinkButton>
												<asp:LinkButton id="lkbtnDeSelectAll" Runat="server" CommandName="DeSelect All" ToolTip="DeSelect All CheckBox"  ><strong>DeSelect All</strong></asp:LinkButton>
											</center>
										</HeaderTemplate>
										<ItemTemplate>
											<center>
														<asp:Label ID="lbchkvisible" AssociatedControlID ="chkSelect" runat ="server"  	></asp:Label>
												<asp:CheckBox ID="chkSelect" Runat="server" ToolTip ="Click To Select" BorderWidth="0"></asp:CheckBox>
											</center>
										</ItemTemplate>
										<FooterTemplate>
											<center>
												<asp:Button ID="BtnDelete" toolTip="Delete Checked Records " Runat="server"
													CommandName="Delete" Text="Delete" CssClass="button"></asp:Button>
											</center>
										</FooterTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn>
										<HeaderTemplate>
										Edit	
										
										</HeaderTemplate>
										<ItemTemplate>
										<asp:LinkButton ID="lnkedit" Runat="server" text="Edit" ToolTip="Edit Record" CommandName="Edit" ></asp:LinkButton>
										<asp:LinkButton ID="lnkupdate" Runat="server" text="Update" ToolTip="Update Record" Visible="False" CommandName="EditSave" ></asp:LinkButton>
										<asp:LinkButton ID="lnkcancel" runat="server" Text="Cancel" ToolTip="Cancel Update" Visible="false" CommandName="CancelEdit"></asp:LinkButton>
		
											</ItemTemplate>
									
									</asp:TemplateColumn>
								</Columns>
								<PagerStyle Wrap="False" Mode="NumericPages" HorizontalAlign="Left"></PagerStyle>
							</asp:datagrid></div>
					</td>
				</tr>
				<tr>
					<td align="center" colspan="2" scope="colgroup">&nbsp;</td>
				</tr>
			</table>
		
    </div>
    <%--</form>--%>
</asp:Content>
