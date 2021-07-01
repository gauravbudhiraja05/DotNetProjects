<%@ Page Language="VB" AutoEventWireup="false" CodeFile="SavedQueries_singleuser.aspx.vb" Inherits="QueryBuilder_SavedQueries_singleuser" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" lang="en-us" xml:lang="en-us">
<head id="Head1" runat="server">
    <title>SavedQueries</title>
    <script language="javascript" type="text/javascript">
        function LTrim(value) {
            var re = /\s*((\S+\s*)*)/;
            return value.replace(re, "$1");
        }

        // Removes ending whitespaces
        function RTrim(value) {
            var re = /((\s*\S+)*)\s*/;
            return value.replace(re, "$1");
        }

        // Removes leading and ending whitespaces
        function trim(value) {
            return LTrim(RTrim(value));
        }

        function updateParent() {

            if (document.getElementById("ddlYourQuery").selectedIndex == 0 && document.getElementById("ddlQueryName").selectedIndex == 0) {
                alert("Please select query")
            }
            else {
                ////////////////fill query name////////
                if (document.getElementById("ddlYourQuery").selectedIndex > 0) {
                    window.opener.document.getElementById("queryname").value = document.getElementById("ddlYourQuery").value;
                }
                else if (document.getElementById("ddlQueryName").selectedIndex > 0) {
                    window.opener.document.getElementById("queryname").value = document.getElementById("ddlQueryName").value;
                }
                ///////////////end///////////////////
                window.opener.document.getElementById("hidReportmode").value = "edit"
                window.opener.document.getElementById("formulaIds").value = document.getElementById("formula").value;
                window.opener.document.getElementById("hidtablename").value = document.getElementById("hidtablename").value;
                window.opener.document.getElementById("Showdata1").value = document.getElementById("Showdata").value;
                //window.opener.document.getElementById("formula").value  = document.getElementById("formula").value;
                //window.opener.document.getElementById("txtaformula").value  = document.getElementById("formula").value;
                window.opener.document.getElementById("crdata").value = document.getElementById("crdata").value;
                window.opener.document.getElementById("column").value = document.getElementById("column").value;
                window.opener.document.getElementById("wheredata").value = document.getElementById("wheredata").value;
                window.opener.document.getElementById("wheredata1").value = document.getElementById("wheredata1").value;
                window.opener.document.getElementById("chkPercentage").value = document.getElementById("chkPercentage").value;
                window.opener.document.getElementById("hidFormulaName").value = document.getElementById("formulaName").value;
                window.opener.document.getElementById("Savedwhere").value = document.getElementById("ActualWhereHId").value;
                var dept
                var ttxet;
                //if (document.getElementById("cbodept").selectedIndex == -1 || document.getElementById("cbodept").selectedIndex == 0) {
                    dept = 60;
                //}
                //else {
                  //  dept = document.getElementById("cbodept").value;

                //}
                var client
                //if (document.getElementById("cboclient").selectedIndex == -1 || document.getElementById("cboclient").selectedIndex == 0) {
                    client = 0;
                //}
                //else {
                  //  client = document.getElementById("cboclient").value;
                //}
                var lob
                //if (document.getElementById("cbolob").selectedIndex == -1 || document.getElementById("cbolob").selectedIndex == 0) {
                    lob = 0;
                //}
                //else {
                  //  lob = document.getElementById("cbolob").value;
                //}

                window.opener.document.getElementById("hiddept").value = dept;
                window.opener.document.getElementById("hidclient").value = client;
                window.opener.document.getElementById("hidlob").value = lob;
                //    			opener.getclient();
                //alert("Please! Click to continue.");
                //window.opener.document.getElementById("hidclient").value=client;

                //				window.opener.GetLOB();
                //window.opener.document.getElementById("hidlob").value = lob;
                //var str1=document.SaveQuery.hidtablename.value;
                //var str2=str1.replace(",","$")
                //opener.shofieldAndTable(str2)
                //window.opener.document.getElementById("hidtablename").value = str1;
                //window.opener.frmquerybl.txt1.value ='ComeFromSavedQuery';
                //alert("hello");

                //				window.opener.getdataForSavedQuery();
                window.opener.getspansaved(document.getElementById("hidtablename").value);
                window.opener.filltable(document.getElementById("hidtablename"));
                window.opener.get_fieldforsavedQuery(document.getElementById("hidtablename").value);
                //alert("helloSecond");
                opener.binddivsForSavedQuery();
                //self.close();
                //.........................ends
                self.close();

            }
        }
		
</script>
    <link href="../App_Themes/Themes/StyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    
<table id="tablogin" style="Z-INDEX: 101; LEFT: 0px; WIDTH: 100%; POSITION: absolute; TOP: 0px; HEIGHT: 200px"
	cellspacing="0" cellpadding="0"  runat="server">
	<caption>Save Query</caption>
	<tr>
	    <td>
	        <table summary="">
	            <tr runat="server" visible="false">
		            <td style="WIDTH: 120px; HEIGHT: 32px" scope="col"><strong>User</strong></td>
		            <td style="HEIGHT: 32px"  scope="col" width="100px"><asp:label id="lblUser" runat="server" Font-Names="Verdana" Font-Bold="True"></asp:label></td>
	            </tr>
	            <tr runat="server" visible="false">
		            <td style="WIDTH: 120px; HEIGHT: 24px" scope="col"><strong><label for="cbodept">Department</label></strong>&nbsp;</td>
		            <td style="HEIGHT: 24px" scope="col"><asp:dropdownlist id="cbodept" CssClass="dropdownlist" runat="server" AutoPostBack="True"></asp:dropdownlist></td>
	            </tr>
	            <tr runat="server" visible="false">
		            <td style="WIDTH: 120px; HEIGHT: 24px"  scope="col" ><strong><label for="cboclient">Client</label></strong>&nbsp;</td>
		            <td style="HEIGHT: 24px" scope="col"><asp:dropdownlist id="cboclient"  CssClass="dropdownlist"  runat="server" AutoPostBack="True"></asp:dropdownlist></td>
	            </tr>
	            <tr runat="server" visible="false">
		            <td style="WIDTH: 120px; HEIGHT: 24px"  scope="col"><strong><label for="cbolob">LOB</label></strong></td>
		            <td style="HEIGHT: 24px" scope="col"><asp:dropdownlist id="cbolob"  CssClass="dropdownlist"  runat="server" AutoPostBack="True"></asp:dropdownlist></td>
	            </tr>
	            <tr>
		            <td style="WIDTH: 120px; HEIGHT: 25px"  scope="col"><strong><label for="ddlYourQuery">Your Query</label></strong>&nbsp;</td>
		            <td style="HEIGHT: 24px" scope="col"><asp:dropdownlist  CssClass="dropdownlist"  id="ddlYourQuery" runat="server" AutoPostBack="True"></asp:dropdownlist></td>
	            </tr>
	            <tr runat="server" visible="false">
		            <td id="Td1" runat="server" visible="false" style="WIDTH: 120px; HEIGHT: 8px" scope="col" ><strong><label for="ddlQueryName">Shared Queryy</label></strong>&nbsp;</td>
		            <td id="Td2" runat="server" visible="false" style="HEIGHT: 24px" scope="col"><asp:dropdownlist  CssClass="dropdownlist"  id="ddlQueryName" runat="server" AutoPostBack="True"></asp:dropdownlist></td>
	            </tr>   
	         </table>
        </td>
    </tr>
    <tr>
        <td  scope="colgroup">
            <table summary="" >
                <tr>
		            <td style="HEIGHT: 4px" align="center" scope="col"><br />
			            <input class="button" onclick="return updateParent();" type="button" value="Submit" id="Button1" />
			            <asp:button id="cmddelete" Visible="False" CssClass="button" Text="Delete" Runat="server"></asp:button>
			            <input class="button"  onclick="javascript:window.close();" type="button" value="Close" />
			            <br />
		            </td>
	            </tr>   
            </table>
        </td>
    </tr>	
	
	<tr>
        <td scope="col"><input id="Showdata" style="WIDTH: 80%" type="hidden" name="Showdata" runat="server" /><!--Showdata--></td>
    </tr>
     <tr>
        <td scope="col"><input id="formula" style="WIDTH: 80%" type="hidden" name="formula" runat="server" /><!--formula--></td>
    </tr>
     <tr>
        <td scope="col"><input id="crdata" style="WIDTH: 80%" type="hidden" name="crdata" runat="server" /><!--crdata--></td>
    </tr>
     <tr>
        <td scope="col"><input id="column" style="WIDTH: 80%" type="hidden" name="column" runat="server" /><!--column--></td>
    </tr>
     <tr>
        <td scope="col"><input id="wheredata" style="WIDTH: 80%" type="hidden" name="wheredata" runat="server" /><!--wheredata--></td>
    </tr>
     <tr>
        <td scope="col"><input id="wheredata1" style="WIDTH: 80%" type="hidden" name="wheredata1" runat="server" /><!--wheredata1--></td>
    </tr>
     <tr>
        <td scope="col"><input id="chkPercentage" style="WIDTH: 80%" type="hidden" name="chkPercentage" runat="server" /><!--ChkPercent--></td>
    </tr>
     <tr>
        <td scope="col"><input id="formulaName" style="WIDTH: 80%" type="hidden" name="formulaName" runat="server" /><!--formulaName--></td>
    </tr>
    <tr>
        <td scope="col"><input id="selectedfield" style="WIDTH: 80%" type="hidden" name="selectedfield" runat="server" /><!--selectedfield--></td>
    </tr>
     <tr>
        <td scope="col"><input id="hidtablename" type="hidden" name="hidtablename" runat="server" /> </td>
    </tr>
    <tr>
        <td scope="col"><input id="txtheading" type="hidden" name="txtheading" runat="server" /></td>
    </tr>
     <tr>
        <td scope="col"><input id="hidclient" type="hidden" runat="server" /> </td>
    </tr>
     <tr>
        <td scope="col"><input id="hidlob" type="hidden" runat="server" /></td>
        
    </tr>	
    <tr>
        <td scope="col"><input id="ActualWhereHId" type="hidden" runat="server" /></td>
    </tr>
    <tr>
        <td scope="col"><input id="owner" type="hidden" runat="server" /></td>
    </tr>
</table>
			

			
    </form>
</body>
</html>
