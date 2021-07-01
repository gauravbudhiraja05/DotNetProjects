<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Regiongraph.aspx.vb" Inherits="GraphicalPresentation_Regiongraph" %>
<%@ Register TagPrefix="dcwc" Namespace="Dundas.Charting.WebControl" Assembly="DundasWebChart" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" lang="en-us" xml:lang="en-us">
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <dcwc:chart id="Chart1" runat="server" RenderType="BinaryStreaming" ImageType="Png" Height="152px" Width="160px" Palette="Dundas" BackColor="#F3DFC1" borderlinestyle="Solid" backgradienttype="TopBottom" borderlinewidth="2" borderlinecolor="181, 64, 1" MapEnabled="False">
	<Legends>
<dcwc:Legend Name="Default" Enabled="False" AutoFitText="False" BackColor="Transparent" Font="Trebuchet MS, 8.25pt, style=Bold"></dcwc:Legend>
</Legends>
	<Titles>
<dcwc:Title Name="Title1" Text="Statistics" Font="Trebuchet MS, 8pt, style=Bold" Color="26, 59, 105" Alignment="TopCenter" ShadowOffset="2" ShadowColor="32, 0, 0, 0"></dcwc:Title>
</Titles>
	<Series>
<dcwc:Series Name="Sales" BorderColor="180, 26, 59, 105"></dcwc:Series>
</Series>
	<ChartAreas>
<dcwc:ChartArea BackColor="OldLace" BackGradientType="TopBottom" BackGradientEndColor="White" ShadowColor="Transparent" BorderColor="64, 64, 64, 64" BorderStyle="Solid" Name="Default">
<AxisY LineColor="64, 64, 64, 64">
<MajorGrid LineColor="64, 64, 64, 64"></MajorGrid>

<LabelStyle Font="Trebuchet MS, 8.25pt, style=Bold" Format="$#0,k"></LabelStyle>
</AxisY>

<AxisX LabelsAutoFit="False" LineColor="64, 64, 64, 64">
<MajorGrid LineColor="64, 64, 64, 64"></MajorGrid>

<LabelStyle Font="Trebuchet MS, 8.25pt, style=Bold" Enabled="False"></LabelStyle>
</AxisX>

<Area3DStyle RightAngleAxes="False" Clustered="True" Perspective="10" XAngle="15" YAngle="10" WallWidth="0"></Area3DStyle>
</dcwc:ChartArea>
</ChartAreas>
</dcwc:chart>
    </div>
    </form>
</body>
</html>
