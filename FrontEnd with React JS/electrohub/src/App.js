import { BrowserRouter } from 'react-router-dom';
import './App.css';
import PublicRoutes from './routes/PublicRoutes';

function App() {
  return (
    <div  className='App'>
      <BrowserRouter>
         <PublicRoutes/>
      </BrowserRouter>
    </div>
  );
}

export default App;
