<%@ Page Language="VB" AutoEventWireup="false" CodeFile="itinerario.aspx.vb" Inherits="crociere_itinerario" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" href="itinerario.css?id=1" type="text/css" />
    <script language="javascript" type="text/javascript">
        function altezza(px) {
            var mis = 80 + (parseInt(px) * 23);
            if (mis < 230) { mis = 230; };
            top.document.getElementById("frameitinerario").style.height = mis + "px";
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <iframe id="framevideo" visible="false" style="margin-bottom: 20px;" runat="server" width="687" height="460" src="//www.youtube.com/embed/skYmlVFRBno?rel=0" frameborder="0" allowfullscreen></iframe>
        <div id="titolo">
            <asp:Label ID="orariop" CssClass="Cl6" runat="server" Text='ITINERARIO'></asp:Label> 
        </div>
        <div id="diviti" >
            <asp:Label ID="lblroulette" Text="La Formula Roulette non permette di scegliere quale itinerario. L'itinerario sarà assegnato insindacabilmente<br /> da Costa Crociere fra i sotto indicati!<br /><br />" Font-Bold="true" Visible="false" runat="server"></asp:Label>
            <div id="divimage">
                <asp:Image ID="ImageIti" Width="250px"  runat="server" ImageUrl=""   />  
            </div>
            <div id="dettaglioiti">                 
                <ul>
                    <li>
                        <asp:Label ID="lblgiorno" CssClass="Cl11" runat="server" Text='giorno'></asp:Label> 
                        <asp:Label ID="lblporto" CssClass="Cl33" runat="server" Text='porto'></asp:Label> 
                        <asp:Label ID="lblarrivo" CssClass="Cl44" runat="server" Text='arrivo'></asp:Label> 
                        <asp:Label ID="lblpartenza" CssClass="Cl55" runat="server" Text='partenza'></asp:Label> 
                    </li>
                    <asp:Repeater ID="Repeateriti" runat="server">   
                    <ItemTemplate>                                 
                        <li>     
                            <asp:Label ID="oraarrivo" visible="false" runat="server" Text='<%#databinder.eval(container.dataitem,"arrivo")%>'></asp:Label> 
                            <asp:Label ID="orapartenza" visible="false" runat="server" Text='<%#databinder.eval(container.dataitem,"partenza")%>'></asp:Label> 
                            <asp:Label ID="giorno" visible="false" runat="server" Text='<%#databinder.eval(container.dataitem,"giorno")%>'></asp:Label> 
                            <asp:Label ID="gg" CssClass="Cl1" runat="server" Text=''></asp:Label> 
                            <asp:Label ID="gg2" CssClass="Cl22" runat="server" Text=''></asp:Label> 
                            <asp:Label ID="Label6" CssClass="Cl3" runat="server" Text='<%#databinder.eval(container.dataitem,"luogo")%>'></asp:Label> 
                            <asp:Label ID="orarioa" CssClass="Cl4" runat="server" Text=''></asp:Label> 
                            <asp:Label ID="orariop" CssClass="Cl5" runat="server" Text=''></asp:Label>                                          
                        </li>
                    </ItemTemplate>
                    </asp:Repeater>                                
                </ul>
            </div>
         </div>
         <asp:panel runat="server" ID="panelroulette" Visible="false">
         <br /><br />Oppure: <br /><br />
          <div id="diviti2" >
            <div id="divimage2">
                <asp:Image ID="Imageiti2" Width="250px"  runat="server" ImageUrl=""   />  
            </div>
            <div id="dettaglioiti2">                 
                <ul>
                    <li>
                        <asp:Label ID="Label1" CssClass="Cl11" runat="server" Text='giorno'></asp:Label> 
                        <asp:Label ID="Label2" CssClass="Cl33" runat="server" Text='porto'></asp:Label> 
                        <asp:Label ID="Label3" CssClass="Cl44" runat="server" Text='arrivo'></asp:Label> 
                        <asp:Label ID="Label4" CssClass="Cl55" runat="server" Text='partenza'></asp:Label> 
                    </li>
                    <asp:Repeater ID="Repeateriti2" runat="server">   
                    <ItemTemplate>                                 
                        <li>     
                            <asp:Label ID="oraarrivo" visible="false" runat="server" Text='<%#databinder.eval(container.dataitem,"arrivo")%>'></asp:Label> 
                            <asp:Label ID="orapartenza" visible="false" runat="server" Text='<%#databinder.eval(container.dataitem,"partenza")%>'></asp:Label> 
                            <asp:Label ID="giorno" visible="false" runat="server" Text='<%#databinder.eval(container.dataitem,"giorno")%>'></asp:Label> 
                            <asp:Label ID="gg" CssClass="Cl1" runat="server" Text=''></asp:Label> 
                            <asp:Label ID="gg2" CssClass="Cl22" runat="server" Text=''></asp:Label> 
                            <asp:Label ID="Label6" CssClass="Cl3" runat="server" Text='<%#databinder.eval(container.dataitem,"luogo")%>'></asp:Label> 
                            <asp:Label ID="orarioa" CssClass="Cl4" runat="server" Text=''></asp:Label> 
                            <asp:Label ID="orariop" CssClass="Cl5" runat="server" Text=''></asp:Label>                                          
                        </li>
                    </ItemTemplate>
                    </asp:Repeater>                                
                </ul>
            </div>
         </div>
         </asp:panel>
    </form>
</body>
</html>
