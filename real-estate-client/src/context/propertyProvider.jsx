import { PropertyContext } from "./propertyContext";
import { usePropertyState } from "../hooks/properties/usePropertyState";

export default function PropertyProvider({ children }) {
  const value = usePropertyState(); 
  return (
    <PropertyContext.Provider value={value}>
      {children}
    </PropertyContext.Provider>
  );
}
