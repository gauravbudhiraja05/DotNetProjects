<%@ Page Language="VB" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="false"
    CodeFile="Search.aspx.vb" Inherits="Accounts_Search" Title="Search/Edit/Delete" %>

<asp:Content ID="Content1" ContentPlaceHolderID="LeftPlaceHolder" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
<link rel="stylesheet" type="text/css" href="../App_Themes/Themes/StyleSheet.css" />


    <table cellspacing="0" cellpadding="0" width="90%" align="center" class="table" summary="purpose of this page is, searching information about member">
        <tr>
            <td style="background-color: #CC6666; height: 22px; width: 10px" scope="col">
                <img src="../images/MemSearch.jpg"  alt="Member Search" />
            </td>
            <td style="background-color: #CC6666; height: 22px; width: 10px" colspan="2" scope="colgroup">
            </td>
            <td style="background-color: #CC6666; height: 22px; width: 350px" align="right" valign="bottom" scope="col">
                <span style="font-size: 12pt; color: White;"><strong>Member Data - Search/Edit/Delete&nbsp;&nbsp;&nbsp;</strong></span>
            </td>
        </tr>
        <tr>
            <td style="background-color: #df9f9f; height: 5px" colspan="4" scope="colgroup">
            </td>
        </tr>
        <tr>
            <td style="height: 12px" scope="col">
            </td>
        </tr>
        <tr>
            <td valign="top" class="label" scope="col" style="color: black">
             <label for="ctl00_MainPlaceHolder_cboSearchCriteria" >Search Criteria</label>
                
            </td>
            <td scope="col">
            
                <asp:DropDownList ID="cboSearchCriteria" TabIndex="1" runat="server"
                    CssClass="dropdownlist" ToolTip="Select Criteria">
                    <asp:ListItem Value="--Select--">--Select--</asp:ListItem>
                    <asp:ListItem Value="userid">UserId</asp:ListItem>
                    <asp:ListItem Value="UserName">UserName</asp:ListItem>
                    <asp:ListItem Value="EMPId">EMPId</asp:ListItem>
                    <asp:ListItem Value="Designation">Designation</asp:ListItem>
                    <asp:ListItem Value="BU">BU</asp:ListItem>
                    <asp:ListItem Value="Email">Email</asp:ListItem>
                    <asp:ListItem Value="LOBID">LOB</asp:ListItem>
                    <asp:ListItem Value="DeptId">Department</asp:ListItem>
                    <asp:ListItem Value="ClientId">Client</asp:ListItem>
                    <asp:ListItem Value="Search All">Search All</asp:ListItem>
                    <asp:ListItem Value ="Resigned">Resigned</asp:ListItem>
                    <asp:ListItem Value="Transfered">Transfered</asp:ListItem>
                </asp:DropDownList>
                &nbsp;
            </td>
            <td valign="top" class="label" style="color: black;text-align: center" scope="col">
                 <label for="ctl00_MainPlaceHolder_txtSearch" >Type Text</label></td> 
            <td valign="top" scope="col">
                <asp:TextBox ID="txtSearch" TabIndex="2" runat="server" MaxLength="50" Width="265px"
                    ToolTip="Enter Text For Search"></asp:TextBox>&nbsp;<asp:ImageButton ID="ImgBtnSearch"
                        runat="server" Width="20px" BorderWidth="0px" ToolTip="Search Record [alt+r]"
                        ImageAlign="AbsBottom" ImageUrl="../Images/Search.gif" Height="20px" AlternateText="search"
                        TabIndex="3" AccessKey="r"></asp:ImageButton>
            </td>
        </tr>
        <tr>
            <td colspan="4" style="height: 12px" scope="colgroup">
                <asp:Label ID="lblMsgBox" runat="server" Width="424px" ForeColor="#C00000" Font-Bold="True"
                    Font-Italic="True"></asp:Label>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ForeColor="#C00000" runat="server" ControlToValidate="cboSearchCriteria"
                    Display="Dynamic" ErrorMessage="Select Search Criteria" InitialValue="--Select--" Width="136px" Font-Bold="True" Font-Italic="True"></asp:RequiredFieldValidator></td>
        </tr>
        <tr>
            <td colspan="4" scope="colgroup">
                <asp:Label ID="lblLink" runat="server" Width="424px" ForeColor="navy" Font-Bold="True"
                    Font-Italic="True"></asp:Label>
            </td>
        </tr>
        <%-- <asp:DropDownList ID="cboState" TabIndex="10" runat="server" Width="216px" Visible="false">
        </asp:DropDownList>--%>
        <tr>
            <td colspan="4" scope="colgroup" style="color: black; height: 408px;">
                <asp:DataGrid ID="grdSearchMember" runat="server" CellPadding="0" DataKeyField="RecId"
                     ShowFooter="True" PageSize="15" AllowPaging="True"
                    AllowSorting="True" AutoGenerateColumns="False" CssClass="datagrid" TabIndex="4">
                    <SelectedItemStyle Wrap="False" ForeColor="Maroon"></SelectedItemStyle>
                    <AlternatingItemStyle Wrap="False"></AlternatingItemStyle>
                    <ItemStyle></ItemStyle>
                    <HeaderStyle CssClass="datagridHeader"></HeaderStyle>
                    <FooterStyle CssClass="datagridHeader"></FooterStyle>
                    <Columns>
                        <asp:TemplateColumn>
                            <HeaderStyle Wrap="False" Width="10%" CssClass="Head"></HeaderStyle>
                            <ItemStyle Wrap="False"></ItemStyle>
                            <HeaderTemplate>
                                <asp:LinkButton ID="lnkusername" runat="server" Text="User Name" CommandName="Detail" />
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkname" runat="server" CausesValidation="false" CommandName="Details1">
												<%#Container.DataItem("username")%>
                                </asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <%--<asp:BoundColumn DataField="UserName" SortExpression="UserName" HeaderText="UserName">
                            <HeaderStyle Wrap="False" CssClass="Head"></HeaderStyle>
                            <ItemStyle Wrap="False"></ItemStyle>
                        </asp:BoundColumn>--%>
                        <asp:BoundColumn DataField="UserId" SortExpression="UserId" HeaderText="UserId">
                            <HeaderStyle Wrap="False" CssClass="Head"></HeaderStyle>
                            <ItemStyle Wrap="False"></ItemStyle>
                        </asp:BoundColumn>
                       <%-- <asp:BoundColumn DataField="empid" SortExpression="empid" HeaderText="EMPId">
                            <HeaderStyle Wrap="False" CssClass="Head"></HeaderStyle>
                            <ItemStyle Wrap="False"></ItemStyle>
                        </asp:BoundColumn>--%>
                        <asp:BoundColumn DataField="Designation" SortExpression="Designation" HeaderText="Designation">
                            <HeaderStyle Wrap="False" CssClass="Head"></HeaderStyle>
                            <ItemStyle Wrap="False"></ItemStyle>
                        </asp:BoundColumn>
                       <%-- <asp:BoundColumn DataField="BU" SortExpression="BU" HeaderText="BU">
                            <HeaderStyle Wrap="False" CssClass="Head" Width="30%"></HeaderStyle>
                            <ItemStyle Wrap="False"></ItemStyle>
                        </asp:BoundColumn>--%>
                        <asp:BoundColumn DataField="Status" SortExpression="Status" HeaderText="Status">
                            <HeaderStyle Wrap="False" CssClass="Head"></HeaderStyle>
                            <ItemStyle Wrap="False"></ItemStyle>
                        </asp:BoundColumn>
                        <asp:TemplateColumn  >
                                <HeaderTemplate >
                                    <asp:Label ID="lblHStatus" runat="server" Text="Working Status"   />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblStatus" runat="server"  Text='<%#showStatus(Eval("lockReason"))%>' />
                                </ItemTemplate>
                                
                            </asp:TemplateColumn>
                
                        <asp:BoundColumn DataField="addDate" SortExpression="addDate" HeaderText="CreatedDate">
                            <HeaderStyle Wrap="False" CssClass="Head"></HeaderStyle>
                            <ItemStyle Wrap="False"></ItemStyle>
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="createdBy" SortExpression="createdBy" HeaderText="CreatedBy">
                            <HeaderStyle Wrap="False" CssClass="Head"></HeaderStyle>
                            <ItemStyle Wrap="False"></ItemStyle>
                        </asp:BoundColumn>
                        <asp:TemplateColumn>
                            <HeaderStyle Wrap="False" CssClass="Head"></HeaderStyle>
                            <ItemStyle Wrap="False"></ItemStyle>
                            <HeaderTemplate>
                                <center>
                                    <asp:LinkButton ID="lkbtnSelectAll" runat="server" CommandName="Select All" Visible="True">
												<font size="0">Select All/</font></asp:LinkButton>
                                    <asp:LinkButton ID="lkbtnDeSelectAll" runat="server" CommandName="DeSelect All" Visible="True">
												<font size="0">DeSelect All</font></asp:LinkButton>
                                </center>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <center>
                                		<asp:Label ID="lbchkvisible" AssociatedControlID ="chkSelect" runat ="server"  ></asp:Label>
                                    <asp:CheckBox ID="chkSelect" runat="server"></asp:CheckBox>
                                </center>
                            </ItemTemplate>
                            <FooterTemplate>
                                <center>
                                    <asp:Button ID="btnDelete" CausesValidation="false" runat="server" ToolTip="Delete User" CommandName="Delete" CssClass="button"
                                        Text="Delete"></asp:Button>
                                </center>
                            </FooterTemplate>
                        </asp:TemplateColumn>
                    </Columns>
                    <PagerStyle ForeColor="Black" Wrap="False" Mode="NumericPages"></PagerStyle>
                </asp:DataGrid>
            </td>
        </tr>
        <%If Session("UserId") = "" Then%>
        <tr>
            <td colspan="4" style="height: 71px" scope="colgroup">
                <asp:TextBox ID="txtSearchcr" runat="server" Visible="False"></asp:TextBox>&nbsp;
                <asp:TextBox ID="txtUser" runat="server" Visible="False"></asp:TextBox><asp:TextBox
                    ID="txtHidRecordId" runat="server" Visible="False"></asp:TextBox><asp:ListBox ID="lstRecId"
                        runat="server" Visible="False"></asp:ListBox>
            </td>
        </tr>
        <%End If%>
        <tr>
            <td colspan="4" style="height: 372px" scope="colgroup">
                <asp:Panel ID="PnlRegDetails" Width="100%" Height="100%" Visible="False" runat="server"
                    BorderStyle="Double" BorderColor="Gray">
                    <table id="tabMainRegistrationTable" cellspacing="0" cellpadding="0" width="100%" align="center">
                        <tbody>
                            <tr class="tableHeader">
                                <th align="left" colspan="2" scope="colgroup" style="height: 14px;background-color:#0591D3">
                                   <strong>Login Information</strong>
                                </th>
                            </tr>
                            <tr>
                                <td style="width: 163px; height: 3px" class="label" scope="col">
                                </td>
                                <td style="height: 3px" class="label" scope="col">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr style="visibility: hidden; position: absolute" >
                                <td class="label" scope="col">
                                    <asp:Label ID="lblUserType" runat="server" style ="color:black"   Text="User Type" />
                                    <asp:Label ID="lbls1" runat="server" ForeColor="#C00000">*</asp:Label>
                                </td>
                                <td scope="col">
                                <label for="ctl00_MainPlaceHolder_cboUserType" ></label>
                                    <asp:DropDownList ID="cboUserType" TabIndex="5" runat="server" CssClass="dropdownlist">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td class="label" style="height: 28px; width: 163px;color:black" scope="col"   >
                                    <asp:Label ID="lblUserId" runat="server" Text="User Id" />
                                    <asp:Label ID="lblls2" runat="server" ForeColor="#C00000">*</asp:Label>
                                </td>
                                <td style="height: 28px" scope="col">
                                  <label for="ctl00_MainPlaceHolder_txtUserId" ></label>
                                    <asp:TextBox ID="txtUserId" TabIndex="2" MaxLength="20" runat="server" ReadOnly="True"
                                        CssClass="textBox"></asp:TextBox>&nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 163px; height: 1px" scope="col">
                                </td>
                                <td style="height: 1px" scope="col">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <th align="left" colspan="2" class="tableHeader" style ="background-color:#0591D3" >
                                    <strong>Personal&nbsp;Information</strong>
                                </th>
                            </tr>
                            <tr>
                                <td style="width: 163px; height: 15px" scope="col">
                                </td>
                                <td style="height: 15px" scope="col">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td class="label" style="width: 163px; color : Black " scope="col">
                                    &nbsp;Name
                                    <asp:Label ID="lblName" runat="server" ForeColor="#C00000">*</asp:Label>
                                </td>
                                <td scope="col">
                                 <label for="ctl00_MainPlaceHolder_cboPrefix" ></label>
                                    <asp:DropDownList ID="cboPrefix" TabIndex="6" runat="server" Width="38px" CssClass="dropdownlist">
                                        <asp:ListItem Value="Mr.">Mr.</asp:ListItem>
                                        <asp:ListItem Value="Miss">Miss</asp:ListItem>
                                        <asp:ListItem Value="Mrs.">Mrs.</asp:ListItem>
                                    </asp:DropDownList>
                                     <label for="ctl00_MainPlaceHolder_txtName" ></label>
                                    <asp:TextBox ID="txtName" TabIndex="7" Width="100px" MaxLength="50" runat="server"
                                        CssClass="textBox" ToolTip="Enter Name"></asp:TextBox>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtName"
                                        ErrorMessage="Enter Alphabets Only!" ValidationExpression="([a-z A-Z \.\s])*"
                                        Width="124px" Display="dynamic"></asp:RegularExpressionValidator><asp:RequiredFieldValidator
                                            ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtName" ErrorMessage="Employee Name is required"
                                            Width="155px" Display="dynamic"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td scope="col" class="label" style="width: 163px; height: 28px; color :Black ">
                                    &nbsp;Employee Id
                                    <asp:Label ID="lblCountry0" runat="server" ForeColor="#C00000">*</asp:Label>
                                </td>
                                <td style="height: 28px" scope="col">
                                 <label for="ctl00_MainPlaceHolder_txtEmpId" ></label>
                                    <asp:TextBox ID="txtEmpId" TabIndex="7" Width="142px" MaxLength="50" runat="server"
                                        CssClass="textBox" ToolTip="Enter Employee ID" Enabled="False"></asp:TextBox>
                                    <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="txtEmpId"
                                        ErrorMessage="Enter Numerics Only!" Operator="DataTypeCheck" Type="Integer" Width="125px"
                                        Display="dynamic"></asp:CompareValidator><asp:RequiredFieldValidator ID="RequiredFieldValidator1"
                                            runat="server" ControlToValidate="txtEmpId" ErrorMessage="Employee Id is required"
                                            Width="152px" Display="dynamic"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td class="label" style="width: 163px; color :Black " scope="col">
                                    &nbsp;Designation
                                </td>
                                <td scope="col">
                                <label for="ctl00_MainPlaceHolder_cboDesignation" ></label>
                                    <asp:DropDownList ID="cboDesignation" TabIndex="8" runat="server" CssClass="dropdownlist">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr style="visibility: hidden">
                                <td style="width: 163px; text-align: right; height: 32px; color : Black " scope="col">
                                    <asp:Label ID="Label2" runat="server" Width="80px" Font-Italic="True" Font-Bold="True"
                                        ForeColor="#C00000">If Not in list</asp:Label>
                                </td>
                                <td style="height: 32px" scope="col">
                                <label for="ctl00_MainPlaceHolder_txtDesig" ></label>
                                    <asp:TextBox ID="txtDesig" TabIndex="9" runat="server" Width="143px" MaxLength="50"
                                        CssClass="textBox" ToolTip="Enter Designation"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="label" style="width: 163px; color :Black " scope="col">
                                    &nbsp;Department
                                    <asp:Label ID="Label5" runat="server" ForeColor="#C00000">*</asp:Label>
                                </td>
                                <td scope="col">
                                <label for="ctl00_MainPlaceHolder_DepartmentName" ></label>
                                    <asp:DropDownList ID="DepartmentName" TabIndex="10" runat="server" AutoPostBack="True"
                                        CssClass="dropdownlist">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td class="label" style="width: 163px; color : Black " scope="col">
                                    &nbsp;Client
                                </td>
                                <td scope="col">
                                <label for="ctl00_MainPlaceHolder_ClientName" ></label>
                                    <asp:DropDownList ID="ClientName" TabIndex="11" runat="server" AutoPostBack="True"
                                        CssClass="dropdownlist">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td class="label" style="height: 20px; width: 163px; color : Black " scope="col">
                                    &nbsp;LOB
                                </td>
                                <td scope="col">
                                <label for="ctl00_MainPlaceHolder_cboLOBDept" ></label>
                                    <asp:DropDownList ID="cboLOBDept" TabIndex="12" runat="server" CssClass="dropdownlist">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td class="label" scope="col" style="height: 22px; width: 163px; color : Black ">
                                    &nbsp;BU
                                    </td>
                                <td style="height: 22px" scope="col">
                                <label for="ctl00_MainPlaceHolder_CBOBU" ></label>
                                    <asp:DropDownList ID="CBOBU" TabIndex="13" runat="server" CssClass="dropdownlist">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr style="visibility: hidden">
                                <td style="width: 163px; height: 19px; text-align: right;" scope="col">
                                    <asp:Label ID="lblNil" runat="server" Width="80px" Font-Italic="True" Font-Bold="True"
                                        ForeColor="#C00000">If Not in list</asp:Label>
                                </td>
                                <td style="height: 19px" scope="col">
                                  <label for="ctl00_MainPlaceHolder_txtBU" ></label>
                                    &nbsp;<asp:TextBox ID="txtBU" TabIndex="14" runat="server" CssClass="textBox" MaxLength="50" ToolTip="Enter BU"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 163px" scope="col">
                                    <asp:Label ID="lblEmail" runat="server" Text="Email" style ="color:black" CssClass="label"></asp:Label>
                                    &nbsp;<asp:Label ID="lblCountry" runat="server" ForeColor="#C00000">*</asp:Label>
                                </td>
                                <td scope="col">
                                 <label for="ctl00_MainPlaceHolder_txtEmail" ></label>
                                    <asp:TextBox ID="txtEmail" TabIndex="15" runat="server" MaxLength="50" CssClass="textBox" ToolTip="Enter Email"></asp:TextBox>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Fill Valid Email Id"
                                        ControlToValidate="txtEmail" ValidationExpression="^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$"
                                        Width="117px" Display="dynamic"></asp:RegularExpressionValidator>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtEmail"
                                        ErrorMessage="Employee Email is Required" Display="dynamic"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td align="center" colspan="2" scope="colgroup" class="label" style="height: 20px">
                                </td>
                            </tr>
                            <tr>
                                <td align="center" colspan="2" style="height: 8px" scope="colgroup">
                                    <asp:Button ID="cmdUpdate" TabIndex="16" runat="server" CssClass="button" Text="Update"
                                        ToolTip="Update Record [alt+u]" AccessKey="u"></asp:Button>&nbsp;
                                    <asp:Button ID="cmdCancel" TabIndex="17" runat="server" CssClass="button" Text="Cancel"
                                        ToolTip="Cancel update [alt+c]" AccessKey="c" CausesValidation="False"></asp:Button>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </asp:Panel>
            </td>
        </tr>
    </table>
    <asp:TextBox ID="hidpageindex" runat="server" Visible="False"></asp:TextBox>
    <asp:TextBox ID="txtEnteredDate" runat="server" Visible="False"></asp:TextBox>
    <asp:TextBox ID="txtEnteredDateView" runat="server" Visible="False"></asp:TextBox>
    <asp:Panel  ID="pnlConfirm" runat="server" Height="87px" BackColor="#eeeeee" BorderStyle="Groove" style="z-index: 102; left: 441px; position: absolute; top: 541px" Width="297px" Visible="false">
    <table width="100%">
         <tr>
           <td scope="col" id="tdmsg" align="center" colspan="2">
                <asp:Label CssClass="label" style ="color:black" ID="msg" runat="server" Text="Are you sure you want to perform this action" Width="295px" Height="32px"></asp:Label>
            </td>
        </tr>
    </table>
    <table>
         <tr>
            <td scope="col"  id="tdspace1" runat="server">
            </td>
            <td scope="col"  id="tdspace2" runat="server">
            </td>
        </tr>
        <tr>
               <td scope="col"  id="tdspace3" runat="server">
               </td>
                <td scope="col"  id="tdspace4" runat="server" >
                </td>
        </tr>
        <tr>
                 <td align="center" style="width: 187px; height: 27px" valign="top" scope="col"> 
                     <asp:Button CssClass="button" ToolTip="Yes" ID="btnYes" runat="server" Text="Yes" Width="33px" CausesValidation="False"/>
                 </td>
                 <td  scope="col" style="width: 198px" align="center" valign="top" >
                          <asp:Button CssClass="button" ToolTip="No" ID="btnNo" runat="server" Text="No" Width="33px" CausesValidation="False"/>
                 </td>
        </tr>
  </table>
          
         
               &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; 
            </asp:Panel>
    <asp:HiddenField ID="hidConfirm" runat="server" />
    
</asp:Content>
