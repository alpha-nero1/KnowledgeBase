declare module 'remote_app/Button' {
  const Button: React.FC<{
    text: string;
    onClick?: () => void;
  }>;
  export default Button;
}

declare module 'remote_app/Counter' {
  const Counter: React.FC<{
    count: number;
    onIncrement: () => void;
    onDecrement: () => void;
  }>;
  export default Counter;
}