<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Default.aspx.vb" Inherits="_Default" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1" />
<title>QlickReport Software combines your Financial and Operational data into a single, seamless source of information for better business decisions</title>
<link href="css/base.css" rel="stylesheet" type="text/css" />
</head>
<body>
<!--navigation -->
<div class="container_16">
    <div class="grid_16 navbg">
        <ul class="navlink">
	        <li><a href="Default.aspx" title="Home">Home</li>
	        <li><a href="Content/Aboutus.aspx" title="About">Company</a></li>
	        <li><a href="Content/Product.aspx" title="Product">Products</a></li>
	        <li><a href="Content/QlickSupport.aspx" title="Solution">Support</a></li>
	        <li><a href="Content/QlickLicense.aspx" title="Solution">Qlick License</a></li>
	        <li class="nodivider"><a href="Content/ContactUs.aspx" title="Contact">Contact</a></li>
        </ul>
    </div>
</div>
<!--navigation -->
<!--header -->
<div class="container_16">
    <div class="grid_16 hdrBg">
	    <a href="index.html"><img src="images/logo.gif" alt="Business.com" border="0" class="logo" title="Business.com"/></a>
	        <h1>Extensive Core Reporting Functionality
	            <span>It is an evolving business intelligence MIS & reporting software provides easy and enhanced MIS reporting, data analysis and customized email-alerts.</span>
	        </h1>
	    <a href="Content/PDF/CP_Qlickreport.pdf" target="_blank" title="read more" class="readmore">read more</a>
    </div>
</div>
<!--header -->
<!--body -->
<div class="container_16">
    <div class="grid_16" id="body">
        <div class="grid_6  alpha">
<!--left panel -->
            <div id="lftPan">
                <form action="" method="get" class="one" runat="server">
                <asp:Label runat="server" ID="lblmsg" Visible="false" ForeColor="Red"></asp:Label><asp:Label runat="server" ID="lbldays" Visible="false" ForeColor="Red"></asp:Label>
                    <p class="member">Member login</p>
                    <label>Username :</label>
                    <input id="txtuserid" name="txtuserid" type="text" class="txtfield" runat="server"/>
                    <label>Password :</label>
                    <input id="txtpassword" name="txtpassword" type="password" class="txtfield" runat="server"/>
                    <p class="forgot"><a href="Misc/ForgotPassword.aspx">Forgot Password?</a></p>
                    <%--<input name="" type="submit" class="login" value="" title="login"/>--%>
                    <input id="Login" name="" type="button" class="login" value="" title="login" runat="server" onserverclick="Login_Click"/>
                    <div class="clear"></div>
                </form>
                <h2 class="service">Salient Features </h2>
                <p class="genareted">To build any type of report, Query, Analysis & Graphs</p>
                    <div class="clear"></div>
                    <p class="pic">&nbsp;</p>
                        <ul class="numberLink">
	                        <li><span>01</span>To enable dynamic, interactive analysis like calculations, grouping and filtering for end users</li>
	                        <li><span>02</span>To control who can view reports as well as the data within those reports. </li>
	                        <li><span>03</span>For end users to select, view, compare and visualize the data most meaningful to them.</li>
	                        <li><span>04</span>Make effective use of the graphical presentation of your data with powerful mapping capabilities </li>
	                        <li><span>05</span>able to provide various analysis base data tables and views. </li>
	                    </ul>
            </div>
<!--left panel -->
       </div>
        <div class="grid_10 omega" style="text-align:justify;">
            <h2>Know about Us</h2>
                <p class="vero">Provide at-a-glance insight about your organization’s performance for your end users.</p>
                <p class="sedTxt">QlickReport is powerful role-based web application which gives dashboard view of key business data. It is an evolving business intelligence MIS & reporting software provides easy and enhanced MIS reporting, data analysis and customized email-alerts. <br>
                <p style="height:5px;"></p>
                <span style="width:60px;">An integrated powerful graphical reporting tool has made charting easy. Users can schedule business<br /> alerts that inform the right people of critical occurrences when time is of the essence.</span>
                <%--<a href="Content/PDF/CP_Qlickreport.pdf" class="moreaTwo1" target="_blank" title="Corporate Presentation">Corporate Presentation</a>--%>
                <span style="height:15px;"><p style="height:20px;"></p></span>
                
            <div class="grid_5 alpha ">
                <h2>Our Qlick Products </h2>
                    <p class="loremtxt">Create Report with ease and convenience with any of the following tools with different abilities and features and export your data to excel.</p>
                    <a href="Content/QlickQueryBuilder.aspx"><p class="manyTwo"><strong><u>Qlick Query Builder</u></strong> - helps any user to create, share, save as, save queries from multiple tables and views. </p></a>
                    <a href="Content/QlickReportDesigner.aspx"><p class="manyTwo"><strong><u>Qlick Report Designer</u></strong> - Header and Footer, Data in specified format, Functions by SQL statement, Summarized output in grouping.</p></a>
                    <a href="Content/QlickDataAnalysis.aspx"><p class="manyTwo"><strong><u>Qlick Data Analysis</u></strong> - Is able to do various analysis on data tables and views. User is able to save & share the analysis.</p></a>
                    <a href="Content/QlickGraph.aspx"><p class="manyTwo"><strong><u>Qlick Graph</u></strong> - The graph of data is based on specification given by creator at the time of creating/edit report/query/analysis etc.</p></a>

            </div>
            <div class="grid_5 omega">
                <div class="company">
                    <h2>Other Qlick Features</h2>
                    <p class="loremtxt">Available with single or multiple user licence with User, Hierarchy and Rights management with multiple Admin & SuperAdmin</p>
                    <a href=""><p class="manyTwo" style="width:220px;"><strong><u>Qlick Database</u></strong> <br>- Single (MSSQL, MySql & Oracle)<br>- Multiple (Two or More)</p></a>
                    <a href=""><p class="manyTwo" style="width:220px;"><strong><u>Qlick Data Transfer</u></strong> <br>- Import<br>- Real Time</p></a>
                    <a href=""><p class="manyTwo" style="width:220px;"><strong><u>Qlick Email Scheduler</u></strong> <br>- Event Based<br>- Condition Based</p></a>
                    <a href=""><p class="manyTwo" style="width:220px;"><strong><u>Qlick Sharing</u></strong> <br>- Local<br>- Non Local</p></a>
                    <!--a href="#" title="more" class="manyMore">more...</a--></p>
                </div>
            </div>
            <div class="clear"></div>
            <div class="block">
                <p class="baner">Technical support<span>Our tech experts will provide you best technical support </span></p>
                <a href="Content/QlickSupport.aspx" title="Details" class="details">Details</a>
            </div>
        </div>
    </div>
</div>

<!--body -->
<!--footer -->
<div class="container_16">
    <div class="grid_16" id="footer">
        <ul class="footerlink">
			<li><a href=Default.aspx title="Home">Home</li><li>|</li>
	        <li><a href="Content/Aboutus.aspx" title="About">Company</a></li><li>|</li>
	        <li><a href="Content/Product.aspx" title="Product">Products</a></li><li>|</li>
			<li><a href="Content/QlickSupport.aspx" title="Solution">Support</a></li><li>|</li>
			<li><a href="Content/QlickLicense.aspx" title="Solution">Qlick License</a></li><li>|</li>
			<li><a href="Content/ContactUs.aspx" title="Contact">Contact</a></li>	
	    </ul>
		<p class="clear">&nbsp;</p>
		<p class="copyright">© Copyright Information Goes Here. All Rights Reserved.
		<span>Designed By : <a href="http://www.bmprojects.co.in" target="_blank" title="bmprojects.co.in">B M Project Engineers Pvt. Ltd.</a></span></p>
    </div>
</div>
<!--footer -->
</body>
</html>
