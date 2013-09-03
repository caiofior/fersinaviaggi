<%@ Page Language="VB" AutoEventWireup="false" CodeFile="navedove.aspx.vb" Inherits="crociere_navedove" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" href="navedove.css?id=1" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div id="itinave">
        <ul>
            <asp:Repeater ID="Repeaternave" runat="server">
                <ItemTemplate> 
                    <li>
                        <img id="fotoiti" runat="server" style="width:250px" src='<%#databinder.eval(container.dataitem,"mappa")%>'  /><br />
                        <asp:Label ID="id_itinerario" Visible="false" runat="server" Text='<%#databinder.eval(container.dataitem,"id_periodo")%>'></asp:Label>  
                                 
                        <asp:Label ID="Labelconta" Visible="false" runat="server" Text='<%#databinder.eval(container.dataitem,"conta")%>'></asp:Label>             
                        <asp:Label ID="dal" Visible="false"  runat="server" Text='<%#databinder.eval(container.dataitem,"dal", "{0:d}")%>'></asp:Label>
                        <asp:Label ID="imbarco" Visible="false"  runat="server" Text='<%#databinder.eval(container.dataitem,"imbarco")%>'></asp:Label>            
                        <asp:Label ID="durata" Visible="false"  runat="server" Text='<%#databinder.eval(container.dataitem,"durata")%>'></asp:Label>                 
                        <asp:Label ID="Labeldescri"  runat="server" Text=''></asp:Label><br /> <br />
                    </li>
                </ItemTemplate>
            </asp:Repeater>
        </ul>    
    </div>
    </form>
</body>
</html>
