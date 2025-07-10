<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="frm_StockTransferIn.aspx.cs" Inherits="frm_Period"  EnableEventValidation="false"%>

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

        function DetEditalert() {
            swal("Update Successfuly!", " Detail record has been saved!!!", "success")
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

        .modalBackground {
            background-color: Black;
            filter: alpha(opacity=90);
            opacity: 0.8;
        }

        .modalPopup {
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

    <section class="content">
        <div class="row">
            <div class="col-lg-12">

                <div class="box box-solid shadowbox " style="border-radius: 15px;">
                    <div class="box-header with-border" style="height: 75px;">
                        <div style="padding-top: 0px; width: 120px; float: left;">
                            <div id="tabmenu">
                                <h3 class="box-title" style="color: #fff; padding-left: 10px;"><strong>Stock Transfer IN</strong></h3>
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
                            <asp:LinkButton ID="btnShowJV" Height="40" CssClass="btn btn-app" runat="server" Style="left: 180px; background-color: #1f4f8a; color: white;" OnClick="btnShowJV_Click">
                     <i class="fas fa-remove"></i>&nbsp Show JV
                            </asp:LinkButton>

                        </div>

                    </div>


                    <div class="form-row">

                        <div class="box-body" style="background-color: #cddbf2;">
                            <div id="fieldbox" runat="server">

                                <table id="tablecontent1" runat="server" style="width: 100%;">
                                    <tr>
                                        <td class="auto-style6"><strong>Document Name</strong></td>
                                        <td class="auto-style11">

                                            <asp:DropDownList ID="ddlDocName" Width="250px" CssClass=" form-control select2" runat="server" ValidationGroup="aaa" OnSelectedIndexChanged="ddlDocName_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" ControlToValidate="ddlDocName" InitialValue="Please select..." Display="None" ErrorMessage="<b> Missing Field</b><br />Please Select Document." ValidationGroup="aaa" />
                                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" TargetControlID="RequiredFieldValidator1" runat="server" HighlightCssClass="vali" PopupPosition="Right"></ajaxToolkit:ValidatorCalloutExtender>


                                        </td>
                                        <td class="auto-style6"><strong>Entry Date </strong></td>
                                        <td class="auto-style11">

                                            <div style="display: inline-block">

                                                <div class="input-group input-group-sm">
                                                    <asp:TextBox ID="txtEntryDate" CssClass="form-control texthieht" Width="220px" runat="server" Font-Bold="False" Font-Italic="False" ForeColor="Black" ValidationGroup="aaa"></asp:TextBox>
                                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender1" PopupButtonID="imgPopup" TargetControlID="txtEntryDate" Format="dd-MMM-yyyy" runat="server" />
                                                    <asp:RequiredFieldValidator runat="server" ID="Req2" ControlToValidate="txtEntryDate" Display="None" ErrorMessage="<b> Missing Field</b><br />A Date is required." ValidationGroup="aaa" />
                                                    <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender11" TargetControlID="Req2" runat="server" HighlightCssClass="vali"></ajaxToolkit:ValidatorCalloutExtender>
                                                    <span class="input-group-btn">

                                                        <asp:ImageButton ID="imgPopup" CssClass="btn btn-flat" ImageUrl="img/calendar.png" ImageAlign="Bottom" runat="server" />
                                                    </span>
                                                </div>

                                            </div>

                                        </td>
                                        <td class="auto-style6"><strong>Document No</strong></td>
                                        <td class="auto-style11">

                                            <asp:TextBox ID="txtDocNo" CssClass="texthieht" Width="250px" runat="server" Font-Bold="False" Font-Italic="False" ForeColor="Black" Height="25px" ValidationGroup="aaa"></asp:TextBox>
                                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator8" ControlToValidate="txtDocNo" InitialValue="Please select..." Display="None" ErrorMessage="<b> Missing Field</b><br />Please Select Document." ValidationGroup="aaa" />
                                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender10" TargetControlID="RequiredFieldValidator8" runat="server" HighlightCssClass="vali" PopupPosition="Right"></ajaxToolkit:ValidatorCalloutExtender>
                                        </td>

                                    </tr>
                                    <tr visible="false">

                                        <td class="auto-style6"><strong>GRN</strong></td>
                                        <td class="auto-style11">

                                            <asp:RadioButton ID="chkDirectGRN" Text="Direct" runat="server" OnCheckedChanged="chkDirectGRN_CheckedChanged" AutoPostBack="true" />

                                            <asp:RadioButton ID="chkNormalGRN" Text="Normal" runat="server" AutoPostBack="True" OnCheckedChanged="chkNormalGRN_CheckedChanged" />

                                        </td>
                                        <td class="auto-style6"><strong>Supplier</strong></td>
                                        <td class="auto-style11">
                                            <asp:DropDownList ID="ddlSupplier" Width="250px" CssClass=" form-control select2" runat="server" ValidationGroup="aaa" AutoPostBack="True" OnSelectedIndexChanged="ddlSupplier_SelectedIndexChanged">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator5" ControlToValidate="ddlSupplier" InitialValue="Please select..." Display="None" ErrorMessage="<b> Missing Field</b><br />Please Select Supplier." ValidationGroup="aaa" />
                                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender7" TargetControlID="RequiredFieldValidator5" runat="server" HighlightCssClass="vali"></ajaxToolkit:ValidatorCalloutExtender>

                                        </td>

                                        <td class="auto-style6"><strong>Select PO #</strong></td>
                                        <td class="auto-style11">

                                            <div>


                                                <%--OnSelectedIndexChanged="lstFruits_SelectedIndexChanged" AutoPostBack="True"  --%>
                                                <asp:ListBox ID="lstFruits" runat="server" SelectionMode="Multiple" Width="250px" DataTextField="docnosupp" DataValueField="Sno" AutoPostBack="true" OnSelectedIndexChanged="lstFruits_SelectedIndexChanged"></asp:ListBox>
                                                <%-- <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:db_HRERPSys %>" SelectCommand="select Sno, [DocumentNo.] + '--' + BPartnerName as docnosupp FROM View_POlistPoNo where Status <> 2 and balQty >0 and IsActive = 1 and Record_Deleted = 0 and GocID =  1 and CompId = 1 and BranchId = 5 and FiscalYearID = 3"></asp:SqlDataSource>--%>
                                            </div>

                                        </td>
                                    </tr>
                                    <tr>


                                        <td class="auto-style6"><strong>Transfer Out Document No</strong></td>
                                        <td class="auto-style11">

                                            <asp:TextBox ID="txttransferNo" CssClass="texthieht" Width="250px" runat="server" Font-Bold="False" Font-Italic="False" ForeColor="Black" Height="25px" ValidationGroup="aaa"></asp:TextBox>
                                            <asp:DropDownList ID="ddltransfer" Width="250px" CssClass=" form-control select2" runat="server" ValidationGroup="aaa" OnSelectedIndexChanged="ddltransfer_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator2" ControlToValidate="ddltransfer" InitialValue="Please select..." Display="None" ErrorMessage="<b> Missing Field</b><br />Please Select Document." ValidationGroup="aaa" />
                                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" TargetControlID="RequiredFieldValidator1" runat="server" HighlightCssClass="vali" PopupPosition="Right"></ajaxToolkit:ValidatorCalloutExtender>


                                        </td>
                                        
                                        <td class="auto-style6" visible="false"><strong>Delivery Challan No.</strong></td>
                                        <td class="auto-style11" visible="false">

                                            <asp:TextBox ID="txtDeliveryChallan" CssClass="texthieht" Width="250px" runat="server" Font-Bold="False" Font-Italic="False" ForeColor="Black" Height="25px" ValidationGroup="aaa"></asp:TextBox>
                                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator3" ControlToValidate="txtDeliveryChallan" Display="None" ErrorMessage="<b> Missing Field</b><br />Please Enter Delivery Challan No." ValidationGroup="aaa" />
                                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender4" TargetControlID="RequiredFieldValidator3" runat="server" HighlightCssClass="vali" PopupPosition="Right"></ajaxToolkit:ValidatorCalloutExtender>

                                        </td>
                                        <td class="auto-style6" visible="false"><strong>Delivery Challan Date</strong></td>
                                        <td class="auto-style11" visible="false">




                                            <div style="display: inline-block">

                                                <div class="input-group input-group-sm">
                                                    <asp:TextBox ID="txtDeliveryChallanDate" CssClass="form-control texthieht" Width="220px" runat="server" Font-Bold="False" Font-Italic="False" ForeColor="Black" ValidationGroup="aaa"></asp:TextBox>
                                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender3" PopupButtonID="ImageButton2" TargetControlID="txtDeliveryChallanDate" Format="dd-MMM-yyyy" runat="server" />
                                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator6" ControlToValidate="txtDeliveryChallanDate" Display="None" ErrorMessage="<b> Missing Field</b><br />A Date is required." ValidationGroup="aaa" />
                                                    <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" TargetControlID="RequiredFieldValidator6" runat="server" HighlightCssClass="vali"></ajaxToolkit:ValidatorCalloutExtender>
                                                    <span class="input-group-btn">

                                                        <asp:ImageButton ID="ImageButton2" CssClass="btn btn-flat" ImageUrl="img/calendar.png" ImageAlign="Bottom" runat="server" />
                                                    </span>
                                                </div>
                                            </div>
                                        </td>


                                        
                                        <td class="auto-style6"><strong>Location-In</strong></td>
                                        <td class="auto-style11">

                                            <asp:DropDownList ID="ddlLocationTo" Width="250px" CssClass=" form-control select2" runat="server" ValidationGroup="aaa">
                                            </asp:DropDownList>
                                            <%-- <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator14" ControlToValidate="ddlLocationTo" InitialValue="Please select..." Display="None" ErrorMessage="<b> Missing Field</b><br />Please Select Location To." ValidationGroup="aaa" />
                                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender15" TargetControlID="RequiredFieldValidator14" runat="server" HighlightCssClass="vali"></ajaxToolkit:ValidatorCalloutExtender>--%>
                                        </td>
                                        <td class="auto-style6"><strong>Location-From</strong></td>
                                        <td class="auto-style11">
                                            <asp:DropDownList ID="ddlLocationFrom" Width="250px" CssClass=" form-control select2" runat="server" ValidationGroup="aaa">
                                            </asp:DropDownList>
                                            <%--<asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator7" ControlToValidate="ddlLocationFrom" InitialValue="Please select..." Display="None" ErrorMessage="<b> Missing Field</b><br />Please Select Location From." ValidationGroup="aaa" />
                                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender5" TargetControlID="RequiredFieldValidator7" runat="server" HighlightCssClass="vali"></ajaxToolkit:ValidatorCalloutExtender>--%>
                                        </td>

                                    </tr>
                                    <tr>
                                        <td class="auto-style6" visible="false"><strong>Inward Gate Pass No</strong></td>
                                        <td class="auto-style11" visible="false">

                                            <asp:TextBox ID="txtInwardGatePassNo" CssClass="texthieht" Width="250px" runat="server" Font-Bold="False" Font-Italic="False" ForeColor="Black" Height="25px" ValidationGroup="aaa"></asp:TextBox>
                                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator13" ControlToValidate="txtInwardGatePassNo" Display="None" ErrorMessage="<b> Missing Field</b><br />Please Enter Inward Gate Pass No." ValidationGroup="aaa" />
                                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender14" TargetControlID="RequiredFieldValidator13" runat="server" HighlightCssClass="vali"></ajaxToolkit:ValidatorCalloutExtender>



                                        </td>
                                        <td class="auto-style6"><strong>Document Authority</strong></td>
                                        <td class="auto-style11">


                                            <asp:DropDownList ID="ddlDocAuth" Width="250px" CssClass=" form-control select2" runat="server" ValidationGroup="aaa">
                                            </asp:DropDownList>

                                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator4" ControlToValidate="ddlDocAuth" InitialValue="Please select..." Display="None" ErrorMessage="<b> Missing Field</b><br />Please Select document Authority." ValidationGroup="aaa" />
                                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender6" TargetControlID="RequiredFieldValidator4" runat="server" HighlightCssClass="vali"></ajaxToolkit:ValidatorCalloutExtender>

                                        </td>
                                         <td class="auto-style6" ><strong>Cost Center From </strong></td>
                                        <td class="auto-style11">

                                            <asp:DropDownList ID="ddlCostCentrFrom" Width="250px" CssClass=" form-control select2" runat="server" ValidationGroup="aaa">
                                            </asp:DropDownList>
                                            <%-- <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator2" ControlToValidate="ddlCostCentrFrom" InitialValue="Please select..." Display="None" ErrorMessage="<b> Missing Field</b><br />Please Select Cost Center from" ValidationGroup="aaa" />
                                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" TargetControlID="RequiredFieldValidator2" runat="server" HighlightCssClass="vali"></ajaxToolkit:ValidatorCalloutExtender>--%>
                                        </td>
                                        <td class="auto-style6"><strong>Cost Center To</strong></td>
                                        <td class="auto-style11">

                                            <asp:DropDownList ID="ddlCostCenterTo" Width="250px" CssClass=" form-control select2" runat="server" ValidationGroup="aaa">
                                            </asp:DropDownList>
                                            <%-- <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator12" ControlToValidate="ddlCostCenterTo" InitialValue="Please select..." Display="None" ErrorMessage="<b> Missing Field</b><br />Please Select Cost Center To." ValidationGroup="aaa" />
                                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender13" TargetControlID="RequiredFieldValidator12" runat="server" HighlightCssClass="vali"></ajaxToolkit:ValidatorCalloutExtender>--%>
                                        </td>
                                        
                                    </tr>
                                    <tr>
                                        <td class="auto-style6"><strong>Account Code</strong></td>
                                        <td class="auto-style11">
                                            <%--<asp:TextBox ID="txtAccountCode" CssClass="texthieht" Width="250px" runat="server" Font-Bold="False" Font-Italic="False" ForeColor="Black" Height="25px"></asp:TextBox>--%>
                                            <%--<asp:DropDownList ID="ddlAccountCode" Width="250px" CssClass="select2" runat="server"></asp:DropDownList>--%>
                                            <telerik:RadComboBox RenderMode="Lightweight" ID="RadComboAcct_Code" runat="server" Height="200" Width="250"
                                                DropDownWidth="500" EmptyMessage="Choose an Account Code" HighlightTemplatedItems="true"
                                                EnableLoadOnDemand="true" Filter="StartsWith"
                                                AutoPostBack="true"
                                                OnSelectedIndexChanged="RadComboAcct_Code_SelectedIndexChanged"
                                                OnItemsRequested="RadComboAcct_Code_ItemsRequested"
                                                Label="">
                                                <HeaderTemplate>
                                                    <table style="width: 100%" cellspacing="0" cellpadding="0">
                                                        <tr>
                                                            <td style="width: 60px;">Acct Code
                                                            </td>
                                                            <td style="width: 175px;">Account Description
                                                            </td>
                                                            <td style="width: 40px;">Acct Type
                                                            </td>
                                                            <td style="width: 40px;">Tran Type
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <table style="width: 500px" cellspacing="0" cellpadding="0">
                                                        <tr>
                                                            <td style="width: 60px;">
                                                                <%# DataBinder.Eval(Container, "Text")%>
                                                            </td>
                                                            <td style="width: 175px;">
                                                                <%# DataBinder.Eval(Container, "Attributes['Acct_Description']")%>
                                                            </td>
                                                            <td style="width: 60px;">
                                                                <%# DataBinder.Eval(Container, "Attributes['Acct_Type_Name']")%>
                                                            </td>
                                                            <td style="width: 60px;">
                                                                <%# DataBinder.Eval(Container, "Attributes['Tran_Type']")%>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </ItemTemplate>
                                            </telerik:RadComboBox>
                                        </td>
                                       
                                        <td class="auto-style6"><strong>Status</strong></td>
                                        <td class="auto-style11">

                                            <asp:DropDownList ID="ddlStatus" Width="250px" CssClass=" form-control select2" runat="server" ValidationGroup="aaa">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator15" ControlToValidate="ddlStatus" InitialValue="Please select..." Display="None" ErrorMessage="<b> Missing Field</b><br />Please Select Status." ValidationGroup="aaa" />
                                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender16" TargetControlID="RequiredFieldValidator15" runat="server" HighlightCssClass="vali"></ajaxToolkit:ValidatorCalloutExtender>
                                        </td>
                                         <td class="auto-style6" ><strong>Narration</strong></td>
                                        <td class="auto-style11">

                                            <asp:TextBox ID="txtRemarks" CssClass="texthieht" Width="250px" runat="server" Font-Bold="False" Font-Italic="False" ForeColor="Black" Height="25px" ValidationGroup="aaa"></asp:TextBox>



                                        </td>

                                    </tr>
                                    <tr>
                                       

                                        <td class="auto-style6" visible="false"><strong>Shop/Office Name</strong></td>
                                        <td class="auto-style11" visible="false">
                                            <asp:TextBox ID="txtShopOffName" CssClass="texthieht" Width="250px" runat="server" Font-Bold="False" Font-Italic="False" ForeColor="Black" Height="25px" ValidationGroup="aaa"></asp:TextBox>
                                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator9" ControlToValidate="txtShopOffName" Display="None" ErrorMessage="<b> Missing Field</b><br />Please Eneter Shop / Office Name." ValidationGroup="aaa" />
                                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender8" TargetControlID="RequiredFieldValidator9" runat="server" HighlightCssClass="vali"></ajaxToolkit:ValidatorCalloutExtender>


                                        </td>
                                        

                                    </tr>

                                    <tr>

                                        <td class="auto-style6" visible="false"><strong>Bill No</strong></td>
                                        <td class="auto-style11" visible="false">

                                            <asp:TextBox ID="txtbillNo" CssClass="texthieht" Width="250px" runat="server" Font-Bold="False" Font-Italic="False" ForeColor="Black" Height="25px" ValidationGroup="aaa"></asp:TextBox>
                                            <%--<asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator19" ControlToValidate="txtbillNo" Display="None" ErrorMessage="<b> Missing Field</b><br />Please Enter Bill No." ValidationGroup="aaa" />
                                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender20" TargetControlID="RequiredFieldValidator19" runat="server" HighlightCssClass="vali"></ajaxToolkit:ValidatorCalloutExtender> --%>


                                        </td>

                                        <td class="auto-style6" visible="false"><strong>Bill Date</strong></td>
                                        <td visible="false">
                                            <div style="display: inline-block" visible="false">

                                                <div class="input-group input-group-sm">
                                                    <asp:TextBox ID="txtBilldate" CssClass="form-control texthieht" Width="220px" runat="server" Font-Bold="False" Font-Italic="False" ForeColor="Black" ValidationGroup="aaa"></asp:TextBox>
                                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender9" PopupButtonID="imgPop2" TargetControlID="txtBilldate" Format="dd-MMM-yyyy" runat="server" />
                                                    <%--<asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator16" ControlToValidate="txtBilldate" Display="None" ErrorMessage="<b> Missing Field</b><br />A Date is required." ValidationGroup="aaa" />
                                                    <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender17" TargetControlID="RequiredFieldValidator16" runat="server" HighlightCssClass="vali"></ajaxToolkit:ValidatorCalloutExtender>--%>
                                                    <span class="input-group-btn">

                                                        <asp:ImageButton ID="imgPop2" CssClass="btn btn-flat" ImageUrl="img/calendar.png" ImageAlign="Bottom" runat="server" />
                                                    </span>
                                                </div>
                                            </div>
                                        </td>





                                        <td class="auto-style6" visible="false"><strong>Weighbridge No.</strong></td>
                                        <td class="auto-style11" visible="false">
                                            <asp:TextBox ID="txtWeighbridgeNo" CssClass="texthieht" Width="250px" runat="server" Font-Bold="False" Font-Italic="False" ForeColor="Black" Height="25px" ValidationGroup="aaa"></asp:TextBox>
                                            <%-- <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator10" ControlToValidate="txtWeighbridgeNo" Display="None" ErrorMessage="<b> Missing Field</b><br />Please enter Weighbridge No." ValidationGroup="aaa" />
                                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender9" TargetControlID="RequiredFieldValidator10" runat="server" HighlightCssClass="vali"></ajaxToolkit:ValidatorCalloutExtender>--%>


                                        </td>


                                    </tr>


                                    <tr>
                                        <td class="auto-style6" visible="false"><strong>Net Weight</strong></td>
                                        <td class="auto-style11" visible="false">

                                            <asp:TextBox ID="txtNetWeight" CssClass="texthieht" Width="250px" runat="server" Font-Bold="False" Font-Italic="False" ForeColor="Black" Height="25px" ValidationGroup="aaa"></asp:TextBox>
                                            <%--<asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator11" ControlToValidate="txtNetWeight" Display="None" ErrorMessage="<b> Missing Field</b><br />Please Enetr Net weight." ValidationGroup="aaa" />
                                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender12" TargetControlID="RequiredFieldValidator11" runat="server" HighlightCssClass="vali"></ajaxToolkit:ValidatorCalloutExtender>--%>


                                        </td>
                                      
                                    </tr>


                                    <tr>
                                          <td class="auto-style6"><strong>Active</strong></td>
                                        <td class="auto-style11">
                                            <asp:Panel ID="Panel1" runat="server">
                                                <asp:CheckBox ID="chkActive" runat="server" Text="Active" TabIndex="3" />
                                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                            </asp:Panel>


                                        </td>
                                        <td class="auto-style6"><strong>Created Date</strong></td>
                                        <td class="auto-style11">
                                            <asp:TextBox ID="txtCreateddate" CssClass="texthieht" Width="250px" runat="server" Font-Bold="False" Font-Italic="False" ForeColor="Black" Height="25px"></asp:TextBox>
                                        </td>
                                        <td class="auto-style6"><strong>Updated Date</strong></td>
                                        <td class="auto-style11">
                                            <asp:TextBox ID="txtUpdateDate" CssClass="texthieht" Width="250px" runat="server" Font-Bold="False" Font-Italic="False" ForeColor="Black" Height="25px"></asp:TextBox>

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
                                                    <asp:BoundField DataField="Sno" HeaderText="Sno" SortExpression="Sno"></asp:BoundField>
                                                    <asp:BoundField DataField="DocDescription" HeaderText="Document" SortExpression="DocDescription"></asp:BoundField>
                                                    <asp:BoundField DataField="DocumentNo" HeaderText="Doc No" SortExpression="DocumentNo"></asp:BoundField>
                                                    <asp:BoundField DataField="StatusName" HeaderText="Status" SortExpression="StatusName" />
                                                    <asp:BoundField DataField="EntryDate" HeaderText="Entry Date" SortExpression="EntryDate" DataFormatString="{0:dd-MMM-yyyy}" />

                                                    <asp:CheckBoxField DataField="IsActive" HeaderText="IsActive" SortExpression="IsActive"></asp:CheckBoxField>
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

                                            <asp:GridView ID="GridView2" runat="server" CssClass="table table-bordered table-hover tableFixHead" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Horizontal" OnSelectedIndexChanged="GridView2_SelectedIndexChanged">
                                                <Columns>
                                                    <asp:CommandField ButtonType="Image" SelectImageUrl="~/dist/img/edit.png" ShowSelectButton="True">
                                                        <ItemStyle Width="20px" />
                                                    </asp:CommandField>
                                                    <asp:BoundField DataField="Sno" HeaderText="Sno" SortExpression="Sno"></asp:BoundField>
                                                    <asp:BoundField DataField="DocDescription" HeaderText="Document" SortExpression="DocDescription"></asp:BoundField>
                                                    <asp:BoundField DataField="DocumentNo" HeaderText="Doc No" SortExpression="DocumentNo"></asp:BoundField>
                                                    
                                                    <asp:BoundField DataField="StatusName" HeaderText="Status" SortExpression="StatusName" />
                                                    <asp:BoundField DataField="EntryDate" HeaderText="Entry Date" SortExpression="EntryDate" DataFormatString="{0:dd-MMM-yyyy}" />

                                                    <asp:CheckBoxField DataField="IsActive" HeaderText="IsActive" SortExpression="IsActive"></asp:CheckBoxField>
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
                                            <asp:GridView ID="GridView3" runat="server" CssClass="table table-bordered table-hover tableFixHead" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Horizontal" OnSelectedIndexChanged="GridView3_SelectedIndexChanged">
                                                <Columns>
                                                    <asp:CommandField ButtonType="Image" SelectImageUrl="~/dist/img/edit.png" ShowSelectButton="True">
                                                        <ItemStyle Width="20px" />
                                                    </asp:CommandField>
                                                    <asp:BoundField DataField="Sno" HeaderText="Sno" SortExpression="Sno"></asp:BoundField>
                                                    <asp:BoundField DataField="DocDescription" HeaderText="Document" SortExpression="DocDescription"></asp:BoundField>
                                                    <asp:BoundField DataField="DocumentNo" HeaderText="Doc No" SortExpression="DocumentNo"></asp:BoundField>
                                                    <asp:BoundField DataField="StatusName" HeaderText="Status" SortExpression="StatusName" />
                                                    <asp:BoundField DataField="EntryDate" HeaderText="Entry Date" SortExpression="EntryDate" DataFormatString="{0:dd-MMM-yyyy}" />

                                                    <asp:CheckBoxField DataField="IsActive" HeaderText="IsActive" SortExpression="IsActive"></asp:CheckBoxField>
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

                                <asp:GridView ID="GridView5" AutoGenerateColumns="False" CssClass="table table-bordered table-hover tableFixHead" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Horizontal" runat="server" DataKeyNames="Sno" OnRowDataBound="GridView5_RowDataBound">
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
                                                <asp:TextBox ID="txtRate" runat="server" Width="50px" Text='<%# Bind("Rate") %>' OnTextChanged="txtRate_TextChanged"></asp:TextBox>
                                                <%-- <asp:Label ID="lblRate" runat="server" ></asp:Label>--%>
                                            </ItemTemplate>
                                        </asp:TemplateField>



                                        <asp:TemplateField HeaderText="GST %" SortExpression="RequiredQuantity">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtGST" runat="server"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtGST" runat="server" Width="50px" AutoPostBack="True" Text='<%# Bind("GST") %>' OnTextChanged="txtGST_TextChanged"></asp:TextBox>
                                                <asp:Label ID="lblGST" runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>




                                        <asp:TemplateField HeaderText="GST Rate" SortExpression="RequiredQuantity">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtGSTRate" runat="server"></asp:TextBox>
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
                                                <asp:Label ID="lblGrossValue" runat="server"></asp:Label>
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
                                                <asp:TextBox ID="txtproject" runat="server"></asp:TextBox>
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
                                                <asp:CheckBox ID="chkall" runat="server" Text="All" Width="83px" />
                                            </HeaderTemplate>
                                            <EditItemTemplate>
                                                <asp:CheckBox ID="chkSelect" runat="server" />
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkSelect" runat="server" />
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

                <%-- Gridview for Direct --%>
                <div id="Div2" runat="server">
                    <div class="box">

                        <!-- /.box-header -->

                        <div class="box-body" style="width: 100%; overflow: auto; height: 50%;">
                            <%-- CssClass="table table-bordered table-hover"--%>

                            <div style="width: 100%; height: 400px; overflow: scroll">





                                <asp:GridView ID="GridView6" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-hover tableFixHead" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Horizontal" ShowFooter="true" ShowHeaderWhenEmpty="true" OnRowDataBound="GridView6_RowDataBound" OnRowDeleting="GridView6_RowDeleting" OnRowCommand="GridView6_RowCommand" OnRowEditing="GridView6_RowEditing" OnRowUpdating="GridView6_RowUpdating" OnRowCancelingEdit="GridView6_RowCancelingEdit">

                                    <Columns>


                                        <asp:TemplateField HeaderText="Sno" ItemStyle-Width="50" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblRowNumber" runat="server" />
                                            </ItemTemplate>

                                            <ItemStyle Width="100px"></ItemStyle>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="A/c code" SortExpression="A/c code">
                                            <EditItemTemplate>
                                                <%--<asp:DropDownList ID="ddlAccode" Width="150px" CssClass=" form-control select2" runat="server" ValidationGroup="aaa"></asp:DropDownList>--%>
                                                <asp:Label ID="lblAcctCode" runat="server" Enabled="false" Text='<%# Bind("Accode") %>'></asp:Label>

                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblAccountCode" runat="server" Text='<%# Bind("Accode") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>





                                                <telerik:RadComboBox RenderMode="Lightweight" ID="RadComboAcct_CodeFooter" runat="server" Height="200" Width="150"
                                                    DropDownWidth="500" EmptyMessage="Choose an Account Code" HighlightTemplatedItems="true"
                                                    EnableLoadOnDemand="true" Filter="StartsWith"
                                                    OnItemsRequested="RadComboAcct_CodeFooter_ItemsRequested"
                                                    Enabled="false"
                                                    Label="">
                                                    <HeaderTemplate>
                                                        <table style="width: 100%" cellspacing="0" cellpadding="0">
                                                            <tr>
                                                                <td style="width: 60px;">Acct Code
                                                                </td>
                                                                <td style="width: 175px;">Account Description
                                                                </td>
                                                                <td style="width: 40px;">Acct Type
                                                                </td>
                                                                <td style="width: 40px;">Tran Type
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <table style="width: 500px" cellspacing="0" cellpadding="0">
                                                            <tr>
                                                                <td style="width: 60px;">
                                                                    <%# DataBinder.Eval(Container, "Text")%>
                                                                </td>
                                                                <td style="width: 175px;">
                                                                    <%# DataBinder.Eval(Container, "Attributes['Acct_Description']")%>
                                                                </td>
                                                                <td style="width: 60px;">
                                                                    <%# DataBinder.Eval(Container, "Attributes['Acct_Type_Name']")%>
                                                                </td>
                                                                <td style="width: 60px;">
                                                                    <%# DataBinder.Eval(Container, "Attributes['Tran_Type']")%>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </ItemTemplate>
                                                </telerik:RadComboBox>






                                                <%--RAD Account Code--%>
                                            </FooterTemplate>
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="PO No." SortExpression="ItemSubHeadName" Visible="false">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtPoNEdit" runat="server" Enabled="false" Text='<%# Bind("PONo") %>'></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblPONo" runat="server" Text='<%# Bind("PONo") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lblPONoFooter" runat="server"></asp:Label>
                                            </FooterTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Item Sub Head" SortExpression="Description">

                                            <ItemTemplate>
                                                <asp:Label ID="lblItemSubHead" runat="server" Text='<%# Bind("ItemSubHead") %>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>

                                                <asp:DropDownList ID="ddlItemSubHeadEdit" Width="150px" CssClass=" form-control select2" runat="server" ValidationGroup="aaa"></asp:DropDownList>
                                                <asp:Label ID="lblItemSubHead" runat="server" Enabled="false" Text='<%# Bind("ItemSubHead") %>'></asp:Label>

                                            </EditItemTemplate>
                                            <FooterTemplate>
                                                <asp:DropDownList ID="ddlItemSubHeadFooter" Width="150px" CssClass=" form-control select2" runat="server" ValidationGroup="aaa" AutoPostBack="True" OnSelectedIndexChanged="ddlitemsub_SelectedIndexChanged"></asp:DropDownList>
                                            </FooterTemplate>
                                        </asp:TemplateField>




                                        <asp:TemplateField HeaderText="Item Master" SortExpression="UOM">
                                            <EditItemTemplate>
                                                <asp:DropDownList ID="ddlItemMasterEdit" Width="150px" CssClass=" form-control select2" runat="server" ValidationGroup="aaa"></asp:DropDownList>
                                                <asp:Label ID="lblItemMaster" runat="server" Enabled="false" Text='<%# Bind("ItemMaster") %>'></asp:Label>

                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblItemMaster" runat="server" Text='<%# Bind("ItemMaster") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:DropDownList ID="ddlItemMasterFooter" Width="150px" CssClass=" form-control select2" runat="server" ValidationGroup="aaa" AutoPostBack="True" OnSelectedIndexChanged="ddlitemMaster_SelectedIndexChanged"></asp:DropDownList>
                                            </FooterTemplate>
                                        </asp:TemplateField>




                                        <asp:TemplateField HeaderText="UOM" SortExpression="CurrentStock">
                                            <EditItemTemplate>
                                                <asp:DropDownList ID="ddlUOMEdit" Width="150px" CssClass=" form-control select2" runat="server" ValidationGroup="aaa"></asp:DropDownList>
                                                <asp:Label ID="lblUOMEdit" runat="server" Width="100px" Text='<%# Bind("UOM") %>'></asp:Label>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblUOM" runat="server" Width="100px" Text='<%# Bind("UOM") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:DropDownList ID="ddlUOMFooter" Width="70px" CssClass=" form-control select2" runat="server" ValidationGroup="aaa"></asp:DropDownList>
                                                <%--<asp:TextBox ID="txtUOMFooter" Width="100px" runat="server"></asp:TextBox>--%>
                                            </FooterTemplate>
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="PO bal Qty" Visible="false" SortExpression="RequiredQuantity">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtPObalQty" runat="server" Width="100px" Text='<%# Bind("PObalQty") %>'></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblPObalQty" runat="server" Width="100px" Text='<%# Bind("PObalQty") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:TextBox ID="txtPObalQtyFooter" Width="100px" runat="server" OnTextChanged="txtPObalQtyFooter_TextChanged" AutoPostBack="true"></asp:TextBox>
                                            </FooterTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="DC Qty" SortExpression="RequiredQuantity">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtDCQtyEdit" runat="server" Width="70px" Text='<%# Bind("DCQty") %>'></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblDCQty" runat="server" Width="70px" Text='<%# Bind("DCQty") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:TextBox ID="txtDCQtyFooter" Width="70px" runat="server" OnTextChanged="txtDCQtyFooter_TextChanged" AutoPostBack="true"></asp:TextBox>
                                            </FooterTemplate>
                                        </asp:TemplateField>



                                        <asp:TemplateField HeaderText="Rate" SortExpression="CostCenter">
                                            <EditItemTemplate>
                                                <%--<asp:DropDownList ID="ddlRate" Width="150px" CssClass=" form-control select2" runat="server" ValidationGroup="aaa"></asp:DropDownList>--%>
                                                <asp:TextBox ID="lblRateEdit" runat="server" Enabled="false" Text='<%# Bind("Rate") %>'></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblRate" runat="server" Width="150px" Text='<%# Bind("Rate") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:TextBox ID="txtRateFooter" Width="100px" runat="server" OnTextChanged="txtRateFooter_TextChanged" AutoPostBack="true"></asp:TextBox>
                                            </FooterTemplate>
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Value" SortExpression="Project">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtValueEdit" runat="server" Text='<%# Bind("Value") %>'></asp:TextBox>
                                                <%--<asp:Label ID="lblValueEdit" runat="server" Enabled="false" Text='<%# Bind("Value") %>'></asp:Label>--%>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblValue" runat="server" Text='<%# Bind("Value") %>'></asp:Label>

                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:TextBox ID="lblValueFooter" Enabled="false" Width="150px" runat="server"></asp:TextBox>

                                            </FooterTemplate>
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Cost Center" SortExpression="Project">
                                            <EditItemTemplate>
                                                <asp:Label ID="lblCostCenterEdit" runat="server" Text='<%# Bind("CostCenter") %>'></asp:Label>
                                                <asp:DropDownList ID="ddlCostCenterEdit" Width="150px" CssClass=" form-control select2" runat="server" ValidationGroup="aaa"></asp:DropDownList>

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
                                                <asp:DropDownList ID="ddlLocationEdit" Width="150px" CssClass=" form-control select2" runat="server" ValidationGroup="aaa"></asp:DropDownList>
                                                <asp:Label ID="lblLocationEdit" runat="server" Text='<%# Bind("Location") %>'></asp:Label>

                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblLocation" runat="server" Text='<%# Bind("Location") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:DropDownList ID="ddlLocationFooter" Width="150px" CssClass=" form-control select2" runat="server" ValidationGroup="aaa"></asp:DropDownList>
                                                <%--<asp:TextBox ID="lblLocationFooter" Width="100px" runat="server"></asp:TextBox>--%>
                                            </FooterTemplate>
                                        </asp:TemplateField>



                                        <asp:TemplateField HeaderText="Project/Job/Batch No." SortExpression="Project">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtProjectEdit" runat="server" Text='<%# Bind("Project") %>'></asp:TextBox>

                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblProject" runat="server" Text='<%# Bind("Project") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:TextBox ID="lblProjectFooter" Width="100px" runat="server"></asp:TextBox>
                                            </FooterTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Warrantee/Expiry Date" SortExpression="Project">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtWarranteeEdit" runat="server" Text='<%# Bind("Warrantee") %>'></asp:TextBox>

                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblWarrantee" runat="server" Text='<%# Bind("Warrantee") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>




                                                <div style="display: inline-block">

                                                    <div class="input-group input-group-sm">
                                                        <asp:TextBox ID="lblWarranteeFooter" CssClass="form-control texthieht" Width="120px" runat="server" Font-Bold="False" Font-Italic="False" ForeColor="Black" ValidationGroup="aaa" Enabled="false"></asp:TextBox>
                                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender9" PopupButtonID="imgPop33" TargetControlID="lblWarranteeFooter" Format="dd-MMM-yyyy" runat="server" />
                                                        <%--<asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator16" ControlToValidate="lblWarranteeFooter" Display="None" ErrorMessage="<b> Missing Field</b><br />A Date is required." ValidationGroup="aaa" />
                                                    <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender17" TargetControlID="RequiredFieldValidator16" runat="server" HighlightCssClass="vali"></ajaxToolkit:ValidatorCalloutExtender>--%>
                                                        <span class="input-group-btn">

                                                            <asp:ImageButton ID="imgPop33" CssClass="btn btn-flat" ImageUrl="img/calendar.png" ImageAlign="Bottom" runat="server" />
                                                        </span>
                                                    </div>
                                                </div>







                                                <%--<asp:TextBox ID="lblWarranteeFooter" Width="100px" runat="server"></asp:TextBox>--%>
                                            </FooterTemplate>
                                        </asp:TemplateField>



                                        <asp:TemplateField HeaderText="UOM2" SortExpression="Project">
                                            <EditItemTemplate>
                                                <asp:Label ID="lblUOM2Edit" runat="server" Text='<%# Bind("UOM2") %>'></asp:Label>
                                                <asp:DropDownList ID="ddlUOM2Edit" Width="150px" CssClass=" form-control select2" runat="server" ValidationGroup="aaa"></asp:DropDownList>

                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblUOM2" runat="server" Text='<%# Bind("UOM2") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:DropDownList ID="ddlUOM2Footer" Width="70px" CssClass=" form-control select2" runat="server" ValidationGroup="aaa"></asp:DropDownList>
                                            </FooterTemplate>
                                        </asp:TemplateField>



                                        <asp:TemplateField HeaderText="Qty2" SortExpression="Project">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtQty2Edit" runat="server" Text='<%# Bind("Qty2") %>'></asp:TextBox>

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
                                                <asp:TextBox ID="txtRate2Edit" runat="server" Text='<%# Bind("Rate2") %>'></asp:TextBox>

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

                <div id="Div5" runat="server">
                    <div class="box">

                        <!-- /.box-header -->

                        <div class="box-body" style="width: 100%; overflow: auto; height: 50%;">
                            <%-- CssClass="table table-bordered table-hover"--%>

                            <div style="width: 100%; height: 400px; overflow: scroll">

                                <asp:GridView ID="GridView8" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-hover tableFixHead" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Horizontal" ShowFooter="true" ShowHeaderWhenEmpty="true">

                                    <Columns>


                                        <asp:TemplateField HeaderText="Sno" ItemStyle-Width="50">
                                            <ItemTemplate>
                                                <asp:Label ID="lblRowNumber" runat="server" />
                                            </ItemTemplate>

                                            <ItemStyle Width="100px"></ItemStyle>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="A/c code" SortExpression="A/c code">
                                            <EditItemTemplate>
                                                <asp:DropDownList ID="ddlAccode" Width="150px" CssClass=" form-control select2" runat="server" ValidationGroup="aaa"></asp:DropDownList>
                                                <%--<asp:Label ID="lblAcctCode" runat="server" Enabled="false" Text='<%# Bind("Accode") %>'></asp:Label>--%>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblAccountCode" runat="server" Text='<%# Bind("DrAccountCode") %>'></asp:Label>
                                            </ItemTemplate>

                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="PO No." SortExpression="ItemSubHeadName">
                                            <EditItemTemplate>
                                                <asp:Label ID="lblPoNEdit" runat="server" Enabled="false" Text='<%# Bind("PONO1") %>'></asp:Label>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblPONo" runat="server" Text='<%# Bind("PONO1") %>'></asp:Label>
                                            </ItemTemplate>

                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Item Sub Head" SortExpression="Description">

                                            <ItemTemplate>
                                                <asp:Label ID="lblItemSubHead" runat="server" Text='<%# Bind("ItemSubHeadName") %>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>

                                                <asp:DropDownList ID="ddlItemSubHeadEdit" Width="150px" CssClass=" form-control select2" runat="server" ValidationGroup="aaa"></asp:DropDownList>
                                                <%--<asp:Label ID="lblItemSubHead" runat="server" Enabled="false" Text='<%# Bind("ItemSubHead") %>'></asp:Label>--%>
                                            </EditItemTemplate>

                                        </asp:TemplateField>




                                        <asp:TemplateField HeaderText="Item Master" SortExpression="UOM">
                                            <EditItemTemplate>
                                                <asp:DropDownList ID="ddlItemMasterEdit" Width="150px" CssClass=" form-control select2" runat="server" ValidationGroup="aaa"></asp:DropDownList>
                                                <%--<asp:Label ID="lblItemMaster" runat="server" Enabled="false" Text='<%# Bind("ItemMaster") %>'></asp:Label>--%>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblItemMaster" runat="server" Text='<%# Bind("Description") %>'></asp:Label>
                                            </ItemTemplate>

                                        </asp:TemplateField>




                                        <asp:TemplateField HeaderText="UOM" SortExpression="CurrentStock">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtUOMEdit" runat="server" Width="100px" Text='<%# Bind("UOMName") %>'></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblUOM" runat="server" Width="100px" Text='<%# Bind("UOMName") %>'></asp:Label>
                                            </ItemTemplate>

                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="PO bal Qty" SortExpression="RequiredQuantity">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtPObalQty" runat="server" Width="100px" Text='<%# Bind("pobal") %>'></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblPObalQty" runat="server" Width="100px" Text='<%# Bind("pobal") %>'></asp:Label>
                                            </ItemTemplate>

                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="DC Qty" SortExpression="RequiredQuantity">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtDCQty" runat="server" Width="100px" Text='<%# Bind("pobal1") %>'></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblDCQty" runat="server" Width="100px" Text='<%# Bind("pobal1") %>'></asp:Label>
                                            </ItemTemplate>

                                        </asp:TemplateField>



                                        <asp:TemplateField HeaderText="Rate" SortExpression="CostCenter">

                                            <EditItemTemplate>
                                                <asp:DropDownList ID="ddlRate" Width="150px" CssClass=" form-control select2" runat="server" ValidationGroup="aaa"></asp:DropDownList>
                                                <asp:Label ID="lblRateEdit" runat="server" Enabled="false" Text='<%# Bind("Rate") %>'></asp:Label>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblRate" runat="server" Width="50px" Text='<%# Bind("Rate") %>'></asp:Label>
                                            </ItemTemplate>

                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Value" SortExpression="Project">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtValueEdit" runat="server" Text='<%# Bind("Value") %>'></asp:TextBox>
                                                <asp:Label ID="lblValueEdit" runat="server" Enabled="false" Text='<%# Bind("Value") %>'></asp:Label>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblValue" runat="server" Text='<%# Bind("Value") %>'></asp:Label>

                                            </ItemTemplate>

                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Cost Center" SortExpression="Project">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtCostCenterEdit" runat="server" Text='<%# Bind("CostCenterName") %>'></asp:TextBox>
                                                <asp:Label ID="lblCostCenterEdit" runat="server" Enabled="false" Text='<%# Bind("CostCenterName") %>'></asp:Label>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblCostCenter" runat="server" Text='<%# Bind("CostCenterName") %>'></asp:Label>
                                            </ItemTemplate>

                                        </asp:TemplateField>




                                        <asp:TemplateField HeaderText="Location" SortExpression="Project">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtLocationEdit" runat="server" Text='<%# Bind("LocName") %>'></asp:TextBox>
                                                <asp:Label ID="lblLocationEdit" runat="server" Enabled="false" Text='<%# Bind("LocName") %>'></asp:Label>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblLocation" runat="server" Text='<%# Bind("LocName") %>'></asp:Label>
                                            </ItemTemplate>

                                        </asp:TemplateField>



                                        <asp:TemplateField HeaderText="Project/Job/Batch No." SortExpression="Project">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtProjectEdit" runat="server" Text='<%# Bind("Project") %>'></asp:TextBox>
                                                <asp:Label ID="lblProjectEdit" runat="server" Enabled="false" Text='<%# Bind("Project") %>'></asp:Label>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblProject" runat="server" Text='<%# Bind("Project") %>'></asp:Label>
                                            </ItemTemplate>

                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Warrantee/Expiry Date" SortExpression="Project">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtWarranteeEdit" runat="server" Text='<%# Bind("Warrentee") %>'></asp:TextBox>
                                                <asp:Label ID="lblWarranteeEdit" runat="server" Enabled="false" Text='<%# Bind("Warrentee") %>'></asp:Label>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblWarrantee" runat="server" Text='<%# Bind("Warrentee") %>'></asp:Label>
                                            </ItemTemplate>

                                        </asp:TemplateField>



                                        <asp:TemplateField HeaderText="UOM2" SortExpression="Project">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtUOM2Edit" runat="server" Text='<%# Bind("uomName2") %>'></asp:TextBox>
                                                <asp:Label ID="lblUOM2Edit" runat="server" Enabled="false" Text='<%# Bind("uomName2") %>'></asp:Label>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblUOM2" runat="server" Text='<%# Bind("uomName2") %>'></asp:Label>
                                            </ItemTemplate>

                                        </asp:TemplateField>



                                        <asp:TemplateField HeaderText="Qty2" SortExpression="Project">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtQty2Edit" runat="server" Text='<%# Bind("Qty2") %>'></asp:TextBox>
                                                <asp:Label ID="lblQty2Edit" runat="server" Enabled="false" Text='<%# Bind("Qty2") %>'></asp:Label>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblQty2" runat="server" Text='<%# Bind("Qty2") %>'></asp:Label>
                                            </ItemTemplate>

                                        </asp:TemplateField>



                                        <asp:TemplateField HeaderText="Rate2" SortExpression="Project">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtRate2Edit" runat="server" Text='<%# Bind("Rate2") %>'></asp:TextBox>
                                                <asp:Label ID="lblRate2Edit" runat="server" Enabled="false" Text='<%# Bind("Rate2") %>'></asp:Label>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblRate2" runat="server" Text='<%# Bind("Rate2") %>'></asp:Label>
                                            </ItemTemplate>

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

                            <div style="width: 100%; height: 280px; overflow: scroll">

                                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
                                    <Triggers>
                                        <asp:PostBackTrigger ControlID="GridView4" />
                                    </Triggers>
                                    <ContentTemplate>
                                        <asp:GridView ID="GridView4" AutoGenerateColumns="False" CssClass="table table-bordered table-hover tableFixHead" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Horizontal" runat="server" DataKeyNames="Sno" OnRowDataBound="GridView4_RowDataBound" OnRowCancelingEdit="GridView4_RowCancelingEdit" OnRowEditing="GridView4_RowEditing" OnRowUpdating="GridView4_RowUpdating">
                                            <Columns>


                                                <asp:TemplateField HeaderText="Sno" Visible="false" ItemStyle-Width="50">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblRowNumber" runat="server" />
                                                    </ItemTemplate>

                                                    <ItemStyle Width="100px"></ItemStyle>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="SNODetail" Visible="false" SortExpression="A/c code">
                                                    <EditItemTemplate>
                                                        <asp:DropDownList ID="ddlAccode" Width="150px" CssClass=" form-control select2" runat="server" ValidationGroup="aaa"></asp:DropDownList>
                                                        <%--<asp:Label ID="lblAcctCode" runat="server" Enabled="false" Text='<%# Bind("Accode") %>'></asp:Label>--%>
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSno" runat="server" Text='<%# Bind("Sno") %>'></asp:Label>
                                                    </ItemTemplate>

                                                </asp:TemplateField>



                                                <asp:TemplateField HeaderText="A/c code" SortExpression="A/c code">
                                                    <EditItemTemplate>
                                                        <asp:DropDownList ID="ddlAccode" Width="150px" CssClass=" form-control select2" runat="server" ValidationGroup="aaa"></asp:DropDownList>
                                                        <%--<asp:Label ID="lblAcctCode" runat="server" Enabled="false" Text='<%# Bind("Accode") %>'></asp:Label>--%>
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblAccountCode" runat="server" Text='<%# Bind("DrAccountCode") %>'></asp:Label>
                                                    </ItemTemplate>

                                                </asp:TemplateField>


                                                <asp:TemplateField HeaderText="PO No." Visible="false" SortExpression="ItemSubHeadName">
                                                    <EditItemTemplate>
                                                        <asp:Label ID="lblPoNEdit" runat="server" Enabled="false" Text='<%# Bind("PONO1") %>'></asp:Label>
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPONo" runat="server" Text='<%# Bind("PONO1") %>'></asp:Label>
                                                    </ItemTemplate>

                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Item Sub Head" SortExpression="Description">

                                                    <ItemTemplate>
                                                        <asp:Label ID="lblItemSubHead" runat="server" Text='<%# Bind("ItemSubHeadName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>

                                                        <asp:DropDownList ID="ddlItemSubHeadEdit" Width="150px" CssClass=" form-control select2" runat="server" ValidationGroup="aaa"></asp:DropDownList>
                                                        <%--<asp:Label ID="lblItemSubHead" runat="server" Enabled="false" Text='<%# Bind("ItemSubHead") %>'></asp:Label>--%>
                                                    </EditItemTemplate>

                                                </asp:TemplateField>




                                                <asp:TemplateField HeaderText="Item Master" SortExpression="UOM">
                                                    <EditItemTemplate>
                                                        <asp:DropDownList ID="ddlItemMasterEdit" Width="150px" CssClass=" form-control select2" runat="server" ValidationGroup="aaa"></asp:DropDownList>
                                                        <%--<asp:Label ID="lblItemMaster" runat="server" Enabled="false" Text='<%# Bind("ItemMaster") %>'></asp:Label>--%>
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblItemMaster" runat="server" Text='<%# Bind("Description") %>'></asp:Label>
                                                    </ItemTemplate>

                                                </asp:TemplateField>




                                                <asp:TemplateField HeaderText="UOM" SortExpression="CurrentStock">
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txtUOMEdit" runat="server" Width="100px" Text='<%# Bind("UOMName") %>'></asp:TextBox>
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblUOM" runat="server" Width="100px" Text='<%# Bind("UOMName") %>'></asp:Label>
                                                    </ItemTemplate>

                                                </asp:TemplateField>


                                                <asp:TemplateField HeaderText="PO bal Qty" SortExpression="RequiredQuantity" Visible="false">
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txtPObalQty" runat="server" Width="100px" Text='<%# Bind("Quantity") %>'></asp:TextBox>
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPObalQty" runat="server" Width="100px" Text='<%# Bind("Quantity") %>'></asp:Label>
                                                    </ItemTemplate>

                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Qty" SortExpression="RequiredQuantity" ItemStyle-HorizontalAlign="Right">
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txtDCQty" runat="server" Width="100px" Text='<%# Bind("Quantity") %>'></asp:TextBox>
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDCQty" runat="server" Width="100px" Text='<%# Bind("Quantity") %>'></asp:Label>
                                                    </ItemTemplate>

                                                </asp:TemplateField>



                                                <asp:TemplateField HeaderText="Rate" SortExpression="CostCenter" Visible="false">

                                                    <EditItemTemplate>
                                                        <asp:DropDownList ID="ddlRate" Width="150px" CssClass=" form-control select2" runat="server" ValidationGroup="aaa"></asp:DropDownList>
                                                        <asp:Label ID="lblRateEdit" runat="server" Enabled="false" Text='<%# Bind("Rate") %>'></asp:Label>
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblRate" runat="server" Width="50px" Text='<%# Bind("Rate") %>'></asp:Label>
                                                    </ItemTemplate>

                                                </asp:TemplateField>


                                                <asp:TemplateField HeaderText="Value" SortExpression="Project" Visible="false">
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txtValueEdit" runat="server" Text='<%# Bind("Value") %>'></asp:TextBox>
                                                        <asp:Label ID="lblValueEdit" runat="server" Enabled="false" Text='<%# Bind("Value") %>'></asp:Label>
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblValue" runat="server" Text='<%# Bind("Value") %>'></asp:Label>

                                                    </ItemTemplate>

                                                </asp:TemplateField>


                                                <asp:TemplateField HeaderText="Cost Center" SortExpression="Project">
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txtCostCenterEdit" runat="server" Text='<%# Bind("CostCenterName") %>'></asp:TextBox>
                                                        <asp:Label ID="lblCostCenterEdit" runat="server" Enabled="false" Text='<%# Bind("CostCenterName") %>'></asp:Label>
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCostCenter" runat="server" Text='<%# Bind("CostCenterName") %>'></asp:Label>
                                                    </ItemTemplate>

                                                </asp:TemplateField>




                                                <asp:TemplateField HeaderText="Location" SortExpression="Project">
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txtLocationEdit" runat="server" Text='<%# Bind("LocName") %>'></asp:TextBox>
                                                        <asp:Label ID="lblLocationEdit" runat="server" Enabled="false" Text='<%# Bind("LocName") %>'></asp:Label>
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblLocation" runat="server" Text='<%# Bind("LocName") %>'></asp:Label>
                                                    </ItemTemplate>

                                                </asp:TemplateField>



                                                <asp:TemplateField HeaderText="Project/Job/Batch No." SortExpression="Project">
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txtProjectEdit" runat="server" Text='<%# Bind("Project") %>'></asp:TextBox>
                                                        <asp:Label ID="lblProjectEdit" runat="server" Enabled="false" Text='<%# Bind("Project") %>'></asp:Label>
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblProject" runat="server" Text='<%# Bind("Project") %>'></asp:Label>
                                                    </ItemTemplate>

                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Warrantee/Expiry Date" SortExpression="Project">
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txtWarranteeEdit" runat="server" Text='<%# Bind("Warrentee","{0:dd-MMM-yyyy}") %>'></asp:TextBox>
                                                        <asp:Label ID="lblWarranteeEdit" runat="server" Enabled="false" Text='<%# Bind("Warrentee","{0:dd-MMM-yyyy}") %>'></asp:Label>
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblWarrantee" runat="server" Text='<%# Bind("Warrentee","{0:dd-MMM-yyyy}") %>'></asp:Label>
                                                    </ItemTemplate>

                                                </asp:TemplateField>



                                                <asp:TemplateField HeaderText="UOM2" SortExpression="Project" Visible="false">
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txtUOM2Edit" runat="server" Text='<%# Bind("uomName2") %>'></asp:TextBox>
                                                        <asp:Label ID="lblUOM2Edit" runat="server" Enabled="false" Text='<%# Bind("uomName2") %>'></asp:Label>
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblUOM2" runat="server" Text='<%# Bind("uomName2") %>'></asp:Label>
                                                    </ItemTemplate>

                                                </asp:TemplateField>



                                                <asp:TemplateField HeaderText="Qty2" SortExpression="Project" Visible="false">
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txtQty2Edit" runat="server" Text='<%# Bind("Qty2") %>'></asp:TextBox>
                                                        <asp:Label ID="lblQty2Edit" runat="server" Enabled="false" Text='<%# Bind("Qty2") %>'></asp:Label>
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblQty2" runat="server" Text='<%# Bind("Qty2") %>'></asp:Label>
                                                    </ItemTemplate>

                                                </asp:TemplateField>



                                                <asp:TemplateField HeaderText="Rate2" SortExpression="Project" Visible="false">
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txtRate2Edit" runat="server" Text='<%# Bind("Rate2") %>'></asp:TextBox>
                                                        <asp:Label ID="lblRate2Edit" runat="server" Enabled="false" Text='<%# Bind("Rate2") %>'></asp:Label>
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblRate2" runat="server" Text='<%# Bind("Rate2") %>'></asp:Label>
                                                    </ItemTemplate>

                                                </asp:TemplateField>







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

                <%-- Gridview for Normal --%>
                <div id="Div4" runat="server">
                    <div class="box">

                        <!-- /.box-header -->

                        <div class="box-body" style="width: 100%; overflow: auto; height: 50%;">
                            <%-- CssClass="table table-bordered table-hover"--%>

                            <div style="width: 100%; height: 400px; overflow: scroll">

                                <asp:GridView ID="GridView7" AutoGenerateColumns="False" CssClass="table table-bordered table-hover tableFixHead" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Horizontal" runat="server" DataKeyNames="Sno" OnRowDataBound="GridView7_RowDataBound">
                                    <Columns>
                                        <%--<asp:CommandField ShowEditButton="True" ButtonType="Image" EditImageUrl="~/dist/img/edit.png" CancelImageUrl="~/dist/img/Cancel.png" UpdateImageUrl="~/dist/img/Update.png" />--%>

                                        <asp:TemplateField HeaderText="Sno" ItemStyle-Width="20" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblRowNumber" runat="server" />
                                            </ItemTemplate>
                                            <ItemStyle Width="100px"></ItemStyle>
                                        </asp:TemplateField>



                                        <asp:TemplateField HeaderText="" SortExpression="Sno" Visible="false">
                                            <EditItemTemplate>
                                                <asp:Label ID="lblSno" runat="server" Visible="false" Text='<%# Eval("Sno") %>'></asp:Label>
                                                <asp:Label ID="lbldetailSno" runat="server" Text='<%# Bind("Sno") %>' Visible="false"></asp:Label>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblSno" runat="server" Visible="false" Text='<%# Bind("Sno") %>'></asp:Label>
                                                <asp:Label ID="lbldetailSno" runat="server" Text='<%# Bind("Sno") %>' Visible="false"></asp:Label>
                                            </ItemTemplate>

                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="A/c code" Visible="false" SortExpression="A/c code">
                                            <EditItemTemplate>
                                                <%--<asp:DropDownList ID="ddlAccode" Width="150px" CssClass=" form-control select2" runat="server" ValidationGroup="aaa"></asp:DropDownList>--%>
                                                <asp:Label ID="lblAcctCode" runat="server" Enabled="false"></asp:Label>

                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblAcctCode" runat="server" Enabled="false"></asp:Label>
                                                <asp:TextBox ID="txtAccCode" runat="server" Enabled="false"></asp:TextBox>

                                                <%--<asp:Label ID="lblAccountCode" runat="server" ></asp:Label>--%>
                                            </ItemTemplate>
                                            <FooterTemplate>



                                                <%--RAD Account Code--%>
                                            </FooterTemplate>
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="PO No." SortExpression="PO No.">
                                            <EditItemTemplate>
                                                <asp:Label ID="lblreq" runat="server" Text='<%# Bind("docno") %>'></asp:Label>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblPoNoN" runat="server" Text='<%# Bind("docno") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>



                                        <asp:TemplateField HeaderText="Item Sub Head Name" SortExpression="ItemSubHeadName">
                                            <EditItemTemplate>
                                                <asp:Label ID="lblsubhead" runat="server" Text='<%# Bind("ItemSubHeadName") %>'></asp:Label>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblsubhead" runat="server" Text='<%# Bind("ItemSubHeadName") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>



                                        <asp:TemplateField HeaderText="Item Master" SortExpression="Description">
                                            <EditItemTemplate>
                                                <asp:Label ID="lblitemmaster" runat="server" Text='<%# Bind("Description") %>'></asp:Label>
                                            </EditItemTemplate>
                                            <ItemTemplate>

                                                <asp:Label ID="ItemMasterid" runat="server" Text='<%# Bind("ItemMaster") %>'></asp:Label>
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


                                        <asp:TemplateField HeaderText="PO bal Qty" SortExpression="Qty">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtQty" runat="server" Text='<%# Bind("balQty") %>'></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblQty" runat="server" Text='<%# Bind("balQty") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>




                                        <asp:TemplateField HeaderText="Dc Qty" SortExpression="ApprovedQty">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtApprovedQty" runat="server"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblPOSNO" Visible="false" runat="server" Text='<%# Bind("Sno") %>'></asp:Label>
                                                <asp:Label ID="lblPOMaster" runat="server" Visible="false" Text='<%# Bind("Sno_Master") %>'></asp:Label>
                                                <%-- <asp:TextBox ID="txtApprovedQty" runat="server" Width="50px" Text='<%# Bind("Quantity") %>'></asp:TextBox>--%>
                                                <asp:TextBox ID="lblDCQtyN" runat="server" Width="50px"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>





                                        <asp:TemplateField HeaderText="Cost Center" SortExpression="RequiredQuantity">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtCostCenter" runat="server"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <%--<asp:DropDownList ID="ddlCostCenter" CssClass="form-control select2"  Width="150px" runat="server"></asp:DropDownList>--%>
                                                <asp:Label ID="lblCostCenter" runat="server" Text='<%# Bind("CostCenterName") %>'></asp:Label>
                                                <%--<asp:Label ID="lblCostCenter" runat="server"></asp:Label>--%>
                                            </ItemTemplate>
                                        </asp:TemplateField>




                                        <asp:TemplateField HeaderText="Location" SortExpression="RequiredQuantity">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtLocation" runat="server" Text='<%# Bind("RequiredQuantity") %>'></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <%--<asp:DropDownList ID="ddlLocation" CssClass="form-control select2" Width="150px" runat="server"></asp:DropDownList>--%>
                                                <asp:Label ID="lblLocation" runat="server" Text='<%# Bind("LocName") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Project/Job/Batch No." SortExpression="RequiredQuantity">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtproject" runat="server"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <%--<asp:TextBox ID="txtproject" Width="100px" runat="server"></asp:TextBox>--%>

                                                <asp:Label ID="lblproject" runat="server" Text='<%# Bind("Project") %>'></asp:Label>
                                                <%--<asp:Label ID="lblproject" runat="server" Text='<%# Bind("RequiredQuantity") %>'></asp:Label>--%>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Warrantee" SortExpression="RequiredQuantity">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtproject" runat="server"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <%--<asp:TextBox ID="txtproject" Width="100px" runat="server"></asp:TextBox>--%>

                                                <%--<asp:Label ID="lblproject" runat="server" Text='<%# Bind("Project") %>'></asp:Label>--%>

                                                <div style="display: inline-block">

                                                    <div class="input-group input-group-sm">
                                                        <asp:TextBox ID="txtWarranteeEdit" CssClass="form-control texthieht" Width="120px" runat="server" Font-Bold="False" Font-Italic="False" ForeColor="Black" ValidationGroup="aaa"></asp:TextBox>
                                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender9" PopupButtonID="imgPop33" TargetControlID="txtWarranteeEdit" Format="dd-MMM-yyyy" runat="server" />
                                                        <%--<asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator16" ControlToValidate="lblWarranteeFooter" Display="None" ErrorMessage="<b> Missing Field</b><br />A Date is required." ValidationGroup="aaa" />
                                                    <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender17" TargetControlID="RequiredFieldValidator16" runat="server" HighlightCssClass="vali"></ajaxToolkit:ValidatorCalloutExtender>--%>
                                                        <span class="input-group-btn">

                                                            <asp:ImageButton ID="imgPop33" CssClass="btn btn-flat" ImageUrl="img/calendar.png" ImageAlign="Bottom" runat="server" />
                                                        </span>
                                                    </div>
                                                </div>






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
                                                <asp:TextBox ID="lblQty2" runat="server" Width="50px" Text=""></asp:TextBox>
                                            </ItemTemplate>

                                        </asp:TemplateField>



                                        <%-- <asp:TemplateField HeaderText="Rate2" SortExpression="Project">
                                                        <EditItemTemplate>
                                                            <asp:TextBox ID="txtProjectEdit" runat="server" Text=""></asp:TextBox>
                                                            <asp:Label ID="lblProjectEdit" runat="server" Enabled="false" Text=""></asp:Label>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblRate2" runat="server" Text=""></asp:Label>
                                                        </ItemTemplate>

                                                    </asp:TemplateField>--%>



                                        <asp:TemplateField HeaderText="">
                                            <HeaderTemplate>
                                                <asp:CheckBox ID="chkall" runat="server" Text="All" Width="83px" />
                                            </HeaderTemplate>
                                            <EditItemTemplate>
                                                <asp:CheckBox ID="chkSelect" runat="server" />
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkSelect" runat="server" />
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

                <%-- Find Detial --%>
                <div id="Div6" runat="server">
                    <div class="box">



                        <div class="box-body" style="width: 100%; overflow: auto; height: 50%;">
                            <%-- CssClass="table table-bordered table-hover"--%>

                            <div style="width: 100%; height: 400px; overflow: scroll">

                                <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
                                    <Triggers>
                                        <asp:PostBackTrigger ControlID="GridView9" />
                                    </Triggers>
                                    <ContentTemplate>
                                        <asp:GridView ID="GridView9" AutoGenerateColumns="False" CssClass="table table-bordered table-hover tableFixHead" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Horizontal" runat="server" DataKeyNames="Sno" OnRowDataBound="GridView9_RowDataBound" OnRowCancelingEdit="GridView9_RowCancelingEdit" OnRowEditing="GridView9_RowEditing" OnRowUpdating="GridView9_RowUpdating">
                                            <Columns>


                                                <asp:TemplateField HeaderText="Sno" Visible="false" ItemStyle-Width="50">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblRowNumber" runat="server" />
                                                    </ItemTemplate>

                                                    <ItemStyle Width="100px"></ItemStyle>
                                                </asp:TemplateField>


                                                <asp:TemplateField HeaderText="A/c code" Visible="false" SortExpression="A/c code">
                                                    <EditItemTemplate>
                                                        <asp:DropDownList ID="ddlAccode" Width="150px" CssClass=" form-control select2" runat="server" ValidationGroup="aaa"></asp:DropDownList>
                                                        <asp:Label ID="lblpurSnoED" runat="server" Text='<%# Bind("purSno") %>'></asp:Label>
                                                        <%--<asp:Label ID="lblAcctCode" runat="server" Enabled="false" Text='<%# Bind("Accode") %>'></asp:Label>--%>
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblAccountCode" runat="server" Text='<%# Bind("DrAccountCode") %>'></asp:Label>
                                                        <asp:Label ID="lblpurSno" runat="server" Text='<%# Bind("purSno") %>'></asp:Label>
                                                    </ItemTemplate>

                                                </asp:TemplateField>


                                                <asp:TemplateField HeaderText="PO No." SortExpression="ItemSubHeadName">
                                                    <EditItemTemplate>
                                                        <asp:Label ID="lblPoNEdit" runat="server" Enabled="false" Text='<%# Bind("PONO1") %>'></asp:Label>

                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSno" Visible="false" runat="server" Text='<%# Bind("Sno") %>'></asp:Label>
                                                        <asp:Label ID="lblPONo" runat="server" Text='<%# Bind("PONO1") %>'></asp:Label>
                                                    </ItemTemplate>

                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Item Sub Head" SortExpression="Description">

                                                    <ItemTemplate>
                                                        <asp:Label ID="lblItemSubHead" runat="server" Text='<%# Bind("ItemSubHeadName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:Label ID="lblItemSubHead" runat="server" Text='<%# Bind("ItemSubHeadName") %>'></asp:Label>
                                                        <%--<asp:DropDownList ID="ddlItemSubHeadEdit" Width="150px" CssClass=" form-control select2" runat="server" ValidationGroup="aaa"></asp:DropDownList>--%>
                                                        <%--<asp:Label ID="lblItemSubHead" runat="server" Enabled="false" Text='<%# Bind("ItemSubHead") %>'></asp:Label>--%>
                                                    </EditItemTemplate>

                                                </asp:TemplateField>




                                                <asp:TemplateField HeaderText="Item Master" SortExpression="UOM">
                                                    <EditItemTemplate>
                                                        <asp:Label ID="lblItemMasterEdit" runat="server" Text='<%# Bind("Description") %>'></asp:Label>
                                                        <%--<asp:DropDownList ID="ddlItemMasterEdit" Width="150px" CssClass=" form-control select2" runat="server" ValidationGroup="aaa"></asp:DropDownList>--%>
                                                        <%--<asp:Label ID="lblItemMaster" runat="server" Enabled="false" Text='<%# Bind("ItemMaster") %>'></asp:Label>--%>
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblItemMaster" runat="server" Text='<%# Bind("Description") %>'></asp:Label>
                                                    </ItemTemplate>

                                                </asp:TemplateField>




                                                <asp:TemplateField HeaderText="UOM" SortExpression="CurrentStock">
                                                    <EditItemTemplate>
                                                        <asp:Label ID="txtUOMEdit" runat="server" Width="100px" Text='<%# Bind("UOMName") %>'></asp:Label>
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblUOM" runat="server" Width="100px" Text='<%# Bind("UOMName") %>'></asp:Label>
                                                    </ItemTemplate>

                                                </asp:TemplateField>


                                                <asp:TemplateField HeaderText="PO bal Qty" SortExpression="RequiredQuantity">
                                                    <EditItemTemplate>
                                                        <asp:Label ID="txtPObalQty" runat="server" Width="100px" Text='<%# Bind("BalQty") %>'></asp:Label>
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPObalQty" runat="server" Width="100px" Text='<%# Bind("BalQty") %>'></asp:Label>
                                                    </ItemTemplate>

                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="DC Qty" SortExpression="RequiredQuantity">
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txtDCQty" runat="server" Width="100px" Text='<%# Bind("Quantity") %>'></asp:TextBox>
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDCQty" runat="server" Width="100px" Text='<%# Bind("Quantity") %>'></asp:Label>
                                                    </ItemTemplate>

                                                </asp:TemplateField>



                                                <%-- <asp:TemplateField HeaderText="Rate" SortExpression="CostCenter">
                                                                
                                                                <EditItemTemplate>
                                                                    <asp:DropDownList ID="ddlRate" Width="150px" CssClass=" form-control select2" runat="server" ValidationGroup="aaa"></asp:DropDownList>
                                                                    <asp:Label ID="lblRateEdit" runat="server" Enabled="false" Text='<%# Bind("Rate") %>'></asp:Label>
                                                                </EditItemTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblRate" runat="server" Width="50px" Text='<%# Bind("Rate") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                
                                                            </asp:TemplateField>


                                                            <asp:TemplateField HeaderText="Value" SortExpression="Project">
                                                                <EditItemTemplate>
                                                                    <asp:TextBox ID="txtValueEdit" runat="server" Text='<%# Bind("Value") %>'></asp:TextBox>
                                                                    <asp:Label ID="lblValueEdit" runat="server" Enabled="false" Text='<%# Bind("Value") %>'></asp:Label>
                                                                </EditItemTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblValue" runat="server" Text='<%# Bind("Value") %>'></asp:Label>

                                                                </ItemTemplate>
                                                               
                                                            </asp:TemplateField>--%>


                                                <asp:TemplateField HeaderText="Cost Center" SortExpression="Project">
                                                    <EditItemTemplate>
                                                        <%--<asp:TextBox ID="txtCostCenterEdit" runat="server" Text='<%# Bind("CostCenterName") %>'></asp:TextBox>--%>
                                                        <asp:Label ID="lblCostCenterEdit" runat="server" Enabled="false" Text='<%# Bind("CostCenterName") %>'></asp:Label>
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCostCenter" runat="server" Text='<%# Bind("CostCenterName") %>'></asp:Label>
                                                    </ItemTemplate>

                                                </asp:TemplateField>




                                                <asp:TemplateField HeaderText="Location" SortExpression="Project">
                                                    <EditItemTemplate>
                                                        <%--<asp:TextBox ID="txtLocationEdit" runat="server" Text='<%# Bind("LocName") %>'></asp:TextBox>--%>
                                                        <asp:Label ID="lblLocationEdit" runat="server" Enabled="false" Text='<%# Bind("LocName") %>'></asp:Label>
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblLocation" runat="server" Text='<%# Bind("LocName") %>'></asp:Label>
                                                    </ItemTemplate>

                                                </asp:TemplateField>



                                                <asp:TemplateField HeaderText="Project/Job/Batch No." SortExpression="Project">
                                                    <EditItemTemplate>
                                                        <%--<asp:TextBox ID="txtProjectEdit" runat="server" Text='<%# Bind("Project") %>'></asp:TextBox>--%>
                                                        <asp:Label ID="lblProjectEdit" runat="server" Enabled="false" Text='<%# Bind("Project") %>'></asp:Label>
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblProject" runat="server" Text='<%# Bind("Project") %>'></asp:Label>
                                                    </ItemTemplate>

                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Warrantee/Expiry Date" SortExpression="Project">
                                                    <EditItemTemplate>
                                                        <%--<asp:TextBox ID="txtWarranteeEdit" runat="server" Text='<%# Bind("Warrentee") %>'></asp:TextBox>--%>
                                                        <asp:Label ID="lblWarranteeEdit" runat="server" Enabled="false" Text='<%# Bind("Warrentee") %>'></asp:Label>
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblWarrantee" runat="server" Text='<%# Bind("Warrentee") %>'></asp:Label>
                                                    </ItemTemplate>

                                                </asp:TemplateField>



                                                <asp:TemplateField HeaderText="UOM2" SortExpression="Project">
                                                    <EditItemTemplate>
                                                        <%--<asp:TextBox ID="txtUOM2Edit" runat="server" Text='<%# Bind("uomName2") %>'></asp:TextBox>--%>
                                                        <asp:Label ID="lblUOM2Edit" runat="server" Enabled="false" Text='<%# Bind("uomName2") %>'></asp:Label>
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblUOM2" runat="server" Text='<%# Bind("uomName2") %>'></asp:Label>
                                                    </ItemTemplate>

                                                </asp:TemplateField>



                                                <asp:TemplateField HeaderText="Qty2" SortExpression="Project">
                                                    <EditItemTemplate>
                                                        <%--<asp:TextBox ID="txtQty2Edit" runat="server" Text='<%# Bind("Qty2") %>'></asp:TextBox>--%>
                                                        <asp:Label ID="lblQty2Edit" runat="server" Enabled="false" Text='<%# Bind("Qty2") %>'></asp:Label>
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblQty2" runat="server" Text='<%# Bind("Qty2") %>'></asp:Label>
                                                    </ItemTemplate>

                                                </asp:TemplateField>



                                                <asp:TemplateField HeaderText="Rate2" SortExpression="Project">
                                                    <EditItemTemplate>
                                                        <%--<asp:TextBox ID="txtRate2Edit" runat="server" Text='<%# Bind("Rate2") %>'></asp:TextBox>--%>
                                                        <asp:Label ID="lblRate2Edit" runat="server" Enabled="false" Text='<%# Bind("Rate2") %>'></asp:Label>
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblRate2" runat="server" Text='<%# Bind("Rate2") %>'></asp:Label>
                                                    </ItemTemplate>

                                                </asp:TemplateField>

                                                <asp:TemplateField>

                                                    <ItemTemplate>

                                                        <asp:ImageButton ID="editbtn" ImageUrl="~/Images123/edit.png" runat="server" CommandName="Edit" ToolTip="Edit" Width="20px" Height="20px" />
                                                        <asp:ImageButton ID="ImageButton8" ImageUrl="~/Images123/delete.png" runat="server" CommandName="Delete" ToolTip="Delete" Width="20px" Height="20px" />



                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:ImageButton ImageUrl="~/Images123/save.png" runat="server" CommandName="Update" ToolTip="Update" Width="20px" Height="20px" />
                                                        <asp:ImageButton ImageUrl="~/Images123/cancel.png" runat="server" CommandName="Cancel" ToolTip="Cancel" Width="20px" Height="20px"></asp:ImageButton>

                                                    </EditItemTemplate>

                                                </asp:TemplateField>





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


                <%-- Update Gridview For direct --%>

                <div id="Div7" runat="server">
                    <div class="box">



                        <div class="box-body" style="width: 100%; overflow: auto; height: 50%;">
                            <%-- CssClass="table table-bordered table-hover"--%>

                            <div style="width: 100%; height: 250px; overflow: scroll">

                                <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
                                    <Triggers>
                                        <asp:PostBackTrigger ControlID="GridView10" />
                                    </Triggers>
                                    <ContentTemplate>


                                        <asp:GridView ID="GridView10" AutoGenerateColumns="False" CssClass="table table-bordered table-hover tableFixHead" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Horizontal" runat="server" DataKeyNames="Sno" OnRowCancelingEdit="GridView10_RowCancelingEdit" OnRowEditing="GridView10_RowEditing" OnRowUpdating="GridView10_RowUpdating" OnRowDeleting="GridView10_RowDeleting">
                                            <Columns>


                                                <asp:TemplateField HeaderText="Sno" Visible="false" ItemStyle-Width="50">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblRowNumber" runat="server" />
                                                    </ItemTemplate>

                                                    <ItemStyle Width="100px"></ItemStyle>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="SNODetail" Visible="false" SortExpression="A/c code">
                                                    <EditItemTemplate>
                                                        <asp:DropDownList ID="SNODetail" Width="150px" CssClass=" form-control select2" runat="server" ValidationGroup="aaa"></asp:DropDownList>
                                                        <%--<asp:Label ID="lblAcctCode" runat="server" Enabled="false" Text='<%# Bind("Accode") %>'></asp:Label>--%>
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSno" runat="server" Text='<%# Bind("Sno") %>'></asp:Label>
                                                    </ItemTemplate>

                                                </asp:TemplateField>



                                                <asp:TemplateField HeaderText="A/c code" Visible="true" SortExpression="A/c code">
                                                    <EditItemTemplate>
                                                        <%--<asp:DropDownList ID="ddlAccode" Width="150px" CssClass=" form-control select2" runat="server" ValidationGroup="aaa"></asp:DropDownList>--%>
                                                        <asp:Label ID="lblAcctCode" runat="server" Enabled="false" Text='<%# Bind("DrAccountCode") %>'></asp:Label>
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblAccountCode" runat="server" Text='<%# Bind("DrAccountCode") %>'></asp:Label>
                                                    </ItemTemplate>

                                                </asp:TemplateField>


                                                <asp:TemplateField HeaderText="PO No." Visible="false" SortExpression="ItemSubHeadName">
                                                    <EditItemTemplate>
                                                        <asp:Label ID="lblPoNEdit" runat="server" Enabled="false" ></asp:Label>
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPONo" runat="server"></asp:Label>
                                                    </ItemTemplate>

                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Item Sub Head" SortExpression="Description">

                                                    <ItemTemplate>
                                                        <asp:Label ID="lblItemSubHead" runat="server" Text='<%# Bind("ItemSubHeadName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>

                                                        <%--<asp:DropDownList ID="ddlItemSubHeadEdit" Width="150px" CssClass=" form-control select2" runat="server" ValidationGroup="aaa"></asp:DropDownList>--%>
                                                        <asp:Label ID="lblItemSubHead" runat="server" Enabled="true" Text='<%# Bind("ItemSubHeadName") %>'></asp:Label>
                                                    </EditItemTemplate>

                                                </asp:TemplateField>




                                                <asp:TemplateField HeaderText="Item Master" SortExpression="UOM">
                                                    <EditItemTemplate>
                                                        <%--<asp:DropDownList ID="ddlItemMasterEdit" Width="150px" CssClass=" form-control select2" runat="server" ValidationGroup="aaa"></asp:DropDownList>--%>
                                                        <asp:Label ID="lblItemMaster" runat="server" Enabled="false" Text='<%# Bind("Description") %>'></asp:Label>
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblItemMaster" runat="server" Text='<%# Bind("Description") %>'></asp:Label>
                                                    </ItemTemplate>

                                                </asp:TemplateField>




                                                <asp:TemplateField HeaderText="UOM" SortExpression="CurrentStock">
                                                    <EditItemTemplate>
                                                        <%--<asp:DropDownList ID="ddlUOMEdit" Width="70px" CssClass=" form-control select2" runat="server" ValidationGroup="aaa"></asp:DropDownList>--%>
                                                        <asp:Label ID="lblUOM" runat="server" Width="100px" Text='<%# Bind("UOMName") %>'></asp:Label>
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblUOM" runat="server" Width="100px" Text='<%# Bind("UOMName") %>'></asp:Label>
                                                    </ItemTemplate>

                                                </asp:TemplateField>


                                                <asp:TemplateField HeaderText="Qty" Visible="false" SortExpression="RequiredQuantity" >
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txtPObalQty" runat="server" Width="80px" Text='<%# Bind("Quantity") %>'></asp:TextBox>
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPObalQty" runat="server" Width="100px" Text='<%# Bind("Quantity") %>'></asp:Label>
                                                    </ItemTemplate>

                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Qty" SortExpression="RequiredQuantity" Visible="true" ItemStyle-HorizontalAlign="Right">
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txtDCQty" runat="server" Width="100px" Text='<%# Bind("Quantity") %>' OnTextChanged="txtDCQty_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDCQty" runat="server" Width="100px" Text='<%# Bind("Quantity") %>'></asp:Label>
                                                    </ItemTemplate>

                                                </asp:TemplateField>



                                                <asp:TemplateField HeaderText="Rate" SortExpression="CostCenter" Visible="false">

                                                    <EditItemTemplate>

                                                        <asp:TextBox ID="lblRateEdit" runat="server" Enabled="false" Width="150px" Text='<%# Bind("Rate","{0:0.0000}") %>' OnTextChanged="lblRateEdit_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblRate" runat="server" Width="50px" Text='<%# Bind("Rate","{0:0.0000}") %>'></asp:Label>
                                                    </ItemTemplate>

                                                </asp:TemplateField>


                                                <asp:TemplateField HeaderText="Value" SortExpression="Project" Visible="false">
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txtValueEdit" runat="server" Enabled="false" Text='<%# Bind("Value") %>' Width="150px"></asp:TextBox>
                                                        <asp:Label ID="lblValueEdit" runat="server" Visible="false" Enabled="false" Text='<%# Bind("Value") %>'></asp:Label>
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblValue" runat="server" Text='<%# Bind("Value") %>'></asp:Label>

                                                    </ItemTemplate>

                                                </asp:TemplateField>


                                                <asp:TemplateField HeaderText="Cost Center" SortExpression="Project">
                                                    <EditItemTemplate>
                                                        <%--<asp:DropDownList ID="ddlCostCenterEdit" Width="150px" CssClass=" form-control select2" runat="server" ValidationGroup="aaa"></asp:DropDownList>--%>
                                                        <asp:Label ID="lblCostCenterEdit" runat="server" Visible="true" Enabled="false" Text='<%# Bind("CostCenterName") %>'></asp:Label>
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCostCenter" runat="server" Text='<%# Bind("CostCenterName") %>'></asp:Label>
                                                    </ItemTemplate>

                                                </asp:TemplateField>




                                                <asp:TemplateField HeaderText="Location" SortExpression="Project" Visible="false">
                                                    <EditItemTemplate>
                                                        <%--<asp:DropDownList ID="ddlLocationEdit" Width="150px" CssClass=" form-control select2" runat="server" ValidationGroup="aaa"></asp:DropDownList>--%>
                                                        <%--<asp:TextBox ID="txtLocationEdit" runat="server" Enabled="false" Text='<%# Bind("LocName") %>' Width="80px"></asp:TextBox>--%>
                                                        <asp:Label ID="lblLocationEdit" runat="server" Visible="true" Enabled="false" Text='<%# Bind("LocName") %>'></asp:Label>
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblLocation" runat="server" Text='<%# Bind("LocName") %>'></asp:Label>
                                                    </ItemTemplate>

                                                </asp:TemplateField>



                                                <asp:TemplateField HeaderText="Project/Job/Batch No." SortExpression="Project">
                                                    <EditItemTemplate>
                                                        <%--<asp:TextBox ID="txtProjectEdit" runat="server" Text='<%# Bind("Project") %>' Width="80px"></asp:TextBox>--%>
                                                        <asp:Label ID="lblProjectEdit" runat="server" Enabled="false" Visible="true" Text='<%# Bind("Project") %>'></asp:Label>
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblProject" runat="server" Text='<%# Bind("Project") %>'></asp:Label>
                                                    </ItemTemplate>

                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Warrantee/Expiry Date" SortExpression="Project">
                                                    <EditItemTemplate>
                                                        <%--<asp:TextBox ID="txtWarranteeEdit" runat="server" Width="100px" Text='<%# Bind("Warrentee","{0:dd-MMM-yyyy}") %>'></asp:TextBox>--%>
                                                        <asp:Label ID="lblWarranteeEdit" runat="server" Enabled="false" Visible="true" Text='<%# Bind("Warrentee","{0:dd-MMM-yyyy}") %>'></asp:Label>
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblWarrantee" runat="server" Text='<%# Bind("Warrentee","{0:dd-MMM-yyyy}") %>'></asp:Label>
                                                    </ItemTemplate>

                                                </asp:TemplateField>



                                                <asp:TemplateField HeaderText="UOM2" SortExpression="Project" Visible="false">
                                                    <EditItemTemplate>

                                                        <%--<asp:DropDownList ID="ddlUOM2Edit" Width="70px" CssClass=" form-control select2" runat="server" ValidationGroup="aaa"></asp:DropDownList>--%>
                                                        <asp:Label ID="lblUOM2Edit" runat="server" Enabled="false" Text='<%# Bind("uomName2") %>'></asp:Label>
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblUOM2" runat="server" Text='<%# Bind("uomName2") %>'></asp:Label>
                                                    </ItemTemplate>

                                                </asp:TemplateField>



                                                <asp:TemplateField HeaderText="Qty2" SortExpression="Project" Visible="false">
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txtQty2Edit" runat="server" Text='<%# Bind("Qty2") %>' Width="80px" AutoPostBack="true" OnTextChanged="txtQty2Edit_TextChanged"></asp:TextBox>
                                                        <asp:Label ID="lblQty2Edit" runat="server" Enabled="false" Visible="false" Text='<%# Bind("Qty2") %>'></asp:Label>
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblQty2" runat="server" Text='<%# Bind("Qty2") %>'></asp:Label>
                                                    </ItemTemplate>

                                                </asp:TemplateField>



                                                <asp:TemplateField HeaderText="Rate2" SortExpression="Project" Visible="false">
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txtRate2Edit" runat="server" Text='<%# Bind("Rate2","{0:0.0000}") %>' Width="80px"></asp:TextBox>
                                                        <asp:Label ID="lblRate2Edit" runat="server" Enabled="false" Visible="false" Text='<%# Bind("Rate2","{0:0.0000}") %>'></asp:Label>
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblRate2" runat="server" Text='<%# Bind("Rate2","{0:0.0000}") %>'></asp:Label>
                                                    </ItemTemplate>

                                                </asp:TemplateField>


                                                <asp:TemplateField>

                                                    <ItemTemplate>

                                                        <asp:ImageButton ID="editbtn" ImageUrl="~/Images123/edit.png" runat="server" CommandName="Edit" ToolTip="Edit" Width="20px" Height="20px" />
                                                        <asp:ImageButton ID="ImageButton8" ImageUrl="~/Images123/delete.png" runat="server" CommandName="Delete" ToolTip="Delete" Width="20px" Height="20px" />



                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:ImageButton ImageUrl="~/Images123/save.png" runat="server" CommandName="Update" ToolTip="Update" Width="20px" Height="20px" />
                                                        <asp:ImageButton ImageUrl="~/Images123/cancel.png" runat="server" CommandName="Cancel" ToolTip="Cancel" Width="20px" Height="20px"></asp:ImageButton>

                                                    </EditItemTemplate>

                                                </asp:TemplateField>






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

                <%-- Update Gridview For direct --%>

             
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

