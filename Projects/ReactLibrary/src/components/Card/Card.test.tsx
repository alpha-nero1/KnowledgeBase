import React from 'react';
import { render, screen, fireEvent } from '@testing-library/react';
import Card from './Card';

describe('Card', () => {
  it('renders card with content', () => {
    render(<Card>Test content</Card>);
    expect(screen.getByText('Test content')).toBeInTheDocument();
  });

  it('renders card with title and subtitle', () => {
    render(
      <Card title="Card Title" subtitle="Card Subtitle">
        Test content
      </Card>
    );
    
    expect(screen.getByText('Card Title')).toBeInTheDocument();
    expect(screen.getByText('Card Subtitle')).toBeInTheDocument();
  });

  it('applies correct variant classes', () => {
    const { rerender } = render(<Card variant="elevated">Content</Card>);
    expect(screen.getByText('Content').parentElement).toHaveClass('rl-card--elevated');

    rerender(<Card variant="outlined">Content</Card>);
    expect(screen.getByText('Content').parentElement).toHaveClass('rl-card--outlined');
  });

  it('applies correct padding classes', () => {
    const { rerender } = render(<Card padding="small">Content</Card>);
    expect(screen.getByText('Content').parentElement).toHaveClass('rl-card--padding-small');

    rerender(<Card padding="large">Content</Card>);
    expect(screen.getByText('Content').parentElement).toHaveClass('rl-card--padding-large');
  });

  it('handles click events when clickable', () => {
    const handleClick = jest.fn();
    render(<Card clickable onClick={handleClick}>Clickable card</Card>);
    
    const card = screen.getByText('Clickable card').parentElement;
    fireEvent.click(card!);
    
    expect(handleClick).toHaveBeenCalledTimes(1);
  });

  it('applies clickable class and attributes when clickable', () => {
    render(<Card clickable>Clickable content</Card>);
    
    const card = screen.getByText('Clickable content').parentElement;
    expect(card).toHaveClass('rl-card--clickable');
    expect(card).toHaveAttribute('role', 'button');
    expect(card).toHaveAttribute('tabIndex', '0');
  });

  it('does not render header when no title or subtitle', () => {
    render(<Card>Just content</Card>);
    
    const headerElement = screen.queryByText('Just content')?.parentElement?.querySelector('.rl-card-header');
    expect(headerElement).not.toBeInTheDocument();
  });
});
