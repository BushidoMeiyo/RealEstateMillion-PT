import React from "react";
import TextField from "../../../inputs/TextField/TextField";
import NumberField from "../../../inputs/NumberField/NumberField";
import { usePropertyCtx } from "../../../../context/propertyContext";

const MainFilters = () => {
  const { filters, setFilters } = usePropertyCtx();

  const handleChange = (key) => (value) => {
    setFilters({ ...filters, [key]: value });
  };

  return (
    <div className="container py-3">
      <div className="bg-white rounded shadow-sm p-4">
        <div className="row g-3">
          <div className="col-12 col-sm-6 col-md-3">
            <TextField
              id="name"
              label="Property Name"
              value={filters.name}
              onChange={handleChange("name")}
              placeholder="Enter property name"
            />
          </div>

          <div className="col-12 col-sm-6 col-md-3">
            <TextField
              id="address"
              label="Address"
              value={filters.address}
              onChange={handleChange("address")}
              placeholder="Enter address"
            />
          </div>

          <div className="col-12 col-sm-6 col-md-3">
            <NumberField
              id="minPrice"
              label="Min Price"
              value={filters.minPrice}
              onChange={handleChange("minPrice")}
              placeholder="0"
            />
          </div>

          <div className="col-12 col-sm-6 col-md-3">
            <NumberField
              id="maxPrice"
              label="Max Price"
              value={filters.maxPrice}
              onChange={handleChange("maxPrice")}
              placeholder="1000000"
            />
          </div>
        </div>
      </div>
    </div>
  );
};

export default MainFilters;
