import React from "react";
import DatePicker from "react-datepicker";

let TextInput = ({
  divClass,
  labelClass,
  labelFor,
  labelText,
  placeholder,
  inputClass,
  inputId,
  inputType,
  inputName,
  onChange,
  error,
  errorClass,
  value,
}) => {
  return (
    <div className={divClass}>
      <label className={labelClass} htmlFor={labelFor}>
        {labelText}
      </label>
      <input
        name={inputName}
        onChange={onChange}
        placeholder={placeholder}
        type={inputType}
        className={inputClass}
        id={inputId}
        value={value}
      />
      {error && <div className={errorClass}>{error}</div>}
    </div>
  );
};

let InputForDate = ({
  name,
  lable = "",
  errors,
  value,
  onChange,
  minDate,
  ...rest
}) => {
  return (
    <div className="form-group">
      <div className="react-datepicker-wrapper">
        <DatePicker
          {...rest}
          onChange={onChange}
          selected={value}
          name={name}
          id={name}
          minDate={minDate}
          className="form-control"
          //dateFormat="MMMM d, yyyy H:MM "
          dateFormat="MMMM d, yyyy"
        />
        {errors && <div className="alert alert-danger">{errors}</div>}
      </div>
    </div>
  );
};
export { TextInput, InputForDate };
