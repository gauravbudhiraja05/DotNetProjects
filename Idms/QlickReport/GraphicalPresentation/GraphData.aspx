<%--Project Name: IDMS Phase 2
    Module Name: graphical Presentation
    Page Name: GraphData
    Summary: Create different types of chart on the basis of  Advance Report Designer
    Created on: 10/03/08
    Created By: Ekta Goyal

--%>

<%@ Page AutoEventWireup="false" EnableEventValidation="false" ValidateRequest="false"  EnableViewState="true" CodeFile="GraphData.aspx.vb" Inherits="GraphData" Language="VB" MasterPageFile="~/MasterPage/MasterPage.master" Title="Graph Data" %>
<%@ Register TagPrefix="dcwc" Namespace="Dundas.Charting.WebControl" Assembly="DundasWebChart" %>
<asp:Content ID="leftPane" runat="server" ContentPlaceHolderID="LeftPlaceHolder">
     <script language="javascript" src="../js/202pop.js" type="text/javascript"></script><%--This hidden Field is used for select multiple chart--%>
     <script language="Javascript" src="../js/collapseableDIV.js" type="text/javascript"></script>  <%--<script language="javascript" type="text/javascript">
	        <!--

	        // active point area, imagemap->area element
	        var activeArea = null;
    		
	        function pointOver( element )
	        {
				var divMask = document.getElementById("divMask");
		        activeArea = element;
		        var data = activeArea.coords.split(",");
		        // data areas with shape 'rect' contains coordinates as 'x1,y1,x2,y2'.
		        var coords = { 
			        x : parseInt(data[0]), 
			        y : parseInt(data[1]), 
			        w : parseInt(data[2])-parseInt(data[0]), 
			        h : parseInt(data[3])-parseInt(data[1])
			        };

		        divMask.style.width = coords.w + "px";
		        divMask.style.height= coords.h + "px";
		        divMask.style.top	= (coords.y - 300) + "px"; // 300 is the chart height.
		        divMask.style.left	= coords.x + "px";
		        divMask.style.display = "block";
	        }

	        function pointClick()
	        {
		        if ( activeArea )
		        {
			        eval( activeArea.href);
		        }
	        }

	        function pointOut()
	        {
				var divMask = document.getElementById("divMask");
		        divMask.style.display = "none";
		        divMask.style.width = "0px";
		        divMask.style.height= "0px";
		        activeArea = null;
	        }
	       
    </script>--%>
     <script language="JavaScript" type="text/javascript" src="../js/picker.js"></script><%-- <label for="ctl00_MainPlaceHolder_ddlDepartmant" class="label">
                                    Select Level 1</label>--%>
     <script language="javascript" type="text/javascript">//This Script used for multiple functions

        function gridshow()//Open Grid Report
        {
        window.open("showgraph.aspx","ViewGraph",'height=500px');
        }
        function gridshowclose()//Close Grid Report
        {
        window.open("showgraph.aspx","ViewGraph",'height=500px');
    }
    function Savegraphs_singleuser()
        {
                getValues_singleuser();
	       	    window.open("Savegraph_singleuser.aspx","SaveGraph","width=400,height=220,scrollbars=yes,status=yes");
	       	    return false;
        }
    function Savegraphs()//Save Graph
	    { 
                getValues();
	       	    window.open("Savegraph.aspx","SaveGraph","width=400,height=220,scrollbars=yes,status=yes");
	       	    return false;
	    }
	    function getValues()//This Function used in Save Graph
	    {
	    
	       var Xoffchk="off";
	        if(document.getElementById("<%=chkShow3D.ClientID%>").checked==false)
	        {
	           var chk="off"; 
	        }
	        else 
	        {
	         chk="on";
	        }
	        if(document.getElementById("<%=chk_Reversed.ClientID%>").checked==false)
	        {
	           var chk_re="off"; 
	        }
	        else 
	        {
	         chk_re="on";
	        }
	        if(document.getElementById("<%=chkBold.ClientID%>").checked==false)
	        {
	           var chkbold="off"; 
	        }
	        else 
	        {
	         chkbold="on";
	        }
	        if(document.getElementById("<%=chkItalic.ClientID%>").checked==false)
	        {
	           var chkitalic="off"; 
	        }
	        else 
	        {
	         chkitalic="on";
	        }
	        if(document.getElementById("<%=chkUline.ClientID%>").checked==false)
	        {
	           var chkUline="off"; 
	        }
	        else 
	        {
	         chkUline="on";
	        }
	        if(document.getElementById("<%=chkSout.ClientID%>").checked==false)
	        {
	           var chkSout="off"; 
	        }
	        else 
	        {
	         chkSout="on";
	        }
	        if(document.getElementById("<%=chkXoffset.ClientID%>").checked==false)
	        {
	         Xoffchk="off"
	        }
	        else
	        {
	          Xoffchk="on"
	        }
	        if(document.getElementById("<%=yaxiscolor.ClientID%>").value=="")
	        {
	        document.getElementById("<%=yaxiscolor.ClientID%>").value="#000000"
	        }
	        if(document.getElementById("<%=titlefontcolor.ClientID%>").value=="")
	        {
	        document.getElementById("<%=titlefontcolor.ClientID%>").value="#000000"
	        }
	        if(document.getElementById("<%=titlebordercolor.ClientID%>").value=="")
	        {
	        document.getElementById("<%=titlebordercolor.ClientID%>").value="#000000"
	        }
	        if(document.getElementById("<%=bkcolor.ClientID%>").value=="")
	        {
	        document.getElementById("<%=bkcolor.ClientID%>").value="#ffffff"
	        }
	        if(document.getElementById("<%=Majorgridcolour.ClientID%>").value=="")
	        {
	        document.getElementById("<%=Majorgridcolour.ClientID%>").value="#000000"
	        }
	        if(document.getElementById("<%=xaxiscolor.ClientID%>").value=="")
	        {
	        document.getElementById("<%=xaxiscolor.ClientID%>").value="#000000"
	        }
	        
	        
	   	    document.getElementById("abc").value="X3DAngle:" + document.getElementById("<%=ddlXangle.ClientID%>").value  + "$" + "Perspective:" + document.getElementById("<%=ddlPerspective.ClientID%>").value + "$" + "Chk3D:"+ chk + "$" + "Y3Dangle:"+ document.getElementById("<%=ddlYang.ClientID%>").value + "$" + "Palettes:" + document.getElementById("<%=ddlPalettes.ClientID%>").value + "$" + "Bkclr:" + document.getElementById("<%=bkcolor.ClientID%>").value + "$" + "Gradient:" + document.getElementById("<%=ddlGradient.ClientID%>").value + "$" + "Hatchstyle:" + document.getElementById("<%=ddlHatchstyle.ClientID%>").value + "$" +  "Brclr:" + document.getElementById("<%=brcolor.ClientID%>").value + "$" +  "Bordersize:" + document.getElementById("<%=ddlBordersize.ClientID%>").value + "$" + "Borderstyle:" + document.getElementById("<%=ddlBorderstyle.ClientID%>").value + "$" + "Xlabelfont:" + document.getElementById("<%=ddlXlabelfont.ClientID%>").value+ "$" + "Xfontsizelist:" + document.getElementById("<%=ddlXfontsizelist.ClientID%>").value + "$" + "XLabelcolour:" +  document.getElementById("<%=xaxiscolor.ClientID%>").value + "$" + "Xontanglelist:" + document.getElementById("<%=ddlXontanglelist.ClientID%>").value + "$" + "Offset:"+ Xoffchk;
	        document.getElementById("abc1").value="Enable:" + document.getElementById("<%=chkXenable.ClientID%>").value+ "$" + "Yfont:" + document.getElementById("<%=ddlYfontname.ClientID%>").value + "$" + "Yfontsize:" + document.getElementById("<%=ddlYlabelfontsize.ClientID%>").value + "$" + "Yfontcolour:" + document.getElementById("<%=yaxiscolor.ClientID%>").value + "$" + "Yangle:" + document.getElementById("<%=ddlYangle.ClientID%>").value + "$" + "chkYoffset:" + document.getElementById("<%=chkYoffset.ClientID%>").value + "$" + "Yenable:" + document.getElementById("<%=chkYenable.ClientID%>").value + "$" + "Charttitle:" + document.getElementById("<%=txtCharttitle.ClientID%>").value + "$" + "XAxisTitle:" + document.getElementById("<%=txtTitleext.ClientID%>").value + "$" + "Yaxistitle:" + document.getElementById("<%=txtYTitle.ClientID%>").value + "$" + "Titlesize:" + document.getElementById("<%=ddlTitlesize.ClientID%>").value + "$" + "Font:" + document.getElementById("<%=ddlFont1.ClientID%>").value + "$" + "Color:" + document.getElementById("<%=titlefontcolor.ClientID%>").value + "$" + "BrClr:" + document.getElementById("<%=titlebordercolor.ClientID%>").value + "$"+ "ArClr:" + document.getElementById("<%=chartareabkcolor.ClientID%>").value;
	        document.getElementById("abc2").value="BkClr:" + document.getElementById("<%=titlebkcolor.ClientID%>").value + "$" + "Alignment:" + document.getElementById("<%=Alignment.ClientID%>").value + "$" + "Italic:" +chkitalic+ "$" + "Bold:" + chkbold + "$"  + "Underline:" + chkUline + "$" + "Strikeout:" + chkSout + "$" + "Mjrgdline:" + document.getElementById("<%=ddlMajorgridline.ClientID%>").value+ "$" + "Linetypes:" + document.getElementById("<%=ddlLinetypes.ClientID%>").value + "$" + "Mjrgdclr:" + document.getElementById("<%=Majorgridcolour.ClientID%>").value + "$" + "Mjrline:" + document.getElementById("<%=ddlMajorline.ClientID%>").value + "$" + "MjrInt:" + document.getElementById("<%=ddlMajorInterval.ClientID%>").value + "$" + "MnrType:" + document.getElementById("<%=ddlMinorType.ClientID%>").value + "$" + "MnrgidLineType:" + document.getElementById("<%=ddlMinoeLinetypes.ClientID%>").value + "$" + "MnrClr:" + document.getElementById("<%=ddlMinorColor1.ClientID%>").value + "$" + "MnrWidth:" + document.getElementById("<%=ddlMinorWidth.ClientID%>").value + "$" + "MnrInt:" + document.getElementById("<%=ddlMinorInterval.ClientID%>").value;
	        document.getElementById("Leg1").value="style:" + document.getElementById("<%=ddlLegendstyleList.ClientID%>").value+ "$" + "Dock:" + document.getElementById("<%=ddlLegendDockinglist.ClientID%>").value + "$" + "Alin:" + document.getElementById("<%=ddlLegendAlinmentList.ClientID%>").value + "$" + "Rev:" + chk_re + "$" + "tabst:" + document.getElementById("<%=ddltheTableStyle.ClientID%>").value + "$" + "lbk:" + document.getElementById("<%=Legendbkcolor.ClientID%>").value + "$" + "lbr:" + document.getElementById("<%=legendbrcolor.ClientID%>").value + "$" + "grd:" + document.getElementById("<%=ddlLegendgradient.ClientID%>").value + "$" + "hat:" + document.getElementById("<%=ddlLegendhatch.ClientID%>").value + "$" + "brs:" + document.getElementById("<%=ddlLegendbordersize.ClientID%>").value + "$" + "brst:" + document.getElementById("<%=ddlLegendborderstyle.ClientID%>").value;// + "$" + "Font:" + document.getElementById("<%=ddlFont1.ClientID%>").value + "$" + "Color:" + document.getElementById("<%=titlefontcolor.ClientID%>").value + "$" + "BrClr:" + document.getElementById("<%=titlebordercolor.ClientID%>").value + "$"+ "ArClr:" + document.getElementById("<%=chartareabkcolor.ClientID%>").value;
    	  	       
	        var chartname=document.getElementById("<%=hidchart.ClientID%>").value;
	        document.getElementById("prp").value="Linetype";

	     	var Repname;
	     	var currentspan;
	     	var currentclientspan;
	     	var currentlobsapn;
	       	     	
	     	if(("<%=currsp%>")=="")
	     	  {
	     	      Repname=document.getElementById("<%=ddlReport.ClientID%>").options[document.getElementById("<%=ddlReport.ClientID%>").selectedIndex].text;
	     	      currentspan=document.getElementById("<%=DepartmentName.ClientID%>").value;
	     	      currentclientspan=document.getElementById("<%=ClientName.ClientID%>").value;
	     	      currentlobsapn=document.getElementById("<%=ddlLobName.ClientID%>").value;
	     	           
	     	   }
  	     	   else 
	     	   {
	     	       Repname=document.getElementById("<%=txtCurrentreport.ClientID%>").value;
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

        function getValues_singleuser()//This Function used in Save Graph
	    {
	    
	       var Xoffchk="off";
	        if(document.getElementById("<%=chkShow3D.ClientID%>").checked==false)
	        {
	           var chk="off"; 
	        }
	        else 
	        {
	         chk="on";
	        }
	        if(document.getElementById("<%=chk_Reversed.ClientID%>").checked==false)
	        {
	           var chk_re="off"; 
	        }
	        else 
	        {
	         chk_re="on";
	        }
	        if(document.getElementById("<%=chkBold.ClientID%>").checked==false)
	        {
	           var chkbold="off"; 
	        }
	        else 
	        {
	         chkbold="on";
	        }
	        if(document.getElementById("<%=chkItalic.ClientID%>").checked==false)
	        {
	           var chkitalic="off"; 
	        }
	        else 
	        {
	         chkitalic="on";
	        }
	        if(document.getElementById("<%=chkUline.ClientID%>").checked==false)
	        {
	           var chkUline="off"; 
	        }
	        else 
	        {
	         chkUline="on";
	        }
	        if(document.getElementById("<%=chkSout.ClientID%>").checked==false)
	        {
	           var chkSout="off"; 
	        }
	        else 
	        {
	         chkSout="on";
	        }
	        if(document.getElementById("<%=chkXoffset.ClientID%>").checked==false)
	        {
	         Xoffchk="off"
	        }
	        else
	        {
	          Xoffchk="on"
	        }
	        if(document.getElementById("<%=yaxiscolor.ClientID%>").value=="")
	        {
	        document.getElementById("<%=yaxiscolor.ClientID%>").value="#000000"
	        }
	        if(document.getElementById("<%=titlefontcolor.ClientID%>").value=="")
	        {
	        document.getElementById("<%=titlefontcolor.ClientID%>").value="#000000"
	        }
	        if(document.getElementById("<%=titlebordercolor.ClientID%>").value=="")
	        {
	        document.getElementById("<%=titlebordercolor.ClientID%>").value="#000000"
	        }
	        if(document.getElementById("<%=bkcolor.ClientID%>").value=="")
	        {
	        document.getElementById("<%=bkcolor.ClientID%>").value="#ffffff"
	        }
	        if(document.getElementById("<%=Majorgridcolour.ClientID%>").value=="")
	        {
	        document.getElementById("<%=Majorgridcolour.ClientID%>").value="#000000"
	        }
	        if(document.getElementById("<%=xaxiscolor.ClientID%>").value=="")
	        {
	        document.getElementById("<%=xaxiscolor.ClientID%>").value="#000000"
	        }
	        
	        
	   	    document.getElementById("abc").value="X3DAngle:" + document.getElementById("<%=ddlXangle.ClientID%>").value  + "$" + "Perspective:" + document.getElementById("<%=ddlPerspective.ClientID%>").value + "$" + "Chk3D:"+ chk + "$" + "Y3Dangle:"+ document.getElementById("<%=ddlYang.ClientID%>").value + "$" + "Palettes:" + document.getElementById("<%=ddlPalettes.ClientID%>").value + "$" + "Bkclr:" + document.getElementById("<%=bkcolor.ClientID%>").value + "$" + "Gradient:" + document.getElementById("<%=ddlGradient.ClientID%>").value + "$" + "Hatchstyle:" + document.getElementById("<%=ddlHatchstyle.ClientID%>").value + "$" +  "Brclr:" + document.getElementById("<%=brcolor.ClientID%>").value + "$" +  "Bordersize:" + document.getElementById("<%=ddlBordersize.ClientID%>").value + "$" + "Borderstyle:" + document.getElementById("<%=ddlBorderstyle.ClientID%>").value + "$" + "Xlabelfont:" + document.getElementById("<%=ddlXlabelfont.ClientID%>").value+ "$" + "Xfontsizelist:" + document.getElementById("<%=ddlXfontsizelist.ClientID%>").value + "$" + "XLabelcolour:" +  document.getElementById("<%=xaxiscolor.ClientID%>").value + "$" + "Xontanglelist:" + document.getElementById("<%=ddlXontanglelist.ClientID%>").value + "$" + "Offset:"+ Xoffchk;
	        document.getElementById("abc1").value="Enable:" + document.getElementById("<%=chkXenable.ClientID%>").value+ "$" + "Yfont:" + document.getElementById("<%=ddlYfontname.ClientID%>").value + "$" + "Yfontsize:" + document.getElementById("<%=ddlYlabelfontsize.ClientID%>").value + "$" + "Yfontcolour:" + document.getElementById("<%=yaxiscolor.ClientID%>").value + "$" + "Yangle:" + document.getElementById("<%=ddlYangle.ClientID%>").value + "$" + "chkYoffset:" + document.getElementById("<%=chkYoffset.ClientID%>").value + "$" + "Yenable:" + document.getElementById("<%=chkYenable.ClientID%>").value + "$" + "Charttitle:" + document.getElementById("<%=txtCharttitle.ClientID%>").value + "$" + "XAxisTitle:" + document.getElementById("<%=txtTitleext.ClientID%>").value + "$" + "Yaxistitle:" + document.getElementById("<%=txtYTitle.ClientID%>").value + "$" + "Titlesize:" + document.getElementById("<%=ddlTitlesize.ClientID%>").value + "$" + "Font:" + document.getElementById("<%=ddlFont1.ClientID%>").value + "$" + "Color:" + document.getElementById("<%=titlefontcolor.ClientID%>").value + "$" + "BrClr:" + document.getElementById("<%=titlebordercolor.ClientID%>").value + "$"+ "ArClr:" + document.getElementById("<%=chartareabkcolor.ClientID%>").value;
	        document.getElementById("abc2").value="BkClr:" + document.getElementById("<%=titlebkcolor.ClientID%>").value + "$" + "Alignment:" + document.getElementById("<%=Alignment.ClientID%>").value + "$" + "Italic:" +chkitalic+ "$" + "Bold:" + chkbold + "$"  + "Underline:" + chkUline + "$" + "Strikeout:" + chkSout + "$" + "Mjrgdline:" + document.getElementById("<%=ddlMajorgridline.ClientID%>").value+ "$" + "Linetypes:" + document.getElementById("<%=ddlLinetypes.ClientID%>").value + "$" + "Mjrgdclr:" + document.getElementById("<%=Majorgridcolour.ClientID%>").value + "$" + "Mjrline:" + document.getElementById("<%=ddlMajorline.ClientID%>").value + "$" + "MjrInt:" + document.getElementById("<%=ddlMajorInterval.ClientID%>").value + "$" + "MnrType:" + document.getElementById("<%=ddlMinorType.ClientID%>").value + "$" + "MnrgidLineType:" + document.getElementById("<%=ddlMinoeLinetypes.ClientID%>").value + "$" + "MnrClr:" + document.getElementById("<%=ddlMinorColor1.ClientID%>").value + "$" + "MnrWidth:" + document.getElementById("<%=ddlMinorWidth.ClientID%>").value + "$" + "MnrInt:" + document.getElementById("<%=ddlMinorInterval.ClientID%>").value;
	        document.getElementById("Leg1").value="style:" + document.getElementById("<%=ddlLegendstyleList.ClientID%>").value+ "$" + "Dock:" + document.getElementById("<%=ddlLegendDockinglist.ClientID%>").value + "$" + "Alin:" + document.getElementById("<%=ddlLegendAlinmentList.ClientID%>").value + "$" + "Rev:" + chk_re + "$" + "tabst:" + document.getElementById("<%=ddltheTableStyle.ClientID%>").value + "$" + "lbk:" + document.getElementById("<%=Legendbkcolor.ClientID%>").value + "$" + "lbr:" + document.getElementById("<%=legendbrcolor.ClientID%>").value + "$" + "grd:" + document.getElementById("<%=ddlLegendgradient.ClientID%>").value + "$" + "hat:" + document.getElementById("<%=ddlLegendhatch.ClientID%>").value + "$" + "brs:" + document.getElementById("<%=ddlLegendbordersize.ClientID%>").value + "$" + "brst:" + document.getElementById("<%=ddlLegendborderstyle.ClientID%>").value;// + "$" + "Font:" + document.getElementById("<%=ddlFont1.ClientID%>").value + "$" + "Color:" + document.getElementById("<%=titlefontcolor.ClientID%>").value + "$" + "BrClr:" + document.getElementById("<%=titlebordercolor.ClientID%>").value + "$"+ "ArClr:" + document.getElementById("<%=chartareabkcolor.ClientID%>").value;
    	  	       
	        var chartname=document.getElementById("<%=hidchart.ClientID%>").value;
	        document.getElementById("prp").value="Linetype";

	     	var Repname;
	     	var currentspan;
	     	var currentclientspan;
	     	var currentlobsapn;
	       	     	
	     	if(("<%=currsp%>")=="")
	     	  {
	     	      Repname=document.getElementById("<%=ddlReport.ClientID%>").options[document.getElementById("<%=ddlReport.ClientID%>").selectedIndex].text;
	     	      currentspan=60;
	     	      currentclientspan=0;
	     	      currentlobsapn=0;
	     	           
	     	   }
  	     	   else 
	     	   {
	     	       Repname=document.getElementById("<%=txtCurrentreport.ClientID%>").value;
	     	       currentspan=60; 
                   currentclientspan=0;
                   currentlobsapn=0;
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
        
        function ShowCalendar()//To Date Calendar
		{
		     window.txtBox = document.getElementById('<%=txtfromdate.ClientID%>');
		     window.txtBox.value = window.showModalDialog('../Calendar/Calendar.htm',window.txtBox.value,'dialogLeft:200px;dialogTop:200px;dialogHeight:210px;dialogWidth:265px;center:No;help:No;scroll:No;resizable:No;status:No;');
				if (window.txtBox.value=="undefined")			
				{
				    window.txtBox.value="";    
				}
		}
		function ShowCalendar1()//From Date Calendar
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
	    
       
    </script>

<div id="divScroll"  style="height:700px; width:220px; overflow:scroll; scrollbar-arrow-color:#0582BE; scrollbar-base-color:#58C7FC;">
   <div id="divScroll1" onclick="toggleDiv('divChart', 'imgChart',500)" style="cursor: pointer;background-color: #4D8678; color: White;">
        <table>
            <tr>
                <td style="width: 190px">
                    <label for="imgChart" title="Select Chart" class="leftLabel"><strong>Select Chart</strong></label> 
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
                <asp:ImageButton ID="imgcolumn" runat="server" AlternateText="Click Here To Select Column Chart" ImageUrl="../images/Chart/ColumnChart.jpg"
                        Style="filter: alpha(opacity=50); -moz-opacity: 0.5; height: 30px; width: 30;" />
                </td>
                                         
                 <td align="left" style="height: 11px; color: #ffffff; font-weight: bold; font-size: 10px;
                    font-family: Verdana;  width: 282px;" valign="middle">
                    <asp:LinkButton ID="lnkColumnchart"  runat="server" Text="Column chart" ToolTip="Click On Column Chart" ForeColor="White"></asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td colspan="2" style="background-color: #86c1cc">
                </td>
            </tr>
            <tr>
                <td style="width: 46px; height: 15px;" valign="middle">
                    <asp:ImageButton ID="imgArea" runat="server" AlternateText="Click Here To Select Area Chart"
                        src="../images/Chart/AreaChart.jpg"
                        Style="filter: alpha(opacity=50); -moz-opacity: 0.5; height: 30px; width: 30;" />
                </td>
                <td align="left" style="height: 15px; color: #ffffff; font-weight: bold; font-size: 10px;
                    font-family: Verdana; width: 282px;">
                    <asp:LinkButton ID="LinkButton2" runat="server" ForeColor="white"  Text="Area chart" ToolTip="Click On Area Chart"></asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td bgcolor="#86c1cc" colspan="2">
                </td>
            </tr>
            <tr>
                <td style="width: 46px; height: 15px;">
                    <asp:ImageButton ID="imgPie" runat="server" AlternateText="Click Here To Select Pie Chart"
                        src="../images/Chart/PieChart.jpg"
                       Style="filter: alpha(opacity=50); -moz-opacity: 0.5; height: 30px; width: 30;" />
                </td>
                <td align="left" style="height: 15px; color: #ffffff; font-weight: bold; font-size: 10px;
                    font-family: Verdana; width: 282px;">
                    <asp:LinkButton ID="LinkButton3" runat="server" ForeColor="white" Text="Pie chart" ToolTip="Click On Pie Chart"></asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td bgcolor="#86c1cc" colspan="2">
                </td>
            </tr>
            <tr>
                <td style="width: 46px; height: 15px;">
                    <asp:ImageButton ID="imgLine" runat="server" AlternateText="Click Here To Select Line Chart" ImageUrl="../images/Chart/LineChart.jpg"
                        Style="filter: alpha(opacity=50); -moz-opacity: 0.5; height: 30px; width: 30;" />
                </td>
                <td align="left" style="height: 15px; color: #ffffff; font-weight: bold; font-size: 10px;
                    font-family: Verdana; width: 282px;">
                    <asp:LinkButton ID="LinkButton4" runat="server" ForeColor="white" Text="Line chart" ToolTip="Click On Line Chart"></asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td bgcolor="#86c1cc" colspan="2">
                </td>
            </tr>
            <tr>
                <td style="width: 46px; height: 15px;">
                    <asp:ImageButton ID="imgScatter" runat="server" AlternateText="Click Here To Select Scatter Chart"
                        src="../images/Chart/ScatterChart.jpg"
                        Style="filter: alpha(opacity=50); -moz-opacity: 0.5; height: 30px; width: 30;" />
                </td>
                <td align="left" style="height: 15px; color: #ffffff; font-weight: bold; font-size: 10px;
                    font-family: Verdana; width: 282px;">
                    <asp:LinkButton ID="LinkButton5" runat="server" ForeColor="white" Text="XY(Scatter) chart" Width="152px" ToolTip="Click On Scatter Chart"></asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td bgcolor="#86c1cc" colspan="2" >
                </td>
            </tr>
            <tr>
                <td style="width: 46px; height: 15px;">
                    <asp:ImageButton ID="imgHistogram" runat="server" AlternateText="Click Here To Select Histogram Chart"
                        ImageUrl="../images/Chart/HistogramChart.jpg"
                        Style="filter: alpha(opacity=50); -moz-opacity: 0.5; height: 30px; width: 30;" />
                </td>
                <td align="left" style="height: 15px; color: #ffffff; font-weight: bold; font-size: 10px;
                    font-family: Verdana; width: 282px;">
                    <asp:LinkButton ID="LinkButton6" runat="server" ForeColor="white" Text="Histogram chart" ToolTip="Click On Histogram Chart"></asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td bgcolor="#86c1cc" colspan="2">
                </td>
            </tr>
            <tr>
                <td style="width: 46px; height: 15px;">
                    <asp:ImageButton ID="imgPareto" runat="server" AlternateText="Click Here To Select Pareto  Chart"
                        ImageUrl="../images/Chart/ParetoChart.jpg"
                        Style="filter: alpha(opacity=50); -moz-opacity: 0.5; height: 30px; width: 30;" />
                </td>
                <td align="left" style="height: 15px; color: #ffffff; font-weight: bold; font-size: 10px;
                    font-family: Verdana; width: 282px;">
                    <asp:LinkButton ID="LinkButton7" runat="server" ForeColor="white" Text="Pareto chart" ToolTip="Click On Pareto Chart"></asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td bgcolor="#86c1cc" colspan="2" >
                </td>
            </tr>
            <tr>
                <td style="width: 46px; height: 15px;">
                    <asp:ImageButton ID="imgRun" runat="server" AlternateText="Click Here To Select Run Chart"
                        ImageUrl="../images/Chart/RunChart.jpg"
                        Style="filter: alpha(opacity=50); -moz-opacity: 0.5; height: 30px; width: 30;" />
                </td>
                <td align="left" style="height: 15px; color: #ffffff; font-weight: bold; font-size: 10px;
                    font-family: Verdana; width: 282px;">
                    <asp:LinkButton ID="LinkButton8" runat="server" ForeColor="white" Text="Run chart" ToolTip="Click On Run Chart"></asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td bgcolor="#86c1cc" colspan="2">
                </td>
            </tr>
            <tr>
                <td style="width: 46px; height: 15px;">
                    <asp:ImageButton ID="imgScaterplot" runat="server" AlternateText="Click Here To Select ScatterPlot Chart"
                        ImageUrl="../images/Chart/ScaterPlotChart.jpg"
                        Style="filter: alpha(opacity=50); -moz-opacity: 0.5; height: 30px; width: 30;" />
                </td>
                <td align="left" style="height: 15px; color: #ffffff; font-weight: bold; font-size: 10px;
                    font-family: Verdana; width: 282px;">
                    <asp:LinkButton ID="LinkButton9" runat="server" ForeColor="white" Text="ScatterPlot chart" ToolTip="Click On ScatterPlot Chart" Width="144px"></asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td bgcolor="#86c1cc" colspan="2">
                </td>
            </tr>
            <tr>
                <td style="width: 46px; height: 15px;">
                    <asp:ImageButton ID="imgStock" runat="server" AlternateText="Click Here To Select Stock Chart"
                        ImageUrl="../images/Chart/StockChart.jpg"
                        Style="filter: alpha(opacity=50); -moz-opacity: 0.5; height: 30px; width: 30;" />
                </td>
                <td align="left" style="height: 15px; color: #ffffff; font-weight: bold; font-size: 10px;
                    font-family: Verdana; width: 282px;">
                    <asp:LinkButton ID="LinkButton10" runat="server" ForeColor="white" Text="Stock chart" ToolTip="Click On Stock Chart"></asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td bgcolor="#86c1cc" colspan="2">
                </td>
            </tr>
            <tr>
                <td style="width: 46px; height: 15px;">
                    <asp:ImageButton ID="imgDaughnt" runat="server" AlternateText="Click Here To Select Doughnut Chart"
                         ImageUrl="../images/Chart/DoughntChart.jpg"
                        Style="filter: alpha(opacity=50); -moz-opacity: 0.5; height: 30px; width: 30;" />
                </td>
                <td align="left" style="height: 15px; color: #ffffff; font-weight: bold; font-size: 10px;
                    font-family: Verdana; width: 282px;">
                    <asp:LinkButton ID="LinkButton11" runat="server" ForeColor="white" Text="Doughnut chart" ToolTip="Click On Doughnut Chart"></asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td bgcolor="#86c1cc" colspan="2">
                </td>
            </tr>
            <tr>
                <td style="width: 46px; height: 15px;">
                    <asp:ImageButton ID="imgBar" runat="server" AlternateText="Click Here To Select Bar Chart"
                         ImageUrl="../images/Chart/BarChart.jpg"
                        Style="filter: alpha(opacity=50); -moz-opacity: 0.5; height: 30px; width: 30;" />
                </td><%--   <label for="ctl00_MainPlaceHolder_ddlClient" class="label">
                                Select Level 2</label>--%>
                <td align="left" style="height: 15px; color: #ffffff; font-weight: bold; font-size: 10px;
                    font-family: Verdana; width: 282px;">
                    <asp:LinkButton ID="LinkButton12" runat="server" ForeColor="white" Text="Bar chart" ToolTip="Click On Bar Chart"></asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td bgcolor="#86c1cc" colspan="2">
                </td>
            </tr>
        </table>
    </div>
    
    <div id="div3" onclick="toggleDiv('divShow3dchart', 'imgFormat1',110)"
        style="cursor: pointer;background-color: #4D8678; color: White;">
        <table>
            <tr>
                <td style="width: 190px">
                    <label for="imgformat1" title="Chart In 3D" class="leftLabel"><strong>Chart In 3D</strong></label> 
                </td>
                <td align="right">
                    <img id="imgFormat1" alt="Expand/Collapse" src="../images/ArrowDown.gif" />
                </td>
            </tr>
        </table>
    </div>
    <div id="divShow3dchart" style="overflow: hidden; display: none; color: White;">
    

        <table summary="This Table Holds 3D Chart Properties" width="80%">
                  <tr>
                <td scope="col" title="3D Chart">
                    <label class="leftLabel" for="ctl00_LeftPlaceHolder_chkShow3D">Show 3D</label>
                </td>
                <td scope="col" title="Show Chart As 3D" style="width: 106px">
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
                            <asp:ListItem Selected="True" Value="20">20</asp:ListItem>
                            <asp:ListItem Value="30">30</asp:ListItem>
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
                            <asp:ListItem Selected="True" Value="0">0</asp:ListItem>
                            <asp:ListItem Value="10">10</asp:ListItem>
                            <asp:ListItem Value="15">15</asp:ListItem>
                            <asp:ListItem  Value="30">30</asp:ListItem>
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
                            <asp:ListItem Selected="True" Value="5">5</asp:ListItem>
                            <asp:ListItem Value="10">10</asp:ListItem>
                            <asp:ListItem Value="30">30</asp:ListItem>
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
        style="cursor: pointer;background-color: #559585; color: White;">
        <table>
            <tr>
                <td style="width: 190px">
                    <label for="img6" title="BackGround Apperance" class="leftLabel"><strong>BackGround Apperance</strong></label> 
                </td>
                <td align="right">
                    <img id="img6" alt="Expand/Collapse"  src="../images/ArrowDown.gif" />
                </td>
            </tr>
        </table>
    </div>
    <div id="divChartappearance" style="overflow: hidden; display: none; color: White;" title="This table holds Chart Background Appearence like Background palettes, Background style.">
        <table summary="This Table Holds Chart Background Appearence like Background Palettes, Background Style Etc." style="width: 82%">
            <tr>
                <td scope="col" title="Chart Background Palettes" style="width: 118px">
                    <label class="leftLabel" for="ctl00_LeftPlaceHolder_ddlPalettes">Palettes</label>
                </td>
                <td scope="col" title="Select Chart Background Palettes" style="width: 135px">
                    <asp:DropDownList ID="ddlPalettes" runat="server" CssClass="leftdropdownlist">
                        <asp:ListItem>SeaGreen</asp:ListItem>
                        <asp:ListItem>EarthTones</asp:ListItem>
                        <asp:ListItem>Pastel</asp:ListItem>
                        <asp:ListItem>Excel</asp:ListItem>
                        <asp:ListItem Selected="True">Dundas</asp:ListItem>
                        <asp:ListItem>Kindergarten</asp:ListItem>
                        
                        
                    </asp:DropDownList>
                </td>
            </tr>
            
            <tr>
                <td scope="col" title="Chart Backcolor" style="width: 118px;">
                 <a href="javascript: onclick=pickerPopup202('<%=bkcolor.ClientID%>','sample_2');">
                        BackColor</a>
                </td>
                <td scope="col" align="left" valign="bottom" title="Select Chart Backcolor" style=" height:26px; width: 200px;">
                   <label  for="sample_2"></label> 
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
                    <%--<label for="ctl00_MainPlaceHolder_ddlLob" class="label">
                                Select Level 3</label>--%>
                    <a href="javascript: onclick=pickerPopup202('<%=brcolor.ClientID%>','sampleBordercolor');">
                    Bordercolor</a>
                 </td>
                <td scope="col" title="Select Chart Border Color" style="width: 135px">
 <label  for="sampleBordercolor"></label> 
                    <input type="text" id="sampleBordercolor" style="width: 80px"/>
                    <%--<tr>
            <td align="left" colspan="1" scope="col" style="width: 122px" title="Show Report Columns">
            </td>
            <td scope="col" title="Show Report Columns" style="width: 594px" align="left" valign="top">
                &nbsp;<table summary="This table used for select multiple column from one listbox to another list box" title="Select Multiple columns From report">
                    <tr>
                        <td scope="col" title="Select multiple column" style="height: 147px" >
<label for="ctl00_MainPlaceHolder_repcols" ></label>
                            <asp:ListBox ID="repcols" SelectionMode="Multiple" runat="server" CssClass="listBox"></asp:ListBox>
                        </td>
                        <td scope="col" title="" valign="middle" style="width: 42px; height: 147px;" >
                            &nbsp;<asp:Button ID="add" runat="server" Text=">>" CssClass="button" Width="40px" ToolTip="Click on button to add items" />
                            <asp:Button ID="remove" runat="server" Text="<<" CssClass="button" Width="40px"  ToolTip="Click on button to remove items" />
                        </td>
                        <td scope="col" title="Selected Column " style="height: 147px; width: 144px;" >
  <label for="ctl00_MainPlaceHolder_selectedcols" ></label>                          
<asp:ListBox ID="selectedcols" SelectionMode="Multiple" runat="server" CssClass="listBox"></asp:ListBox>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>--%>
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
    </div>
    <div id="divLegend" onclick="toggleDiv('divLegendappearance', 'img6',400)"
        style="cursor: pointer;background-color: #559585; color: White;">
        <table>
            <tr>
                <td style="width: 190px">
                  <label for="img1" title="Legend Apperance" class="leftLabel"><strong>Legend Apperance</strong></label>
                </td>
                <td align="right">
                    <img id="img1" alt="Expand/Collapse"  src="../images/ArrowDown.gif" />
                </td>
            </tr>
        </table>
    </div>
    <div id="divLegendappearance" style="overflow: hidden; display: none; color: White;" title="This Table Holds Legend Background Appearence like Background Palettes, Background Style Etc.">
        <table summary="This Table Holds Legend Background Appearence like Background Palettes, Background Style Etc." style="width: 82%">
            <tr>
                <td scope="col" title="Legend Backcolor" style="height: 10px; width: 118px;">
                 <a href="javascript: onclick=pickerPopup202('<%=legendbkcolor.ClientID%>','txtlegengbkcolor');">
                    <%--<input type="button" id="btnopen" onclick="return Opengraph();" class="button" runat="server" name="OpenGraph" title="Click To Open Graph" value="OpenGraph" style="width: 73px" />  --%>BackColor</a>
                </td>
                <td scope="col" title="Select Legend Backcolor" style="height: 10px; width: 200px;">
  <label  for="txtlegengbkcolor"></label>
                  <input type="text" id="txtlegengbkcolor" style="width: 80px"/>
               </td>
            </tr>
            <tr>
                <td scope="col" title="Legend Gradient Style" style="width: 118px">
                    <label class="leftLabel" for="ctl00_LeftPlaceHolder_ddlLegendgradient">Gradient</label>
                </td>
                <td scope="col" title="Select Legend Gradient Style" style="width: 135px">
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
<label  for="txtlegengbrcolor"></label>
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
				<td scope="col" title="Show Table Style" style="width: 135px">
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
			<tr>
				<td scope="col" title="Reversed" style="width: 118px">
				 <label class="leftLabel" for="ctl00_LeftPlaceHolder_chk_Reversed">Reversed</label>
				 </td>
				<td scope="col" title="Check Reversed" style="width: 135px">
					<asp:checkbox id="chk_Reversed" runat="server" ></asp:checkbox></td>
				</tr>
		</table>
    </div>
    
    <div id="div9" title="Label Format" onclick="toggleDiv('divLabelformat', 'img7',350)"
        style="cursor: pointer;background-color: #5a9e8d; color: White;">
        <table summary="This Table Holds The Label Format Image">
            <tr>
                <td scope="col" title="Click Here To Expand/Collapse The Label Format" style="width: 190px" >
                   <label for="img7" title=" Chart Label Format" class="leftLabel"><strong>Chart Label Format</strong></label>
                </td>
                <td align="right" scope="col" title="Click Here To Expand/Collapse The Label Format" style="width: 16px">
                    <img id="img7" alt="Expand/Collapse"  src="../images/ArrowDown.gif" />
                </td>
            </tr>
        </table>
    </div>
    <div id="divLabelformat" title="Chart X And Y Axis Label Format" style="overflow: hidden; display: none; color:White;">
        <table summary="This Table Holds The X Axis Labels Font Name,Font Size,Text,Font Color,Offset.">
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
                     <a href="javascript: onclick=pickerPopup202('<%=xaxiscolor.ClientID%>','SampleCol');">
               FontColour</a>
                </td>
                <td scope="col" title="Select X-Axis Label Font Color" style="width: 112px">
<label  for="SampleCol"></label>
                  <input type="text" id="SampleCol" style="width: 80px"/>
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
                    <a href="javascript: onclick=pickerPopup202('<%=yaxiscolor.ClientID%>','LColor');">
                  FontColour</a>
                </td>
                <td scope="col" title="Select Y-Axis Label Font Color" style="width: 144px">
<label  for="LColor"></label>
                  <input type="text" id="LColor" style="width: 80px"/>
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
        <table summary="This Table Holds Chart Title Formating">
            <tr>
                <td scope="col" title="Chart Title" style="width: 190px">
                    <label for="img8" title="Title and Axis Format" class="leftLabel"><strong>Title and Axis Format</strong></label>
                </td>
                <td align="right" scope="col" title="Chart Title">
                    <img id="img8" title="Expand/Collapse" alt="Expand/Collapse" src="../images/ArrowDown.gif" />
                </td>
            </tr>
        </table>
    </div>
    <div id="divFormat" title="Chart Title and Axis Format" style="overflow: hidden; display: none; color:White;">
        <table summary="This Table Holds The Chart Title,X-Axis Title ,Y-Axis Title,Title Back-Color,Font-Color,Font-Size,Title Aligment,Italic Font,Bold Font." style="width: 192px">
            <tr>
                <td scope="col" title="ChartTitle" style="width: 89px">
                    <label class="leftLabel" for="ctl00_LeftPlaceHolder_txtCharttitle">Chart Title:</label>
                </td>
                <td scope="col" title="Enter Chart Title" style="width: 93px">
                    <asp:TextBox ID="txtCharttitle" runat="server" Height="16px" CssClass="leftdropdownlist" Width="96px">Chart Title</asp:TextBox>
                </td>
            </tr>
            <tr>
                <td scope="col" title="X-Axis Title" style="width: 89px">
                   <label class="leftLabel" for="ctl00_LeftPlaceHolder_txtTitleext">X-AxisTitle:</label>
                </td>
                <td scope="col" title="Enter X-Axis Title" style="width: 93px">
                   <asp:TextBox ID="txtTitleext" runat="server" Height="16px" CssClass="leftdropdownlist" Width="96px">XAxis Title</asp:TextBox>
                </td>
            </tr>
            <tr>
                <td scope="col" title="Y-Axis Title" style="width: 89px">
                    <label class="leftLabel" for="ctl00_LeftPlaceHolder_txtYTitle">YAxis Title:</label>
                </td>
                <td scope="col" title=" Enter Y-Axis Title" style="width: 93px">
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
<label  for="SamColor"></label>
                  <input type="text" id="SamColor" style="width: 80px"/>
                </td>
            </tr>
            <tr>
                <td scope="col" title="Chart  Border Color" style="width: 89px">
                  <a href="javascript: onclick=pickerPopup202('<%=titlebordercolor.ClientID%>','LineColor');">
                BorderColor</a>
                </td>
                <td scope="col" title="Select Chart Border Color" style="width: 93px">
<label  for="LineColor"></label>
                  <input type="text" id="LineColor" style="width: 80px"/>
                </td>
            </tr>
            <tr>
                <td scope="col" title="Chart Back Color" style="width: 89px">
                    <a href="javascript: onclick=pickerPopup202('<%=titlebkcolor.ClientID%>','SampleColor');">
               Back Color</a>
                 </td>
                <td scope="col" title="Select Chart Back Color" style="width: 93px">
<label  for="SampleColor"></label>
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
                    <label for="imgGridline" title="GridLines Format" class="leftLabel"><strong>GridLines Format</strong></label>
                </td>
                <td align="right" scope="col" title="Expand/Collapse">
                    <img id="imgGridline" title="Expand/Collapse" alt="Expand/Collapse" src="../images/ArrowDown.gif" />
                </td>
            </tr>        </table>
    </div>
    <div id="divGridline" title="GridLine Format" style="overflow: hidden; display: none; color:White;">
      <table summary="This Table Is Used For Select Major/Minor Gridlines/Trickmark,Gridlins Color,Gridline Width,Gridline Interval.">
          <tr>
              <td colspan="2" scope="col" title="MajorGridLine Format" style="text-decoration: underline">
                 <label class="leftLabel" title="MinorGridLine Format" for="ctl00_LeftPlaceHolder_ddlMajorgridline"> MajorGridLine Format</label> 
              </td>
          </tr>
            <tr>
                <td scope="col" title="Major GridLine And Major Tickmark" style="width: 204px">
                    <label class="leftLabel" for="ctl00_LeftPlaceHolder_ddlMajorgridline">Major</label>
                </td>
                <td scope="col" title="Select Major GridLine Or Major TickMark" style="width: 132px">
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
                <td scope="col" title="Select Major Vertical GridLine Or Major Horizantal GridLine" style="width: 132px">
                    <asp:DropDownList ID="ddlLinetypes" runat="server" CssClass="leftdropdownlist">
                        <asp:ListItem Value="Vertical">Vertical</asp:ListItem>
                        <asp:ListItem Value="Horizontal">Horizontal</asp:ListItem>
                    </asp:DropDownList>
                 </td>
            </tr>
            <tr>
                <td scope="col" title="Major GridLine Color" style="width: 204px">
                    <a href="javascript: onclick=pickerPopup202('<%=Majorgridcolour.ClientID%>','SampleLine');">
                   LineColor</a>
                </td>
                <td scope="col" title="Select Major GridLine Color" style="width: 132px">
<label  for="SampleLine"></label>
                  <input type="text" id="SampleLine" style="width: 80px"/>
                </td>
            </tr>
            <tr>
                <td scope="col" title="Major GridLine Width" style="width: 204px">
                    <label class="leftLabel" for="ctl00_LeftPlaceHolder_ddlMajorline">LineWidth</label>
                </td>
                <td scope="col" title="Select Major GridLine Width" style="width: 132px">
                    <asp:DropDownList ID="ddlMajorline" runat="server" Height="22px" CssClass="leftdropdownlist">
                        <asp:ListItem Value="1">1</asp:ListItem>
                        <asp:ListItem Value="2">2</asp:ListItem>
                        <asp:ListItem Value="3">3</asp:ListItem>
                        <asp:ListItem Value="4">4</asp:ListItem>
                    </asp:DropDownList>
                 </td>
            </tr>
            <tr>
                <td scope="col" title="Major GridLine Interval" style="width: 204px">
                    <label class="leftLabel" for="ctl00_LeftPlaceHolder_ddlMajorInterval">Interval</label>
                 </td>
                <td scope="col" title="Select Major GridLine Interval" style="width: 132px">
                    <asp:DropDownList ID="ddlMajorInterval" runat="server" CssClass="leftdropdownlist">
                        <asp:ListItem Value="1">1</asp:ListItem>
                        <asp:ListItem Value="2">2</asp:ListItem>
                        <asp:ListItem Value="3">3</asp:ListItem>
                        <asp:ListItem Value="4">4</asp:ListItem>
                        <asp:ListItem Value="5">5</asp:ListItem>
                        <asp:ListItem Value="6">6</asp:ListItem>
                        <asp:ListItem Value="7">7</asp:ListItem>
                        <asp:ListItem Value="8">8</asp:ListItem>
                        <asp:ListItem Value="9">9</asp:ListItem>
                        <asp:ListItem Value="10">10</asp:ListItem>
                        <asp:ListItem Value="20">20</asp:ListItem>
                        <asp:ListItem Value="30">30</asp:ListItem>
                        <asp:ListItem Value="40">40</asp:ListItem>
                        <asp:ListItem Value="50">50</asp:ListItem>
                        <asp:ListItem Value="60">60</asp:ListItem>
                        <asp:ListItem Value="70">70</asp:ListItem>
                        <asp:ListItem Value="80">80</asp:ListItem>
                        <asp:ListItem Value="90">90</asp:ListItem>
                        <asp:ListItem Value="100">100</asp:ListItem>
                        <asp:ListItem Value="200">200</asp:ListItem>
                        <asp:ListItem Value="300">300</asp:ListItem>
                        <asp:ListItem Value="400">400</asp:ListItem>
                        <asp:ListItem Value="500">500</asp:ListItem>
                    </asp:DropDownList>
                 </td>
            </tr>
          <tr>
              <td colspan="2" scope="col" title="Minor GridLine Format" style="text-decoration: underline">
                <label class="leftLabel" title="Minor GridLine Format" for="ctl00_LeftPlaceHolder_ddlMinorType"> MinorGridLine Format</label> 
              </td>
          </tr>
            <tr>
                <td scope="col" title="Minor GridLine Or Minor Tickmark" style="width: 204px; height: 24px">
                    <label class="leftLabel" for="ctl00_LeftPlaceHolder_ddlMinorType">Minor:</label>
                </td>
                <td scope="col" title="Select Minor GridLine Or MinorTickmark" style="width: 132px; height: 24px">
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
                    <%--<table summary="This table hold the Graphcontrol and multiple Graph based Dropdown list controls." title="Graph On Selected Columns">--%>
                    <a href="javascript: onclick=pickerPopup202('<%=ddlMinorColor1.ClientID%>','SampleLineColor');">
                  LineColor </a>
                </td>
                <td scope="col" title=" Select Minor GridLines Color" style="width: 132px">
<label  for="SampleLineColor"></label>
                  <input type="text" id="SampleLineColor" style="width: 80px"/>
                    <%-- <tr>
                     <td align="center" scope="col" style="width: 56589796px" title="">--%>
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
                        <asp:ListItem Value="0.9">0.9</asp:ListItem>
                    </asp:DropDownList>
                 </td>
            </tr>
            <!-- REPEAT THE ROW ABOVE FOR EACH LABEL - CONTROL PAIR -->
        </table>
    </div>
    <div id="divChartArea" title="ChartArea Format" onclick="toggleDiv('divAreaposition', 'imgChartarea',370)" style="cursor: pointer;
         background-color: #76b1a3; color: White;">
        <table>
            <tr>
                <td scope="col" style="width: 190px" title="Expand/Collapse">
                    <label for="imgChartarea" title="ChartArea Format" class="leftLabel"><strong>ChartArea Format</strong></label>
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
								<td colspan="2" >
								    <label for="" title="Chart Area Position" class="leftLabel"><strong>Chart Area Position</strong></label>
								</td>
								<td></td>
							</tr>
							<tr>
								<td style="width: 110px" >
								    <label for="ctl00_LeftPlaceHolder_txtX1" title="X" class="leftLabel"><strong>X</strong></label>
								</td>
								<td><asp:textbox id="txtX1" runat="server" Width="100px">10</asp:textbox></td>
							</tr>
							<tr>
								<td style="width: 110px" >
								    <label for="ctl00_LeftPlaceHolder_txtY1" title="Y" class="leftLabel"><strong>Y</strong></label>
								 </td>
								<td><asp:textbox id="txtY1" runat="server" Width="100px">10</asp:textbox></td>
							</tr>
							<tr>
								<td style="width: 110px" >
								    <label for="ctl00_LeftPlaceHolder_txtWidth1" title="Width" class="leftLabel"><strong>Width</strong></label>
								</td>
								<td><asp:textbox id="txtWidth1" runat="server" Width="100px">80</asp:textbox></td>
							</tr>
							<tr>
								<td style="width: 110px" >
								    <label for="ctl00_LeftPlaceHolder_txtHeight1" title="Height" class="leftLabel"><strong>Height</strong></label>
								</td>
								<td><asp:textbox id="txtHeight1" runat="server" Width="100px">80</asp:textbox></td>
							</tr>
							<tr>
								<td colspan="2" >
								    <label for="ctl00_LeftPlaceHolder_txtX2" title="Plotting Area Position" class="leftLabel"><strong>Plotting Area Position</strong></label>
								 </td>
								<td>
								</td>
							</tr>
							<tr>
								<td style="width: 110px" >
								    <label for="ctl00_LeftPlaceHolder_txtX2" title="X Axis" class="leftLabel"><strong>X Axis</strong></label>
								 </td>
								<td><asp:textbox id="txtX2" runat="server" Width="100px">10</asp:textbox></td>
							</tr>
							<tr>
								<td style="width: 110px" >
								    <label for="ctl00_LeftPlaceHolder_txtY2" title="Y Axis" class="leftLabel"><strong>Y Axis</strong></label>
								</td>
								<td><asp:textbox id="txtY2" runat="server" Width="100px">10</asp:textbox></td>
							</tr>
							<tr>
								<td style="width: 110px" >
								    <label for="ctl00_LeftPlaceHolder_txtWidth2" title="Width" class="leftLabel"><strong>Width</strong></label>
								</td>
								<td><asp:textbox id="txtWidth2" runat="server" Width="100px">80</asp:textbox></td>
							</tr>
							<tr>
								<td style="width: 110px" >
								    <label for="ctl00_LeftPlaceHolder_txtHeight2" title="Height" class="leftLabel"><strong>Height</strong></label>
								</td>
								<td><asp:textbox id="txtHeight2" runat="server" Width="100px">80</asp:textbox></td>
							</tr>
							<tr>
								<td colspan="2" >
								    <label for="ctl00_LeftPlaceHolder_ddlChartwidth" title="Chart Zooming:" class="leftLabel"><strong>Chart Zooming</strong></label>
								</td>
								<td></td>
							</tr>
							<tr>
								<td style="width: 110px" >
								    <label for="ctl00_LeftPlaceHolder_ddlChartwidth" title="Position" class="leftLabel"><strong>Position</strong></label>
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
                                    <label for="Areabackcolor"  ></label>
					<input type="text" id="Areabackcolor"  enableviewstate="true" style="width: 72px;"/>
                                </td>
                             </tr>
                             <tr>
                                <td>
                                    <label for="ctl00_LeftPlaceHolder_chk_hide" title="PieCollect" ><strong>PieCollect</strong></label>
                                </td>
                                <td>
                                    <asp:CheckBox ID="chk_hide" runat="server" AutoPostBack="false" Text="PieCollect" />
                                </td>
                             </tr>
							</table>   
            
    </div>
      <div id="divAnimation1" title="Animation Format" onclick="toggleDiv('divAnimation', 'img13',240)" style="cursor: pointer;
        background-color: #97c4ba; color: White;">
        <table>
            <tr>
                <td scope="col" style="width: 190px" title="Click Here for Expand/Collapse GridLine Format">
                    <label for="img13" title="Animation Format" class="leftLabel"><strong>Animation Format</strong></label>
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
				<td ><label for="ctl00_LeftPlaceHolder_CheckBoxLegend" title="Legend" class="leftLabel"><strong>Legend</strong></label></td>
				<td>
					<asp:checkbox id="CheckBoxLegend" runat="server" ForeColor="white" Checked="True" Text="Move Legend Items One-By-One" AutoPostBack="false" ></asp:checkbox>
				</td>
			</tr>
		   <tr>
								<td> <label for="ctl00_LeftPlaceHolder_CheckBoxSeries" title="seriese" class="leftLabel"><strong>seriese</strong></label></td>
								<td>
									<asp:checkbox id="CheckBoxSeries" runat="server"  Checked="True" Text="Move Series One-By-One" AutoPostBack="false" ></asp:checkbox>
								</td>
							</tr>
		   <tr>
								<td ><label for="ctl00_LeftPlaceHolder_CheckBoxPoints" title="Points" class="leftLabel"><strong>Points</strong></label></td>
								<td>
									<asp:checkbox id="CheckBoxPoints" runat="server" ForeColor="white" Checked="True" Text="Move Points One-By-One" AutoPostBack="false" ></asp:checkbox>
								</td>
							</tr>
		   <tr>
				<td >
					<label for="ctl00_LeftPlaceHolder_ddlTheme" title="Theme" class="leftLabel"><strong>Theme</strong></label></td>
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
    <%--<div id="divMask" style="display: none; filter: alpha(opacity=30); left: 0px; cursor: hand; position: relative; top: 0px; background-color:Green; cursor: pointer;" onmouseout="javascript:pointOut();" onclick="javascript:pointClick();">
                        </div>--%>
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
</div>



<div id="mm" runat="server" visible="false">
       
    </div>
     <%--</tr>
   </table>
        --%>
    <asp:HiddenField ID="hidChart" runat="server" />
</asp:Content>

<asp:Content ID="mainPane" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <link rel="stylesheet" type="text/css" href="../App_Themes/Themes/StyleSheet.css" />
    <script language="JavaScript" src="../js/picker.js" type="text/javascript">
    function imgFormat_onclick() 
    {
    }
      
   </script>
    <%-- </div>--%>
   <div id="divcaption" style="padding:20px;" >
        <table summary="This table hold the span , Reportname and columns "  width="100%">
            <caption><strong>SELECT REPORT</strong></caption>
            <tr>
                <td style="width: 725px" >
                    <div id="Selectreport" runat="server" visible="false">
                        <table >
                        <tr>
                            <td scope="col" title="Department" class="style2"  >
                                 </td>
                            <%--</tr>
       </table>
            
   </div>--%>                    </td>
                            <td scope="col" title=" Select Department"  colspan="" >
                                <asp:DropDownList ID="ddlDepartmant"  runat="server" AutoPostBack="True" 
                                    CssClass="dropdownlist" Visible="False">
                                </asp:DropDownList>
                             </td>
                        </tr>
                        <tr>
                            <td scope="col" title="Client" class="style2"   >
                              
                                 
                                
                                
                                <%--<style>.FadingTooltip { BORDER-RIGHT: darkgray 1px outset; BORDER-TOP: darkgray 1px outset; FONT-SIZE: 12pt; BORDER-LEFT: darkgray 1px outset; WIDTH: auto; COLOR: black; BORDER-BOTTOM: darkgray 1px outset; HEIGHT: auto; BACKGROUND-COLOR: lemonchiffon; MARGIN: 3px 3px 3px 3px; padding: 3px 3px 3px 3px; borderBottomWidth: 3px 3px 3px 3px }
			</style>--%>
         </td>
        
                            <td scope="col" title="Select Client"  >
                                <asp:DropDownList ID="ddlClient" runat="server" AutoPostBack="True" 
                                    CssClass="dropdownlist" Visible="False">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td scope="col" title="LOB" class="style2"   >
                                <%-- <div class="FadingTooltip" id="FADINGTOOLTIP" style="Z-INDEX: 999; VISIBILITY: hidden; POSITION: absolute"></div>--%>
                            </td>
                            <td scope="col" title="Select Lob"   >
                                <asp:DropDownList ID="ddlLob" runat="server" AutoPostBack="True" 
                                    CssClass="dropdownlist" Visible="False">
                                </asp:DropDownList>
                            
                             </td>
                             <td>&nbsp;</td>
                             <td><asp:Label ID="lblChartimage" runat="server" ForeColor="#006666" Font-Bold="true" Font-Underline="true" ></asp:Label></td>
                        </tr>
                         <tr>   
                           <td class="style2">
                           <table id="spandisplay" runat="server">
                           <tr>
                           <td class="style1"><asp:Label ID="select_level1" runat="server" Text="Select level 1" CssClass="label"></asp:Label></td>
                           <td class="style5"><asp:DropDownList ID="DepartmentName" Visible="true" CssClass="dropdownlist" 
                                   runat="server" AutoPostBack="True"></asp:DropDownList></td>
                           </tr>
                           <tr>
                           <td class="style1"><asp:Label ID="select_level2" runat="server" Text="Select level 2" CssClass="label"></asp:Label></td>
                           <td class="style5"><asp:DropDownList ID="ClientName" Visible="true" CssClass="dropdownlist" 
                                   runat="server" AutoPostBack="True"></asp:DropDownList></td>
                           </tr>
                           <tr>
                           <td class="style1"><asp:Label ID="select_level3" runat="server" Text="Select level 3" CssClass="label"></asp:Label></td>
                           <td class="style5"><asp:DropDownList ID="ddlLobName" Visible="true" CssClass="dropdownlist" 
                                   runat="server" AutoPostBack="True"></asp:DropDownList></td>
                           </tr>
                           </table>                                                 
                           </td>
                           <td>
                               <asp:Button ID="get_report" runat="server" Text=" Get Report" CssClass="button"/></td>
                           </tr>
                        <tr>
                            <td scope="col"  title="Report Name" nowrap="nowrap" class="style2">
                                <label for="ctl00_MainPlaceHolder_txtCurrentReport" class="label">Report Name</label></td>
                            <td scope="col" colspan="7" nowrap="nowrap" title="Current Report Name" style="width: 545px"   >
                              <asp:TextBox ID="txtCurrentReport" runat="server" ReadOnly="true" Width="150px" Visible="false"></asp:TextBox>
                                <label for="ctl00_MainPlaceHolder_ddlReport" ></label> 
                                <asp:DropDownList ID="ddlReport" runat="server" CssClass="dropdownlist" AutoPostBack="True"></asp:DropDownList>
                                &nbsp; &nbsp; &nbsp; &nbsp;<asp:Label ID ="lblgraphname" runat="server" Text="Graph Name"></asp:Label>
         <label for="ctl00_MainPlaceHolder_ddlgraphname" ></label>                        
<asp:DropDownList ID="ddlgraphname" runat="server" CssClass="dropdownlist"  AutoPostBack="true"></asp:DropDownList>
                          </td>
                            </tr>
                        </table>
                        </div>
                        <div id ="analysistable">
                </div>
                         <div id="date">
                        <table summary="">
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
                        <div id="btn">
                    <table width="100%" summary="">
                      <tr>
                        <td align="center" colspan="1" scope="col" style="width: 109px" title="Click On Show Report Button To Show The Report Column ">
                        </td>
                        <td align="left" scope="col" title="Click On Show Report Button To Show The Report Column " style="width: 594px">
                        <asp:Button ID="ShowReport" runat="server" Text="Show Report" CssClass="button" />&nbsp;&nbsp;
                         &nbsp;
                        <asp:Button ID="ShowReport_singleuser" runat="server" Text="Show Report" 
                                CssClass="button" />
                        </td>
                     </tr>
                  </table>
                </div>
                <div id ="lst">
                    <table summary="">
                        <tr>
                           <td valign="top"  scope="col" title="Start Date" style="width:97px"   >
                           
                           </td>
                            <td style="width: 134px" scope="col">
 <label for="ctl00_MainPlaceHolder_repcols" ></label>   
                                 <asp:ListBox ID="repcols" SelectionMode="Multiple" runat="server" CssClass="listBox" Width="136px"></asp:ListBox>
                            </td>
                            <td style="width: 36px" scope="col">
                                <asp:Button ID="add" runat="server" Text=">>" CssClass="button" Width="40px" ToolTip="Click on button to add items" />    
                                <asp:Button ID="remove" runat="server" Text="<<" CssClass="button" Width="40px"  ToolTip="Click on button to remove items" />
                            </td>
                            <td style="width: 125px" scope="col">
<label for="ctl00_MainPlaceHolder_selectedcols" ></label>   
                               <asp:ListBox ID="selectedcols" SelectionMode="Multiple" runat="server" CssClass="listBox" Width="136px"></asp:ListBox>
                            </td>
                            <td>
                            </td>
                        </tr>
                    </table>
                   </div>
                    <%--<script language="javascript">
   

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
			</script>--%>
        
        <div>
            <table  summary="">
            <tr>
            <td align="center" colspan="1" scope="col" style="width: 107px" title="">
            </td>
            <td colspan="8" scope="colgroup" title="" align="left" style="color: black;" 
                    class="style3">
               <asp:RadioButton ID="rbnRow" runat="server" GroupName="check" Text="Row Series" Width="96px" ToolTip="Select chart  Row series"  AutoPostBack="true" /><asp:RadioButton ID="rbnColumn" runat="server" GroupName="check" Text="Column Series" Width="120px" ToolTip="Select chart  Column series" AutoPostBack="True" />
                <asp:CheckBox ID="chkanimated" runat="server" Text="Animated Graph" AutoPostBack="true" Width="136px" /><asp:CheckBox ID="chkSunnarized" runat="server" Text="Summarized Graph" AutoPostBack="true" />
            </td>
        </tr>
        <tr>
            <td align="center" colspan="1" scope="col" style="width: 107px; height: 13px" title=" Click on Graph Button">
            </td>
            <td align="left" colspan="8" scope="colgroup" title=" Click on Graph Button" 
                class="style4">
            <asp:Button ID="btnGraph" runat="server" Text="Plot Graph" CssClass="button" ToolTip="Click To Show Graph" Width="96px"   />&nbsp;<asp:Button 
                    ID="btnGraph_singleuser" runat="server" Text="Plot Graph" CssClass="button" 
                    ToolTip="Click To Show Graph" Width="96px"   />&nbsp;
            <input type="button" id="btnsave" runat="server" onclick="javascript:return Savegraphs();" class="button" runat="server" name="SaveGraph" title="Click To Save Graph" value="Save" style="width: 73px" />  
                <input type="button" runat="server" id="btnsave_singleuser" 
                    onclick="javascript:return Savegraphs_singleuser();" class="button" runat="server" 
                    name="SaveGraph0" title="Click To Save Graph" value="Save" 
                    style="width: 73px" />  
                <%--<img src="../images/Chart/RunChart.jpg"/>--%>
            <asp:Button ID="btnOpenGraph"  runat="server" Text="OpenGraph"  CssClass="button" ToolTip="Click To Open Graph" Width="88px"   />
                &nbsp;&nbsp;
            <asp:Button ID="btnOpenGraph_singleuser"  runat="server" Text="OpenGraph"  
                    CssClass="button" ToolTip="Click To Open Graph" Width="88px"   />
            </td>
        </tr>
                <tr>
                    <td align="center" colspan="1" scope="col" style="width: 107px; height: 13px" title=" Click on Graph Button">
                    </td>
                    <td align="left" colspan="8" scope="colgroup" title=" Click on Graph Button" 
                        class="style4">
                        <span id="spnGraphname"></span>
                        <asp:Label ID="lblgraph" runat="server"></asp:Label>
                    </td>
                </tr>
            </table>
        </div>
        <div></div>
        <div>
             <table summary="This table hold the Graphcontrol and multiple Graph based Dropdown list controls." title="Graph On Selected Columns">
        <tr>
            <td scope="col" title="Graph On Selected Columns" style="width: 112px">
           
                <%--This hidden field is used for Clendar control--%><%-- hidden report/constituent backcolor --%>
                    </td>
                        <td scope="col" title="Graph On Selected Columns" style="height: 908px" valign="top">
                        
                            <dcwc:Chart ID="Chart1" runat="server" Visible="false" BackColor="#FAE6E6" BackGradientEndColor="255, 240, 240" alt="Default Chart"
                BackGradientType="DiagonalLeft" BorderLineColor="LightSlateGray" BorderLineStyle="Solid" 
                Palette="Kindergarten" Width="400px" Height="400px" MapEnabled="False" >
                <Legends>
<dcwc:Legend BackGradientEndColor="245, 224, 224" BackColor="100, 255, 255, 255" LegendStyle="Column" BorderColor="100, 0, 0, 0" Name="Default"></dcwc:Legend>
</Legends>
                <UI>
<Toolbar BackColor="255, 240, 240" BorderColor="75, 0, 0, 0">
<BorderSkin PageColor="Transparent" SkinStyle="Raised"></BorderSkin>
</Toolbar>
</UI>
                <Titles>
<dcwc:Title Font="Microsoft Sans Serif, 9.75pt" Text="Column Chart" Color="DodgerBlue" Style="Emboss" Name="Title1"></dcwc:Title>
</Titles>
                <BorderSkin FrameBackColor="SteelBlue" FrameBackGradientEndColor="LightBlue" FrameBorderColor="100, 0, 0, 0"
                    FrameBorderWidth="2" PageColor="Transparent" SkinStyle="Emboss" />
                 <ChartAreas>
<dcwc:ChartArea BackGradientEndColor="245, 224, 224" BorderColor="100, 0, 0, 0" BackColor="100, 255, 255, 255" BorderStyle="Solid" Name="Chart Area 1">
<AxisX Interval="1" LineColor="64, 64, 64, 64">
<LabelStyle Format=""></LabelStyle>

<MajorTickMark Enabled="False" Size="2"></MajorTickMark>

<MajorGrid LineColor="64, 64, 64, 64"></MajorGrid>
</AxisX>

<Area3DStyle XAngle="15" YAngle="10" RightAngleAxes="False" Clustered="True" Perspective="10" WallWidth="0"></Area3DStyle>

<AxisY LineColor="64, 64, 64, 64">
<MajorGrid LineColor="64, 64, 64, 64"></MajorGrid>
</AxisY>

<AxisY2>
<MajorGrid Enabled="False"></MajorGrid>
</AxisY2>
</dcwc:ChartArea>
</ChartAreas>
            </dcwc:Chart>
            
                            <dcwc:chart id="Chart2"  runat="server" alt="Chart"
                             Width="412px" Height="296px" BackColor="#F3DFC1" Palette="Dundas" 
                             BorderLineStyle="Solid" BackGradientType="TopBottom" BorderLineWidth="2" 
                             BorderLineColor="181, 64, 1" ImageType="Png" ImageUrl="~/TempImages/ChartPic_#SEQ(300,3)" Visible="False" MapEnabled="False">
						
						<Titles>
<dcwc:Title Font="Microsoft Sans Serif, 9.75pt" Text="Histogram" Color="DodgerBlue"></dcwc:Title>
</Titles>
						
						
<dcwc:Legend Name="Default" Enabled="False" AutoFitText="False" BackColor="Transparent" Font="Trebuchet MS, 8.25pt, style=Bold"   ></dcwc:Legend>

							<series>

<dcwc:Series Name="DataDistribution" XValueType="Double" YValueType="Double" ChartType="Point" Color="120, 252, 180, 65" BorderColor="110, 26, 59, 105" Font="Trebuchet MS, 8.25pt" MarkerSize="8" ></dcwc:Series>
<dcwc:Series Name="Histogram" XValueType="Double" YValueType="Double" ChartArea="HistogramArea" ShowLabelAsValue="True" Color="224, 64, 10" BorderColor="180, 26, 59, 105" Font="Trebuchet MS, 8.25pt"></dcwc:Series>

</series>

<chartareas>
<dcwc:ChartArea AlignWithChartArea="HistogramArea" BackColor="OldLace" BackGradientType="TopBottom" BackGradientEndColor="White" ShadowColor="Transparent" BorderColor="64, 64, 64, 64" BorderStyle="Solid" Name="Default">
<AxisY LabelsAutoFit="False" Title="" TitleFont="Trebuchet MS, 8pt" LineColor="64, 64, 64, 64" Enabled="True">
<MajorGrid LineColor="64, 64, 64, 64"></MajorGrid>

<MajorTickMark Size="1.5" LineColor="Transparent" Enabled="False"></MajorTickMark>

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
						    <dcwc:chart id="StockChart" runat="server" 
						    Visible="false" BackColor="#D3DFF0" Width="460px" Height="400px" 
						    ImageUrl="~/TempImages/ChartPic_#SEQ(300,3)" ImageType="Png" BorderLineStyle="Solid" 
						    Palette="Dundas" BackGradientEndColor="White" BackGradientType="TopBottom" BorderLineWidth="2" 
						    BorderLineColor="26, 59, 105" enableviewstate="True" viewstatecontent="All" alt="Chart" MapEnabled="False">
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
		                </td>
                 </tr>
                </table>
                </div>
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
                   
   --%><%-- hidden report/constituent backcolor --%>    
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
                   
   --%><%-- hidden report/constituent backcolor --%>
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
    <asp:HiddenField ID="specificformat" runat="server" />
    <asp:HiddenField ID="RepGraphtype" runat="server" />
      <asp:HiddenField ID="Hiddenquerylalit" runat="server" />
    <input type="hidden" name="abc" id="abc" />
    <input type="hidden" name="abc1" id="abc1" />
    <input type="hidden" name="abc2" id="abc2" />
    <input type="hidden" name="Leg1" id="Leg1" />
    <input type="hidden" name="Leg2" id="Leg2" />
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
    <input type="hidden" name="hiddlob1" id="hiddlob" runat="server" />
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
    <input type="hidden" name="ReportType" id="ReportType" /></asp:Content>
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
<asp:Content ID="Content1" runat="server" contentplaceholderid="head">
    <style type="text/css">
        .style1
        {
            width: 190px;
        }
        .style2
        {
            width: 325px;
        }
        .style3
        {
            width: 594px;
        }
        .style4
        {
            height: 13px;
            width: 594px;
        }
        .style5
        {
            width: 219px;
        }
    </style>
    </asp:Content>

