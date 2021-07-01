<%--Project Name: IDMS Phase 2
    Module Name: graphical Presentation
    Page Name: Opengraph
    Summary: Create different types of chart on the basis of DataAnalysis and Advance Report Designer
    Created on: 10/03/08
    Created By: Ekta Goyal

--%>


<%@ Page Language="VB"  EnableEventValidation="false" ValidateRequest="false"  EnableViewState="true" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="false" CodeFile="Open.aspx.vb" Inherits="Open" title="OpenGraph Page" %>
<%@ Register TagPrefix="dcwc" Namespace="Dundas.Charting.WebControl" Assembly="DundasWebChart" %>

<asp:Content ID="leftPane" runat="server" ContentPlaceHolderID="LeftPlaceHolder">
     <script language="javascript" src="../js/202pop.js" type="text/javascript"></script>
    <script language="Javascript" src="../js/collapseableDIV.js" type="text/javascript"></script>
  <script language="JavaScript" type="text/javascript" src="../js/picker.js"></script>      
    

<script language="javascript" type="text/javascript"> 

function gridshow()
{
window.open("showgraph.aspx","ViewGraph",'height=500px');
}
function drillshow()
{
window.open("Detailedchart.aspx","DrillGraph",'height=500px');
}
function gridshowclose()
{
window.open("showgraph.aspx","ViewGraph",'height=500px');
}
 function ShowCalendar()
		{
		     window.txtBox = document.getElementById('<%=txtfromdate.ClientID%>');
		     window.txtBox.value = window.showModalDialog('../Calendar/Calendar.htm',window.txtBox.value,'dialogLeft:200px;dialogTop:200px;dialogHeight:210px;dialogWidth:265px;center:No;help:No;scroll:No;resizable:No;status:No;');
				if (window.txtBox.value=="undefined")			
				{
				    window.txtBox.value="";    
				}
		}
		function ShowCalendar1()
		{
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
	{ //debugger;
	        getValues();
	       	window.open("Savegraph.aspx","SaveGraph","width=400,height=220,scrollbars=yes,status=yes");
	}
	function getValues()
	{
//	    debugger;
	    if(document.getElementById("<%=chkShow3D.ClientID%>").checked==false)
	    {
	       var chk="off"; 
	    }
	    else 
	    {
	     chk="on";
	    }
	    document.getElementById("abc").value="XAngle:" + document.getElementById("<%=ddlXangle.ClientID%>").value  + "$" + "Perspective:" + document.getElementById("<%=ddlPerspective.ClientID%>").value + "$" + "Chk3D:"+ chk + "$" + "Yangle:"+ document.getElementById("<%=ddlYang.ClientID%>").value + "$" + "Palettes:" + document.getElementById("<%=ddlPalettes.ClientID%>").value + "$" + "Bkclr:" + document.getElementById("<%=bkcolor.ClientID%>").value + "$" + "Gradient:" + document.getElementById("<%=ddlGradient.ClientID%>").value + "$" + "Hatchstyle:" + document.getElementById("<%=ddlHatchstyle.ClientID%>").value + "$" +  "Brclr:" + document.getElementById("<%=brcolor.ClientID%>").value + "$" +  "Bordersize:" + document.getElementById("<%=ddlBordersize.ClientID%>").value + "$" + "Borderstyle:" + document.getElementById("<%=ddlBorderstyle.ClientID%>").value + "$" + "Xlabelfont:" + document.getElementById("<%=ddlXlabelfont.ClientID%>").value+ "$" + "Xfontsizelist:" + document.getElementById("<%=ddlXfontsizelist.ClientID%>").value + "$" + "XLabelcolour:" +  document.getElementById("<%=xaxiscolor.ClientID%>").value + "$" + "Xontanglelist:" + document.getElementById("<%=ddlXontanglelist.ClientID%>").value + "$" + "Offset:" + document.getElementById("<%=chkXoffset.ClientID%>").value;
	    document.getElementById("abc1").value="Enable:" + document.getElementById("<%=chkXenable.ClientID%>").value+ "$" + "Yfont:" + document.getElementById("<%=ddlYfontname.ClientID%>").value + "$" + "Yfontsize:" + document.getElementById("<%=ddlYlabelfontsize.ClientID%>").value + "$" + "Yfontcolour:" + document.getElementById("<%=yaxiscolor.ClientID%>").value + "$" + "Yangle:" + document.getElementById("<%=ddlYangle.ClientID%>").value + "$" + "chkYoffset:" + document.getElementById("<%=chkYoffset.ClientID%>").value + "$" + "Yenable:" + document.getElementById("<%=chkYenable.ClientID%>").value + "$" + "Charttitle:" + document.getElementById("<%=txtCharttitle.ClientID%>").value + "$" + "XAxisTitle:" + document.getElementById("<%=txtTitleext.ClientID%>").value + "$" + "Yaxistitle:" + document.getElementById("<%=txtYTitle.ClientID%>").value + "$" + "Titlesize:" + document.getElementById("<%=ddlTitlesize.ClientID%>").value + "$" + "Font:" + document.getElementById("<%=ddlFont1.ClientID%>").value + "$" + "Color:" + document.getElementById("<%=titlefontcolor.ClientID%>").value + "$" + "BrClr:" + document.getElementById("<%=titlebordercolor.ClientID%>").value + "$"+ "ArClr:" + document.getElementById("<%=chartareabkcolor.ClientID%>").value ;
	    document.getElementById("abc2").value="BkClr:" + document.getElementById("<%=titlebkcolor.ClientID%>").value + "$" + "Alignment:" + document.getElementById("<%=Alignment.ClientID%>").value + "$" + "Italic:" + document.getElementById("<%=chkItalic.ClientID%>").value + "$" + "Bold:" + document.getElementById("<%=chkBold.ClientID%>").value + "$"  + "Underline:" + document.getElementById("<%=chkUline.ClientID%>").value + "$" + "Strikeout:" + document.getElementById("<%=chkSout.ClientID%>").value + "$" + "Mjrgdline:" + document.getElementById("<%=ddlMajorgridline.ClientID%>").value+ "$" + "Linetypes:" + document.getElementById("<%=ddlLinetypes.ClientID%>").value + "$" + "Mjrgdclr:" + document.getElementById("<%=Majorgridcolour.ClientID%>").value + "$" + "Mjrline:" + document.getElementById("<%=ddlMajorline.ClientID%>").value + "$" + "MjrInt:" + document.getElementById("<%=ddlMajorInterval.ClientID%>").value + "$" + "MnrType:" + document.getElementById("<%=ddlMinorType.ClientID%>").value + "$" + "MnrgidLineType:" + document.getElementById("<%=ddlMinoeLinetypes.ClientID%>").value + "$" + "MnrClr:" + document.getElementById("<%=ddlMinorColor1.ClientID%>").value + "$" + "MnrWidth:" + document.getElementById("<%=ddlMinorWidth.ClientID%>").value + "$" + "MnrInt:" + document.getElementById("<%=ddlMinorInterval.ClientID%>").value;
	        
	   var chartname=document.getElementById("<%=hidchart.ClientID%>").value;
	  document.getElementById("prp").value="Linetype";

	     	var Repname;
	     	var currentspan;
	     	var currentclientspan;
	     	var currentlobsapn;
	     	  if(("<%=currsp%>")=="")
	     	        {
	     	            Repname=document.getElementById("<%=ddlReport.ClientID%>").options[document.getElementById("<%=ddlReport.ClientID%>").selectedIndex].text;
	     	            currentspan=document.getElementById("<%=ddldepartmant.ClientID%>").value;
	     	            currentclientspan=document.getElementById("<%=hiddclient.ClientID%>").value;
	     	            currentlobsapn=document.getElementById("<%=ddllob.ClientID%>").value;
	     	        }
  	     	        else 
	     	        {
	     	            Repname=document.getElementById("<%=txtcurrentreport.ClientID%>").value;
	     	            currentspan=document.getElementById("<%=Currentrepdept.ClientID%>").value; 
                        currentclientspan=document.getElementById("<%=Currentrepclient.ClientID%>").value;
                        currentlobsapn=document.getElementById("<%=Currentreplob.ClientID%>").value;
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
	   document.getElementById("ReportType").value=document.getElementById("<%=RepGraphType.ClientID%>").value;
    
        

	}
    </script>
 
<div id="divScroll"  style="height:700px; width:220px; overflow:scroll; scrollbar-arrow-color:#0582BE; scrollbar-base-color:#58C7FC;">
   <div id="divScroll1" onclick="toggleDiv('divChart', 'imgChart',500)" style="cursor: pointer;background-color: #4D8678; color: White;">
        <table summary="">
            <tr>
                <td style="width: 190px" scope="col">
                    <label for="imgChart" title="Select Chart" class="leftLabel"><b>Select Chart</b></label> 
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
                <td align="center" style="height: 15px" valign="top" scope="col">
                <asp:ImageButton ID="imgcolumn" runat="server" AlternateText="Click Here To Select Column Chart" ImageUrl="../images/Chart/ColumnChart.jpg"
                        Style="filter: alpha(opacity=50); -moz-opacity: 0.5; height: 30px; width: 30;" />
                </td>
                                         
                 <td align="left" style="height: 11px; color: #ffffff; font-weight: bold; font-size: 10px;
                    font-family: Verdana;  width: 282px;" valign="middle" scope="col">
                    <asp:LinkButton ID="lnkColumnchart"  runat="server" Text="Column chart" ToolTip="Click On Column Chart" ForeColor="White"></asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td colspan="2" style="background-color: #86c1cc" scope="colgroup">
                </td>
            </tr>
            <tr>
                <td style="width: 46px; height: 15px;" valign="middle" scope="col">
                    <asp:ImageButton ID="imgArea" runat="server" AlternateText="Click Here To Select Area Chart"
                        src="../images/Chart/AreaChart.jpg"
                        Style="filter: alpha(opacity=50); -moz-opacity: 0.5; height: 30px; width: 30;" />
                </td>
                <td align="left" style="height: 15px; color: #ffffff; font-weight: bold; font-size: 10px;
                    font-family: Verdana; width: 282px;" scope="col">
                    <asp:LinkButton ID="LinkButton2" runat="server" ForeColor="white"  Text="Area chart" ToolTip="Click On Area Chart"></asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td bgcolor="#86c1cc" colspan="2" scope="colgroup">
                </td>
            </tr>
            <tr>
                <td style="width: 46px; height: 15px;" scope="col">
                    <asp:ImageButton ID="imgPie" runat="server" AlternateText="Click Here To Select Pie Chart"
                        src="../images/Chart/PieChart.jpg"
                       Style="filter: alpha(opacity=50); -moz-opacity: 0.5; height: 30px; width: 30;" />
                </td>
                <td align="left" style="height: 15px; color: #ffffff; font-weight: bold; font-size: 10px;
                    font-family: Verdana; width: 282px;" scope="col">
                    <asp:LinkButton ID="LinkButton3" runat="server" ForeColor="white" Text="Pie chart" ToolTip="Click On Pie Chart"></asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td bgcolor="#86c1cc" colspan="2" scope="colgroup">
                </td>
            </tr>
            <tr>
                <td style="width: 46px; height: 15px;" scope="col">
                    <asp:ImageButton ID="imgLine" runat="server" AlternateText="Click Here To Select Line Chart" ImageUrl="../images/Chart/LineChart.jpg"
                        Style="filter: alpha(opacity=50); -moz-opacity: 0.5; height: 30px; width: 30;" />
                </td>
                <td align="left" style="height: 15px; color: #ffffff; font-weight: bold; font-size: 10px;
                    font-family: Verdana; width: 282px;" scope="col">
                    <asp:LinkButton ID="LinkButton4" runat="server" ForeColor="white" Text="Line chart" ToolTip="Click On Line Chart"></asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td bgcolor="#86c1cc" colspan="2" scope="colgroup">
                </td>
            </tr>
            <tr>
                <td style="width: 46px; height: 15px;" scope="col">
                    <asp:ImageButton ID="imgScatter" runat="server" AlternateText="Click Here To Select Scatter Chart"
                        src="../images/Chart/ScatterChart.jpg"
                        Style="filter: alpha(opacity=50); -moz-opacity: 0.5; height: 30px; width: 30;" />
                </td>
                <td align="left" style="height: 15px; color: #ffffff; font-weight: bold; font-size: 10px;
                    font-family: Verdana; width: 282px;" scope="col">
                    <asp:LinkButton ID="LinkButton5" runat="server" ForeColor="white" Text="XY(Scatter) chart" Width="152px" ToolTip="Click On Scatter Chart"></asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td bgcolor="#86c1cc" colspan="2"  scope="colgroup">
                </td>
            </tr>
            <tr>
                <td style="width: 46px; height: 15px;" scope="col">
                    <asp:ImageButton ID="imgHistogram" runat="server" AlternateText="Click Here To Select Histogram Chart"
                        ImageUrl="../images/Chart/HistogramChart.jpg"
                        Style="filter: alpha(opacity=50); -moz-opacity: 0.5; height: 30px; width: 30;" />
                </td>
                <td align="left" style="height: 15px; color: #ffffff; font-weight: bold; font-size: 10px;
                    font-family: Verdana; width: 282px;" scope="col">
                    <asp:LinkButton ID="LinkButton6" runat="server" ForeColor="white" Text="Histogram chart" ToolTip="Click On Histogram Chart"></asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td bgcolor="#86c1cc" colspan="2" scope="colgroup">
                </td>
            </tr>
            <tr>
                <td style="width: 46px; height: 15px;" scope="col">
                    <asp:ImageButton ID="imgPareto" runat="server" AlternateText="Click Here To Select Pareto  Chart"
                        ImageUrl="../images/Chart/ParetoChart.jpg"
                        Style="filter: alpha(opacity=50); -moz-opacity: 0.5; height: 30px; width: 30;" />
                </td>
                <td align="left" style="height: 15px; color: #ffffff; font-weight: bold; font-size: 10px;
                    font-family: Verdana; width: 282px;" scope="col">
                    <asp:LinkButton ID="LinkButton7" runat="server" ForeColor="white" Text="Pareto chart" ToolTip="Click On Pareto Chart"></asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td bgcolor="#86c1cc" colspan="2" scope="colgroup" >
                </td>
            </tr>
            <tr>
                <td style="width: 46px; height: 15px;">
                    <asp:ImageButton ID="imgRun" runat="server" AlternateText="Click Here To Select Run Chart"
                        ImageUrl="../images/Chart/RunChart.jpg"
                        Style="filter: alpha(opacity=50); -moz-opacity: 0.5; height: 30px; width: 30;" />
                </td>
                <td align="left" style="height: 15px; color: #ffffff; font-weight: bold; font-size: 10px;
                    font-family: Verdana; width: 282px;" scope="col">
                    <asp:LinkButton ID="LinkButton8" runat="server" ForeColor="white" Text="Run chart" ToolTip="Click On Run Chart"></asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td bgcolor="#86c1cc" colspan="2" scope="colgroup">
                </td>
            </tr>
            <tr>
                <td style="width: 46px; height: 15px;" scope="col">
                    <asp:ImageButton ID="imgScaterplot" runat="server" AlternateText="Click Here To Select ScatterPlot Chart"
                        ImageUrl="../images/Chart/ScaterPlotChart.jpg"
                        Style="filter: alpha(opacity=50); -moz-opacity: 0.5; height: 30px; width: 30;" />
                </td>
                <td align="left" style="height: 15px; color: #ffffff; font-weight: bold; font-size: 10px;
                    font-family: Verdana; width: 282px;" scope="col">
                    <asp:LinkButton ID="LinkButton9" runat="server" ForeColor="white" Text="ScatterPlot chart" ToolTip="Click On ScatterPlot Chart" Width="144px"></asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td bgcolor="#86c1cc" colspan="2" scope="colgroup">
                </td>
            </tr>
            <tr>
                <td style="width: 46px; height: 15px;" scope="col">
                    <asp:ImageButton ID="imgStock" runat="server" AlternateText="Click Here To Select Stock Chart"
                        ImageUrl="../images/Chart/StockChart.jpg"
                        Style="filter: alpha(opacity=50); -moz-opacity: 0.5; height: 30px; width: 30;" />
                </td>
                <td align="left" style="height: 15px; color: #ffffff; font-weight: bold; font-size: 10px;
                    font-family: Verdana; width: 282px;" scope="col">
                    <asp:LinkButton ID="LinkButton10" runat="server" ForeColor="white" Text="Stock chart" ToolTip="Click On Stock Chart"></asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td bgcolor="#86c1cc" colspan="2"  scope="colgroup">
                </td>
            </tr>
            <tr>
                <td style="width: 46px; height: 15px;">
                    <asp:ImageButton ID="imgDaughnt" runat="server" AlternateText="Click Here To Select Doughnut Chart"
                         ImageUrl="../images/Chart/DoughntChart.jpg"
                        Style="filter: alpha(opacity=50); -moz-opacity: 0.5; height: 30px; width: 30;" />
                </td>
                <td align="left" style="height: 15px; color: #ffffff; font-weight: bold; font-size: 10px;
                    font-family: Verdana; width: 282px;" scope="col">
                    <asp:LinkButton ID="LinkButton11" runat="server" ForeColor="white" Text="Doughnut chart" ToolTip="Click On Doughnut Chart"></asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td bgcolor="#86c1cc" colspan="2" scope="colgroup">
                </td>
            </tr>
            <tr>
                <td style="width: 46px; height: 15px;" scope="col">
                    <asp:ImageButton ID="imgBar" runat="server" AlternateText="Click Here To Select Bar Chart"
                         ImageUrl="../images/Chart/BarChart.jpg"
                        Style="filter: alpha(opacity=50); -moz-opacity: 0.5; height: 30px; width: 30;" />
                </td><%--<label class="leftLabel" for="ctl00_LeftPlaceHolder_ddlcolour">Line Color:</label>--%>
                <td align="left" style="height: 15px; color: #ffffff; font-weight: bold; font-size: 10px;
                    font-family: Verdana; width: 282px;" scope="col">
                    <asp:LinkButton ID="LinkButton12" runat="server" ForeColor="white" Text="Bar chart" ToolTip="Click On Bar Chart"></asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td bgcolor="#86c1cc" colspan="2" scope="colgroup">
                </td>
            </tr>
        </table>
    </div>
    
    <div id="div3" onclick="toggleDiv('divShow3dchart', 'imgFormat1',110)"
        style="cursor: pointer; background-color: #4D8678; color: White;">
        <table>
            <tr>
                <td style="width: 190px" scope="col">
                    <label for="imgFormat1" title="Chart In 3D" class="leftLabel"><b>Chart In 3D</b></label> 
                </td>
                <td align="right" scope="col">
                    <img id="imgFormat1" alt="Expand/Collapse" src="../images/ArrowDown.gif" />
                </td>
            </tr>
        </table>
    </div>
    <div id="divShow3dchart" style="overflow: hidden; display: none; color: White;">
    

        <table summary="This table holds 3D chart Properties" width="80%">
                  <tr>
                <td scope="col" title="3D Chart">
                    <label class="leftLabel" for="ctl00_LeftPlaceHolder_chkShow3D">Show 3D</label>
                </td>
                <td scope="col" title="Show Chart as 3D" style="width: 106px">
                    <asp:CheckBox ID="chkShow3D" ForeColor="WHITE" runat="server" Text="3D" />
                </td>
            </tr>
            <tr>
                <td scope="col" title="Chart Perspective">
                    <label class="leftLabel" for="ctl00_LeftPlaceHolder_ddlPerspective">Perspective</label>
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
                    <label class="leftLabel" for="ctl00_LeftPlaceHolder_ddlXangle">Rotate X</label>
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
                    <label class="leftLabel" for="ctl00_LeftPlaceHolder_ddlYang">Rotate Y</label>
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
    
    
    <div id="div6" onclick="toggleDiv('divChartappearance', 'img6',200)"
        style="cursor: pointer; background-color: #559585; color: White;">
        <table>
            <tr>
                <td style="width: 190px" scope="col">
                    <label for="img6" title="BackGround Apperance" class="leftLabel"><b>BackGround Apperance</b></label> 
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
                    <label class="leftLabel" for="ctl00_LeftPlaceHolder_ddlPalettes">Palettes</label>
                </td>
                <td scope="col" title="Select Chart Background Palettes" style="width: 135px">
                    <asp:DropDownList ID="ddlPalettes" runat="server" CssClass="leftdropdownlist">
                        <asp:ListItem Value ="SeaGreen">SeaGreen</asp:ListItem>
                        <asp:ListItem Value ="EarthTones">EarthTones</asp:ListItem>
                        <asp:ListItem Value ="Pastel">Pastel</asp:ListItem>
                        <asp:ListItem Value ="Excel" >Excel</asp:ListItem>
                        <asp:ListItem  Value ="Dundas" Selected="True">Dundas</asp:ListItem>
                       <asp:ListItem Value="Kindergarten">Kindergarten</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            
            <tr>
                <td scope="col" title="Chart Backcolor" style="width: 118px;">
                 <a href="javascript: onclick=pickerPopup202('<%=bkcolor.ClientID%>','sample_2');">BackColor</a>
                </td>
                <td scope="col" align="left" valign="bottom" title="Select Chart Backcolor" style=" height:26px; width: 200px;">
<label for="sample_2" ></label>
                    <input type="text" id="sample_2"  enableviewstate="true" style="width: 48px;"/>
               </td>
            </tr>
            <tr>
                <td scope="col" title="Chart Gradient Style" style="width: 118px; height: 22px;">
                    <label class="leftLabel" for="ctl00_LeftPlaceHolder_ddlGradient">Gradient</label>
                </td>
                <td scope="col" title="Select Chart Gradient" style="width: 135px; height: 22px;">
                    <asp:DropDownList ID="ddlGradient" runat="server" OnSelectedIndexChanged="Gradient_SelectedIndexChanged"
                        CssClass="leftdropdownlist">
                    </asp:DropDownList>
               </td>
            </tr>
            <tr>
                <td scope="col" title="Chart Hatch Style" style="width: 118px">
                    <label class="leftLabel" for="ctl00_LeftPlaceHolder_ddlHatchstyle">HatchStyle</label>
                </td>
                <td scope="col" title="Select Chart Hatch Style" style="width: 135px">
                    <asp:DropDownList ID="ddlHatchstyle" runat="server" OnSelectedIndexChanged="HatchStyle_SelectedIndexChanged"
                        CssClass="leftdropdownlist">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td scope="col" title="Chart Border color" style="width: 118px">
                    <%-- <img src="../images/bkcolor1.jpg" id="Img12" onclick="majorbkcolor('ctl00_LeftPlaceHolder_Majorgridcolour');" />--%>
                    <a href="javascript: onclick=pickerPopup202('<%=brcolor.ClientID%>','sampleBordercolor');">
                    Bordercolor</a>
                 </td>
                <td scope="col" title="Select Chart Border Color" style="width: 135px">
<label for="sampleBordercolor" ></label>
                    <input type="text" id="sampleBordercolor" style="width: 80px"/>
                    <%--<label class="leftLabel" for="ddlor1">LineColor:</label>--%>
               </td>
            </tr>
            <tr>
                <td scope="col" title="Chart Border Size" style="width: 118px">
                    <label class="leftLabel" for="ctl00_LeftPlaceHolder_ddlBordersize">Border Size</label>
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
                    <label class="leftLabel" for="ctl00_LeftPlaceHolder_ddlBorderstyle">Borderstyle</label>
                </td>
                <td scope="col" title="Select Chart Border Style" style="width: 135px">
                    <asp:DropDownList ID="ddlBorderstyle" runat="server" CssClass="leftdropdownlist">
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
                      <asp:TextBox  ID="back" runat="server" Visible="false"   Height="16px" style="z-index: 100; left: 0px; position: absolute; top: 0px" Width="32px" ></asp:TextBox>
    </div>
    <div id="divLegend" onclick="toggleDiv('divLegendappearance', 'img6',400)"
        style="cursor: pointer; background-color: #559585; color: White;">
        <table>
            <tr>
                <td style="width: 190px" scope="col">
                  <label for="img1" title="Legend Apperance" class="leftLabel"><b>Legend Apperance</b></label>
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
                 <a href="javascript: onclick=pickerPopup202('<%=legendbkcolor.ClientID%>','txtlegengbkcolor');">
                 BackColor</a>
                </td>
                <td scope="col" title="Select Legend Backcolor" style="height: 10px; width: 200px;">
<label for="txtlegengbkcolor" ></label>
                  <input type="text" id="txtlegengbkcolor" style="width: 80px"/>
               </td>
            </tr>
            <tr>
                <td scope="col" title="Legend Gradient Style" style="width: 118px">
                    <label class="leftLabel" for="ctl00_LeftPlaceHolder_ddlLegendgradient">Gradient</label>
                </td>
                <td scope="col" title="Select Legend Gradient" style="width: 135px">
                    <asp:DropDownList ID="ddlLegendgradient" runat="server" OnSelectedIndexChanged="Gradient_SelectedIndexChanged"
                        CssClass="leftdropdownlist">
                    </asp:DropDownList>
               </td>
            </tr>
            <tr>
                <td scope="col" title="Legend Hatch Style" style="width: 118px">
                    <label class="leftLabel" for="ctl00_LeftPlaceHolder_ddlLegendhatch">HatchStyle</label>
                </td>
                <td scope="col" title="Select Legend Hatch Style" style="width: 135px">
                    <asp:DropDownList ID="ddlLegendhatch" runat="server" OnSelectedIndexChanged="HatchStyle_SelectedIndexChanged"
                        CssClass="leftdropdownlist">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td scope="col" title="Legend Border color" style="width: 118px">
                     <a href="javascript: onclick=pickerPopup202('<%=legendbrcolor.ClientID%>','txtlegengbrcolor');">
                      Bordercolor</a>
                 </td>
                <td scope="col" title="Select Legend Border Color" style="width: 135px">
<label for="txtlegengbrcolor" ></label>
                    <input type="text" id="txtlegengbrcolor" style="width: 80px"/>
                </td>
            </tr>
            <tr>
                <td scope="col" title="Legend Border Size" style="width: 118px">
                    <label class="leftLabel" for="ctl00_LeftPlaceHolder_ddlLegendbordersize">Border Size</label>
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
                    <label class="leftLabel" for="ctl00_LeftPlaceHolder_ddlLegendborderstyle">Borderstyle</label>
                </td>
                <td scope="col" title="Select Legend Border Style" style="width: 135px">
                    <asp:DropDownList ID="ddlLegendborderstyle" runat="server" CssClass="leftdropdownlist">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
				<td scope="col" title="Show Average" style="width: 118px" >
				    <label class="leftLabel" for="ctl00_LeftPlaceHolder_chk_ShowAvg">Show Average</label>
				</td>
				<td scope="col" title="Check Show Average" style="width: 135px">
				    <asp:checkbox id="chk_ShowAvg" runat="server" Width="237px"></asp:checkbox></td>
			</tr>
			<tr>
				<td scope="col" title="Show Total" style="width: 118px" >
				    <label class="leftLabel" for="ctl00_LeftPlaceHolder_chk_ShowTotal">Show Total</label>
				</td>
				<td scope="col" title="Check Show Total" style="width: 135px">
					<asp:checkbox id="chk_ShowTotal" runat="server" Width="237px" ></asp:checkbox></td>
			</tr>
			<tr>
				<td  scope="col" title="Show Minimum" style="width: 118px">
				<label class="leftLabel" for="ctl00_LeftPlaceHolder_chk_ShowMin">Show Total</label>
				    <label class="leftLabel" for="ctl00_LeftPlaceHolder_chk_ShowMin">Show Minimum</label>
				</td>
				<td scope="col" title="Check Show Minimum" style="width: 135px">
					<asp:checkbox id="chk_ShowMin" runat="server" Width="237px"></asp:checkbox></td>
			</tr>
			<tr>
				<td scope="col" title="Show  Style" style="width: 118px" >
				   <label class="leftLabel" for="ctl00_LeftPlaceHolder_ddlLegendStyleList">Style</label>
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
				   <label class="leftLabel" for="ctl00_LeftPlaceHolder_ddlTheTableStyle">Table Style</label>
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
				<label class="leftLabel" for="ctl00_LeftPlaceHolder_ddlLegendDockingList">Docking</label>
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
				<label class="leftLabel" for="ctl00_LeftPlaceHolder_ddlLegendAlinmentList">Alignment</label>
					</td>
				<td scope="col" title="Legend Alinment List" style="width: 135px">
					<asp:dropdownlist id="ddlLegendAlinmentList" runat="server" Width="103px" CssClass="leftdropdownlist" >
					<asp:listitem Value="Near" Selected="True">Near</asp:listitem>
					<asp:listitem Value="Center">Center</asp:listitem>
					<asp:listitem Value="Far">Far</asp:listitem>
					</asp:dropdownlist></td>
			</tr>
            <%--<img src="../images/bkcolor1.jpg" id="Img11" onclick="minorbkcolor('ctl00_LeftPlaceHolder_ddlMinorColor1');" />--%>
			<tr>
				<td scope="col" title="Reversed" style="width: 118px">
				 <label class="leftLabel" for="ctl00_LeftPlaceHolder_chk_Reversed">Reversed</label>
				 </td>
				<td scope="col" title="Check Reversed" style="width: 135px">
					<asp:checkbox id="chk_Reversed" runat="server" ></asp:checkbox></td>
				</tr>
		</table>
    </div>
    
    <div id="div9" title="Label format" onclick="toggleDiv('divLabelformat', 'img7',350)"
        style="cursor: pointer; background-color: #5a9e8d; color: White;">
        <table summary="This table holds the Label format image">
            <tr>
                <td scope="col" title="Click Here to Expand/Collapse The Label Format" style="width: 190px" >
                   <label for="" title=" Chart label Format" class="leftLabel"><B>Chart label Format</B></label>
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
                        <label class="leftLabel" title=" X-Axis Chart Format" for="ctl00_LeftPlaceHolder_ddlXlabelfont"> X-Axis Chart Format</label>
                     </td>
            </tr>
            <tr>
                <td scope="col" title="X-Axis Label Font Name">
                    <label class="leftLabel" for="ctl00_LeftPlaceHolder_ddlXlabelfont">FontName</label>
                </td>
                <td scope="col" title=" Select X-Axis Label Font Name" style="width: 112px">
                    <asp:DropDownList ID="ddlXlabelfont" runat="server" CssClass="leftdropdownlist" >
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td scope="col" title="X-Axis Label Font Size">
                    <label class="leftLabel" for="ctl00_LeftPlaceHolder_ddlXfontsizelist">FontSize</label>
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
                    <%--<div id="div1llll" title="Animation Format" background-color: #339999; color: White;">
        <table>
            <tr>
                <td scope="col" style="width: 190px" title="Click Here for Expand/Collapse GridLine Format">
                    <asp:LinkButton ID="lnkopengp" Text="Open Graph" PostBackUrl="~/GraphicalPresentation/OpenGraph.aspx"
                </td>
                <td align="right" scope="col" title="Click Here for Expand/Collapse GridLine Format">
                </td>
            </tr>
            
        </table>
    </div>--%>
                    <a href="javascript: onclick=pickerPopup202('<%=xaxiscolor.ClientID%>','SampleCol');">
                  FontColour</a>
                </td>
                <td scope="col" title="Select X-Axis Label Font Color" style="width: 112px">
<label for="SampleCol" ></label>
                  <input type="text" id="SampleCol" style="width: 80px"/>
<%--This hidden Field is used for select multiple chart--%>
                  
                </td>
            </tr>
            <tr>
                <td scope="col" title="X-Axis Label Angle">
                    <label class="leftLabel" for="ctl00_LeftPlaceHolder_ddlXontanglelist">Angle</label>
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
                    <label class="leftLabel" for="ctl00_LeftPlaceHolder_chkXoffset">Offset</label>
                </td>
                <td scope="col" title="Click X-Axis Label Offset" style="height: 3px; width: 112px;" >
                    <asp:CheckBox ID="chkXoffset" runat="server" ForeColor="White" />
                </td>
            </tr>
            <tr>
                <td scope="col" title="X-Axis Label Enable">
                    <label class="leftLabel" for="ctl00_LeftPlaceHolder_chkXenable">Enable</label>
                </td>
                <td scope="col" title="Select X-Axis Label Enable" style="width: 112px" >
                    <asp:CheckBox ID="chkXenable" runat="server" Checked="True" ForeColor="White" />
               </td>
            </tr>
            </table>
        <table>
            <tr>
                <td colspan="2" scope="colgroup" title="Y-Axis Label Font Name" style="text-decoration: underline">
                    <label class="leftLabel" title=" Y-Axis Chart Format" for="ctl00_LeftPlaceHolder_ddlYfontname"> Y-Axis Chart Format</label>
                   </td>
            </tr>
            <tr>
                <td scope="col" title="Y-Axis Label Font Name">
                    <label class="leftLabel" for="ctl00_LeftPlaceHolder_ddlYfontname">FontName</label>
                </td>
                <td scope="col" title="Select Y-Axis Label Font Name" style="width: 144px">
                    <asp:DropDownList ID="ddlYfontname" runat="server" CssClass="leftdropdownlist"></asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td scope="col" title="Y-Axis Label Font Size">
                    <label class="leftLabel" for="ctl00_LeftPlaceHolder_ddlYlabelfontsize">FontSize</label>
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
                    <%-- <label for="ctl00_MainPlaceHolder_ddlDepartmant" class="label">
                                    Department</label>--%>
                   <a href="javascript: onclick=pickerPopup202('<%=yaxiscolor.ClientID%>','LColor');">
               FontColour</a>
                </td>
                <td scope="col" title="Select Y-Axis Label Font Color" style="width: 144px">
<label for="LColor" ></label>
                  <input type="text" id="LColor" style="width: 80px"/>
                    <%--<label for="ctl00_MainPlaceHolder_ddlClient" class="label">
                                Client</label>--%>
                   
                </td>
            </tr>
            <tr>
                <td scope="col" title="Y-Axis Label Angle" >
                    <label class="leftLabel" for="ctl00_LeftPlaceHolder_ddlYangle">Angle</label>
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
                    <label class="leftLabel" for="ctl00_LeftPlaceHolder_chkYoffset">Offset</label>
                </td>
                <td scope="col" title="Select Y-Axis Label Offset" style="width: 144px">
                     <asp:CheckBox ID="chkYoffset" runat="server" ForeColor="White" />
                </td>
            </tr>
            <tr>
                <td scope="col" title="Y-Axis Label Enable">
                    <label class="leftLabel" for="ctl00_LeftPlaceHolder_chkYenable">Enable</label>
                    </td>
                <td scope="col" title="Select Y-Axis Label Enable" style="width: 144px">
                     <asp:CheckBox ID="chkYenable" runat="server" Checked="True" ForeColor="White" />
                </td>
            </tr>
        </table>
    </div>
    
    <div id="divShow" title="Chart Axis and Title Format" onclick="toggleDiv('divFormat', 'img8',340)" style="cursor: pointer;
        background-color: #5a9e8d; color: White;">
        <table summary="This table holds Chart Title Image">
            <tr>
                <td scope="col" title="Chart Title" style="width: 190px">
                    <label for="" title="Title and Axis Format" class="leftLabel"><B>Title and Axis Format</B></label>
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
                    <label class="leftLabel" for="ctl00_LeftPlaceHolder_txtCharttitle">Chart Title:</label>
                </td>
                <td scope="col" title="Enter ChartTitle" style="width: 93px">
                    <asp:TextBox ID="txtCharttitle" runat="server" Height="16px" CssClass="leftdropdownlist" Width="96px">Chart Title</asp:TextBox>
                </td>
            </tr>
            <tr>
                <td scope="col" title="X-Axis Title" style="width: 89px">
                   <label class="leftLabel" for="ctl00_LeftPlaceHolder_txtTitleext">X-AxisTitle:</label>
                </td>
                <td scope="col" title="Enter Axis Title" style="width: 93px">
                   <asp:TextBox ID="txtTitleext" runat="server" Height="16px" CssClass="leftdropdownlist" Width="96px">XAxis Title</asp:TextBox>
                </td>
            </tr>
            <tr>
                <td scope="col" title="Y-Axis Title" style="width: 89px">
                    <label class="leftLabel" for="ctl00_LeftPlaceHolder_txtYTitle">YAxis Title:</label>
                </td>
                <td scope="col" title=" Enter YAxis Title" style="width: 93px">
                    <asp:TextBox ID="txtYTitle" runat="server" Height="16px" CssClass="leftdropdownlist" Width="96px">YAxis Title</asp:TextBox>
                </td>
            </tr>
            <tr>
                <td scope="col" title="Chart Title FontSize" style="width: 89px">
                    <label class="leftLabel" for="ctl00_LeftPlaceHolder_ddlTitlesize">Font Size:</label>
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
                  <label class="leftLabel" for="ctl00_LeftPlaceHolder_ddlFont1">Title Font:</label>
               </td>
               <td scope="col" title="Select Chart Title Font" style="width: 93px" >
                  <asp:DropDownList ID="ddlFont1" runat="server" CssClass="leftdropdownlist">
                  </asp:DropDownList>
               </td>
            </tr>
            <tr>
                <td scope="col" title="Chart Title Font Color" style="width: 89px">
                   <a href="javascript: onclick=pickerPopup202('<%=titlefontcolor.ClientID%>','SamColor');">
                FontColor</a>
                </td>
                <td scope="col" title="Select Chart Title Color" style="width: 93px">
<label for="SamColor" ></label>
                  <input type="text" id="SamColor" style="width: 80px"/>
                </td>
            </tr>
            <tr>
                <td scope="col" title="Chart Title  Border Color" style="width: 89px">
                  <a href="javascript: onclick=pickerPopup202('<%=titlebordercolor.ClientID%>','LineColor');">
                BorderColor</a>
                </td>
                <td scope="col" title="Select Chart Border Color" style="width: 93px">
<label for="LineColor" ></label>
                  <input type="text" id="LineColor" style="width: 80px"/>
                </td>
            </tr>
            <tr>
                <td scope="col" title="Chart Back Color" style="width: 89px">
                    <a href="javascript: onclick=pickerPopup202('<%=titlebkcolor.ClientID%>','SampleColor');">
         Back Color </a>
                 </td>
                <td scope="col" title="Select Chart Back Color" style="width: 93px">
<label for="SampleColor" ></label>
                  <input type="text" id="SampleColor" style="width: 80px"/>
                  </td>
               </tr>
            <tr>
                <td scope="col" title="Chart Title Alignment" style="width: 89px">
                    <label class="leftLabel" for="ctl00_LeftPlaceHolder_Alignment">Alignment:</label>
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
                    <label class="leftLabel" for="ctl00_LeftPlaceHolder_chkItalic">Italic:</label>
                 </td>
                 <td scope="col" title="Select Italic Font" style="width: 93px">
                    <asp:CheckBox ID="chkItalic" runat="server" />
                 </td>
            </tr>
            <tr>
                <td scope="col" title="Bold Font" style="width: 89px">
                    <label class="leftLabel" for="ctl00_LeftPlaceHolder_chkBold">Bold:</label>
                </td>
                <td scope="col" title="Select Bold Font" style="width: 93px">
                   <asp:CheckBox ID="chkBold" runat="server" />
                </td>
            </tr>
            <tr>
                <td scope="col" title="UnderLine Font" style="width: 89px">
                    <label class="leftLabel" for="ctl00_LeftPlaceHolder_chkUline">UnderLine:</label>
                </td>
                <td scope="col" title="Select UnderLine Font" style="width: 93px">
                   <asp:CheckBox ID="chkUline" runat="server" Width="104px" />
                </td>
            </tr>
            <tr>
                <td scope="col" title="StrikeOut Font" style="width: 89px">
                    <label class="leftLabel" for="ctl00_LeftPlaceHolder_chkSout">StrikeOut:</label>
                </td>
                <td scope="col" title="Select StrikeOut Font" style="width: 93px">
                   <asp:CheckBox ID="chkSout" runat="server" />
                </td>
            </tr>
            
        </table>
    </div>
    
    <div id="divShowgridline" title="Major/Minor GridLine Format" onclick="toggleDiv('divGridline', 'imgGridline',310)" style="cursor: pointer;
       background-color: #76b1a3; color: White;">
        <table>
            <tr>
                <td scope="col" style="width: 190px" title="Expand/Collapse">
                    <label for="" title="GridLines Format" class="leftLabel"><b>GridLines Format</b></label>
                </td>
                <td align="right" scope="col" title="Expand/Collapse">
                    <img id="imgGridline" title="Expand/Collapse" alt="Expand/Collapse" src="../images/ArrowDown.gif" />
                </td>
            </tr>        </table>
    </div>
    <div id="divGridline" title="GridLine Format" style="overflow: hidden; display: none; color:White;">
      <table summary="This Table is used for select major/minor gridlines/trickmark.,Gridlins color,gridline width,gridline interval.">
          <tr>
              <td colspan="2" scope="col" title="MajorGridLine Format" style="text-decoration: underline">
                 <label class="leftLabel" title="MinorGridLine Format" for="ctl00_LeftPlaceHolder_ddlMajorgridline"> MinorGridLine Format</label> 
              </td>
          </tr>
            <tr>
                <td scope="col" title="MajorGridLine And Major Tickmark" style="width: 204px">
                    <label class="leftLabel" for="ctl00_LeftPlaceHolder_ddlMajorgridline">Major</label>
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
                    <label class="leftLabel" for="ctl00_LeftPlaceHolder_ddlLinetypes">Types:</label>
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
                    <%--<label for="ctl00_MainPlaceHolder_ddlLob" class="label">
                                LOB</label>--%>
                    <a href="javascript: onclick=pickerPopup202('<%=Majorgridcolour.ClientID%>','SampleLine');">
             LineColor </a>
                </td>
                <td scope="col" title="Select MajorGridLine Color" style="width: 132px">
<label for="SampleLine" ></label>
                  <input type="text" id="SampleLine" style="width: 80px"/>
                    <%--<td id="lblGraphname1" runat="server" scope="col"  title="Report Graph" nowrap="nowrap" >
                                    <label   for="ctl00_MainPlaceHolder_ddlGraphname" class="label" >Graph Name</label>
                                </td>
                                <td scope="col"  title="Report Graph Name " >
                                    <asp:DropDownList ID="ddlGraphname" runat="server" CssClass="dropdownlist"  AutoPostBack="true">
                                    </asp:DropDownList>
                                </td>--%>
                   
                 </td>
            </tr>
            <tr>
                <td scope="col" title="MajorGridLine Width" style="width: 204px">
                    <label class="leftLabel" for="ctl00_LeftPlaceHolder_ddlMajorline">LineWidth</label>
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
                    <label class="leftLabel" for="ctl00_LeftPlaceHolder_ddlMajorInterval">Interval</label>
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
              <td colspan="2" scope="col" title="MinorGridLine Format" style="text-decoration: underline">
                <label class="leftLabel" title="MinorGridLine Format" for="ctl00_LeftPlaceHolder_ddlMinorType"> MinorGridLine Format</label> 
              </td>
          </tr>
            <tr>
                <td scope="col" title="MinorGridLine Or MinorTickmark" style="width: 204px; height: 24px">
                    <label class="leftLabel" for="ctl00_LeftPlaceHolder_ddlMinorType">Minor:</label>
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
                    <label class="leftLabel" for="ctl00_LeftPlaceHolder_ddlMinoeLinetypes">Types:</label>
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
                    <%--<tr>
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
                                    </tr>--%>
                    <a href="javascript: onclick=pickerPopup202('<%=ddlMinorColor1.ClientID%>','SampleLineColor');">
                  LineColor</a>
                </td>
                <td scope="col" title=" Select Minor GridLines Color" style="width: 132px">
<label for="SampleLineColor" ></label>
                  <input type="text" id="SampleLineColor" style="width: 80px"/>
                    <%--<tr>
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
                                    </tr>--%>
                                    </td>
            </tr>
            <tr>
                <td scope="col" title="Minor GridLines Width" style="width: 204px">
                    <label class="leftLabel" for="ctl00_LeftPlaceHolder_ddlMinorWidth">Line Width:</label>
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
                    <label class="leftLabel" for="ctl00_LeftPlaceHolder_ddlMinorInterval">Interval:</label>
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
    <div id="divChartArea" title="ChartArea Format" onclick="toggleDiv('divAreaposition', 'imgChartarea',350)" style="cursor: pointer;
       background-color: #76b1a3; color: White;">
        <table>
            <tr>
                <td scope="col" style="width: 190px" title="Expand/Collapse">
                    <label for="" title="ChartArea Format" class="leftLabel"><b>ChartArea Format</b></label>
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
								<td colspan="2"  scope="colgroup">
								    <label for="" title="Chart Area Position" class="leftLabel"><b>Chart Area Position</b></label>
								</td>
								<td></td>
							</tr>
							<tr>
								<td style="width: 110px"  scope="col">
								    <label for="ctl00_LeftPlaceHolder_txtX1" title="X" class="leftLabel"><b>X</b></label>
								</td>
								<td><asp:textbox id="txtX1" runat="server" Width="100px">10</asp:textbox></td>
							</tr>
							<tr>
								<td style="width: 110px"  scope="col">
								    <label for="ctl00_LeftPlaceHolder_txtY1" title="Y" class="leftLabel"><b>Y</b></label>
								 </td>
								<td><asp:textbox id="txtY1" runat="server" Width="100px">10</asp:textbox></td>
							</tr>
							<tr>
								<td style="width: 110px" scope="col" >
								    <label for="ctl00_LeftPlaceHolder_txtWidth1" title="Width" class="leftLabel"><b>Width</b></label>
								</td>
								<td><asp:textbox id="txtWidth1" runat="server" Width="100px">80</asp:textbox></td>
							</tr>
							<tr>
								<td style="width: 110px" >
								    <label for="ctl00_LeftPlaceHolder_txtHeight1" title="Height" class="leftLabel"><b>Height</b></label>
								</td>
								<td><asp:textbox id="txtHeight1" runat="server" Width="100px">80</asp:textbox></td>
							</tr>
							<tr>
								<td colspan="2" >
								    <label for="" title="Plotting Area Position" class="leftLabel"><b>Plotting Area Position</b></label>
								 </td>
								<td>
								</td>
							</tr>
							<tr>
								<td style="width: 110px" >
								    <label for="ctl00_LeftPlaceHolder_txtX2" title="X Axis" class="leftLabel"><b>X Axis</b></label>
								 </td>
								<td><asp:textbox id="txtX2" runat="server" Width="100px">10</asp:textbox></td>
							</tr>
							<tr>
								<td style="width: 110px" >
								    <label for="ctl00_LeftPlaceHolder_txtY2" title="Y Axis" class="leftLabel"><b>Y Axis</b></label>
								</td>
								<td><asp:textbox id="txtY2" runat="server" Width="100px">10</asp:textbox></td>
							</tr>
							<tr>
								<td style="width: 110px" >
								    <label for="ctl00_LeftPlaceHolder_txtWidth2" title="Width" class="leftLabel"><b>Width</b></label>
								</td>
								<td><asp:textbox id="txtWidth2" runat="server" Width="100px">80</asp:textbox></td>
							</tr>
							<tr>
								<td style="width: 110px" >
								    <label for="ctl00_LeftPlaceHolder_txtHeight2" title="Height" class="leftLabel"><b>Height</b></label>
								</td>
								<td><asp:textbox id="txtHeight2" runat="server" Width="100px">80</asp:textbox></td>
							</tr>
							<tr>
								<td colspan="2" >
								    <label for="" title="Chart Zooming:" class="leftLabel"><b>Chart Zooming</b></label>
								</td>
								<td></td>
							</tr>
							<tr>
								<td style="width: 110px" >
								    <label for="ctl00_LeftPlaceHolder_ddlChartwidth" title="Position" class="leftLabel"><b>Position</b></label>
								</td>
								<td>
								<asp:DropDownList ID="ddlChartwidth" runat="server" Height="22px" CssClass="leftdropdownlist">
                                <asp:ListItem Value="100%">100%</asp:ListItem>
                                <asp:ListItem Value="200%">200%</asp:ListItem>
                                 <asp:ListItem Value="300%">300%</asp:ListItem>
                                <asp:ListItem Value="400%">400%</asp:ListItem>
                                <asp:ListItem Value="500%">500%</asp:ListItem>
                                <asp:ListItem Value="500%">1000%</asp:ListItem>
                                 </asp:DropDownList>
								</td>
							</tr>
							 <tr>
                                 <td scope="col" title="Backcolor" style="width: 110px; height: 26px;">
                                    <a  href="javascript: onclick=pickerPopup202('<%=chartareabkcolor.ClientID%>','Areabackcolor');">
                                    BackColor</a>
                                 </td>
                                 <td scope="col" align="left" valign="bottom" title="Select ChartArea Backcolor" style=" height:26px; width: 200px;">
                                  <label for="Areabackcolor" ></label>
                                    <input type="text" id="Areabackcolor"  enableviewstate="true" style="width: 72px;"/>
                                </td>
                             </tr>
							</table>   
            
    </div>
      <div id="divAnimation1" title="Animation Format" onclick="toggleDiv('divAnimation', 'img13',240)" style="cursor: pointer;
        background-color: #97c4ba; color: White;">
        <table>
            <tr>
                <td scope="col" style="width: 190px" title="Click Here for Expand/Collapse GridLine Format">
                    <label for="img13" title="Animation Format" class="leftLabel"><b>Animation Format</b></label>
                </td>
                <td align="right" scope="col" title="Click Here for Expand/Collapse GridLine Format">
                    <img id="img13" title="Click Here for Expand/Collapse GridLine Format" alt="Expand/Collapse" src="../images/ArrowDown.gif" />
                </td>
            </tr>
            
        </table>
    </div>
    <div id="divAnimation" title="Animation Format" style="overflow: hidden; display: none; color:White;">
      <table summary="" style="width: 200px">
           <tr>
				<td ><label for="ctl00_LeftPlaceHolder_CheckBoxLegend" title="Legend" class="leftLabel"><b>Legend</b></label></td>
				<td>
					<asp:checkbox id="CheckBoxLegend" runat="server" ForeColor="white" Checked="True" Text="Move Legend Items One-By-One" AutoPostBack="false" ></asp:checkbox>
				</td>
			</tr>
		   <tr>
								<td> <label for="ctl00_LeftPlaceHolder_CheckBoxSeries" title="seriese" class="leftLabel"><b>seriese</b></label></td>
								<td>
									<asp:checkbox id="CheckBoxSeries" runat="server"  Checked="True" Text="Move Series One-By-One" AutoPostBack="false" ></asp:checkbox>
								</td>
							</tr>
		   <tr>
								<td ><label for="ctl00_LeftPlaceHolder_CheckBoxPoints" title="Points" class="leftLabel"><b>Points</b></label></td>
								<td>
									<asp:checkbox id="CheckBoxPoints" runat="server" ForeColor="white" Checked="True" Text="Move Points One-By-One" AutoPostBack="false" ></asp:checkbox>
								</td>
							</tr>
		   <tr>
				<td >
					<label for="ctl00_LeftPlaceHolder_ddlTheme" title="Theme" class="leftLabel"><b>Theme</b></label></td>
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
    <%--<tr>
                                        <td scope="col" title="Pie Chart Line Size">
                                        <label for="ddlPielinearrowsize" class="label">Pie Line Size</label>
                                            
                                        </td>
                                        <td scope="col" title="Select Pie Chart Line Size">
                                            <asp:DropDownList ID="ddlPielinearrowsize" runat="server" AutoPostBack="false" CssClass="dropdownlist">
                                                <asp:ListItem>1</asp:ListItem>
                                                <asp:ListItem>2</asp:ListItem>
                                            </asp:DropDownList></td>
                                    </tr>--%>
    </div>
    
   


<div id="ll" style="display:none;">
<label for="ctl00_LeftPlaceHolder_bkcolor" ></label>
<input type="text" runat="server" id="bkcolor" style="width: 24px; background-color:#59afbb; border-bottom-color:#59afbb; border-color:#59afbb;" />
<label for="ctl00_LeftPlaceHolder_chartareabkcolor" ></label>
<input type="text" runat="server" id="chartareabkcolor" style="width: 24px; background-color:#59afbb; border-bottom-color:#59afbb; border-color:#59afbb;" />
<label for="ctl00_LeftPlaceHolder_brcolor" ></label>
<input type="text" runat="server" id="brcolor" style="width: 24px; background-color:#59afbb; border-bottom-color:#59afbb; border-color:#59afbb;" />
<label for="ctl00_LeftPlaceHolder_legendbkcolor" ></label>
<input type="text" runat="server" id="legendbkcolor" style="width: 24px; background-color:#59afbb; border-bottom-color:#59afbb; border-color:#59afbb;" />
<label for="ctl00_LeftPlaceHolder_legendbrcolor" ></label>
<input type="text" runat="server" id="legendbrcolor" style="width: 24px; background-color:#59afbb; border-bottom-color:#59afbb; border-color:#59afbb;" />
<label for="ctl00_LeftPlaceHolder_xaxiscolor" ></label>
<input type="text" runat="server" id="xaxiscolor" style="width: 24px; background-color:#59afbb; border-bottom-color:#59afbb; border-color:#59afbb;" />
<label for="ctl00_LeftPlaceHolder_yaxiscolor" ></label>
<input type="text" runat="server" id="yaxiscolor" style="width: 24px; background-color:#59afbb; border-bottom-color:#59afbb; border-color:#59afbb;" />
<label for="ctl00_LeftPlaceHolder_titlefontcolor" ></label>
<input type="text" runat="server" id="titlefontcolor" style="width: 24px; background-color:#59afbb; border-bottom-color:#59afbb; border-color:#59afbb;" />
<label for="ctl00_LeftPlaceHolder_titlebordercolor" ></label>
<input type="text" runat="server" id="titlebordercolor" style="width: 24px; background-color:#59afbb; border-bottom-color:#59afbb; border-color:#59afbb;" />
<label for="ctl00_LeftPlaceHolder_titlebkcolor" ></label>
<input type="text" runat="server" id="titlebkcolor" style="width: 24px; background-color:#59afbb; border-bottom-color:#59afbb; border-color:#59afbb;" />
<label for="ctl00_LeftPlaceHolder_ddlMinorColor1" ></label>
<input type="text" runat="server" id="ddlMinorColor1" style="width: 24px; background-color:#59afbb; border-bottom-color:#59afbb; border-color:#59afbb;" />
<label for="ctl00_LeftPlaceHolder_Majorgridcolour" ></label>
<input type="text" runat="server" id="Majorgridcolour" style="width: 24px; background-color:#59afbb; border-bottom-color:#59afbb; border-color:#59afbb;" />
<label for="ctl00_LeftPlaceHolder_hidYangle" ></label>
<input type="text" runat="server" id="hidYangle" style="width: 24px; background-color:#59afbb; border-bottom-color:#59afbb; border-color:#59afbb;" />
<label for="ctl00_LeftPlaceHolder_hidXangle" ></label>
<input type="text" runat="server" id="hidXangle" style="width: 24px; background-color:#59afbb; border-bottom-color:#59afbb; border-color:#59afbb;" />
<label for="ctl00_LeftPlaceHolder_hidGraident" ></label>
<input type="text" runat="server" id="hidGraident" style="width: 24px; background-color:#59afbb; border-bottom-color:#59afbb; border-color:#59afbb;" />
<label for="ctl00_LeftPlaceHolder_hidhatch" ></label>
<input type="text" runat="server" id="hidhatch" style="width: 24px; background-color:#59afbb; border-bottom-color:#59afbb; border-color:#59afbb;" />
<label for="ctl00_LeftPlaceHolder_hidbordersize" ></label>
<input type="text" runat="server" id="hidbordersize" style="width: 24px; background-color:#59afbb; border-bottom-color:#59afbb; border-color:#59afbb;" />
<label for="ctl00_LeftPlaceHolder_hidborderstyle" ></label>
<input type="text" runat="server" id="hidborderstyle" style="width: 24px; background-color:#59afbb; border-bottom-color:#59afbb; border-color:#59afbb;" />
<label for="ctl00_LeftPlaceHolder_hidXlabelfont" ></label>
<input type="text" runat="server" id="hidXlabelfont" style="width: 24px; background-color:#59afbb; border-bottom-color:#59afbb; border-color:#59afbb;" />
<label for="ctl00_LeftPlaceHolder_hidYlabelfont" ></label>
<input type="text" runat="server" id="hidYlabelfont" style="width: 24px; background-color:#59afbb; border-bottom-color:#59afbb; border-color:#59afbb;" />
<label for="ctl00_LeftPlaceHolder_hidxfontsize" ></label>
<input type="text" runat="server" id="hidxfontsize" style="width: 24px; background-color:#59afbb; border-bottom-color:#59afbb; border-color:#59afbb;" />
<label for="ctl00_LeftPlaceHolder_hidyfontsiae" ></label>
<input type="text" runat="server" id="hidyfontsiae" style="width: 24px; background-color:#59afbb; border-bottom-color:#59afbb; border-color:#59afbb;" />
<label for="ctl00_LeftPlaceHolder_hidcharttitle" ></label>
<input type="text" runat="server" id="hidcharttitle" style="width: 24px; background-color:#59afbb; border-bottom-color:#59afbb; border-color:#59afbb;" />
<label for="ctl00_LeftPlaceHolder_hidXtitle" ></label>
<input type="text" runat="server" id="hidXtitle" style="width: 24px; background-color:#59afbb; border-bottom-color:#59afbb; border-color:#59afbb;" />
<label for="ctl00_LeftPlaceHolder_hidYtitle" ></label>
<input type="text" runat="server" id="hidYtitle" style="width: 24px; background-color:#59afbb; border-bottom-color:#59afbb; border-color:#59afbb;" />
<label for="ctl00_LeftPlaceHolder_hidtitlesize" ></label>
<input type="text" runat="server" id="hidtitlesize" style="width: 24px; background-color:#59afbb; border-bottom-color:#59afbb; border-color:#59afbb;" />
<label for="ctl00_LeftPlaceHolder_hidtitlefont" ></label>
<input type="text" runat="server" id="hidtitlefont" style="width: 24px; background-color:#59afbb; border-bottom-color:#59afbb; border-color:#59afbb;" />
<label for="ctl00_LeftPlaceHolder_hidAligment" ></label>
<input type="text" runat="server" id="hidAligment" style="width: 24px; background-color:#59afbb; border-bottom-color:#59afbb; border-color:#59afbb;" />
<label for="ctl00_LeftPlaceHolder_hidmjrgridline" ></label>
<input type="text" runat="server" id="hidmjrgridline" style="width: 24px; background-color:#59afbb; border-bottom-color:#59afbb; border-color:#59afbb;" />
<label for="ctl00_LeftPlaceHolder_hidmjrlinetype" ></label>
<input type="text" runat="server" id="hidmjrlinetype" style="width: 24px; background-color:#59afbb; border-bottom-color:#59afbb; border-color:#59afbb;" />
<label for="ctl00_LeftPlaceHolder_hidmjrline" ></label>
<input type="text" runat="server" id="hidmjrline" style="width: 24px; background-color:#59afbb; border-bottom-color:#59afbb; border-color:#59afbb;" />
<label for="ctl00_LeftPlaceHolder_hidmjrinterval" ></label>
<input type="text" runat="server" id="hidmjrinterval" style="width: 24px; background-color:#59afbb; border-bottom-color:#59afbb; border-color:#59afbb;" />
<label for="ctl00_LeftPlaceHolder_hidmnrgirdline" ></label>
<input type="text" runat="server" id="hidmnrgirdline" style="width: 24px; background-color:#59afbb; border-bottom-color:#59afbb; border-color:#59afbb;" />
<label for="ctl00_LeftPlaceHolder_hidmnrlinetype" ></label>
<input type="text" runat="server" id="hidmnrlinetype" style="width: 24px; background-color:#59afbb; border-bottom-color:#59afbb; border-color:#59afbb;" />
<label for="ctl00_LeftPlaceHolder_hidmnrline" ></label>
<input type="text" runat="server" id="hidmnrline" style="width: 24px; background-color:#59afbb; border-bottom-color:#59afbb; border-color:#59afbb;" />
<label for="ctl00_LeftPlaceHolder_hidmnrinterval" ></label>
<input type="text" runat="server" id="hidmnrinterval" style="width: 24px; background-color:#59afbb; border-bottom-color:#59afbb; border-color:#59afbb;" />
<label for="ctl00_LeftPlaceHolder_Text8" ></label>
<input type="text" runat="server" id="Text8" style="width: 24px; background-color:#59afbb; border-bottom-color:#59afbb; border-color:#59afbb;" />

</div>




<div id="mm" runat="server" visible="false">
       
    </div>
     <%--<tr>
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
                                    </tr>--%>
    <asp:HiddenField ID="hidChart" runat="server" />
</asp:Content>
<asp:Content ID="mainPane" runat="server" ContentPlaceHolderID="ContentPlaceHolder1" >
      <link rel="stylesheet" type="text/css" href="../App_Themes/Themes/StyleSheet.css" />
   <div id="divcaption" style="padding:20px;" >
        <table summary="This table hold the span , Reportname and columns "  width="100%">
            <caption><b>SELECT REPORT</b></caption>
            <tr>
                <td style="width: 725px" >
                    <div id="Selectreport" runat="server" visible="false">
                        <table>
                        <tr>
                            <td scope="col" title="Department" style="width: 100px"  >
                                <%--<tr>
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
                                    </tr>--%>
                            </td>
                            <td scope="col" title=" Select Department"  colspan="" >
                                <asp:DropDownList ID="ddlDepartmant" Visible="false"  runat="server" AutoPostBack="True" CssClass="dropdownlist">
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:Label id="lblChartimage" runat="server" ForeColor="#006666" Font-Bold="true" Font-Underline="true"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td scope="col" title="Client" style="width: 100px"   >
                                <%-- <tr>
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
                                    </tr>--%>
                            </td>
                            <td scope="col" title="Select Client"  >
                                <asp:DropDownList Visible="false" ID="ddlClient" runat="server" AutoPostBack="True" CssClass="dropdownlist">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td scope="col" title="LOB" style="width: 100px"   >
                                <%--<tr>
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
                                    </tr>--%>
                            </td>
                            <td scope="col" title="Select Lob"   >
                                <asp:DropDownList Visible="false" ID="ddlLob" runat="server" AutoPostBack="True" CssClass="dropdownlist">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                        <td>
                        <table id="spandisplay" runat="server">
                        <tr>
                        <td>
                            <asp:Label ID="select_level1" runat="server" Text="Select Level 1" CssClass="label"></asp:Label></td>
                            <td> 
                                <asp:DropDownList ID="Departmentname" CssClass="dropdownlist" runat="server" 
                                    AutoPostBack="True">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                        <td>
                            <asp:Label ID="select_level2" runat="server" Text="Select Level 2"  CssClass="label"></asp:Label></td>
                            <td> 
                                <asp:DropDownList ID="ClientName" CssClass="dropdownlist" runat="server" 
                                    AutoPostBack="True">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                        <td>
                            <asp:Label ID="select_level3" runat="server" Text="Select Level 3"  CssClass="label"></asp:Label></td>
                            <td> 
                                <asp:DropDownList ID="ddlLobName" CssClass="dropdownlist" runat="server" 
                                    AutoPostBack="True">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        </table>
                        <td>
                            <asp:Button ID="getreport" runat="server" Text="Get Report" Visible="false" 
                                CssClass="button" /></td>
                        </td>
                        </tr>
                        <tr>
                            <td scope="col"  title="Report Name" nowrap="nowrap" style="width: 100px">
                                <label for="ctl00_MainPlaceHolder_txtCurrentReport" class="label">Report Name</label></td>
                            <td scope="col" colspan="7" nowrap="nowrap" title="Current Report Name" style="width: 545px"   >
                           
                                <asp:TextBox ID="txtCurrentReport" runat="server" ReadOnly="true" Width="150px" Visible="false"></asp:TextBox>
                                 <label for="ctl00_MainPlaceHolder_ddlReport" ></label>
                                <asp:DropDownList ID="ddlReport" runat="server" CssClass="dropdownlist" AutoPostBack="True"></asp:DropDownList>
                                &nbsp;<asp:DropDownList ID="ddlReport_single" runat="server" 
                                    CssClass="dropdownlist" AutoPostBack="True"></asp:DropDownList>
                                &nbsp;&nbsp; &nbsp; &nbsp;<asp:Label ID ="lblgraphname" runat="server" Text="Graph Name"></asp:Label>
                                <label for="ctl00_MainPlaceHolder_ddlgraphname" ></label>
                                <asp:DropDownList ID="ddlgraphname" runat="server" CssClass="dropdownlist"  AutoPostBack="true"></asp:DropDownList>
                                <asp:DropDownList ID="ddlgraphname_single" runat="server" 
                                    CssClass="dropdownlist"  AutoPostBack="true"></asp:DropDownList>
                          </td>
                            <%-- <tr>
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
							        </tr>--%>
                            </tr>
                        </table>
                        </div>
                        <div id ="analysistable">
                </div>
                <div id="date">
                        <table >
                         <tr>
                         <td valign="top"  scope="col" title="Start Date" style="width:98px"   >
                            <label for="ctl00_MainPlaceHolder_txtFromdate" class="label">StartDate</label>
                         </td>
				          <td  valign="top" align="left" title="Enter Start Date" >
				               <asp:TextBox ID="txtFromdate" runat="server" Width="150px"></asp:TextBox>
				           </td>
				          <td style="width: 30px"  >
				              <img id="imgStartDate" onclick="ShowCalendar();" src="../images/Calendar.gif"
                                      title="Click To Select Start date" alt="Click To Select Start date" />
				          </td>
                          <td  valign="top" title="End Date" style="width: 78px"  >    
				                <label title="End Date" for="ctl00_MainPlaceHolder_txtTodate" style="color: black">EndDate</label>				                        
				           </td>
				           <td valign="top" align="left" title="Enter End Date"  >
				                <asp:TextBox ID="txtTodate" runat="server" Width="150px"></asp:TextBox>
				           </td>
				           <td  >
                                  <img id="imgEnddate" alt="Enter End Date"  onclick="ShowCalendar1();" src="../images/Calendar.gif" title="Click To Select End Date" />
				           </td>
                             <td >
                                <asp:Label ID="lbldatemsg" runat="server" Text="*" Font-Bold="true" ForeColor="Red" Visible="false"></asp:Label> 
                             </td>
                          </tr>
                          </table>
                   </div>
                   <div id ="lst">
                    <table>
                        <tr>
                           <td valign="top"  scope="col" title="Start Date" style="width:97px"   >
                           
                           </td>
                            <td>
<label for="ctl00_MainPlaceHolder_repcols" ></label>
                                 <asp:ListBox ID="repcols" SelectionMode="Multiple" runat="server" CssClass="listBox" Width="136px"></asp:ListBox>
                            </td>
                            <td style="width: 36px">
                                <asp:Button ID="add" runat="server" Text=">>" CssClass="button" Width="40px" ToolTip="Click on button to add items" />    
                                <asp:Button ID="remove" runat="server" Text="<<" CssClass="button" Width="40px"  ToolTip="Click on button to remove items" />
                            </td>
                            <td>
 <label for="ctl00_MainPlaceHolder_selectedcols" ></label>
                               <asp:ListBox ID="selectedcols" SelectionMode="Multiple" runat="server" CssClass="listBox" Width="136px"></asp:ListBox>
                            </td>
                            <td>
                            </td>
                        </tr>
                    </table>
                   </div>
                   <div id="btn">
            <table >
              <tr>
                  <td colspan="12" scope="col" title="" align="center" style="width: 652px; height: 22px;">
                      &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                      <label for="ctl00_MainPlaceHolder_rbnRow" ></label>
                      <asp:RadioButton ID="rbnRow" runat="server" GroupName="check" Text="Row Series" Width="96px" ToolTip="Select chart  Row series"  AutoPostBack="true" ForeColor="Black" />
                     <label for="ctl00_MainPlaceHolder_rbnColumn" ></label>
                      <asp:RadioButton ID="rbnColumn" runat="server" GroupName="check" Text="Column Series" Width="120px" ToolTip="Select chart  Column series" AutoPostBack="True" ForeColor="Black" />
                    
                      <label for="ctl00_MainPlaceHolder_chkanimated" ></label>
                    <asp:CheckBox ID="chkanimated" runat="server" Text="Animated Graph" AutoPostBack="true" Width="136px" ForeColor="Black" />
                            <label for="ctl00_MainPlaceHolder_chkSunnarized" ></label>
                    <asp:CheckBox ID="chkSunnarized" runat="server" Text="Summarized Graph" AutoPostBack="true" ForeColor="Black" />
                 </td>
        </tr>
        <tr>
            <td align="center" colspan="12" scope="col" style="height: 8px; width: 652px;">
                &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; 
            <asp:Button ID="btnGraph" runat="server" Text="Plot Graph" CssClass="button" 
                    ToolTip="Click To Show Graph Multi User" Width="96px"   /> 
            <asp:Button ID="btnGraph_singleuser" runat="server" Text="Plot Graph" 
                    CssClass="button" ToolTip="Click To Show Graph single User" 
                    Width="96px"   />&nbsp;
            <asp:Button ID="btnUpdate" runat="server" Text="Update"  CssClass="button" ToolTip="Click To Update Graph"   Width="94px"   />&nbsp;
            <asp:Button ID="btnDelete" runat="server" Text="Delete"  CssClass="button" ToolTip="Click To Delete Graph"  Width="98px"   />&nbsp;
            <asp:Button ID="btnreset" runat="server" CssClass="button"  Text="Reset" ToolTip="Reset window" />
            </td>
        </tr>
      </table>
   </div>
                </td>
                <td align="right" style="width:400px; height: 185px" valign="top">
                    &nbsp;</td>
            </tr>
            </table>
            
   </div>
   
    
    <table summary="This table hold the Graphcontrol and multiple Graph based Dropdown list controls." title="Graph On Selected Columns">
        <tr>
            <td scope="col" title="Graph On Selected Columns" style="width: 809px">
           
                <table summary="This table hold the Graphcontrol and multiple Graph based Dropdown list controls." title="Graph On Selected Columns">
                    <tr>
                     
                        <td scope="col" title="Graph On Selected Columns" style="height: 908px; width: 809px;" valign="top" align="center" colspan="2">
                        
                            <DCWC:Chart ID="Chart1"  runat="server" BackGradientEndColor="White" BackGradientType="DiagonalLeft" alt="Default Chart"
                                BorderLineColor="26, 59, 105" BorderLineStyle="Solid" Height="600px" Width="600px" Palette="Dundas" Visible="false" MapEnabled="False"  >
                           <Legends>
<dcwc:Legend BackGradientEndColor="245, 224, 224" BackColor="100, 255, 255, 255" LegendStyle="Column" BorderColor="100, 0, 0, 0" Name="Default"></dcwc:Legend>
</Legends>
                                <Titles>
<dcwc:Title ShadowColor="32, 0, 0, 0" Font="Trebuchet MS, 14.25pt, style=Bold" Color="26, 59, 105" Name="Title1"></dcwc:Title>
</Titles>
                                <BorderSkin FrameBackColor="CornflowerBlue" FrameBackGradientEndColor="CornflowerBlue"
                                    FrameBorderColor="100, 0, 0, 0" FrameBorderWidth="2" PageColor="Control" SkinStyle="Emboss" />
                                <ChartAreas>
<dcwc:ChartArea BackGradientEndColor="White" BorderColor="64, 64, 64, 64" ShadowColor="Transparent" BackColor="White" Name="Chart Area 1">
<AxisX LineColor="64, 64, 64, 64" LabelsAutoFit="False">
<LabelStyle Font="Trebuchet MS, 8.25pt, style=Bold"></LabelStyle>

<MajorTickMark Enabled="False" Size="2"></MajorTickMark>

<MajorGrid LineColor="64, 64, 64, 64"></MajorGrid>
</AxisX>

<Area3DStyle XAngle="15" YAngle="10" RightAngleAxes="False" Clustered="True" Perspective="10" WallWidth="0"></Area3DStyle>

<AxisY LineColor="64, 64, 64, 64" LabelsAutoFit="False">
<LabelStyle Font="Trebuchet MS, 8.25pt, style=Bold"></LabelStyle>

<MajorGrid LineColor="64, 64, 64, 64"></MajorGrid>
</AxisY>

<AxisY2>
<MajorGrid Enabled="False"></MajorGrid>
</AxisY2>
</dcwc:ChartArea>
</ChartAreas>
                            </DCWC:Chart>
                            <dcwc:Chart ID="Chart2" runat="server" alt="Chart" BackColor="#F3DFC1" BackGradientType="TopBottom"
                                BorderLineColor="181, 64, 1" BorderLineStyle="Solid" BorderLineWidth="2" Height="296px"
                                ImageType="Png" ImageUrl="~/TempImages/ChartPic_#SEQ(300,3)" MapEnabled="False"
                                Palette="Dundas" Visible="False" Width="412px">
                                <Titles>
                                    <dcwc:Title Color="DodgerBlue" Font="Microsoft Sans Serif, 9.75pt" Text="Histogram">
                                    </dcwc:Title>
                                </Titles>
                                <dcwc:Legend AutoFitText="False" BackColor="Transparent" Enabled="False" Font="Trebuchet MS, 8.25pt, style=Bold"
                                    Name="Default">
                                </dcwc:Legend>
                                <Series>
                                    <dcwc:Series BorderColor="110, 26, 59, 105" ChartType="Point" Color="120, 252, 180, 65"
                                        Font="Trebuchet MS, 8.25pt" MarkerSize="8" Name="DataDistribution" XValueType="Double"
                                        YValueType="Double">
                                    </dcwc:Series>
                                    <dcwc:Series BorderColor="180, 26, 59, 105" ChartArea="HistogramArea" Color="224, 64, 10"
                                        Font="Trebuchet MS, 8.25pt" Name="Histogram" ShowLabelAsValue="True" XValueType="Double"
                                        YValueType="Double">
                                    </dcwc:Series>
                                </Series>
                                <ChartAreas>
                                    <dcwc:ChartArea AlignWithChartArea="HistogramArea" BackColor="OldLace" BackGradientEndColor="White"
                                        BackGradientType="TopBottom" BorderColor="64, 64, 64, 64" BorderStyle="Solid"
                                        Name="Default" ShadowColor="Transparent">
                                        <AxisY LineColor="64, 64, 64, 64" Maximum="2" Minimum="0" Reverse="True">
                                            <MajorGrid Enabled="False" LineColor="64, 64, 64, 64" />
                                            <MajorTickMark Enabled="False" />
                                            <LabelStyle Enabled="False" Font="Trebuchet MS, 8.25pt, style=Bold" />
                                        </AxisY>
                                        <AxisX Enabled="True" LabelsAutoFit="False" LineColor="64, 64, 64, 64" Title="" TitleFont="Trebuchet MS, 8pt">
                                            <MajorGrid LineColor="64, 64, 64, 64" />
                                            <MajorTickMark Enabled="False" LineColor="Transparent" Size="1.5" />
                                            <LabelStyle Enabled="False" Font="Trebuchet MS, 8.25pt, style=Bold" />
                                        </AxisX>
                                        <AxisY2 LabelsAutoFit="False">
                                            <LabelStyle Font="Trebuchet MS, 8.25pt, style=Bold" />
                                        </AxisY2>
                                        <Position Height="15" Width="96" X="3" Y="4" />
                                        <Area3DStyle Clustered="True" Perspective="10" RightAngleAxes="False" WallWidth="0"
                                            XAngle="15" YAngle="10" />
                                    </dcwc:ChartArea>
                                    <dcwc:ChartArea BackColor="OldLace" BackGradientEndColor="White" BackGradientType="TopBottom"
                                        BorderColor="64, 64, 64, 64" BorderStyle="Solid" Name="HistogramArea" ShadowColor="Transparent">
                                        <AxisY LineColor="64, 64, 64, 64">
                                            <MajorGrid LineColor="64, 64, 64, 64" />
                                            <LabelStyle Font="Trebuchet MS, 8.25pt, style=Bold" />
                                        </AxisY>
                                        <AxisX LabelsAutoFit="False" LineColor="64, 64, 64, 64" Title="" TitleFont="Trebuchet MS, 8pt">
                                            <MajorGrid LineColor="64, 64, 64, 64" />
                                            <LabelStyle Font="Trebuchet MS, 8.25pt, style=Bold" />
                                        </AxisX>
                                        <AxisY2 LabelsAutoFit="False">
                                            <LabelStyle Font="Trebuchet MS, 8.25pt, style=Bold" />
                                        </AxisY2>
                                        <Position Height="77" Width="93" X="3" Y="18" />
                                        <Area3DStyle Clustered="True" Perspective="10" RightAngleAxes="False" WallWidth="0"
                                            XAngle="15" YAngle="10" />
                                    </dcwc:ChartArea>
                                </ChartAreas>
                                <BorderSkin SkinStyle="Emboss" />
                            </dcwc:Chart>
                            &nbsp;
						    <dcwc:chart id="StockChart" runat="server" Visible="false" BackColor="#D3DFF0" Width="460px" Height="400px" ImageUrl="~/TempImages/ChartPic_#SEQ(300,3)" ImageType="Png" BorderLineStyle="Solid" Palette="Dundas" BackGradientEndColor="White" BackGradientType="TopBottom" BorderLineWidth="2" BorderLineColor="26, 59, 105" enableviewstate="True" viewstatecontent="All" MapEnabled="False">
							<legends>
<dcwc:Legend Name="Default" DockToChartArea="Price" DockInsideChartArea="False" AutoFitText="False" LegendStyle="Row" BackColor="Transparent" Font="Trebuchet MS, 8.25pt, style=Bold" Alignment="Far" Docking="Top">
<Position X="55" Y="5" Width="38.19123" Height="7.127659"></Position>
</dcwc:Legend>
</legends>
							<chartareas>
<dcwc:ChartArea BackColor="64, 165, 191, 228" BackGradientType="TopBottom" BackGradientEndColor="White" ShadowColor="Transparent" BorderColor="64, 64, 64, 64" BorderStyle="Solid" Name="Price">
<AxisY LabelsAutoFit="False" LineColor="64, 64, 64, 64" StartFromZero="False">
<MajorGrid LineColor="64, 64, 64, 64"></MajorGrid>

<LabelStyle Font="Trebuchet MS, 8.25pt, style=Bold"></LabelStyle>
</AxisY>

<AxisX LabelsAutoFit="False" LineColor="64, 64, 64, 64">
<MajorGrid LineColor="64, 64, 64, 64"></MajorGrid>

<LabelStyle Font="Trebuchet MS, 8.25pt, style=Bold" ShowEndLabels="False"></LabelStyle>
</AxisX>

<Position X="3" Width="88" Height="100"></Position>

<Area3DStyle RightAngleAxes="False" Clustered="True" Perspective="10" XAngle="15" YAngle="10" WallWidth="0"></Area3DStyle>
</dcwc:ChartArea>
</chartareas>
							<borderskin skinstyle="Emboss"></borderskin>
						</dcwc:chart>
                            <div id="divpie" runat="server" style="display: none;" title=" Select Pie chart properties like Label Style, Line Arrow Type, Line Type,Offset,Legend">
                                <table summary="This table hold the Pie chart properties like Label Style, Line Arrow Type, Line Type,Offset,Legend" title="Pie Chart Properties">
                                    <%--<div id="divMask" style="display: none; filter: alpha(opacity=30); left: 0px; cursor: hand; position: relative; top: 0px; background-color:Green; cursor: pointer;" onmouseout="javascript:pointOut();" onclick="javascript:pointClick();">
                        </div>--%><%--<style>.FadingTooltip { BORDER-RIGHT: darkgray 1px outset; BORDER-TOP: darkgray 1px outset; FONT-SIZE: 12pt; BORDER-LEFT: darkgray 1px outset; WIDTH: auto; COLOR: black; BORDER-BOTTOM: darkgray 1px outset; HEIGHT: auto; BACKGROUND-COLOR: lemonchiffon; MARGIN: 3px 3px 3px 3px; padding: 3px 3px 3px 3px; borderBottomWidth: 3px 3px 3px 3px }
			</style>--%><%-- <div class="FadingTooltip" id="FADINGTOOLTIP" style="Z-INDEX: 999; VISIBILITY: hidden; POSITION: absolute"></div>--%><%--<script language="javascript">
   
  // debugger;
            var FADINGTOOLTIP
			var wnd_height, wnd_width;
			var tooltip_height, tooltip_width;
			var tooltip_shown=false;
			var	transparency = 100;
			var timer_id = 1;
			var tooltiptext;
			
			// override events
			window.onload = WindowLoading;
			window.onresize = UpdateWindowSize;
			document.onmousemove = AdjustToolTipPosition;

			function DisplayTooltip(tooltip_text)
			{
				FADINGTOOLTIP.innerHTML = tooltip_text;
				tooltip_shown = (tooltip_text != "")? true : false;
				if(tooltip_text != "")
				{
					// Get tooltip window height
					tooltip_height=(FADINGTOOLTIP.style.pixelHeight)? FADINGTOOLTIP.style.pixelHeight : FADINGTOOLTIP.offsetHeight;
					transparency=0;
					ToolTipFading();
				} 
				else 
				{
					clearTimeout(timer_id);
					FADINGTOOLTIP.style.visibility="hidden";
				}
			}

			function AdjustToolTipPosition(e)
			{
				if(tooltip_shown)
				{
				    // Depending on IE/Firefox, find out what object to use to find mouse position
				    var ev;
				    if(e)
				        ev = e;
				    else
				        ev = event;

					FADINGTOOLTIP.style.visibility = "visible";
					offset_y = (ev.clientY + tooltip_height - document.body.scrollTop + 30 >= wnd_height) ? - 15 - tooltip_height: 20;
					FADINGTOOLTIP.style.left = Math.min(wnd_width - tooltip_width - 10 , Math.max(3, ev.clientX + 6)) + document.body.scrollLeft + 'px';
					FADINGTOOLTIP.style.top = ev.clientY + offset_y + document.body.scrollTop + 'px';
				}
			}

			function WindowLoading()
			{
				FADINGTOOLTIP=document.getElementById('FADINGTOOLTIP');
	
				// Get tooltip  window width				
				tooltip_width = (FADINGTOOLTIP.style.pixelWidth) ? FADINGTOOLTIP.style.pixelWidth : FADINGTOOLTIP.offsetWidth;
				
				// Get tooltip window height
				tooltip_height=(FADINGTOOLTIP.style.pixelHeight)? FADINGTOOLTIP.style.pixelHeight : FADINGTOOLTIP.offsetHeight;

				UpdateWindowSize();
			}
			
			function ToolTipFading()
			{
				if(transparency <= 100)
				{
					FADINGTOOLTIP.style.filter="alpha(opacity="+transparency+")";
					FADINGTOOLTIP.style.opacity=transparency/100;
					transparency += 5;
					timer_id = setTimeout('ToolTipFading()', 35);
				}
			}

			function UpdateWindowSize() 
			{
				wnd_height=document.body.clientHeight;
				wnd_width=document.body.clientWidth;
			}
			</script>--%><%--<img src="../images/Chart/RunChart.jpg"/>--%>
                                </table>
                            </div>
                            <div id="divdaughnt" runat="server" style="display: none;" title="Select Daughnt Chart Properties like LabelStyle,Hole size,Line type,label offset, Radius">
                                <table border="2" summary="This table hold the Daughnt Chart Properties like LabelStyle,Hole size,Line type,label offset, Radius" title="Daughnt Chart Properties">
                                    <%--This hidden field is used for Clendar control--%>
                                </table>
                            </div>
                            <div id="divArea" runat="server" style="display: none;" title="Select Types of area chart, Line type ,Axis margin.">
                                <table border="2" summary="This table hold the Types of area chart, Line type." title="Area Chart Properties">
                                                                       <%-- hidden report/constituent backcolor --%>
                                </table>
                            </div>
                            <div id="divLine" runat="server" style="display: none;" title="Line Chart Prpperties">
                                <table summary="This table hold the Line chart properties like Line Chart Type,Points labels, Points marker size" title="Line Chart Properties">
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
						        </table>
                            </div>
		                </td>
                    </tr>
                </table>
                <%-- hidden report/constituent backcolor --%>
            </td>
        </tr>
   </table>
      <%------------------ Change History -------------------------
1. Dated: 20-03-08
   Changes Made By: Ekta Goyal
   Change Summary:Change left panel for modify multiple chart at a time
                   boz div was not work properly in Graphdata page.
2. Dated: 06-04-08
   Changes Made By: Ekta Goyal
   Change Summary: Change left panel for selct multiple chart
                   boz GraphDesign page was not work properly. 
                   
   --%><%-- hidden report/constituent backcolor --%><%------------------ Change History -------------------------
1. Dated: 20-03-08
   Changes Made By: Ekta Goyal
   Change Summary:Change left panel for modify multiple chart at a time
                   boz div was not work properly in Graphdata page.
2. Dated: 06-04-08
   Changes Made By: Ekta Goyal
   Change Summary: Change left panel for selct multiple chart
                   boz GraphDesign page was not work properly. 
                   
   --%>  <%-- hidden report/constituent backcolor --%>
    <asp:HiddenField ID="copyrep" runat="server" />
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
    <asp:HiddenField ID="Calender" runat="server" />
    <%-- hidden report/constituent backcolor --%>              
    <asp:HiddenField ID="hid" runat="server" />
        <asp:HiddenField ID="HiddenField2" runat="server" />
       <asp:HiddenField ID="reotablename" runat="server" />
      <asp:HiddenField ID="hidfunname" runat="server" />
    <asp:HiddenField ID="totalcol" runat="server" />
    <asp:HiddenField ID="opentotalcolumn" runat="server" />
      <asp:HiddenField ID="Currentrepdept" runat="server" />
      <asp:HiddenField ID="Currentrepclient" runat="server" />
      <asp:HiddenField ID="Currentreplob" runat="server" />
    <asp:HiddenField ID="openselectedcolumn" runat="server" />
    <asp:HiddenField ID="seriesbtn" runat="server" />
    <asp:HiddenField ID="commangraphfprmat" runat="server" />
    <asp:HiddenField ID="commangraphfprmat1" runat="server" />
    <asp:HiddenField ID="commangraphfprmat2" runat="server" />
    <asp:HiddenField ID="legendgraphfprmat" runat="server" />
    <asp:HiddenField ID="specificformat" runat="server" />
    <asp:HiddenField ID="RepGraphtype" runat="server" />
    <input type="hidden" name="abc" id="abc" />
    <input type="hidden" name="abc1" id="abc1" />
    <input type="hidden" name="abc2" id="abc2" />
    <input type="hidden" name="hidcolumname1" id="hidcolumname" runat="server" />
    <input type="hidden" name="hidgroup1" id="hidgroup" runat="server" />
    <input type="hidden" name="hidhaving1" id="hidhaving" runat="server" />
    <input type="hidden" name="hidwheretxt1" id="hidwheretxt" runat="server" />
     <input type="hidden" name="hidtabname" id="hidtabname" runat="server" />
      <input type="hidden" name="hidcolname" id="hidcolname" runat="server" />
    <input type="hidden" name="hidorder1" id="hidorder" runat="server" />
    <input type="hidden" name="hidtablename1" id="hidtablename" runat="server" />
    <input type="hidden" name="formulaarray1" id="formulaarray" runat="server" />
        <input type="hidden" name="Normalarray1" id="Normalarray" runat="server" />
        <input type="hidden" name="hiddclient1" id="hiddclient" runat="server" />
       <input type="hidden" name="hidreportname1" id="hidreportname" runat="server" />
    <input type="hidden" name="prp1" id="prp" />
    <input type="hidden" name="Report1" id="Report" />
    <input type="hidden" name="department1" id="department" />
    <input type="hidden" name="client1" id="client" />
    <input type="hidden" name="lob1" id="lob" />
    <input type="hidden" name="Graph1" id="Graph" />
    <input type="hidden" name="ToDate1" id="ToDate" />
    <input type="hidden" name="FromDate1" id="FromDate" />
    <input type="hidden" name="ColumnName1" id="ColumnName" />
    <input type="hidden" name="ColumnSeries1" id="ColumnSeries" />
     <input type="hidden" name="CreatedOn1" id="CreatedOn" />
      <input type="hidden" name="SavedBy1" id="SavedBy" />
      <input type="hidden" name="totalcolumn1" id="totalcolumn" />
      <input type="hidden" name="ReportType" id="ReportType" />
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


