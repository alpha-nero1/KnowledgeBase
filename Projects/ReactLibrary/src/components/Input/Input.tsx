import React from 'react';
import './Input.css';

export interface InputProps {
  /**
   * Input label
   */
  label?: string;
  /**
   * Input placeholder
   */
  placeholder?: string;
  /**
   * Input value
   */
  value?: string;
  /**
   * Input type
   */
  type?: 'text' | 'email' | 'password' | 'number' | 'tel' | 'url';
  /**
   * Input size
   */
  size?: 'small' | 'medium' | 'large';
  /**
   * Is input disabled?
   */
  disabled?: boolean;
  /**
   * Is input required?
   */
  required?: boolean;
  /**
   * Error message
   */
  error?: string;
  /**
   * Help text
   */
  helpText?: string;
  /**
   * Change handler
   */
  onChange?: (event: React.ChangeEvent<HTMLInputElement>) => void;
  /**
   * Additional CSS classes
   */
  className?: string;
  /**
   * Input ID
   */
  id?: string;
}

const Input: React.FC<InputProps> = ({
  label,
  placeholder,
  value,
  type = 'text',
  size = 'medium',
  disabled = false,
  required = false,
  error,
  helpText,
  onChange,
  className = '',
  id,
  ...props
}) => {
  const baseClass = 'rl-input';
  const sizeClass = `${baseClass}--${size}`;
  const errorClass = error ? `${baseClass}--error` : '';
  const disabledClass = disabled ? `${baseClass}--disabled` : '';
  
  const inputClass = [
    baseClass,
    sizeClass,
    errorClass,
    disabledClass,
    className
  ].filter(Boolean).join(' ');

  const inputId = id || (label ? label.toLowerCase().replace(/\s+/g, '-') : undefined);

  return (
    <div className={`${baseClass}-wrapper`}>
      {label && (
        <label htmlFor={inputId} className={`${baseClass}-label`}>
          {label}
          {required && <span className={`${baseClass}-required`}>*</span>}
        </label>
      )}
      <input
        id={inputId}
        type={type}
        className={inputClass}
        placeholder={placeholder}
        value={value}
        disabled={disabled}
        required={required}
        onChange={onChange}
        {...props}
      />
      {error && <div className={`${baseClass}-error-message`}>{error}</div>}
      {helpText && !error && <div className={`${baseClass}-help-text`}>{helpText}</div>}
    </div>
  );
};

export default Input;
