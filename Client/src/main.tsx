import { StrictMode } from 'react'
import { createRoot } from 'react-dom/client'
import './index.css'
import { createBrowserRouter, Navigate, RouterProvider } from 'react-router-dom'
import HomePage from './pages/HomePage'
import QuizPage from './pages/QuizPage'
import MainLayout from './layouts/MainLayout'

const router = createBrowserRouter([
  {
    path: '/',
    element: <MainLayout />,
    children: [
      {
        path: '',
        element: <Navigate to="/home" replace />
      },
      {
        path: 'home',
        element: <HomePage />,
      },
      {
        path: 'quiz',
        element: <QuizPage />
      }
    ],
  },
])

createRoot(document.getElementById('root')!).render(
  <StrictMode>
    <main>
      <RouterProvider router={router} />
    </main>
  </StrictMode>,
)
