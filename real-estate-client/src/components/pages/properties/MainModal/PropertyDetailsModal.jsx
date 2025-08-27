import React from "react";
import { Modal, Button, Card, ListGroup } from "react-bootstrap";
import { usePropertyCtx } from "../../../../context/propertyContext"; // o usar useProperties()

const PropertyDetailsModal = () => {
  const { showDetails, setShowDetails, selectedProperty, setSelectedProperty } = usePropertyCtx();
  
  const handleClose = () => {
    setShowDetails(false);
    setSelectedProperty(null);  // opcional: limpiar datos al cerrar
  };

  return (
    <Modal show={showDetails} onHide={handleClose} size="lg" backdrop="static">
      <Modal.Header closeButton>
        <Modal.Title>Detalles de Propiedad</Modal.Title>
      </Modal.Header>

      <Modal.Body>
        {selectedProperty && (
          <Card>
            {/* Imagen principal de la propiedad, si existe */}
            {selectedProperty.property.image && (
              <Card.Img 
                variant="top" 
                src={`data:image/jpeg;base64,${selectedProperty.property.image}`} 
                alt="Imagen de la Propiedad" 
              />
            )}
            <Card.Body>
              <Card.Title>{selectedProperty.property.name}</Card.Title>
              <Card.Text>
                <strong>Dirección:</strong> {selectedProperty.property.address}<br/>
                <strong>Precio:</strong> {selectedProperty.property.price}<br/>
                <strong>Código Interno:</strong> {selectedProperty.property.codeInternal}<br/>
                <strong>Año:</strong> {selectedProperty.property.year}
              </Card.Text>
            </Card.Body>
            <ListGroup variant="flush">
              <ListGroup.Item>
                <strong>Propietario:</strong> {selectedProperty.owner.name}
              </ListGroup.Item>
              <ListGroup.Item>
                <strong>Dirección (Propietario):</strong> {selectedProperty.owner.address}
              </ListGroup.Item>
              <ListGroup.Item>
                <strong>Fecha de Nacimiento:</strong> {selectedProperty.owner.birthday}
              </ListGroup.Item>
            </ListGroup>
          </Card>
        )}

        {/* Mostrar imágenes adicionales, si existen */}
        {selectedProperty?.images?.length > 0 && (
          <div className="mt-3">
            <h5>Imágenes:</h5>
            {selectedProperty.images.map(img => (
              <img 
                key={img.idPropertyImage} 
                src={`data:image/jpeg;base64,${img.file}`} 
                alt="Propiedad" 
                style={{ width: "100px", marginRight: "10px", marginBottom: "10px" }} 
              />
            ))}
          </div>
        )}

        {/* Mostrar historial (traces) si existen */}
        {selectedProperty?.traces?.length > 0 && (
          <div className="mt-3">
            <h5>Historial de Precios:</h5>
            <ul>
              {selectedProperty.traces.map(trace => (
                <li key={trace.idPropertyTrace}>
                  {trace.name} – <strong>Fecha:</strong> {new Date(trace.dateSale).toLocaleDateString()} – 
                  <strong>Valor:</strong> {trace.value} – <strong>Impuesto:</strong> {trace.tax}
                </li>
              ))}
            </ul>
          </div>
        )}
      </Modal.Body>

      <Modal.Footer>
        <Button variant="secondary" onClick={handleClose}>Cerrar</Button>
      </Modal.Footer>
    </Modal>
  );
};

export default PropertyDetailsModal;
