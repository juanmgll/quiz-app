interface ButtonProps {
  text: string;
  onClick?: () => void;
  disabled?: boolean;
}

const Button: React.FC<ButtonProps> = ({ text, onClick, disabled }) => {
  return (
    <button
      className="w-60 border-2 border-black bg-orange-300  text-xl font-semibold rounded-lg px-8 py-1 hover:bg-white"
      onClick={onClick}
      disabled={disabled}
    >
      {text}
    </button>
  );
};

export default Button;
