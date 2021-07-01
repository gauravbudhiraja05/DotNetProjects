<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Product.aspx.vb" Inherits="Content_Product" %>
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
        <div class="grid_10 omega">
            <h2>Know About Qlick Products</h2>
                <p class="sedTxtlink"><a href="PDF/CP_Qlickreport.pdf" target="_blank" style="color:#0000CC;">Corporate Presentation</a></p>
                <p class="sedTxt">IDMS is powerful role-based web application which gives dashboard view of key business data. It is an evolving business intelligence MIS & reporting software provides easy and enhanced MIS reporting, data analysis and customized email-alerts. <br>
                <p height="5"></p>
                <span>An integrated powerful graphical reporting tool has made charting easy. Users can schedule business alerts that inform the right people of critical occurrences when time is of the essence.</span></p>
            <div class="grid_5 alpha ">
                <h2>Our Qlick Products </h2>
                    <p class="loremtxt" style="width:550px; text-align:justify;">Create Report with ease and convenience with any of the following tools with different abilities and features and export your data to excel.</p>
                    <a href="QlickQueryBuilder.aspx"><p class="manyTwo" style="width:520px;"><strong><u>Qlick Query Builder</u></strong> - helps any user to create, share, save as, save queries from multiple tables and views. </p></a>
                    <a href="QlickReportDesigner.aspx"><p class="manyTwo" style="width:520px;"><strong><u>Qlick Report Designer</u></strong> - Header and Footer, Data in specified format, Functions by SQL statement, Summarized output in grouping.</p></a>
                    <a href="QlickDataAnalysis.aspx"><p class="manyTwo" style="width:520px;"><strong><u>Qlick Data Analysis</u></strong> - Is able to do various analysis on data tables and views. User is able to save & share the analysis.</p></a>
                    <a href="QlickGraph.aspx"><p class="manyTwo" style="width:520px;"><strong><u>Qlick Graph</u></strong> - The graph of data is based on specification given by creator at the time of creating/edit report/query/analysis etc.</p></a>
                <h2>Other Qlick Features</h2>
                    <p class="loremtxt" style="width:540px; text-align:justify;">Available with single or multiple user licence with User, Hierarchy and Rights management with multiple Admin & SuperAdmin</p>
                    <a href=""><p class="manyTwo" style="width:520px;"><strong><u>Qlick Database</u></strong> <br>- Single (MSSQL, MySql & Oracle)<br>- Multiple (Two or More)</p></a>
                    <a href=""><p class="manyTwo" style="width:520px;"><strong><u>Qlick Data Transfer</u></strong> <br>- Import<br>- Real Time</p></a>
                    <a href=""><p class="manyTwo" style="width:520px;"><strong><u>Qlick Email Scheduler</u></strong> <br>- Event Based<br>- Condition Based</p></a>
                    <a href=""><p class="manyTwo" style="width:520px;"><strong><u>Qlick Sharing</u></strong> <br>- Local<br>- Non Local</p></a>
                    <!--a href="#" title="more" class="manyMore">more...</a--></p>
                </div>
            </div>
            
        </div>
    </div>
</div>

<!--body -->
<!--footer -->
<uc4:Footer ID="Footer" runat="server" />
<!--footer -->
</body>
</html>
