<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="frm_UserManager.aspx.cs" Inherits="frm_UserManager" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

   <script>
        function alertme() {
            swal("Insert Successfuly!", "record has been saved!!!", "success")
                .then(function () {
                    window.location = "frm_UserManager.aspx?UserID=" + userid + "&UsergrpID=" + UserGrpID + "&fiscaly=" + fiscal + "&FormID=" + FormID + "";
            });
           
        }

        function Editalert() {
            swal("Update Successfuly!", "record has been saved!!!", "success")
                .then(function () {
                    window.location = "frm_UserManager.aspx?UserID=" + userid + "&UsergrpID=" + UserGrpID + "&fiscaly=" + fiscal + "&FormID=" + FormID + "";
            });
        }
        function wrong() {
            swal({
                type: 'error',
                icon: "warning",
                title: 'Oops...',
                text: 'Something went wrong!',
                
            })
        }
        function noupdate() {
            swal({
                type: 'error',
                icon: "warning",
                title: 'No Update',
                text: 'Nothing Change',

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
                    icon: "dist/img/del.png",
                    title: 'Deleted',
                    text: 'Delete Data Successfully!'
                }).then(function () {
                    window.location = "frm_UserManager.aspx?UserID=" + userid + "&UsergrpID=" + UserGrpID + "&fiscaly=" + fiscal + "&FormID=" + FormID + "";
                });
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

        .auto-style3 {
            width: 173px;
            padding-bottom: 7px;
        }

        .texthieht {
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

     
    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
   <Triggers >
       <asp:PostBackTrigger ControlID="btnSave"  />
       <asp:PostBackTrigger ControlID="btnUpdate"  />
        <asp:PostBackTrigger ControlID="btnDelete_after"  />
        <asp:PostBackTrigger ControlID="btnUpload" />
       <asp:PostBackTrigger ControlID="btnCancel" />
   </Triggers>
         <ContentTemplate>
        


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
                        <div style="height: 60px; width: 650px; margin-left: 310px;"">

                            <asp:LinkButton ID="btnInsert" Height="40" CssClass="btn btn-app " runat="server" Style="left: 180px; background-color: #1f4f8a; color: white;" OnClick="btnInsert_Click" ValidationGroup="changest">
                     <i class="fas fa-save"></i>&nbsp Add
                            </asp:LinkButton>
                            <asp:LinkButton ID="btnSave" Height="40" CssClass="btn btn-app " runat="server" Style="left: 180px; background-color: #1f4f8a; color: white;" OnClick="btnSave_Click" ValidationGroup="aaa">
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
                            <asp:LinkButton ID="btnDelete_after" OnClientClick="return sweetAlertConfirm(this);" Height="40" CssClass="btn btn-app " runat="server" Style="left: 180px; background-color: #1f4f8a; color: white;" OnClick="btnDelete_Click">
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
                                        <td class="auto-style6"><strong>Login Name</strong></td>
                                        <td class="auto-style7">
                                           <asp:TextBox ID="txtLoginName" CssClass="form-control texthieht" Width="250px" runat="server" Font-Bold="False" Font-Italic="False" ForeColor="Black"></asp:TextBox>
                                        <asp:RequiredFieldValidator runat="server" ID="Req" ControlToValidate="txtLoginName" Display="None" ErrorMessage="<b> Missing Field</b><br />Login Name is required." ValidationGroup="aaa" />
                                       <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" TargetControlID="Req" runat="server" HighlightCssClass="vali"></ajaxToolkit:ValidatorCalloutExtender>
                                        </td>
                                        <td class="auto-style5"><strong>User Group Name</strong></td>
                                        <td class="padspace">
                                            <asp:DropDownList ID="ddlUserGrpName" CssClass="ddlHeight" Width="250px" runat="server" ></asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="auto-style3"><strong>Password</strong></td>
                                        <td class="auto-style8">
                                            <asp:TextBox ID="txtPassword" CssClass="form-control texthieht" Width="250px" runat="server" TextMode="Password" Font-Bold="False" Font-Italic="False" ForeColor="Black"></asp:TextBox>
                                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" ControlToValidate="txtPassword" Display="None" ErrorMessage="<b> Missing Field</b><br />Password is required." ValidationGroup="aaa" />
                                       <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" TargetControlID="RequiredFieldValidator1" runat="server" HighlightCssClass="vali"></ajaxToolkit:ValidatorCalloutExtender>
                                        </td>
                                        <td class="auto-style5"><strong>Entry Date</strong></td>
                                        <td class="padspace">
                                             <div style="display:inline-block">
                                            <div class="input-group input-group-sm">
                                                <asp:TextBox ID="txtEntry_Date" CssClass="form-control texthieht" Width="220px" runat="server" Font-Bold="False" Font-Italic="False" ForeColor="Black"></asp:TextBox>
                                                <ajaxToolkit:CalendarExtender ID="CalendarExtender1" PopupButtonID="imgPopup" TargetControlID="txtEntry_Date" Format="dd-MMM-yyyy" runat="server" />
                                                <asp:RequiredFieldValidator runat="server" ID="Req2" ControlToValidate="txtEntry_Date" Display="None" ErrorMessage="<b> Missing Field</b><br />A Date is required." ValidationGroup="aaa" />
                                                <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender4" TargetControlID="Req2" runat="server" HighlightCssClass="vali" PopupPosition="BottomLeft"></ajaxToolkit:ValidatorCalloutExtender>
                                                <span class="input-group-btn">
                                                    <%--<button type="button" class="btn btn-info btn-flat">Go!</button>--%>
                                                    <asp:ImageButton ID="imgPopup" CssClass="btn btn-flat" ImageUrl="img/calendar.png" ImageAlign="Bottom" runat="server" />
                                                </span>
                                            </div>
                                                 </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="auto-style3"><strong>Date OF Start Work</strong></td>
                                        <td class="auto-style8">
                                            <div style="display:inline-block">
                                            <div class="input-group input-group-sm">
                                                <asp:TextBox ID="txtDateOFStartWork" CssClass="form-control texthieht" Width="220px" runat="server" Font-Bold="False" Font-Italic="False" ForeColor="Black"></asp:TextBox>
                                                <ajaxToolkit:CalendarExtender ID="CalendarExtender2" PopupButtonID="ImageButton1" TargetControlID="txtDateOFStartWork" Format="dd-MMM-yyyy" runat="server" />
                                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator2" ControlToValidate="txtDateOFStartWork" Display="None" ErrorMessage="<b> Missing Field</b><br />A Date is required." ValidationGroup="aaa" />
                                                <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" TargetControlID="RequiredFieldValidator2" runat="server" HighlightCssClass="vali" ></ajaxToolkit:ValidatorCalloutExtender>
                                                <span class="input-group-btn">
                                                    <%--<button type="button" class="btn btn-info btn-flat">Go!</button>--%>
                                                    <asp:ImageButton ID="ImageButton1" CssClass="btn btn-flat" ImageUrl="img/calendar.png" ImageAlign="Bottom" runat="server" />
                                                </span>
                                            </div>
                                                 </div>
                                        </td>
                                        <td class="auto-style5"><strong>System Date</strong></td>
                                        <td class="padspace">
                                            <div class="backcolorlbl">
                                                <asp:Label ID="lblSystemDate" runat="server" Font-Bold="False" Font-Italic="False" ForeColor="Black"></asp:Label>
                                            </div>
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
                                        <td class="auto-style3"><strong>Remarks</strong></td>
                                        <td class="auto-style8">
                                            <asp:TextBox ID="txtRemarks" CssClass="form-control texthieht" Width="250px" runat="server" Font-Bold="False" Font-Italic="False" ForeColor="Black" Height="75px" TextMode="MultiLine"></asp:TextBox>
                                        </td>
                                        <td class="auto-style5"><strong>Profile Image</strong></td>
                                        <td class="padspace">
                                            <asp:Image ID="imgDisplayArea" runat="server" Height="85px" Width="74px" AlternateText="  NO IMAGE"/>
                                            <asp:FileUpload ID="FileUpload1" runat="server" />
                                            <asp:Button ID="btnUpload" runat="server" Text="Upload" OnClick="btnUpload_Click" />
                                           
                                            
                                        </td>
                                    </tr>

                                </table>
                            </div>
                            <div id="Findbox" runat="server">
                                <div class="box">

                                    <!-- /.box-header -->
                                    <div class="box-body" style="width: 100%; overflow: auto; height: 50%">
                                        <%--CssClass="table table-bordered table-hover"--%>
                                          <div style="width: 100%; height: 400px; overflow: scroll">

                                        <asp:GridView ID="GridView1" runat="server" CssClass="table table-bordered table-hover tableFixHead" AutoGenerateColumns="False" OnSelectedIndexChanged="GridView1_SelectedIndexChanged" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Horizontal">
                                            <Columns>
                                                <asp:CommandField ShowSelectButton="True" ButtonType="Image" SelectImageUrl="~/dist/img/edit.png" />
                                                <asp:BoundField DataField="Sno" HeaderText="ID" SortExpression="Sno" />
                                                <asp:BoundField DataField="user_name" HeaderText="User Name" SortExpression="user_name" />
                                                <asp:BoundField DataField="DepartmentName" HeaderText="Department Name" SortExpression="DepartmentName" />
                                                <asp:BoundField DataField="LoginName" HeaderText="Login Name" SortExpression="LoginName" />
                                                <asp:BoundField DataField="IsActive" HeaderText="Active" SortExpression="IsActive" />
                                                <asp:BoundField DataField="UserGrpName" HeaderText="User Group Name" SortExpression="UserGrpName" />
                                                <asp:TemplateField HeaderText="Picture" SortExpression="pic_path">
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("pic_path") %>'></asp:TextBox>
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:Image ID="Image1" runat="server" Height="50px" ImageUrl='<%# Bind("pic_path") %>' Width="79px" AlternateText="NO IMAGE" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

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

                                       



                                    </div>
                                    <!-- /.box-body -->
                                </div>
                            </div>

                            <%-- Table View --%>



                            <div id="DeleteBox" runat="server">
                                <div class="box">

                                    <!-- /.box-header -->
                                   <div class="box-body" style="width: 100%; overflow: auto; height: 50%">
                                        <%--CssClass="table table-bordered table-hover"--%>
                                          <div style="width: 100%; height: 400px; overflow: scroll">

                                        <asp:GridView ID="GridView2" runat="server" CssClass="table table-bordered table-hover tableFixHead" AutoGenerateColumns="False"  OnSelectedIndexChanged="GridView2_SelectedIndexChanged" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Horizontal">
                                           <Columns>
                                                <asp:CommandField ShowSelectButton="True" ButtonType="Image" SelectImageUrl="~/dist/img/edit.png" />
                                                <asp:BoundField DataField="Sno" HeaderText="ID" SortExpression="Sno" />
                                                <asp:BoundField DataField="user_name" HeaderText="User Name" SortExpression="user_name" />
                                                <asp:BoundField DataField="DepartmentName" HeaderText="Department Name" SortExpression="DepartmentName" />
                                                <asp:BoundField DataField="LoginName" HeaderText="Login Name" SortExpression="LoginName" />
                                                <asp:BoundField DataField="IsActive" HeaderText="Active" SortExpression="IsActive" />
                                                <asp:BoundField DataField="UserGrpName" HeaderText="User Group Name" SortExpression="UserGrpName" />
                                                <asp:TemplateField HeaderText="Picture" SortExpression="pic_path">
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("pic_path") %>'></asp:TextBox>
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:Image ID="Image1" runat="server" Height="50px" ImageUrl='<%# Bind("pic_path") %>' Width="79px" AlternateText="NO IMAGE" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

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

                                    </div>
                                    <!-- /.box-body -->
                                </div>
                            </div>


                            <div id="Editbox" runat="server">
                                <div class="box">

                                    <!-- /.box-header -->
                                    <div class="box-body" style="width: 100%; overflow: auto; height: 50%">
                                        <%--CssClass="table table-bordered table-hover"--%>
                                          <div style="width: 100%; height: 400px; overflow: scroll">

                                        <asp:GridView ID="GridView3" runat="server" CssClass="table table-bordered table-hover tableFixHead" AutoGenerateColumns="False" OnSelectedIndexChanged="GridView3_SelectedIndexChanged" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Horizontal">
                                         <Columns>
                                                <asp:CommandField ShowSelectButton="True" ButtonType="Image" SelectImageUrl="~/dist/img/edit.png" />
                                                <asp:BoundField DataField="Sno" HeaderText="ID" SortExpression="Sno" />
                                                <asp:BoundField DataField="user_name" HeaderText="User Name" SortExpression="user_name" />
                                                <asp:BoundField DataField="DepartmentName" HeaderText="Department Name" SortExpression="DepartmentName" />
                                                <asp:BoundField DataField="LoginName" HeaderText="Login Name" SortExpression="LoginName" />
                                                <asp:BoundField DataField="IsActive" HeaderText="Active" SortExpression="IsActive" />
                                                <asp:BoundField DataField="UserGrpName" HeaderText="User Group Name" SortExpression="UserGrpName" />
                                                <asp:TemplateField HeaderText="Picture" SortExpression="pic_path">
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("pic_path") %>'></asp:TextBox>
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:Image ID="Image1" runat="server" Height="50px" ImageUrl='<%# Bind("pic_path") %>' Width="79px" AlternateText="NO IMAGE" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

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
             </ContentTemplate>
             </asp:UpdatePanel>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
   <Triggers >
       <asp:PostBackTrigger ControlID="btnSearchEdit"/>
       <asp:PostBackTrigger ControlID="btn_Search"  />
        <asp:PostBackTrigger ControlID="btn_delete"  />

   </Triggers>
         <ContentTemplate>
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
                            <td><strong>Login Name :</strong></td>
                            <td>
                                <%--<asp:TextBox ID="txtEdit_LN" CssClass="form-control gap" runat="server"></asp:TextBox>--%>
                                <asp:DropDownList ID="txtEdit_LN" CssClass="form-control select2" Width="100%" runat="server"></asp:DropDownList>
                            </td>

                        </tr>
                        <tr>
                            <td><strong>User Group Name :</strong> </td>
                            <td>
                               <%-- <asp:TextBox ID="txtEdit_UGN" CssClass="form-control gap" runat="server"></asp:TextBox>--%>
                                <asp:DropDownList ID="txtEdit_UGN" CssClass="form-control select2" Width="100%" runat="server"></asp:DropDownList>
                            </td>

                        </tr>
                        <tr>
                            <td><strong>Department :</strong> </td>
                            <td>

                                 <asp:DropDownList ID="txtEdit_DEp" CssClass="form-control select2" Width="100%" runat="server"></asp:DropDownList>
                                <%--<asp:TextBox ID="txtEdit_DEp" CssClass="form-control gap" runat="server"></asp:TextBox>--%>
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
                               <%-- <asp:TextBox ID="txtFind_LN" CssClass="form-control gap" runat="server"></asp:TextBox>--%>
                                 <asp:DropDownList ID="txtFind_LN" CssClass="form-control select2" Width="100%" runat="server"></asp:DropDownList>
                            </td>

                        </tr>
                        <tr>
                            <td><strong>User Group Name :</strong> </td>
                            <td>
                                <%--<asp:TextBox ID="txtFind_UGN" CssClass="form-control gap" runat="server"></asp:TextBox>--%>
                                <asp:DropDownList ID="txtFind_UGN" CssClass="form-control select2" Width="100%" runat="server"></asp:DropDownList>
                            </td>

                        </tr>
                        <tr>
                            <td><strong>Department :</strong> </td>
                            <td>
                                <asp:DropDownList ID="txtFind_Dep" CssClass="form-control select2" Width="100%" runat="server"></asp:DropDownList>

                                <%--<asp:TextBox ID="txtFind_Dep" CssClass="form-control gap" runat="server"></asp:TextBox>--%>
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
                                <%--<asp:TextBox ID="txtDelete_LN" CssClass="form-control gap" runat="server"></asp:TextBox>--%>
                                <asp:DropDownList ID="txtDelete_LN" CssClass="form-control select2" Width="100%" runat="server"></asp:DropDownList>
                            </td>

                        </tr>
                        <tr>
                            <td><strong>User Group Name :</strong> </td>
                            <td>
                                <%--<asp:TextBox ID="txtDelete_UGN" CssClass="form-control gap" runat="server"></asp:TextBox>--%>
                                <asp:DropDownList ID="txtDelete_UGN" CssClass="form-control select2" Width="100%" runat="server"></asp:DropDownList>
                            </td>

                        </tr>
                        <tr>
                            <td><strong>Department :</strong> </td>
                            <td>
                                <%--<asp:TextBox ID="txtDelete_Dep" CssClass="form-control gap" runat="server"></asp:TextBox>--%>
                                <asp:DropDownList ID="txtDelete_Dep" CssClass="form-control select2" Width="100%" runat="server"></asp:DropDownList>
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
             </ContentTemplate>

    </asp:UpdatePanel>

    <script src="https://unpkg.com/sweetalert/dist/sweetalert.min.js"></script>





</asp:Content>
