import React from 'react';
import './App.css'
import { Routes, Route } from "react-router-dom";
import PropertyPage from './pages/property/propertyPages.jsx';
import HomePage from "./pages/HomePage"; 
import NotFound from "./pages/NotFound"; 

const App = () => {
  return (
    <Routes>
      <Route path="/" element={<HomePage />} />
      <Route path="/properties" element={<PropertyPage />} />
      <Route path="*" element={<NotFound />} />
    </Routes>
  );
};

export default App;
