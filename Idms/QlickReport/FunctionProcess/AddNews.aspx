<%@ Page Language="VB" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="false" CodeFile="AddNews.aspx.vb" Inherits="FunctionProcess_AddNews" title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="LeftPlaceHolder" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server" >
 <link rel="stylesheet" type="text/css" href="../App_Themes/Themes/StyleSheet.css" />
    <table cellpadding="0" cellspacing="0" width="750" class="table" id="tableh1" runat="server">
    <caption style="background-color:#67A897">Add New Hierarchy</caption>
        <tr>
            <td class="style1">
                <asp:Label ID="ll1" runat="server" Text="Level 1" CssClass="label"></asp:Label>
            </td>
            <td class="style2">
                <asp:TextBox ID="dept" runat="server"></asp:TextBox>
&nbsp;&nbsp;&nbsp;
                
            </td>
            <td>&nbsp;(*) <asp:Label ID="errdept" runat="server" ForeColor="Red" CssClass="label " Text="Label" 
                    Visible="False"></asp:Label></td>
        </tr>
        <tr>
            <td class="style1">
                <asp:Label ID="ll2" runat="server" Text="Level 2" CssClass="label"></asp:Label>
            </td>
            <td class="style2">
                <asp:TextBox ID="client" runat="server" style="margin-left: 0px"></asp:TextBox>
&nbsp;&nbsp;&nbsp;
                
            </td>
            <td>&nbsp;(*) <asp:Label ID="errclient" runat="server" ForeColor="Red" CssClass="label" Text="Label" 
                    Visible="False"></asp:Label></td>
        </tr>
        <tr>
            <td class="style1">
                <asp:Label ID="ll3" runat="server" Text="Level 3" CssClass="label"></asp:Label>
            </td>
            <td class="style2">
                <asp:TextBox ID="lob" runat="server"></asp:TextBox>
&nbsp;&nbsp;&nbsp;
                
            </td>
            <td>(*) <asp:Label ID="errlob" runat="server" ForeColor="Red" CssClass="label" Text="Label" 
                    Visible="False"></asp:Label></td>
        </tr>
        <tr>
        <td class="style1"></td>
            <td class="style2">
            <asp:Button ID="Button1" runat="server" Text="Submit" CssClass="button" />
                &nbsp;</td>
            
        </tr>
        <tr>
            <td class="style1">
                &nbsp;</td>
            <td class="style2">
                &nbsp;</td>
        </tr>
        <tr>
            <td class="style1">
                &nbsp;</td>
            <td class="style2">
                &nbsp;</td>
        </tr>
    </table>

    <table width="750px">
    <tr>
    <td style="width:50px;" ></td>
    <td style="width:700px;">
    <asp:GridView ID="grdlob"  Width="400px" runat="server" AutoGenerateColumns="False"
                    DataKeyNames="MenuID" CssClass="datagrid" > 
                    <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
               <Columns>
                <asp:TemplateField  HeaderText="Level Name" HeaderStyle-Width="125px" SortExpression="MenuDescription"><ItemTemplate><%# Eval("MenuDescription")%></ItemTemplate>
                <EditItemTemplate><asp:TextBox ID="txtLob"  runat="server"  Text='<%# Eval("MenuDescription") %>'></asp:TextBox></EditItemTemplate>
                </asp:TemplateField>
                         <asp:CommandField ShowHeader="true"  HeaderText="Edit" ShowEditButton="true" 
                       CausesValidation="false" HeaderStyle-Width="75px">
                   </asp:CommandField>
              </Columns>
                 <AlternatingRowStyle BackColor="#DCDCDC" />
             </asp:GridView>
    </td>
    </tr>
    </table>

</asp:Content>

<asp:Content ID="Content3" runat="server" contentplaceholderid="head">
    <style type="text/css">
        .style1
        {
            width: 170px;
        }
        .style2
        {
            width: 136px;
        }
    </style>
</asp:Content>


