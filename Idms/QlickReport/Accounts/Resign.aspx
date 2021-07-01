<%@ Page Language="VB" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="false"
    CodeFile="Resign.aspx.vb" Inherits="Resign" Title="Untitled Page" %>

<%--Project Name: IDMS Phase 2
    Module Name: Accounts Management
    Page Name: Resign
    Summary: Transfers/Resign User
    Created on: 10/05/08
    Created By: Yogesh Kumar Verma

--%>
<asp:Content ID="Content1" ContentPlaceHolderID="LeftPlaceHolder" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div>

        <script language="javascript" type="text/javascript">
        function CheckAllDataGridCheckBoxes(aspCheckBoxID, checkVal) // to (de)select all the items together
		{
            for(i = 0; i < document.forms[0].elements.length; i++)
             {
               elm = document.forms[0].elements[i];
                if (elm.type == 'checkbox') 
                {
                            elm.checked = checkVal ;

               } 

               
            }
        }

        </script>

        <table class="table" id="PassUtility" summary="Purpose of this form:Reset User Password, Unlock/Unlock Account, Set Default Password, Set Password Duration"
            cellpadding="0" cellspacing="0" style="background-color: #cccccc">
            <tr>
                <td style="background-color: #CC6666; height: 22px; width: 10px" scope="col">
                    <img src="../images/ResignTransfer.jpg" alt="ResignTransfer Image"/></td>
                <td style="background-color: #CC6666; height: 22px; width: 350px" align="right" valign="bottom" scope="col">
                    <span style="font-size: 12pt; color: White;"><strong>Resign/ Transfer &nbsp;&nbsp;&nbsp;&nbsp;</strong></span></td>
            </tr>
            <tr>
                <td style="background-color: #df9f9f; height: 5px" colspan="4" scope="colgroup">
                </td>
            </tr>
            <tr>
                <td colspan="1" scope="colgroup">
                    <table width="100%">
                        <thead title="SelectUser">
                        </thead>
                        <tr>
                            <td colspan="2" bgcolor="#666666" align="center" scope="colgroup">
                                <span style="font-size: 10pt; color: White;"><strong>Search By Span</strong></span></td>
                        </tr>
                        <tr>
                            <td colspan="2" scope="colgroup">
                            </td>
                        </tr>
                        <tr>
                            <td scope="col">
                                <label style="background-color: #cccccc" class="label" for="ctl00_MainPlaceHolder_ddlDepartmentuser" title="Department">
                                    Department</label>
                            </td>
                            <td style="width: 8px" scope="col">
                                <asp:DropDownList runat="server" ToolTip="Select Department" CssClass="dropdownlist" ID="ddlDepartmentuser" AutoPostBack="True">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td scope="col">
                                <label style="background-color: #cccccc" class="label" for="ctl00_MainPlaceHolder_ddlClientuser" title="Client">
                                    Client</label>
                            </td>
                            <td style="width: 8px" scope="col">
                                <asp:DropDownList runat="server" ToolTip="Select Client" CssClass="dropdownlist" ID="ddlClientuser" AutoPostBack="True">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 80px" scope="col">
                                <label style="background-color: #cccccc" class="label" for="ctl00_MainPlaceHolder_ddlLobuser" title="LOB">
                                    LOB</label>
                            </td>
                            <td style="width: 8px" scope="col">
                                <asp:DropDownList runat="server" ToolTip="Select LOB" CssClass="dropdownlist" ID="ddlLobuser">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" scope="colgroup" align="center" style="height: 24px">
                                <asp:Button runat="server" ID="btnUser" Text="Get User" CssClass="button" Width="63px" ToolTip="Click to get User list" />
                            </td>
                        </tr>
                    </table>
                </td>
                <td colspan="1" scope="colgroup" valign="top">
                    <table width="100%">
                        <thead title="Search User">
                        </thead>
                        <tr>
                            <td colspan="2" scope="colgroup" bgcolor="#666666" align="center" style="height: 16px">
                                <span style="font-size: 10pt; color: White;"><strong>Search By User</strong></span></td>
                        </tr>
                        <tr>
                            <td colspan="2" scope="colgroup">
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 80px">
                                <label style="background-color: #cccccc" class="label" for="ctl00_MainPlaceHolder_txtUserId" title="UserID">
                                    UserID</label>
                            </td>
                            <td style="width: 8px" scope="col">
                                <asp:TextBox runat="server" CssClass="dropdownlist" ID="txtUserId"
                                    MaxLength="15" ToolTip="Enter Userid" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="text-align: center" scope="colgroup">
                                <asp:Label runat="server" ID="lblBlank" ToolTip="Enter a UserID" Text="Enter a UserID" Visible="False" Font-Bold="True"
                                    ForeColor="Red" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" scope="colgroup" align="center" style="height: 48px" valign="bottom">
                                <asp:Button runat="server" ID="btnSearch" Text="Get User" CssClass="button" Width="63px" ToolTip="Click to get User" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            
            <tr>
                <td colspan="4" scope="colgroup" style="text-align: center; background-color: #cccccc; height: 22px;">
                    <asp:Label runat="server" ID="lblUNA" Text="No User Found" ToolTip="No User Found"  Visible="False" Font-Bold="True"
                        ForeColor="Red" />
                </td>
            </tr>
            <tr>
            <td>
            <label for ="ctl00_MainPlaceHolder_chkShow" ></label>
            <asp:CheckBox ID="chkShow" Text="Show Resigned/Transfered" ToolTip="Resigned/Transfered" runat="server"/></td>
            </tr>
            <tr>
                <td colspan="2" scope="colgroup">
                    <asp:DataGrid runat="server" AutoGenerateColumns="false" ID="DGUser" Width="100%"
                        CssClass="datagrid" ShowFooter="True">
                        <Columns>
                            <asp:TemplateColumn HeaderStyle-CssClass="datagridHeader">
                                <HeaderTemplate>
                                    <asp:Label ID="lblHUserName" runat="server" Text="User Name" />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblUserName" runat="server" Text='<%#Eval("username")%>' />
                                </ItemTemplate>
                                
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderStyle-CssClass="datagridHeader">
                                <HeaderTemplate>
                                    <asp:Label ID="lblHUserActive" runat="server" Text="Active/Deactive Status" />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblUserActive" runat="server" Text='<%#Eval("status")%>' />
                                </ItemTemplate>
                                
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderStyle-CssClass="datagridHeader">
                                <HeaderTemplate>
                                    <asp:Label ID="lblHStatus" runat="server" Text="Working Status" />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblStatus" runat="server" Text='<%#showStatus(Eval("newlock"))%>' />
                                </ItemTemplate>
                                <FooterTemplate>
                                <asp:Button runat="server" ID="cmdRecall" ToolTip="Cancel Transfer" Text="Cancel Transfer" CommandName="cmdreTransfer" CssClass="button"/>
                                </FooterTemplate>
                            </asp:TemplateColumn>
                            
                            <asp:TemplateColumn HeaderStyle-CssClass="datagridHeader">
                                <HeaderTemplate>
                                    <asp:Label ID="lblHUserId" runat="server" Text="UserId" />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblUserId" runat="server" Text='<%#Eval("userid")%>' />
                                </ItemTemplate>
                                <FooterTemplate>
                                <asp:Button runat="server" ID="cmdTransfer" ToolTip="Transfer" Text="Transfer" CommandName="cmdTransfer" CssClass="button" />
                                </FooterTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderStyle-CssClass="datagridHeader">
                                <HeaderTemplate>
                                     <label for="chkAllItems"></label>
                                    <input id="chkAllItems" type="checkbox" onclick="CheckAllDataGridCheckBoxes('chkSelect',document.forms[0].chkAllItems.checked)" />Select
                                    All
                                </HeaderTemplate>
                                <ItemTemplate>
                                <asp:Label ID="lbchkvisible" AssociatedControlID ="chkSelect" runat ="server"  ></asp:Label>
                                    <asp:CheckBox ID="chkSelect" runat="server" />
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Button runat="server" ID="cmdSave" Text="Resign" ToolTip="Resign" CommandName="cmdSave" CssClass="button" />
                                </FooterTemplate>
                            </asp:TemplateColumn>
                        </Columns>
                    </asp:DataGrid>
                </td>
            </tr>
            <tr>
                <td colspan="2" scope="colgroup" align="center">
                </td>
            </tr>
        </table>
        <asp:Panel ID="pandelete" Width="272px" Runat="server" BorderColor="lightgrey" BackColor="whitesmoke"
				BorderStyle="Outset" style="Z-INDEX: 101; LEFT: 336px; POSITION: absolute; TOP:480px" Height="84px"
				BorderWidth="1px" Visible="False">
				<table style="WIDTH: 273px; HEIGHT: 64px" width="273">
					<tr>
						<td align="center" scope="col" style="height: 27px; width: 269px;"><strong>Are you sure, you want to Resign?</strong></td>
					</tr>
					<tr>
						<td align="center" style="width: 269px" scope="col">
							<asp:Button id="cmdyes" Runat="server" CssClass="button" Text="Yes" Width="38px"></asp:Button>
							<asp:Button id="cmdno" Runat="server" CssClass="button" Text="No" Width="38px"></asp:Button></td>
					</tr>
				</table>
			</asp:Panel>
			<asp:Panel ID="panTransfer" Width="272px" Runat="server" BorderColor="lightgrey" BackColor="whitesmoke"
				BorderStyle="Outset" style="Z-INDEX: 101; LEFT: 336px; POSITION: absolute; TOP:480px" Height="84px"
				BorderWidth="1px" Visible="False">
				<table style="WIDTH: 273px; HEIGHT: 64px" width="273">
					<tr>
						<td align="center" scope="col" style="height: 27px; width: 269px;"><strong>Are you sure, you want to Transfer?</strong></td>
					</tr>
					<tr>
						<td align="center" style="width: 269px; height: 26px;" scope="col">
							<asp:Button id="transferyes" Runat="server" CssClass="button" Text="Yes" Width="38px"></asp:Button>
							<asp:Button id="transferno" Runat="server" CssClass="button" Text="No" Width="38px"></asp:Button></td>
					</tr>
				</table>
			</asp:Panel>
			<asp:Panel ID="panreTransfer" Width="272px" Runat="server" BorderColor="lightgrey" BackColor="whitesmoke"
				BorderStyle="Outset" style="Z-INDEX: 101; LEFT: 336px; POSITION: absolute; TOP:480px" Height="84px"
				BorderWidth="1px" Visible="False">
				<table style="WIDTH: 273px; HEIGHT: 64px" width="273">
					<tr>
						<td align="center" style="height: 27px; width: 269px;" scope="col"><strong>Are you sure, you want to Cancel Transfer?</strong></td>
					</tr>
					<tr>
						<td align="center" style="width: 269px; height: 26px;" scope="col">
							<asp:Button id="cancelyes" Runat="server" CssClass="button" Text="Yes" Width="38px"></asp:Button>
							<asp:Button id="cancelNo" Runat="server" CssClass="button" Text="No" Width="38px"></asp:Button></td>
					</tr>
				</table>
			</asp:Panel>
			<asp:HiddenField ID="dept1" runat="server" />
			<asp:HiddenField ID="client1" runat="server" />
			<asp:HiddenField ID="lob1" runat="server" />
    </div>
</asp:Content>
