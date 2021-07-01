<%@ Page Language="VB" AutoEventWireup="false" CodeFile="importdatahelp.aspx.vb" Inherits="DataManager_importdatahelp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<link rel="stylesheet" type="text/css" href="../App_Themes/Themes/StyleSheet.css" />

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .style1
        {
            width: 100%;
        }
        .style2
        {
            height: 490px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:Label ID="Label1" runat="server" BackColor="#CCCC00" Font-Bold="True" 
            Font-Size="Large" Text="Hwo to Import Data From Excel"></asp:Label>
    
        <br />
        <br />
        <br />
        <br />
    
    </div>
    <table class="style1">
        <tr>
            <td class="style2">
                <br />
                <asp:Label ID="Label2" runat="server" 
                    
                    Text="Step 1 : Create a table  in Excel with same numbers of Columns as Created  in Existed Table Name " 
                     Font-Size="Medium" CssClass='label'></asp:Label>
            &nbsp;<br />
                <br />
                <br />
                <br />
                <asp:Label ID="Label3" runat="server" Font-Bold="True" 
                    Text="For eg: Table name  Corelation_gg" CssClass='label '></asp:Label>
                <br />
                <br />
                <br />
                <asp:Image ID="Image1" runat="server" Height="451px" 
                    ImageUrl="~/images/excel images/dm1.png" Width="412px" />
                <br />
                <br />
                <br />
                <br />
                <br />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label4" runat="server" 
                    Text="Step 2 : The Name Of the Excel Sheet should Be same as Table Name  " 
                     Font-Size="Medium" CssClass='label'></asp:Label>
                <br />
                <br />
                <br />
                <br />
                <asp:Image ID="Image2" runat="server" 
                    ImageUrl="~/images/excel images/dm2.png" />
                <br />
                <br />
                <br />
                <br />
                <br />
                <asp:Label ID="Label5" runat="server" 
                    
                    Text="Step 3 : Sheet Name should be same as Excel Sheet name and Table Name  " 
                    CssClass="label" Font-Size="Medium"></asp:Label>
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <asp:Image ID="Image3" runat="server" 
                    ImageUrl="~/images/excel images/dm3.png" />
            </td>
        </tr>
        <tr>
            <td>
                <br />
                <br />
                <br />
                <br />
                <br />
                <asp:Label ID="Label6" runat="server" 
                    
                    Text="Step 4 : Save the Excel Sheet  in  .xlxs or .xls format only to import the data in existing table " 
                    CssClass='label' Font-Size="Medium"></asp:Label>
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <asp:Image ID="Image4" runat="server" 
                    ImageUrl="~/images/excel images/dm4.png" />
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
        </tr>
        </table>
    </form>
</body>
</html>
