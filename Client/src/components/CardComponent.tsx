import { FC } from "react";
import ButtonComponent from "./ButtonComponent";

interface CardProps
{
    icon: string;
    title: string;
    onClick: () => void;
}

export const CardComponent: FC<CardProps> = ({icon, title, onClick}) => {
  return (
    <div className="flex flex-col items-center border-2 border-black p-4">
        <p className="text-4xl">{icon}</p>
        <h2>{title}</h2>
        <ButtonComponent text="Start" onClick={onClick} />
    </div>
  )
}
