<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="frm_SET_UserCompBramchFyear.aspx.cs" Inherits="frm_SET_COA_Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
     <script>
        function alertme() {
            swal("Insert Successfuly!", "record has been saved!!!", "success")
                .then(function () {
                    window.location = "frm_SET_UserCompBramchFyear.aspx?UserID=" + userid + "&UsergrpID=" + UserGrpID + "&fiscaly=" + fiscal + "&FormID=" + FormID + "";
            });
           
        }

        function Editalert() {
            swal("Update Successfuly!", "record has been saved!!!", "success")
                .then(function () {
                    window.location = "frm_SET_UserCompBramchFyear.aspx?UserID=" + userid + "&UsergrpID=" + UserGrpID + "&fiscaly=" + fiscal + "&FormID=" + FormID + "";
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
                    window.location = "frm_SET_UserCompBramchFyear.aspx?UserID=" + userid + "&UsergrpID=" + UserGrpID + "&fiscaly=" + fiscal + "&FormID=" + FormID + "";
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
            padding-left: 10px;
            padding-top: 5px;
            background-color: #eeeeee;
        }

        .texthieht {
            height: 25px;
        }


        .padspace {
            padding-bottom: 0px;
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
        .auto-style1 {
            height: 54px;
        }
        .auto-style3 {
            height: 36px;
        }
        .auto-style4 {
            height: 44px;
        }
        .auto-style5 {
            height: 40px;
        }
        .auto-style6 {
            height: 40px;
            width:10%;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <div class="container shadow-lg" runat="server" id="Fiscal_Error">
        <p class="text-center" style="color:darkred;" > CAN NOT INSERT DATA WITHOUT FISCAL YEAR</p>
    </div>

     <section class="content">
        <div class="row">
            <div class="col-lg-12">

                <div class="box box-solid shadowbox " style="border-radius: 15px;">
                    <div class="box-header with-border" style="height:75px;">

                        <div style=" padding-top:0px; width:120px;float:left;">
                            <div id="tabmenu">
                                <h3 class="box-title" style="color: #fff; padding-left: 10px;"><strong>User Permission</strong></h3>
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
                                    <td class="auto-style6"><strong>User Name</strong></td>
                                    <td class="auto-style6">
                                        
                                            <asp:DropDownList ID="lblUserName" CssClass="ddlHeight select2" Width="250px" runat="server" ></asp:DropDownList>
                                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" InitialValue="Please select..." ControlToValidate="lblUserName" Display="None" ErrorMessage="<b> Missing Field</b><br />Please select Username." ValidationGroup="aaa" />
                                       <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" TargetControlID="RequiredFieldValidator1" runat="server" HighlightCssClass="vali"></ajaxToolkit:ValidatorCalloutExtender>
                                            <%--<asp:Label ID="lblUserGrpName" runat="server" Font-Italic="False" ForeColor="Black"></asp:Label>--%>
                                        <asp:Label ID="Hide_lblusergrpname" Visible="false"  runat="server" Text="Label"></asp:Label>
                                        
                                    </td>
                                    <td class="auto-style6"><strong>Goc Name</strong></td>
                                    <td class="auto-style6">
                                       
                                          <div class="backcolorlbl texthieht">
                                                  <asp:Label ID="lblGocName" runat="server" Font-Italic="False" ForeColor="Black"></asp:Label>
                                              </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="auto-style3"><strong>Company All</strong></td>
                                    <td class="auto-style8">
                                       <asp:Panel ID="Panel3" runat="server">
                                            <asp:CheckBox ID="chkCompAll" runat="server" Text="All" AutoPostBack="True" OnCheckedChanged="chkCompAll_CheckedChanged" />
                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        </asp:Panel>
                                           
                                    </td>
                                    <td class="auto-style5"><strong>Company Name</strong></td>
                                    <td>
                                         <asp:DropDownList ID="ddlComp" CssClass="ddlHeight select2" Width="250px" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlComp_SelectedIndexChanged" ></asp:DropDownList>
                                        <br />
                                        <asp:Label ID="lblcomp" runat="server" Text="Label" ForeColor="Red"></asp:Label>
                                        </td>
                                </tr>
                                <tr>
                                    <td class="auto-style5"><strong>Branch All</strong></td>
                                    <td class="padspace">
                                      
                                        <asp:Panel ID="Panel4" runat="server">
                                            <asp:CheckBox ID="chkBranchAll" runat="server" Text="All" AutoPostBack="True" OnCheckedChanged="chkBranchAll_CheckedChanged" />
                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        </asp:Panel>
                                    </td>

                                    <td class="auto-style3"><strong>Branch Name</strong></td>
                                    <td class="padspace">
                                         <asp:DropDownList ID="ddlBranch" CssClass="ddlHeight select2" Width="250px" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged" ></asp:DropDownList>
                                        <br />
                                        <asp:Label ID="lblbranch" runat="server" Text="Label" ForeColor="Red"></asp:Label>
                                    </td>
                                    
                                     
                                </tr>
                                <tr>
                                    <td class="auto-style3"><strong>Fiscal Year All</strong></td>
                                    <td class="auto-style8">
                                        <asp:Panel ID="Panel5" runat="server">
                                            <asp:CheckBox ID="chkFYearAll" runat="server" Text="All" OnCheckedChanged="chkFYearAll_CheckedChanged" AutoPostBack="True" />
                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        </asp:Panel>
                                    </td>
                                    <td class="auto-style10"><strong>Fiscal Year</strong></td>
                                   <td class="auto-style8">
                                        <asp:DropDownList ID="ddlFiscalYear" CssClass="ddlHeight select2" Width="250px" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlFiscalYear_SelectedIndexChanged" ></asp:DropDownList>
                                       <br />
                                       <asp:Label ID="lblfyear" runat="server" Text="Label" ForeColor="Red"></asp:Label>
                                    </td>
                                </tr>
                                      <tr>
                                          <td class="auto-style3"><strong>Entry Date</strong></td>
                                          <td class="auto-style8">
                                             
                                         <%-- <asp:TextBox ID="txtEntryDate" CssClass="form-control texthieht" Width="250px" runat="server" Font-Bold="False" Font-Italic="False" ForeColor="Black"></asp:TextBox>--%>

                                               <div style="display:inline-block">

                                                        <div class="input-group input-group-sm">
                                                            <asp:TextBox ID="txtEntryDate" CssClass="form-control texthieht" Width="220px" runat="server" Font-Bold="False" Font-Italic="False" ForeColor="Black"  ValidationGroup="aaa" ></asp:TextBox>
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
                                          <td class="auto-style10"><strong>System Date</strong></td>
                                          <td class="padspace">
                                             
                                              <div class="backcolorlbl texthieht">
                                            <asp:Label ID="lblSystemDate" runat="server" Font-Italic="False" ForeColor="Black"></asp:Label>
                                        </div>
                                          </td>
                                      </tr>

                                      <tr>
                                          <td class="auto-style3"><strong>Sort Order</strong></td>
                                          <td class="auto-style8">
                                              <asp:TextBox ID="txtSortORder" CssClass="form-control texthieht" Width="250px" runat="server" Font-Bold="False" Font-Italic="False" ForeColor="Black"></asp:TextBox>
                                          </td>
                                          <td class="auto-style3"><strong>Entry User Name</strong></td>
                                          <td class="auto-style8" colspan="3">
                                              <div class="backcolorlbl texthieht">
                                                  <asp:Label ID="lblEntryUserName" runat="server" Font-Italic="False" ForeColor="Black"></asp:Label>
                                              </div>
                                          </td>

                                      </tr>
                                      <tr>
                                          <td class="auto-style3"><strong>Active</strong></td>
                                          <td class="auto-style8">
                                             
                                                <asp:Panel ID="Panel1" runat="server">
                                            <asp:CheckBox ID="chkActive" runat="server" Text="Active" />
                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        </asp:Panel>
                                              
                                          </td>
                                          <td class="auto-style10"><strong>Remarks</strong></td>
                                          <td class="padspace">
                                              <asp:TextBox ID="txtRemarks" CssClass="form-control texthieht" Width="250px" runat="server" Font-Bold="False" Font-Italic="False" ForeColor="Black"></asp:TextBox>
                                          </td>
                                      </tr>
                            </table>
                                             
                                       
                                </div>
                          

                             <%--  ENd Modals --%>
                            <div id="Findbox" runat="server">
                                <div class="box">

                                    <!-- /.box-header -->
                                    <div class="box-body" style="width: 100%; overflow: auto; height: 50%;">
                                        <%--  ENd Modals --%>


                                          <asp:GridView ID="GridView1" runat="server" CssClass="table table-bordered table-hover tableFixHead" AutoGenerateColumns="False" OnSelectedIndexChanged="GridView1_SelectedIndexChanged" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Horizontal" DataKeyNames="Sno">
                                            <Columns>
                                                <asp:CommandField ShowSelectButton="True" ButtonType="Image" SelectImageUrl="~/dist/img/edit.png" />
                                                <asp:BoundField DataField="Sno" HeaderText="ID" SortExpression="Sno" ReadOnly="True" />
                                                <asp:BoundField DataField="user_name" HeaderText="User Name" SortExpression="user_name"></asp:BoundField>
                                                <asp:BoundField DataField="GOCName" HeaderText="Group of Company" SortExpression="GOCName" />
                                                <asp:BoundField DataField="CompName" HeaderText="Company Name" SortExpression="CompName" />
                                                <asp:BoundField DataField="BranchName" HeaderText="Branch Name" SortExpression="BranchName" />
                                                <asp:BoundField DataField="Description" HeaderText="Fiscal Year" SortExpression="Description" />
                                                <asp:BoundField DataField="CompAll" HeaderText="CompAll" SortExpression="CompAll" />
                                                <asp:BoundField DataField="BranchAll" HeaderText="BranchAll" SortExpression="BranchAll" />
                                                <asp:BoundField DataField="FiscalYearAll" HeaderText="FiscalYearAll" SortExpression="FiscalYearAll" />
                                                <asp:BoundField DataField="EntryDate" HeaderText="Entry Date" SortExpression="EntryDate" DataFormatString = "{0:dd-MMM-yyyy}" >
                                                    <HeaderStyle Width="200px" />
                                                    </asp:BoundField>
                                                <asp:BoundField DataField="active" HeaderText="Active" SortExpression="active" />
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
                                    <!-- /.box-body -->
                                </div>
                            </div>

                             <%--  ENd Modals --%>

                            <div id="Deletebox" runat="server">
                                <div class="box">

                                    <!-- /.box-header -->
                                    <div class="box-body" style="width: 100%; overflow: auto; height: 50%;">
                                        <%--  ENd Modals --%>

                                 <asp:GridView ID="GridView2" runat="server" CssClass="table table-bordered table-hover tableFixHead" AutoGenerateColumns="False"  BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Horizontal" OnSelectedIndexChanged="GridView2_SelectedIndexChanged" OnRowDataBound="GridView2_RowDataBound" >
                                              <Columns>
                                                <asp:CommandField ShowSelectButton="True" ButtonType="Image" SelectImageUrl="~/dist/img/edit.png" />
                                                <asp:BoundField DataField="Sno" HeaderText="ID" SortExpression="Sno" ReadOnly="True" />
                                                <asp:BoundField DataField="user_name" HeaderText="User Name" SortExpression="user_name"></asp:BoundField>
                                                <asp:BoundField DataField="GOCName" HeaderText="Group of Company" SortExpression="GOCName" />
                                                <asp:BoundField DataField="CompName" HeaderText="Company Name" SortExpression="CompName" />
                                                <asp:BoundField DataField="BranchName" HeaderText="Branch Name" SortExpression="BranchName" />
                                                <asp:BoundField DataField="Description" HeaderText="Fiscal Year" SortExpression="Description" />
                                                <asp:BoundField DataField="CompAll" HeaderText="CompAll" SortExpression="CompAll" />
                                                <asp:BoundField DataField="BranchAll" HeaderText="BranchAll" SortExpression="BranchAll" />
                                                <asp:BoundField DataField="FiscalYearAll" HeaderText="FiscalYearAll" SortExpression="FiscalYearAll" />
                                                  <asp:BoundField DataField="EntryDate" HeaderText="Entry Date" SortExpression="EntryDate" DataFormatString = "{0:dd-MMM-yyyy}" >
                                                    <HeaderStyle Width="200px" />
                                                    </asp:BoundField>
                                                <asp:BoundField DataField="active" HeaderText="Active" SortExpression="active" />
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
                                    <!-- /.box-body -->
                                </div>
                            </div>

                            <div id="Editbox" runat="server">
                                <div class="box">

                                    <!-- /.box-header -->
                                    <div class="box-body" style="width: 100%; overflow: auto; height: 50%;">
                                        <%--  ENd Modals --%>
                                        <asp:GridView ID="GridView3" runat="server" CssClass="table table-bordered table-hover tableFixHead" AutoGenerateColumns="False"  OnSelectedIndexChanged="GridView3_SelectedIndexChanged" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Horizontal" OnRowDataBound="GridView3_RowDataBound">
                                          <Columns>
                                                <asp:CommandField ShowSelectButton="True" ButtonType="Image" SelectImageUrl="~/dist/img/edit.png" />
                                                <asp:BoundField DataField="Sno" HeaderText="ID" SortExpression="Sno" ReadOnly="True" />
                                                <asp:BoundField DataField="user_name" HeaderText="User Name" SortExpression="user_name"></asp:BoundField>
                                                <asp:BoundField DataField="GOCName" HeaderText="Group of Company" SortExpression="GOCName" />
                                                <asp:BoundField DataField="CompName" HeaderText="Company Name" SortExpression="CompName" />
                                                <asp:BoundField DataField="BranchName" HeaderText="Branch Name" SortExpression="BranchName" />
                                                <asp:BoundField DataField="Description" HeaderText="Fiscal Year" SortExpression="Description" />
                                                <asp:BoundField DataField="CompAll" HeaderText="CompAll" SortExpression="CompAll" />
                                                <asp:BoundField DataField="BranchAll" HeaderText="BranchAll" SortExpression="BranchAll" />
                                                <asp:BoundField DataField="FiscalYearAll" HeaderText="FiscalYearAll" SortExpression="FiscalYearAll" />
                                              <asp:BoundField DataField="EntryDate" HeaderText="Entry Date" SortExpression="EntryDate" DataFormatString = "{0:dd-MMM-yyyy}" >
                                                    <HeaderStyle Width="200px" />
                                                    </asp:BoundField>
                                                <asp:BoundField DataField="active" HeaderText="Active" SortExpression="active" />
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
                            <td><strong>User Name</strong> </td>
                            <td>
                                <asp:DropDownList ID="ddlEdit_UserName" CssClass="form-control select2" Width="100%" runat="server"></asp:DropDownList>

                                <%--  ENd Modals --%>
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
                            <td><strong>User Name</strong> </td>
                            <td>
                                <asp:DropDownList ID="ddlFind_UserName" CssClass="form-control select2" Width="100%" runat="server"></asp:DropDownList>

                                <%--  ENd Modals --%>
                            </td>

                        </tr>
                        
                         <tr>
                            <td><strong>Active</strong> </td>
                            <td>
                                  <asp:CheckBox ID="ChkFInd_Active" runat="server" Checked="true" Text="Active" />
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
                            <td><strong>User Name</strong> </td>
                            <td>
                                <asp:DropDownList ID="ddlDel_UserName" CssClass="form-control select2" Width="100%" runat="server"></asp:DropDownList>

                                <%--  ENd Modals --%>
                            </td>

                        </tr>
                         <tr>
                            <td><strong>Active</strong> </td>
                            <td>
                                  <asp:CheckBox ID="chkDelete_Active" runat="server" Checked="true" Text="Active" />
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

