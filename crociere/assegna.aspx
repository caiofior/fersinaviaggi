<%@ Page Language="VB" AutoEventWireup="false" CodeFile="assegna.aspx.vb" Inherits="crociere_assegna" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <meta http-equiv="Pragma" content="no-cache" />
    <meta http-equiv="Cache-Control" content=" no-store, no-cache, must-revalidate, pre-check=0, post-check=0, max-age=0" />
    <meta http-equiv="Cache-Control" content="private" />
        <script type="text/javascript" src="altezza.js"></script>
        <script type="text/javascript" src="dati.js"></script>
        <link rel="stylesheet" href="cabinecosta.css" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Size="Medium"></asp:Label>
        <asp:GridView ID="GridResult" runat="server" AutoGenerateColumns="false"  Visible="false">
            <Columns>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                           <asp:Label ID="Number"   runat="server" Text='<%#databinder.eval(container.dataitem,"Number")%>'></asp:Label>
                                                           <asp:Label ID="Status"   runat="server" Text='<%#databinder.eval(container.dataitem,"Status")%>'></asp:Label>
                                                           <asp:Label ID="Facility"   runat="server" Text='<%#databinder.eval(container.dataitem,"Facility")%>'></asp:Label>
                                                            <asp:Label ID="Label2"   runat="server" Text='<%#databinder.eval(container.dataitem,"URL")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                            </Columns>
        </asp:GridView>
        <div id="layoutcosta" style="">
        <div id="costacentro">  
            <asp:Panel ID="panelcabine" runat="server" Visible="false">
            <h1><asp:Label ID="Label7" runat="server" Text=""></asp:Label></h1>  
            <ul>
                    <li id="rigatitoli" runat="server" style="margin: 0 0px 0 5px; width:685px;">
                                <asp:Label ID="lblCabina" Font-Bold="true" CssClass="Cle1" runat="server" Text='Cabina:'></asp:Label>                                                    
                                <asp:Label ID="lblPonte" Font-Bold="true" CssClass="Cle2" runat="server" Text='Ponte:'></asp:Label> 
                                <asp:Label ID="lbltipo" Font-Bold="true" CssClass="Cle3" runat="server" Text='Tipo Cabina:'></asp:Label> 
                            </li>            
                <asp:Repeater ID="Repeatercabine" runat="server" >
                                <ItemTemplate>
                                    <li style="height:30px; padding: 1px 0 1px 0; width:685px;  margin: 0 0px 0 5px; font-size:small;" id="riga" runat="server">                                    
                                          <asp:Label ID="LabelCabina" Font-Bold="true" CssClass="Cle13" runat="server" Text='<%#databinder.eval(container.dataitem,"Number")%>'></asp:Label>                                                    
                                          <asp:Label ID="LabelNPonte"  CssClass="Cle14" runat="server" Text='<%#databinder.eval(container.dataitem,"DeckCode")%>'></asp:Label>
                                          <asp:Label ID="LabelPonte" Font-Size="X-Small"  CssClass="Cle15" runat="server" Text='<%#databinder.eval(container.dataitem,"DeckName")%>'></asp:Label>
                                          <asp:Label ID="LabelUrl"  visible="false" runat="server" Text='<%#databinder.eval(container.dataitem,"URL")%>'></asp:Label>
                                          <asp:Label ID="LabelFacility"  visible="false" runat="server" Text='<%#databinder.eval(container.dataitem,"Facility")%>'></asp:Label>
                                          <asp:Label ID="LabelTipoCabina" CssClass="Cle17"  runat="server" Text=''></asp:Label>
                                          <img runat="server" id="disabili" class="Cle19"  style="visibility:hidden" src="../images/cabina-disabili.gif" title="cabina per disabili" alt="cabina per disabili" />                                          
                                          <asp:HyperLink ID="prendi" CssClass="Cle16" style="margin: 2px 0 0 600px; " runat="server">Seleziona</asp:HyperLink><br /> 
                                    </li>
                                </ItemTemplate>
                            </asp:Repeater>  
            </ul>
            </asp:Panel>
        </div>
        </div>
    </div>
    </form>
</body>
</html>
