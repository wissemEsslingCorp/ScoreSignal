const url = "/api/MatchsAPI";
const connection = new signalR.HubConnectionBuilder().withUrl("/matchHub").build();
connection.start()
    .catch(function (err) { return console.error(err.tostring()) })
document.getElementById("createbt").addEventListener("click", function (event) {
    var equipe1 = document.getElementById("equipe1").value;
    var equipe2 = document.getElementById("equipe2").value;
    var date = document.getElementById("date").value;
    var ligue = document.getElementById("ligue").value;
   
    const match =
    {
         equipe1: equipe1, equipe2: equipe2,ligue: ligue, scoreEquipe1 :0, scoreEquipe2 : 0 , date: date
    };
    fetch(url, {
        method: "POST",
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(match)
    })
        .then(response => response.json())
        .then(() => {
            console.log("Réponse de la requête POST :");
            connection.invoke("SendMessage").catch(function (err) {
                return console.error(err.toString());
            });
        })
        .catch(error => alert("Erreur API"+ error ));
    event.preventDefault();
});