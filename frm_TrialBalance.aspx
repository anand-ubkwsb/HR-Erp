<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="frm_TrialBalance.aspx.cs" Inherits="frm_TrailBalance" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script>
        function ConfirmForUpdate() {
           return confirm("Do you Want to Update Current Trial balance?");
        }

        function alertme() {
            swal("Data Inserted Successfuly!", "record has been saved!!!", "success")
                .then(function () {
                    window.location = "frm_CostCenter.aspx?UserID=" + userid + "&UsergrpID=" + UserGrpID + "&fiscaly=" + fiscal + "&FormID=" + FormID + "";
                });

        }

        function Editalert() {
            swal("Data Update Successfuly!", "record has been saved!!!", "success")
                .then(function () {
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


        .table-flex {
            display: flex;
            flex-flow: column wrap;
        }

        .container-button {
        display:flex;
        flex-flow:row wrap;
        justify-content:center;
        
        }

        .container-table {
        display:flex;
        max-height:500px;
        overflow-y:scroll;
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
            display: inline-block
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

        .table_height {
            overflow-y: scroll;
            max-height: 500px;
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

        .tablerow {
            height: 40px;
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
                                <h3 class="box-title" style="color: #fff; padding-left: 10px;"><strong>Trail Balance</strong></h3>
                            </div>
                        </div>

                        <div style="height: 60px; width: 650px;">
                            <asp:LinkButton ID="btnGenerate" Height="40" CssClass="btn btn-app " runat="server" Style="left: 180px; background-color: #1f4f8a; color: white;" ValidationGroup="changest" OnClick="btnInsert_Click">
                     <i class="fas fa-save"></i>&nbsp Generate Trial Balance
                            </asp:LinkButton>

                            <asp:LinkButton ID="btnBack" Height="40" CssClass="btn btn-app " runat="server" AutoPostBack='True' Style="left: 180px; background-color: #1f4f8a; color: white;" ValidationGroup="changest" OnClick="btnBack_Click">
                     <i class="fas fa-save"></i>&nbsp Back
                            </asp:LinkButton>

                            <asp:LinkButton ID="SaveInDataBase" Height="40" CssClass="btn btn-app " runat="server" AutoPostBack='True' Style="left: 180px; background-color: #1f4f8a; color: white;" ValidationGroup="changest" OnClick="btnSave_Click">
                     <i class="fas fa-save"></i>&nbsp Save In DataBase
                            </asp:LinkButton>
                        </div>




                        <%--<div style="height: 60px; width: 650px; margin-left: 310px;"">
                            <asp:LinkButton ID="btnGenerate" Height="40" CssClass="btn btn-app " runat="server" Style="left: 180px; background-color: #1f4f8a; color: white;" OnClick="btnGenerate_Click" ValidationGroup="changest">
                     <i class="fas fa-save"></i>&nbsp Generate Trail Balance
                            </asp:LinkButton>
                            
                        </div>--%>
                    </div>


                    <div id="modal_DataDiv" runat="server">
                        <div class="form-row">
                            <div class="box-body" style="background-color: #cddbf2;">
                                <div id="fieldbox" runat="server">
                                    <div class="container-fluid">
                                        <div class="row">
                                            <div class="col-sm-6">

                                                <div class="form-group">
                                                    <label><b>Group Of Companies:</b></label>&nbsp
                                                <b>
                                                    <asp:Label ID="GroupOfCompaniesText" CssClass="" runat="server" Text="Label"></asp:Label>
                                                </b>
                                                </div>

                                                <div class="form-group">
                                                    <label class="col-sm-2"><b>Branch:</b></label>
                                                    <b>
                                                        <asp:DropDownList Style="width: 50%;" ID="ddlBranch" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged"></asp:DropDownList>
                                                        <asp:Label ID="BranchText" CssClass="" runat="server" Text="Label"></asp:Label>
                                                    </b>
                                                </div>

                                                <div class="form-group">
                                                    <label class="col-sm-2"><b>Heading Level:</b></label>
                                                    <b>
                                                        <asp:DropDownList Style="width: 50%;" ID="ddlHeadingLevel" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlHeadingLevel_SelectedIndexChanged"></asp:DropDownList>
                                                    </b>
                                                </div>


                                            </div>

                                            <div class="col-sm-6">
                                                <div class="form-group">
                                                    <label class="col-sm-2"><b>Company:</b></label>&nbsp
                                                <b>
                                                    <asp:DropDownList Style="width: 50%;" ID="ddlCompany" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlCompany_SelectedIndexChanged"></asp:DropDownList>
                                                    <asp:Label ID="CompanyText" runat="server"></asp:Label></b>

                                                </div>

                                                <div class="form-group">
                                                    <label class="col-sm-2"><b>FiscalYear:</b></label>&nbsp
                                                <b>
                                                    <asp:DropDownList Style="width: 50%;" ID="ddlFiscalYear" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlFiscal_SelectedIndexChanged"></asp:DropDownList>
                                                    <asp:Label ID="FiscalYearText" CssClass="" runat="server" Text="Fiscal"></asp:Label>
                                                </b>
                                                </div>

                                            </div>
                                        </div>
                                    </div>

                                </div>

                            </div>
                        </div>
                    </div>

                    


                    <div id="modal_TrialBalanceDiv" runat="server">
                        <div class="box-body" style="background-color: #cddbf2;">
                                    <asp:PlaceHolder ID="Table_Div" runat="server"></asp:PlaceHolder>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

        <div id="Error_Message" runat="server">
            <asp:Label ID="Error_Label" runat="server" Text="Label"></asp:Label>
        </div>
    </section>
        <script src="https://unpkg.com/sweetalert/dist/sweetalert.min.js"></script>

</asp:Content>



