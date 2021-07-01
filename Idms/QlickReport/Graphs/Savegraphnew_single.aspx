<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Savegraphnew_single.aspx.vb" Inherits="Graphs_Savegraphnew_single" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<link rel="Stylesheet" href="../App_Themes/Themes/StyleSheet.css" type="text/css" />
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
     <table>
    <tr>
		                <td style="width: 91px" scope="col" title="">
		                    Report Name&nbsp;</td>
		                <td style="width: 270px" scope="col" title="Report name" valign="top" align="left">
                            <asp:TextBox ID="txtReportname" runat="server" CssClass="textbox" 
                                ReadOnly="true" ToolTip="Report Name" Width="136px" Enabled="False"></asp:TextBox>
                        </td>
		            </tr>
		            <tr>
		                <td style="width: 91px" scope="col" title="">
		                    <label class="label" title="Graph Name" for="txtGraphname1">
                                Graph Name:</label>
		                </td>
		                <td style="width: 270px" scope="col" title="" align="left">
		                    <asp:TextBox runat="server" ToolTip="Enter Graph Name" ID="txtGraphname" CssClass="textbox" Width="136px"></asp:TextBox></td>
		            </tr>
		            <tr>
		                <td style="width: 91px" scope="col" title="">
		                    <label class="label" title="Graph Type" for="txtGraphtype1">
                                Graph Type</label>
		                </td>
		                <td style="width: 270px" scope="col" title="" align="left">
		                    <asp:TextBox runat="server" ToolTip="Graph type" ID="txtGraphtype" 
                                ReadOnly="true" CssClass="textbox" Width="136px" Enabled="False"></asp:TextBox></td>
		            </tr>
		            
		            <tr>
		                <td colspan="2" scope="colgroup" title="" align="left">
		                    <asp:Button runat="server" ID="btnSavenew" Text="Save " cssclass="button" />
		                    <%--<input type="button" id="btnClose" value="Close" class="button" onclick="return Close();" />--%>
                            
		                </td>
		            </tr>
		            <tr>
		                <td colspan="2" scope="colgroup" title="" align="left">
		                    <asp:Label ID="savemesssage" runat="server"></asp:Label>                            
		                </td>
		            </tr>
    </table>
    </div>
    </form>
</body>
</html>
