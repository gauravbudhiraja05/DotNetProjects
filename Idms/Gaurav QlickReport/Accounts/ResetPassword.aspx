<%@ Page Language="VB" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="false"
    CodeFile="ResetPassword.aspx.vb" Inherits="ResetPassword" Title="Reset Password" %>

<%--Project Name: IDMS Phase 2
    Module Name: Accounts Management
    Page Name: ResetPassword
    Summary: Resets User Passwords
    Created on: 10/05/08
    Created By: Yogesh Kumar Verma

--%>
<asp:Content ID="Content1" ContentPlaceHolderID="LeftPlaceHolder" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="divReset" runat="server">

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


        function SelectAllCheckboxes(spanChk){
        // Added as ASPX uses SPAN for checkbox
        var oItem = spanChk.children;
        var theBox=(spanChk.type=="checkbox")?spanChk:spanChk.children.item[0];
        xState=theBox.checked;

        elm=theBox.form.elements;
        for(i=0;i<elm.length;i++)
        if(elm[i].type=="checkbox" && elm[i].id!=theBox.id)
        {
        //elm[i].click();
        if(elm[i].checked!=xState)
        elm[i].click();
        //elm[i].checked=xState;
        }
        }
        </script>

        <table class="table" id="PassUtility" summary="Purpose of this form:Reset User Password"
            cellpadding="0" cellspacing="0" style="background-color: #cccccc">
            <tr>
                <td style="background-color: #CC6666; height: 22px; width: 10px" scope="col">
                    <img src="../images/Resetpwd.jpg" alt="Reset Password Image"/></td>
                <td style="background-color: #CC6666; height: 22px; width: 350px" align="right" valign="bottom" scope="col">
                    <span style="font-size: 12pt; color: White;"><strong>Reset Password &nbsp;&nbsp;&nbsp;</strong></span></td>
            </tr>
            <tr>
                <td style="background-color: #df9f9f; height: 5px" colspan="4" scope="colgroup">
                </td>
            </tr>
            <tr>
                <td colspan="1" style="height: 148px; width: 332px;" valign="top" scope="colgroup">
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
                                <label style="background-color: #cccccc" class="label" for="ctl00_MainPlaceHolder_ddlDepartmentuser" title="Department">
                                    Department</label>
                            </td>
                            <td style="width: 8px" scope="col">
                                <asp:DropDownList runat="server" ToolTip="Select Department" CssClass="dropdownlist" ID="ddlDepartmentuser" AutoPostBack="True">
                                </asp:DropDownList></td>
                        </tr>
                        <tr>
                            <td scope="col">
                                <label style="background-color: #cccccc" class="label" for="ctl00_MainPlaceHolder_ddlClientuser" title="Client">
                                    Client</label>
                            </td>
                            <td style="width: 8px" scope="col">
                                <asp:DropDownList runat="server" ToolTip="Select Client" CssClass="dropdownlist" ID="ddlClientuser" AutoPostBack="True">
                                </asp:DropDownList></td>
                        </tr>
                        <tr>
                            <td style="width: 80px" scope="col">
                                <label style="background-color: #cccccc" class="label" for="ctl00_MainPlaceHolder_ddlLobuser" title="LOB">
                                    LOB</label>
                            </td>
                            <td style="width: 8px" scope="col">
                                <asp:DropDownList runat="server" ToolTip="Select LOB" CssClass="dropdownlist" ID="ddlLobuser">
                                </asp:DropDownList></td>
                        </tr>
                        <tr>
                            <td colspan="2" align="center" style="height: 24px" scope="colgroup"> 
                                <asp:Button runat="server" ID="btnUser" Text="Get User" CssClass="button" Width="63px" ToolTip="Click to get User list" />
                            </td>
                        </tr>
                    </table>
                </td>
                <td colspan="1" valign="top" style="height: 148px" scope="colgroup">
                    <table width="100%" style="background-color: #cccccc">
                        <thead title="Search User">
                        </thead>
                        <tr>
                            <td colspan="2" bgcolor="#666666" align="center" style="height: 18px" scope="colgroup">
                                <span style="font-size: 10pt; color: White;"><strong>Search By User</strong></span></td>
                        </tr>
                        <tr>
                            <td colspan="2" scope="colgroup">
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 80px" scope="col">
                                <label style="background-color: #cccccc" class="label" for="ctl00_MainPlaceHolder_txtUserId" title="UserID">
                                    User Id</label></td>
                            <td style="width: 8px" scope="col">
                                <asp:TextBox runat="server" CssClass="textbox" ID="txtUserId" MaxLength="15" ToolTip="Enter Userid" /></td>
                        </tr>
                        <tr>
                            <td colspan="2" style="text-align: center" scope="colgroup">
                                <asp:Label runat="server" ID="lblBlank" Text="Enter a UserID" Visible="False" Font-Bold="True"
                                    ForeColor="Red" /></td>
                        </tr>
                        <tr>
                            <td colspan="2" scope="colgroup" align="center" style="height: 47px" valign="bottom">
                                <asp:Button runat="server" ID="btnSearch" Text="Get User" CssClass="button" Width="63px" ToolTip="Click to get User " />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td colspan="2" scope="colgroup" style="text-align: center; background-color: #cccccc; height: 22px;">
                    <asp:Label runat="server" ID="lblUNA" Text="No User Found" Visible="False" Font-Bold="True"
                        ForeColor="Red" /></td>
            </tr>
            <tr>
                <td colspan="2" scope="colgroup" align="center">
                </td>
            </tr>
            <tr>
                <td colspan="2" scope="colgroup">
                    <asp:DataGrid runat="server" AutoGenerateColumns="false" ID="DGUser" Width="100%"
                        CssClass="datagrid" ShowFooter="True">
                        <Columns>
                            <asp:TemplateColumn HeaderStyle-CssClass="datagridHeader" >
                                <HeaderTemplate >
                                    <asp:Label ID="lblHUserName" runat="server" Text="User Name" />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblUserName" runat="server" Text='<%#Eval("username")%>' />
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderStyle-CssClass="datagridHeader">
                                <HeaderTemplate>
                                    <asp:Label ID="lblHUserId" runat="server" Text="UserId" />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblUserId" runat="server" Text='<%#Eval("userid")%>' />
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderStyle-CssClass="datagridHeader" HeaderStyle-Wrap ="False">
                                <HeaderTemplate >
                                    <asp:Label AssociatedControlID="chkAllItems" runat="server" ID="lblchkAllItems"></asp:Label>
                                    <asp:CheckBox ID="chkAllItems" runat="server" onclick="javascript:SelectAllCheckboxes(this);" />Default All
                                    <%--<input id="chkAllItems" type="checkbox" onclick="CheckAllDataGridCheckBoxes('chkSelect',document.forms[0].chkAllItems.checked)"/>Default All--%>
                                
                                </HeaderTemplate>
                                <ItemTemplate>
                                <asp:Label ID="lbchkvisible" AssociatedControlID ="chkSelect" runat ="server"  ></asp:Label>
                                    <asp:CheckBox ID="chkSelect" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderStyle-CssClass="datagridHeader">
                                <HeaderTemplate>
                                    <asp:Label AssociatedControlID="txtDefAll" runat="server" ID="lbltxtDefAll"></asp:Label>
                                    <asp:TextBox ID="txtDefAll" ToolTip="Enter Password"  Text="" runat="server" MaxLength="15" />
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ControlToValidate="txtDefAll"
                                        ValidationExpression="^.*(?=.{8,})(?=.*\d)(?=.*[a-zA-Z])(?=.*[0-9])(?=.*[@#$%^&+=]).*$"
                                        runat="server" ErrorMessage="Enter 8-15 Characters in Length (Mix Of Alphabetic, Non Alphabetic & Special Characters)" Display="Dynamic" />
                                </HeaderTemplate>
                                <ItemTemplate>
                                <asp:Label AssociatedControlID="txtPassword" runat="server" ID="lbltxtPassword"></asp:Label>
                                    <asp:TextBox ID="txtPassword" Text="" ToolTip="Enter Password"  runat="server" MaxLength="15" />
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" ControlToValidate="txtPassword"
                                        ValidationExpression="^.*(?=.{8,})(?=.*\d)(?=.*[a-zA-Z])(?=.*[0-9])(?=.*[@#$%^&+=]).*$"
                                        runat="server" ErrorMessage="Enter 8-15 Characters in Length (Mix Of Alphabetic, Non Alphabetic & Special Characters)" Display="dynamic" />
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Button runat="server" ID="cmdSave" Text="Reset Password" ToolTip="Reset Password" CommandName="cmdSave"
                                        CssClass="button" />
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
    </div>
</asp:Content>
