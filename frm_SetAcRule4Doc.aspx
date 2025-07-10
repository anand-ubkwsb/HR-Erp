<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="frm_SetAcRule4Doc.aspx.cs" Inherits="frm_Period" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
        
    <script>
        function alertme() {
            swal("Insert Successfuly!", "record has been saved!!!", "success")
                .then(function () {
                    window.location = "frm_ SetAcRule4Doc.aspx?UserID=" + userid + "&UsergrpID=" + UserGrpID + "&fiscaly=" + fiscal + "&FormID=" + FormID + "";
                });

        }

        function Editalert() {
            swal("Update Successfuly!", "record has been saved!!!", "success")
                .then(function () {
                    window.location = "frm_ SetAcRule4Doc.aspx?UserID=" + userid + "&UsergrpID=" + UserGrpID + "&fiscaly=" + fiscal + "&FormID=" + FormID + "";
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
                window.location = "frm_ SetAcRule4Doc.aspx?UserID=" + userid + "&UsergrpID=" + UserGrpID + "&fiscaly=" + fiscal + "&FormID=" + FormID + "";
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
            font-weight: 700;
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
                border-left: 1px solid #333333;
            border-right: 1px solid #333333;
            border-top: 1px solid #333333;
            margin-right: 5px;
                margin-top: 20px;
                color: #FFFFFF;
                display: block;
                float: left;
                height: 40px;
                border-radius: 10px 10px 0px 0px;
                line-height: 62px;
                text-decoration: none;
                vertical-align: middle;
                padding: 0 20px;
                background-image: url('nav-tab-bg.png');
                background-color: #1f4f8a; /*A basic background color is set incase the image will not load*/
                font-style: normal;
            font-variant: normal;
            font-weight: 700;
            font-size: 14px;
            font-family: Arial, Helvetica, sans-serif;
            border-bottom-style: none;
            border-bottom-color: inherit;
            border-bottom-width: medium;
        }
        /*End tab style*/
        .auto-style11 {
            padding-bottom: 7px;
            width: 271px;
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
                                <h3 class="box-title" style="color: #fff; padding-left: 10px;">ACCOUNT RULES FOR DOCUMENT</h3>
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
                                                <td class="auto-style6">Doc Name</td>
                                                <td class="auto-style11">
                                                    <asp:DropDownList ID="ddllDocName" CssClass="ddlHeight select2" Width="250px" runat="server"></asp:DropDownList>
                                                     <asp:RequiredFieldValidator runat="server" ID="Req" ControlToValidate="ddllDocName" Display="None" ErrorMessage="<b> Missing Field</b><br />A Document Name is required." ValidationGroup="aaa" />
                                                    <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" TargetControlID="Req" runat="server" HighlightCssClass="vali"></ajaxToolkit:ValidatorCalloutExtender>
                                                     
                                                </td>
                                                 <td class="auto-style6">Apply Date</td>
                                                <td class="auto-style11">
                                                    <div style="display: inline-block">

                                                        <div class="input-group input-group-sm">
                                                            <asp:TextBox ID="txtEntryDate" CssClass="form-control texthieht" Width="220px" runat="server" Font-Bold="False" Font-Italic="False" ForeColor="Black" ValidationGroup="aaa" ></asp:TextBox>
                                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender1" PopupButtonID="imgPopup" TargetControlID="txtEntryDate" Format="dd-MMM-yyyy" runat="server" />
                                                            <asp:RequiredFieldValidator runat="server" ID="Req2" ControlToValidate="txtEntryDate" Display="None" ErrorMessage="<b> Missing Field</b><br />A Date is required." ValidationGroup="aaa" />
                                                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender11" TargetControlID="Req2" runat="server" HighlightCssClass="vali"></ajaxToolkit:ValidatorCalloutExtender>
                                                            <span class="input-group-btn">

                                                                <asp:ImageButton ID="imgPopup" CssClass="btn btn-flat" ImageUrl="img/calendar.png" ImageAlign="Bottom" runat="server" />
                                                            </span>
                                                        </div>

                                                    </div>
                                                </td>
                                                </tr>
                                            <tr>
                                                <td class="auto-style6">Master/Detail</td>
                                                <td class="auto-style11">
                                                    <asp:RadioButton ID="rdb_Master" runat="server" GroupName="Hidesite" Text="Master" AutoPostBack ="true" OnCheckedChanged="rdb_Master_CheckedChanged"/>
                                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                    <asp:RadioButton ID="rdb_Detail" runat="server" GroupName="Hidesite" Text="Detail" AutoPostBack="True" OnCheckedChanged="rdb_Detail_CheckedChanged" />
                                                  </td>

                                                <td class="auto-style6">Refer Documnet</td>
                                                <td class="auto-style11">
                                                   <asp:DropDownList ID="ddlReferDoc" CssClass="ddlHeight select2" Width="250px" runat="server" OnSelectedIndexChanged="ddlReferDoc_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                                     <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator5" ControlToValidate="ddlReferDoc" Display="None" ErrorMessage="<b> Missing Field</b><br />A Refer Document is required." ValidationGroup="aaa" />
                                                    <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender6" TargetControlID="Req" runat="server" HighlightCssClass="vali"></ajaxToolkit:ValidatorCalloutExtender>
                                                     </td>
                                                </tr>


                                             <tr>
                                               <td class="auto-style6">Ref Doc Acct Code</td>
                                                <td class="auto-style11">
                                                   <telerik:RadComboBox RenderMode="Lightweight" ID="Radcmb_RefDocAcctCode" runat="server" Height="200" Width="250"
                                    DropDownWidth="500" EmptyMessage="Choose an Account Code" HighlightTemplatedItems="true"
                                    EnableLoadOnDemand="true" Filter="StartsWith" OnItemsRequested="Radcmb_RefDocAcctCode_ItemsRequested"
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

                                                 <td class="auto-style6"><strong>Ref Doc GL Impact</strong></td>
                                                <td class="auto-style11">
                                                    <%--<asp:TextBox ID="txtGLImpact" CssClass="texthieht" Width="250px" runat="server" Font-Bold="False" Font-Italic="False" ForeColor="Black" Height="25px" ValidationGroup="aaa"></asp:TextBox>--%>
                                                     <asp:DropDownList ID="ddlRefDocGlImapct" CssClass="select2" Width="250px"  runat="server" >
                                                         <asp:ListItem Value="0">No Impact</asp:ListItem>
                                                         <asp:ListItem Value="1">Credit Impact</asp:ListItem>
                                                         <asp:ListItem Value="2">Debit Impact</asp:ListItem>
                                                    </asp:DropDownList>
                                                    
                                                </td>
                                              </tr>
                                            <tr>
                                                   <td class="auto-style6">Form Name</td>
                                                <td class="auto-style11">
                                                    <%--<asp:TextBox ID="txtStatus" CssClass="texthieht" Width="250px" runat="server" Font-Bold="False" Font-Italic="False" ForeColor="Black" Height="25px"></asp:TextBox>--%>
                                                      <asp:DropDownList ID="ddlFormName" CssClass="ddlHeight select2" Width="250px" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlFormName_SelectedIndexChanged"></asp:DropDownList>
                                                     <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" ControlToValidate="ddlReferDoc" Display="None" ErrorMessage="<b> Missing Field</b><br />A Refer Document is required." ValidationGroup="aaa" />
                                                    <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" TargetControlID="Req" runat="server" HighlightCssClass="vali"></ajaxToolkit:ValidatorCalloutExtender>
                                                     </td>
                                                     
                                                
                                                </tr>
                                            
                                            
                                            
                                             <tr>
                                                <td class="auto-style6" runat="server" id="labelType">Type</td>
                                                <td class="auto-style11">
                                                 
                                                    <div id="DivBptype" runat="server">
                                                    <asp:DropDownList ID="ddlBpType" CssClass="ddlHeight select2" Width="250px" runat="server"></asp:DropDownList>
                                                     <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator2" ControlToValidate="ddlBpType" Display="None" ErrorMessage="<b> Missing Field</b><br />A Refer Document is required." ValidationGroup="aaa" />
                                                    <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" TargetControlID="RequiredFieldValidator2" runat="server" HighlightCssClass="vali"></ajaxToolkit:ValidatorCalloutExtender>
                                                    </div>
                                                    
                                                      <%-- <asp:RadioButton ID="rdb_BPartnerType" runat="server" GroupName="bpitem" Text="Business Partner Type" />--%>
                                                  
                                                    
                                                  
                                                      <div id="divitemtypes" runat="server">
                                                          <asp:DropDownList ID="ddlItemTypes" CssClass="ddlHeight select2" Width="250px" runat="server">
                                                              <asp:ListItem Value="0">Please Select ....</asp:ListItem>
                                                              <asp:ListItem Value="1">Is Asset</asp:ListItem>
                                                              <asp:ListItem Value="2">Is Consumable</asp:ListItem>
                                                              <asp:ListItem Value="3">Is Expense</asp:ListItem>

                                                          </asp:DropDownList>
                                                   
                                                  </div>
                                                </td>
                                                <td class="auto-style6">Account Type</td>
                                                <td class="auto-style11">
                                                   
                                                    
                                                     <div id="asccounttypeitem" runat="server">
                                                        <asp:DropDownList ID="ddlaccounttype" CssClass="ddlHeight select2" Width="250px" runat="server">
                                                            <asp:ListItem Value="0">Please Select ....</asp:ListItem>
                                                            <asp:ListItem Value="1">Purchases Account</asp:ListItem>
                                                            <asp:ListItem Value="2">Expense Account</asp:ListItem>
                                                            <asp:ListItem Value="3">Inventory Account</asp:ListItem>
                                                            <asp:ListItem Value="4">Cost of Goods Manifucature Account</asp:ListItem>
                                                            <asp:ListItem Value="5">Cost of Goods Sold Account</asp:ListItem>
                                                            <asp:ListItem Value="6">WIP Cwip Account</asp:ListItem>
                                                            <asp:ListItem Value="7">Finished Goods Account</asp:ListItem>
                                                            <asp:ListItem Value="8">GST Receivable Account</asp:ListItem>

                                                            <asp:ListItem Value="9">GST Payable Account</asp:ListItem>
                                                            <asp:ListItem Value="10">Purchase Discount Account</asp:ListItem>
                                                            <asp:ListItem Value="11">Sales Discount Account</asp:ListItem>
                                                            <asp:ListItem Value="12">Purchase Return Account</asp:ListItem>
                                                            <asp:ListItem Value="13">Sales Return Account</asp:ListItem>
                                                            <asp:ListItem Value="14">FED Tax Payable Account</asp:ListItem>
                                                            <asp:ListItem Value="15">SED Tax Payable Account</asp:ListItem>
                                                            <asp:ListItem Value="16">WH Tax Account</asp:ListItem>
                                                            <asp:ListItem Value="17">FED Expense Account</asp:ListItem>
                                                            <asp:ListItem Value="18">SED Expense Account</asp:ListItem>
                                                            <asp:ListItem Value="19">Stock Adjustment Account</asp:ListItem>
                                                            <asp:ListItem Value="20">Fixed Asset Account</asp:ListItem>

                                                          
                                                            
                                                        </asp:DropDownList>

                                                   </div>

                                                    <div id="accounttypeBP" runat="server">
                                                         <asp:DropDownList ID="ddlactBP" CssClass="ddlHeight select2" Width="250px" runat="server"></asp:DropDownList>
                                                    </div>

<%--
                                                <telerik:RadComboBox RenderMode="Lightweight" ID="radAccountCode" runat="server" Height="200" Width="250"
                                    DropDownWidth="500" EmptyMessage="Choose a Product" HighlightTemplatedItems="true"
                                    EnableLoadOnDemand="true" Filter="StartsWith" OnItemsRequested="radAccountCode_ItemsRequested"
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
                                                    
                                                    
                                                    
                                                    --%>
                                                    
                                                     
                                                </td>
                                                </tr>
                                             <tr>
                                                <td class="auto-style6">Force Account Code</td>
                                                <td class="auto-style11">
                                                   <telerik:RadComboBox RenderMode="Lightweight" ID="radForceAccount" runat="server" Height="200" Width="250"
                                    DropDownWidth="500" EmptyMessage="Choose an Account Code" HighlightTemplatedItems="true"
                                    EnableLoadOnDemand="true" Filter="StartsWith" OnItemsRequested="radForceAccount_ItemsRequested"
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
                                                <td class="auto-style6">Ref Documnet Rules</td>
                                                <td class="auto-style11">
                                                    <%--<asp:TextBox ID="txtStatus" CssClass="texthieht" Width="250px" runat="server" Font-Bold="False" Font-Italic="False" ForeColor="Black" Height="25px"></asp:TextBox>--%>
                                                      <asp:Panel ID="Panel5" runat="server">
                                                        <asp:CheckBox ID="chkRefDocRule" runat="server" Text="YES" TabIndex="2" />
                                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                    </asp:Panel>
                                                     
                                                </td>
                                                </tr>
                                             <tr>
                                                <td class="auto-style6"><strong>GL Impact</strong></td>
                                                <td class="auto-style11">
                                                    <%--<asp:TextBox ID="txtGLImpact" CssClass="texthieht" Width="250px" runat="server" Font-Bold="False" Font-Italic="False" ForeColor="Black" Height="25px" ValidationGroup="aaa"></asp:TextBox>--%>
                                                     <asp:DropDownList ID="ddlGLImpact" CssClass="select2" Width="250px"  runat="server" >
                                                         <asp:ListItem Value="0">No Impact</asp:ListItem>
                                                         <asp:ListItem Value="1">Credit Impact</asp:ListItem>
                                                         <asp:ListItem Value="2">Debit Impact</asp:ListItem>
                                                    </asp:DropDownList>
                                                    
                                                </td>
                                                <td class="auto-style6">Inventory Impact</td>
                                                <td class="padspace">
                                                    <div class="texthieht">
                                                    <%--<asp:TextBox ID="txtInventoryImpact" CssClass="texthieht" Width="250px" runat="server" Font-Bold="False" Font-Italic="False" ForeColor="Black" Height="25px"></asp:TextBox>--%>
                                                    <asp:DropDownList ID="ddlInventoryImpact" Width="250px" CssClass="select2" runat="server" >
                                                         <asp:ListItem Value="0">No Impact</asp:ListItem>
                                                         <asp:ListItem Value="1">PLUS</asp:ListItem>
                                                         <asp:ListItem Value="2">MINUS</asp:ListItem>
                                                    </asp:DropDownList>
                                                    </div>
                                                </td>

                                            </tr>
                                           <tr>
                                                 <td class="auto-style6"><strong>Difference</strong></td>
                                                <td class="auto-style11">
                                                    <asp:Panel ID="Panel1" runat="server">
                                                        <asp:CheckBox ID="chkDiff" runat="server" Text="YES" TabIndex="3" />
                                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                    </asp:Panel>


                                                </td>

                                               <td class="auto-style6"><strong>Hide</strong></td>
                                                <td class="auto-style11">
                                                    <asp:Panel ID="Panel8" runat="server">
                                                        <asp:CheckBox ID="chkHide" runat="server" Text="YES" TabIndex="3" />
                                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                    </asp:Panel>


                                                </td>

                                               </tr>
                                            <tr>
                                                <td class="auto-style6"><strong>Active</strong></td>
                                                <td class="auto-style11">
                                                    <asp:Panel ID="Panel7" runat="server">
                                                        <asp:CheckBox ID="chkActive" runat="server" Text="Active" TabIndex="3" />
                                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                    </asp:Panel>


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
                                                    <asp:BoundField DataField="Sno" HeaderText="Sno" SortExpression="Sno" ReadOnly="True">
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="ApplyDate" HeaderText="ApplyDate" SortExpression="ApplyDate" DataFormatString="{0:dd-MMM-yyyy}">
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="DocDescription" HeaderText="DocDescription" SortExpression="DocDescription" />
                                                    <asp:BoundField DataField="MasterDetail" HeaderText="MasterDetail" SortExpression="MasterDetail" />
                                                    <asp:BoundField DataField="refDocName" HeaderText="refDocName" SortExpression="refDocName" />
                                                    <asp:BoundField DataField="FormTitle" HeaderText="FormTitle" SortExpression="FormTitle" />
                                                    <asp:BoundField DataField="Type" HeaderText="Type" SortExpression="Type" />
                                                    <asp:BoundField DataField="AccountType" HeaderText="AccountType" SortExpression="AccountType" />
                                                    <asp:CheckBoxField DataField="RefDocRules" HeaderText="RefDocRules" SortExpression="RefDocRules" >
                                                    </asp:CheckBoxField>
                                                    <asp:BoundField DataField="GLImpact" HeaderText="GLImpact" SortExpression="GLImpact" />
                                                    <asp:BoundField DataField="InventoryImpact" HeaderText="InventoryImpact" SortExpression="InventoryImpact" />
                                                    <asp:BoundField DataField="Diff" HeaderText="Difference" SortExpression="Diff" />
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
                                                    <asp:BoundField DataField="Sno" HeaderText="Sno" SortExpression="Sno" ReadOnly="True">
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="ApplyDate" HeaderText="ApplyDate" SortExpression="ApplyDate" DataFormatString="{0:dd-MMM-yyyy}">
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="DocDescription" HeaderText="DocDescription" SortExpression="DocDescription" />
                                                    <asp:BoundField DataField="MasterDetail" HeaderText="MasterDetail" SortExpression="MasterDetail" />
                                                    <asp:BoundField DataField="refDocName" HeaderText="refDocName" SortExpression="refDocName" />
                                                    <asp:BoundField DataField="FormTitle" HeaderText="FormTitle" SortExpression="FormTitle" />
                                                    <asp:BoundField DataField="Type" HeaderText="Type" SortExpression="Type" />
                                                    <asp:BoundField DataField="AccountType" HeaderText="AccountType" SortExpression="AccountType" />
                                                    <asp:CheckBoxField DataField="RefDocRules" HeaderText="RefDocRules" SortExpression="RefDocRules" >
                                                    </asp:CheckBoxField>
                                                    <asp:BoundField DataField="GLImpact" HeaderText="GLImpact" SortExpression="GLImpact" />
                                                    <asp:BoundField DataField="InventoryImpact" HeaderText="InventoryImpact" SortExpression="InventoryImpact" />
                                                    <asp:BoundField DataField="Diff" HeaderText="Difference" SortExpression="Diff" />
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
                                                    <asp:BoundField DataField="Sno" HeaderText="Sno" SortExpression="Sno" ReadOnly="True">
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="ApplyDate" HeaderText="ApplyDate" SortExpression="ApplyDate" DataFormatString="{0:dd-MMM-yyyy}">
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="DocDescription" HeaderText="DocDescription" SortExpression="DocDescription" />
                                                    <asp:BoundField DataField="MasterDetail" HeaderText="MasterDetail" SortExpression="MasterDetail" />
                                                    <asp:BoundField DataField="refDocName" HeaderText="refDocName" SortExpression="refDocName" />
                                                    <asp:BoundField DataField="FormTitle" HeaderText="FormTitle" SortExpression="FormTitle" />
                                                    <asp:BoundField DataField="Type" HeaderText="Type" SortExpression="Type" />
                                                    <asp:BoundField DataField="AccountType" HeaderText="AccountType" SortExpression="AccountType" />
                                                    <asp:CheckBoxField DataField="RefDocRules" HeaderText="RefDocRules" SortExpression="RefDocRules" >
                                                    </asp:CheckBoxField>
                                                    <asp:BoundField DataField="GLImpact" HeaderText="GLImpact" SortExpression="GLImpact" />
                                                    <asp:BoundField DataField="InventoryImpact" HeaderText="InventoryImpact" SortExpression="InventoryImpact" />
                                                    <asp:BoundField DataField="Diff" HeaderText="Difference" SortExpression="Diff" />
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
                                
                                <asp:DropDownList ID="ddlEdit_MenuTitle" CssClass="form-control select2" Width="100%" runat="server"></asp:DropDownList>
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
                                
                                <asp:DropDownList ID="ddlFind_MenuName" CssClass="form-control select2" Width="100%" runat="server"></asp:DropDownList>
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
                                
                                <asp:DropDownList ID="ddlDel_MenuName" CssClass="form-control select2" Width="100%" runat="server"></asp:DropDownList>
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

