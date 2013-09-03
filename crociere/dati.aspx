<%@ Page Language="VB" AutoEventWireup="false" CodeFile="dati.aspx.vb" Inherits="crociere_dati" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
        <link rel="stylesheet" href="dati.css" type="text/css" />
        <meta http-equiv="Pragma" content="no-cache" />
        <meta http-equiv="Cache-Control" content=" no-store, no-cache, must-revalidate, pre-check=0, post-check=0, max-age=0" />
        <meta http-equiv="Cache-Control" content="private" />
        <script type="text/javascript" src="altezza.js"></script>
        <script type="text/javascript" src="dati.js?id=3"></script>
</head>
<body >
    <form id="form1" runat="server">
        <input id="eccopreventivo" style="visibility:hidden; width:1px" runat="server" type="text" />
        <div id="layoutcosta">
            <div style="color:#067788; width:680px; text-align:justify;">
                <asp:label id="funziona" runat="server"></asp:label> 
                <asp:label id="memid2" Visible="false" Text="0" runat="server"></asp:label>                                                 
            </div>
            <br />
            <div id="titolodati3">
                        <img src="../images/turno.gif" alt="dati anagrafici" /><h1><asp:Label ID="Label8" runat="server" Text="TURNO RISTORANTE:" Width="400px"></asp:Label></h1>
                </div>
                <div id="div1">
                 <br />
                        <asp:DropDownList ID="DropTurno" Font-Bold="true"  Font-Size="Medium" ForeColor="#1A6011" runat="server" Width="180px">
                            <asp:ListItem Value="0">- Non selezionato -</asp:ListItem>
                            <asp:ListItem Value="1">1° Turno</asp:ListItem>
                            <asp:ListItem Value="2">2° Turno</asp:ListItem>
                        </asp:DropDownList> 
                        <br /> <br />
                    <asp:Label ID="Labelturno" Font-Size="medium" runat="server" ForeColor="#1A6011" Text="Puoi segnalare il turno del ristorante per la cena servita. Informiamo che il turno del ristorante è una segnalazione soggetta a riconferma a bordo della nave."></asp:Label>              
                </div>
                <br /><br />
            <div id="giacliente">
                <asp:Label ID="Label6" style="position:absolute; margin: 46px 0 0 220px; color: #79818A;" runat="server" Font-Size="x-Small" Text="allora inserisci il codice dell'ultima prenotazione e la email, i dati saranno automaticamente caricati!" ></asp:Label>
                <img src="../images/cliente.gif" alt="dati anagrafici" /><h1><asp:Label ID="Label5" runat="server" Text="SEI GIA' CLIENTE di FERSINA VIAGGI?" Width="600px"></asp:Label></h1><br />
                <ul>
                    <li>
                        <asp:Label ID="Label7" runat="server" Text="Codice ultima prenotazione:" CssClass="Clt" Font-Bold="True" ></asp:Label>
                        <asp:TextBox ID="Tcodice"  runat="server" CssClass="Cltt" Width="60px"></asp:TextBox>                          
                        <asp:Label ID="Label9" runat="server" Text="e-mail:" CssClass="Clttt" Font-Bold="True" ></asp:Label>                        
                        <asp:RequiredFieldValidator id="RequiredFieldValidator8" runat="server" CssClass="RequireMA" ValidationGroup="validamail" ControlToValidate="Ttemail" Display="Static" ErrorMessage=" " ></asp:RequiredFieldValidator>
                         <asp:RegularExpressionValidator id="RegularExpressionValidator7" runat="server" CssClass="RequireMA" ValidationGroup="validamail" ControlToValidate="Ttemail" Display="Static" ErrorMessage=" " ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                        <asp:TextBox ID="Ttemail" runat="server" CssClass="Cltttt" Width="130px"></asp:TextBox>                           
                        <asp:Button ValidationGroup="validamail" ID="caricacliente" runat="server" CssClass="Clttttt" Text="Carica"  />
                        <asp:LinkButton ID="inviamailcliente" ValidationGroup="validamail"  style="color: #79818A;"
                            runat="server" CssClass="Cltttttt" 
                            Text="Se hai dimenticato il codice clicca qui per riceverlo via mail" 
                            Width="147px" />
                    </li>
                </ul>
            </div>
            <br />
            <div id="titolodati">
                <img src="../images/icondatianagrafici.gif" alt="dati anagrafici" /><h1><asp:Label ID="Label22" runat="server" Text="I TUOI DATI ANAGRAFICI:" Width="400px"></asp:Label></h1>                                  
            </div>
            <div id="divprenota">
                <ul>
                    <li>
                        <asp:Label ID="Label19" runat="server" Text="Nome e Cognome:" CssClass="ClI" Font-Bold="True" ></asp:Label>
                        <asp:RequiredFieldValidator id="RequiredFieldValidator2" runat="server" CssClass="RequireA" ValidationGroup="valida" ControlToValidate="nomeecognome" Display="Static" ErrorMessage=" " ></asp:RequiredFieldValidator>
                        <asp:TextBox ID="nomeecognome" onkeypress="return isCharKey(event)" runat="server" CssClass="ClL" Width="200px"></asp:TextBox>                                    
                    </li>
                    <li style="margin:-41px 0 0 350px;">
                        <asp:Label ID="Label20" runat="server" Text="Indirizzo:" CssClass="ClI" Font-Bold="True"></asp:Label>
                        <asp:RequiredFieldValidator id="RequiredFieldValidator1" runat="server" CssClass="RequireA" ValidationGroup="valida" ControlToValidate="indirizzo" Display="Static" ErrorMessage=" "></asp:RequiredFieldValidator>
                        <asp:TextBox ID="indirizzo" onkeypress="return isCharNumberKey(event)" runat="server" CssClass="ClL" Width="200px"></asp:TextBox>                                    
                    </li>
                    <li>
                        <asp:Label ID="Label21" runat="server" Text="Cap / Città:" CssClass="ClI" Font-Bold="True"></asp:Label>
                        <asp:RequiredFieldValidator id="RequiredFieldValidator3" runat="server" CssClass="RequireB" ValidationGroup="valida" ControlToValidate="cap" Display="Dynamic" ErrorMessage=" " ></asp:RequiredFieldValidator>
                        <asp:RequiredFieldValidator id="RequiredFieldValidator4" runat="server" CssClass="RequireD" ValidationGroup="valida" ControlToValidate="citta" Display="Static" ErrorMessage=" " ></asp:RequiredFieldValidator>
                        <asp:TextBox ID="cap" onkeypress="return isNumberKey(event)"  runat="server" CssClass="ClL" Width="58px"></asp:TextBox>
                        <asp:TextBox ID="citta" runat="server" onkeypress="return isCharKey(event)" CssClass="ClM" Width="132px"></asp:TextBox>
                    </li>
                    <li style="margin:-41px 0 0 350px;">
                        <asp:Label ID="Label23" runat="server" Text="Telefono Cell.:" CssClass="ClI" Font-Bold="True"></asp:Label>
                        <asp:RequiredFieldValidator id="RequiredFieldValidator6" runat="server" CssClass="RequireA" ValidationGroup="valida" ControlToValidate="telefono" Display="Static" ErrorMessage=" " ></asp:RequiredFieldValidator>
                        <asp:TextBox ID="telefono" onkeypress="return isNumberKey(event)"  runat="server" CssClass="ClL" Width="200px"></asp:TextBox>                                    
                    </li>
                    <li>
                        <asp:Label ID="Label24" runat="server" Text="e-mail:" CssClass="ClI" Font-Bold="True"></asp:Label>
                        <asp:RequiredFieldValidator id="RequiredFieldValidator5" runat="server" CssClass="RequireA" ValidationGroup="valida" ControlToValidate="email" Display="Static" ErrorMessage=" "></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator id="RegularExpressionValidator5" runat="server" CssClass="RequireA" ValidationGroup="valida" ControlToValidate="email" Display="Static" ErrorMessage=" " ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                        <asp:TextBox ID="email" runat="server" CssClass="ClL" Width="200px"></asp:TextBox>                                    
                        <asp:RegularExpressionValidator id="RegularExpressionValidator1" runat="server" CssClass="RequireC" ValidationGroup="valida" ControlToValidate="email" Display="Static" ErrorMessage="e-mail non corretta!" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" Font-Size="XX-Small" Font-Names="Arial"></asp:RegularExpressionValidator>
                    </li>
                    <li id="lifiscale" style="margin:-41px 0 0 350px;">
                        <asp:Label ID="Label25" runat="server" Text="Codice Fiscale:" CssClass="ClI" Font-Bold="True"></asp:Label>
                        <asp:RequiredFieldValidator id="ValidaFiscale2" runat="server" CssClass="RequireA" ValidationGroup="valida" ControlToValidate="codfiscale" Display="Dynamic" ErrorMessage=" "></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator id="ValidaFiscale3" runat="server" CssClass="RequireA" ValidationGroup="valida" ControlToValidate="codfiscale" Display="Dynamic" ErrorMessage=" " ValidationExpression="^[A-Z|a-z]{6}\d{2}[A-Z|a-z]\d{2}[A-Z|a-z]\d{3}[A-Z|a-z]$" ></asp:RegularExpressionValidator>                                    
                        <asp:TextBox ID="codfiscale" runat="server" CssClass="ClL" Width="200px"></asp:TextBox>
                        <asp:RegularExpressionValidator id="ValidaFiscale" runat="server" CssClass="RequireC" ValidationGroup="valida" ControlToValidate="codfiscale" Display="Dynamic" ErrorMessage="c.fiscale non corretto!" ValidationExpression="^[A-Z|a-z]{6}\d{2}[A-Z|a-z]\d{2}[A-Z|a-z]\d{3}[A-Z|a-z]$" Font-Size="XX-Small" Font-Names="Arial"></asp:RegularExpressionValidator>                                    
                        
                    </li>
                                                                           
                    <li style="width:680px; height: 75px;">
                        <asp:Label ID="LabelCmc" runat="server" Text="Eventuali<br />comunicazioni:"  CssClass="ClI" Font-Bold="True" ></asp:Label>
                        <asp:TextBox ID="TextCmc" runat="server" onkeypress="return isCharNumberKey(event)" TextMode="MultiLine" Height="60px" CssClass="ClL" Width="550px"></asp:TextBox>                                            
                    </li>
                    <li style="width:680px; height:auto;">
                            
                    
                    </li>
                    </ul>
                </div>
                <br />
            <div id="titolodati2">
                <img src="../images/icondatianagrafici.gif" alt="dati anagrafici" /><h1><asp:Label ID="Label1" runat="server" Text="DATI PASSEGGERI:" Width="400px"></asp:Label></h1>
            </div>
            <div id="divpax">
                            <ul>
                            <asp:Repeater ID="RepeaterPax" runat="server">
                                <ItemTemplate> 
                                    <li runat="server" id="riga">
                                        <asp:Label ID="LabelClub" Visible="false" runat="server" Text="Inserire il nominativo della persona titolare della tessera Costa Club. Si prega di inserire il nome come riportato sulla tessera" Font-Size="X-Small" style="position:absolute; margin: 32px 0 0 0; color: #ff0000"  Font-Bold="True"></asp:Label>
                                        <asp:Label ID="Label25" runat="server" Text="Nome:" CssClass="ClI" Font-Bold="True"></asp:Label>
                                        <asp:RequiredFieldValidator id="RequiredFieldValidator2" runat="server" CssClass="NN2" ValidationGroup="valida" ControlToValidate="nomeparte" Display="Static" ErrorMessage=" " ></asp:RequiredFieldValidator>
                                        <asp:TextBox ID="nomeparte" onkeypress="return isCharKey(event)"  runat="server" CssClass="NN1" Width="100px"></asp:TextBox>
                                        <asp:Label ID="Label222" runat="server" Text="Cognome:" CssClass="NN3" Font-Bold="True"></asp:Label>
                                        <asp:RequiredFieldValidator id="RequiredFieldValidator7" runat="server" CssClass="NN4" ValidationGroup="valida" ControlToValidate="cognomeparte" Display="Static" ErrorMessage=" " ></asp:RequiredFieldValidator>
                                        <asp:TextBox ID="cognomeparte" onkeypress="return isCharKey(event)"  runat="server" CssClass="NN5" Width="100px"></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator4" ControlToValidate="DropTipo" CssClass="RQ4" ValidationExpression="(^([0-9]*|\d*\d{1}?\d*)$)" runat="server" Display="Static" ErrorMessage=" " ValidationGroup="valida"></asp:RegularExpressionValidator>
                                        <asp:DropDownList ID="DropTipo" CssClass="NN6"  runat="server" Width="65px">
                                        <asp:ListItem style="color:#0473BD; " Value="--">Sesso</asp:ListItem>
                                        <asp:ListItem Value="1">M</asp:ListItem>
                                        <asp:ListItem Value="2">F</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:Label ID="Label26" runat="server" Text="Data Nascita:" CssClass="NN7" Font-Bold="True"></asp:Label>
                                        <asp:RegularExpressionValidator ID="RangeValidator1" ControlToValidate="DropGiorno" CssClass="RQ1" ValidationExpression="(^([0-9]*|\d*\d{1}?\d*)$)" runat="server" Display="Static" ErrorMessage=" "  ValidationGroup="valida"></asp:RegularExpressionValidator>
                                        <asp:DropDownList ID="DropGiorno" CssClass="NN8" runat="server" Width="40px">
                                        </asp:DropDownList>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" ControlToValidate="DropMese" CssClass="RQ2" ValidationExpression="(^([0-9]*|\d*\d{1}?\d*)$)" runat="server" Display="Static" ErrorMessage=" " ValidationGroup="valida"></asp:RegularExpressionValidator>
                                        <asp:DropDownList ID="DropMese" CssClass="NN9" runat="server" Width="40px">
                                        </asp:DropDownList>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator3" ControlToValidate="DropAnno" CssClass="RQ3" ValidationExpression="(^([0-9]*|\d*\d{1}?\d*)$)" runat="server" Display="Static" ErrorMessage=" " ValidationGroup="valida"></asp:RegularExpressionValidator>
                                        <asp:DropDownList ID="DropAnno" CssClass="NN10" runat="server" Width="60px">
                                        </asp:DropDownList>                                                        
                                    </li>                                                          
                                </ItemTemplate>
                            </asp:Repeater>                                        
                            <li style="font-size:small; text-align:justify; width:685px; height: 70px; padding-top:10px;">                                
                                <asp:Checkbox ID="Checkboxprivacy" style="margin:5px; padding-top:5px;" runat="server" />
                                <div style="margin: -16px 0 0 30px">
                                    <asp:Label ID="Label32" runat="server" Text="Dichiaro di avere ricevuto le informazioni di cui all’art. 13 del D.lgs. 196/2003 in particolare riguardo ai diritti da me riconosciuti dalla legge ex art. 7 D.lgs. 196/2003, acconsento al trattamento dei miei dati con le modalità e per le finalità indicate nella informativa stessa, comunque strettamente connesse e strumentali alla gestione del rapporto contrattuale. Per leggere le condizioni relative  all'informativa della privacy "></asp:Label><a href="../privacy.html" target="_blank">premi qui.</a>                                
                                </div>
                            </li>
                            <li style="font-size:small; text-align:justify; width:685px; height: 70px; padding-top:10px;">                                
                                <asp:Checkbox ID="Checkboxgenerali" style="margin:5px; padding-top:5px;" runat="server" />
                                <div style="margin: -16px 0 0 30px">
                                    <asp:Label ID="Label3" runat="server" Text="Questa richiesta non è vincolante per entrambe le parti. Nel caso di esito positivo riceverete una mail di riconferma con il dettaglio della crociera ed i termini per l'accettazione. <b>Nulla è dovuto da parte vostra nel caso di rifiuto dell'offerta che vi sarà inviata</b>. Per l'eventuale successiva conferma indichiamo già le "></asp:Label><asp:HyperLink runat="server" target="_blank" ID="hypercondizioni">condizioni generali</asp:HyperLink> <asp:Label ID="Label4" runat="server" Text=", le "></asp:Label><asp:HyperLink runat="server" target="_blank" ID="Hypercanc">condizioni di cancellazione</asp:HyperLink>
                                    <asp:Label ID="Label10" runat="server" Text=" e le "></asp:Label><asp:HyperLink runat="server" target="_blank" ID="Hyperassi">condizioni di assicurazione.</asp:HyperLink>                                  
                                </div>
                            </li>
                            <li style="font-size:small; text-align:justify; width:685px; height: 30px; padding-top:10px;">                                
                                <asp:Checkbox ID="Checkboxnews" style="margin:5px; padding-top:5px;" runat="server" /><asp:Label ID="Label2" runat="server" Text="Desidero ricevere le offerte esclusivamente di Fersina Viaggi via mail."></asp:Label>                                
                            </li>
                        </ul>

                        </div>
                        <asp:Label ID="Label42" runat="server" Text="E' obbligatorio specificare tutti i dati altrimenti la richiesta non sarà elaborata. Se i dati inseriti sono corretti il sistema passa alla fase successiva ed invia in automatico una mail di conferma!" Font-Size="X-Small" Width="700px"></asp:Label>
                <br />
                <div style="text-align:center; width:685px;">
                    <asp:Label ID="LabelCampi" Font-Size="medium" ForeColor="red" runat="server" Text=""></asp:Label>              
                    
                <br />
                
                    <asp:ImageButton ID="ButtonPrenota" ImageUrl="../images/invia-richiesta.gif" ValidationGroup="valida"  runat="server" />
                <asp:label ID="id_nave" runat="server" Visible="false"></asp:label>
                <asp:label ID="lblerror" runat="server" Font-Bold="true" Visible="false" text="<br />Spiacenti la richiesta non può essere inviata. <br />Preghiamo contattare i nostri uffici al numero 0461 914471."></asp:label>
           
        </div>
        </div>
    </form>
</body>
</html>
