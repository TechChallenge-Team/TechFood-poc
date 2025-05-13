import { NumericFormat } from "react-number-format";
import { Text, TextField } from "@radix-ui/themes";

interface CurrencyInputProps {
  value: number | undefined;
  onChange: (value: number | undefined) => void;
  error?: string;
  id: string;
  name: string;
}

export function CurrencyInput({
  value,
  onChange,
  error,
  id,
}: CurrencyInputProps) {
  return (
    <div>
      <NumericFormat
        id={id}
        value={value ?? undefined}
        thousandSeparator="."
        decimalSeparator=","
        prefix="R$ "
        decimalScale={2}
        fixedDecimalScale
        allowNegative={false}
        customInput={TextField.Root}
        placeholder="R$ 0,00"
        onValueChange={(values) => {
          const floatValue = values.floatValue;
          onChange(floatValue ?? undefined);
        }}
      />
      {error && <Text color="red">{error}</Text>}
    </div>
  );
}
