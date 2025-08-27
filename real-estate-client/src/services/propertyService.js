import { makeUrl, fetchJson } from "./apiClient";

export async function getProperties(filters = {}) {
  const { name, address, minPrice, maxPrice } = filters;

  const query = {};
  if (name) query.name = name;
  if (address) query.address = address;
  if (minPrice !== "" && minPrice !== null && minPrice !== undefined) {
    query.minPrice = Number(minPrice);
  }
  if (maxPrice !== "" && maxPrice !== null && maxPrice !== undefined) {
    query.maxPrice = Number(maxPrice);
  }

  const url = makeUrl("/api/v1/Properties", query);
  const list = await fetchJson(url);
  return Array.isArray(list) ? list : [];
}

export async function getPropertyDetails(idProperty) {
  if (!idProperty) throw new Error("idProperty es requerido");
  const url = makeUrl(`/api/v1/Properties/${encodeURIComponent(idProperty)}`);
  const details = await fetchJson(url);

  return {
    property: details?.property ?? null,
    owner: details?.owner ?? null,
    images: Array.isArray(details?.images) ? details.images : [],
    traces: Array.isArray(details?.traces) ? details.traces : [],
  };
}
