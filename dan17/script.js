function ListEmployees() {
    let localEmployees = JSON.parse(localStorage.getItem("StoredEmployees")) || [];
    document.getElementById("empList").innerHTML = "<div class='grid-item'><p><b>First name</b></p></div>";
    document.getElementById("empList").innerHTML += "<div class='grid-item'><p><b>Last name</b></p></div>";
    document.getElementById("empList").innerHTML += "<div class='grid-item'><p><b>Date of birth</b></p></div>";
    for (i = 0; i <= localEmployees.length - 1; i++) {
        for (j = 0; j <= 2; j++) {
            document.getElementById("empList").innerHTML += "<div class='grid-item'><p>" + localEmployees[i][j] + "</p></div>";
        }
    }
}

function PostEmployee() {
    let firstName = document.getElementById("firstName").value;
    let lastName = document.getElementById("lastName").value;
    let dob = document.getElementById("dob").value;
    let localEmployees = JSON.parse(localStorage.getItem("StoredEmployees")) || [];
    let localArr = [firstName, lastName, dob];
    localEmployees.push(localArr);
    localStorage.clear();
    localStorage.setItem("StoredEmployees", JSON.stringify(localEmployees));
}

function SearchEmployees() {
    let localEmployees = JSON.parse(localStorage.getItem("StoredEmployees")) || [];

    document.getElementById("empList").innerHTML = "<div class='grid-item'><p><b>First name</b></p></div>";
    document.getElementById("empList").innerHTML += "<div class='grid-item'><p><b>Last name</b></p></div>";
    document.getElementById("empList").innerHTML += "<div class='grid-item'><p><b>Date of birth</b></p></div>";
  
    let searchResult = "";
    let search = document.getElementById("search").value;
    for (i = 0; i <= localEmployees.length - 1; i++) {
      for (j = 0; j <= 2; j++) {
        if ((localEmployees[i][j]).toLowerCase() == search.toLowerCase()) {
          searchResult += "<div class='grid-item'><p>" + localEmployees[i][0] + "</p></div>";
          searchResult += "<div class='grid-item'><p>" + localEmployees[i][1] + "</p></div>";
          searchResult += "<div class='grid-item'><p>" + localEmployees[i][2] + "</p></div>";
          break;
        }
      }
    }
    document.getElementById("empList").innerHTML += searchResult;
}