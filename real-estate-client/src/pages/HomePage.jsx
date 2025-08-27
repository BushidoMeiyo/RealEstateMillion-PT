import React from "react";
import { Link } from "react-router-dom";

const HomePage = () => {
  return (
    <div className="container mt-5">
      <h1>Welcome to the Real Estate Million</h1>
      <p>Click the button below to view the registered properties:</p>
      <Link to="/properties" className="btn btn-primary">
        View Properties
      </Link>
    </div>
  );
};

export default HomePage;
