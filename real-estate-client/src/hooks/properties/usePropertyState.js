import { useState, useMemo } from "react";

function usePropertyState() {

  const [filters, setFilters] = useState({
    name: "",
    address: "",
    minPrice: "",
    maxPrice: "",
  });

  const [properties, setProperties] = useState([]);
  const [loading, setLoading] = useState(false);

  const [selectedProperty, setSelectedProperty] = useState(null); 
  const [showDetails, setShowDetails] = useState(false);

  return useMemo(
    () => ({
      filters, setFilters,
      properties, setProperties,
      loading, setLoading,
      selectedProperty, setSelectedProperty,
      showDetails, setShowDetails,
    }),
    [
      filters, properties, loading, 
      selectedProperty, showDetails
    ]
  );
}

export { usePropertyState };