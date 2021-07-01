<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="false" CodeFile="table.aspx.vb" Inherits="TableTools_table" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="LeftPlaceHolder" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<link rel="stylesheet" type="text/css" href="../App_Themes/Themes/StyleSheet.css" />
    <script language="javascript" type="text/javascript">
		<!--
        //var bool=false;

        var primcol;
        window.primcol = "";
        function IsNumeric(sText) {
            var ValidChars = "0123456789.";
            var IsNumber = true;
            var Char;
            for (i = 0; i < sText.length && IsNumber == true; i++) {
                Char = sText.charAt(i);
                if (ValidChars.indexOf(Char) == -1) {
                    IsNumber = false;
                }
            }
            return IsNumber;
        }


        function addfield() {

            if ((document.getElementById("<%=txtfieldname.ClientId %>").value == "") || (document.getElementById("<%=cbodatatype.ClientId %>").value == "")) {
                alert("Please Enter information of Column first");
                return;
            }
            else if (((document.getElementById("<%=cbodatatype.ClientId %>").value == "VARCHAR") || (document.getElementById("<%=cbodatatype.ClientId %>").value == "Varchar")) && (document.getElementById("<%=txtsize.ClientId %>").value == "")) {

                alert("Size must not blank for datatype varchar");
                return;
            }
            else
            // This Block iS For Visible TextArea
            {
                document.getElementById("<%=txtfieldname.ClientId %>").value = document.getElementById("<%=txtfieldname.ClientId %>").value.replace(" ", "");

                if (document.getElementById("<%=chkbox.ClientId %>").checked == true) {
                    // Primary Check Block
                    //					             if(document.getElementById("<%=prichkbox.ClientId %>").checked==true)
                    //					                 {

                    //					                      if(bool==true)
                    //					                      {
                    //					                      alert("You can't create more than one primary key")
                    //					                      return;
                    //					                      }
                    //					                       bool=true

                    if (document.getElementById("<%=txthidden.ClientId %>").value == "") {
                        document.getElementById("<%=txthidden.ClientId %>").value = document.getElementById("<%=txtfieldname.ClientId %>").value

                    }

                    else {

                        document.getElementById("<%=txthidden.ClientId %>").value = document.getElementById("<%=txthidden.ClientId %>").value + "," + document.getElementById("<%=txtfieldname.ClientId %>").value

                    }

                    setvisibletabsql();
                }
                //					             else
                //					                    {
                //					                       if(document.getElementById("<%=txthidden.ClientId %>").value=="")
                //					                         {
                //					                            document.getElementById("<%=txthidden.ClientId %>").value= document.getElementById("<%=txtfieldname.ClientId %>").value
                //		        
                //					                         }
                //					            
                //					                      else
                //					                        {
                //					            
                //					                         document.getElementById("<%=txthidden.ClientId %>").value=document.getElementById("<%=txthidden.ClientId %>").value +","+ document.getElementById("<%=txtfieldname.ClientId %>").value

                //					                         }
                //					                    setvisibletabsql();
                //					                    }    

                //}
                else {
                    //******************tablesql start*******************         

                    setvisibletabsql();
                    //*******************end tablesql*******************
                }


                // This Block iS For TableSql TextArea	
            }
            document.getElementById("<%=txtfieldname.ClientId %>").value = ""
            document.getElementById("<%=cbodatatype.ClientId %>").selectedIndex = 0
            document.getElementById("<%=txtsize.ClientId %>").value = ""
        }

        function setvisibletabsql() {


            if (document.getElementById("<%=txttablsql.ClientId %>").value == "") {


                if (document.getElementById("<%=prichkbox.ClientId %>").checked == true) {

                    //					            if(primcol=="")
                    //					            {
                    window.primcol = document.getElementById("<%=txtfieldname.ClientId %>").value;
                    document.getElementById("<%=PrimColValue.ClientId %>").value = window.primcol;


                    //					            }
                    //					            else
                    //					            {
                    //					            primcol = primcol + "," +document.getElementById("<%=txtfieldname.ClientId %>").value;
                    //					            alert(primcol);
                    //					            }

                    //					                  if(bool==true)
                    //					                      {
                    //					                      alert("You can't create more than one primary key")
                    //					                      return;
                    //					                      }
                    //					                       bool=true

                    if (document.getElementById("<%=cbodatatype.ClientId %>").value == "DATETIME" || document.getElementById("<%=cbodatatype.ClientId %>").value == "NUMERIC" || document.getElementById("<%=cbodatatype.ClientId %>").value == "FLOAT") {
                        document.getElementById("<%=txttablsql.ClientId %>").value = document.getElementById("<%=txtfieldname.ClientId %>").value + " " + document.getElementById("<%=cbodatatype.ClientId %>").value
                    }
                    else {
                        if (IsNumeric(document.getElementById("<%=txtsize.ClientId %>").value))
                            document.getElementById("<%=txttablsql.ClientId %>").value = document.getElementById("<%=txtfieldname.ClientId %>").value + " " + document.getElementById("<%=cbodatatype.ClientId %>").value + "(" + document.getElementById("<%=txtsize.ClientId %>").value + ")"
                        else {
                            alert("Please Enter Numeric value");
                            document.getElementById("<%=txtsize.ClientId %>").value = ""
                        }
                    }
                }

                else {
                    if (document.getElementById("<%=cbodatatype.ClientId %>").value == "DATETIME" || document.getElementById("<%=cbodatatype.ClientId %>").value == "NUMERIC" || document.getElementById("<%=cbodatatype.ClientId %>").value == "FLOAT")
                        document.getElementById("<%=txttablsql.ClientId %>").value = document.getElementById("<%=txtfieldname.ClientId %>").value + " " + document.getElementById("<%=cbodatatype.ClientId %>").value
                    else {
                        if (IsNumeric(document.getElementById("<%=txtsize.ClientId %>").value))
                            document.getElementById("<%=txttablsql.ClientId %>").value = document.getElementById("<%=txtfieldname.ClientId %>").value + " " + document.getElementById("<%=cbodatatype.ClientId %>").value + "(" + document.getElementById("<%=txtsize.ClientId %>").value + ")"
                        else {
                            alert("Please Enter Numeric value");
                            document.getElementById("<%=txtsize.ClientId %>").value = ""
                        }
                    }
                }
            }


            else {
                if (document.getElementById("<%=prichkbox.ClientId %>").checked == true) {
                    //					  var primcol="";
                    if (window.primcol == "") {
                        window.primcol = document.getElementById("<%=txtfieldname.ClientId %>").value;
                        document.getElementById("<%=PrimColValue.ClientId %>").value = window.primcol;
                    }
                    else {
                        window.primcol = window.primcol + "," + document.getElementById("<%=txtfieldname.ClientId %>").value;
                        document.getElementById("<%=PrimColValue.ClientId %>").value = window.primcol;
                    }
                    if (document.getElementById("<%=cbodatatype.ClientId %>").value == "DATETIME" || document.getElementById("<%=cbodatatype.ClientId %>").value == "NUMERIC" || document.getElementById("<%=cbodatatype.ClientId %>").value == "FLOAT")
                        document.getElementById("<%=txttablsql.ClientId %>").value = document.getElementById("<%=txttablsql.ClientId %>").value + "," + "\n" + document.getElementById("<%=txtfieldname.ClientId %>").value + " " + document.getElementById("<%=cbodatatype.ClientId %>").value
                    else {
                        if (IsNumeric(document.getElementById("<%=txtsize.ClientId %>").value))
                            document.getElementById("<%=txttablsql.ClientId %>").value = document.getElementById("<%=txttablsql.ClientId %>").value + "," + "\n" + document.getElementById("<%=txtfieldname.ClientId %>").value + " " + document.getElementById("<%=cbodatatype.ClientId %>").value + "(" + document.getElementById("<%=txtsize.ClientId %>").value + ")"
                        else {
                            alert("Please Enter Numeric value");
                            document.getElementById("<%=txtsize.ClientId %>").value = ""
                        }
                    }
                }
                else {
                    if (document.getElementById("<%=cbodatatype.ClientId %>").value == "DATETIME" || document.getElementById("<%=cbodatatype.ClientId %>").value == "NUMERIC" || document.getElementById("<%=cbodatatype.ClientId %>").value == "FLOAT") {
                        document.getElementById("<%=txttablsql.ClientId %>").value = document.getElementById("<%=txttablsql.ClientId %>").value + "," + "\n" + document.getElementById("<%=txtfieldname.ClientId %>").value + " " + document.getElementById("<%=cbodatatype.ClientId %>").value
                    }
                    else {
                        if (IsNumeric(document.getElementById("<%=txtsize.ClientId %>").value))
                            document.getElementById("<%=txttablsql.ClientId %>").value = document.getElementById("<%=txttablsql.ClientId %>").value + "," + "\n" + document.getElementById("<%=txtfieldname.ClientId %>").value + " " + document.getElementById("<%=cbodatatype.ClientId %>").value + "(" + document.getElementById("<%=txtsize.ClientId %>").value + ")"
                        else {
                            alert("Please Enter Numeric value");
                            document.getElementById("<%=txtsize.ClientId %>").value = ""
                        }
                    }
                }


            }
        }

        function getclientid() {

            if (document.frmtable.DepartmentName.selectedIndex != 0) {
                IDMS.AjaxClass.BindClient(document.frmtable.DepartmentName.value, filclient)
            }


        }

        function filclient(res) {
            var m = 0;
            for (m = document.frmtable.Clientname.length - 1; m > 0; m--) {
                document.frmtable.Clientname.remove(m)
            }
            if (res.value != "N") {
                var strdata = ""
                strdata = res.value;

                var strdataarr = strdata.split("$");
                var x;
                var y = 0;
                var str = ""
                for (x = 1; x <= strdataarr.length; x++) {
                    y = y + 1
                    strdvalue = strdataarr[x - 1].split("#");
                    if (y == 1) {
                        document.frmtable.Clientname.options[0] = new Option("---Select---", "Select");
                    }
                    document.frmtable.Clientname.options[x] = new Option(strdvalue[1], strdvalue[0]);
                }

            }



        }

        function getLOB() {

            if (document.frmtable.Clientname.selectedIndex != 0) {

                IDMS.AjaxClass.BindLOB(document.frmtable.DepartmentName.value, document.frmtable.Clientname.value, filLOB)
            }


        }

        function filLOB(res) {


            var m = 0;
            for (m = document.getElementById("ddlLobname").length - 1; m > 0; m--) {
                document.getElementById("ddlLobname").remove(m)
            }
            if (res.value != "N") {
                var strdata = ""
                strdata = res.value;

                var strdataarr = strdata.split("$");
                var x;
                var y = 0;
                var str = ""
                for (x = 1; x <= strdataarr.length; x++) {
                    y = y + 1
                    strdvalue = strdataarr[x - 1].split("#");
                    if (y == 1) {
                        document.getElementById("ddlLobname").options[0] = new Option("---Select---", "Select");
                    }
                    document.getElementById("ddlLobname").options[x] = new Option(strdvalue[1], strdvalue[0]);
                }

            }

        }

        function createtable_onclick() {

        }

				//-->
    </script>
 &nbsp;<table cellpadding="0" cellspacing="0" align="center" class="table" summary="purpose of this form is creating new table"
        width="80%">
        <%--<th>
							<asp:button id="btnclose" tabIndex="1" Runat="server" Width="10" CssClass="button" Text="X"></asp:button>
						</th>--%>
        <caption>
            Create New Table</caption>
        <tr>
            <th align="left" colspan="6" style="height: 16px">
            </th>
        </tr>
        <tr><td><table runat="server" class="table" id="spandisplay" visible="false" ><tr><td>
        <tr>
            <td class="label"><asp:Label ID="lblDept" runat="server" 
                    AssociatedControlID ="DepartmentName" Text=" Select Level 1" 
                    ToolTip="Department"></asp:Label></td>
            <td>
                <asp:DropDownList ID="DepartmentName"  runat="server" AutoPostBack="True" CssClass="dropdownlist"
                    ToolTip="Select Department" TabIndex="1">
                </asp:DropDownList>
            </td>
            <td colspan="3">
            </td>
        </tr>
        <tr>
            <td class="label"><asp:Label ID="lblClient" runat="server" 
                    AssociatedControlID ="Clientname" Text="Select Level 2" ToolTip="Client"/>
            </td>
            <td>
                <asp:DropDownList ID="Clientname"   runat="server" AutoPostBack="True" CssClass="dropdownlist"
                    ToolTip='"Select Client"' TabIndex="2">
                </asp:DropDownList>
            </td>
            <td colspan="3">
            </td>
        </tr>
        <tr>
            <td class="label" style="height: 20px"><asp:Label ID="lblLOB" runat="server" 
                    AssociatedControlID ="ddlLobname" Text="Select Level 3" ToolTip='"LOB"'/></td>
            <td style="height: 20px">
                <asp:DropDownList ID="ddlLobname"  runat="server" CssClass="dropdownlist" ToolTip='"Select Lob"'
                    TabIndex="3" AutoPostBack="True">
                </asp:DropDownList>
            </td>
            <td colspan="3" style="height: 20px">
            </td>
        </tr>
        </td>
        </tr>
        </table>
        </td>
        </tr>
        <tr>
            <td class="label"><asp:Label ID="lblTable" runat="server" AssociatedControlID ="txttable" Text="Table Name" ToolTip="Table"/>
            </td>
            <td>
                <input id="txttable" type="text" class="textbox" runat="server" title='"Enter Table Name"'
                    tabindex="4" style="width: 144px" maxlength="20" /></td>
            <td colspan="3">
                <%--<td><input onclick="addfield()" type="button"  value="Add field" class="button"/></td>--%>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txttable"
                    ErrorMessage="Table Name should be atleast of 4 Characters!" ValidationExpression=".{4,}"></asp:RegularExpressionValidator><br />
                &nbsp;</td>
        </tr>
        <tr>
            <%--<asp:TextBox runat="server" ID="txthidden" Rows="2" Columns ="60"  ></asp:TextBox>--%>
            <td style="height: 22px">
                <asp:CheckBox ID="chkSelectscope" Visible="false" runat="server" TabIndex="11" Text=" Local"
                    AutoPostBack="true" Font-Bold="True" Width="72px" />
            </td>
            <td colspan="3" style="height: 22px">
            </td>
        </tr>
        <tr>
            <td align="left"  colspan="6">
            </td>
        </tr>
        <tr>
            <td class="label" style="height: 20px"><asp:Label ID="lblColumn" runat="server" AssociatedControlID ="txtfieldname" Text="Column Name" ToolTip="Column Name"/>
            </td>
            <td class="label" style="height: 20px">
                <asp:Label ID="lblDatatype" runat="server" AssociatedControlID ="cbodatatype" Text="Data Type" ToolTip="Data Type"></asp:Label></td>
            <td class="label" style="height: 20px">
                <asp:Label ID="lblSize" runat="server" AssociatedControlID ="txtsize" Text="Size" ToolTip="Size"></asp:Label>
            </td>
            <td class="label" style="height: 20px">
                <asp:Label ID="lblVisible" runat="server" AssociatedControlID ="chkbox" Text="Visible" ToolTip="Visible"></asp:Label></td>
            <td class="label" style="height: 20px" title="Primary Key">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Primary</td>
            <%--<th>
							<asp:button id="btnclose" tabIndex="1" Runat="server" Width="10" CssClass="button" Text="X"></asp:button>
						</th>--%>
        </tr>
        <tr>
            <td style="height: 24px">
                <input id="txtfieldname" class="textbox" type="text" runat="server" title='"Enter Column Name"'
                    tabindex="5" maxlength="20" style="width: 257px" /></td>
            <td style="height: 24px">
                <select id="cbodatatype" name="cbodatatype" class="dropdownlist" runat="server" title='"Select DataType"'
                    tabindex="6">
                    <option value="" selected="selected">Select Datatype</option>
                    <option value="Varchar">VARCHAR</option>
                    <option value="DATETIME">DATE TIME</option>
                    <option value="NUMERIC">NUMERIC</option>
                    <option value="FLOAT">FLOAT</option>
                </select>
            </td>
            <td style="height: 24px; text-align: right;">
                <input id="txtsize" type="text" runat="server" title='"Enter Column Size  "'
                    tabindex="7" maxlength="4" style="width: 116px" /></td>
            <td align="center" style="height: 24px; width: 150px;">
                <input id="chkbox" type="checkbox" runat="server" title='"Make Column Visible/Unvisible"'
                    tabindex="8" /></td>
           <td align="center" style="height: 24px">
                    <input id ="prichkbox" type="checkbox" runat="server" title="Make Column Primary" tabindex="9" /></td>     
            <%--<td><input onclick="addfield()" type="button"  value="Add field" class="button"/></td>--%>
            <td style="height: 24px">
                <asp:Button ID="btnAddField" runat="server" CssClass="button" Text="Add Field" ToolTip='"Add Column Name[Alt+A]"'
                    Width="70" TabIndex="9" AccessKey="A" /></td>
        </tr>
        <tr>
            <td style="height: 12px">
                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtfieldname"
                    ErrorMessage="Field Name should be atleast of 2 Characters!" ValidationExpression=".{2,}"
                    Width="266px"></asp:RegularExpressionValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ControlToValidate="txtfieldname"
                    ErrorMessage="Field Name should Alphanumeric Only!" ValidationExpression="([A-Za-z0-9])*"
                    Width="247px"></asp:RegularExpressionValidator></td>
            
            <td colspan="3"  align="center">
                <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="txtsize"
                    ErrorMessage="Enter Numerics Only!" Operator="DataTypeCheck" Type="Integer"></asp:CompareValidator><br />
                <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="txtsize" Operator="GreaterThan" ValueToCompare="0" ErrorMessage="Size should be Greater then Zero"></asp:CompareValidator></td>
            
            <td style="height: 12px">
            </td>
        </tr>
        <tr>
            <td colspan="6" style="height: 88px">
                <textarea id="txttablsql" rows="5" runat="server" title='"All Column Name List"'
                    tabindex="10" cols="696" style="width: 699px"></textarea>
            </td>
        </tr>
        <tr>
            <td align="center" colspan="6" class="label"><asp:Label ID="lblvis" runat="server" Text="Visible Columns" ToolTip="Visible Columns"/>
            </td>
        </tr>
        <tr>
            <td colspan="6">
                <textarea id="txthidden" rows="2" name="txthidden" runat="server" title='"Visible Column List"'
                    cols="696" tabindex="11" style="width: 696px"></textarea>
                <%--<asp:TextBox runat="server" ID="txthidden" Rows="2" Columns ="60"  ></asp:TextBox>--%>
            </td>
        </tr>
        <tr>
            <td height="12">
            </td>
        </tr>
        <tr>
            <td align="center" colspan="6">
                <input id="createtable" type="button" value="Create" name="createtable" class="button"
                    runat="server" tabindex="12" title="Create Table[Alt+C]" accesskey="C" />
            </td>
        </tr>
    </table>
    <asp:Panel ID="divmsgbox" Style="z-index: 400; left: 450px; color: black; position: absolute;
        top: 430px; text-decoration: none" runat="server" BorderColor="Gray" BorderWidth="2px"
        BorderStyle="Solid" Width="200px" Height="66px" BackColor="#eeeeee" Visible="false" >
        <table id="tabmsgbox" cellspacing="0" cellpadding="0" width="250" border="0">
            <tr>
                <th class="tableHeader" style="width: 95%; height: 14px;">
                    <b>Message Box</b>
                </th>
                <%--<th>
							<asp:button id="btnclose" tabIndex="1" Runat="server" Width="10" CssClass="button" Text="X"></asp:button>
						</th>--%>
            </tr>
            <tr>
                <td height="10px" colspan="2">
                </td>
            </tr>
            <tr>
                <td colspan="2" style="text-align: center">
                    <b>Are You sure that you want to create the Table?</b></td>
            </tr>
            <tr>
                <td height="10px" colspan="2">
                </td>
            </tr>
            <tr>
                <td align="center" colspan="2">
                    <asp:Button ID="cmdyes_multiuser" runat="server" CssClass="button" Text="Yes" ToolTip='"Create Table[Alt+Y]"'
                        TabIndex="11" AccessKey="Y"></asp:Button>
                    <asp:Button ID="cmdyes_singleuser" runat="server" AccessKey="Y" 
                        CssClass="button" TabIndex="11" Text="Yes" 
                        ToolTip="&quot;Create Table[Alt+Y]&quot;" />
                    &nbsp;
                    <asp:Button ID="cmdno" runat="server" CssClass="button" Text="No" ToolTip="Click on it if don't want to create table[Alt+N]"
                        TabIndex="12" AccessKey="N"></asp:Button></td>
            </tr>
        </table>
    </asp:Panel>
    <asp:HiddenField ID="PrimColValue"  runat="server" />
</asp:Content>

