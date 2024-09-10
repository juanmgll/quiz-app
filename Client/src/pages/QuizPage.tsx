import { FC, useEffect, useState } from "react";
import axios from "axios";
import { z } from "zod";
import Button from "../components/ButtonComponent";
import ModalComponent from "../components/ModalComponent";
import { useLocation, useNavigate } from "react-router-dom";

const AnswerSchema = z.object({
  id: z.number(),
  answerText: z.string(),
  isCorrect: z.boolean(),
  questionId: z.number(),
});

const QuestionSchema = z.object({
  id: z.number(),
  categoryId: z.number(),
  title: z.string(),
  answers: z.array(AnswerSchema),
});

type Question = z.infer<typeof QuestionSchema>;
type Answer = z.infer<typeof AnswerSchema>;

const QuizPage: FC = () => {
  const [questions, setQuestions] = useState<Question[]>([]);
  const [selectedAnswer, setSelectedAnswer] = useState<Answer | null>(null);
  const [isModalOpen, setIsModalOpen] = useState(false);
  const [correctAnswer, setCorrectAnswer] = useState<string | null>(null);

  // Get category from URL
  const { search } = useLocation();
  const params = new URLSearchParams(search);
  const category = params.get("category");

  const openModal = () => setIsModalOpen(true);

  const playAgain = () => {
    window.location.reload();
  };

  const navigate = useNavigate();

  const getQuestion = async () => {
    try {
      const response = await axios.get(
        `https://localhost:7022/api/Question/random/category/${category}`
      );
      const questionData = QuestionSchema.parse(response.data);

      setQuestions([questionData]);
      setSelectedAnswer(null);
      setCorrectAnswer(null); // Reset correct answer
      console.log(response);
    } catch (error) {
      console.error("Error fetching data:", error);
    }
  };

  const handleAnswerSelect = (answer: Answer) => {
    setSelectedAnswer(answer);
    if (answer.isCorrect) {
      getQuestion();
    } else {
      // Find the correct answer from the current question
      const correct =
        questions[0]?.answers.find((a) => a.isCorrect)?.answerText ||
        "No correct answer available";
      setCorrectAnswer(correct);
      openModal();
    }
  };

  useEffect(() => {
    getQuestion();
  }, []);

  return (
    <div className="h-screen w-screen flex flex-col overflow-hidden">
      <div className="flex flex-col h-screen w-screen justify-center items-center">
        {questions.map((question) => (
          <div key={question.id}>
            <span className="flex items-center justify-center text-4xl m-4">
              🤔
            </span>
            <p className="text-3xl font-semibold">{question.title}</p>
            <ul className="flex flex-col mt-8">
              {question.answers.map((answer) => (
                <div className="flex justify-center m-2" key={answer.id}>
                  <Button
                    text={answer.answerText}
                    onClick={() => handleAnswerSelect(answer)}
                    disabled={selectedAnswer !== null}
                  />
                </div>
              ))}
            </ul>
          </div>
        ))}
        <ModalComponent
          isOpen={isModalOpen}
          onClick={playAgain}
          title="You lost!"
          textModal={`The correct answer was: ${correctAnswer}`}
        />
      </div>
      <div className="m-4">
        <Button
          text="Go home"
          onClick={() => {
            navigate(`/home`);
          }}
        />
      </div>
    </div>
  );
};

export default QuizPage;
