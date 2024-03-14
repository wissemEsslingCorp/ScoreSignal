const url5 = "/api/MatchsAPI";
let $temps = $("#temps");
var idMatch = document.getElementById("matchId").value.trim();
var intId = parseInt(idMatch, 10);
console.log(intId)
getMatchs(intId);

function getMatchs(intId) {
    fetch(`${url5}/${intId}`)
        .then(response => response.json())
        .then(match => {
            let demarrerMatchBtn = $("#demarrerMatchBtn");

          

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

            console.log(match);
            console.log("TEST");
        })
        .catch(error => alert("Erreur API"));
}

const connection5 = new signalR.HubConnectionBuilder().withUrl("/matchHub").build();
connection3.start()
    .catch(function (err) { return console.error(err.tostring()) })

connection5.on("MatchChange", function () {
    $matchs.empty();
    getMatchs(intId);
});
