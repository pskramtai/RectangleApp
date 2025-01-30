import logo from './logo.svg';
import './App.css';
import React from "react";
import RectangleDrawer from "./components/RectangleDrawer";

function App() {
  return (
    <div className="App">
      <div>
        <h1>Rectangle App</h1>
        <RectangleDrawer />
      </div>
    </div>
  );
}

export default App;
