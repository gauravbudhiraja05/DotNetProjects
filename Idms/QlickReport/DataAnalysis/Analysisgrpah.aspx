<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Analysisgrpah.aspx.vb" Inherits="DataAnalysis_Analysisgrpah" %>
<%@ Register TagPrefix="dcwc" Namespace="Dundas.Charting.WebControl" Assembly="DundasWebChart" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" lang="en-us" xml:lang="en-us">
<head runat="server">
    <title>Analysis Graph</title>
    <link rel="Stylesheet" href="../App_Themes/Themes/StyleSheet.css" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <table width="100%" summary="Analysis Graph"> 
    <tr>
        <td align="center" scope="col">
            <asp:DropDownList ID="ddlanalysistable" runat="server" AutoPostBack="true" CssClass="leftdropdownlist" Width="120px">
                   <asp:ListItem Value="Correlation">Correlation</asp:ListItem>
                   <asp:ListItem Value="Regression">Regression</asp:ListItem>
             </asp:DropDownList> 
        </td>
        <%--<td>
								<asp:DropDownList ID="ddlChartwidth" runat="server" Height="22px" CssClass="leftdropdownlist">
                                <asp:ListItem Value="100%">100%</asp:ListItem>
                                <asp:ListItem Value="200%">200%</asp:ListItem>
                                 <asp:ListItem Value="300%">300%</asp:ListItem>
                                <asp:ListItem Value="400%">400%</asp:ListItem>
                                <asp:ListItem Value="500%">500%</asp:ListItem>
                                <asp:ListItem Value="500%">1000%</asp:ListItem>
                                 </asp:DropDownList>
								</td>--%>
    </tr>
        <tr>
            <td align="center" scope="col">    
                <dcwc:Chart ID="Chart1" runat="server" BackColor="#FAE6E6" BackGradientEndColor="255, 240, 240" alt="Default Chart"
                BackGradientType="DiagonalLeft" BorderLineColor="LightSlateGray" BorderLineStyle="Solid" 
                Palette="Kindergarten" Width="800px" Height="600px" >
                <Legends>
                    <dcwc:Legend BackColor="100, 255, 255, 255" BackGradientEndColor="245, 224, 224"
                        BorderColor="100, 0, 0, 0" LegendStyle="Column" Name="Default">
                    </dcwc:Legend>
                </Legends>
                <UI>
                    <Toolbar BackColor="255, 240, 240" BorderColor="75, 0, 0, 0">
                        <BorderSkin PageColor="Transparent" SkinStyle="Raised" />
                    </Toolbar>
                </UI>
                <Titles>
                    <dcwc:Title Color="DodgerBlue" Font="Microsoft Sans Serif, 9.75pt" Name="Title1"
                        Style="emboss" >
                    </dcwc:Title>
                </Titles>
                 <ChartAreas>
                                <dcwc:ChartArea BackColor="100, 255, 255, 255" BackGradientEndColor="245, 224, 224"
                        BorderColor="100, 0, 0, 0" BorderStyle="Solid" Name="Chart Area 1" >
                                <AxisY2>
										<MajorGrid Enabled="False"></MajorGrid>
									</AxisY2>
									<Area3DStyle YAngle="10" Perspective="10" XAngle="15" RightAngleAxes="False" WallWidth="0" Clustered="True"></Area3DStyle>
									<AxisY LineColor="64, 64, 64, 64" LabelsAutoFit="False">
										<LabelStyle Font="Trebuchet MS, 8.25pt, style=Bold"></LabelStyle>
										<MajorGrid LineColor="64, 64, 64, 64"></MajorGrid>
									</AxisY>
									<AxisX LineColor="64, 64, 64, 64" LabelsAutoFit="False">
										<LabelStyle Font="Trebuchet MS, 8.25pt, style=Bold"></LabelStyle>
										<MajorGrid LineColor="64, 64, 64, 64"></MajorGrid>
										<MajorTickMark Size="2" Enabled="False"></MajorTickMark>
									</AxisX>
                                   </dcwc:ChartArea>
                                    
                                </ChartAreas>
                <BorderSkin FrameBackColor="SteelBlue" FrameBackGradientEndColor="LightBlue" FrameBorderColor="100, 0, 0, 0"
                    FrameBorderWidth="2" PageColor="Transparent" SkinStyle="Emboss" />
            </dcwc:Chart>
            </td>
        </tr>
    </table>
   
    </div>
    </form>
</body>
</html>
