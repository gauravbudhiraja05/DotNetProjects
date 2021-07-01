<%@ Page Language="VB" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="false"
    CodeFile="SearchMem.aspx.vb" Inherits="Accounts_SearchMem" Title="Member Data - Search/Edit/Delete" %>

<asp:Content ID="Content1" ContentPlaceHolderID="LeftPlaceHolder" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table cellspacing="0" cellpadding="0" width="90%" align="center" class="table" summary="purpose of this page is, searching information about member">
        <tr>
            <td style="background-color: #CC6666; height: 22px; width: 10px" scope="col">
                <img src="../images/MemSearch.jpg" alt="Search Member Image" />
            </td scope="col">
            <td style="background-color: #CC6666; height: 22px; width: 10px" colspan="2">
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
            <td valign="top" class="label" scope="col">
                Search Criteria&nbsp;
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
                </asp:DropDownList>
                &nbsp;
            </td>
            <td valign="top"  class="label" scope="col">
                Type Text 
                </td>
            <td valign="middle" scope="col">
                <asp:TextBox ID="txtSearch" TabIndex="2" runat="server" MaxLength="50" Width="265px"
                    ToolTip="Enter Text For Search"></asp:TextBox>&nbsp;<asp:ImageButton ID="ImgBtnSearch"
                        runat="server" Width="20px" BorderWidth="0px" ToolTip="Search Record [alt+r]"
                        ImageAlign="AbsBottom" ImageUrl="../Images/Search.gif" Height="20px" AlternateText="search"
                        TabIndex="3" AccessKey="r"></asp:ImageButton>
            </td>
        </tr>
        <tr>
            <td colspan="4" scope="colgroup">
                <asp:Label ID="lblMsgBox" runat="server" Width="424px" ForeColor="#C00000" Font-Bold="True"
                    Font-Italic="True"></asp:Label>
            </td>
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
            <td colspan="4" scope="colgroup">
                <asp:DataGrid ID="grdSearchMember" runat="server" Width="100%" CellPadding="0" DataKeyField="RecId"
                    HeaderStyle-ForeColor="white" ItemStyle-Wrap="False" PagerStyle-Wrap="False"
                    AlternatingItemStyle-Wrap="False" ShowFooter="True" PageSize="15" AllowPaging="True"
                    AllowSorting="True" AutoGenerateColumns="False" CssClass="datagrid" TabIndex="4">
                    <SelectedItemStyle Wrap="False" ForeColor="Maroon"></SelectedItemStyle>
                    <AlternatingItemStyle Wrap="False"></AlternatingItemStyle>
                    <ItemStyle Wrap="False"></ItemStyle>
                    <HeaderStyle Font-Size="Small" CssClass="datagridHeader" ForeColor="White"></HeaderStyle>
                    <FooterStyle CssClass="datagridHeader"></FooterStyle>
                    <Columns>
                        <asp:TemplateColumn>
                            <HeaderStyle Wrap="False" Width="25%" CssClass="Head"></HeaderStyle>
                            <ItemStyle Wrap="False"></ItemStyle>
                            <HeaderTemplate>
                                <asp:LinkButton ID="lnkusername" runat="server" Text="User Name" CommandName="Detail" />
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkname" runat="server" CommandName="Details1">
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
                        <%--<asp:BoundColumn DataField="empid" SortExpression="empid" HeaderText="EMPId">
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
                        <asp:BoundColumn DataField="pwdStatus" SortExpression="pwdStatus" HeaderText="Status">
                            <HeaderStyle Wrap="False" CssClass="Head"></HeaderStyle>
                            <ItemStyle Wrap="False"></ItemStyle>
                        </asp:BoundColumn>
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
												<font size="1">Select All/</font></asp:LinkButton>
                                    <asp:LinkButton ID="lkbtnDeSelectAll" runat="server" CommandName="DeSelect All" Visible="True">
												<font size="1">DeSelect All</font></asp:LinkButton>
                                </center>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <center>
                                    <asp:CheckBox ID="chkSelect" runat="server"></asp:CheckBox>
                                </center>
                            </ItemTemplate>
                            <FooterTemplate>
                                <center>
                                    <asp:Button ID="btnDelete" runat="server" CommandName="Delete" CssClass="button" ToolTip="Delete User"
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
            <td colspan="4" scope="colgroup">
                <asp:TextBox ID="txtSearchcr" runat="server" Visible="False"></asp:TextBox>&nbsp;
                <asp:TextBox ID="txtUser" runat="server" Visible="False"></asp:TextBox><asp:TextBox
                    ID="txtHidRecordId" runat="server" Visible="False"></asp:TextBox><asp:ListBox ID="lstRecId"
                        runat="server" Visible="False"></asp:ListBox>
            </td>
        </tr>
        <%End If%>
        <tr>
            <td colspan="4" style="height: 372px" scope="colgroup">
                <asp:Panel ID="PnlRegDetails" Width="100%" Height="337px" Visible="False" runat="server"
                    BorderStyle="Double" BorderColor="Gray">
                    <table id="tabMain" cellspacing="0" cellpadding="0" width="100%" align="center">
                        <tbody>
                            <tr class="tableHeader">
                                <th align="left" colspan="2">
                                   <strong>Information</strong>
                                </th>
                            </tr>
                            <tr>
                                <td style="width: 163px; height: 3px" class="label" scope="col">
                                </td>
                                <td style="height: 3px" class="label" scope="col">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr style="visibility: hidden; position: absolute">
                                <td class="label" scope="col">
                                    <asp:Label ID="lblUserType" runat="server" Text="User Type" />
                                    <asp:Label ID="lbls1" runat="server" ForeColor="#C00000">*</asp:Label>
                                </td>
                                <td scope="col">
                                    <asp:DropDownList ID="cboUserType" TabIndex="5" runat="server" CssClass="dropdownlist">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td class="label" style="height: 28px; width: 163px;" scope="col">
                                    <asp:Label ID="lblUserId" runat="server" Text="User Id" />
                                    <asp:Label ID="lblls2" runat="server" ForeColor="#C00000">*</asp:Label>
                                </td>
                                <td style="height: 28px" scope="col">
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
                                <th align="left" colspan="2" class="tableHeader">
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
                                <td class="label" style="width: 163px" scope="col">
                                    &nbsp;Name
                                    <asp:Label ID="lblName" runat="server" ForeColor="#C00000">*</asp:Label>
                                </td>
                                <td scope="col">
                                    <asp:DropDownList ID="cboPrefix" TabIndex="6" runat="server" Width="38px" CssClass="dropdownlist">
                                        <asp:ListItem Value="Mr.">Mr.</asp:ListItem>
                                        <asp:ListItem Value="Miss">Miss</asp:ListItem>
                                        <asp:ListItem Value="Mrs.">Mrs.</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:TextBox ID="txtName" TabIndex="7" Width="100px" MaxLength="50" runat="server"
                                        CssClass="textBox"></asp:TextBox>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtName"
                                        ErrorMessage="Enter Alphabets Only!" ValidationExpression="([a-z A-Z \.\s])*"
                                        Width="124px" Display="dynamic"></asp:RegularExpressionValidator><asp:RequiredFieldValidator
                                            ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtName" ErrorMessage="Employee Name is required"
                                            Width="155px" Display="dynamic"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td class="label" style="width: 163px; height: 28px;" scope="col">
                                    &nbsp;Employee Id
                                    <asp:Label ID="lblCountry0" runat="server" ForeColor="#C00000">*</asp:Label>
                                </td>
                                <td style="height: 28px" scope="col">
                                    <asp:TextBox ID="txtEmpId" TabIndex="7" Width="142px" MaxLength="50" runat="server"
                                        CssClass="textBox"></asp:TextBox>
                                    <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="txtEmpId"
                                        ErrorMessage="Enter Numerics Only!" Operator="DataTypeCheck" Type="Integer" Width="125px"
                                        Display="dynamic"></asp:CompareValidator><asp:RequiredFieldValidator ID="RequiredFieldValidator1"
                                            runat="server" ControlToValidate="txtEmpId" ErrorMessage="Employee Id is required"
                                            Width="152px" Display="dynamic"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td class="label" style="width: 163px" scope="col">
                                    &nbsp;Designation
                                </td>
                                <td scope="col">
                                    <asp:DropDownList ID="cboDesignation" TabIndex="8" runat="server" CssClass="dropdownlist">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr style="visibility: hidden">
                                <td style="width: 163px; text-align: right;" scope="col">
                                    <asp:Label ID="Label2" runat="server" Width="80px" Font-Italic="True" Font-Bold="True"
                                        ForeColor="#C00000">If Not in list</asp:Label>
                                </td>
                                <td scope="col">
                                    <asp:TextBox ID="txtDesig" TabIndex="9" runat="server" Width="143px" MaxLength="50"
                                        CssClass="textBox"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="label" style="width: 163px" scope="col">
                                    &nbsp;Department
                                    <asp:Label ID="Label5" runat="server" ForeColor="#C00000">*</asp:Label>
                                </td>
                                <td scope="col">
                                    <asp:DropDownList ID="DepartmentName" TabIndex="10" runat="server" AutoPostBack="True"
                                        CssClass="dropdownlist">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td class="label" style="width: 163px" scope="col">
                                    &nbsp;Client
                                </td>
                                <td scope="col">
                                    <asp:DropDownList ID="ClientName" TabIndex="11" runat="server" AutoPostBack="True"
                                        CssClass="dropdownlist">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td class="label" style="height: 20px; width: 163px;" scope="col">
                                    &nbsp;LOB
                                </td>
                                <td scope="col">
                                    <asp:DropDownList ID="cboLOBDept" TabIndex="12" runat="server" CssClass="dropdownlist">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td class="label" style="height: 22px; width: 163px;" scope="col">
                                    &nbsp;BU
                                    </td>
                                <td style="height: 22px" scope="col">
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
                                    &nbsp;<asp:TextBox ID="txtBU" TabIndex="14" runat="server" CssClass="textBox" MaxLength="50"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 163px" scope="col">
                                    <asp:Label ID="lblEmail" runat="server" Text="Email" CssClass="label"></asp:Label>
                                    &nbsp;<asp:Label ID="lblCountry" runat="server" ForeColor="#C00000">*</asp:Label>
                                </td>
                                <td scope="col">
                                    <asp:TextBox ID="txtEmail" TabIndex="15" runat="server" MaxLength="50" CssClass="textBox"></asp:TextBox>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Fill Valid Email Id"
                                        ControlToValidate="txtEmail" ValidationExpression="^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$"
                                        Width="117px" Display="dynamic"></asp:RegularExpressionValidator>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtEmail"
                                        ErrorMessage="Employee Email is Required" Display="dynamic"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td align="center" colspan="2" class="label" scope="col">
                                </td>
                            </tr>
                            <tr>
                                <td align="center" colspan="2" style="height: 8px" scope="col">
                                    <asp:Button ID="cmdUpdate" TabIndex="16" runat="server" CssClass="button" Text="Update"
                                        ToolTip="Update Record [alt+u]" AccessKey="u"></asp:Button>&nbsp;
                                    <asp:Button ID="cmdCancel" TabIndex="17" runat="server" CssClass="button" Text="Cancel"
                                        ToolTip="cancel update [alt+c]" AccessKey="c"></asp:Button>
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
</asp:Content>
