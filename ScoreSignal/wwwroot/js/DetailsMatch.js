const url = "/api/EvenementsAPI";
let $evenements = $("#evenement");
var idMatch = document.getElementById("matchId").value.trim();
var intId = parseInt(idMatch, 10);
console.log(intId);
getEvenements(intId);

function getEvenements(intId) {
    fetch(`${url}/${intId}`)
        .then(response => response.json())
        .then(data => data.forEach(evenement => {
                let template = "";

                if (evenement.Description === "BUT") {
                    template = `
                        <div class="card border-secondary mb-3 justify-content-center" style="max-width: 18rem;">
                            <div class="card-header">${evenement.temps}</div>
                            <div class="card-body text-secondary">
                                <h5 class="card-title">${evenement.Description}</h5>
                                <p class="card-text">${evenement.buteur}</p>
                            </div>
                        </div>`;
                } else if (evenement.Description === "Carton Jaune") {
                    template = `
                        <div class="card border-warning mb-3" style="max-width: 18rem;">
                            <div class="card-header">${evenement.temps}</div>
                            <div class="card-body text-warning">
                                <h5 class="card-title">${evenement.Description}</h5>
                                <p class="card-text">${evenement.buteur}</p>
                            </div>
                        </div>`;
                } else if (evenement.Description === "Carton Rouge") {
                    template = `
                        <div class="card border-danger mb-3" style="max-width: 18rem;">
                            <div class="card-header">Temps</div>
                            <div class="card-body text-danger">
                                <h5 class="card-title">${evenement.description}</h5>
                                <p class="card-text">${evenement.buteur}</p>
                            </div>
                        </div>`;
                } else {
                    template = `
                        <div class="card border-secondary mb-3" style="max-width: 18rem;">
                            <div class="card-header">${evenement.description}</div>
                            <div class="card-body text-secondary">
                                <h5 class="card-title">GOAL</h5>
                                <p class="card-text">${evenement.buteur}</p>
                            </div>
                        </div>`;
                }

                $evenements.append($(template));
            }))
            .catch(error => alert(error));
}

const connection = new signalR.HubConnectionBuilder().withUrl("/EvenementHub").build();
connection.start()
    .catch(function (err) { console.error(err.toString()); });

connection.on("EvenementChange", function () {
    $evenements.empty();
    getEvenements(intId);
});
