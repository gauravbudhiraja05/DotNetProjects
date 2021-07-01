<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="false" CodeFile="EditOntable.aspx.vb" Inherits="TableTools_EditOntable" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .style1
        {
            width: 293px;
        }
        .style2
        {
            height: 20px;
            width: 293px;
        }
        .style3
        {
            font-family: Verdana,tahoma,sans-serif;
            text-align: left;
            font-size: 11px;
            width: 150px;
            font-weight: bold;
            height: 19px;
            color: #000000;
            letter-spacing: 0.01em;
        }
        .style4
        {
            font-family: Verdana,tahoma,sans-serif;
            text-align: left;
            font-size: 11px;
            width: 150px;
            font-weight: bold;
            height: 23px;
            color: #000000;
            letter-spacing: 0.01em;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="LeftPlaceHolder" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <link rel="stylesheet" type="text/css" href="../App_Themes/Themes/StyleSheet.css" />
    <script language="javascript" type="text/javascript">
        
        function gettab() {

                AjaxClass1.bindtable(filtab)

        }
        function filtab(res) {

            if (res.value != null) {
                for (i = document.getElementById("<%=ddlTablename.ClientId %>").length; i >= 0; i--) {
                    document.getElementById("<%=ddlTablename.ClientId %>").remove(i);
                }

                var strdata = res.value
                var arrdata = strdata.split("$")
                var arrdata1
                document.getElementById("<%=ddlTablename.ClientId %>").options[0] = new Option("---select---", "0");
                for (i = 0; i < arrdata.length; i++) {
                    arrdata1 = arrdata[i].split("#")
                    document.getElementById("<%=ddlTablename.ClientId %>").options[i + 1] = new Option(arrdata1[1], arrdata1[0] + "$" + arrdata1[1]);
                }

            }

        }


    </script>

    <table cellspacing="0" cellpadding="0" class="table" style="left: 10" border="1">
        <tr>
            <th class="tableHeader" colspan="6" align="center" style="color:White">
                Edit/Delete Table</th>
        </tr>
        <tr>
            <td colspan="4" height="15px">
             <table id="spandisplay" runat="server" visible="false" class="table">
                <tr>
            <td class="style3"><asp:Label ID="lblDept" runat="server" 
                    AssociatedControlID ="DepartmentName" Text=" Select Level 1" 
                    ToolTip="Department"></asp:Label></td>
            <td class="style1">
                <asp:DropDownList ID="DepartmentName"  runat="server" AutoPostBack="True" CssClass="dropdownlist"
                    ToolTip="Select Department" TabIndex="1">
                </asp:DropDownList>
            </td>
           
        </tr>
        <tr>
            <td class="style4"><asp:Label ID="lblClient" runat="server" 
                    AssociatedControlID ="Clientname" Text="Select Level 2" ToolTip="Client"/>
            </td>
            <td class="style1">
                <asp:DropDownList ID="Clientname"   runat="server" AutoPostBack="True" CssClass="dropdownlist"
                    ToolTip='"Select Client"' TabIndex="2">
                </asp:DropDownList>
            </td>
           </tr> 
           <tr>
            <td class="label" style="height: 20px"><asp:Label ID="lblLOB" runat="server" 
                    AssociatedControlID ="ddlLobname" Text="Select Level 3" ToolTip='"LOB"'/></td>
            <td class="style2">
                <asp:DropDownList ID="ddlLobname"  runat="server" CssClass="dropdownlist" ToolTip='"Select Lob"'
                    TabIndex="3" AutoPostBack="True">
                </asp:DropDownList>
            </td>
            <td colspan="3" style="height: 20px">
            </td>
        </tr>
        </table>
        </td>
        </tr>
        <tr>
            <td colspan="3"><asp:Button  runat="server" ID="gettable" CssClass="button" 
                    Visible="false" Text="Get Table"/>
            </td>
        </tr>
         <tr>

            <td style="color:Black">
                <asp:Label ID="lblTablename" runat="server" CssClass="label">Table Name</asp:Label>
            </td>
            <td>   <label for="ctl00_MainPlaceHolder_ddlTablename"></label>
                <asp:DropDownList ID="ddlTablename" TabIndex="4" runat="server" CssClass="dropdownlist"
                    AutoPostBack="true" ToolTip="Select Table Name">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td colspan="4" height="15px">
            </td>
        </tr>
        <tr>
            <td colspan="4" align="center">
                <input type="button" id="cmdDeleterecord" runat="server" value="Delete Records" class="button"
                    name="cmdDeleterecord" title="Delete Record From Table[Alt+D]" accesskey="D"
                    tabindex="5" visible="false" onserverclick="cmdDeleterecord_ServerClick" />
                <input type="button" id="cmdDeleteTable" runat="server" value="Delete Table" class="button"
                    name="cmdDeleteTable" title="Delete Table[Alt+T]" accesskey="T" tabindex="6" visible="false" />
                <input type="button" id="cmdchastrutable" runat="server" value="Alter Table" class="button"
                    name="cmdchastrutable" title="Update Table Structure[Alt+U]" accesskey="U" tabindex="7" visible="false" />
                <asp:Button ID="cmdAddColumn" runat="server" CssClass="button" Text="Add Column"
                    ToolTip="Add Column{alt+A " AccessKey="A" TabIndex="8" Visible="False" />
            </td>
        </tr>
    </table>
    <table cellspacing="0" cellpadding="0" class="table" style="left: 10">
        <tr>
            <td>
                <div id="divDatalist" runat="server" visible="false">
                    <table width="100%">
                        <tr>
                            <th class="tableHeader" align="center" style="color:White">
                                Alter Table
                            </th>
                        </tr>
                        <tr>
                            <td align="center">
                                <asp:DataList ID="dlreg" runat="server" BorderWidth="2px" CssClass="datagrid" DataKeyField="colname"
                                    Width="99%">
                                    <HeaderTemplate>
                                      
                                        <tr class="datagridHeader">
                                            <td width="250px" align="left" style="color:Black">
                                                <strong>Column Name</strong></td>
                                            <td width="170px" align="left" style="color:Black">
                                                <strong>Data Type</strong></td>
                                            <td width="80px" align="left" style="color:Black">
                                                <strong>Size</strong></td>
                                            <td width="50px" style="color:Black">
                                                <strong>Primary Key</strong></td>
                                            
                                        </tr>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <tr>
                                            <td align="left">
                                                <asp:Label ID="Label1" runat="server" Text='<%#(container.dataitem("colname"))%>'
                                                    Visible="<%#IsNotEditable %>" Width="20px">
                                                </asp:Label>
                                                <asp:Label ID="txtCol" runat="server" Text='<%#(container.dataitem("colname"))%>'
                                                    Visible="<%#IsEditable %>" Width="80px">
                                                </asp:Label>
                                                </td>
                                            <td align="left">
                                                <asp:Label ID="Label2" runat="server" Text='<%#(container.dataitem("type"))%>' Visible="<%#IsNotEditable %>"
                                                    Width="20px">
                                                </asp:Label>
                                                <asp:Label runat="server" AssociatedControlID="cboEx" ID="lblcboEx"></asp:Label>
                                                <asp:DropDownList ID="cboEx" runat="server" DataSource="<%# GetdataType() %>" DataTextField="type"
                                                    Visible="<%#IsEditable %>" Width="100px">
                                                </asp:DropDownList>
                                               </td>
                                            <td align="left">
                                                <asp:Label ID="lblLN" runat="server" Text='<%#(container.dataitem("size"))%>' Visible="<%#IsNotEditable %>"
                                                    Width="30px">
                                                </asp:Label>
                                                  <asp:Label runat="server" AssociatedControlID="txtsize" ID="lbltxtsize"></asp:Label>
                                                <asp:TextBox ID="txtsize" runat="server" Text='<%#(container.dataitem("size"))%>'
                                                    Visible="<%#IsEditable %>" Width="80px">
                                                </asp:TextBox></td>
                                            <td align="center">
                                            <asp:Label runat="server" AssociatedControlID="chkprimary" ID="lblchkprimary"></asp:Label>
                                                <asp:CheckBox ID="chkprimary" runat="server" />
                                            </td>
                                         
                                    </ItemTemplate>
                                    
                                    
                        
                                    <FooterTemplate>
                                        <tr><td height="13px"></td></tr>
                                        <tr>
                                        
                                            <td align ="center" >
                                                <asp:Button ID="cmdEdit" runat="server" CommandName="Edit"  CssClass="button"  Text="Edit" ToolTip="Edit Field"
                                                    Visible="<%#IsNotEditable %>" />
                                            
                                                <asp:Button ID="cmdUpdate" runat="server" AccessKey="U"  CssClass="button"  CommandName="Update" Font-Italic="True"
                                                    Text="Update" ToolTip=" Update Field[Ctrl+U]" Visible="<%#IsEditable %>" />
                                            
                                                <asp:Button ID="cmdCancel" runat="server" AccessKey="C" CssClass="button" CommandName="Cancel" Font-Italic="True"
                                                    Text="Cancel" ToolTip="Cancel Update Field[Ctrl+C] " Visible="<%#IsEditable %>" />
                                            </td>
                                        </tr>
                                        </table>
                                    </FooterTemplate>
                                </asp:DataList>
                            </td>
                            <%--<tr>
                                <td align="center">
                                    <asp:Button ID="cmdEdit" runat="server" CommandName="Edit" Text="Edit" ToolTip="Edit Field"
                                        Width="30px" />
                                    <asp:Button ID="cmdUpdate" runat="server" AccessKey="U" CommandName="Update" Font-Italic="True"
                                        Text="Update" ToolTip=" Update Field[Ctrl+U]" />
                                    <asp:Button ID="cmdCancel" runat="server" AccessKey="C" CommandName="Cancel" Font-Italic="True"
                                        Text="Cancel" ToolTip="Cancel Update Field[Ctrl+C] " />
                                </td>
                            </tr>--%>
                        </tr>
                        <%--<tr>
                            <td align="center" >
                                <asp:Label ID="scope" Text="Scope" CssClass="label" runat="server" />
                                <label for="ctl00_MainPlaceHolder_chkscope"></label>
                                &nbsp;<asp:CheckBox ID="chkscope" runat="server" AutoPostBack="true" ToolTip="Making Local" />
                            </td>
                        </tr>--%>
                        <tr>
                            <td align="center" style="height: 24px">
                                <asp:Button ID="btnclosedatalist" runat="server" CssClass="button" Text="Close" ToolTip="Close Alter Table " /></td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
        <tr>
            <td>
                <div id="dvAddColumn" runat="server">
                    <table width="100%">
                        <%--<caption>Add Column</caption>--%>
                        <tr>
                            <th class="tableHeader" colspan="6" align="center">
                                Add Column</th>
                        </tr>
                        <tr>
                            <td class="label" style="width: 115px">
                                Table
                            </td>
                            <td colspan="5">
                              <label for="ctl00_MainPlaceHolder_txtTbalename"></label>
                                <input id="txtTbalename" type="text" readonly runat="server" name="txtTbalename"
                                    title="Selected Table Name" tabindex="9" style="width: 147px" />
                            </td>
                        </tr>
                        <tr>
                            <td class="label" style="width: 115px">
                                Column
                            </td>
                            <td style="width: 203px">
                             <label for="ctl00_MainPlaceHolder_NewColName"></label>
                                <input type="text" id="NewColName" runat="server" tabindex="10" title="Enter New Column Name"
                                    style="width: 148px" />
                            </td>
                            <td class="label" style="width: 85px; text-align: right">
                                DataType
                            </td>
                            <td style="width: 71px">
                              <label for="ctl00_MainPlaceHolder_cbodatatype"></label>
                                <select id="cbodatatype" name="cbodatatype" class="dropdownlist" runat="server" tabindex="11"
                                    title="Select DataType">
                                    <option value="selected">Select Datatype</option>
                                    <option value="Varchar">varchar</option>
                                    <option value="DATETIME">datetime</option>
                                    <option value="NUMERIC">numeric</option>
                                    <option value="FLOAT">float</option>
                                </select>
                            </td>
                            <td class="label" style="width: 108px; text-align: right">
                                Size
                            </td>
                            <td><label for="ctl00_MainPlaceHolder_txtsize"></label>
                                <input id="txtsize" type="text" runat="server" name="txtsize" maxlength="4" size="5"
                                    tabindex="12" title="Enter Size Column" />
                            </td>
                        </tr>
                        <tr>
                            <td class="label" style="width: 115px">
                                Default Value
                            </td>
                            <td><label for="ctl00_MainPlaceHolder_txtdefault"></label>
                                <input id="txtdefault" type="text" runat="server" name="txtDefault" tabindex="13"
                                    title="Defalut Value For Date" />
                            </td>
                            <td class="label" colspan="4">
                                For DateTime(mm/dd/yy)
                            </td>
                        </tr>
                        <tr>
                            <td align="center" colspan="3">
                                <input type="button" id="Add" runat="server" value="Add" class="button" name="Add"
                                    title="Add More Column" />&nbsp;&nbsp;
                                <input type="button" id="Close" runat="server" value="Close" class="button" name="Close"
                                    title="Exit From Add More Column" />
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
    </table>
    <asp:Panel ID="divmsgboxdelrecord" BorderColor="Gray" BorderStyle="Groove" BorderWidth="1px"
        BackColor="#eeeeee" Style="z-index: 101; left: 392px; position: absolute; top: 420px;
        text-decoration: none" runat="server">
        <table id="tabmsgbox" width="250">
            <tr class="tableHeader">
                <th colspan="2">
                    <strong>Message Box</strong></th>
                    
                
            </tr>
            <tr>
                <td align="left" colspan="2" style="height: 34px">
                    Are you sure to delete the record of this table</td>
            </tr>
            <tr>
                <td align="center" colspan="2">
                    <asp:Button ID="cmdyesr" runat="server" Text="Yes" CssClass="button"></asp:Button>&nbsp;
                    <asp:Button ID="cmdnor" runat="server" Text="No" CssClass="button"></asp:Button>
                </td>
            </tr>
        </table>
    </asp:Panel>
    <asp:Panel ID="divmsgboxdeltable" BorderColor="Gray" BorderStyle="Groove" BorderWidth="1px"
        BackColor="#eeeeee" Style="z-index: 102; left: 392px; position: absolute; top: 420px;
        text-decoration: none" runat="server">
        <table id="tabmsgboxdeltable" width="250">
            <tr>
                <th class="tableHeader" style="width: 95%">
                    <strong>Message Box</strong></th>
                
            </tr>
            <tr>
                <td align="left" colspan="2" style="height: 14px; text-align: center">
                    <strong>Are you sure that you want to delete this table?</strong></td>
            </tr>
            <tr>
                <td align="center" colspan="2" style="height: 24px">
                    <asp:Button ID="cmdyesdt" runat="server" Text="Yes" CssClass="button"></asp:Button>&nbsp;
                    <asp:Button ID="cmdnodt" runat="server" Text="No" CssClass="button"></asp:Button>
                </td>
            </tr>
        </table>
    </asp:Panel>
    <asp:TextBox ID="txttablename" runat="server" Visible="False"></asp:TextBox>
    <asp:TextBox ID="txtclient" runat="server" Visible="False"></asp:TextBox>
    <asp:TextBox ID="txtlob" runat="server" Visible="False"></asp:TextBox>
</asp:Content>

