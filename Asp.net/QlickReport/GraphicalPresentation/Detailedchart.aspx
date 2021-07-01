<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Detailedchart.aspx.vb" Inherits="GraphicalPresentation_Detailedchart" %>
<%@ Register TagPrefix="dcwc" Namespace="Dundas.Charting.WebControl" Assembly="DundasWebChart" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" lang="en-us" xml:lang="en-us">
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <div> 
    <dcwc:chart id="Chart1" runat="server" BackColor="#F3DFC1" Palette="Dundas" ImageType="Png" ImageUrl="~/TempImages/ChartPic_#SEQ(300,3)" Width="412px" Height="296px" borderlinestyle="Solid" backgradienttype="TopBottom" borderlinewidth="2" borderlinecolor="181, 64, 1">
							<titles>
								<dcwc:Title ShadowColor="32, 0, 0, 0" Font="Trebuchet MS, 14.25pt, style=Bold" ShadowOffset="3" Text="Regional Sales" Alignment="TopLeft" Color="26, 59, 105"></dcwc:Title>
							</titles>
							<legends>
								<dcwc:Legend Enabled="False" AutoFitText="False" Name="Default" BackColor="Transparent" Font="Trebuchet MS, 8.25pt, style=Bold">
									<position y="21" height="22" width="18" x="73"></position>
								</dcwc:Legend>
							</legends>
							<borderskin skinstyle="Emboss"></borderskin>
							<series>
								<dcwc:Series Name="Sales" BorderColor="180, 26, 59, 105"></dcwc:Series>
							</series>
							<chartareas>
								<dcwc:ChartArea Name="Default" BorderColor="64, 64, 64, 64" BorderStyle="Solid" BackGradientEndColor="White" BackColor="OldLace" ShadowColor="Transparent" BackGradientType="TopBottom">
									<area3dstyle yangle="10" perspective="10" xangle="15" rightangleaxes="False" wallwidth="0" clustered="True"></area3dstyle>
									<axisy linecolor="64, 64, 64, 64" labelsautofit="False">
										<labelstyle font="Trebuchet MS, 8.25pt, style=Bold"></labelstyle>
										<majorgrid linecolor="64, 64, 64, 64"></majorgrid>
									</axisy>
									<axisx linecolor="64, 64, 64, 64" labelsautofit="False">
										<labelstyle font="Trebuchet MS, 8.25pt, style=Bold"></labelstyle>
										<majorgrid linecolor="64, 64, 64, 64"></majorgrid>
									</axisx>
								</dcwc:ChartArea>
							</chartareas>
						</dcwc:chart>
					<table class="controls" cellpadding="4" summary="">
							<tr>
								<td class="label48" scope="col"></td>
								<td>&nbsp;-&nbsp;<a href="javascript:window.history.back(1);">Back</a> -</td>
							</tr>
						</table>
    </div>
    </form>
</body>
</html>
