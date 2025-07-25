import React from 'react';

export type IButtonProps = {
  children: React.ReactNode;
  onClick?: () => void;
  style?: React.CSSProperties;
};

export const Button: React.FC<IButtonProps> = ({ children, onClick, style }) => (
  <button onClick={onClick} style={{ padding: '8px 16px', borderRadius: 4, background: '#007bff', color: '#fff', border: 'none', ...style }}>
    {children}
  </button>
);

