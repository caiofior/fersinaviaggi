<%@ Page Language="VB" AutoEventWireup="false" CodeFile="preventivoparti.aspx.vb" Inherits="crociere_categorie" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
        <link rel="stylesheet" href="preventivo.css" type="text/css" />
        <meta http-equiv="Pragma" content="no-cache" />
        <meta http-equiv="Cache-Control" content=" no-store, no-cache, must-revalidate, pre-check=0, post-check=0, max-age=0" />
        <meta http-equiv="Cache-Control" content="private" />
        <script type="text/javascript" src="preventivo.js?id=1"></script>
         <script type="text/javascript" src="altezza.js"></script>
</head>
<body >
    <form id="form1" runat="server">   
    <div id="layoutcosta" style="">
        <h1><asp:Label ID="labeltitolo" runat="server"></asp:Label></h1>
        <asp:Panel ID="PanelPreventivo" runat="server" >     
                <%--<img src="../images/preventivo.gif" style="position:absolute; margin: 4px 0 0 600px;" alt="preventivo" />--%>
                <div id="divpreventivo">
                    <ul>
                        <%--<li style="background:url(../images/sfondopreventivo.gif); margin-left:0px; width:683px; height:25px"></li>--%>
                        <asp:Repeater ID="RepeaterPreventivo" runat="server">
                            <ItemTemplate> 
                                 <li runat="server" id="riga" >
                                    <asp:Label ID="descrizione" style="width:670px" CssClass="Cl1" runat="server" Text='<%#databinder.eval(container.dataitem,"descrizione")%>'></asp:Label>
                                    <asp:Label ID="persone" CssClass="Cl2" runat="server" Text='<%#databinder.eval(container.dataitem,"persone")%>'></asp:Label>
                                    <asp:Label ID="prezzo" CssClass="Cl3" runat="server" Text='<%#databinder.eval(container.dataitem,"prezzo", "{0:c}")%>'></asp:Label>
                                    <asp:Label ID="totale" CssClass="Cl4" runat="server" Text='<%#databinder.eval(container.dataitem,"totale", "{0:c}")%>'></asp:Label>
                                    <asp:Label ID="prezzop" visible = "false" runat="server" Text='<%#databinder.eval(container.dataitem,"prezzo" )%>'></asp:Label>
                                    <asp:Label ID="totalep" visible = "false" runat="server" Text='<%#databinder.eval(container.dataitem,"totale")%>'></asp:Label>
                                 </li>                                                          
                            </ItemTemplate>
                        </asp:Repeater>
                    </ul>
                </div>
                <br /><br />
                <div  style="text-align:center;"> 
                    <img src="../images/prosegui-rosa.gif" id="prosegui" runat="server"  style="cursor:pointer" alt="prosegui" title="prosegui" />
                </div>
                <div id="menu" runat="server" >
                    <ul>
                        <li id="vdett" runat="server" style="margin-top:-10px; height:140px; border-bottom:1px solid #ffffff;"><img src="../images/vedidettaglio.gif" alt="vedi dettaglio" title="vedi dettaglio" /></li>
                        <li id="vpacc" runat="server"><img src="../images/vedipacchetti.gif" alt="vedi dettaglio" title="aggiungi pacchetti" /></li>
                        <li id="voffe" runat="server"><img src="../images/vediriceviofferta.gif" alt="vedi dettaglio" title="ricevi offerta via mail" /></li>
                        <li id="vprez" runat="server"><img src="../images/vediseguiilprezzo.gif" alt="vedi dettaglio" title="segui il prezzo migliore" /></li>
                        <li></li>
                        <li></li>
                        <li></li>
                    </ul>
                </div>
                <iframe id="framedettaglio" runat="server" frameborder="0" style="border:none"></iframe>
            </asp:Panel>
        <asp:panel ID="paneleerrore" runat="server" Visible="false">                         
            <asp:Label ID="labelerrore" runat="server" style="font-size:large; font-weight: bold;" Visible="false"></asp:Label>
        </asp:panel>                        
      </div>   
       <asp:GridView ID="GridPrezzi" runat="server" AutoGenerateColumns="false"  Visible="false">
                                            <Columns>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                           <asp:Label ID="Code"   runat="server" Text='<%#databinder.eval(container.dataitem,"Code")%>'></asp:Label>
                                                           <asp:Label ID="Description"   runat="server" Text='<%#databinder.eval(container.dataitem,"Description")%>'></asp:Label>
                                                           <asp:Label ID="Amount"   runat="server" Text='<%#databinder.eval(container.dataitem,"Amount")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                            </Columns>
                                            </asp:GridView>   
                                            <asp:Label ID="prezzo" Visible="false" runat="server" CssClass="ClPPP" Font-Bold="True"></asp:Label>
    </form>           
</body>
</html>
