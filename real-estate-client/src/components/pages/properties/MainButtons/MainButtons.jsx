import React from 'react'
import Button from 'react-bootstrap/Button'
import { Spinner } from 'react-bootstrap'
import { useProperties } from "../../../../hooks/properties/useProperties";

const MainButtons = () => {
  const { search, loading } = useProperties()

  return (
    <div className="d-flex gap-3 mb-3">
      <Button
        variant="primary"
        className="d-flex align-items-center gap-2"
        onClick={search}
        disabled={loading}
      >
        {loading ? (
          <>
            <Spinner animation="border" size="sm" />
            Searching...
          </>
        ) : (
          <>
            <i className="bi bi-search" />
            Search
          </>
        )}
      </Button>
    </div>
  )
}

export default MainButtons
