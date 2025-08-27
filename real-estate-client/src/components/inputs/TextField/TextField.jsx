export default function TextField({
  id,
  label,
  value,
  onChange,
  placeholder
}) {
  return (
    <div>
      <label htmlFor={id} className="form-label mb-1">{label}</label>
      <input
        id={id}
        type="text"
        className="form-control"
        value={value}
        onChange={(e) => onChange(e.target.value)}
        placeholder={placeholder}
      />
    </div>
  );
}