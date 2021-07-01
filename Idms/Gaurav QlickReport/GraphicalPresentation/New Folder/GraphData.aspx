<%--Project Name: IDMS Phase 2
    Module Name: graphical Presentation
    Page Name: GraphData
    Summary: Create different types of chart on the basis of DataAnalysis and Advance Report Designer
    Created on: 10/03/08
    Created By: Ekta Goyal

--%>

<%@ Page AutoEventWireup="false" EnableEventValidation="false" ValidateRequest="false"  EnableViewState="true" CodeFile="GraphData.aspx.vb" Inherits="GraphData" Language="VB" MasterPageFile="~/MasterPage/MasterPage.master" Title="Graph Data" %>
<%@ Register TagPrefix="dcwc" Namespace="Dundas.Charting.WebControl" Assembly="DundasWebChart" %>

<asp:Content ID="leftPane" runat="server" ContentPlaceHolderID="LeftPlaceHolder">
    <%--This java script file show the div in left content page--%>

    <script language="javascript" src="../js/202pop.js" type="text/javascript"></script>
    <script language="Javascript" src="../js/collapseableDIV.js" type="text/javascript"></script>
  <script language="JavaScript" type="text/javascript" src="../js/picker.js"></script>   <%--To allow the user to chose among a wide range of colors--%>
   
    

<script language="javascript" type="text/javascript">

function gridshow()
{
window.open("showgraph.aspx","ViewGraph",'height=500px');
//    document.forms[0].target ="grid";	// open in same window	
//    		document.forms[0].action="showgraph.aspx";
//    document.forms[0].submit();
//		document.forms[0].target="_self"; // Call in same window
}
 function ShowCalendar()
		{
		     //debugger;
		     window.txtBox = document.getElementById('<%=txtfromdate.ClientID%>');
		     window.txtBox.value = window.showModalDialog('../Calendar/Calendar.htm',window.txtBox.value,'dialogLeft:200px;dialogTop:200px;dialogHeight:210px;dialogWidth:265px;center:No;help:No;scroll:No;resizable:No;status:No;');
				if (window.txtBox.value=="undefined")			
				{
				    window.txtBox.value="";    
				}
		}
		function ShowCalendar1()
		{
		     //debugger;
		     window.txtBox = document.getElementById('<%=txttodate.ClientID%>');			 
			 window.txtBox.value = window.showModalDialog('../Calendar/Calendar.htm',window.txtBox.value,'dialogLeft:200px;dialogTop:200px;dialogHeight:210px;dialogWidth:265px;center:No;help:No;scroll:No;resizable:No;status:No;');
            if (window.txtBox.value=="undefined")			
			{
			    window.txtBox.value="";    
			}
	    
	    }
	    function Opengraph()// Open Existing  Graph
	{
	
			window.open("OpenGraph.aspx","OpenGraph","width=420,height=230,scrollbars=no,status=yes");
	}
    function Savegraph()//Save Graph
	{
	        getValues();
	        //debugger;
			window.open("Savegraph.aspx","SaveGraph","width=400,height=220,scrollbars=yes,status=yes");
	}
	function getValues()
	{
	   document.getElementById("abc").value="XAngle:" + document.getElementById("<%=ddlXangle.ClientID%>").value  + "$" + "Perspective:" + document.getElementById("<%=ddlPerspective.ClientID%>").value + "$" + "CheckBoxShow3D:"+ document.getElementById("<%=chkShow3D.ClientID%>").value + "$" + "Yangle:"+ document.getElementById("<%=ddlYang.ClientID%>").value + "$" + " Palettes:" + document.getElementById("<%=ddlPalettes.ClientID%>").value + "$" + + "Backcolour:" + document.getElementById("<%=bkcolor.ClientID%>").value + "$" + "Gradient:" + document.getElementById("<%=ddlGradient.ClientID%>").value + "$" + "Hatchstyle:" + document.getElementById("<%=ddlHatchstyle.ClientID%>").value + "$" +  "Bordercolor:" + document.getElementById("<%=titlebordercolor.ClientID%>").value + "$" +  "Bordersize:" + document.getElementById("<%=ddlBordersize.ClientID%>").value + "$" + "Borderstyle:" + document.getElementById("<%=ddlBorderstyle.ClientID%>").value + "$" + "Xlabelfont:" + document.getElementById("<%=ddlXlabelfont.ClientID%>").value+ "$" + "Xfontsizelist:" + document.getElementById("<%=ddlXfontsizelist.ClientID%>").value + "$" + "XLabelcolour:" +  document.getElementById("<%=xaxiscolor.ClientID%>").value + "$" + "Xontanglelist:" + document.getElementById("<%=ddlXontanglelist.ClientID%>").value+ "$" + "OffsetLabels:" + document.getElementById("<%=chkXoffset.ClientID%>").value+ "$" + "EnableLabels:" + document.getElementById("<%=chkXenable.ClientID%>").value+ "$" + "Yfontname:" + document.getElementById("<%=ddlYfontname.ClientID%>").value + "$" + "Ylabelfontsize:" + document.getElementById("<%=ddlYlabelfontsize.ClientID%>").value + "$" + "Yfontcolour:" + document.getElementById("<%=yaxiscolor.ClientID%>").value + "$" + "Yangle:" + document.getElementById("<%=ddlYangle.ClientID%>").value + "$" + "chkYoffset:" + document.getElementById("<%=chkYoffset.ClientID%>").value + "$" + "Yenable:" + document.getElementById("<%=chkYenable.ClientID%>").value + "$" + "Charttitle:" + document.getElementById("<%=txtCharttitle.ClientID%>").value + "$" + "XAxisTitle:" + document.getElementById("<%=txtTitleext.ClientID%>").value + "$" + "Yaxistitle:" + document.getElementById("<%=txtYTitle.ClientID%>").value + "$" + "Titlesize:" + document.getElementById("<%=ddlTitlesize.ClientID%>").value + "$" + "Font:" + document.getElementById("<%=ddlFont1.ClientID%>").value + "$" + "Color:" + document.getElementById("<%=titlefontcolor.ClientID%>").value + "$" + "BorderColor:" + document.getElementById("<%=titlebordercolor.ClientID%>").value + "$" + "BackColor:" + document.getElementById("<%=titlebkcolor.ClientID%>").value + "$" + "Alignment:" + document.getElementById("<%=Alignment.ClientID%>").value + "$" + "Italic:" + document.getElementById("<%=chkItalic.ClientID%>").value + "$" + "Bold:" + document.getElementById("<%=chkBold.ClientID%>").value + "$"  + "Underline:" + document.getElementById("<%=chkUline.ClientID%>").value + "$" + "Strikeout:" + document.getElementById("<%=chkSout.ClientID%>").value + "$" + "Majorgridline:" + document.getElementById("<%=ddlMajorgridline.ClientID%>").value+ "$" + "Linetypes:" + document.getElementById("<%=ddlLinetypes.ClientID%>").value + "$" + "Majorgridcolour:" + document.getElementById("<%=Majorgridcolour.ClientID%>").value + "$" + "Majorline:" + document.getElementById("<%=ddlMajorline.ClientID%>").value + "$" + "MajorInterval:" + document.getElementById("<%=ddlMajorInterval.ClientID%>").value + "$" + "MinorType:" + document.getElementById("<%=ddlMinorType.ClientID%>").value + "$" + "MinorColor:" + document.getElementById("<%=ddlMinorColor1.ClientID%>").value + "$" + "MinorWidth:" + document.getElementById("<%=ddlMinorWidth.ClientID%>").value + "$" + "MinorInterval:" + document.getElementById("<%=ddlMinorInterval.ClientID%>").value;
	   var chartname=document.getElementById("<%=hidchart.ClientID%>").value;
	  
	   if(chartname=="Pie:") 
	   {
	    document.getElementById("prp").value="Linetype:"+document.getElementById("<%=ddlLabelstylelist.ClientID%>").value;
	   }
	     	var Repname;
	     	var currentspan;
	     	var currentclientspan;
	     	var currentlobsapn;
	     	if(document.getElementById("<%=rbnReport.ClientID%>").checked==true)
	     	{
	     	       if(("<%=currsp%>")=="")
	     	        {
	     	            Repname=document.getElementById("<%=ddlReport.ClientID%>").options[document.getElementById("<%=ddlReport.ClientID%>").selectedIndex].text;
	     	            currentspan=document.getElementById("<%=ddldepartmant.ClientID%>").value;
	     	            currentclientspan=document.getElementById("<%=ddlclient.ClientID%>").value;
	     	            currentlobsapn=document.getElementById("<%=ddllob.ClientID%>").value;
	     	        }
  	     	        else 
	     	        {
	     	            Repname=document.getElementById("<%=txtcurrentreport.ClientID%>").value;
	     	            currentspan=document.getElementById("<%=Currentrepdept.ClientID%>").value; 
                        currentclientspan=document.getElementById("<%=Currentrepclient.ClientID%>").value;
                        currentlobsapn=document.getElementById("<%=Currentreplob.ClientID%>").value;
	     	        }
	    	}
	     	else
	     	{
	     	 Repname=document.getElementById("<%=ddlAnalysistable.ClientID%>").value;
	     	}

	   document.getElementById("Report").value=Repname;
	   document.getElementById("department").value=currentspan;
	   document.getElementById("Graph").value=chartname;
	   document.getElementById("client").value=currentclientspan;
	   document.getElementById("lob").value=currentlobsapn;
	   document.getElementById("ToDate").value=document.getElementById("<%=txtTodate.ClientID %>").value;
	   document.getElementById("FromDate").value=document.getElementById("<%=txtFromdate.ClientID %>").value;
	   document.getElementById("ColumnName").value=document.getElementById("<%=hid.ClientID%>").value;
	   document.getElementById("ColumnSeries").value=document.getElementById("<%=seriesbtn.ClientID%>").value;
	   document.getElementById("totalcolumn").value=document.getElementById("<%=totalcol.ClientID%>").value;
	   document.getElementById("SavedBy").value=("<%=session("Userid")%>");
//	   if chartname="Area"
//	   {
//	   
//	   }
//	   // alert(document.getElementById("abc").value);
        
        

	}
    </script>
 
<div id="divScroll"  style="height:700px; width:220px; overflow:scroll; scrollbar-arrow-color:Aqua; scrollbar-base-color:#59afbb;">
   <div id="div1" onclick="toggleDiv('divChart', 'imgChart',500)" style="cursor: pointer;
        background-color: #1C3E42; color: White;">
        <table>
            <tr>
                <td style="width: 190px">
                    <strong>SelectChart</strong>
                </td>
                <td align="right">
                    <img id="imgChart" alt="Expand/Collapse" src="../images/ArrowDown.gif" />
                </td>
            </tr>
        </table>
    </div>
   <div id="divChart" style="overflow: hidden; display: none;">
        <table style="vertical-align: text-top; border: 1;" width="80%">
            <tr>
                <td align="center" style="height: 15px" valign="top">
                <asp:ImageButton ID="imgcolumn" runat="server" alt="Click Here To Select Column Chart"
                        src="../images/Chart/ColumnChart.jpg"
                        Style="filter: alpha(opacity=50); -moz-opacity: 0.5; height: 30px; width: 30;" />
                </td>
                    
                        
                 <td align="left" style="height: 11px; color: #ffffff; font-weight: bold; font-size: 10px;
                    font-family: Verdana; width: 282px;" valign="middle">
                    <asp:LinkButton ID="lnkColumnchart" runat="server" Text="Column chart" ToolTip="Click On Column Chart"></asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td colspan="2" style="background-color: #86c1cc">
                </td>
            </tr>
            <tr>
                <td style="width: 46px; height: 15px;" valign="middle">
                    <asp:ImageButton ID="imgArea" runat="server" alt="Click Here To Select Area Chart"
                        src="../images/Chart/AreaChart.jpg"
                        Style="filter: alpha(opacity=50); -moz-opacity: 0.5; height: 30px; width: 30;" />
                </td>
                <td align="left" style="height: 15px; color: #ffffff; font-weight: bold; font-size: 10px;
                    font-family: Verdana; width: 282px;">
                    <asp:LinkButton ID="LinkButton2" runat="server" Text="Area chart" ToolTip="Click On Area Chart"></asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td bgcolor="#86c1cc" colspan="2">
                </td>
            </tr>
            <tr>
                <td style="width: 46px; height: 15px;">
                    <asp:ImageButton ID="imgPie" runat="server" alt="Click Here To Select Pie Chart"
                        src="../images/Chart/PieChart.jpg"
                       Style="filter: alpha(opacity=50); -moz-opacity: 0.5; height: 30px; width: 30;" />
                </td>
                <td align="left" style="height: 15px; color: #ffffff; font-weight: bold; font-size: 10px;
                    font-family: Verdana; width: 282px;">
                    <asp:LinkButton ID="LinkButton3" runat="server" Text="Pie chart" ToolTip="Click On Pie Chart"></asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td bgcolor="#86c1cc" colspan="2">
                </td>
            </tr>
            <tr>
                <td style="width: 46px; height: 15px;">
                    <asp:ImageButton ID="imgLine" runat="server" alt="Click Here To Select Line Chart"
                        src="../images/Chart/LineChart.jpg"
                        Style="filter: alpha(opacity=50); -moz-opacity: 0.5; height: 30px; width: 30;" />
                </td>
                <td align="left" style="height: 15px; color: #ffffff; font-weight: bold; font-size: 10px;
                    font-family: Verdana; width: 282px;">
                    <asp:LinkButton ID="LinkButton4" runat="server" Text="Line chart" ToolTip="Click On Line Chart"></asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td bgcolor="#86c1cc" colspan="2">
                </td>
            </tr>
            <tr>
                <td style="width: 46px; height: 15px;">
                    <asp:ImageButton ID="imgScatter" runat="server" alt="Click Here To Select Scatter Chart"
                        src="../images/Chart/ScatterChart.jpg"
                        Style="filter: alpha(opacity=50); -moz-opacity: 0.5; height: 30px; width: 30;" />
                </td>
                <td align="left" style="height: 15px; color: #ffffff; font-weight: bold; font-size: 10px;
                    font-family: Verdana; width: 282px;">
                    <asp:LinkButton ID="LinkButton5" runat="server" Text="XY(Scatter) chart" Width="152px" ToolTip="Click On Scatter Chart"></asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td bgcolor="#86c1cc" colspan="2" >
                </td>
            </tr>
            <tr>
                <td style="width: 46px; height: 15px;">
                    <asp:ImageButton ID="imgHistogram" runat="server" alt="Click Here To Select Histogram Chart"
                        src="../images/Chart/HistogramChart.jpg"
                        Style="filter: alpha(opacity=50); -moz-opacity: 0.5; height: 30px; width: 30;" />
                </td>
                <td align="left" style="height: 15px; color: #ffffff; font-weight: bold; font-size: 10px;
                    font-family: Verdana; width: 282px;">
                    <asp:LinkButton ID="LinkButton6" runat="server" Text="Histogram chart" ToolTip="Click On Histogram Chart"></asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td bgcolor="#86c1cc" colspan="2">
                </td>
            </tr>
            <tr>
                <td style="width: 46px; height: 15px;">
                    <asp:ImageButton ID="imgPareto" runat="server" alt="Click Here To Select Pareto  Chart"
                        src="../images/Chart/ParetoChart.jpg"
                        Style="filter: alpha(opacity=50); -moz-opacity: 0.5; height: 30px; width: 30;" />
                </td>
                <td align="left" style="height: 15px; color: #ffffff; font-weight: bold; font-size: 10px;
                    font-family: Verdana; width: 282px;">
                    <asp:LinkButton ID="LinkButton7" runat="server" Text="Pareto chart" ToolTip="Click On Pareto Chart"></asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td bgcolor="#86c1cc" colspan="2" >
                </td>
            </tr>
            <tr>
                <td style="width: 46px; height: 15px;">
                    <asp:ImageButton ID="imgRun" runat="server" alt="Click Here To Select Run Chart"
                        src="../images/Chart/RunChart.jpg"
                        Style="filter: alpha(opacity=50); -moz-opacity: 0.5; height: 30px; width: 30;" />
                </td>
                <td align="left" style="height: 15px; color: #ffffff; font-weight: bold; font-size: 10px;
                    font-family: Verdana; width: 282px;">
                    <asp:LinkButton ID="LinkButton8" runat="server" Text="Run chart" ToolTip="Click On Run Chart"></asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td bgcolor="#86c1cc" colspan="2">
                </td>
            </tr>
            <tr>
                <td style="width: 46px; height: 15px;">
                    <asp:ImageButton ID="imgScaterplot" runat="server" alt="Click Here To Select ScatterPlot Chart"
                        src="../images/Chart/ScaterPlotChart.jpg"
                        Style="filter: alpha(opacity=50); -moz-opacity: 0.5; height: 30px; width: 30;" />
                </td>
                <td align="left" style="height: 15px; color: #ffffff; font-weight: bold; font-size: 10px;
                    font-family: Verdana; width: 282px;">
                    <asp:LinkButton ID="LinkButton9" runat="server" Text="ScatterPlot chart" ToolTip="Click On ScatterPlot Chart" Width="144px"></asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td bgcolor="#86c1cc" colspan="2">
                </td>
            </tr>
            <tr>
                <td style="width: 46px; height: 15px;">
                    <asp:ImageButton ID="imgStock" runat="server" alt="Click Here To Select Stock Chart"
                        src="../images/Chart/StockChart.jpg"
                        Style="filter: alpha(opacity=50); -moz-opacity: 0.5; height: 30px; width: 30;" />
                </td>
                <td align="left" style="height: 15px; color: #ffffff; font-weight: bold; font-size: 10px;
                    font-family: Verdana; width: 282px;">
                    <asp:LinkButton ID="LinkButton10" runat="server" Text="Stock chart" ToolTip="Click On Stock Chart"></asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td bgcolor="#86c1cc" colspan="2">
                </td>
            </tr>
            <tr>
                <td style="width: 46px; height: 15px;">
                    <asp:ImageButton ID="imgDaughnt" runat="server" alt="Click Here To Select Doughnut Chart"
                         src="../images/Chart/DoughntChart.jpg"
                        Style="filter: alpha(opacity=50); -moz-opacity: 0.5; height: 30px; width: 30;" />
                </td>
                <td align="left" style="height: 15px; color: #ffffff; font-weight: bold; font-size: 10px;
                    font-family: Verdana; width: 282px;">
                    <asp:LinkButton ID="LinkButton11" runat="server" Text="Doughnut chart" ToolTip="Click On Doughnut Chart"></asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td bgcolor="#86c1cc" colspan="2">
                </td>
            </tr>
            <tr>
                <td style="width: 46px; height: 15px;">
                    <asp:ImageButton ID="imgBar" runat="server" alt="Click Here To Select Bar Chart"
                         src="../images/Chart/BarChart.jpg"
                        Style="filter: alpha(opacity=50); -moz-opacity: 0.5; height: 30px; width: 30;" />
                </td><%--onmouseout="makevisible(this,1)" onmouseover="makevisible(this,0)"--%>
                <td align="left" style="height: 15px; color: #ffffff; font-weight: bold; font-size: 10px;
                    font-family: Verdana; width: 282px;">
                    <asp:LinkButton ID="LinkButton12" runat="server" Text="Bar chart" ToolTip="Click On Bar Chart"></asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td bgcolor="#86c1cc" colspan="2">
                </td>
            </tr>
        </table>
    </div>
    
    <div id="div3" onclick="toggleDiv('divShow3dchart', 'imgFormat',110)"
        style="cursor: pointer; background-color: #245055; color: White;">
        <table>
            <tr>
                <td style="width: 190px">
                    <strong>Chart In 3D</strong>
                </td>
                <td align="right">
                    <img id="imgFormat" alt="Expand/Collapse" src="../images/ArrowDown.gif" />
                </td>
            </tr>
        </table>
    </div>
    <div id="divShow3dchart" style="overflow: hidden; display: none; color: White;">
    

        <table summary="This table holds 3D chart Properties" width="80%">
                  <tr>
                <td scope="col" title="3D Chart">
                    <label class="leftlabel" for="chkShow3D">Show 3D:</label>
                </td>
                <td scope="col" title="Show Chart as 3D" style="width: 106px">
                    <asp:CheckBox ID="chkShow3D" runat="server" Text="3D" />
                </td>
            </tr>
            <tr>
                <td scope="col" title="Chart Perspective">
                    <label class="leftlabel" for="ddlPerspective">Perspective:</label>
                </td>
                <td scope="col" title="Select Chart Perspective">
                       <asp:DropDownList ID="ddlPerspective" runat="server" OnSelectedIndexChanged="Perspective_SelectedIndexChanged" CssClass="leftdropdownlist">
                            <asp:ListItem Value="0">0</asp:ListItem>
                            <asp:ListItem Value="10">10</asp:ListItem>
                            <asp:ListItem Value="20">20</asp:ListItem>
                            <asp:ListItem Selected="True" Value="30">30</asp:ListItem>
                            <asp:ListItem Value="40">40</asp:ListItem>
                            <asp:ListItem Value="60">60</asp:ListItem>
                            <asp:ListItem Value="80">80</asp:ListItem>
                            <asp:ListItem Value="100">100</asp:ListItem>
                        </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td scope="col" title="Chart Rotate In X Angel">
                    <label class="leftlabel" for="ddlXangle">Rotate X:</label>
                 </td>
                <td scope="col" title="Select Chart Rotate In X Angel">
                     <asp:DropDownList ID="ddlXangle" runat="server" OnSelectedIndexChanged="XAngle_SelectedIndexChanged" CssClass="leftdropdownlist">
                            <asp:ListItem Value="-90">-90</asp:ListItem>
                            <asp:ListItem Value="-70">-70</asp:ListItem>
                            <asp:ListItem Value="-50">-50</asp:ListItem>
                            <asp:ListItem Value="-30">-30</asp:ListItem>
                            <asp:ListItem Value="-10">-10</asp:ListItem>
                            <asp:ListItem Value="0">0</asp:ListItem>
                            <asp:ListItem Value="10">10</asp:ListItem>
                            <asp:ListItem Value="15">15</asp:ListItem>
                            <asp:ListItem Selected="True" Value="30">30</asp:ListItem>
                            <asp:ListItem Value="50">50</asp:ListItem>
                            <asp:ListItem Value="70">70</asp:ListItem>
                            <asp:ListItem Value="90">90</asp:ListItem>
                        </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td scope="col" title="Chart Rotate In  Y Angel">
                    <label class="leftlabel" for="ddlYang">Rotate Y:</label>
                    </td>
                <td scope="col" title="Select Chart Rotate In Y Angel">
                    <asp:DropDownList ID="ddlYang" runat="server" OnSelectedIndexChanged="YAngle_SelectedIndexChanged" CssClass="leftdropdownlist">
                            <asp:ListItem Value="-110">-110</asp:ListItem>
                            <asp:ListItem Value="-90">-90</asp:ListItem>
                            <asp:ListItem Value="-70">-70</asp:ListItem>
                            <asp:ListItem Value="-50">-50</asp:ListItem>
                            <asp:ListItem Value="-30">-30</asp:ListItem>
                            <asp:ListItem Value="-10">-10</asp:ListItem>
                            <asp:ListItem Value="0">0</asp:ListItem>
                            <asp:ListItem Value="5">5</asp:ListItem>
                            <asp:ListItem Value="10">10</asp:ListItem>
                            <asp:ListItem Selected="True" Value="30">30</asp:ListItem>
                            <asp:ListItem Value="50">50</asp:ListItem>
                            <asp:ListItem Value="70">70</asp:ListItem>
                            <asp:ListItem Value="90">90</asp:ListItem>
                            <asp:ListItem Value="110">110</asp:ListItem>
                        </asp:DropDownList>
                </td>
            </tr>
         </table>
    </div>
    
    
    <div id="div6" onclick="toggleDiv('divChartappearance', 'img6',225)"
        style="cursor: pointer; background-color: #2D646A; color: White;">
        <table>
            <tr>
                <td style="width: 190px">
                    <strong>BackGround Apperance</strong>
                </td>
                <td align="right">
                    <img id="img6" alt="Expand/Collapse"  src="../images/ArrowDown.gif" />
                </td>
            </tr>
        </table>
    </div>
    <div id="divChartappearance" style="overflow: hidden; display: none; color: White;" title="This table holds Chart Background Appearence like Background palettes, Background style.">
        <table summary="This table holds Chart Background Appearence like Background palettes, Background style." style="width: 82%">
            <tr>
                <td scope="col" title="Chart Background Palettes" style="width: 118px">
                    <label class="leftlabel" for="ddlPalettes">Palettes:</label>
                </td>
                <td scope="col" title="Select Chart Background Palettes" style="width: 135px">
                    <asp:DropDownList ID="ddlPalettes" runat="server" CssClass="leftdropdownlist">
                        <asp:ListItem>SeaGreen</asp:ListItem>
                        <asp:ListItem>EarthTones</asp:ListItem>
                        <asp:ListItem>Pastel</asp:ListItem>
                        <asp:ListItem>Excel</asp:ListItem>
                        <asp:ListItem Selected="True">Dundas</asp:ListItem>
                        <asp:ListItem>Pastel</asp:ListItem>
                        <asp:ListItem>Custom</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            
            <tr>
                <td scope="col" title="Chart Backcolor" style="height: 10px; width: 118px;">
                 <a href="javascript: onclick=pickerPopup202('<%=bkcolor.ClientID%>','sample_2');">BackColor:</a>
                </td>
                <td scope="col" title="Select Chart Backcolor" style="height: 10px; width: 200px;">
                  <input type="text" id="sample_2" value="" style="width: 80px"/>
               </td>
            </tr>
            <tr>
                <td scope="col" title="Chart Gradient Style" style="width: 118px">
                    <label class="leftlabel" for="ddlGradient">Gradient:</label>
                </td>
                <td scope="col" title="Select Chart Gradient" style="width: 135px">
                    <asp:DropDownList ID="ddlGradient" runat="server" OnSelectedIndexChanged="Gradient_SelectedIndexChanged"
                        CssClass="leftdropdownlist">
                    </asp:DropDownList>
               </td>
            </tr>
            <tr>
                <td scope="col" title="Chart Hatch Style" style="width: 118px">
                    <label class="leftlabel" for="ddlHatchstyle">HatchStyle</label>
                </td>
                <td scope="col" title="Select Chart Hatch Style" style="width: 135px">
                    <asp:DropDownList ID="ddlHatchstyle" runat="server" OnSelectedIndexChanged="HatchStyle_SelectedIndexChanged"
                        CssClass="leftdropdownlist">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td scope="col" title="Chart Border color" style="width: 118px">
                    <%--<label class="leftlabel" for="ddlBorder">Bordercolor:</label>--%>
                    <a href="javascript: onclick=pickerPopup202('<%=brcolor.ClientID%>','sampleBordercolor');">Bordercolor:</a>
                 </td>
                <td scope="col" title="Select Chart Border Color" style="width: 135px">
                    <input type="text" id="sampleBordercolor" value="" style="width: 80px"/>
                    <%--<img src="../images/bkcolor1.jpg" id="Img2" onclick="bdr('ctl00_LeftPlaceHolder_brcolor');" />--%>
               </td>
            </tr>
            <tr>
                <td scope="col" title="Chart Border Size" style="width: 118px">
                    <label class="leftlabel" for="ddlBordersize">Border Size:</label>
                </td>
                <td scope="col" title="Select Chart Border Size" style="width: 135px">
                    <asp:DropDownList ID="ddlBordersize" runat="server" CssClass="leftdropdownlist">
                        <asp:ListItem Value="1">1</asp:ListItem>
                        <asp:ListItem Selected="True" Value="2">2</asp:ListItem>
                        <asp:ListItem Value="3">3</asp:ListItem>
                        <asp:ListItem Value="4">4</asp:ListItem>
                        <asp:ListItem Value="5">5</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td scope="col" title="Chart Border Style" style="width: 118px">
                    <label class="leftlabel" for="ddlBorderstyle">Borderstyle:</label>
                </td>
                <td scope="col" title="Select Chart Border Style" style="width: 135px">
                    <asp:DropDownList ID="ddlBorderstyle" runat="server" CssClass="leftdropdownlist">
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
    </div>
    <div id="divLegend" onclick="toggleDiv('divLegendappearance', 'img6',500)"
        style="cursor: pointer; background-color: #2D646A; color: White;">
        <table>
            <tr>
                <td style="width: 190px">
                    <strong>Legend Apperance</strong>
                </td>
                <td align="right">
                    <img id="img1" alt="Expand/Collapse"  src="../images/ArrowDown.gif" />
                </td>
            </tr>
        </table>
    </div>
    <div id="divLegendappearance" style="overflow: hidden; display: none; color: White;" title="This table holds Legend Background Appearence like Background palettes, Background style.">
        <table summary="This table holds Legend Background Appearence like Background palettes, Background style." style="width: 82%">
            <tr>
                <td scope="col" title="Legend Backcolor" style="height: 10px; width: 118px;">
                 <a href="javascript: onclick=pickerPopup202('<%=legendbkcolor.ClientID%>','txtlegengbkcolor');">BackColor:</a>
                </td>
                <td scope="col" title="Select Legend Backcolor" style="height: 10px; width: 200px;">
                  <input type="text" id="txtlegengbkcolor" value="" style="width: 80px"/>
               </td>
            </tr>
            <tr>
                <td scope="col" title="Legend Gradient Style" style="width: 118px">
                    <label class="leftlabel" for="ddlGradient">Gradient:</label>
                </td>
                <td scope="col" title="Select Legend Gradient" style="width: 135px">
                    <asp:DropDownList ID="ddlLegendgradient" runat="server" OnSelectedIndexChanged="Gradient_SelectedIndexChanged"
                        CssClass="leftdropdownlist">
                    </asp:DropDownList>
               </td>
            </tr>
            <tr>
                <td scope="col" title="Legend Hatch Style" style="width: 118px">
                    <label class="leftlabel" for="ddlHatchstyle">HatchStyle</label>
                </td>
                <td scope="col" title="Select Legend Hatch Style" style="width: 135px">
                    <asp:DropDownList ID="ddlLegendhatch" runat="server" OnSelectedIndexChanged="HatchStyle_SelectedIndexChanged"
                        CssClass="leftdropdownlist">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td scope="col" title="Legend Border color" style="width: 118px">
                     <a href="javascript: onclick=pickerPopup202('<%=legendbrcolor.ClientID%>','txtlegengbrcolor');">Bordercolor:</a>
                 </td>
                <td scope="col" title="Select Legend Border Color" style="width: 135px">
                    <input type="text" id="txtlegengbrcolor" value="" style="width: 80px"/>
                </td>
            </tr>
            <tr>
                <td scope="col" title="Legend Border Size" style="width: 118px">
                    <label class="leftlabel" for="ddlBordersize">Border Size:</label>
                </td>
                <td scope="col" title="Select Legend Border Size" style="width: 135px">
                    <asp:DropDownList ID="ddlLegendbordersize" runat="server" CssClass="leftdropdownlist">
                        <asp:ListItem Value="1">1</asp:ListItem>
                        <asp:ListItem Selected="True" Value="2">2</asp:ListItem>
                        <asp:ListItem Value="3">3</asp:ListItem>
                        <asp:ListItem Value="4">4</asp:ListItem>
                        <asp:ListItem Value="5">5</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td scope="col" title="Legend Border Style" style="width: 118px">
                    <label class="leftlabel" for="ddlBorderstyle">Borderstyle:</label>
                </td>
                <td scope="col" title="Select Legend Border Style" style="width: 135px">
                    <asp:DropDownList ID="ddlLegendborderstyle" runat="server" CssClass="leftdropdownlist">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
				<td scope="col" title="Show Average" style="width: 118px" >
				    <label class="leftlabel" for="chk_ShowAvg">Show Average:</label>
				</td>
				<td scope="col" title="Check Show Average" style="width: 135px">
				    <asp:checkbox id="chk_ShowAvg" runat="server" Width="237px"></asp:checkbox></td>
			</tr>
			<tr>
				<td scope="col" title="Show Total" style="width: 118px" >
				    <label class="leftlabel" for="chk_ShowTotal">Show Total:</label>
				</td>
				<td scope="col" title="Check Show Total" style="width: 135px">
					<asp:checkbox id="chk_ShowTotal" runat="server" Width="237px" ></asp:checkbox></td>
			</tr>
			<tr>
				<td  scope="col" title="Show Minimum" style="width: 118px">
				<label class="leftlabel" for="chk_ShowTotal">Show Total:</label>
				    <label class="leftlabel" for="chk_ShowMin">Show Minimum:</label>
				</td>
				<td scope="col" title="Check Show Minimum" style="width: 135px">
					<asp:checkbox id="chk_ShowMin" runat="server" Width="237px"></asp:checkbox></td>
			</tr>
			<tr>
				<td scope="col" title="Show  Style" style="width: 118px" >
				   <label class="leftlabel" for="chk_LegendStyleList"> Style:</label>
				</td>
				<td scope="col" title="Show Legend Style List" style="width: 135px">
					<asp:dropdownlist id="ddlLegendStyleList" runat="server" Width="103px" CssClass="leftdropdownlist" >
						<asp:ListItem Value="Table" Selected="True">Table</asp:ListItem>
						<asp:ListItem Value="Column">Column</asp:ListItem>
						<asp:ListItem Value="Row">Row</asp:ListItem>
					</asp:dropdownlist></td>
			</tr>
			<tr>
				<td scope="col" title="Show Table Style" style="width: 118px" >
				   <label class="leftlabel" for="ddlTheTableStyle">Table Style:</label>
				</td>
				<td scope="col" title="show Table Style" style="width: 135px">
						<asp:dropdownlist id="ddlTheTableStyle" runat="server" Width="103px"  CssClass="leftdropdownlist">
						<asp:ListItem Value="Auto" Selected="True">Auto</asp:ListItem>
						<asp:ListItem Value="Wide">Wide</asp:ListItem>
						<asp:ListItem Value="Tall">Tall</asp:ListItem>
						</asp:dropdownlist></td>
		    </tr>
			<tr>
				<td scope="col" title="Show Docking" style="width: 118px">
				<label class="leftlabel" for="ddlLegendDockingList">Docking:</label>
					</td>
				<td scope="col" title="Legend Docking List" style="width: 135px">
				     <asp:dropdownlist id="ddlLegendDockingList" runat="server" Width="103px" CssClass="leftdropdownlist"  >
					 <asp:listitem Value="Right" Selected="True">Right</asp:listitem>
					<asp:listitem Value="Bottom">Bottom</asp:listitem>
					<asp:listitem Value="Left">Left</asp:listitem>
					<asp:listitem Value="Top">Top</asp:listitem>
					</asp:dropdownlist></td>
			</tr>
			<tr>
				<td scope="col" title="Show Alignment" style="width: 118px" >
				<label class="leftlabel" for="ddlLegendAlinmentList">Alignment:</label>
					</td>
				<td scope="col" title="Legend Alinment List" style="width: 135px">
					<asp:dropdownlist id="ddlLegendAlinmentList" runat="server" Width="103px" CssClass="leftdropdownlist" >
					<asp:listitem Value="Near" Selected="True">Near</asp:listitem>
					<asp:listitem Value="Center">Center</asp:listitem>
					<asp:listitem Value="Far">Far</asp:listitem>
					</asp:dropdownlist></td>
			</tr>
			<tr>
				
				<td scope="col" title="Show Inside Chart Area:" style="width: 118px">
				<label class="leftlabel" for="chk_InsideChartArea">Inside Chart Area:</label>
				</td>
				<td scope="col" title="Check Inside Chart Area" style="width: 135px">
					<asp:checkbox id="chk_InsideChartArea" runat="server"  ></asp:checkbox>
				</td>
			</tr>
			<tr>
				<td scope="col" title="Disabled" style="width: 118px">
				<label class="leftlabel" for="chk_Disabled">Disabled:</label>
				</td>
			<td scope="col" title="Check Disabled" style="width: 135px">
				<asp:checkbox id="chk_Disabled" runat="server" ></asp:checkbox>
			</td>
			</tr>
			<tr>
				<td scope="col" title="Reversed" style="width: 118px">
				 <label class="leftlabel" for="chk_Reversed">Reversed:</label>
				 </td>
				<td scope="col" title="Check Reversed" style="width: 135px">
					<asp:checkbox id="chk_Reversed" runat="server" ></asp:checkbox></td>
				</tr>
		</table>
    </div>
    
    <div id="div9" title="Label format" onclick="toggleDiv('divLabelformat', 'img7',350)"
        style="cursor: pointer; background-color: #447b7c; color: White;">
        <table summary="This table holds the Label format image">
            <tr>
                <td scope="col" title="Click Here to Expand/Collapse The Label Format" style="width: 190px" >
                    <strong>Chart label Format</strong>
                </td>
                <td align="right" scope="col" title="Click Here to Expand/Collapse The Label Format" style="width: 16px">
                    <img id="img7" alt="Expand/Collapse"  src="../images/ArrowDown.gif" />
                </td>
            </tr>
        </table>
    </div>
    <div id="divLabelformat" title="Chart X And Y Axis Label Format" style="overflow: hidden; display: none; color:White;">
        <table summary="This table holdss the X axis labels font name,font size,text,font color,offset.">
            <tr>
                <td colspan="2" scope="colgroup" title="Chart X-Axis Label Format" style="text-decoration: underline">
                    X-Axis Chart Format :</td>
            </tr>
            <tr>
                <td scope="col" title="X-Axis Label Font Name">
                    <label class="leftlabel" for="ddlXlabelfont">FontName:</label>
                </td>
                <td scope="col" title=" Select X-Axis Label Font Name" style="width: 112px">
                    <asp:DropDownList ID="ddlXlabelfont" runat="server" CssClass="leftdropdownlist" >
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td scope="col" title="X-Axis Label Font Size">
                    <label class="leftlabel" for="ddlXfontsizelist">FontSize:</label>
                </td>
                <td scope="col" title="Select X-Axis Label Font Size" style="width: 112px">
                    <asp:DropDownList ID="ddlXfontsizelist" runat="server" CssClass="leftdropdownlist">
                        <asp:ListItem Value="6">6</asp:ListItem>
                        <asp:ListItem Selected="True" Value="8">8</asp:ListItem>
                        <asp:ListItem Value="10">10</asp:ListItem>
                        <asp:ListItem Value="12">12</asp:ListItem>
                        <asp:ListItem Value="14">14</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td scope="col" title="X-Axis Label Font Colour">
                    <%--<label class="leftlabel" for="ddlXLabelcolour">FontColour:</label>--%>
                    <a href="javascript: onclick=pickerPopup202('<%=xaxiscolor.ClientID%>','SampleCol');">FontColour:</a>
                </td>
                <td scope="col" title="Select X-Axis Label Font Color" style="width: 112px">
                  <input type="text" id="SampleCol" value="" style="width: 80px"/>
<%--                <img src="../images/bkcolor1.jpg" id="Img3" onclick="xcolor('ctl00_LeftPlaceHolder_xaxiscolor');" />--%>
                  
                </td>
            </tr>
            <tr>
                <td scope="col" title="X-Axis Label Angle">
                    <label class="leftlabel" for="ddlXontanglelist">Angle:</label>
                </td>
                <td scope="col" title="Select X-Axis Label Angle" style="width: 112px">
                    <asp:DropDownList ID="ddlXontanglelist" runat="server" CssClass="leftdropdownlist">
                        <asp:ListItem Selected="True" Value="0">0</asp:ListItem>
                        <asp:ListItem Value="30">30</asp:ListItem>
                        <asp:ListItem Value="45">45</asp:ListItem>
                        <asp:ListItem Value="60">60</asp:ListItem>
                        <asp:ListItem Value="90">90</asp:ListItem>
                        <asp:ListItem Value="-30">-30</asp:ListItem>
                        <asp:ListItem Value="-45">-45</asp:ListItem>
                        <asp:ListItem Value="-60">-60</asp:ListItem>
                        <asp:ListItem Value="-90">-90</asp:ListItem>
                        <asp:ListItem></asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td scope="col" title="X-Axis Label Offset" style="height: 3px">
                    <label class="leftlabel" for="OffsetLabels">Offset:</label>
                </td>
                <td scope="col" title="Click X-Axis Label Offset" style="height: 3px; width: 112px;" >
                    <asp:CheckBox ID="chkXoffset" runat="server" ForeColor="White" />
                </td>
            </tr>
            <tr>
                <td scope="col" title="X-Axis Label Enable">
                    <label class="leftlabel" for="EnableLabels">Enable:</label>
                </td>
                <td scope="col" title="Select X-Axis Label Enable" style="width: 112px" >
                    <asp:CheckBox ID="chkXenable" runat="server" Checked="True" ForeColor="White" />
               </td>
            </tr>
            </table>
        <table>
            <tr>
                <td colspan="2" scope="colgroup" title="Y-Axis Label Font Name" style="text-decoration: underline">
                    Y-Axis Chart Format:</td>
            </tr>
            <tr>
                <td scope="col" title="Y-Axis Label Font Name">
                    <label class="leftlabel" for="ddlYfontname">FontName:</label>
                </td>
                <td scope="col" title="Select Y-Axis Label Font Name" style="width: 144px">
                    <asp:DropDownList ID="ddlYfontname" runat="server" CssClass="leftdropdownlist"></asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td scope="col" title="Y-Axis Label Font Size">
                    <label class="leftlabel" for="ddlYlabelfontsize">FontSize:</label>
             </td>
                <td scope="col" title="Select Y-Axis Label Font Size" style="width: 144px">
                    <asp:DropDownList ID="ddlYlabelfontsize" runat="server" CssClass="leftdropdownlist">
                        <asp:ListItem Value="6">6</asp:ListItem>
                        <asp:ListItem Selected="True" Value="8">8</asp:ListItem>
                        <asp:ListItem Value="10">10</asp:ListItem>
                        <asp:ListItem Value="12">12</asp:ListItem>
                        <asp:ListItem Value="14">14</asp:ListItem>
                    </asp:DropDownList>
                </td >
            </tr>
            <tr>
                <td scope="col" title="Y-Axis Label Font Color">
                   <%-- <label class="leftlabel" for="ddlYfontcolour">FontColour:</label>--%>
                   <a href="javascript: onclick=pickerPopup202('<%=yaxiscolor.ClientID%>','LColor');">FontColour:</a>
                </td>
                <td scope="col" title="Select Y-Axis Label Font Color" style="width: 144px">
                  <input type="text" id="LColor" value="" style="width: 80px"/>
               <%-- <img src="../images/bkcolor1.jpg" id="Img4" onclick="ycolor('ctl00_LeftPlaceHolder_yaxiscolor');" />--%>
                   
                </td>
            </tr>
            <tr>
                <td scope="col" title="Y-Axis Label Angle" >
                    <label class="leftlabel" for="ddlYangle">Angle:</label>
                </td>
                <td scope="col" title="Select Y-Axis Label Angle" style="width: 144px">
                    <asp:DropDownList ID="ddlYangle" runat="server" CssClass="leftdropdownlist">
                        <asp:ListItem Selected="True" Value="0">0</asp:ListItem>
                        <asp:ListItem Value="30">30</asp:ListItem>
                        <asp:ListItem Value="45">45</asp:ListItem>
                        <asp:ListItem Value="60">60</asp:ListItem>
                        <asp:ListItem Value="90">90</asp:ListItem>
                        <asp:ListItem Value="-30">-30</asp:ListItem>
                        <asp:ListItem Value="-45">-45</asp:ListItem>
                        <asp:ListItem Value="-60">-60</asp:ListItem>
                        <asp:ListItem Value="-90">-90</asp:ListItem>
                        <asp:ListItem></asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td scope="col" title="Y-Axis Label Offset">
                    <label class="leftlabel" for="chkYoffset">Offset:</label>
                </td>
                <td scope="col" title="Select Y-Axis Label Offset" style="width: 144px">
                     <asp:CheckBox ID="chkYoffset" runat="server" ForeColor="White" />
                </td>
            </tr>
            <tr>
                <td scope="col" title="Y-Axis Label Enable">
                    <label class="leftlabel" for="chkYenable">Enable:</label>
                    </td>
                <td scope="col" title="Select Y-Axis Label Enable" style="width: 144px">
                     <asp:CheckBox ID="chkYenable" runat="server" Checked="True" ForeColor="White" />
                </td>
            </tr>
        </table>
    </div>
    
    <div id="divShow" title="Chart Axis and Title Format" onclick="toggleDiv('divFormat', 'img8',360)" style="cursor: pointer;
        background-color: #377C84; color: White;">
        <table summary="This table holds Chart Title Image">
            <tr>
                <td scope="col" title="Chart Title" style="width: 190px">
                    <strong> Title and Axis Format</strong>
                </td>
                <td align="right" scope="col" title="Chart Title">
                    <img id="img8" title="Expand/Collapse" alt="Expand/Collapse" src="../images/ArrowDown.gif" />
                </td>
            </tr>
        </table>
    </div>
    <div id="divFormat" title="Chart Title and Axis Format" style="overflow: hidden; display: none; color:White;">
        <table summary="This Table Holds The Chart Title,X-Axis Title ,Y-Axis title,Title Back-Color,font-color,font-size,Title Aligment,Italic Font,Bold Font." style="width: 192px">
            <tr>
                <td scope="col" title="ChartTitle" style="width: 89px">
                    <label class="leftlabel" for="txtCharttitle">Chart Title:</label>
                </td>
                <td scope="col" title="Enter ChartTitle" style="width: 93px">
                    <asp:TextBox ID="txtCharttitle" runat="server" Height="16px" CssClass="leftdropdownlist" Width="96px">Chart Title</asp:TextBox>
                </td>
            </tr>
            <tr>
                <td scope="col" title="X-Axis Title" style="width: 89px">
                   <label class="leftlabel" for="txtTitleext">X-AxisTitle:</label>
                </td>
                <td scope="col" title="Enter Axis Title" style="width: 93px">
                   <asp:TextBox ID="txtTitleext" runat="server" Height="16px" CssClass="leftdropdownlist" Width="96px">XAxis Title</asp:TextBox>
                </td>
            </tr>
            <tr>
                <td scope="col" title="Y-Axis Title" style="width: 89px">
                    <label class="leftlabel" for="txtYTitle">YAxis Title:</label>
                </td>
                <td scope="col" title=" Enter YAxis Title" style="width: 93px">
                    <asp:TextBox ID="txtYTitle" runat="server" Height="16px" CssClass="leftdropdownlist" Width="96px">YAxis Title</asp:TextBox>
                </td>
            </tr>
            <tr>
                <td scope="col" title="Chart Title FontSize" style="width: 89px">
                    <label class="leftlabel" for="ddlTitlesize">Font Size:</label>
                </td>
                <td scope="col" title="Select Chart Title Font Size" style="width: 93px">
                    <asp:DropDownList ID="ddlTitlesize" runat="server" CssClass="leftdropdownlist">
                        <asp:ListItem Value="8">8</asp:ListItem>
                        <asp:ListItem Value="10">10</asp:ListItem>
                        <asp:ListItem Selected="True" Value="12">12</asp:ListItem>
                        <asp:ListItem Value="14">14</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
               <td scope="col" title="Chart Title Font" style="width: 89px">
                  <label class="leftlabel" for="ddlFont1">Title Font:</label>
               </td>
               <td scope="col" title="Select Chart Title Font" style="width: 93px" >
                  <asp:DropDownList ID="ddlFont1" runat="server" CssClass="leftdropdownlist">
                  </asp:DropDownList>
               </td>
            </tr>
            <tr>
                <td scope="col" title="Chart Title Font Color" style="width: 89px">
                   <a href="javascript: onclick=pickerPopup202('<%=titlefontcolor.ClientID%>','SamColor');">FontColor:</a>
                </td>
                <td scope="col" title="Select Chart Title Color" style="width: 93px">
                  <input type="text" id="SamColor" value="" style="width: 80px"/>
                </td>
            </tr>
            <tr>
                <td scope="col" title="Chart  Border Color" style="width: 89px">
                  <a href="javascript: onclick=pickerPopup202('<%=titlebordercolor.ClientID%>','LineColor');">BorderColor:</a>
                </td>
                <td scope="col" title="Select Chart Border Color" style="width: 93px">
                  <input type="text" id="LineColor" value="" style="width: 80px"/>
                </td>
            </tr>
            <tr>
                <td scope="col" title="Chart Back Color" style="width: 89px">
                    <a href="javascript: onclick=pickerPopup202('<%=titlebkcolor.ClientID%>','SampleColor');">Back Color:</a>
                 </td>
                <td scope="col" title="Select Chart Back Color" style="width: 93px">
                  <input type="text" id="SampleColor" value="" style="width: 80px"/>
                  </td>
               </tr>
            <tr>
                <td scope="col" title="Chart Title Alignment" style="width: 89px">
                    <label class="leftlabel" for="Alignment">Alignment:</label>
                </td>
                <td scope="col" title="Select Chart Title Alignment" style="width: 93px">
                    <asp:DropDownList ID="Alignment" runat="server" Height="24px" CssClass="leftdropdownlist">
                        <asp:ListItem Value="Left">Left</asp:ListItem>
                        <asp:ListItem Selected="True" Value="Center">Center</asp:ListItem>
                        <asp:ListItem Value="Right">Right</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td scope="col" title="Italic Font" style="width: 89px">
                    <label class="leftlabel" for="chkItalic">Italic:</label>
                 </td>
                 <td scope="col" title="Select Italic Font" style="width: 93px">
                    <asp:CheckBox ID="chkItalic" runat="server" />
                 </td>
            </tr>
            <tr>
                <td scope="col" title="Bold Font" style="width: 89px">
                    <label class="leftlabel" for="chkBold">Bold:</label>
                </td>
                <td scope="col" title="Select Bold Font" style="width: 93px">
                   <asp:CheckBox ID="chkBold" runat="server" />
                </td>
            </tr>
            <tr>
                <td scope="col" title="UnderLine Font" style="width: 89px">
                    <label class="leftlabel" for="chkUnderline">UnderLine:</label>
                </td>
                <td scope="col" title="Select UnderLine Font" style="width: 93px">
                   <asp:CheckBox ID="chkUline" runat="server" Width="104px" />
                </td>
            </tr>
            <tr>
                <td scope="col" title="StrikeOut Font" style="width: 89px">
                    <label class="leftlabel" for="chkStrikeout">StrikeOut:</label>
                </td>
                <td scope="col" title="Select StrikeOut Font" style="width: 93px">
                   <asp:CheckBox ID="chkSout" runat="server" />
                </td>
            </tr>
            
        </table>
    </div>
    
    <div id="divShowgridline" title="Major/Minor GridLine Format" onclick="toggleDiv('divGridline', 'imgGridline',300)" style="cursor: pointer;
        background-color: #429699; color: White;">
        <table>
            <tr>
                <td scope="col" style="width: 190px" title="Expand/Collapse">
                    <strong>GridLines Format</strong>
                </td>
                <td align="right" scope="col" title="Expand/Collapse">
                    <img id="imgGridline" title="Expand/Collapse" alt="Expand/Collapse" src="../images/ArrowDown.gif" />
                </td>
            </tr>        </table>
    </div>
    <div id="divGridline" title="GridLine Format" style="overflow: hidden; display: none; color:White;">
      <table summary="This Table is used for select major/minor gridlines/trickmark.,Gridlins color,gridline width,gridline interval.">
          <tr>
              <td colspan="2" scope="col" title="MajorGridLine Format" style="text-decoration: underline">MajorGridLine Format:
              </td>
          </tr>
            <tr>
                <td scope="col" title="MajorGridLine And Major Tickmark" style="width: 204px">
                    <label class="leftlabel" for="ddlMajorgridline">Major:</label>
                </td>
                <td scope="col" title="Select MajorGridLine Or Major TickMark" style="width: 132px">
                    <asp:DropDownList ID="ddlMajorgridline" runat="server" CssClass="leftdropdownlist">
                        <asp:ListItem Value="Grid">Grid</asp:ListItem>
                        <asp:ListItem Value="Tickmark">Tickmark</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td scope="col" title="Major Vertical And horizontal GridLine" style="width: 204px">
                    <label class="leftlabel" for="ddlLinetypes">Types:</label>
                </td>
                <td scope="col" title="Select Major VerticalGridLine Or Major HorizantalGridLine" style="width: 132px">
                    <asp:DropDownList ID="ddlLinetypes" runat="server" CssClass="leftdropdownlist">
                        <asp:ListItem Value="Vertical">Vertical</asp:ListItem>
                        <asp:ListItem Value="Horizontal">Horizontal</asp:ListItem>
                    </asp:DropDownList>
                 </td>
            </tr>
            <tr>
                <td scope="col" title="MajorGridLine Color" style="width: 204px">
                    <%--<label class="leftlabel" for="ddlcolour">Line Color:</label>--%>
                    <a href="javascript: onclick=pickerPopup202('<%=Majorgridcolour.ClientID%>','SampleLine');">LineColor::</a>
                </td>
                <td scope="col" title="Select MajorGridLine Color" style="width: 132px">
                  <input type="text" id="SampleLine" value="" style="width: 80px"/>
               <%-- <img src="../images/bkcolor1.jpg" id="Img12" onclick="majorbkcolor('ctl00_LeftPlaceHolder_Majorgridcolour');" />--%>
                   
                 </td>
            </tr>
            <tr>
                <td scope="col" title="MajorGridLine Width" style="width: 204px">
                    <label class="leftlabel" for="ddlMajorline">LineWidth:</label>
                </td>
                <td scope="col" title="Select MajorGridLine Width" style="width: 132px">
                    <asp:DropDownList ID="ddlMajorline" runat="server" Height="22px" CssClass="leftdropdownlist">
                        <asp:ListItem Value="1">1</asp:ListItem>
                        <asp:ListItem Value="2">2</asp:ListItem>
                        <asp:ListItem Value="3">3</asp:ListItem>
                        <asp:ListItem Value="4">4</asp:ListItem>
                    </asp:DropDownList>
                 </td>
            </tr>
            <tr>
                <td scope="col" title="MajorGridLine Interval" style="width: 204px">
                    <label class="leftlabel" for="ddlMajorInterval">Interval:</label>
                 </td>
                <td scope="col" title="Select MajorGridLine Interval" style="width: 132px">
                    <asp:DropDownList ID="ddlMajorInterval" runat="server" CssClass="leftdropdownlist">
                        <asp:ListItem Value="1">1</asp:ListItem>
                        <asp:ListItem Value="2">2</asp:ListItem>
                        <asp:ListItem Value="3">3</asp:ListItem>
                    </asp:DropDownList>
                 </td>
            </tr>
          <tr>
              <td colspan="2" scope="col" title="MinorGridLine Format" style="text-decoration: underline">MinorGridLine Format:</td>
          </tr>
            <tr>
                <td scope="col" title="MinorGridLine Or MinorTickmark" style="width: 204px; height: 24px">
                    <label class="leftlabel" for="ddlMinorType">Minor:</label>
                </td>
                <td scope="col" title="Select MinorGridLine Or MinorTickmark" style="width: 132px; height: 24px">
                    <asp:DropDownList ID="ddlMinorType" runat="server" CssClass="leftdropdownlist">
                        <asp:ListItem Value="Grid">Grid</asp:ListItem>
                        <asp:ListItem Value="Tickmark">Tickmark</asp:ListItem>
                    </asp:DropDownList>
                 </td>
            </tr>
          <tr>
                <td scope="col" title="Minor Vertical And Horizontal GridLine" style="width: 204px">
                    <label class="leftlabel" for="ddlMinoeLinetypes">Types:</label>
                </td>
                <td scope="col" title="Select Minor VerticalGridLine Or Minor HorizontalGridLine" style="width: 132px">
                    <asp:DropDownList ID="ddlMinoeLinetypes" runat="server" CssClass="leftdropdownlist">
                        <asp:ListItem Value="Vertical">Vertical</asp:ListItem>
                        <asp:ListItem Value="Horizontal">Horizontal</asp:ListItem>
                    </asp:DropDownList>
                 </td>
            </tr>
            <tr>
                <td scope="col" title="Minor GridLines Color" style="width: 204px">
                    <%--<label class="leftlabel" for="ddlor1">LineColor:</label>--%>
                    <a href="javascript: onclick=pickerPopup202('<%=ddlMinorColor1.ClientID%>','SampleLineColor');">LineColor:</a>
                </td>
                <td scope="col" title=" Select Minor GridLines Color" style="width: 132px">
                  <input type="text" id="SampleLineColor" value="" style="width: 80px"/>
                <%--<img src="../images/bkcolor1.jpg" id="Img11" onclick="minorbkcolor('ctl00_LeftPlaceHolder_ddlMinorColor1');" />--%>
                                    </td>
            </tr>
            <tr>
                <td scope="col" title="Minor GridLines Width" style="width: 204px">
                    <label class="leftlabel" for="ddlMinorWidth">Line Width:</label>
                </td>
                <td scope="col" title="Select Minor GridLine Width" style="width: 132px">
                    <asp:DropDownList ID="ddlMinorWidth" runat="server" Height="22px" CssClass="leftdropdownlist">
                        <asp:ListItem Value="1">1</asp:ListItem>
                        <asp:ListItem Value="2">2</asp:ListItem>
                        <asp:ListItem Value="3">3</asp:ListItem>
                        <asp:ListItem Value="4">4</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td scope="col" title="Minor GridLines Interval" style="width: 204px">
                    <label class="leftlabel" for="ddlMinorInterval">Interval:</label>
                </td>
                <td scope="col" title="Select Minor Interval" style="width: 132px">
                    <asp:DropDownList ID="ddlMinorInterval" runat="server" Height="22px" CssClass="leftdropdownlist">
                        <asp:ListItem Value="0.1">0.1</asp:ListItem>
                        <asp:ListItem Value="0.2">0.2</asp:ListItem>
                        <asp:ListItem Value="0.25">0.25</asp:ListItem>
                        <asp:ListItem Value="0.5">0.5</asp:ListItem>
                    </asp:DropDownList>
                 </td>
            </tr>
            <!-- REPEAT THE ROW ABOVE FOR EACH LABEL - CONTROL PAIR -->
        </table>
    </div>
    <div id="divChartArea" title="ChartArea Format" onclick="toggleDiv('divAreaposition', 'imgChartarea',300)" style="cursor: pointer;
        background-color: #339999; color: White;">
        <table>
            <tr>
                <td scope="col" style="width: 190px" title="Expand/Collapse">
                    <strong>ChartArea Format</strong>
                </td>
                <td align="right" scope="col" title="Expand/Collapse">
                    <img id="imgChartarea" title="Expand/Collapse" alt="Expand/Collapse" src="../images/ArrowDown.gif" />
                </td>
            </tr>
         </table>
    </div>
    <div id="divAreaposition" title="ChartArea Format" style="overflow: hidden; display: none; color:White;">
         <table >
							<tr>
								<td colspan="2" >Chart Area Position:</td>
								<td></td>
							</tr>
							<tr>
								<td >X:</td>
								<td><asp:textbox id="txtX1" runat="server" Width="100px">10</asp:textbox></td>
							</tr>
							<tr>
								<td >Y:</td>
								<td><asp:textbox id="txtY1" runat="server" Width="100px">10</asp:textbox></td>
							</tr>
							<tr>
								<td >Width:</td>
								<td><asp:textbox id="txtWidth1" runat="server" Width="100px">80</asp:textbox></td>
							</tr>
							<tr>
								<td >Height:</td>
								<td><asp:textbox id="txtHeight1" runat="server" Width="100px">80</asp:textbox></td>
							</tr>
							<tr>
								<td colspan="2" >Plotting Area Position:</td>
								<td></td>
							</tr>
							<tr>
								<td >X:</td>
								<td><asp:textbox id="txtX2" runat="server" Width="100px">10</asp:textbox></td>
							</tr>
							<tr>
								<td >Y:</td>
								<td><asp:textbox id="txtY2" runat="server" Width="100px">10</asp:textbox></td>
							</tr>
							<tr>
								<td >Width:</td>
								<td><asp:textbox id="txtWidth2" runat="server" Width="100px">80</asp:textbox></td>
							</tr>
							<tr>
								<td >Height:</td>
								<td><asp:textbox id="txtHeight2" runat="server" Width="100px">80</asp:textbox></td>
							</tr>
							</table>   
            
    </div>
    <%--<div id="div4" title="Zooming Format" onclick="toggleDiv('divZoom', 'imgGridline',240)" style="cursor: pointer;
        background-color: #339999; color: White;">
        <table>
            <tr>
                <td scope="col" style="width: 190px" title="Click Here for Expand/Collapse GridLine Format">
                    <strong>Zoom Format</strong>
                </td>
                <td align="right" scope="col" title="Click Here for Expand/Collapse GridLine Format">
                    <img id="img13" title="Click Here for Expand/Collapse GridLine Format" alt="Expand/Collapse" src="../images/ArrowDown.gif" />
                </td>
            </tr>
            
        </table>
    </div>--%>
   <%--<div id="divZoom" title="divZoom Format" style="overflow: hidden; display: none; color:White;">
            
            <table summary="This Table is used for select major/minor gridlines/trickmark.,Gridlins color,gridline width,gridline interval." style="width: 200px">
							<tr>
								<td >
                                 Zooming:</td>
								<td><asp:DropDownList id="ddlAxisList" runat="server" tabIndex="5" CssClass="leftdropdownlist" OnSelectedIndexChanged="ddlAxisList_SelectedIndexChanged" >
										<asp:ListItem Value="--Select--" Selected="True">--Select--</asp:ListItem>
										<asp:ListItem Value="X Axis">X Axis</asp:ListItem>
										<asp:ListItem Value="Y Axis">Y Axis</asp:ListItem>
										<asp:ListItem Value="Both Axis">Both Axis</asp:ListItem>
									</asp:DropDownList>
								</td>
							</tr>
							<tr>
							</tr>
							<tr>
							    <td style="text-align: right">
                                    ZoomEna:</td><td>
                                    <asp:CheckBox ID="CheckBoxAJAXZoomEnabled" runat="server" Checked="True"  /></td>
							</tr>
							<tr>
								<td class="label" style="height: 29px">
								    <asp:Button ID="btnZoom" runat="server" Text="Zoom"  />
								</td>
								<td style="height: 29px">
                                    <asp:Button ID="ButtonReset" runat="server" Text="Reset Zoom" /></td>
							</tr>
						</table>
    </div>--%>
   <div id="divAnimation1" title="Animation Format" onclick="toggleDiv('divAnimation', 'imgGridline',240)" style="cursor: pointer;
        background-color: #339999; color: White;">
        <table>
            <tr>
                <td scope="col" style="width: 190px" title="Click Here for Expand/Collapse GridLine Format">
                    <strong>Animation Format</strong>
                </td>
                <td align="right" scope="col" title="Click Here for Expand/Collapse GridLine Format">
                    <img id="img13" title="Click Here for Expand/Collapse GridLine Format" alt="Expand/Collapse" src="../images/ArrowDown.gif" />
                </td>
            </tr>
            
        </table>
    </div>
    <div id="divAnimation" title="divAnimation Format" style="overflow: hidden; display: none; color:White;">
      <table summary="" style="width: 200px">
           <tr>
				<td >Legend</td>
				<td>
					<asp:checkbox id="CheckBoxLegend" runat="server" Checked="True" Text="Move Legend Items One-By-One" AutoPostBack="false" oncheckedchanged="CheckBoxLegend_CheckedChanged"></asp:checkbox>
				</td>
			</tr>
		   <tr>
								<td>seriese</td>
								<td>
									<asp:checkbox id="CheckBoxSeries" runat="server" Checked="True" Text="Move Series One-By-One" AutoPostBack="false" oncheckedchanged="CheckBoxSeries_CheckedChanged"></asp:checkbox>
								</td>
							</tr>
		   <tr>
								<td >Points</td>
								<td>
									<asp:checkbox id="CheckBoxPoints" runat="server" Checked="True" Text="Move Points One-By-One" AutoPostBack="false" oncheckedchanged="CheckBoxPoints_CheckedChanged"></asp:checkbox>
								</td>
							</tr>
		   <tr>
				<td >
					<asp:label id="lbltheme" runat="server" Width="85px" Height="19px">Theme:</asp:label></td>
				<td>
					<p dir="ltr" >
							<asp:dropdownlist id="ddlTheme" runat="server" CssClass="leftdropdownlist" AutoPostBack="false">
							<asp:listitem Value="GrowingTogether">GrowingTogether</asp:listitem>
							<asp:listitem Value="Fading">Fading</asp:listitem>
							<asp:listitem Value="GrowingOneByOne">GrowingOneByOne</asp:listitem>
							<asp:listitem Value="MovingFromTop">MovingFromTop</asp:listitem>
							<asp:listitem Value="GrowingAndFading" Selected="True">GrowingAndFading</asp:listitem>
						    </asp:dropdownlist></p>
				</td>
			</tr>
											
	   </table>
    </div>
    <%--<div id="divGrouping" title="Group Format" onclick="toggleDiv('divGroup', 'imgGridline',240)" style="cursor: pointer;
        background-color: #339999; color: White;">
        <table>
            <tr>
                <td scope="col" style="width: 190px" title="Click Here for Expand/Collapse Group Format">
                    <strong>Animation Format</strong>
                </td>
                <td align="right" scope="col" title="Click Here for Expand/Collapse Group Format">
                    <img id="img1" title="Click Here for Expand/Collapse Group Format" alt="Expand/Collapse" src="../images/ArrowDown.gif" />
                </td>
            </tr>
            
        </table>
    </div>--%>
    <%--<div id="divGroup" title="divGroup Format" style="overflow: hidden; display: none; color:White;">
      <table summary="" style="width: 200px">
           <tr>
				<td><asp:label id="Label2" runat="server">Grouping Formula:</asp:label></td>
				<td>
					<asp:dropdownlist id="GroupingFormulaList" runat="server" AutoPostBack="True" onselectedindexchanged="GroupingFormulaList_SelectedIndexChanged">
					<asp:listitem Value="SUM" Selected="True">Sum</asp:listitem>
					<asp:listitem Value="Max">Maximum</asp:listitem>
					<asp:listitem Value="Min">Minimum</asp:listitem>
					<asp:listitem Value="Ave">Average</asp:listitem>
					<asp:listitem Value="Count">Count</asp:listitem>
					</asp:dropdownlist>
			   </td>
		  </tr>
		</table>
    </div>--%>
    <%--<div id="div2" title="sorting Format" onclick="toggleDiv('divSort', 'imgGridline',240)" style="cursor: pointer;
        background-color: #339999; color: White;">
        <table>
            <tr>
                <td scope="col" style="width: 190px" title="Click Here for Expand/Collapse GridLine Format">
                    <strong>Sort Format</strong>
                </td>
                <td align="right" scope="col" title="Click Here for Expand/Collapse GridLine Format">
                    <img id="img1" title="Click Here for Expand/Collapse GridLine Format" alt="Expand/Collapse" src="../images/ArrowDown.gif" />
                </td>
            </tr>
            
        </table>
    </div>
    <div id="divSort" title="divSort Format" style="overflow: hidden; display: none; color:White;">
      <table summary="This Table is used for select major/minor gridlines/trickmark.,Gridlins color,gridline width,gridline interval." style="width: 200px">
            
            <tr>
                <td scope="col"  title="Axis Sorting" style="width: 130px">
                    Axis value</td>
                <td scope="col"  title="Axis Sorting">
                    <asp:dropdownlist id="ddlSortlist" runat="server" AutoPostBack="false" CssClass="leftdropdownlist">
										<asp:listitem Value="Unsorted" Selected="True">Unsorted</asp:listitem>
										<asp:listitem Value="Y value">Y Value</asp:listitem>
										<asp:listitem Value="X Value">X Value</asp:listitem>
										
									</asp:dropdownlist>
                </td>
            </tr>
            <tr>
                <td scope="col" title="Sort Order" style="width: 130px">
                    Sort Order</td>
                <td scope="col" style="width: 106px" title="Show Sort Order">
                    <asp:dropdownlist id="ddlSortorderlist" runat="server" AutoPostBack="false" CssClass="leftdropdownlist" >
										<asp:listitem Value="Ascending" Selected="True">Ascending</asp:listitem>
										<asp:listitem Value="Descending">Descending</asp:listitem>
									</asp:dropdownlist>
                </td>
            </tr>       
            
              
              
            <!-- REPEAT THE ROW ABOVE FOR EACH LABEL - CONTROL PAIR -->
        </table>
        <%--<table >
							<tr>
								<td >Legend</td>
								<td>
									<asp:checkbox id="CheckBoxLegend" runat="server" Checked="True" Text="Move Legend Items One-By-One" AutoPostBack="false" oncheckedchanged="CheckBoxLegend_CheckedChanged"></asp:checkbox>
								</td>
							</tr>
							<tr>
								<td>seriese</td>
								<td>
									<asp:checkbox id="CheckBoxSeries" runat="server" Checked="True" Text="Move Series One-By-One" AutoPostBack="false" oncheckedchanged="CheckBoxSeries_CheckedChanged"></asp:checkbox>
								</td>
							</tr>
							<tr>
								<td >Points</td>
								<td>
									<asp:checkbox id="CheckBoxPoints" runat="server" Checked="True" Text="Move Points One-By-One" AutoPostBack="false" oncheckedchanged="CheckBoxPoints_CheckedChanged"></asp:checkbox>
								</td>
							</tr>
							<tr>
								<td ></td>
								<td></td>
							</tr>
						</table>--%>
						
    </div>
    
   

<div id="ll" style="display:none;">
<input type="text" runat="server" id="bkcolor" style="width: 24px; background-color:#59afbb; border-bottom-color:#59afbb; border-color:#59afbb;" />
<input type="text" runat="server" id="brcolor" style="width: 24px; background-color:#59afbb; border-bottom-color:#59afbb; border-color:#59afbb;" />
<input type="text" runat="server" id="legendbkcolor" style="width: 24px; background-color:#59afbb; border-bottom-color:#59afbb; border-color:#59afbb;" />
<input type="text" runat="server" id="legendbrcolor" style="width: 24px; background-color:#59afbb; border-bottom-color:#59afbb; border-color:#59afbb;" />
<input type="text" runat="server" id="xaxiscolor" style="width: 24px; background-color:#59afbb; border-bottom-color:#59afbb; border-color:#59afbb;" />
<input type="text" runat="server" id="yaxiscolor" style="width: 24px; background-color:#59afbb; border-bottom-color:#59afbb; border-color:#59afbb;" />
<input type="text" runat="server" id="titlefontcolor" style="width: 24px; background-color:#59afbb; border-bottom-color:#59afbb; border-color:#59afbb;" />
<input type="text" runat="server" id="titlebordercolor" style="width: 24px; background-color:#59afbb; border-bottom-color:#59afbb; border-color:#59afbb;" />
<input type="text" runat="server" id="titlebkcolor" style="width: 24px; background-color:#59afbb; border-bottom-color:#59afbb; border-color:#59afbb;" />
<input type="text" runat="server" id="ddlMinorColor1" style="width: 24px; background-color:#59afbb; border-bottom-color:#59afbb; border-color:#59afbb;" />
<input type="text" runat="server" id="Majorgridcolour" style="width: 24px; background-color:#59afbb; border-bottom-color:#59afbb; border-color:#59afbb;" />
</div>



<div id="mm" runat="server" visible="false">
       
    </div>
    <%--This hidden Field is used for select multiple chart--%>
    <asp:HiddenField ID="hidChart" runat="server" />
    
   <%-- <asp:HiddenField ID="hidbkcolor" runat="server" />--%>
    <%--<input type="hidden" name="hidbkcolor" id="hidbkcolor" runat="server" />--%>
</asp:Content>
<asp:Content ID="mainPane" runat="server" ContentPlaceHolderID="MainPlaceHolder">
    <script language="JavaScript" src="../js/picker.js" type="text/javascript">function imgFormat_onclick() 
    {}
function TABLE1_onclick()
{

}

   </script>
   <div id="divcaption" style="padding:20px;" >
        <table summary="This table hold the span , Reportname and columns " width="100%" id="TABLE1"  onclick="return TABLE1_onclick()">
            <caption><b>SELECT REPORT</b></caption>
            <tr>
                <td style="width: 707px; height: 185px;">
                    <div id="Selectreport" runat="server" visible="false">
                        <table>
                        <tr>
                            <td scope="col" title="Department" style="width: 106px;" >
                                <label for="ddlDepartmant" class="label">
                                    Department</label>
                            </td>
                            <td scope="col" title=" Select Department"  colspan="">
                                <asp:DropDownList ID="ddlDepartmant"  runat="server" AutoPostBack="True" CssClass="dropdownlist">
                                </asp:DropDownList><asp:TextBox ID="txtDepartment" Visible="false"  runat="server" ReadOnly="true"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td scope="col" title="Client" style="width: 106px"  >
                                <label for="ddlClient" class="label">
                                Client</label>
                            </td>
                            <td scope="col" title="Select Client" >
                                <asp:DropDownList ID="ddlClient" runat="server" AutoPostBack="True" CssClass="dropdownlist">
                                </asp:DropDownList><asp:TextBox ID="txtClient" runat="server" Visible="false"  ReadOnly="true"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td scope="col" title="LOB" style="width: 106px"  >
                                <label for="ddlLob" class="label">
                                LOB</label>
                            </td>
                            <td scope="col" title="Select Lob"  >
                                <asp:DropDownList ID="ddlLob" runat="server" AutoPostBack="True" CssClass="dropdownlist">
                                </asp:DropDownList><asp:TextBox ID="txtLob" runat="server" Visible="false"  ReadOnly="true"></asp:TextBox>
                            </td>
                        </tr>
                            <tr>
                                <td scope="col" style="width: 106px" title="LOB">
                                <label for="ddlUser" class="label">User</label>  
                                </td>
                                <td scope="col" title="Select Lob">
                                <asp:DropDownList id="ddlUser" runat="server" AutoPostBack="True" ToolTip="Select User" CssClass="dropdownlist">
                       </asp:DropDownList>
                                </td>
                            </tr>
                        <tr>
                            <td scope="col" title=" Select Graph Type"  align="left" style="width: 106px"  >
                                Select Type&nbsp;</td>
                            <td align="left" colspan="1" scope="col"  title=" Select Graph Type">
                                <asp:RadioButton   id="rbnAnalysis" runat="server" Text="Analysis" Width="80px" GroupName="Report" AutoPostBack="True" ToolTip="Analysis Report"></asp:RadioButton><asp:RadioButton  id="rbnReport" runat="server" Text="Report" Width="72px" GroupName="Report" AutoPostBack="True" ToolTip="Report Designer Report"></asp:RadioButton>&nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td scope="col" title="Report Name" style="width: 106px"  >
                                <label for="txtCurrentReport" class="label">Report Name</label></td>
                            <td scope="col" title="Current Report Name"  >
                                <asp:TextBox ID="txtCurrentReport" runat="server" ReadOnly="true" Width="144px" Visible="false"  ></asp:TextBox><asp:DropDownList ID="ddlReport" runat="server" CssClass="dropdownlist" AutoPostBack="True">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr id="trAnalysis" runat="server">
                            <td id="labelAnalysis" runat="server" scope="col"  title="Analysis Report Name" style="width: 106px">
                                <label  for="ddlAnalysistable" class="label" >Analysis Table</label>
                            </td>
                            <td scope="col"  title="Analysis Report Name ">
                            <asp:DropDownList ID="ddlAnalysistable" runat="server" CssClass="dropdownlist">
                                </asp:DropDownList>
                            </td>
                        </tr>
                </table>
                </div>
                    <div id="Opengraph" runat="server" visible="false" >
                        <table style="width: 296px; height: 200px;">
                             <tr>
                                <td scope="col" title="Department" style="width: 106px; height: 14px;" >
                                    <label for="ddlDepartmant" class="label">Department</label>
                                </td>
                                <td scope="col" title=" Select Department"  colspan="" style="width: 198px; height: 14px;">
                                        <asp:DropDownList ID="ddlDepartment" runat="server" AutoPostBack="True" CssClass="dropdownlist"></asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td scope="col" title="Client" style="width: 106px"  >
                                    <label for="ddlClient" class="label">Client</label>
                                 </td>
                                 <td scope="col" title="Select Client" style="width: 198px" >
                                        <asp:DropDownList ID="ddlOpenclient" runat="server" AutoPostBack="True" CssClass="dropdownlist"></asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td scope="col" title="LOB" style="width: 106px"  >
                                    <label for="ddlLob" class="label">LOB</label>
                                </td>
                                <td scope="col" title="Select Lob" style="width: 198px"  >
                                    <asp:DropDownList ID="ddlOpenlob" runat="server" AutoPostBack="True" CssClass="dropdownlist"></asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td scope="col" style="width: 106px" title="User">
                                <label for="ddlopenuser" class="label">User</label>
                                </td>
                                <td scope="col" style="width: 198px" title="Select User">
                                    <asp:DropDownList id="ddlopenuser" runat="server" AutoPostBack="True" ToolTip="Select User" CssClass="dropdownlist">
                       </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td scope="col" title=" Select Graph Type"  align="left" style="width: 106px"  >
                                    <label for="rbnAnalysis" class="label">Select Type</label>
                                </td>
                                <td align="left" colspan="1" scope="col"  title=" " style="width: 198px">
                                    <asp:RadioButton  id="rbnOpenanalysis" runat="server" Text="Analysis " ToolTip="Analysis Report" Width="80px" GroupName="Report" AutoPostBack="True"></asp:RadioButton><asp:RadioButton  id="rbnOpenreport" runat="server" ToolTip="Report Designer Report" Text="Report" Width="72px" GroupName="Report" AutoPostBack="True"></asp:RadioButton>
                                </td>
                            </tr>
                            <tr>
                                <td scope="col" title="Report Name" style="width: 106px"  >
                                     <label for="ddlOpenreport" class="label">Report Name</label>
                                </td>
                                <td scope="col" title="Report Name" style="width: 198px"  >
                                    <asp:DropDownList ID="ddlOpenreport" runat="server" CssClass="dropdownlist" AutoPostBack="True">        </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td id="labelOpenanalysis" runat="server" scope="col"  title="Analysis Report Name" style="width: 106px; height: 23px;">
                                    <label  for="ddlOpenanalysistable"  class="label" >Analysis Table</label>
                                </td>
                                <td scope="col"  title="Analysis Report Name " style="width: 198px; height: 23px;">
                                    <asp:DropDownList ID="ddlOpenanalysistable" runat="server" CssClass="dropdownlist" Visible="False" AutoPostBack="True">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td id="lblGraphname" runat="server" scope="col"  title="Report Graph" style="width: 106px">
                                    <label  for="ddlGraphname" class="label" >Graph Name</label>
                                </td>
                                <td scope="col"  title="Report Graph Name " style="width: 198px">
                                    <asp:DropDownList ID="ddlGraphname" runat="server" CssClass="dropdownlist" >
                                    </asp:DropDownList>
                                </td>
                            </tr>
                       </table>
                   </div>
                </td>
                <td align="right" style="width:400px; height: 185px" valign="top">
                        <asp:Label ID="lblChartimage" runat="server" ForeColor="#006666" Font-Bold="true" Font-Underline="true" ></asp:Label>
                </td>
            </tr>
            <tr>
            <td style="width: 651px; height: 44px" colspan="4">
               <table>
                         <tr>
                         <td valign="top"  title="Start Date" style="width: 103px" >
                            <label for="txtFromdate" class="label">StartDate</label>
                         </td>
				          <td  valign="top" align="left" title="Enter Start Date" >
				               <asp:TextBox ID="txtFromdate" runat="server" Width="144px"></asp:TextBox>
				           </td>
				          <td style="width: 74px" >
				              <img id="imgStartDate" onclick="ShowCalendar();" src="../images/Calendar.gif"
                                      title="Click To Select Start date" alt="Click To Select Start date" />
				          </td>
                          <td  valign="top" title="End Date" style="width: 60px" >    
				                <label title="End Date" for="txtTodate">EndDate</label>				                        
				           </td>
				           <td valign="top" align="left" title="Enter End Date" >
				                <asp:TextBox ID="txtTodate" runat="server"></asp:TextBox>
				           </td>
				           <td >
                                  <img id="imgEnddate" alt="Enter End Date"  onclick="ShowCalendar1();" src="../images/Calendar.gif" title="Click To Select End Date" />
				           </td>
                          </tr>
                          <tr>
                            <td>    
                                
                            </td>
                            <td colspan="5">
                                <asp:Label ID="lbldatemsg" runat="server" Text="*" Font-Bold="true" ForeColor="Red" Visible="false"></asp:Label>
                            </td>
                          </tr>
                        </table> 
            </td>
       </tr>
       <tr>
           <td align="center" colspan="1" scope="col" title="Click On Show Report Button To Show The Report Column " style="width: 707px">
               &nbsp; &nbsp;&nbsp;
                <asp:Button ID="ShowReport" runat="server" Text="Show Report" CssClass="button" /> <asp:Button ID="Button1" runat="server" Text="ShowReport" CssClass="button"/>
               <asp:Button ID="btnreset" runat="server" CssClass="button"  Text="Reset" ToolTip="Reset window" />
           </td>
           <td align="center" colspan="5" scope="col" title="Click On Show Report Button To Show The Report Column ">
               &nbsp;</td>
        </tr>
        <tr>
            <td align="center" scope="col" title="Show Report Columns"  colspan="1" style="width: 707px">
                &nbsp;<table summary="This table used for select multiple column from one listbox to another list box" title="Select Multiple columns From report">
                    <tr>
                        <td scope="col" title="Select multiple column" style="height: 147px" >
                            <asp:ListBox ID="repcols" SelectionMode="Multiple" runat="server" CssClass="listBox"></asp:ListBox>
                        </td>
                        <td scope="col" title="" valign="middle" style="width: 42px; height: 147px;" >
                            &nbsp;<asp:Button ID="add" runat="server" Text=">>" CssClass="button" Width="40px" ToolTip="Click on button to add items" />
                            <asp:Button ID="remove" runat="server" Text="<<" CssClass="button" Width="40px"  ToolTip="Click on button to remove items" />
                        </td>
                        <td scope="col" title="Selected Column " style="height: 147px" >
                            <asp:ListBox ID="selectedcols" SelectionMode="Multiple" runat="server" CssClass="listBox"></asp:ListBox>
                        </td>
                    </tr>
                </table>
            </td>
            <td align="center" colspan="2" scope="col" title="Show Report Columns">
            </td>
            <td align="left" colspan="5" scope="col"  >
            </td>
        </tr>
        <tr>
            <td align="center" colspan="3" scope="col" title="" style="height: 22px;">
                <asp:RadioButton ID="rbnRow" runat="server" GroupName="check" Text="Row Series" Width="112px" ToolTip="Select chart  Row series"  AutoPostBack="true" /><asp:RadioButton ID="rbnColumn" runat="server" GroupName="check" Text="Column Series" Width="128px" ToolTip="Select chart  Column series" AutoPostBack="True" />
                <asp:CheckBox ID="chkanimated" runat="server" Text="Animated Graph" AutoPostBack="true" Width="144px" /><asp:CheckBox ID="chkSunnarized" runat="server" Text="Summarized Graph" AutoPostBack="true" Width="160px" />
            </td>
            <td align="left" colspan="5" scope="col" style="height: 22px;">
            </td>
        </tr>
        <tr>
            <td align="left" colspan="3" scope="col" style="height: 8px;" title=" Click on Graph Button">
                &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
            <asp:Button ID="btnGraph" runat="server" Text="Plot Graph" CssClass="button" ToolTip="Click To Show Graph" Width="96px"   />&nbsp;
            <input type="button" id="btnsave" onclick="return Savegraph();" class="button" runat="server" name="SaveGraph" title="Click To Save Graph" value="Save" style="width: 73px" />  
            <asp:Button ID="btnOpenGraph" runat="server" Text="OpenGraph"  CssClass="button" ToolTip="Click To Open Graph" Width="88px"   />&nbsp;
            <asp:Button ID="btnUpdate" runat="server" Text="Update"  CssClass="button" ToolTip="Click To Update Graph"  Visible="False" Width="94px"   />&nbsp;
            <asp:Button ID="btnDelete" runat="server" Text="Delete"  CssClass="button" ToolTip="Click To Delete Graph" Visible="False" Width="98px"   />&nbsp;
            </td>
            <td align="left" colspan="5" scope="col" style="height: 22px;">
            </td>
        </tr>
      </table>
   </div>
    
    <table summary="This table hold the Graphcontrol and multiple Graph based Dropdown list controls." title="Graph On Selected Columns">
        <tr>
            <td scope="col" title="Graph On Selected Columns">
                <table summary="This table hold the Graphcontrol and multiple Graph based Dropdown list controls." title="Graph On Selected Columns">
                    <tr>
                        <td scope="col" title="Graph On Selected Columns" style="height: 908px" valign="top">
                        
                            <DCWC:Chart ID="Chart1"  runat="server" BackGradientEndColor="White" BackGradientType="DiagonalLeft" 
                                BorderLineColor="26, 59, 105" BorderLineStyle="Solid" Height="400px" Palette="Dundas"
                                Visible="false" Width="500px">
                                <Legends>
                                    <DCWC:Legend AutoFitText="False" BackColor="Transparent"  Font="Trebuchet MS, 8.25pt, style=Bold"
                                        Name="Default">
                                    </DCWC:Legend>
                                </Legends>
                                <Titles>
								<dcwc:Title ShadowColor="32, 0, 0, 0" Font="Trebuchet MS, 14.25pt, style=Bold" Color="26, 59, 105" Name="Title1"></dcwc:Title>
							</Titles>
                                <ChartAreas>
                                    <DCWC:ChartArea BackGradientEndColor="White" BackGradientType="TopBottom" BorderColor="64, 64, 64, 64"
                                        BorderStyle="Solid" Name="Chart Area 1" ShadowColor="Transparent">
                                        <AxisY LineColor="64, 64, 64, 64">
										<LabelStyle Font="Trebuchet MS, 8.25pt, style=Bold" Interval="Auto" IntervalOffset="Auto" IntervalOffsetType="Auto" IntervalType="Auto"></LabelStyle>
										<%--<MajorGrid LineColor="64, 64, 64, 64" Interval="Auto" IntervalOffset="Auto" IntervalOffsetType="Auto" IntervalType="Auto"></MajorGrid>
                                        <MajorTickMark Interval="Auto" IntervalOffset="Auto" IntervalOffsetType="Auto" IntervalType="Auto" />--%>
									</AxisY>
									<AxisX LineColor="64, 64, 64, 64">
										<LabelStyle Font="Trebuchet MS, 8.25pt, style=Bold" Interval="Auto" IntervalOffset="Auto" IntervalOffsetType="Auto" IntervalType="Auto"></LabelStyle>
										<%--<MajorGrid LineColor="64, 64, 64, 64" Interval="Auto" IntervalOffset="Auto" IntervalOffsetType="Auto" IntervalType="Auto"></MajorGrid>
                                        <MajorTickMark Interval="Auto" IntervalOffset="Auto" IntervalOffsetType="Auto" IntervalType="Auto" />--%>
									</AxisX>
                                    </DCWC:ChartArea>
                                    
                                </ChartAreas>
                                <BorderSkin FrameBackColor="CornflowerBlue" FrameBackGradientEndColor="CornflowerBlue"
                                    FrameBorderColor="100, 0, 0, 0" FrameBorderWidth="2" PageColor="Control" SkinStyle="Emboss" />
                            </DCWC:Chart>
                            <dcwc:chart id="Chart2" runat="server" Width="412px" Height="296px" BackColor="#F3DFC1" Palette="Dundas" BorderLineStyle="Solid" BackGradientType="TopBottom" BorderLineWidth="2" BorderLineColor="181, 64, 1" ImageType="Png" ImageUrl="~/TempImages/ChartPic_#SEQ(300,3)" Visible="False">
							<legends>
								<dcwc:legend Enabled="False" AutoFitText="False" Name="Default" BackColor="Transparent" Font="Trebuchet MS, 8.25pt, style=Bold"></dcwc:legend>
							</legends>
							<borderskin skinstyle="Emboss"></borderskin>
							<series>
								<dcwc:series MarkerSize="8" XValueType="Double" Name="DataDistribution" ChartType="Point" BorderColor="110, 26, 59, 105" Color="120, 252, 180, 65" Font="Trebuchet MS, 8.25pt" YValueType="Double"></dcwc:series>
							<dcwc:series ShowLabelAsValue="True" ChartArea="HistogramArea" XValueType="Double" Name="Histogram" BorderColor="180, 26, 59, 105" Color="224, 64, 10" Font="Trebuchet MS, 8.25pt" YValueType="Double"></dcwc:series>
							</series>
							<chartareas>
								<dcwc:chartarea Name="Default" BorderColor="64, 64, 64, 64" BorderStyle="Solid" BackGradientEndColor="White" BackColor="OldLace" ShadowColor="Transparent" AlignWithChartArea="HistogramArea" BackGradientType="TopBottom">
									<axisy2 labelsautofit="False">
										<labelstyle font="Trebuchet MS, 8.25pt, style=Bold"></labelstyle>
									</axisy2>
									<area3dstyle yangle="10" perspective="10" xangle="15" rightangleaxes="False" wallwidth="0" clustered="True"></area3dstyle>
									<position y="4" height="15" width="96" x="3"></position>
									<axisy linecolor="64, 64, 64, 64" reverse="True" maximum="2" minimum="0">
										<labelstyle font="Trebuchet MS, 8.25pt, style=Bold" enabled="False"></labelstyle>
										<majorgrid linecolor="64, 64, 64, 64" enabled="False"></majorgrid>
										<majortickmark enabled="False"></majortickmark>
									</axisy>
									<axisx titlefont="Trebuchet MS, 8pt" linecolor="64, 64, 64, 64" labelsautofit="False" enabled="True" title="One axis data distribution chart">
										<labelstyle font="Trebuchet MS, 8.25pt, style=Bold" enabled="False"></labelstyle>
										<majorgrid linecolor="64, 64, 64, 64"></majorgrid>
										<majortickmark size="1.5" linecolor="Transparent" enabled="False"></majortickmark>
									</axisx>
								</dcwc:chartarea>
								<dcwc:chartarea Name="HistogramArea" BorderColor="64, 64, 64, 64" BorderStyle="Solid" BackGradientEndColor="White" BackColor="OldLace" ShadowColor="Transparent" BackGradientType="TopBottom">
									<axisy2 labelsautofit="False">
										<labelstyle font="Trebuchet MS, 8.25pt, style=Bold"></labelstyle>
									</axisy2>
									<area3dstyle yangle="10" perspective="10" xangle="15" rightangleaxes="False" wallwidth="0" clustered="True"></area3dstyle>
									<position y="18" height="77" width="93" x="3"></position>
									<axisy linecolor="64, 64, 64, 64">
										<labelstyle font="Trebuchet MS, 8.25pt, style=Bold"></labelstyle>
										<majorgrid linecolor="64, 64, 64, 64"></majorgrid>
									</axisy>
									<axisx titlefont="Trebuchet MS, 8pt" linecolor="64, 64, 64, 64" labelsautofit="False" title="Histogram (Frequency Diagram)">
										<labelstyle font="Trebuchet MS, 8.25pt, style=Bold"></labelstyle>
										<majorgrid linecolor="64, 64, 64, 64"></majorgrid>
									</axisx>
								</dcwc:chartarea>
							</chartareas>
						</dcwc:chart>
						    <dcwc:chart id="StockChart" runat="server" Visible="false" BackColor="#D3DFF0" Width="460px" Height="400px" ImageUrl="~/TempImages/ChartPic_#SEQ(300,3)" ImageType="Png" BorderLineStyle="Solid" Palette="Dundas" BackGradientEndColor="White" BackGradientType="TopBottom" BorderLineWidth="2" BorderLineColor="26, 59, 105" enableviewstate="True" viewstatecontent="All">
							<legends>
								<dcwc:legend LegendStyle="Row" AutoFitText="False" DockToChartArea="Price" Docking="Top" DockInsideChartArea="False" Name="Default" BackColor="Transparent" Font="Trebuchet MS, 8.25pt, style=Bold" Alignment="Far">
									<position y="5" height="7.127659" width="38.19123" x="55"></position>
								</dcwc:legend>
							</legends>
							<borderskin skinstyle="Emboss"></borderskin>
							<%--<series>
								<%--<dcwc:series YValuesPerPoint="4" ChartArea="Price" XValueType="DateTime" ShowInLegend="False" Name="Price" ChartType="Stock" BorderColor="180, 26, 59, 105"></dcwc:series>--%>
								<%--<dcwc:series ChartArea="Volume" XValueType="DateTime" ShowInLegend="False" Name="Volume" BorderColor="180, 26, 59, 105" Color="224, 64, 10"></dcwc:series>
							</series>--%>
							<chartareas>
								<dcwc:chartarea Name="Price" BorderColor="64, 64, 64, 64" BorderStyle="Solid" BackGradientEndColor="White" BackColor="64, 165, 191, 228" ShadowColor="Transparent" BackGradientType="TopBottom">
									<area3dstyle yangle="10" perspective="10" xangle="15" rightangleaxes="False" wallwidth="0" clustered="True"></area3dstyle>
									<position y="10" height="100" width="88" x="3"></position>
									<axisy linecolor="64, 64, 64, 64" labelsautofit="False" startfromzero="False">
										<labelstyle font="Trebuchet MS, 8.25pt, style=Bold"></labelstyle>
										<majorgrid linecolor="64, 64, 64, 64"></majorgrid>
									</axisy>
									<axisx linecolor="64, 64, 64, 64" labelsautofit="False">
										<labelstyle font="Trebuchet MS, 8.25pt, style=Bold" showendlabels="False"></labelstyle>
										<majorgrid linecolor="64, 64, 64, 64"></majorgrid>
									</axisx>
								</dcwc:chartarea>
								<%--<dcwc:chartarea Name="Volume" BorderColor="64, 64, 64, 64" BorderStyle="Solid" BackGradientEndColor="White" BackColor="64, 165, 191, 228" ShadowColor="Transparent" AlignWithChartArea="Price" BackGradientType="TopBottom">
									<area3dstyle yangle="10" perspective="10" xangle="15" rightangleaxes="False" wallwidth="0" clustered="True"></area3dstyle>
									<position y="51.84195" height="42" width="88" x="3"></position>
									<axisy linecolor="64, 64, 64, 64" labelsautofit="False" startfromzero="False">
										<labelstyle font="Trebuchet MS, 8.25pt, style=Bold"></labelstyle>
										<majorgrid linecolor="64, 64, 64, 64"></majorgrid>
									</axisy>
									<axisx linecolor="64, 64, 64, 64" labelsautofit="False">
										<labelstyle font="Trebuchet MS, 8.25pt, style=Bold" showendlabels="False"></labelstyle>
										<majorgrid linecolor="64, 64, 64, 64"></majorgrid>
									</axisx>
								</dcwc:chartarea>--%>
							</chartareas>
						</dcwc:chart>
		                     </td>
                        <td valign="top" scope="col" title="Select Pie Chart Drawing Style" style="height: 908px">
                            <div id="divpie" runat="server" style="display: none;" title=" Select Pie chart properties like Label Style, Line Arrow Type, Line Type,Offset,Legend">
                                <table summary="This table hold the Pie chart properties like Label Style, Line Arrow Type, Line Type,Offset,Legend" title="Pie Chart Properties">
                                    <tr>
                                        <td scope="col" title="Pie Chart LabelStyle">
                                        <label for="ddlLabelstylelist" class="label">
                                            LabelStyle</label>
                                            </td>
                                        <td scope="col" title="Select Pie Chart LabelStyle">
                                            <asp:DropDownList ID="ddlLabelstylelist" runat="server" CssClass="dropdownlist">
                                                <asp:ListItem Selected="True" Value="Inside">Inside</asp:ListItem>
                                                <asp:ListItem Value="Outside">Outside</asp:ListItem>
                                                <asp:ListItem Value="Disabled">Disabled</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td scope="col" title="Pie Chart line ArrowType">
                                        <label for="ddlPielinearrowtype" class="label">
                                            line ArrowType</label>
                                            
                                        </td>
                                        <td scope="col" title="Select Pie Chart line ArrowType">
                                            <asp:DropDownList ID="ddlPielinearrowtype" runat="server" AutoPostBack="false" CssClass="dropdownlist">
                                                <asp:ListItem>None</asp:ListItem>
                                                <asp:ListItem>Triangle</asp:ListItem>
                                                <asp:ListItem>SharpTriangle</asp:ListItem>
                                                <asp:ListItem>Lines</asp:ListItem>
                                            </asp:DropDownList></td>
                                    </tr>
                                    <tr>
                                        <td scope="col" title="Pie Chart Line Size">
                                        <label for="ddlPielinearrowsize" class="label">Pie Line Size</label>
                                            
                                        </td>
                                        <td scope="col" title="Select Pie Chart Line Size">
                                            <asp:DropDownList ID="ddlPielinearrowsize" runat="server" AutoPostBack="false" CssClass="dropdownlist">
                                                <asp:ListItem>1</asp:ListItem>
                                                <asp:ListItem>2</asp:ListItem>
                                            </asp:DropDownList></td>
                                    </tr>
                                    <tr>
                                        <td scope="col" title="Pie Chart Label Offset">
                                        <label for="ddlPielabeloffsetlist" class="label">Pie Label Offset</label>
                                            
                                        </td>
                                        <td scope="col" title="Select Pie Chart Label Offset">
                                            <asp:DropDownList ID="ddlPielabeloffsetlist" runat="server" AutoPostBack="false" CssClass="dropdownlist">
                                                <asp:ListItem>0:0</asp:ListItem>
                                                <asp:ListItem>0:10</asp:ListItem>
                                                <asp:ListItem>10:0</asp:ListItem>
                                                <asp:ListItem>10:10</asp:ListItem>
                                            </asp:DropDownList></td>
                                    </tr>
                                    <tr>
                                        <td scope="col" title="Pie Chart Legend">
                                        <label for="chkShowlegend" class="label">Show Legend</label>
                                            </td>
                                        <td scope="col" title="Select Pie Chart Legend">
                                            <asp:CheckBox ID="chkShowlegend" runat="server" Checked="True" Text="" /></td>
                                    </tr>
                                    <tr>
                                        <td scope="col" title="Pie Chart Drawing Style">
                                        <label for="ddldrawing" class="label">Drawing Style</label>
                                            </td>
                                        <td scope="col" title="Select Pie Chart Drawing Style">
                                            <asp:DropDownList ID="ddldrawing" runat="server" CssClass="dropdownlist">
                                                <asp:ListItem Value="Default">Default</asp:ListItem>
                                                <asp:ListItem Selected="True" Value="SoftEdge">SoftEdge</asp:ListItem>
                                                <asp:ListItem Value="Concave">Concave</asp:ListItem>
                                            </asp:DropDownList></td>
                                    </tr>
                                </table>
                            </div>
                            <div id="divdaughnt" runat="server" style="display: none;" title="Select Daughnt Chart Properties like LabelStyle,Hole size,Line type,label offset, Radius">
                                <table border="2" summary="This table hold the Daughnt Chart Properties like LabelStyle,Hole size,Line type,label offset, Radius" title="Daughnt Chart Properties">
                                    <tr>
                                        <td scope="col" title="Duaghnt Chart Hole style ">
                                            <label for="ddlDaughntlablestyle" class="label">
                                                LabelStyle</label>
                                        </td>
                                        <td scope="col" title="Select Duaghnt Chart Label Style">
                                            <asp:DropDownList ID="ddlDaughntlablestyle" runat="server" CssClass="dropdownlist">
                                                <asp:ListItem Selected="True" Value="Inside">Inside</asp:ListItem>
                                                <asp:ListItem Value="Outside">Outside</asp:ListItem>
                                                <asp:ListItem Value="Disabled">Disabled</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td scope="col" title="Duaghnt Chart Line Style">
                                        <label for="ddlDaughntlinestyle" class="label">
                                            lineArrowType</label>
                                         
                                        </td>
                                        <td scope="col" title="Select Duaghnt Chart Line Style">
                                            <asp:DropDownList ID="ddlDaughntlinestyle" runat="server" AutoPostBack="false" CssClass="dropdownlist">
                                                <asp:ListItem>None</asp:ListItem>
                                                <asp:ListItem>Triangle</asp:ListItem>
                                                <asp:ListItem>SharpTriangle</asp:ListItem>
                                                <asp:ListItem>Lines</asp:ListItem>
                                            </asp:DropDownList></td>
                                    </tr>
                                    <tr>
                                        <td scope="col" title="Duaghnt Chart Line Size">
                                        <label for="ddlDaughntlinesize" class="label">
                                            Line Size</label>
                                       </td >
                                        <td scope="col" title="Select Duaghnt Chart Line Size">
                                            <asp:DropDownList ID="ddlDaughntlinesize" runat="server" AutoPostBack="false" CssClass="dropdownlist">
                                                <asp:ListItem>1</asp:ListItem>
                                                <asp:ListItem>2</asp:ListItem>
                                            </asp:DropDownList></td>
                                    </tr>
                                    <tr>
                                        <td scope="col" title="Duaghnt Chart Label Offset">
                                        <label for="ddlDaughntoffset" class="label">
                                            Label Offset</label>
                                            
                                        </td>
                                        <td scope="col" title="Select Duaghnt Chart Label Offset">
                                            <asp:DropDownList ID="ddlDaughntoffset" runat="server" AutoPostBack="false" CssClass="dropdownlist">
                                                <asp:ListItem>0:0</asp:ListItem>
                                                <asp:ListItem>0:10</asp:ListItem>
                                                <asp:ListItem>10:0</asp:ListItem>
                                                <asp:ListItem>10:10</asp:ListItem>
                                            </asp:DropDownList></td>
                                    </tr>
                                    <tr>
                                        <td scope="col" title="Duaghnt Chart Legend">
                                        <label for="chkDaughntshowlegend" class="label">
                                            ShowLegend</label>
                                            </td>
                                        <td scope="col" title="Select Duaghnt Chart Legend">
                                            <asp:CheckBox ID="chkDaughntshowlegend" runat="server" Checked="True" Text="" /></td>
                                    </tr>
                                    <tr>
                                        <td scope="col" title="Duaghnt Chart Drawing Style"> 
                                        <label for="ddlDaughntdrawing" class="label">
                                            DrawingStyle</label>
                                            </td>
                                        <td scope="col" title="Select Duaghnt Chart Drawing Style">
                                            <asp:DropDownList ID="ddlDaughntdrawing" runat="server" CssClass="dropdownlist">
                                                <asp:ListItem Value="Default">Default</asp:ListItem>
                                                <asp:ListItem Selected="True" Value="SoftEdge">SoftEdge</asp:ListItem>
                                                <asp:ListItem Value="Concave">Concave</asp:ListItem>
                                            </asp:DropDownList></td>
                                    </tr>
                                    <tr>
                                        <td valign="top" scope="col" title="Duaghnt Chart Doughnut Radius (%)">
                                            <label for="ddlHoleSizeList" class="label"> Radius (%)</label>
                                        </td>
                                        <td scope="col" title="Select Duaghnt Chart Doughnut Radius (%)">
                                            <asp:DropDownList ID="ddlHoleSizeList" runat="server" AutoPostBack="false" CssClass="dropdownlist" Enabled="False">
                                                <asp:ListItem Value="20">20</asp:ListItem>
                                                <asp:ListItem Value="30">30</asp:ListItem>
                                                <asp:ListItem Value="40">40</asp:ListItem>
                                                <asp:ListItem Value="50">50</asp:ListItem>
                                                <asp:ListItem Selected="True" Value="60">60</asp:ListItem>
                                                <asp:ListItem Value="70">70</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <div id="divArea" runat="server" style="display: none;" title="Select Types of area chart, Line type ,Axis margin.">
                                <table border="2" summary="This table hold the Types of area chart, Line type." title="Area Chart Properties">
                                                                       <tr>
                                        <td scope="col" title="Area Chart X Axis Margins.">
                                        <label for="chkShowmargins" class="label">
                                            X AxisMargins</label>
                                            </td>
                                        <td scope="col" title="select Area Chart X Axis Margins.">
                                            <asp:CheckBox ID="chkShowmargins" runat="server" Text="" /></td>
                                    </tr>
                                   </table>
                            </div>
                            <div id="divScatter" runat="server" style="display: none;" title="Scatter Chart Prpperties">
                                <table border="2" summary="This table hold the Scatter chart Properties like Label Position,MarkerShape,Markersize,Marker Color." title="Scatter Chart Prpperties">
                                    <tr>
                                        <td scope="col" title="Scatter Chart Point Label List" style="height: 36px">
                                            <label for="ddlPointlabelslist" class="label">Point Label Position:</label>
                                         </td>
                                        <td scope="col" title="Select Scatter Chart Point Label List" style="height: 36px">
                                            <asp:DropDownList ID="ddlPointlabelslist" runat="server" ToolTip="Select Scatter Chart Point Label List" CssClass="dropdownlist">
                                                <asp:ListItem Selected="True" Value="None">None</asp:ListItem>
                                                <asp:ListItem Value="TopLeft">TopLeft</asp:ListItem>
                                                <asp:ListItem Value="Top">Top</asp:ListItem>
                                                <asp:ListItem Value="TopRight">TopRight</asp:ListItem>
                                                <asp:ListItem Value="Right">Right</asp:ListItem>
                                                <asp:ListItem Value="BottomRight">BottomRight</asp:ListItem>
                                                <asp:ListItem Value="Bottom">Bottom</asp:ListItem>
                                                <asp:ListItem Value="BottomLeft">BottomLeft</asp:ListItem>
                                                <asp:ListItem Value="Left">Left</asp:ListItem>
                                                <asp:ListItem Value="Center">Center</asp:ListItem>
                                            </asp:DropDownList></td>
                                    </tr>
                                    <tr>
                                        <td scope="col" title="Scatter Chart MarkerShape List">
                                            <label for="ddlMarkershapelist" class="label">
                                                MarkerShape</label>
                                            </td>
                                        <td scope="col" title="Select Scatter Chart MarkerShape List">
                                            <asp:DropDownList ID="ddlMarkershapelist" runat="server" CssClass="dropdownlist">
                                                <asp:ListItem Selected="True" Value="Circle">Circle</asp:ListItem>
                                                <asp:ListItem Value="Diamond">Diamond</asp:ListItem>
                                                <asp:ListItem Value="Cross">Cross</asp:ListItem>
                                            </asp:DropDownList></td>
                                    </tr>
                                    <tr>
                                        <td scope="col" title="Scatter Chart MarkerShape Color">
                                            <label for="ddlMarkersizelist" class="label"> Markercolour</label>
                                           </td>
                                        <td scope="col" title="Select Scatter Chart MarkerShape Color">
                                            <asp:DropDownList ID="ddlMarkershapecolour" runat="server" CssClass="dropdownlist">
                                                <asp:ListItem Selected="True" Value="Chocolate">Original Color</asp:ListItem>
                                                <asp:ListItem Value="Aquamarine">Aquamarine</asp:ListItem>
                                                <asp:ListItem Value="BlueViolet">BlueViolet</asp:ListItem>
                                                <asp:ListItem Value="Red">Red</asp:ListItem>
                                                <asp:ListItem Value="Yellow">Yellow</asp:ListItem>
                                                <asp:ListItem Value="Coral">Coral</asp:ListItem>
                                                <asp:ListItem Value="Green">Green</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td scope="col" title="Scatter Chart MarkerSize"> 
                                            <label for="ddlMarkersizelist" class="label">
                                                Marker Size</label>
                                            </td>
                                        <td scope="col" title="Select Scatter Chart MarkerSize">
                                            <asp:DropDownList ID="ddlMarkersizelist" runat="server" CssClass="dropdownlist">
                                                <asp:ListItem Value="5">5</asp:ListItem>
                                                <asp:ListItem Selected="True" Value="7">7</asp:ListItem>
                                                <asp:ListItem Value="10">10</asp:ListItem>
                                                <asp:ListItem Value="12">12</asp:ListItem>
                                                <asp:ListItem Value="18">18</asp:ListItem>
                                            </asp:DropDownList></td>
                                    </tr>
                                </table>
                            </div>
                            <div id="divLine" runat="server" style="display: none;" title="Line Chart Prpperties">
                                <table summary="This table hold the Line chart properties like Line Chart Type,Points labels, Points marker size" title="Line Chart Properties">
							        <tr>
								<td scope="col" title="Line Chart Types">
								    <label for="ddlLinecharttypelist" class="label">
                                        LineChartType</label>
								</td>
								<td scope="col" title=" Select Line Chart Types">
								    <asp:DropDownList id="ddlLinecharttypelist" runat="server" CssClass="dropdownlist">
										<asp:ListItem Value="Line">Line</asp:ListItem>
										<asp:ListItem Value="Spline" Selected="True">Spline</asp:ListItem>
										<asp:ListItem Value="StepLine">StepLine</asp:ListItem>
									</asp:DropDownList>
							    </td>
							</tr>
							        <tr>
								<td scope="col" title="Line Chart Point Labels ">
								    <label for="ddlLinepointlabelslist" class="label">Point Labels</label>
								</td>
								<td scope="col" title="Select Line Chart Point Labels">
									<asp:dropdownlist id="ddlLinepointlabelslist" runat="server" CssClass="dropdownlist">
										<asp:ListItem Value="None" Selected="True">None</asp:ListItem>
										<asp:ListItem Value="Auto">Auto</asp:ListItem>
										<asp:ListItem Value="TopLeft">TopLeft</asp:ListItem>
										<asp:ListItem Value="Top">Top</asp:ListItem>
										<asp:ListItem Value="TopRight">TopRight</asp:ListItem>
										<asp:ListItem Value="Right">Right</asp:ListItem>
										<asp:ListItem Value="BottomRight">BottomRight</asp:ListItem>
										<asp:ListItem Value="Bottom">Bottom</asp:ListItem>
										<asp:ListItem Value="BottomLeft">BottomLeft</asp:ListItem>
										<asp:ListItem Value="Left">Left</asp:ListItem>
										<asp:ListItem Value="Center">Center</asp:ListItem>
									</asp:dropdownlist>
								</td>
							</tr>
							        <tr>
								        <td scope="col" title="Line Chart X Axis Margins">
								            <label for="chkShowlinemargins" class="label"> X Axis Margins</label>
								        </td>
								        <td scope="col" title=" Show Line Chart X Axis Margins">
								            <asp:CheckBox id="chkShowlinemargins" runat="server" Text="margin"  Checked="True"></asp:CheckBox>
								        </td>
							        </tr>
							        <tr>
								        <td scope="col" title="Line Chart Markers Lines">
								            <label for="chkShowlinemarkers" class="label"> Markers Lines</label>
								        </td>
								        <td scope="col" title="Select Line Chart Markers Lines">
								            <asp:CheckBox id="chkShowlinemarkers" runat="server"  Text="marker"></asp:CheckBox>
								        </td>
							        </tr>
						        </table>
                            </div>
                        </td>
                    </tr>
                </table>
                
            </td>
        </tr>
   </table>
  <%--<img src="../images/Chart/RunChart.jpg"/>--%>
    <asp:HiddenField ID="copyrep" runat="server" />
    <%--This hidden field is used for Clendar control--%>
    <asp:HiddenField ID="Calender" runat="server" />
    <%-- hidden report/constituent backcolor --%>              
    <asp:HiddenField ID="hid" runat="server" />
    <%--<asp:HiddenField ID="formulaarray" runat="server" />
    <asp:HiddenField ID="hidgroup" runat="server" />--%>
      <%--<asp:HiddenField ID="hidcolumname"  runat="server" />--%>
    <%--<asp:HiddenField ID="hidtablename"  runat="server" />--%>
    <asp:HiddenField ID="HiddenField2" runat="server" />
    <%--<asp:HiddenField ID="hidhaving" runat="server" />
    <asp:HiddenField ID="hidwheretxt" runat="server" />
    <asp:HiddenField ID="hidorder" runat="server" />--%>
    <asp:HiddenField ID="reotablename" runat="server" />
    <asp:HiddenField ID="totalcol" runat="server" />
    <asp:HiddenField ID="opentotalcolumn" runat="server" />
      <asp:HiddenField ID="Currentrepdept" runat="server" />
      <asp:HiddenField ID="Currentrepclient" runat="server" />
      <asp:HiddenField ID="Currentreplob" runat="server" />
    <asp:HiddenField ID="openselectedcolumn" runat="server" />
    <asp:HiddenField ID="seriesbtn" runat="server" />
    <asp:HiddenField ID="commangraphfprmat" runat="server" />
    <asp:HiddenField ID="specificformat" runat="server" />
    <input type="hidden" name="abc" id="abc" />
    <input type="hidden" name="hidcolumname" id="hidcolumname" runat="server" />
    <input type="hidden" name="hidgroup" id="hidgroup" runat="server" />
    <input type="hidden" name="hidhaving" id="hidhaving" runat="server" />
    <input type="hidden" name="hidwheretxt" id="hidwheretxt" runat="server" />
    <input type="hidden" name="hidorder" id="hidorder" runat="server" />
    <input type="hidden" name="hidtablename" id="hidtablename" runat="server" />
    <input type="hidden" name="formulaarray" id="formulaarray" runat="server" />
    <input type="hidden" name="hidreportname" id="hidreportname" runat="server" />
    <input type="hidden" name="prp" id="prp" />
    <input type="hidden" name="Report" id="Report" />
    <input type="hidden" name="department" id="department" />
    <input type="hidden" name="client" id="client" />
    <input type="hidden" name="lob" id="lob" />
    <input type="hidden" name="Graph" id="Graph" />
    <input type="hidden" name="ToDate" id="ToDate" />
    <input type="hidden" name="FromDate" id="FromDate" />
    <input type="hidden" name="ColumnName" id="ColumnName" />
    <input type="hidden" name="ColumnSeries" id="ColumnSeries" />
     <input type="hidden" name="CreatedOn" id="CreatedOn" />
      <input type="hidden" name="SavedBy" id="SavedBy" />
      <input type="hidden" name="totalcolumn" id="totalcolumn" />
    </asp:Content>
<%------------------ Change History -------------------------
1. Dated: 20-03-08
   Changes Made By: Ekta Goyal
   Change Summary:Change left panel for modify multiple chart at a time
                   boz div was not work properly in Graphdata page.
2. Dated: 06-04-08
   Changes Made By: Ekta Goyal
   Change Summary: Change left panel for selct multiple chart
                   boz GraphDesign page was not work properly. 
                   
   --%>
