import React, { useMemo } from "react";
import { AgGridReact } from 'ag-grid-react';
import { ModuleRegistry, AllCommunityModule } from 'ag-grid-community';

import 'ag-grid-community/styles/ag-grid.css';
import 'ag-grid-community/styles/ag-theme-alpine.css';
ModuleRegistry.registerModules([AllCommunityModule]);

import { BsEye } from "react-icons/bs";
import { useProperties } from "../../../../hooks/properties/useProperties";


const MainGrid = () => {
  const { properties, openDetails } = useProperties();

  const columnDefs = useMemo(() => [
    {
      headerName: "",
      field: "view",
      width: 60,
      cellRenderer: (params) => {
        return (
          <BsEye
            size={18}
            color="#0d6efd"
            style={{ cursor: "pointer" }}
            title="View Details"
            onClick={() => openDetails(params.data.idProperty)}
          />
        );
      },
      suppressMenu: true,
      sortable: false,
      filter: false,
    },
    { headerName: "Name", field: "name", flex: 1 },
    { headerName: "Address", field: "address", flex: 1 },
    { headerName: "Price", field: "price", flex: 1 },
    { headerName: "Code Internal", field: "codeInternal", flex: 1 },
    { headerName: "Year", field: "year", flex: 1 },
    { headerName: "Owner ID", field: "idOwner", flex: 1 },
  ], [openDetails]);

  const defaultColDef = useMemo(() => ({
    resizable: true,
    sortable: true,
    filter: true,
  }), []);

  return (
    <div className="ag-theme-alpine mt-4" style={{ height: 500, width: "100%" }}>
      <AgGridReact
        rowData={properties}
        columnDefs={columnDefs}
        defaultColDef={defaultColDef}
        domLayout="autoHeight"
      />
    </div>
  );
};

export default MainGrid;
