<%--Project Name: IDMS Phase 2
    Module Name: Advance Report Designer
    Page Name: View Graphs
    Summary: Open existing graphs attached with a saved report
    Created on: 10/08/08
    Created By: Ekta Goyal

--%>

<%@ Page Language="VB" AutoEventWireup="false" CodeFile="viewGraphs.aspx.vb" Inherits="ReportDesigner_viewGraphs" %>
<%@ Register TagPrefix="dcwc" Namespace="Dundas.Charting.WebControl" Assembly="DundasWebChart" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" lang="EN" >
<head runat="server">
    <title>ViewGraph</title>
    <link href="../App_Themes/Themes/StyleSheet.css" type="text/css" rel="stylesheet" />
    <script type="text/javascript" language="javascript">
          var rep="";
        function load()
        {
            var str=window.opener.document.getElementById("hidReportname").value;
            document.getElementById("hidrepname").value=window.opener.document.getElementById("hidReportname").value;
            document.getElementById("hiddept").value=window.opener.document.getElementById("hidDepartment").value;
            document.getElementById("hidclient").value=window.opener.document.getElementById("hidClient").value;
            document.getElementById("hidlob").value=window.opener.document.getElementById("hidLob").value;
//            document.getElementById("hidtodate").value=window.opener.document.getElementById("hidStart").value;
//            document.getElementById("hidfromdate").value=window.opener.document.getElementById("hidEnd").value;
           rep=str;
        }
              
    </script>
</head>
<body  onload="load();">
    <form id="frmViewgraph" runat="server">
    <div id="divcaption" align="center" style="padding:20px;" >
   
   <table summary ="table">
   <tr>
    <td style="width: 550px" align="center" colspan="3" scope ="colgroup" >
         <%--<input id="btn" runat="server"   type="button"  class="button" value="Create New Graph>>" style="width: 144px" />--%>    
        <asp:Button ID="btngraph" runat="server" Visible="false"  Text="More Options" class="button" Width="152px" /> 
     <label for="ddlchart"></label>      
<asp:DropDownList ID="ddlchart"  runat="server"  AutoPostBack="true" CssClass="leftdropdownlist" Width="120px">
           <asp:ListItem Value="100%">100%</asp:ListItem>
            <asp:ListItem Value="125%">125%</asp:ListItem>
             <asp:ListItem Value="150%">150%</asp:ListItem>
              <asp:ListItem Value="175%">175%</asp:ListItem>
               <asp:ListItem Value="200%">200%</asp:ListItem>              
                </asp:DropDownList>
    </td>
    </tr>
   <tr> 
        <td valign="top" colspan="3" style="width: 550px" scope ="colgroup" >
           
            <dcwc:Chart ID="Chart1" runat="server" Visible="false" alt="Default Chart"
                BackGradientType="DiagonalLeft" BackGradientEndColor="lightslategray"  BorderLineColor="LightSlateGray" BorderLineStyle="Solid" 
                Palette="Kindergarten" Width="800px" Height="800px" MapEnabled="False" >
                <Legends>
<dcwc:Legend Name="Default" LegendStyle="Column" BackColor="100, 255, 255, 255" BorderColor="100, 0, 0, 0" BackGradientEndColor="245, 224, 224">
<Position X="79.4468155" Y="8.983587" Width="16.1451817" Height="3.25406766"></Position>
</dcwc:Legend>
</Legends>
                <UI>
<ContextMenu><Items>
<dcwc:CommandUIItem CommandName="Separator"></dcwc:CommandUIItem>
<dcwc:CommandUIItem CommandName="Separator"></dcwc:CommandUIItem>
<dcwc:CommandUIItem CommandName="Separator"></dcwc:CommandUIItem>
<dcwc:CommandUIItem CommandName="Separator"></dcwc:CommandUIItem>
<dcwc:CommandUIItem CommandName="Separator"></dcwc:CommandUIItem>
<dcwc:CommandUIItem CommandName="Separator"></dcwc:CommandUIItem>
<dcwc:CommandUIItem CommandName="Separator"></dcwc:CommandUIItem>
<dcwc:CommandUIItem CommandName="Separator"></dcwc:CommandUIItem>
<dcwc:CommandUIItem CommandName="Separator"></dcwc:CommandUIItem>
<dcwc:CommandUIItem CommandName="Separator"></dcwc:CommandUIItem>
<dcwc:CommandUIItem CommandName="Separator"></dcwc:CommandUIItem>
<dcwc:CommandUIItem CommandName="Separator"></dcwc:CommandUIItem>
<dcwc:CommandUIItem CommandName="Separator"></dcwc:CommandUIItem>
</Items>
</ContextMenu>

<Toolbar BackColor="255, 240, 240" BorderColor="75, 0, 0, 0"><Items>
<dcwc:CommandUIItem CommandName="Separator"></dcwc:CommandUIItem>
<dcwc:CommandUIItem CommandName="Separator"></dcwc:CommandUIItem>
<dcwc:CommandUIItem CommandName="Separator"></dcwc:CommandUIItem>
<dcwc:CommandUIItem CommandName="Separator"></dcwc:CommandUIItem>
<dcwc:CommandUIItem CommandName="Separator"></dcwc:CommandUIItem>
<dcwc:CommandUIItem CommandName="Separator"></dcwc:CommandUIItem>
<dcwc:CommandUIItem CommandName="Separator"></dcwc:CommandUIItem>
<dcwc:CommandUIItem CommandName="Separator"></dcwc:CommandUIItem>
<dcwc:CommandUIItem CommandName="Separator"></dcwc:CommandUIItem>
<dcwc:CommandUIItem CommandName="Separator"></dcwc:CommandUIItem>
<dcwc:CommandUIItem CommandName="Separator"></dcwc:CommandUIItem>
</Items>

<BorderSkin PageColor="Transparent" SkinStyle="Raised"></BorderSkin>
</Toolbar>
<Commands>
<dcwc:Command CommandType="LoadGroup"><SubCommands>
<dcwc:Command CommandType="Separator"></dcwc:Command>
</SubCommands>
</dcwc:Command>
</Commands>
</UI>
                <Titles>
<dcwc:Title Name="Title1" Text="Column Chart" Style="Emboss" Font="Microsoft Sans Serif, 9.75pt" Color="DodgerBlue">
<Position X="3.93867326" Y="3.93867326" Width="91.65332" Height="2.044914"></Position>
</dcwc:Title>
</Titles>
                <mapareas>
<dcwc:MapArea Coordinates="0,0,0,0" MapAreaAttributes="alt='Alternative text'"></dcwc:MapArea>
</mapareas>
                <ChartAreas>
<dcwc:ChartArea BackColor="100, 255, 255, 255" BackGradientEndColor="245, 224, 224" ShadowColor="Transparent" BorderColor="100, 0, 0, 0" BorderStyle="Solid" Name="Default">
<AxisY>
<MajorGrid IntervalOffset="Auto" IntervalOffsetType="Auto" Interval="Auto" IntervalType="Auto" LineColor="50, 0, 0, 0"></MajorGrid>

<MinorGrid LineColor="20, 0, 0, 0"></MinorGrid>

<MajorTickMark IntervalOffset="Auto" IntervalOffsetType="Auto" Interval="Auto" IntervalType="Auto" LineColor="150, 0, 0, 0" LineWidth="2"></MajorTickMark>

<MinorTickMark Size="2" LineColor="75, 0, 0, 0"></MinorTickMark>

<LabelStyle IntervalOffset="Auto" IntervalOffsetType="Auto" Interval="Auto" IntervalType="Auto" FontColor="180, 0, 0, 0"></LabelStyle>
</AxisY>
<%--<AxisX LineColor="64, 64, 64, 64" LabelsAutoFit="False">
<LabelStyle Font="Trebuchet MS, 8.25pt, style=Bold"></LabelStyle>

<MajorTickMark Enabled="False" Size="2"></MajorTickMark>

<MajorGrid LineColor="64, 64, 64, 64"></MajorGrid>
</AxisX>--%>

<AxisX LineColor="64, 64, 64, 64" >
<MajorGrid LineColor="64, 64, 64, 64"></MajorGrid>

<MinorGrid LineColor="20, 0, 0, 0"></MinorGrid>

<%--<MajorTickMark IntervalOffset="Auto" IntervalOffsetType="Auto" Interval="Auto" IntervalType="Auto" LineColor="150, 0, 0, 0" LineWidth="2"></MajorTickMark>
--%>
<MajorTickMark Enabled="False" Size="2"></MajorTickMark>

<MinorTickMark Size="2" LineColor="75, 0, 0, 0"></MinorTickMark>
<LabelStyle Font="Trebuchet MS, 8.25pt, style=Bold"></LabelStyle>

<%--<LabelStyle IntervalOffset="Auto" IntervalOffsetType="Auto" Interval="Auto" IntervalType="Auto" FontColor="180, 0, 0, 0" Format="G"></LabelStyle>
--%>

</AxisX>

<AxisX2>
<MajorGrid IntervalOffset="Auto" IntervalOffsetType="Auto" Interval="Auto" IntervalType="Auto"></MajorGrid>

<MajorTickMark IntervalOffset="Auto" IntervalOffsetType="Auto" Interval="Auto" IntervalType="Auto"></MajorTickMark>

<LabelStyle IntervalOffset="Auto" IntervalOffsetType="Auto" Interval="Auto" IntervalType="Auto"></LabelStyle>
</AxisX2>

<AxisY2>
<MajorGrid IntervalOffset="Auto" IntervalOffsetType="Auto" Interval="Auto" IntervalType="Auto"></MajorGrid>

<MajorTickMark IntervalOffset="Auto" IntervalOffsetType="Auto" Interval="Auto" IntervalType="Auto"></MajorTickMark>

<LabelStyle IntervalOffset="Auto" IntervalOffsetType="Auto" Interval="Auto" IntervalType="Auto"></LabelStyle>
</AxisY2>

<Position X="3.93867326" Y="8.983587" Width="72.50814" Height="86.6084061"></Position>

<InnerPlotPosition Width="100" Height="100"></InnerPlotPosition>

<Area3DStyle RightAngleAxes="False" Clustered="True" WallWidth="0"></Area3DStyle>
</dcwc:ChartArea>
</ChartAreas>
                <BorderSkin FrameBackColor="SteelBlue" FrameBackGradientEndColor="LightBlue" FrameBorderColor="100, 0, 0, 0"
                    FrameBorderWidth="2" PageColor="Transparent" SkinStyle="Emboss" />
            </dcwc:Chart>
       </td>
   </tr>
   
    <tr>
        <td colspan="3" style="width: 550px" scope ="colgroup" >
            <dcwc:chart id="Chart2" runat="server" Width="412px" Height="296px" BackColor="#F3DFC1" Palette="Kindergarten" BorderLineStyle="Solid" BackGradientType="TopBottom" BorderLineWidth="2" BorderLineColor="181, 64, 1" ImageType="Png" alt="Default Chart"  ImageUrl="~/TempImages/ChartPic_#SEQ(300,3)" Visible="False" MapEnabled="False">
					<Titles>
<dcwc:Title Name="Title1" Text="" Style="Emboss" Font="Microsoft Sans Serif, 9.75pt" Color="DodgerBlue"></dcwc:Title>
</Titles>	

							<legends>
<dcwc:Legend Name="Default" Enabled="False" AutoFitText="False" BackColor="Transparent" Font="Trebuchet MS, 8.25pt, style=Bold"></dcwc:Legend>
</legends>
							<series>
<dcwc:Series Name="DataDistribution" XValueType="Double" YValueType="Double" ChartType="Point" Color="120, 252, 180, 65" BorderColor="110, 26, 59, 105" Font="Trebuchet MS, 8.25pt" MarkerSize="8"></dcwc:Series>
<dcwc:Series Name="Histogram" XValueType="Double" YValueType="Double" ChartArea="HistogramArea" ShowLabelAsValue="True" Color="224, 64, 10" BorderColor="180, 26, 59, 105" Font="Trebuchet MS, 8.25pt"></dcwc:Series>
</series>
 
                <mapareas>
<dcwc:MapArea Coordinates="0,0,0,0" MapAreaAttributes="alt='Alternative text'"></dcwc:MapArea>
</mapareas>
							<chartareas>
<dcwc:ChartArea AlignWithChartArea="HistogramArea" BackColor="OldLace" BackGradientType="TopBottom" BackGradientEndColor="White" ShadowColor="Transparent" BorderColor="64, 64, 64, 64" BorderStyle="Solid" Name="Default">
<AxisY LineColor="64, 64, 64, 64" Reverse="True" Maximum="2" Minimum="0">
<MajorGrid LineColor="64, 64, 64, 64" Enabled="False"></MajorGrid>

<MajorTickMark Enabled="False"></MajorTickMark>

<LabelStyle Font="Trebuchet MS, 8.25pt, style=Bold" Enabled="False"></LabelStyle>
</AxisY>

<AxisX LabelsAutoFit="False" Title="" TitleFont="Trebuchet MS, 8pt" LineColor="64, 64, 64, 64" Enabled="True">
<MajorGrid LineColor="64, 64, 64, 64"></MajorGrid>

<MajorTickMark Size="1.5" LineColor="Transparent" Enabled="False"></MajorTickMark>

<LabelStyle Font="Trebuchet MS, 8.25pt, style=Bold" Enabled="False"></LabelStyle>
</AxisX>

<AxisY2 LabelsAutoFit="False">
<LabelStyle Font="Trebuchet MS, 8.25pt, style=Bold"></LabelStyle>
</AxisY2>

<Position X="3" Y="4" Width="96" Height="15"></Position>

<Area3DStyle RightAngleAxes="False" Clustered="True" Perspective="10" XAngle="15" YAngle="10" WallWidth="0"></Area3DStyle>
</dcwc:ChartArea>
<dcwc:ChartArea BackColor="OldLace" BackGradientType="TopBottom" BackGradientEndColor="White" ShadowColor="Transparent" BorderColor="64, 64, 64, 64" BorderStyle="Solid" Name="HistogramArea">
<AxisY LineColor="64, 64, 64, 64">
<MajorGrid LineColor="64, 64, 64, 64"></MajorGrid>

<LabelStyle Font="Trebuchet MS, 8.25pt, style=Bold"></LabelStyle>
</AxisY>

<AxisX LabelsAutoFit="False" Title="" TitleFont="Trebuchet MS, 8pt" LineColor="64, 64, 64, 64">
<MajorGrid LineColor="64, 64, 64, 64"></MajorGrid>

<LabelStyle Font="Trebuchet MS, 8.25pt, style=Bold"></LabelStyle>
</AxisX>

<AxisY2 LabelsAutoFit="False">
<LabelStyle Font="Trebuchet MS, 8.25pt, style=Bold"></LabelStyle>
</AxisY2>

<Position X="3" Y="18" Width="93" Height="77"></Position>

<Area3DStyle RightAngleAxes="False" Clustered="True" Perspective="10" XAngle="15" YAngle="10" WallWidth="0"></Area3DStyle>
</dcwc:ChartArea>
</chartareas>
							<borderskin skinstyle="Emboss"></borderskin>
						</dcwc:chart>
        </td>
    </tr>
    <tr>
        <td colspan="3" style="width: 550px" align="center" scope ="colgroup" >
           <dcwc:ChartToolbar ID="ChartToolbar1" runat="server" cellpadding="0" cellspacing="0"
                 Height="44px" onclick="return(false);" Visible="false" PageColor="" Width="296px" ChartID="Chart3" />
            <dcwc:Chart ID="Chart3" runat="server" Visible="false" BackColor="#FAE6E6" BackGradientEndColor="255, 240, 240" alt="Default Chart"
                BackGradientType="DiagonalLeft" BorderLineColor="LightSlateGray" BorderLineStyle="Solid" 
                Palette="Kindergarten" Width="800px" Height="800px" MapEnabled="False" >
                <Legends>
<dcwc:Legend Name="Default" LegendStyle="Column" BackColor="100, 255, 255, 255" BorderColor="100, 0, 0, 0" BackGradientEndColor="245, 224, 224"></dcwc:Legend>
</Legends>
                <UI>
<Toolbar BackColor="255, 240, 240" BorderColor="75, 0, 0, 0">
<BorderSkin PageColor="Transparent" SkinStyle="Raised"></BorderSkin>
</Toolbar>
</UI>
                <Titles>
<dcwc:Title Name="Title1" Text="Column Chart" Style="Emboss" Font="Microsoft Sans Serif, 9.75pt" Color="DodgerBlue"></dcwc:Title>
</Titles>
                <mapareas>
<dcwc:MapArea Coordinates="0,0,0,0" MapAreaAttributes="alt='Alternative text'"></dcwc:MapArea>
</mapareas>
                <ChartAreas>
<dcwc:ChartArea BackColor="100, 255, 255, 255" BackGradientEndColor="245, 224, 224" BorderColor="100, 0, 0, 0" BorderStyle="Solid" Name="Default">
<AxisY>
<MajorGrid LineColor="50, 0, 0, 0"></MajorGrid>

<MinorGrid LineColor="20, 0, 0, 0"></MinorGrid>

<MajorTickMark LineColor="150, 0, 0, 0" LineWidth="2"></MajorTickMark>

<MinorTickMark Size="2" LineColor="75, 0, 0, 0"></MinorTickMark>

<LabelStyle FontColor="180, 0, 0, 0"></LabelStyle>
</AxisY>

<AxisX LineColor="64, 64, 64, 64" Interval="1">
<MajorGrid LineColor="50, 0, 0, 0"></MajorGrid>

<MinorGrid LineColor="20, 0, 0, 0"></MinorGrid>

<MajorTickMark Style="None" LineColor="150, 0, 0, 0" LineWidth="2"></MajorTickMark>

<MinorTickMark Size="2" LineColor="75, 0, 0, 0"></MinorTickMark>

<LabelStyle FontColor="180, 0, 0, 0" Format="# K"></LabelStyle>
</AxisX>

<Area3DStyle WallWidth="0"></Area3DStyle>
</dcwc:ChartArea>
</ChartAreas>
                <BorderSkin FrameBackColor="SteelBlue" FrameBackGradientEndColor="LightBlue" FrameBorderColor="100, 0, 0, 0"
                    FrameBorderWidth="2" PageColor="Transparent" SkinStyle="Emboss" />
            </dcwc:Chart>
        </td>
    </tr>
    <tr>
        <td id="charttd" runat="server" colspan="3" style="width: 550px"> 
            <%
                 Repgraph()
            %>
        </td>
    </tr>
    
   </table>     
 </div>
          <input type="hidden" runat="server" name="hidReporttype" id="hidReporttype" />
          <input type="hidden" runat="server" name="hidrepname" id="hidrepname" />
          <input type="hidden" runat="server" name="hiddept" id="hiddept" />
          <input type="hidden" runat="server" name="hidclient" id="hidclient" />
          <input type="hidden" runat="server" name="hidtodate" id="hidtodate" />
          <input type="hidden" runat="server" name="hidfromdate" id="hidfromdate" />
          <input type="hidden" runat="server" name="hidlob" id="hidlob" />
           <input type="hidden" runat="server" name="formulaarray" id="formulaarray" /><%-- Hidden report type --%>
           
    </form>
</body>

</html>



