const url2= "/api/MatchsAPI";
let $matchs = $("#match");
var idMatch = document.getElementById("matchId").value.trim(); 
var intId = parseInt(idMatch, 10);
getMatchs(intId);
function getMatchs(intId) {
    fetch(`${url2}/${intId}`)
        .then(response => response.json())
        .then(match => {

            let demarrerMatchBtn = $("#tempsBtn");
           
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
            console.log(match);
            console.log("TEEEEST editmatchVue" +  demarrerMatchBtn.lenght);
            // Mettez à jour le bouton en fonction du temps du match
            if (match.temps == null) {
                demarrerMatchBtn.text("Démarrer le Match");
                demarrerMatchBtn.val("demarrerMatch");
            } else if (match.temps == "Première Mi-Temps") {
                demarrerMatchBtn.text("Mi-Temps");
                demarrerMatchBtn.val("miTemps");
            } else if (match.temps == "Mi-Temps") {
                demarrerMatchBtn.text("Démarrer la deuxième Mi-Temps");
                demarrerMatchBtn.val("deuxiemeMiTemps");
            } else if (match.temps == "Deuxième Mi-Temps") {
                demarrerMatchBtn.text("Arrêter le Match");
                demarrerMatchBtn.val("FinMatch");
            }
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