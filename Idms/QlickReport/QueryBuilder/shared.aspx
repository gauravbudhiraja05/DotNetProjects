<%@ Page Language="Vb" AutoEventWireup="false" CodeFile="shared.aspx.vb" Inherits="Querybuilder_shared" %>

<!DOCTYPE html PUbLIC "-//W3C//Dtd XHTML 1.0 transitional//EN" "http://www.w3.org/tr/xhtml1/Dtd/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" lang="en-us" xml:lang="en-us">
<head runat="server">
    <title>Share Query</title>
    <link rel="Stylesheet" href="../App_Themes/Themes/StyleSheet.css" type="text/css" />
   
    <script  type="text/javascript">
    
		function back()
		{
		window.close()
		}

		
		</script>
</head>
<body>
   <form id="frmshared" method="post" runat="server">
			<table id="tabmain" style="width:515px" >
				<caption class="caption">Share Queries</caption>
				
				<tr>
					<td align="right" colspan="2" scope="colgroup">
						<%=formatdatetime(now,1)%>
					</td>
				</tr>
				<tr>
					<td style="height: 15px" align="right" scope="col">
					    <label dir="ltr" for="lblUser" title="Click To Select department" class="label" >User -&gt;</label>&nbsp;</td>
					<td style="height: 15px"  ><asp:label id="lblUser" runat="server" ></asp:label></td>
				</tr>
				<tr>
				    <td valign="top" scope="rowgroup">
				        <table style="width:249px" summary="">
				            <caption class="caption">Select Query</caption>
				            <%--<tr>
				                <td  scope="col">
				                    <label dir="ltr" for="cbodept" title="Click To Select department" class="label" >Department</label></td>
					            <td  scope="col">
					                <asp:dropdownlist id="cbodept" runat="server" AutoPostback="true" CssClass="dropdownlist"></asp:dropdownlist>&nbsp;
                                </td>
                                
				            </tr>
				            <tr>
					            <td  scope="col">
					                <label dir="ltr" for="cboclient" title="Click To Select Client" class="label" >Client</label>
					            </td>
					            <td scope="col" ><asp:dropdownlist id="cboclient" runat="server" AutoPostback="true" CssClass="dropdownlist"></asp:dropdownlist></td>
				            </tr>
				            <tr>
					            <td scope="col" >
					                <label dir="ltr" for="cbolob" title="Click To Select LOB" class="label" >LOb</label></td>
					            <td scope="col" ><asp:dropdownlist id="cbolob" runat="server" AutoPostback="true" CssClass="dropdownlist"></asp:dropdownlist></td>
				            </tr>--%>
				            <tr>
					            <td  scope="col">
					                <label dir="ltr" for="ddlQueryName" title="Click To Select QueryName" class="label" >Query Name</label></td>
					            <td  scope="col"><asp:dropdownlist id="ddlQueryName" runat="server" AutoPostback="true" CssClass="dropdownlist">
							            <asp:ListItem Value="Select">Select</asp:ListItem>
						            </asp:dropdownlist>
						        </td>
			                </tr>
			           </table>
			      </td>
			      <td valign="top" scope="rowgroup">
				        <table style="width:249px" summary="Select Buddy">
				            <caption class="caption">Select Buddy</caption>
				            <%--<tr>
				                <td  scope="col" >
				                    <label dir="ltr" for="ddlBuddyDepartment" title="Click To Select department" class="label" >Department</label>
				                </td>
					            <td  scope="col">
					                <asp:dropdownlist id="ddlBuddyDepartment" runat="server" AutoPostback="true" CssClass="dropdownlist"></asp:dropdownlist>&nbsp;
                                </td>
                                
				            </tr>
				            <tr>
					            <td scope="col" >
					                <label dir="ltr" for="ddlBuddyClient" title="Click To Select Client" class="label" >Client</label>
					            </td>
					            <td scope="col" ><asp:dropdownlist id="ddlBuddyClient" runat="server" AutoPostback="true" CssClass="dropdownlist"></asp:dropdownlist></td>
				            </tr>
				            <tr>
					            <td  scope="col">
					                <label dir="ltr" for="ddlBuddyLob" title="Click To Select LOB" class="label" >LOb</label>
					            </td>
					            <td  scope="col"><asp:dropdownlist id="ddlBuddyLob" runat="server" AutoPostback="true" CssClass="dropdownlist"></asp:dropdownlist></td>
				            </tr>--%>
				            <tr>
				                <td scope="row" align="center" colspan="2">
				                    <asp:Button runat="server" ID="btnGetBudy" Text="Get Buddy" ToolTip="Get Buddy" cssclass="button" />
				                </td>
				            </tr>
				            
			           </table>
			      </td>
            	</tr>
            	<tr>
            	    <td colspan="2" scope="colgroup">
                        <asp:Label ID="lblMessage" runat="server" Width="506px" Font-Bold="True" Font-Size="8pt" ForeColor="#FF3300"></asp:Label>
            	    </td>
            	</tr>
            	<tr>
					<td colspan="2"  valign="top" scope="colgroup">
						<asp:datagrid id="dgUser" CssClass="datagrid" runat="server" Width="500px" PageSize="15" DataKeyField="userid" AutoGenerateColumns="False" Visible="false" HorizontalAlign="center" AllowPaging="True">
						
						<Columns>
                            <asp:TemplateColumn>
                                <HeaderStyle CssClass="datagridHeader" Width="60%"/>
                                <HeaderTemplate >
                                    <asp:LinkButton ID="lnkBtnSelectAll" CommandName="selectAll"  runat="server">Select All</asp:LinkButton>
                                    <asp:LinkButton ID="lnkBtnDeSelectAll" CommandName="deSelectAll" runat="server">/DeSelect All</asp:LinkButton>
                                </HeaderTemplate>
                                <ItemTemplate>
                                  <asp:Label  AssociatedControlID="chkBoxItem" ID="lblchkBoxItem" runat="server"></asp:Label>
                                
                                    <asp:Checkbox ID="chkBoxItem" Runat="server" AutoPostBack="true" OnCheckedChanged="chkBoxItem_CheckedChanged"></asp:Checkbox>
                                    <asp:label ID="lblUserId" Runat="server"  text='<%#container.dataitem("userid")%>'>
                                    </asp:label>
                                    <asp:label ID="lblUserName" Runat="server"  text='<%#container.dataitem("username")%>'>
                                    </asp:label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn>
                                <HeaderStyle CssClass="datagridHeader" Width="10%"/>
                                <HeaderTemplate>
                                    View
                                </HeaderTemplate>
                                <ItemTemplate>
                                  <asp:Label  AssociatedControlID="chkBoxView" ID="lblchkBoxView" runat="server"></asp:Label>
                                    <asp:Checkbox ID="chkBoxView" Runat="server" AutoPostBack="true" OnCheckedChanged="chkBoxView_CheckedChanged" ></asp:Checkbox>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                             <asp:TemplateColumn>
                                <HeaderStyle CssClass="datagridHeader" Width="10%"/>
                                <HeaderTemplate>
                                    Edit
                                </HeaderTemplate>
                                <ItemTemplate>
                                   <asp:Label  AssociatedControlID="chkBoxEdit" ID="lblchkBoxEdit" runat="server"></asp:Label>
                                    <asp:Checkbox ID="chkBoxEdit" Runat="server" Enabled="false"></asp:Checkbox>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn>
                                <HeaderStyle CssClass="datagridHeader" Width="10%"/>
                                <HeaderTemplate>
                                    Delete
                                </HeaderTemplate>
                                <ItemTemplate>
                             <asp:Label  AssociatedControlID="chkBoxDelete" ID="lblchkBoxDelete" runat="server"></asp:Label>

                                    <asp:Checkbox ID="chkBoxDelete" Runat="server" Enabled="false"></asp:Checkbox>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn>
                                <HeaderStyle CssClass="datagridHeader" Width="10%"/>
                                <HeaderTemplate>
                                    Save As
                                </HeaderTemplate>
                                <ItemTemplate>
                                 <asp:Label  AssociatedControlID="chkBoxSaveAs" ID="lblchkBoxSaveAs" runat="server"></asp:Label>

                                    <asp:Checkbox ID="chkBoxSaveAs" Runat="server" Enabled="false"></asp:Checkbox>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                             
                        </Columns>
                        <PagerStyle Mode="NumericPages"></PagerStyle>
                        </asp:datagrid>
			        </td>
		       </tr>
		       <tr>
		            <td scope="row" colspan="2" align="center">
		                <asp:button id="cmdup" CssClass="button" Text="Share Queries" Runat="server"></asp:button>
		                <input class="button" onclick="back()" type="button" value="Close"/>
		            </td>
		            
		       </tr>
	    </table>
	    
    </form>
</body>
</html>
