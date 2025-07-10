<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="frm_SetItemType.aspx.cs" Inherits="frm_Period" %>

<%@ Register Assembly="ExtendedWebComboBox" Namespace="ExtendedWebComboBox" TagPrefix="cc1" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    
    <script>
        function alertme() {
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

        function myfocus(elementRef)
        {
          //  elementRef.style.backgroundColor = "yellow";

        }

        function myBlur(elementRef) {
            elementRef.style.backgroundColor = "red";

        }

               
           
        function Mload() {
            $(function () {
                $("select[id*='ddlPuchaseAccount']").select2();
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
        .auto-style11 {
            padding-bottom: 7px;
            width: 360px;
            margin-left: 40px;
        }
    </style>

</asp:Content>
    <asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
        <%--<asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>--%>


    <section class="content">
        <div class="row">
            <div class="col-lg-12">

                <div class="box box-solid shadowbox " style="border-radius: 15px;">
                    <div class="box-header with-border" style="height:75px;">
                        <div style="padding-top:0px; width: 120px; float: left;">
                            <div id="tabmenu">
                                <h3 class="box-title" style="color: #fff; padding-left: 10px;"><strong>ITEM TYPE</strong></h3>
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
                                    <td class="auto-style6"><strong>Description</strong></td>
                                    <td class="auto-style11">
                                        <asp:TextBox ID="txtDescription" CssClass="texthieht" Width="250px" runat="server"  Font-Bold="False" Font-Italic="False" ForeColor="Black" Height="25px" ValidationGroup="aaa"></asp:TextBox>
                                         <asp:RequiredFieldValidator runat="server" ID="Req" ControlToValidate="txtDescription" Display="None" ErrorMessage="<b> Missing Field</b><br />A Description is required." ValidationGroup="aaa" />
                                       <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" TargetControlID="Req" runat="server" HighlightCssClass="vali"></ajaxToolkit:ValidatorCalloutExtender>
                                    </td>
                                    <td class="auto-style10">Active</td>
                                    <td class="padspace">

                                        <asp:Panel ID="Panel7" runat="server">
                                            <asp:CheckBox ID="chk_Active" runat="server" Text="Active"  />
                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        </asp:Panel>
                                        
                                    </td>
                                    
                                </tr>
                                 <tr>
                                   
                                     
                                     
                                      <td class="auto-style6"><strong>Purchases Account</strong></td>
                                    <td class="auto-style11">
                                    
                  <telerik:RadComboBox RenderMode="Lightweight" ID="RadComboAcct_Code" runat="server" Height="200" Width="250"
                        DropDownWidth="500" EmptyMessage="Choose an Account Code" HighlightTemplatedItems="true"
                        EnableLoadOnDemand="true" Filter="StartsWith" 
                        AutoPostBack="true" 
                      OnSelectedIndexChanged="RadComboAcct_Code_SelectedIndexChanged"
                         OnItemsRequested="RadComboAcct_Code_ItemsRequested" 
                        Label="" >
                        <HeaderTemplate>
                            <table style="width: 100%" cellspacing="0" cellpadding="0">
                                <tr>
                                    <td style="width: 60px;">
                                        Acct Code
                                    </td>
                                    <td style="width: 175px;">
                                        
                                        Account Description
                                    </td>
                                    <td style="width: 40px;">
                                        Acct Type
                                    </td>
                                     <td style="width: 40px;">
                                        Tran Type
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

                                        
                                        
                                        <%--   <asp:DropDownList ID="ddlPuchaseAccount" CssClass="form-control select2" Width="250px" runat="server"  ></asp:DropDownList>--%>
                                    
                                    </td>
                                    
                                   
                                     
                                     <td class="auto-style10">Inventory Account</td>
                                    <td class="padspace">
                             <div class="texthieht">
                     <telerik:RadComboBox RenderMode="Lightweight" ID="radInventoryAcct" runat="server" Height="200" Width="250"
                        DropDownWidth="500" EmptyMessage="Choose an Account Code" HighlightTemplatedItems="true"
                        EnableLoadOnDemand="true" Filter="StartsWith" 
                        AutoPostBack="true" 
                         OnSelectedIndexChanged="radInventoryAcct_SelectedIndexChanged"
                         OnItemsRequested="radInventoryAcct_ItemsRequested" 
                        Label="" >
                        <HeaderTemplate>
                            <table style="width: 100%" cellspacing="0" cellpadding="0">
                                <tr>
                                    <td style="width: 60px;">
                                        Acct Code
                                    </td>
                                    <td style="width: 175px;">
                                        
                                        Account Description
                                    </td>
                                    <td style="width: 40px;">
                                        Acct Type
                                    </td>
                                     <td style="width: 40px;">
                                        Tran Type
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
                                            
                                            <%--<asp:DropDownList ID="txtInventoryAcct" CssClass="form-control select2" Width="250px" runat="server" ></asp:DropDownList>--%>
                                        </div>
                                    </td>
                                    
                                </tr>
                                 <tr>
                                    <td class="auto-style6"><strong>Expense Account</strong></td>
                                    <td class="auto-style11">
                                     
                      <telerik:RadComboBox RenderMode="Lightweight" ID="RadExpenseAccount" runat="server" Height="200" Width="250"
                        DropDownWidth="500" EmptyMessage="Choose an Account Code" HighlightTemplatedItems="true"
                        EnableLoadOnDemand="true" Filter="StartsWith" 
                        AutoPostBack="true" 
                       OnSelectedIndexChanged="RadExpenseAccount_SelectedIndexChanged"
                         OnItemsRequested="RadExpenseAccount_ItemsRequested" 
                        Label="" >
                        <HeaderTemplate>
                            <table style="width: 100%" cellspacing="0" cellpadding="0">
                                <tr>
                                    <td style="width: 60px;">
                                        Acct Code
                                    </td>
                                    <td style="width: 175px;">
                                        
                                        Account Description
                                    </td>
                                    <td style="width: 40px;">
                                        Acct Type
                                    </td>
                                     <td style="width: 40px;">
                                        Tran Type
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
                                        
                                           <%--<asp:DropDownList ID="txtExpensesAcct"  CssClass="form-control select2"  Width="250px" runat="server"  ></asp:DropDownList>--%>
                                    </td>
                                    <td class="auto-style10">Cost of Goods Manifucature Account</td>
                                    <td class="padspace">
                                        <div class="texthieht">
                                         <%--   <asp:DropDownList ID="txtCostOfGoodMAcct" CssClass="form-control select2" Width="250px" runat="server"  ></asp:DropDownList>--%>

                                             <telerik:RadComboBox RenderMode="Lightweight" ID="radCostOfGoodMAcct" runat="server" Height="200" Width="250"
                        DropDownWidth="500" EmptyMessage="Choose an Account Code" HighlightTemplatedItems="true"
                        EnableLoadOnDemand="true" Filter="StartsWith" 
                        AutoPostBack="true" 
                        OnSelectedIndexChanged="radCostOfGoodMAcct_SelectedIndexChanged"
                        OnItemsRequested="radCostOfGoodMAcct_ItemsRequested" 
                        Label="" >
                        <HeaderTemplate>
                            <table style="width: 100%" cellspacing="0" cellpadding="0">
                                <tr>
                                    <td style="width: 60px;">
                                        Acct Code
                                    </td>
                                    <td style="width: 175px;">
                                        
                                        Account Description
                                    </td>
                                    <td style="width: 40px;">
                                        Acct Type
                                    </td>
                                     <td style="width: 40px;">
                                        Tran Type
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
                                        </div>
                                    </td>
                                    
                                </tr>
                                 <tr>
                                    <td class="auto-style6"><strong>Cost of Goods Sold Account</strong></td>
                                    <td class="auto-style11">
                                        
                                        <%--<asp:DropDownList ID="txtCostOfGoodsSAcct" CssClass="form-control select2" Width="250px" runat="server"  ></asp:DropDownList>--%>
                                             <telerik:RadComboBox RenderMode="Lightweight" ID="radCostOfGoodsSAcct" runat="server" Height="200" Width="250"
                        DropDownWidth="500" EmptyMessage="Choose an Account Code" HighlightTemplatedItems="true"
                        EnableLoadOnDemand="true" Filter="StartsWith" 
                        AutoPostBack="true" 
                          OnSelectedIndexChanged="radCostOfGoodsSAcct_SelectedIndexChanged"
                         OnItemsRequested="radCostOfGoodsSAcct_ItemsRequested" 
                        Label="" >
                        <HeaderTemplate>
                            <table style="width: 100%" cellspacing="0" cellpadding="0">
                                <tr>
                                    <td style="width: 60px;">
                                        Acct Code
                                    </td>
                                    <td style="width: 175px;">
                                        
                                        Account Description
                                    </td>
                                    <td style="width: 40px;">
                                        Acct Type
                                    </td>
                                     <td style="width: 40px;">
                                        Tran Type
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
                                    <td class="auto-style10">WIP Cwip Account</td>
                                    <td class="padspace">
                                        <div class="texthieht">
                                            
                                        <%--<asp:DropDownList ID="txtWIP_CwipAcct" CssClass="form-control select2" Width="250px" runat="server" ></asp:DropDownList>--%>
                                                <telerik:RadComboBox RenderMode="Lightweight" ID="radWIP_CwipAcct" runat="server" Height="200" Width="250"
                        DropDownWidth="500" EmptyMessage="Choose an Account Code" HighlightTemplatedItems="true"
                        EnableLoadOnDemand="true" Filter="StartsWith" 
                        AutoPostBack="true" 
                        OnSelectedIndexChanged="radWIP_CwipAcct_SelectedIndexChanged"
                         OnItemsRequested="radWIP_CwipAcct_ItemsRequested" 
                        Label="" >
                        <HeaderTemplate>
                            <table style="width: 100%" cellspacing="0" cellpadding="0">
                                <tr>
                                    <td style="width: 60px;">
                                        Acct Code
                                    </td>
                                    <td style="width: 175px;">
                                        
                                        Account Description
                                    </td>
                                    <td style="width: 40px;">
                                        Acct Type
                                    </td>
                                     <td style="width: 40px;">
                                        Tran Type
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

                                        </div>
                                    </td>
                                    
                                </tr>
                                 <tr>
                                    <td class="auto-style6"><strong>Finished Goods Account</strong></td>
                                    <td class="auto-style11">
                                        
                                        <%--<asp:DropDownList ID="txtFinishGoodsAcct" CssClass="form-control select2" Width="250px" runat="server" ></asp:DropDownList>--%>

                                         <telerik:RadComboBox RenderMode="Lightweight" ID="radFinishGoodsAcct" runat="server" Height="200" Width="250"
                        DropDownWidth="500" EmptyMessage="Choose an Account Code" HighlightTemplatedItems="true"
                        EnableLoadOnDemand="true" Filter="StartsWith" 
                        AutoPostBack="true" 
                        OnSelectedIndexChanged="radFinishGoodsAcct_SelectedIndexChanged"
                         OnItemsRequested="radFinishGoodsAcct_ItemsRequested" 
                        Label="" >
                        <HeaderTemplate>
                            <table style="width: 100%" cellspacing="0" cellpadding="0">
                                <tr>
                                    <td style="width: 60px;">
                                        Acct Code
                                    </td>
                                    <td style="width: 175px;">
                                        
                                        Account Description
                                    </td>
                                    <td style="width: 40px;">
                                        Acct Type
                                    </td>
                                     <td style="width: 40px;">
                                        Tran Type
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
                                    <td class="auto-style10">GST Receivable Account</td>
                                    <td class="padspace">
                                        <div class="texthieht">
                                            
                                      <%--  <asp:DropDownList ID="txtGST_R_Acct" CssClass="form-control select2" Width="250px" runat="server"  ></asp:DropDownList>--%>
                                              <telerik:RadComboBox RenderMode="Lightweight" ID="radGST_R_Acct" runat="server" Height="200" Width="250"
                        DropDownWidth="500" EmptyMessage="Choose an Account Code" HighlightTemplatedItems="true"
                        EnableLoadOnDemand="true" Filter="StartsWith" 
                        AutoPostBack="true" 
                        OnSelectedIndexChanged="radGST_R_Acct_SelectedIndexChanged"
                         OnItemsRequested="radGST_R_Acct_ItemsRequested" 
                        Label="" >
                        <HeaderTemplate>
                            <table style="width: 100%" cellspacing="0" cellpadding="0">
                                <tr>
                                    <td style="width: 60px;">
                                        Acct Code
                                    </td>
                                    <td style="width: 175px;">
                                        
                                        Account Description
                                    </td>
                                    <td style="width: 40px;">
                                        Acct Type
                                    </td>
                                     <td style="width: 40px;">
                                        Tran Type
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

                                        </div>
                                    </td>
                                    
                                </tr>
                                 <tr>
                                    <td class="auto-style6"><strong>GST Payable Account</strong></td>
                                    <td class="auto-style11">
                                        
                                       <%-- <asp:DropDownList ID="txtGST_P_Acct" CssClass="form-control select2" Width="250px" runat="server"  ></asp:DropDownList>--%>
                                        <telerik:RadComboBox RenderMode="Lightweight" ID="radGST_P_Acct" runat="server" Height="200" Width="250"
                        DropDownWidth="500" EmptyMessage="Choose an Account Code" HighlightTemplatedItems="true"
                        EnableLoadOnDemand="true" Filter="StartsWith" 
                        AutoPostBack="true" 
                        OnSelectedIndexChanged="radGST_P_Acct_SelectedIndexChanged"
                         OnItemsRequested="radGST_P_Acct_ItemsRequested" 
                        Label="" >
                        <HeaderTemplate>
                            <table style="width: 100%" cellspacing="0" cellpadding="0">
                                <tr>
                                    <td style="width: 60px;">
                                        Acct Code
                                    </td>
                                    <td style="width: 175px;">
                                        
                                        Account Description
                                    </td>
                                    <td style="width: 40px;">
                                        Acct Type
                                    </td>
                                     <td style="width: 40px;">
                                        Tran Type
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
                                    <td class="auto-style10">Purchase Discount Account</td>
                                    <td class="padspace">
                                        <div class="texthieht">
                                            
                                        <%--<asp:DropDownList ID="txtPurchase_Dis_Acct" CssClass="form-control select2" Width="250px" runat="server"  ></asp:DropDownList>--%>
                                             <telerik:RadComboBox RenderMode="Lightweight" ID="radPurchase_Dis_Acct" runat="server" Height="200" Width="250"
                        DropDownWidth="500" EmptyMessage="Choose an Account Code" HighlightTemplatedItems="true"
                        EnableLoadOnDemand="true" Filter="StartsWith" 
                        AutoPostBack="true" 
                        OnSelectedIndexChanged="radPurchase_Dis_Acct_SelectedIndexChanged"
                         OnItemsRequested="radPurchase_Dis_Acct_ItemsRequested" 
                        Label="" >
                        <HeaderTemplate>
                            <table style="width: 100%" cellspacing="0" cellpadding="0">
                                <tr>
                                    <td style="width: 60px;">
                                        Acct Code
                                    </td>
                                    <td style="width: 175px;">
                                        
                                        Account Description
                                    </td>
                                    <td style="width: 40px;">
                                        Acct Type
                                    </td>
                                     <td style="width: 40px;">
                                        Tran Type
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

                                        </div>
                                    </td>
                                    
                                </tr>
                                 <tr>
                                    <td class="auto-style6"><strong>Sales Discount Account</strong></td>
                                    <td class="auto-style11">
                                        
                                        <%--<asp:DropDownList ID="txtSales_Disc_Acct" CssClass="form-control select2" Width="250px" runat="server"  ></asp:DropDownList>--%>
                                        <telerik:RadComboBox RenderMode="Lightweight" ID="radSales_Disc_Acct" runat="server" Height="200" Width="250"
                        DropDownWidth="500" EmptyMessage="Choose an Account Code" HighlightTemplatedItems="true"
                        EnableLoadOnDemand="true" Filter="StartsWith" 
                        AutoPostBack="true" 
                        OnSelectedIndexChanged="radSales_Disc_Acct_SelectedIndexChanged"
                         OnItemsRequested="radSales_Disc_Acct_ItemsRequested" 
                        Label="" >
                        <HeaderTemplate>
                            <table style="width: 100%" cellspacing="0" cellpadding="0">
                                <tr>
                                    <td style="width: 60px;">
                                        Acct Code
                                    </td>
                                    <td style="width: 175px;">
                                        
                                        Account Description
                                    </td>
                                    <td style="width: 40px;">
                                        Acct Type
                                    </td>
                                     <td style="width: 40px;">
                                        Tran Type
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
                                    <td class="auto-style10">Purchase Return Account</td>
                                    <td class="padspace">
                                        <div class="texthieht">
                                                                                    
                                        <%--<asp:DropDownList ID="txtPurchaseReturnAcct" CssClass="form-control select2" Width="250px" runat="server"  ></asp:DropDownList>--%>
                                            <telerik:RadComboBox RenderMode="Lightweight" ID="radPurchaseReturnAcct" runat="server" Height="200" Width="250"
                        DropDownWidth="500" EmptyMessage="Choose an Account Code" HighlightTemplatedItems="true"
                        EnableLoadOnDemand="true" Filter="StartsWith" 
                        AutoPostBack="true" 
                        OnSelectedIndexChanged="radPurchaseReturnAcct_SelectedIndexChanged"
                         OnItemsRequested="radPurchaseReturnAcct_ItemsRequested" 
                        Label="" >
                        <HeaderTemplate>
                            <table style="width: 100%" cellspacing="0" cellpadding="0">
                                <tr>
                                    <td style="width: 60px;">
                                        Acct Code
                                    </td>
                                    <td style="width: 175px;">
                                        
                                        Account Description
                                    </td>
                                    <td style="width: 40px;">
                                        Acct Type
                                    </td>
                                     <td style="width: 40px;">
                                        Tran Type
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

                                        </div>
                                    </td>
                                    
                                </tr>
                                 <tr>
                                    <td class="auto-style6"><strong>Sales Return Account</strong></td>
                                    <td class="auto-style11">
                                        
                                       <%-- <asp:DropDownList ID="txtSalesReturnedAcct" CssClass="form-control select2" Width="250px" runat="server"></asp:DropDownList>--%>

                                         <telerik:RadComboBox RenderMode="Lightweight" ID="radSalesReturnedAcct" runat="server" Height="200" Width="250"
                        DropDownWidth="500" EmptyMessage="Choose an Account Code" HighlightTemplatedItems="true"
                        EnableLoadOnDemand="true" Filter="StartsWith" 
                        AutoPostBack="true"
                        OnSelectedIndexChanged="radSalesReturnedAcct_SelectedIndexChanged" 
                         OnItemsRequested="radSalesReturnedAcct_ItemsRequested" 
                        Label="" >
                        <HeaderTemplate>
                            <table style="width: 100%" cellspacing="0" cellpadding="0">
                                <tr>
                                    <td style="width: 60px;">
                                        Acct Code
                                    </td>
                                    <td style="width: 175px;">
                                        
                                        Account Description
                                    </td>
                                    <td style="width: 40px;">
                                        Acct Type
                                    </td>
                                     <td style="width: 40px;">
                                        Tran Type
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
                                    <td class="auto-style10">FED Tax Payable Account</td>
                                    <td class="padspace">
                                       
                                        <%--<asp:DropDownList ID="txtFEDTax_Pay_Acct" CssClass="form-control select2" Width="250px" runat="server"></asp:DropDownList>--%>
                                         <telerik:RadComboBox RenderMode="Lightweight" ID="radFEDTax_Pay_Acct" runat="server" Height="200" Width="250"
                        DropDownWidth="500" EmptyMessage="Choose an Account Code" HighlightTemplatedItems="true"
                        EnableLoadOnDemand="true" Filter="StartsWith" 
                        AutoPostBack="true" 
                        OnSelectedIndexChanged="radFEDTax_Pay_Acct_SelectedIndexChanged" 
                         OnItemsRequested="radFEDTax_Pay_Acct_ItemsRequested" 
                        Label="" ToolTip="">
                        <HeaderTemplate>
                            <table style="width: 100%" cellspacing="0" cellpadding="0">
                                <tr>
                                    <td style="width: 60px;">
                                        Acct Code
                                    </td>
                                    <td style="width: 175px;">
                                        
                                        Account Description
                                    </td>
                                    <td style="width: 40px;">
                                        Acct Type
                                    </td>
                                     <td style="width: 40px;">
                                        Tran Type
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
                                    
                                </tr>
                                <tr>
                                    <td class="auto-style6"><strong>SED Tax Payable Account</strong></td>
                                    <td class="auto-style11">
                                        <%--<asp:DropDownList ID="txtSED_Tax_PAy_Acct" CssClass="form-control select2" Width="250px" runat="server"  ></asp:DropDownList>--%>

                                         <telerik:RadComboBox RenderMode="Lightweight" ID="radSED_Tax_PAy_Acct" runat="server" Height="200" Width="250"
                        DropDownWidth="500" EmptyMessage="Choose an Account Code" HighlightTemplatedItems="true"
                        EnableLoadOnDemand="true" Filter="StartsWith" 
                        AutoPostBack="true"
                         OnSelectedIndexChanged="radSED_Tax_PAy_Acct_SelectedIndexChanged"  
                         OnItemsRequested="radSED_Tax_PAy_Acct_ItemsRequested" 
                        Label="" >
                        <HeaderTemplate>
                            <table style="width: 100%" cellspacing="0" cellpadding="0">
                                <tr>
                                    <td style="width: 60px;">
                                        Acct Code
                                    </td>
                                    <td style="width: 175px;">
                                        
                                        Account Description
                                    </td>
                                    <td style="width: 40px;">
                                        Acct Type
                                    </td>
                                     <td style="width: 40px;">
                                        Tran Type
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
                                    <td class="auto-style10">WH Tax Account</td>
                                    <td class="padspace">
                                       
                                       <%-- <asp:DropDownList ID="txtWHTax_Acct" CssClass="form-control select2" Width="250px" runat="server"  ></asp:DropDownList>--%>

                                          <telerik:RadComboBox RenderMode="Lightweight" ID="radWHTax_Acct" runat="server" Height="200" Width="250"
                        DropDownWidth="500" EmptyMessage="Choose an Account Code" HighlightTemplatedItems="true"
                        EnableLoadOnDemand="true" Filter="StartsWith" 
                        AutoPostBack="true" 
                        OnSelectedIndexChanged="radWHTax_Acct_SelectedIndexChanged"  
                         OnItemsRequested="radWHTax_Acct_ItemsRequested" 
                        Label="" >
                        <HeaderTemplate>
                            <table style="width: 100%" cellspacing="0" cellpadding="0">
                                <tr>
                                    <td style="width: 60px;">
                                        Acct Code
                                    </td>
                                    <td style="width: 175px;">
                                        
                                        Account Description
                                    </td>
                                    <td style="width: 40px;">
                                        Acct Type
                                    </td>
                                     <td style="width: 40px;">
                                        Tran Type
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
                                    
                                </tr>
                                <tr>
                                    <td class="auto-style6"><strong>FED Expense Account</strong></td>
                                    <td class="auto-style11">
                                        
                                       <%-- <asp:DropDownList ID="txtFEDExpenseAcct" CssClass="form-control select2"  Width="250px" runat="server"  ></asp:DropDownList>--%>
                                         <telerik:RadComboBox RenderMode="Lightweight" ID="radFEDExpenseAcct" runat="server" Height="200" Width="250"
                        DropDownWidth="500" EmptyMessage="Choose an Account Code" HighlightTemplatedItems="true"
                        EnableLoadOnDemand="true" Filter="StartsWith" 
                        AutoPostBack="true"
                       OnSelectedIndexChanged="radFEDExpenseAcct_SelectedIndexChanged"   
                         OnItemsRequested="radFEDExpenseAcct_ItemsRequested" 
                        Label="" >
                        <HeaderTemplate>
                            <table style="width: 100%" cellspacing="0" cellpadding="0">
                                <tr>
                                    <td style="width: 60px;">
                                        Acct Code
                                    </td>
                                    <td style="width: 175px;">
                                        
                                        Account Description
                                    </td>
                                    <td style="width: 40px;">
                                        Acct Type
                                    </td>
                                     <td style="width: 40px;">
                                        Tran Type
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
                                    <td class="auto-style10">SED Expense Account</td>
                                    <td class="padspace">
                                       
                                       <%-- <asp:DropDownList ID="txtSEDExpenseAcct" CssClass="form-control select2" Width="250px" runat="server" ></asp:DropDownList>--%>

                                          <telerik:RadComboBox RenderMode="Lightweight" ID="radSEDExpenseAcct" runat="server" Height="200" Width="250"
                        DropDownWidth="500" EmptyMessage="Choose an Account Code" HighlightTemplatedItems="true"
                        EnableLoadOnDemand="true" Filter="StartsWith" 
                        AutoPostBack="true"
                         OnSelectedIndexChanged="radSEDExpenseAcct_SelectedIndexChanged" 
                         OnItemsRequested="radSEDExpenseAcct_ItemsRequested" 
                        Label="" >
                        <HeaderTemplate>
                            <table style="width: 100%" cellspacing="0" cellpadding="0">
                                <tr>
                                    <td style="width: 60px;">
                                        Acct Code
                                    </td>
                                    <td style="width: 175px;">
                                        
                                        Account Description
                                    </td>
                                    <td style="width: 40px;">
                                        Acct Type
                                    </td>
                                     <td style="width: 40px;">
                                        Tran Type
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
                                    
                                </tr>
                                <tr>
                                    <td class="auto-style6"><strong>Stock Adjustment Account</strong></td>
                                    <td class="auto-style11">
                                        
                                        <%--<asp:DropDownList ID="txtStockAdjustmentAcct" CssClass="form-control select2" Width="250px" runat="server"  ></asp:DropDownList>--%>
                                          <telerik:RadComboBox RenderMode="Lightweight" ID="radStockAdjustmentAcct" runat="server" Height="200" Width="250"
                        DropDownWidth="500" EmptyMessage="Choose an Account Code" HighlightTemplatedItems="true"
                        EnableLoadOnDemand="true" Filter="StartsWith" 
                        AutoPostBack="true" 
                         OnSelectedIndexChanged="radStockAdjustmentAcct_SelectedIndexChanged" 
                         OnItemsRequested="radStockAdjustmentAcct_ItemsRequested" 
                        Label="" >
                        <HeaderTemplate>
                            <table style="width: 100%" cellspacing="0" cellpadding="0">
                                <tr>
                                    <td style="width: 60px;">
                                        Acct Code
                                    </td>
                                    <td style="width: 175px;">
                                        
                                        Account Description
                                    </td>
                                    <td style="width: 40px;">
                                        Acct Type
                                    </td>
                                     <td style="width: 40px;">
                                        Tran Type
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






                                     <td class="auto-style6"><strong>Fixed Asset Account</strong></td>
                                    <td class="auto-style11">
                                        
                                        <%--<asp:DropDownList ID="txtStockAdjustmentAcct" CssClass="form-control select2" Width="250px" runat="server"  ></asp:DropDownList>--%>
                                          <telerik:RadComboBox RenderMode="Lightweight" ID="radFixedAssetAcct" runat="server" Height="200" Width="250"
                        DropDownWidth="500" EmptyMessage="Choose an Account Code" HighlightTemplatedItems="true"
                        EnableLoadOnDemand="true" Filter="StartsWith" 
                        AutoPostBack="true" 
                         OnSelectedIndexChanged="radFixedAssetAcct_SelectedIndexChanged" 
                         OnItemsRequested="radFixedAssetAcct_ItemsRequested" 
                        Label="" >
                        <HeaderTemplate>
                            <table style="width: 100%" cellspacing="0" cellpadding="0">
                                <tr>
                                    <td style="width: 60px;">
                                        Acct Code
                                    </td>
                                    <td style="width: 175px;">
                                        
                                        Account Description
                                    </td>
                                    <td style="width: 40px;">
                                        Acct Type
                                    </td>
                                     <td style="width: 40px;">
                                        Tran Type
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
                                 </tr>
                                <tr>
                                     <td class="auto-style10">Created By</td>
                                    <td class="padspace">
                                        <asp:TextBox ID="txtCreatedBy" CssClass="texthieht" Width="250px" runat="server"  Font-Bold="False" Font-Italic="False" ForeColor="Black" Height="25px"></asp:TextBox>
                                                                               
                                        </td>
                                     <td class="auto-style10">Created Date</td>
                                    <td class="padspace">
                                        <asp:TextBox ID="txtSystemDate" CssClass="texthieht" Width="250px" runat="server"  Font-Bold="False" Font-Italic="False" ForeColor="Black" Height="25px"></asp:TextBox>
                                        

                                    </td>
                                </tr>

                                 <tr id="updatecol" runat="server">
                                     <td class="auto-style10">Updated By</td>
                                    <td class="padspace">
                                        <asp:TextBox ID="txtUpdatedBy" CssClass="texthieht" Width="250px" runat="server"  Font-Bold="False" Font-Italic="False" ForeColor="Black" Height="25px"></asp:TextBox>
                                        
                                        </td>
                                     <td class="auto-style10">Updated Date</td>
                                    <td class="padspace">
                                        <asp:TextBox ID="txtUpadtedDate" CssClass="texthieht" Width="250px" runat="server"  Font-Bold="False" Font-Italic="False" ForeColor="Black" Height="25px"></asp:TextBox>

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

                                            <asp:GridView ID="GridView1" runat="server" CssClass="table table-bordered table-hover tableFixHead" AutoGenerateColumns="False" OnSelectedIndexChanged="GridView1_SelectedIndexChanged" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Horizontal" DataKeyNames="ItemTypeID">
                                                <Columns>
                                                    <asp:CommandField ShowSelectButton="True" ButtonType="Image" SelectImageUrl="~/dist/img/edit.png" >
                                                    <HeaderStyle Width="20px" />
                                                    </asp:CommandField>
                                                    <asp:BoundField DataField="ItemTypeID" HeaderText="ID" SortExpression="ItemTypeID" InsertVisible="False" ReadOnly="True">
                                                    <HeaderStyle Width="20px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="Description" HeaderText="Description" SortExpression="Description" >
                                                    <HeaderStyle Width="500px" />
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
                                                    <asp:CommandField ShowSelectButton="True" ButtonType="Image" SelectImageUrl="~/dist/img/edit.png" >
                                                    <HeaderStyle Width="20px" />
                                                    </asp:CommandField>
                                                    <asp:BoundField DataField="ItemTypeID" HeaderText="ID" SortExpression="ItemTypeID" InsertVisible="False" ReadOnly="True">
                                                    <HeaderStyle Width="20px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="Description" HeaderText="Description" SortExpression="Description" >
                                                    <HeaderStyle Width="500px" />
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
                                                    <asp:CommandField ShowSelectButton="True" ButtonType="Image" SelectImageUrl="~/dist/img/edit.png" >
                                                    <HeaderStyle Width="20px" />
                                                    </asp:CommandField>
                                                    <asp:BoundField DataField="ItemTypeID" HeaderText="ID" SortExpression="ItemTypeID" InsertVisible="False" ReadOnly="True">
                                                    <HeaderStyle Width="20px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="Description" HeaderText="Description" SortExpression="Description" >
                                                    <HeaderStyle Width="500px" />
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
                            <td><strong>Description</strong> </td>
                            <td>
                                <%--<asp:TextBox ID="txtEdit_Description" CssClass="form-control gap" runat="server"></asp:TextBox>--%>
                                <asp:DropDownList ID="ddlEdit_Description" CssClass="form-control select2" Width="100%" runat="server" ></asp:DropDownList>
                               
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
                            <td><strong>Description</strong> </td>
                            <td>
                                <%--<asp:TextBox ID="txtFind_Description" CssClass="form-control gap" runat="server"></asp:TextBox>--%>
                                <asp:DropDownList ID="ddlFind_Description" CssClass="form-control select2" Width="100%" runat="server" ></asp:DropDownList>
                               
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

                    <asp:Button ID="btn_Search" CssClass="btn btn-primary" runat="server" Text="Search" OnClick="btn_Search_Click"/>

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
                            <td><strong>Description</strong> </td>
                            <td>
                                <%--<asp:TextBox ID="txtdelete_Description" CssClass="form-control gap" runat="server"></asp:TextBox>--%>
                                 <asp:DropDownList ID="ddldelete_Description" CssClass="form-control select2" Width="100%" runat="server" ></asp:DropDownList>
                               
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

