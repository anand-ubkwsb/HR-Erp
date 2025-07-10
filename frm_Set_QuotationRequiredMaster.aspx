<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="frm_Set_QuotationRequiredMaster.aspx.cs" Inherits="frm_Period"  EnableEventValidation="false"%>

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
                                <h3 class="box-title" style="color: #fff; padding-left: 10px;"><strong>Ask For Quotation</strong></h3>
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
                        </div>

                    </div>


                    <div class="form-row">

                        <div class="box-body" style="background-color: #cddbf2;">
                            <div id="fieldbox" runat="server">

                                <table id="tablecontent1" runat="server" style="width: 100%;">
                                    <tr>
                                        <td class="auto-style6"><strong>Document Name</strong></td>
                                        <td class="auto-style11">

                                            <asp:DropDownList ID="ddlDocName" Width="250px" CssClass=" form-control select2" runat="server" ValidationGroup="aaa" AutoPostBack="True" OnSelectedIndexChanged="ddlDocTypes_SelectedIndexChanged" ></asp:DropDownList>
                                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" ControlToValidate="ddlDocName" InitialValue="Please select..." Display="None" ErrorMessage="<b> Missing Field</b><br />Please Select Document." ValidationGroup="aaa" />
                                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" TargetControlID="RequiredFieldValidator1" runat="server" HighlightCssClass="vali" PopupPosition="Right"></ajaxToolkit:ValidatorCalloutExtender>
                                        </td>
                                         <td class="auto-style6"><strong>Entry Date </strong></td>
                                        <td class="auto-style11">

                                            <div style="display: inline-block">

                                                <div class="input-group input-group-sm">
                                                    <asp:TextBox ID="txtReqDate" CssClass="form-control texthieht" Width="220px" runat="server" Font-Bold="False" Font-Italic="False" ForeColor="Black" ValidationGroup="aaa" AutoPostBack="True" OnTextChanged="txtReqDate_TextChanged"></asp:TextBox>
                                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender1" PopupButtonID="imgPopup" TargetControlID="txtReqDate" Format="dd-MMM-yyyy" runat="server" />
                                                    <asp:RequiredFieldValidator runat="server" ID="Req2" ControlToValidate="txtReqDate" Display="None" ErrorMessage="<b> Missing Field</b><br />A Date is required." ValidationGroup="aaa" />
                                                    <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender11" TargetControlID="Req2" runat="server" HighlightCssClass="vali"></ajaxToolkit:ValidatorCalloutExtender>
                                                    <span class="input-group-btn">
                                                        <%--<button type="button" class="btn btn-info btn-flat">Go!</button>--%>
                                                        <asp:ImageButton ID="imgPopup" CssClass="btn btn-flat" ImageUrl="img/calendar.png" ImageAlign="Bottom" runat="server" />
                                                    </span>
                                                </div>

                                            </div>

                                        </td>
                                         <td class="auto-style6"  visible="false"><strong>Document Type</strong></td>
                                        <td class="auto-style11" visible="false">
                                            
                                            <asp:DropDownList ID="ddlDocTypes" Width="250px" CssClass=" form-control select2" runat="server" ValidationGroup="aaa" AutoPostBack="True" OnSelectedIndexChanged="ddlDocTypes_SelectedIndexChanged" ></asp:DropDownList>
                                            
                                            <asp:Panel ID="Panel2" runat="server" Visible="false"> 
                                                <asp:RadioButton ID="rdbDepartment" Text="Department" runat="server"  GroupName="Group_L_I" CssClass="rdbgap" OnCheckedChanged="rdbDepartment_CheckedChanged" AutoPostBack="True" />
                                                <asp:RadioButton ID="rdbPurchase" Text="Purchase" Checked="true" runat="server" GroupName="Group_L_I" OnCheckedChanged="rdbPurchase_CheckedChanged" AutoPostBack="True" />
                                            </asp:Panel>

                                        </td>
                                      

                                    </tr>
                                    <tr>

                                       
                                         <td class="auto-style6"><strong>Document No</strong></td>
                                        <td class="auto-style11">

                                            <asp:TextBox ID="txtReqQNo" CssClass="texthieht" Width="250px" runat="server" Font-Bold="False" Font-Italic="False" ForeColor="Black" Height="25px" ValidationGroup="aaa"></asp:TextBox>
                                            <asp:Label ID="lbldocno" runat="server" Text=""></asp:Label>
                                          
                                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator3" ControlToValidate="txtReqQNo"  Display="None" ErrorMessage="<b> Missing Field</b><br />Please Select Document." ValidationGroup="aaa" />
                                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender4" TargetControlID="RequiredFieldValidator3" runat="server" HighlightCssClass="vali" PopupPosition="Right"></ajaxToolkit:ValidatorCalloutExtender>

                                        </td>
                                        <td class="auto-style6"><strong>Document Authority</strong></td>
                                        <td class="auto-style11">
                                            
                                           
                                            <asp:DropDownList ID="ddlReqStatus" Width="250px" CssClass=" form-control select2" runat="server" ValidationGroup="aaa">
                                            </asp:DropDownList>
                                            
                                             <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator4" ControlToValidate="ddlReqStatus" Display="None" ErrorMessage="<b> Missing Field</b><br />Please input document name." ValidationGroup="aaa" />
                                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender6" TargetControlID="RequiredFieldValidator4" runat="server" HighlightCssClass="vali"></ajaxToolkit:ValidatorCalloutExtender>

                                        </td>
                                       
                                    </tr>
                                    <tr>
                                          <td class="auto-style6"><strong>Supplier</strong></td>
                                        <td class="auto-style11">
                                            
                                           
                                            <asp:DropDownList ID="ddlSupplier" Width="250px" CssClass=" form-control select2" runat="server" ValidationGroup="aaa">
                                            </asp:DropDownList>
                                            
                                             <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator6" ControlToValidate="ddlSupplier" Display="None" InitialValue="Please select..." ErrorMessage="<b> Missing Field</b><br />Please Select Supplier name." ValidationGroup="aaa" />
                                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" TargetControlID="RequiredFieldValidator6" runat="server" HighlightCssClass="vali"></ajaxToolkit:ValidatorCalloutExtender>

                                        </td>
                                        <td class="auto-style6"><strong>Priority</strong></td>
                                        <td class="auto-style11">
                                            <asp:DropDownList ID="ddlPriority" Width="250px" CssClass=" form-control select2" runat="server" ValidationGroup="aaa">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator2" ControlToValidate="ddlPriority" InitialValue="Please select..." Display="None" ErrorMessage="<b> Missing Field</b><br />Please Select Document." ValidationGroup="aaa" />
                                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" TargetControlID="RequiredFieldValidator2" runat="server" HighlightCssClass="vali" PopupPosition="Right"></ajaxToolkit:ValidatorCalloutExtender>


                                        </td>
                                    </tr>
                                    <tr>
                                       
                                        <td class="auto-style6"><strong>For Store/Department </strong></td>
                                        <td class="auto-style11">
                                            <asp:DropDownList ID="ddlFromStroeDept" Width="250px" CssClass=" form-control select2" runat="server" ValidationGroup="aaa"></asp:DropDownList>
                                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator10" ControlToValidate="ddlFromStroeDept" InitialValue="Please select..." Display="None" ErrorMessage="<b> Missing Field</b><br />Please Select Document." ValidationGroup="aaa" />
                                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender13" TargetControlID="RequiredFieldValidator3" runat="server" HighlightCssClass="vali" PopupPosition="Right"></ajaxToolkit:ValidatorCalloutExtender>

                                            <%--<asp:TextBox ID="txtProject" CssClass="texthieht" Width="250px" runat="server" Font-Bold="False" Font-Italic="False" ForeColor="Black" Height="25px" ValidationGroup="aaa"></asp:TextBox>
                                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator7" ControlToValidate="txtProject" Display="None" ErrorMessage="<b> Missing Field</b><br />Please input document name." ValidationGroup="aaa" />
                                                    <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender9" TargetControlID="RequiredFieldValidator7" runat="server" HighlightCssClass="vali"></ajaxToolkit:ValidatorCalloutExtender>--%>
                                                     
                                        </td>
                                         <td class="auto-style6"><strong>Quotation Submission Date</strong></td>
                                        <td class="auto-style11">
                                            <div style="display: inline-block">

                                                <div class="input-group input-group-sm">
                                                    <asp:TextBox ID="txtRequirmentDueDate" CssClass="form-control texthieht" Width="220px" runat="server" Font-Bold="False" Font-Italic="False" ForeColor="Black" ValidationGroup="aaa"></asp:TextBox>
                                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender2" PopupButtonID="ImageButton1" TargetControlID="txtRequirmentDueDate" Format="dd-MMM-yyyy" runat="server" />
                                                    <%--<asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator9" ControlToValidate="txtRequirmentDueDate" Display="None" ErrorMessage="<b> Missing Field</b><br />A Date is required." ValidationGroup="aaa" />
                                                    <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender12" TargetControlID="RequiredFieldValidator9" runat="server" HighlightCssClass="vali"></ajaxToolkit:ValidatorCalloutExtender>--%>
                                                    <span class="input-group-btn">
                                                        <%--<button type="button" class="btn btn-info btn-flat">Go!</button>--%>
                                                        <asp:ImageButton ID="ImageButton1" CssClass="btn btn-flat" ImageUrl="img/calendar.png" ImageAlign="Bottom" runat="server" />
                                                    </span>
                                                </div>

                                            </div>

                                        </td>

                                    </tr>
                                    <tr>
                                        <td class="auto-style6"><strong>Location</strong></td>
                                        <td class="auto-style11">
                                             <asp:DropDownList ID="ddllocation" Width="250px" CssClass=" form-control select2" runat="server" ValidationGroup="aaa">
                                            </asp:DropDownList>
                                            
                                          


                                        </td>
                                        <%--<td class="auto-style6"><strong>Copy Requisition No.</strong></td>
                                        <td class="auto-style11">
                                            <asp:TextBox ID="txtCopyReqNo" CssClass="texthieht" Width="250px" runat="server" Font-Bold="False" Font-Italic="False" ForeColor="Black" Height="25px" ValidationGroup="aaa"></asp:TextBox>
                                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator6" ControlToValidate="txtCopyReqNo" Display="None" ErrorMessage="<b> Missing Field</b><br />Please input document name." ValidationGroup="aaa" />
                                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender8" TargetControlID="RequiredFieldValidator6" runat="server" HighlightCssClass="vali"></ajaxToolkit:ValidatorCalloutExtender>

                                        </td>--%>
                                        <td class="auto-style6"><strong>Stock Requisition No.</strong></td>
                                        <td class="auto-style11">
                                            <%-- <asp:DropDownList ID="dddlReqNo" Width="250px" CssClass=" form-control select2" runat="server" ValidationGroup="aaa" OnSelectedIndexChanged="dddlReqNo_SelectedIndexChanged" AutoPostBack="true" ></asp:DropDownList>
                                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator7" ControlToValidate="dddlReqNo" InitialValue="Please select..." Display="None" ErrorMessage="<b> Missing Field</b><br />Please Select Document." ValidationGroup="aaa" />
                                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender5" TargetControlID="RequiredFieldValidator7" runat="server" HighlightCssClass="vali" PopupPosition="Right"></ajaxToolkit:ValidatorCalloutExtender>--%>
                                            <div>
                                                <asp:ListBox ID="lstFruits" runat="server" SelectionMode="Multiple" OnSelectedIndexChanged="lstFruits_SelectedIndexChanged" AutoPostBack="True" DataSourceID="SqlDataSource1" Width="250px" DataTextField="RequisitionNo" DataValueField="Sno"></asp:ListBox>
                                                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:db_HRERPSys %>" SelectCommand="SELECT [RequisitionNo], [Sno] FROM [SET_StockRequisitionMaster] where [Status]= '1' AND [RequisitionType] = 'P' AND [Record_Deleted] = '0' AND [IsActive] = '1'"></asp:SqlDataSource>
                                               
                                            </div>
                                        </td>

                                    </tr>

                                    <tr>

                                        <td class="auto-style6"><strong>Remarks</strong></td>
                                        <td class="auto-style11">

                                            <asp:TextBox ID="txtRemarks" CssClass="texthieht" Width="250px" runat="server" Font-Bold="False" Font-Italic="False" ForeColor="Black" Height="25px" ValidationGroup="aaa"></asp:TextBox>
                                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator8" ControlToValidate="txtRemarks" Display="None" ErrorMessage="<b> Missing Field</b><br />Please input document name." ValidationGroup="aaa" />
                                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender10" TargetControlID="RequiredFieldValidator8" runat="server" HighlightCssClass="vali"></ajaxToolkit:ValidatorCalloutExtender>

                                        </td>
                                         <td class="auto-style6"><strong>Status</strong></td>
                                        <td class="auto-style11">
                                            
                                           
                                            <asp:DropDownList ID="ddlStatus" Width="250px" CssClass=" form-control select2" runat="server" ValidationGroup="aaa">
                                                
                                            </asp:DropDownList>
                                            
                                             <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator7" ControlToValidate="ddlReqStatus" Display="None" ErrorMessage="<b> Missing Field</b><br />Please input document name." ValidationGroup="aaa" />
                                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender5" TargetControlID="RequiredFieldValidator4" runat="server" HighlightCssClass="vali"></ajaxToolkit:ValidatorCalloutExtender>

                                        </td>
                                        
                                    </tr>

                                    <tr>

                                        <td class="auto-style6"><strong>Created By</strong></td>
                                        <td class="auto-style11">
                                            <asp:TextBox ID="txtCreatedby" CssClass="texthieht" Width="250px" runat="server" Font-Bold="False" Font-Italic="False" ForeColor="Black" Height="25px"></asp:TextBox>
                                        </td>
                                        <td class="auto-style6"><strong>Created Date</strong></td>
                                        <td class="auto-style11">
                                            <asp:TextBox ID="txtCreatedDate" CssClass="texthieht" Width="250px" runat="server" Font-Bold="False" Font-Italic="False" ForeColor="Black" Height="25px"></asp:TextBox>

                                        </td>
                                    </tr>

                                    <tr id="updatecol" runat="server">
                                        <td class="auto-style6"><strong>Updated By</strong></td>
                                        <td class="auto-style11">
                                            <asp:TextBox ID="txtUpdateBy" CssClass="texthieht" Width="250px" runat="server" Font-Bold="False" Font-Italic="False" ForeColor="Black" Height="25px"></asp:TextBox>
                                        </td>
                                        <td class="auto-style6"><strong>Updated Date</strong></td>
                                        <td class="auto-style11">
                                            <asp:TextBox ID="txtUpdateDate" CssClass="texthieht" Width="250px" runat="server" Font-Bold="False" Font-Italic="False" ForeColor="Black" Height="25px"></asp:TextBox>

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
                                                     <asp:BoundField DataField="DocDescription" HeaderText="DocDescription" SortExpression="DocDescription">
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="DocName" HeaderText="DocName" SortExpression="DocName">
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="RequisitionType" HeaderText="RequisitionType" SortExpression="RequisitionType">
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="StatusName" HeaderText="Status" SortExpression="StatusName" />
                                                    <asp:BoundField DataField="RequisitionNo" HeaderText="RequisitionNo" SortExpression="RequisitionNo" />
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
                                                     <asp:BoundField DataField="DocDescription" HeaderText="DocDescription" SortExpression="DocDescription">
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="DocName" HeaderText="DocName" SortExpression="DocName">
                                                    </asp:BoundField>
                                                   
                                                    <asp:BoundField DataField="RequisitionType" HeaderText="RequisitionType" SortExpression="RequisitionType">
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="StatusName" HeaderText="Status" SortExpression="StatusName" />
                                                    <asp:BoundField DataField="RequisitionNo" HeaderText="RequisitionNo" SortExpression="RequisitionNo" />
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
                                                    <asp:BoundField DataField="DocDescription" HeaderText="DocDescription" SortExpression="DocDescription">
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="DocName" HeaderText="DocName" SortExpression="DocName">
                                                    </asp:BoundField>
                                                    
                                                    <asp:BoundField DataField="RequisitionType" HeaderText="RequisitionType" SortExpression="RequisitionType">
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="StatusName" HeaderText="Status" SortExpression="StatusName" />
                                                    <asp:BoundField DataField="RequisitionNo" HeaderText="RequisitionNo" SortExpression="RequisitionNo" />
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
                                      
                                        <div style="width: 100%; height: 200px; overflow: scroll">

                                           

                                             
                                            <asp:GridView ID="GridView5" AutoGenerateColumns="False" CssClass="table table-bordered table-hover tableFixHead" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Horizontal" runat="server" DataKeyNames="Sno" OnRowEditing="GridView5_RowEditing" OnRowUpdating="GridView5_RowUpdating" OnRowCancelingEdit="GridView5_RowCancelingEdit" OnRowDataBound="GridView5_RowDataBound">
                                                <Columns>

                                                    <asp:CommandField ShowEditButton="True" ButtonType="Image" EditImageUrl="~/dist/img/edit.png" CancelImageUrl="~/dist/img/Cancel.png" UpdateImageUrl="~/dist/img/Update.png" />

                                                    <asp:TemplateField HeaderText="Sno" ItemStyle-Width="50">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblRowNumber" runat="server" />
                                                        </ItemTemplate>

                                                        <ItemStyle Width="100px"></ItemStyle>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="" Visible ="false" SortExpression="Sno">
                                                        <EditItemTemplate>
                                                            <asp:Label ID="lblSno" runat="server" Text='<%# Eval("Sno") %>'></asp:Label>
                                                             <asp:Label ID="lbldetailSno" runat="server" Text='<%# Bind("SnoDetail") %>' Visible="false"></asp:Label>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSno" runat="server" Text='<%# Bind("SnoDetail") %>'></asp:Label>
                                                             <asp:Label ID="lbldetailSno" runat="server" Text='<%# Bind("Sno") %>' Visible="false"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="RequisitionNo" SortExpression="RequisitionNo">
                                                        <EditItemTemplate>
                                                            <asp:Label ID="lblreq" runat="server" Text='<%# Bind("RequisitionNo") %>'></asp:Label>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblreq" runat="server" Text='<%# Bind("RequisitionNo") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="ItemSubHeadName" SortExpression="ItemSubHead">
                                                        <EditItemTemplate>
                                                            <asp:Label ID="lblsubhead" runat="server" Text='<%# Bind("ItemSubHeadName") %>'></asp:Label>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblsubheadid" Visible="false" runat="server" Text='<%# Bind("ItemSubHead") %>'></asp:Label>
                                                            <asp:Label ID="lblsubhead" runat="server" Text='<%# Bind("ItemSubHeadName") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="ItemMaster" SortExpression="ItemMaster">
                                                        <EditItemTemplate>
                                                            <asp:Label ID="lblitemmaster" runat="server" Text='<%# Bind("Description") %>'></asp:Label>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            
                                                            <asp:Label ID="lblitemmasterid" Visible="false" runat="server" Text='<%# Bind("ItemMaster") %>'></asp:Label>
                                                            <asp:Label ID="lblitemmaster" runat="server" Text='<%# Bind("Description") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="UOM" SortExpression="UOM">
                                                        <EditItemTemplate>
                                                            <asp:Label ID="lbluom" runat="server" Text='<%# Bind("UOMName") %>'></asp:Label>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbluomid" Visible="false" runat="server" Text='<%# Bind("UOM") %>'></asp:Label>
                                                            <asp:Label ID="lbluom" runat="server" Text='<%# Bind("UOMName") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="CurrentStock" SortExpression="CurrentStock">
                                                        <EditItemTemplate>
                                                            <asp:TextBox ID="txtCss" runat="server" Text='<%# Bind("CurrentStock") %>'></asp:TextBox>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblStockQ" runat="server" Text='<%# Bind("CurrentStock") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="RequiredQuantity" SortExpression="RequiredQuantity">
                                                        <EditItemTemplate>
                                                            <asp:TextBox ID="txtReqQ" runat="server" Text='<%# Bind("RequiredQuantity") %>'></asp:TextBox>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblreqQ" runat="server" Text='<%# Bind("RequiredQuantity") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                 
                                                    <%-- listView coloum --%>
                                                    <asp:TemplateField HeaderText="Supplier" SortExpression="RequiredQuantity" Visible ="false">
                                                        <EditItemTemplate>
                                                            <asp:Label ID="lblsupp" runat="server" Text='<%# Bind("Supplier") %>'></asp:Label>

                                                            <asp:ListBox ID="lstboxedit" runat="server" Visible="false" Width="150px" DataTextField="BPartnerName" DataValueField="BPartnerName" SelectionMode="Single" Height="60px"></asp:ListBox>

                                                            <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Select" Text="ADD" CssClass="btn"></asp:LinkButton>

                                                            <ajaxToolkit:ModalPopupExtender ID="mp1" runat="server" PopupControlID="Panel1" TargetControlID="LinkButton1"
                                                                CancelControlID="btnClose" BackgroundCssClass="modalBackground">
                                                            </ajaxToolkit:ModalPopupExtender>
                                                            <asp:Panel ID="Panel1" runat="server" CssClass="modalPopup" align="center" Style="display: none">
                                                                <div style="height: 60px">
                                                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                                                        <Triggers>
                                                                            <asp:PostBackTrigger ControlID="DropDownList112" />
                                                                        </Triggers>
                                                                        <ContentTemplate>
                                                                            Supplier&nbsp;
                                                                           <asp:DropDownList ID="DropDownList112" runat="server" AutoPostBack="true" OnSelectedIndexChanged="OnSelectedIndexChanged">
                                                                           </asp:DropDownList>

                                                                        </ContentTemplate>
                                                                    </asp:UpdatePanel>
                                                                </div>
                                                                <asp:Button ID="btnClose" runat="server" Text="Close" />
                                                            </asp:Panel>

                                                            <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:db_HRERPSys %>" SelectCommand="select BPartnerName from SET_BusinessPartner WHERE BPartnerId in (select BPartnerId from SET_BPartnerType where BPNatureID = 37)"></asp:SqlDataSource>

                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                           
                                                            
                                                            <asp:Label ID="lblsupp" runat="server" Text='<%# Bind("Supplier") %>'></asp:Label>
                                                            
                                                            
                                                            
                                                             <asp:ListBox ID="ListBox1" runat="server" Visible="false" Width="150px" DataTextField="BPartnerName" DataValueField="BPartnerName" SelectionMode="Single" Height="60px"></asp:ListBox>

                                                            <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Select" Text="ADD" CssClass="btn"></asp:LinkButton>

                                                            <ajaxToolkit:ModalPopupExtender ID="mp1" runat="server" PopupControlID="Panel1" TargetControlID="LinkButton1"
                                                                CancelControlID="btnClose" BackgroundCssClass="modalBackground">
                                                            </ajaxToolkit:ModalPopupExtender>
                                                            <asp:Panel ID="Panel1" runat="server" CssClass="modalPopup" align="center" Style="display: none">
                                                                <div style="height: 60px">
                                                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                                                        <Triggers>
                                                                            <asp:PostBackTrigger ControlID="DropDownList71" />
                                                                        </Triggers>
                                                                        <ContentTemplate>
                                                                            Supplier&nbsp;
                                                                    <asp:DropDownList ID="DropDownList71" runat="server" AutoPostBack="true" OnSelectedIndexChanged="OnSelectedIndexChanged">
                                                                        </asp:DropDownList>

                                                                        </ContentTemplate>
                                                                    </asp:UpdatePanel>
                                                                </div>
                                                                <asp:Button ID="btnClose" runat="server" Text="Close" />
                                                            </asp:Panel>

                                                            <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:db_HRERPSys %>" SelectCommand="select BPartnerName from SET_BusinessPartner WHERE BPartnerId in (select BPartnerId from SET_BPartnerType where BPNatureID = 37)"></asp:SqlDataSource>

                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                     <asp:TemplateField HeaderText="" >
                                                         <HeaderTemplate>
                                                              <asp:CheckBox ID="chkall" runat="server" AutoPostBack="True" OnCheckedChanged="chkall_CheckedChanged" Text="All" Width="83px" />
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
                                                <HeaderStyle BackColor="#000080" Font-Bold="True" ForeColor="White" Height="5px"/>
                                                <PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="Right" />
                                            </asp:GridView>
                                            



                                            <%--<asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:db_HRERPSys %>" SelectCommand="SELECT [Sno],[RequistionNo], [ItemSubHeadName], [Description], [UOM], [RequiredQuantity] FROM [VIEW_ASKFORQOUT]"></asp:SqlDataSource>--%>

                                           <%-- <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:db_HRERPSys %>" SelectCommand="SELECT [DocID], [DocDescription], [DocName], [IsActive], [IsHide] , [Main_Site], [ApplyDate] FROM [viewDOCUSER] order by DocDescription asc"></asp:SqlDataSource>--%>

                                        </div>

                                    </div>
                                    <!-- /.box-body -->
                                </div>
                                <div style="float:right;">
                     <asp:Button ID="Button1" runat="server" CssClass="btn btn-primary" Text="Save" Width="120px" Visible="false" OnClick="Button1_Click" />
                    <asp:Button ID="Button2" runat="server" CssClass="btn btn-default" Text="Cancel" Width="120px" Visible="false"   />
                    </div>
                            </div>
                
                    

                           <div id="Div2" runat="server">
                                <div class="box">

                                    <!-- /.box-header -->
                                    
                                    <div class="box-body" style="width: 100%; overflow: auto; height: 50%;">
                                       <%-- CssClass="table table-bordered table-hover"--%>
                                      
                                        <div style="width: 100%; height: 200px; overflow: scroll">

                                           

                                             
                                            <asp:GridView ID="GridView6" AutoGenerateColumns="False" CssClass="table table-bordered table-hover tableFixHead" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Horizontal" runat="server" DataKeyNames="Sno" OnRowDataBound="GridView6_RowDataBound">
                                                <Columns>

                                                    

                                                    <asp:TemplateField HeaderText="Sno" ItemStyle-Width="50">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblRowNumber" runat="server" />
                                                        </ItemTemplate>

                                                        <ItemStyle Width="100px"></ItemStyle>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="" SortExpression="Sno" Visible ="false">
                                                        <EditItemTemplate>
                                                            <asp:Label ID="lblSno" runat="server" Text='<%# Eval("Sno") %>'></asp:Label>
                                                             <asp:Label ID="lbldetailSno" runat="server" Text='<%# Bind("SnoDetail") %>' Visible="false"></asp:Label>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSno" runat="server" Text='<%# Bind("SnoDetail") %>'></asp:Label>
                                                             <asp:Label ID="lbldetailSno" runat="server" Text='<%# Bind("SnoDetail") %>' Visible="false"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="RequisitionNo" SortExpression="RequisitionNo">
                                                        <EditItemTemplate>
                                                            <asp:Label ID="lblreq" runat="server" Text='<%# Bind("reqNOdetail") %>'></asp:Label>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblreq" runat="server" Text='<%# Bind("reqNOdetail") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="ItemSubHeadName" SortExpression="ItemSubHead">
                                                        <EditItemTemplate>
                                                            <asp:Label ID="lblsubhead" runat="server" Text='<%# Bind("ItemSubHeadName") %>'></asp:Label>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblsubheadid" Visible="false" runat="server" Text='<%# Bind("ItemSubHead") %>'></asp:Label>
                                                            <asp:Label ID="lblsubhead" runat="server" Text='<%# Bind("ItemSubHeadName") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="ItemMaster" SortExpression="ItemMaster">
                                                        <EditItemTemplate>
                                                            <asp:Label ID="lblitemmaster" runat="server" Text='<%# Bind("Description") %>'></asp:Label>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            
                                                            <asp:Label ID="lblitemmasterid" Visible="false" runat="server" Text='<%# Bind("ItemMaster") %>'></asp:Label>
                                                            <asp:Label ID="lblitemmaster" runat="server" Text='<%# Bind("Description") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="UOM" SortExpression="UOM">
                                                        <EditItemTemplate>
                                                            <asp:Label ID="lbluom" runat="server" Text='<%# Bind("UOMName") %>'></asp:Label>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbluomid" Visible="false" runat="server" Text='<%# Bind("UOM") %>'></asp:Label>
                                                            <asp:Label ID="lbluom" runat="server" Text='<%# Bind("UOMName") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="CurrentStock" SortExpression="CurrentStock">
                                                        <EditItemTemplate>
                                                            <asp:TextBox ID="txtCss" runat="server" Text='<%# Bind("CurrentStock") %>'></asp:TextBox>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblStockQ" runat="server" Text='<%# Bind("CurrentStock") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="RequiredQuantity" SortExpression="RequiredQuantity">
                                                        <EditItemTemplate>
                                                            <asp:TextBox ID="txtReqQ" runat="server" Text='<%# Bind("RequiredQuantity") %>'></asp:TextBox>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblreqQ" runat="server" Text='<%# Bind("RequiredQuantity") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <%-- listView coloum --%>
                                                    <asp:TemplateField HeaderText="Supplier" SortExpression="RequiredQuantity" Visible ="false">
                                                        <EditItemTemplate>
                                                            <asp:Label ID="lblsupp" runat="server" Text='<%# Bind("Supplier") %>'></asp:Label>

                                                            <asp:ListBox ID="lstboxedit" runat="server" Visible="false" Width="150px" DataTextField="BPartnerName" DataValueField="BPartnerName" SelectionMode="Single" Height="60px"></asp:ListBox>

                                                            <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Select" Text="ADD" CssClass="btn"></asp:LinkButton>

                                                            <ajaxToolkit:ModalPopupExtender ID="mp1" runat="server" PopupControlID="Panel1" TargetControlID="LinkButton1"
                                                                CancelControlID="btnClose" BackgroundCssClass="modalBackground">
                                                            </ajaxToolkit:ModalPopupExtender>
                                                            <asp:Panel ID="Panel1" runat="server" CssClass="modalPopup" align="center" Style="display: none">
                                                                <div style="height: 60px">
                                                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                                                        <Triggers>
                                                                            <asp:PostBackTrigger ControlID="DropDownList112a" />
                                                                        </Triggers>
                                                                        <ContentTemplate>
                                                                            Supplier&nbsp;
                                                                           <asp:DropDownList ID="DropDownList112a" runat="server" AutoPostBack="true" OnSelectedIndexChanged="OnSelectedIndexChanged">
                                                                           </asp:DropDownList>

                                                                        </ContentTemplate>
                                                                    </asp:UpdatePanel>
                                                                </div>
                                                                <asp:Button ID="btnClose" runat="server" Text="Close" />
                                                            </asp:Panel>

                                                            <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:db_HRERPSys %>" SelectCommand="select BPartnerName from SET_BusinessPartner WHERE BPartnerId in (select BPartnerId from SET_BPartnerType where BPNatureID = 37)"></asp:SqlDataSource>

                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                           
                                                            
                                                            <asp:Label ID="lblsupp" runat="server" Text='<%# Bind("Supplier") %>'></asp:Label>
                                                            
                                                            
                                                            
                                                             <asp:ListBox ID="ListBox1" runat="server" Visible="false" Width="150px" DataTextField="BPartnerName" DataValueField="BPartnerName" SelectionMode="Single" Height="60px"></asp:ListBox>

                                                            <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Select" Text="ADD" CssClass="btn"></asp:LinkButton>

                                                            <ajaxToolkit:ModalPopupExtender ID="mp1" runat="server" PopupControlID="Panel1" TargetControlID="LinkButton1"
                                                                CancelControlID="btnClose" BackgroundCssClass="modalBackground">
                                                            </ajaxToolkit:ModalPopupExtender>
                                                            <asp:Panel ID="Panel1" runat="server" CssClass="modalPopup" align="center" Style="display: none">
                                                                <div style="height: 60px">
                                                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                                                        <Triggers>
                                                                            <asp:PostBackTrigger ControlID="DropDownList71a" />
                                                                        </Triggers>
                                                                        <ContentTemplate>
                                                                            Supplier&nbsp;
                                                                    <asp:DropDownList ID="DropDownList71a" runat="server" AutoPostBack="true" OnSelectedIndexChanged="OnSelectedIndexChanged">
                                                                        </asp:DropDownList>

                                                                        </ContentTemplate>
                                                                    </asp:UpdatePanel>
                                                                </div>
                                                                <asp:Button ID="btnClose" runat="server" Text="Close" />
                                                            </asp:Panel>

                                                            <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:db_HRERPSys %>" SelectCommand="select BPartnerName from SET_BusinessPartner WHERE BPartnerId in (select BPartnerId from SET_BPartnerType where BPNatureID = 37)"></asp:SqlDataSource>

                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                     <%--<asp:TemplateField HeaderText="">
                                                         <HeaderTemplate>
                                                              <asp:CheckBox ID="chkall" runat="server" AutoPostBack="True" OnCheckedChanged="chkall_CheckedChanged" Text="All" Width="83px" />
                                                         </HeaderTemplate>
                                                        <EditItemTemplate>
                                                            <asp:CheckBox ID="chkSelect" runat="server" />
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="chkSelect" runat="server"/>
                                                        </ItemTemplate>
                                                        <HeaderStyle Width="40px" />
                                                          <ItemStyle Height="10px" />
                                                    </asp:TemplateField>--%>
                                                    <%-- listView coloum --%>
                                                </Columns>
                                                <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
                                                <HeaderStyle BackColor="#000080" Font-Bold="True" ForeColor="White" Height="5px" />
                                                <PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="Right" />
                                            </asp:GridView>
                                            



                                            <%--<asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:db_HRERPSys %>" SelectCommand="SELECT [Sno],[RequistionNo], [ItemSubHeadName], [Description], [UOM], [RequiredQuantity] FROM [VIEW_ASKFORQOUT]"></asp:SqlDataSource>--%>

                                           <%-- <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:db_HRERPSys %>" SelectCommand="SELECT [DocID], [DocDescription], [DocName], [IsActive], [IsHide] , [Main_Site], [ApplyDate] FROM [viewDOCUSER] order by DocDescription asc"></asp:SqlDataSource>--%>

                                        </div>

                                    </div>
                                    <!-- /.box-body -->
                                </div>
                                <div style="float:right;">
                     <asp:Button ID="Button3" runat="server" CssClass="btn btn-primary" Text="Save" Width="120px" Visible="false" OnClick="Button1_Click" />
                    <asp:Button ID="Button4" runat="server" CssClass="btn btn-default" Text="Cancel" Width="120px" Visible="false"   />
                    </div>
                            </div>
                
                

                 <div id="Div3" runat="server">
                                <div class="box">

                                    <!-- /.box-header -->
                                    
                                    <div class="box-body" style="width: 100%; overflow: auto; height: 50%;">
                                       <%-- CssClass="table table-bordered table-hover"--%>
                                      
                                        <div style="width: 100%; height: 200px; overflow: scroll">

                                           

                                             
                                            <asp:GridView ID="GridView7" AutoGenerateColumns="False" CssClass="table table-bordered table-hover tableFixHead" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Horizontal" runat="server" DataKeyNames="Sno" OnRowDataBound="GridView7_RowDataBound" OnRowDeleting="GridView7_RowDeleting">
                                                <Columns>

                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:ImageButton ImageUrl="~/Images123/delete.png" runat="server" CommandName="Delete" ToolTip="Delete" Width="20px" Height="20px" ID="imgdel" />
                                                        </ItemTemplate>

                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Sno" ItemStyle-Width="50">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblRowNumber" runat="server" />
                                                        </ItemTemplate>

                                                        <ItemStyle Width="100px"></ItemStyle>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="" SortExpression="Sno">
                                                        <EditItemTemplate>
                                                            <asp:Label ID="lblSno" runat="server" Text='<%# Eval("Sno") %>'></asp:Label>
                                                             <asp:Label ID="lbldetailSno" runat="server" Text='<%# Bind("SnoDetail") %>' Visible="false"></asp:Label>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSno" runat="server" Text='<%# Bind("SnoDetail") %>'></asp:Label>
                                                             <asp:Label ID="lbldetailSno" runat="server" Text='<%# Bind("SnoDetail") %>' Visible="false"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="RequisitionNo" SortExpression="RequisitionNo">
                                                        <EditItemTemplate>
                                                            <asp:Label ID="lblreq" runat="server" Text='<%# Bind("reqNOdetail") %>'></asp:Label>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblreq" runat="server" Text='<%# Bind("reqNOdetail") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="ItemSubHeadName" SortExpression="ItemSubHead">
                                                        <EditItemTemplate>
                                                            <asp:Label ID="lblsubhead" runat="server" Text='<%# Bind("ItemSubHeadName") %>'></asp:Label>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblsubheadid" Visible="false" runat="server" Text='<%# Bind("ItemSubHead") %>'></asp:Label>
                                                            <asp:Label ID="lblsubhead" runat="server" Text='<%# Bind("ItemSubHeadName") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="ItemMaster" SortExpression="ItemMaster">
                                                        <EditItemTemplate>
                                                            <asp:Label ID="lblitemmaster" runat="server" Text='<%# Bind("Description") %>'></asp:Label>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            
                                                            <asp:Label ID="lblitemmasterid" Visible="false" runat="server" Text='<%# Bind("ItemMaster") %>'></asp:Label>
                                                            <asp:Label ID="lblitemmaster" runat="server" Text='<%# Bind("Description") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="UOM" SortExpression="UOM">
                                                        <EditItemTemplate>
                                                            <asp:Label ID="lbluom" runat="server" Text='<%# Bind("UOMName") %>'></asp:Label>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbluomid" Visible="false" runat="server" Text='<%# Bind("UOM") %>'></asp:Label>
                                                            <asp:Label ID="lbluom" runat="server" Text='<%# Bind("UOMName") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="CurrentStock" SortExpression="CurrentStock">
                                                        <EditItemTemplate>
                                                            <asp:TextBox ID="txtCss" runat="server" Text='<%# Bind("CurrentStock") %>'></asp:TextBox>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblStockQ" runat="server" Text='<%# Bind("CurrentStock") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="RequiredQuantity" SortExpression="RequiredQuantity">
                                                        <EditItemTemplate>
                                                            <asp:TextBox ID="txtReqQ" runat="server" Text='<%# Bind("RequiredQuantity") %>'></asp:TextBox>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblreqQ" runat="server" Text='<%# Bind("RequiredQuantity") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <%-- listView coloum --%>
                                                    <asp:TemplateField HeaderText="Supplier" SortExpression="RequiredQuantity" Visible ="false">
                                                        <EditItemTemplate>
                                                            <asp:Label ID="lblsupp" runat="server" Text='<%# Bind("Supplier") %>'></asp:Label>

                                                            <asp:ListBox ID="lstboxedit" runat="server" Visible="false" Width="150px" DataTextField="BPartnerName" DataValueField="BPartnerName" SelectionMode="Single" Height="60px"></asp:ListBox>

                                                            <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Select" Text="ADD" CssClass="btn"></asp:LinkButton>

                                                            <ajaxToolkit:ModalPopupExtender ID="mp1" runat="server" PopupControlID="Panel1" TargetControlID="LinkButton1"
                                                                CancelControlID="btnClose" BackgroundCssClass="modalBackground">
                                                            </ajaxToolkit:ModalPopupExtender>
                                                            <asp:Panel ID="Panel1" runat="server" CssClass="modalPopup" align="center" Style="display: none">
                                                                <div style="height: 60px">
                                                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                                                        <Triggers>
                                                                            <asp:PostBackTrigger ControlID="DropDownList112aa" />
                                                                        </Triggers>
                                                                        <ContentTemplate>
                                                                            Supplier&nbsp;
                                                                           <asp:DropDownList ID="DropDownList112aa" runat="server" AutoPostBack="true" OnSelectedIndexChanged="OnSelectedIndexChanged">
                                                                           </asp:DropDownList>

                                                                        </ContentTemplate>
                                                                    </asp:UpdatePanel>
                                                                </div>
                                                                <asp:Button ID="btnClose" runat="server" Text="Close" />
                                                            </asp:Panel>

                                                            <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:db_HRERPSys %>" SelectCommand="select BPartnerName from SET_BusinessPartner WHERE BPartnerId in (select BPartnerId from SET_BPartnerType where BPNatureID = 37)"></asp:SqlDataSource>

                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                           
                                                            
                                                            <asp:Label ID="lblsupp" runat="server" Text='<%# Bind("Supplier") %>'></asp:Label>
                                                            
                                                            
                                                            
                                                             <asp:ListBox ID="ListBox1" runat="server" Visible="false" Width="150px" DataTextField="BPartnerName" DataValueField="BPartnerName" SelectionMode="Single" Height="60px"></asp:ListBox>

                                                            <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Select" Text="ADD" CssClass="btn"></asp:LinkButton>

                                                            <ajaxToolkit:ModalPopupExtender ID="mp1" runat="server" PopupControlID="Panel1" TargetControlID="LinkButton1"
                                                                CancelControlID="btnClose" BackgroundCssClass="modalBackground">
                                                            </ajaxToolkit:ModalPopupExtender>
                                                            <asp:Panel ID="Panel1" runat="server" CssClass="modalPopup" align="center" Style="display: none">
                                                                <div style="height: 60px">
                                                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                                                        <Triggers>
                                                                            <asp:PostBackTrigger ControlID="DropDownList71aa" />
                                                                        </Triggers>
                                                                        <ContentTemplate>
                                                                            Supplier&nbsp;
                                                                    <asp:DropDownList ID="DropDownList71aa" runat="server" AutoPostBack="true" OnSelectedIndexChanged="OnSelectedIndexChanged">
                                                                        </asp:DropDownList>

                                                                        </ContentTemplate>
                                                                    </asp:UpdatePanel>
                                                                </div>
                                                                <asp:Button ID="btnClose" runat="server" Text="Close" />
                                                            </asp:Panel>

                                                            <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:db_HRERPSys %>" SelectCommand="select BPartnerName from SET_BusinessPartner WHERE BPartnerId in (select BPartnerId from SET_BPartnerType where BPNatureID = 37)"></asp:SqlDataSource>

                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                     <%--<asp:TemplateField HeaderText="">
                                                         <HeaderTemplate>
                                                              <asp:CheckBox ID="chkall" runat="server" AutoPostBack="True" OnCheckedChanged="chkall_CheckedChanged" Text="All" Width="83px" />
                                                         </HeaderTemplate>
                                                        <EditItemTemplate>
                                                            <asp:CheckBox ID="chkSelect" runat="server" />
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="chkSelect" runat="server"/>
                                                        </ItemTemplate>
                                                        <HeaderStyle Width="40px" />
                                                          <ItemStyle Height="10px" />
                                                    </asp:TemplateField>--%>
                                                    <%-- listView coloum --%>
                                                </Columns>
                                                <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
                                                <HeaderStyle BackColor="#000080" Font-Bold="True" ForeColor="White" Height="5px"/>
                                                <PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="Right" />
                                            </asp:GridView>
                                            



                                            <%--<asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:db_HRERPSys %>" SelectCommand="SELECT [Sno],[RequistionNo], [ItemSubHeadName], [Description], [UOM], [RequiredQuantity] FROM [VIEW_ASKFORQOUT]"></asp:SqlDataSource>--%>

                                           <%-- <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:db_HRERPSys %>" SelectCommand="SELECT [DocID], [DocDescription], [DocName], [IsActive], [IsHide] , [Main_Site], [ApplyDate] FROM [viewDOCUSER] order by DocDescription asc"></asp:SqlDataSource>--%>

                                        </div>

                                    </div>
                                    <!-- /.box-body -->
                                </div>
                                <div style="float:right;">
                     <asp:Button ID="Button5" runat="server" CssClass="btn btn-primary" Text="Save" Width="120px" Visible="false" OnClick="Button1_Click" />
                    <asp:Button ID="Button6" runat="server" CssClass="btn btn-default" Text="Cancel" Width="120px" Visible="false"   />
                    </div>
                            </div>

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
                            <td class="rowpadd"><strong>Doc Type</strong> </td>
                            <td class="rowpadd">

                                <asp:DropDownList ID="ddlEdit_DocType" CssClass="form-control select2" Width="100%" runat="server"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="rowpadd"><strong>Document</strong> </td>
                            <td class="rowpadd">
                                
                                <asp:DropDownList ID="ddlEdit_Doc" CssClass="form-control select2" Width="100%" runat="server"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="rowpadd"><strong>Document Status</strong> </td>
                            <td class="rowpadd">
                                <asp:DropDownList ID="ddlEdit_DocStatus" Width="100%" CssClass=" form-control select2" runat="server" ValidationGroup="aaa">
                                    <asp:ListItem>Please select...</asp:ListItem>
                                    <asp:ListItem>Initial Entry</asp:ListItem>
                                    <asp:ListItem>Checked</asp:ListItem>
                                    <asp:ListItem>Posted</asp:ListItem>
                                </asp:DropDownList>
                                
                            </td>
                        </tr>
                        <tr>
                            <td class="rowpadd"><strong>Requisition Type</strong> </td>
                            <td class="rowpadd">

                                <asp:DropDownList ID="ddlEdit_ReqType" CssClass="form-control select2" Width="100%" runat="server">
                                    <asp:ListItem>Please select...</asp:ListItem>
                                    <asp:ListItem Value="D">Department</asp:ListItem>
                                    <asp:ListItem Value="P">Purchase</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="rowpadd"><strong>Priority</strong> </td>
                            <td class="rowpadd">
                                <asp:DropDownList ID="ddlEdit_Priority" Width="100%" CssClass=" form-control select2" runat="server" ValidationGroup="aaa">
                                    <asp:ListItem>Please select...</asp:ListItem>
                                    <asp:ListItem>Normal</asp:ListItem>
                                    <asp:ListItem>Urgent</asp:ListItem>
                                    <asp:ListItem>Very Urgent</asp:ListItem>
                                </asp:DropDownList>
                                
                            </td>
                        </tr>
                        <tr>
                            <td class="rowpadd"><strong>Quotation Required Number</strong> </td>
                            <td class="rowpadd">

                                <asp:DropDownList ID="ddlEdit_QuotationReqNO" Width="100%" CssClass=" form-control select2" runat="server" ValidationGroup="aaa">
                                    <asp:ListItem>Please select...</asp:ListItem>
                                    <asp:ListItem>Monthly</asp:ListItem>
                                    <asp:ListItem>Yearly</asp:ListItem>


                                </asp:DropDownList>
                            </td>
                        </tr>
                         <tr>
                            <td class="rowpadd"><strong>Requistion No</strong> </td>
                            <td class="rowpadd">

                                <asp:DropDownList ID="ddlEdit_ReqNO" CssClass="form-control select2" Width="100%" runat="server"></asp:DropDownList>
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
                            <td class="rowpadd"><strong>Doc Type</strong> </td>
                            <td class="rowpadd">

                                <asp:DropDownList ID="ddlFind_DocType" CssClass="form-control select2" Width="100%" runat="server"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="rowpadd"><strong>Document</strong> </td>
                            <td class="rowpadd">
                                
                                <asp:DropDownList ID="ddlFind_Doc" CssClass="form-control select2" Width="100%" runat="server"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="rowpadd"><strong>Document Status</strong> </td>
                            <td class="rowpadd">
                                <asp:DropDownList ID="ddlFind_DocStatus" Width="100%" CssClass=" form-control select2" runat="server" ValidationGroup="aaa">
                                    <asp:ListItem>Please select...</asp:ListItem>
                                    <asp:ListItem>Initial Entry</asp:ListItem>
                                    <asp:ListItem>Checked</asp:ListItem>
                                    <asp:ListItem>Posted</asp:ListItem>
                                </asp:DropDownList>
                                
                            </td>
                        </tr>
                        <tr>
                            <td class="rowpadd"><strong>Requisition Type</strong> </td>
                            <td class="rowpadd">

                                <asp:DropDownList ID="ddlFind_ReqType" CssClass="form-control select2" Width="100%" runat="server">
                                    <asp:ListItem>Please select...</asp:ListItem>
                                    <asp:ListItem Value="D">Department</asp:ListItem>
                                    <asp:ListItem Value="P">Purchase</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="rowpadd"><strong>Priority</strong> </td>
                            <td class="rowpadd">
                                <asp:DropDownList ID="ddlFind_Prioity" Width="100%" CssClass=" form-control select2" runat="server" ValidationGroup="aaa">
                                    <asp:ListItem>Please select...</asp:ListItem>
                                    <asp:ListItem>Normal</asp:ListItem>
                                    <asp:ListItem>Urgent</asp:ListItem>
                                    <asp:ListItem>Very Urgent</asp:ListItem>
                                </asp:DropDownList>
                                
                            </td>
                        </tr>
                        <tr>
                            <td class="rowpadd"><strong>Quotation Required Number</strong> </td>
                            <td class="rowpadd">

                                <asp:DropDownList ID="ddlFind_QuoReqNO" Width="100%" CssClass=" form-control select2" runat="server" ValidationGroup="aaa">
                                    <asp:ListItem>Please select...</asp:ListItem>
                                    <asp:ListItem>Monthly</asp:ListItem>
                                    <asp:ListItem>Yearly</asp:ListItem>


                                </asp:DropDownList>
                            </td>
                        </tr>
                         <tr>
                            <td class="rowpadd"><strong>Requistion No</strong> </td>
                            <td class="rowpadd">

                                <asp:DropDownList ID="ddlFind_Req_No" CssClass="form-control select2" Width="100%" runat="server"></asp:DropDownList>
                            </td>
                        </tr>



                        <tr>
                            <td class="rowpadd"><strong>Active</strong> </td>
                            <td class="rowpadd">
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
                            <td class="rowpadd"><strong>Doc Type</strong> </td>
                            <td class="rowpadd">

                                <asp:DropDownList ID="ddlDel_DocType" CssClass="form-control select2" Width="100%" runat="server"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="rowpadd"><strong>Document</strong> </td>
                            <td class="rowpadd">
                                
                                <asp:DropDownList ID="ddlDel_Doc" CssClass="form-control select2" Width="100%" runat="server"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="rowpadd"><strong>Document Status</strong> </td>
                            <td class="rowpadd">
                                <asp:DropDownList ID="ddlDel_DocStatus" Width="100%" CssClass=" form-control select2" runat="server" ValidationGroup="aaa">
                                    <asp:ListItem>Please select...</asp:ListItem>
                                    <asp:ListItem>Initial Entry</asp:ListItem>
                                    <asp:ListItem>Checked</asp:ListItem>
                                    <asp:ListItem>Posted</asp:ListItem>
                                </asp:DropDownList>
                                
                            </td>
                        </tr>
                        <tr>
                            <td class="rowpadd"><strong>Requisition Type</strong> </td>
                            <td class="rowpadd">

                                <asp:DropDownList ID="ddlDel_ReqType" CssClass="form-control select2" Width="100%" runat="server">
                                    <asp:ListItem>Please select...</asp:ListItem>
                                    <asp:ListItem Value="D">Department</asp:ListItem>
                                    <asp:ListItem Value="P">Purchase</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="rowpadd"><strong>Priority</strong> </td>
                            <td class="rowpadd">
                                <asp:DropDownList ID="ddlDel_Priority" Width="100%" CssClass=" form-control select2" runat="server" ValidationGroup="aaa">
                                    <asp:ListItem>Please select...</asp:ListItem>
                                    <asp:ListItem>Normal</asp:ListItem>
                                    <asp:ListItem>Urgent</asp:ListItem>
                                    <asp:ListItem>Very Urgent</asp:ListItem>
                                </asp:DropDownList>
                                
                            </td>
                        </tr>
                        <tr>
                            <td class="rowpadd"><strong>Quotation Required Number</strong> </td>
                            <td class="rowpadd">

                                <asp:DropDownList ID="ddlDel_QuotReqNo" Width="100%" CssClass=" form-control select2" runat="server" ValidationGroup="aaa">
                                    <asp:ListItem>Please select...</asp:ListItem>
                                    <asp:ListItem>Monthly</asp:ListItem>
                                    <asp:ListItem>Yearly</asp:ListItem>


                                </asp:DropDownList>
                            </td>
                        </tr>
                         <tr>
                            <td class="rowpadd"><strong>Requistion No</strong> </td>
                            <td class="rowpadd">

                                <asp:DropDownList ID="ddlDel_ReqNO" CssClass="form-control select2" Width="100%" runat="server"></asp:DropDownList>
                            </td>
                        </tr>


                        <tr>
                            <td class="rowpadd"><strong>Active</strong> </td>
                            <td class="rowpadd">
                                <asp:CheckBox ID="chkDel_Active" runat="server" Checked="true" Text="Active" />
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

