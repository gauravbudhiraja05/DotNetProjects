<%@ Page Language="VB" AutoEventWireup="false" CodeFile="QlickGraph.aspx.vb" Inherits="Content_QlickGraph" %>
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
                <p class="Graph">Qlick Graph<span>enables any member to analyze their reports and graphically see the output</span></p>
            </div>
            <div class="clear" style="height:10px;"></div>
                <p class="sedTxtlink"><a href="QlickQueryBuilder.aspx"style="color:#0000CC;">Qlick Query Builder</a>&nbsp;&nbsp;|&nbsp;&nbsp;<a href="QlickReportDesigner.aspx"style="color:#0000CC;">Qlick Report Designer</a>&nbsp;&nbsp;|&nbsp;&nbsp;<a href="QlickDataAnalysis.aspx" style="color:#0000CC;">Qlick Data Analysis</a></p>
                <p>It has graphical presentation of data. The presentation of data is based on specification given by creator at the time of creating/edit report/query/analysis etc.</p><br />
                <p>When the report is displayed it has an option for graphical presentation when graphical presentation button is clicked it displays the data in specified style. Here on this screen viewer is able to manipulate the graphs by adding or removing data columns available in respective report. <br>
                <p style="height:10px;"></p>
            <div class="Clear">
                <h2>Following Graphs/Charts types can be made by the User</h2>
                    <p>Area Chart</p>
                    <p>Bar Chart</p>
                    <p>Column Chart</p>
                    <p>Line Chart (Multi-line)</p>
                    <p>Pie Chart</p>
                    <p>XY (Scatter) Chart</p>
                    <p>Doughnut Chart</p>
                    <p>Radar Chart</p>
                    <p>Surface Chart</p>
                    <p>Bubble Chart</p>
                    <p>Stock Chart</p>
                    <p>Cone, Cylinder, and Pyramid Chart Types</p>

            </div>
            <div class="Clear">
                <h2>Other Features</h2>
                        <p class="sedTxt">Chart Type</p>
                        <p class="sedTxt">Chart Label Format</p>
                        <p class="sedTxt">Gridlines Format</p>
                        <p class="sedTxt">Chart Area Format</p>
                        <p class="sedTxt">Gridlines Format</p>
                        <p class="sedTxt">Row Series</p>
                        <p class="sedTxt">Column Series</p>
                        <p class="sedTxt">Summarized Graph</p>
                        <p class="sedTxt">Animated Graph</p>
                        <p class="sedTxt">Save Graphs</p>
            </div>
            <div class="clear" style="height:10px;"></div>
                <p class="vero">Other Details of Qlick Graph</p>
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
