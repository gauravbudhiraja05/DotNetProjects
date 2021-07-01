<%@ Page Language="VB" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="false" CodeFile="Account.aspx.vb" Inherits="Accounts_Account" Title="Lock/Unlock" %>

<%--Project Name: IDMS Phase 2
    Module Name: Accounts Management
    Page Name: Accounts
    Summary: Locks/Unlocks the User Accounts
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

        <%--<table class="table" border="0" id="PassUtility" style="background-color:#CC6666" summary="Purpose of this form:Reset User Password, Unlock/Unlock Account, Set Default Password, Set Password Duration" cellpadding="0" cellspacing="0">
    <tr>
        <td style="background-color:#CC6666; height:22px; width:10px"></td>
        <td style="background-color:#CC6666; height:22px; width:15px" align="left">
            <Img src="../images/lockunlock.gif" style="height:19px; width:12px" alt="lockunlock" />
        </td>
        <td style="background-color:#CC6666; height:22px"><span style="font-size: 10pt; color:White;"><strong>Lock/ Unlock User Account</strong></span></td>
    </tr>
    <tr><td style="background-color:#df9f9f; height:5px" colspan="3"></td></tr>
</table>--%>
        <table class="table" id="Table2" summary="Purpose of this form:Reset User Password, Unlock/Unlock Account, Set Default Password, Set Password Duration"
            cellpadding="0" cellspacing="0">
            <tr>
                <td scope="col" style="background-color: #CC6666; height: 22px; width: 10px">
                </td>
                <td scope="col" style="background-color: #CC6666; height: 22px; width: 15px" align="left">
                    <img src="../images/LockLogo.jpg" alt="LockLogo Image"/>
                </td>
                <td scope="colgroup" colspan="2" style="background-color: #CC6666; height: 22px" valign="bottom" align="right">
                    <span style="font-size: 12pt; color: White;"><strong>Lock/ Unlock </strong></span></td>
                <td  scope="col"style="background-color: #CC6666; height: 22px; width: 1px">
                </td>
            </tr>
            <tr>
                <td scope="col" style="background-color: #df9f9f; height: 5px" colspan="4">
                </td>
            </tr>
            <tr>
                <td colspan="3" scope="colgroup">
                    <table width="100%" summary="Span to search users" style="background-color: #cccccc">
                        <thead title="SelectUser">
                        </thead>
                        <tr>
                            <td colspan="2"  scope="colgroup" bgcolor="#666666" align="center">
                                <span style="font-size: 10pt; color: White;"><strong>Search By Span</strong></span></td>
                        </tr>
                        <tr>
                            <td colspan="2"  scope="colgroup">
                            </td>
                        </tr>
                        <tr>
                            <td style="height: 21px"  scope="col">
                                <label style="background-color: #cccccc" class="label" for="ctl00_MainPlaceHolder_ddlDepartmentuser" title="Department">
                                    Department</label>
                            </td>
                            <td style="width: 8px; height: 21px;"  scope="col">
                                <asp:DropDownList runat="server" CssClass="dropdownlist" ToolTip="Select Department" ID="ddlDepartmentuser" AutoPostBack="True">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td style="height: 21px"  scope="col">
                                <label class="label" style="background-color: #cccccc" for="ctl00_MainPlaceHolder_ddlClientuser" title="Client">
                                    Client</label>
                            </td>
                            <td style="width: 8px; height: 21px;"  scope="col">
                                <asp:DropDownList runat="server" ToolTip="Select Client" CssClass="dropdownlist" ID="ddlClientuser" AutoPostBack="True">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 8px; height: 21px;"  scope="col">
                              <label class="label" style="background-color: #cccccc" for="ctl00_MainPlaceHolder_ddlLobuser" title="LOB">
                                    LOB</label>
                            </td>
                            <td style="width: 80px" >
                              <asp:DropDownList runat="server" ToolTip="Select LOB" CssClass="dropdownlist" ID="ddlLobuser">
                                </asp:DropDownList>
                            </td>
                            <td style="width: 8px" scope="col">
                                
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" align="center" style="height: 24px" scope="colgroup">
                                <asp:Button runat="server" ID="btnUser" Text="Get User" CssClass="button" ToolTip="Click to get User list" Width="63px" />
                            </td>
                        </tr>
                    </table>
                </td>
                <td colspan="1" valign="top" bgcolor="#cccccc" scope="colgroup">
                    <table width="100%" style="background-color: #cccccc" summary="Search by userid">
                        <thead title="Search User">
                        </thead>
                        <tr>
                            <td colspan="2" bgcolor="#666666" align="center" style="height: 9px"  scope="colgroup">
                                <span style="font-size: 10pt; color: White;"><strong>Search By User</strong></span></td>
                        </tr>
                        <tr>
                            <td colspan="2" scope="colgroup">
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 80px" scope="col">
                                <label class="label" style="background-color: #cccccc" for="ctl00_MainPlaceHolder_txtUserId" title="UserID">
                                    UserId</label>
                            </td>
                            <td style="width: 8px" scope="col">
                                <asp:TextBox runat="server" CssClass="dropdownlist" Height="15" ToolTip="Enter UserId" ID="txtUserId" MaxLength="15" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="text-align: center" scope="colgroup">
                                <asp:Label runat="server" ID="lblBlank" Text="Enter a UserID" Visible="False" Font-Bold="True"
                                    ForeColor="Red" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" align="center" style="height: 54px" valign="bottom" scope="colgroup">
                                <asp:Button runat="server" ID="btnSearch" Text="Get User" CssClass="button" Width="63px" ToolTip="Click to get User " />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td colspan="4" style="text-align: center; background-color: #cccccc; height: 22px;" scope="colgroup">
                    <asp:Label runat="server" ID="lblUNA" Text="No User Found" Visible="False" Font-Bold="True"
                        ForeColor="Red" />
                </td>
            </tr>
        </table>
        <table class="table" id="Table1" summary="Display the searched records" style="background-color: #cccccc; width: 500">
            <tr>
                <td scope="col">
                    <asp:DataGrid runat="server" AutoGenerateColumns="false" ID="DGUser" CssClass="datagrid"
                        ShowFooter="True" >
                        
                        <Columns>
                            <asp:TemplateColumn HeaderStyle-CssClass="datagridHeader" >
                                <ItemStyle Wrap="false" Width="100%" />
                                <HeaderTemplate>
                                    <asp:Label ID="lblHUserId" runat="server" Text="UserId" />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblUserId" runat="server" Text='<%#Eval("userid")%>' />
                                </ItemTemplate>
                             
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderStyle-CssClass="datagridHeader" >
                                <ItemStyle Wrap="false" Width="100%" />
                                <HeaderTemplate>
                                    <asp:Label ID="lblHUserName" runat="server" Text="User Name" />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblUserName" runat="server" Text='<%#Eval("username")%>' />
                                </ItemTemplate>
                               
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderStyle-CssClass="datagridHeader" >
                                <ItemStyle Wrap="false" Width="100%" />
                                <HeaderTemplate>
                                    <asp:Label ID="lblHStatus" runat="server" Text="Status" />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblStatus" runat="server" Text='<%#setStatus(eval("status"))%>' />
                                </ItemTemplate>
                               
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderStyle-CssClass="datagridHeader" >
                                <ItemStyle Wrap="false" Width="100px" />
                                <HeaderTemplate>
                                    <label for="chkAllItems"></label>
                                    <input id="chkAllItems" type="checkbox" onclick="CheckAllDataGridCheckBoxes('chkSelect',document.forms[0].chkAllItems.checked)" />Lock/Unlock
                                </HeaderTemplate>
                                <ItemTemplate>
                                <asp:Label ID="lbchkvisible" AssociatedControlID="chkSelect" runat ="server"  ></asp:Label>
                                    <asp:CheckBox ID="chkSelect" runat="server" />
                                </ItemTemplate>
                                
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderStyle-CssClass="datagridHeader" >
                                <ItemStyle Wrap="false" Width="100%" />
                                <HeaderTemplate>
                                    <asp:Label ID="lblCause" Text="Lock/Unlock Reason" runat="server" />
                                </HeaderTemplate>
                                <ItemTemplate>
                                <asp:Label AssociatedControlID="txtReason" runat="server" ID="lbltxtReason"></asp:Label>
                                    <asp:TextBox ID="txtReason" Text="" runat="server" MaxLength="20" />
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Button runat="server" ID="cmdSave" Text="Lock/Unlock" CommandName="cmdSave" CssClass="button" ToolTip="Lock or Unlocks The Selected User(s)" />
                                </FooterTemplate>
                               
                            </asp:TemplateColumn>
                        </Columns>
                    </asp:DataGrid>
                </td>
            </tr>
            <tr>
                <td align="center" scope="col">
                </td>
            </tr>
        </table>
        <asp:HiddenField ID="dept1" runat="server" />
			<asp:HiddenField ID="client1" runat="server" />
			<asp:HiddenField ID="lob1" runat="server" />
    </div>
</asp:Content>
<%--
---------------- Change History -------------------------
None

--%>
