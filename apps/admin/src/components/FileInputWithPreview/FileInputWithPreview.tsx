import { Button, Text, Flex, Box, Card } from "@radix-ui/themes";
import * as Label from "@radix-ui/react-label";
import { useState, useRef, useEffect } from "react";

interface FileInputWithPreviewProps {
  value?: FileList;
  onChange: (files: FileList | undefined) => void;
  error?: any;
  name: string;
}

export function FileInputWithPreview({
  value,
  onChange,
  error,
  name,
}: FileInputWithPreviewProps) {
  const [previewUrl, setPreviewUrl] = useState<string | null>(null);
  const inputRef = useRef<HTMLInputElement | null>(null);

  const file = value?.[0];
  const fileName = file?.name ?? "";

  // Atualiza o preview sempre que o arquivo mudar
  useEffect(() => {
    if (file) {
      const objectUrl = URL.createObjectURL(file);
      setPreviewUrl(objectUrl);

      // Limpa o objeto da memória quando não for mais usado
      return () => URL.revokeObjectURL(objectUrl);
    } else {
      setPreviewUrl(null);
    }
  }, [file]);

  const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const files = e.target.files;
    onChange(files && files.length > 0 ? files : undefined);
  };

  const handleClear = () => {
    setPreviewUrl(null);
    onChange(undefined); // mais seguro que forçar um FileList vazio
    if (inputRef.current) {
      inputRef.current.value = "";
    }
  };

  return (
    <Flex direction="column" gap="2">
      <Flex gap="2" align="center">
        <Button asChild>
          <Label.Root htmlFor={name} style={{ cursor: "pointer" }}>
            Escolher Imagem
          </Label.Root>
        </Button>

        {file && (
          <Button
            color="red"
            variant="soft"
            onClick={handleClear}
            type="button"
          >
            Remover
          </Button>
        )}
      </Flex>

      <input
        id={name}
        type="file"
        accept="image/*"
        onChange={handleChange}
        ref={inputRef}
        style={{ display: "none" }}
      />

      {fileName && <Text size="1">Selecionado: {fileName}</Text>}

      {previewUrl && (
        <Card style={{ width: 160, height: 160, overflow: "hidden" }}>
          <img
            src={previewUrl}
            alt="Pré-visualização"
            style={{ objectFit: "cover", width: "100%", height: "100%" }}
          />
        </Card>
      )}

      {error && <Text color="red">{error.message}</Text>}
    </Flex>
  );
}
