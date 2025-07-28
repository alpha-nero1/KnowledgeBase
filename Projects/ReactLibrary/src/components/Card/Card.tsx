import React from 'react';
import './Card.css';

export interface CardProps {
  /**
   * Card content
   */
  children: React.ReactNode;
  /**
   * Card title
   */
  title?: string;
  /**
   * Card subtitle
   */
  subtitle?: string;
  /**
   * Card variant
   */
  variant?: 'default' | 'elevated' | 'outlined';
  /**
   * Card padding
   */
  padding?: 'none' | 'small' | 'medium' | 'large';
  /**
   * Is card clickable?
   */
  clickable?: boolean;
  /**
   * Click handler
   */
  onClick?: (event: React.MouseEvent<HTMLDivElement>) => void;
  /**
   * Additional CSS classes
   */
  className?: string;
}

const Card: React.FC<CardProps> = ({
  children,
  title,
  subtitle,
  variant = 'default',
  padding = 'medium',
  clickable = false,
  onClick,
  className = '',
  ...props
}) => {
  const baseClass = 'rl-card';
  const variantClass = `${baseClass}--${variant}`;
  const paddingClass = `${baseClass}--padding-${padding}`;
  const clickableClass = clickable ? `${baseClass}--clickable` : '';
  
  const cardClass = [
    baseClass,
    variantClass,
    paddingClass,
    clickableClass,
    className
  ].filter(Boolean).join(' ');

  return (
    <div
      className={cardClass}
      onClick={clickable ? onClick : undefined}
      role={clickable ? 'button' : undefined}
      tabIndex={clickable ? 0 : undefined}
      {...props}
    >
      {(title || subtitle) && (
        <div className={`${baseClass}-header`}>
          {title && <h3 className={`${baseClass}-title`}>{title}</h3>}
          {subtitle && <p className={`${baseClass}-subtitle`}>{subtitle}</p>}
        </div>
      )}
      <div className={`${baseClass}-content`}>
        {children}
      </div>
    </div>
  );
};

export default Card;
