<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="false" CodeFile="DataImportFromSql.aspx.vb" Inherits="TableTools_DataImportFromSql" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .style1
        {
            width: 100%;
        }
        .style2
        {
            width: 124px;
        }
        .style3
        {
            width: 124px;
            height: 332px;
        }
        .style4
        {
            height: 332px;
        }
        .style5
        {
            width: 102px;
        }
        .style7
        {
            width: 251px;
        }
        .style8
        {
            height: 332px;
            width: 251px;
        }
        .style9
        {
            width: 150px;
        }
        .style10
        {
            width: 693px;
        }
        .style11
        {
            height: 332px;
            width: 693px;
        }
        .style12
        {
            height: 121px;
        }
        .style13
        {
            width: 693px;
            height: 121px;
        }
        .style14
        {
            height: 121px;
            width: 150px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="LeftPlaceHolder" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <link rel="stylesheet" type="text/css" href="../App_Themes/Themes/StyleSheet.css" />
    <table class="style1">
     <caption>
            DATA IMPORT FROM SQLSERVER</caption>
               <tr>
            <th align="left" colspan="6" style="height: 16px">
            </th>
        </tr>
        <tr>
            <td class="style2">
                <asp:Label ID="Label1" runat="server" Text="Sever Name"></asp:Label>
            </td>
            <td class="style10">
                <asp:TextBox ID="TextBox1" runat="server" CssClass="textBox "></asp:TextBox>
            &nbsp;
            &nbsp;<asp:Label ID="msg1" Visible="false" Text="Plz enter the server name first" ForeColor="Red"   runat="server"></asp:Label>  
            </td>
        </tr>
        <tr>
            <td class="style2">
                <asp:Label ID="Label2" runat="server" Text="User ID"></asp:Label>
            </td>
            <td class="style10">
                <asp:TextBox ID="txt2" runat="server" CssClass="textBox"></asp:TextBox>
            &nbsp;&nbsp;
                <asp:Label ID="msg2" Visible="false" Text="Plz enter the userid first" ForeColor="Red" runat="server"></asp:Label>  
            </td>
        </tr>
        
        <tr>
            <td class="style2">
                <asp:Label ID="Label3" runat="server" Text="Password"></asp:Label>
            </td>
            <td class="style10">
                <asp:TextBox ID="txt3" runat="server" CssClass="textBox"></asp:TextBox>
            &nbsp;&nbsp;
                <asp:Label ID="msg3" Visible="false" Text="Plz enter the password first" ForeColor="Red" runat="server"></asp:Label>  
            </td>
        </tr>
        <tr>
            <td class="style2">
                &nbsp;</td>
            <td class="style10">
                &nbsp;<br />
                <asp:Button ID="Get_database" runat="server" Text="Get DataBase" Width="167px" 
                    CssClass="button" />
                <br />
                <br />
            </td>
        </tr>
        <tr>
            <td class="style2">
                <asp:Label ID="Label4" runat="server" Text="Select DataBase"></asp:Label>
            </td>
            <td class="style10">
                <asp:DropDownList ID="select_database" runat="server" AutoPostBack="True" 
                    CssClass ="dropdownlist">
                </asp:DropDownList>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<br />
            </td>
            <td class="style9"></td>
            <td class="style7">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
        </tr>
        <tr>
        <td class="style12"></td>
        <td class="style13">
                <asp:Label ID="Label5" runat="server" 
                    Text="Maximum 5 tables Are Allowed To Select" CssClass="label" Visible="False"></asp:Label></td>
         <td class="style14"></td>
         <td class="style12"><asp:Label ID="change" runat="server" CssClass="label" Text="Change the Table Name" Visible="false"></asp:Label></td>
        </tr>
        <tr>
            <td class="style3">
                </td>
            <td class="style11">
               <asp:ListBox ID="table_list" runat="server"  CssClass="listBox" Height="325px" Width="259px" 
                    SelectionMode="Multiple"></asp:ListBox>
                  </td>
                  <td class="style9">
                  <asp:Button ID="selected_table" runat="server" Text="Select Tables"  
                          CssClass="button"/>   
                
                      <br />
                      <br />
                      <br />
                    <asp:Button ID="created_table" runat="server" Text="Create Table "  
                        CssClass="button"/>
                      <br />
                      <br />
                      <br />
                <asp:Button ID="clear_table" runat="server" Text="Clear Tables"
                          CssClass="button"/>

                </td>
                <td class="style8">
                <asp:Panel ID="Panel1" runat="server" Height="331px"  CssClass="Panel" Width="403px" 
                        style="margin-left: 10px; margin-top: 0px;">
                    
                    <asp:TextBox ID="tx0" runat="server" Visible="False" Width="181px" CssClass="textBox"></asp:TextBox>
                    <asp:Label ID="Lbl1" runat="server" Text="Table Already Exists" Visible="False" 
                        ForeColor="Red"></asp:Label>
                    <br />
                    <br />
                    
                    <br />
                    <asp:TextBox ID="tx1" runat="server" Visible="False" Width="177px" 
                        CssClass="textBox"></asp:TextBox>
                    <asp:Label ID="Lbl2" runat="server" Text="Table Already Exists" Visible="False" ForeColor="Red"></asp:Label>
                    <br />
                    <br />
                    <br />
                    <asp:TextBox ID="tx2" runat="server" Visible="False" Width="181px" CssClass="textBox"></asp:TextBox>
                    <asp:Label ID="Lbl3" runat="server" Text="Table Already Exists" Visible="False" ForeColor="Red"></asp:Label>
                    <br />
                    <br />
                    <br />
                    <asp:TextBox ID="tx3" runat="server" Visible="False" Width="181px" CssClass="textBox"></asp:TextBox>
                    <asp:Label ID="Lbl4" runat="server" Text="Table Already Exists" Visible="False" ForeColor="Red"></asp:Label>
                    <br />
                    <br />
                    <br />
                    <asp:TextBox ID="tx4" runat="server" Visible="False" Width="181px" CssClass="textBox"></asp:TextBox>
                    <asp:Label ID="Lbl5" runat="server" Text="Table Already Exists" Visible="False" ForeColor="Red"></asp:Label>
                    <br />
                    <br />
                </asp:Panel>
            </td>
            <td class="style4">
                <br />
                <br />
                <br />
            </td>
        </tr>
        <tr><td></td><td class="style10">
                <asp:Button ID="data_import" runat="server" CssClass="button" Text="Import Data" 
                    Width="177px" />
                </td><td class="style9"></td><td class="style5"></td><td class="style7">&nbsp;</td></tr>

        <tr>
            <td class="style2">
                &nbsp;</td>
            <td class="style10">
                <br />
                <br />
            </td>
        </tr>

    </table>
    
    

</asp:Content>

