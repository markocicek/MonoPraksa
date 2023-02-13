import React from 'react';
import ReactDOM from 'react-dom/client';
import './index.css';
import PostEmployee from './PostEmployee';
import reportWebVitals from './reportWebVitals';
import SearchEmployee from './SearchEmployee';
import ListEmployees from './ListEmployees';

const postEmp = ReactDOM.createRoot(document.getElementById('postEmployee'));
const searchEmp = ReactDOM.createRoot(document.getElementById('searchEmployee'));
const listEmps = ReactDOM.createRoot(document.getElementById('listEmployees'));


class Clock extends React.Component {
  constructor(props) {
      super(props);
      this.state = { date: new Date() };
  }

  componentDidMount() {
      this.timerID = setInterval(
          () => this.tick(),
          1000
      );
  }

  componentWillUnmount() {
      clearInterval(this.timerID);
  }

  tick() {
      this.setState({
          date: new Date()
      });
  }

  render() {
      return (
          <div>
              <h2>It is {this.state.date.toLocaleTimeString()}.</h2>
          </div>
      );
  }
}

postEmp.render(
  <React.StrictMode>
    <PostEmployee />
  </React.StrictMode>
);
searchEmp.render(
  <React.StrictMode>
    <SearchEmployee />
  </React.StrictMode>
);
listEmps.render(
  <React.StrictMode>
    <ListEmployees />
  </React.StrictMode>
);
const clock = ReactDOM.createRoot(document.getElementById('clock'));

clock.render(<Clock />);

// If you want to start measuring performance in your app, pass a function
// to log results (for example: reportWebVitals(console.log))
// or send to an analytics endpoint. Learn more: https://bit.ly/CRA-vitals
reportWebVitals();
