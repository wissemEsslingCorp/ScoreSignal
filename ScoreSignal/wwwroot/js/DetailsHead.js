const url2= "/api/MatchsAPI";
let $matchs = $("#match");
var idMatch = document.getElementById("matchId").value.trim(); 
var intId = parseInt(idMatch, 10);
console.log(intId)
getMatchs(intId);
function getMatchs(intId) {
    fetch(`${url2}/${intId}`)
        .then(response => response.json())
        .then(match => {

   
            let template = `<tr>
            <td class="text-center" colspan="3">Évènements du match</td>
            </tr>
            <tr>
            <td class="text-right">${match.equipe1}</td>
            <td class="text-center"> ${match.scoreEquipe1} - ${match.scoreEquipe2}</td>
            <td>${match.equipe2}</td>
            </tr> 
            <tr>
            <td class="text-center" colspan="3">  
            ${
                match.temps === null
                    ? match.date
                    : match.temps
            }
        </td>
          </tr> `;
            $matchs.append($(template));
           
        })
        .catch(error => alert("Erreur API"));
}

const connection2 = new signalR.HubConnectionBuilder().withUrl("/matchHub").build();
connection2.start()
    .catch(function (err) { return console.error(err.tostring()) })
connection2.on("MatchChange", function () {
    $matchs.empty();
    getMatchs(intId);
});