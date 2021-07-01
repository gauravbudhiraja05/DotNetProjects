<%@ Page Language="VB" AutoEventWireup="false" CodeFile="tabdelimtedhelpfile.aspx.vb" Inherits="TableTools_tabdelimtedhelpfile" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<link rel="stylesheet" type="text/css" href="../App_Themes/Themes/StyleSheet.css" />
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .style1
        {
            width: 54%;
        }
        .style2
        {
            width: 605px;
        }
        .style3
        {
            width: 605px;
            height: 54px;
        }
        </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:Label ID="Label1" runat="server" BackColor="Yellow" Font-Size="X-Large" 
            Text="How to Create a Tab Delimited File in Microsoft Excel"></asp:Label>
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <table class="style1">
            <tr>
                <td class="style3">
                    <asp:Label ID="Label2" runat="server" Font-Size="Medium" 
                        Text="Step 1: Create a Table in Excel " CssClass="label"></asp:Label>
                    <br />
                    <br />
                    <img alt="" src="../images/excel%20images/p1.png" style="width: 596px" /></td>
            </tr>
            <tr>
                <td class="style2">
                    <br />
                    <asp:Label ID="Label3" runat="server" Font-Size="Medium" 
                        Text="Step 2 : Save Excel file in a tab delimited format"  
                        CssClass="label"></asp:Label>
                    <br />
                    <br />
                    <br />
                    <img alt="" src="../images/excel%20images/p2.png" style="width: 596px" /></td>
            </tr>
            <tr>
                <td class="style2">
                    <br />
                </td>
            </tr>
            <tr>
                <td class="style2">
                    <asp:Label ID="Label4" runat="server"  CssClass="label" 
                        
                        Text="Step 3: You may receive a warning about the file type not supporting workbooks with multiple sheets. Click OK to save only the active sheet." 
                        Font-Size="Medium"></asp:Label>
                    <br />
                    <br />
                    <img alt="" src="../images/excel%20images/p3.png" style="width: 596px" /></td>
            </tr>
            <tr>
                <td class="style2">
                    <br />
                    <asp:Label ID="Label5" runat="server" 
                        Text="Step 4 :You may also receive a warning about the file containing features not compatible with Text (Tab delimited). Click Yes to keep this format." 
                        CssClass="label" Font-Size="Medium"></asp:Label>
&nbsp; 
                    <br />
                    <br />
                    <img alt="" src="../images/excel%20images/p4.png" style="width: 596px" /></td>
            </tr>
            <tr>
                <td class="style2">
                    <br />
                </td>
            </tr>
            <tr>
                <td class="style2">
                    <asp:Label ID="Label6" runat="server" CssClass="label" Text="Step 5 : You have now created a tab delimited text file that contains your data exchange file. Locate 
                    the text (.txt) file in the location to which you saved it on your PC." 
                        Font-Size="Medium"></asp:Label>
&nbsp;<br />
                    <br />
                    <img alt="" src="../images/excel%20images/p5.png" style="width: 596px" /></td>
            </tr>
            <tr>
                <td class="style2">
                    <br />
                    <asp:Label ID="Label7" runat="server" CssClass="label" 
                        
                        Text=" Step  6 : If you open the text file, it should look similar to the example below." 
                        Font-Size="Medium"></asp:Label>
                    <br />
                    <br />
                    <img alt="" src="../images/excel%20images/p6.png" 
                        style="width: 610px; height: 134px;" /></td>
            </tr>
        </table>
    
    </div>
    <br />
    <br />
    </form>
</body>
</html>
