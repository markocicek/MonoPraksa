import React from 'react'
import { Container, Nav, Navbar } from 'react-bootstrap'
import './Navigation.css';

export default function () {
  return (
    <Navbar variant='dark' expand='lg'>
        <Container fluid>
            <Nav>
                <Nav.Link href='/'>Employees</Nav.Link>
                <Nav.Link href='/postEmployee'>Add employee</Nav.Link>
                
            </Nav>
        </Container>
    </Navbar>
  )
}