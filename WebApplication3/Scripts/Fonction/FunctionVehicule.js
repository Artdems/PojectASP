$(document).ready(function () {

    $('.c-search').on('keyup', function (event) {
        var input = document.getElementById('searchInput').value;
        console.log(input);

        
    });
    

    $('#GetModal').on('show.bs.modal', function (e) {
        console.log("On show fonctionne");

        var button = e.relatedTarget;

        var id = button.id;
        var action = button.dataset.action;
        var type = button.dataset.type;
        var XHRGet = new XMLHttpRequest();

        XHRGet.addEventListener("load", function (event) {

            var champ = document.getElementById('ReturnBody');
            if (action != 'Suprimer') {
                document.getElementById('ModalDiv').style.height = "477px";
                document.getElementById('ModalDiv').style.width = "500px";
            }
            else {
                document.getElementById('ModalDiv').style.height = "250px";
                document.getElementById('ModalDiv').style.width = "500px";
            }
            champ.innerHTML = "<i class='fa fa-spinner fa-spin' style='font-size:100px;color:#072048' aligne='center'></i>";

            //alert(event.target.responseText);
            var champ = document.getElementById('ReturnBody');
            var titre = document.getElementById('ModalTitle');
            var btnCancel = document.getElementById('ModalCancel');
            var btnAction = document.getElementById('ModalAction');
            //var Return = document.getElementById('return');

            //btnAction.addEventListener("click", function (e) {
            //    sendData(action, type, id);
            //});
            btnAction.dataset.action = action;
            btnAction.dataset.type = type;
            btnAction.dataset.id = id;



            //Return.innerHTML = "";
            titre.innerHTML = action + " " + type;
            btnCancel.innerHTML = "Annuler";
            btnAction.innerHTML = action;
            var reponse = event.target.responseText;
            var myVar = setTimeout(loadModal(reponse), 10000);
        });

        XHRGet.addEventListener("error", function (event) {
            alert('Oups! Something goes wrong.');
        });

        XHRGet.open("GET", "/Home/" + action + type + "/" + id);

        XHRGet.send();
    });

    $(".onglet").on("click", function (e) {
        var button = e.relatedTarget;
        var type = this.dataset.type;
        var champ = document.getElementById('TableVehicule');
        var uri = '/Home/Tableau' + type + '/1';
        $.ajax({
            type: "POST",
            url: uri,
            contentType: "application/json; charset=utf-8",
            dataType: "html",
            success: function (response) {
                console.log(response);
                champ.innerHTML = response;
                console.log("responce");
                var lignes = document.getElementsByClassName('fournisseur');
                //$('.fournisseur').on('mouseover', function (e) {
                //    console.log('tr');
                //});
                console.log(lignes);
                for (var i = 0; i < lignes.lenght; i++) {
                    console.log(lignes[i]);
                    var ligne = lignes[i];
                    ligne.addEventListener("mouseover", function (e) {
                        console.log('event listner');
                    });
                }
            },
            failure: function (response) {
                alert(response.responseText);
            },
            error: function (response) {
                alert(response.responseText);
            }
        });
    });

    $('.fournisseur').on('mouseover', function (e) {
        console.log('tr');
        console.log(this);
        var id = this.dataset.id;
        console.log(id);
        var XHRGet = new XMLHttpRequest();

        XHRGet.addEventListener("load", function (event) {
            var div = document.getElementById('resultaFournisseur');
            var responce = event.target.responseText;
            div.innerHTML = responce;
        });

        XHRGet.addEventListener("error", function (event) {
            alert('Oups! Something goes wrong.');
        });

        XHRGet.open("GET", "/Home/TableauCommand/" + id);

        XHRGet.send();
    });
    $(".onglet").on('mouseover', function (e) {
        console.log("test x+1");
    });

    console.log("test x");


    $("#ModalAction").on("click", function (e) {
        sendData();
    });


});

function loadModal(reponse) {
    var champ = document.getElementById('ReturnBody');
    champ.innerHTML = reponse;
    var offsetHeight = document.getElementById('ModalDiv').offsetHeight;
    var offsetWidth = document.getElementById('ModalDiv').offsetWidth;
    console.log(offsetWidth);
    console.log(offsetHeight)
}

function search(search) {
    var input = document.getElementById('searchInput');
    input.value = search;
}





function sendData() {
    btnAction = document.getElementById('ModalAction');
    action = btnAction.dataset.action;
    type = btnAction.dataset.type;
    id = btnAction.dataset.id;
    console.log("action = " + action);
    console.log("type = " + type);
    console.log("id = " + id);
    var form = document.getElementById(action + type + 'Form');


    var XHRPost = new XMLHttpRequest();

    // Bind the FormData object and the form element
    var FD = new FormData(form);

    // Define what happens on successful data submission
    XHRPost.addEventListener("load", function (event) {

        var Return = document.getElementById('return');
        console.log("return = ");
        console.log(event.target.responseText);
        if (event.target.responseText == "") {
            location.reload();
            
        }
        else {
            Return.innerHTML = event.target.responseText;
        }

       
    });

    XHRPost.addEventListener("error", function (event) {
        alert('Oups! Something goes wrong.');

        //btnAction.removeEventListener("click", sendData(action, type, id, btnAction));
    });

    XHRPost.open("POST", "/Home/" + action + type + "/" + id);

    XHRPost.send(FD);

    
}

function supreCommand(id) {
    console.log(id)
    console.log(this);

    var uri = '/Home/ActiveCommand/' + id;
    $.ajax({
        type: "POST",
        url: uri,
        contentType: "application/json; charset=utf-8",
        dataType: "html",
        success: function (response) {
        },
        failure: function (response) {
            alert(response.responseText);
        },
        error: function (response) {
            alert(response.responseText);
        }
    });
}

function nav(id) {
    var champ = document.getElementById('TableVehicule');
    var uri = '/Home/TableauEntrer/' + id;
    $.ajax({
        type: "POST",
        url: uri,
        contentType: "application/json; charset=utf-8",
        dataType: "html",
        success: function (response) {
            champ.appendChild(response);
            console.log("responce");
            var table = document.getElementById("CustomerGrid");
            var lignes = table.getElementsByTagName('tr');

            for (var i = 0; i < lignes.lenght; i++){
                console.log(lignes[i]);
                var ligne = lignes[i];
                ligne.onmouseover = test;
            }
        },
        failure: function (response) {
            alert(response.responseText);
        },
        error: function (response) {
            alert(response.responseText);
        }
    });
}

