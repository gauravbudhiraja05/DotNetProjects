<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Aboutus.aspx.vb" Inherits="Content_Aboutus" %>
<%@ Register src="~/Content/WebControl/Navigation.ascx" tagname="Navigation" tagprefix="uc1" %>
<%@ Register Src="~/Content/WebControl/TopBanner.ascx" TagName="Top" TagPrefix="uc2" %>
<%@ Register Src="~/Content/WebControl/LeftPanel.ascx" TagName="Left" TagPrefix="uc3" %>
<%@ Register Src="~/Content/WebControl/FooterBanner.ascx" TagName="Footer" TagPrefix="uc4" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1" />
<title>QlickReport Software combines your Financial and Operational data into a single, seamless source of information for better business decisions</title>
<link href="../css/base.css" rel="stylesheet" type="text/css" />
</head>
<body style="background-image:url('images/fadestrip2.png'); background-position:top; background-repeat:repeat-x; background-attachment: fixed;">
<!--navigation -->

<uc1:Navigation ID="Navigation" runat="server" />

<!--navigation -->
<!--header -->

<uc2:Top ID="Top" runat="server" />

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
            <h2>Know about Us</h2>
            <h2 style="height:10px;"></h2>
            <span style="text-align:justify;"><b>B.M. Project Engineers Private Limited (BMPEPL)</b> was established in 1992 and has over the years evolved into a Total Information Technology Solution provider. Headquartered in Gurgaon, India it covers the Middle East, Europe and the Indian subcontinent.</span><br /><br />
            <span>With over a decade of experience in providing technology solutions to organizations in diversity of verticals, and to the government sectors, we are focused on delivering high-quality products and services in many domains including technology solutions implementation, customized application development, hardware infrastructure, product deployment & training, complete network solutions and IT consultancy.  </span><br /><br />
            <span>Our vast experience ensures that all our solutions benefit from adopting the best practices approach, to deliver a distinct edge to our customers in today's competitive and technology driven business scenario. We understand that businesses must exploit the opportunities offered by evolving technologies to stay on top. With this focus on empowering businesses with technology, we've delivered solutions to a diversity of implementation environments. </span><br /><br />
            <span>With a strong team of highly qualified and skilled personnel, we stay at the leading edge of technology by evolving with the market trends, adopting new technologies and setting high standards to benefit our customer. </span><br /><br />
            <span><b>BMPEPL</b> has developed and implemented Web & Client Server based application software products, supplied & implemented third party network operating system products and undertaken several software consultancy, development, and maintenance & support services projects.  </span><br /><br />
            <span>With the tremendous changes in the Information Technology Industry, BMPEPL has become increasingly aware of the strategic initiative required to reduce end user costs without diluting its service standards. BMPEPL therefore understands the need to achieve an affordable balance in the relationship between end users and itself as the solutions provider.</span><br /><br />
            <span>Businesses today demands constant optimization i.e. minimization of inputs and maximization of output. This in turn demands constant analysis of data’s (both static and dynamic) and proper reporting with much ease and fast and particularly without any software development effort. <b>Qlickreport.com is an effort in that direction.</b> </span><br /><br />
            <p class="sedTxtlink"><a href="PDF/CP_Qlickreport.pdf" target="_blank" style="color:#0000CC;">Corporate Presentation</a></p>
            
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
