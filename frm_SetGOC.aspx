<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="frm_SetGOC.aspx.cs" Inherits="frm_SetGOC" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script>
        function alertme() {
            swal("Insert Successfuly!", "record has been saved!!!", "success")
                .then(function () {
                    window.location = "frm_SetGOC.aspx?UserID=" + userid + "&UsergrpID=" + UserGrpID + "&fiscaly=" + fiscal + "&FormID=" + FormID + "";
            });
           
        }

        function Editalert() {
            swal("Update Successfuly!", "record has been saved!!!", "success")
                .then(function () {
                    window.location = "frm_SetGOC.aspx?UserID=" + userid + "&UsergrpID=" + UserGrpID + "&fiscaly=" + fiscal + "&FormID=" + FormID + "";
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
                    window.location = "frm_SetGOC.aspx?UserID=" + userid + "&UsergrpID=" + UserGrpID + "&fiscaly=" + fiscal + "&FormID=" + FormID + "";
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

        .auto-style3 {
            width: 173px;
            padding-bottom: 7px;
        }

        .texthieht {
            height: 25px;
        }

        .auto-style4 {
            height: 20px;
            width: 239px;
            padding-bottom: 7px;
        }

        .auto-style5 {
            width: 239px;
            padding-bottom: 7px;
        }

        .auto-style6 {
            width: 173px;
        }

        .auto-style7 {
            height: 20px;
            padding-bottom: 7px;
            width: 296px;
        }

        .auto-style8 {
            padding-bottom: 7px;
            width: 296px;
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

    <%--<asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>--%>
   

    
    <section class="content">
        <div class="row">
            <div class="col-lg-12">

                <div class="box box-solid shadowbox " style="border-radius: 15px;">
                    <div class="box-header with-border" style="height: 75px;">

                        <div style="padding-top: 0px; width: 120px; float: left;">
                            <div id="tabmenu">
                                <h3 class="box-title" style="color: #fff; padding-left: 10px;""><b>Group of Company</b></h3>
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
                                                <td class="auto-style6"><strong>Goc ID</strong></td>
                                                <td class="auto-style7">
                                                    <div class="backcolorlbl">
                                                        <asp:Label ID="lblSerialNo" runat="server" Font-Bold="False" Font-Italic="False" ForeColor="Black"></asp:Label>
                                                    </div>
                                                </td>
                                                <td class="auto-style4"><strong>Group of Company Name</strong></td>
                                                <td class="auto-style1 ">
                                                    <asp:TextBox ID="txtGOCName" CssClass="form-control texthieht" Width="250px" runat="server" Font-Bold="False" Font-Italic="False" ForeColor="Black"></asp:TextBox>
                                                    <asp:RequiredFieldValidator runat="server" ID="Req2" ControlToValidate="txtGOCName" Display="None" ErrorMessage="<b> Missing Field</b><br />A GOC NAme is required." ValidationGroup="aaa" />
                                                    <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender4" TargetControlID="Req2" runat="server" HighlightCssClass="vali" PopupPosition="BottomLeft"></ajaxToolkit:ValidatorCalloutExtender>
                                                </td>
                                                <td class="auto-style3"><strong>Registered Office</strong></td>
                                                <td class="auto-style8">
                                                    <asp:TextBox ID="txtRegisteredOffice" CssClass="form-control texthieht" Width="250px" runat="server" Font-Bold="False" Font-Italic="False" ForeColor="Black"></asp:TextBox>
                                                </td>
                                            </tr>
                                          
                                            <tr>
                                                <td class="auto-style5"><strong>Incorporation Date </strong></td>
                                                <td class="padspace">
                                                    
                                                        <%--<asp:TextBox ID="txtIncorporationDate" CssClass="form-control texthieht" Width="250px" runat="server" Font-Bold="False" Font-Italic="False" ForeColor="Black"></asp:TextBox>
                                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtIncorporationDate" Format="dd-MMM-yyyy" />--%>




                                                          <div style="display:inline-block">

                                                        <div class="input-group input-group-sm">
                                                            <asp:TextBox ID="txtIncorporationDate" CssClass="form-control texthieht" Width="220px" runat="server" Font-Bold="False" Font-Italic="False" ForeColor="Black"  ValidationGroup="aaa"></asp:TextBox>
                                                             <ajaxToolkit:CalendarExtender ID="CalendarExtender1" PopupButtonID="ImageButton1" TargetControlID="txtIncorporationDate" Format="dd-MMM-yyyy" runat="server" />
                                                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" ControlToValidate="txtIncorporationDate" Display="None" ErrorMessage="<b> Missing Field</b><br />A Date is required." ValidationGroup="aaa" />
                                                        <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" TargetControlID="RequiredFieldValidator1" runat="server" HighlightCssClass="vali"></ajaxToolkit:ValidatorCalloutExtender>
                                                            <span class="input-group-btn">
                                                                <%--<button type="button" class="btn btn-info btn-flat">Go!</button>--%>
                                                                <asp:ImageButton ID="ImageButton1" CssClass="btn btn-flat" ImageUrl="img/calendar.png" ImageAlign="Bottom" runat="server" />
                                                            </span>
                                                       </div>


                                                       
                                                    </div>


                                                </td>
                                                <td class="auto-style3"><strong>Country</strong></td>
                                                <td class="auto-style8">
                                                    <asp:DropDownList ID="ddlCountry"  CssClass="form-control select2" runat="server" Width="250px" OnSelectedIndexChanged="ddlCountry_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>
                                                </td>
                                                <td class="auto-style5"><strong>City</strong></td>
                                                <td class="padspace">

                                                    <asp:DropDownList ID="ddlCity"  CssClass="form-control select2" runat="server" AutoPostBack="True" Width="250px"></asp:DropDownList>

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

                                                <td class="auto-style5"><strong>No of Segments</strong></td>
                                                <td class="padspace">
                                                    <asp:TextBox ID="txtNoofSegments" CssClass="form-control texthieht" Width="250px" runat="server" Font-Bold="False" Font-Italic="False" ForeColor="Black" AutoPostBack="True" OnLoad="txtNoofSegments_Load"></asp:TextBox>
                                                </td>
                                                <td class="auto-style3"><strong>Account Separtor</strong></td>
                                                <td class="auto-style8">
                                                    <asp:TextBox ID="txtAccountSepartor" CssClass="form-control texthieht" Width="250px" runat="server" Font-Bold="False" Font-Italic="False" ForeColor="Black"></asp:TextBox>
                                                </td>
                                            </tr>

                                           
                                            <tr>
                                                
                                                <td class="auto-style5"><strong>Segment 1 Size</strong></td>
                                                <td class="padspace">
                                                    <asp:TextBox ID="txtSegment_0_Size" CssClass="form-control texthieht" Width="250px" runat="server" Font-Bold="False" Font-Italic="False" ForeColor="Black" MaxLength="3" AutoPostBack="true"></asp:TextBox>
                                                </td>
                                                <td class="auto-style3"><strong>Segment 2 Size</strong></td>
                                                <td class="auto-style8">
                                                    <asp:TextBox ID="txtSegment_1_Size" CssClass="form-control texthieht" Width="250px" runat="server" Font-Bold="False" Font-Italic="False" ForeColor="Black" MaxLength="3" AutoPostBack="true"></asp:TextBox>
                                                </td>
                                                <td class="auto-style5"><strong>Segment 3 Size</strong></td>
                                                <td class="padspace">
                                                    <asp:TextBox ID="txtSegment_2_Size" CssClass="form-control texthieht" Width="250px" runat="server" Font-Bold="False" Font-Italic="False" ForeColor="Black" MaxLength="6" AutoPostBack="true"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="auto-style3"><strong>Segment 4 Size</strong></td>
                                                <td class="auto-style8">
                                                    <asp:TextBox ID="txtSegment_3_Size" CssClass="form-control texthieht" Width="250px" runat="server" Font-Bold="False" Font-Italic="False" ForeColor="Black" MaxLength="6" AutoPostBack="true"></asp:TextBox>
                                                </td>
                                                <td class="auto-style5"><strong>Segment 5 Size</strong></td>
                                                <td>
                                                    <asp:TextBox ID="txtSegment_4_Size" CssClass="form-control texthieht" Width="250px" runat="server" Font-Bold="False" Font-Italic="False" ForeColor="Black" MaxLength="6" AutoPostBack="true"></asp:TextBox>
                                                </td>
                                                <td class="auto-style3"><strong>Segment 6 Size</strong></td>
                                                <td class="auto-style8">
                                                    <asp:TextBox ID="txtSegment_5_Size" CssClass="form-control texthieht" Width="250px" runat="server" Font-Bold="False" Font-Italic="False" ForeColor="Black" MaxLength="6" AutoPostBack="true"></asp:TextBox>
                                                </td>
                                            </tr>
                                           
                                            <tr>
                                                 <td class="auto-style5"><strong>Segment 7 Size</strong></td>
                                                <td class="padspace">
                                                    <div class="backcolorlbl">
                                                        <asp:TextBox ID="txtSegment_6_Size" CssClass="form-control texthieht" Width="250px" runat="server" MaxLength="6" AutoPostBack="true"></asp:TextBox>
                                                    </div>
                                                </td>
                                                <td class="auto-style3"><strong>Segment 8 Size</strong></td>
                                                <td class="auto-style8">
                                                    <div class="backcolorlbl">
                                                        <asp:TextBox ID="txtSegment_7_Size" CssClass="form-control texthieht" Width="250px" runat="server" Font-Bold="False" Font-Italic="False" ForeColor="Black" MaxLength="6" AutoPostBack="true"></asp:TextBox>
                                                    </div>
                                                </td>
                                                <td class="auto-style5"><strong>Segment 9 Size</strong></td>
                                                <td class="padspace">
                                                    <asp:TextBox ID="txtSegment_8_Size" CssClass="form-control texthieht" Width="250px" runat="server" Font-Bold="False" Font-Italic="False" ForeColor="Black" MaxLength="6" AutoPostBack="true"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="auto-style5"><strong>Segment 10 Size</strong></td>
                                                <td class="padspace">
                                                    <asp:TextBox ID="txtSegment_9_Size" CssClass="form-control texthieht" Width="250px" runat="server" Font-Bold="False" Font-Italic="False" ForeColor="Black" MaxLength="6" AutoPostBack="true"></asp:TextBox>

                                                </td>
                                                <td class="auto-style3"><strong>COA Format</strong></td>
                                                <td class="auto-style8">
                                                    <asp:TextBox ID="txtCOA_Format" CssClass="form-control texthieht" Width="250px" runat="server" Font-Bold="False" Font-Italic="False" ForeColor="Black"></asp:TextBox>
                                                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender1" TargetControlID="txtCOA_Format" Mask="0" ClearMaskOnLostFocus="false" runat="server" />

                                                </td>
                                                <td class="auto-style3"><strong>Group with GOC</strong></td>
                                                <td class="padspace">

                                                    <asp:DropDownList ID="ddlGrpGOC" CssClass="form-control select2" runat="server" AutoPostBack="True" Width="250px"></asp:DropDownList>

                                                </td>


                                                <td class="auto-style5" visible="false"><strong>Different COA for Branches</strong></td>
                                                  <%--<asp:TextBox ID="txtDifCOA_for_Branches" CssClass="form-control texthieht" Width="250px" runat="server" Font-Bold="False" Font-Italic="False" ForeColor="Black"></asp:TextBox>--%>
                                                    <td class="auto-style11" visible="false">
                                                        <asp:RadioButton ID="rdb_DIFFCoaBranch_Y" runat="server" GroupName=" Multicurrency" Text="YES" />
                                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                         <asp:RadioButton ID="rdb_DIFFCoaBranch_N" runat="server" GroupName=" Multicurrency" Text="NO" />

                                                    </td>
                                            </tr>

                                            <tr>
                                                <td class="auto-style3" visible="false"><strong>No of Branches</strong></td>
                                                <td class="auto-style8" visible="false">
                                                    <asp:TextBox ID="txtNo_of_Branches" CssClass="form-control texthieht" Width="250px" runat="server" Font-Bold="False" Font-Italic="False" ForeColor="Black"></asp:TextBox>
                                                </td>
                                                <td class="auto-style5"><strong>User Name</strong></td>
                                                <td class="padspace">
                                                    <asp:TextBox ID="txtUser_Name" CssClass="form-control texthieht" Width="250px" runat="server" Font-Bold="False" Font-Italic="False" ForeColor="Black"></asp:TextBox>
                                                </td>
                                                <td class="auto-style3"><strong>Entry Date</strong></td>
                                                <td class="auto-style8">
                                                    <%--<asp:TextBox ID="txtEntry_Date" CssClass="form-control texthieht" Width="250px" runat="server" Font-Bold="False" Font-Italic="False" ForeColor="Black"></asp:TextBox>
                                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txtEntry_Date" Format="dd-MMM-yyyy" />--%>

                                                    <div style="display:inline-block">

                                                        <div class="input-group input-group-sm">
                                                            <asp:TextBox ID="txtEntry_Date" CssClass="form-control texthieht" Width="220px" runat="server" Font-Bold="False" Font-Italic="False" ForeColor="Black"  ValidationGroup="aaa"></asp:TextBox>
                                                             <ajaxToolkit:CalendarExtender ID="CalendarExtender3" PopupButtonID="ImageButton2" TargetControlID="txtEntry_Date" Format="dd-MMM-yyyy" runat="server" />
                                                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator2" ControlToValidate="txtEntry_Date" Display="None" ErrorMessage="<b> Missing Field</b><br />A Date is required." ValidationGroup="aaa" />
                                                        <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" TargetControlID="RequiredFieldValidator2" runat="server" HighlightCssClass="vali"></ajaxToolkit:ValidatorCalloutExtender>
                                                            <span class="input-group-btn">
                                                                <%--<button type="button" class="btn btn-info btn-flat">Go!</button>--%>
                                                                <asp:ImageButton ID="ImageButton2" CssClass="btn btn-flat" ImageUrl="img/calendar.png" ImageAlign="Bottom" runat="server" />
                                                            </span>
                                                       </div>


                                                       
                                                    </div>





                                                </td>
                                                 <td class="auto-style5"><strong>System Date</strong></td>
                                                <td class="padspace">
                                                    <asp:TextBox ID="txtSystem_Date" CssClass="form-control texthieht" Width="250px" runat="server" Font-Bold="False" Font-Italic="False" ForeColor="Black"></asp:TextBox>
                                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtSystem_Date" Format="dd-MMM-yyyy" />
                                                </td>
                                            </tr>
                                          
                                        </table>

                                    
                            </div>

                            <div id="Findbox" runat="server">
                                <div class="box">

                                    <!-- /.box-header -->
                                    <div class="box-body" style="width: 100%; overflow: auto; height: 50%;">
                                        <%--CssClass="table table-bordered table-hover"--%>


                                        <asp:GridView ID="GridView1" runat="server" CssClass="table table-bordered table-hover tableFixHead" AutoGenerateColumns="False" DataKeyNames="GocId" OnSelectedIndexChanged="GridView1_SelectedIndexChanged" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Horizontal" >
                                            <Columns>
                                                <asp:CommandField ButtonType="Image" SelectImageUrl="~/dist/img/edit.png" ShowSelectButton="True">
                                                    <HeaderStyle Width="50px" />
                                                    </asp:CommandField>
                                                <asp:BoundField DataField="GocId" HeaderText="ID" ReadOnly="True" SortExpression="GocId" InsertVisible="False" >
                                                <HeaderStyle Width="20px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="GOCName" HeaderText="Group Of Company Name" SortExpression="GOCName">
                                                <HeaderStyle Width="500px" />
                                                </asp:BoundField>
                                                <asp:CheckBoxField DataField="IsActive" HeaderText="Active" SortExpression="IsActive" >
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


                                        <asp:GridView ID="GridView2" runat="server" CssClass="table table-bordered table-hover tableFixHead" AutoGenerateColumns="False" DataKeyNames="GocId" OnSelectedIndexChanged="GridView2_SelectedIndexChanged" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Horizontal" OnRowDataBound="GridView2_RowDataBound" >
                                             <Columns>
                                                <asp:CommandField ButtonType="Image" SelectImageUrl="~/dist/img/edit.png" ShowSelectButton="True">
                                                    <HeaderStyle Width="50px" />
                                                    </asp:CommandField>
                                                <asp:BoundField DataField="GocId" HeaderText="ID" ReadOnly="True" SortExpression="GocId" InsertVisible="False" >
                                                <HeaderStyle Width="20px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="GOCName" HeaderText="Group Of Company Name" SortExpression="GOCName">
                                                <HeaderStyle Width="500px" />
                                                </asp:BoundField>
                                                <asp:CheckBoxField DataField="IsActive" HeaderText="Active" SortExpression="IsActive" >
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


                                        <asp:GridView ID="GridView3" runat="server" CssClass="table table-bordered table-hover tableFixHead" AutoGenerateColumns="False" DataKeyNames="GocId" OnSelectedIndexChanged="GridView3_SelectedIndexChanged" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Horizontal" OnRowDataBound="GridView3_RowDataBound">
                                             <Columns>
                                                <asp:CommandField ButtonType="Image" SelectImageUrl="~/dist/img/edit.png" ShowSelectButton="True">
                                                    <HeaderStyle Width="50px" />
                                                    </asp:CommandField>
                                                <asp:BoundField DataField="GocId" HeaderText="ID" ReadOnly="True" SortExpression="GocId" InsertVisible="False" >
                                                <HeaderStyle Width="20px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="GOCName" HeaderText="Group Of Company Name" SortExpression="GOCName">
                                                <HeaderStyle Width="500px" />
                                                </asp:BoundField>
                                                <asp:CheckBoxField DataField="IsActive" HeaderText="Active" SortExpression="IsActive" >
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
                            <td><strong>GOC Name</strong> </td>
                            <td>
                                <%--<asp:TextBox ID="txtEdit_GOCName" CssClass="form-control gap" runat="server"></asp:TextBox>--%>
                                <asp:DropDownList ID="txtEdit_GOCName" CssClass="form-control select2" Width="100%" runat="server"></asp:DropDownList>
                            </td>

                        </tr>
                        <tr>
                            <td><strong>Entry Date</strong> </td>
                            <td>
                                <asp:TextBox ID="txtEdit_EntryD" CssClass="form-control gap" runat="server"></asp:TextBox>
                            </td>

                        </tr>
                        <tr>
                            <td><strong>No of COA Segement :</strong> </td>
                            <td>
                                <asp:TextBox ID="txtEdit_NOofSeg" CssClass="form-control gap" runat="server"></asp:TextBox>
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
                            <td><strong>GOC Name</strong> </td>
                            <td>
                                <%--<asp:TextBox ID="txtFind_GOC" CssClass="form-control gap" runat="server"></asp:TextBox>--%>
                                <asp:DropDownList ID="txtFind_GOC" CssClass="form-control select2" Width="100%" runat="server"></asp:DropDownList>
                            </td>

                        </tr>
                        <tr>
                            <td><strong>Entry Date</strong> </td>
                            <td>
                                <asp:TextBox ID="txtFind_ED" CssClass="form-control gap" runat="server"></asp:TextBox>
                            </td>

                        </tr>
                        <tr>
                            <td><strong>No of COA Segement :</strong> </td>
                            <td>
                                <asp:TextBox ID="txtFind_COASEg" CssClass="form-control gap" runat="server"></asp:TextBox>
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
                    <h4 class="modal-title">Delete </h4>
                </div>
                <div class="modal-body">

                    <table style="width: 100%;">
                        <tr>
                            <td><strong>GOC Name</strong> </td>
                            <td>
                                <%--<asp:TextBox ID="txtDelete_GOC" CssClass="form-control gap" runat="server"></asp:TextBox>--%>
                                <asp:DropDownList ID="txtDelete_GOC" CssClass="form-control select2" Width="100%" runat="server"></asp:DropDownList>
                            </td>

                        </tr>
                        <tr>
                            <td><strong>Entry Date</strong> </td>
                            <td>
                                <asp:TextBox ID="txtDelete_ED" CssClass="form-control gap" runat="server"></asp:TextBox>
                            </td>

                        </tr>
                        <tr>
                            <td><strong>No of COA Segement :</strong> </td>
                            <td>
                                <asp:TextBox ID="txtDelete_COASeg" CssClass="form-control gap" runat="server"></asp:TextBox>
                            </td>

                        </tr>
                        <tr>
                            <td><strong>Active</strong> </td>
                            <td>
                                <asp:CheckBox ID="ChkDelete_Active" runat="server" Checked="true" Text="Active" />
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


    <script src="https://unpkg.com/sweetalert/dist/sweetalert.min.js"></script>



</asp:Content>
