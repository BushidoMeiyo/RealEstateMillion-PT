import React from "react";
import "./CircularLoading.css";

export function CircularLoading ({ loading }) {
  if (!loading) return null;

  return (
    <div className="loading-backdrop d-flex justify-content-center align-items-center">
      <div className="spinner-border text-primary" role="status" style={{ width: "3rem", height: "3rem" }}>
        <span className="visually-hidden">Loading...</span>
      </div>
    </div>
  );
};

