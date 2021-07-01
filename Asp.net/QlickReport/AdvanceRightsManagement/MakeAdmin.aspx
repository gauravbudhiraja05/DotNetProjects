<%--<%-
ProjectName :-  IDMS
ModuleName:-    Advance Right Management
Page Tittle:-   Make Admin
Created on :-
Created By:-   Vikas & Jitendra

- %>--%>

<%@ Page AutoEventWireup="false" CodeFile="MakeAdmin.aspx.vb" Inherits="AdvanceRightsManagement_MakeAdmin"
    Language="VB" MasterPageFile="~/MasterPage/MasterPage.master" %>
    
<%@ Register Src="~/AdvanceRightsManagement/UserConrol/DCLHUserControl.ascx" TagName="DCLHUserControl"
    TagPrefix="uc1" %>

<%@ Register Src="UserConrol/DCLUserControl.ascx" TagName="DCLUserControl" TagPrefix="uc1" %>
<asp:Content ID="lftplaceholder" runat="server" ContentPlaceHolderID="LeftPlaceHolder">
    <table >
        <tr>
            <td style="width: 206px;">
            </td>
        </tr>
    </table>
</asp:Content> 
<asp:Content ID="MakeAdmin" runat="server"    ContentPlaceHolderID="ContentPlaceHolder1">
 <link rel="stylesheet" type="text/css" href="../App_Themes/Themes/StyleSheet.css" />
<script language="javascript" type="text/javascript">

function textCounter( maxlimit)
 {
if (document.getElementById("<%=tbxComment.ClientID%>").value.length > maxlimit) // if too long...trim it!
document.getElementById("<%=tbxComment.ClientID%>").value = document.getElementById("<%=tbxComment.ClientID%>").value.substring(0, maxlimit);
// otherwise, update 'characters left' counter
else 
document.getElementById("<%=remLen.ClientID%>").value = maxlimit - document.getElementById("<%=tbxComment.ClientID%>").value.length;
}
</script>
    <table class="table" summary="used for contain controls for Make Admin" >    
        <caption class="caption">Make Admin</caption>
        <tr>
        <td style="width: 332px; color :Black "  >
        <asp:Label ID="lblCap" runat ="server"  Text ="Select User To Make Admin" CssClass ="label" Width="202px"  ToolTip ="Select User From Span"></asp:Label>
         </td>
       
         <td style="width: 303px; color :Black "  >
             <asp:Label ID="lblSpan" runat="server" CssClass="label" Text="Select Span for new admin or get admins"
                 ToolTip="Select Span for new admin or get admins" Width="275px"></asp:Label>
                 </td>
        </tr>
        <tr>
            <td style="width: 332px" >
                <uc1:DCLUserControl ID="DCLUserControl2" runat="server" />                
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="DCLUserControl2$ddlDepartment"
                    ErrorMessage="Select Department to get user" InitialValue="--Select--" SetFocusOnError="True"
                    ValidationGroup="user" ForeColor="White">*</asp:RequiredFieldValidator>
                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                    ShowSummary="False" ValidationGroup="user" />
            </td>              
            <td style="width: 303px" >                
                <uc1:DCLUserControl ID="DCLUserControl1" runat="server"  Visible ="true" />
                <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="DCLUserControl1$ddlDepartment"
                    ErrorMessage="Select Department for Admin span" InitialValue="--Select--" SetFocusOnError="True"
                    ValidationGroup="final" ForeColor="White">*</asp:RequiredFieldValidator>&nbsp;--%>
            </td>
        </tr>
         <tr>
         <td style="width: 332px" align="center"   >
             &nbsp;<asp:Button ID="btnGetUser" runat="server" CssClass="button"
                    Text="Get User" ToolTip ="Click to Get User of Selected span or press alt+u" AccessKey="u" ValidationGroup="user" />
            </td>
            <td style="width: 303px" align="center"  >
                &nbsp;<asp:Button ID="btnShowAdmin" runat="server" CssClass="button" Text="Get  Admins"
                    ToolTip="Click To see Admins list " />
                
                    
                    </td>
        </tr>
        <tr></tr>
        <tr></tr>
        <tr>
            <td  title ="Select User " style="width: 332px; height: 28px; color :Black "  >
                <asp:Label ID="lblSelectNewAdmin" runat="server" AssociatedControlID="ddlSelectNewAdmin"
                    CssClass="label" Text="Select  User" ToolTip ="Select User From DropDown List" Width="120px"></asp:Label>
                     <asp:Label ID="Label2" runat ="server" Text ="v" ForeColor ="White" Visible="False" ></asp:Label>
                <label for="ctl00_MainPlaceHolder_ddlSelectNewAdmin"></label>
                <asp:DropDownList ID="ddlSelectNewAdmin" runat="server" CssClass="dropdownList" Width ="150px" AutoPostBack="True" >
                </asp:DropDownList>&nbsp;
                               
            </td> 
            <td style="width: 303px; height: 28px; color : Black ">
                <asp:Label ID="lblGetAdmins" runat="server"  Text="Admins at selected span are:" CssClass ="label"></asp:Label>
            
                &nbsp;<asp:RequiredFieldValidator ID="rfvalSelectNewAdmin" runat="server" ControlToValidate="ddlSelectNewAdmin"
                    ErrorMessage="Select User" SetFocusOnError="True" ValidationGroup="final" ForeColor ="white" InitialValue="--Select--">*</asp:RequiredFieldValidator>
                    </td>  
                    </tr> 
                    
        
        <tr>
        <td  style="width: 332px"   align ="center"   >
            &nbsp;<asp:GridView ID="gvUserExistence" runat="server" AllowPaging="True"  PageSize ="5" AllowSorting="True"
                AutoGenerateColumns="False" ToolTip="User is admin here" Caption="User is admin at" CssClass="datagrid">
                <PagerStyle BackColor="#337C84" ForeColor="White" />
                <HeaderStyle BackColor="#377C84" ForeColor="White" />
                
                <Columns>
                    <asp:BoundField DataField="DepartmentName"  HeaderText="Department" />
                    <asp:BoundField DataField="ClientName" HeaderText="Client" />
                    <asp:BoundField DataField="LobName" HeaderText="Lob" />
                    </Columns>
            </asp:GridView>
        </td>
        <td align ="center" style="width: 303px" >
         <label for="ctl00_MainPlaceHolder_lstAdmin"></label>
            <asp:ListBox ID="lstAdmin" runat="server" AutoPostBack="True" CssClass="listBox"
                ToolTip="Admins at selected span " SelectionMode="Multiple"></asp:ListBox>
        </td>
        </tr>
        <tr></tr>
        <tr>
        <td style="width: 332px; color : Black "  >
        
            <asp:Label ID="lblComment" runat="server" AssociatedControlID="tbxComment" CssClass="label"
                Text="Comment " ToolTip ="Enter Comment" ></asp:Label>
                 <asp:Label ID="Label1" runat ="server" Text ="*" ForeColor ="red"  ></asp:Label>
                 <asp:Label ID="Label3" runat ="server" Text ="vi" ForeColor ="White" Visible="False"  ></asp:Label>
                                 <textarea  id="tbxComment" runat ="server" tabindex ="5" name ="txtdesc" onkeydown ="javascript:textCounter(100)" onkeyup ="javascript:textCounter(100)" style="width: 128px; height: 40px"  ></textarea>
                      <label for="ctl00_MainPlaceHolder_remLen"></label>
                       <input name="remLen" id ="remLen" type ="text"  runat ="server" maxlength ="3" value ="100" readonly="readonly" style="width: 16px" />
			
      
       </td>
        <td style="width: 303px" >
            <asp:Button ID="btnMakeAdmin" runat="server" CssClass="button" Text="Make Admin"  ToolTip ="Click To Make Admin" ValidationGroup="final"/>
            <asp:Button ID="btnDeleteAdmin" runat="server" CssClass="button" Text="Delete Admin"
                ToolTip="Click To delete admin " />            
            </td>
        </tr>       
        <tr>       
           
        </tr>
        <tr>
        <td style="width: 332px">
            <asp:ValidationSummary ID="svalMakeAdmin" runat="server" ShowMessageBox="True" ShowSummary="False" ValidationGroup="final" />
        </td>
           
        </tr>
        
       
        <tr>
            <td style="width: 332px">
                &nbsp;</td>
        </tr>
        
        <tr>
        <td  align ="center" colspan ="2" >  &nbsp;</td>
                 <td></td>
           
            </tr>
            <tr>
            <td align="left" style="font-style:italic; width: 332px;"><font color="red">*</font>Indicate Mandatory Field</td></tr>
</table>
    <asp:Panel ID="panConfirm" runat="server" BackColor="whitesmoke" BorderColor="lightgrey"
        BorderStyle="Outset" BorderWidth="1px" Height="32px" Style="z-index: 800; left: 430px;
        position: absolute; top: 400px" Visible="False" Width="276px">
        <table style="width: 273px; height: 40px" summary="used to contain control for confirmation box"
            width="273">
            <%--<caption class="caption">
            </caption>--%>
          <tr>
          <td class="tableHeader" align="center" style ="background-color:#0591D3">Confirm</td>
          </tr>
            <tr>
                <td align="center" style="height: 34px">
                    <strong>Admin Exist Already?</strong></td>
            </tr>
            <tr>
                <td align="center">
                    <asp:Button ID="cmdyes" runat="server" CssClass="button" Text="OK" ToolTip="Click OK as Admin alredy exist"
                        Width="38px"  /></td>
                    
            </tr>            
        </table>
        
    </asp:Panel>
    
    <asp:Panel ID="Paneldelete" runat="server" BackColor="whitesmoke" BorderColor="lightgrey"
        BorderStyle="Outset" BorderWidth="1px" Height="32px" Style="z-index: 800; left: 530px;
        position: absolute; top: 570px" Visible="False" Width="276px">
        <table style="width: 273px; height: 40px" summary="used to contain control for confirmation box"
            width="273">
            <%--<caption class="caption">
            </caption>--%>
            <tr>
                <td align="center" class="tableHeader" style="height: 15px;background-color:#0591D3">
                    Confirm</td>
            </tr>
            <tr>
                <td align="center" style="height: 34px">
                    <strong>Are you sure,you want to delete admin?</strong></td>
            </tr>
            <tr>
                <td align="center">
                    <asp:Button ID="Button1" runat="server" CssClass="button" Text="YES" ToolTip="Click to delete admin"
                        Width="38px" />
                    <asp:Button ID="Button2" runat="server" CssClass="button" Text="NO" ToolTip="Click to cancel"
                        Width="38px" />
                </td>
                        
            </tr>
        </table>
    </asp:Panel>
           
    
</asp:Content>
<%--<%-

Changed on :-
Created By:-   Vikas & Jitendra

- %>--%>
