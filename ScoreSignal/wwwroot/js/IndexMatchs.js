const url = "/api/MatchsAPI";
let $matchs = $("#matchs");
var ligue = document.getElementById("ligue").value
getMatchs(ligue);
function getMatchs(ligue) {
    fetch(`${url}?ligue=${ligue}`)
        .then(response => response.json())
        .then(data => data.forEach(match => {
            
            let template = `<tr class="text-white">
                <th>${match.equipe1}</th>
                <td class="font-white-bold text-success">${match.scoreEquipe1} - ${match.scoreEquipe2}</td>
                <td>${match.equipe2}</td>
                <td>${match.temps === null ? "À Venir" : match.temps}</td>
                <td>${match.ligue}</td>
                <td><a href="/Matchs/Details/${match.matchId}">Details</a> </td>
                <td><a href="/Matchs/Edit/${match.matchId}">Modifier</a> </td>
                </tr>`;
            $matchs.append($(template));
            console.log(match.date)
        }))
        .catch(error => alert("Erreur API"));
}
const connection = new signalR.HubConnectionBuilder().withUrl("/matchHub").build();
connection.start()
    .catch(function (err) { return console.error(err.tostring()) })
connection.on("MatchChange", function () {
    $matchs.empty();
    getMatchs(ligue);
});