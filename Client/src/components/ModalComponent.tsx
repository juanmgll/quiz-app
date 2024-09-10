import { FC } from "react";
import Button from "./ButtonComponent";

interface ModalProps {
  isOpen: boolean;
  onClick: () => void;
  title?: string;
  textModal: string;
}

const ModalComponent: FC<ModalProps> = ({ isOpen, onClick, title, textModal,  }) => {
  if (!isOpen) return null;

  return (
    <div className="fixed inset-0 z-50 flex items-center justify-center bg-red-500/25">
      <div className="flex flex-col items-center bg-yellow-50 p-6 rounded-lg max-w-lg w-full relative">
        {title && <h2 className="text-3xl font-semibold mb-4">{title}</h2>}
        <p>{textModal}</p>
        <div className="m-4">
            <Button text="Play again" onClick={onClick} />
        </div>
      </div>
    </div>
  );
};

export default ModalComponent;
