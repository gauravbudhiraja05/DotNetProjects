<%@ Page Language="VB" AutoEventWireup="false" CodeFile="QlickQueryBuilder.aspx.vb" Inherits="Content_QlickQueryBuilder" %>
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
                <p class="QueryBuilder">Qlick Query Builder<span>helps any user to create, share, save as, save queries from multiple tables and views. </span></p>
            </div>
            <div class="clear" style="height:10px;"></div>
                <p class="sedTxtlink"><a href="QlickReportDesigner.aspx"style="color:#0000CC;">Qlick Report Designer</a>&nbsp;&nbsp;|&nbsp;&nbsp;<a href="QlickDataAnalysis.aspx" style="color:#0000CC;">Qlick Data Analysis</a>&nbsp;&nbsp;|&nbsp;&nbsp;<a href="QlickGraph.aspx"style="color:#0000CC;">Qlick Graph</a></p>
                <p>The Query Builder is used  to create, share, save as, save queries from multiple tables and views. It’s gives the ability to use  the simple graphical interface to build statements and create views in a database .<br>
                <p style="height:10px;"></p>
        
            <div class="Clear">
                <h2>Features</h2>
                <p class="sedTxt">To create queries from multiple tables and views.</p>
                <p class="sedTxt">Allow user’s to save , save as and  share queries.</p>
                <p class="sedTxt">Allow user’s to generate queries by drag and drop.</p>
                <p class="sedTxt">Allow user’s to print reports in Excel format. </p>
                <p class="sedTxt">A visual interface to design queries</p>
                <p class="sedTxt">Create joins with drag and drop</p>
                <p class="sedTxt">The Grid pane to specify criteria</p>

                        <%--<p class="sedTxt">Crasstab (Open/ Share)</p>
                        <p class="sedTxt">Where</p>
                        <p class="sedTxt">Row</p>
                        <p class="sedTxt">Column</p>
                        <p class="sedTxt">Data</p>
                        <p class="sedTxt">Trash</p>
                        <p class="sedTxt">Formula</p>
                        <p class="sedTxt">Show SubTotal</p>
                        <p class="sedTxt">Drill Down </p>
                        <p class="sedTxt">Other Features</p>--%>


            
            </div>
            <div class="clear" style="height:10px;"></div>
                <p class="vero">Other Details of Qlick Query Builder</p>
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
