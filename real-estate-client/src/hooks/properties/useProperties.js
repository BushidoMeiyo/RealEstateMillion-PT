import { useCallback } from "react";
import { usePropertyCtx } from "../../context/propertyContext";
import { getProperties, getPropertyDetails } from "../../services/propertyService";

export function useProperties() {
  const {
    filters, setFilters,
    properties, setProperties,
    loading, setLoading,
    selectedProperty, setSelectedProperty,
    showDetails, setShowDetails,
  } = usePropertyCtx();

  const search = useCallback(async (partial) => {
    setLoading(true);
    try {
      const next = partial ? { ...filters, ...partial } : filters;
      if (partial) setFilters(next);
      const items = await getProperties(next);
      setProperties(items);
    } finally {
      setLoading(false);
    }
  }, [filters, setFilters, setLoading, setProperties]);

  const openDetails = useCallback(async (id) => {
    setLoading(true);
    try {
      const details = await getPropertyDetails(id);
      setSelectedProperty(details);
      setShowDetails(true);
    } finally {
      setLoading(false);
    }
  }, [setLoading, setSelectedProperty, setShowDetails]);

  return { filters, properties, loading, selectedProperty, showDetails, search, openDetails };
}
