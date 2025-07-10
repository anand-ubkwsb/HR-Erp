<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="frm_PurchaseOrder.aspx.cs" Inherits="frm_Period"  EnableEventValidation="false"%>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script>
        function alertme() {
            swal("Insert Successfuly!", "record has been saved!!!", "success")
                .then(function () {
                    window.location = "frm_Set_Department.aspx?UserID=" + userid + "&UsergrpID=" + UserGrpID + "&fiscaly=" + fiscal + "&FormID=" + FormID + "";
                });

        }

        function alertEmail() {
            swal("Email Send Successfully!", "Email has been Sent to all suppplier!!!", "success")
                .then(function () {
                    window.location = "frm_Set_Department.aspx?UserID=" + userid + "&UsergrpID=" + UserGrpID + "&fiscaly=" + fiscal + "&FormID=" + FormID + "";
                });

        }

        function Editalert() {
            swal("Update Successfuly!", "record has been saved!!!", "success")
                .then(function () {
                    window.location = "frm_Set_Department.aspx?UserID=" + userid + "&UsergrpID=" + UserGrpID + "&fiscaly=" + fiscal + "&FormID=" + FormID + "";
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
                window.location = "frm_Set_Department.aspx?UserID=" + userid + "&UsergrpID=" + UserGrpID + "&fiscaly=" + fiscal + "&FormID=" + FormID + "";
            });
        }
    </script>
    <style type="text/css">

        .modalBackground
        {
            background-color: Black;
            filter: alpha(opacity=90);
            opacity: 0.8;
        }
        .modalPopup
        {
            background-color: #fff;
            border: 3px solid #ccc;
            padding: 10px;
            width: 300px;
        }
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

        .rowpadd {
            padding-bottom: 8px;
        }

        .rowpadd1 {
            padding-top: 15px;
        }

        .padspace {
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
            width: 271px;
        }

        .rdbgap {
            padding-right: 30px;
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
                                <h3 class="box-title" style="color: #fff; padding-left: 10px;"><strong>Purchase Order</strong></h3>
                            </div>
                        </div>
                        <div style="height: 60px; width: 650px; margin-left: 310px;">
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
                             <asp:LinkButton ID="btnUpdatePO" Height="40" CssClass="btn btn-app" runat="server" Style="left: 180px; background-color: #1f4f8a; color: white;" OnClick="btnUpdatePO_Click">
                     <i class="fas fa-remove"></i>&nbsp Update PO
                            </asp:LinkButton>
                        </div>

                    </div>


                    <div class="form-row">

                        <div class="box-body" style="background-color: #cddbf2;">
                            <div id="fieldbox" runat="server">

                                <table id="tablecontent1" runat="server" style="width: 100%;">
                                    <tr>
                                        <td class="auto-style6"><strong>PO Document Name</strong></td>
                                        <td class="auto-style11">

                                            <asp:DropDownList ID="ddlPODocName" Width="250px" CssClass=" form-control select2" runat="server" ValidationGroup="aaa" AutoPostBack="True" OnSelectedIndexChanged="ddlPODocName_SelectedIndexChanged" ></asp:DropDownList>
                                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" ControlToValidate="ddlPODocName" InitialValue="Please select..." Display="None" ErrorMessage="<b> Missing Field</b><br />Please Select Document." ValidationGroup="aaa" />
                                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" TargetControlID="RequiredFieldValidator1" runat="server" HighlightCssClass="vali" PopupPosition="Right"></ajaxToolkit:ValidatorCalloutExtender>
                                        </td>
                                         <td class="auto-style6"><strong>Entry Date </strong></td>
                                        <td class="auto-style11">

                                            <div style="display: inline-block">

                                                <div class="input-group input-group-sm">
                                                    <asp:TextBox ID="txtEntryDate" CssClass="form-control texthieht" Width="220px" runat="server" Font-Bold="False" Font-Italic="False" ForeColor="Black" ValidationGroup="aaa" AutoPostBack="True" OnTextChanged="txtEntryDate_TextChanged"></asp:TextBox>
                                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender1" PopupButtonID="imgPopup" TargetControlID="txtEntryDate" Format="dd-MMM-yyyy" runat="server" />
                                                    <asp:RequiredFieldValidator runat="server" ID="Req2" ControlToValidate="txtEntryDate" Display="None" ErrorMessage="<b> Missing Field</b><br />A Date is required." ValidationGroup="aaa" />
                                                    <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender11" TargetControlID="Req2" runat="server" HighlightCssClass="vali"></ajaxToolkit:ValidatorCalloutExtender>
                                                    <span class="input-group-btn">
                                                        
                                                        <asp:ImageButton ID="imgPopup" CssClass="btn btn-flat" ImageUrl="img/calendar.png" ImageAlign="Bottom" runat="server" />
                                                    </span>
                                                </div>

                                            </div>

                                        </td>
                                          <td class="auto-style6"><strong>PO Document No</strong></td>
                                        <td class="auto-style11">

                                            <asp:TextBox ID="txtPODocNo" CssClass="texthieht" Width="250px" runat="server" Font-Bold="False" Font-Italic="False" ForeColor="Black" Height="25px" ValidationGroup="aaa"></asp:TextBox>
                                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator8" ControlToValidate="txtPODocNo" InitialValue="Please select..." Display="None" ErrorMessage="<b> Missing Field</b><br />Please Select Document." ValidationGroup="aaa" />
                                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender10" TargetControlID="RequiredFieldValidator8" runat="server" HighlightCssClass="vali" PopupPosition="Right"></ajaxToolkit:ValidatorCalloutExtender>
                                        </td>

                                    </tr>
                                    <tr>

                                         <td class="auto-style6"><strong>PO</strong></td>
                                        <td class="auto-style11">
                                            
                                            <asp:RadioButton ID="chkDirect" Text="Direct" runat="server" AutoPostBack="True" OnCheckedChanged="chkDirect_CheckedChanged" />

                                             <asp:RadioButton ID="chkPRNo" Text="PR" runat="server" AutoPostBack="True" OnCheckedChanged="chkPRNo_CheckedChanged" />

                                        </td>
                                         <td class="auto-style6"><strong>Supplier</strong></td>
                                        <td class="auto-style11">
                                            <asp:DropDownList ID="ddlSupplier" Width="250px" CssClass=" form-control select2" runat="server" ValidationGroup="aaa">
                                            </asp:DropDownList>

                                        </td>

                                        <td class="auto-style6"><strong>Appr. Quotation/Pur Req.</strong></td>
                                        <td class="auto-style11">
                                             <div>
                                                <asp:ListBox ID="lstFruits" runat="server" SelectionMode="Multiple" OnSelectedIndexChanged="lstFruits_SelectedIndexChanged" AutoPostBack="True"  Width="250px" DataTextField="RequisitionNo" DataValueField="Sno"></asp:ListBox>
                                                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:db_HRERPSys %>" SelectCommand="select StockReqMId,RequisitionNo from V_PurchaseOrder_DirectPRNo"></asp:SqlDataSource>                                               
                                            </div>

                                        </td>
                                        </tr>
                                    <tr>
                                        <td class="auto-style6"><strong>Document Authority</strong></td>
                                        <td class="auto-style11">
                                            
                                           
                                            <asp:DropDownList ID="ddlDocAuth" Width="250px" CssClass=" form-control select2" runat="server" ValidationGroup="aaa">
                                                </asp:DropDownList>
                                            
                                             <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator4" ControlToValidate="ddlDocAuth" Display="None" ErrorMessage="<b> Missing Field</b><br />Please input document name." ValidationGroup="aaa" />
                                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender6" TargetControlID="RequiredFieldValidator4" runat="server" HighlightCssClass="vali"></ajaxToolkit:ValidatorCalloutExtender>

                                        </td>
                                         <td class="auto-style6"><strong>Bill to Department</strong></td>
                                        <td class="auto-style11">

                                            <%--<asp:TextBox ID="txtbilltodeprt" CssClass="texthieht" Width="250px" runat="server" Font-Bold="False" Font-Italic="False" ForeColor="Black" Height="25px" ValidationGroup="aaa"></asp:TextBox>--%>
                                            <asp:DropDownList ID="ddlbilltodeprt" Width="250px" CssClass=" form-control select2" runat="server" ValidationGroup="aaa">
                                            </asp:DropDownList>
                                          
                                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator3" ControlToValidate="ddlbilltodeprt"  Display="None" ErrorMessage="<b> Missing Field</b><br />Please Select Document." ValidationGroup="aaa" />
                                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender4" TargetControlID="RequiredFieldValidator3" runat="server" HighlightCssClass="vali" PopupPosition="Right"></ajaxToolkit:ValidatorCalloutExtender>

                                        </td>
                                        <td class="auto-style6"><strong>Mode of Payment</strong></td>
                                        <td class="auto-style11">
                                            
                                           
                                            <asp:DropDownList ID="ddlmodeofpay" Width="250px" CssClass=" form-control select2" runat="server" ValidationGroup="aaa">
                                                </asp:DropDownList>
                                            
                                             <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator12" ControlToValidate="ddlmodeofpay" Display="None" ErrorMessage="<b> Missing Field</b><br />Please input document name." ValidationGroup="aaa" />
                                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender13" TargetControlID="RequiredFieldValidator12" runat="server" HighlightCssClass="vali"></ajaxToolkit:ValidatorCalloutExtender>

                                        </td>

                                    </tr>
                                    <tr>
                                          <td class="auto-style6"><strong>Shipment Via </strong></td>
                                        <td class="auto-style11">
                                              <asp:DropDownList ID="ddlShipVia" Width="250px" CssClass=" form-control select2" runat="server" ValidationGroup="aaa">
                                                </asp:DropDownList>
                                            
<%--                                             <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator13" ControlToValidate="ddlShipVia" InitialValue="Please select..." Display="None" ErrorMessage="<b> Missing Field</b><br />Please Select Shipment Via ." ValidationGroup="aaa" />
                                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender14" TargetControlID="RequiredFieldValidator13" runat="server" HighlightCssClass="vali"></ajaxToolkit:ValidatorCalloutExtender>--%>

                                            

                                        </td>
                                         <td class="auto-style6"><strong>Supplier Ref. No.</strong></td>
                                        <td class="auto-style11">
                                            <asp:TextBox ID="txtSuppRefNo" CssClass="texthieht" Width="250px" runat="server" Font-Bold="False" Font-Italic="False" ForeColor="Black" Height="25px" ValidationGroup="aaa"></asp:TextBox>
                                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator7" ControlToValidate="txtSuppRefNo" Display="None" ErrorMessage="<b> Missing Field</b><br />Please input document name." ValidationGroup="aaa" />
                                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender5" TargetControlID="RequiredFieldValidator7" runat="server" HighlightCssClass="vali"></ajaxToolkit:ValidatorCalloutExtender>
                                        </td>
                                          <td class="auto-style6"><strong>Freight Terms</strong></td>
                                        <td class="auto-style11">
                                            
                                            <asp:DropDownList ID="ddlFreightTerms" Width="250px" CssClass=" form-control select2" runat="server" ValidationGroup="aaa">
                                                </asp:DropDownList>
                                             <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator14" ControlToValidate="ddlFreightTerms" Display="None" ErrorMessage="<b> Missing Field</b><br />Please Select Frieght Term." ValidationGroup="aaa" />
                                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender15" TargetControlID="RequiredFieldValidator14" runat="server" HighlightCssClass="vali"></ajaxToolkit:ValidatorCalloutExtender>
                                       </td>
                                    </tr>
                                    <tr>
                                       
                                        <td class="auto-style6"><strong>Delivery Due Date </strong></td>
                                        <td>
                                        <div style="display: inline-block">

                                                <div class="input-group input-group-sm">
                                                    <asp:TextBox ID="txtDeliveryDate" CssClass="form-control texthieht" Width="220px" runat="server" Font-Bold="False" Font-Italic="False" ForeColor="Black" AutoPostBack="True"  OnTextChanged="txtDeliveryDate_TextChanged"  ValidationGroup="aaa"></asp:TextBox>
                                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender3" PopupButtonID="ImageButton2" TargetControlID="txtDeliveryDate" Format="dd-MMM-yyyy" runat="server" />
                                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator6" ControlToValidate="txtDeliveryDate" Display="None" ErrorMessage="<b> Missing Field</b><br />A Date is required." ValidationGroup="aaa" />
                                                    <asp:CustomValidator ID="TextDelivery" runat="server" ControlToValidate="txtDeliveryDate" OnServerValidate="Check_CustomDateForDeliveryDate" ErrorMessage="Enter Valid Message" />
                                                    <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" TargetControlID="RequiredFieldValidator6" runat="server" HighlightCssClass="vali"></ajaxToolkit:ValidatorCalloutExtender>
                                                    <span class="input-group-btn">
                                                        
                                                        <asp:ImageButton ID="ImageButton2" CssClass="btn btn-flat" ImageUrl="img/calendar.png" ImageAlign="Bottom" runat="server" />
                                                    </span>
                                                </div>
                                            </div>
                                            </td>
                                        <td class="auto-style6"><strong>Delivery Due Days </strong></td>
                                        <td>
                                        <div style="display: inline-block">

                                                <div class="input-group input-group-sm">
                                                    <asp:TextBox ID="txtDeliDuedays" CssClass="texthieht" Width="250px" runat="server" Font-Bold="False" Font-Italic="False" ForeColor="Black" Height="25px" ValidationGroup="aaa"></asp:TextBox>
                                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator15" ControlToValidate="txtDeliDuedays" Display="None" ErrorMessage="<b> Missing Field</b><br />A Date is required." ValidationGroup="aaa" />
                                                    <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender16" TargetControlID="RequiredFieldValidator15" runat="server" HighlightCssClass="vali"></ajaxToolkit:ValidatorCalloutExtender>
                                                    
                                                </div>
                                            </div>
                                            </td>
                                        <td class="auto-style6"><strong>Force closed PO after Due Days </strong></td>
                                        <td>
                                        <div style="display: inline-block">

                                                <div class="input-group input-group-sm">
                                                    <asp:TextBox ID="txtforceclosed" CssClass="texthieht" Width="250px" runat="server" Font-Bold="False" Font-Italic="False" ForeColor="Black" Height="25px" ValidationGroup="aaa"></asp:TextBox>
                                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator16" ControlToValidate="txtforceclosed" Display="None" ErrorMessage="<b> Missing Field</b><br />A Date is required." ValidationGroup="aaa" />
                                                    <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender17" TargetControlID="RequiredFieldValidator16" runat="server" HighlightCssClass="vali"></ajaxToolkit:ValidatorCalloutExtender>
                                                    
                                                </div>
                                            </div>
                                            </td>

                                    </tr>
                                    <tr>
                                        <td class="auto-style6"><strong>Foreign Currency</strong></td>
                                        <td class="auto-style11">

                                            <asp:DropDownList ID="ddlFCurrency" Width="250px" CssClass=" form-control select2" runat="server" ValidationGroup="aaa" AutoPostBack="True" OnSelectedIndexChanged="ddlFCurrency_SelectedIndexChanged">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator5" ControlToValidate="ddlFCurrency" Display="None" ErrorMessage="<b> Missing Field</b><br />Please Select Foreign Currency." ValidationGroup="aaa" />
                                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender7" TargetControlID="RequiredFieldValidator14" runat="server" HighlightCssClass="vali"></ajaxToolkit:ValidatorCalloutExtender>


                                        </td>
                                       
                                        <td class="auto-style6"><strong>FC Rate</strong></td>
                                        <td class="auto-style11">

                                          <%-- <asp:TextBox ID="txtFCRate" CssClass="texthieht" Width="250px" runat="server" Font-Bold="False" Font-Italic="False" ForeColor="Black" Height="25px" ValidationGroup="aaa"></asp:TextBox>--%>
                                           
                                            <asp:DropDownList ID="ddlFCRate" Width="250px" CssClass=" form-control select2" runat="server" ValidationGroup="aaa"></asp:DropDownList>
                                             <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator9" ControlToValidate="ddlFCRate" Display="None" ErrorMessage="<b> Missing Field</b><br />Please select FC Rate." ValidationGroup="aaa" />
                                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender8" TargetControlID="RequiredFieldValidator9" runat="server" HighlightCssClass="vali"></ajaxToolkit:ValidatorCalloutExtender>


                                        </td>
                                        <td class="auto-style6"><strong>Total FC Value</strong></td>
                                        <td class="auto-style11">

                                           <asp:TextBox ID="txtTotalFCVal" CssClass="texthieht" Width="250px" runat="server" Font-Bold="False" Font-Italic="False" ForeColor="Black" Height="25px" ValidationGroup="aaa"></asp:TextBox>
                                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator17" ControlToValidate="txtTotalFCVal" Display="None" ErrorMessage="<b> Missing Field</b><br />Please select Payment mode." ValidationGroup="aaa" />
                                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender18" TargetControlID="RequiredFieldValidator17" runat="server" HighlightCssClass="vali"></ajaxToolkit:ValidatorCalloutExtender>


                                        </td>

                                    </tr>

                                    <tr>
                                        
                                        <td class="auto-style6"><strong>Total Local Currency Value</strong></td>
                                        <td class="auto-style11">

                                           <asp:TextBox ID="txtTotalLocalCurVal" CssClass="texthieht" Width="250px" runat="server" Font-Bold="False" Font-Italic="False" ForeColor="Black" Height="25px" ValidationGroup="aaa"></asp:TextBox>
                                            


                                        </td>
                                       
                                        <td class="auto-style6"><strong>Advance against PO %</strong></td>
                                        <td class="auto-style11">

                                           <asp:TextBox ID="txtAdvaceAgaPO" CssClass="texthieht" Width="250px" runat="server" Font-Bold="False" Font-Italic="False" ForeColor="Black" Height="25px" ValidationGroup="aaa"></asp:TextBox>
                                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator19" ControlToValidate="txtAdvaceAgaPO" Display="None" ErrorMessage="<b> Missing Field</b><br />Please select Payment mode." ValidationGroup="aaa" />
                                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender20" TargetControlID="RequiredFieldValidator19" runat="server" HighlightCssClass="vali"></ajaxToolkit:ValidatorCalloutExtender>


                                        </td>
                                       <td class="auto-style6"><strong>Status</strong></td>
                                        <td class="auto-style11">
                                            <asp:DropDownList ID="ddlStatus" Width="250px" CssClass=" form-control select2" runat="server" ValidationGroup="aaa">
                                            </asp:DropDownList>

                                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator10" ControlToValidate="ddlStatus" Display="None" ErrorMessage="<b> Missing Field</b><br />Please input document name." ValidationGroup="aaa" />
                                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender9" TargetControlID="RequiredFieldValidator10" runat="server" HighlightCssClass="vali"></ajaxToolkit:ValidatorCalloutExtender>


                                        </td>
                                        
                                        
                                    </tr>

                                    <tr>
                                          <td class="auto-style6"><strong>Created Date</strong></td>
                                        <td class="auto-style11">
                                            <asp:TextBox ID="txtCreateddate" CssClass="texthieht" Width="250px" runat="server" Font-Bold="False" Font-Italic="False" ForeColor="Black" Height="25px"></asp:TextBox>
                                        </td>
                                         <td class="auto-style6"><strong>Updated Date</strong></td>
                                        <td class="auto-style11">
                                            <asp:TextBox ID="txtUpdateDate" CssClass="texthieht" Width="250px" runat="server" Font-Bold="False" Font-Italic="False" ForeColor="Black" Height="25px"></asp:TextBox>

                                        </td>
                                        <td class="auto-style6"><strong>Active</strong></td>
                                        <td class="auto-style11">
                                            <asp:Panel ID="Panel1" runat="server">
                                                <asp:CheckBox ID="chkActive" runat="server" Text="Active" TabIndex="3" />
                                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                            </asp:Panel>


                                        </td>
                                    </tr>

                                   <tr>
                                         <td class="auto-style6"><strong>Remarks</strong></td>
                                        <td class="auto-style11">

                                           <asp:TextBox ID="txtRemarks" CssClass="texthieht" Width="250px" runat="server" Font-Bold="False" Font-Italic="False" ForeColor="Black" Height="25px" ValidationGroup="aaa"></asp:TextBox>


                                        </td>
                                   </tr>

                                </table>


                            </div>

                            <%-- Table View --%>
                            <div id="Findbox" runat="server">
                                <div class="box">

                                    <!-- /.box-header -->
                                    <div class="box-body" style="width: 100%; overflow: auto; height: 50%;">
                                        <%--CssClass="table table-bordered table-hover"--%>

                                        <div style="width: 100%; height: 400px; overflow: scroll">

                                            <asp:GridView ID="GridView1" runat="server" CssClass="table table-bordered table-hover tableFixHead" AutoGenerateColumns="False" OnSelectedIndexChanged="GridView1_SelectedIndexChanged" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Horizontal" DataKeyNames="Sno">
                                                <Columns>
                                                    <asp:CommandField ButtonType="Image" SelectImageUrl="~/dist/img/edit.png" ShowSelectButton="True">
                                                        <ItemStyle Width="20px" />
                                                    </asp:CommandField>
                                                    <asp:BoundField DataField="Sno" HeaderText="Sno" SortExpression="Sno">
                                                    </asp:BoundField>
                                                     <asp:BoundField DataField="DocDescription" HeaderText="Document" SortExpression="DocDescription">
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="DocNo" HeaderText="Doc No" SortExpression="DocNo">
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="DepartmentName" HeaderText="Department" SortExpression="DepartmentName">
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="StatusName" HeaderText="Status" SortExpression="StatusName" />
                                                    <asp:BoundField DataField="EntryDate" HeaderText="Entry Date" SortExpression="EntryDate" DataFormatString = "{0:dd-MMM-yyyy}"/>
                                                    <asp:BoundField DataField="DeliveryDueDate" HeaderText="Delivery Due Date" SortExpression="DeliveryDueDate" DataFormatString = "{0:dd-MMM-yyyy}"/>
                                                    <asp:CheckBoxField DataField="IsActive" HeaderText="IsActive" SortExpression="IsActive">
                                                    </asp:CheckBoxField>
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
                                    <div class="box-body" style="width: 100%; overflow: auto; height: 50%;">
                                        <div style="width: 100%; height: 400px; overflow: scroll">

                                            <asp:GridView ID="GridView2" runat="server" CssClass="table table-bordered table-hover tableFixHead" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Horizontal" OnSelectedIndexChanged="GridView2_SelectedIndexChanged" OnRowDataBound="GridView2_RowDataBound">
                                                <Columns>
                                                    <asp:CommandField ButtonType="Image" SelectImageUrl="~/dist/img/edit.png" ShowSelectButton="True">
                                                        <ItemStyle Width="20px" />
                                                    </asp:CommandField>
                                                    <asp:BoundField DataField="Sno" HeaderText="Sno" SortExpression="Sno">
                                                    </asp:BoundField>
                                                     <asp:BoundField DataField="DocDescription" HeaderText="Document" SortExpression="DocDescription">
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="DocNo" HeaderText="Doc No" SortExpression="DocNo">
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="DepartmentName" HeaderText="Department" SortExpression="DepartmentName">
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="StatusName" HeaderText="Status" SortExpression="StatusName" />
                                                    <asp:BoundField DataField="EntryDate" HeaderText="Entry Date" SortExpression="EntryDate" DataFormatString = "{0:dd-MMM-yyyy}"/>
                                                    <asp:BoundField DataField="DeliveryDueDate" HeaderText="Delivery Due Date" SortExpression="DeliveryDueDate" DataFormatString = "{0:dd-MMM-yyyy}"/>
                                                    <asp:CheckBoxField DataField="IsActive" HeaderText="IsActive" SortExpression="IsActive">
                                                    </asp:CheckBoxField>
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
                                    <div class="box-body" style="width: 100%; overflow: auto; height: 50%;">
                                        <div style="width: 100%; height: 400px; overflow: scroll">
                                            <asp:GridView ID="GridView3" runat="server" CssClass="table table-bordered table-hover tableFixHead" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Horizontal" OnSelectedIndexChanged="GridView3_SelectedIndexChanged" OnRowDataBound="GridView3_RowDataBound">
                                              <Columns>
                                                    <asp:CommandField ButtonType="Image" SelectImageUrl="~/dist/img/edit.png" ShowSelectButton="True">
                                                        <ItemStyle Width="20px" />
                                                    </asp:CommandField>
                                                    <asp:BoundField DataField="Sno" HeaderText="Sno" SortExpression="Sno">
                                                    </asp:BoundField>
                                                     <asp:BoundField DataField="DocDescription" HeaderText="Document" SortExpression="DocDescription">
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="DocNo" HeaderText="Doc No" SortExpression="DocNo">
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="DepartmentName" HeaderText="Department" SortExpression="DepartmentName">
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="StatusName" HeaderText="Status" SortExpression="StatusName" />
                                                    <asp:BoundField DataField="EntryDate" HeaderText="Entry Date" SortExpression="EntryDate" DataFormatString = "{0:dd-MMM-yyyy}"/>
                                                    <asp:BoundField DataField="DeliveryDueDate" HeaderText="Delivery Due Date" SortExpression="DeliveryDueDate" DataFormatString = "{0:dd-MMM-yyyy}"/>
                                                    <asp:CheckBoxField DataField="IsActive" HeaderText="IsActive" SortExpression="IsActive">
                                                    </asp:CheckBoxField>
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

                            <asp:Label ID="Label1" runat="server" Text="" ForeColor="Red"></asp:Label>


                        </div>
                        <!-- /.box-footer -->
                    </div>
                </div>
                <!-- /.box -->
              


                   <div id="Div1" runat="server">
                                <div class="box">

                                    <!-- /.box-header -->
                                    
                                    <div class="box-body" style="width: 100%; overflow: auto; height: 50%;">
                                       <%-- CssClass="table table-bordered table-hover"--%>
                                      
                                        <div style="width: 100%; height: 400px; overflow: scroll">
                                                                                         
                                 <asp:GridView ID="GridView5" AutoGenerateColumns="False" CssClass="table table-bordered table-hover tableFixHead" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Horizontal" runat="server" DataKeyNames="Sno" OnRowDataBound="GridView5_RowDataBound" >
                                                <Columns>
                                                    <%--<asp:CommandField ShowEditButton="True" ButtonType="Image" EditImageUrl="~/dist/img/edit.png" CancelImageUrl="~/dist/img/Cancel.png" UpdateImageUrl="~/dist/img/Update.png" />--%>

                                                    <asp:TemplateField HeaderText="Sno" ItemStyle-Width="20">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblRowNumber" runat="server" />
                                                        </ItemTemplate>
                                                        <ItemStyle Width="100px"></ItemStyle>
                                                    </asp:TemplateField>



                                                    <asp:TemplateField HeaderText="" SortExpression="Sno">
                                                        <EditItemTemplate>
                                                            <asp:Label ID="lblSno" runat="server" Visible="false" Text='<%# Eval("Sno") %>'></asp:Label>
                                                             <asp:Label ID="lbldetailSno" runat="server" Text='<%# Bind("SnoDetail") %>' Visible="false"></asp:Label>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSno" runat="server" Visible="false" Text='<%# Bind("Sno") %>'></asp:Label>
                                                            <asp:Label ID="lbldetailSno" runat="server" Text='<%# Bind("SnoDetail") %>' Visible="false"></asp:Label>
                                                        </ItemTemplate>
                                                       
                                                    </asp:TemplateField>




                                                    <asp:TemplateField HeaderText="Requisition No" SortExpression="reqNOdetail">
                                                        <EditItemTemplate>
                                                            <asp:Label ID="lblreq" runat="server" Text='<%# Bind("RequisitionNo") %>'></asp:Label>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblreq" runat="server" Text='<%# Bind("RequisitionNo") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>



                                                    <asp:TemplateField HeaderText="ItemSubHeadName" SortExpression="ItemSubHeadName">
                                                        <EditItemTemplate>
                                                            <asp:Label ID="lblsubhead" runat="server" Text='<%# Bind("ItemSubHeadName") %>'></asp:Label>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblsubhead" runat="server" Text='<%# Bind("ItemSubHeadName") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>



                                                    <asp:TemplateField HeaderText="ItemMaster" SortExpression="Description">
                                                        <EditItemTemplate>
                                                            <asp:Label ID="lblitemmaster" runat="server" Text='<%# Bind("Description") %>'></asp:Label>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblitemmaster" runat="server" Text='<%# Bind("Description") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>



                                                    <asp:TemplateField HeaderText="UOM" SortExpression="UOMName">
                                                        <EditItemTemplate>
                                                            <asp:Label ID="lbluom" runat="server" Text='<%# Bind("UOMName") %>'></asp:Label>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbluom" runat="server" Text='<%# Bind("UOMName") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>



                                                    <asp:TemplateField HeaderText="Qty" SortExpression="Qty">
                                                        <EditItemTemplate>
                                                            <asp:TextBox ID="txtQty" runat="server" Text='<%# Bind("Quantity") %>'></asp:TextBox>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblQty" runat="server" Text='<%# Bind("Quantity") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>




                                                     <asp:TemplateField HeaderText="Approved Qty" SortExpression="ApprovedQty">
                                                        <EditItemTemplate>
                                                            <asp:TextBox ID="txtApprovedQty" runat="server" Text='<%# Bind("ApprovedQuantity") %>'></asp:TextBox>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtApprovedQty" runat="server" Width="50px" Text='<%# Bind("ApprovedQuantity") %>'></asp:TextBox>
                                                            <%--<asp:Label ID="lblApprovedQty" runat="server" Text='<%# Bind("BalanceQty") %>'></asp:Label>--%>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>




                                                    <asp:TemplateField HeaderText="Rate" SortExpression="Quantity">
                                                        <EditItemTemplate>
                                                            <asp:TextBox ID="txtRate" runat="server"></asp:TextBox>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtRate" runat="server" Width="50px" Text='<%# Bind("Rate") %>' AutoPostBack="true"  OnTextChanged="txtRate_TextChanged"></asp:TextBox>
                                                           <%-- <asp:Label ID="lblRate" runat="server" ></asp:Label>--%>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>



                                                     <asp:TemplateField HeaderText="GST %" SortExpression="RequiredQuantity">
                                                        <EditItemTemplate>
                                                            <asp:TextBox ID="txtGST" runat="server"></asp:TextBox>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtGST" runat="server" Width="50px" AutoPostBack="True" Text='<%# Bind("GST") %>'  OnTextChanged="txtGST_TextChanged"></asp:TextBox>
                                                            <asp:Label ID="lblGST" runat="server" ></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>




                                                     <asp:TemplateField HeaderText="GST Rate" SortExpression="RequiredQuantity">
                                                        <EditItemTemplate>
                                                            <asp:TextBox ID="txtGSTRate" runat="server" ></asp:TextBox>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblGSTRate" runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>




                                                     <asp:TemplateField HeaderText="Gross Value" SortExpression="RequiredQuantity">
                                                        <EditItemTemplate>
                                                            <asp:TextBox ID="txtGrossValue" runat="server" Text='<%# Bind("RequiredQuantity") %>'></asp:TextBox>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblQtyVal" runat="server" Visible="false"></asp:Label>
                                                            <asp:Label ID="lblGstValue" runat="server" Visible="false"></asp:Label>
                                                            <asp:Label ID="lblGrossValue" runat="server" ></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>




                                                     <asp:TemplateField HeaderText="Cost Center" SortExpression="RequiredQuantity">
                                                        <EditItemTemplate>
                                                            <asp:TextBox ID="txtCostCenter" runat="server"></asp:TextBox>
                                                        </EditItemTemplate>
                                                        <ItemTemplate> 
                                                            <asp:DropDownList ID="ddlCostCenter" CssClass="form-control select2" Width="150px" runat="server"></asp:DropDownList>
                                                            <%--<asp:Label ID="lblCostCenter" runat="server"></asp:Label>--%>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>




                                                     <asp:TemplateField HeaderText="Location" SortExpression="RequiredQuantity">
                                                        <EditItemTemplate>
                                                            <asp:TextBox ID="txtLocation" runat="server" Text='<%# Bind("RequiredQuantity") %>'></asp:TextBox>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                             <asp:DropDownList ID="ddlLocation" CssClass="form-control select2" Width="150px" runat="server"></asp:DropDownList>
                                                            <%--<asp:Label ID="lblLocation" runat="server" Text='<%# Bind("RequiredQuantity") %>'></asp:Label>--%>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>



                                                    
                                                     <asp:TemplateField HeaderText="project" SortExpression="RequiredQuantity">
                                                        <EditItemTemplate>
                                                            <asp:TextBox ID="txtproject" runat="server" ></asp:TextBox>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtproject" Width="100px" runat="server"></asp:TextBox>
                                                             <%--<asp:Label ID="lblproject" runat="server" Text='<%# Bind("RequiredQuantity") %>'></asp:Label>--%>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>



                                                    <asp:TemplateField HeaderText="UOM2" SortExpression="Project">
                                                        <EditItemTemplate>
                                                            <asp:TextBox ID="txtProjectEdit" runat="server" Text=""></asp:TextBox>
                                                            <asp:Label ID="lblProjectEdit" runat="server" Enabled="false" Text=""></asp:Label>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:DropDownList ID="ddlUOM2" CssClass="form-control select2" Width="70px" runat="server"></asp:DropDownList>
                                                        </ItemTemplate>

                                                    </asp:TemplateField>



                                                    <asp:TemplateField HeaderText="Qty2" SortExpression="Project">
                                                        <EditItemTemplate>
                                                            <asp:TextBox ID="txtProjectEdit" runat="server" Text=""></asp:TextBox>
                                                            <asp:Label ID="lblProjectEdit" runat="server" Enabled="false" Text=""></asp:Label>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="lblQty2A" runat="server" Width="50px" Text="" AutoPostBack="True" OnTextChanged="lblQty2A_TextChanged"></asp:TextBox>
                                                        </ItemTemplate>

                                                    </asp:TemplateField>



                                                    <asp:TemplateField HeaderText="Rate2" SortExpression="Project">
                                                        <EditItemTemplate>
                                                            <asp:TextBox ID="txtProjectEdit" runat="server" Text=""></asp:TextBox>
                                                            <asp:Label ID="lblProjectEdit" runat="server" Enabled="false" Text=""></asp:Label>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblRate2" runat="server" Text=""></asp:Label>
                                                        </ItemTemplate>

                                                    </asp:TemplateField>



                                                    <asp:TemplateField HeaderText="">
                                                         <HeaderTemplate>
                                                              <asp:CheckBox ID="chkall" runat="server"  Text="All" Width="83px" />
                                                         </HeaderTemplate>
                                                        <EditItemTemplate>
                                                            <asp:CheckBox ID="chkSelect" runat="server" />
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="chkSelect" runat="server"/>
                                                        </ItemTemplate>
                                                        <HeaderStyle Width="40px" />
                                                          <ItemStyle Height="10px" />
                                                    </asp:TemplateField>





                                                    <%-- listView coloum --%>
                                                </Columns>
                                                <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
                                                <HeaderStyle BackColor="#000080" Font-Bold="True" ForeColor="White" BorderStyle="Solid" BorderWidth="1px" Wrap="True" Height="5px" />
                                                <PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="Right" />
                                            </asp:GridView>



                                            <%--<asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:db_HRERPSys %>" SelectCommand="SELECT [Sno],[RequistionNo], [ItemSubHeadName], [Description], [UOM], [RequiredQuantity] FROM [VIEW_ASKFORQOUT]"></asp:SqlDataSource>--%>

                                           <%-- <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:db_HRERPSys %>" SelectCommand="SELECT [DocID], [DocDescription], [DocName], [IsActive], [IsHide] , [Main_Site], [ApplyDate] FROM [viewDOCUSER] order by DocDescription asc"></asp:SqlDataSource>--%>

                                        </div>

                                    </div>
                                    <!-- /.box-body -->
                                </div>
                                
                            </div>
                
                    


                 <div id="Div2" runat="server">
                    <div class="box">

                        <!-- /.box-header -->

                        <div class="box-body" style="width: 100%; overflow: auto; height: 50%;">
                            <%-- CssClass="table table-bordered table-hover"--%>

                            <div style="width: 100%; height: 400px; overflow: scroll">

                                <asp:GridView ID="GridView6" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-hover tableFixHead" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Horizontal" ShowFooter="true" ShowHeaderWhenEmpty="true" OnRowCommand="GridView6_RowCommand" OnRowDataBound="GridView6_RowDataBound" OnRowDeleting="GridView6_RowDeleting" >

                                    <Columns>


                                        <asp:TemplateField HeaderText="Sno" ItemStyle-Width="50">
                                            <ItemTemplate>
                                                <asp:Label ID="lblRowNumber" runat="server" />
                                            </ItemTemplate>

                                            <ItemStyle Width="100px"></ItemStyle>
                                        </asp:TemplateField>




                                        <asp:TemplateField HeaderText="Item Sub Head Name" SortExpression="ItemSubHeadName">
                                            <EditItemTemplate>
                                                <asp:DropDownList ID="ddlitemsubEdit" Width="150px" CssClass=" form-control select2" runat="server" ValidationGroup="aaa"></asp:DropDownList>
                                                <asp:Label ID="Label2" runat="server" Enabled="false" Text='<%# Bind("ItemSubHeadName") %>'></asp:Label>
                                                <%--<asp:TextBox ID="ddlitemsubEdit" runat="server" Text='<%# Bind("ItemSubHeadName") %>'></asp:TextBox>--%>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblItemSubHeadName" runat="server" Text='<%# Bind("ItemSubHeadName") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:DropDownList ID="ddlitemsub" Width="150px" CssClass=" form-control select2" runat="server" ValidationGroup="aaa" AutoPostBack="True" OnSelectedIndexChanged="ddlitemsub_SelectedIndexChanged" ></asp:DropDownList>
                                            </FooterTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Item Master" SortExpression="Description">

                                            <ItemTemplate>
                                                <asp:Label ID="lblDescription" runat="server" Text='<%# Bind("Description") %>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>

                                                <asp:DropDownList ID="ddlItemMasterEdit" Width="150px" CssClass=" form-control select2" runat="server" ValidationGroup="aaa"></asp:DropDownList>
                                                <asp:Label ID="lblitemmaster" runat="server" Enabled="false" Text='<%# Bind("Description") %>'></asp:Label>

                                            </EditItemTemplate>
                                            <FooterTemplate>
                                                <asp:DropDownList ID="ddlitemMaster" Width="150px" CssClass=" form-control select2" runat="server" ValidationGroup="aaa" AutoPostBack="True" OnSelectedIndexChanged="ddlitemMaster_SelectedIndexChanged"></asp:DropDownList>
                                            </FooterTemplate>
                                        </asp:TemplateField>




                                        <asp:TemplateField HeaderText="UOM" SortExpression="UOM">
                                            <EditItemTemplate>
                                                <asp:DropDownList ID="ddlUomEdit" Width="150px" CssClass=" form-control select2" runat="server" ValidationGroup="aaa"></asp:DropDownList>
                                                <asp:Label ID="lblUOMEdit" runat="server" Enabled="false" Text='<%# Bind("UOM") %>'></asp:Label>

                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblUom" runat="server" Text='<%# Bind("UOM") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:DropDownList ID="ddlUomFooter" Width="150px" CssClass=" form-control select2" runat="server" ValidationGroup="aaa"></asp:DropDownList>
                                            </FooterTemplate>
                                        </asp:TemplateField>




                                        <asp:TemplateField HeaderText="Qty" SortExpression="CurrentStock">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="TextBox4" runat="server" Width="100px" Text='<%# Bind("Qty") %>'></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblQty" runat="server" Width="100px" Text='<%# Bind("Qty") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:TextBox ID="txtQtyFooter" Width="100px" runat="server"></asp:TextBox>
                                            </FooterTemplate>
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Approved Qty" SortExpression="RequiredQuantity">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="TextBox5" runat="server" Width="100px" Text='<%# Bind("ApprovedQty") %>'></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblApprovedQty" runat="server" Width="100px" Text='<%# Bind("ApprovedQty") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:TextBox ID="txtApprovedQtyFooter" Width="100px" runat="server"></asp:TextBox>
                                            </FooterTemplate>
                                        </asp:TemplateField>




                                        <asp:TemplateField HeaderText="Rate" SortExpression="CostCenter">
                                            <EditItemTemplate>
                                                <asp:DropDownList ID="ddlCostCenterEdit" Width="150px" CssClass=" form-control select2" runat="server" ValidationGroup="aaa"></asp:DropDownList>
                                                <asp:Label ID="lblcostCenterEdit" runat="server" Enabled="false" Text='<%# Bind("Rate") %>'></asp:Label>
                                             </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblRate" runat="server" Width="150px" Text='<%# Bind("Rate") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:TextBox ID="txtRateFooter" Width="100px" runat="server"></asp:TextBox>
                                            </FooterTemplate>
                                        </asp:TemplateField>




                                        <asp:TemplateField HeaderText="GST%" SortExpression="Project">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtGSTEdit" runat="server" Text='<%# Bind("GST") %>'></asp:TextBox>
                                                <asp:Label ID="lblProjectEdit" runat="server" Enabled="false" Text='<%# Bind("Project") %>'></asp:Label>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblGST" runat="server" Text='<%# Bind("GST") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:TextBox ID="lblGSTFooter" Width="100px" runat="server" AutoPostBack="True" OnTextChanged="lblGSTFooter_TextChanged"></asp:TextBox>
                                            </FooterTemplate>
                                        </asp:TemplateField>
                                        



                                         <asp:TemplateField HeaderText="GST Rate" SortExpression="Project">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtProjectEdit" runat="server" Text='<%# Bind("GSTRate") %>'></asp:TextBox>
                                                <asp:Label ID="lblProjectEdit" runat="server" Enabled="false" Text='<%# Bind("GSTRate") %>'></asp:Label>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblGSTRate" runat="server" Text='<%# Bind("GSTRate") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:TextBox ID="lblGSTRateFooter" Enabled="false" Width="100px" runat="server"></asp:TextBox>
                                            </FooterTemplate>
                                        </asp:TemplateField>




                                        <asp:TemplateField HeaderText="Gross Value" SortExpression="Project">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtProjectEdit" runat="server" Text='<%# Bind("GrossValue") %>'></asp:TextBox>
                                                <asp:Label ID="lblProjectEdit" runat="server" Enabled="false" Text='<%# Bind("Project") %>'></asp:Label>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblGrossValue" runat="server" Text='<%# Bind("GrossValue") %>'></asp:Label>
                                                 <asp:Label ID="lblFGV" Visible="false" runat="server" Text='<%# Bind("GrossValue1") %>'></asp:Label>
                                                 
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:TextBox ID="lblGrossValueFooter" Enabled="false" Width="100px" runat="server"></asp:TextBox>
                                                <asp:TextBox ID="lblfloatgV" Visible="false"  runat="server" Width="100px"></asp:TextBox>
                                            </FooterTemplate>
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Cost Center" SortExpression="Project">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtProjectEdit" runat="server" Text='<%# Bind("CostCenter") %>'></asp:TextBox>
                                                <asp:Label ID="lblProjectEdit" runat="server" Enabled="false" Text='<%# Bind("CostCenter") %>'></asp:Label>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblCostCenter" runat="server" Text='<%# Bind("CostCenter") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:DropDownList ID="lblCostCenterFooter" Width="150px" CssClass=" form-control select2" runat="server" ValidationGroup="aaa"></asp:DropDownList>
                                            </FooterTemplate>
                                        </asp:TemplateField>




                                        <asp:TemplateField HeaderText="Location" SortExpression="Project">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtProjectEdit" runat="server" Text='<%# Bind("Location") %>'></asp:TextBox>
                                                <asp:Label ID="lblProjectEdit" runat="server" Enabled="false" Text='<%# Bind("Location") %>'></asp:Label>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblLocation" runat="server" Text='<%# Bind("Location") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:DropDownList ID="ddlLocationFooter" Width="150px" CssClass=" form-control select2" runat="server" ValidationGroup="aaa"></asp:DropDownList>
                                                <%--<asp:TextBox ID="lblLocationFooter" Width="100px" runat="server"></asp:TextBox>--%>
                                            </FooterTemplate>
                                        </asp:TemplateField>



                                         <asp:TemplateField HeaderText="Project" SortExpression="Project">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtProjectEdit" runat="server" Text='<%# Bind("Project") %>'></asp:TextBox>
                                                <asp:Label ID="lblProjectEdit" runat="server" Enabled="false" Text='<%# Bind("Project") %>'></asp:Label>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblProject" runat="server" Text='<%# Bind("Project") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:TextBox ID="lblProjectFooter" Width="100px" runat="server"></asp:TextBox>
                                            </FooterTemplate>
                                        </asp:TemplateField>




                                        <asp:TemplateField HeaderText="UOM2" SortExpression="Project">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtProjectEdit" runat="server" Text='<%# Bind("UOM2") %>'></asp:TextBox>
                                                <asp:Label ID="lblProjectEdit" runat="server" Enabled="false" Text='<%# Bind("UOM2") %>'></asp:Label>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblUOM2" runat="server" Text='<%# Bind("UOM2") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:DropDownList ID="ddlUOM2Footer" Width="150px" CssClass=" form-control select2" runat="server" ValidationGroup="aaa"></asp:DropDownList>
                                            </FooterTemplate>
                                        </asp:TemplateField>



                                        <asp:TemplateField HeaderText="Qty2" SortExpression="Project">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtProjectEdit" runat="server" Text='<%# Bind("Qty2") %>'></asp:TextBox>
                                                <asp:Label ID="lblProjectEdit" runat="server" Enabled="false" Text='<%# Bind("Qty2") %>'></asp:Label>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblQty2" runat="server" Text='<%# Bind("Qty2") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:TextBox ID="lblQty2Footer" Width="100px" runat="server" AutoPostBack="True" OnTextChanged="lblQty2Footer_TextChanged"></asp:TextBox>
                                            </FooterTemplate>
                                        </asp:TemplateField>



                                         <asp:TemplateField HeaderText="Rate2" SortExpression="Project">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtProjectEdit" runat="server" Text='<%# Bind("Rate2") %>'></asp:TextBox>
                                                <asp:Label ID="lblProjectEdit" runat="server" Enabled="false" Text='<%# Bind("Rate2") %>'></asp:Label>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblRate2" runat="server" Text='<%# Bind("Rate2") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:TextBox ID="lblRate2Footer" Width="100px" Enabled="false" runat="server"></asp:TextBox>
                                            </FooterTemplate>
                                        </asp:TemplateField>



                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:ImageButton ImageUrl="~/Images123/edit.png" runat="server" CommandName="Edit" ToolTip="Edit" Width="20px" Height="20px" />
                                                <asp:ImageButton ImageUrl="~/Images123/delete.png" runat="server" CommandName="Delete" ToolTip="Delete" Width="20px" Height="20px" />
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:ImageButton ImageUrl="~/Images123/save.png" runat="server" CommandName="Update" ToolTip="Update" Width="20px" Height="20px" />
                                                <asp:ImageButton ImageUrl="~/Images123/cancel.png" runat="server" CommandName="Cancel" ToolTip="Cancel" Width="20px" Height="20px" />
                                            </EditItemTemplate>
                                            <FooterTemplate>
                                                <asp:ImageButton ImageUrl="~/Images123/addnew.png" runat="server" CommandName="AddNew" ToolTip="Add New" Width="20px" Height="20px" />
                                            </FooterTemplate>
                                        </asp:TemplateField>



                                    </Columns>

                                    <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
                                    <HeaderStyle BackColor="#000080" Font-Bold="True" ForeColor="White" />
                                    <PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="Right" />
                                </asp:GridView>
                                 
                                 
                                    
                                <%--  <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:db_HRERPSys %>" SelectCommand="SELECT [Sno], [ItemSubHeadName], [Description], [UOM], [CurrentStock], [RequiredQuantity], [Supplier] FROM [View_StockReq]"></asp:SqlDataSource>--%>
                            </div>
                        </div>
                    </div>
                </div>





                <%-- Find Detial --%>
                    <div id="Div3" runat="server">
                                <div class="box">

                                    <div class="box-body" style="width: 100%; overflow: auto; height: 50%;">
                                       <%-- CssClass="table table-bordered table-hover"--%>
                                      
                                        <div style="width: 100%; height: 400px; overflow: scroll">

                                            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
                                                <Triggers>
                                                    <asp:PostBackTrigger ControlID="GridView4" />
                                                </Triggers>
                                                <ContentTemplate>

                                                    <asp:GridView ID="GridView4" AutoGenerateColumns="False" CssClass="table table-bordered table-hover tableFixHead" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Horizontal" runat="server" DataKeyNames="Sno" OnRowDataBound="GridView4_RowDataBound" OnRowCancelingEdit="GridView4_RowCancelingEdit" OnRowEditing="GridView4_RowEditing" OnRowUpdating="GridView4_RowUpdating">
                                                        <Columns>
                                                            <%--<asp:CommandField ShowEditButton="True" ButtonType="Image" EditImageUrl="~/dist/img/edit.png" CancelImageUrl="~/dist/img/Cancel.png" UpdateImageUrl="~/dist/img/Update.png" />--%>

                                                            <asp:TemplateField HeaderText="Sno" ItemStyle-Width="20">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblRowNumber" runat="server" />
                                                                </ItemTemplate>
                                                                <ItemStyle Width="100px"></ItemStyle>
                                                            </asp:TemplateField>



                                                            <asp:TemplateField HeaderText="" SortExpression="Sno" Visible="false">
                                                                <EditItemTemplate>
                                                                    <asp:Label ID="lblSno" runat="server" Visible="false" Text='<%# Eval("Sno_Master") %>'></asp:Label>
                                                                    <asp:Label ID="lbldetailSno" runat="server" Text='<%# Bind("Sno") %>' Visible="false"></asp:Label>
                                                                </EditItemTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblSno" runat="server" Visible="false" Text='<%# Bind("Sno_Master") %>'></asp:Label>
                                                                    <asp:Label ID="lbldetailSno" runat="server" Text='<%# Bind("Sno") %>' Visible="false"></asp:Label>
                                                                </ItemTemplate>

                                                            </asp:TemplateField>




                                                            <asp:TemplateField HeaderText="Requisition No" SortExpression="reqNOdetail">
                                                                <EditItemTemplate>
                                                                    <asp:Label ID="lblreq" runat="server" Text='<%# Bind("PRNo_AQno") %>'></asp:Label>
                                                                </EditItemTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblreq" runat="server" Text='<%# Bind("PRNo_AQno") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>



                                                            <asp:TemplateField HeaderText="ItemSubHeadName" SortExpression="ItemSubHeadName">
                                                                <EditItemTemplate>
                                                                    <asp:DropDownList ID="ddlitemsubEDIT" Width="150px" CssClass=" form-control select2" runat="server" ValidationGroup="aaa"></asp:DropDownList>
                                                                    <asp:Label ID="lblsubhead" runat="server" Text='<%# Bind("ItemSubHeadName") %>'></asp:Label>
                                                                </EditItemTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblsubhead" runat="server" Text='<%# Bind("ItemSubHeadName") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>



                                                            <asp:TemplateField HeaderText="ItemMaster" SortExpression="Description">
                                                                <EditItemTemplate>

                                                                    <asp:DropDownList ID="ddlItemmter" Width="70px" CssClass=" form-control select2" runat="server" ValidationGroup="aaa"></asp:DropDownList>
                                                                    <asp:Label ID="lblitemmaster" runat="server" Text='<%# Bind("Description") %>'></asp:Label>
                                                                </EditItemTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblitemmaster" runat="server" Text='<%# Bind("Description") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>



                                                            <asp:TemplateField HeaderText="UOM" SortExpression="UOMName">
                                                                <EditItemTemplate>
                                                                    <asp:DropDownList ID="ddlUOMs" Width="150px" CssClass=" form-control select2" runat="server" ValidationGroup="aaa"></asp:DropDownList>
                                                                    <asp:Label ID="lbluom" runat="server" Width="50px" Text='<%# Bind("UOMName") %>'></asp:Label>
                                                                </EditItemTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbluom" runat="server" Text='<%# Bind("UOMName") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>



                                                            <asp:TemplateField HeaderText="Qty" SortExpression="Qty">
                                                                <EditItemTemplate>
                                                                    <asp:TextBox ID="txtQty" runat="server" Width="50px" Text='<%# Bind("Quantity") %>'></asp:TextBox>
                                                                </EditItemTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblQty" runat="server" Text='<%# Bind("Quantity") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>




                                                            <asp:TemplateField HeaderText="Approved Qty" SortExpression="ApprovedQuantity">
                                                                <EditItemTemplate>
                                                                    <asp:TextBox ID="txtApprovedQty" runat="server" Width="50px" Text='<%# Bind("ApprovedQuantity") %>'></asp:TextBox>
                                                                </EditItemTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="txtApprovedQty" runat="server" Width="50px" Text='<%# Bind("ApprovedQuantity") %>'></asp:Label>
                                                                    <%--<asp:Label ID="lblApprovedQty" runat="server" Text='<%# Bind("BalanceQty") %>'></asp:Label>--%>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>




                                                            <asp:TemplateField HeaderText="Rate" SortExpression="Rate">
                                                                <EditItemTemplate>
                                                                    <asp:TextBox ID="txtRate" Width="50px" Text='<%# Bind("Rate") %>' runat="server"></asp:TextBox>
                                                                </EditItemTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="txtRate" runat="server" Width="50px" Text='<%# Bind("Rate") %>' OnTextChanged="txtRate_TextChanged"></asp:Label>
                                                                    <%-- <asp:Label ID="lblRate" runat="server" ></asp:Label>--%>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>



                                                            <asp:TemplateField HeaderText="GST %" SortExpression="GST">
                                                                <EditItemTemplate>
                                                                    <asp:TextBox ID="txtGST" Width="50px" Text='<%# Bind("GST") %>' runat="server"></asp:TextBox>
                                                                </EditItemTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label" runat="server" Width="50px" AutoPostBack="True" Text='<%# Bind("GST") %>' OnTextChanged="txtGST_TextChanged"></asp:Label>
                                                                    <asp:Label ID="lblGST" runat="server"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>




                                                            <asp:TemplateField HeaderText="GST Rate" SortExpression="GSTRate">
                                                                <EditItemTemplate>
                                                                    <asp:TextBox ID="txtGSTRate" Width="50px" runat="server" Text='<%# Bind("GSTRate") %>'></asp:TextBox>
                                                                </EditItemTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblGSTRate" Text='<%# Bind("GSTRate") %>' runat="server"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>




                                                            <asp:TemplateField HeaderText="Gross Value" SortExpression="GrossValue">
                                                                <EditItemTemplate>
                                                                    <asp:TextBox ID="txtGrossValue" Width="50px" runat="server" Text='<%# Bind("GrossValue") %>'></asp:TextBox>
                                                                </EditItemTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblQtyVal" runat="server" Visible="false"></asp:Label>
                                                                    <asp:Label ID="lblGstValue" runat="server" Visible="false"></asp:Label>
                                                                    <asp:Label ID="lblGrossValue" Text='<%# Bind("GrossValue") %>' runat="server"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>




                                                            <asp:TemplateField HeaderText="Cost Center" SortExpression="CostCenterName">
                                                                <EditItemTemplate>
                                                                    <asp:TextBox ID="txtCostCenter" runat="server" Width="70px" Text='<%# Bind("CostCenterName") %>'></asp:TextBox>
                                                                </EditItemTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblCostCenter" runat="server" Width="50px" Text='<%# Bind("CostCenterName") %>'></asp:Label>
                                                                    <%--<asp:Label ID="lblCostCenter" runat="server"></asp:Label>--%>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>




                                                            <asp:TemplateField HeaderText="Location" SortExpression="CostCenterName">
                                                                <EditItemTemplate>
                                                                    <asp:DropDownList ID="ddlLocation" Width="150px" CssClass=" form-control select2" runat="server" ValidationGroup="aaa"></asp:DropDownList>
                                                                    <asp:TextBox ID="txtLocation" runat="server" Width="70px" Text='<%# Bind("LocName") %>'></asp:TextBox>
                                                                </EditItemTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbllocation" runat="server" Width="50px" Text='<%# Bind("LocName") %>'></asp:Label>
                                                                    <%--<asp:Label ID="lblLocation" runat="server" Text='<%# Bind("RequiredQuantity") %>'></asp:Label>--%>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>




                                                            <asp:TemplateField HeaderText="project" SortExpression="Project">
                                                                <EditItemTemplate>
                                                                    <asp:TextBox ID="txtproject" runat="server" Width="70px" Text='<%# Bind("Project")%>'></asp:TextBox>
                                                                </EditItemTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="txtproject" Width="100px" Text='<%# Bind("Project") %>' runat="server"></asp:Label>
                                                                    <%--<asp:Label ID="lblproject" runat="server" Text='<%# Bind("RequiredQuantity") %>'></asp:Label>--%>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>



                                                            <asp:TemplateField HeaderText="UOM2" SortExpression="uom2name">
                                                                <EditItemTemplate>
                                                                    <asp:TextBox ID="txtUOM2Edit" runat="server" Width="70px" Text='<%# Bind("uom2name")%>'></asp:TextBox>
                                                                    <asp:Label ID="lblUOM2Edit" runat="server" Enabled="false" Text=""></asp:Label>
                                                                </EditItemTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblUOM2" runat="server" Text='<%# Bind("uom2name") %>'></asp:Label>
                                                                </ItemTemplate>

                                                            </asp:TemplateField>



                                                            <asp:TemplateField HeaderText="Qty2" SortExpression="Qty2">
                                                                <EditItemTemplate>
                                                                    <asp:TextBox ID="txtQty2Edit" runat="server" Width="70px" Text='<%# Bind("Qty2")%>'></asp:TextBox>
                                                                    <asp:Label ID="lblQty2Edit" runat="server" Enabled="false" Text=""></asp:Label>
                                                                </EditItemTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblQty2" runat="server" Text='<%# Bind("Qty2") %>'></asp:Label>
                                                                </ItemTemplate>

                                                            </asp:TemplateField>



                                                            <asp:TemplateField HeaderText="Rate2" SortExpression="Rate2">
                                                                <EditItemTemplate>
                                                                    <asp:TextBox ID="txtRate2Edit" runat="server" Width="70px" Text='<%# Bind("Rate2")%>'></asp:TextBox>
                                                                    <asp:Label ID="lblRate2Edit" runat="server" Enabled="false" Text=""></asp:Label>
                                                                </EditItemTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblRate2" runat="server" Text='<%# Bind("Rate2") %>'></asp:Label>
                                                                </ItemTemplate>

                                                            </asp:TemplateField>


                                                            <asp:TemplateField>

                                                                <ItemTemplate>

                                                                    <asp:ImageButton ID="editbtn" ImageUrl="~/Images123/edit.png" runat="server" CommandName="Edit" ToolTip="Edit" Width="20px" Height="20px" />




                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:ImageButton ImageUrl="~/Images123/save.png" runat="server" CommandName="Update" ToolTip="Update" Width="20px" Height="20px" />
                                                                    <asp:ImageButton ImageUrl="~/Images123/cancel.png" runat="server" CommandName="Cancel" ToolTip="Cancel" Width="20px" Height="20px"></asp:ImageButton>
                                                                </EditItemTemplate>

                                                            </asp:TemplateField>




                                                            <%-- listView coloum --%>
                                                        </Columns>
                                                        <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
                                                        <HeaderStyle BackColor="#000080" Font-Bold="True" ForeColor="White" BorderStyle="Solid" BorderWidth="1px" Wrap="True" Height="5px" />
                                                        <PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="Right" Height="5px" />
                                                    </asp:GridView>


                                                </ContentTemplate>
                                            </asp:UpdatePanel>





                                        </div>

                                    </div>
                                    <!-- /.box-body -->
                                </div>
                                
                            </div>
                <%-- Find Detail --%>
                
               


                <%-- Chkpr --%>
                  <div id="Div4" runat="server">
                                <div class="box">

                                    <!-- /.box-header -->
                                    
                                    <div class="box-body" style="width: 100%; overflow: auto; height: 20%;">
                                       <%-- CssClass="table table-bordered table-hover"--%>
                                      
                                        <div style="width: 100%; height: 200px; overflow: scroll">
                                                                                         
                                 <asp:GridView ID="GridView7" AutoGenerateColumns="False" CssClass="table table-bordered table-hover tableFixHead" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Horizontal" runat="server" DataKeyNames="Sno" OnRowDataBound="GridView7_RowDataBound" >
                                                <Columns>
                                                    <%--<asp:CommandField ShowEditButton="True" ButtonType="Image" EditImageUrl="~/dist/img/edit.png" CancelImageUrl="~/dist/img/Cancel.png" UpdateImageUrl="~/dist/img/Update.png" />--%>

                                                    <asp:TemplateField HeaderText="Sno" ItemStyle-Width="20px">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblRowNumber" runat="server" />
                                                        </ItemTemplate>
                                                        <ItemStyle Width="10px"></ItemStyle>
                                                    </asp:TemplateField>



                                                    <asp:TemplateField HeaderText="" SortExpression="Sno" Visible="false">
                                                        <EditItemTemplate>
                                                            <asp:Label ID="lblSno" runat="server" Visible="false" Text='<%# Eval("Sno") %>'></asp:Label>
                                                             <asp:Label ID="lbldetailSno" runat="server" Text='<%# Bind("SnoDetail") %>' Visible="false"></asp:Label>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSno" runat="server" Visible="false" Text='<%# Bind("Sno") %>'></asp:Label>
                                                            <asp:Label ID="lbldetailSno" runat="server" Text='<%# Bind("SnoDetail") %>' Visible="false"></asp:Label>
                                                        </ItemTemplate>
                                                       
                                                    </asp:TemplateField>




                                                    <asp:TemplateField HeaderText="Requisition No" SortExpression="reqNOdetail">
                                                        <EditItemTemplate>
                                                            <asp:Label ID="lblreq" runat="server" Text='<%# Bind("RequisitionNo") %>'></asp:Label>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblreq" runat="server" Text='<%# Bind("RequisitionNo") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>



                                                    <asp:TemplateField HeaderText="ItemSubHeadName" SortExpression="ItemSubHeadName">
                                                        <EditItemTemplate>
                                                            <asp:Label ID="lblsubhead" runat="server" Text='<%# Bind("ItemSubHeadName") %>'></asp:Label>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblsubhead" runat="server" Text='<%# Bind("ItemSubHeadName") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>



                                                    <asp:TemplateField HeaderText="ItemMaster" SortExpression="Description">
                                                        <EditItemTemplate>
                                                            <asp:Label ID="lblitemmaster" runat="server" Text='<%# Bind("Description") %>'></asp:Label>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblitemmaster" runat="server" Text='<%# Bind("Description") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>



                                                    <asp:TemplateField HeaderText="UOM" SortExpression="UOMName">
                                                        <EditItemTemplate>
                                                            <asp:Label ID="lbluom" runat="server" Text='<%# Bind("UOMName") %>'></asp:Label>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbluom" runat="server" Text='<%# Bind("UOMName") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>



                                                    <asp:TemplateField HeaderText="Qty" SortExpression="Qty">
                                                        <EditItemTemplate>
                                                            <asp:TextBox ID="txtQty" runat="server" Text='<%# Bind("BalanceQty") %>'></asp:TextBox>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblQty" runat="server" Text='<%# Bind("BalanceQty") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>




                                                    <asp:TemplateField HeaderText="Approved Qty" SortExpression="ApprovedQty">
                                                        <EditItemTemplate>
                                                            <asp:TextBox ID="txtApprovedQty" runat="server" Text='<%# Bind("BalanceQty") %>'></asp:TextBox>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtApprovedQty" runat="server" Width="50px" Text='<%# Bind("BalanceQty") %>'></asp:TextBox>
                                                            <%--<asp:Label ID="lblApprovedQty" runat="server" Text='<%# Bind("BalanceQty") %>'></asp:Label>--%>
                                                       </ItemTemplate>
                                                    </asp:TemplateField>



                                                    <asp:TemplateField HeaderText="Rate" SortExpression="Quantity">
                                                        <EditItemTemplate>
                                                            <asp:TextBox ID="txtRate" runat="server"></asp:TextBox>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtRate_PR" runat="server" Width="50px" AutoPostBack="true" OnTextChanged="txtRate_PR_TextChanged"></asp:TextBox>
                                                           <%-- <asp:Label ID="lblRate" runat="server" ></asp:Label>--%>
                                                        </ItemTemplate>
                                                         <ItemStyle Width="100px"></ItemStyle>
                                                    </asp:TemplateField>



                                                     <asp:TemplateField HeaderText="GST %" SortExpression="RequiredQuantity">
                                                        <EditItemTemplate>
                                                            <asp:TextBox ID="txtGST" runat="server"></asp:TextBox>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtGST_PR" runat="server" Width="50px" AutoPostBack="True" OnTextChanged="txtGST_PR_TextChanged"></asp:TextBox>
                                                            <asp:Label ID="lblGST" runat="server" ></asp:Label>
                                                        </ItemTemplate>
                                                         <ItemStyle Width="100px"></ItemStyle>
                                                    </asp:TemplateField>




                                                     <asp:TemplateField HeaderText="GST Rate" SortExpression="RequiredQuantity">
                                                        <EditItemTemplate>
                                                            <asp:TextBox ID="txtGSTRate" runat="server" ></asp:TextBox>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblGSTRate" runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                         <ItemStyle Width="100px"></ItemStyle>
                                                    </asp:TemplateField>




                                                     <asp:TemplateField HeaderText="Gross Value" SortExpression="RequiredQuantity">
                                                        <EditItemTemplate>
                                                            <asp:TextBox ID="txtGrossValue" runat="server" Text='<%# Bind("RequiredQuantity") %>'></asp:TextBox>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblQtyVal" runat="server" Visible="false"></asp:Label>
                                                            <asp:Label ID="lblGstValue" runat="server" Visible="false"></asp:Label>
                                                            <asp:Label ID="lblGrossValue" runat="server" ></asp:Label>
                                                        </ItemTemplate>
                                                         <ItemStyle Width="100px"></ItemStyle>
                                                    </asp:TemplateField>




                                                     <asp:TemplateField HeaderText="Cost Center" SortExpression="RequiredQuantity">
                                                        <EditItemTemplate>
                                                            <asp:TextBox ID="txtCostCenter" runat="server"></asp:TextBox>
                                                        </EditItemTemplate>
                                                        <ItemTemplate> 
                                                            <asp:DropDownList ID="ddlCostCenter" CssClass="form-control select2" Width="150px" runat="server"></asp:DropDownList>
                                                            <%--<asp:Label ID="lblCostCenter" runat="server"></asp:Label>--%>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>




                                                     <asp:TemplateField HeaderText="Location" SortExpression="RequiredQuantity">
                                                        <EditItemTemplate>
                                                            <asp:TextBox ID="txtLocation" runat="server" Text='<%# Bind("RequiredQuantity") %>'></asp:TextBox>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                             <asp:DropDownList ID="ddlLocation" CssClass="form-control select2" Width="150px" runat="server"></asp:DropDownList>
                                                            <%--<asp:Label ID="lblLocation" runat="server" Text='<%# Bind("RequiredQuantity") %>'></asp:Label>--%>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>



                                                    
                                                     <asp:TemplateField HeaderText="project" SortExpression="RequiredQuantity">
                                                        <EditItemTemplate>
                                                            <asp:TextBox ID="txtproject" runat="server" ></asp:TextBox>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtproject" Width="100px" runat="server"></asp:TextBox>
                                                             <%--<asp:Label ID="lblproject" runat="server" Text='<%# Bind("RequiredQuantity") %>'></asp:Label>--%>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>



                                                    <asp:TemplateField HeaderText="UOM2" SortExpression="Project">
                                                        <EditItemTemplate>
                                                            <asp:TextBox ID="txtProjectEdit" runat="server" Text=""></asp:TextBox>
                                                            <asp:Label ID="lblProjectEdit" runat="server" Enabled="false" Text=""></asp:Label>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:DropDownList ID="ddlUOM2" CssClass="form-control select2" Width="70px" runat="server"></asp:DropDownList>
                                                        </ItemTemplate>

                                                    </asp:TemplateField>



                                                    <asp:TemplateField HeaderText="Qty2" SortExpression="Project">
                                                        <EditItemTemplate>
                                                            <asp:TextBox ID="txtProjectEdit" runat="server" Text=""></asp:TextBox>
                                                            <asp:Label ID="lblProjectEdit" runat="server" Enabled="false" Text=""></asp:Label>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="lblQty2" runat="server" Width="50px" Text="" AutoPostBack="True" OnTextChanged="lblQty2_TextChanged"></asp:TextBox>
                                                        </ItemTemplate>

                                                    </asp:TemplateField>



                                                    <asp:TemplateField HeaderText="Rate2" SortExpression="Project">
                                                        <EditItemTemplate>
                                                            <asp:TextBox ID="txtProjectEdit" runat="server" Text=""></asp:TextBox>
                                                            <asp:Label ID="lblProjectEdit" runat="server" Enabled="false" Text=""></asp:Label>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblRate2" runat="server" Text=""></asp:Label>
                                                        </ItemTemplate>

                                                    </asp:TemplateField>



                                                    <asp:TemplateField HeaderText="">
                                                         <HeaderTemplate>
                                                              <asp:CheckBox ID="chkall" runat="server"  Text="All" Width="83px" />
                                                         </HeaderTemplate>
                                                        <EditItemTemplate>
                                                            <asp:CheckBox ID="chkSelect" runat="server" />
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="chkSelect" runat="server"/>
                                                        </ItemTemplate>
                                                        <HeaderStyle Width="40px" />
                                                          <ItemStyle Height="10px" />
                                                    </asp:TemplateField>





                                                    <%-- listView coloum --%>
                                                </Columns>
                                                <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
                                                <HeaderStyle BackColor="#000080" Font-Bold="True" ForeColor="White" BorderStyle="Solid" BorderWidth="1px" Wrap="True" Height="5px" />
                                                <PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="Right" />
                                            </asp:GridView>



                                            <%--<asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:db_HRERPSys %>" SelectCommand="SELECT [Sno],[RequistionNo], [ItemSubHeadName], [Description], [UOM], [RequiredQuantity] FROM [VIEW_ASKFORQOUT]"></asp:SqlDataSource>--%>

                                           <%-- <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:db_HRERPSys %>" SelectCommand="SELECT [DocID], [DocDescription], [DocName], [IsActive], [IsHide] , [Main_Site], [ApplyDate] FROM [viewDOCUSER] order by DocDescription asc"></asp:SqlDataSource>--%>

                                        </div>

                                    </div>
                                    <!-- /.box-body -->
                                </div>
                                
                            </div>
                <%-- ChkPr --%>
            </div>
        </div>

    </section>




  



   



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
                            <td class="rowpadd"><strong>Document</strong> </td>
                            <td class="rowpadd">

                                <asp:DropDownList ID="ddlEdit_Document" CssClass="form-control select2" Width="100%" runat="server"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="rowpadd"><strong>Doc NO</strong> </td>
                            <td class="rowpadd">
                                
                                <asp:DropDownList ID="ddlEdit_DocNO" CssClass="form-control select2" Width="100%" runat="server"></asp:DropDownList>
                            </td>
                        </tr>
                        
                        <tr>
                            <td class="rowpadd"><strong>Entry date</strong> </td>
                            <td class="rowpadd">

                                <div style="display: inline-block">

                                    <div class="input-group input-group-sm">
                                        <asp:TextBox ID="txtEditEntrydate" CssClass="form-control texthieht" Width="220px" runat="server" Font-Bold="False" Font-Italic="False" ForeColor="Black" ValidationGroup="aaa"></asp:TextBox>
                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender2" PopupButtonID="ImageButton1" TargetControlID="txtEditEntrydate" Format="dd-MMM-yyyy" runat="server" />
                                        <span class="input-group-btn">
                                            <asp:ImageButton ID="ImageButton1" CssClass="btn btn-flat" ImageUrl="img/calendar.png" ImageAlign="Bottom" runat="server" />
                                        </span>
                                    </div>
                                </div>

                            </td>
                        </tr>
                        <tr>
                            <td class="rowpadd"><strong>Delivery date</strong> </td>
                            <td class="rowpadd">
                                <div style="display: inline-block">

                                    <div class="input-group input-group-sm">
                                        <asp:TextBox ID="txtEdit_deliveryDate" CssClass="form-control texthieht" Width="220px" runat="server" Font-Bold="False" Font-Italic="False" ForeColor="Black" ValidationGroup="aaa"></asp:TextBox>
                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender4" PopupButtonID="ImageButton3" TargetControlID="txtEdit_deliveryDate" Format="dd-MMM-yyyy" runat="server" />
                                        <span class="input-group-btn">
                                            <asp:ImageButton ID="ImageButton3" CssClass="btn btn-flat" ImageUrl="img/calendar.png" ImageAlign="Bottom" runat="server" />
                                        </span>
                                    </div>
                                </div>

                            </td>
                        </tr>
                        <tr>
                            <td class="rowpadd"><strong>Department</strong> </td>
                            <td class="rowpadd">

                                <asp:DropDownList ID="ddlEdit_Depart" Width="100%" CssClass=" form-control select2" runat="server" ValidationGroup="aaa">
                                 


                                </asp:DropDownList>
                            </td>
                        </tr>
                         <tr>
                            <td class="rowpadd"><strong>Status</strong> </td>
                            <td class="rowpadd">

                                <asp:DropDownList ID="ddlEdit_Status" CssClass="form-control select2" Width="100%" runat="server"></asp:DropDownList>
                            </td>
                        </tr>

                        <tr>
                            <td class="rowpadd"><strong>Active</strong> </td>
                            <td class="rowpadd">
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
                            <td class="rowpadd"><strong>Document</strong> </td>
                            <td class="rowpadd">

                                <asp:DropDownList ID="ddlFind_Document" CssClass="form-control select2" Width="100%" runat="server"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="rowpadd"><strong>Doc NO</strong> </td>
                            <td class="rowpadd">
                                
                                <asp:DropDownList ID="ddlFind_DocNO" CssClass="form-control select2" Width="100%" runat="server"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="rowpadd"><strong>AFQ/Requisition No</strong> </td>
                            <td class="rowpadd">
                                <asp:DropDownList ID="ddlFind_AFQRequisitionNo" Width="100%" CssClass=" form-control select2" runat="server" ValidationGroup="aaa">
                                   
                                </asp:DropDownList>
                                
                            </td>
                        </tr>
                        <tr>
                            <td class="rowpadd"><strong>Entry date</strong> </td>
                            <td class="rowpadd">

                                <div style="display: inline-block">

                                    <div class="input-group input-group-sm">
                                        <asp:TextBox ID="txtFind_Entrydate" CssClass="form-control texthieht" Width="220px" runat="server" Font-Bold="False" Font-Italic="False" ForeColor="Black" ValidationGroup="aaa"></asp:TextBox>
                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender5" PopupButtonID="ImageButton4" TargetControlID="txtFind_Entrydate" Format="dd-MMM-yyyy" runat="server" />
                                        <span class="input-group-btn">
                                            <asp:ImageButton ID="ImageButton4" CssClass="btn btn-flat" ImageUrl="img/calendar.png" ImageAlign="Bottom" runat="server" />
                                        </span>
                                    </div>
                                </div>

                            </td>
                        </tr>
                        <tr>
                            <td class="rowpadd"><strong>Delivery date</strong> </td>
                            <td class="rowpadd">
                                <div style="display: inline-block">

                                    <div class="input-group input-group-sm">
                                        <asp:TextBox ID="txtFind_DeliveryDate" CssClass="form-control texthieht" Width="220px" runat="server" Font-Bold="False" Font-Italic="False" ForeColor="Black" ValidationGroup="aaa"></asp:TextBox>
                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender6" PopupButtonID="ImageButton5" TargetControlID="txtFind_DeliveryDate" Format="dd-MMM-yyyy" runat="server" />
                                        <span class="input-group-btn">
                                            <asp:ImageButton ID="ImageButton5" CssClass="btn btn-flat" ImageUrl="img/calendar.png" ImageAlign="Bottom" runat="server" />
                                        </span>
                                    </div>
                                </div>

                            </td>
                        </tr>
                        <tr>
                            <td class="rowpadd"><strong>Department</strong> </td>
                            <td class="rowpadd">

                                <asp:DropDownList ID="ddlFind_Department" Width="100%" CssClass=" form-control select2" runat="server" ValidationGroup="aaa">
                                 


                                </asp:DropDownList>
                            </td>
                        </tr>
                         <tr>
                            <td class="rowpadd"><strong>Status</strong> </td>
                            <td class="rowpadd">

                                <asp:DropDownList ID="ddlFind_Status" CssClass="form-control select2" Width="100%" runat="server"></asp:DropDownList>
                            </td>
                        </tr>

                        <tr>
                            <td class="rowpadd"><strong>Active</strong> </td>
                            <td class="rowpadd">
                                <asp:CheckBox ID="chkFind" runat="server" Checked="true" Text="Active" />
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
                            <td class="rowpadd"><strong>Document</strong> </td>
                            <td class="rowpadd">

                                <asp:DropDownList ID="ddlDel_Document" CssClass="form-control select2" Width="100%" runat="server"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="rowpadd"><strong>Doc NO</strong> </td>
                            <td class="rowpadd">
                                
                                <asp:DropDownList ID="ddlDel_DocNO" CssClass="form-control select2" Width="100%" runat="server"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="rowpadd"><strong>AFQ/Requisition No</strong> </td>
                            <td class="rowpadd">
                                <asp:DropDownList ID="ddlDel_AFQRequisitionNo" Width="100%" CssClass=" form-control select2" runat="server" ValidationGroup="aaa">
                                   
                                </asp:DropDownList>
                                
                            </td>
                        </tr>
                        <tr>
                            <td class="rowpadd"><strong>Entry date</strong> </td>
                            <td class="rowpadd">

                                <div style="display: inline-block">

                                    <div class="input-group input-group-sm">
                                        <asp:TextBox ID="txtDel_Entrydate" CssClass="form-control texthieht" Width="220px" runat="server" Font-Bold="False" Font-Italic="False" ForeColor="Black" ValidationGroup="aaa"></asp:TextBox>
                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender7" PopupButtonID="ImageButton6" TargetControlID="txtEditEntrydate" Format="dd-MMM-yyyy" runat="server" />
                                        <span class="input-group-btn">
                                            <asp:ImageButton ID="ImageButton6" CssClass="btn btn-flat" ImageUrl="img/calendar.png" ImageAlign="Bottom" runat="server" />
                                        </span>
                                    </div>
                                </div>

                            </td>
                        </tr>
                        <tr>
                            <td class="rowpadd"><strong>Delivery date</strong> </td>
                            <td class="rowpadd">
                                <div style="display: inline-block">

                                    <div class="input-group input-group-sm">
                                        <asp:TextBox ID="txtDel_DelviDate" CssClass="form-control texthieht" Width="220px" runat="server" Font-Bold="False" Font-Italic="False" ForeColor="Black" ValidationGroup="aaa"></asp:TextBox>
                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender8" PopupButtonID="ImageButton7" TargetControlID="txtEdit_deliveryDate" Format="dd-MMM-yyyy" runat="server" />
                                        <span class="input-group-btn">
                                            <asp:ImageButton ID="ImageButton7" CssClass="btn btn-flat" ImageUrl="img/calendar.png" ImageAlign="Bottom" runat="server" />
                                        </span>
                                    </div>
                                </div>

                            </td>
                        </tr>
                        <tr>
                            <td class="rowpadd"><strong>Department</strong> </td>
                            <td class="rowpadd">

                                <asp:DropDownList ID="ddlDel_Department" Width="100%" CssClass=" form-control select2" runat="server" ValidationGroup="aaa">
                                 


                                </asp:DropDownList>
                            </td>
                        </tr>
                         <tr>
                            <td class="rowpadd"><strong>Status</strong> </td>
                            <td class="rowpadd">

                                <asp:DropDownList ID="ddlDel_Status" CssClass="form-control select2" Width="100%" runat="server"></asp:DropDownList>
                            </td>
                        </tr>

                        <tr>
                            <td class="rowpadd"><strong>Active</strong> </td>
                            <td class="rowpadd">
                                <asp:CheckBox ID="chkDel" runat="server" Checked="true" Text="Active" />
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
    <%--  ENd Modals --%>





    
    

    <script src="https://unpkg.com/sweetalert/dist/sweetalert.min.js"></script>

</asp:Content>

