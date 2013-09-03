<%@ Page Language="VB" AutoEventWireup="false" CodeFile="nave.aspx.vb" Inherits="crociere_itinerario" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" href="nave.css" type="text/css" />
    <script type="text/javascript" src="altezza.js"></script>
</head>
<body>
    <form id="form1" runat="server">            
        <div id="divnave" >
            <div id="divimage">
                <asp:Image ID="Imagenave" Width="250px"  runat="server" ImageUrl=""   />  
            </div>
            <div id="dettaglionave"> 
                <ul>
                   <li style="float:none; margin-bottom:10px; width:430px; border-bottom:none;">
                        <asp:Label ID="Labeltitolo" CssClass="Cl0" runat="server" Text='MSC ARMONIA'></asp:Label> 
                   </li>                              
                </ul>
                <ul>
                    <asp:Repeater ID="Repeaternave" runat="server">
                        <ItemTemplate>                
                            <li style="width:430px">
                                <asp:Label ID="Label2" CssClass="Cl4" runat="server" Text='Anno di costruzione:'></asp:Label> 
                                <asp:Label ID="Label12" CssClass="Cl3" runat="server" Text='<%#databinder.eval(container.dataitem,"annocostruzione")%>'></asp:Label> 
                            </li>
                            <li style="width:430px">
                                <asp:Label ID="Label5" CssClass="Cl4" runat="server" Text='Numero passeggeri:'></asp:Label> 
                                <asp:Label ID="Label15" CssClass="Cl3" runat="server" Text='<%#databinder.eval(container.dataitem,"numeropax", "{0:##,##}")%>'></asp:Label> 
                            </li>
                            <li style="width:430px">
                                <asp:Label ID="Label6" CssClass="Cl4" runat="server" Text='Numero membri equipaggio:'></asp:Label> 
                                <asp:Label ID="Label16" CssClass="Cl3" runat="server" Text='<%#databinder.eval(container.dataitem,"numeroequi", "{0:##,##}")%>'></asp:Label> 
                            </li>
                            <li style="width:430px">
                                <asp:Label ID="Label22" CssClass="Cl4" runat="server" Text='Rapporto persone / equipaggio:'></asp:Label> 
                                <asp:Label ID="Label23" CssClass="Cl3" runat="server" Text='<%#databinder.eval(container.dataitem,"rapportopaxequi")%>'></asp:Label> 
                            </li>
                            <li style="width:430px">
                                <asp:Label ID="Label24" CssClass="Cl4" runat="server" Text='Spazio bordo:'></asp:Label> 
                                <asp:Label ID="Label25" CssClass="Cl3" runat="server" Text='<%#databinder.eval(container.dataitem,"spaziobordo")%>'></asp:Label> 
                            </li>
                            <li style="width:687px; border-bottom:none;"></li>
                            <li>
                                <asp:Label ID="Label3" CssClass="Cl1" runat="server" Text='Velocità (nodi):'></asp:Label> 
                                <asp:Label ID="Label13" CssClass="Cl2" runat="server" Text='<%#databinder.eval(container.dataitem,"velocita")%>'></asp:Label> 
                            </li>
                            <li >
                                <asp:Label ID="Label4" CssClass="Cl1" runat="server" Text='Capacità massima cabina:'></asp:Label> 
                                <asp:Label ID="Label14" CssClass="Cl2" runat="server" Text='<%#databinder.eval(container.dataitem,"capacitacabina")%>'></asp:Label> 
                            </li>

                            <li>
                                <asp:Label ID="Label7" CssClass="Cl1" runat="server" Text='Numero ponti:'></asp:Label> 
                                <asp:Label ID="Label17" CssClass="Cl2" runat="server" Text='<%#databinder.eval(container.dataitem,"numeroponti")%>'></asp:Label> 
                            </li>
                            <li >
                                <asp:Label ID="Label8" CssClass="Cl1" runat="server" Text='Numero cabine totali:'></asp:Label> 
                                <asp:Label ID="Label18" CssClass="Cl2" runat="server" Text='<%#databinder.eval(container.dataitem,"cabinetotali", "{0:##,##}")%>'></asp:Label> 
                            </li>
                            <li>
                                <asp:Label ID="Label9" CssClass="Cl1" runat="server" Text='Numero cabine interne:'></asp:Label> 
                                <asp:Label ID="Label19" CssClass="Cl2" runat="server" Text='<%#databinder.eval(container.dataitem,"cabineinterne", "{0:##,##}")%>'></asp:Label> 
                            </li>
                            <li >
                                <asp:Label ID="Label10" CssClass="Cl1" runat="server" Text='Numero cabine esterne:'></asp:Label> 
                                <asp:Label ID="Label20" CssClass="Cl2" runat="server" Text='<%#databinder.eval(container.dataitem,"cabineesterne", "{0:##,##}")%>'></asp:Label> 
                            </li>
                            <li>
                                <asp:Label ID="Label11" CssClass="Cl1" runat="server" Text='Numero cabine balcone:'></asp:Label> 
                                <asp:Label ID="Label21" CssClass="Cl2" runat="server" Text='<%#databinder.eval(container.dataitem,"cabinebalcone", "{0:##,##}")%>'></asp:Label> 
                            </li>
                            <li>
                                <asp:Label ID="Label26" CssClass="Cl1" runat="server" Text='Lunghezza (mt.):'></asp:Label> 
                                <asp:Label ID="Label27" CssClass="Cl2" runat="server" Text='<%#databinder.eval(container.dataitem,"lunghezza", "{0:##,##}")%>'></asp:Label> 
                            </li>
                            <li>
                                <asp:Label ID="Label28" CssClass="Cl1" runat="server" Text='Larghezza (mt.):'></asp:Label> 
                                <asp:Label ID="Label29" CssClass="Cl2" runat="server" Text='<%#databinder.eval(container.dataitem,"larghezza", "{0:##,##}")%>'></asp:Label> 
                            </li>
                            <li>
                                <asp:Label ID="Label30" CssClass="Cl1" runat="server" Text='Stazza (tonnellate):'></asp:Label> 
                                <asp:Label ID="Label31" CssClass="Cl2" runat="server" Text='<%#databinder.eval(container.dataitem,"stazza", "{0:##,##}")%>'></asp:Label> 
                            </li>
                            <li>
                                <asp:Label ID="Label32" CssClass="Cl1" runat="server" Text='Numero bar e saloni:'></asp:Label> 
                                <asp:Label ID="Label33" CssClass="Cl2" runat="server" Text='<%#databinder.eval(container.dataitem,"numerobar" )%>'></asp:Label> 
                            </li>
                            <li>
                                <asp:Label ID="Label34" CssClass="Cl1" runat="server" Text='Numero ristoranti:'></asp:Label> 
                                <asp:Label ID="Label35" CssClass="Cl2" runat="server" Text='<%#databinder.eval(container.dataitem,"numeroristoranti" )%>'></asp:Label> 
                            </li>
                            <li>
                                <asp:Label ID="Label36" CssClass="Cl1" runat="server" Text='Numero jacuzzi:'></asp:Label> 
                                <asp:Label ID="Label37" CssClass="Cl2" runat="server" Text='<%#databinder.eval(container.dataitem,"numerojacuzzi" )%>'></asp:Label> 
                            </li>
                            <li>
                                <asp:Label ID="Label38" CssClass="Cl1" runat="server" Text='Numero piscine:'></asp:Label> 
                                <asp:Label ID="Label39" CssClass="Cl2" runat="server" Text='<%#databinder.eval(container.dataitem,"numeropiscine" )%>'></asp:Label> 
                            </li>
                            <li>
                                <asp:Label ID="Label40" CssClass="Cl1" runat="server" Text='Numero piscine interne:'></asp:Label> 
                                <asp:Label ID="Label41" CssClass="Cl2" runat="server" Text='<%#databinder.eval(container.dataitem,"piscineinterne" )%>'></asp:Label> 
                            </li>
                            <li>
                                <asp:Label ID="Label42" CssClass="Cl1" runat="server" Text='Numero piscine esterne:'></asp:Label> 
                                <asp:Label ID="Label43" CssClass="Cl2" runat="server" Text='<%#databinder.eval(container.dataitem,"piscineesterne" )%>'></asp:Label> 
                            </li>
                            <li>
                                <asp:Label ID="Label44" CssClass="Cl1" runat="server" Text='Numero ascensori:'></asp:Label> 
                                <asp:Label ID="Label45" CssClass="Cl2" runat="server" Text='<%#databinder.eval(container.dataitem,"ascensori" )%>'></asp:Label> 
                            </li>
                            <li style="width:687px; border-bottom:none;"></li>
                            <li>
                                <asp:Label ID="Label1" CssClass="Cl1" runat="server" Text='Teatro:'></asp:Label> 
                                <img class="Cl22" src='../images/x<%#databinder.eval(container.dataitem,"teatro")%>.png' />                                
                            </li>
                            <li>
                                <asp:Label ID="Label46" CssClass="Cl1" runat="server" Text='Spa:'></asp:Label> 
                                <img class="Cl22" src='../images/x<%#databinder.eval(container.dataitem,"spa")%>.png' />                                
                            </li>
                            <li>
                                <asp:Label ID="Label47" CssClass="Cl1" runat="server" Text='Matrimonio a bordo:'></asp:Label> 
                                <img class="Cl22" src='../images/x<%#databinder.eval(container.dataitem,"matrimoniabordo")%>.png' />                                
                            </li>
                            <li>
                                <asp:Label ID="Label48" CssClass="Cl1" runat="server" Text='Lavanderia:'></asp:Label> 
                                <img class="Cl22" src='../images/x<%#databinder.eval(container.dataitem,"lavanderia")%>.png' />                                
                            </li>
                            <li>
                                <asp:Label ID="Label49" CssClass="Cl1" runat="server" Text="Galleria d'arte:"></asp:Label> 
                                <img class="Cl22" src='../images/x<%#databinder.eval(container.dataitem,"galleriaarte")%>.png' />                                
                            </li>
                            <li>
                                <asp:Label ID="Label50" CssClass="Cl1" runat="server" Text="Casinò:"></asp:Label> 
                                <img class="Cl22" src='../images/x<%#databinder.eval(container.dataitem,"casino")%>.png' />                                
                            </li>
                            <li>
                                <asp:Label ID="Label51" CssClass="Cl1" runat="server" Text="Cinema:"></asp:Label> 
                                <img class="Cl22" src='../images/x<%#databinder.eval(container.dataitem,"cinema")%>.png' />                                
                            </li>
                            <li>
                                <asp:Label ID="Label52" CssClass="Cl1" runat="server" Text="Negozi:"></asp:Label> 
                                <img class="Cl22" src='../images/x<%#databinder.eval(container.dataitem,"negozi")%>.png' />                                
                            </li>
                            <li>
                                <asp:Label ID="Label53" CssClass="Cl1" runat="server" Text="Biblioteca:"></asp:Label> 
                                <img class="Cl22" src='../images/x<%#databinder.eval(container.dataitem,"biblioteca")%>.png' />                                
                            </li>
                            <li>
                                <asp:Label ID="Label54" CssClass="Cl1" runat="server" Text="Discoteca:"></asp:Label> 
                                <img class="Cl22" src='../images/x<%#databinder.eval(container.dataitem,"discoteca")%>.png' />                                
                            </li>
                            <li>
                                <asp:Label ID="Label55" CssClass="Cl1" runat="server" Text="Sala giochi:"></asp:Label> 
                                <img class="Cl22" src='../images/x<%#databinder.eval(container.dataitem,"salagiochi")%>.png' />                                
                            </li>
                            <li>
                                <asp:Label ID="Label56" CssClass="Cl1" runat="server" Text="Accesso ad internet:"></asp:Label> 
                                <img class="Cl22" src='../images/x<%#databinder.eval(container.dataitem,"accessointernet")%>.png' />                                
                            </li>
                            <li style="width:687px; border-bottom:none;"></li>
                            <li>
                                <asp:Label ID="Label57" CssClass="Cl1" runat="server" Text="Jogging:"></asp:Label> 
                                <img class="Cl22" src='../images/x<%#databinder.eval(container.dataitem,"jogging")%>.png' />                                
                            </li>
                            <li>
                                <asp:Label ID="Label58" CssClass="Cl1" runat="server" Text="Palestra:"></asp:Label> 
                                <img class="Cl22" src='../images/x<%#databinder.eval(container.dataitem,"palestra")%>.png' />                                
                            </li>
                            <li>
                                <asp:Label ID="Label59" CssClass="Cl1" runat="server" Text="Campo da basket:"></asp:Label> 
                                <img class="Cl22" src='../images/x<%#databinder.eval(container.dataitem,"campobasket")%>.png' />                                
                            </li>
                            <li>
                                <asp:Label ID="Label60" CssClass="Cl1" runat="server" Text="Campo da tennis:"></asp:Label> 
                                <img class="Cl22" src='../images/x<%#databinder.eval(container.dataitem,"campotennis")%>.png' />                                
                            </li>
                            <li>
                                <asp:Label ID="Label66" CssClass="Cl1" runat="server" Text="Minigolf:"></asp:Label> 
                                <img class="Cl22" src='../images/x<%#databinder.eval(container.dataitem,"minigolf")%>.png' />                                
                            </li>
                            <li>
                                <asp:Label ID="Label67" CssClass="Cl1" runat="server" Text="Simulatore golf:"></asp:Label> 
                                <img class="Cl22" src='../images/x<%#databinder.eval(container.dataitem,"simulatoregolf")%>.png' />                                
                            </li>
                            <li style="width:687px; border-bottom:none;"></li>
                            <li>
                                <asp:Label ID="Label61" CssClass="Cl1" runat="server" Text="Giardino d'infanzia:"></asp:Label> 
                                <img class="Cl22" src='../images/x<%#databinder.eval(container.dataitem,"giardinoinfanzia")%>.png' />                                
                            </li>
                            <li>
                                <asp:Label ID="Label62" CssClass="Cl1" runat="server" Text="Zona ragazzi:"></asp:Label> 
                                <img class="Cl22" src='../images/x<%#databinder.eval(container.dataitem,"zonaragazzi")%>.png' />                                
                            </li>
                            <li>
                                <asp:Label ID="Label63" CssClass="Cl1" runat="server" Text="Programma ragazzi:"></asp:Label> 
                                <img class="Cl22" src='../images/x<%#databinder.eval(container.dataitem,"programmaragazzi")%>.png' />                                
                            </li>
                            <li>
                                <asp:Label ID="Label64" CssClass="Cl1" runat="server" Text="Zona bambini:"></asp:Label> 
                                <img class="Cl22" src='../images/x<%#databinder.eval(container.dataitem,"zonabambini")%>.png' />                                
                            </li>
                            <li>
                                <asp:Label ID="Label65" CssClass="Cl1" runat="server" Text="Piscina bambini:"></asp:Label> 
                                <img class="Cl22" src='../images/x<%#databinder.eval(container.dataitem,"piscinabambini")%>.png' />                                
                            </li>
                        </ItemTemplate>
                    
                    </asp:Repeater>
                </ul>
            </div>
         </div>         
    </form>
</body>
</html>
