import { Outlet } from 'react-router-dom'


const MainLayout = () => {
  return (
    <div className='flex min-h-screen w-full bg-yellow-50'>
        <div>
            <Outlet />
        </div>
    </div>
  )
}

export default MainLayout