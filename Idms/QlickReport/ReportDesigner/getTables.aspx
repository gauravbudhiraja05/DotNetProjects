<%@ Page Language="VB" AutoEventWireup="false" CodeFile="getTables.aspx.vb" Inherits="ReportDesigner_getTables" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" type="text/css" href="../App_Themes/Themes/StyleSheet.css" />
    <script language="javascript" type="text/javascript"  >
        function parentUpdate()//To pass the selected tables to the parent form (welcome.aspx)
        {
            var tbl = document.getElementById("<%=hidTables.ClientID%>").value;
            if (document.getElementById("<%=hidSource.ClientID%>").value == "Welcome") {
                window.opener.setTable(tbl);
            }
            else if (document.getElementById("<%=hidSource.ClientID%>").value == "setData") {
                window.opener.showColumns(tbl);
            }
            else if (document.getElementById("<%=hidSource.ClientID%>").value == "where") {
                window.opener.addColumns(tbl);
            }
            else if (document.getElementById("<%=hidSource.ClientID%>").value == "formula") {
                window.opener.addColumns(tbl);
            }
            else if (document.getElementById("<%=hidSource.ClientID%>").value == "having") {
                window.opener.addColumns(tbl);
            }
            else if (document.getElementById("<%=hidSource.ClientID%>").value == "orderby") {
                window.opener.addColumns(tbl);
            }
            else if (document.getElementById("<%=hidSource.ClientID%>").value == "groupby") {
                window.opener.addColumns(tbl);
            }
            else if (document.getElementById("<%=hidSource.ClientID%>").value == "colorcondition") {
                window.opener.addColumns(tbl);
            }
            else if (document.getElementById("<%=hidSource.ClientID%>").value == "colCon") {
                window.opener.addColorcolumns(tbl);
            }
            window.parent.focus();
            window.self.close();
        }
        function closeTables() // Close the window
        {
            window.parent.focus();
            window.self.close();
        }
        function CheckAllDataGridCheckBoxes(aspCheckBoxID, checkVal) // to (de)select all the items together
        {
            for (i = 0; i < document.forms[0].elements.length; i++) {
                elm = document.forms[0].elements[i];
                if (elm.type == 'checkbox') {
                    elm.checked = checkVal;

                }


            }
        }
        
    </script>
    <style type="text/css">
        .button
        {}
    </style>
</head>
<body>
    <form id="formGettables" title="Select Tables" runat="server">
       <div  id="divTab" title="Select Tables"  style="width:400px; left: 0px; top: 0px;">               
		 <table runat="server" visible="false" id="spandisplay" style="width: 400px; background-color: #ffffff;">
		      <caption style ="background-color:#67A897">Select Tables</caption>
                  <tr>
		                <td style="width: 46px; color : Black " scope ="col" >                           
		                    <asp:Label ID="lbl1" runat="server" CssClass="label" Text="Select Level 1 "></asp:Label>  
		                </td>
		                <td style="width: 8px" title="Select Department" scope="col">
		                <asp:DropDownList ToolTip="Select Department" runat="server" CssClass="dropdownlist" ID="ddlDepartment" AutoPostBack="True" EnableTheming="True"></asp:DropDownList>
		                </td>
		            </tr>
		            <tr>
		                <td style="width: 46px; color : Black " title="Select Client" scope="col">                            
		                   <asp:Label ID="lbl2" runat="server" CssClass="label" Text="Select Level 2 "></asp:Label>
		                </td>
		                <td style="width: 8px" title="Select Client" scope="col">
		                    <asp:DropDownList ToolTip="Select Client" runat="server" CssClass="dropdownlist" ID="ddlClient" AutoPostBack="True"></asp:DropDownList>
		                </td>
		            </tr>
		            <tr>
		                <td style="width: 46px; color : Black " title="Select LOB" scope="col">
		                   <asp:Label ID="lbl3" runat="server" CssClass="label" Text="Select Level 3 "></asp:Label>
		                </td>
		                <td style="width: 8px" title="Select LOB" scope="col">
		                    <asp:DropDownList runat="server" CssClass="dropdownlist" ID="ddlLob"></asp:DropDownList>
		                </td>
		            </tr>
                    <tr>
                        <td colspan="2" align="center"><asp:Button runat="server" ID="btnGoMul" ToolTip="Select Tables"  Text="Select Tables" cssclass="button" Width="100px" /></td>
                    </tr>
                    </table>
         <table  style="width: 400px; background-color: #ffffff;" summary="This table holds the department, client & LOB to set the span of the tables." >
                  <tr>
		                <td style="width: 100px" scope="col">
		                </td>
		                <td scope="col" title="Select Tables">
		                    <asp:Button runat="server" ID="btnGo" Visible="false" ToolTip="Select Tables"  Text="Select Tables" cssclass="button" Width="100px" />
                             <asp:Button runat="server" ID="selecttemptab" ToolTip="Select Temprary Table"  
                                Text="Select Temp Table" cssclass="button" Width="122px" />
                            <input type="button" id="btnClosetop" title="Close" onclick="javascript:closeTables();" value="Close" class="button" style="width:63px;" /></td>
		            </tr>
		            <tr>
		                <td colspan="2" title="Message Box" scope="colgroup">
		                <asp:Label runat="server" ID="lblMsg" CssClass="label" ForeColor="Red" Width="400px"></asp:Label></td>
		            </tr>
		          <tr>
		                <td colspan="2" scope="colgroup" title="Select Tables">
		                    <asp:DataGrid runat="server" ID="datagridTables" CssClass="datagrid" ToolTip="Select Tables" AutoGenerateColumns="false" ShowFooter="true" style ="color:black" AllowPaging="True" PageSize="10" Width="400px">
		                        <Columns>
		                            <asp:TemplateColumn>
		                            <HeaderStyle CssClass="datagridHeader" />
		                                <HeaderTemplate>
		                                    <%--<asp:LinkButton runat="server" ID="lnkAll" CommandName="chkAll">SelectAll</asp:LinkButton>/
		                                    <asp:LinkButton runat="server" ID="lnkNone" CommandName="unchkAll">Unselect</asp:LinkButton>--%>
		                                   
		                                    <input id="chkAllItems" type="checkbox" onclick="CheckAllDataGridCheckBoxes('chkItem',document.forms[0].chkAllItems.checked)" />
		                                    <asp:Label runat="server" ID="lblTablename" Text="Table Name" ></asp:Label>
		                                    <label for="chkAllItems" ></label>
		                                </HeaderTemplate>
		                                <ItemTemplate>
		                                   <%-- <asp:Label runat="server" ID="lblchkItem" AssociatedControlID="chkItem"  ></asp:Label>--%>

		                                     <asp:checkbox runat="server"  id="chkItem" Width="100px" />
		                                     <asp:HiddenField ID="lblAutoId" runat="server" Value='<%#Container.DataItem("tableid")%>'  />
		                                     <%--<asp:Label ID= Runat=server Text='<%#Container.DataItem("Tableid")%>' Visible=False></asp:Label>--%>
											<asp:Label id="LnkLOB" Runat="server" Text='<%#Container.DataItem("tablename")%>' ></asp:Label>
		                               </ItemTemplate>
		                               <FooterStyle CssClass="datagridHeader" />
		                                <FooterTemplate> 
		                                    <asp:Button runat="server" CssClass="button" CommandName="Done" ID="btnDone" Text="Done" />
		                                    <%--<input type="button" id="btnClosebottom" onclick="javascript:closeTables()" value="Close" class="button" style="Width:63px" />--%>
		                                </FooterTemplate>        
		                           </asp:TemplateColumn>		                            
		                        </Columns>
                                <PagerStyle Mode="NumericPages" />
                                
		                    </asp:DataGrid>
                          </td>
		            </tr>
		      </table>		      
         </div>    
         <%-- Hidden Fields Declaration --%>
         <input type="hidden" id="hidTables" runat="server" name="hiddenTables" /> <%-- To store all the selected tables --%>         
         <input type="hidden" id="hidUsertype" runat="server" name="hiddenUsertype" /> <%-- To store all the selected tables --%>         
         <input type="hidden" id="hidSource" runat="server" name="hiddenSource" /> <%-- To track down the source of the page --%>         
         <%-- Hidden Fields Declaration ends --%>
    </form>
</body>
</html>
