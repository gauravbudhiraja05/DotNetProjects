<%@ Page Language="VB" AutoEventWireup="false" CodeFile="QlickDataAnalysis.aspx.vb" Inherits="Content_QlickDataAnalysis" %>
<%@ Register src="~/Content/WebControl/Navigation.ascx" tagname="Navigation" tagprefix="uc1" %>
<%@ Register Src="~/Content/WebControl/LeftPanel.ascx" TagName="Left" TagPrefix="uc3" %>
<%@ Register Src="~/Content/WebControl/FooterBanner.ascx" TagName="Footer" TagPrefix="uc4" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1" />
<title>QlickReport Software combines your Financial and Operational data into a single, seamless source of information for better business decisions</title>
<link href="../css/base.css" rel="stylesheet" type="text/css" />
</head>
<body  style="background-image:url('images/fadestrip2.png'); background-position:top; background-repeat:repeat-x; background-attachment: fixed;">
<!--navigation -->

<uc1:Navigation ID="Navigation" runat="server" />

<!--navigation -->
<!--header -->
<div class="container_161">
    <table>
        <tr>
            <td style="width:30px;"></td>
            <td><img src="Images/MiniTop.jpg"/></td>
            <td style="width:30px;"></td>
        </tr>
    </table>
	    
</div>
<!--header -->
<!--body -->

<div class="container_16">
    <div class="grid_16" id="body">
        <div class="grid_6  alpha">
<!--left panel -->
            <uc3:Left ID="Left" runat="server" />
<!--left panel -->
        </div>
        <div class="grid_10 omega" style="width:550px;text-align:justify;">
            <!--Detail Content panel START-->
            
            <div class="block">
                <p class="DataAnalysis">Qlick Data Analysis<span>enables any member to analyze their reports and graphically see the output</span></p>
            </div>
            <div class="clear" style="height:10px;"></div>
                <p class="sedTxtlink"><a href="QlickQueryBuilder.aspx"style="color:#0000CC;">Qlick Query Builder</a>&nbsp;&nbsp;|&nbsp;&nbsp;<a href="QlickReportDesigner.aspx"style="color:#0000CC;">Qlick Report Designer</a>&nbsp;&nbsp;|&nbsp;&nbsp;<a href="QlickGraph.aspx" style="color:#0000CC;">Qlick Graph</a></p>
                <p>Application is able to provide various analysis base data tables and views. User is able to save/save as/edit/share/remove the analysis.</p>
                <div class="Clear">
                <h2>Following features makes the data analysis very effective:</h2>
                <p class="sedTxt">Overview reporting</p>
                <p class="sedTxt">Drill-down reporting</p>
                <p class="sedTxt">Cross-tab analysis in 2D or 3D</p>
                <p class="sedTxt">Correlation</p>
                <p class="sedTxt">Regression</p>
                <p class="sedTxt">Compare samples</p>
                <p class="sedTxt">Row percentage</p>
                <p class="sedTxt">Column percentage</p>
                <p class="sedTxt">Non-weighted number, Population (weighted number)</p>
                <p class="sedTxt">Filter percentage</p>
                <p class="sedTxt">Index</p>
                <p class="sedTxt">Quantitative values: Mean, Median, Standard deviation, Standard error, Row sum percentage, Column sum percentage, Filter percentage, Accumulated sum and Min/max/count/average.</p>
            </div>
            <div class="clear" style="height:10px;"></div>
                <p class="vero">Other Details of Qlick Data Analysis</p>
                <p class="sedTxt"><a href="#" style="color:#0000CC;">Comments</a>&nbsp;&nbsp;|&nbsp;&nbsp;<a href="#" style="color:#0000CC;">Proposal</a>&nbsp;&nbsp;|&nbsp;&nbsp;<a href="#" style="color:#0000CC;">PPT</a>&nbsp;&nbsp;|&nbsp;&nbsp;<a href="#" style="color:#0000CC;">Business Enquiry</a><br></p>
            </div>
            <!--Detail Content panel END-->
        </div>
    </div>
</div>

<!--body -->
<!--footer -->
<uc4:Footer ID="Footer" runat="server" />
<!--footer -->
</body>
</html>
