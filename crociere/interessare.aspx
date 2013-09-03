<%@ Page Language="VB" AutoEventWireup="false" CodeFile="interessare.aspx.vb" Inherits="crociere_interessare" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css" >
        body{font-family:Arial; background: url(../images/interessare.gif?id=1) no-repeat fixed;}
        #risultato ul{list-style: none; padding:0px; margin:0px;}
        #risultato li{height:130px; margin: 40px 0 0 170px; list-style:none; font-size:small; color: #ffffff;}
        .dividi{float:left; width:60px; height:30px; background: #ffffff; margin-right:4px; text-align:center;}
    </style>
       <script language="javascript" type="text/javascript">
           var objdiv = new Array;
           var objlnk = new Array;
           var doct;
           var gira = 0;
           var intx;
           var varcounter8 = 100;
                    function closex() {
                        alert("pippo");
                        var obj = top.document.getElementById("inte");
                        var obj2 = top.document.getElementById("interessare");
                        obj.id = "inte2";

                        obj2.id = "interessare2";
                    }

                    function caricaprimo() {
                        doct = document.getElementById("ecco");                   
                        if (objdiv[0] != null) {
                            doct.innerHTML = objdiv[0].innerHTML;
                            doct.style.cursor = "pointer";
                            doct.setAttribute("onClick", objlnk[0]);    
                        }
                    }

                    function scrollinte(quanti) {                       
                        intx = window.setInterval("cambiainte('" + quanti + "')", 12000);
                    }

                    function cambiainte(quanti) {
                        //doct = document.getElementById("ecco");
                        if (gira <= quanti) {
                            doct.innerHTML = objdiv[gira].innerHTML;
                            doct.setAttribute("onClick", objlnk[gira]);
                            doct.style.cursor = "pointer";
                            gira = gira + 1;
                            varcounter8 = 100;
                            setInterval("varName8()", 20)
                        } else {
                            gira = 0;   
                        }
                    }

                    
                    var varName8 = function () {
                        if (varcounter8 > 0) {
                           varcounter8--;
                                if (top.document.getElementById("interessare") != null) {
                                    top.document.getElementById("interessare").style.right = varcounter8 + "%";
                                }                                                       
                        } else {                      
                            clearInterval(varName8);
                        }
                    };

                    
        </script>

</head>
<body>
    <form id="form1" runat="server">
    <div id="risultato">
        <ul>
            <li id="ecco"></li>
        </ul>
    </div>
    <div id="ris" style="visibility:hidden">
            <asp:Repeater ID="RepeaterInteressi" runat="server">
                        <ItemTemplate>
                             <li id="clicca" runat="server" style="cursor:pointer">
                                <div style="position:absolute; margin: -15px 0 0 -160px;"><asp:Image style="border:2px solid #ffffff;" id="fotop" imageurl='<%#databinder.eval(container.dataitem,"fotop")%>' Width="145px" Height="95px" runat="server"></asp:Image></div>
                                <asp:hyperlink ID="datanota" style="border:none; width:150px; font-weight:bold;"   runat="server" Text='<%#databinder.eval(container.dataitem,"titolo")%>'></asp:hyperlink><br />
                                <asp:label ID="lbldal" runat="server" Text="dal "></asp:label><asp:Label ID="dal"   runat="server" Text='<%#databinder.eval(container.dataitem,"dal", "{0:d}")%>'></asp:Label>&nbsp;&nbsp;
                                <asp:label ID="lblal" runat="server" Text="al "></asp:label><asp:Label ID="Label2"   runat="server" Text='<%#databinder.eval(container.dataitem,"al", "{0:d}")%>'></asp:Label><br />
                                <asp:label ID="Label1" runat="server" Text="partenza da "></asp:label><asp:Label ID="Label3"   runat="server" Text='<%#databinder.eval(container.dataitem,"imbarco")%>'></asp:Label><br />
                                <div style="margin-top:5px">
                                    <div class="dividi"><asp:label ID="Labeli" Font-Size="X-Small" ForeColor="Black" runat="server" Text="interna"></asp:label><br /><asp:Label ID="lbli" ForeColor="#A8001A" Font-Bold="true" runat="server" Text='<%#databinder.eval(container.dataitem,"prezzoi")%>'></asp:Label></div>
                                    <div class="dividi"><asp:label ID="Labele" Font-Size="X-Small" ForeColor="Black" runat="server" Text="finestra"></asp:label><br /><asp:Label ID="lble" ForeColor="#A8001A" Font-Bold="true" runat="server" Text='<%#databinder.eval(container.dataitem,"prezzoe")%>'></asp:Label></div>
                                    <div class="dividi"><asp:label ID="Labelb" Font-Size="X-Small" ForeColor="Black" runat="server" Text="balcone"></asp:label><br /><asp:Label ID="lblb" ForeColor="#A8001A" Font-Bold="true" runat="server" Text='<%#databinder.eval(container.dataitem,"prezzob")%>'></asp:Label></div>
                                    <asp:Label ID="idperiodo" Visible="false" runat="server" Text='<%#databinder.eval(container.dataitem,"id_periodo")%>'></asp:Label>
                                </div>
                            </li>                            
                        </ItemTemplate>
            </asp:Repeater>
    </div>
    </form>


</body>
</html>
