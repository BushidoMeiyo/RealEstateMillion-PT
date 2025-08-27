import React from "react";

const ButtonsLayout = ({ children }) => {
  return (
    <div className="d-flex gap-2 mt-2 px-3 mb-4">
      {children}
    </div>
  );
};

export default ButtonsLayout;