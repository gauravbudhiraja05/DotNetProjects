<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="false" CodeFile="dataimportfromoracle.aspx.vb" Inherits="TableTools_dataimportfromoracle" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="LeftPlaceHolder" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<link rel="stylesheet" type="text/css" href="../App_Themes/Themes/StyleSheet.css" />
    <table class="table" cellpadding="0" width="90%">
        <tr>
	 		<td align="center" colspan="2" class="tableHeader"><asp:label id="lblheading" Width="100%" Runat="server">Data Import From Oracle</asp:label></td>
					</tr>
        <tr>
            <td class="style2">
                <asp:Label ID="Label1" runat="server" style="color:Black" CssClass ="label" 
                    Text="Server Name :"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="servername" runat="server" Width="168px" CssClass="textbox" 
                    AutoPostBack="True" ></asp:TextBox>
            </td>
            <td>
                <asp:Label ID="msg1" Visible="false" Text="Plz enter the server name first" ForeColor="Red"   runat="server"></asp:Label>  
            </td>
        </tr>
        <tr>
            <td class="style2">
                <asp:Label ID="Label2" runat="server" style="color:Black" CssClass ="label" 
                    Text="UserId :"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="uid" runat="server" Width="168px" AutoPostBack="True"></asp:TextBox>
            </td>
            <td>
                <asp:Label ID="msg2" Visible="false" Text="Plz enter the userid first" ForeColor="Red" runat="server"></asp:Label>  
            </td>
        </tr>
        <tr>
            <td class="style2">
                <asp:Label ID="Label3" runat="server" style="color:Black" CssClass ="label"
                    Text="Password :"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="pw" runat="server" Width="168px" SkinID="*" 
                    AutoPostBack="True" ></asp:TextBox>
            </td>
            <td>
                <asp:Label ID="msg3" Visible="false" Text="Plz enter the password first" ForeColor="Red" runat="server"></asp:Label>  
            </td>
        </tr>
        <tr>
           <td colspan="2" align="center">
           <asp:Button  ID="getdb" runat="server" CssClass="button" Text="GetTables" /> 
           </td>  
        </tr>
    </table>
    <table class="style3" align="center">
        <tr>
            <td>
                (<span class="style1">*</span>)Only Select 5 tables at a time<strong><br />
                Select Tables</strong></td>
              <td>
                <asp:Label ID="error1" runat="server"  Visible="False" 
              Text="Plz select atleast </br> one table" ForeColor="Red"></asp:Label>
                
              </td>
            <td>
                You can change name of the tables<br />
                <strong>Selected Tables</strong></td>
        </tr>
        <tr>
            <td>
                <asp:ListBox ID="selecttb" runat="server" Height="173px" 
                    Width="180px" SelectionMode="Multiple"></asp:ListBox>
            </td>
            <td>
                <asp:Button ID="addtable" runat="server" CssClass="button" Text="AddTable" /> 
            </td>
            <td>
                <asp:Panel ID="Panel1" runat="server" Height="160px" Width="192px">
                    <asp:TextBox ID="tb0" runat="server" Height="23px" Visible="false" 
                        Width="168px" CssClass="textbox" ></asp:TextBox>
                    <br />
                    <asp:TextBox ID="tb1" runat="server" Height="24px" Visible="false" 
                        Width="168px" CssClass="textbox" ></asp:TextBox>
                    <br />
                    <asp:TextBox ID="tb2" runat="server" Height="22px" Visible="false" 
                        Width="168px" CssClass="textbox"></asp:TextBox>
                    <br />
                    <asp:TextBox ID="tb3" runat="server" Height="24px" Visible="false" 
                        Width="168px" CssClass="textbox"></asp:TextBox>
                    <br />
                    <asp:TextBox ID="tb4" runat="server" Height="24px" Visible="false" 
                        Width="168px" CssClass="textbox"></asp:TextBox>
                </asp:Panel>
            </td>
            <td>
            <asp:Panel ID="Panel2" runat="server" Height="160px" Width="192px">
            <table>
            <tr><td>
            <asp:Label ID="lbl1" runat="server" Visible="false" Height="18px"    ForeColor="Red" Text="Table are already exist"></asp:Label></td></tr>
            <tr><td>
             <asp:Label ID="lbl2" runat="server" Visible="false" Height="18px"   ForeColor="Red" Text="Table are already exist"></asp:Label></td></tr>
            <tr><td>
             <asp:Label ID="lbl3" runat="server" Visible="false" Height="18px"  ForeColor="Red" Text="Table are already exist"></asp:Label></td></tr>
            <tr><td>
             <asp:Label ID="lbl4" runat="server" Visible="false" Height="18px"   ForeColor="Red" Text="Table are already exist"></asp:Label></td></tr>
            <tr><td>
             <asp:Label ID="lbl5" runat="server" Visible="false" Height="18px"   ForeColor="Red" Text="Table are already exist"></asp:Label></td></tr>
                </asp:Panel>
             </table>
            </td>
        </tr>
        <tr>
        <tr />
        <tr>
           <td>
             <asp:Button ID="Chkexist" Text="Check Tables" runat="server" CssClass="button" />
         </td>
        <td align="center">
         <asp:Button id="Importdata" Text="Import Data" CssClass="button" runat="server" />
        </td>
        </tr>
    </table>
    <br />
    <br />
    <br />
    <br />
    <br />
</asp:Content>

