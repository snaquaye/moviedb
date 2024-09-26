import { Outlet } from 'react-router-dom';
import './App.css'

function App() {
  return (
    <div className="w-full p-4">
      <h1 className="text-2xl font-bold pb-10">
        <span className='text-blue-700'>Movie</span><span className='text-red-600'>Directory</span>
      </h1>
      <Outlet />
    </div>
  );
}

export default App
