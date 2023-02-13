import React, { useState } from 'react';
import { Button, Col, Form, Row } from 'react-bootstrap';
import http from '../http-common';
import './EmployeeForm.css';

export default function EmployeeForm() {
    const [firstName, setFirstName] = useState("");
    const [lastName, setLastName] = useState("");
    const [dateOfBirth, setDateOfBirth] = useState(new Date());

    function PostEmployee(e){
        var object = { 
        "FirstName": firstName,
        "LastName": lastName,
        "DateOfBirth": dateOfBirth,
        };
        http.post("/Employee", object).then(res => {
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
                        <Form.Control name='FirstName' value={firstName} onChange={(e)=>{setFirstName(e.target.value)}} type='text' required/>
                    </Col>
                </Row>
            </Form.Group>
            <Form.Group className='mb-3' controlId='formLastName'>
                <Row>
                    <Col>
                        <Form.Label>Last Name:</Form.Label>
                    </Col>
                    <Col>
                        <Form.Control name='LastName' value={lastName} onChange={(e)=>{setLastName(e.target.value)}} type='text' required/>
                    </Col>
                </Row>
            </Form.Group>
            <Form.Group className='mb-3' controlId='formDateOfBirth'>
                <Row>
                    <Col>
                        <Form.Label>Date Of Birth:</Form.Label>
                    </Col>
                    <Col>
                        <Form.Control name='DateOfBirth' value={dateOfBirth} onChange={e=>{setDateOfBirth(e.target.value)}} type='date' required/>
                    </Col>
                </Row>
            </Form.Group>
            <Button type='submit' onClick={PostEmployee}>Submit</Button>
        </Form>
    </div>
  )
}