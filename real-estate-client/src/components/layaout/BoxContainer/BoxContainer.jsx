import React from "react";

function BoxContainer({ children }) {
  return (
    <div className="box-container" style={{ maxWidth: '1200px', margin: '0 auto' }}>
      {children}
    </div>
  );
}

export default BoxContainer;