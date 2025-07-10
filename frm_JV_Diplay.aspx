<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="frm_JV_Diplay.aspx.cs" Inherits="frm_JV_Diplay" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <section class="invoice">
      <!-- title row -->
      <div class="row">
        <div class="col-xs-12">
          <h2 class="page-header">
            <i class="fa fa-globe"></i> Journal Voucher
            <small class="pull-right">Print Date: <% =DateTime.Now.ToString("dd-MMM-yyyy") %></small>
          </h2>
        </div>
        <!-- /.col -->
      </div>
      <!-- info row -->
      <div class="row invoice-info">
        <div class="col-sm-4 invoice-col">
          <table >
        <tr>
            <td>
                <strong>Company Name : </strong><%= Request.Cookies["compNAme"].Value %>
                
            </td>
        </tr>
        <tr>
            <td>
             <strong>Branch Name : </strong><%=Request.Cookies["branchNAme"].Value%>
                <br />
            </td>
        </tr>
              <tr>
            <td>
             <strong>Entry Date : </strong><%=Request.QueryString["billdate"]%>
                <br />
            </td>
        </tr>
          </table>
        </div>
  
      </div>
      <!-- /.row -->

      <!-- Table row -->
      <div class="row">
        <div class="col-xs-12 table-responsive">
        
            
            <asp:GridView ID="grdviewdisplay" CssClass="table table-striped"  Font-Size="Large" ShowFooter="True"  Width="100%" runat="server" AutoGenerateColumns="False"  OnRowDataBound="grdviewdisplay_RowDataBound">
                <Columns>
                    <asp:BoundField DataField="AccountCode" HeaderText="AccountCode" SortExpression="AccountCode" />
                    <asp:TemplateField HeaderText="Description" SortExpression="Description">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("Acct_Description") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="Label1" runat="server" Text='<%# Bind("Acct_Description") %>'></asp:Label>
                        </ItemTemplate>
                        <FooterTemplate>
                            <strong> TOTAL :</strong>
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="VoucherNo" HeaderText="VoucherNo" SortExpression="VoucherNo" />
                    <asp:TemplateField HeaderText="DrAmount" SortExpression="DrAmount" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign ="Right">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("DrAmount","{0:0.00}") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblDrAmount" runat="server" Text='<%# Bind("DrAmount","{0:0.00}") %>' ></asp:Label>
                        </ItemTemplate>

                        <FooterTemplate>
                          <strong>  <asp:Label ID="lblTotalDrAmount" runat="server" Text="0"></asp:Label></strong>
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="CrAmount" SortExpression="CrAmount" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign ="Right">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox3" runat="server" Text='<%# Bind("CrAmount","{0:0.00}") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                           <asp:Label ID="lblCrAmount" runat="server" Text='<%# Bind("CrAmount","{0:0.00}") %>'></asp:Label>
                        </ItemTemplate>
                        <FooterTemplate>
                            <strong> <asp:Label ID="lblTotalCrAmount" runat="server" Text="0"></asp:Label></strong>
                        </FooterTemplate>
                    </asp:TemplateField>

                   
                </Columns>
                <FooterStyle BackColor="Silver" />
            </asp:GridView>
         
        </div>
        <!-- /.col -->
      </div>
      <!-- /.row -->

        <div class="row no-print">
        <div class="col-xs-12">
         
            <p style="font-size:18px"> <strong>Narration&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;  Record liabilty of <%= Request.QueryString["shopname"] %> against bill no. <%= Request.QueryString["bilno"] %> Dt. <%= Request.QueryString["billdate"] %>      </strong></p>
             
        </div>
        </div>

        <br />
        <br />
        <br />


         <div class="row no-print">
        <div >
         <div style="margin-left:150px; font-size:medium">   <strong><u> Prepared By: </u></strong></div>



            <div style="margin-left:1200px;font-size:medium""> <strong><u> Approved By: </u></strong></div>
          


            
            
             
        </div>
      </div>
     <br />
        <br />
        <br />

      <!-- this row will not appear when printing -->
      <div class="row no-print">
        <div class="col-xs-12">
           
          <%--<a href="frm_GRN_Inv_In.aspx?UserID=6b9c1166-0f4b-41dc-99e8-b47be96c8157&UsergrpID=ff43b221-f9e1-4423-aa61-f12880a9e13d&fiscaly=2020-2021&FormID=f19df26f-72d8-4d50-82dc-308fea4fa012&Menuid=f5526115-3d19-4c6f-b060-2ecf95b20a19"  class="btn btn-default"><i class="fa fa-close"></i> Close</a>--%>

            <a href="#" onclick="GoBackWithRefresh();return false;"  class="btn btn-default pull-right"><i class="fa fa-close"></i> Close</a>
          <%--<button type="button" class="btn btn-success pull-right"><i class="fa fa-print"></i> Print
          </button>--%>
             <a href="javascript:window.print()"   class="btn btn-success pull-right"><i class="fa fa-print"></i> Print</a>
            
            <asp:LinkButton ID="btnConvert"  CssClass="btn btn-primary pull-right" runat="server"  style="margin-right: 5px;  color: white;" ValidationGroup="aaa" OnClick="btnConvert_Click">
                     <i class="fa fa-download"></i>&nbsp Convert PDF/Excel/Doc
                            </asp:LinkButton>
         
            


            <script>
                function GoBackWithRefresh(event) {
                    if ('referrer' in document) {
                        window.location = document.referrer;
                        /* OR */
                        //location.replace(document.referrer);
                    } else {
                        window.history.back();
                    }
                }
            </script>
             
        </div>
      </div>
    </section>
</asp:Content>

