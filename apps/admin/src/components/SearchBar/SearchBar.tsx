import ReactDOM from "react-dom";
import { TextField } from "@radix-ui/themes";
import { MagnifyingGlassIcon } from "@radix-ui/react-icons";
import { SearchBarProps } from "./SearchBar.types";

import classNames from "./SearchBar.module.css";

export const SearchBar = ({ placeholder, onChange }: SearchBarProps) => {
  const slot = document.getElementById("search-bar-slot");

  return (
    slot &&
    ReactDOM.createPortal(
      <TextField.Root
        className={classNames.root}
        placeholder={placeholder}
        onChange={onChange}
        size="3"
      >
        <TextField.Slot>
          <MagnifyingGlassIcon height="25" width="25" />
        </TextField.Slot>
      </TextField.Root>,
      slot
    )
  );
};
