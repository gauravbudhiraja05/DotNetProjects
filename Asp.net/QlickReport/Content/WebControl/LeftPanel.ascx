<%@ Control Language="VB" AutoEventWireup="false" CodeFile="LeftPanel.ascx.vb" Inherits="WebControl_LeftPanel" %>
<div id="lftPan">
                <%--<form id="Form2" action="" method="get" class="one">
                <asp:Label runat="server" ID="lblmsg" Visible="false" ForeColor="Red"></asp:Label>
                    <p class="member">Member login</p>
                    <label>Username :</label>
                    <input id="txtuserid" name="txtuserid" type="text" class="txtfield" tabindex="1" runat="server"/>
                    <label>Password :</label>
                    <input id="txtpassword" name="txtpassword" type="password" class="txtfield"  tabindex="2" runat="server"/>
                    <p class="forgot"><a href="../../Misc/ForgotPassword.aspx">Forgot Password?</a></p>
                    <input id="Login" name="" type="button" class="login" value="" 
                    title="login" runat="server"  tabindex="3"/>
                    <div class="clear"></div>
                </form>--%>
                <form id="Form1" action="" method="get" class="one" runat="server">
                <asp:Label runat="server" ID="lblmsg" Visible="false" ForeColor="Red"></asp:Label><asp:Label runat="server" ID="lbldays" Visible="false" ForeColor="Red"></asp:Label>
                    <p class="member">Member login</p>
                    <label style="width:80px;">Username :</label>
                    <input id="txtuserid" name="txtuserid" type="text" class="txtfield"  style="width:183px;"  runat="server"/>
                    <label style="width:80px;">Password :</label>
                    <input id="txtpassword" name="txtpassword" type="password" class="txtfield"  style="width:183px;"  runat="server"/>
                    <b><label id="Label1" runat="server" style="color:red"    onclick="return windowForPassword()">Forgot Password?</label></b>  
                    <asp:Button ID="Login" Text="Login" CssClass="login" runat="server" />   
                    <%--<input id="Login1" name="" type="button" class="login" value="" title="login" runat="server" onserverclick="Login_Click"/>--%>
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