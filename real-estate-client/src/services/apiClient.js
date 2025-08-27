
const API_BASE = "https://localhost:7186";

function buildQuery(params = {}) {
  const qp = new URLSearchParams();
  for (const [k, v] of Object.entries(params)) {
    if (v === null || v === undefined) continue;
    if (typeof v === "string" && v.trim() === "") continue;
    qp.append(k, v);
  }
  const qs = qp.toString();
  return qs ? `?${qs}` : "";
}

export function makeUrl(path = "", queryParams) {
  const cleanPath = `/${String(path).replace(/^\/+/, "")}`;
  return `${API_BASE}${cleanPath}${buildQuery(queryParams)}`;
}

export async function fetchJson(url, init = {}) {
  const res = await fetch(url, {
    ...init,
    headers: {
      Accept: "application/json",
      "Content-Type": "application/json",
      ...(init.headers || {}),
    },
  });

  if (!res.ok) {
    const msg = await res.text().catch(() => "");
    const err = new Error(`HTTP ${res.status} ${res.statusText} - ${msg}`);
    err.status = res.status;
    throw err;
  }
  return res.json();
}
