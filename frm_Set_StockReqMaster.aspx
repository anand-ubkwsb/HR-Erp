<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="frm_Set_StockReqMaster.aspx.cs" Inherits="frm_Period" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script>
        function alertme() {
            swal("Insert Successfuly!", "record has been saved!!!", "success")
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
        .rdbgap{
            padding-right:30px;
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
                                <h3 class="box-title" style="color: #fff; padding-left: 10px;"><strong>Stock Requisition</strong></h3>
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
                                            <tr >
                                                <td id="s" runat="server" visible="false" class="auto-style6"><strong>Document Type</strong></td>
                                                <td id="s21" runat="server" visible="false" class="auto-style11" >
                                                   
                                                      <asp:DropDownList ID="ddlDocTypes" Width="250px" CssClass=" form-control select2" runat="server" ValidationGroup="aaa" OnSelectedIndexChanged="ddlDocTypes_SelectedIndexChanged" AutoPostBack="true"  ></asp:DropDownList>
                                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" ControlToValidate="ddlDocTypes" InitialValue="Please select..." Display="None" ErrorMessage="<b> Missing Field</b><br />Please Select Document." ValidationGroup="aaa" />
                                                    <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" TargetControlID="RequiredFieldValidator1" runat="server" HighlightCssClass="vali" PopupPosition="Right"></ajaxToolkit:ValidatorCalloutExtender>
                                                </td>
                                                <td class="auto-style6"><strong>Document</strong></td>
                                                <td class="auto-style11">
                                                    <asp:DropDownList ID="ddlDocument" Width="250px" CssClass=" form-control select2" runat="server" ValidationGroup="aaa" AutoPostBack="True" OnSelectedIndexChanged="ddlDocument_SelectedIndexChanged"   ></asp:DropDownList>
                                                    <asp:RequiredFieldValidator runat="server" ID="req1" ControlToValidate="ddlDocument" InitialValue="Please select..." Display="None" ErrorMessage="<b> Missing Field</b><br />Please Select Document." ValidationGroup="aaa" />
                                                    <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" TargetControlID="req1" runat="server" HighlightCssClass="vali" PopupPosition="Right"></ajaxToolkit:ValidatorCalloutExtender>
                                                     
                                                </td>
                                                 <td class="auto-style6"><strong>Requisition Date </strong></td>
                                                <td class="auto-style11">
                                                   
                                                      <div style="display:inline-block">

                                                        <div class="input-group input-group-sm">
                                                            <asp:TextBox ID="txtReqDate" CssClass="form-control texthieht" Width="220px" runat="server" Font-Bold="False" Font-Italic="False" ForeColor="Black"  ValidationGroup="aaa" AutoPostBack="True" OnTextChanged="txtReqDate_TextChanged"></asp:TextBox>
                                                            <asp:Label id="lblreqdate" Visible="false" runat="server"></asp:Label>
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
                                                
                                                </tr>
                              <tr>
                                  <td class="auto-style6"><strong>Requisition Type</strong></td>
                                  <td class="auto-style11">
                                      <asp:Panel ID="Panel2" runat="server">
                                          <asp:RadioButton ID="rdbDepartment" Text="Department" runat="server" GroupName="Group_L_I" CssClass="rdbgap" AutoPostBack="True" OnCheckedChanged="rdbDepartment_CheckedChanged"/>
                                          <asp:RadioButton ID="rdbPurchase" Text="Purchase" runat="server" GroupName="Group_L_I" AutoPostBack="True" OnCheckedChanged="rdbPurchase_CheckedChanged" />
                                      </asp:Panel>
                                     
                                  </td>
                                   <td class="auto-style6"><strong>Priority</strong></td>
                                                <td class="auto-style11">
                                                    <asp:DropDownList ID="ddlPriority" Width="250px" CssClass=" form-control select2" runat="server" ValidationGroup="aaa"  >
                                                        <%--<asp:ListItem>Please select...</asp:ListItem>
                                                        <asp:ListItem>Normal</asp:ListItem>
                                                        <asp:ListItem>Urgent</asp:ListItem>
                                                        <asp:ListItem>Very Urgent</asp:ListItem>--%>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator2" ControlToValidate="ddlPriority" InitialValue="Please select..." Display="None" ErrorMessage="<b> Missing Field</b><br />Please Select Document." ValidationGroup="aaa" />
                                                    <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" TargetControlID="RequiredFieldValidator2" runat="server" HighlightCssClass="vali" PopupPosition="Right"></ajaxToolkit:ValidatorCalloutExtender>
                                                     
                                                     
                                                </td>
                                   
                              </tr>
                                            <tr>
                                                <td class="auto-style6"><strong>Requisition No.</strong></td>
                                                <td class="auto-style11">
                                                    <asp:TextBox ID="txtReqNO" CssClass="texthieht" Width="250px" runat="server" Font-Bold="False" Font-Italic="False" ForeColor="Black" Height="25px" ValidationGroup="aaa"></asp:TextBox>
                                                     <asp:Label ID="lbldocno" Visible="false" runat="server" Text=""></asp:Label>
                                                    <asp:RequiredFieldValidator runat="server" ID="Req" ControlToValidate="txtReqNO" Display="None" ErrorMessage="<b> Missing Field</b><br />Please input document name." ValidationGroup="aaa" />
                                                     <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender5" TargetControlID="Req" runat="server" HighlightCssClass="vali"></ajaxToolkit:ValidatorCalloutExtender>
                                                     
                                                </td>
                                               <td class="auto-style6"><strong>From Store/Department </strong></td>
                                                <td class="auto-style11">
                                                    <asp:DropDownList ID="ddlFromStroeDept" Width="250px" CssClass=" form-control select2" runat="server" ValidationGroup="aaa"  ></asp:DropDownList>
                                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator10" ControlToValidate="ddlFromStroeDept" InitialValue="Please select..." Display="None" ErrorMessage="<b> Missing Field</b><br />Please Select Department." ValidationGroup="aaa" />
                                                    <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender13" TargetControlID="RequiredFieldValidator10" runat="server" HighlightCssClass="vali" PopupPosition="Right"></ajaxToolkit:ValidatorCalloutExtender>

                                                    <%--<asp:TextBox ID="txtProject" CssClass="texthieht" Width="250px" runat="server" Font-Bold="False" Font-Italic="False" ForeColor="Black" Height="25px" ValidationGroup="aaa"></asp:TextBox>
                                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator7" ControlToValidate="txtProject" Display="None" ErrorMessage="<b> Missing Field</b><br />Please input document name." ValidationGroup="aaa" />
                                                    <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender9" TargetControlID="RequiredFieldValidator7" runat="server" HighlightCssClass="vali"></ajaxToolkit:ValidatorCalloutExtender>--%>
                                                     
                                                </td>
                                                </tr>
                                              <tr>
                                                <td class="auto-style6"><strong>Document Authority</strong></td>
                                                <td class="auto-style11">
                                                   <%--<asp:TextBox ID="txtReqStatus" CssClass="texthieht" Width="250px" runat="server" Font-Bold="False" Font-Italic="False" ForeColor="Black" Height="25px" ValidationGroup="aaa"></asp:TextBox>--%>
                                                    <asp:DropDownList ID="ddlReqStatus" Width="250px" CssClass=" form-control select2" runat="server" ValidationGroup="aaa" ></asp:DropDownList>

                                                   
                                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator4" ControlToValidate="ddlReqStatus" Display="None" InitialValue="Please select..." ErrorMessage="<b> Missing Field</b><br />Please Select Doc Authority." ValidationGroup="aaa" />
                                                     <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender6" TargetControlID="RequiredFieldValidator4" runat="server" HighlightCssClass="vali"></ajaxToolkit:ValidatorCalloutExtender>
                                                     

                                                     <%-- <telerik:RadComboBox RenderMode="Lightweight" ID="RadComboAuth" runat="server" Height="200" Width="250"
                                                        DropDownWidth="500" EmptyMessage="Choose a Authority" HighlightTemplatedItems="true"
                                                        EnableLoadOnDemand="true" Filter="StartsWith"
                                                        AutoPostBack="true" 
                                                        OnItemsRequested="RadComboAuth_ItemsRequested"
                                                        Label="">
                                                        <HeaderTemplate>
                                                            <table style="width: 100%" cellspacing="0" cellpadding="0">
                                                                <tr>
                                                                    <td style="width: 100px;">Sno
                                                                    </td>
                                                                    <td style="width: 175px;">Authority Name
                                                                    </td>
                                                                    
                                                                </tr>
                                                            </table>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <table style="width: 500px" cellspacing="0" cellpadding="0">
                                                                <tr>
                                                                    <td style="width: 100px;">
                                                                        <%# DataBinder.Eval(Container, "Text")%>
                                                                    </td>
                                                                    <td style="width: 175px;">
                                                                        <%# DataBinder.Eval(Container, "Attributes['AuthorityName']")%>
                                                                    </td>
                                                                  
                                                                   
                                                                </tr>
                                                            </table>
                                                        </ItemTemplate>
                                                    </telerik:RadComboBox>--%>
                                                    
                                                    <telerik:RadComboBox RenderMode="Lightweight" ID="RadComboAuth" runat="server" Height="200" Width="250"
                                                        DropDownWidth="250" EmptyMessage="Choose a Item Code" HighlightTemplatedItems="true"
                                                        EnableLoadOnDemand="true" Filter="StartsWith"
                                                        OnItemsRequested="RadComboAuth_ItemsRequested"
                                                        Label="" Visible="false">
                                                        <HeaderTemplate>
                                                            <table style="width: 100px" cellspacing="0" cellpadding="0">
                                                                <tr>
                                                                    <td style="width: 30px;"><b>ID</b>
                                                                    </td>
                                                                    <td style="width: 70px;"><b>Authority</b>
                                                                    </td>
                                                                    
                                                                   
                                                                </tr>
                                                            </table>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <table style="width: 140px" cellspacing="0" cellpadding="0">
                                                                <tr>
                                                                    <td style="width: 30px;">
                                                                        <%# DataBinder.Eval(Container, "Text")%>
                                                                    </td>
                                                                    <td style="width: 70px;">
                                                                        <%# DataBinder.Eval(Container, "Attributes['AuthorityName']")%>
                                                                    </td>
                                                                    
                                                                   
                                                                </tr>
                                                            </table>
                                                        </ItemTemplate>
                                                    </telerik:RadComboBox>
                                                    

                                                </td>
                                                   
                                                <td class="auto-style6"><strong>Copy Requisition No.</strong></td>
                                                <td class="auto-style11">
                                                    <asp:TextBox ID="txtCopyReqNo" CssClass="texthieht" Width="250px" runat="server" Font-Bold="False" Font-Italic="False" ForeColor="Black" Height="25px" ValidationGroup="aaa"></asp:TextBox>
                                                    
                                                     
                                                </td>
                                               
                                                </tr>
                                             <tr>
                                                <td class="auto-style6"><strong>Issue to Store/Department</strong></td>
                                                <td class="auto-style11">
                                                     <asp:DropDownList ID="ddlDepartment" Width="250px" CssClass=" form-control select2" runat="server" ValidationGroup="aaa"  ></asp:DropDownList>
                                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator3" ControlToValidate="ddlDepartment" InitialValue="Please select..." Display="None" ErrorMessage="<b> Missing Field</b><br />Please Select Issue to Store/Department." ValidationGroup="aaa" />
                                                    <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender4" TargetControlID="RequiredFieldValidator3" runat="server" HighlightCssClass="vali" PopupPosition="Right"></ajaxToolkit:ValidatorCalloutExtender>
                                                     
                                                </td>
                                                  <td class="auto-style6"><strong>Remarks</strong></td>
                                                <td class="auto-style11">
                                                   
                                                     <asp:TextBox ID="txtRemarks" CssClass="texthieht" Width="250px" runat="server" Font-Bold="False" Font-Italic="False" ForeColor="Black" Height="25px" ValidationGroup="aaa"></asp:TextBox>
                                                    
                                                     
                                                </td>
                                               
                                                </tr>
                                                <tr>
                                                    <td class="auto-style6"><strong>Location</strong></td>
                                                <td class="auto-style11">


                                                    <asp:DropDownList ID="ddllocation" Width="250px" CssClass=" form-control select2" runat="server" ValidationGroup="aaa"  >
                                                        <%--<asp:ListItem>Please select...</asp:ListItem>
                                                        <asp:ListItem>Normal</asp:ListItem>
                                                        <asp:ListItem>Urgent</asp:ListItem>
                                                        <asp:ListItem>Very Urgent</asp:ListItem>--%>
                                                    </asp:DropDownList>
                                                   <%--<asp:TextBox ID="txtLocation" CssClass="texthieht" Width="250px" runat="server" Font-Bold="False" Font-Italic="False" ForeColor="Black" Height="25px" ValidationGroup="aaa"></asp:TextBox>--%>
                                                    <%--<asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator5" ControlToValidate="txtLocation" Display="None" ErrorMessage="<b> Missing Field</b><br />Please input document name." ValidationGroup="aaa" />
                                                     <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender7" TargetControlID="RequiredFieldValidator5" runat="server" HighlightCssClass="vali"></ajaxToolkit:ValidatorCalloutExtender>--%>
                                                     
                                                     </td>
                                              
                                                                                                    
                                                
                                                <td class="auto-style6"><strong>Requirement Due Date</strong></td>
                                                <td class="auto-style11">
                                                     <div style="display:inline-block">

                                                        <div class="input-group input-group-sm">
                                                            <asp:TextBox ID="txtRequirmentDueDate" CssClass="form-control texthieht" Width="220px" runat="server" Font-Bold="False" Font-Italic="False" ForeColor="Black"  ValidationGroup="aaa"></asp:TextBox>
                                                             <ajaxToolkit:CalendarExtender ID="CalendarExtender2" PopupButtonID="ImageButton1" TargetControlID="txtRequirmentDueDate" Format="dd-MMM-yyyy" runat="server" />
                                                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator9" ControlToValidate="txtRequirmentDueDate" Display="None" ErrorMessage="<b> Missing Field</b><br />A Date is required." ValidationGroup="aaa" />
                                                        <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender12" TargetControlID="RequiredFieldValidator9" runat="server" HighlightCssClass="vali"></ajaxToolkit:ValidatorCalloutExtender>
                                                            <span class="input-group-btn">
                                                                <%--<button type="button" class="btn btn-info btn-flat">Go!</button>--%>
                                                                <asp:ImageButton ID="ImageButton1" CssClass="btn btn-flat" ImageUrl="img/calendar.png" ImageAlign="Bottom" runat="server" />
                                                            </span>
                                                       </div>
                                                       
                                                    </div>
                                                     
                                                </td>
                                               
                                                </tr>
                                             
                                             <tr>
                                                  <td class="auto-style6"><strong>Created Date</strong></td>
                                                <td class="auto-style11">
                                                    <asp:TextBox ID="txtCreatedDate" CssClass="texthieht" Width="250px" runat="server" Font-Bold="False" Font-Italic="False" ForeColor="Black" Height="25px"></asp:TextBox>

                                                </td>



                                                <td class="auto-style6" visible="false"><strong>Created By</strong></td>
                                                <td class="auto-style11" visible="false">
                                                    <asp:TextBox ID="txtCreatedby" CssClass="texthieht" Width="250px" runat="server" Font-Bold="False" Font-Italic="False" ForeColor="Black" Height="25px"></asp:TextBox>
                                                </td>
                                                <td class="auto-style6"><strong>Status</strong></td>
                                        <td class="auto-style11">
                                            
                                           
                                            <asp:DropDownList ID="ddlStatus" Width="250px" CssClass=" form-control select2" runat="server" ValidationGroup="aaa">
                                                
                                            </asp:DropDownList>
                                            
                                             <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator7" ControlToValidate="ddlReqStatus" Display="None" ErrorMessage="<b> Missing Field</b><br />Please Select Status." ValidationGroup="aaa" />
                                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender8" TargetControlID="RequiredFieldValidator4" runat="server" HighlightCssClass="vali"></ajaxToolkit:ValidatorCalloutExtender>

                                        </td>
                                            </tr>
                                           
                                            <tr id="updatecol" runat="server">
                                                <td class="auto-style6"><strong>Updated By</strong></td>
                                                <td class="auto-style11">
                                                    <asp:TextBox ID="txtUpdateBy" CssClass="texthieht" Width="250px" runat="server" Font-Bold="False" Font-Italic="False" ForeColor="Black" Height="25px"></asp:TextBox>
                                                </td>
                                               
                                            </tr>
                              <tr>
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
                                                    <asp:BoundField DataField="Sno" HeaderText="ID" SortExpression="Sno" InsertVisible="False" ReadOnly="True">
                                                    <HeaderStyle Width="20px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="DocDescription" HeaderText="Document Name" SortExpression="DocDescription" >
                                                    <HeaderStyle Width="200px" />
                                                    </asp:BoundField>
                                                     <asp:BoundField DataField="RequisitionNo" HeaderText="Requisition No" SortExpression="RequisitionNo" >
                                                    <HeaderStyle Width="200px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="RequistionDate" HeaderText="Requistion Date" SortExpression="Requistion Date" DataFormatString = "{0:dd-MMM-yyyy}"  >
                                                    <HeaderStyle Width="200px" />
                                                    </asp:BoundField>

                                                    <asp:BoundField DataField="StatusName" HeaderText="Status" SortExpression="StatusName">
                                                    <HeaderStyle Width="200px" />
                                                    </asp:BoundField>
                                                    
                                                     <asp:BoundField DataField="DepartmentName" HeaderText="Issue Department Name" SortExpression="DepartmentName" >
                                                    <HeaderStyle Width="200px" />
                                                    </asp:BoundField>
                                                    <asp:CheckBoxField DataField="IsActive" HeaderText="Active" SortExpression="IsActive" >
                                                    <HeaderStyle Width="20px" />
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
                                                    <asp:BoundField DataField="Sno" HeaderText="ID" SortExpression="Sno" InsertVisible="False" ReadOnly="True">
                                                    <HeaderStyle Width="20px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="DocDescription" HeaderText="Document Name" SortExpression="DocDescription" >
                                                    <HeaderStyle Width="200px" />
                                                    </asp:BoundField>
                                                     <asp:BoundField DataField="RequisitionNo" HeaderText="Requisition No" SortExpression="RequisitionNo" >
                                                    <HeaderStyle Width="200px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="RequistionDate" HeaderText="Requistion Date" SortExpression="Requistion Date" DataFormatString = "{0:dd-MMM-yyyy}"  >
                                                    <HeaderStyle Width="200px" />
                                                    </asp:BoundField>
                                                      <asp:BoundField DataField="StatusName" HeaderText="Status" SortExpression="StatusName">
                                                    <HeaderStyle Width="200px" />
                                                    </asp:BoundField>
                                                    
                                                     <asp:BoundField DataField="DepartmentName" HeaderText="Issue Department Name" SortExpression="DepartmentName" >
                                                    <HeaderStyle Width="200px" />
                                                    </asp:BoundField>
                                                    <asp:CheckBoxField DataField="IsActive" HeaderText="Active" SortExpression="IsActive" >
                                                    <HeaderStyle Width="20px" />
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
                                                    <asp:BoundField DataField="Sno" HeaderText="ID" SortExpression="Sno" InsertVisible="False" ReadOnly="True">
                                                    <HeaderStyle Width="20px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="DocDescription" HeaderText="Document Name" SortExpression="DocDescription" >
                                                    <HeaderStyle Width="200px" />
                                                    </asp:BoundField>
                                                     <asp:BoundField DataField="RequisitionNo" HeaderText="Requisition No" SortExpression="RequisitionNo" >
                                                    <HeaderStyle Width="200px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="RequistionDate" HeaderText="Requistion Date" SortExpression="Requistion Date" DataFormatString = "{0:dd-MMM-yyyy}"  >
                                                    <HeaderStyle Width="200px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="StatusName" HeaderText="Status" SortExpression="StatusName">
                                                    <HeaderStyle Width="200px" />
                                                    </asp:BoundField>
                                                    
                                                     <asp:BoundField DataField="DepartmentName" HeaderText="Issue Department Name" SortExpression="DepartmentName" >
                                                    <HeaderStyle Width="200px" />
                                                    </asp:BoundField>
                                                    <asp:CheckBoxField DataField="IsActive" HeaderText="Active" SortExpression="IsActive" >
                                                    <HeaderStyle Width="20px" />
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
                 <div id="Div2" runat="server">
                    <div class="box">

                        <!-- /.box-header -->

                        <div class="box-body" style="width: 100%; overflow: auto; height: 50%;">
                            <%-- CssClass="table table-bordered table-hover"--%>

                            <div style="width: 100%; height: 250px; overflow: scroll">

                            
                                    
                                  

                                <asp:GridView ID="GridView6" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-hover tableFixHead" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Horizontal"  ShowFooter="true" ShowHeaderWhenEmpty="true" OnRowCommand="GridView6_RowCommand" OnRowCancelingEdit="GridView6_RowCancelingEdit" OnRowDeleting="GridView6_RowDeleting" OnRowEditing="GridView6_RowEditing" OnRowUpdating="GridView6_RowUpdating" OnRowDataBound="GridView6_RowDataBound">
                                
                                        <Columns>
                                       
                                        <asp:TemplateField HeaderText="Sno" >
                                            
                                            <ItemTemplate>
                                                <asp:Label ID="lblRowNumber" runat="server" />
                                            </ItemTemplate>

                                          
                                      
                                              </asp:TemplateField>



                                        <asp:TemplateField HeaderText="Item Sub Head Name" SortExpression="ItemSubHeadName">
                                            <EditItemTemplate>
                                                <asp:DropDownList ID="ddlitemsubEdit" Width="150px" CssClass=" form-control select2" runat="server" ValidationGroup="gv"></asp:DropDownList>
                                                
                                                
                                                <asp:Label ID="Label2" runat="server"  Enabled="false" Text='<%# Bind("ItemSubHeadName") %>'></asp:Label>
                                                <%--<asp:TextBox ID="ddlitemsubEdit" runat="server" Text='<%# Bind("ItemSubHeadName") %>'></asp:TextBox>--%>
                                                       
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="Label2" runat="server" Text='<%# Bind("ItemSubHeadName") %>'></asp:Label>
                                                
                                                 
                                            </ItemTemplate>
                                            <FooterTemplate>

                                               
                                                    <%-- <asp:Label ID="lblerroritemmaster" runat="server"  Enabled="false" ForeColor="Red"></asp:Label>--%>
                                               
                                                      <asp:DropDownList ID="ddlitemsub" Width="250px" CssClass=" form-control select2" runat="server" ValidationGroup="gv" AutoPostBack="True" OnSelectedIndexChanged="ddlitemsub_SelectedIndexChanged"></asp:DropDownList>
                                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldV" ControlToValidate="ddlitemsub" Display="None" InitialValue="Please select..." ErrorMessage="<b> Missing Field</b><br />Please Select item sub head." ValidationGroup="gv" />
                                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender8" TargetControlID="RequiredFieldV" runat="server" HighlightCssClass="vali" PopupPosition="BottomRight"></ajaxToolkit:ValidatorCalloutExtender>

                                               
                                                         
                                            </FooterTemplate>
                                        </asp:TemplateField>




                                        <asp:TemplateField HeaderText="Item Master" SortExpression="Description">

                                            <ItemTemplate>
                                                <asp:Label ID="Label3" runat="server" Text='<%# Bind("Description") %>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>

                                                <asp:DropDownList ID="ddlItemMasterEdit" Width="150px" CssClass=" form-control select2" runat="server" ValidationGroup="aaa"></asp:DropDownList>
                                                <asp:Label ID="lblitemmaster" runat="server" Enabled="false" Text='<%# Bind("Description") %>'></asp:Label>

                                            </EditItemTemplate>
                                            <FooterTemplate>
                                                <asp:DropDownList ID="ddlitemMaster" Width="250px" CssClass=" form-control select2" runat="server" ValidationGroup="gv" AutoPostBack="True" OnSelectedIndexChanged="ddlitemMaster_SelectedIndexChanged"></asp:DropDownList>
                                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldV1" ControlToValidate="ddlitemMaster" Display="None" InitialValue="Please select..." ErrorMessage="<b> Missing Field</b><br />Please Select item Master." ValidationGroup="gv" />
                                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCallout9" TargetControlID="RequiredFieldV1" runat="server" HighlightCssClass="vali" PopupPosition="BottomRight"></ajaxToolkit:ValidatorCalloutExtender>
                                            </FooterTemplate>
                                        </asp:TemplateField>





                                        <asp:TemplateField HeaderText="UOM" SortExpression="UOM">
                                            <EditItemTemplate>
                                                <asp:DropDownList ID="ddlUomEdit" Width="150px" CssClass=" form-control select2" runat="server" ValidationGroup="aaa"></asp:DropDownList>
                                                <asp:Label ID="lblUOMEdit" runat="server" Enabled="false" Text='<%# Bind("UOM") %>'></asp:Label>

                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="Label4" runat="server" Text='<%# Bind("UOM") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:DropDownList ID="ddlUomFooter" Width="150px" CssClass=" form-control select2" runat="server" ></asp:DropDownList>
                                                
                                            </FooterTemplate>
                                        </asp:TemplateField>




                                        <asp:TemplateField HeaderText="Current Stock" SortExpression="CurrentStock">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="TextBox4" runat="server" Width="100px" Text='<%# Bind("CurrentStock") %>'></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="Label5" runat="server"  Width= "100px" Text='<%# Bind("CurrentStock") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:TextBox ID="txtcurrStockFooter"  Width="100px" runat="server" ValidationGroup="gv"></asp:TextBox>
                                            
                                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldV4" ControlToValidate="txtcurrStockFooter" Display="None"  ErrorMessage="<b> Missing Field</b><br />Please Input Current Stock." ValidationGroup="gv" />
                                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCallout11" TargetControlID="RequiredFieldV4" runat="server" HighlightCssClass="vali" PopupPosition="BottomRight"></ajaxToolkit:ValidatorCalloutExtender>

                                            </FooterTemplate>
                                        </asp:TemplateField>




                                        <asp:TemplateField HeaderText="Required Quantity" SortExpression="RequiredQuantity">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="TextBox5" runat="server"  Width="100px" Text='<%# Bind("RequiredQuantity") %>'></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="Label6" runat="server"  Width="100px" Text='<%# Bind("RequiredQuantity") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:TextBox ID="txtReqStockFooter"  Width="100px" runat="server" ValidationGroup="gv"></asp:TextBox>

                                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldV5" ControlToValidate="txtReqStockFooter" Display="None"  ErrorMessage="<b> Missing Field</b><br />Please Input Required Quantity." ValidationGroup="gv" />
                                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCallout14" TargetControlID="RequiredFieldV5" runat="server" HighlightCssClass="vali" PopupPosition="BottomRight"></ajaxToolkit:ValidatorCalloutExtender>
                                            </FooterTemplate>
                                        </asp:TemplateField>





                                        <asp:TemplateField HeaderText="Cost Center" SortExpression="CostCenter">
                                            <EditItemTemplate>

                                                <asp:DropDownList ID="ddlCostCenterEdit" Width="150px" CssClass=" form-control select2" runat="server" ValidationGroup="aaa"></asp:DropDownList>
                                                <asp:Label ID="lblcostCenterEdit" runat="server" Enabled="false" Text='<%# Bind("CostCenter") %>'></asp:Label>


                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="Label7" runat="server"  Width="150px"  Text='<%# Bind("CostCenter") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:DropDownList ID="ddlCostCenterFooter"  Width="150px" CssClass=" form-control select2" runat="server" ValidationGroup="aaa"></asp:DropDownList>
                                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldV7" ControlToValidate="ddlCostCenterFooter" Display="None" InitialValue="Please select..."  ErrorMessage="<b> Missing Field</b><br />Please Select Cost Center." ValidationGroup="gv" />
                                                <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCallout12" TargetControlID="RequiredFieldV7" runat="server" HighlightCssClass="vali" PopupPosition="BottomRight"></ajaxToolkit:ValidatorCalloutExtender>
                                            </FooterTemplate>
                                        </asp:TemplateField>





                                        <asp:TemplateField HeaderText="Project" SortExpression="Project">
                                            <EditItemTemplate>

                                                <asp:TextBox ID="txtProjectEdit" runat="server" Text='<%# Bind("Project") %>'></asp:TextBox>
                                                <asp:Label ID="lblProjectEdit" runat="server" Enabled="false" Text='<%# Bind("Project") %>'></asp:Label>


                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="Label8" runat="server" Text='<%# Bind("Project") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                               <asp:TextBox ID="lblProjectFooter" runat="server" ValidationGroup="gv"></asp:TextBox>
                                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldV8" ControlToValidate="lblProjectFooter" Display="None"  ErrorMessage="<b> Missing Field</b><br />Please input Project Name." ValidationGroup="gv" />
                                                <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCallout13" TargetControlID="RequiredFieldV8" runat="server" HighlightCssClass="vali" PopupPosition="BottomRight"></ajaxToolkit:ValidatorCalloutExtender>

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
                                                <asp:ImageButton ImageUrl="~/Images123/addnew.png" runat="server" CommandName="AddNew" ToolTip="Add New" Width="20px" Height="20px" ValidationGroup="gv" />
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


                    <div id="Div1" runat="server">
                    <div class="box">

                        <!-- /.box-header -->

                        <div class="box-body" style="width: 100%; overflow: auto; height: 50%;">
                            <%-- CssClass="table table-bordered table-hover"--%>

                            <div style="width: 100%; height: 200px; overflow: scroll">
                                <asp:GridView ID="GridView5" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-hover tableFixHead" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Horizontal"  ShowFooter="false" ShowHeaderWhenEmpty="True" OnRowCancelingEdit="GridView5_RowCancelingEdit" OnRowEditing="GridView5_RowEditing" OnRowUpdating="GridView5_RowUpdating">
                                    <Columns>


                                        <asp:TemplateField HeaderText="ItemSubHead" SortExpression="ItemSubHead">
                                            <EditItemTemplate>
                                                <%--<asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("ItemSubHead") %>'></asp:TextBox>--%>

                                                <asp:DropDownList ID="ddlitemsubEDIT" Width="150px" CssClass=" form-control select2" runat="server" ValidationGroup="aaa"></asp:DropDownList>
                                                <asp:Label ID="lblitemsubn" runat="server" Text='<%# Bind("ItemSubHeadName") %>'></asp:Label>
                                                <asp:Label ID="lblsno" Enabled="false" Visible="false" runat="server" Text='<%# Bind("Sno") %>'></asp:Label>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblitemsubn" runat="server" Text='<%# Bind("ItemSubHeadName") %>'></asp:Label>

                                            </ItemTemplate>
                                        </asp:TemplateField>



                                        <asp:TemplateField HeaderText="ItemMaster" SortExpression="ItemMaster">
                                            <EditItemTemplate>
                                                <asp:DropDownList ID="ddlItemmter" Width="150px" CssClass=" form-control select2" runat="server" ValidationGroup="aaa"></asp:DropDownList>
                                                <asp:Label ID="lblitemmas" runat="server" Text='<%# Bind("Description") %>'></asp:Label>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="Label2" runat="server" Text='<%# Bind("Description") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>



                                        <asp:TemplateField HeaderText="UOM" SortExpression="UOM">
                                            <EditItemTemplate>
                                                <asp:DropDownList ID="ddlUOMs" Width="150px" CssClass=" form-control select2" runat="server" ValidationGroup="aaa"></asp:DropDownList>
                                                <asp:Label ID="lblUOMs" runat="server" Text='<%# Bind("UOMName") %>'></asp:Label>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="Label3" runat="server" Text='<%# Bind("UOMName") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>



                                        <asp:TemplateField HeaderText="CurrentStock" SortExpression="CurrentStock" ItemStyle-HorizontalAlign="Right">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="TextBox4" runat="server" Text='<%# Bind("CurrentStock") %>'></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="Label4" runat="server" Text='<%# Bind("CurrentStock") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>



                                        <asp:TemplateField HeaderText="RequiredQuantity" SortExpression="RequiredQuantity" ItemStyle-HorizontalAlign="Right">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="TextBox5" runat="server" Text='<%# Bind("RequiredQuantity") %>'></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="Label5" runat="server" Text='<%# Bind("RequiredQuantity") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>




                                        <asp:TemplateField HeaderText="CostCenter" SortExpression="CostCenter">
                                            <EditItemTemplate>
                                                <asp:DropDownList ID="ddlCostCenter" Width="150px" CssClass=" form-control select2" runat="server" ValidationGroup="aaa"></asp:DropDownList>
                                                <asp:Label ID="lblCC" runat="server" Text='<%# Bind("CostCenterName") %>'></asp:Label>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="Label6" runat="server" Text='<%# Bind("CostCenterName") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>




                                        <asp:TemplateField HeaderText="Project" SortExpression="Project">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="TextBox7" runat="server" Text='<%# Bind("Project") %>'></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="Label7" runat="server" Text='<%# Bind("Project") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>




                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:ImageButton ImageUrl="~/Images123/edit.png" runat="server" CommandName="Edit" ToolTip="Edit" Width="20px" Height="20px" />

                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:ImageButton ImageUrl="~/Images123/save.png" runat="server" CommandName="Update" ToolTip="Update" Width="20px" Height="20px" />
                                                <asp:ImageButton ImageUrl="~/Images123/cancel.png" runat="server" CommandName="Cancel" ToolTip="Cancel" Width="20px" Height="20px" />
                                            </EditItemTemplate>

                                        </asp:TemplateField>

                                    </Columns>
                                    
                                    <HeaderStyle BackColor="#000080" Font-Bold="True" ForeColor="White" />
                                    <PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="Right" />
                                </asp:GridView>

                                

                                <%--  <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:db_HRERPSys %>" SelectCommand="SELECT [Sno], [ItemSubHeadName], [Description], [UOM], [CurrentStock], [RequiredQuantity], [Supplier] FROM [View_StockReq]"></asp:SqlDataSource>--%>
                            </div>
                        </div>
                    </div>
                </div>

                

                <%-- DElete gridview Start --%>


                  <div id="Div3" runat="server">
                    <div class="box">

                        <!-- /.box-header -->

                        <div class="box-body" style="width: 100%; overflow: auto; height: 50%;">
                            <%-- CssClass="table table-bordered table-hover"--%>

                            <div style="width: 100%; height: 200px; overflow: scroll">
                                <asp:GridView ID="GridView7" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-hover tableFixHead" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Horizontal"  ShowFooter="false" ShowHeaderWhenEmpty="True" OnRowDeleting="GridView7_RowDeleting">
                                    <Columns>
                                    

                                        <asp:TemplateField HeaderText="ItemSubHead" SortExpression="ItemSubHead">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("ItemSubHeadName") %>'></asp:TextBox>
                                                
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="Label1" runat="server" Text='<%# Bind("ItemSubHeadName") %>'></asp:Label>
                                                <asp:Label ID="lblsno" Enabled="false" Visible="false" runat="server" Text='<%# Bind("Sno") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="ItemMaster" SortExpression="ItemMaster">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("Description") %>'></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="Label2" runat="server" Text='<%# Bind("Description") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="UOM" SortExpression="UOM">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="TextBox3" runat="server" Text='<%# Bind("UOMName") %>'></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="Label3" runat="server" Text='<%# Bind("UOMName") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="CurrentStock" SortExpression="CurrentStock" ItemStyle-HorizontalAlign="Right">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="TextBox4" runat="server" Text='<%# Bind("CurrentStock") %>'></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="Label4" runat="server" Text='<%# Bind("CurrentStock") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="RequiredQuantity" SortExpression="RequiredQuantity" ItemStyle-HorizontalAlign="Right">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="TextBox5" runat="server" Text='<%# Bind("RequiredQuantity") %>'></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="Label5" runat="server" Text='<%# Bind("RequiredQuantity") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="CostCenter" SortExpression="CostCenter">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="TextBox6" runat="server" Text='<%# Bind("CostCenterName") %>'></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="Label6" runat="server" Text='<%# Bind("CostCenterName") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Project" SortExpression="Project">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="TextBox7" runat="server" Text='<%# Bind("Project") %>'></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="Label7" runat="server" Text='<%# Bind("Project") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                         <asp:TemplateField>
                                            <ItemTemplate>
                                              <asp:ImageButton ImageUrl="~/Images123/delete.png" runat="server" CommandName="Delete" ToolTip="Delete" Width="20px" Height="20px" ID="imgdel" />
                                            </ItemTemplate>

                                        </asp:TemplateField>

                                    </Columns>
                                    
                                    <HeaderStyle BackColor="#000080" Font-Bold="True" ForeColor="White" />
                                    <PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="Right" />
                                </asp:GridView>

                                

                                <%--  <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:db_HRERPSys %>" SelectCommand="SELECT [Sno], [ItemSubHeadName], [Description], [UOM], [CurrentStock], [RequiredQuantity], [Supplier] FROM [View_StockReq]"></asp:SqlDataSource>--%>
                            </div>
                        </div>
                    </div>
                </div>

                <%-- Delete gridView End --%>

               <%-- Find Gridv Start --%>
                   <div id="Div4" runat="server">
                    <div class="box">

                        <!-- /.box-header -->

                        <div class="box-body" style="width: 100%; overflow: auto; height: 50%;">
                            <%-- CssClass="table table-bordered table-hover"--%>

                            <div style="width: 100%; height: 200px; overflow: scroll">
                                <asp:GridView ID="GridView4" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-hover tableFixHead" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Horizontal"  ShowFooter="false" ShowHeaderWhenEmpty="True" OnRowDeleting="GridView7_RowDeleting" OnRowDataBound="GridView4_RowDataBound">
                                    <Columns>
                                        <%-- <asp:TemplateField HeaderText="Sno" SortExpression="Sno">
                                  <ItemTemplate>
                                       <asp:Label ID="Label1" runat="server" Text='<%# Bind("Sno") %>'></asp:Label>
                                   </ItemTemplate>
                                    <EditItemTemplate>
                                       <asp:TextBox ID="txtsno" runat="server" Text='<%# Eval("Sno") %>'></asp:TextBox>
                                   </EditItemTemplate>
                                  <FooterTemplate>
                                      <asp:TextBox ID="txtsnofooter" runat="server"></asp:TextBox>
                                  </FooterTemplate>
                               </asp:TemplateField>--%>

                                        <asp:TemplateField HeaderText="ItemSubHead" SortExpression="ItemSubHead">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("ItemSubHeadName") %>'></asp:TextBox>
                                                
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="Label1" runat="server" Text='<%# Bind("ItemSubHeadName") %>'></asp:Label>
                                                <asp:Label ID="lblsno" Visible="false" runat="server" Text='<%# Bind("Sno") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="ItemMaster" SortExpression="ItemMaster">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("Description") %>'></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="Label2" runat="server" Text='<%# Bind("Description") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="UOM" SortExpression="UOM">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="TextBox3" runat="server" Text='<%# Bind("UOMName") %>'></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="Label3" runat="server" Text='<%# Bind("UOMName") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="CurrentStock" SortExpression="CurrentStock" ItemStyle-HorizontalAlign="Right">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="TextBox4" runat="server" Text='<%# Bind("CurrentStock") %>'></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="Label4" runat="server" Text='<%# Bind("CurrentStock") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="RequiredQuantity" SortExpression="RequiredQuantity" ItemStyle-HorizontalAlign="Right">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="TextBox5" runat="server" Text='<%# Bind("RequiredQuantity") %>'></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="Label5" runat="server" Text='<%# Bind("RequiredQuantity") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="CostCenter" SortExpression="CostCenter">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="TextBox6" runat="server" Text='<%# Bind("CostCenterName") %>'></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="Label6" runat="server" Text='<%# Bind("CostCenterName") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Project" SortExpression="Project">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="TextBox7" runat="server" Text='<%# Bind("Project") %>'></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="Label7" runat="server" Text='<%# Bind("Project") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                       

                                    </Columns>
                                    
                                    <HeaderStyle BackColor="#000080" Font-Bold="True" ForeColor="White" />
                                    <PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="Right" />
                                </asp:GridView>

                                

                                <%--  <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:db_HRERPSys %>" SelectCommand="SELECT [Sno], [ItemSubHeadName], [Description], [UOM], [CurrentStock], [RequiredQuantity], [Supplier] FROM [View_StockReq]"></asp:SqlDataSource>--%>
                            </div>
                        </div>
                    </div>
                </div>
                <%-- Find GridV End --%>

            </div>
        </div>

    </section>
            


    <%-- Start Modals --%>
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
                            <td class="rowpadd"><strong>Document Name</strong> </td>
                            <td class="rowpadd" >
                                
                                <asp:DropDownList ID="ddlEdit_DocName" CssClass="form-control select2" Width="100%" runat="server"></asp:DropDownList>
                            </td>
                        </tr>
                       <tr>
                            <td class="rowpadd"><strong>Requisition Type</strong> </td>
                            <td class="rowpadd" >

                                <asp:DropDownList ID="ddlEdit_ReqTypr" CssClass="form-control select2" Width="100%" runat="server">
                                    <asp:ListItem>Please select...</asp:ListItem>
                                    <asp:ListItem Text="Department" Value="D">Department</asp:ListItem>
                                    <asp:ListItem Text="Purchase" Value="P">Purchase</asp:ListItem>
                                   

                                </asp:DropDownList>
                            </td>
                        </tr>

                         <tr>
                            <td class="rowpadd"><strong>Requisition Date</strong> </td>
                            <td class="rowpadd" >
                                
                                <div style="display: inline-block">

                                    <div class="input-group input-group-sm">
                                        <asp:TextBox ID="txtEdit_ReqDate" CssClass="form-control texthieht" Width="350px" runat="server" Font-Bold="False" Font-Italic="False" ForeColor="Black" ValidationGroup="aaa"></asp:TextBox>
                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender6" PopupButtonID="ImageButton3" TargetControlID="txtEdit_ReqDate" Format="dd-MMM-yyyy" runat="server" />
                                        <span class="input-group-btn">
                                            <%--<button type="button" class="btn btn-info btn-flat">Go!</button>--%>
                                            <asp:ImageButton ID="ImageButton3" CssClass="btn btn-flat" ImageUrl="img/calendar.png" ImageAlign="Bottom" runat="server" />
                                        </span>
                                    </div>

                                </div>
                            </td>
                        </tr>

                        <tr>
                            <td class="rowpadd"><strong>Priority</strong> </td>
                            <td class="rowpadd" >
                                
                                <asp:DropDownList ID="ddlEdit_Priority" CssClass="form-control select2" Width="100%" runat="server">
                                     <asp:ListItem>Please select...</asp:ListItem>
                                    <asp:ListItem Text="Department" Value="D">Normal</asp:ListItem>
                                    <asp:ListItem Text="Purchase" Value="P">Urgent</asp:ListItem>
                                    <asp:ListItem Text="Purchase" Value="P">Very Urgent</asp:ListItem>
                                </asp:DropDownList>

                            </td>
                        </tr>

                        <tr>
                            <td class="rowpadd"><strong>Department</strong> </td>
                            <td class="rowpadd" >
                                
                                <asp:DropDownList ID="ddlEdit_Department" CssClass="form-control select2" Width="100%" runat="server"></asp:DropDownList>
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
                            <td class="rowpadd"><strong>Document Name</strong> </td>
                            <td class="rowpadd" >
                                
                                <asp:DropDownList ID="dllFind_DocName" CssClass="form-control select2" Width="100%" runat="server"></asp:DropDownList>
                            </td>
                        </tr>
                       <tr>
                            <td class="rowpadd"><strong>Requisition Type</strong> </td>
                            <td class="rowpadd" >
                                
                                <asp:DropDownList ID="dllFind_ReqType" CssClass="form-control select2" Width="100%" runat="server">
                                     <asp:ListItem>Please select...</asp:ListItem>
                                    <asp:ListItem Text="Department" Value="D">Department</asp:ListItem>
                                    <asp:ListItem Text="Purchase" Value="P">Purchase</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>

                         <tr>
                            <td class="rowpadd"><strong>Requisition Date</strong> </td>
                            <td class="rowpadd" >
                                
                                <div style="display: inline-block">

                                    <div class="input-group input-group-sm">
                                        <asp:TextBox ID="txtFind_ReqDate" CssClass="form-control texthieht" Width="350px" runat="server" Font-Bold="False" Font-Italic="False" ForeColor="Black" ValidationGroup="aaa"></asp:TextBox>
                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender3" PopupButtonID="ImageButton2" TargetControlID="txtFind_ReqDate" Format="dd-MMM-yyyy" runat="server" />
                                        <span class="input-group-btn">
                                            <%--<button type="button" class="btn btn-info btn-flat">Go!</button>--%>
                                            <asp:ImageButton ID="ImageButton2" CssClass="btn btn-flat" ImageUrl="img/calendar.png" ImageAlign="Bottom" runat="server" />
                                        </span>
                                    </div>

                                </div>
                            </td>
                        </tr>

                        <tr>
                            <td class="rowpadd"><strong>Priority</strong> </td>
                            <td class="rowpadd" >
                                
                                <asp:DropDownList ID="dllFind_Priority" CssClass="form-control select2" Width="100%" runat="server">
                                    <asp:ListItem>Please select...</asp:ListItem>
                                    <asp:ListItem Text="Department" Value="D">Normal</asp:ListItem>
                                    <asp:ListItem Text="Purchase" Value="P">Urgent</asp:ListItem>
                                    <asp:ListItem Text="Purchase" Value="P">Very Urgent</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>

                        <tr>
                            <td class="rowpadd"><strong>Department</strong> </td>
                            <td class="rowpadd" >
                                
                                <asp:DropDownList ID="dllFind_Department" CssClass="form-control select2" Width="100%" runat="server"></asp:DropDownList>
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
                            <td class="rowpadd"><strong>Document Name</strong> </td>
                            <td class="rowpadd" >
                                
                                <asp:DropDownList ID="dllDelete_DocName" CssClass="form-control select2" Width="100%" runat="server"></asp:DropDownList>
                            </td>
                        </tr>
                       <tr>
                            <td class="rowpadd"><strong>Requisition Type</strong> </td>
                            <td class="rowpadd" >
                                
                                <asp:DropDownList ID="dllDelete_ReqType" CssClass="form-control select2" Width="100%" runat="server">
                                     <asp:ListItem>Please select...</asp:ListItem>
                                    <asp:ListItem Text="Department" Value="D">Department</asp:ListItem>
                                    <asp:ListItem Text="Purchase" Value="P">Purchase</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>

                         <tr>
                            <td class="rowpadd"><strong>Requisition Date</strong> </td>
                            <td class="rowpadd" >
                                
                              <div style="display: inline-block">

                                    <div class="input-group input-group-sm">
                                        <asp:TextBox ID="txtDelete_REqDate" CssClass="form-control texthieht" Width="400px" runat="server" Font-Bold="False" Font-Italic="False" ForeColor="Black" ValidationGroup="aaa"></asp:TextBox>
                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender4" PopupButtonID="ImageButton4" TargetControlID="txtDelete_REqDate" Format="dd-MMM-yyyy" runat="server" />
                                        <span class="input-group-btn">
                                            <%--<button type="button" class="btn btn-info btn-flat">Go!</button>--%>
                                            <asp:ImageButton ID="ImageButton4" CssClass="btn btn-flat" ImageUrl="img/calendar.png" ImageAlign="Bottom" runat="server" />
                                        </span>
                                    </div>

                                </div>
                            </td>
                        </tr>

                        <tr>
                            <td class="rowpadd"><strong>Priority</strong> </td>
                            <td class="rowpadd" >
                                
                                <asp:DropDownList ID="dllDelete_Priority" CssClass="form-control select2" Width="100%" runat="server">
                                      <asp:ListItem>Please select...</asp:ListItem>
                                    <asp:ListItem Text="Department" Value="D">Normal</asp:ListItem>
                                    <asp:ListItem Text="Purchase" Value="P">Urgent</asp:ListItem>
                                    <asp:ListItem Text="Purchase" Value="P">Very Urgent</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>

                        <tr>
                            <td class="rowpadd"><strong>Department</strong> </td>
                            <td class="rowpadd" >
                                
                                <asp:DropDownList ID="dllDelete_Department" CssClass="form-control select2" Width="100%" runat="server"></asp:DropDownList>
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

