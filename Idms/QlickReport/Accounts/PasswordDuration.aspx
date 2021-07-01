<%@ Page Language="VB" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="false" CodeFile="PasswordDuration.aspx.vb" Inherits="PasswordDuration" Title="Password Duration" %>

<%--Project Name: IDMS Phase 2
    Module Name: Accounts Management
    Page Name: Password Duration
    Summary: Sets Password Duration for Account
    Created on: 10/05/08
    Created By: Yogesh Kumar Verma
--%>
<asp:Content ID="Content1" ContentPlaceHolderID="LeftPlaceHolder" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div>
        <table class="table" id="PassUtility" border="0" summary="Purpose of this form: Set Password Duration"
            cellpadding="0" cellspacing="0">
            <tr>
                <td style="background-color: #CC6666; height: 22px; width: 10px" scope="col">
                    <img src="../images/Set PwdDefault.jpg" alt="Set Default Password"/></td>
                <td style="background-color: #CC6666; height: 22px; width: 350px" align="right" valign="bottom" scope="col">
                    <span style="font-size: 12pt; color: White;"><strong>Set Password Duration &nbsp;&nbsp;&nbsp;</strong></span></td>
            </tr>
            <tr>
                <td style="background-color: #df9f9f; height: 5px" colspan="4" scope="colgroup">
                </td>
            </tr>
            <tr>
                <td colspan="1" style="background-color: #cccccc; height:147px" valign="Top" scope="colgroup">
                    <table width="100%" style="background-color: #cccccc">
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
                                <label style="background-color: #cccccc" class="label" for="ddlDepartmentuser" title="Department">
                                    Department</label>
                            </td>
                            <td style="width: 8px" scope="col">
                                <asp:DropDownList runat="server" ToolTip="Select Department" CssClass="dropdownlist" ID="ddlDepartmentuser" AutoPostBack="True">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td scope="col">
                                <label style="background-color: #cccccc" class="label" for="ddlClientuser" title="Client">
                                    Client</label>
                            </td>
                            <td style="width: 8px" scope="col">
                                <asp:DropDownList runat="server" ToolTip="Select Client" CssClass="dropdownlist" ID="ddlClientuser" AutoPostBack="True">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 80px" scope="col">
                                <label style="background-color: #cccccc" class="label" for="ddlLobuser" title="LOB">
                                    LOB</label>
                            </td>
                            <td style="width: 8px" scope="col">
                                <asp:DropDownList runat="server" ToolTip="Select LOB" CssClass="dropdownlist" ID="ddlLobuser">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" align="center" style="height: 24px" scope="colgroup">
                                <asp:Button runat="server" ID="btnUser" Text="Get User" CssClass="button" Width="63px" ToolTip="Click to get User list" />
                            </td>
                        </tr>
                    </table>
                </td>
                <td colspan="1" valign="top"  style="background-color: #cccccc; height:147px" scope="colgroup">
                    <table width="100%" style="background-color: #cccccc; height: 132px;">
                        <thead title="Search User">
                        </thead>
                        <tr>
                            <td colspan="2" bgcolor="#666666" align="center" style="height: 1px" scope="colgroup">
                                <span style="font-size: 10pt; color: White;"><strong>Search By User</strong></span></td>
                        </tr>
                        <tr>
                            <td colspan="2" style="height: 9px" scope="colgroup">
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 80px; text-align: right; height: 6px;" scope="col">
                                <label style="background-color: #cccccc" class="label" for="txtUserId" title="UserID">
                                    UserId</label>
                            </td>
                            <td style="width: 8px; height: 6px;" scope="col">
                                <asp:TextBox runat="server" CssClass="textbox" ID="txtUserId"
                                    MaxLength="15" ToolTip="Enter USerid" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="text-align: center; height: 4px;" scope="colgroup">
                                <asp:Label runat="server" ID="lblBlank" Text="Enter a UserID" Visible="False" Font-Bold="True"
                                    ForeColor="Red" /></td>
                        </tr>
                        <tr>
                            <td colspan="2" align="center" style="height: 43px" valign="bottom" scope="colgroup">
                                <asp:Button runat="server" ID="btnSearch" Text="Get User" CssClass="button" Width="63px" ToolTip="Click to get User" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td colspan="2" style="text-align: center; background-color: #cccccc; height: 22px;" scope="colgroup">
                    <asp:Label runat="server" ID="lblUNA" Text="No User Found" Visible="False" Font-Bold="True"
                        ForeColor="Red" />
                </td>
            </tr>
            <tr>
                <td colspan="2" style="height: 132px" scope="colgroup">
                    <asp:DataGrid runat="server" AutoGenerateColumns="false" style ="color:black"  ID="DGUser" Width="100%"
                        CssClass="datagrid" ShowFooter="True" AllowPaging="True">
                        <Columns>
                            <asp:TemplateColumn HeaderStyle-CssClass="datagridHeader">
                                <HeaderTemplate>
                                    <asp:Label ID="lblHUserName" runat="server" Text="User Name" />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblUserName" runat="server" Text='<%#Eval("username")%>' />
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lblDurText" runat="server" Text="Enter the Password Duration (Days): " />
                                    <asp:TextBox ID="txtDuration" Text="90" runat="server" MaxLength="2" />
                                    <asp:CompareValidator
                                        ID="CompareValidator1" runat="server" ControlToValidate="txtDuration" Display="Dynamic" Type="Integer" Operator="LessThan" ValueToCompare="91" ErrorMessage="Please Enter Days In Numeric Or Less/Equal To 90 Days"></asp:CompareValidator>
                                </FooterTemplate>

<HeaderStyle CssClass="datagridHeader"></HeaderStyle>
                            </asp:TemplateColumn>
                            
                            <asp:TemplateColumn HeaderStyle-CssClass="datagridHeader">
                                <HeaderTemplate>
                                    <asp:Label ID="lblHDuration" runat="server" Text="Current Duration" />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblDuration" runat="server" Text='<%#Eval("duration")%>' />
                                </ItemTemplate>

<HeaderStyle CssClass="datagridHeader"></HeaderStyle>
                            </asp:TemplateColumn>
                            
                            <asp:TemplateColumn HeaderStyle-CssClass="datagridHeader">
                                <HeaderTemplate>
                                    <asp:Label ID="lblHUpdateDate" runat="server" Text="Update Date" ToolTip="Password Update Date" />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblUpdateDate" runat="server" Text='<%#Eval("updatedate")%>' />
                                </ItemTemplate>

<HeaderStyle CssClass="datagridHeader"></HeaderStyle>
                            </asp:TemplateColumn>
                            
                            <asp:TemplateColumn HeaderStyle-CssClass="datagridHeader">
                                <HeaderTemplate>
                                    <asp:Label ID="lblHUpdatedBy" runat="server" Text="Updated By" ToolTip="Password Updated By" />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblUpdatedBy" runat="server" Text='<%#Eval("updatedby")%>' />
                                </ItemTemplate>

<HeaderStyle CssClass="datagridHeader"></HeaderStyle>
                            </asp:TemplateColumn>
                            
                            <asp:TemplateColumn HeaderStyle-CssClass="datagridHeader">
                                <HeaderTemplate>
                                    <asp:Label ID="lblHUserId" runat="server" Text="UserId" />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblUserId" runat="server" Text='<%#Eval("userid")%>' />
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Button runat="server" ID="cmdSave" Text="Set Duration" CommandName="cmdSave"
                                        CssClass="button" ToolTip="Sets The Password Duration Of Span" />
                                </FooterTemplate>

<HeaderStyle CssClass="datagridHeader"></HeaderStyle>
                            </asp:TemplateColumn>
                        </Columns>
                    </asp:DataGrid>
                    </td>
            </tr>
            <tr>
                <td colspan="2" align="center" scope="colgroup">
                    <asp:Label runat="server" ID="lblDuration" Text="Please Enter Duration in Days" Visible="False" Font-Bold="True"
                                    ForeColor="Red" /></td>
            </tr>
        </table>
        <asp:HiddenField ID="userattach" runat="server" />
    </div>
</asp:Content>
