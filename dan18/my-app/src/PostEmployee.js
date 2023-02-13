//import './PostEmployee.css';

function PostEmployee() {
    return (
        <div class="container">
            <h1>Add employee</h1>
            <form id="employeeForm">
                <div class="row">
                    <div class="col">
                        <label for="firstName"><b>First name:</b></label><br/>
                        <input type="text" name="firstName" id="firstName" required />
                    </div>
                    <div class="col">
                        <label for="lastName"><b>Last name:</b></label><br></br>
                        <input type="text" name="lastName" id="lastName" required />

                    </div>
                    <div class="col">
                        <label for="dob"><b>Date of birth:</b></label><br />
                        <input type="date" name="dob" id="dob" required />
                    </div>
                    <div class="col"><br />
                        <button onClick={Post} class="btn btn-info" type="submit">Submit</button>
                    </div>
                </div>
            </form>
        </div>
    );
}

function Post() {
    let firstName = document.getElementById("firstName").value;
    let lastName = document.getElementById("lastName").value;
    let dob = document.getElementById("dob").value;
    let localEmployees = JSON.parse(localStorage.getItem("StoredEmployees")) || [];
    let localArr = [firstName, lastName, dob];
    localEmployees.push(localArr);
    localStorage.clear();
    localStorage.setItem("StoredEmployees", JSON.stringify(localEmployees));
}
export default PostEmployee;
