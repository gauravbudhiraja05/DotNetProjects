<%@ Page Language="VB" AutoEventWireup="false" CodeFile="QlickReportDesigner.aspx.vb" Inherits="Content_QlickReportDesigner" %>
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
                <p class="ReportDesigner">Qlick Report Designer<span>helps any user to create, share, save as, save queries from multiple tables and views. </span></p>
            </div>
            <div class="clear" style="height:10px;"></div>
                <p class="sedTxtlink"><a href="QlickQueryBuilder.aspx"style="color:#0000CC;">Qlick Query Builder</a>&nbsp;&nbsp;|&nbsp;&nbsp;<a href="QlickDataAnalysis.aspx" style="color:#0000CC;">Qlick Data Analysis</a>&nbsp;&nbsp;|&nbsp;&nbsp;<a href="QlickGraph.aspx"style="color:#0000CC;">Qlick Graph</a></p>
                <p class="vero">It is able to display data in specified format like colors, font size, bold, italic and underline.&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</p>
                <p class="sedTxt">This module will enable any member to create, share, save, edit, save as and graphically represent reports created through any number of tables and views.<br>
                <p style="height:5px;"></p>
                <span>It is user friendly and attractive.	</span></br></br>
                <span>It has designing feature of header and footer using which we are able to display any data from any table anywhere in header and footer. We are able to apply colors based on conditions.</span></br></br>
                <span>We are able to display data in specified format like colors, font size, bold, italic and underline. Colors on data are based on conditions. Conditions can be fixed and dynamic i.e. application is able to decide the color of column based on other columns in report. There is no limitation for applying conditions. </span></br></br>
                <span>It is able to perform all the functions that can be performed by SQL statement. </span></br></br>
                <span>Summarized output in multi level of groupings.</span></br></br>
                <span>After the report has been displayed view, it can sort data on columns by clicking them.</span></p>
                <p style="height:10px;"></p>
            
            <div class="Clear">
                <h2>Features</h2>
                        <p class="sedTxt">Drilldown Report</p>
                        <p class="sedTxt">Pivot Report</p>
                        <span>A pivot query allows multiple representations of data according to different dimensions. This query type is similar to tabular query, except it also allows data to be represented in summary format</span>
                        <p class="sedTxt">Summarized Report </p>
                        <span><b>Depending on the data type of the field you plan to summarize, you can:</b><br />
                                1- Sum the values in each group.<br />
                                2- Count all the values or only those values that are distinct from one another.<br />
                                3- Determine the maximum, minimum, average, or Nth largest value.<br />
                                4- Calculate up to two kinds of standard deviations and variances.</span><br /><br />
                        <p class="sedTxt">Design Report (Header, Detail Pane, Footer)</p>
                        <span><b>Header - </b>The Report Header section generally contains the report title and other information you want to appear only at the beginning of the report. Formulas placed in this section are evaluated once, at the beginning of the report.</span><br /><br />
                        <span><b>Footer - </b>This section is used to contain information you want to appear only once at the end of the report, such as grand totals. Formulas placed in this section are evaluated once, at the end of the report.</span>
                        <p class="sedTxt">Set Clauses (Where, Formula, Having, Order by, Group By)</p>
                        <p class="sedTxt">Formats (on Whole Report, Header, Detail Pane, Footer)</p>
                        <p class="sedTxt">Save Report (Report Save with Local, Non-Local, Global)</p>
                        <p class="sedTxt">Share Report</p>
                        <p class="sedTxt">Archive Report</p>

            
            </div>
            <div class="clear" style="height:10px;"></div>
                <p class="vero">Other Details of Qlick Report Designer</p>
                <p class="sedTxt"><a href="#" style="color:#0000CC;">Comments</a>&nbsp;&nbsp;|&nbsp;&nbsp;<a href="#" style="color:#0000CC;">Proposal</a>&nbsp;&nbsp;|&nbsp;&nbsp;<a href="#" style="color:#0000CC;">PPT</a>&nbsp;&nbsp;|&nbsp;&nbsp;<a href="#" style="color:#0000CC;">Business Enquiry</a><br></p>
            </div>
            
        </div>
        
         <!--Detail Content panel END-->
        
    </div>
</div>

<!--body -->
<!--footer -->
<uc4:Footer ID="Footer" runat="server" />
<!--footer -->
</body>
</html>
