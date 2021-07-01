<%@ Page Language="VB" AutoEventWireup="false" CodeFile="QlickLicense.aspx.vb" Inherits="Content_QlickLicense" %>
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
    <script language="javascript" type="text/javascript">
// <![CDATA[

     

// ]]>
    </script>
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
            <%--<uc3:Left ID="Left" runat="server" />--%>
            <div id="lftPan" style="background-color:White; margin-left:10px; margin-right:10px; border-bottom-color:Black;">
                <form id="form1" runat="server">
                <asp:Label runat="server" ID="Label1" Visible="false" ForeColor="Red"></asp:Label><asp:Label runat="server" ID="lbldays" Visible="false" ForeColor="Red"></asp:Label>
                    <p class="member">Member login</p>
                    <label style="width:80px;">Username :</label>
                    <input id="txtuserid" name="txtuserid" style="width:183px;" type="text" class="txtfield" runat="server"/>
                    <label style="width:80px;">Password :</label>
                    <input id="txtpassword" name="txtpassword" style="width:183px;" type="password" class="txtfield" runat="server"/>
                    <p class="forgot"><a href="Misc/ForgotPassword.aspx">Forgot Password?</a></p>
                    <%--<input name="" type="submit" class="login" value="" title="login"/>--%>
                    <asp:Button ID="Button1" Text="Login" CssClass="login" runat="server" />
                    <%--<input id="Login" name="" type="button" class="login" value="" title="login" onclick="return Login_onclick()" />--%>
                    <div class="clear"></div>
            </div>
            <div id="Div1">    
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
        <div class="grid_10 omega" style="width:550px;text-align:justify;">
            <!--Detail Content panel START-->
            <h2>Qlick License</h2>
            <h2 style="height:10px;"></h2>
            <div style="text-align:justify;">
           
                <table style="width:350px;">
                    <tr>
                        <td><asp:Label runat="server" ID="lblmsg" ForeColor="Red"></asp:Label></td>
                        <td align="right"><asp:Label runat="server" ID="lblmsg1" ForeColor="Red"></asp:Label></td>
                    </tr>
                    <tr>
                        <td> Select Product Type</td>
                        <td id="td1" runat="server" align="right"> Select Database Type</td>        
                    </tr>
                    <tr>
                        <td runat="server">
                            <asp:DropDownList runat="server" id="DropDownList1">
                                <asp:ListItem Selected="True">--Select Product--</asp:ListItem>
                                <asp:ListItem>Single User</asp:ListItem>
                                <asp:ListItem>Multiple User</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td id="td2" runat="server" align="right">
                            <asp:DropDownList runat="server" id="drop1">
                                <asp:ListItem Selected="True">--Select Database--</asp:ListItem>
                                <asp:ListItem>Single Database</asp:ListItem>
                                <asp:ListItem>Multiple Database</asp:ListItem>
                            </asp:DropDownList>
                        </td>        
                    </tr>
                    <tr>
                        <td style="height:20px"></td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <table style="width:390px;">
                                <tr>
                                    <td>
                                        <asp:Label runat="server" ID="lblmsg2" ForeColor="Red"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                <asp:CheckBoxList ID="CheckBoxList1" runat="server" 
                                RepeatDirection="Horizontal">
                                <asp:ListItem Value ="0">Excel</asp:ListItem>
                                <asp:ListItem Value ="1" >MS-SQL</asp:ListItem>
                                <asp:ListItem Value ="3">Oracle</asp:ListItem>
                                <asp:ListItem Value ="2" >MySQL</asp:ListItem>
                                <asp:ListItem Value ="4" >PostgreSQL</asp:ListItem>
                            </asp:CheckBoxList>
                                    <%--<td style="width:20px;"><asp:CheckBox runat="server" ID="CheckBox1"/></td>
                                    <td style="width:120px;" valign="top">Excel</td>
                                    <td style="width:20px;"><asp:CheckBox runat="server" ID="CheckBox2" /></td>
                                    <td  style="width:120px;"valign="top">MS-SQL</td>
                                    <td style="width:20px;"> <asp:CheckBox runat="server" ID="CheckBox3" /></td>
                                    <td style="width:120px;" valign="top">MySQL</td>
                                </tr>
                                <tr>
                                    <td> <asp:CheckBox runat="server" ID="CheckBox4" /></td>
                                    <td valign="top">Oracle</td>
                                    <td><asp:CheckBox runat="server" ID="CheckBox5" /></td>
                                    <td valign="top">PostgreSQL</td>--%>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="height:2px"></td>
                    </tr>
                     <tr>
                        <td colspan="2">
                            <asp:Button runat="server" ID="b1" Text="Submit" />
                        </td>
                    </tr>
                </table>
                 <table style="width:570px;" cellpadding="1" cellspacing="1" id="SinUserSingDBExc" runat="server">
                    <tr>
                        <td style="font-family:Verdana; font-size:14px; color:#0000CC;" colspan="5"><b>Single User License With Single Database (Excel)base (Excel)</b></td>
                    </tr>
                    <tr><td style="height:5px;"></td></tr>
                    <tr>
                        <td style="font-family:Verdana; font-size:11px; color:Black; background-color:#cccccc; height:25px;" valign="middle">Basic Product (BP)</td>
                        <td style="font-family:Verdana; font-size:11px; color:Black; background-color:#cccccc; height:25px;" valign="middle">BP+Data Analysis (DA)</td>
                        <td style="font-family:Verdana; font-size:11px; color:Black; background-color:#cccccc; height:25px;" valign="middle">BP+DA+Graph (GP)</td>
                       <%-- <td style="font-family:Verdana; font-size:11px; color:Black; background-color:#cccccc; height:25px;" valign="middle">BP+DA+GP+Email Alert (EA)</td>--%>
                    </tr>
                    <tr><td style="height:10px;"></td></tr>
                    <tr>
                        <td style="font-family:Verdana; font-size:11px; color:Black;">Query Builder <br />(Code -QBSUSDExc)<br /><font color="#0000cc"><b>Price : 25$</b></font><br /><b><%--<a href="Registration.aspx?itemcode=QBSDExc" id="A0" runat="server">Demo</a>--%><asp:LinkButton ID="lb1" Text="Demo" CommandArgument="QBSUSDExc"  OnCommand="LinkButton_Command" runat="server"></asp:LinkButton>   <br />Buy Now</b></td>
                        <td style="font-family:Verdana; font-size:11px; color:Black;">QB+Data Analysis<br />(Code -QBSUSDExcDA)<br /><font color="#0000cc"><b>Price : 75$</b></font><br /><b><asp:LinkButton ID="LinkButton1" Text="Demo" CommandArgument="QBSUSDExcDA"  OnCommand="LinkButton_Command" runat="server"></asp:LinkButton> <br />Buy Now</b></td>
                        <td style="font-family:Verdana; font-size:11px; color:Black;">QB+DA+Graph<br />(Code -QBSUSDExcDAGP)<br /><font color="#0000cc"><b>Price : 125$</b></font><br /><b><asp:LinkButton ID="LinkButton2" Text="Demo" CommandArgument="QBSUSDExcDAGP"  OnCommand="LinkButton_Command" runat="server"></asp:LinkButton> <br />Buy Now</b></td>
                       <%-- <td style="font-family:Verdana; font-size:11px; color:Black;">QB+DA+Graph+Email Alert<br />(Code -QBSUSDExcDAGPEA)<br /><font color="#0000cc"><b>Price : 175$</b></font><br /><b><asp:LinkButton ID="LinkButton3" Text="Demo" CommandArgument="QBSUSDExcDAGPEA"  OnCommand="LinkButton_Command" runat="server"></asp:LinkButton> <br />Buy Now</b></td>--%>
                    </tr>
                     <tr><td style="height:10px;"></td></tr>
                     <tr><td style="height:10px;font-family:Verdana; font-size:14px; color:Black;" colspan="4">or</td></tr>
                     <tr><td style="height:10px;"></td></tr>
                    <tr>
                        <td style="font-family:Verdana; font-size:11px; color:Black;">Report Designer <br />(Code -RDSUSDExc)<br /><font color="#0000cc"><b>Price : 50$</b></font><br /><b><asp:LinkButton ID="LinkButton4" Text="Demo" CommandArgument="RDSUSDExc"  OnCommand="LinkButton_Command" runat="server"></asp:LinkButton> <br />Buy Now</b></td>
                        <td style="font-family:Verdana; font-size:11px; color:Black;">RD+Data Analysis<br />(Code -RDSUSDExcDA)<br /><font color="#0000cc"><b>Price : 100$</b></font><br /><b><asp:LinkButton ID="LinkButton5" Text="Demo" CommandArgument="RDSUSDExcDA"  OnCommand="LinkButton_Command" runat="server"></asp:LinkButton> <br />Buy Now</b></td>
                        <td style="font-family:Verdana; font-size:11px; color:Black;">RD+DA+ Graph<br />(Code -RDSUSDExcDAGP)<br /><font color="#0000cc"><b>Price : 150$</b></font><br /><b><asp:LinkButton ID="LinkButton6" Text="Demo" CommandArgument="RDSUSDExcDAGP"  OnCommand="LinkButton_Command" runat="server"></asp:LinkButton> <br />Buy Now</b></td>
                        <%--<td style="font-family:Verdana; font-size:11px; color:Black;">RD+DA+ Graph + Email<br />(Code -RDSUSDExcDAGPEA)<br /><font color="#0000cc"><b>Price : 200$</b></font><br /><b><asp:LinkButton ID="LinkButton7" Text="Demo" CommandArgument="RDSUSDExcDAGPEA"  OnCommand="LinkButton_Command" runat="server"></asp:LinkButton> <br />Buy Now</b></td>--%>
                    </tr>
                </table>
                <table style="width:570px;" cellpadding="1" cellspacing="1" id="SinUserSingDBSql" runat="server">
                    <tr>
                        <td style="font-family:Verdana; font-size:14px; color:#0000CC;" colspan="5"><b>Single User License With Single Database (MS-SQL)</b></td>
                    </tr>
                    <tr><td style="height:5px;"></td></tr>
                    <tr>
                        <td style="font-family:Verdana; font-size:11px; color:Black; background-color:#cccccc; height:25px;" valign="middle">Basic Product (BP)</td>
                        <td style="font-family:Verdana; font-size:11px; color:Black; background-color:#cccccc; height:25px;" valign="middle">BP+Data Analysis (DA)</td>
                        <td style="font-family:Verdana; font-size:11px; color:Black; background-color:#cccccc; height:25px;" valign="middle">BP+DA+Graph (GP)</td>
                       <%-- <td style="font-family:Verdana; font-size:11px; color:Black; background-color:#cccccc; height:25px;" valign="middle">BP+DA+GP+Email Alert (EA)</td>--%>
                    </tr>
                    <tr><td style="height:10px;"></td></tr>
                    <tr>
                        <td style="font-family:Verdana; font-size:11px; color:Black;">Query Builder <br />(Code -QBSUSDSql)<br /><font color="#0000cc"><b>Price : 25$</b></font><br /><b><asp:LinkButton ID="LinkButton8" Text="Demo" CommandArgument="QBSUSDSql"  OnCommand="LinkButton_Command" runat="server"></asp:LinkButton> <br />Buy Now</b></td>
                        <td style="font-family:Verdana; font-size:11px; color:Black;">QB+Data Analysis<br />(Code -QBSUSDSqlDA)<br /><font color="#0000cc"><b>Price : 75$</b></font><br /><b><asp:LinkButton ID="LinkButton9" Text="Demo" CommandArgument="QBSUSDSqlDA"  OnCommand="LinkButton_Command" runat="server"></asp:LinkButton> <br />Buy Now</b></td>
                        <td style="font-family:Verdana; font-size:11px; color:Black;">QB+DA+Graph<br />(Code -QBSUSDSqlDAGP)<br /><font color="#0000cc"><b>Price : 125$</b></font><br /><b><asp:LinkButton ID="LinkButton10" Text="Demo" CommandArgument="QBSUSDSqlDAGP"  OnCommand="LinkButton_Command" runat="server"></asp:LinkButton> <br />Buy Now</b></td>
                        <%--<td style="font-family:Verdana; font-size:11px; color:Black;">QB+DA+Graph+Email Alert<br />(Code -QBSUSDSqlDAGPEA)<br /><font color="#0000cc"><b>Price : 175$</b></font><br /><b><asp:LinkButton ID="LinkButton11" Text="Demo" CommandArgument="QBSUSDSqlDAGPEA"  OnCommand="LinkButton_Command" runat="server"></asp:LinkButton> <br />Buy Now</b></td>--%>
                    </tr>
                     <tr><td style="height:10px;"></td></tr>
                     <tr><td style="height:10px;font-family:Verdana; font-size:14px; color:Black;" colspan="4">or</td></tr>
                     <tr><td style="height:10px;"></td></tr>
                    <tr>
                        <td style="font-family:Verdana; font-size:11px; color:Black;">Report Designer <br />(Code -RDSUSDSql)<br /><font color="#0000cc"><b>Price : 50$</b></font><br /><b><asp:LinkButton ID="LinkButton12" Text="Demo" CommandArgument="RDSUSDSql"  OnCommand="LinkButton_Command" runat="server"></asp:LinkButton> <br />Buy Now</b></td>
                        <td style="font-family:Verdana; font-size:11px; color:Black;">RD+Data Analysis<br />(Code -RDSUSDSqlDA)<br /><font color="#0000cc"><b>Price : 100$</b></font><br /><b><asp:LinkButton ID="LinkButton13" Text="Demo" CommandArgument="RDSUSDSqlDA"  OnCommand="LinkButton_Command" runat="server"></asp:LinkButton> <br />Buy Now</b></td>
                        <td style="font-family:Verdana; font-size:11px; color:Black;">RD+DA+ Graph<br />(Code -RDSUSDSqlDAGP)<br /><font color="#0000cc"><b>Price : 150$</b></font><br /><b><asp:LinkButton ID="LinkButton14" Text="Demo" CommandArgument="RDSUSDSqlDAGP"  OnCommand="LinkButton_Command" runat="server"></asp:LinkButton> <br />Buy Now</b></td>
                        <%--<td style="font-family:Verdana; font-size:11px; color:Black;">RD+DA+ Graph + Email<br />(Code -RDSUSDSqlDAGPEA)<br /><font color="#0000cc"><b>Price : 200$</b></font><br /><b><asp:LinkButton ID="LinkButton15" Text="Demo" CommandArgument="RDSUSDSqlDAGPEA"  OnCommand="LinkButton_Command" runat="server"></asp:LinkButton> <br />Buy Now</b></td>--%>
                    </tr>
                </table>
                <table style="width:570px;" cellpadding="1" cellspacing="1" id="SinUserSingDBMySql" runat="server">
                    <tr>
                        <td style="font-family:Verdana; font-size:14px; color:#0000CC;" colspan="5"><b>Single User License With Single Database (MySQL)</b></td>
                    </tr>
                    <tr><td style="height:5px;"></td></tr>
                    <tr>
                        <td style="font-family:Verdana; font-size:11px; color:Black; background-color:#cccccc; height:25px;" valign="middle">Basic Product (BP)</td>
                        <td style="font-family:Verdana; font-size:11px; color:Black; background-color:#cccccc; height:25px;" valign="middle">BP+Data Analysis (DA)</td>
                        <td style="font-family:Verdana; font-size:11px; color:Black; background-color:#cccccc; height:25px;" valign="middle">BP+DA+Graph (GP)</td>
                       <%-- <td style="font-family:Verdana; font-size:11px; color:Black; background-color:#cccccc; height:25px;" valign="middle">BP+DA+GP+Email Alert (EA)</td>--%>
                    </tr>
                    <tr><td style="height:10px;"></td></tr>
                    <tr>
                        <td style="font-family:Verdana; font-size:11px; color:Black;">Query Builder <br />(Code -QBSUSDMySql)<br /><font color="#0000cc"><b>Price : 25$</b></font><br /><b><asp:LinkButton ID="LinkButton16" Text="Demo" CommandArgument="QBSUSDMySql"  OnCommand="LinkButton_Command" runat="server"></asp:LinkButton> <br />Buy Now</b></td>
                        <td style="font-family:Verdana; font-size:11px; color:Black;">QB+Data Analysis<br />(Code -QBSUSDMySqlDA)<br /><font color="#0000cc"><b>Price : 75$</b></font><br /><b><asp:LinkButton ID="LinkButton17" Text="Demo" CommandArgument="QBSUSDMySqlDA"  OnCommand="LinkButton_Command" runat="server"></asp:LinkButton> <br />Buy Now</b></td>
                        <td style="font-family:Verdana; font-size:11px; color:Black;">QB+DA+Graph<br />(Code -QBSUSDMySqlDAGP)<br /><font color="#0000cc"><b>Price : 125$</b></font><br /><b><asp:LinkButton ID="LinkButton18" Text="Demo" CommandArgument="QBSUSDMySqlDAGP"  OnCommand="LinkButton_Command" runat="server"></asp:LinkButton> <br />Buy Now</b></td>
                        <%--<td style="font-family:Verdana; font-size:11px; color:Black;">QB+DA+Graph+Email Alert<br />(Code -QBSUSDMySqlDAGPEA)<br /><font color="#0000cc"><b>Price : 175$</b></font><br /><b><asp:LinkButton ID="LinkButton19" Text="Demo" CommandArgument="QBSUSDMySqlDAGPEA"  OnCommand="LinkButton_Command" runat="server"></asp:LinkButton> <br />Buy Now</b></td>--%>
                    </tr>
                     <tr><td style="height:10px;"></td></tr>
                     <tr><td style="height:10px;font-family:Verdana; font-size:14px; color:Black;" colspan="4">or</td></tr>
                     <tr><td style="height:10px;"></td></tr>
                    <tr>
                        <td style="font-family:Verdana; font-size:11px; color:Black;">Report Designer <br />(Code -RDSUSDMySql)<br /><font color="#0000cc"><b>Price : 50$</b></font><br /><b><asp:LinkButton ID="LinkButton20" Text="Demo" CommandArgument="RDSUSDMySql"  OnCommand="LinkButton_Command" runat="server"></asp:LinkButton> <br />Buy Now</b></td>
                        <td style="font-family:Verdana; font-size:11px; color:Black;">RD+Data Analysis<br />(Code -RDSUSDMySqlDA)<br /><font color="#0000cc"><b>Price : 100$</b></font><br /><b><asp:LinkButton ID="LinkButton21" Text="Demo" CommandArgument="RDSUSDMySqlDA"  OnCommand="LinkButton_Command" runat="server"></asp:LinkButton> <br />Buy Now</b></td>
                        <td style="font-family:Verdana; font-size:11px; color:Black;">RD+DA+ Graph<br />(Code -RDSUSDMySqlDAGP)<br /><font color="#0000cc"><b>Price : 150$</b></font><br /><b><asp:LinkButton ID="LinkButton22" Text="Demo" CommandArgument="RDSUSDMySqlDAGP"  OnCommand="LinkButton_Command" runat="server"></asp:LinkButton> <br />Buy Now</b></td>
                        <%--<td style="font-family:Verdana; font-size:11px; color:Black;">RD+DA+ Graph + Email<br />(Code -RDSUSDMySqlDAGPEA)<br /><font color="#0000cc"><b>Price : 200$</b></font><br /><b><asp:LinkButton ID="LinkButton23" Text="Demo" CommandArgument="RDSUSDMySqlDAGPEA"  OnCommand="LinkButton_Command" runat="server"></asp:LinkButton> <br />Buy Now</b></td>--%>
                    </tr>
                </table>
                <table style="width:570px;" cellpadding="1" cellspacing="1" id="SinUserSingDBOra" runat="server">
                    <tr>
                        <td style="font-family:Verdana; font-size:14px; color:#0000CC;" colspan="5"><b>Single User License With Single Database (Oracle)</b></td>
                    </tr>
                    <tr><td style="height:5px;"></td></tr>
                    <tr>
                        <td style="font-family:Verdana; font-size:11px; color:Black; background-color:#cccccc; height:25px;" valign="middle">Basic Product (BP)</td>
                        <td style="font-family:Verdana; font-size:11px; color:Black; background-color:#cccccc; height:25px;" valign="middle">BP+Data Analysis (DA)</td>
                        <td style="font-family:Verdana; font-size:11px; color:Black; background-color:#cccccc; height:25px;" valign="middle">BP+DA+Graph (GP)</td>
                        <%--<td style="font-family:Verdana; font-size:11px; color:Black; background-color:#cccccc; height:25px;" valign="middle">BP+DA+GP+Email Alert (EA)</td>--%>
                    </tr>
                    <tr><td style="height:10px;"></td></tr>
                    <tr>
                        <td style="font-family:Verdana; font-size:11px; color:Black;">Query Builder <br />(Code -QBSUSDOra)<br /><font color="#0000cc"><b>Price : 25$</b></font><br /><b><asp:LinkButton ID="LinkButton24" Text="Demo" CommandArgument="QBSUSDOra"  OnCommand="LinkButton_Command" runat="server"></asp:LinkButton> <br />Buy Now</b></td>
                        <td style="font-family:Verdana; font-size:11px; color:Black;">QB+Data Analysis<br />(Code -QBSUSDOraDA)<br /><font color="#0000cc"><b>Price : 75$</b></font><br /><b><asp:LinkButton ID="LinkButton25" Text="Demo" CommandArgument="QBSUSDOraDA"  OnCommand="LinkButton_Command" runat="server"></asp:LinkButton> <br />Buy Now</b></td>
                        <td style="font-family:Verdana; font-size:11px; color:Black;">QB+DA+Graph<br />(Code -QBSUSDOraDAGP)<br /><font color="#0000cc"><b>Price : 125$</b></font><br /><b><asp:LinkButton ID="LinkButton26" Text="Demo" CommandArgument="QBSUSDOraDAGP"  OnCommand="LinkButton_Command" runat="server"></asp:LinkButton> <br />Buy Now</b></td>
                        <%--<td style="font-family:Verdana; font-size:11px; color:Black;">QB+DA+Graph+Email Alert<br />(Code -QBSUSDOraDAGPEA)<br /><font color="#0000cc"><b>Price : 175$</b></font><br /><b><asp:LinkButton ID="LinkButton27" Text="Demo" CommandArgument="QBSUSDOraDAGPEA"  OnCommand="LinkButton_Command" runat="server"></asp:LinkButton> <br />Buy Now</b></td>--%>
                    </tr>
                     <tr><td style="height:10px;"></td></tr>
                     <tr><td style="height:10px;font-family:Verdana; font-size:14px; color:Black;" colspan="4">or</td></tr>
                     <tr><td style="height:10px;"></td></tr>
                    <tr>
                        <td style="font-family:Verdana; font-size:11px; color:Black;">Report Designer <br />(Code -RDSUSDOra)<br /><font color="#0000cc"><b>Price : 50$</b></font><br /><b><asp:LinkButton ID="LinkButton28" Text="Demo" CommandArgument="RDSUSDOra"  OnCommand="LinkButton_Command" runat="server"></asp:LinkButton> <br />Buy Now</b></td>
                        <td style="font-family:Verdana; font-size:11px; color:Black;">RD+Data Analysis<br />(Code -RDSUSDOraDA)<br /><font color="#0000cc"><b>Price : 100$</b></font><br /><b><asp:LinkButton ID="LinkButton29" Text="Demo" CommandArgument="RDSUSDOraDA"  OnCommand="LinkButton_Command" runat="server"></asp:LinkButton> <br />Buy Now</b></td>
                        <td style="font-family:Verdana; font-size:11px; color:Black;">RD+DA+ Graph<br />(Code -RDSUSDOraDAGP)<br /><font color="#0000cc"><b>Price : 150$</b></font><br /><b><asp:LinkButton ID="LinkButton30" Text="Demo" CommandArgument="RDSUSDOraDAGP"  OnCommand="LinkButton_Command" runat="server"></asp:LinkButton> <br />Buy Now</b></td>
                        <%--<td style="font-family:Verdana; font-size:11px; color:Black;">RD+DA+ Graph + Email<br />(Code -RDSUSDOraDAGPEA)<br /><font color="#0000cc"><b>Price : 200$</b></font><br /><b><asp:LinkButton ID="LinkButton31" Text="Demo" CommandArgument="RDSUSDOraDAGPEA"  OnCommand="LinkButton_Command" runat="server"></asp:LinkButton> <br />Buy Now</b></td>--%>
                    </tr>
                </table>
                <table style="width:570px;" cellpadding="1" cellspacing="1" id="SinUserSingDBPogr" runat="server">
                    <tr>
                        <td style="font-family:Verdana; font-size:14px; color:#0000CC;" colspan="5"><b>Single User License With Single Database (PostgreSQL)</b></td>
                    </tr>
                    <tr><td style="height:5px;"></td></tr>
                    <tr>
                        <td style="font-family:Verdana; font-size:11px; color:Black; background-color:#cccccc; height:25px;" valign="middle">Basic Product (BP)</td>
                        <td style="font-family:Verdana; font-size:11px; color:Black; background-color:#cccccc; height:25px;" valign="middle">BP+Data Analysis (DA)</td>
                        <td style="font-family:Verdana; font-size:11px; color:Black; background-color:#cccccc; height:25px;" valign="middle">BP+DA+Graph (GP)</td>
                       <%-- <td style="font-family:Verdana; font-size:11px; color:Black; background-color:#cccccc; height:25px;" valign="middle">BP+DA+GP+Email Alert (EA)</td>--%>
                    </tr>
                    <tr><td style="height:10px;"></td></tr>
                    <tr>
                        <td style="font-family:Verdana; font-size:11px; color:Black;">Query Builder <br />(Code -QBSUSDPoGre)<br /><font color="#0000cc"><b>Price : 25$</b></font><br /><b><asp:LinkButton ID="LinkButton32" Text="Demo" CommandArgument="QBSUSDPoGre"  OnCommand="LinkButton_Command" runat="server"></asp:LinkButton> <br />Buy Now</b></td>
                        <td style="font-family:Verdana; font-size:11px; color:Black;">QB+Data Analysis<br />(Code -QBSUSDPoGreDA)<br /><font color="#0000cc"><b>Price : 75$</b></font><br /><b><asp:LinkButton ID="LinkButton33" Text="Demo" CommandArgument="QBSUSDPoGreDA"  OnCommand="LinkButton_Command" runat="server"></asp:LinkButton> <br />Buy Now</b></td>
                        <td style="font-family:Verdana; font-size:11px; color:Black;">QB+DA+Graph<br />(Code -QBSUSDPoGreDAGP)<br /><font color="#0000cc"><b>Price : 125$</b></font><br /><b><asp:LinkButton ID="LinkButton34" Text="Demo" CommandArgument="QBSUSDPoGreDAGP"  OnCommand="LinkButton_Command" runat="server"></asp:LinkButton> <br />Buy Now</b></td>
                        <%--<td style="font-family:Verdana; font-size:11px; color:Black;">QB+DA+Graph+Email Alert<br />(Code -QBSUSDPoGreDAGPEA)<br /><font color="#0000cc"><b>Price : 175$</b></font><br /><b><asp:LinkButton ID="LinkButton35" Text="Demo" CommandArgument="QBSUSDPoGreDAGPEA"  OnCommand="LinkButton_Command" runat="server"></asp:LinkButton> <br />Buy Now</b></td>--%>
                    </tr>
                     <tr><td style="height:10px;"></td></tr>
                     <tr><td style="height:10px;font-family:Verdana; font-size:14px; color:Black;" colspan="4">or</td></tr>
                     <tr><td style="height:10px;"></td></tr>
                    <tr>
                        <td style="font-family:Verdana; font-size:11px; color:Black;">Report Designer <br />(Code -RDSUSDPoGre)<br /><font color="#0000cc"><b>Price : 50$</b></font><br /><b><asp:LinkButton ID="LinkButton36" Text="Demo" CommandArgument="RDSUSDPoGre"  OnCommand="LinkButton_Command" runat="server"></asp:LinkButton> <br />Buy Now</b></td>
                        <td style="font-family:Verdana; font-size:11px; color:Black;">RD+Data Analysis<br />(Code -RDSUSDPoGreDA)<br /><font color="#0000cc"><b>Price : 100$</b></font><br /><b><asp:LinkButton ID="LinkButton37" Text="Demo" CommandArgument="RDSUSDPoGreDA"  OnCommand="LinkButton_Command" runat="server"></asp:LinkButton> <br />Buy Now</b></td>
                        <td style="font-family:Verdana; font-size:11px; color:Black;">RD+DA+ Graph<br />(Code -RDSUSDPoGreDAGP)<br /><font color="#0000cc"><b>Price : 150$</b></font><br /><b><asp:LinkButton ID="LinkButton38" Text="Demo" CommandArgument="RDSUSDPoGreDAGP"  OnCommand="LinkButton_Command" runat="server"></asp:LinkButton> <br />Buy Now</b></td>
                        <%--<td style="font-family:Verdana; font-size:11px; color:Black;">RD+DA+ Graph + Email<br />(Code -RDSUSDPoGreDAGPEA)<br /><font color="#0000cc"><b>Price : 200$</b></font><br /><b><asp:LinkButton ID="LinkButton39" Text="Demo" CommandArgument="RDSUSDPoGreDAGPEA"  OnCommand="LinkButton_Command" runat="server"></asp:LinkButton> <br />Buy Now</b></td>--%>
                    </tr>
                </table>
                
                                <table style="width:570px;" cellpadding="1" cellspacing="1" id="SinUserMulDB" runat="server">
                    <tr>
                        <td style="font-family:Verdana; font-size:14px; color:#0000CC;" colspan="5"><b>Single User License With Multiple Database (With two or more of above)</b></td>
                    </tr>
                    <tr><td style="height:5px;"></td></tr>
                    <tr>
                        <td style="font-family:Verdana; font-size:11px; color:Black; background-color:#cccccc; height:25px;" valign="middle">Basic Product (BP)</td>
                        <td style="font-family:Verdana; font-size:11px; color:Black; background-color:#cccccc; height:25px;" valign="middle">BP+Data Analysis (DA)</td>
                        <td style="font-family:Verdana; font-size:11px; color:Black; background-color:#cccccc; height:25px;" valign="middle">BP+DA+Graph (GP)</td>
                        <%--<td style="font-family:Verdana; font-size:11px; color:Black; background-color:#cccccc; height:25px;" valign="middle">BP+DA+GP+Email Alert (EA)</td>--%>
                    </tr>
                    <tr><td style="height:10px;"></td></tr>
                    <tr>
                        <td style="font-family:Verdana; font-size:11px; color:Black;">Query Builder <br />(Code -QBSUMD)<br /><font color="#0000cc"><b>Price : 50$</b></font><br /><b><asp:LinkButton ID="LinkButton40" Text="Demo" CommandArgument="QBSUMD"  OnCommand="LinkButton_Command" runat="server"></asp:LinkButton> <br />Buy Now</b></td>
                        <td style="font-family:Verdana; font-size:11px; color:Black;">QB+Data Analysis<br />(Code -QBSUMDDA)<br /><font color="#0000cc"><b>Price : 100$</b></font><br /><b><asp:LinkButton ID="LinkButton41" Text="Demo" CommandArgument="QBSUMDDA"  OnCommand="LinkButton_Command" runat="server"></asp:LinkButton> <br />Buy Now</b></td>
                        <td style="font-family:Verdana; font-size:11px; color:Black;">QB+DA+Graph<br />(Code -QBSUMDDAGP)<br /><font color="#0000cc"><b>Price : 150$</b></font><br /><b><asp:LinkButton ID="LinkButton42" Text="Demo" CommandArgument="QBSUMDDAGP"  OnCommand="LinkButton_Command" runat="server"></asp:LinkButton> <br />Buy Now</b></td>
                        <%--<td style="font-family:Verdana; font-size:11px; color:Black;">QB+DA+Graph+Email Alert<br />(Code -QBSUMDDAGPEA)<br /><font color="#0000cc"><b>Price : 200$</b></font><br /><b><asp:LinkButton ID="LinkButton43" Text="Demo" CommandArgument="QBSUMDDAGPEA"  OnCommand="LinkButton_Command" runat="server"></asp:LinkButton> <br />Buy Now</b></td>--%>
                    </tr>
                     <tr><td style="height:10px;"></td></tr>
                     <tr><td style="height:10px;font-family:Verdana; font-size:14px; color:Black;" colspan="4">or</td></tr>
                     <tr><td style="height:10px;"></td></tr>
                    <tr>
                        <td style="font-family:Verdana; font-size:11px; color:Black;">Report Designer <br />(Code -RDSUMD)<br /><font color="#0000cc"><b>Price : 75$</b></font><br /><b><asp:LinkButton ID="LinkButton44" Text="Demo" CommandArgument="RDSUMD"  OnCommand="LinkButton_Command" runat="server"></asp:LinkButton> <br />Buy Now</b></td>
                        <td style="font-family:Verdana; font-size:11px; color:Black;">RD+Data Analysis<br />(Code -RDSUMDDA)<br /><font color="#0000cc"><b>Price : 125$</b></font><br /><b><asp:LinkButton ID="LinkButton45" Text="Demo" CommandArgument="RDSUMDDA"  OnCommand="LinkButton_Command" runat="server"></asp:LinkButton> <br />Buy Now</b></td>
                        <td style="font-family:Verdana; font-size:11px; color:Black;">RD+DA+ Graph<br />(Code -RDSUMDDAGP)<br /><font color="#0000cc"><b>Price : 175$</b></font><br /><b><asp:LinkButton ID="LinkButton46" Text="Demo" CommandArgument="RDSUMDDAGP"  OnCommand="LinkButton_Command" runat="server"></asp:LinkButton> <br />Buy Now</b></td>
                        <%--<td style="font-family:Verdana; font-size:11px; color:Black;">RD+DA+ Graph + Email<br />(Code -RDSUMDDAGPEA)<br /><font color="#0000cc"><b>Price : 225$</b></font><br /><b><asp:LinkButton ID="LinkButton47" Text="Demo" CommandArgument="RDSUMDDAGPEA"  OnCommand="LinkButton_Command" runat="server"></asp:LinkButton> <br />Buy Now</b></td>--%>
                    </tr>
                </table>


                <table style="width:570px;" cellpadding="1" cellspacing="1" id="MulUserSingDBExc" runat="server">
                    <tr>
                        <td style="font-family:Verdana; font-size:14px; color:#0000CC;" colspan="5"><b>Multiple User License With Single Database (Excel)</b></td>
                    </tr>
                    <tr><td style="height:5px;"></td></tr>
                    <tr>
                        <td style="font-family:Verdana; font-size:11px; color:Black; background-color:#cccccc; height:25px;" valign="middle">Basic Product (BP)</td>
                        <td style="font-family:Verdana; font-size:11px; color:Black; background-color:#cccccc; height:25px;" valign="middle">BP+Data Analysis (DA)</td>
                        <td style="font-family:Verdana; font-size:11px; color:Black; background-color:#cccccc; height:25px;" valign="middle">BP+DA+Graph (GP)</td>
                       <%-- <td style="font-family:Verdana; font-size:11px; color:Black; background-color:#cccccc; height:25px;" valign="middle">BP+DA+GP+Email Alert (EA)</td>--%>
                    </tr>
                    <tr><td style="height:10px;"></td></tr>
                    <tr>
                        <td style="font-family:Verdana; font-size:11px; color:Black;">Query Builder <br />(Code -QBMUSDExc)<br /><font color="#0000cc"><b>Price : 25$</b></font><br /><b><asp:LinkButton ID="LinkButton48" Text="Demo" CommandArgument="QBMUSDExc"  OnCommand="LinkButton_Command" runat="server"></asp:LinkButton> <br />Buy Now</b></td>
                        <td style="font-family:Verdana; font-size:11px; color:Black;">QB+Data Analysis<br />(Code -QBMUSDExcDA)<br /><font color="#0000cc"><b>Price : 75$</b></font><br /><b><asp:LinkButton ID="LinkButton49" Text="Demo" CommandArgument="QBMUSDExcDA"  OnCommand="LinkButton_Command" runat="server"></asp:LinkButton> <br />Buy Now</b></td>
                        <td style="font-family:Verdana; font-size:11px; color:Black;">QB+DA+Graph<br />(Code -QBMUSDExcDAGP)<br /><font color="#0000cc"><b>Price : 125$</b></font><br /><b><asp:LinkButton ID="LinkButton50" Text="Demo" CommandArgument="QBMUSDExcDAGP"  OnCommand="LinkButton_Command" runat="server"></asp:LinkButton> <br />Buy Now</b></td>
                        <%--<td style="font-family:Verdana; font-size:11px; color:Black;">QB+DA+Graph+Email Alert<br />(Code -QBMUSDExcDAGPEA)<br /><font color="#0000cc"><b>Price : 175$</b></font><br /><b><asp:LinkButton ID="LinkButton51" Text="Demo" CommandArgument="QBMUSDExcDAGPEA"  OnCommand="LinkButton_Command" runat="server"></asp:LinkButton> <br />Buy Now</b></td>--%>
                    </tr>
                     <tr><td style="height:10px;"></td></tr>
                     <tr><td style="height:10px;font-family:Verdana; font-size:14px; color:Black;" colspan="4">or</td></tr>
                     <tr><td style="height:10px;"></td></tr>
                    <tr>
                        <td style="font-family:Verdana; font-size:11px; color:Black;">Report Designer <br />(Code -RDMUSDExc)<br /><font color="#0000cc"><b>Price : 50$</b></font><br /><b><asp:LinkButton ID="LinkButton52" Text="Demo" CommandArgument="RDMUSDExc"  OnCommand="LinkButton_Command" runat="server"></asp:LinkButton> <br />Buy Now</b></td>
                        <td style="font-family:Verdana; font-size:11px; color:Black;">RD+Data Analysis<br />(Code -RDMUSDExcDA)<br /><font color="#0000cc"><b>Price : 100$</b></font><br /><b><asp:LinkButton ID="LinkButton53" Text="Demo" CommandArgument="RDMUSDExcDA"  OnCommand="LinkButton_Command" runat="server"></asp:LinkButton> <br />Buy Now</b></td>
                        <td style="font-family:Verdana; font-size:11px; color:Black;">RD+DA+ Graph<br />(Code -RDMUSDExcDAGP)<br /><font color="#0000cc"><b>Price : 150$</b></font><br /><b><asp:LinkButton ID="LinkButton54" Text="Demo" CommandArgument="RDMUSDExcDAGP"  OnCommand="LinkButton_Command" runat="server"></asp:LinkButton> <br />Buy Now</b></td>
                       <%-- <td style="font-family:Verdana; font-size:11px; color:Black;">RD+DA+ Graph + Email<br />(Code -RDMUSDExcDAGPEA)<br /><font color="#0000cc"><b>Price : 200$</b></font><br /><b><asp:LinkButton ID="LinkButton55" Text="Demo" CommandArgument="RDMUSDExcDAGPEA"  OnCommand="LinkButton_Command" runat="server"></asp:LinkButton> <br />Buy Now</b></td>--%>
                    </tr>
                </table>
                <table style="width:570px;" cellpadding="1" cellspacing="1" id="MulUserSingDBSQL" runat="server">
                    <tr>
                        <td style="font-family:Verdana; font-size:14px; color:#0000CC;" colspan="5"><b>Multiple User License With Single Database (MS-SQL)</b></td>
                    </tr>
                    <tr><td style="height:5px;"></td></tr>
                    <tr>
                        <td style="font-family:Verdana; font-size:11px; color:Black; background-color:#cccccc; height:25px;" valign="middle">Basic Product (BP)</td>
                        <td style="font-family:Verdana; font-size:11px; color:Black; background-color:#cccccc; height:25px;" valign="middle">BP+Data Analysis (DA)</td>
                        <td style="font-family:Verdana; font-size:11px; color:Black; background-color:#cccccc; height:25px;" valign="middle">BP+DA+Graph (GP)</td>
                        <%--<td style="font-family:Verdana; font-size:11px; color:Black; background-color:#cccccc; height:25px;" valign="middle">BP+DA+GP+Email Alert (EA)</td>--%>
                    </tr>
                    <tr><td style="height:10px;"></td></tr>
                    <tr>
                        <td style="font-family:Verdana; font-size:11px; color:Black;">Query Builder <br />(Code -QBMUSDSql)<br /><font color="#0000cc"><b>Price : 25$</b></font><br /><b><asp:LinkButton ID="LinkButton56" Text="Demo" CommandArgument="QBMUSDSql"  OnCommand="LinkButton_Command" runat="server"></asp:LinkButton> <br />Buy Now</b></td>
                        <td style="font-family:Verdana; font-size:11px; color:Black;">QB+Data Analysis<br />(Code -QBMUSDSqlDA)<br /><font color="#0000cc"><b>Price : 75$</b></font><br /><b><asp:LinkButton ID="LinkButton57" Text="Demo" CommandArgument="QBMUSDSqlDA"  OnCommand="LinkButton_Command" runat="server"></asp:LinkButton> <br />Buy Now</b></td>
                        <td style="font-family:Verdana; font-size:11px; color:Black;">QB+DA+Graph<br />(Code -QBMUSDSqlDAGP)<br /><font color="#0000cc"><b>Price : 125$</b></font><br /><b><asp:LinkButton ID="LinkButton58" Text="Demo" CommandArgument="QBMUSDSqlDAGP"  OnCommand="LinkButton_Command" runat="server"></asp:LinkButton> <br />Buy Now</b></td>
                        <%--<td style="font-family:Verdana; font-size:11px; color:Black;">QB+DA+Graph+Email Alert<br />(Code -QBMUSDSqlDAGPEA)<br /><font color="#0000cc"><b>Price : 175$</b></font><br /><b><asp:LinkButton ID="LinkButton59" Text="Demo" CommandArgument="QBMUSDSqlDAGPEA"  OnCommand="LinkButton_Command" runat="server"></asp:LinkButton> <br />Buy Now</b></td>--%>
                    </tr>
                     <tr><td style="height:10px;"></td></tr>
                     <tr><td style="height:10px;font-family:Verdana; font-size:14px; color:Black;" colspan="4">or</td></tr>
                     <tr><td style="height:10px;"></td></tr>
                    <tr>
                        <td style="font-family:Verdana; font-size:11px; color:Black;">Report Designer <br />(Code -RDMUSDSql)<br /><font color="#0000cc"><b>Price : 50$</b></font><br /><b><asp:LinkButton ID="LinkButton60" Text="Demo" CommandArgument="RDMUSDSql"  OnCommand="LinkButton_Command" runat="server"></asp:LinkButton> <br />Buy Now</b></td>
                        <td style="font-family:Verdana; font-size:11px; color:Black;">RD+Data Analysis<br />(Code -RDMUSDSqlDA)<br /><font color="#0000cc"><b>Price : 100$</b></font><br /><b><asp:LinkButton ID="LinkButton61" Text="Demo" CommandArgument="RDMUSDSqlDA"  OnCommand="LinkButton_Command" runat="server"></asp:LinkButton> <br />Buy Now</b></td>
                        <td style="font-family:Verdana; font-size:11px; color:Black;">RD+DA+ Graph<br />(Code -RDMUSDSqlDAGP)<br /><font color="#0000cc"><b>Price : 150$</b></font><br /><b><asp:LinkButton ID="LinkButton62" Text="Demo" CommandArgument="RDMUSDSqlDAGP"  OnCommand="LinkButton_Command" runat="server"></asp:LinkButton> <br />Buy Now</b></td>
                       <%-- <td style="font-family:Verdana; font-size:11px; color:Black;">RD+DA+ Graph + Email<br />(Code -RDMUSDSqlDAGPEA)<br /><font color="#0000cc"><b>Price : 200$</b></font><br /><b><asp:LinkButton ID="LinkButton63" Text="Demo" CommandArgument="RDMUSDSqlDAGPEA"  OnCommand="LinkButton_Command" runat="server"></asp:LinkButton> <br />Buy Now</b></td>--%>
                    </tr>
                </table>
                <table style="width:570px;" cellpadding="1" cellspacing="1" id="MulUserSingDBMySQL" runat="server">
                    <tr>
                        <td style="font-family:Verdana; font-size:14px; color:#0000CC;" colspan="5"><b>Multiple User License With Single Database (MySQL)</b></td>
                    </tr>
                    <tr><td style="height:5px;"></td></tr>
                    <tr>
                        <td style="font-family:Verdana; font-size:11px; color:Black; background-color:#cccccc; height:25px;" valign="middle">Basic Product (BP)</td>
                        <td style="font-family:Verdana; font-size:11px; color:Black; background-color:#cccccc; height:25px;" valign="middle">BP+Data Analysis (DA)</td>
                        <td style="font-family:Verdana; font-size:11px; color:Black; background-color:#cccccc; height:25px;" valign="middle">BP+DA+Graph (GP)</td>
                        <%--<td style="font-family:Verdana; font-size:11px; color:Black; background-color:#cccccc; height:25px;" valign="middle">BP+DA+GP+Email Alert (EA)</td>--%>
                    </tr>
                    <tr><td style="height:10px;"></td></tr>
                    <tr>
                        <td style="font-family:Verdana; font-size:11px; color:Black;">Query Builder <br />(Code -QBMUSDMySql)<br /><font color="#0000cc"><b>Price : 25$</b></font><br /><b><asp:LinkButton ID="LinkButton64" Text="Demo" CommandArgument="QBMUSDMySql"  OnCommand="LinkButton_Command" runat="server"></asp:LinkButton> <br />Buy Now</b></td>
                        <td style="font-family:Verdana; font-size:11px; color:Black;">QB+Data Analysis<br />(Code -QBMUSDMySqlDA)<br /><font color="#0000cc"><b>Price : 75$</b></font><br /><b><asp:LinkButton ID="LinkButton65" Text="Demo" CommandArgument="QBMUSDMySqlDA"  OnCommand="LinkButton_Command" runat="server"></asp:LinkButton> <br />Buy Now</b></td>
                        <td style="font-family:Verdana; font-size:11px; color:Black;">QB+DA+Graph<br />(Code -QBMUSDMySqlDAGP)<br /><font color="#0000cc"><b>Price : 125$</b></font><br /><b><asp:LinkButton ID="LinkButton66" Text="Demo" CommandArgument="QBMUSDMySqlDAGP"  OnCommand="LinkButton_Command" runat="server"></asp:LinkButton> <br />Buy Now</b></td>
                        <%--<td style="font-family:Verdana; font-size:11px; color:Black;">QB+DA+Graph+Email Alert<br />(Code -QBMUSDMySqlDAGPEA)<br /><font color="#0000cc"><b>Price : 175$</b></font><br /><b><asp:LinkButton ID="LinkButton67" Text="Demo" CommandArgument="QBMUSDMySqlDAGPEA"  OnCommand="LinkButton_Command" runat="server"></asp:LinkButton> <br />Buy Now</b></td>--%>
                    </tr>
                     <tr><td style="height:10px;"></td></tr>
                     <tr><td style="height:10px;font-family:Verdana; font-size:14px; color:Black;" colspan="4">or</td></tr>
                     <tr><td style="height:10px;"></td></tr>
                    <tr>
                        <td style="font-family:Verdana; font-size:11px; color:Black;">Report Designer <br />(Code -RDMUSDMySql)<br /><font color="#0000cc"><b>Price : 50$</b></font><br /><b><asp:LinkButton ID="LinkButton68" Text="Demo" CommandArgument="RDMUSDMySql"  OnCommand="LinkButton_Command" runat="server"></asp:LinkButton> <br />Buy Now</b></td>
                        <td style="font-family:Verdana; font-size:11px; color:Black;">RD+Data Analysis<br />(Code -RDMUSDMySqlDA)<br /><font color="#0000cc"><b>Price : 100$</b></font><br /><b><asp:LinkButton ID="LinkButton69" Text="Demo" CommandArgument="RDMUSDMySqlDA"  OnCommand="LinkButton_Command" runat="server"></asp:LinkButton> <br />Buy Now</b></td>
                        <td style="font-family:Verdana; font-size:11px; color:Black;">RD+DA+ Graph<br />(Code -RDMUSDMySqlDAGP)<br /><font color="#0000cc"><b>Price : 150$</b></font><br /><b><asp:LinkButton ID="LinkButton70" Text="Demo" CommandArgument="RDMUSDMySqlDAGP"  OnCommand="LinkButton_Command" runat="server"></asp:LinkButton> <br />Buy Now</b></td>
                       <%-- <td style="font-family:Verdana; font-size:11px; color:Black;">RD+DA+ Graph + Email<br />(Code -RDMUSDMySqlDAGPEA)<br /><font color="#0000cc"><b>Price : 200$</b></font><br /><b><asp:LinkButton ID="LinkButton71" Text="Demo" CommandArgument="RDMUSDMySqlDAGPEA"  OnCommand="LinkButton_Command" runat="server"></asp:LinkButton> <br />Buy Now</b></td>--%>
                    </tr>
                </table>
                <table style="width:570px;" cellpadding="1" cellspacing="1" id="MulUserSingDBOra" runat="server">
                    <tr>
                        <td style="font-family:Verdana; font-size:14px; color:#0000CC;" colspan="5"><b>Multiple User License With Single Database (Oracle)</b></td>
                    </tr>
                    <tr><td style="height:5px;"></td></tr>
                    <tr>
                        <td style="font-family:Verdana; font-size:11px; color:Black; background-color:#cccccc; height:25px;" valign="middle">Basic Product (BP)</td>
                        <td style="font-family:Verdana; font-size:11px; color:Black; background-color:#cccccc; height:25px;" valign="middle">BP+Data Analysis (DA)</td>
                        <td style="font-family:Verdana; font-size:11px; color:Black; background-color:#cccccc; height:25px;" valign="middle">BP+DA+Graph (GP)</td>
                        <%--<td style="font-family:Verdana; font-size:11px; color:Black; background-color:#cccccc; height:25px;" valign="middle">BP+DA+GP+Email Alert (EA)</td>--%>
                    </tr>
                    <tr><td style="height:10px;"></td></tr>
                    <tr>
                        <td style="font-family:Verdana; font-size:11px; color:Black;">Query Builder <br />(Code -QBMUSDOra)<br /><font color="#0000cc"><b>Price : 25$</b></font><br /><b><asp:LinkButton ID="LinkButton72" Text="Demo" CommandArgument="QBMUSDOra"  OnCommand="LinkButton_Command" runat="server"></asp:LinkButton> <br />Buy Now</b></td>
                        <td style="font-family:Verdana; font-size:11px; color:Black;">QB+Data Analysis<br />(Code -QBMUSDOraDA)<br /><font color="#0000cc"><b>Price : 75$</b></font><br /><b><asp:LinkButton ID="LinkButton73" Text="Demo" CommandArgument="QBMUSDOraDA"  OnCommand="LinkButton_Command" runat="server"></asp:LinkButton> <br />Buy Now</b></td>
                        <td style="font-family:Verdana; font-size:11px; color:Black;">QB+DA+Graph<br />(Code -QBMUSDOraDAGP)<br /><font color="#0000cc"><b>Price : 125$</b></font><br /><b><asp:LinkButton ID="LinkButton74" Text="Demo" CommandArgument="QBMUSDOraDAGP"  OnCommand="LinkButton_Command" runat="server"></asp:LinkButton> <br />Buy Now</b></td>
                        <%--<td style="font-family:Verdana; font-size:11px; color:Black;">QB+DA+Graph+Email Alert<br />(Code -QBMUSDOraDAGPEA)<br /><font color="#0000cc"><b>Price : 175$</b></font><br /><b><asp:LinkButton ID="LinkButton75" Text="Demo" CommandArgument="QBMUSDOraDAGPEA"  OnCommand="LinkButton_Command" runat="server"></asp:LinkButton> <br />Buy Now</b></td>--%>
                    </tr>
                     <tr><td style="height:10px;"></td></tr>
                     <tr><td style="height:10px;font-family:Verdana; font-size:14px; color:Black;" colspan="4">or</td></tr>
                     <tr><td style="height:10px;"></td></tr>
                    <tr>
                        <td style="font-family:Verdana; font-size:11px; color:Black;">Report Designer <br />(Code -RDMUSDOra)<br /><font color="#0000cc"><b>Price : 50$</b></font><br /><b><asp:LinkButton ID="LinkButton76" Text="Demo" CommandArgument="RDMUSDOra"  OnCommand="LinkButton_Command" runat="server"></asp:LinkButton> <br />Buy Now</b></td>
                        <td style="font-family:Verdana; font-size:11px; color:Black;">RD+Data Analysis<br />(Code -RDMUSDOraDA)<br /><font color="#0000cc"><b>Price : 100$</b></font><br /><b><asp:LinkButton ID="LinkButton77" Text="Demo" CommandArgument="RDMUSDOraDA"  OnCommand="LinkButton_Command" runat="server"></asp:LinkButton> <br />Buy Now</b></td>
                        <td style="font-family:Verdana; font-size:11px; color:Black;">RD+DA+ Graph<br />(Code -RDMUSDOraDAGP)<br /><font color="#0000cc"><b>Price : 150$</b></font><br /><b><asp:LinkButton ID="LinkButton78" Text="Demo" CommandArgument="RDMUSDOraDAGP"  OnCommand="LinkButton_Command" runat="server"></asp:LinkButton> <br />Buy Now</b></td>
                        <%--<td style="font-family:Verdana; font-size:11px; color:Black;">RD+DA+ Graph + Email<br />(Code -RDMUSDOraDAGPEA)<br /><font color="#0000cc"><b>Price : 200$</b></font><br /><b><asp:LinkButton ID="LinkButton79" Text="Demo" CommandArgument="RDMUSDOraDAGPEA"  OnCommand="LinkButton_Command" runat="server"></asp:LinkButton> <br />Buy Now</b></td>--%>
                    </tr>
                </table>
                <table style="width:570px;" cellpadding="1" cellspacing="1" id="MulUserSingDBPogr" runat="server">
                    <tr>
                        <td style="font-family:Verdana; font-size:14px; color:#0000CC;" colspan="5"><b>Multiple User License With Single Database (PostgreSQL)</b></td>
                    </tr>
                    <tr><td style="height:5px;"></td></tr>
                    <tr>
                        <td style="font-family:Verdana; font-size:11px; color:Black; background-color:#cccccc; height:25px;" valign="middle">Basic Product (BP)</td>
                        <td style="font-family:Verdana; font-size:11px; color:Black; background-color:#cccccc; height:25px;" valign="middle">BP+Data Analysis (DA)</td>
                        <td style="font-family:Verdana; font-size:11px; color:Black; background-color:#cccccc; height:25px;" valign="middle">BP+DA+Graph (GP)</td>
                        <%--<td style="font-family:Verdana; font-size:11px; color:Black; background-color:#cccccc; height:25px;" valign="middle">BP+DA+GP+Email Alert (EA)</td>--%>
                    </tr>
                    <tr><td style="height:10px;"></td></tr>
                    <tr>
                        <td style="font-family:Verdana; font-size:11px; color:Black;">Query Builder <br />(Code -QBMUSDPoGre)<br /><font color="#0000cc"><b>Price : 25$</b></font><br /><b><asp:LinkButton ID="LinkButton80" Text="Demo" CommandArgument="QBMUSDPoGre"  OnCommand="LinkButton_Command" runat="server"></asp:LinkButton> <br />Buy Now</b></td>
                        <td style="font-family:Verdana; font-size:11px; color:Black;">QB+Data Analysis<br />(Code -QBMUSDPoGreDA)<br /><font color="#0000cc"><b>Price : 75$</b></font><br /><b><asp:LinkButton ID="LinkButton81" Text="Demo" CommandArgument="QBMUSDPoGreDA"  OnCommand="LinkButton_Command" runat="server"></asp:LinkButton> <br />Buy Now</b></td>
                        <td style="font-family:Verdana; font-size:11px; color:Black;">QB+DA+Graph<br />(Code -QBMUSDPoGreDAGP)<br /><font color="#0000cc"><b>Price : 125$</b></font><br /><b><asp:LinkButton ID="LinkButton82" Text="Demo" CommandArgument="QBMUSDPoGreDAGP"  OnCommand="LinkButton_Command" runat="server"></asp:LinkButton> <br />Buy Now</b></td>
                        <%--<td style="font-family:Verdana; font-size:11px; color:Black;">QB+DA+Graph+Email Alert<br />(Code -QBMUSDPoGreDAGPEA)<br /><font color="#0000cc"><b>Price : 175$</b></font><br /><b><asp:LinkButton ID="LinkButton83" Text="Demo" CommandArgument="QBMUSDPoGreDAGPEA"  OnCommand="LinkButton_Command" runat="server"></asp:LinkButton> <br />Buy Now</b></td>--%>
                    </tr>
                     <tr><td style="height:10px;"></td></tr>
                     <tr><td style="height:10px;font-family:Verdana; font-size:14px; color:Black;" colspan="4">or</td></tr>
                     <tr><td style="height:10px;"></td></tr>
                    <tr>
                        <td style="font-family:Verdana; font-size:11px; color:Black;">Report Designer <br />(Code -RDMUSDPoGre)<br /><font color="#0000cc"><b>Price : 50$</b></font><br /><b><asp:LinkButton ID="LinkButton84" Text="Demo" CommandArgument="RDMUSDPoGre"  OnCommand="LinkButton_Command" runat="server"></asp:LinkButton> <br />Buy Now</b></td>
                        <td style="font-family:Verdana; font-size:11px; color:Black;">RD+Data Analysis<br />(Code -RDMUSDPoGreDA)<br /><font color="#0000cc"><b>Price : 100$</b></font><br /><b><asp:LinkButton ID="LinkButton85" Text="Demo" CommandArgument="RDMUSDPoGreDA"  OnCommand="LinkButton_Command" runat="server"></asp:LinkButton> <br />Buy Now</b></td>
                        <td style="font-family:Verdana; font-size:11px; color:Black;">RD+DA+ Graph<br />(Code -RDMUSDPoGreDAGP)<br /><font color="#0000cc"><b>Price : 150$</b></font><br /><b><asp:LinkButton ID="LinkButton86" Text="Demo" CommandArgument="RDMUSDPoGreDAGP"  OnCommand="LinkButton_Command" runat="server"></asp:LinkButton> <br />Buy Now</b></td>
                        <%--<td style="font-family:Verdana; font-size:11px; color:Black;">RD+DA+ Graph + Email<br />(Code -RDMUSDPoGreDAGPEA)<br /><font color="#0000cc"><b>Price : 200$</b></font><br /><b><asp:LinkButton ID="LinkButton87" Text="Demo" CommandArgument="RDMUSDPoGreDAGPEA"  OnCommand="LinkButton_Command" runat="server"></asp:LinkButton> <br />Buy Now</b></td>--%>
                    </tr>
                </table>

                <table style="width:570px;" cellpadding="1" cellspacing="1" id="MulUserMulDB" runat="server">
                    <tr>
                        <td style="font-family:Verdana; font-size:14px; color:#0000CC;" colspan="5"><b>Multiple User License With Multiple Database (With two or more of above)</b></td>
                    </tr>
                    <tr><td style="height:5px;"></td></tr>
                    <tr>
                        <td style="font-family:Verdana; font-size:11px; color:Black; background-color:#cccccc; height:25px;" valign="middle">Basic Product (BP)</td>
                        <td style="font-family:Verdana; font-size:11px; color:Black; background-color:#cccccc; height:25px;" valign="middle">BP+Data Analysis (DA)</td>
                        <td style="font-family:Verdana; font-size:11px; color:Black; background-color:#cccccc; height:25px;" valign="middle">BP+DA+Graph (GP)</td>
                        <%--<td style="font-family:Verdana; font-size:11px; color:Black; background-color:#cccccc; height:25px;" valign="middle">BP+DA+GP+Email Alert (EA)</td>--%>
                    </tr>
                    <tr><td style="height:10px;"></td></tr>
                    <tr>
                        <td style="font-family:Verdana; font-size:11px; color:Black;">Query Builder <br />(Code -QBMUMD)<br /><font color="#0000cc"><b>Price : 50$</b></font><br /><b><asp:LinkButton ID="LinkButton88" Text="Demo" CommandArgument="QBMUMD"  OnCommand="LinkButton_Command" runat="server"></asp:LinkButton> <br />Buy Now</b></td>
                        <td style="font-family:Verdana; font-size:11px; color:Black;">QB+Data Analysis<br />(Code -QBMUMDDA)<br /><font color="#0000cc"><b>Price : 100$</b></font><br /><b><asp:LinkButton ID="LinkButton89" Text="Demo" CommandArgument="QBMUMDDA"  OnCommand="LinkButton_Command" runat="server"></asp:LinkButton> <br />Buy Now</b></td>
                        <td style="font-family:Verdana; font-size:11px; color:Black;">QB+DA+Graph<br />(Code -QBMUMDDAGP)<br /><font color="#0000cc"><b>Price : 150$</b></font><br /><b><asp:LinkButton ID="LinkButton90" Text="Demo" CommandArgument="QBMUMDDAGP"  OnCommand="LinkButton_Command" runat="server"></asp:LinkButton> <br />Buy Now</b></td>
                        <%--<td style="font-family:Verdana; font-size:11px; color:Black;">QB+DA+Graph+Email Alert<br />(Code -QBMUMDDAGPEA)<br /><font color="#0000cc"><b>Price : 175$</b></font><br /><b><asp:LinkButton ID="LinkButton91" Text="Demo" CommandArgument="QBMUMDDAGPEA"  OnCommand="LinkButton_Command" runat="server"></asp:LinkButton> <br />Buy Now</b></td>--%>
                    </tr>
                     <tr><td style="height:10px;"></td></tr>
                     <tr><td style="height:10px;font-family:Verdana; font-size:14px; color:Black;" colspan="4">or</td></tr>
                     <tr><td style="height:10px;"></td></tr>
                    <tr>
                        <td style="font-family:Verdana; font-size:11px; color:Black;">Report Designer <br />(Code -RDMUMD)<br /><font color="#0000cc"><b>Price : 75$</b></font><br /><b><asp:LinkButton ID="LinkButton92" Text="Demo" CommandArgument="RDMUMD"  OnCommand="LinkButton_Command" runat="server"></asp:LinkButton> <br />Buy Now</b></td>
                        <td style="font-family:Verdana; font-size:11px; color:Black;">RD+Data Analysis<br />(Code -RDMUMDDA)<br /><font color="#0000cc"><b>Price : 125$</b></font><br /><b><asp:LinkButton ID="LinkButton93" Text="Demo" CommandArgument="RDMUMDDA"  OnCommand="LinkButton_Command" runat="server"></asp:LinkButton> <br />Buy Now</b></td>
                        <td style="font-family:Verdana; font-size:11px; color:Black;">RD+DA+ Graph<br />(Code -RDMUMDDAGP)<br /><font color="#0000cc"><b>Price : 175$</b></font><br /><b><asp:LinkButton ID="LinkButton94" Text="Demo" CommandArgument="RDMUMDDAGP"  OnCommand="LinkButton_Command" runat="server"></asp:LinkButton> <br />Buy Now</b></td>
                        <%--<td style="font-family:Verdana; font-size:11px; color:Black;">RD+DA+ Graph + Email<br />(Code -RDMUMDDAGPEA)<br /><font color="#0000cc"><b>Price : 200$</b></font><br /><b><asp:LinkButton ID="LinkButton95" Text="Demo" CommandArgument="RDMUMDDAGPEA"  OnCommand="LinkButton_Command" runat="server"></asp:LinkButton> <br />Buy Now</b></td>--%>
                    </tr>
                </table>
                </form>
            </div>
            <!--Detail Content panel END-->
        </div>
    </div>
</div>

<!--body -->
<!--footer -->
<uc4:Footer ID="Footer" runat="server" />
<!--footer -->
<asp:textbox id="txtHidden" Runat="server"  Visible="False"></asp:textbox>
</body>
</html>
