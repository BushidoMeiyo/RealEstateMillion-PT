import React from "react";
import PropertyProvider from "../../context/propertyProvider.jsx";
import Header from "../../components/layaout/header/header.jsx";
import BoxContainer from "../../components/layaout/BoxContainer/BoxContainer.jsx";
import ButtonsLayout from "../../components/layaout/buttons/ButtonsLayout.jsx";
import MainButtons from "../../components/pages/properties/MainButtons/MainButtons.jsx";
import MainFilters from "../../components/pages/properties/MainFilters/MainFilters.jsx";
import { CircularLoading } from '../../components/shared/CircularLoading/CircularLoading.jsx';
import MainGrid from "../../components/pages/properties/MainGrid/MainGrid.jsx";
import PropertyDetailsModal from "../../components/pages/properties/MainModal/PropertyDetailsModal.jsx";
import { ToastContainer } from "react-toastify";
import { usePropertyCtx } from "../../context/propertyContext.js";
import { Link } from "react-router-dom";

const InnerLoading = () => {
  const { loading } = usePropertyCtx();
  return <CircularLoading loading={loading} />;
};

const PropertyPage = () => {
  return (
    <BoxContainer>
      <PropertyProvider>
        <div className="mb-3">
          <Link to="/" className="btn btn-secondary">
            ← Back to Home
          </Link>
        </div>
        <InnerLoading />
        <ToastContainer />
        <Header headerName="Properties" />
        <ButtonsLayout>
          <MainButtons />
        </ButtonsLayout>
        <MainFilters />
        <MainGrid />
        <PropertyDetailsModal />
      </PropertyProvider>
    </BoxContainer>
  );
};

export default PropertyPage;
