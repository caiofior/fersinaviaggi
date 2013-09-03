<%@ Page Language="VB" AutoEventWireup="false" CodeFile="contratto.aspx.vb" Inherits="crociere_contratto" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
     <link rel="stylesheet" href="contratto.css" type="text/css" />
     <script type="text/javascript" src="altezza.js"></script>
     <style type="text/css">
        @page {size: 210mm 297mm; margin: 5mm;}
     </style>
</head>
<body>
    <form id="form1" runat="server">
     <div id="centrocontratto">
        <div id="titolocontratto">
            <div id="astoi">
                <img src="../images/astoi.jpg" width="240px" />
            </div>
                <p>COMUNICAZIONE DI CONFERMA DI PACCHETTO/SERVIZIO TURISTICO</p><br />
                Modulo per adempiere le disposizioni dell’art.35 del Codice del Turismo.<br />
                Il cliente ha diritto di ricevere copia del presente contratto di<br /> compravendita di
                pacchetto/servizio turistico<br />            
        </div>
        <div id="dentrocontratto">
                <div id="divagenzia">
                    <ul>
                        <li>
                            <asp:Label ID="Label1" runat="server" Text="Agenzia:" CssClass="RT1"></asp:Label>
                        </li>
                        <li>
                            <asp:Label ID="Label2" runat="server" Text="Fersina viaggi srl" CssClass="RT11"></asp:Label>
                        </li>
                        <li>
                            <asp:Label ID="Label3" runat="server" Text="Via Stella, 5/M - 38123 Trento" CssClass="RT11"></asp:Label>
                        </li>
                        <li style="border:0px solid">
                            <asp:Label ID="Label4" runat="server" Text="tel. 0461 914471 - fax 0461 1810136" CssClass="RT11"></asp:Label>
                        </li>
                    </ul>
                </div>
                <div id="divoperatore">
                    <ul>
                        <li>
                            <asp:Label ID="Label5" runat="server" Text="Operatore T.O.:" CssClass="RT1"></asp:Label>
                            <asp:Label ID="Labeloperatore" runat="server" Text="" CssClass="RT2"></asp:Label>
                        </li>
                        <li>
                            <asp:Label ID="Label6" runat="server" Text="N. pratica T.O.:" CssClass="RT1"></asp:Label>
                            <asp:Label ID="Labelpratica" runat="server" Text="" CssClass="RT2"></asp:Label>
                        </li>
                        <li style="border:0px solid">
                            <asp:Label ID="Label7" runat="server" Text="Nave:" CssClass="RT1"></asp:Label>
                            <asp:Label ID="labelnave" runat="server" Text="" CssClass="RT2"></asp:Label>
                        </li>
                    </ul>
                </div>
                <div id="divclientecontratto">
                    <ul>
                        <li>
                            <asp:Label ID="Label9" runat="server" Text="Nome / Cognome:" CssClass="RT1"></asp:Label>
                            <asp:Label ID="Labelnome" runat="server" Text="" CssClass="RT2"></asp:Label>
                             <asp:Label ID="Label16" runat="server" Text="Telefono:" CssClass="RT13"></asp:Label>
                            <asp:Label ID="Labeltelefono" runat="server" Text="" CssClass="RT12"></asp:Label>
                        </li>
                        <li>
                            <asp:Label ID="Label10" runat="server" Text="Indirizzo:" CssClass="RT1"></asp:Label>
                            <asp:Label ID="Labelindirizzo" runat="server" Text="" CssClass="RT2"></asp:Label>
                            <asp:Label ID="Label18" runat="server" Text="Codice Fiscale:" CssClass="RT13"></asp:Label>
                            <asp:Label ID="Labelfiscale" runat="server" Text="" style="color:Black; font-weight:normal" CssClass="RT6"></asp:Label>
                        </li>
                        <li style="border-bottom:none">
                            <asp:Label ID="Label11" runat="server" Text="Cap:" CssClass="RT1"></asp:Label>
                            <asp:Label ID="Labelcap" runat="server" Text="" CssClass="RT2"></asp:Label>
                            <asp:Label ID="Label14" runat="server" Text="Città:" CssClass="RT3"></asp:Label>
                            <asp:Label ID="Labelcitta" runat="server" Text="" CssClass="RT9"></asp:Label>
                        </li>
                    </ul>
                </div>
                <div id="sottocontratto">
                <asp:Label ID="Label12" runat="server" Font-Bold="true" Text="In nome e per conto proprio, oltre che in nome e per conto delle persone di seguito elencate:" ></asp:Label><br />
                    <div id="divpersonecontratto">                    
                        <ul>
                            <asp:Repeater ID="RepeaterNomi" runat="server">
                                <ItemTemplate>
                                    <li>
                                        <asp:Label ID="Label10" runat="server" Text="Nome / Cognome:" CssClass="RT1"></asp:Label>
                                        <asp:Label ID="Labelcognome" runat="server" Text='<%#databinder.eval(container.dataitem,"nomecognome")%>' CssClass="RT2"></asp:Label> 
                                        <asp:Label ID="Label13" runat="server" Text="Data nascita:" CssClass="RT6"></asp:Label>
                                        <asp:Label ID="Label15" runat="server" Text='<%#databinder.eval(container.dataitem,"datanascita", "{0:d}")%>' CssClass="RT7"></asp:Label> 
                                    </li>
                                </ItemTemplate>
                            </asp:Repeater>
                        </ul>                    
                    </div>                  
                    <div style="text-align:center">
                    <asp:Label ID="Label17" runat="server" 
                            Text="nel ribadire che il contratto è disciplinato dalle condizioni precedentemente sottoscritte, nonché dai depliant, opuscoli, sito/i web dell’organizzatore e qualsivoglia altra documentazione illustrativa del pacchetto turistico fin qui fornita; che di conseguenza se ne presume la conoscenza ai sensi dell’art. 1341 comma 1 del codice civile e le parti non potranno pertanto reciprocamente contestare la mancata conoscenza delle informazioni scambiate; che il contratto è altresì disciplinato dalla CCV ratificata e resa esecutiva con la legge n. 1084/1977 nonché dal Codice del Turismo," 
                            Font-Size="X-Small" ></asp:Label><br /><br />
                    <asp:Label ID="Label19" runat="server" Text="si comunica che" ></asp:Label><br />
                    <asp:Label ID="Label20" Font-Bold="true" Font-Size="Medium" runat="server" Text="l' intermediario Fersina Viaggi srl" ></asp:Label><br />
                    <asp:Label ID="Label21" runat="server" 
                            Text="ha confermato la disponibilità di quanto richiesto dal contraente, in relazione alla stipulazione del presente contratto, avente ad oggetto il seguente pacchetto turistico:" 
                            Font-Size="X-Small" ></asp:Label>
                    </div>
                    <br />
                    <div id="serviziocontratto">
                        <ul>
                            <li>
                                <asp:Label ID="Label22" runat="server" Text="Cod. pren. elett.:" CssClass="RT1" ></asp:Label>
                                <asp:Label ID="lblcodpre" runat="server" Text="" CssClass="RT2"></asp:Label>
                                <asp:Label ID="Label23" runat="server" Text="Condizioni:" CssClass="RT3" ></asp:Label>
                                <asp:Label ID="lblservizio" runat="server" Text="" CssClass="RT8"></asp:Label>
                            </li>
                            <li>
                                <asp:Label ID="Label24" runat="server" Text="Itinerario:" CssClass="RT1" ></asp:Label>
                                <asp:Label ID="lbldestinazione" runat="server" Text="" CssClass="RT2"></asp:Label>
                            </li>
                            <li>
                                <asp:Label ID="Label25" runat="server" Text="Durata:" CssClass="RT1" ></asp:Label>
                                <asp:Label ID="lbldurata" runat="server" Text="" CssClass="RT2"></asp:Label>
                                <asp:Label ID="label30" runat="server" Text="dal:" CssClass="RT3" ></asp:Label>
                                <asp:Label ID="lbldal" runat="server" Text="" CssClass="RT9"></asp:Label>
                                <asp:Label ID="Label28" runat="server" Text="al:" CssClass="RT10" ></asp:Label>
                                <asp:Label ID="lblal" runat="server" Text="" CssClass="RT12"></asp:Label>
                            </li>
                            <li style="border-bottom:none;">
                                <asp:Label ID="Label26" runat="server" Text="Partenza da:" CssClass="RT1" ></asp:Label>
                                <asp:Label ID="lblpartenza" runat="server" Text="" CssClass="RT2"></asp:Label>
                                <asp:Label ID="Label27" runat="server" Text="Ritorno a:" CssClass="RT13" ></asp:Label>
                                <asp:Label ID="lblritorno" runat="server" Text="" CssClass="RT12"></asp:Label>
                            </li>
                                 
                        </ul>
                    </div>
                    <br />
                    <asp:Label ID="Label29" runat="server" 
                        Text="l’intermediario ha consegnato al cliente copia del contratto sottoscritto nel modello approvato da Assoviaggi, ASTOI, Confindustria Assotravel, Federviaggio, Fiavet." 
                        Font-Size="X-Small" ></asp:Label>
                    <br />
                    <div id="pagamenticontratto">
                        <ul>
                            <li><asp:Label ID="Label32" runat="server" Text="Pagamenti" Font-Bold="true" ></asp:Label></li>
                            <asp:Repeater ID="RepeaterPaga" runat="server">
                                <ItemTemplate>
                                    <li>
                                        <asp:Label ID="Labeldescripaga" runat="server" Text='<%#databinder.eval(container.dataitem,"descripaga")%>' CssClass="RT1"></asp:Label>                                         
                                        <asp:Label ID="Label33" runat="server" Text='<%#databinder.eval(container.dataitem,"prezzo", "{0:c}")%>' CssClass="RT19"></asp:Label>                                         
                                        <asp:Label ID="Labelscadenza" runat="server" Text='<%#databinder.eval(container.dataitem,"scadenza", "{0:d}")%>' CssClass="RT21" ></asp:Label> 
                                        <asp:Label ID="lblentro" runat="server" Text='Entro:' CssClass="RT20"></asp:Label> 
                                        <asp:Label ID="Labelricevuto" runat="server" Text='' CssClass="RT22"></asp:Label> 
                                        <asp:Label ID="ricevutodata" runat="server" Text='<%#databinder.eval(container.dataitem,"ricevutodata", "{0:d}")%>' visible="false"></asp:Label> 
                                        <asp:Label ID="ricevuto" runat="server" Text='<%#databinder.eval(container.dataitem,"ricevuto")%>' visible="false"></asp:Label> 
                                    </li>
                                </ItemTemplate>
                            </asp:Repeater>
                        </ul>
                    </div>
                    <div id="prezzicontratto">
                        <ul>
                            <asp:Repeater ID="RepeaterPreventivo" runat="server">
                                <ItemTemplate>
                                   <li>
                                        <asp:Label ID="lblquota" Font-Size="X-Small" runat="server" Text='<%#databinder.eval(container.dataitem,"descrizione")%>' CssClass="RT14" ></asp:Label>
                                        <asp:Label ID="lblpax" runat="server" Text='<%#databinder.eval(container.dataitem,"pax")%>' CssClass="RT15"></asp:Label>
                                        <asp:Label ID="lblprezzo" runat="server" Text='<%#databinder.eval(container.dataitem,"importo", "{0:c}")%>' CssClass="RT16" ></asp:Label>
                                        <asp:Label ID="lbltotale" runat="server" Text='<%#databinder.eval(container.dataitem,"totale", "{0:c}")%>' CssClass="RT17"></asp:Label>
                                    </li>                                                                          
                                    </ItemTemplate>
                             </asp:Repeater>
                             <li id="garantita" runat="server" visible="false" style="font-size:x-small; color:Red;">
                                <asp:Label ID="Labelgarantita" runat="server" Text="Cabina garantita: durante la crociera potrebbe essere richiesto un cambio cabina" ></asp:Label>
                             </li>
                             <li style="border-bottom:none;">
                                        <asp:Label ID="Label31" runat="server" Text="Prezzo Complessivo:" Font-Bold="true" CssClass="RT18" ></asp:Label>
                                        <asp:Label ID="lbltotalone" runat="server" Text="" Font-Bold="true" CssClass="RT17"></asp:Label>
                             </li>
                        </ul>
                        
                    </div>
                    <asp:Label ID="Labeldocumenti" runat="server" 
                        Text="" Visible="false" 
                        Font-Size="X-Small" Font-Bold="true"  ></asp:Label><br />
                    <asp:Label ID="Label8" runat="server" 
                        Text="Nel caso di annullamento per motivi non imputabili all’organizzatore di un pacchetto turistico costruito conformemente alle specifiche e personali indicazioni del cliente (viaggio su misura), questi sarà tenuto a rimborsare le eventuali spese sostenute per l’espletamento dell’incarico. Le condizioni assicurative sono presenti nella sezione 'Assicura' della pratica. Accettando il contratto il cliente accetta anche le condizioni assicurative." 
                        Font-Size="X-Small" ></asp:Label><br /><br />
                    
                    <div style="text-align:left">
                        <div style="position:absolute; margin: 0 0 0 480px">
                            <img src="../images/firmacontratto.gif" />
                        </div>
                        <asp:Label ID="lbldata" runat="server" Text="" Font-Bold="true" ></asp:Label><br /><br />
                        <asp:Label ID="Labelcontraente" runat="server" Text="" Font-Bold="true" ></asp:Label>
                    </div>
                    <div style="text-align:center; margin-top:50px;">
                        <asp:HyperLink ID="HyperContratto" Font-Bold="true" Font-Size="large" runat="server">Visualizza e stampa il contratto completo</asp:HyperLink>
                    </div>
                </div>
            
        </div>
        <div id="dettagliocontratto" runat="server" visible="false">
            <ul>
                <li style="font-weight:bold; font-size:medium;">
                    CONDIZIONI GENERALI DI CONTRATTO DI VENDITA DI PACCHETTI TURISTICI
                </li>
                <li>    
                    <p>1. FONTI LEGISLATIVE</p>
                    La vendita di pacchetti turistici, che abbiano ad oggetto servizi da fornire in territorio sia nazionale sia internazio-nale, è disciplinata – fino alla sua abrogazione ai sensi dell’art. 3 del D. Lgs. n. 79 del 23 maggio 2011 (il “Codice del Turismo”) - dalla L. 27/12/1977 n° 1084 di ratifica ed esecuzione della Convenzione Internazionale relativa al contratto di viaggio (CCV), firmata a Bruxelles il 23.4.1970 - in quanto applicabile - nonché dal Codice del Turismo (artt. 32-51) e sue successive modificazioni.
                </li>
                <li>
                    <p>2. REGIME AMMINISTRATIVO</p>
                    L’organizzatore e l’intermediario del pacchetto turistico, cui il turista si rivolge, devono essere abilitati all’esecuzione delle rispettive attività in base alla normativa amministrativa applicabile, anche regionale.
                    Ai sensi dell'art. 18, comma VI, del Cod. Tur., l'uso nella ragione o denominazione sociale delle parole "agenzia di viaggio", "agenzia di turismo" , "tour operator", "mediatore di viaggio" ovvero altre parole e locuzioni, anche in lingua straniera, di natura similare, è consentito esclusivamente alle imprese abilitate di cui al primo comma.
                </li>
                <li>
                    <p>3. DEFINIZIONI</p>
                    Ai fini del presente contratto s’intende per:
                    a) organizzatore di viaggio: il soggetto che si obbliga in nome proprio e verso corrispettivo forfetario, a procurare a terzi pacchetti turistici, realizzando la combinazione degli elementi di cui al seguente art. 4 o offrendo al turista, anche tramite un sistema di comunicazione a distanza, la possibilità di realizzare autonomamente ed acquistare tale combinazione;
                    b) intermediario: il soggetto che, anche non professionalmente e senza scopo di lucro, vende o si obbliga a procu-rare pacchetti turistici realizzati ai sensi del seguente art. 4 verso un corrispettivo forfetario;
                    c) turista: l'acquirente, il cessionario di un pacchetto turistico o qualunque persona anche da nominare, purché soddisfi tutte le condizioni richieste per la fruizione del servizio, per conto della quale il contraente principale si impegna ad acquistare senza remunerazione un pacchetto turistico.
                </li>
                <li>
                    <p>4. NOZIONE DI PACCHETTO TURISTICO</p>
                    La nozione di pacchetto turistico è la seguente:
                    “I pacchetti turistici hanno ad oggetto i viaggi, le vacanze, i circuiti “tutto compreso”, le crociere turistiche, risultanti dalla combinazione, da chiunque ed in qualunque modo realizzata, di almeno due degli elementi di seguito indicati, venduti od offerti in vendita ad un prezzo forfetario: a) trasporto; b) alloggio; c) servizi turistici non accessori al trasporto o all’alloggio di cui all’art. 36 che costituiscano per la soddisfazione delle esigenze ricreative del turista, parte significativa del “pacchetto turistico” (art. 34 Cod. Tur.).
                    Il turista ha diritto di ricevere copia del contratto di vendita di pacchetto turistico (redatto ai sensi e con le modalità di cui all’art. 35 Cod. Tur.). Il contratto costituisce titolo per accedere al Fondo di garanzia di cui al successivo art. 21.
                </li>
                <li>
                    <p>5. INFORMAZIONI AL TURISTA - SCHEDA TECNICA</p>
                    L’organizzatore predispone in catalogo o nel programma fuori catalogo – anche su supporto elettronico o per via telematica - una scheda tecnica. Gli elementi obbligatori della scheda tecnica del catalogo o del programma fuori catalogo sono:
                    - estremi dell’autorizzazione amministrativa o, se applicabile, la D.I.A. o S.C.I.A. dell’organizzatore;
                    - estremi della polizza assicurativa di responsabilità civile;
                    - periodo di validità del catalogo o del programma fuori catalogo;
                    - modalità e condizioni di sostituzione del viaggiatore (Art. 39 Cod. Tur.);
                    - parametri e criteri di adeguamento del prezzo del viaggio (Art. 40 Cod. Tur.).
                    L’organizzatore inserirà altresì nella scheda tecnica eventuali ulteriori condizioni particolari.
                    Al momento della conclusione del contratto l’organizzatore inoltre informerà i passeggeri circa l’identità del/i vetto-re/i effettivo/i , fermo quanto previsto dall’art. 11 del Reg. CE 2111/2005, e della sua/loro eventuale inclusione nella cd. “black list” prevista dal medesimo Regolamento.
                </li>
                <li>
                    <p>6. PRENOTAZIONI</p>
                    La proposta di prenotazione dovrà essere redatta su apposito modulo contrattuale, se del caso elettronico, compi-lato in ogni sua parte e sottoscritto dal cliente, che ne riceverà copia.
                    L’accettazione delle prenotazioni si intende perfezionata, con conseguente conclusione del contratto, solo nel momento in cui l’organizzatore invierà relativa conferma, anche a mezzo sistema telematico, al turista presso l’agenzia di viaggi intermediaria.
                    L’organizzatore fornirà prima della partenza le indicazioni relative al pacchetto turistico non contenute nei docu-menti contrattuali, negli opuscoli ovvero in altri mezzi di comunicazione scritta, come previsto dall’art. 37, comma 2 Cod. Tur.
                    Ai sensi dell’art. 32, comma 2, Cod. Tur., nel caso di contratti conclusi a distanza o al di fuori dei locali commerciali (come rispettivamente definiti dagli artt. 50 e 45 del D. Lgs. 206/2005), l’organizzatore si riserva di comunicare per iscritto l’inesistenza del diritto di recesso previsto dagli artt. 64 e ss. del D. Lgs. 206/2005.
                </li>
                <li>
                    <p>7. PAGAMENTI</p>
                    La misura dell’acconto, fino ad un massimo del 25% del prezzo del pacchetto turistico, da versare all’atto
                    della prenotazione ovvero all’atto della richiesta impegnativa e la data entro cui, prima della partenza, dovrà
                    essere effettuato il saldo, risultano dal catalogo, dall’opuscolo o da quanto altro.
                    Il mancato pagamento delle somme di cui sopra alle date stabilite costituisce clausola risolutiva espressa tale
                    da determinarne, da parte dell’agenzia intermediaria e/o dell’organizzatore, la risoluzione di diritto.
                </li>
                <li>
                    <p>8. PREZZO</p>
                    Il prezzo del pacchetto turistico è determinato nel contratto, con riferimento a quanto indicato in catalogo o pro-gramma fuori catalogo ed agli eventuali aggiornamenti degli stessi cataloghi o programmi fuori catalogo successi-vamente intervenuti. Esso potrà essere variato fino a 20 giorni precedenti la partenza e soltanto in conseguenza alle variazioni di:
                    - costi di trasporto, incluso il costo del carburante;
                    - diritti e tasse su alcune tipologie di servizi turistici quali imposte, tasse o diritti di atterraggio, di sbarco o di imbar-co nei porti e negli aeroporti;
                    - tassi di cambio applicati al pacchetto in questione.
                    Per tali variazioni si farà riferimento al corso dei cambi ed ai costi di cui sopra in vigore alla data di pubblicazione del programma, come riportata nella scheda tecnica del catalogo, ovvero alla data riportata negli eventuali aggior-namenti di cui sopra.
                    Le oscillazioni incideranno sul prezzo forfetario del pacchetto turistico nella percentuale espressamente indicata nella scheda tecnica del catalogo o programma fuori catalogo.
                </li>
                <li>
                    <p>9. MODIFICA O ANNULLAMENTO DEL PACCHETTO TURISTICO PRIMA DELLA PARTENZA</p>
                    Prima della partenza l'organizzatore o l’intermediario che abbia necessità di modificare in modo significativo uno o più elementi del contratto, ne dà immediato avviso in forma scritta al turista, indicando il tipo di modifica e la varia-zione del prezzo che ne consegue.
                    Ove non accetti la proposta di modifica di cui al comma 1, il turista potrà esercitare alternativamente il diritto di riacquisire la somma già pagata o di godere dell’offerta di un pacchetto turistico sostituivo ai sensi del 2° e 3° comma dell’articolo 10.
                    Il turista può esercitare i diritti sopra previsti anche quando l’annullamento dipenda dal mancato raggiungimento del numero minimo di partecipanti previsto nel Catalogo o nel Programma fuori catalogo o da casi di forza maggio-re e caso fortuito, relativi al pacchetto turistico acquistato.
                    Per gli annullamenti diversi da quelli causati da forza maggiore, da caso fortuito e da mancato raggiungimento del numero minimo di partecipanti, nonché per quelli diversi dalla mancata accettazione da parte del turista del pac-chetto turistico alternativo offerto, l’organizzatore che annulla, (Art. 33 lett. e Cod. Cons.) restituirà al turista il dop-pio di quanto dallo stesso pagato e incassato dall’organizzatore, tramite l’agente di viaggio.
                    La somma oggetto della restituzione non sarà mai superiore al doppio degli importi di cui il turista sarebbe in pari data debitore secondo quanto previsto dall’art. 10, 4° comma qualora fosse egli ad annullare.
                </li>
                <li>
                    <p>10. RECESSO DEL TURISTA</p>
                    Il turista può recedere dal contratto, senza pagare penali, nelle seguenti ipotesi:
                    - aumento del prezzo di cui al precedente art. 8 in misura eccedente il 10%;
                    - modifica in modo significativo di uno o più elementi del contratto oggettivamente configurabili come fondamentali ai fini della fruizione del pacchetto turistico complessivamente considerato e proposta dall’organizzatore dopo la conclusione del contratto stesso ma prima della partenza e non accettata dal turista.
                    Nei casi di cui sopra, il turista ha alternativamente diritto:
                    - ad usufruire di un pacchetto turistico alternativo, senza supplemento di prezzo o con la restituzione dell'ecceden-za di prezzo, qualora il secondo pacchetto turistico abbia valore inferiore al primo;
                    - alla restituzione della sola parte di prezzo già corrisposta. Tale restituzione dovrà essere effettuata entro sette giorni lavorativi dal momento del ricevimento della richiesta di rimborso. Il turista dovrà dare comunicazione della propria decisione (di accettare la modifica o di recedere) entro e non oltre due giorni lavorativi dal momento in cui ha ricevuto l’avviso di aumento o di modifica. In difetto di espressa comunicazione entro il termine suddetto, la proposta formulata dall’organizzatore si intende accettata.
                    Al turista che receda dal contratto prima della partenza al di fuori delle ipotesi elencate al primo comma, o nel caso previsto dall’art. 7, comma 2, saranno addebitati – indipendentemente dal pagamento dell’acconto di cui all’art. 7 comma 1 – il costo individuale di gestione pratica, la penale nella misura indicata nella scheda tecnica del Catalo-go o Programma fuori catalogo o viaggio su misura, l’eventuale corrispettivo di coperture assicurative già richieste al momento della conclusione del contratto o per altri servizi già resi.
                    Nel caso di gruppi precostituiti tali somme verranno concordate di volta in volta alla firma del contratto.
                </li>
                <li>
                    <p>11. MODIFICHE DOPO LA PARTENZA</p>
                    L’organizzatore, qualora dopo la partenza si trovi nell’impossibilità di fornire per qualsiasi ragione, tranne che per un fatto proprio del turista, una parte essenziale dei servizi contemplati in contratto, dovrà predisporre soluzioni alternative, senza supplementi di prezzo a carico del contraente e qualora le prestazioni fornite siano di valore inferiore rispetto a quelle previste, rimborsarlo in misura pari a tale differenza.
                    Qualora non risulti possibile alcuna soluzione alternativa, ovvero la soluzione predisposta dall’organizzatore venga rifiutata dal turista per comprovati e giustificati motivi, l’organizzatore fornirà senza supplemento di prezzo, un mezzo di trasporto equivalente a quello originario previsto per il ritorno al luogo di partenza o al diverso luogo eventualmente pattuito, compatibilmente alle disponibilità di mezzi e posti, e lo rimborserà nella misura della diffe-renza tra il costo delle prestazioni previste e quello delle prestazioni effettuate fino al momento del rientro anticipa-to.
                </li>
                <li>
                    <p>12. SOSTITUZIONI</p>
                    Il turista rinunciatario può farsi sostituire da altra persona sempre che:
                    a) l’organizzatore ne sia informato per iscritto almeno 4 giorni lavorativi prima della data fissata per la partenza, ricevendo contestualmente comunicazione circa le ragioni della sostituzione e le generalità del cessionario;
                    b) il cessionario soddisfi tutte le condizioni per la fruizione del servizio (ex art. 39 Cod. Tur. ) ed in particolare i requisiti relativi al passaporto, ai visti, ai certificati sanitari;
                    c) i servizi medesimi o altri servizi in sostituzione possano essere erogati a seguito della sostituzione;
                    d) il sostituto rimborsi all’organizzatore tutte le spese aggiuntive sostenute per procedere alla sostituzione, nella misura che gli verrà quantificata prima della cessione.
                    Il cedente ed il cessionario sono solidalmente responsabili per il pagamento del saldo del prezzo nonché degli importi di cui alla lettera d) del presente articolo.
                    Le eventuali ulteriori modalità e condizioni di sostituzione sono indicate in scheda tecnica.
                </li>
                <li>
                    <p>13. OBBLIGHI DEI TURISTI</p>
                    Nel corso delle trattative e comunque prima della conclusione del contratto, ai cittadini italiani sono fornite per iscritto le informazioni di carattere generale - aggiornate alla data di stampa del catalogo - relative agli obblighi sanitari e alla documentazione necessaria per l’espatrio. I cittadini stranieri reperiranno le corrispondenti infor-mazioni attraverso le loro rappresentanze diplomatiche presenti in Italia e/o i rispettivi canali informativi governa-tivi ufficiali.
                    In ogni caso i turisti provvederanno, prima della partenza, a verificarne l’aggiornamento presso le competenti Autorità (per i cittadini italiani le locali Questure ovvero il Ministero degli Affari Esteri tramite il sito www.viaggiaresicuri.it ovvero la Centrale Operativa Telefonica al numero 06.491115) adeguandovisi prima del viaggio. In assenza di tale verifica, nessuna responsabilità per la mancata partenza di uno o più turisti potrà essere imputata all’intermediario o all’organizzatore.
                    I turisti dovranno informare l’intermediario e l’organizzatore della propria cittadinanza e, al momento della par-tenza, dovranno accertarsi definitivamente di essere muniti dei certificati di vaccinazione, del passaporto indivi-duale e di ogni altro documento valido per tutti i Paesi toccati dall’itinerario, nonché dei visti di soggiorno, di transito e dei certificati sanitari che fossero eventualmente richiesti.
                    Inoltre, al fine di valutare la situazione sanitaria e di sicurezza dei Paesi di destinazione e, dunque, l’utilizzabilità oggettiva dei servizi acquistati o da acquistare, il turista reperirà (facendo uso delle fonti informative indicate al comma 2) le informazioni ufficiali di carattere generale presso il Ministero Affari Esteri che indica espressamente se le destinazioni sono o meno assoggettate a formale sconsiglio.
                    I turisti dovranno inoltre attenersi all’osservanza delle regole di normale prudenza e diligenza ed a quelle specifi-che in vigore nei Paesi destinazione del viaggio, a tutte le informazioni fornite loro dall’organizzatore, nonché ai regolamenti, alle disposizioni amministrative o legislative relative al pacchetto turistico. I turisti saranno chiamati a rispondere di tutti i danni che l’organizzatore e/o l’intermediario dovessero subire anche a causa del mancato rispetto degli obblighi sopra indicati, ivi incluse le spese necessarie al loro rimpatrio
                    Il turista è tenuto a fornire all’organizzatore tutti i documenti, le informazioni e gli elementi in suo possesso utili per l’esercizio del diritto di surroga di quest’ultimo nei confronti dei terzi responsabili del danno ed è responsabile verso l’organizzatore del pregiudizio arrecato al diritto di surrogazione.
                    Il turista comunicherà altresì per iscritto all’organizzatore, all’atto della prenotazione, le particolari richieste per-sonali che potranno formare oggetto di accordi specifici sulle modalità del viaggio, sempre che ne risulti possibile l’attuazione.
                    Il turista è sempre tenuto ad informare l’Intermediario e l’organizzatore di eventuali sue esigenze o condizioni particolari (gravidanza, intolleranze alimentari, disabilità, ecc…) ed a specificare esplicitamente la richiesta di relativi servizi personalizzati.
                </li>
                <li>
                    <p>14. CLASSIFICAZIONE ALBERGHIERA</p>
                    La classificazione ufficiale delle strutture alberghiere viene fornita in catalogo od in altro materiale informativo soltanto in base alle espresse e formali indicazioni delle competenti autorità del paese in cui il servizio è erogato.
                    In assenza di classificazioni ufficiali riconosciute dalle competenti Pubbliche Autorità dei Paesi anche membri della UE cui il servizio si riferisce, l’organizzatore si riserva la facoltà di fornire in catalogo o nel depliant una propria descrizione della struttura ricettiva, tale da permettere una valutazione e conseguente accettazione della stessa da parte del turista.
                </li>
                <li>
                    <p>15. REGIME DI RESPONSABILITÀ</p>
                    L’organizzatore risponde dei danni arrecati al turista a motivo dell’inadempimento totale o parziale delle presta-zioni contrattualmente dovute, sia che le stesse vengano effettuate da lui personalmente che da terzi fornitori dei servizi, a meno che provi che l’evento è derivato da fatto del turista (ivi comprese iniziative autonomamente assunte da quest’ultimo nel corso dell’esecuzione dei servizi turistici) o dal fatto di un terzo a carattere impreve-dibile o inevitabile, da circostanze estranee alla fornitura delle prestazioni previste in contratto, da caso fortuito, da forza maggiore, ovvero da circostanze che lo stesso organizzatore non poteva, secondo la diligenza profes-sionale, ragionevolmente prevedere o risolvere.
                    L’intermediario presso il quale sia stata effettuata la prenotazione del pacchetto turistico non risponde in alcun caso delle obbligazioni nascenti dall’organizzazione del viaggio, ma è responsabile esclusivamente delle obbli-gazioni nascenti dalla sua qualità di intermediario e, comunque, nei limiti previsti per tale responsabilità dalle norme vigenti in materia, salvo l’esonero di cui all’art. 46 Cod. Tur.
                </li>
                <li>
                    <p>16. LIMITI DEL RISARCIMENTO</p>
                    I risarcimenti di cui agli artt. 44, 45 e 47 del Cod. Tur. e relativi termini di prescrizione, sono disciplinati da quanto ivi previsto e comunque nei limiti stabiliti , dalla C.C.V, .dalle Convenzioni Internazionali che disciplinano le pre-stazioni che formano oggetto del pacchetto turistico nonché dagli articoli 1783 e 1784 del codice civile.
                </li>
                <li>
                    <p>17. OBBLIGO DI ASSISTENZA</p>
                    L’organizzatore è tenuto a prestare le misure di assistenza al turista secondo il criterio di diligenza professionale con esclusivo riferimento agli obblighi a proprio carico per disposizione di legge o di contratto.
                    L’organizzatore e l’intermediario sono esonerati dalle rispettive responsabilità (artt. 15 e 16 delle presenti Condi-zioni Generali), quando la mancata od inesatta esecuzione del contratto è imputabile al turista o è dipesa dal fatto di un terzo a carattere imprevedibile o inevitabile, ovvero è stata causata da un caso fortuito o di forza maggiore.
                </li>
                <li>
                    <p>18. RECLAMI E DENUNCE</p>
                    Ogni mancanza nell’esecuzione del contratto deve essere contestata dal turista durante la fruizione del pacchet-to mediante tempestiva presentazione di reclamo affinché l’organizzatore, il suo rappresentante locale o l’accompagnatore vi pongano tempestivamente rimedio. In caso contrario il risarcimento del danno sarà diminui-to o escluso ai sensi dell’art. 1227 c.c.
                    Il turista dovrà altresì – a pena di decadenza - sporgere reclamo mediante l’invio di una raccomandata,con avvi-so di ricevimento, o altro mezzo che garantisca la prova dell’avvenuto ricevimento, all’organizzatore o all’intermediario, entro e non oltre dieci giorni lavorativi dalla data di rientro nel luogo di partenza.
                </li>
                <li>
                    <p>19. ASSICURAZIONE CONTRO LE SPESE DI ANNULLAMENTO E DI RIMPATRIO</p>
                    Se non espressamente comprese nel prezzo, è possibile, ed anzi consigliabile, stipulare al momento della pre-notazione presso gli uffici dell’organizzatore o dell’intermediario speciali polizze assicurative contro le spese derivanti dall’annullamento del pacchetto turistico, da eventuali infortuni e da vicende relative ai bagagli traspor-tati. Sarà altresì possibile stipulare un contratto di assistenza che copra le spese di rimpatrio in caso di incidenti, malattie, casi fortuiti e/o di forza maggiore. Il turista eserciterà i diritti nascenti da tali contratti esclusivamente nei confronti delle Compagnie di Assicurazioni stipulanti, alle condizioni e con le modalità previste da tali polizze.
                </li>
                <li>
                    <p>20. STRUMENTI ALTERNATIVI DI RISOLUZIONE DELLE CONTESTAZIONI</p>
                    Ai sensi e con gli effetti di cui all’art. 67 Cod. Tur. l’organizzatore potrà proporre al turista - a catalogo, sul pro-prio sito o in altre forme - modalità di risoluzione alternativa delle contestazioni insorte . In tal caso l’organizzatore indicherà la tipologia di risoluzione alternativa proposta e gli effetti che tale adesione comporta.
                </li>
                <li>
                    <p>21. FONDO DI GARANZIA (art. 51 Cod. Tur.). Il Fondo Nazionale di Garanzia istituito a tutela dei turisti che siano in possesso di contratto, provvede alle seguenti esigenze in caso di insolvenza o di fallimento dichiarato dell’intermediario o dell’organizzatore:</p>
                    a) rimborso del prezzo versato;
                    b) rimpatrio nel caso di viaggi all’estero.
                    Il fondo deve altresì fornire un’immediata disponibilità economica in caso di rientro forzato di turisti da Paesi extracomunitari in occasione di emergenze imputabili o meno al comportamento dell’organizzatore.
                    Le modalità di intervento del Fondo sono stabilite col decreto del Presidente del Consiglio dei Ministri del 23/07/99, n. 349 e le istanze di rimborso al Fondo non sono soggette ad alcun termine di decadenza.
                    L’organizzatore e l’intermediario concorrono ad alimentare tale Fondo nella misura stabilita dal comma 2 del citato art. 51 Cod. Tur. attraverso il pagamento del premio di assicurazione obbligatoria che è tenuto a stipulare, una quota del quale viene versata al Fondo con le modalità previste dall’art. 6 del DM 349/99.
                </li>
                <li>
                    ADDENDUM
                    <p>CONDIZIONI GENERALI DI CONTRATTO DI VENDITA DI SINGOLI SERVIZI TURISTICI</p>
                    A) DISPOSIZIONI NORMATIVE
                    I contratti aventi ad oggetto l’offerta del solo servizio di trasporto, del solo servizio di soggiorno, ovvero di qua-lunque altro separato servizio turistico, non potendosi configurare come fattispecie negoziale di organizzazione di viaggio ovvero di pacchetto turistico, sono disciplinati dalle seguenti disposizioni della CCV: art. 1, n. 3 e n. 6; artt. da 17 a 23; artt. da 24 a 31 (limitatamente alle parti di tali disposizioni che non si riferiscono al contratto di organizzazione) nonché dalle altre pattuizioni specificamente riferite alla vendita del singolo servizio oggetto di contratto. Il venditore che si obbliga a procurare a terzi, anche in via telematica, un servizio turistico disaggrega-to, è tenuto a rilasciare al turista i documenti relativi a questo servizio, che riportino la somma pagata per il servi-zio e non può in alcun modo essere considerato organizzatore di viaggio.
                    B) CONDIZIONI DI CONTRATTO
                    A tali contratti sono altresì applicabili le seguenti clausole delle condizioni generali di contratto di vendita di pac-chetti turistici sopra riportate: art. 6 comma 1; art. 7 comma 2; art. 13; art. 18.
                    L’applicazione di dette clausole non determina assolutamente la configurazione dei relativi servizi come fattispe-cie di pacchetto turistico. La terminologia delle citate clausole relativa al contratto di pacchetto turistico (organiz-zatore, viaggio ecc.) va pertanto intesa con riferimento alle corrispondenti figure del contratto di vendita di singoli servizi turistici (venditore, soggiorno ecc.).
                </li>
            </ul>
        </div>
    </div>
    </form>
</body>
</html>
