import React from "react";
import "./Header.css"; 

const Header = ({ headerName }) => {
  return (
    <div className="bg-primary text-white py-3 px-4 mb-3 rounded shadow-sm">
      <h2 className="m-0">{headerName}</h2>
    </div>
  );
};

export default Header;
