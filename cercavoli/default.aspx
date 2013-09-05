<% @ Page Language="VB" MasterPageFile="master\cercavoli.master" CodeFile="default.aspx.vb" Inherits="FlySearch" Title="Content Page 1" %>
<asp:Content ID="Results" ContentPlaceHolderID="Results" Runat="Server">
<div id="fly_search">
            <form id="fly_search_form" name="fly_search_form" method="post" action="results.aspx" >
               <div class="ui-widget">
                <div>
                 <label for="departure_location_name">Partenza: </label>
                 <input id="departure_location_name" name="departure_location_name" />
                 <input type="hidden" id="departure_location_info" name="departure_location_info" />
                 <span style="display:none;" class="error departure_location_name_error">La partenza è richiesta</span>
                </div>
                <div>
                 <label for="arrival_location_name">Destinazione: </label>
                 <input id="arrival_location_name" name="arrival_location_name" />
                 <input type="hidden" id="arrival_location_info" name="arrival_location_info" />
                 <span style="display:none;" class="error arrival_location_name_error">La destinazione è richiesta</span>
                 </div>
                 <div>
                 <label for="departure_datetime">Andata: </label>
                 <input id="departure_datetime" name="departure_datetime" />
                 <span style="display:none;" class="error departure_datetime_error">L'andata è richiesta</span>
                 </div>
                 <div>
                 <label for="arrival_datetime">Ritorno: </label>
                 <input id="arrival_datetime" name="arrival_datetime" value="Sola andata"/>
                 <a id="reset_oneway" style="display:none;" href="#">Sola andata</a>
                 </div>
                 <div>
                 <label for="adult">Adulti:</label>
                 <select id="adult">
                    <option>1</option>
                    <option>2</option>
                    <option>3</option>
                    <option>4</option>
                    <option>5</option>
                    <option>6</option>
                    <option>7</option>
                    <option>8</option>
                    <option>9</option>
                    <option>10</option>
                 </select>
                 </div>
                 <div>
                 <label for="children">Bambini:</label>
                 <select id="children">
                    <option>0</option>
                    <option>1</option>
                    <option>2</option>
                    <option>3</option>
                    <option>4</option>
                    <option>5</option>
                    <option>6</option>
                    <option>7</option>
                    <option>8</option>
                    <option>9</option>
                    <option>10</option>
                 </select>
                 </div>
                 <div>
                 <label for="enfant">Neonati:</label>
                 <select id="enfant">
                    <option>0</option>
                    <option>1</option>
                    <option>2</option>
                    <option>3</option>
                    <option>4</option>
                    <option>5</option>
                    <option>6</option>
                    <option>7</option>
                    <option>8</option>
                    <option>9</option>
                    <option>10</option>
                 </select>
                 </div>
               </div>
               <div>
               <button type="submit">Cerca Volo</button>
               </div>
            </form>
            </div>
<div id="fly_search_right_content">
<img class="main_image" alt="Vola con noi" title="Vola con noi" src="images/fly.jpg" width="700" height="242"/>
</div>
</asp:Content>