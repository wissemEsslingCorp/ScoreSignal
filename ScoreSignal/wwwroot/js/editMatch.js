const url = "/api/MatchsAPI";
const connection = new signalR.HubConnectionBuilder().withUrl("/matchHub").build();
connection.start()
    .catch(function (err) { 
        console.error(err.toString());
    });

document.querySelectorAll(".modifierMatch").forEach(function (button) {
    button.addEventListener("click", function (event) {
        var matchId = document.getElementById("matchId").value.trim();
        var id = parseInt(matchId, 10);
        var score1 = document.getElementById("score1");
        var score2 = document.getElementById("score2"); 
        var equipe1 = document.getElementById("equipe1").value;
        var equipe2 = document.getElementById("equipe2").value;
        var scoreEquipe1 = parseInt(document.getElementById("score1").value, 10);
        var scoreEquipe2 = parseInt(document.getElementById("score2").value, 10);
        var ligue = document.getElementById("ligue").value;
        var date = document.getElementById("date").value;
        
        var temps = document.getElementById("temps1");
        var momentDate = moment(date, "DD/MM/YYYY");

        if (momentDate.isValid()) {
            var dateMatch = momentDate.toISOString();
            console.log("Date convertie avec moment.js :", dateMatch);
        } else {
            console.error("Date non valide");
        }

       console.log("SCORE : " + scoreEquipe1); 

        var boutonValue = event.target.value;
        if (boutonValue === "but_equipe1") {
            scoreEquipe1++;
            score1.value = scoreEquipe1 ; 
        } else if (boutonValue === "but_equipe2") {
            scoreEquipe2++;
            score2.value = scoreEquipe2 ; 
        }else if (boutonValue === "demarrerMatch"){
            temps = "Première Mi-Temps"; 
            temps.value = "miTemps" ;   
            boutonValue.value = "miTemps";   
        }else if (boutonValue === "miTemps"){
            temps = "Mi-Temps";
            boutonValue.value ="deuxiemeMiTemps";  
        }else if (boutonValue === "deuxiemeMiTemps"){
            temps = "Deuxième Mi-Temps";
            boutonValue.value = "FinMatch"; 
        }else if ("FinMatch"){
            temps = "Fin du Match"; 
            boutonValue.value = "FinMatch"; 
        }
        console.log("SCORE : " + scoreEquipe1); 

    

        const match = {
            Matchid: id,
            equipe1: equipe1,
            equipe2: equipe2,
            ligue: ligue,
            scoreEquipe1: scoreEquipe1,
            scoreEquipe2: scoreEquipe2,
            date: dateMatch,
            Temps:temps
        };



        fetch(`${url}/${id}`, {
            method: "PUT",
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(match)
        })
            .then(response => {
                if (!response.ok) {
                    throw new Error(`Erreur HTTP! Statut: ${response.status}`);
                }
                return response.json();
            })
            .then(updatedMatch => {
                // Faites quelque chose avec les données mises à jour si nécessaire
                console.log("mis à jour:", updatedMatch);

                // Envoyez le message après la mise à jour réussie
                connection.invoke("SendMessage").catch(function (err) {
                    console.error(err.toString());
                });
            })
            .catch(error => {
                console.error("Erreur de l'API:", error);
                alert("Erreur API: " + error.message);
            });

        event.preventDefault();
    });
});

