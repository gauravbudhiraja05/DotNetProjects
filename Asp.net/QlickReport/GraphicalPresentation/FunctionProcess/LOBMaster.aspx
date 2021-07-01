<%@ Page Language="VB" AutoEventWireup="false" MasterPageFile="~/MasterPage/MasterPage.master" CodeFile="LOBMaster.aspx.vb" Inherits="Function_And_Process_LOBMaster" %>
<%--<%@ Register TagPrefix="uc1" TagName="Menu" Src="~/NewMenu/menu.ascx" %>--%>

<asp:Content ID="LobMaster" ContentPlaceHolderID="MainPlaceHolder" runat="server">

<script language="javascript" type="text/javascript">
function confirm_delete()
{
var strAns=confirm("Are you sure you want to delete this item?")
  if (strAns==true)
    return true;
  else
    return false;
}	

function getclientid()
			{
			
			
			if (document.getElementById("<%=DepartmentName.ClientID%>").selectedIndex != 0)
			{
				
				AjaxSearchBind.bindClientOnDept(document.getElementById("<%=DepartmentName.ClientID%>").value,fillddlclient);
				
			}
		
			
			}
			function fillddlclient(Response)
			{
				for(i=document.getElementById("<%=ddlClient.ClientID%>").length;i>=0;i--)
				{
					document.getElementById("<%=ddlClient.ClientID%>").remove(i);
				}
				
				var ds = Response.value
				
				if(ds!= null && typeof(ds) == "object" && ds.Tables!=null)
				{
				   	document.getElementById("<%=ddlClient.ClientID%>").options[0]=new Option("--Select--",0);
				    for(i=0;i<ds.Tables[0].Rows.length;i++)
				    {
					    document.getElementById("<%=ddlClient.ClientID%>").options[i+1]=new Option(ds.Tables[0].Rows[i].ClientName,ds.Tables[0].Rows[i].AutoId);
				    }
			    }
			   
			}
			
		function	setClientName()
			{
			//document.getElementById("<%=hfClientName.ClientID%>").value=document.getElementById("<%=ddlClient.ClientID%>").item(document.getElementById("<%=ddlClient.ClientID%>").selectedIndex).text ;
			document.getElementById("<%=hfClientName.ClientID%>").value =document.getElementById("<%=ddlClient.ClientID%>").value
			}

		</script>
		
		<asp:textbox id="txtSort" style="Z-INDEX: 100; LEFT: 952px; POSITION: absolute; TOP: 45px" runat="server"
				Visible="False" MaxLength="50"></asp:textbox>
				
			
				
				<asp:panel id="pnlConfirmDel" style="Z-INDEX: 104; LEFT: 450px; POSITION: absolute; TOP: 480px"
				runat="server" BorderColor="Gray" BorderStyle="Groove" Height="110px" BorderWidth="2px" BackColor="#eeeeee" Width="380px">
				<table id="Table1" style="height:100%" cellspacing="0" cellpadding="0" width="100%" summary ="confirm deletion panel">
					<tr>
						<td class="tableHeader"  colspan="2" align="center" scope ="colgroup" style ="background-color:#0591D3" ><strong>Confirm Deletion</strong></td>
					</tr>
					<tr>
					<td colspan="2" height="12" scope ="colgroup"></td>
					</tr>
					<tr>
						<td colspan="2" scope ="colgroup" style ="color:black">Are you sure you want to delete the selected records?</td>
					</tr>
					<tr>
						<td colspan="2" scope ="colgroup">&nbsp;</td>
					</tr>
					<tr>
						<td align="right" style="width:50%" scope="col"  >
							<asp:Button id="cmdYes" Width="40" runat="server" Text="Yes" CssClass="button"></asp:Button></td>
						<td align="left" style="width:50%" scope="col" >
							<asp:Button id="cmdNo" runat="server" Width="40" Text="No" CssClass="button"></asp:Button></td>
					</tr>
					<tr>
						<td  align="center" colspan="2" scope ="colgroup">&nbsp;</td>
					</tr>
				</table>
			</asp:panel><asp:textbox id="txtsortorder" style="Z-INDEX: 103; LEFT: 952px; POSITION: absolute; TOP: 17px"
				runat="server" Visible="False" MaxLength="50">ASC</asp:textbox>
			<table cellspacing="0" cellpadding="0" width="90%"  class="table" summary="purpose of this page is creating  a LOB">
				<caption style ="background-color:#0591D3">LOB Master</caption>
				<tr>
					<td height="12" scope="col" ></td>
				</tr>
				<tr>
					<td class="label" scope ="col" style ="color:black">
					
					
					<label for="ctl00_MainPlaceHolder_DepartmentName">Department Name</label></td>
					<td scope ="col"  >
					
					
					<select id="DepartmentName" onchange="javascript:getclientid();" name="department" runat="server" class="dropdownlist" title="Select Department" tabindex="1"></select>
					</td>
				</tr>
				<tr>
					<td class="label" scope ="col" style ="color:black" >
					
					<label for="ctl00_MainPlaceHolder_ddlClient">Client Name</label></td>
					<td scope ="col"  >
					<%--<select id="Clientname" name="department" runat="server" class="dropdownlist" title="Select Client" tabindex="2"></select>--%>
                        <asp:DropDownList ID="ddlClient" runat="server"  TabIndex="2" ToolTip="Select Client" CssClass="dropdownlist"> </asp:DropDownList>
                        </td>
				</tr>
				<tr>
					<td class="label" scope ="col" style ="color:black" ><label for="ctl00_MainPlaceHolder_txtLob">LOB Name</label></td>
					<td scope="col" ><asp:textbox id="txtLob"  MaxLength="20" runat="server" CssClass="textBox"  ToolTip="Enter Lob Name" TabIndex="3"></asp:textbox>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtLob"
                            Display="Dynamic" ErrorMessage="LOB Name Must Be Alphabetic" ValidationExpression="[a-z A-Z _]*"></asp:RegularExpressionValidator></td>
				</tr>
				<tr>
					<td class="label" style="height: 28px; color :Black" scope ="col" ><label for="ctl00_MainPlaceHolder_txtDescript">Message</label></td>
					<td style="height: 28px" scope ="col" ><asp:textbox id="txtDescript" MaxLength="50"  runat="server" CssClass="textBox" ToolTip="Enter Message" TabIndex="4"></asp:textbox></td>
				</tr>
				<tr>
					<td colspan="2" scope ="colgroup">
					</td>
				</tr>
				<tr>
					<td align="center" colspan="2" scope ="colgroup" style="height: 22px"><asp:button id="cmdSave" runat="server" Width="56" OnClientClick="setClientName();" CssClass="button" Text="Save" ToolTip="Create LOB [ALT+V]" AccessKey="v" TabIndex="5"></asp:button>&nbsp;
						<asp:button id="cmdCancel" runat="server" CssClass="button" Text="Reset" Width="56" ToolTip=" Reset Values [ALT+C]" TabIndex="6" AccessKey="c"></asp:button></td>
				</tr>
				<tr>
					<td height="9" colspan="2" scope ="colgroup"></td>
				</tr>
				<tr>
					<td valign="top" align="center" colspan="2" scope ="colgroup">
						<div id="div2" style="color :Black ;overflow:scroll;width:720px;" runat="server">
							<asp:datagrid id="grdlob" runat="server" AutoGenerateColumns="False" AllowSorting="True"
								AllowPaging="True" PageSize="15" ShowFooter="True" AlternatingItemStyle-Wrap="False" PagerStyle-Wrap="False"
								ItemStyle-Wrap="False"  DataKeyField="autoid" CssClass="datagrid" Font-Bold="False">
								<SelectedItemStyle Wrap="False"></SelectedItemStyle>
								<EditItemStyle Wrap="False" ></EditItemStyle>
								<AlternatingItemStyle Wrap="False" ></AlternatingItemStyle>
								<ItemStyle Font-Names="Verdana" Wrap="false" HorizontalAlign="Left" ></ItemStyle>
								<HeaderStyle  
									CssClass="datagridHeader"></HeaderStyle>
								<FooterStyle CssClass="datagridHeader"></FooterStyle>
								<Columns>
								
                                    <%--<asp:TemplateColumn SortExpression="departmentname"   HeaderText="Department Name"  >
                                        <HeaderStyle CssClass="datagridHeader"  Wrap="true" Font-Size="Small"  ></HeaderStyle>
                                        
                                        <ItemTemplate>
                                            <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.departmentname") %>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.departmentname") %>'></asp:TextBox>
                                        </EditItemTemplate>
                                    </asp:TemplateColumn>--%>
                                    <asp:TemplateColumn SortExpression="departmentname" HeaderText="Department Name">
										<HeaderStyle CssClass="datagridHeader" Wrap="false" Font-Size="Small" ></HeaderStyle>
										<ItemTemplate>
											
											<asp:Label id="LnkLOB1" Runat="server" Text='<%#Container.DataItem("departmentname")%>'>
											</asp:Label>
											
										</ItemTemplate>
									</asp:TemplateColumn>
                                    
                                    
                                    <%--<asp:TemplateColumn SortExpression="clientname"  HeaderText="Client Name">
                                       <HeaderStyle CssClass="datagridHeader" Wrap="false" Font-Size="Small" ></HeaderStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="Label1" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.clientname") %>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.clientname") %>'></asp:TextBox>
                                        </EditItemTemplate>
                                    </asp:TemplateColumn>--%>
                                    <asp:TemplateColumn SortExpression="clientname" HeaderText="Client Name">
										<HeaderStyle CssClass="datagridHeader" Wrap="false" Font-Size="Small" ></HeaderStyle>
										<ItemTemplate>
											
											<asp:Label id="LnkLOB2" Runat="server" Text='<%#Container.DataItem("clientname")%>'>
											</asp:Label>
											
										</ItemTemplate>
									</asp:TemplateColumn>
                                    
                                    
									<asp:TemplateColumn SortExpression="LOBName" HeaderText="LOB">
										<HeaderStyle CssClass="datagridHeader" Wrap="false" Font-Size="Small" ></HeaderStyle>
										<ItemTemplate>
											<asp:Label ID="lblAutoId" Runat="server" text='<%#Container.DataItem("Autoid")%>' Visible="False">
											</asp:Label>
											<asp:Label id="LnkLOB" Runat="server" Text='<%#Container.DataItem("LOBName")%>'>
											</asp:Label>
											<asp:Label AssociatedControlID="txtLOB" runat="server" ID="lvltxtLOB"></asp:Label>
											<asp:TextBox ID="txtLOB" Runat="server" Visible="False" MaxLength="50" ></asp:TextBox>
											<asp:ImageButton Runat="server" ID="cmdimgEdit" BorderWidth="0" ImageUrl="../Images/edit_16.gif"
												CommandName="EditSave" Visible="False"></asp:ImageButton>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="Message" HeaderText="Message">
										
										<ItemTemplate>
											<asp:Label id="lnkmsg" Runat="server" Text='<%#Container.DataItem("Description")%>'>
											</asp:Label>
											<asp:Label AssociatedControlID="txtmsg" runat="server" ID="lvltxtmsg"></asp:Label>
											<asp:TextBox ID="txtmsg" Runat="server" Visible="False" MaxLength="50"></asp:TextBox>
											<asp:ImageButton Runat="server" ID="cmdimgmsg" BorderWidth="0" ImageUrl="../Images/edit_16.gif"
												CommandName="EditSaveMsg" Visible="False"></asp:ImageButton>
										</ItemTemplate>
										<HeaderStyle CssClass="datagridHeader" Font-Size="Small"   ></HeaderStyle>
									</asp:TemplateColumn>
									<%--<asp:BoundColumn DataField="SaveDate" SortExpression="SaveDate" HeaderText="Added On ">
										<HeaderStyle CssClass="datagridHeader" Wrap="False"  Font-Size="Small" ></HeaderStyle>
									</asp:BoundColumn>--%>
									<asp:TemplateColumn SortExpression="SaveDate"   HeaderText="Added On">
										
										<ItemTemplate>
											<asp:Label id="lnkmsg1" Runat="server"  Text='<%#Container.DataItem("SaveDate")%>'>
											</asp:Label>
											
										</ItemTemplate>
										<ItemStyle Wrap ="false" />
										<HeaderStyle CssClass="datagridHeader" Font-Size="Small" Wrap ="false"    ></HeaderStyle>
									</asp:TemplateColumn>
									
									<%--<asp:BoundColumn DataField="SavedBy" SortExpression="SavedBy"  HeaderText="Created By">
										<HeaderStyle CssClass="datagridHeader" Wrap="False" Font-Size="Small" ></HeaderStyle></asp:BoundColumn>--%>
									<asp:TemplateColumn SortExpression="SavedBy" HeaderText="Created By">
										
										<ItemTemplate>
											<asp:Label id="lnkmsg2" Runat="server" Text='<%#Container.DataItem("SavedBy")%>'>
											</asp:Label>
											
										</ItemTemplate>
										<HeaderStyle CssClass="datagridHeader" Wrap ="false" Font-Size="Small"   ></HeaderStyle>
									</asp:TemplateColumn>
									
									 
									<asp:TemplateColumn>
									
										<HeaderStyle Wrap="False"  CssClass="datagridHeader" Font-Size="X-Small" ></HeaderStyle>
										<ItemStyle Wrap="False"></ItemStyle>
										<HeaderTemplate >
											<center>
												<asp:LinkButton id="lkbtnSelectAll" Runat="server" Font-Size="XX-Small"  ToolTip="Select All CheckBox" CommandName="Select All" >Select All/</asp:LinkButton>
												<asp:LinkButton id="lkbtnDeSelectAll" Runat="server" Font-Size="XX-Small"  ToolTip="DeSelect All CheckBox" CommandName="DeSelect All" >DeSelect All</asp:LinkButton>
											</center>
										</HeaderTemplate>
										<ItemTemplate>
											<center>
											<asp:Label ID="lbchkvisible" AssociatedControlID ="chkSelect" runat ="server"  	></asp:Label>
												<asp:CheckBox ID="chkSelect" Runat="server" BorderColor="white" BorderWidth="0" ToolTip ="Click To Select"></asp:CheckBox>
											</center>
										</ItemTemplate>
										<FooterTemplate>
											<center>
												<asp:Button ID="BtnDelete" toolTip="Delete Checked Records " Runat="server"
													CommandName="Delete" Text="Delete" CssClass="button"></asp:Button>
											</center>
										</FooterTemplate>
										
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Edit">
										<ItemTemplate>
											<asp:LinkButton ID="lnkedit" Runat="server" text="Edit"  CommandName="Edit" ToolTip="Edit Record" ></asp:LinkButton>
											<asp:LinkButton ID="lnkupdate" Runat="server" text="Update"  Visible="False" ToolTip="Update Record" CommandName="EditSave" ></asp:LinkButton>
										<asp:LinkButton ID="lnkcancel" runat="server" Text="Cancel" ToolTip="Cancel Update" Visible="false" CommandName="CancelEdit"></asp:LinkButton>
										</ItemTemplate>
										<HeaderStyle CssClass="datagridHeader"  ></HeaderStyle>
									</asp:TemplateColumn>
								</Columns>
								<PagerStyle Wrap="False" Mode="NumericPages" HorizontalAlign="Left" ></PagerStyle>
							</asp:datagrid></div>
					</td>
				</tr>
				<tr>
					<td align="center" colspan="2" scope ="colgroup">&nbsp;</td>
				</tr>
			</table>
    <asp:HiddenField ID="hfClientName" runat="server" />
		

</asp:Content>