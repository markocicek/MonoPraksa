import './App.css';
import { Button, Container } from 'react-bootstrap';
import 'bootstrap/dist/css/bootstrap.css';
import { Route, Routes } from "react-router-dom";
import Employee from './components/Employee';
import EmployeeForm from './components/EmployeeForm';
import Navigation from './components/Navigation';
import EmployeeUpdate from './components/EmployeeUpdate';


function App() {
  const fields = ['FirstName', 'LastName', 'DateOfBirth'];
  return (
    <div className="App">
      <header className="App-header">
        <Navigation />
      </header>
      <main>
        <Container>
          <Routes>
            <Route path='/' element={<Employee />} />
            <Route path='/postEmployee' element={<EmployeeForm fields={fields} />} />
            <Route path='/updateEmployee' element={<EmployeeUpdate Id/>}/>
          </Routes>
        </Container>


      </main>

    </div>
  );
}

export default App;