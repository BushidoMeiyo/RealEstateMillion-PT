export default function NumberField({
  id,
  label,
  value,
  onChange,
  placeholder,
  min = 0,
  className = "",
}) {
  return (
    <div className={`mb-2 ${className}`}>
      <label htmlFor={id} className="form-label mb-1">{label}</label>
      <input
        id={id}
        type="number"
        className="form-control"
        value={value}
        min={min}
        onChange={(e) => onChange(e.target.value)}
        placeholder={placeholder}
      />
    </div>
  );
}
