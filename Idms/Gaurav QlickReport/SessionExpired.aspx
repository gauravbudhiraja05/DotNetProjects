<%@ Page Language="VB" AutoEventWireup="false" CodeFile="SessionExpired.aspx.vb" Inherits="ReportDesigner_SessionExpired" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" lang="en-us" xml:lang="en-us">
<head runat="server">
<%--<SCRIPT LANGUAGE="JavaScript"> 

function onKeyDown() {
if ( (event.altKey) || ((event.keyCode == 8) && 
(event.srcElement.type != "text" &&
event.srcElement.type != "textarea" &&
event.srcElement.type != "password")) || 
((event.ctrlKey) && ((event.keyCode == 82)) ) ||
(event.keyCode == 122) ) {
event.keyCode = 0;
event.returnValue = false;
}
}

if (window.location.search.indexOf("realwindow") == -1) {
Nwindow=window.open(window.location+"?realwindow=1", "", "width="+(screen.width-8)+", height="+(screen.height-45));
Nwindow.moveTo((screen.width/2-((screen.width-8))/2), (screen.height/2-((screen.height-45))/2)-25);
window.opener="";
window.close();
}
</script>--%>
    <title>Session Expired</title>
    <link href="App_Themes/Themes/StyleSheet.css" />
<script language="javascript" type="text/javascript">
// <!CDATA[

function SessionExpired_r5_c5_onclick() {

}

// ]]>
</script>
</head>
<body bgcolor="#ffffff" topmargin="0" bottommargin="0" rightmargin="0" leftmargin="0">
<form id="form1" runat="server">
<table border="0" cellpadding="0" cellspacing="0" width="100%">
<!-- fwtable fwsrc="Session Expired.png" fwbase="SessionExpired.jpg" fwstyle="Dreamweaver" fwdocid = "1895290659" fwnested="0" -->
  <tr>
   <td bgcolor="#ffffff" width="137" height="1" border="0" ></td>
   <td bgcolor="#ffffff" width="133" height="1" border="0" ></td>
   <td bgcolor="#ffffff" width="125" height="1" border="0" ></td>
   <td bgcolor="#ffffff" width="46" height="1" border="0" ></td>
   <td bgcolor="#ffffff" width="158" height="1" border="0"></td>
   <td bgcolor="#ffffff" width="120" height="1" border="0"></td>
   <td bgcolor="#ffffff" width="80" height="1" border="0"></td>
   <td bgcolor="#ffffff" width="1" height="1" border="0" ></td>
  </tr>

  <tr>
   <td rowspan="2" colspan="2" bgcolor="#2174BA"><img name="SessionExpired_r1_c1" src="images/Session/SessionExpired_r1_c1.jpg" width="270" height="148" border="0" id="SessionExpired_r1_c1" alt="COMPANY LOGO" /></td>
   <td colspan="5" bgcolor="#2174BA" width="529" height="109"></td>
   <td bgcolor="#ffffff" width="1" height="109"></td>
  </tr>
  <tr>
   <td colspan="3" bgcolor="#2174BA" width="329" height="39"></td>
   <td colspan="2" bgcolor="#2174BA" align="right"><img name="SessionExpired_r2_c6" src="images/Session/SessionExpired_r2_c6.jpg" width="200" height="39" border="0" id="SessionExpired_r2_c6" alt="Ato-Whiz Image" /></td>
   <td bgcolor="#ffffff" width="1" height="39" border="0" ></td>
  </tr>
  <tr>
   <td colspan="7" bgcolor="#FFFFFF" width="799" height="40"></td>
   <td bgcolor="#ffffff" width="1" height="40" border="0"></td>
  </tr>
  <tr>
   <td rowspan="3" bgcolor="#FFFFFF" width="137" height="264"></td>
   <td rowspan="3" colspan="2" align="right"><img name="SessionExpired_r4_c2" src="images/Session/SessionExpired_r4_c2.jpg" width="258" height="264" border="0" id="SessionExpired_r4_c2" alt="Clock" /></td>
   <td rowspan="3" bgcolor="#FFFFFF" width="46" height="264"></td>
   <td colspan="3" bgcolor="#FFFFFF" width="358" height="47"></td>
   <td bgcolor="#ffffff" width="1" height="47" border="0" ></td>
  </tr>
  <tr>
   <td colspan="2"><img name="SessionExpired_r5_c5" src="images/Session/SessionExpired_r5_c5.jpg" width="278" height="174" border="0" id="SessionExpired_r5_c5" usemap="#m_SessionExpired_r5_c5" alt="Session Expired" onclick="return SessionExpired_r5_c5_onclick()" /></td>
   <td bgcolor="#FFFFFF" width="80" height="174"></td>
   <td bgcolor="#ffffff" width="1" height="174" border="0"></td>
  </tr>
  <tr>
   <td colspan="3" bgcolor="#FFFFFF" width="358" height="43"></td>
   <td bgcolor="#ffffff" width="1" height="43" border="0"></td>
  </tr>
  <tr>
   <td colspan="7" bgcolor="#FFFFFF" width="799" height="148"></td>
   <td bgcolor="#ffffff" width="1" height="148" border="0" ></td>
  </tr>
</table>
<map name="m_SessionExpired_r5_c5" id="m_SessionExpired_r5_c5">
<area shape="rect" coords="9,142,191,163" href="Default.aspx" alt="Login Again" />
</map>
</form>
</body>



<%--<body style="text-align:center;background-color:#669999 ">
    
    <div>
    <table style="width: 552px; height: 424px">
    <tr rowspan="5">
    
    </tr>
        <tr>
            <td style="height: 7px">
                 <img id="imgSessionexpired" title="Oops! Session Expired" src="images/sessionExpired.jpg" />
            </td>
        </tr>
        <tr>
            <td>
                <a href="../AutoWhiz/Index.aspx">Your session has been expired. Login again to continue....</a>
            </td>
        </tr>
    </table>
       
        
    </div>
 
</body>--%>
</html>
