<%--<%-
ProjectName :-  IDMS
ModuleName:-    Advance Right Management
Page Tittle:-   Assign Menu Rights
Created on :-
Created By:-   Vikas & Jitendra

- %>--%>

<%@ Page AutoEventWireup="false" CodeFile="AssignMenuRights.aspx.vb" Inherits="AdvanceRightsManagement_MenuRightsManagement_AssignMenuRights"
    Language="VB" MasterPageFile="~/MasterPage/MasterPage.master" %>

<%@ Register Src="~/AdvanceRightsManagement/UserConrol/DCLUserControl.ascx" TagName="DCLUserControl"
    TagPrefix="uc1" %>
<asp:Content ID="lftplaceholder" runat="server" ContentPlaceHolderID="LeftPlaceHolder">
    <table>
        <tr>
            <td style="width: 206px;">
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="AssignMenuRights" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
  
    <div>
        <table class="table" width="80%" summary ="used to contain controls for Assign Menu Rights">
            <caption  class ="caption" style ="background-color:#0591D3" >Assign Menu Rights</caption>
           
            <tr>
                <td align ="center"  scope="col" style ="color:black">
                    <asp:Label ID="lblSelectMenu" runat="server" CssClass="label" Text="Choose Menu " ToolTip="Choose menu"></asp:Label>
                </td>
                <td align="center" style="width: 233px; color : Black " scope="col" >
                    <asp:Label ID="lblSubLink" runat="server" AssociatedControlID="lstsubhead2" CssClass="label"
                        Text="Choose SubLink1" ToolTip="Choose sublonk here"></asp:Label>
                </td>
               
            </tr>
            <tr>
                <td align ="center" scope="col">
                    <asp:ListBox ID="lstmainhead" runat="server" CssClass="listBox" ToolTip="Choose main menu" AutoPostBack="True">
                    </asp:ListBox>&nbsp;
                </td>
                <td align ="center" style="width: 233px" scope="col" >
                    <asp:ListBox ID="lstsubhead2" runat="server" AutoPostBack="True" CssClass="listBox"
                        SelectionMode="Multiple" ToolTip="Choose Sublink"></asp:ListBox>
                </td>   
                    
            </tr>        
           
            <tr>
                <td align="center" style ="color:black"  scope="col">
                    <asp:Label ID="Label2" runat="server" AssociatedControlID="lstsubhead1" CssClass="label"
                        Text=" Choose Link" ToolTip="Choose link here"></asp:Label>
                </td>                
                <td align ="center" style="width: 233px; color :Black "  scope="col">
                    <asp:Label ID="Label1" runat="server" AssociatedControlID="lstsubhead3" CssClass="label"
                        Text="Choose SubLink2" ToolTip="Choose sublink here"></asp:Label>
                </td>                
            </tr>
            <tr>
                <td align="center" scope="col" >
                    <asp:ListBox ID="lstsubhead1" runat="server" AutoPostBack="True" CssClass="listBox" SelectionMode="Multiple"
                        ToolTip="Choose Link here"></asp:ListBox>&nbsp;
                    </td>              
                <td align ="center" style="width: 233px" >
                    <asp:ListBox ID="lstsubhead3" runat="server" CssClass="listBox" SelectionMode="Multiple"
                        ToolTip="select more links"></asp:ListBox>
                </td>                
            </tr>
            <tr></tr>  
            <tr></tr>
                 
            <tr>
                <td align ="center" colspan ="2" scope="colgroup" style ="color:black">
                    
                    <uc1:DCLUserControl ID="DCLUserControl1" runat="server" /><asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="DCLUserControl1$ddlDepartment"
                        ErrorMessage="Select Department" ValidationGroup="user"  InitialValue="--Select--" SetFocusOnError="True" ForeColor="White">*</asp:RequiredFieldValidator>                                      
                   
                        <asp:ValidationSummary ID="ValidationSummary2" runat="server" ShowMessageBox="True"
                        ShowSummary="False" ValidationGroup="user" /></td>
                <td></td>
            </tr>
            <tr></tr>
            <tr></tr>
            <tr></tr>
             <tr>
            <td colspan ="2" align ="center"  scope="colgroup" style ="color:black" >
            <div id="Superadmin" runat ="server" >
                <asp:Label ID="lblSelectUser" runat="server" CssClass="label"
                    Text="Select User Type:" ToolTip="Select user" style ="color:black"></asp:Label>
                    <asp:RadioButton ID ="rdoAdmin" runat ="server" Text ="Admins" ToolTip ="Select usertype" AutoPostBack="True"  GroupName ="Select"/>
                    <asp:RadioButton  ID="rdoUser" runat="server"  Text ="Users"  ToolTip ="Select usertype" AutoPostBack="True" GroupName ="Select"/>
                    </div>
                    <div>
                    <asp:Button ID="btnAdmin" runat ="server"  Text ="Get User" CssClass ="button" ToolTip ="click to get users" />
                    </div>
            </td>
            <td></td>
            </tr>   
            <tr></tr>
            <tr></tr>
            <tr></tr>
            <tr>
                <td align="center"  colspan ="2" valign="top" scope="colgroup" style ="color:black">
                    <asp:Label ID="Label3" runat="server" Text="Select User" CssClass ="label"></asp:Label>
                    </td>
                <td></td>
            </tr>
            <tr>
            <td align ="center" colspan ="2"  scope="colgroup" style ="color:black">
            <asp:Label ID="Label5" runat ="server" Text ="spaceforlistvikasbhardwaj" ForeColor ="white"  Visible ="false" ></asp:Label>
            <asp:ListBox ID="lstusers" runat="server" CssClass="listBox" SelectionMode="Multiple"
                        ToolTip="Choose users"></asp:ListBox>&nbsp;
               
                       
            </td>
             <td></td>
             </tr>
            <tr></tr>
            <tr></tr>
            <tr>
            
                <td  colspan ="2" align="center" scope="colgroup" style ="color:black">
                    <asp:Label ID="Label4" runat="server" ForeColor="white" Text="spaceforu" Visible ="false" ></asp:Label>
                    <asp:Button ID="cmdsav" runat="server" AccessKey="g" CssClass="button"
                        Text="AssignRights" ToolTip="Click to Assign Rights" Visible="false" ValidationGroup="final" />
                </td>
                <td></td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
            </tr>
        </table>
       <asp:Panel ID="pandelete" runat="server" BackColor="whitesmoke" BorderColor="lightgrey"
                    BorderStyle="Outset" BorderWidth="1px" Height="32px" Style="z-index: 300; left: 680px;
                    position: absolute; top: 700px" Visible="False" Width="276px">
                    <table style="width: 273px; height: 40px" width="273" summary ="used to contain control for confirmation box"  border ="2">
                    <tr>
                    <td class ="tableHeader" scope="col" style ="background-color:#0591D3" >Confirm Deletion                   
                    </td>
                    </tr>
                        <tr>
                            <td align="center" style="height: 34px; color :Black "  scope="col">
                                <strong>Are you sure, you want to cancel all other  assigned right?</strong></td>
                        </tr>
                        <tr>
                            <td align="center" scope="col">
                                <asp:Button ID="cmdyes" runat="server" CssClass="button" Text="Yes" Width="38px" ToolTip="Click YES  if you want to cancel rights or press (alt+y)" AccessKey="y" />
                                <asp:Button ID="cmdno" runat="server" CssClass="button" Text="No" Width="38px" ToolTip="click NO  if you  do not cancel the rights or press(alt+n)" AccessKey="n" /></td>
                        </tr>
                    </table>
                </asp:Panel>
                
                
    </div>
</asp:Content>
<%--<%-

Changed on :-
Created By:-   Vikas & Jitendra

- %>--%>
