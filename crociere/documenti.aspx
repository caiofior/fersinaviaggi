<%@ Page Language="VB" AutoEventWireup="false" CodeFile="documenti.aspx.vb" Inherits="crociere_iti" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" href="documenti.css" type="text/css" />
           <link rel="stylesheet" type="text/css" media="all" href="../skins/aqua/theme.css" title="Aqua" />
    <script type="text/javascript" src="calendar.js"></script>
    <script type="text/javascript" src="calendar-it.js"></script>
    <script type="text/javascript" src="calendar-setup.js"></script>
    <script type="text/javascript" src="altezza.js"></script>
    <script type="text/javascript">
        function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode
            if (charCode > 31 && (charCode < 48 || charCode > 57))
                return false;
            return true;
        }

        function isNothingKey(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode
            if (charCode < 48 || charCode > 48)
                return false;
            return true;
        }

        function isCharKey(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode
            if (charCode > 31 && (charCode < 65 || charCode > 90) && (charCode < 97 || charCode > 122)) {
                if (charCode == 32) {
                    return true;
                } else {
                    return false;
                }
            }
            return true;
        }

    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div id="centroitx">
        <asp:Label ID="calendariosetup" runat="server" Text="" Visible="false"></asp:Label>
            <div id="dettagliodoc">
              <div id="cc" ><img src="../images/passaporto2.gif" alt="documenti" /></div>
                            <h1><asp:Label ID="Label29" runat="server" Text="DOCUMENTI:" Width="320px"></asp:Label></h1>
                                    <ul>
                                        <li style="height:auto;"><asp:Label ID="lbldocumenti" runat="server" Text=""></asp:Label></li>
                                    </ul>
                                    <br /><br />
                             <h1><asp:Label ID="Label14" runat="server" Text="INSERISCI I DOCUMENTI:" Width="200px"></asp:Label></h1> 
                                      <asp:label ID="codice" runat="server" Visible="false"></asp:label><asp:label ID="email" runat="server" Visible="false"></asp:label>
                                      <asp:label ID="regola" forecolor="#067788" Font-Size="Small" Font-Bold="true" Text="Gentile cliente, da nuovo regolamento internazionale necessitiamo dei dati relativi ai documenti di viaggio. Preghiamo di compilare tutti i campi sotto indicati prima possibile. L'invio del biglietto è subordinato alla compilazione dei dati." runat="server" Visible="false"></asp:label><br /><br />
                                      <ul>                        
                        <asp:Repeater ID="RepeaterPax" runat="server" Visible="false">
                            <ItemTemplate> 
                                <li style="width:675px; height:160px; border-bottom:2px solid #10AAC6;">
                                    <asp:Label ID="Label25" runat="server" forecolor="#067788" Text="Nome:" CssClass="ClI" Font-Bold="True"></asp:Label>                                    
                                    <asp:TextBox ID="nomeparte" runat="server" Text='<%#databinder.eval(container.dataitem,"nomecognome")%>' CssClass="XClL" Width="150px"  ReadOnly="true"></asp:TextBox>                                
                                    <asp:DropDownList ID="DropGiorno" CssClass="XClO" runat="server" Width="40px" Enabled="false" >
                                    </asp:DropDownList>                                    
                                    <asp:DropDownList ID="DropMese" CssClass="XClOO" runat="server" Width="40px" Enabled="false" >
                                    </asp:DropDownList>                                    
                                    <asp:DropDownList ID="DropAnno" CssClass="XClOOO" runat="server" Width="60px" Enabled="false" >
                                    </asp:DropDownList>  
                                    <asp:Label ID="datanascita" Text='<%#databinder.eval(container.dataitem,"datanascita")%>' runat="server" Visible="false" CssClass="ClN" Font-Bold="True"></asp:Label>                                                  
                                    <asp:Label ID="idnomi" Text='<%#databinder.eval(container.dataitem,"id_nomi")%>' runat="server" Visible="false" CssClass="ClN" Font-Bold="True"></asp:Label>                                                  
                                    <asp:Label ID="tipodocumento" Text='<%#databinder.eval(container.dataitem,"tipodocumento")%>' runat="server" Visible="false" CssClass="ClN" Font-Bold="True"></asp:Label>                                                  
                                    <asp:Label ID="docu" Text='<%#databinder.eval(container.dataitem,"documento")%>' runat="server" Visible="false" CssClass="ClN" Font-Bold="True"></asp:Label>                                                  
                                    <asp:Label ID="iemissione" Text='<%#databinder.eval(container.dataitem,"emissioned")%>' runat="server" Visible="false" CssClass="ClN" Font-Bold="True"></asp:Label>                                                  
                                    <asp:Label ID="iscadenza" Text='<%#databinder.eval(container.dataitem,"scadenzad")%>' runat="server" Visible="false" CssClass="ClN" Font-Bold="True"></asp:Label>                                                  
                                    <asp:Label ID="inazionalita" Text='<%#databinder.eval(container.dataitem,"nazionalita")%>' runat="server" Visible="false" CssClass="ClN" Font-Bold="True"></asp:Label>                                                  
                                    <asp:Label ID="inomereferente" Text='<%#databinder.eval(container.dataitem,"nomereferente")%>' runat="server" Visible="false" CssClass="ClN" Font-Bold="True"></asp:Label>                                                  
                                    <asp:Label ID="icognomereferente" Text='<%#databinder.eval(container.dataitem,"cognomereferente")%>' runat="server" Visible="false" CssClass="ClN" Font-Bold="True"></asp:Label>                                                  
                                    <asp:Label ID="itelreferente" Text='<%#databinder.eval(container.dataitem,"telreferente")%>' runat="server" Visible="false" CssClass="ClN" Font-Bold="True"></asp:Label>                                                  
                                    <asp:Label ID="iluogorilascio" Text='<%#databinder.eval(container.dataitem,"luogorilascio")%>' runat="server" Visible="false" CssClass="ClN" Font-Bold="True"></asp:Label>         
                                    <asp:Label ID="iluogonascita" Text='<%#databinder.eval(container.dataitem,"luogonascita")%>' runat="server" Visible="false" CssClass="ClN" Font-Bold="True"></asp:Label>                                                                                           
                                    <asp:Label ID="Label4"  runat="server" forecolor="#067788" Text="Nazionalità:" Font-Bold="true" CssClass="XCl0000000001"></asp:Label>
                                    <asp:Label ID="Label1"  runat="server" forecolor="#067788" Text="Documento:" Font-Bold="true" CssClass="XCl01"></asp:Label>
                                    <asp:RangeValidator ID="RangeValidator1" CssClass="XCl001" style="background:#ff0000;" width="122px" Height="25px"  ControlToValidate="Dropdocumento" ValidationGroup="valida"  runat="server" ErrorMessage="RangeValidator" MaximumValue="5" MinimumValue="1"></asp:RangeValidator>
                                    <asp:DropDownList  ID="Dropdocumento" CssClass="XCl001" runat="server" Width="120px" Height="22px" >
                                        <asp:ListItem Value="0">- Selezionare -</asp:ListItem>
                                        <asp:ListItem Value="1">Passaporto</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:Label ID="Label11"  runat="server" forecolor="#067788" Text="Luogo di nascita:" Font-Bold="true" CssClass="XCl10"></asp:Label>
                                    <asp:requiredfieldvalidator id="Requiredfieldvalidator5" runat="server" Width="208px" ValidationGroup="valida"  CssClass="XCl110" style="background:#ff0000;"  Display="Dynamic" ErrorMessage="." ControlToValidate="Luogonascita" Height="25px" Visible="true"></asp:requiredfieldvalidator>
                                    <asp:TextBox ID="Luogonascita" onkeypress="return isCharKey(event)"  runat="server" Text='' CssClass="XCl110" Width="200px" ></asp:TextBox>                                    
                                    <asp:Label ID="lblnum" forecolor="#067788" runat="server" Text="n." Font-Bold="true" CssClass="XCl0001"></asp:Label>    
                                    <asp:Label ID="Label5" forecolor="#067788" runat="server" Text="Luogo rilascio:" Font-Bold="true" CssClass="XCl000001"></asp:Label>
                                    <asp:requiredfieldvalidator id="vali3" runat="server" Width="78px" ValidationGroup="valida"  CssClass="XCl0000001" style="background:#ff0000;"  Display="Dynamic" ErrorMessage="." ControlToValidate="Luogo" Height="25px" Visible="true"></asp:requiredfieldvalidator>
                                    <asp:TextBox ID="Luogo" onkeypress="return isCharKey(event)"  runat="server" Text='' CssClass="XCl0000001" Width="70px" ></asp:TextBox>                                                                                                            
                                    <asp:requiredfieldvalidator id="Requiredfieldvalidator1" runat="server" Width="88px" ValidationGroup="valida"  CssClass="XCl00001" style="background:#ff0000;"  Display="Dynamic" ErrorMessage="." ControlToValidate="Luogo" Height="25px" Visible="true"></asp:requiredfieldvalidator>                
                                    <asp:TextBox ID="documento"  runat="server" Text='' CssClass="XCl00001" Width="80px" ></asp:TextBox>                                                        
                                    <asp:Label ID="Label2" forecolor="#067788" runat="server" Text="Emissione:" Font-Bold="true" CssClass="XCl10000001"></asp:Label> 
                                    <asp:requiredfieldvalidator id="vali1" runat="server" Width="75px" ValidationGroup="valida"  CssClass="XCl100000001" style="background:#ff0000;"  Display="Dynamic" ErrorMessage="." ControlToValidate="sel1" Height="25px" Visible="true"></asp:requiredfieldvalidator>
                                    <asp:TextBox ID="sel1" onkeypress="return isNothingKey(event)"  runat="server" Text='' CssClass="XCl100000001" Width="67px"  ></asp:TextBox>                                    
                                    <asp:Label ID="Label3" forecolor="#067788" runat="server" Text="Scadenza:" Font-Bold="true" CssClass="XCl00000001"></asp:Label>                                     
                                    <asp:requiredfieldvalidator id="vali2" runat="server" Width="75px" ValidationGroup="valida"  CssClass="XCl000000001" style="background:#ff0000;"  Display="Dynamic" ErrorMessage="." ControlToValidate="sel2" Height="25px" Visible="true"></asp:requiredfieldvalidator>
                                    <asp:TextBox ID="sel2" onkeypress="return isNothingKey(event)"  runat="server" Text='' CssClass="XCl000000001" Width="67px"  ></asp:TextBox>
                                    <asp:Label ID="Label6" forecolor="#FF708B" runat="server" Text="Familiare o amico che non viaggia con te da contattare in caso di necessità:" Font-Bold="true" CssClass="XCl201"></asp:Label> 
                                    <asp:Label ID="Label7" forecolor="#FF708B" runat="server" Text="Nome:" Font-Bold="true" CssClass="XCl202"></asp:Label>    
                                    <asp:requiredfieldvalidator id="Requiredfieldvalidator2" runat="server" Width="128px" ValidationGroup="valida"  CssClass="XCl203" style="background:#ff0000;"  Display="Dynamic" ErrorMessage="." ControlToValidate="nome" Height="25px" Visible="true"></asp:requiredfieldvalidator>                                 
                                    <asp:TextBox ID="nome" onkeypress="return isCharKey(event)"  runat="server" Text='' CssClass="XCl203" Width="120px"  ></asp:TextBox>
                                    <asp:Label ID="Label8" forecolor="#FF708B" runat="server" Text="Cognome:" Font-Bold="true" CssClass="XCl204"></asp:Label>   
                                    <asp:requiredfieldvalidator id="Requiredfieldvalidator3" runat="server" Width="128px" ValidationGroup="valida"  CssClass="XCl205" style="background:#ff0000;"  Display="Dynamic" ErrorMessage="." ControlToValidate="cognome" Height="25px" Visible="true"></asp:requiredfieldvalidator>                                  
                                    <asp:TextBox ID="cognome" onkeypress="return isCharKey(event)"  runat="server" Text='' CssClass="XCl205" Width="120px"  ></asp:TextBox>
                                    <asp:Label ID="Label9" forecolor="#FF708B" runat="server" Text="Telefono:" Font-Bold="true" CssClass="XCl206"></asp:Label>     
                                    <asp:requiredfieldvalidator id="Requiredfieldvalidator4" runat="server" Width="88px" ValidationGroup="valida"  CssClass="XCl207" style="background:#ff0000;"  Display="Dynamic" ErrorMessage="." ControlToValidate="telefono" Height="25px" Visible="true"></asp:requiredfieldvalidator>                                
                                    <asp:TextBox ID="Telefono" onkeypress="return isNumberKey(event)"  runat="server" Text='' CssClass="XCl207" Width="80px"  ></asp:TextBox>
                                    <asp:DropDownList ID="DropNazione" runat="server" CssClass="XCl00000000001" Width="195px">
                                        <asp:ListItem Value="AFG">AFGHANISTAN</asp:ListItem>
                                        <asp:ListItem Value="AL">ALBANIA</asp:ListItem>
                                        <asp:ListItem Value="DZ">ALGERIA</asp:ListItem> 
                                        <asp:ListItem Value="AS">AMERICAN SAMOA</asp:ListItem> 
                                        <asp:ListItem Value="AND">ANDORRA</asp:ListItem>
                                        <asp:ListItem Value="ANG">ANGOLA</asp:ListItem>
                                        <asp:ListItem Value="AI">ANGUILLA</asp:ListItem>
                                        <asp:ListItem Value="AG">ANTIGUA AND BARBUDA</asp:ListItem>
                                        <asp:ListItem Value="RA">ARGENTINA</asp:ListItem>
                                        <asp:ListItem Value="AM">ARMENIA</asp:ListItem>
                                        <asp:ListItem Value="AW">ARUBA</asp:ListItem>
                                        <asp:ListItem Value="AUS">AUSTRALIA</asp:ListItem>
                                        <asp:ListItem Value="A">AUSTRIA</asp:ListItem>
                                        <asp:ListItem Value="AZ">AZERBAIJAN</asp:ListItem>
                                        <asp:ListItem Value="BS">BAHAMAS</asp:ListItem>
                                        <asp:ListItem Value="BRN">BAHRAIN</asp:ListItem>
                                        <asp:ListItem Value="BD">BANGLADESH</asp:ListItem>
                                        <asp:ListItem Value="BDS">BARBADOS</asp:ListItem>
                                        <asp:ListItem Value="B">BELGIUM</asp:ListItem>
                                        <asp:ListItem Value="BH">BELIZE</asp:ListItem>
                                        <asp:ListItem Value="DY">BENIN</asp:ListItem>
                                        <asp:ListItem Value="BT">BHUTAN</asp:ListItem>
                                        <asp:ListItem Value="BIR">BIRMANIA</asp:ListItem>
                                        <asp:ListItem Value="BOL">BOLIVIA</asp:ListItem>
                                        <asp:ListItem Value="BON">Bonaire Netherlands Antille</asp:ListItem>
                                        <asp:ListItem Value="BA">BOSNIA</asp:ListItem>
                                        <asp:ListItem Value="RB">BOTSWANA</asp:ListItem>
                                        <asp:ListItem Value="BR">BRASIL</asp:ListItem>
                                        <asp:ListItem Value="VG">BRITISH VIRGIN ISLANDS</asp:ListItem>
                                        <asp:ListItem Value="BRU">BRUNEI</asp:ListItem>
                                        <asp:ListItem Value="BN">BRUNEL DARUSSALAM</asp:ListItem>
                                        <asp:ListItem Value="BG">BULGARIA</asp:ListItem>
                                        <asp:ListItem Value="BF">BURKINA FASO</asp:ListItem>
                                        <asp:ListItem Value="BUR">BURUNDI</asp:ListItem>
                                        <asp:ListItem Value="BY">BYELORUSSIAN SSR</asp:ListItem>
                                        <asp:ListItem Value="KH">CAMBODIA</asp:ListItem>
                                        <asp:ListItem Value="CAM">CAMEROON</asp:ListItem>
                                        <asp:ListItem Value="CDN">CANADA</asp:ListItem>
                                        <asp:ListItem Value="CVD">CAPE VERDE</asp:ListItem>
                                        <asp:ListItem Value="KY">CAYMAN ISLANDS</asp:ListItem>
                                        <asp:ListItem Value="RCA">CENTRAL AFRICAN REPUBLIC</asp:ListItem>
                                        <asp:ListItem Value="CHA">CHAD</asp:ListItem>
                                        <asp:ListItem Value="RCH">CHILE</asp:ListItem>
                                        <asp:ListItem Value="RC">CHINA</asp:ListItem>
                                        <asp:ListItem Value="CX">CHRISTAMS ISLANDS</asp:ListItem>
                                        <asp:ListItem Value="CC">COCOS KEELING ISLANDS</asp:ListItem>
                                        <asp:ListItem Value="CO">COLOMBIA</asp:ListItem>
                                        <asp:ListItem Value="COM">COMORE</asp:ListItem>
                                        <asp:ListItem Value="RCB">CONGO</asp:ListItem>
                                        <asp:ListItem Value="CK">COOK ISLANDS</asp:ListItem>
                                        <asp:ListItem Value="CR">COSTA RICA</asp:ListItem>
                                        <asp:ListItem Value="CRO">CROATIA</asp:ListItem>
                                        <asp:ListItem Value="C">CUBA</asp:ListItem>
                                        <asp:ListItem Value="CUR">CURACAO  NETHERLANDS  ANTILLE</asp:ListItem>
                                        <asp:ListItem Value="CY">CYPRUS</asp:ListItem>
                                        <asp:ListItem Value="CZ">CZECH REPUBLIC</asp:ListItem>
                                        <asp:ListItem Value="DK">DENMARK</asp:ListItem>
                                        <asp:ListItem Value="GIB">DJIBOUTI</asp:ListItem>
                                        <asp:ListItem Value="DM">DOMINICA</asp:ListItem>
                                        <asp:ListItem Value="DO">DOMINICAN REPUBLIC</asp:ListItem>
                                        <asp:ListItem Value="EC">ECUADOR</asp:ListItem>
                                        <asp:ListItem Value="ET">EGYPT</asp:ListItem>
                                        <asp:ListItem Value="SV">EL SALVADOR</asp:ListItem>
                                        <asp:ListItem Value="E">ESPANA</asp:ListItem>
                                        <asp:ListItem Value="EST">ESTONIA</asp:ListItem>
                                        <asp:ListItem Value="ETH">ETHIOPIA</asp:ListItem>
                                        <asp:ListItem Value="FO">FAEROE ISLANDS</asp:ListItem>
                                        <asp:ListItem Value="FK">FALKLAND ISLANDS MALVIN</asp:ListItem>
                                        <asp:ListItem Value="FJI">FIJI</asp:ListItem>
                                        <asp:ListItem Value="FI">FINLAND</asp:ListItem>
                                        <asp:ListItem Value="SF">FINLAND</asp:ListItem>
                                        <asp:ListItem Value="F">FRANCE</asp:ListItem>
                                        <asp:ListItem Value="FG">FRENCH GUIANA</asp:ListItem>
                                        <asp:ListItem Value="PF">FRENCH POLYNESIA</asp:ListItem>
                                        <asp:ListItem Value="GAB">GABON</asp:ListItem>
                                        <asp:ListItem Value="WAG">GAMBIA</asp:ListItem>
                                        <asp:ListItem Value="GE">GEORGIA</asp:ListItem>
                                        <asp:ListItem Value="D">GERMANY</asp:ListItem>
                                        <asp:ListItem Value="GH">GHANA</asp:ListItem>
                                        <asp:ListItem Value="GBZ">GIBRALTAR</asp:ListItem>
                                        <asp:ListItem Value="GB">GREAT BRITAIN</asp:ListItem>
                                        <asp:ListItem Value="GR">GREECE</asp:ListItem>
                                        <asp:ListItem Value="GRO">GREENLAND</asp:ListItem>
                                        <asp:ListItem Value="GD">GRENADA</asp:ListItem>
                                        <asp:ListItem Value="GP">GUADELOUPE/FRENCH WES</asp:ListItem> 
                                        <asp:ListItem Value="GU">GUAM</asp:ListItem>
                                        <asp:ListItem Value="GCA">GUATEMALA</asp:ListItem>
                                        <asp:ListItem Value="GUE">GUIANA EQUATORIALE</asp:ListItem>
                                        <asp:ListItem Value="GUI">GUINEA</asp:ListItem>
                                        <asp:ListItem Value="GUB">GUINEA BISSAU</asp:ListItem>
                                        <asp:ListItem Value="GUY">GUYANA</asp:ListItem>
                                        <asp:ListItem Value="GF">GUYANA FR.</asp:ListItem>
                                        <asp:ListItem Value="RH">HAITI</asp:ListItem>
                                        <asp:ListItem Value="HON">HONDURAS</asp:ListItem>
                                        <asp:ListItem Value="HK">HONG KONG</asp:ListItem>
                                        <asp:ListItem Value="H">HUNGARY</asp:ListItem>
                                        <asp:ListItem Value="IS">ICELAND</asp:ListItem>
                                        <asp:ListItem Value="IND">INDIA</asp:ListItem>
                                        <asp:ListItem Value="RI">INDONESIA</asp:ListItem>
                                        <asp:ListItem Value="IR">IRAN</asp:ListItem>
                                        <asp:ListItem Value="IRQ">IRAQ</asp:ListItem>
                                        <asp:ListItem Value="IRL">IRELAND</asp:ListItem>
                                        <asp:ListItem Value="IL">ISRAEL</asp:ListItem>
                                        <asp:ListItem Value="I" Selected="true">ITALY</asp:ListItem>
                                        <asp:ListItem Value="CI">IVORY COAST</asp:ListItem>
                                        <asp:ListItem Value="JA">JAMAICA</asp:ListItem>
                                        <asp:ListItem Value="J">JAPAN</asp:ListItem>
                                        <asp:ListItem Value="JOR">JORDAN</asp:ListItem>
                                        <asp:ListItem Value="KZ">KAZAKHSTAN</asp:ListItem>
                                        <asp:ListItem Value="EAK">KENYA</asp:ListItem>
                                        <asp:ListItem Value="KI">KIRIBATI</asp:ListItem>
                                        <asp:ListItem Value="KP">KOREA  DEM. PEOPLE REP</asp:ListItem> 
                                        <asp:ListItem Value="KR">KOREA  REPUBLIC OF</asp:ListItem>
                                        <asp:ListItem Value="KWT">KUWAIT</asp:ListItem>
                                        <asp:ListItem Value="KG">KYRGYSTAN</asp:ListItem>
                                        <asp:ListItem Value="LA">LAO PEOPLE DEM. REPUBLIC</asp:ListItem>
                                        <asp:ListItem Value="LV">LATVIA</asp:ListItem>
                                        <asp:ListItem Value="LET">LATVIA</asp:ListItem>
                                        <asp:ListItem Value="RL">LEBANON</asp:ListItem>
                                        <asp:ListItem Value="LS">LESOTHO</asp:ListItem>
                                        <asp:ListItem Value="LIB">LIBERIA</asp:ListItem>
                                        <asp:ListItem Value="LAR">LIBYA</asp:ListItem>
                                        <asp:ListItem Value="FL">LICHTENSTEIN</asp:ListItem>
                                        <asp:ListItem Value="LIT">LITHUANIA</asp:ListItem>
                                        <asp:ListItem Value="L">LUXEMBURG</asp:ListItem>
                                        <asp:ListItem Value="MO">MACAU</asp:ListItem>
                                        <asp:ListItem Value="MAC">MACEDONIA</asp:ListItem>
                                        <asp:ListItem Value="RM">MADAGASCAR</asp:ListItem>
                                        <asp:ListItem Value="MW">MALAWI</asp:ListItem>
                                        <asp:ListItem Value="MAL">MALAYSIA</asp:ListItem>
                                        <asp:ListItem Value="MLD">MALDIVE ISLANDS</asp:ListItem>
                                        <asp:ListItem Value="RMM">MALI</asp:ListItem>
                                        <asp:ListItem Value="M">MALTA</asp:ListItem>
                                        <asp:ListItem Value="MH">MARSHALL ISLANDS</asp:ListItem>
                                        <asp:ListItem Value="MQ">MARTINIQUE</asp:ListItem>
                                        <asp:ListItem Value="RIM">MAURITANIA</asp:ListItem>
                                        <asp:ListItem Value="MEX">MEXICO</asp:ListItem>
                                        <asp:ListItem Value="FM">MICRONESIA</asp:ListItem>
                                        <asp:ListItem Value="MD">MOLDOVA  REPUBLIC OF</asp:ListItem>
                                        <asp:ListItem Value="MON">MONGOLIA</asp:ListItem>
                                        <asp:ListItem Value="MS">MONTSERRAT</asp:ListItem>
                                        <asp:ListItem Value="MA">MOROCCO</asp:ListItem>
                                        <asp:ListItem Value="MOZ">MOZAMBIQUE</asp:ListItem>
                                        <asp:ListItem Value="MM">MYANMAR</asp:ListItem>
                                        <asp:ListItem Value="NA">NAMIBIA</asp:ListItem>
                                        <asp:ListItem Value="NR">NAURU</asp:ListItem>
                                        <asp:ListItem Value="NEP">NEPAL</asp:ListItem>
                                        <asp:ListItem Value="NL">NETHERLANDS</asp:ListItem>
                                        <asp:ListItem Value="NC">NEW  CALEDONIA</asp:ListItem>
                                        <asp:ListItem Value="NZ">NEW ZEALAND</asp:ListItem>
                                        <asp:ListItem Value="NIC">NICARAGUA</asp:ListItem>
                                        <asp:ListItem Value="RN">NIGER</asp:ListItem>
                                        <asp:ListItem Value="WAN">NIGERIA</asp:ListItem>
                                        <asp:ListItem Value="NU">NIUE</asp:ListItem>
                                        <asp:ListItem Value="NF">NORFOLK ISLAND</asp:ListItem>
                                        <asp:ListItem Value="MP">NORTHEN MARIANA ISLAND</asp:ListItem>
                                        <asp:ListItem Value="N">NORWAY</asp:ListItem>
                                        <asp:ListItem Value="OMA">OMAN</asp:ListItem>
                                        <asp:ListItem Value="PAK">PAKISTAN</asp:ListItem>
                                        <asp:ListItem Value="PW">PALAU ISLANDS</asp:ListItem>
                                        <asp:ListItem Value="PN">PALESTINE</asp:ListItem>
                                        <asp:ListItem Value="PA">PANAMA</asp:ListItem>
                                        <asp:ListItem Value="PG">PAPUA NEW GUINEA</asp:ListItem>
                                        <asp:ListItem Value="PY">PARAGUAY</asp:ListItem>
                                        <asp:ListItem Value="PE">PERU</asp:ListItem>
                                        <asp:ListItem Value="PH">PHILIPPINES</asp:ListItem>
                                        <asp:ListItem Value="RP">PHILIPPINES</asp:ListItem>
                                        <asp:ListItem Value="PL">POLAND</asp:ListItem>
                                        <asp:ListItem Value="P">PORTUGAL</asp:ListItem>
                                        <asp:ListItem Value="MC">PRINCIPATO DI MONACO</asp:ListItem>
                                        <asp:ListItem Value="PR">PUERTO RICO</asp:ListItem>
                                        <asp:ListItem Value="QUA">QATAR</asp:ListItem>
                                        <asp:ListItem Value="RE">REUNION</asp:ListItem>
                                        <asp:ListItem Value="RO">ROMANIA</asp:ListItem>
                                        <asp:ListItem Value="RUS">RUSSIA</asp:ListItem>
                                        <asp:ListItem Value="RWA">RWANDA</asp:ListItem>
                                        <asp:ListItem Value="KN">SAINT KITTS AND NEVIS</asp:ListItem>
                                        <asp:ListItem Value="VC">SAINT VICENT AND GRENADA</asp:ListItem>
                                        <asp:ListItem Value="WS">SAMOA</asp:ListItem>
                                        <asp:ListItem Value="RSM">SAN MARINO</asp:ListItem>
                                        <asp:ListItem Value="ST">SAO TOME AND PRINCIPE</asp:ListItem>
                                        <asp:ListItem Value="ARS">SAUDI ARABIA</asp:ListItem>
                                        <asp:ListItem Value="SN">SENEGAL</asp:ListItem>
                                        <asp:ListItem Value="SER">SERBIA</asp:ListItem>
                                        <asp:ListItem Value="SY">SEYCHELLES</asp:ListItem>
                                        <asp:ListItem Value="WAL">SIERRA LEONE</asp:ListItem>
                                        <asp:ListItem Value="SGP">SINGAPORE</asp:ListItem>
                                        <asp:ListItem Value="SK">SLOVAKIA</asp:ListItem>
                                        <asp:ListItem Value="SLO">SLOVENIA</asp:ListItem>
                                        <asp:ListItem Value="SB">SOLOMON ISLANDS</asp:ListItem>
                                        <asp:ListItem Value="SP">SOMALIA</asp:ListItem>
                                        <asp:ListItem Value="ZA">SOUTH AFRICA</asp:ListItem>
                                        <asp:ListItem Value="SRI">SRI LANKA</asp:ListItem> 
                                        <asp:ListItem Value="SH">ST. HELENA</asp:ListItem> 
                                        <asp:ListItem Value="PM">ST. PIERRE AND MIQUELON</asp:ListItem> 
                                        <asp:ListItem Value="SLU">ST.LUCIA</asp:ListItem> 
                                        <asp:ListItem Value="SL">STATELESS</asp:ListItem> 
                                        <asp:ListItem Value="SUD">SUDAN</asp:ListItem> 
                                        <asp:ListItem Value="SME">SURINAME</asp:ListItem> 
                                        <asp:ListItem Value="SD">SWAZILAND</asp:ListItem> 
                                        <asp:ListItem Value="S">SWEDEN</asp:ListItem> 
                                        <asp:ListItem Value="CH">SWITZERLAND</asp:ListItem> 
                                        <asp:ListItem Value="SYR">SYRIA</asp:ListItem> 
                                        <asp:ListItem Value="TAI">TAIWAN</asp:ListItem> 
                                        <asp:ListItem Value="TAN">TANZANIA</asp:ListItem> 
                                        <asp:ListItem Value="T">THAILAND</asp:ListItem> 
                                        <asp:ListItem Value="BER">THE BERMUDAS</asp:ListItem> 
                                        <asp:ListItem Value="TG">TOGO</asp:ListItem> 
                                        <asp:ListItem Value="TON">TONGA</asp:ListItem> 
                                        <asp:ListItem Value="TT">TRINIDAD E TOBAGO</asp:ListItem> 
                                        <asp:ListItem Value="TN">TUNIS</asp:ListItem> 
                                        <asp:ListItem Value="TR">TURKEY</asp:ListItem> 
                                        <asp:ListItem Value="TM">TURKMENISTAN</asp:ListItem> 
                                        <asp:ListItem Value="TUC">TURKS E CAICOS ISOLE</asp:ListItem> 
                                        <asp:ListItem Value="TV">TUVALU</asp:ListItem> 
                                        <asp:ListItem Value="UGA">UGANDA</asp:ListItem> 
                                        <asp:ListItem Value="UA">UKRAINE</asp:ListItem> 
                                        <asp:ListItem Value="EMU">UNIT ARABE EMIRAT</asp:ListItem> 
                                        <asp:ListItem Value="ROU">URUGUAY</asp:ListItem> 
                                        <asp:ListItem Value="UM">US MINOR OUTLYING ISLAND</asp:ListItem>
                                        <asp:ListItem Value="USA">USA</asp:ListItem>
                                        <asp:ListItem Value="UZ">UZBEKISTAN</asp:ListItem>
                                        <asp:ListItem Value="VU">VANATU</asp:ListItem>
                                        <asp:ListItem Value="YV">VENEZUELA</asp:ListItem>
                                        <asp:ListItem Value="VN">VIETNAM</asp:ListItem>
                                        <asp:ListItem Value="VI">VIRGIN ISLANDS  US</asp:ListItem>
                                        <asp:ListItem Value="WK">WAKE ISLAND</asp:ListItem>
                                        <asp:ListItem Value="WF">WALLIS AND FUTUNA ISLANDS</asp:ListItem>
                                        <asp:ListItem Value="YEM">YEMEN</asp:ListItem>
                                        <asp:ListItem Value="YUG">YUGOSLAVIA</asp:ListItem>
                                        <asp:ListItem Value="ZRE">ZAIRE</asp:ListItem> 
                                        <asp:ListItem Value="Z">ZAMBIA</asp:ListItem> 
                                        <asp:ListItem Value="ZW">ZIMBABWE</asp:ListItem>
                                    </asp:DropDownList>
                                </li>                                                          
                            </ItemTemplate>
                        </asp:Repeater>
                       </ul>
                        <%--<asp:Label ID="lbldocu" runat="server" Text="A conferma della pratica vi preghiamo inserire i documenti d'identità. Se attualmente non disponibili chiediamo di inserire i dati richiesti prima della partenza"></asp:Label>--%>
                        </div>
                        <br />
                        <asp:label ID="Labelvalida" forecolor="#FF0000" Font-Size="Small" Font-Bold="true" Text="I campi evidenziati in rosso sono incompleti. Preghiamo inserire tutti i dati" runat="server" Visible="false"></asp:label>
                     <div style="text-align:center; font-size:x-small;">   
                     <asp:Label ID="Label32" runat="server" Text="dichiaro di avere ricevuto le informazioni di cui all’art. 13 del D.lgs. 196/2003 in particolare riguardo ai diritti da me riconosciuti dalla legge ex art. 7 D.lgs. 196/2003, acconsento al trattamento dei miei dati con le modalità e per le finalità indicate nella informativa stessa, comunque strettamente connesse e strumentali alla gestione del rapporto contrattuale. Per leggere le condizioni relative  all'informativa della privacy "></asp:Label><a href="../privacy.html" target="_blank">premi qui.</a><br /><br />
                        <asp:Button ID="BttSalvadocumenti" runat="server" Text="Salva documenti d'identità" ValidationGroup="valida"  Font-Bold="True" Height="50px" />
                        <br /><br />
                        </div>
                        <asp:label ID="Label10" forecolor="#067788" Font-Size="Small" Font-Bold="true" Text="La carta d'identità scaduta prolungata con il timbro di prologa non è valida per l'ingresso negli stati fuori CEE (tipo Turchia, Tunisia, Egitto, ecc.). Se si è in possesso di questo documento preghiamo richiedere presso il proprio Comune un nuovo documento d'identità, in caso contrario non saranno imbarcati nei porti di origine i passeggeri prenotati nella crociera che prevede scali nei porti fuori Cee." runat="server" Visible="false"></asp:label>
                <script type="text/javascript">
                    Calendar.setup({ inputField: 'RepeaterPax_sel1_0', ifFormat: '%d/%m/%Y', button: 'RepeaterPax_sel1_0', align: 'Bl', position: [rx('RepeaterPax_sel1_0'), ry('RepeaterPax_sel1_0')], singleClick: true })
                    Calendar.setup({ inputField: 'RepeaterPax_sel2_0', ifFormat: '%d/%m/%Y', button: 'RepeaterPax_sel2_0', align: 'right', position: [rx('RepeaterPax_sel2_0'), ry('RepeaterPax_sel2_0')], singleClick: true })
                    Calendar.setup({ inputField: 'RepeaterPax_sel1_1', ifFormat: '%d/%m/%Y', button: 'RepeaterPax_sel1_1', align: 'right', position: [rx('RepeaterPax_sel1_1'), ry('RepeaterPax_sel1_1')], singleClick: true })
                    Calendar.setup({ inputField: 'RepeaterPax_sel2_1', ifFormat: '%d/%m/%Y', button: 'RepeaterPax_sel2_1', align: 'right', position: [rx('RepeaterPax_sel2_1'), ry('RepeaterPax_sel2_1')], singleClick: true })
                    Calendar.setup({ inputField: 'RepeaterPax_sel1_2', ifFormat: '%d/%m/%Y', button: 'RepeaterPax_sel1_2', align: 'right', position: [rx('RepeaterPax_sel1_2'), ry('RepeaterPax_sel1_2')], singleClick: true })
                    Calendar.setup({ inputField: 'RepeaterPax_sel2_2', ifFormat: '%d/%m/%Y', button: 'RepeaterPax_sel2_2', align: 'right', position: [rx('RepeaterPax_sel2_2'), ry('RepeaterPax_sel2_2')], singleClick: true })
                    Calendar.setup({ inputField: 'RepeaterPax_sel1_3', ifFormat: '%d/%m/%Y', button: 'RepeaterPax_sel1_3', align: 'right', position: [rx('RepeaterPax_sel1_3'), ry('RepeaterPax_sel1_3')], singleClick: true })
                    Calendar.setup({ inputField: 'RepeaterPax_sel2_3', ifFormat: '%d/%m/%Y', button: 'RepeaterPax_sel2_3', align: 'right', position: [rx('RepeaterPax_sel2_3'), ry('RepeaterPax_sel2_3')], singleClick: true })
                    Calendar.setup({ inputField: 'RepeaterPax_sel1_4', ifFormat: '%d/%m/%Y', button: 'RepeaterPax_sel1_4', align: 'right', position: [rx('RepeaterPax_sel1_4'), ry('RepeaterPax_sel1_4')], singleClick: true })
                    Calendar.setup({ inputField: 'RepeaterPax_sel2_4', ifFormat: '%d/%m/%Y', button: 'RepeaterPax_sel2_4', align: 'right', position: [rx('RepeaterPax_sel2_4'), ry('RepeaterPax_sel2_4')], singleClick: true })
                    Calendar.setup({ inputField: 'RepeaterPax_sel1_5', ifFormat: '%d/%m/%Y', button: 'RepeaterPax_sel1_5', align: 'right', position: [rx('RepeaterPax_sel1_5'), ry('RepeaterPax_sel1_5')], singleClick: true })
                    Calendar.setup({ inputField: 'RepeaterPax_sel2_5', ifFormat: '%d/%m/%Y', button: 'RepeaterPax_sel2_5', align: 'right', position: [rx('RepeaterPax_sel2_5'), ry('RepeaterPax_sel2_5')], singleClick: true })
                    Calendar.setup({ inputField: 'RepeaterPax_sel1_6', ifFormat: '%d/%m/%Y', button: 'RepeaterPax_sel1_6', align: 'right', position: [rx('RepeaterPax_sel1_6'), ry('RepeaterPax_sel1_6')], singleClick: true })
                    Calendar.setup({ inputField: 'RepeaterPax_sel2_6', ifFormat: '%d/%m/%Y', button: 'RepeaterPax_sel2_6', align: 'right', position: [rx('RepeaterPax_sel2_6'), ry('RepeaterPax_sel2_6')], singleClick: true })
                    Calendar.setup({ inputField: 'RepeaterPax_sel1_7', ifFormat: '%d/%m/%Y', button: 'RepeaterPax_sel1_7', align: 'right', position: [rx('RepeaterPax_sel1_7'), ry('RepeaterPax_sel1_7')], singleClick: true })
                    Calendar.setup({ inputField: 'RepeaterPax_sel2_7', ifFormat: '%d/%m/%Y', button: 'RepeaterPax_sel2_7', align: 'right', position: [rx('RepeaterPax_sel2_7'), ry('RepeaterPax_sel2_7')], singleClick: true })
                    Calendar.setup({ inputField: 'RepeaterPax_sel1_8', ifFormat: '%d/%m/%Y', button: 'RepeaterPax_sel1_8', align: 'right', position: [rx('RepeaterPax_sel1_8'), ry('RepeaterPax_sel1_8')], singleClick: true })
                    Calendar.setup({ inputField: 'RepeaterPax_sel2_8', ifFormat: '%d/%m/%Y', button: 'RepeaterPax_sel2_8', align: 'right', position: [rx('RepeaterPax_sel2_8'), ry('RepeaterPax_sel2_8')], singleClick: true })
                    Calendar.setup({ inputField: 'RepeaterPax_sel1_9', ifFormat: '%d/%m/%Y', button: 'RepeaterPax_sel1_9', align: 'right', position: [rx('RepeaterPax_sel1_9'), ry('RepeaterPax_sel1_9')], singleClick: true })
                    Calendar.setup({ inputField: 'RepeaterPax_sel2_9', ifFormat: '%d/%m/%Y', button: 'RepeaterPax_sel2_9', align: 'right', position: [rx('RepeaterPax_sel2_9'), ry('RepeaterPax_sel2_9')], singleClick: true })
                    function rx(nomediv) {
                        if (document.getElementById(nomediv) != null) {
                            var x = document.getElementById(nomediv).offsetLeft;
                            var xx = document.body.clientWidth;
                            var posx = Math.floor((xx + x) / 2) - 220;
                            return x -150
                        } else {
                            return null;
                        }
                    }
                    function ry(nomediv) {
                        if (document.getElementById(nomediv) != null) {
                            var y = document.getElementById(nomediv).offsetTop;
                            var yy = document.body.clientHeight;
                            var posy = Math.floor((yy + y) / 2);
                            return y +20
                        } else {
                            return null;
                        }
                    }
               </script>
            
      </div>              
    </form>

</body>
</html>
