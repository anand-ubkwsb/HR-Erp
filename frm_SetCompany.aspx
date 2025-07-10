<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="frm_SetCompany.aspx.cs" Inherits="frm_SetCompany" MaintainScrollPositionOnPostback="true" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script>
        function alertme() {
            swal("Insert Successfuly!", "record has been saved!!!", "success")
                .then(function () {
                    window.location = "frm_SetCompany.aspx?UserID=" + userid + "&UsergrpID=" + UserGrpID + "&fiscaly=" + fiscal + "&FormID=" + FormID + "";
                });

        }

        function Editalert() {
            swal("Update Successfuly!", "record has been saved!!!", "success")
                .then(function () {
                    window.location = "frm_SetCompany.aspx?UserID=" + userid + "&UsergrpID=" + UserGrpID + "&fiscaly=" + fiscal + "&FormID=" + FormID + "";
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
                window.location = "frm_SetCompany.aspx?UserID=" + userid + "&UsergrpID=" + UserGrpID + "&fiscaly=" + fiscal + "&FormID=" + FormID + "";
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
        }

        .padspace {
            padding-bottom: 7px;
        }

        .auto-style1 {
            height: 20px;
            padding-bottom: 7px;
        }

        .texthieht {
            height: 25px;
        }

        .auto-style4 {
            height: 20px;
            width: 90px;
            padding-bottom: 7px;
        }

        .auto-style5 {
            width: 169px;
            padding-bottom: 7px;
        }

        .auto-style6 {
            width: 113px;
        }

        .auto-style7 {
            height: 20px;
            padding-bottom: 7px;
            width: 287px;
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

            #tabmenu a {
                display: inline;
                float: left;
            }

            #tabmenu a {
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
        .auto-style9 {
            padding-bottom: 7px;
            width: 234px;
        }

        .auto-style10 {
            width: 113px;
            padding-bottom: 7px;
        }

        .auto-style11 {
            width: 287px;
        }

        .auto-style12 {
            padding-bottom: 7px;
            width: 287px;
        }

        .auto-style13 {
            height: 20px;
            width: 169px;
            padding-bottom: 7px;
        }

        .auto-style14 {
            width: 120px;
            float: left;
        }


         /*view tab*/
         .Initial
        {
            display: block;
            padding: 4px 18px 4px 18px;
            margin-top:23px;
            float: left;
            height:40px;
             font: 14px/100% Arial, Helvetica, sans-serif;
            border-radius: 10px 10px 0px 0px;
            /*background: url("../img/InitialImage.png") no-repeat right top;*/
            background-color: #1f4f8a;
            margin-right: 5px;
            color: white;
            font-weight: bold;
        }
        .Initial:hover
        {
            color: white;
            background: url("../img/SelectedButton.png") no-repeat right top;
        }
        .Clicked {
            float: left;
            display: block;
            /*background: url("../img/SelectedButton.png") no-repeat right top;*/
            border-radius: 10px 10px 0px 0px;
            background-color: #7fa2cc;
            padding: 4px 18px 4px 18px;
            margin-top: 23px;
            font: 14px/100% Arial, Helvetica, sans-serif;
            height: 40px;
            color: Black;
            font-weight: bold;
            margin-right: 5px;
            color: white;
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





    <section class="content">
        <div class="row">
            <div class="col-lg-12">

               <div class="box box-solid shadowbox " style="border-radius: 15px;">
                    <div class="box-header with-border" style="height: 75px;">

                        <div style="padding-top: 0px; width: 120px; float: left;">
                            <div id="tabmenu">
                                <h3 class="box-title" style="color: #fff; padding-left: 10px;""><b>COMPANY</b></h3>
                            </div>
                        </div>
                      <div style="height: 60px; width: 650px; margin-left: 310px;"">


                            <asp:LinkButton ID="btnInsert" Height="40" CssClass="btn btn-app " runat="server" Style="left: 180px; background-color: #1f4f8a; color: white;" OnClick="btnInsert_Click" ValidationGroup="changest">
                     <i class="fas fa-save"></i>&nbsp Add
                            </asp:LinkButton>
                            <asp:LinkButton ID="btnSave" Height="40" CssClass="btn btn-app " runat="server" Style="left: 180px; background-color: #1f4f8a; color: white;" OnClick="btnSave_Click">
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
                             
                            <div class="tab-content">
                              
                       
                                       

                                    <div id="fieldbox" runat="server">
                                         <asp:MultiView ID="MainView" runat="server">
                                         <asp:View ID="View1" runat="server">

                                        <table id="tablecontent1" runat="server" style="width: 100%;">
                                            <tr>
                                                <td class="auto-style6"><strong>Serial No.</strong></td>
                                                <td class="auto-style7">
                                                    <div class="backcolorlbl">
                                                        <asp:Label ID="lblSerialNo" runat="server" Font-Bold="False" Font-Italic="False" ForeColor="Black"></asp:Label>
                                                    </div>
                                                </td>
                                                <td class="auto-style13"><strong>Company Name</strong></td>
                                                <td class="auto-style11">
                                                    <asp:TextBox ID="txtCompanyName" CssClass="form-control texthieht" Width="250px" runat="server" Font-Bold="False" Font-Italic="False" ForeColor="Black"></asp:TextBox>
                                                    <asp:RequiredFieldValidator runat="server" ID="Req" ControlToValidate="txtCompanyName" Display="None" ErrorMessage="<b> Missing Field</b><br />A Company Name is required." ValidationGroup="aaa" />
                                                    <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" TargetControlID="Req" runat="server" HighlightCssClass="vali" PopupPosition="BottomLeft"></ajaxToolkit:ValidatorCalloutExtender>
                                                </td>
                                                <td class="auto-style4"><strong>GOC Name</strong></td>
                                                <td class="auto-style1 ">
                                                    <asp:DropDownList ID="ddlGOCName" runat="server" Width="250px" OnSelectedIndexChanged="ddlGOCName_SelectedIndexChanged" AutoPostBack="true">
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" ControlToValidate="ddlGOCName" InitialValue="Please select..." Display="None" ErrorMessage="<b> Missing Field</b><br />A GOC Name is required." ValidationGroup="aaa" />
                                                    <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" TargetControlID="RequiredFieldValidator1" runat="server" HighlightCssClass="vali"></ajaxToolkit:ValidatorCalloutExtender>
                                                </td>
                                            </tr>
                                            <tr>
                                                
                                                <td class="auto-style10"><strong>Nature of Business</strong></td>
                                                <td class="auto-style9">
                                                    <asp:DropDownList ID="ddlNatureOFBusiness" runat="server" Width="250px">
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator3" ControlToValidate="ddlNatureOFBusiness" InitialValue="Please select..." Display="None" ErrorMessage="<b> Missing Field</b><br />A Nature of Business is required." ValidationGroup="aaa" />
                                                    <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender5" TargetControlID="RequiredFieldValidator3" runat="server" HighlightCssClass="vali"></ajaxToolkit:ValidatorCalloutExtender>
                                                </td>
                                                  <td class="auto-style5"><strong>Address 1</strong></td>
                                                <td class="auto-style12">
                                                    <div class="backcolorlbl">
                                                    <asp:TextBox ID="txtAddress1" CssClass="form-control texthieht" Width="250px" runat="server" Font-Bold="False" Font-Italic="False" ForeColor="Black"></asp:TextBox>
                                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator7" ControlToValidate="txtAddress1" Display="None" ErrorMessage="<b> Missing Field</b><br />An Address is required." ValidationGroup="aaa" />
                                                    <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender9" TargetControlID="RequiredFieldValidator7" runat="server" HighlightCssClass="vali" PopupPosition="BottomLeft"></ajaxToolkit:ValidatorCalloutExtender>

                                                    </div>
                                                </td>
                                                 <td class="auto-style4"><strong>Address 2</strong></td>
                                                <td class="auto-style1 ">
                                                    <asp:TextBox ID="txtAddress2" CssClass="form-control texthieht" Width="250px" runat="server" Font-Bold="False" Font-Italic="False" ForeColor="Black"></asp:TextBox>
                                                </td>

                                           
                                               

                                            </tr>

                                            <tr>
                                                <td class="auto-style10"><strong>Address 3</strong></td>
                                                <td class="auto-style9">
                                                    <asp:TextBox ID="txtAddress3" CssClass="form-control texthieht" Width="250px" runat="server" Font-Bold="False" Font-Italic="False" ForeColor="Black"></asp:TextBox>
                                                </td>
                                                <td class="auto-style10"><strong>Fax Number</strong></td>
                                                <td class="auto-style9">
                                                    <asp:TextBox ID="txtFaxNumber" CssClass="form-control texthieht" Width="250px" runat="server" Font-Bold="False" Font-Italic="False" ForeColor="Black"></asp:TextBox>
                                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" FilterType="Numbers" TargetControlID="txtFaxNumber" />
                                                </td>
                                                 <td class="auto-style5"><strong>Phone No.</strong></td>
                                                <td class="auto-style12">
                                                    <asp:TextBox ID="txtPhoneNo" CssClass="form-control texthieht" Width="250px" runat="server" Font-Bold="False" Font-Italic="False" ForeColor="Black" Style="margin-bottom: 0"></asp:TextBox>
                                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator8" ControlToValidate="txtPhoneNo" Display="None" ErrorMessage="<b> Missing Field</b><br />Phone No. is required." ValidationGroup="aaa" />
                                                    <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender10" TargetControlID="RequiredFieldValidator8" runat="server" HighlightCssClass="vali" PopupPosition="BottomLeft"></ajaxToolkit:ValidatorCalloutExtender>
                                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" FilterType="Numbers" TargetControlID="txtPhoneNo" />
                                                </td>

                                            </tr>
                                            <tr>
                                               

                                                <td class="auto-style4"><strong>Cell No.</strong></td>
                                                <td class="auto-style1 ">
                                                    <asp:TextBox ID="txtCellNo" CssClass="form-control texthieht" Width="250px" runat="server" Font-Bold="False" Font-Italic="False" ForeColor="Black"></asp:TextBox>
                                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Numbers" TargetControlID="txtCellNo" />
                                                </td>
                                                 <td class="auto-style10"><strong>Country</strong></td>
                                                <td class="auto-style9">
                                                    <asp:Panel ID="Panel1" runat="server">

                                                        <asp:DropDownList ID="ddlCountry" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlCountry_SelectedIndexChanged" Width="250px">
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator4" ControlToValidate="ddlCountry" InitialValue="Please select..." Display="None" ErrorMessage="<b> Missing Field</b><br />Please select the Country." ValidationGroup="aaa" />
                                                    <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender6" TargetControlID="RequiredFieldValidator4" runat="server" HighlightCssClass="vali"></ajaxToolkit:ValidatorCalloutExtender>
                                                    </asp:Panel>
                                                </td>
                                                 <td class="auto-style5"><strong>City</strong></td>
                                                <td class="auto-style12">

                                                    <asp:DropDownList ID="ddlCity" runat="server" AutoPostBack="True" Width="250px"></asp:DropDownList>
                                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator5" ControlToValidate="ddlCity" InitialValue="Please select..." Display="None" ErrorMessage="<b> Missing Field</b><br />Please select the City." ValidationGroup="aaa" />
                                                    <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender7" TargetControlID="RequiredFieldValidator5" runat="server" HighlightCssClass="vali"></ajaxToolkit:ValidatorCalloutExtender>
                                                </td>
                                            </tr>
                                           

                                            <tr>
                                                <td class="auto-style4"><strong>Zip Code</strong></td>
                                                <td class="auto-style1 ">
                                                    <asp:TextBox ID="txtZipCode" CssClass="form-control texthieht" Width="250px" runat="server" Font-Bold="False" Font-Italic="False" ForeColor="Black"></asp:TextBox>
                                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" FilterType="Numbers" TargetControlID="txtZipCode" />
                                                </td>
                                                <td class="auto-style5"><strong>Email</strong></td>
                                                <td class="auto-style12">
                                                    <asp:TextBox ID="txtEmail" CssClass="form-control texthieht" Width="250px" runat="server" Font-Bold="False" Font-Italic="False" ForeColor="Black"></asp:TextBox>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server"
                                                        ErrorMessage=" Invalid Email ID. Please Enter Valid ID like abc@erp.com " ControlToValidate="txtEmail"
                                                        Display="Dynamic" ForeColor="#FF3300" SetFocusOnError="True"
                                                        ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>


                                                </td>
                                                 <td class="auto-style4"><strong>Incorporation Date</strong></td>
                                                <td class="auto-style1 ">

                                                    <div style="display: inline-block">

                                                        <div class="input-group input-group-sm">
                                                            <asp:TextBox ID="txtIncorporationDate" CssClass="form-control texthieht" Width="220px" runat="server" Font-Bold="False" Font-Italic="False" ForeColor="Black" ValidationGroup="aaa"></asp:TextBox>
                                                            <ajaxToolkit:CalendarExtender ID="txtIncorporationDate_CalendarExtender" PopupButtonID="imgPopup" TargetControlID="txtIncorporationDate" Format="dd-MMM-yyyy" runat="server" />
                                                            <asp:RequiredFieldValidator runat="server" ID="Req2" ControlToValidate="txtIncorporationDate" Display="None" ErrorMessage="<b> Missing Field</b><br />A Date is required." ValidationGroup="aaa" />
                                                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender4" TargetControlID="Req2" runat="server" HighlightCssClass="vali"></ajaxToolkit:ValidatorCalloutExtender>
                                                            <span class="input-group-btn">
                                                                <%--<button type="button" class="btn btn-info btn-flat">Go!</button>--%>
                                                                <asp:ImageButton ID="imgPopup" CssClass="btn btn-flat" ImageUrl="img/calendar.png" ImageAlign="Bottom" runat="server" />
                                                            </span>
                                                        </div>



                                                    </div>





                                                </td>

                                            </tr>
                                        
                                            <tr>
                                                 <td class="auto-style10"><strong>Contact Person</strong></td>
                                                <td class="auto-style9">
                                                    <asp:TextBox ID="txtCOntactPerson" CssClass="form-control texthieht" Width="250px" runat="server" Font-Bold="False" Font-Italic="False" ForeColor="Black"></asp:TextBox>
                                                </td>
                                                <td class="auto-style5"><strong>NTN No</strong></td>
                                                <td class="auto-style12">
                                                    <asp:TextBox ID="txtNTNNo" CssClass="form-control texthieht" Width="250px" runat="server" Font-Bold="False" Font-Italic="False" ForeColor="Black"></asp:TextBox>
                                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server" FilterType="Numbers" TargetControlID="txtNTNNo" />
                                                </td>

                                                <td class="auto-style4"><strong>GST No.</strong></td>
                                                <td class="auto-style1 ">
                                                    <asp:TextBox ID="txtGSTNo" CssClass="form-control texthieht" Width="250px" runat="server" Font-Bold="False" Font-Italic="False" ForeColor="Black"></asp:TextBox>
                                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" runat="server" FilterType="Numbers" TargetControlID="txtGSTNo" />
                                                </td>
                                            </tr>
                                           
                                             <tr>
                                                <td class="auto-style10"><strong>Main Currency</strong></td>
                                                <td class="auto-style9">
                                                    <asp:DropDownList ID="ddlMainCurrency" runat="server" Width="250px">
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator6" ControlToValidate="ddlMainCurrency" InitialValue="Please select..." Display="None" ErrorMessage="<b> Missing Field</b><br />Please select the main currency." ValidationGroup="aaa" />
                                                    <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender8" TargetControlID="RequiredFieldValidator6" runat="server" HighlightCssClass="vali"></ajaxToolkit:ValidatorCalloutExtender>
                                                </td>
                                                <td class="auto-style5"><strong>Use Mutli Currency</strong></td>
                                                <td class="auto-style11">
                                                    <%--<asp:TextBox ID="txtUSeMultiCurrency" CssClass="form-control texthieht" Width="250px" runat="server"  Font-Bold="False" Font-Italic="False" ForeColor="Black"></asp:TextBox>--%>

                                                    <asp:RadioButton ID="rdb_UMC_Y" runat="server" GroupName="Hide" Text="Allow" />
                                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                       <asp:RadioButton ID="rdb_UMC_N" runat="server" GroupName="Hide" Text="Not Allow" />

                                                </td>
                                                  <td class="auto-style4"><strong>Depreciation Method</strong></td>
                                                <td class="auto-style1 ">
                                                    <asp:DropDownList ID="ddlDescriptionMethod" runat="server" Width="250px">
                                                    </asp:DropDownList>
                                                </td>

                                            </tr>
                                          
                                            <tr>
                                                
                                                <td class="auto-style10"><strong>Main Business</strong></td>
                                                <td class="auto-style9">
                                                    <asp:DropDownList ID="ddlMainBusiness" runat="server" Width="250px">
                                                        <asp:ListItem Value="1">Product</asp:ListItem>
                                                        <asp:ListItem Value="2">Service</asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                                <td class="auto-style5"><strong>By Product</strong></td>
                                                <td class="auto-style12">
                                                    <div class="backcolorlbl">
                                                        <asp:TextBox ID="txtBy_Product" CssClass="form-control texthieht" Width="250px" runat="server"></asp:TextBox>
                                                    </div>
                                                </td>
                                                <td class="auto-style13"><strong>Active </strong></td>
                                                <td class="auto-style11">
                                                    <asp:CheckBox ID="chkActive" runat="server" Text="Active" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="auto-style10"><strong>Inventory Method</strong></td>
                                                <td class="auto-style9">
                                                    <div class="backcolorlbl">
                                                        <asp:TextBox ID="txtInventoryMethod" CssClass="form-control texthieht" Width="250px" runat="server" Font-Bold="False" Font-Italic="False" ForeColor="Black"></asp:TextBox>
                                                    </div>
                                                </td>
                                                <td class="auto-style10"><strong>User Name</strong></td>
                                                <td class="auto-style9">
                                                    <asp:TextBox ID="txtUserName_Entry" CssClass="form-control texthieht" Width="250px" runat="server" Font-Bold="False" Font-Italic="False" ForeColor="Black"></asp:TextBox>
                                                </td>
                                                 <td class="auto-style13"><strong>User Name</strong></td>
                                                <td class="auto-style11">
                                                    <asp:TextBox ID="txtUserName_loginName" CssClass="form-control texthieht" Width="250px" runat="server" Font-Bold="False" Font-Italic="False" ForeColor="Black"></asp:TextBox>
                                                </td>
                                            </tr>

                                           

                                            <tr>
                                                <td class="auto-style4"><strong>Entry Date</strong></td>
                                                <td class="auto-style11">

                                                    <div style="display: inline-block">

                                                        <div class="input-group input-group-sm">
                                                            <asp:TextBox ID="txtEntry_Date" CssClass="form-control texthieht" Width="220px" runat="server" Font-Bold="False" Font-Italic="False" ForeColor="Black" ValidationGroup="aaa"></asp:TextBox>
                                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender1" PopupButtonID="ImageButton1" TargetControlID="txtEntry_Date" Format="dd-MMM-yyyy" runat="server" />
                                                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator2" ControlToValidate="txtEntry_Date" Display="None" ErrorMessage="<b> Missing Field</b><br />A Date is required." ValidationGroup="aaa" />
                                                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" TargetControlID="RequiredFieldValidator2" runat="server" HighlightCssClass="vali"></ajaxToolkit:ValidatorCalloutExtender>
                                                            <span class="input-group-btn">
                                                                <%--<button type="button" class="btn btn-info btn-flat">Go!</button>--%>
                                                                <asp:ImageButton ID="ImageButton1" CssClass="btn btn-flat" ImageUrl="img/calendar.png" ImageAlign="Bottom" runat="server" />
                                                            </span>
                                                        </div>



                                                    </div>


                                                </td>
                                                <td class="auto-style10"><strong>System Date</strong> </td>
                                                <td class="auto-style9">
                                                    <asp:TextBox ID="txtSystem_Date" CssClass="form-control texthieht" Width="250px" runat="server" Font-Bold="False" Font-Italic="False" ForeColor="Black"></asp:TextBox>

                                                </td>



                                            </tr>

                                        </table>
                                     </asp:View>
                                             <div id="aa" visible="false">
                                             <asp:View ID="View2" runat="server" >
                                

                                            <table id="tablecontent2" runat="server" style="width: 100%;">
                                                <tr>
                                                    <td class="auto-style4"><strong>Use Company COA</strong></td>
                                                    <td class="auto-style1 ">

                                                        <asp:Panel ID="Panel2" runat="server">

                                                            <asp:RadioButton ID="rdbCOmpanyCOA_YES" runat="server" Text="YES" GroupName="rdbcomapycoa" OnCheckedChanged="rdbCOmpanyCOA_YES_CheckedChanged" AutoPostBack="True" />
                                                            <asp:RadioButton ID="rdbCOmpanyCOA_NO" runat="server" Text="NO" GroupName="rdbcomapycoa" OnCheckedChanged="rdbCOmpanyCOA_NO_CheckedChanged" AutoPostBack="True" />
                                                        </asp:Panel>


                                                    </td>
                                                </tr>
                                                <tr>

                                                    <td class="auto-style10"><strong>No of Segment</strong></td>
                                                    <td class="auto-style9">
                                                        <asp:TextBox ID="txtNoOfCOASeg" CssClass="form-control texthieht" Width="250px" runat="server" Font-Bold="False" Font-Italic="False" ForeColor="Black" OnLoad="txtNoOfCOASeg_Load" AutoPostBack="true"></asp:TextBox>
                                                    </td>

                                                    <td class="auto-style10"><strong>Segment Separataor</strong></td>
                                                    <td class="auto-style9">
                                                        <div class="backcolorlbl">
                                                            <asp:TextBox ID="txtAccountSepartor" CssClass="form-control texthieht" Width="250px" runat="server" Font-Bold="False" Font-Italic="False" ForeColor="Black" AutoPostBack="true"></asp:TextBox>
                                                        </div>
                                                    </td>

                                                </tr>
                                                <tr>
                                                    <td class="auto-style5"><strong>Segment 1 Size</strong></td>
                                                    <td class="auto-style12">
                                                        <asp:TextBox ID="txtSegment_0_Size" CssClass="form-control texthieht" Width="250px" runat="server" Font-Bold="False" Font-Italic="False" ForeColor="Black" AutoPostBack="true"></asp:TextBox>
                                                    </td>

                                                    <td class="auto-style4"><strong>Segment 2 Size</strong></td>
                                                    <td class="auto-style1 ">
                                                        <asp:TextBox ID="txtSegment_1_Size" CssClass="form-control texthieht" Width="250px" runat="server" Font-Bold="False" Font-Italic="False" ForeColor="Black" AutoPostBack="true"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="auto-style10"><strong>Segment 3 Size</strong></td>
                                                    <td class="auto-style9">
                                                        <asp:TextBox ID="txtSegment_2_Size" CssClass="form-control texthieht" Width="250px" runat="server" Font-Bold="False" Font-Italic="False" ForeColor="Black" AutoPostBack="true"></asp:TextBox>

                                                    </td>
                                                    <td class="auto-style5"><strong>Segment 4 Size</strong></td>
                                                    <td class="auto-style12">
                                                        <asp:TextBox ID="txtSegment_3_Size" CssClass="form-control texthieht" Width="250px" runat="server" Font-Bold="False" Font-Italic="False" ForeColor="Black" AutoPostBack="true"></asp:TextBox>
                                                    </td>


                                                </tr>

                                                <tr>
                                                    <td class="auto-style4"><strong>Segment 5 Size</strong></td>
                                                    <td class="auto-style1 ">
                                                        <asp:TextBox ID="txtSegment_4_Size" CssClass="form-control texthieht" Width="250px" runat="server" Font-Bold="False" Font-Italic="False" ForeColor="Black" AutoPostBack="true"></asp:TextBox>
                                                    </td>
                                                    <td class="auto-style10"><strong>Segment 6 Size</strong></td>
                                                    <td class="auto-style9">
                                                        <asp:TextBox ID="txtSegment_5_Size" CssClass="form-control texthieht" Width="250px" runat="server" Font-Bold="False" Font-Italic="False" ForeColor="Black" AutoPostBack="true"></asp:TextBox>
                                                    </td>

                                                </tr>
                                                <tr>
                                                    <td class="auto-style5"><strong>Segment 7 Size</strong></td>
                                                    <td class="auto-style12">
                                                        <asp:TextBox ID="txtSegment_6_Size" CssClass="form-control texthieht" Width="250px" runat="server" AutoPostBack="true"></asp:TextBox>
                                                    </td>

                                                    <td class="auto-style4"><strong>Segment 8 Size</strong></td>
                                                    <td class="auto-style1 ">
                                                        <asp:TextBox ID="txtSegment_7_Size" CssClass="form-control texthieht" Width="250px" runat="server" Font-Bold="False" Font-Italic="False" ForeColor="Black" AutoPostBack="true"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="auto-style10"><strong>Segment 9 Size</strong></td>
                                                    <td class="auto-style9">
                                                        <asp:TextBox ID="txtSegment_8_Size" CssClass="form-control texthieht" Width="250px" runat="server" Font-Bold="False" Font-Italic="False" ForeColor="Black" AutoPostBack="true"></asp:TextBox>
                                                    </td>
                                                    <td class="auto-style5"><strong>Segment 10 Size</strong></td>
                                                    <td class="auto-style12">
                                                        <asp:TextBox ID="txtSegment_9_Size" CssClass="form-control texthieht" Width="250px" runat="server" Font-Bold="False" Font-Italic="False" ForeColor="Black" AutoPostBack="true"></asp:TextBox>
                                                    </td>


                                                </tr>
                                                <tr>
                                                    <td class="auto-style4"><strong>COA - Format</strong></td>
                                                    <td class="auto-style1 ">
                                                        <asp:TextBox ID="txtCOA_Format" CssClass="form-control texthieht" Width="250px" runat="server" Font-Bold="False" Font-Italic="False" ForeColor="Black"></asp:TextBox>
                                                        <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender1" TargetControlID="txtCOA_Format" ClearMaskOnLostFocus="false" runat="server" Mask="0" />
                                                    </td>
                                                </tr>
                                            </table>
                                       </asp:View>
                                                 </div>
                                </asp:MultiView>

                                    </div>
                                        
                                    <div id="Findbox" runat="server">
                                        <div class="box">

                                            <!-- /.box-header -->
                                            <div class="box-body" style="width: 100%; overflow: auto; height: 50%;">
                                                <%--CssClass="table table-bordered table-hover"--%>


                                                <asp:GridView ID="GridView1" runat="server" CssClass="table table-bordered table-hover tableFixHead" AutoGenerateColumns="False" DataKeyNames="CompId" OnSelectedIndexChanged="GridView1_SelectedIndexChanged" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Horizontal">
                                                    <Columns>

                                                        <asp:CommandField ShowSelectButton="True" ButtonType="Image" SelectImageUrl="~/dist/img/edit.png">
                                                            <HeaderStyle Width="20px" />
                                                        </asp:CommandField>
                                                        <asp:BoundField DataField="CompId" HeaderText="ID" ReadOnly="True" SortExpression="CompId" InsertVisible="False">
                                                            <HeaderStyle Width="20px" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="CompName" HeaderText="Company Name" SortExpression="CompName">
                                                            <HeaderStyle Width="500px" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="GOCName" HeaderText="Group Of Company Name" SortExpression="GOCName">
                                                            <HeaderStyle Width="500px" />
                                                        </asp:BoundField>
                                                         <asp:BoundField DataField="BusinessNature" HeaderText="Business Nature" SortExpression="BusinessNature">
                                                            <HeaderStyle Width="500px" />
                                                        </asp:BoundField>
                                                        <asp:CheckBoxField DataField="IsActive" HeaderText="Active" SortExpression="IsActive">
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
                                            <!-- /.box-body -->
                                        </div>

                                    </div>
                                    <div id="DeleteBox" runat="server">
                                        <div class="box">

                                            <!-- /.box-header -->
                                            <div class="box-body" style="width: 100%; overflow: auto; height: 50%;">
                                                <%--CssClass="table table-bordered table-hover"--%>


                                                <asp:GridView ID="GridView2" runat="server" CssClass="table table-bordered table-hover tableFixHead" AutoGenerateColumns="False" DataKeyNames="CompId" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Horizontal" OnSelectedIndexChanged="GridView2_SelectedIndexChanged" OnRowDataBound="GridView2_RowDataBound">
                                                    <Columns>

                                                        <asp:CommandField ShowSelectButton="True" ButtonType="Image" SelectImageUrl="~/dist/img/edit.png">
                                                            <HeaderStyle Width="20px" />
                                                        </asp:CommandField>
                                                        <asp:BoundField DataField="CompId" HeaderText="ID" ReadOnly="True" SortExpression="CompId" InsertVisible="False">
                                                            <HeaderStyle Width="20px" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="CompName" HeaderText="Company Name" SortExpression="CompName">
                                                            <HeaderStyle Width="500px" />
                                                        </asp:BoundField>
                                                         <asp:BoundField DataField="GOCName" HeaderText="Group Of Company Name" SortExpression="GOCName">
                                                            <HeaderStyle Width="500px" />
                                                        </asp:BoundField>
                                                         <asp:BoundField DataField="BusinessNature" HeaderText="Business Nature" SortExpression="BusinessNature">
                                                            <HeaderStyle Width="500px" />
                                                        </asp:BoundField>
                                                        <asp:CheckBoxField DataField="IsActive" HeaderText="Active" SortExpression="IsActive">
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
                                            <!-- /.box-body -->
                                        </div>

                                    </div>
                                    <div id="Editbox" runat="server">
                                        <div class="box">

                                            <!-- /.box-header -->
                                            <div class="box-body" style="width: 100%; overflow: auto; height: 50%;">
                                                <%--CssClass="table table-bordered table-hover"--%>


                                                <asp:GridView ID="GridView3" runat="server" CssClass="table table-bordered table-hover tableFixHead" AutoGenerateColumns="False" DataKeyNames="CompId" OnSelectedIndexChanged="GridView3_SelectedIndexChanged" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Horizontal" OnRowDataBound="GridView3_RowDataBound">
                                                    <Columns>

                                                        <asp:CommandField ShowSelectButton="True" ButtonType="Image" SelectImageUrl="~/dist/img/edit.png">
                                                            <HeaderStyle Width="20px" />
                                                        </asp:CommandField>
                                                        <asp:BoundField DataField="CompId" HeaderText="ID" ReadOnly="True" SortExpression="CompId" InsertVisible="False">
                                                            <HeaderStyle Width="20px" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="CompName" HeaderText="Company Name" SortExpression="CompName">
                                                            <HeaderStyle Width="500px" />
                                                        </asp:BoundField>
                                                         <asp:BoundField DataField="GOCName" HeaderText="Group Of Company Name" SortExpression="GOCName">
                                                            <HeaderStyle Width="500px" />
                                                        </asp:BoundField>
                                                         <asp:BoundField DataField="BusinessNature" HeaderText="Business Nature" SortExpression="BusinessNature">
                                                            <HeaderStyle Width="500px" />
                                                        </asp:BoundField>
                                                        <asp:CheckBoxField DataField="IsActive" HeaderText="Active" SortExpression="IsActive">
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
                                            <!-- /.box-body -->
                                        </div>

                                    </div>

                               
                                    
                                </div>
                                

                                <%--<div id="menu2" class="tab-pane fade">
                                     <h3>Menu 2</h3>
                                     <p>Some content in menu 2.</p>
                                     </div>--%>
                                
                            </div>
                        </div>
                        <!-- /.box-body -->
                        <div class="box-footer">

                            <asp:Label ID="Label1" runat="server" Text=""></asp:Label>

                        </div>
                        <!-- /.box-footer -->

                    </div>
                    <!-- /.box -->


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
                    <h4 class="modal-title">Edit </h4>
                </div>
                <div class="modal-body">

                    <table style="width: 100%;">
                        <tr>
                            <td><strong>Company Name </strong></td>
                            <td colspan="3">
                                <%--<asp:TextBox ID="txtEdit_CompName" CssClass="form-control gap" runat="server"></asp:TextBox>--%>
                                <asp:DropDownList ID="ddlEdit_CompName" CssClass="form-control gap select2"  Width="100%" runat="server"></asp:DropDownList>
                            </td>

                        </tr>
                        <tr>
                            <td><strong>GOC Name</strong> </td>
                            <td colspan="3">
                                <%--<asp:TextBox ID="txtEdit_GOC" CssClass="form-control gap" runat="server"></asp:TextBox>--%>
                                <asp:DropDownList ID="ddlEdit_GOCName" CssClass="form-control gap select2"  Width="100%" runat="server"></asp:DropDownList>
                            </td>


                        </tr>
                        <tr>
                            <td><strong>Nature of business</strong> </td>
                            <td colspan="3">
                                <%--<asp:TextBox ID="txtEdit_NOB" CssClass="form-control gap" runat="server"></asp:TextBox>--%>
                                <asp:DropDownList ID="txtEdit_NOB" CssClass="form-control gap select2"  Width="100%" runat="server"></asp:DropDownList>
                            </td>

                        </tr>
                        <tr>
                            <td><strong>NTN No.</strong> </td>
                            <td>
                                <asp:TextBox ID="txtEdit_NTN" CssClass="form-control gap" runat="server"></asp:TextBox>
                            </td>
                            <td><strong>&nbsp;&nbsp; GST No. </strong></td>
                            <td>
                                <asp:TextBox ID="txtEdit_GST" CssClass="form-control gap" runat="server"></asp:TextBox>
                            </td>

                        </tr>
                        <tr>
                            <td><strong>Entry Date </strong></td>
                            <td colspan="3">
                               <%-- <asp:TextBox ID="txtEdit_Ed" CssClass="form-control gap" runat="server"></asp:TextBox>--%>


                                <div style="display:inline-block">

                                                        <div class="input-group input-group-sm">
                                                            <asp:TextBox ID="txtEdit_Ed" CssClass="form-control texthieht" Width="420px" runat="server" Font-Bold="False" Font-Italic="False" ForeColor="Black"  ValidationGroup="aaa"></asp:TextBox>
                                                             <ajaxToolkit:CalendarExtender ID="CalendarExtender4" PopupButtonID="ImageButton4" TargetControlID="txtEdit_Ed" Format="dd-MMM-yyyy" runat="server" />
                                                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator11" ControlToValidate="txtEdit_Ed" Display="None" ErrorMessage="<b> Missing Field</b><br />A Date is required." ValidationGroup="aaa" />
                                                        <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender13" TargetControlID="RequiredFieldValidator11" runat="server" HighlightCssClass="vali"></ajaxToolkit:ValidatorCalloutExtender>
                                                            <span class="input-group-btn">
                                                                <%--<button type="button" class="btn btn-info btn-flat">Go!</button>--%>
                                                                <asp:ImageButton ID="ImageButton4" CssClass="btn btn-flat" ImageUrl="img/calendar.png" ImageAlign="Bottom" runat="server" />
                                                            </span>
                                                       </div>
                                                       
                                                    </div>

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
                    <h4 class="modal-title">Find</h4>
                </div>
                <div class="modal-body">

                    <table style="width: 100%;">
                        <tr>
                            <td><strong>Company Name </strong></td>
                            <td colspan="3">
                                <%--<asp:TextBox ID="txtFind_CN" CssClass="form-control gap" runat="server"></asp:TextBox>--%>
                                 <asp:DropDownList ID="ddlFind_CompName" CssClass="form-control gap select2"  Width="100%" runat="server"></asp:DropDownList>
                            </td>

                        </tr>
                        <tr>
                            <td><strong>GOC Name</strong> </td>
                            <td colspan="3">
                                <%--<asp:TextBox ID="txtEdit_GOC" CssClass="form-control gap" runat="server"></asp:TextBox>--%>
                                <asp:DropDownList ID="ddlFnd_GOC" CssClass="form-control gap select2"  Width="100%" runat="server"></asp:DropDownList>
                            </td>


                        </tr>
                        <tr>
                            <td><strong>Nature of business</strong> </td>
                            <td colspan="3">
                                <%--<asp:TextBox ID="txtFind_NOB" CssClass="form-control gap" runat="server"></asp:TextBox>--%>
                                 <asp:DropDownList ID="txtFind_NOB" CssClass="form-control gap select2"  Width="100%" runat="server"></asp:DropDownList>
                            </td>

                        </tr>
                        <tr>
                            <td><strong>NTN No.</strong> </td>
                            <td>
                                <asp:TextBox ID="txtFind_NTN" CssClass="form-control gap" runat="server"></asp:TextBox>
                            </td>
                            <td><strong>&nbsp;&nbsp; GST No. </strong></td>
                            <td>
                                <asp:TextBox ID="txtFind_GST" CssClass="form-control gap" runat="server"></asp:TextBox>
                            </td>

                        </tr>
                        <tr>
                            <td><strong>Entry Date </strong></td>
                            <td colspan="3">
                                <%--<asp:TextBox ID="txtFind_ED" CssClass="form-control gap" runat="server"></asp:TextBox>--%>
                              
                                  <div style="display:inline-block">

                                                        <div class="input-group input-group-sm">
                                                            <asp:TextBox ID="txtFind_ED" CssClass="form-control texthieht" Width="420px" runat="server" Font-Bold="False" Font-Italic="False" ForeColor="Black"  ValidationGroup="aaa"></asp:TextBox>
                                                             <ajaxToolkit:CalendarExtender ID="CalendarExtender3" PopupButtonID="ImageButton3" TargetControlID="txtFind_ED" Format="dd-MMM-yyyy" runat="server" />
                                                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator10" ControlToValidate="txtFind_ED" Display="None" ErrorMessage="<b> Missing Field</b><br />A Date is required." ValidationGroup="aaa" />
                                                        <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender12" TargetControlID="RequiredFieldValidator10" runat="server" HighlightCssClass="vali"></ajaxToolkit:ValidatorCalloutExtender>
                                                            <span class="input-group-btn">
                                                                <%--<button type="button" class="btn btn-info btn-flat">Go!</button>--%>
                                                                <asp:ImageButton ID="ImageButton3" CssClass="btn btn-flat" ImageUrl="img/calendar.png" ImageAlign="Bottom" runat="server" />
                                                            </span>
                                                       </div>
                                                       
                                                    </div>


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
                            <td><strong>Company Name </strong></td>
                            <td colspan="3">
                                <%--<asp:TextBox ID="txtDelete_CompName" CssClass="form-control gap" runat="server"></asp:TextBox>--%>
                                <asp:DropDownList ID="ddlDel_CompName" CssClass="form-control gap select2" Width="100%" runat="server"></asp:DropDownList>
                            </td>

                        </tr>
                        <tr>
                            <td><strong>GOC Name</strong> </td>
                            <td colspan="3">
                                <%--<asp:TextBox ID="txtEdit_GOC" CssClass="form-control gap" runat="server"></asp:TextBox>--%>
                                <asp:DropDownList ID="ddlDelete_GOCName" CssClass="form-control gap select2"  Width="100%" runat="server"></asp:DropDownList>
                            </td>


                        </tr>
                        <tr>
                            <td><strong>Nature of business</strong> </td>
                            <td colspan="3">
                                <%--<asp:TextBox ID="txtDelete_NOB" CssClass="form-control gap" runat="server"></asp:TextBox>--%>
                                <asp:DropDownList ID="txtDelete_NOB" CssClass="form-control gap select2"  Width="100%" runat="server"></asp:DropDownList>
                            </td>

                        </tr>
                        <tr>
                            <td><strong>NTN No. </strong></td>
                            <td>
                                <asp:TextBox ID="txtDelete_NTN" CssClass="form-control gap" runat="server"></asp:TextBox>
                            </td>
                            <td><strong>&nbsp;&nbsp; GST No. </strong></td>
                            <td>
                                <asp:TextBox ID="txtDelete_GST" CssClass="form-control gap" runat="server"></asp:TextBox>
                            </td>

                        </tr>
                        <tr>
                            <td><strong>Entry Date  </strong></td>
                            <td colspan="3">
                                <%--<asp:TextBox ID="" CssClass="form-control gap" runat="server"></asp:TextBox>--%>




                                 <div style="display:inline-block">

                                                        <div class="input-group input-group-sm">
                                                            <asp:TextBox ID="txtDelete_ED" CssClass="form-control texthieht" Width="420px" runat="server" Font-Bold="False" Font-Italic="False" ForeColor="Black"  ValidationGroup="aaa"></asp:TextBox>
                                                             <ajaxToolkit:CalendarExtender ID="CalendarExtender2" PopupButtonID="ImageButton2" TargetControlID="txtDelete_ED" Format="dd-MMM-yyyy" runat="server" />
                                                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator9" ControlToValidate="txtDelete_ED" Display="None" ErrorMessage="<b> Missing Field</b><br />A Date is required." ValidationGroup="aaa" />
                                                        <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender11" TargetControlID="RequiredFieldValidator9" runat="server" HighlightCssClass="vali"></ajaxToolkit:ValidatorCalloutExtender>
                                                            <span class="input-group-btn">
                                                                <%--<button type="button" class="btn btn-info btn-flat">Go!</button>--%>
                                                                <asp:ImageButton ID="ImageButton2" CssClass="btn btn-flat" ImageUrl="img/calendar.png" ImageAlign="Bottom" runat="server" />
                                                            </span>
                                                       </div>
                                                       
                                                    </div>



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

                    <asp:Button ID="btn_delete" CssClass="btn btn-primary" runat="server" Text="Search" OnClick="btn_delete_Click" />

                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>

    <script type="text/javascript">
        $('a[data-toggle="tab"]').on('shown.bs.tab', function (e) {
            var target = $(e.target).attr("href") // activated tab
            alert(target);
        });
    </script>


    <script src="https://unpkg.com/sweetalert/dist/sweetalert.min.js"></script>
</asp:Content>

