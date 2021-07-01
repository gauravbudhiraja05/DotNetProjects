<%@ Page Language="VB" AutoEventWireup="false" CodeFile="datadisplay.aspx.vb" Inherits="QueryBuilder_datadisplay" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" lang="en-us" xml:lang="en-us">
<head runat="server">
    <title>datadisplay </title>


    <script language="javascript" type="text/jscript">

		function email()
		{ 
		
		}
		function xls()
		{ 
		
		}
		
		function chart()
		{ 
		
		
		}
		
		</script>
	</head>
	<body>
		<form id="frmdisplay"  method="post" action="datadisplay.aspx" >
			<table  width="100%" summary="Display Data">
			
				<tr>
					<td scope="col"><asp:datagrid  ID="dgdisplay" runat="server" cssclass="grid1" Width="100%" BorderColor="#3C5F84"
							BorderStyle="None" BorderWidth="1px" BackColor="black" CellPadding="3" GridLines="Vertical"
							Font-Size="Small" HorizontalAlign="Center">
							<SelectedItemStyle Font-Bold="True" ForeColor="White" BackColor="#3C5F84"></SelectedItemStyle>
							<AlternatingItemStyle BackColor="Gainsboro"></AlternatingItemStyle>
							<ItemStyle ForeColor="Black" BackColor="#EEEEEE"></ItemStyle>
							<HeaderStyle Font-Size="XX-Large" Font-Underline="True" Font-Bold="True" ForeColor="#ffffff"
								BackColor="#3C5F84"></HeaderStyle>
							<FooterStyle ForeColor="Black" BackColor="#CCCCCC"></FooterStyle>
							<PagerStyle HorizontalAlign="Center" ForeColor="Black" BackColor="#999999" Mode="NumericPages"></PagerStyle>
						</asp:datagrid></td>
				</tr>
				<tr>
					<td scope="col" align="center">
					</td>
				</tr>
				<tr>
					<td scope="col"><div id="divdis" runat="server"><asp:label id="lbl1" Runat="server">Name of Excel File</asp:label>
							<asp:textbox  ID="txttb" Width="100px" Runat="server"></asp:textbox>
							<asp:button  CssClass="button" id="cmdsave" Runat="server" Text="Save Result Into File"></asp:button>
							<asp:textbox ID="txtpath" runat="server" Visible="False"></asp:textbox>
							<asp:textbox  ID="txthidden" runat="server" Visible="False"></asp:textbox></div>
					</td>
				</tr>
			</table>
    </form>
</body>
</html>
