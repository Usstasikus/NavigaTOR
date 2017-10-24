var placesList = document.getElementById("placesList");
// var input = document.getElementById("placesInput");
function newElement(e, input) {
    var code = (e.keyCode ? e.keyCode : e.which);
    if (code == 13) {
        var listElement = document.createElement("li");
        listElement.className = "list-group-item";
        var text = document.createTextNode(input.value);
        listElement.appendChild(text);
        placesList.appendChild(listElement);
        input.value = ""
    }
}

// localStorage["placesList"] = document.getElementById("placesList"));
// localStorage.setItem("i", 0)
// function newElement(){
//   var i = localStorage.getItem("i");
//   var placesList = localStorage["placesList"];
//   listElement.className = "list-group-item";
//   var text = document.createTextNode(i);
//   listElement.appendChild(text);
//   placesList.appendChild(listElement);
// }
