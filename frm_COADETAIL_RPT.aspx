<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="frm_COADETAIL_RPT.aspx.cs" Inherits="frm_Period" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script>
        function alertme() {
            swal("Insert Successfuly!", "record has been saved!!!", "success")
                .then(function () {

                    window.location = "frm_CostCenter.aspx?UserID=" + userid + "&UsergrpID=" + UserGrpID + "&fiscaly=" + fiscal + "&FormID=" + FormID + "";
                });

        }


        function alertFileGenerated() {
            swal("Insert Successfuly!", "record has been saved!!!", "success")
                .then(function () {

                    window.location = "frm_CostCenter.aspx?UserID=" + userid + "&UsergrpID=" + UserGrpID + "&fiscaly=" + fiscal + "&FormID=" + FormID + "";
                });

        }

        function Editalert() {
            swal("Update Successfuly!", "record has been saved!!!", "success")
                .then(function () {
                    window.location = "frm_CostCenter.aspx?UserID=" + userid + "&UsergrpID=" + UserGrpID + "&fiscaly=" + fiscal + "&FormID=" + FormID + "";
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
                window.location = "frm_CostCenter.aspx?UserID=" + userid + "&UsergrpID=" + UserGrpID + "&fiscaly=" + fiscal + "&FormID=" + FormID + "";
            });
        }

        function myfocus(elementRef) {
            //  elementRef.style.backgroundColor = "yellow";

        }

        function myBlur(elementRef) {
            elementRef.style.backgroundColor = "red";

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

        .auto-style3 {
            width: 173px;
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
        .auto-style11 {
            padding-bottom: 7px;
            width: 360px;
        }
        .linee{
            display:inline-block;
        }
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
                                <h3 class="box-title" style="color: #fff; padding-left: 10px;"><strong>COA Detail Report</strong></h3>
                            </div>
                        </div>
                        <div style="height: 60px; width: 650px; margin-left: 310px;">
                            <asp:LinkButton ID="btnInsert" Height="40" CssClass="btn btn-app" runat="server" Style="left: 180px; background-color: #1f4f8a; color: white;" OnClick="btnInsert_Click">
                     <i class="fas fa-print"></i>&nbsp Print
                            </asp:LinkButton>
                           
                              <asp:LinkButton ID="LinkButton1" Height="40" CssClass="btn btn-app" runat="server" Style="left: 180px; background-color: #1f4f8a; color: white;" OnClick="btnGenerateExcel_Click">
                     <i class="fas fa-print"></i>&nbsp Generate Excel
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
                                                <td class="auto-style6"><strong>Account Code</strong></td>
                                                <td class="auto-style11">

                                                    <asp:DropDownList ID="ddlAcct_code" Width="250px" CssClass=" form-control select2" runat="server" ValidationGroup="aaa" AutoPostBack="True" OnSelectedIndexChanged="ddlItemTypes_SelectedIndexChanged"></asp:DropDownList>
                                                    <asp:RequiredFieldValidator runat="server" ID="Req" ControlToValidate="ddlAcct_code" InitialValue="Please select..." Display="None" ErrorMessage="<b> Missing Field</b><br />Please Select Item Type." ValidationGroup="aaa" />
                                                    <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" TargetControlID="Req" runat="server" HighlightCssClass="vali" PopupPosition="Left"></ajaxToolkit:ValidatorCalloutExtender>
                                                </td>
                                               <%-- <td class="auto-style6"><strong>Item Head</strong></td>
                                                <td class="auto-style11">
                                                    <asp:DropDownList ID="ddlItemsHead" Width="250px" CssClass=" form-control select2" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlItemsHead_SelectedIndexChanged"></asp:DropDownList>
                                                    <asp:RequiredFieldValidator runat="server" ID="req2" ControlToValidate="ddlItemsHead" InitialValue="Please select..." Display="None" ErrorMessage="<b> Missing Field</b><br />Please Select Item Head." ValidationGroup="aaa" />
                                                    <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" TargetControlID="req2" runat="server" HighlightCssClass="vali" PopupPosition="Left"></ajaxToolkit:ValidatorCalloutExtender>
                                                </td>
                                            </tr>
                                            <tr>
                                                
                                                <td class="auto-style6"><strong>Item Sub Head</strong></td>
                                                <td class="auto-style11">
                                                    <asp:DropDownList ID="ddlItemSubHead" Width="250px" CssClass="form-control select2" runat="server" ></asp:DropDownList>

                                                </td>--%>
                                                 <td class="auto-style6"><strong>Head Level <asp:Label ID="lblSteriek" runat="server" Text="" ForeColor="Red"></asp:Label></strong></td>
                                                <td class="auto-style11">
                                                   <asp:DropDownList ID="ddlHeadLevel" Width="250px" CssClass=" form-control select2" runat="server" ValidationGroup="aaa" AutoPostBack="True" OnSelectedIndexChanged="ddlItemTypes_SelectedIndexChanged">
                                                       <asp:ListItem Value="0">Please select...</asp:ListItem>
                                                       <asp:ListItem Value="1">H1</asp:ListItem>
                                                       <asp:ListItem Value="2">H2</asp:ListItem>
                                                       <asp:ListItem Value="3">H3</asp:ListItem>
                                                       <asp:ListItem Value="4">H4</asp:ListItem>
                                                       <asp:ListItem Value="5">H5</asp:ListItem>
                                                       <asp:ListItem Value="6">H6</asp:ListItem>
                                                       <asp:ListItem Value="7">H7</asp:ListItem>
                                                       <asp:ListItem Value="8">H8</asp:ListItem>
                                                       <asp:ListItem Value="9">H9</asp:ListItem>
                                                       <asp:ListItem Value="10">D1</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" ControlToValidate="ddlAcct_code" InitialValue="Please select..." Display="None" ErrorMessage="<b> Missing Field</b><br />Please Select Item Type." ValidationGroup="aaa" />
                                                    <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" TargetControlID="Req" runat="server" HighlightCssClass="vali" PopupPosition="Left"></ajaxToolkit:ValidatorCalloutExtender>

                                                </td>

                                            </tr>
                                            
                                             </table>
                                 
                            </div>



                            <%-- Table View --%>
                       

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

           
    <%-- Start Modals --%>
    <div class="modal fade" id="modal_edit">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title">Edit</h4>
                </div>
                <div class="modal-body">


                    <table style="width: 100%;">
                        <tr>
                            <td><strong>Item Sub Head Name</strong> </td>
                            <td>
                                <%--<asp:TextBox ID="txtEdit_ItemSubHead" CssClass="form-control gap" runat="server"></asp:TextBox>--%>
                                <asp:DropDownList ID="ddlEdit_ItemSubHead" CssClass="select2"  Width="100%" runat="server"></asp:DropDownList>

                            </td>
                        </tr>
                       
                        <tr>
                            <td><strong>Active</strong> </td>
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
                    <h4 class="modal-title">Find </h4>
                </div>
                <div class="modal-body">


                    <table style="width: 100%;">
                        <tr>
                            <td><strong>Item Sub Head Name</strong> </td>
                            <td> o
                                <asp:DropDownList ID="ddlFind_ItemSubHeadName" CssClass="select2"  Width="100%" runat="server"></asp:DropDownList>

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
                    <h4 class="modal-title">Delete</h4>
                </div>
                <div class="modal-body">

                    <table style="width: 100%;">
                        <tr>
                            <td><strong>Item Sub Head Name</strong> </td>
                            <td>
                                <asp:DropDownList ID="ddlDel_ItemSubHeadName" CssClass="select2"  Width="100%" runat="server"></asp:DropDownList>

                            </td>
                        </tr>
                      <tr>
                            <td><strong>Active</strong> </td>
                            <td>
                                <asp:CheckBox ID="chkdelete_Active" runat="server" Checked="true" Text="Active" />
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
    <%--  ENd Modals --%>

    <script src="https://unpkg.com/sweetalert/dist/sweetalert.min.js"></script>


</asp:Content>

