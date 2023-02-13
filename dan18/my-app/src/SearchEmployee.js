import './SearchEmployee.css';

function SearchEmployee() {
    
    return (
        <div class="container">
            <h1>Search employee</h1>
            <div class="grid">
                <div class="grid-item">
                    <input id="search" name="search" class="form-control mr-sm-2"
                        type="search" placeholder="Input"></input>
                </div>
                <div class="grid-item">
                    <button for="search" class="btn btn-outline-success my-2 my-sm-0" type="submit"
                        onClick={Search}>Search</button>
                </div>
            </div>
            <br></br>
            <button onClick={ListEmployees} class="btn btn-outline-success my-2 my-sm-0" type="submit">List
                all employees</button>
        </div>
    );
}
export default SearchEmployee;

function ListEmployees() {
    let localEmployees = JSON.parse(localStorage.getItem("StoredEmployees")) || [];
    document.getElementById("empList").innerHTML = "<div class='grid-item'><p><b>First name</b></p></div>";
    document.getElementById("empList").innerHTML += "<div class='grid-item'><p><b>Last name</b></p></div>";
    document.getElementById("empList").innerHTML += "<div class='grid-item'><p><b>Date of birth</b></p></div>";
    let i = 0;
    let j = 0;
    for (i = 0; i <= localEmployees.length - 1; i++) {
        for (j = 0; j <= 2; j++) {
            document.getElementById("empList").innerHTML += "<div class='grid-item'><p>" + localEmployees[i][j] + "</p></div>";
        }
    }
}
function Search() {
    let localEmployees = JSON.parse(localStorage.getItem("StoredEmployees")) || [];

    document.getElementById("empList").innerHTML = "<div class='grid-item'><p><b>First name</b></p></div>";
    document.getElementById("empList").innerHTML += "<div class='grid-item'><p><b>Last name</b></p></div>";
    document.getElementById("empList").innerHTML += "<div class='grid-item'><p><b>Date of birth</b></p></div>";
    let i = 0;
    let j = 0;
    let searchResult = "";
    let search = document.getElementById("search").value;
    for (i = 0; i <= localEmployees.length - 1; i++) {
      for (j = 0; j <= 2; j++) {
        if ((localEmployees[i][j]).toLowerCase() === search.toLowerCase()) {
          searchResult += "<div class='grid-item'><p>" + localEmployees[i][0] + "</p></div>";
          searchResult += "<div class='grid-item'><p>" + localEmployees[i][1] + "</p></div>";
          searchResult += "<div class='grid-item'><p>" + localEmployees[i][2] + "</p></div>";
          break;
        }
      }
    }
    document.getElementById("empList").innerHTML += searchResult;
}

