<%@ Page Language="VB" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="false" CodeFile="IdmsDepartment.aspx.vb" Inherits="Function_And_Process_IdmsDepartment" %>

<asp:Content id ="LeftContent" ContentPlaceHolderID="LeftPlaceHolder" runat="server">
<table dir="ltr" width="206" summary ="table">
   <tr>
     <td style="width:206px;" scope ="col" >
     </td>
   </tr>
</table>
</asp:Content>
<asp:Content ID="RightContent" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
 <link rel="stylesheet" type="text/css" href="../App_Themes/Themes/StyleSheet.css" />
<div>
    <asp:textbox id="txtSort" style="Z-INDEX: 100; LEFT: 952px; POSITION: absolute; TOP: 45px" runat="server"
				MaxLength="50" Visible="False"></asp:textbox>
				<asp:panel id="pnlConfirmDel" style="Z-INDEX: 104; LEFT: 400px; POSITION: absolute; TOP: 400px"
				runat="server" BorderColor="Gray" BorderStyle="Groove" BorderWidth="1px" BackColor="#eeeeee" Width="375px" Height="110px">
				<table id="Table1"   cellspacing="0" cellpadding="0" width="100%" summary ="Confirm Deletion panel">
					<tr>
						<td class="tableHeader"  colspan="2" align="center" scope="colgroup" style ="background-color:#0591D3">Confirm Deletion</td>
					</tr>
					<tr>
					<td colspan="2" height="12" scope="colgroup"></td>
					</tr>
					<tr>
						<td colspan="2" scope="colgroup" style ="color:black">&nbsp;Are you sure you want to delete the selected records?</td>
					</tr>
					<tr>
						<td colspan="2" scope="colgroup">&nbsp;</td>
					</tr>
					<tr>
						<td align="right" style="width:50%; height: 22px;" scope ="col">
							<asp:Button id="cmdYes" runat="server" Text="Yes" width="40"  CssClass="button"></asp:Button></td>
						<td align="left"  style="width:50%; height: 22px;" scope="col" >
							<asp:Button id="cmdNo" runat="server" Text="No" width="40" CssClass="button"></asp:Button></td>
					</tr>
					<%--<tr>
						<td  align="center" colspan="2">&nbsp;</td>
					</tr>--%>
				</table>
			</asp:panel>
			<asp:textbox id="txtsortorder" style="Z-INDEX: 103; LEFT: 952px; POSITION: absolute; TOP: 17px"
				runat="server" MaxLength="50" Visible="False">ASC</asp:textbox>
			
			<table cellspacing="0" cellpadding="0" width="90%" class="table" summary="Purpose of this page is Creating New Department">
				<caption style ="background-color:#67A897">Add Level 1</caption>
				
				<tr>
					<td colspan="2" scope="colgroup">&nbsp;</td>
				</tr>
				<tr>
					<td scope="col"><strong><asp:Label ID="lbl1" Text="Level 1" runat="server" ></asp:Label></strong>
					</td>
					<td class="textBox" style="height: 20px" scope="col">
					<input id="txtdepart" type="text" maxlength="20" class="textBox" name="txtdepart" runat="server" tabindex="1" title="Enter Department Name"/>
					<%--<asp:TextBox id="txtdepart" CssClass="textBox" runat="server" ToolTip="Enter Department Name"></asp:TextBox>--%>
					</td>
				</tr>
				<tr>
					<td class="label" style="height: 28px; color :Black " scope="col" >Message</td>
					<td style="height: 28px" scope="col"  >
					<input id="txtmsg" type="text" maxlength="50" class="textBox" name="txtmsg" runat="server" tabindex="2" title="Enter Message"/>&nbsp;
					&nbsp;
					<asp:button id="cmdSave" runat="server" Width="56px" CssClass="button" Text="Add" ToolTip="Create  Department[ALT+A]" TabIndex="3" AccessKey="a"></asp:button>&nbsp;
					<asp:button id="cmdCancel" runat="server" Width="56px" CssClass="button" Text="Reset" ToolTip="Reset Values[ALT+C]" TabIndex="4" AccessKey="c"></asp:button></td>
				</tr>
				<tr>
					<td colspan="2" scope="colgroup">&nbsp;</td>
				</tr>
				<tr>
					<td align="center" colspan="2" style="height: 15px" scope="colgroup">&nbsp;</td>
				</tr>
				<tr>
					<td valign="top" align="center" colspan="2" style="height: 452px" scope="colgroup">
						<div id="div2" style="WIDTH: 100%; color :Black " runat="server">
						<asp:datagrid id="grdlob" runat="server" Width="100%" AutoGenerateColumns="False" AllowSorting="True" 
								AllowPaging="True" PageSize="15" ShowFooter="True" AlternatingItemStyle-Wrap="False" PagerStyle-Wrap="False"
								DataKeyField="autoid" CssClass ="datagrid">
								<SelectedItemStyle Wrap="False"></SelectedItemStyle>
								<EditItemStyle Wrap="False"></EditItemStyle>
							
								<ItemStyle Wrap="False" HorizontalAlign="Left"></ItemStyle>
								<HeaderStyle 
									CssClass="datagridHeader"   ></HeaderStyle>
								<FooterStyle CssClass="datagrid"></FooterStyle>
								<Columns>
									<asp:TemplateColumn>
										<HeaderTemplate>
										<asp:LinkButton id="Linkbutton1" Runat="server" Font-Bold="true"    CommandName="DeptName"   text='Level1 Name' >
											
										</asp:LinkButton>
											<!--<B>Department Name</B>-->
										</HeaderTemplate>
										
										<ItemTemplate>
											<asp:Label ID="lblAutoId" Runat="server" text='<%#Container.DataItem("Autoid")%>' Visible="False">
											</asp:Label>
											<asp:Label id="LnkLOB" Runat="server" Text='<%#Container.DataItem("DepartmentName")%>'>
											</asp:Label>
											<asp:Label AssociatedControlID="txtLOB" runat="server" ID="lbltxtLOB"></asp:Label>
											<asp:TextBox ID="txtLOB" Runat="server" Visible="False" MaxLength="50"></asp:TextBox>
											
											<asp:ImageButton Runat="server" ID="cmdimgEdit" BorderWidth="0" ImageUrl="../Images/edit_16.gif"
												CommandName="EditSave" Visible="False" ></asp:ImageButton>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn>
										<HeaderTemplate>
											<strong>Message</strong> 
										</HeaderTemplate>
										<ItemTemplate>
											<asp:Label id="lnkmsg" Runat="server" AssociatedControlID="txtmessage"  Text='<%#Container.DataItem("Message")%>'>
											</asp:Label>
											
											<asp:TextBox ID="txtmessage" Runat="server" Visible="False" MaxLength="50"></asp:TextBox>
											<asp:ImageButton Runat="server" AlternateText="EDIT" ID="cmdimgmsg" BorderWidth="0" ImageUrl="../Images/edit_16.gif"
												CommandName="EditSavemsg" Visible="False"></asp:ImageButton>
										</ItemTemplate>
									</asp:TemplateColumn>
									<%--<asp:BoundColumn DataField="SavedBy"   HeaderText="Created By">
										<HeaderStyle CssClass="datagridHeader" ></HeaderStyle>
									</asp:BoundColumn>--%>
									<asp:TemplateColumn>
										<HeaderTemplate>
											<strong>Created By</strong>
										</HeaderTemplate>
										<ItemTemplate>
											<asp:Label id="lnkmsg1" Runat="server" Text='<%#Container.DataItem("SavedBy")%>'>
											</asp:Label>
						
										</ItemTemplate>
									</asp:TemplateColumn>
									<%--<asp:BoundColumn DataField="SaveDate" SortExpression="SaveDate"  HeaderText="Added On " >
										<HeaderStyle CssClass="datagridHeader" ></HeaderStyle>
									</asp:BoundColumn>--%>
									<asp:TemplateColumn>
										<HeaderTemplate>
											<strong>Added On</strong>
										</HeaderTemplate>
										<ItemTemplate>
											<asp:Label id="lnkmsg2" Runat="server" Text='<%#Container.DataItem("SaveDate")%>'>
											</asp:Label>
						
										</ItemTemplate>
									</asp:TemplateColumn>
									
									
									<asp:TemplateColumn>
										<HeaderStyle Wrap="False" Width="110px" CssClass="datagridHeader" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="#0033FF"></HeaderStyle>
										<ItemStyle Wrap="False"></ItemStyle>
										<HeaderTemplate>
											<center>
												<asp:LinkButton id="lkbtnSelectAll" Runat="server" CommandName="Select All" ToolTip="Select All CheckBox"  ><strong>Select All</strong></asp:LinkButton>
												<asp:LinkButton id="lkbtnDeSelectAll" Runat="server" CommandName="DeSelect All" ToolTip="DeSelect All CheckBox"  ><strong>DeSelect All</strong></asp:LinkButton>
											</center>
										</HeaderTemplate>
										<ItemTemplate>
											<center>
									            <asp:Label ID="lbchkvisible" AssociatedControlID ="chkSelect" runat ="server"  	></asp:Label>
												<asp:CheckBox ID="chkSelect" Runat="server" BorderWidth="0" ToolTip ="Click To Select"></asp:CheckBox>
											</center>
										</ItemTemplate>
										<FooterTemplate>
											<center>
												<asp:Button ID="BtnDelete" toolTip="Delete Checked Records " Runat="server"
													CommandName="Delete" Text="Delete" Font-Bold="true"  CssClass="button"></asp:Button>
											</center>
										</FooterTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn>
										<HeaderTemplate>
											<strong>Edit</strong>
										</HeaderTemplate>
										<ItemTemplate>
											<asp:LinkButton ID="lnkedit" Runat="server" text="Edit" CommandName="Edit" ToolTip="Edit Record" ></asp:LinkButton>
											<asp:LinkButton ID="lnkupdate" Runat="server" text="Update" Visible="False" ToolTip="Update Record" CommandName="EditSave" ></asp:LinkButton>
										<asp:LinkButton ID="lnkcancel" runat="server" Text="Cancel" ToolTip="Cancel Update" Visible="false" CommandName="CancelEdit"></asp:LinkButton>
										</ItemTemplate>
									</asp:TemplateColumn>
								</Columns>
								<PagerStyle Wrap="False" Mode="NumericPages" HorizontalAlign="Left"></PagerStyle>
                            <AlternatingItemStyle Wrap="False" />
							</asp:datagrid></div>
					</td>
				</tr>
				<tr>
					<td align="center" colspan="2" scope="colgroup">&nbsp;</td>
				</tr>
			</table>
		
    </div>


</asp:Content>


