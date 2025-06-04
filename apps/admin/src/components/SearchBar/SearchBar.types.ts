export type SearchBarProps = {
  placeholder?: string;
} & Omit<React.InputHTMLAttributes<HTMLInputElement>, "placeholder" | "type">;
