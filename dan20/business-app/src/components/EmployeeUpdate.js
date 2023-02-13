import axios from 'axios';
import React, { useState } from 'react'
import { Button, Col, Form, Row } from 'react-bootstrap'
import http from '../http-common'



export default function UpdateEmployee() {

    const queryParams = new URLSearchParams(window.location.search);
    const Id = queryParams.get('Id');
    const [firstName, setFirstName] = useState("");
    const [lastName, setLastName] = useState("");
    const [dateOfBirth, setDateOfBirth] = useState("");

    function EmployeeUpdate(e){
        var object = { 
        "FirstName": firstName,
        "LastName": lastName,
        "DateOfBirth": dateOfBirth,
        };
        http.put("/Employee?Id=" + Id , object).then(res => {
            console.log(res);
        });
        console.log(object);
    }

  return (
    <div>
        <Form>
            <Form.Group className='mb-3' controlId='formEmployeeFirstName'>
                <Row>
                    <Col>
                        <Form.Label>First Name:</Form.Label>
                    </Col>
                    <Col>
                        <Form.Control name='FirstName' value={firstName} onChange={(e)=>{setFirstName(e.target.value)}} type='text'/>
                    </Col>
                </Row>
            </Form.Group>
            <Form.Group className='mb-3' controlId='formLastName'>
                <Row>
                    <Col>
                        <Form.Label>Last Name:</Form.Label>
                    </Col>
                    <Col>
                        <Form.Control name='LastName' value={lastName} onChange={(e)=>{setLastName(e.target.value)}} type='text'/>
                    </Col>
                </Row>
            </Form.Group>
            <Form.Group className='mb-3' controlId='formDateOfBirth'>
                <Row>
                    <Col>
                        <Form.Label>Date Of Birth:</Form.Label>
                    </Col>
                    <Col>
                        <Form.Control name='DateOfBirth' value={dateOfBirth} onChange={e=>{setDateOfBirth(e.target.value)}} type='date'/>
                    </Col>
                </Row>
            </Form.Group>
            <Button type='submit' onClick={EmployeeUpdate}>Update</Button>
        </Form>
    </div>
  )
}