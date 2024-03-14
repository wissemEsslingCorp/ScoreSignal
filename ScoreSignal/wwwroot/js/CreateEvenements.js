const url3 = "/api/EvenementsAPI";
const connection3 = new signalR.HubConnectionBuilder().withUrl("/evenementHub").build();

connection3.start()
    .catch(function (err) { return console.error(err.toString()); });

document.getElementById("createEvenement").addEventListener("click", function (event) {
    var description = document.getElementById("description").value;
    var buteur = document.getElementById("buteur").value;
    var tempsInput = document.getElementById("Temps");
    var temps = tempsInput.value;
    var idDuMatch = document.getElementById("idDuMatch").value.trim();
    var idDuMatchInt = parseInt(idDuMatch, 10);

    // Ajout de la date actuelle pour obtenir une date complète avec l'heure sélectionnée
    var currentDate = new Date();
    var formattedDate = currentDate.toISOString().split('T')[0];
    var formattedDateTime = formattedDate + ' ' + temps;

    var momentDate2 = moment(formattedDateTime, "YYYY-MM-DD HH:mm");
    var heureMatch;

    if (momentDate2.isValid()) {
        heureMatch = momentDate2.toISOString();
        console.log("Date convertie avec moment.js :", heureMatch);
    } else {
        console.error("Date non valide");
        // Gérer le cas où la date n'est pas valide
        return;
    }

    const evenement =
    {
         id : 0,  Buteur: buteur, Description: description, Temps: heureMatch, MatchId: idDuMatchInt
    };
    console.log("EVENEMENT : "+ evenement)
    fetch(url3, {
        method: "POST",
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(evenement)
    })
        .then(response => response.json())
        .then(data => {
            console.log("Réponse de la requête POST :");
            connection3.invoke("SendMessage").catch(function (err) {
                return console.error(err.toString());
            });
        })
        .catch(error => alert("Erreur API" + error));

    event.preventDefault();
});
