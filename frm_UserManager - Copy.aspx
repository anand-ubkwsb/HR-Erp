<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="frm_UserManager - Copy.aspx.cs" Inherits="frm_UserManager" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script>
        function alertme() {
            swal("Insert Successfuly!", "record has been saved!!!", "success");
        }

        function Editalert() {
            swal("Update Successfuly!", "record has been saved!!!", "success");
        }
        function wrong() {
            swal({
                type: 'error',
                icon: "warning",
                title: 'Oops...',
                text: 'Something went wrong!',

            })
        }

        function periodoverlap() {
            swal({
                type: 'error',
                icon: "warning",
                title: 'Oops...',
                text: 'Date period overlap!!',

            })
        }
        function fillrequird() {
            swal({
                type: 'error',
                icon: "warning",
                title: 'Oops...',
                text: 'Please select the row to delete!',

            })
        }
        function nodatafound() {
            swal({
                type: 'error',
                icon: "error",
                title: 'Data Not Found !',
                text: 'Search again ...',

            })
        }


        function alertDelete() {

            swal({
                type: 'error',
                icon: "error",
                title: 'Deleted',
                text: 'Delete Data Successfully!'
            })
        }
    </script>
    <style type="text/css">
        .shadowbox {
            box-shadow: 0 4px 8px 0 rgba(0,0,0,0.2), 0 6px 20px 0 rgba(0,0,0,0.19);
            background-color: #026dbd;
        }

        .gap {
            margin-bottom: 10px;
        }

        .example button {
            float: left;
            background-color: #4E3E55;
            color: white;
            border: none;
            box-shadow: none;
            font-size: 17px;
            font-weight: 500;
            font-weight: 600;
            border-radius: 3px;
            padding: 15px 35px;
            margin: 26px 5px 0 5px;
            cursor: pointer;
        }

            .example button:focus {
                outline: none;
            }

            .example button:hover {
                background-color: #33DE23;
            }

            .example button:active {
                background-color: #81ccee;
            }

        .fas {
            top: 0px;
            display: inline;
            font: normal normal normal 14px/1 FontAwesome;
            font-size: 20px;
            text-rendering: auto;
            -webkit-font-smoothing: antialiased;
            -moz-osx-font-smoothing: grayscale;
        }

        .fa-save, .fa-edit, .fa-trash, .fa-search, .fa-remove {
            font-size: 14px;
        }

        .backcolorlbl {
            height: 25px;
            width: 250px;
            /*background-color:#fbfcf2;*/
            background-color: #eeeeee;
            padding-left: 14px;
        }

        .padspace {
            padding-bottom: 7px;
        }

        .auto-style1 {
            height: 20px;
            padding-bottom: 7px;
        }

        .auto-style3 {
            width: 173px;
            padding-bottom: 7px;
        }

        .texthieht {
            height: 25px;
        }

        .auto-style4 {
            height: 20px;
            width: 239px;
            padding-bottom: 7px;
        }

        .auto-style5 {
            width: 239px;
            padding-bottom: 7px;
        }

        .auto-style6 {
            width: 173px;
        }

        .auto-style7 {
            height: 20px;
            padding-bottom: 7px;
            width: 296px;
        }

        .auto-style8 {
            padding-bottom: 7px;
            width: 296px;
        }


        /*tab style*/
        #tabmenu {
            clear: both;
            float: left;
            list-style: none;
            margin: 0px 10px 0 0px;
            border-bottom: 1px solid #333333; /*Remove to create tab effect*/
            width: 800px; /*If tab effect is not being used, this will define how long the bottom border will be*/
        }

            #tabmenu h3 {
                display: inline;
                float: left;
            }

            #tabmenu h3 {
                margin-right: 5px;
                margin-top: 20px;
                color: #FFFFFF;
                display: block;
                float: left;
                height: 40px;
                border-radius: 10px 10px 0px 0px;
                font: 14px/100% Arial, Helvetica, sans-serif;
                line-height: 62px;
                text-decoration: none;
                vertical-align: middle;
                padding: 0 20px;
                background-image: url(nav-tab-bg.png);
                background-color: #1f4f8a; /*A basic background color is set incase the image will not load*/
                border: 1px solid #333333;
                border-bottom: none;
            }
        /*End tab style*/
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <%--<asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>--%>

    <section class="content">
        <div class="row">
            <div class="col-lg-12">

                <div class="box box-solid shadowbox " style="border-radius: 15px;">
                    <div class="box-header with-border" style="height: 75px;">

                        <div style="padding-top: 0px; width: 120px; float: left;">
                            <div id="tabmenu">
                                <h3 class="box-title" style="color: #fff; padding-left: 10px;">User Manager Form</h3>
                            </div>
                        </div>
                        <div style="height: 60px; width: 650px; margin-left: 520px;">

                            <asp:LinkButton ID="btnInsert" Height="40" CssClass="btn btn-app " runat="server" Style="left: 180px; background-color: #1f4f8a; color: white;" OnClick="btnInsert_Click" ValidationGroup="changest">
                     <i class="fas fa-save"></i>&nbsp Add
                            </asp:LinkButton>
                            <asp:LinkButton ID="btnSave" Height="40" CssClass="btn btn-app " runat="server" Style="left: 180px; background-color: #1f4f8a; color: white;" OnClick="btnSave_Click">
                     <i class="fas fa-save"></i>&nbsp Save
                            </asp:LinkButton>
                            <asp:LinkButton ID="btnEdit" Height="40" CssClass="btn btn-app " runat="server" data-toggle="modal" data-target="#modal_edit" Style="left: 180px; background-color: #1f4f8a; color: white;">
                     <i class="fas fa-edit"></i>&nbsp Edit
                            </asp:LinkButton>
                            <asp:LinkButton ID="btnUpdate" Height="40" CssClass="btn btn-app " runat="server" Style="left: 180px; background-color: #1f4f8a; color: white;" OnClick="btnUpdate_Click">
                     <i class="fas fa-edit"></i>&nbsp Update
                            </asp:LinkButton>
                            <asp:LinkButton ID="btnDelete" Height="40" CssClass="btn btn-app " runat="server" Style="left: 180px; background-color: #1f4f8a; color: white;" data-toggle="modal" data-target="#modal_delete">
                     <i class="fas fa-trash"></i>&nbsp Delete
                            </asp:LinkButton>
                            <asp:LinkButton ID="btnDelete_after" Height="40" CssClass="btn btn-app " runat="server" Style="left: 180px; background-color: #1f4f8a; color: white;" OnClick="btnDelete_Click">
                     <i class="fas fa-trash"></i>&nbsp Delete
                            </asp:LinkButton>
                            <asp:LinkButton ID="btnFind" Height="40" CssClass="btn btn-app " runat="server" data-toggle="modal" data-target="#modal_find" Style="left: 180px; background-color: #1f4f8a; color: white;">
                     <i class="fas fa-search"></i>&nbsp Find
                            </asp:LinkButton>
                            <asp:LinkButton ID="btnCancel" Height="40" CssClass="btn btn-app" runat="server" Style="left: 180px; background-color: #1f4f8a; color: white;" OnClick="btnCancel_Click">
                     <i class="fas fa-remove"></i>&nbsp Cancel
                            </asp:LinkButton>
                        </div>

                    </div>

                    <div class="form-row">

                        <div class="box-body" style="background-color: #cddbf2;">
                            <div id="fieldbox" runat="server">
                                <table id="tablecontent1" runat="server" style="width: 100%;">
                                    <tr>
                                        <td class="auto-style6"><strong>User ID:</strong></td>
                                        <td class="auto-style7">
                                            <div class="backcolorlbl">
                                                <asp:Label ID="lblUserId" runat="server" Font-Bold="False" Font-Italic="False" ForeColor="Black"></asp:Label>
                                            </div>
                                        </td>
                                        <td class="auto-style4"><strong>Login Name</strong></td>
                                        <td class="auto-style1 ">
                                            <asp:TextBox ID="txtLoginName" CssClass="form-control texthieht" Width="250px" runat="server" Font-Bold="False" Font-Italic="False" ForeColor="Black"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="auto-style3"><strong>Password</strong></td>
                                        <td class="auto-style8">
                                            <asp:TextBox ID="txtPassword" CssClass="form-control texthieht" Width="250px" runat="server" TextMode="Password" Font-Bold="False" Font-Italic="False" ForeColor="Black"></asp:TextBox>
                                        </td>
                                        <td class="auto-style5"><strong>Entry Date</strong></td>
                                        <td class="padspace">

                                            <asp:TextBox ID="txtEntry_Date" CssClass="form-control texthieht" Width="250px" runat="server" Font-Bold="False" Font-Italic="False" ForeColor="Black"></asp:TextBox>
                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtEntry_Date" Format="dd-MMM-yyyy" />

                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="auto-style3"><strong>Date OF Start Work</strong></td>
                                        <td class="auto-style8">
                                            <asp:TextBox ID="txtDateOFStartWork" CssClass="form-control texthieht" Width="250px" runat="server" Font-Bold="False" Font-Italic="False" ForeColor="Black"></asp:TextBox>
                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtDateOFStartWork" Format="dd-MMM-yyyy" />
                                        </td>
                                        <td class="auto-style5"><strong>System Date</strong></td>
                                        <td class="padspace">
                                            <div class="backcolorlbl">
                                                <asp:Label ID="lblSystemDate" runat="server" Font-Bold="False" Font-Italic="False" ForeColor="Black"></asp:Label>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="auto-style3"><strong>Remarks</strong></td>
                                        <td class="auto-style8">
                                            <asp:TextBox ID="txtRemarks" CssClass="form-control texthieht" Width="250px" runat="server" Font-Bold="False" Font-Italic="False" ForeColor="Black"></asp:TextBox>
                                        </td>
                                        <td class="auto-style5"><strong>Display Area</strong></td>
                                        <td class="padspace">
                                            <asp:Image ID="imgDisplayArea" runat="server" Height="85px" Width="74px" />
                                            
                                            
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="auto-style3"><strong>User Name</strong></td>
                                        <td class="auto-style8">
                                            <asp:TextBox ID="txtUserName" CssClass="form-control texthieht" Width="250px" runat="server" Font-Bold="False" Font-Italic="False" ForeColor="Black"></asp:TextBox>
                                        </td>
                                        <td class="auto-style5"><strong>Department</strong></td>
                                        <td class="padspace">
                                            <asp:DropDownList ID="ddlDepartment" Width="250px" runat="server"></asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="auto-style3"><strong>Employee</strong></td>
                                        <td class="auto-style8">
                                            <asp:TextBox ID="txtEmployee" CssClass="form-control texthieht" Width="250px" runat="server" Font-Bold="False" Font-Italic="False" ForeColor="Black"></asp:TextBox>
                                        </td>
                                        <td class="auto-style5"><strong>Active</strong></td>
                                        <td class="padspace">
                                            <asp:Panel ID="GrpActives" runat="server">
                                                <asp:CheckBox ID="chkActive" runat="server" Text="Active" />
                                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="auto-style3"><strong>Allowed Wrong Attempts</strong></td>
                                        <td class="auto-style8">
                                            <asp:TextBox ID="txtAllowedWrongAttempts" CssClass="form-control texthieht" Width="250px" runat="server" Font-Bold="False" Font-Italic="False" ForeColor="Black"></asp:TextBox>
                                        </td>
                                        <td class="auto-style5"><strong>No of Wrongs Attempts</strong></td>
                                        <td>
                                            <div id="wrgAttempt" runat="server">
                                            <div class="backcolorlbl">
                                                <asp:Label ID="lblNoOfWrongsAttempts" runat="server" Font-Bold="False" Font-Italic="False" ForeColor="Black"></asp:Label>
                                            </div>
                                                </div>
                                            <asp:TextBox ID="txtwrgAttemp" CssClass="form-control texthieht" Width="250px" runat="server" Font-Bold="False" Font-Italic="False" ForeColor="Black"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="auto-style3"><strong>Force Password Change</strong></td>
                                        <td class="auto-style11">
                                            <asp:RadioButton ID="rdb_ForcePWDChn_Y" runat="server" GroupName=" Multicurrency" Text="YES" />
                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                            <asp:RadioButton ID="rdb_ForcePWDChn_N" runat="server" GroupName=" Multicurrency" Text="NO" />

                                        </td>
                                        <td class="auto-style5"><strong>Change Password Days</strong></td>
                                        <td class="padspace">

                                            <asp:TextBox ID="lblChangePasswordDays" CssClass="form-control texthieht" Width="250px" runat="server"></asp:TextBox>

                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="auto-style3"><strong>Remaining Days to Change Pwd</strong></td>
                                        <td class="auto-style8">
                                            <div class="backcolorlbl">
                                                <asp:Label ID="lblRemaingDaysToChangePwd" runat="server" Font-Bold="False" Font-Italic="False" ForeColor="Black"></asp:Label>
                                            </div>
                                        </td>
                                        <td class="auto-style5"><strong>Ip Address</strong></td>
                                        <td class="padspace">
                                            <asp:TextBox ID="txtIpAddress" CssClass="form-control texthieht" Width="250px" runat="server" Font-Bold="False" Font-Italic="False" ForeColor="Black"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="auto-style5"><strong>Login Status</strong></td>
                                        <td class="padspace">
                                            <asp:TextBox ID="txtLoginStatus" CssClass="form-control texthieht" Width="250px" runat="server" Font-Bold="False" Font-Italic="False" ForeColor="Black"></asp:TextBox>
                                        </td>
                                        <td class="auto-style3"><strong>Employee Left Date</strong></td>
                                        <td class="auto-style8">
                                            <asp:TextBox ID="txtEmployeeLeftDate" CssClass="form-control texthieht" Width="250px" runat="server" Font-Bold="False" Font-Italic="False" ForeColor="Black"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="auto-style5"><strong>User Group Name</strong></td>
                                        <td class="padspace">
                                            <asp:DropDownList ID="ddlUserGrpName" CssClass="ddlHeight" Width="250px" runat="server" ></asp:DropDownList>
                                        </td>
                                        
                                    </tr>

                                </table>
                            </div>
                            <div id="Findbox" runat="server">
                                <div class="box">

                                    <!-- /.box-header -->
                                    <div class="box-body" style="width: 100%; overflow: auto; height: 50%;">
                                        <%--CssClass="table table-bordered table-hover"--%>


                                        <asp:GridView ID="GridView1" runat="server" CssClass="table table-bordered table-hover" AutoGenerateColumns="False" DataKeyNames="UserId" OnSelectedIndexChanged="GridView1_SelectedIndexChanged" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Horizontal">
                                            <Columns>
                                                <asp:CommandField ShowSelectButton="True" />
                                                <asp:BoundField DataField="Sno" HeaderText="Sno" InsertVisible="False" ReadOnly="True" SortExpression="Sno" />
                                                <asp:BoundField DataField="UserId" HeaderText="UserId" SortExpression="UserId" ReadOnly="True" />
                                                <asp:BoundField DataField="LoginName" HeaderText="LoginName" SortExpression="LoginName" />
                                                <asp:BoundField DataField="pwd" HeaderText="pwd" SortExpression="pwd" />
                                                <asp:BoundField DataField="SysDate" HeaderText="SysDate" SortExpression="SysDate" />
                                                <asp:BoundField DataField="remarks" HeaderText="remarks" SortExpression="remarks" />
                                                <asp:BoundField DataField="pic_path" HeaderText="pic_path" SortExpression="pic_path" />

                                                <asp:BoundField DataField="user_name" HeaderText="user_name" SortExpression="user_name" />
                                                <asp:CheckBoxField DataField="ForcePwdChange" HeaderText="ForcePwdChange" SortExpression="ForcePwdChange" />
                                                <asp:BoundField DataField="Wrong_attempt" HeaderText="Wrong_attempt" SortExpression="Wrong_attempt" />
                                                <asp:BoundField DataField="Lock_user" HeaderText="Lock_user" SortExpression="Lock_user" />


                                                <asp:BoundField DataField="last_pwd" HeaderText="last_pwd" SortExpression="last_pwd" />
                                                <asp:BoundField DataField="Last_pwd_date" HeaderText="Last_pwd_date" SortExpression="Last_pwd_date" />
                                                <asp:BoundField DataField="Last_Login" HeaderText="Last_Login" SortExpression="Last_Login" />
                                                <asp:BoundField DataField="IsActive" HeaderText="IsActive" SortExpression="IsActive" />
                                                <asp:BoundField DataField="department" HeaderText="department" SortExpression="department" />
                                                <asp:BoundField DataField="emp_id" HeaderText="emp_id" SortExpression="emp_id" />
                                                <asp:BoundField DataField="PwdChangeDays" HeaderText="PwdChangeDays" SortExpression="PwdChangeDays" />
                                                <asp:BoundField DataField="PwdChangeCounter" HeaderText="PwdChangeCounter" SortExpression="PwdChangeCounter" />
                                                <asp:BoundField DataField="login_ip" HeaderText="login_ip" SortExpression="login_ip" />
                                                <asp:BoundField DataField="UserGrpId" HeaderText="UserGrpId" SortExpression="UserGrpId" />
                                                <asp:BoundField DataField="user_login_status" HeaderText="user_login_status" SortExpression="user_login_status" />
                                                <asp:BoundField DataField="user_Left_Date" HeaderText="user_Left_Date" SortExpression="user_Left_Date" />
                                                <asp:BoundField DataField="Last_Mac_add" HeaderText="Last_Mac_add" SortExpression="Last_Mac_add" />
                                                <asp:BoundField DataField="Last_Ip" HeaderText="Last_Ip" SortExpression="Last_Ip" />


                                            </Columns>
                                            <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
                                            <HeaderStyle BackColor="#000080" Font-Bold="True" ForeColor="White" />
                                            <PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="Right" />

                                            <SortedAscendingCellStyle BackColor="#F7F7F7" />
                                            <SortedAscendingHeaderStyle BackColor="#4B4B4B" />
                                            <SortedDescendingCellStyle BackColor="#E5E5E5" />
                                            <SortedDescendingHeaderStyle BackColor="#242121" />
                                        </asp:GridView>



                                    </div>
                                    <!-- /.box-body -->
                                </div>
                            </div>

                            <%-- Table View --%>



                            <div id="DeleteBox" runat="server">
                                <div class="box">

                                    <!-- /.box-header -->
                                    <div class="box-body" style="width: 100%; overflow: auto; height: 50%;">
                                        <%--CssClass="table table-bordered table-hover"--%>


                                        <asp:GridView ID="GridView2" runat="server" CssClass="table table-bordered table-hover" AutoGenerateColumns="False" DataKeyNames="UserId" OnSelectedIndexChanged="GridView1_SelectedIndexChanged" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Horizontal">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Delete">
                                                    <EditItemTemplate>
                                                        <asp:CheckBox ID="chkDeletegd" runat="server" />
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkDeletegd" runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Sno" InsertVisible="False" SortExpression="Sno">
                                                    <EditItemTemplate>
                                                        <asp:Label ID="lbl_ID" runat="server" Text='<%# Eval("Sno") %>'></asp:Label>
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_ID" runat="server" Text='<%# Bind("Sno") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="UserId" HeaderText="UserId" SortExpression="UserId" ReadOnly="True" />
                                                <asp:BoundField DataField="LoginName" HeaderText="LoginName" SortExpression="LoginName" />
                                                <asp:BoundField DataField="pwd" HeaderText="pwd" SortExpression="pwd" />
                                                <asp:BoundField DataField="SysDate" HeaderText="SysDate" SortExpression="SysDate" />
                                                <asp:BoundField DataField="remarks" HeaderText="remarks" SortExpression="remarks" />
                                                <asp:BoundField DataField="pic_path" HeaderText="pic_path" SortExpression="pic_path" />

                                                <asp:BoundField DataField="user_name" HeaderText="user_name" SortExpression="user_name" />
                                                <asp:CheckBoxField DataField="ForcePwdChange" HeaderText="ForcePwdChange" SortExpression="ForcePwdChange" />
                                                <asp:BoundField DataField="Wrong_attempt" HeaderText="Wrong_attempt" SortExpression="Wrong_attempt" />
                                                <asp:BoundField DataField="Lock_user" HeaderText="Lock_user" SortExpression="Lock_user" />


                                                <asp:BoundField DataField="last_pwd" HeaderText="last_pwd" SortExpression="last_pwd" />
                                                <asp:BoundField DataField="Last_pwd_date" HeaderText="Last_pwd_date" SortExpression="Last_pwd_date" />
                                                <asp:BoundField DataField="Last_Login" HeaderText="Last_Login" SortExpression="Last_Login" />
                                                <asp:BoundField DataField="IsActive" HeaderText="IsActive" SortExpression="IsActive" />
                                                <asp:BoundField DataField="department" HeaderText="department" SortExpression="department" />
                                                <asp:BoundField DataField="emp_id" HeaderText="emp_id" SortExpression="emp_id" />
                                                <asp:BoundField DataField="PwdChangeDays" HeaderText="PwdChangeDays" SortExpression="PwdChangeDays" />
                                                <asp:BoundField DataField="PwdChangeCounter" HeaderText="PwdChangeCounter" SortExpression="PwdChangeCounter" />
                                                <asp:BoundField DataField="login_ip" HeaderText="login_ip" SortExpression="login_ip" />
                                                <asp:BoundField DataField="UserGrpId" HeaderText="UserGrpId" SortExpression="UserGrpId" />
                                                <asp:BoundField DataField="user_login_status" HeaderText="user_login_status" SortExpression="user_login_status" />
                                                <asp:BoundField DataField="user_Left_Date" HeaderText="user_Left_Date" SortExpression="user_Left_Date" />
                                                <asp:BoundField DataField="Last_Mac_add" HeaderText="Last_Mac_add" SortExpression="Last_Mac_add" />
                                                <asp:BoundField DataField="Last_Ip" HeaderText="Last_Ip" SortExpression="Last_Ip" />


                                            </Columns>
                                            <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
                                            <HeaderStyle BackColor="#000080" Font-Bold="True" ForeColor="White" />
                                            <PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="Right" />

                                            <SortedAscendingCellStyle BackColor="#F7F7F7" />
                                            <SortedAscendingHeaderStyle BackColor="#4B4B4B" />
                                            <SortedDescendingCellStyle BackColor="#E5E5E5" />
                                            <SortedDescendingHeaderStyle BackColor="#242121" />
                                        </asp:GridView>





                                    </div>
                                    <!-- /.box-body -->
                                </div>
                            </div>


                            <div id="Editbox" runat="server">
                                <div class="box">

                                    <!-- /.box-header -->
                                    <div class="box-body" style="width: 100%; overflow: auto; height: 50%;">
                                        <%--CssClass="table table-bordered table-hover"--%>


                                        <asp:GridView ID="GridView3" runat="server" CssClass="table table-bordered table-hover" AutoGenerateColumns="False" DataKeyNames="UserId" OnSelectedIndexChanged="GridView3_SelectedIndexChanged" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Horizontal">
                                            <Columns>
                                                <asp:CommandField ShowSelectButton="True" />
                                                <asp:BoundField DataField="Sno" HeaderText="Sno" InsertVisible="False" ReadOnly="True" SortExpression="Sno" />
                                                <asp:BoundField DataField="UserId" HeaderText="UserId" SortExpression="UserId" ReadOnly="True" />
                                                <asp:BoundField DataField="LoginName" HeaderText="LoginName" SortExpression="LoginName" />
                                                <asp:BoundField DataField="pwd" HeaderText="pwd" SortExpression="pwd" />
                                                <asp:BoundField DataField="SysDate" HeaderText="SysDate" SortExpression="SysDate" />
                                                <asp:BoundField DataField="remarks" HeaderText="remarks" SortExpression="remarks" />
                                                <asp:BoundField DataField="pic_path" HeaderText="pic_path" SortExpression="pic_path" />

                                                <asp:BoundField DataField="user_name" HeaderText="user_name" SortExpression="user_name" />
                                                <asp:CheckBoxField DataField="ForcePwdChange" HeaderText="ForcePwdChange" SortExpression="ForcePwdChange" />
                                                <asp:BoundField DataField="Wrong_attempt" HeaderText="Wrong_attempt" SortExpression="Wrong_attempt" />
                                                <asp:BoundField DataField="Lock_user" HeaderText="Lock_user" SortExpression="Lock_user" />


                                                <asp:BoundField DataField="last_pwd" HeaderText="last_pwd" SortExpression="last_pwd" />
                                                <asp:BoundField DataField="Last_pwd_date" HeaderText="Last_pwd_date" SortExpression="Last_pwd_date" />
                                                <asp:BoundField DataField="Last_Login" HeaderText="Last_Login" SortExpression="Last_Login" />
                                                <asp:BoundField DataField="IsActive" HeaderText="IsActive" SortExpression="IsActive" />
                                                <asp:BoundField DataField="department" HeaderText="department" SortExpression="department" />
                                                <asp:BoundField DataField="emp_id" HeaderText="emp_id" SortExpression="emp_id" />
                                                <asp:BoundField DataField="PwdChangeDays" HeaderText="PwdChangeDays" SortExpression="PwdChangeDays" />
                                                <asp:BoundField DataField="PwdChangeCounter" HeaderText="PwdChangeCounter" SortExpression="PwdChangeCounter" />
                                                <asp:BoundField DataField="login_ip" HeaderText="login_ip" SortExpression="login_ip" />
                                                <asp:BoundField DataField="UserGrpId" HeaderText="UserGrpId" SortExpression="UserGrpId" />
                                                <asp:BoundField DataField="user_login_status" HeaderText="user_login_status" SortExpression="user_login_status" />
                                                <asp:BoundField DataField="user_Left_Date" HeaderText="user_Left_Date" SortExpression="user_Left_Date" />
                                                <asp:BoundField DataField="Last_Mac_add" HeaderText="Last_Mac_add" SortExpression="Last_Mac_add" />
                                                <asp:BoundField DataField="Last_Ip" HeaderText="Last_Ip" SortExpression="Last_Ip" />


                                            </Columns>
                                            <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
                                            <HeaderStyle BackColor="#000080" Font-Bold="True" ForeColor="White" />
                                            <PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="Right" />

                                            <SortedAscendingCellStyle BackColor="#F7F7F7" />
                                            <SortedAscendingHeaderStyle BackColor="#4B4B4B" />
                                            <SortedDescendingCellStyle BackColor="#E5E5E5" />
                                            <SortedDescendingHeaderStyle BackColor="#242121" />
                                        </asp:GridView>





                                    </div>
                                    <!-- /.box-body -->
                                </div>
                            </div>


                        </div>
                        <!-- /.box-body -->
                        <div class="box-footer">

                            <asp:Label ID="Label1" runat="server" Text=""></asp:Label>

                        </div>
                        <!-- /.box-footer -->
                    </div>
                </div>
                <!-- /.box -->


            </div>
        </div>

    </section>

    <div class="modal fade" id="modal_edit">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title">Edit </h4>
                </div>
                <div class="modal-body">

                    <table style="width: 100%;">
                        <tr>
                            <td><strong>Login Name : </strong></td>
                            <td>
                                <asp:TextBox ID="txtEdit_LN" CssClass="form-control gap" runat="server"></asp:TextBox>
                            </td>

                        </tr>
                        <tr>
                            <td><strong>User Group Name :</strong> </td>
                            <td>
                                <asp:TextBox ID="txtEdit_UGN" CssClass="form-control gap" runat="server"></asp:TextBox>
                            </td>

                        </tr>
                        <tr>
                            <td><strong>Department :</strong> </td>
                            <td>
                                <asp:TextBox ID="txtEdit_DEp" CssClass="form-control gap" runat="server"></asp:TextBox>
                            </td>

                        </tr>
                        <tr>
                            <td><strong>Active :</strong> </td>
                            <td>
                                <asp:CheckBox ID="chkEdit_Active" runat="server" Checked="true" Text="Active" />
                            </td>

                        </tr>

                    </table>

                </div>

                <div class="modal-footer">
                    <button type="button" class="btn btn-default pull-left" data-dismiss="modal">Close</button>

                    <asp:Button ID="btnSearchEdit" CssClass="btn btn-primary" runat="server" Text="Search" OnClick="btnSearchEdit_Click" />

                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>
    <div class="modal fade" id="modal_find">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title">Find</h4>
                </div>
                <div class="modal-body">

                    <table style="width: 100%;">
                        <tr>
                            <td><strong>Login Name : </strong></td>
                            <td>
                                <asp:TextBox ID="txtFind_LN" CssClass="form-control gap" runat="server"></asp:TextBox>
                            </td>

                        </tr>
                        <tr>
                            <td><strong>User Group Name :</strong> </td>
                            <td>
                                <asp:TextBox ID="txtFind_UGN" CssClass="form-control gap" runat="server"></asp:TextBox>
                            </td>

                        </tr>
                        <tr>
                            <td><strong>Department :</strong> </td>
                            <td>
                                <asp:TextBox ID="txtFind_Dep" CssClass="form-control gap" runat="server"></asp:TextBox>
                            </td>

                        </tr>
                        <tr>
                            <td><strong>Active :</strong> </td>
                            <td>
                                <asp:CheckBox ID="chkFind_Active" runat="server" Checked="true" Text="Active" />
                            </td>

                        </tr>

                    </table>

                </div>

                <div class="modal-footer">
                    <button type="button" class="btn btn-default pull-left" data-dismiss="modal">Close</button>

                    <asp:Button ID="btn_Search" CssClass="btn btn-primary" runat="server" Text="Search" OnClick="btn_Search_Click" />

                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>

    <div class="modal fade" id="modal_delete">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title">Delete </h4>
                </div>
                <div class="modal-body">

                    <table style="width: 100%;">
                        <tr>
                            <td><strong>Login Name : </strong></td>
                            <td>
                                <asp:TextBox ID="txtDelete_LN" CssClass="form-control gap" runat="server"></asp:TextBox>
                            </td>

                        </tr>
                        <tr>
                            <td><strong>User Group Name :</strong> </td>
                            <td>
                                <asp:TextBox ID="txtDelete_UGN" CssClass="form-control gap" runat="server"></asp:TextBox>
                            </td>

                        </tr>
                        <tr>
                            <td><strong>Department :</strong> </td>
                            <td>
                                <asp:TextBox ID="txtDelete_Dep" CssClass="form-control gap" runat="server"></asp:TextBox>
                            </td>

                        </tr>
                        <tr>
                            <td><strong>Active :</strong> </td>
                            <td>
                                <asp:CheckBox ID="chkDelete_Active" runat="server" Checked="true" Text="Active" />
                            </td>

                        </tr>

                    </table>

                </div>

                <div class="modal-footer">
                    <button type="button" class="btn btn-default pull-left" data-dismiss="modal">Close</button>

                    <asp:Button ID="btn_delete" CssClass="btn btn-primary" runat="server" Text="Search" OnClick="btn_delete_Click" />

                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>

    <script src="https://unpkg.com/sweetalert/dist/sweetalert.min.js"></script>





</asp:Content>
