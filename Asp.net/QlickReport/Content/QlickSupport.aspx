<%@ Page Language="VB" AutoEventWireup="false" CodeFile="QlickSupport.aspx.vb" Inherits="Content_QlickSupport" %>
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
            <h2>Customer Support</h2>
            <h2 style="height:10px;"></h2>
            <div class="clear"></div>
            <div class="block">
                <p class="baner">Technical support<span>Our tech experts will provide you best technical support </span></p>
                <%--<a href="Content/QlickSupport.aspx" title="Details" class="details">Details</a>--%>
            </div>
            <h2 style="height:10px;"></h2>
            <span style="text-align:justify;">
                    On line Customer MIS will be the default communication through a tool made available to all the customers for communicating any problem, Change requests, New developments to the service provider. It should be noted that service provider will be only responsible for meeting the customer requirements as per the requests, bugs as logged in the MIS Tool in timely and efficient manner. Any verbal, telephonic or email requests will not be entertained unless backed by logging of the requirement in the MIS Tool. 
                    <br /><br />   
                    a.	Call Centre Support: Telephonic support, online support will be provided during the normal office timing of 9 am to 6pm.On some rare occasions call centre support after office hours can be provided subject to customer makes the request in writing and communicate the same by email to the service provider.<br /><br />
                    b.	Onsite Support:  Based on the onsite support needs, service provider will assign the resources with the required skill set to provide the necessary time bound onsite support. The ongoing onsite support and handholding services will be provided by the IT Consultants on chargeable basis. <br /><br />
                    c.	Changes and Software Error Management: Incorporation of authorized changes and bugs will be promptly attended to as per the response time listed in next point. <br /><br />
                    d.	Response Time : We should define the priority level in the MIS Tool in form of Low, Medium, High and Critical. Remote assistance shall be provided in-line with the following timescales dependent on the priority of the support request<br /><br />
            </span>
            <span style="text-align:justify; font-weight:bold;">
                    <table>
                        <tr>
                            <td>
                                <ul>
                                    <li>•	Run time errors correction - 3 working hours </li>
                                    <li>•	Incorporation of minor changes in input screens / output reports -3 working days </li>
                                    <li>•	Change request - 7 to 10 working days </li>
                                    <li>•	Development of new software module including user testing - 3 months </li>
                                </ul>
                            </td>
                        </tr>
                    </table>
                    
            </span>
            <span style="text-align:justify;">
                    e.	Out-of-Scope Services : Following are the irritants which are to be very effectively looked after by the Customer by separate specialists (resources)<br /><br />
            </span>
            <span style="text-align:justify; font-weight:bold;">
                    <table>
                        <tr>
                            <td>
                                <ul>
                                    <li>•	Operating Systems support </li>
                                    <li>•	Third party systems which are not integrated with Qlick.</li>
                                    <li>•	Network Support for connectivity around Qlick.</li>
                                    <li>•	Hardware Maintenance and support activities.</li>
                                </ul>
                            </td>
                        </tr>
                    </table>
                    
            </span>
                    With the above Service framework in place we will be able to ensure <b>GOOD DELIVERY.</b>
            </span>
            
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
