<%@ Page Language="VB" AutoEventWireup="false" CodeFile="cabinecosta.aspx.vb" Inherits="crociere_categorie" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
        <link rel="stylesheet" href="cabinecosta.css" type="text/css" />
        <meta http-equiv="Pragma" content="no-cache" />
        <meta http-equiv="Cache-Control" content=" no-store, no-cache, must-revalidate, pre-check=0, post-check=0, max-age=0" />
        <meta http-equiv="Cache-Control" content="private" />
        <script type="text/javascript" src="cabinecosta.js?id=3"></script>
        <script type="text/javascript" src="altezza.js"></script>
</head>
<body style="margin:0px; background:none #ffffff;" >
    <form id="form1" runat="server">
      

    <div id="layoutcosta" style="">

      <asp:Panel ID="PanelCabine" runat="server" >                   
                 <h1><asp:Label ID="Label7" runat="server" Text="SCEGLI CABINA:"></asp:Label></h1>  
                        <div id="costacentro">       
                            <asp:GridView ID="GridView1" AutoGenerateColumns="False" Visible="FALSE"  runat="server">
                                <Columns>
                                <asp:TemplateField>                                                                       
                                        <ItemTemplate>
                                            <asp:Label ID="Code" runat="server"  Text='<%# DataBinder.Eval(Container.DataItem,"Code") %>'></asp:Label>       
                                            <asp:Label ID="Availability" runat="server"  Text='<%# DataBinder.Eval(Container.DataItem,"Availability") %>'></asp:Label>                       
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        <ul>                             
                            <li id="rigatitoli" runat="server" style="margin: 0 0px 0 5px; width:685px;">
                                <asp:Label ID="lblCabina" Font-Bold="true" CssClass="Cle1" runat="server" Text='Cabina:'></asp:Label>                                                    
                                <asp:Label ID="lblPonte" Font-Bold="true" CssClass="Cle2" runat="server" Text='Ponte:'></asp:Label> 
                                <asp:Label ID="lbltipo" Font-Bold="true" CssClass="Cle3" runat="server" Text='Tipo Cabina:'></asp:Label> 
                            </li>                                                                               
                            <asp:Repeater ID="Repeatercabine" runat="server">
                                <ItemTemplate>
                                    <li style="height:30px; padding: 1px 0 1px 0; width:685px;  margin: 0 0px 0 5px; font-size:small;" id="riga" runat="server">                                    
                                          <asp:Label ID="LabelCabina" Font-Bold="true" CssClass="Cle13" runat="server" Text='<%#databinder.eval(container.dataitem,"Number")%>'></asp:Label>                                                    
                                          <asp:Label ID="LabelNPonte"  CssClass="Cle14" runat="server" Text='<%#databinder.eval(container.dataitem,"DeckCode")%>'></asp:Label>
                                          <asp:Label ID="LabelPonte" Font-Size="X-Small"  CssClass="Cle15" runat="server" Text='<%#databinder.eval(container.dataitem,"DeckName")%>'></asp:Label>
                                          <asp:Label ID="LabelUrl"  visible="false" runat="server" Text='<%#databinder.eval(container.dataitem,"URL")%>'></asp:Label>
                                          <asp:Label ID="LabelFacility"  visible="false" runat="server" Text='<%#databinder.eval(container.dataitem,"Facility")%>'></asp:Label>
                                          <asp:Label ID="LabelTipoCabina" CssClass="Cle17"  runat="server" Text=''></asp:Label>
                                          <img runat="server" id="disabili" class="Cle19"  style="visibility:hidden" src="../images/cabina-disabili.gif" title="cabina per disabili" alt="cabina per disabili" />
                                          <img runat="server" id="poscabina" class="Cle18" src="../images/poscabina.gif" alt="mappa posizione cabina" title="mappa posizione cabina" />
                                          <asp:HyperLink ID="Prendicabina" CssClass="Cle16" runat="server">Seleziona</asp:HyperLink><br />                                          
                                          <iframe runat="server" id="frameimage" frameborder="0" scrolling="no" style='height:310px;width:690px; border:none; visibility:hidden;' />
                                    </li>
                                </ItemTemplate>
                            </asp:Repeater>  
                        </ul>   
                        <br /><br />
                        <div id="barrainfo" runat="server" style="color: #ffffff; font-weight:bold; background:  #AD1063; width:693px;" >
                                    <img src="../images/perpreventivo.gif" style="margin:3px 0 0 5px;"  alt="Ora seleziona una cabina. Se CLICCHI sulla nave visualizzi la posizione della cabina"/> 
                                    <div style="margin:-30px 0 0 0px; padding:5px 0px 0 40px;  height:40px; "><asp:Label ID="lblpr" style="font-size:small" runat="server" Text="Ora seleziona una cabina e ottieni il preventivo.<br />Se CLICCHI sulla nave visualizzi la posizione della cabina."></asp:Label></div>
                                </div>
                        <div id="pontenave">                        
                        </div>   
                        <div id="divpreis">                        
                        </div>                    
                        </div>
                        </asp:Panel>
                         <asp:panel ID="paneleerrore" runat="server" Visible="false"> 
                        
                            <asp:Label ID="labelerrore" runat="server" style="font-size:large; font-weight: bold;" Visible="false"></asp:Label>
                            <asp:HyperLink ID="HyperRQ" style="cursor:pointer" runat="server" NavigateUrl="#" Visible="false" Font-Size="Small" Font-Bold="true" >Per inviare la richiesta premi qui</asp:HyperLink>
                        
                        </asp:panel>
                        
      </div>
      <asp:label ID="valorevolo" runat="server" Text=""></asp:label>
      <asp:label ID="passaporto" runat="server" Visible="false"></asp:label>
    </form>
           
</body>
</html>
