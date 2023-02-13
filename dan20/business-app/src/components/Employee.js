import axios from 'axios';
import React, { useState, useEffect } from 'react';
import { Button, Table } from 'react-bootstrap';
import http from "../http-common";
import './Employee.css';


export default function Employee() {
  const [employees, setEmployees] = useState([]);
  const [listElements, setListElements] = useState();


  useEffect(() => {
    setListElements(dataRows);
  }
    , [employees]);



  const FetchEmployees = () => {
    http.get("/employee/customers/all").then((res) => {
      setEmployees(res.data);
    });
  };

  const dataRows = () => (<>{employees.map((employee) =>
    <tr key={employee.Id}>
      <td>{employee.FirstName}</td>
      <td>{employee.LastName}</td>
      <td>{employee.DateOfBirth}</td>
      <td className="delete" onClick={DeleteEmployee} id={employee.Id}>DELETE</td>
      <td className="update" onClick={UpdateEmp} id={employee.Id}>UPDATE</td>
    </tr>)}</>);

  function UpdateEmp(e){
    console.log(e.target.id);
    const path="/updateEmployee?Id=" + e.target.id;
    window.location.href = path;
  }
  function DeleteEmployee(e) {
    console.log(e.target.id);
    const path = "/employee?Id=" + e.target.id;
    console.log(path);
    http.delete(path).then(res => {
      console.log(res);
      FetchEmployees();
    });

  }

  return (
    <div>
      <Button onClick={FetchEmployees}>Get all employees</Button>
      {employees.length > 0 &&
        <Table striped bordered hover>
          <thead>
            <tr>
              <th>First name</th>
              <th>Last name</th>
              <th>Date of birth</th>
            </tr>
            {listElements}
          </thead>
        </Table>
      }
    </div>
  )
}