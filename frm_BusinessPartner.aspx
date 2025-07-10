<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="frm_BusinessPartner.aspx.cs" Inherits="frm_SetCompany" MaintainScrollPositionOnPostback="true"  EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.4.1/jquery.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/chosen/1.8.7/chosen.jquery.min.js"></script>
    <link href="https://cdnjs.cloudflare.com/ajax/libs/chosen/1.8.7/chosen.min.css" rel="stylesheet" />
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
        .vali{
            background-color:#ffd8d8
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

        .texthieht{
            height:25px;
            display:inline-block
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
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
   
    
   
    <section class="content">
        <div class="row">
            <div class="col-lg-12">

                <div class="box box-solid shadowbox " style="border-radius: 15px;">
                    <div class="box-header with-border" style="height:75px;">
                        
                         <div style=" padding-top:0px; " class="auto-style14">
                                <div>
                                    <div id="tab_En_dis" runat="server">
                                <div class="nav nav-tabs" id="tabmenu">
                                    <a data-toggle="tab" href="#home">INFORMATION</a>
                                    <a data-toggle="tab" href="#Menu3">BUSINESS PARTNER TYPE</a>
                                    <a data-toggle="tab" href="#menu1">OTHERS</a>

                                    <%--CssClass="table table-bordered table-hover"--%>
                                </div>
                                        </div>
                                    <%--CssClass="table table-bordered table-hover"--%>
                            </div>
                        </div>
                  <div style="height: 60px; width: 650px; margin-left: 310px;"">

                        <asp:LinkButton ID="btnInsert" Height="40" CssClass="btn btn-app " runat="server" Style="left: 180px; background-color: #1f4f8a; color: white;" OnClick="btnInsert_Click" ValidationGroup="changest">
                     <i class="fas fa-save"></i>&nbsp Add
                        </asp:LinkButton>
                        <asp:LinkButton ID="btnSave" Height="40" CssClass="btn btn-app " runat="server" Style="left: 180px; background-color: #1f4f8a; color: white;" OnClick="btnSave_Click" ValidationGroup="valGroup1">
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

                            <div class="tab-content">
                         <div id="home" class="tab-pane fade in active" >

                                <div id="fieldbox" runat="server">
                                    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                                 <ContentTemplate>
                            <table id="tablecontent1" runat="server" style="width: 100%;">
                                <tr>
                                    <td class="auto-style6"><strong>Business Partner Id</strong></td>
                                    <td class="auto-style7">
                                        <div class="backcolorlbl">
                                            <asp:Label ID="lblGOC" runat="server" CssClass="padspace" Font-Bold="False" Font-Italic="False" ForeColor="Black"></asp:Label>
                                        </div>
                                    </td>
                                    <td class="auto-style13"><strong>Business Partner Name</strong></td>
                                    <td class="auto-style11">
                                        <asp:TextBox ID="txtB_PartnerName" CssClass="form-control texthieht" Width="250px" runat="server" Font-Bold="False" Font-Italic="False" ForeColor="Black"></asp:TextBox>
                                    </td>
                                    
                                </tr>
                                <tr>
                                    <td class="auto-style4"><strong>Address Line 1</strong></td>
                                    <td class="auto-style1 ">
                                        <asp:TextBox ID="txtAdd1" CssClass="form-control texthieht" Width="250px" runat="server" Font-Bold="False" Font-Italic="False" ForeColor="Black"></asp:TextBox>
                                    </td>
                                    <td class="auto-style10"><strong>Address Line 2</strong></td>
                                    <td class="auto-style9">
                                        <asp:TextBox ID="txtAdd2" CssClass="form-control texthieht" Width="250px" runat="server" Font-Bold="False" Font-Italic="False" ForeColor="Black"></asp:TextBox>
                                    </td>
                                   
                                </tr>

                                <tr>
                                     <td class="auto-style5"><strong>Address Line 3</strong></td>
                                    <td class="auto-style12">
                                        <div class="backcolorlbl">
                                           <asp:TextBox ID="txtAdd3" CssClass="form-control texthieht" Width="250px" runat="server"  Font-Bold="False" Font-Italic="False" ForeColor="Black"></asp:TextBox>
                                          
                                        </div>
                                    </td>
                                    <td class="auto-style4"><strong>Address Line 4</strong></td>
                                    <td class="auto-style1 ">
                                        <asp:TextBox ID="txtAdd4" CssClass="form-control texthieht" Width="250px" runat="server" Font-Bold="False" Font-Italic="False" ForeColor="Black"></asp:TextBox>
                                    </td>

                                </tr>

                                <tr>
                                    <td class="auto-style10"><strong>Area</strong></td>
                                    <td class="auto-style9">
                                        <asp:TextBox ID="txtArea" CssClass="form-control texthieht" Width="250px" runat="server" Font-Bold="False" Font-Italic="False" ForeColor="Black"></asp:TextBox>
                                    </td>
                                    <td class="auto-style10"><strong>District</strong></td>
                                    <td class="auto-style9">
                                        <asp:TextBox ID="txtDistrict" CssClass="form-control texthieht" Width="250px" runat="server" Font-Bold="False" Font-Italic="False" ForeColor="Black"></asp:TextBox>
                                    </td>
                                    
                                    
                                </tr>
                                <tr>
                                    <td class="auto-style5"><strong>Province</strong></td>
                                    <td class="auto-style12">
                                       <asp:TextBox ID="txtProvince" CssClass="form-control texthieht" Width="250px" runat="server"  Font-Bold="False" Font-Italic="False" ForeColor="Black" style="margin-bottom: 0"></asp:TextBox>
                                    </td>

                                    <td class="auto-style4"><strong>Zip Code</strong></td>
                                    <td class="auto-style1 ">
                                        <asp:TextBox ID="txtZipCode" CssClass="form-control texthieht" Width="250px" runat="server" Font-Bold="False" Font-Italic="False" ForeColor="Black"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    
                                    <td class="auto-style10"><strong>Country</strong></td>
                                    <td class="auto-style9">
                                         <asp:Panel ID="Panel1" runat="server">
                                             
                                             <asp:DropDownList ID="ddlCountry" runat="server" AutoPostBack="True" CssClass="select2" Width="250px" OnSelectedIndexChanged="ddlCountry_SelectedIndexChanged">
                                             </asp:DropDownList>
                                                   
                                        </asp:Panel>
                                    </td>
                                    <td class="auto-style5"><strong>City</strong></td>
                                    <td class="auto-style12">
                                        
                                 <asp:DropDownList ID="ddlCity" runat="server" CssClass="select2"  Width="250px" ></asp:DropDownList>
                                        
                                    </td>
                                    
                                </tr>
                                
                                <tr>
                                    <td class="auto-style4"><strong>Phone No.</strong></td>
                                    <td class="auto-style1 ">
                                        <asp:TextBox ID="txtPhoneNo" CssClass="form-control texthieht" Width="250px" runat="server" Font-Bold="False" Font-Italic="False" ForeColor="Black"></asp:TextBox>
                                    </td>
                                    <td class="auto-style5"><strong>Contact Person Name</strong></td>
                                    <td class="auto-style12">
                                       <asp:TextBox ID="txtContactPerName" CssClass="form-control texthieht" Width="250px" runat="server"  Font-Bold="False" Font-Italic="False" ForeColor="Black"></asp:TextBox>
                                    </td>

                                   
                                </tr>
                                <tr>
                                     <td class="auto-style4"><strong>Contact Number</strong></td>
                                    <td class="auto-style1 ">
                                           <asp:TextBox ID="txtContactNo" CssClass="form-control texthieht" Width="250px" runat="server"  Font-Bold="False" Font-Italic="False" ForeColor="Black"></asp:TextBox>
                                            
                                    </td>
                                    <td class="auto-style10"><strong>Email Address</strong></td>
                                    <td class="auto-style9">
                                        <asp:TextBox ID="txtEmailAdd" CssClass="form-control texthieht" Width="250px" runat="server" Font-Bold="False" Font-Italic="False" ForeColor="Black"></asp:TextBox>
                                    </td>
                                   
                                </tr>
                                <tr>
                                     <td class="auto-style5"><strong>CNIC No.</strong></td>
                                    <td class="auto-style12">
                                        <asp:TextBox ID="txtCNIC" CssClass="form-control texthieht" Width="250px" runat="server"  Font-Bold="False" Font-Italic="False" ForeColor="Black" ValidationGroup="valGroup1"></asp:TextBox>
                                        <asp:RequiredFieldValidator runat="server" ID="Req" ControlToValidate="txtCNIC" Display="None" ErrorMessage="<b> Missing Field</b><br />A CNIC No is required." ValidationGroup="valGroup1" />
                                        <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" TargetControlID="Req" runat="server" HighlightCssClass="vali"></ajaxToolkit:ValidatorCalloutExtender>
                                    </td>

                                    <td class="auto-style4"><strong>NTN No.</strong></td>
                                    <td class="auto-style1 ">
                                        <asp:TextBox ID="txtNTNno" CssClass="form-control texthieht" Width="250px" runat="server" Font-Bold="False" Font-Italic="False" ForeColor="Black"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="auto-style10"><strong>Sales Tax Reg No.</strong></td>
                                    <td class="auto-style9">
                                        <asp:TextBox ID="txtSaleTaxRegNo" CssClass="form-control texthieht" Width="250px" runat="server" Font-Bold="False" Font-Italic="False" ForeColor="Black" ></asp:TextBox>
                                    </td>
                                    <td class="auto-style10"><strong>Is Filer?</strong></td>
                                    <td class="auto-style9">
                                        <asp:Panel ID="Panel2" runat="server">
                                            <asp:CheckBox ID="chkFiler" runat="server" Text="YES"  />
                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        </asp:Panel>
                                    </td>
                                    </tr>
                                    <tr>
                                    <td class="auto-style5"><strong>Is Register?</strong></td>
                                    <td class="auto-style11">
                                        <asp:Panel ID="Panel3" runat="server">
                                            <asp:CheckBox ID="chkRegister" runat="server" Text="YES"  />
                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        </asp:Panel>
                                    </td>

                                         <td class="auto-style5"><strong>Is Gst Service Provider?</strong></td>
                                    <td class="auto-style11">
                                        <asp:Panel ID="Panel10" runat="server">
                                            <asp:CheckBox ID="chkGstSER_Pro" runat="server" Text="YES"  />
                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        </asp:Panel>
                                    </td>
                                    </tr>
                                    
                               
                               </table>
                                                       </ContentTemplate>
                                                 </asp:UpdatePanel>
                                    </div>

                             <div id="Findbox" runat="server">
                                 <div class="box">

                                     <!-- /.box-header -->
                                     <div class="box-body" style="width: 100%; overflow: auto; height: 50%;">
                                         <%--CssClass="table table-bordered table-hover"--%>
                                         <div style="width: 100%; height: 400px; overflow: scroll">

                                             <asp:GridView ID="GridView1" runat="server" CssClass="table table-bordered table-hover tableFixHead" AutoGenerateColumns="False" OnSelectedIndexChanged="GridView1_SelectedIndexChanged" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Horizontal">
                                                 <Columns>

                                                     <asp:CommandField ShowSelectButton="True" ButtonType="Image" SelectImageUrl="~/dist/img/edit.png" >

                                                     <HeaderStyle Width="20px" />
                                                     </asp:CommandField>

                                                     <asp:BoundField DataField="BPartnerId" HeaderText="ID" ReadOnly="True" SortExpression="BPartnerId" InsertVisible="False" >
                                                     <HeaderStyle Width="20px" />
                                                     </asp:BoundField>
                                                     <asp:BoundField DataField="BPartnerName" HeaderText="Business Partner Name" SortExpression="BPartnerName">
                                                     <HeaderStyle Width="500px" />
                                                     </asp:BoundField>
                                                     <asp:BoundField DataField="CNICNo" HeaderText="CNIC NO" SortExpression="CNICNo" >
                                                     <HeaderStyle Width="50px" />
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

                                     </div>
                                     <!-- /.box-body -->
                                 </div>

                             </div>
                             <div id="DeleteBox" runat="server">
                                 <div class="box">

                                     <!-- /.box-header -->
                                     <div class="box-body" style="width: 100%; overflow: auto; height: 50%;">
                                        
                                         <div style="width: 100%; height: 400px; overflow: scroll">
                                             <asp:GridView ID="GridView2" runat="server" CssClass="table table-bordered table-hover tableFixHead" AutoGenerateColumns="False" OnSelectedIndexChanged="GridView2_SelectedIndexChanged" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Horizontal" OnRowDataBound="GridView2_RowDataBound">
                                                 <Columns>

                                                     <asp:CommandField ShowSelectButton="True" ButtonType="Image" SelectImageUrl="~/dist/img/edit.png" >

                                                     <HeaderStyle Width="20px" />
                                                     </asp:CommandField>

                                                     <asp:BoundField DataField="BPartnerId" HeaderText="ID" ReadOnly="True" SortExpression="BPartnerId" InsertVisible="False" >
                                                     <HeaderStyle Width="20px" />
                                                     </asp:BoundField>
                                                     <asp:BoundField DataField="BPartnerName" HeaderText="Business Partner Name" SortExpression="BPartnerName">
                                                     <HeaderStyle Width="500px" />
                                                     </asp:BoundField>
                                                     <asp:BoundField DataField="CNICNo" HeaderText="CNIC NO" SortExpression="CNICNo" >
                                                     <HeaderStyle Width="50px" />
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

                                     </div>
                                     <!-- /.box-body -->
                                 </div>

                             </div>
                             <div id="Editbox" runat="server">
                                 <div class="box">

                                     <!-- /.box-header -->
                                     <div class="box-body" style="width:100%; overflow:auto; height:50%;">
                                         <%--<asp:TextBox ID="txtEdit_GOC" CssClass="form-control gap" runat="server"></asp:TextBox>--%>

                                          <div style="width:100%; height:400px; overflow:scroll">
                                       <asp:GridView ID="GridView3" runat="server" CssClass="table table-bordered table-hover tableFixHead" AutoGenerateColumns="False" OnSelectedIndexChanged="GridView3_SelectedIndexChanged" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Horizontal" OnRowDataBound="GridView3_RowDataBound">
                                              <Columns>

                                                     <asp:CommandField ShowSelectButton="True" ButtonType="Image" SelectImageUrl="~/dist/img/edit.png" >

                                                     <HeaderStyle Width="20px" />
                                                     </asp:CommandField>

                                                     <asp:BoundField DataField="BPartnerId" HeaderText="ID" ReadOnly="True" SortExpression="BPartnerId" InsertVisible="False" >
                                                     <HeaderStyle Width="20px" />
                                                     </asp:BoundField>
                                                     <asp:BoundField DataField="BPartnerName" HeaderText="Business Partner Name" SortExpression="BPartnerName">
                                                     <HeaderStyle Width="500px" />
                                                     </asp:BoundField>
                                                     <asp:BoundField DataField="CNICNo" HeaderText="CNIC NO" SortExpression="CNICNo" >
                                                     <HeaderStyle Width="50px" />
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
                                         
                                     </div>
                                     <!-- /.box-body -->
                                 </div>

                                 

                             </div>
                           

                              </div>


                                <div id="Menu3" class="tab-pane fade">

                                     <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                                         <ContentTemplate>

                                            
                               <div class="box-body" style="width: 100%; overflow: auto; height: 50%;">
                                        <%--CssClass="table table-bordered table-hover"--%>
                                                  
                                             <div style="width: 100%; height: 400px; overflow: scroll">
                                             <asp:GridView ID="GridView4" runat="server" CssClass="table table-bordered table-hover tableFixHead" AutoGenerateColumns="False" OnSelectedIndexChanged="GridView3_SelectedIndexChanged" CellPadding="4" ForeColor="#333333" GridLines="None" DataSourceID="SqlDataSource1">
                                                 <AlternatingRowStyle BackColor="White" />
                                                 <Columns>
                                                     <asp:TemplateField HeaderText="ID" InsertVisible="False" SortExpression="BPNatureID" HeaderStyle-Width="20px">
                                                         <EditItemTemplate>
                                                             <asp:Label ID="lblNatureID" runat="server" Text='<%# Eval("BPNatureID") %>'></asp:Label>
                                                         </EditItemTemplate>
                                                         <ItemTemplate>
                                                             <asp:Label ID="lblNatureID" runat="server" Text='<%# Bind("BPNatureID") %>'></asp:Label>
                                                         </ItemTemplate>
                                                     </asp:TemplateField>
                                                     <asp:TemplateField ShowHeader="True" HeaderText="Select" HeaderStyle-Width="20px">
                                                         <ItemTemplate>
                                                             <asp:CheckBox ID="chkSelect" runat="server" />
                                                         </ItemTemplate>
                                                     </asp:TemplateField>
                                                     <asp:BoundField DataField="BPNatureDescription" HeaderText="BPNatureDescription" SortExpression="BPNatureDescription" />
                                                     <asp:BoundField DataField="Acct_Description" HeaderText="Account Description" SortExpression="Acct_Description" />
                                                     <asp:BoundField DataField="Acct_code" HeaderText="Account code" SortExpression="Acct_code" />
                                                     <asp:CheckBoxField DataField="IsActive" HeaderText="Active" SortExpression="IsActive" />
                                                 </Columns>
                                                 <EditRowStyle BackColor="#2461BF" />
                                             <FooterStyle BackColor="#507CD1" ForeColor="White" Font-Bold="True" />
                                             <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" Width="250  " />
                                             <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />

                                                 <RowStyle BackColor="#EFF3FB" />
                                                 <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />

                                             <SortedAscendingCellStyle BackColor="#F5F7FB" />
                                             <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                                             <SortedDescendingCellStyle BackColor="#E9EBEF" />
                                             <SortedDescendingHeaderStyle BackColor="#4870BE" />
                                         </asp:GridView>
                                                 
                                                 <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:db_HRERPSys %>" SelectCommand="select BPNatureID, BPNatureDescription,SET_COA_detail.Acct_Description, SET_BPartnerNature.Acct_Code, SET_BPartnerNature.IsActive from SET_BPartnerNature LEFT JOIN SET_COA_detail on SET_BPartnerNature.Acct_Code = SET_COA_detail.Acct_Code order by BPNatureDescription"></asp:SqlDataSource>
                                             

                                            </div>
                                        
                              </div>

                                            
                                         </ContentTemplate>
                                         </asp:UpdatePanel>
                                 </div>

                                
                                 <div id="menu1" class="tab-pane fade">

                                     <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                         <ContentTemplate>
                                    <table id="tablecontent2" runat="server" style="width: 100%;">
                                 
                                        <tr>
                                            
                                   
                                                    
                                    <td class="auto-style10"><strong>Contact Person</strong></td>
                                    <td class="auto-style9">
                                        <div class="backcolorlbl">
                                        <asp:TextBox ID="txtContactPerson" CssClass="form-control texthieht" Width="250px" runat="server" Font-Bold="False" Font-Italic="False" ForeColor="Black" ></asp:TextBox>
                                        </div>
                                    </td>
                                    <td class="auto-style5"><strong>Credit Checking</strong></td>
                                    <td class="auto-style12">
                                       <asp:Panel ID="Panel4" runat="server">
                                            <asp:CheckBox ID="chkCreditChecking" runat="server" Text="YES" />
                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        </asp:Panel>
                                    </td>
                                    
                                </tr>
                                        <tr>
                                   
                                    <td class="auto-style4"><strong>Credit Analyst</strong></td>
                                    <td class="auto-style1 ">
                                        <asp:TextBox ID="txtCreditAnalyst" CssClass="form-control texthieht" Width="250px" runat="server" Font-Bold="False" Font-Italic="False" ForeColor="Black" ></asp:TextBox>
                                    </td>
                                            <td class="auto-style10"><strong>Credit Rating</strong></td>
                                    <td class="auto-style9">
                                        <asp:TextBox ID="txtCreditRating" CssClass="form-control texthieht" Width="250px" runat="server"  Font-Bold="False" Font-Italic="False" ForeColor="Black" ></asp:TextBox>
                                        
                                    </td>
                                        </tr>
                                <tr>
                                    
                                    <td class="auto-style5"><strong>Next Credit Review Date</strong></td>
                                    <td class="auto-style12">
                                        <div style="display:inline-block">
                                        <asp:TextBox ID="txtNextCreditReview" CssClass="form-control texthieht"  Width="230px" runat="server" Font-Bold="False" Font-Italic="False" ForeColor="Black" ></asp:TextBox>
                                        <asp:ImageButton ID="imgPopup" ImageUrl="img/calendar.png" ImageAlign="Bottom" runat="server" />
                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender1" PopupButtonID="imgPopup" TargetControlID="txtNextCreditReview" Format="dd-MMM-yyyy" runat="server" />
                                            <asp:RequiredFieldValidator runat="server" ID="Req2" ControlToValidate="txtNextCreditReview" Display="None" ErrorMessage="<b> Missing Field</b><br />A Date is required." ValidationGroup="valGroup1" />
                                        <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" TargetControlID="Req2" runat="server" HighlightCssClass="vali"></ajaxToolkit:ValidatorCalloutExtender>
                                            </div>
                                    </td>
                                    <td class="auto-style4"><strong>Tolerance Percent</strong></td>
                                    <td class="auto-style1 ">
                                        <asp:TextBox ID="txtTolerancePercent" CssClass="form-control texthieht" Width="250px" runat="server"  Font-Bold="False" Font-Italic="False" ForeColor="Black" ></asp:TextBox>
                                    </td>

                                    
                                </tr>

                                <tr>
                                    
                                    <td class="auto-style10"><strong>Interest Charge</strong></td>
                                    <td class="auto-style9">
                                         <asp:Panel ID="Panel8" runat="server">
                                            <asp:CheckBox ID="chkInterestCharge" runat="server" Text="YES" />
                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        </asp:Panel>
                                    </td>
                                    <td class="auto-style5"><strong>Interest Period Days</strong></td>
                                    <td class="auto-style12">
                                            <asp:TextBox ID="txtInterestPeriodDays" CssClass="form-control texthieht" Width="250px"  runat="server" ></asp:TextBox>
                                    </td>
                                    
                                </tr>
                                        <tr>
                                            

                                    <td class="auto-style4"><strong>Payment Grace Days</strong></td>
                                    <td class="auto-style1 ">
                                        <asp:TextBox ID="txtPaymentGraceD" CssClass="form-control texthieht" Width="250px" runat="server" Font-Bold="False" Font-Italic="False" ForeColor="Black" ></asp:TextBox>
                                    </td>
                                            <td class="auto-style10"><strong>Discount Grace Days</strong></td>
                                            <td class="auto-style9">
                                                <asp:TextBox ID="txtDiscountGraceDay" CssClass="form-control texthieht" Width="250px" runat="server" Font-Bold="False" Font-Italic="False" ForeColor="Black" ></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            
                                            <td class="auto-style5"><strong>Clearing Days</strong></td>
                                            <td class="auto-style12">
                                                <asp:TextBox ID="txtClearingDays" CssClass="form-control texthieht" Width="250px" runat="server" Font-Bold="False" Font-Italic="False" ForeColor="Black" ></asp:TextBox>
                                            </td>
                                            <td class="auto-style10"><strong>Tax Exempted</strong></td>
                                            <td class="auto-style9">
                                                <asp:Panel ID="Panel5" runat="server">
                                            <asp:CheckBox ID="chkTaxExempted" runat="server" Text="YES"  />
                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        </asp:Panel>
                                            </td>

                                        </tr>
                                        <tr>
                                            
                                            <td class="auto-style5"><strong>Exempted Amount</strong></td>
                                            <td class="auto-style12">
                                                <asp:TextBox ID="txtExemptedAmount" CssClass="form-control texthieht" Width="250px" runat="server" Font-Bold="False" Font-Italic="False" ForeColor="Black" ></asp:TextBox>
                                            </td>

                                            <td class="auto-style10"><strong>Additional Deduction Tax Expemted</strong></td>
                                            <td class="auto-style9">
                                               <asp:Panel ID="Panel6" runat="server">
                                            <asp:CheckBox ID="chkAdditionalTax" runat="server" Text="YES"  />
                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        </asp:Panel>
                                            </td>
                                        </tr>
                                        <tr>
                                            
                                            <td class="auto-style5"><strong>Is Stop Payment?</strong></td>
                                            <td class="auto-style12">
                                                <asp:Panel ID="Panel7" runat="server">
                                            <asp:CheckBox ID="chkIsStopped" runat="server" Text="YES"  />
                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        </asp:Panel>
                                            </td>
                                            <td class="auto-style10"><strong>Fixed Discount</strong></td>
                                            <td class="auto-style9">
                                                <asp:TextBox ID="txtFixedDiscount" CssClass="form-control texthieht" Width="250px" runat="server" Font-Bold="False" Font-Italic="False" ForeColor="Black" ></asp:TextBox>
                                            </td>

                                        </tr>
                                        <tr>
                                            
                                            <td class="auto-style5"><strong>Preferred Payment Type</strong></td>
                                            <td class="auto-style12">
                                                <asp:TextBox ID="txtPreferredpaymentType" CssClass="form-control texthieht" Width="250px" runat="server" Font-Bold="False" Font-Italic="False" ForeColor="Black" ></asp:TextBox>
                                            </td>
                                             <td class="auto-style10"><strong>Remarks</strong></td>
                                            <td class="auto-style9">
                                                <asp:TextBox ID="txtRemarks" CssClass="form-control texthieht" Width="250px" runat="server" Font-Bold="False" Font-Italic="False" ForeColor="Black" ></asp:TextBox>
                                            </td>

                                        </tr>
                                          <tr>
                                           
                                            <td class="auto-style5"><strong>OS Code</strong></td>
                                            <td class="auto-style12">
                                                <asp:TextBox ID="txtOSCode" CssClass="form-control texthieht" Width="250px" runat="server" Font-Bold="False" Font-Italic="False" ForeColor="Black" ></asp:TextBox>
                                            </td>
                                              <td class="auto-style10"><strong>Security Deposit</strong></td>
                                            <td class="auto-style9">
                                                <asp:TextBox ID="txtSecuritydeposit" CssClass="form-control texthieht" Width="250px" runat="server" Font-Bold="False" Font-Italic="False" ForeColor="Black" ></asp:TextBox>
                                            </td>

                                        </tr>
                                          <tr>
                                            
                                            <td class="auto-style5"><strong>Special Deal</strong></td>
                                            <td class="auto-style12">
                                                <asp:TextBox ID="txtSpecialDeal" CssClass="form-control texthieht" Width="250px" runat="server" Font-Bold="False" Font-Italic="False" ForeColor="Black" ></asp:TextBox>
                                            </td>
                                              <td class="auto-style10"><strong>Created Date</strong></td>
                                            <td class="auto-style9">
                                                <asp:TextBox ID="txtCreatedDate" CssClass="form-control texthieht" Width="250px" runat="server" Font-Bold="False" Font-Italic="False" ForeColor="Black" ></asp:TextBox>
                                            </td>

                                        </tr>
                                          <tr>
                                            
                                            <td class="auto-style5"><strong>Created By</strong></td>
                                            <td class="auto-style12">
                                                <asp:TextBox ID="txtCreatedBy" CssClass="form-control texthieht" Width="250px" runat="server" Font-Bold="False" Font-Italic="False" ForeColor="Black" ></asp:TextBox>
                                            </td>
                                              <td class="auto-style10"><strong>Updated Date</strong></td>
                                            <td class="auto-style9">
                                                <asp:TextBox ID="txtUpdatedDate" CssClass="form-control texthieht" Width="250px" runat="server" Font-Bold="False" Font-Italic="False" ForeColor="Black" ></asp:TextBox>
                                            </td>

                                        </tr>
                                          <tr>
                                            
                                            <td class="auto-style5"><strong>Update By</strong></td>
                                            <td class="auto-style12">
                                                <asp:TextBox ID="txtUpdateBy" CssClass="form-control texthieht" Width="250px" runat="server" Font-Bold="False" Font-Italic="False" ForeColor="Black" ></asp:TextBox>
                                            </td>
                                              <td class="auto-style5"><strong>Is Active?</strong></td>
                                            <td class="auto-style12">
                                                 <asp:Panel ID="Panel9" runat="server">
                                            <asp:CheckBox ID="chkActive" runat="server" Text="YES"  />
                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        </asp:Panel>
                                               
                                            </td>

                                        </tr>
                                        
                                    </table>
                                         </ContentTemplate>
                                         </asp:UpdatePanel>
                                 </div>
                                  
                                 <%--<asp:TextBox ID="txtEdit_GOC" CssClass="form-control gap" runat="server"></asp:TextBox>--%>


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
                            <td ><strong>Business Partner Name</strong> </td>
                            <td colspan="3">
                                <asp:DropDownList ID="ddlEdit_BusinessPartnerName" CssClass="form-control gap select2" Width="100%" runat="server" ></asp:DropDownList>
                            </td>

                        </tr>
                                           
                          <tr>
                            <td ><strong>CNIC</strong> </td>
                            <td colspan="3">
                                <asp:TextBox ID="txtEdit_CNIC" CssClass="form-control gap" runat="server"></asp:TextBox>
                            </td>

                        </tr>
                         <tr>
                            <td><strong>NTN No.</strong> </td>
                            <td>
                                <asp:TextBox ID="txtEdit_NtNno" CssClass="form-control gap" runat="server"></asp:TextBox>
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
                            <td ><strong>Business Partner Name</strong> </td>
                            <td colspan="3">
                                <asp:DropDownList ID="ddlFindBusinessPartnerName" CssClass="form-control select2" Width="100%"  runat="server" ></asp:DropDownList>
                               <div>
                                    <br />
                                </div>
                            </td>

                        </tr>
                        <tr>
                            <td ><strong>CNIC</strong> </td>
                            <td colspan="3">
                                <asp:TextBox ID="txtFind_CNIC" CssClass="form-control gap" runat="server"></asp:TextBox>
                            </td>

                        </tr>
                         <tr>
                            <td><strong>NTN No.</strong> </td>
                            <td>
                                <asp:TextBox ID="txtFind_NTNNo" CssClass="form-control gap" runat="server"></asp:TextBox>
                            </td>
                          </tr>
                         <tr>
                            <td><strong>Active</strong> </td>
                            <td>
                                  <asp:CheckBox ID="chkFind_Active" runat="server" Checked="true" Text="Active" />
                            </td>

                        </tr>
                       
                    </table>
                    <%--<script>
                          $('#<%=DropDownList1.ClientID%>').chosen();
                        </script>--%>
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
                            <td ><strong>Business Partner Name</strong> </td>
                            <td colspan="3">
                                <asp:DropDownList ID="ddlDel_BusinessPartnerName" CssClass="form-control select2" Width="100%" runat="server" ></asp:DropDownList>
                            </td>

                        </tr>
                       
                          <tr>
                            <td ><strong>CNIC</strong> </td>
                            <td colspan="3">
                                <asp:TextBox ID="txtDelete_CNIC" CssClass="form-control gap" runat="server"></asp:TextBox>
                            </td>

                        </tr>
                         <tr>
                            <td><strong>NTN No.</strong> </td>
                            <td>
                                <asp:TextBox ID="txtDelete_NTN" CssClass="form-control gap" runat="server"></asp:TextBox>
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

    <script>
      
         <%-- $('#<%=DropDownList1.ClientID%>').chosen();--%>
        </script>
           
      <script src="https://unpkg.com/sweetalert/dist/sweetalert.min.js"></script>
  
</asp:Content>

