import { createContext, useContext } from "react";

export const PropertyContext = createContext(null);

export function usePropertyCtx() {
  const ctx = useContext(PropertyContext);
  if (!ctx) {
    throw new Error("usePropertyCtx must be used within <PropertyProvider />");
  }
  return ctx;
}