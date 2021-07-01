<%--<%-
ProjectName :-  IDMS
ModuleName:-    Advance Right Management
Page Tittle:-  Edit Menu rights form
Created on :-
Created By:-   Vikas & Jitendra

- %>--%>

<%@ Page AutoEventWireup="false" CodeFile="ShowMenuRights.aspx.vb" Inherits="AdvanceRightsManagement_ShowMenuRights"
    Language="VB" MasterPageFile="~/MasterPage/MasterPage.master" %>

<%@ Register Src="~/AdvanceRightsManagement/UserConrol/DCLHUserControl.ascx" TagName="DCLHUserControl"
    TagPrefix="uc1" %>
<asp:Content ID="lftplaceholder" runat="server" ContentPlaceHolderID="LeftPlaceHolder">
    <table>
        <tr>
            <td style="width: 206px;">
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="UserMenuRights" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <table class="table" summary="used for contain controls for User Menu  Rights">
    <caption class ="caption" style ="background-color:#0591D3">Edit Menu Rights</caption>
               <tr></tr>
               <tr></tr>
               <tr>
            <td>
                <uc1:DCLHUserControl ID="DCLHUserControl1" runat="server" />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="DCLHUserControl1$ddlDepartment"
                    ErrorMessage="Select Department" ForeColor="White" InitialValue="--Select--"
                    SetFocusOnError="True" ValidationGroup="final">*</asp:RequiredFieldValidator>
                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                    ShowSummary="False" ValidationGroup="final" />
            </td>
            <td  style ="" >
                <asp:ImageButton ID="ImgBtnSearch" runat="server" AlternateText="SearchButton" BorderWidth="0px"
                    ImageAlign="AbsBottom" ImageUrl="~/images/Search.gif" ToolTip="Show users" Width="35px" ValidationGroup="final" />
            </td>
        </tr>
        <tr>
            <td align="left">
                <asp:GridView ID="gvMenuRights" runat="server" AllowPaging="True" style ="color:black" AllowSorting="True"
                    AutoGenerateColumns="False" ToolTip="user rights are shown here" PageSize="20">
                    <PagerStyle BackColor="#337C84" ForeColor="White" />
                    <HeaderStyle BackColor="#377C84" ForeColor="White" />
                    <Columns>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <asp:Label ID="lblUserId" runat="server" Text="UserId"></asp:Label>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkUserId" runat="server" CommandName="SortByUserId" 
                                    Text='<%#eval("UserId") %>' forecolor="Blue" ToolTip ="Click to edit rights here"></asp:LinkButton></ItemTemplate>
                        </asp:TemplateField>
                        
                        <asp:BoundField DataField="UserName" HeaderText="User Name" />
                         <asp:BoundField DataField="TypeName" HeaderText="User Type" />  
                        
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <asp:Label ID="lblcancelRights" runat="server" Text="Cancel Rights"></asp:Label>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:ImageButton ID="imgbtnCancel" runat="server" AlternateText="CancelButton" CommandName="Delete"
                                    ImageUrl="~/images/delete_16.gif" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                <asp:Panel ID="pandelete" runat="server" BackColor="whitesmoke" BorderColor="lightgrey"
                    BorderStyle="Outset" BorderWidth="1px" Height="32px" Style="z-index: 300; left: 330px;
                    position: absolute; top: 300px" Visible="False" Width="276px">
                    <table style="width: 273px; height: 40px" width="273" summary ="used to contain control for confirmation box"  border ="2">
                    <tr>
                    <td class ="tableHeader">Confirm Deletion                   
                    </td>
                    </tr>
                        <tr>
                            <td align="center" style="height: 34px">
                                <b>Are you sure, you want to cancel this assigned right?</b></td>
                        </tr>
                        <tr>
                            <td align="center">
                                <asp:Button ID="cmdyes" runat="server" CssClass="button" Text="Yes" Width="38px" ToolTip="Click YES  if you want to cancel rights or press (alt+y)" AccessKey="y" />
                                <asp:Button ID="cmdno" runat="server" CssClass="button" Text="No" Width="38px" ToolTip="click NO  if you  do not cancel the rights or press(alt+n)" AccessKey="n" /></td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
        </tr>
    </table>
</asp:Content>
<%--<%-

Changed on :-
Created By:-   Vikas & Jitendra

- %>--%>
