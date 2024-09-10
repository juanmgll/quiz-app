import { useNavigate } from "react-router-dom";
import { CardComponent } from "../components/CardComponent";

const HomePage = () => {
  const navigate = useNavigate();

  const play = (category: number) => {
    navigate(`/quiz?category=${category}`);
  };

  return (
    <div className="flex flex-col h-screen w-screen overflow-hidden">
      <div className="flex items-center justify-center py-4">
        <h1 className="text-4xl font-bold">Quiz Game</h1>
      </div>

      {/* Contenedor para las tarjetas */}
      <div className="flex flex-grow items-center justify-center">
        <div className="flex flex-row">
          <div className="m-4">
            <CardComponent icon="📖" title="History" onClick={() => play(1)} />
          </div>
          <div className="m-4">
            <CardComponent
              icon="🌍"
              title="Geography"
              onClick={() => play(2)}
            />
          </div>
          <div className="m-4">
            <CardComponent
              icon="🎈"
              title="General culture"
              onClick={() => play(3)}
            />
          </div>
        </div>
      </div>
    </div>
  );
};

export default HomePage;
