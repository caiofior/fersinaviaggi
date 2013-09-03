<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Copia di prezzi.aspx.vb" Inherits="crociere_prezzi" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link rel="stylesheet" href="prezzi.css" type="text/css" />
    <script type="text/javascript" src="vedisotto.js"></script>
</head>
<body style="position:fixed">
    <form id="form1" runat="server">
        <asp:Label ID="Labelnonesiste" Visible="false" runat="server" Text="" Font-Bold="true"></asp:Label>
        <asp:Panel ID="PanelCategorie" runat="server" >    
        <div id="divprezzi" >
            <div id="dettaglioprezzi"> 
                <ul>
               <asp:Repeater ID="Repeatercategorie" runat="server">
                                <HeaderTemplate>
                                        <li  id="Li1" style="background:url(../images/barra-prezzi.gif); " runat="server">                                                                                        
                                            <asp:Label ID="Label2" CssClass="Co222"  runat="server" Text='periodo richiesto'></asp:Label> 
                                            <asp:Label ID="Label3" CssClass="Co333"  runat="server" Text='periodo prima'></asp:Label>   
                                            <asp:Label ID="Label4" CssClass="Co444"  runat="server" Text='periodo dopo'></asp:Label>                              
                                        </li>
                                        <li  id="riga" runat="server" >                                            
                                            <asp:Label Font-Bold="true" ForeColor="#CE006B" ID="eti1" CssClass="Co11"  runat="server" Text='Tipo Cabine'></asp:Label>  
                                            <asp:Label Font-Bold="true" ForeColor="#CE006B" ID="eti2" CssClass="Co22"  runat="server" Text=''></asp:Label> 
                                            <asp:Label Font-Bold="true" ForeColor="#CE006B" ID="eti3" CssClass="Co33"  runat="server" Text=''></asp:Label>   
                                            <asp:Label Font-Bold="true" ForeColor="#CE006B" ID="eti4" CssClass="Co44"  runat="server" Text=''></asp:Label>                              
                                        </li>
                                </HeaderTemplate>
                                <FooterTemplate>
                                     <li  id="Li1" style="background:url(../images/barra-prezzi.gif); height:5px; " runat="server"> </li>
                                </FooterTemplate>
                                <ItemTemplate>
                                    <li  id="riga" runat="server">
                                    <asp:Image style="margin:8px; cursor:pointer; border:1px solid #dcdcdc;"  ID="ImageDetail" ImageUrl="../images/piu.gif" CssClass="Co00"   runat="server" />
                                        <asp:Label ID="codiceperiodo" Visible="false" runat="server" Text='<%#databinder.eval(container.dataitem,"codiceperiodo")%>'></asp:Label>
                                        <asp:Label ID="categoria" Visible="false" runat="server" Text='<%#databinder.eval(container.dataitem,"categoria")%>'></asp:Label>
                                        <asp:Label ID="descri" CssClass="Co11"  runat="server" Text='<%#databinder.eval(container.dataitem,"descrizione")%>'></asp:Label>  
                                        <asp:Label ID="pr" Visible="false" runat="server" Text='<%#databinder.eval(container.dataitem,"prezzo")%>'></asp:Label> 
                                        <asp:Label ID="Labelprezzo" CssClass="Co22"  runat="server" Text=''></asp:Label>     
                                        <asp:Label ID="Labelprezzoprima" CssClass="Co33"  runat="server" Text=''></asp:Label>     
                                        <asp:Label ID="Labelprezzodopo" CssClass="Co44"  runat="server" Text=''></asp:Label>                              
                                       <div id="divdescri" style="visibility:hidden; height:auto; position:absolute; margin:33px 0 0 5px; " runat="server" >     
                                           <div style="float:left; border-top:1px solid #dcdcdc; ">
                                               <ul>
                                                <asp:Repeater ID="Repeaterprezzi" runat="server">                                                 
                                                    <ItemTemplate>                                            
                                                        <li  id="rigaprezzi" runat="server" style="width:410px;">
                                                            <asp:Label ID="controllo" visible="false" runat="server" Text='0'></asp:Label>  
                                                            <asp:Label ID="conta" visible="false" runat="server" Text='1'></asp:Label>                                                            
                                                            <asp:Label ID="LabelCategoria" CssClass="Co0" runat="server" Text='<%#databinder.eval(container.dataitem,"categoria")%>'></asp:Label>                                                    
                                                            <asp:Label ID="LabelTipo" CssClass="Co1" runat="server" Text='<%#databinder.eval(container.dataitem,"descrizione")%>'></asp:Label>
                                                            <asp:Label ID="LabelListino" Font-Strikeout="true" CssClass="Co2" runat="server" Text=''></asp:Label>                                                    
                                                            <asp:Label ID="Labelpr"  Visible="false" runat="server" Text='<%#databinder.eval(container.dataitem,"prezzo")%>'></asp:Label>
                                                            <asp:Label ID="codiceperiodo"  Visible="false" runat="server" Text='<%#databinder.eval(container.dataitem,"codiceperiodo")%>'></asp:Label>
                                                             <asp:Label ID="Label1"  Visible="false" runat="server" Text='<%#databinder.eval(container.dataitem,"categoria")%>'></asp:Label>
                                                            <asp:Label ID="Labeldisponibile"  Visible="false" runat="server" Text='<%#databinder.eval(container.dataitem,"disponibile")%>'></asp:Label>
                                                            <asp:Label ID="LabelPrezzo"  CssClass="Co2" runat="server" Text=''></asp:Label>
                                                            <img id="indica"  runat="server" class="Cl6"  alt="prendi" />
                                                        </li>                                                        
                                                    </ItemTemplate>                                                                 
                                                </asp:Repeater>
                                                        <li style="width:410px; height:45px; padding-top:4px;" id="rigarq" runat="server" visible="false">
                                                             <asp:Label ID="LabelRQ" CssClass="Co3" ForeColor="#CE006B"  runat="server" Width="600px" Text=""></asp:Label>
                                                             <asp:HyperLink ID="HyperRQ"  CssClass="Co2" ForeColor="#CE006B" runat="server">Richiedi</asp:HyperLink>
                                                        </li>
                                                 </ul>
                                            </div>
                                            <div style="position:absolute; margin:0 0 0 417px; border-top:1px solid #dcdcdc;">
                                                <ul>
                                                <asp:Repeater ID="Repeaterprezziprima" runat="server">                                                 
                                                    <ItemTemplate>                                                        
                                                        <li  id="rigaprezzi" runat="server" style="width:120px;">    
                                                            <asp:Label ID="LabelTipo" visible="false" runat="server" Text='<%#databinder.eval(container.dataitem,"descrizione")%>'></asp:Label>     
                                                            <asp:Label ID="controllo" visible="false" runat="server" Text='1'></asp:Label>                                        
                                                            <asp:Label ID="conta" visible="false" runat="server" Text='0'></asp:Label>                                                            
                                                            <asp:Label ID="Labelpr"  Visible="false" runat="server" Text='<%#databinder.eval(container.dataitem,"prezzo")%>'></asp:Label>
                                                            <asp:Label ID="codiceperiodo"  Visible="false" runat="server" Text='<%#databinder.eval(container.dataitem,"codiceperiodo")%>'></asp:Label>
                                                            <asp:Label ID="Labeldisponibile"  Visible="false" runat="server" Text='<%#databinder.eval(container.dataitem,"disponibile")%>'></asp:Label>
                                                            <asp:Label ID="Labelcategoria"  Visible="false" runat="server" Text='<%#databinder.eval(container.dataitem,"categoria")%>'></asp:Label>
                                                            <asp:Label ID="LabelPrezzo"  CssClass="Co000" runat="server" Text=''></asp:Label>
                                                            <img id="indica"  runat="server" class="Cl7"  alt="prendi" />
                                                        </li>                                                                 
                                                    </ItemTemplate>                                                                 
                                                </asp:Repeater>
                                                <li style="width:120px;  padding-top:4px;" id="rigarq2" runat="server" visible="false">                                                             
                                                             <asp:HyperLink ID="HyperRQ2"  CssClass="Co000" ForeColor="#CE006B" runat="server">Richiedi</asp:HyperLink>
                                                        </li>
                                                        </ul>
                                            </div>
                                            <div style="position:absolute; margin: 0 0 0 553px; border-top:1px solid #dcdcdc;">
                                            <ul>
                                                <asp:Repeater ID="Repeaterprezzidopo" runat="server">                                                 
                                                    <ItemTemplate>
                                                        
                                                        <li  id="rigaprezzi" runat="server" style="width:120px;">   
                                                            <asp:Label ID="LabelTipo" visible="false" runat="server" Text='<%#databinder.eval(container.dataitem,"descrizione")%>'></asp:Label>      
                                                            <asp:Label ID="controllo" visible="false" runat="server" Text='2'></asp:Label>   
                                                            <asp:Label ID="conta" visible="false" runat="server" Text='0'></asp:Label>                                                              
                                                            <asp:Label ID="Labelpr"  Visible="false" runat="server" Text='<%#databinder.eval(container.dataitem,"prezzo")%>'></asp:Label>
                                                            <asp:Label ID="codiceperiodo"  Visible="false" runat="server" Text='<%#databinder.eval(container.dataitem,"codiceperiodo")%>'></asp:Label>
                                                             <asp:Label ID="Labelcategoria"  Visible="false" runat="server" Text='<%#databinder.eval(container.dataitem,"categoria")%>'></asp:Label>
                                                            <asp:Label ID="Labeldisponibile"  Visible="false" runat="server" Text='<%#databinder.eval(container.dataitem,"disponibile")%>'></asp:Label>
                                                            <asp:Label ID="LabelPrezzo"  CssClass="Co000" runat="server" Text=''></asp:Label>
                                                            <img id="indica" runat="server" class="Cl7"  alt="prendi" />
                                                        </li>         
                                                
                                                    </ItemTemplate>                                                                 
                                                </asp:Repeater>
                                                <li style="width:120px;  padding-top:4px;" id="rigarq3" runat="server" visible="false">
                                                             <asp:HyperLink ID="HyperRQ3"  CssClass="Co000" ForeColor="#CE006B" runat="server">Richiedi</asp:HyperLink>
                                                             </li>
                                                        </ul>
                                            </div>
                                        </div>
                                    </li>
                                </ItemTemplate>
                            </asp:Repeater>   
                            
                                                        
                </ul>

            </div>
            <br />
         </div>                    
         </asp:Panel>
          <div id="barrainfo" style="color: #ffffff; font-weight:bold; background:  #AD1063; width:693px;" >
                        <img src="../images/perpreventivo.gif" style="margin:3px 0 0 5px;"  alt="Per il preventivo inserisci il numero di passeggeri e l’età. CLICCA sul prezzo corrispondente al periodo d’interesse"/> 
                        <div style="margin:-30px 0 0 0px; padding:5px 0px 0 40px;  height:40px; "><asp:Label ID="lblpr" style="font-size:small" runat="server" Text="Per il preventivo inserisci il numero di passeggeri e l’età.<br /> CLICCA sul prezzo corrispondente alla cabina e al periodo d’interesse"></asp:Label></div>
                    </div>  
         <asp:GridView ID="GridCat" runat="server" AutoGenerateColumns="false"  Visible="false">
    <Columns>
        <asp:TemplateField>
            <ItemTemplate>
                   <asp:Label ID="Code"   runat="server" Text='<%#databinder.eval(container.dataitem,"Code")%>'></asp:Label>
                                                    <asp:Label ID="StatusCode"   runat="server" Text='<%#databinder.eval(container.dataitem,"StatusCode")%>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>

    </Columns>
    </asp:GridView>
    </form>
</body>
</html>
