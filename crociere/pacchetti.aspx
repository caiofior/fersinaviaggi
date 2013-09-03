<%@ Page Language="VB" AutoEventWireup="false" CodeFile="pacchetti.aspx.vb" Inherits="crociere_categorie" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
        <link rel="stylesheet" href="pacchetti.css" type="text/css" />
        <meta http-equiv="Pragma" content="no-cache" />
        <meta http-equiv="Cache-Control" content=" no-store, no-cache, must-revalidate, pre-check=0, post-check=0, max-age=0" />
        <meta http-equiv="Cache-Control" content="private" />
        <script type="text/javascript" src="pacchetti.js?id=1"></script>
        <script type="text/javascript" src="altezza.js"></script>

</head>
<body >
    <form id="form1" runat="server">   
    <div id="layoutcosta" style="">
        <div id="divpacchetti">
            <ul>
            <li id="rigabenessere" runat="server">                    
                    <img  src="../images/iconbenessere.gif" style="position:absolute; margin: 30px 0 0 590px;" alt="pacchetti benessere" />                                      
                    <div class="dxx">
  
                        <div id="rptwel">                  
                        <ul>
                            <li style="background:url(../images/sfondononcomprende.gif); margin-left:-3px; width:580px; height:20px;"></li>
                            <asp:Repeater ID="RepeaterBenessere" runat="server">
                                <ItemTemplate>
                                    <li style="font-size:small; height:25px;" id="riga" runat="server">
                                        <asp:Image style="cursor:pointer;"  ID="ImageDetail" ImageUrl="../images/icondett.gif" CssClass="GG0"  Width="25px" runat="server" />
                                        <asp:Label ID="nomepacchetto" CssClass="GG1"  runat="server" Text='<%#databinder.eval(container.dataitem,"nomepacchetto")%>'></asp:Label>
                                        <asp:Label ID="listino" CssClass="GG2"  runat="server" Text='<%#databinder.eval(container.dataitem,"listino")%>' Font-Strikeout="true"></asp:Label>
                                        <asp:Label ID="vendita" CssClass="GG3"  runat="server" Text='<%#databinder.eval(container.dataitem,"vendita")%>'></asp:Label>
                                        <asp:Label ID="vendita2" Visible="false" runat="server" Text='<%#databinder.eval(container.dataitem,"vendita")%>'></asp:Label>
                                        <asp:Label ID="id_pacchetto" CssClass="GG3"  runat="server" Text='<%#databinder.eval(container.dataitem,"id_pacchetto")%>' Visible="false"></asp:Label>
                                        <asp:DropDownList ID="DropSel" Width="40px" CssClass="GG4"  runat="server">                                            
                                        </asp:DropDownList>
                                        <div id="divdescri" style="visibility:hidden; height:auto; position:absolute; margin:20px 0 0 30px; " runat="server" >
                                            <asp:Label ID="descrizione"   runat="server" Text='<%#databinder.eval(container.dataitem,"descrizione")%>'></asp:Label>
                                        </div>
                                    </li>
                                </ItemTemplate>
                            </asp:Repeater>
                        </ul>
                        </div>
                    </div>
                </li>
                <li>                                        
                        <img  src="../images/iconbevande.gif" style="position:absolute; margin: 30px 0 0 590px;" alt="pacchetti bevande" />                                           
                    <div class="dxx">
                        <div id="rptbev">                  
                        <ul>
                        <li style="background:url(../images/sfondononcomprende.gif); margin-left:-3px; width:580px; height:20px;"></li>
                            <asp:Repeater ID="RepeaterBevande" runat="server">
                                <ItemTemplate>
                                    <li style="font-size:small; height:25px;" id="riga" runat="server">
                                        <asp:Image style="cursor:pointer;"  ID="ImageDetail" ImageUrl="../images/icondett.gif" CssClass="GG0"  Width="25px" runat="server" />
                                        <asp:Label ID="nomepacchetto" CssClass="GG1"  runat="server" Text='<%#databinder.eval(container.dataitem,"nomepacchetto")%>'></asp:Label>
                                        <asp:Label ID="listino" CssClass="GG2"  runat="server" Text='<%#databinder.eval(container.dataitem,"listino")%>' Font-Strikeout="true"></asp:Label>
                                        <asp:Label ID="vendita" CssClass="GG3"  runat="server" Text='<%#databinder.eval(container.dataitem,"vendita")%>'></asp:Label>
                                        <asp:Label ID="vendita2" Visible="false" runat="server" Text='<%#databinder.eval(container.dataitem,"vendita")%>'></asp:Label>
                                        <asp:Label ID="id_pacchetto" CssClass="GG3"  runat="server" Text='<%#databinder.eval(container.dataitem,"id_pacchetto")%>' Visible="false"></asp:Label>
                                         <asp:Label ID="algiorno" runat="server" Text='<%#databinder.eval(container.dataitem,"algiorno")%>' Visible="false"></asp:Label>
                                         <asp:Label ID="listinoadulti" runat="server" Text='<%#databinder.eval(container.dataitem,"listino")%>' Visible="false"></asp:Label>
                                         <asp:Label ID="venditaadulti" runat="server" Text='<%#databinder.eval(container.dataitem,"vendita")%>' Visible="false"></asp:Label>
                                         <asp:Label ID="listinobambini" runat="server" Text='<%#databinder.eval(container.dataitem,"listinobambini")%>' Visible="false"></asp:Label>
                                         <asp:Label ID="venditabambini" runat="server" Text='<%#databinder.eval(container.dataitem,"venditabambini")%>' Visible="false"></asp:Label>
                                        <asp:DropDownList ID="DropSel" Width="40px" CssClass="GG4"  runat="server">                                            
                                        </asp:DropDownList>
                                        <div id="divdescri" style="visibility:hidden; height:auto; position:absolute; margin:10px 0 0 40px;" runat="server" >
                                            <asp:Label ID="descrizione"  runat="server" Text='<%#databinder.eval(container.dataitem,"descrizione")%>'></asp:Label>
                                        </div>
                                    </li>
                                </ItemTemplate>
                            </asp:Repeater>
                        </ul>
                        </div>
                    </div>
                </li>
                <li id="rigaaltri" runat="server">

                        <img  src="../images/iconaltri.gif" alt="altri pacchetti" style="position:absolute; margin: 30px 0 0 590px;" />                                        
                    
                    <div class="dxx">
                        <div id="rptaltri">                  
                        <ul>
                            <li style="background:url(../images/sfondononcomprende.gif); margin-left:-3px; width:580px; height:20px;"></li>
                            <asp:Repeater ID="RepeaterAltri" runat="server">
                                <ItemTemplate>
                                    <li style="font-size:small; height:25px;" id="riga" runat="server">
                                        <asp:Image style="cursor:pointer;"  ID="ImageDetail" ImageUrl="../images/icondett.gif" CssClass="GG0"  Width="25px" runat="server" />
                                        <asp:Label ID="nomepacchetto" CssClass="GG1"  runat="server" Text='<%#databinder.eval(container.dataitem,"nomepacchetto")%>'></asp:Label>
                                        <asp:Label ID="listino" CssClass="GG2"  runat="server" Text='<%#databinder.eval(container.dataitem,"listino")%>' Font-Strikeout="true"></asp:Label>
                                        <asp:Label ID="vendita" CssClass="GG3"  runat="server" Text='<%#databinder.eval(container.dataitem,"vendita")%>'></asp:Label>
                                        <asp:Label ID="vendita2" Visible="false" runat="server" Text='<%#databinder.eval(container.dataitem,"vendita")%>'></asp:Label>
                                        <asp:Label ID="id_pacchetto" CssClass="GG3"  runat="server" Text='<%#databinder.eval(container.dataitem,"id_pacchetto")%>' Visible="false"></asp:Label>
                                        <asp:DropDownList ID="DropSel" Width="40px" CssClass="GG4"  runat="server">                                            
                                        </asp:DropDownList>
                                        <div id="divdescri" style="visibility:hidden; height:auto; position:absolute; margin:20px 0 0 30px;" runat="server" >
                                            <asp:Label ID="descrizione"  runat="server" Text='<%#databinder.eval(container.dataitem,"descrizione")%>'></asp:Label>
                                        </div>
                                    </li>
                                </ItemTemplate>
                            </asp:Repeater>
                        </ul>
                        </div>
                    </div>
                </li>
            </ul>
         </div>
      </div>   
      <br />
      <div style="text-align:center">        
        <img ID="bttadegua" alt="Conferma pacchetti" src="../images/confermapacchetti.gif" runat="server" style="cursor:pointer;" onclick="return bttadegua_onclick()" />
        <asp:Button ID="bttadegua2" runat="server" Height="50px" Font-Bold="true" Width="250px" Text="Conferma pacchetti selezionati" Visible="false" />
          <asp:Label ID="Labelblocco" runat="server" Text="" Visible="false"></asp:Label>
       </div>
    </form>           
</body>
</html>
