<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="frm_SET_UserGrpReport.aspx.cs" Inherits="frm_SET_COA_Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link rel='stylesheet' type='text/css' href="Styles/StaticHeader.css" />
     <script type='text/javascript' src="Styles/x.js"></script>

    <script type='text/javascript' src="Styles/xtableheaderfixed.js"></script>
     
   
     <script>

         xAddEventListener(window, 'load',
           function () { new xTableHeaderFixed('GridView4', 'table-container', 0); }, false);

        function alertme() {
            swal("Insert Successfuly!", "record has been saved!!!", "success")
                .then(function () {
                    window.location = "frm_SET_UserGrpReport.aspx?UserID=" + userid + "&UsergrpID=" + UserGrpID + "&fiscaly=" + fiscal + "&FormID=" + FormID + "";
            });
           
        }

        function Editalert() {
            swal("Update Successfuly!", "record has been saved!!!", "success")
                .then(function () {
                    window.location = "frm_SET_UserGrpReport.aspx?UserID=" + userid + "&UsergrpID=" + UserGrpID + "&fiscaly=" + fiscal + "&FormID=" + FormID + "";
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
                    window.location = "frm_SET_UserGrpReport.aspx?UserID=" + userid + "&UsergrpID=" + UserGrpID + "&fiscaly=" + fiscal + "&FormID=" + FormID + "";
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
            padding-left: 10px;
            padding-top: 5px;
            background-color: #eeeeee;
        }

        .padspace {
            padding-bottom: 7px;
        }

        .auto-style1 {
            padding-bottom: 7px;
        }

        .auto-style3 {
            width: 173px;
            padding-bottom: 7px;
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

        .texthieht {
            height: 25px;
        }

        .ddlHeight {
            height: 25px;
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

        .auto-style9 {
            font-weight: bold;
        }

        .auto-style10 {
            width: 239px;
            padding-bottom: 7px;
            font-weight: bold;
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
                margin-top:20px;
                color: #FFFFFF;
                display: block;
                float: left;
                height:40px;
                border-radius:10px 10px 0px 0px;
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
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
   

   
     <section class="content">
        <div class="row">
            <div class="col-lg-12">

                <div class="box box-solid shadowbox " style="border-radius: 15px;">
                    <div class="box-header with-border" style="height:75px;">

                        <div style=" padding-top:0px; width:120px;float:left;">
                            <div id="tabmenu">
                                <h3 class="box-title" style="color: #fff; padding-left: 10px;"><strong>User Group Report</strong></h3>
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
                          <asp:LinkButton ID="btnDelete" Height="40" CssClass="btn btn-app " runat="server" Style="left: 180px; background-color: #1f4f8a; color: white;" data-toggle="modal" data-target="#modal_delete" >
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
                                    <td class="auto-style6"><strong>User Group Name:</strong></td>
                                    <td class="auto-style7">
                                        <div>
                                         <asp:DropDownList ID="lblUserGrpName" CssClass="ddlHeight" Width="250px" runat="server" AutoPostBack="True" OnSelectedIndexChanged="lblUserGrpName_SelectedIndexChanged" ></asp:DropDownList>
                                            <%--<asp:Label ID="lblUserGrpName" runat="server" Font-Italic="False" ForeColor="Black"></asp:Label>--%>


                                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" ControlToValidate="lblUserGrpName" InitialValue="Please select..." Display="None" ErrorMessage="<b> Missing Field</b><br />Please Select User Group Name." ValidationGroup="aaa" />
                                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender4" TargetControlID="RequiredFieldValidator1" runat="server" HighlightCssClass="vali"></ajaxToolkit:ValidatorCalloutExtender>

                                        </div>
                                    </td>
                                    <td class="auto-style4"><span class="auto-style9">Menu</span></td>
                                    <td class="auto-style1 ">
                                       <div>
                                            <asp:DropDownList ID="lblmenu" CssClass="ddlHeight" Width="250px" runat="server"></asp:DropDownList>
                                            <%--<asp:Label ID="lblmenu" runat="server"  Font-Italic="False" ForeColor="Black"></asp:Label>--%>
                                           <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator2" ControlToValidate="lblmenu" InitialValue="Please select..." Display="None" ErrorMessage="<b> Missing Field</b><br />Please Select Menu." ValidationGroup="aaa" />
                                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" TargetControlID="RequiredFieldValidator2" runat="server" HighlightCssClass="vali" PopupPosition="BottomLeft"></ajaxToolkit:ValidatorCalloutExtender>

                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="auto-style3"><span class="auto-style9">Report</span></td>
                                    <td class="auto-style8">
                                        <div>
                                            <asp:DropDownList ID="lblReport" CssClass="ddlHeight" Width="250px" runat="server" ></asp:DropDownList>
                                   
                                             <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator3" ControlToValidate="lblReport" InitialValue="Please select..." Display="None" ErrorMessage="<b> Missing Field</b><br />Please Select Report." ValidationGroup="aaa" />
                                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" TargetControlID="RequiredFieldValidator3" runat="server" HighlightCssClass="vali"></ajaxToolkit:ValidatorCalloutExtender>
                                            <%--<asp:Label ID="lblModule" runat="server"  Font-Italic="False" ForeColor="Black"></asp:Label>--%>
                                        </div>
                                    </td>
                                    <td class="auto-style5"><strong>Sort Order</strong></td>
                                    <td>
                                            <asp:TextBox ID="txtSortOrder" CssClass="form-control texthieht" Width="250px" runat="server" Font-Bold="False" Font-Italic="False" ForeColor="Black"></asp:TextBox>
                                        </td>
                                </tr>
                                <tr>
                                    <td class="auto-style5"><strong>Hide</strong></td>
                                    <td class="padspace">
                                        <asp:RadioButton ID="rdb_Hide_Y" runat="server" GroupName="Hide" Text="YES" />
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                       <asp:RadioButton ID="rdb_Hide_N" runat="server" GroupName="Hide" Text="NO" />
                                    </td>

                                    <td class="auto-style3"><span class="auto-style9">Active</span></td>
                                    <td class="auto-style8">
                                        <asp:Panel ID="GrpActiveStatus" runat="server">
                                            <asp:CheckBox ID="chkActive_Status" runat="server" Text="Active" />
                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        </asp:Panel>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="auto-style3"><span class="auto-style9">Entry User Name</span></td>
                                    <td class="auto-style8">
                                        <div class="backcolorlbl">
                                            <asp:Label ID="lblEntryUSer_Name" runat="server" Font-Italic="False" ForeColor="Black"></asp:Label>
                                        </div>
                                    </td>
                                    <td class="auto-style10">System Date</td>
                                    <td class="padspace">
                                        <div class="backcolorlbl texthieht">
                                            <asp:Label ID="lblSystemDate" runat="server" Font-Italic="False" ForeColor="Black"></asp:Label>
                                        </div>
                                    </td>
                                </tr>
                                      
                                      <tr>
                                          <td class="auto-style3"><strong>Entry Date</strong></td>
                                          <td class="auto-style8">
                                             
                                                  <%--<asp:TextBox ID="txtEntryDate" CssClass="form-control texthieht" Width="250px" runat="server" Font-Bold="False" Font-Italic="False" ForeColor="Black"></asp:TextBox>--%>
                                              <div style="display:inline-block">

                                                        <div class="input-group input-group-sm">
                                                            <asp:TextBox ID="txtEntryDate" CssClass="form-control texthieht" Width="220px" runat="server" Font-Bold="False" Font-Italic="False" ForeColor="Black"  ValidationGroup="aaa"></asp:TextBox>
                                                             <ajaxToolkit:CalendarExtender ID="CalendarExtender1" PopupButtonID="imgPopup" TargetControlID="txtEntryDate" Format="dd-MMM-yyyy" runat="server" />
                                                        <asp:RequiredFieldValidator runat="server" ID="Req2" ControlToValidate="txtEntryDate" Display="None" ErrorMessage="<b> Missing Field</b><br />A Date is required." ValidationGroup="aaa" />
                                                        <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" TargetControlID="Req2" runat="server" HighlightCssClass="vali"></ajaxToolkit:ValidatorCalloutExtender>
                                                            <span class="input-group-btn">
                                                                <%--<button type="button" class="btn btn-info btn-flat">Go!</button>--%>
                                                                <asp:ImageButton ID="imgPopup" CssClass="btn btn-flat" ImageUrl="img/calendar.png" ImageAlign="Bottom" runat="server" />
                                                            </span>
                                                       </div>
                                                       
                                                    </div>
                                              
                                          </td>
                                         <td class="auto-style3"><strong>Action Rights</strong></td>
                                          <td class="auto-style8" colspan="3">
                                              <asp:Panel ID="Panel1" runat="server">
                                                  
                                                  <asp:CheckBox ID="chk_R_View" runat="server" Text="View" />
                                                  &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                  <asp:CheckBox ID="chk_R_print" runat="server" Text="Print" />
                                                 </asp:Panel>
                                          </td>
                                      </tr>
                            </table>
                                       
                                </div>


                             <%--  ENd Modals --%>
                            <div id="Findbox" runat="server">
                                <div class="box">

                                    <!-- /.box-header -->
                                     <div class="box-body" style="width:100%; overflow:auto; height: 50%;">
                                        <%--  ENd Modals --%>
                                          <div style="width: 100%; height: 400px; overflow: scroll">
                                         <asp:GridView ID="GridView1" runat="server" CssClass="table table-bordered table-hover tableFixHead" AutoGenerateColumns="False" OnSelectedIndexChanged="GridView1_SelectedIndexChanged" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Horizontal">
                                            <Columns>
                                                <asp:CommandField ShowSelectButton="True" ButtonType="Image" SelectImageUrl="~/dist/img/edit.png" >
                                                <HeaderStyle Width="20px" />
                                                </asp:CommandField>
                                                <asp:BoundField DataField="Sno" HeaderText="ID" SortExpression="Sno" >
                                                <HeaderStyle Width="20px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Report_Title" HeaderText="Report Title" SortExpression="Report_Title" >
                                                <HeaderStyle Width="100px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Menu_title" HeaderText="Menu Title" SortExpression="Menu_title">
                                                <HeaderStyle Width="100px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="UserGrpName" HeaderText="User Group Name" SortExpression="UserGrpName" >
                                                <HeaderStyle Width="100px" />
                                                </asp:BoundField>
                                                 <asp:BoundField DataField="EntryDate" HeaderText="Entry Date" SortExpression="EntryDate" DataFormatString=" {0:dd-MMM-yyyy}" >
                                                <HeaderStyle Width="100px" />
                                                </asp:BoundField>
                                                <asp:CheckBoxField DataField="IsActive" HeaderText="Active" SortExpression="IsActive" >
                                                <HeaderStyle Width="20px" />
                                                </asp:CheckBoxField>
                                            </Columns>
                                            <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
                                            <HeaderStyle BackColor="#000080" Font-Bold="True" ForeColor="White" Width="250  " />
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

                             <%--  ENd Modals --%>

                            <div id="Deletebox" runat="server">
                                <div class="box">

                                    <!-- /.box-header -->
                                    <div class="box-body" style="width: 100%; overflow: auto; height: 50%;">
                                        <%--  ENd Modals --%>
                                         <div style="width: 100%; height: 400px; overflow: scroll">
                                         <asp:GridView ID="GridView2" runat="server" CssClass="table table-bordered table-hover tableFixHead" AutoGenerateColumns="False"  BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Horizontal" OnSelectedIndexChanged="GridView2_SelectedIndexChanged" >
                                              <Columns>
                                                <asp:CommandField ShowSelectButton="True" ButtonType="Image" SelectImageUrl="~/dist/img/edit.png" >
                                                <HeaderStyle Width="20px" />
                                                </asp:CommandField>
                                                <asp:BoundField DataField="Sno" HeaderText="ID" SortExpression="Sno" >
                                                <HeaderStyle Width="20px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Report_Title" HeaderText="Report Title" SortExpression="Report_Title" >
                                                <HeaderStyle Width="100px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Menu_title" HeaderText="Menu Title" SortExpression="Menu_title">
                                                <HeaderStyle Width="100px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="UserGrpName" HeaderText="User Group Name" SortExpression="UserGrpName" >
                                                <HeaderStyle Width="100px" />
                                                </asp:BoundField>
                                                  <asp:BoundField DataField="EntryDate" HeaderText="Entry Date" SortExpression="EntryDate" DataFormatString=" {0:dd-MMM-yyyy}" >
                                                <HeaderStyle Width="100px" />
                                                </asp:BoundField>
                                                <asp:CheckBoxField DataField="IsActive" HeaderText="Active" SortExpression="IsActive" >
                                                <HeaderStyle Width="20px" />
                                                </asp:CheckBoxField>
                                            </Columns>
                                            <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
                                            <HeaderStyle BackColor="#000080" Font-Bold="True" ForeColor="White" Width="250  " />
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
                                    <div class="box-body" style="width: 100%; overflow: auto; height: 50%;">
                                         <div style="width: 100%; height: 400px; overflow: scroll">
                                        <%--  ENd Modals --%>
                                        <asp:GridView ID="GridView3" runat="server" CssClass="table table-bordered table-hover tableFixHead" AutoGenerateColumns="False" OnSelectedIndexChanged="GridView3_SelectedIndexChanged" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Horizontal">
                                           <Columns>
                                                <asp:CommandField ShowSelectButton="True" ButtonType="Image" SelectImageUrl="~/dist/img/edit.png" >
                                                <HeaderStyle Width="20px" />
                                                </asp:CommandField>
                                                <asp:BoundField DataField="Sno" HeaderText="ID" SortExpression="Sno" >
                                                <HeaderStyle Width="20px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Report_Title" HeaderText="Report Title" SortExpression="Report_Title" >
                                                <HeaderStyle Width="100px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Menu_title" HeaderText="Menu Title" SortExpression="Menu_title">
                                                <HeaderStyle Width="100px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="UserGrpName" HeaderText="User Group Name" SortExpression="UserGrpName" >
                                                <HeaderStyle Width="100px" />
                                                </asp:BoundField>
                                               <asp:BoundField DataField="EntryDate" HeaderText="Entry Date" SortExpression="EntryDate" DataFormatString=" {0:dd-MMM-yyyy}" >
                                                <HeaderStyle Width="100px" />
                                                </asp:BoundField>
                                                <asp:CheckBoxField DataField="IsActive" HeaderText="Active" SortExpression="IsActive" >
                                                <HeaderStyle Width="20px" />
                                                </asp:CheckBoxField>
                                            </Columns>
                                            <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
                                            <HeaderStyle BackColor="#000080" Font-Bold="True" ForeColor="White" Width="250  " />
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


                <div id="Div1" runat="server">
                    <div class="box">

                        <!-- /.box-header -->
                        <div class="box-body" style="width: 100%; overflow: auto; height: 50%;">
                            <%--CssClass="table table-bordered table-hover"--%>

                            <div style="width: 100%; height: 400px; overflow: scroll">

                                <asp:GridView ID="GridView4" runat="server" CssClass="table table-bordered table-hover tableFixHead" AutoGenerateColumns="False" OnSelectedIndexChanged="GridView4_SelectedIndexChanged" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Horizontal" DataKeyNames="ReportId" OnRowDataBound="GridView4_RowDataBound">
                                    <Columns>
                                        <asp:CommandField ButtonType="Image" SelectImageUrl="~/dist/img/iconeditdata.png" ShowSelectButton="True" />
                                        <asp:TemplateField HeaderText="ID" SortExpression="ReportId">
                                            <EditItemTemplate>
                                                <asp:Label ID="lblNatureID" runat="server" Text='<%# Eval("ReportId") %>'></asp:Label>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblNatureID" runat="server" Text='<%# Bind("ReportId") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Width="20px" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Report Name" SortExpression="Report_Title">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("Report_Title") %>'></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lbldoc" runat="server" Text='<%# Bind("Report_Title") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Width="300px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="View " SortExpression="View">
                                            <EditItemTemplate>
                                                <asp:RadioButton ID="rdb_VY" runat="server" Text=" YES" GroupName="rdbdcheck" />
                                                <asp:RadioButton ID="rdb_VN" runat="server" Text=" NO" Checked="true" GroupName="rdbdcheck" />
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:RadioButton ID="rdb_VY" runat="server" Text=" YES" GroupName="rdbdcheck" />
                                                <asp:RadioButton ID="rdb_VN" runat="server" Text=" NO" Checked="true" GroupName="rdbdcheck" />
                                            </ItemTemplate>
                                            <HeaderStyle Width="200px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Hide?">
                                            <EditItemTemplate>
                                                <asp:RadioButton ID="rdb_HYes" runat="server" Text=" YES" GroupName="rdbcheck" />
                                                <asp:RadioButton ID="rdb_NYes" runat="server" Text=" NO" Checked="true" GroupName="rdbcheck" />
                                                <%-- <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("IsHide") %>'></asp:TextBox>--%>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:RadioButton ID="rdb_HYes" runat="server" Text=" YES" GroupName="rdbcheck" />
                                                <asp:RadioButton ID="rdb_NYes" runat="server" Text=" NO" Checked="true" GroupName="rdbcheck" />
                                                <%--<asp:Label ID="Label1" runat="server" Text='<%# Bind("IsHide") %>'></asp:Label>--%>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Print?">
                                            <EditItemTemplate>
                                                <asp:RadioButton ID="rdb_PY" runat="server" Text=" YES" Checked="true" GroupName="rdbcheckMain" />
                                                <asp:RadioButton ID="rdb_PN" runat="server" Text=" NO" GroupName="rdbcheckMain" />
                                                <%-- <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("IsHide") %>'></asp:TextBox>--%>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:RadioButton ID="rdb_PY" runat="server" Text=" YES" Checked="true" GroupName="rdbcheckMain" />
                                                <asp:RadioButton ID="rdb_PN" runat="server" Text=" NO" GroupName="rdbcheckMain" />
                                                <%--<asp:Label ID="Label1" runat="server" Text='<%# Bind("IsHide") %>'></asp:Label>--%>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Apply Date">
                                            <HeaderTemplate>
                                                &nbsp;
                                                            <asp:Label ID="Label2" runat="server" Text="Apply Date"></asp:Label>
                                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;

                                                            <asp:CheckBox ID="chkall" runat="server" AutoPostBack="True" OnCheckedChanged="chkall_CheckedChanged" Text="All" />
                                            </HeaderTemplate>
                                            <EditItemTemplate>
                                                <div style="display: inline-block">

                                                    <div class="input-group input-group-sm">
                                                        <asp:TextBox ID="txtAppleDate123" CssClass="form-control texthieht" Width="220px" runat="server" Font-Bold="False" Font-Italic="False" ForeColor="Black" ValidationGroup="aaab"></asp:TextBox>
                                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender1" PopupButtonID="imgPopup1" TargetControlID="txtAppleDate123" Format="dd-MMM-yyyy" runat="server" />
                                                        <asp:RequiredFieldValidator runat="server" ID="Req212" ControlToValidate="txtAppleDate123" Display="None" ErrorMessage="<b> Missing Field</b><br />A Date is required." ValidationGroup="aaab" />
                                                        <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" TargetControlID="Req212" runat="server" HighlightCssClass="vali"></ajaxToolkit:ValidatorCalloutExtender>
                                                        <span class="input-group-btn">
                                                            <%--<button type="button" class="btn btn-info btn-flat">Go!</button>--%>
                                                            <asp:ImageButton ID="imgPopup1" CssClass="btn btn-flat" ImageUrl="img/calendar.png" ImageAlign="Bottom" runat="server" />
                                                        </span>
                                                    </div>

                                                </div>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <div style="display: inline-block">

                                                    <div class="input-group input-group-sm">
                                                        <asp:TextBox ID="txtAppleDate123" CssClass="form-control texthieht" Width="220px" runat="server" Font-Bold="False" Font-Italic="False" ForeColor="Black" ValidationGroup="aaab"></asp:TextBox>
                                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender1" PopupButtonID="imgPopup1" TargetControlID="txtAppleDate123" Format="dd-MMM-yyyy" runat="server" />
                                                        <asp:RequiredFieldValidator runat="server" ID="Req212" ControlToValidate="txtAppleDate123" Display="None" ErrorMessage="<b> Missing Field</b><br />A Date is required." ValidationGroup="aaab" />
                                                        <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" TargetControlID="Req212" runat="server" HighlightCssClass="vali"></ajaxToolkit:ValidatorCalloutExtender>
                                                        <span class="input-group-btn">
                                                            <%--<button type="button" class="btn btn-info btn-flat">Go!</button>--%>
                                                            <asp:ImageButton ID="imgPopup1" CssClass="btn btn-flat" ImageUrl="img/calendar.png" ImageAlign="Bottom" runat="server" />
                                                        </span>
                                                    </div>

                                                </div>
                                            </ItemTemplate>
                                            <HeaderStyle Width="20px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="">
                                            <EditItemTemplate>
                                                <asp:CheckBox ID="chkSelect" runat="server" />
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkSelect" runat="server" />
                                            </ItemTemplate>
                                            <HeaderStyle Width="20px" />
                                        </asp:TemplateField>


                                        <asp:CommandField />


                                    </Columns>
                                    <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
                                    <HeaderStyle BackColor="#000080" Font-Bold="True" ForeColor="White" />
                                    <PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="Right" />

                                    <SortedAscendingCellStyle BackColor="#F7F7F7" />
                                    <SortedAscendingHeaderStyle BackColor="#4B4B4B" />
                                    <SortedDescendingCellStyle BackColor="#E5E5E5" />
                                    <SortedDescendingHeaderStyle BackColor="#242121" />
                                </asp:GridView>

                                <%--  <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:db_HRERPSys %>" SelectCommand="SELECT [ReportId], [Report_Title], [View],  [Hide] , [Print], [EntryDate] FROM [ViewReportUSER] order by ReportId asc"></asp:SqlDataSource>--%>
                            </div>
                            </div>
                      
                        <!-- /.box-body -->
                    </div>
                    <div style="float: right;">
                        <asp:Button ID="Button1" runat="server" CssClass="btn btn-primary" Text="Save" Width="120px" OnClick="Button1_Click" />
                        <asp:Button ID="Button2" runat="server" CssClass="btn btn-default" Text="Cancel" Width="120px" />
                    </div>
               
           

            </div>
        </div>

    </section>
             

     <%--  ENd Modals --%>
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
                            <td><strong>Report Title</strong> </td>
                            <td>
                               <%--<asp:TextBox ID="txtEdit_Report" CssClass="form-control gap" runat="server"></asp:TextBox>--%>
                                 <asp:DropDownList ID="txtEdit_Report" CssClass="form-control select2" width="100%"  runat="server"></asp:DropDownList>
                            </td>
                        </tr>
                         <tr>
                            <td><strong>Menu Title</strong> </td>
                            <td>
                                <%--<asp:TextBox ID="txtEdit_menutitle" CssClass="form-control gap" runat="server"></asp:TextBox>--%>
                                 <asp:DropDownList ID="txtEdit_menutitle" CssClass="form-control select2" width="100%"  runat="server"></asp:DropDownList>
                            </td>

                        </tr>
                         <tr>
                            <td><strong>User Group Name</strong> </td>
                            <td>
                                <%--<asp:TextBox ID="txtEdit_UserGrpname" CssClass="form-control gap" runat="server"></asp:TextBox>--%>
                                 <asp:DropDownList ID="txtEdit_UserGrpname" CssClass="form-control select2" width="100%"  runat="server"></asp:DropDownList>
                            </td>

                        </tr>
                         <tr>
                            <td><strong>Active</strong> </td>
                            <td>
                                  <asp:CheckBox ID="ChkEdit_Active" runat="server" Checked="true" Text="Active" />
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
                    <h4 class="modal-title">Find </h4>
                </div>
                <div class="modal-body">

                <table style="width: 100%;">
                       <tr>
                            <td><strong>Report Title</strong> </td>
                            <td>
                               <%--<asp:TextBox ID="txtFind_ReportTitle" CssClass="form-control gap" runat="server"></asp:TextBox>--%>
                                 <asp:DropDownList ID="txtFind_ReportTitle" CssClass="form-control select2" width="100%"  runat="server"></asp:DropDownList>
                                
                            </td>

                        </tr>
                         <tr>
                            <td><strong>Menu Title</strong> </td>
                            <td>
                                <%--<asp:TextBox ID="txtFind_MenuTitle" CssClass="form-control gap" runat="server"></asp:TextBox>--%>
                                 <asp:DropDownList ID="txtFind_MenuTitle" CssClass="form-control select2" width="100%"  runat="server"></asp:DropDownList>
                            </td>
                        </tr>
                         <tr>
                            <td><strong>User Group Name</strong> </td>
                            <td>
                                <%--<asp:TextBox ID="txtFind_UserGrpName" CssClass="form-control gap" runat="server"></asp:TextBox>--%>
                                 <asp:DropDownList ID="txtFind_UserGrpName" CssClass="form-control select2" width="100%"  runat="server"></asp:DropDownList>
                            </td>
                        </tr>
                         <tr>
                            <td><strong>Active</strong> </td>
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
                            <td><strong>Report Title</strong> </td>
                            <td>
                               <%--<asp:TextBox ID="txtDel_reportName" CssClass="form-control gap" runat="server"></asp:TextBox>--%>
                                  <asp:DropDownList ID="txtDel_reportName" CssClass="form-control select2" width="100%"  runat="server"></asp:DropDownList>

                                <%--  ENd Modals --%>
                            </td>

                        </tr>
                         <tr>
                            <td><strong>Menu Title</strong> </td>
                            <td>
                                <%--<asp:TextBox ID="txtDel_MenuTitle" CssClass="form-control gap" runat="server"></asp:TextBox>--%>
                                  <asp:DropDownList ID="txtDel_MenuTitle" CssClass="form-control select2" width="100%"  runat="server"></asp:DropDownList>
                            </td>

                        </tr>
                         <tr>
                            <td><strong>User Group Name</strong> </td>
                            <td>
                                <%--<asp:TextBox ID="txtDel_UserGrpName" CssClass="form-control gap" runat="server"></asp:TextBox>--%>
                                  <asp:DropDownList ID="txtDel_UserGrpName" CssClass="form-control select2" width="100%"  runat="server"></asp:DropDownList>
                            </td>

                        </tr>
                         <tr>
                            <td><strong>Active</strong> </td>
                            <td>
                                  <asp:CheckBox ID="chkDel_Active" runat="server" Checked="true" Text="Active" />
                            </td>

                        </tr>

                    </table>

                </div>

                <div class="modal-footer">
                    <button type="button" class="btn btn-default pull-left" data-dismiss="modal">Close</button>

                    <asp:Button ID="btn_sdelete" CssClass="btn btn-primary" runat="server" Text="Search" OnClick="btn_delete_Click" />

                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>
    <%--  ENd Modals --%>


         <script src="https://unpkg.com/sweetalert/dist/sweetalert.min.js"></script>

</asp:Content>

